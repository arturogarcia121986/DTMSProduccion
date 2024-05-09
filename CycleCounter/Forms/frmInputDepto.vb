Public Class frmInputDepto
    Dim _line, _id, query As String


    Public Structure iResult
        Public cerrar As String
    End Structure

    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Public Sub New(line As String, id As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _line = line
        _id = id
    End Sub
    Private Sub frmInputDepto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tRemain.Start()

        Select Case _line
            Case "1100", "2100", "3100"
                lbLine.Text = "100"
            Case "1400", "2400", "3400"
                lbLine.Text = "400"
            Case "1700", "2700", "3700"
                lbLine.Text = "700"
        End Select

    End Sub

    Private Sub tRemain_Tick(sender As Object, e As EventArgs) Handles tRemain.Tick
        Try
            If lbSegundos.Text = "0" Then
                tRemain.Stop()
            Else
                lbSegundos.Text = CInt(lbSegundos.Text) - 1
                query = "Select top 1 id,step
                    From [db_kyungshin].[dbo].[t_bma_downtime]
                    Where id='" & _id & "'
                    order by id desc"
                If QueryRow(query, "step", "getLastRowStep") <> "0" Then
                    frmMonitor.warning1100 = False
                    Me.Close()

                End If


            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Event CerrarFormulario()



    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cbDepto.SelectedIndex = -1 Then
            MessageBox.Show("Select 1 department", "Data missing", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Call Ejecuta("UPDATE t_bma_downtime SET step=1,alarmeddepto='" & cbDepto.Text & "' WHERE id='" & _id & "'", "updStep1")
        frmMonitor.warning1100 = False
        tRemain.Stop()
        Me.Close()
    End Sub

End Class