Public Class frmBreaktime
    Public iResult As Boolean = False

    Dim hora2 As DateTime
    Dim hora1 As DateTime
    Dim horaAcumTime, min, seg As String
    Dim totaltiempo As TimeSpan
    Dim tiempoTranscurrido As String
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ' Format(CLng(lbS.Text), "00")
            '  Dim hora As String = Convert.ToDateTime(cbSt1.Text).AddMinutes(15)
            Dim query As String = "UPDATE [dbo].[t_bma_breaktime]
                               SET [startup1] = '" & cbSt1.Text & "'
                               ,[startup1fin] = '" & Convert.ToDateTime(cbSt1.Text).AddMinutes(lbs1.Text).ToShortTimeString & "'
                                  ,[break1] ='" & cbBr1.Text & "'
                                   ,[break1fin] ='" & Convert.ToDateTime(cbBr1.Text).AddMinutes(lbb1.Text).ToShortTimeString & "'
                                  ,[lunch1] ='" & cbL1.Text & "'
                                      ,[lunch1fin] ='" & Convert.ToDateTime(cbL1.Text).AddMinutes(lbl1.Text).ToShortTimeString & "'
                                  ,[break2] ='" & cbBr2.Text & "'
                                      ,[break2fin] ='" & Convert.ToDateTime(cbBr2.Text).AddMinutes(lbb2.Text).ToShortTimeString & "'
                                  ,[startup2] = '" & cbSt2.Text & "'
                                      ,[startup2fin] ='" & Convert.ToDateTime(cbSt2.Text).AddMinutes(lbs2.Text).ToShortTimeString & "'
                                  ,[lunch2] = '" & cbL2.Text & "'
                                      ,[lunch2fin] ='" & Convert.ToDateTime(cbL2.Text).AddMinutes(lbl2.Text).ToShortTimeString & "'
                                  ,[break3] = '" & cbBr3.Text & "'
                                      ,[break3fin] ='" & Convert.ToDateTime(cbBr3.Text).AddMinutes(lbb3.Text).ToShortTimeString & "'
                                  ,[break4] = '" & cbBr4.Text & "'
                                      ,[break4fin] ='" & Convert.ToDateTime(cbBr4.Text).AddMinutes(lbb4.Text).ToShortTimeString & "'
                                  ,[insert_user] = '" & Current_Session.usuario & "'
                                  ,[insert_date] = '" & GetCurrentDateTime() & "'
                             WHERE line='" & cbLinea.Text & "'"

            If Ejecuta(query, "updBrkTime") = True Then
                MsgBox("Updated")
                iResult = True
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLinea.SelectedIndexChanged
        Try
            Dim arre2 As DataRowCollection = GetRows("SELECT * FROM [t_bma_breaktime] WHERE line='" & cbLinea.Text & "'")

            If Not IsNothing(arre2) Then
                For Each item As DataRow In arre2
                    cbSt1.Text = item("startup1").ToString.Trim
                    cbBr1.Text = item("break1").ToString.Trim
                    cbL1.Text = item("lunch1").ToString.Trim
                    cbBr2.Text = item("break2").ToString.Trim
                    cbSt2.Text = item("startup2").ToString.Trim
                    cbL2.Text = item("lunch2").ToString.Trim
                    cbBr3.Text = item("break3").ToString.Trim
                    cbBr4.Text = item("break4").ToString.Trim
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmBreaktime_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class