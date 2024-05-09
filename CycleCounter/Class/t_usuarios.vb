
Imports System.Data.SqlClient
Public Class t_usuarios
    Private cmd As SqlCommand = Nothing
    Private Adapter As SqlDataAdapter = Nothing
    Private ds As DataSet = Nothing

    Public Structure datos
        Public usuario As String
        Public Password As String
        Public tipo As String      'tipo de usuario
        Public detenidos As Integer
        Public evaluaciones As Integer
        Public fatiga As Integer
        Public graficos As Integer
        Public operativos As Integer
        Public altaCRP As Integer
        Public parque As Integer
        Public registro_armas As Integer
        Public portes As Integer
        Public iph As Integer
        Public vehiculos As Integer
        Public conciliacion As Integer
        Public vialidad As Integer
        Public reclutamiento As Integer
        Public recepcion As Integer
        Public permisos As String
        Public email As String
        Public depto As String
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
            cmd = New SqlCommand("SELECT * FROM t_usuarios WHERE usuario='" & m_usuario & "'", conexion)

            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then

                With iresult
                    .usuario = ds.Tables(0).Rows(0)("usuario").ToString
                    .tipo = ds.Tables(0).Rows(0)("tipo").ToString
                    .Password = ds.Tables(0).Rows(0)("pass").ToString
                    .email = ds.Tables(0).Rows(0)("email").ToString
                    .depto = ds.Tables(0).Rows(0)("depto").ToString
                    .permisos = ds.Tables(0).Rows(0)("permisos").ToString
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
