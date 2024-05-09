Public Class frmHistory
    Dim _sender As String = ""
    Public Sub New(sender As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _sender = sender
    End Sub
    Private Sub frmHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim query As String = "SELECT TOP (1000) [process]
                                  ,[status]
                                  ,[startTime]
                                  ,[endTime]
                                  ,[AcumTime]
                                  ,convert(date,[insert_date]) as insert_date
                              FROM [db_kyungshin].[dbo].[t_bma_downtime]
                              WHERE process='" & _sender & "'
                              ORDER BY id desc"
            dgv.DataSource = EjecutaSelects(query, "buscahistorial")
            ResizeCols(dgv)
        Catch ex As Exception

        End Try
    End Sub
End Class