Public Class fmrSetComputerName
    Private Sub fmrSetComputerName_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lbComputerName.Text = GetComputerName()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If tbMachineName.Text.Trim = "" Then
                MessageBox.Show("Especifique un nombre de máquina", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            If Ejecuta("INSERT INTO t_lines_settings(line_name,computer_id) VALUES ('" & tbMachineName.Text &
                       "','" & GetComputerName() & "')", "insertPC") Then
                MessageBox.Show("El nombre de la máquina ha sido guardado", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class