
Imports System.Data.SqlClient

Public Class Class_parametros
    ' Inherits VB6_ComboBox
    Private cmd As SqlCommand = Nothing
    Private Adapter As SqlDataAdapter = Nothing
    Private ds As DataSet = Nothing
    Public Structure datos
        Public id As String
        Public tipoVehiculo As String
        Public tipoGruas As String
        Public tipoSeguros As String
    End Structure
    Public Sub LoadItemsInCombobox(ByVal cboVehiculo As ComboBox, ByVal cboGrua As ComboBox, ByVal cboSeguro As ComboBox)

        Try
            Dim m_sql As String = "SELECT tipoVehiculo, tipoGruas,tipoSeguros FROM t_parametros WHERE tipoVehiculo<>'' AND tipoVehiculo IS NOT NULL" &
                                    " OR tipoGruas<>'' AND tipoGruas IS NOT NULL OR tipoSeguros<>'' AND tipoSeguros IS NOT NULL"
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    If rw("tipoVehiculo").ToString <> "" Then
                        cboVehiculo.Items.Add(rw("tipoVehiculo").ToString)
                    End If
                    If rw("tipoGruas").ToString <> "" Then
                        cboGrua.Items.Add(rw("tipoGruas").ToString)
                    End If
                    If rw("tipoSeguros").ToString <> "" Then
                        cboSeguro.Items.Add(rw("tipoSeguros").ToString)
                    End If
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


    Public Sub LoadItemsInComboReporte(ByVal cboVehiculo As ComboBox)

        Try
            Dim m_sql As String = "SELECT tipoVehiculo, tipoGruas,tipoSeguros FROM t_parametros WHERE tipoVehiculo<>'' AND tipoVehiculo IS NOT NULL"
            cmd = New SqlCommand(m_sql, conexion)
            ds = New DataSet
            Adapter = New SqlDataAdapter
            Adapter.SelectCommand = cmd
            Adapter.Fill(ds)

            If (ds.Tables(0).Rows.Count > 0) Then
                For Each rw As DataRow In ds.Tables(0).Rows
                    cboVehiculo.Items.Add(rw("tipoVehiculo").ToString)
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


End Class
