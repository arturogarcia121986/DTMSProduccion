
Imports System.Data.SqlClient

Public Class Class_Usuarios
    ' Inherits VB6_ComboBox
    Private cmd As SqlCommand = Nothing
    Private Adapter As SqlDataAdapter = Nothing
    Private ds As DataSet = Nothing
    Public Structure datos
        Public id As String
        Public usuario As String
        Public preq As String
        Public activo As String
        Public idSuc As String
        Public claveSuc As String
        Public sucursal As String
        Public IsLogged As Boolean 'GP (General Purpose)
        Public Password As String
        Public depto As String
        Public email As String
        Public tipo As String      'tipo de usuario, administrador,usuario,sucursal.
    End Structure
    Public Sub LoadUsersInComboBox(ByVal cboDestino As ComboBox, ByVal m_sql As String)

        Try
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    cboDestino.Items.Add(rw("usuario").ToString)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : LoadInComboBox(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Sub
    Public Function GetInfo(ByVal m_usuario As String) As datos

        Dim iresult As datos = Nothing

        Try
            Dim m_sql As String = "SELECT usuario, pass, tipo, id,email,depto FROM t_usuarios WHERE usuario= '" & m_usuario & "'"

            cmd = New SqlCommand(m_sql, conexion)

            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then

                With iresult
                    .usuario = ds.Tables(0).Rows(0)("usuario").ToString.Trim
                    .tipo = ds.Tables(0).Rows(0)("tipo").ToString.Trim
                    .Password = ds.Tables(0).Rows(0)("pass").ToString.Trim 'IIf(.preq = "1", ds.Tables(0).Rows(0)("pass").ToString, "")
                    .id = ds.Tables(0).Rows(0)("id").ToString.Trim
                    .depto = ds.Tables(0).Rows(0)("depto").ToString.Trim
                    .email = ds.Tables(0).Rows(0)("email").ToString.Trim
                End With

                Return iresult
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbCrLf & "Sender : GetInfo(..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ds = Nothing
            Adapter = Nothing
            cmd = Nothing
        End Try
    End Function
End Class
