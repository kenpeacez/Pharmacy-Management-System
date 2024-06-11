Public Class Form4
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the TableLayoutPanel properties
        TableLayoutPanel1.ColumnCount = 4
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.Dock = DockStyle.Fill

        ' Adjust the size of each cell in the TableLayoutPanel
        For i As Integer = 0 To TableLayoutPanel1.ColumnCount - 1
            TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        Next
        For i As Integer = 0 To TableLayoutPanel1.RowCount - 1
            TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50.0F))
        Next

        ' Add MonthCalendar controls for each month of the year
        For month As Integer = 1 To 12
            Dim calendar As New MonthCalendar()
            calendar.MaxSelectionCount = 1
            calendar.SetDate(New DateTime(DateTime.Now.Year, month, 1))
            TableLayoutPanel1.Controls.Add(calendar)
        Next
    End Sub
End Class