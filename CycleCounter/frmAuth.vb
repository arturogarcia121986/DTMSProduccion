Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Public Class frmAuth
    Dim _depto, checkStopDepto, _frm As String
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Public Sub New(department As String, Optional stopDepto As String = "")

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _depto = department
        checkStopDepto = stopDepto
    End Sub

    Public Sub New(frm As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _frm = frm
    End Sub

    Public Structure datosUsuario
        Public userAuthenticated As String
    End Structure

    Public iResult As datosUsuario
    Private Sub frmAuth_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tbScan.Select()
        tbScan.Focus()
    End Sub

    Private _ClosedByUser As Boolean = False 'si el usuario se autentica = false , si cierra el formulario devuelve true


    ''' <summary>
    ''' CONEXION A HISS BD
    ''' </summary>
    ''' <returns></returns>

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CanceladoPorElUsuario = True
        Success = False
        Me.Close()
    End Sub

    Private _Success As Boolean = False
    Public Property Success As Boolean
        Get
            Return _Success
        End Get
        Set(ByVal value As Boolean)
            _Success = value
        End Set
    End Property

    Private _isSuperAdmin As Boolean = False
    Public Property isSuperAdmin As Boolean
        Get
            Return _isSuperAdmin
        End Get
        Set(ByVal value As Boolean)
            _isSuperAdmin = value
        End Set
    End Property

    Private _isSuperAdminClose As Boolean = False
    Public Property isSuperAdminClose As Boolean
        Get
            Return _isSuperAdminClose
        End Get
        Set(ByVal value As Boolean)
            _isSuperAdminClose = value
        End Set
    End Property

    Public Property CanceladoPorElUsuario As Boolean
        Get
            Return _ClosedByUser
        End Get
        Set(ByVal value As Boolean)
            _ClosedByUser = value
        End Set
    End Property

    Private Sub auth(Optional sAdmin As String = "")
        Try
            If _frm = "superAdmin" Then
                'superAdmin solo es para cuando se daran mantenimientos
                Dim userAdmin As String = QueryRow("SELECT * FROM t_usuarios WHERE codeSupervisor='" & tbScan.Text & "' and supervisor=1 and superAdmin=1", "usuario", "frmAutenticar.btnChecar_Click(..")
                If userAdmin <> "" Then
                    Success = True
                    isSuperAdmin = True
                    Me.Close()
                    Exit Sub
                Else
                    MessageBox.Show("El usuario no tiene permisos de Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tbScan.Text = ""
                    tbScan.Focus()
                    Exit Sub
                End If
            ElseIf _frm = "superAdminClose" Then
                'un superAdmin solo puede cerrar el mantenimiento
                Dim userAdmin As String = QueryRow("SELECT * FROM t_usuarios WHERE codeSupervisor='" & tbScan.Text & "' and supervisor=1 and superAdmin=1", "usuario", "frmAutenticar.btnChecar_Click(..")
                If userAdmin <> "" Then
                    Success = True
                    isSuperAdmin = False
                    isSuperAdminClose = True
                    Me.Close()
                    Exit Sub
                Else
                    MessageBox.Show("El usuario no tiene permisos de Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tbScan.Text = ""
                    tbScan.Focus()
                    Exit Sub
                End If
            ElseIf _frm = "closeSystem" Then
                'solo un usuario con permiso para cerrar, puede cerrar el sistema
                Dim userAdmin As String = QueryRow("SELECT * FROM t_usuarios WHERE codeSupervisor='" & tbScan.Text & "' and supervisor=1 and closeSystem=1", "usuario", "frmAutenticar.closeSystem(..")
                If userAdmin <> "" Then
                    Success = True
                    Me.Close()
                    Exit Sub
                Else
                    MessageBox.Show("El usuario no tiene permisos de Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tbScan.Text = ""
                    tbScan.Focus()
                    Exit Sub
                End If
            End If


            Dim userAuth As String = QueryRow("SELECT * FROM t_usuarios WHERE codeSupervisor='" & tbScan.Text & "' and supervisor=1", "usuario", "frmAutenticar.btnChecar_Click(..")
            If (userAuth <> "") Then
                If _depto = "STOP" Then
                    Dim userDepto As String = QueryRow("SELECT * FROM t_usuarios WHERE depto='" & checkStopDepto & "' and usuario='" & userAuth & "'", "depto", "frmAutenticar.btnChecar_Click(..")
                    If userDepto = checkStopDepto Then
                        Call Ejecuta("UPDATE t_usuarios SET lastSession='" & GetCurrentDateTime() & "' WHERE usuario='" & userAuth & "'", "UPDATE")
                        Success = True
                        iResult.userAuthenticated = userAuth
                        Me.Close()
                    Else
                        MessageBox.Show("El usuario no corresponde al departamento solicitado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbScan.Text = ""
                        tbScan.Select()
                        tbScan.Focus()
                        Exit Sub
                    End If
                Else
                    Dim useruserDepto As String = QueryRow("SELECT * FROM t_usuarios WHERE depto='" & _depto & "' and usuario='" & userAuth & "'", "depto", "frmAutenticar.btnChecar_Click(..")
                    If useruserDepto = _depto Then
                        Call Ejecuta("UPDATE t_usuarios SET lastSession='" & GetCurrentDateTime() & "' WHERE usuario='" & userAuth & "'", "UPDATE")
                        Success = True
                        iResult.userAuthenticated = userAuth
                        Me.Close()
                    Else
                        MessageBox.Show("El usuario no corresponde al departamento solicitado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbScan.Text = ""
                        tbScan.Select()
                        tbScan.Focus()
                        Exit Sub
                    End If
                End If
            Else
                MessageBox.Show("User not found, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                tbScan.Text = ""
                tbScan.Select()
                tbScan.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tbScan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbScan.KeyPress
        If (Asc(e.KeyChar) = 13) Then
            auth()
        End If
    End Sub
End Class