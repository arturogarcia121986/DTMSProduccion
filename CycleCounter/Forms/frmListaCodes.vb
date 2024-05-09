Public Class frmListaCodes
    Dim _codeEC, _depto As String
    Private selectedIndex As Integer
    Dim folioSelected As String = ""


    Public iResult As codigos
    Public Structure codigos
        Public code As String
    End Structure

    Public Sub New(sender As String, depto As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        _codeEC = sender
        _depto = depto
    End Sub
    Private Sub frmListaCodes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sql As String = ""

        sql = "SELECT TOP (1000) [id]
      ,[code]
      ,[description]
  FROM [db_kyungshin].[dbo].[t_bma_dtms_codes] WHERE depto='" & _depto & "' AND nivel='" & _codeEC & "'"
        dgv.DataSource = EjecutaSelects(sql, "fillDgv")


        ResizeCols(dgv)

        dgv.ClearSelection()

    End Sub

    Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellDoubleClick
        Try
            With iResult
                .code = dgv.Rows(e.RowIndex).Cells("code").Value.ToString.Trim
            End With
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        Try
            selectedIndex = dgv.CurrentRow.Index
            folioSelected = dgv.Rows(selectedIndex).Cells("id").Value

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class