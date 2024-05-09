''' <summary>
''' Original author
''' http://social.msdn.microsoft.com/Forums/en-US/winformsdatacontrols/thread/a44622c0-74e1-463b-97b9-27b87513747e#faq8
''' </summary>
''' <remarks>
''' Original code was in C Sharp, I converted and tweaked some code
''' which did not compile under VB.NET
''' </remarks>
Public Class GroupByGrid
    Inherits DataGridView

    Protected Overrides Sub OnCellFormatting(ByVal args As DataGridViewCellFormattingEventArgs)
        MyBase.OnCellFormatting(args)

        ' First row always displays
        If args.RowIndex = 0 Then
            Return
        End If

        If IsRepeatedCellValue(args.RowIndex, args.ColumnIndex) Then
            args.Value = String.Empty
            args.FormattingApplied = True
        End If
    End Sub
    Private Function IsRepeatedCellValue(ByVal rowIndex As Integer, ByVal colIndex As Integer) As Boolean
        Dim currCell As DataGridViewCell = Rows(rowIndex).Cells(colIndex)
        Dim prevCell As DataGridViewCell = Rows(rowIndex - 1).Cells(colIndex)

        If (currCell.Value Is prevCell.Value) OrElse (currCell.Value IsNot Nothing AndAlso prevCell.Value IsNot Nothing AndAlso currCell.Value.ToString() = prevCell.Value.ToString()) Then
            Return True
        Else
            Return False
        End If

    End Function
    Protected Overrides Sub OnCellPainting(ByVal args As DataGridViewCellPaintingEventArgs)

        MyBase.OnCellPainting(args)

        args.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None

        ' Ignore column and row headers and first row
        If args.RowIndex < 1 OrElse args.ColumnIndex < 0 Then
            Return
        End If

        If IsRepeatedCellValue(args.RowIndex, args.ColumnIndex) Then
            args.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None
        Else
            args.AdvancedBorderStyle.Top = AdvancedCellBorderStyle.Top
        End If

    End Sub
End Class