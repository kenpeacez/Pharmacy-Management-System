Imports Microsoft.SqlServer
Imports MySql.Data.MySqlClient

Imports System.Windows.Forms.DataVisualization.Charting

Public Class Form4

    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Dim dr As MySqlDataReader

    Dim Server As String
    Dim UID As String
    Dim PWD As String
    Dim DBName As String
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDB()
        DisplayDailyStatistics()
        lblClinicName.Text = Form1.txtClinicName.Text
    End Sub

    Private Sub InitializeDB()

        Server = Form1.txtDBServerAddress.Text
        UID = Form1.txtDBUserID.Text
        PWD = Form1.txtDBPassword.Text
        DBName = Form1.txtDBName.Text

        myConnectionString = "server=" & Server & ";" _
                & "uid=" & UID & ";" _
                & "pwd=" & PWD & ";" _
                & "database=" & DBName
        conn.ConnectionString = myConnectionString
    End Sub

    Public Sub DisplayDailyStatistics()
        ' Clear previous chart data
        chartStatistics.Series.Clear()
        chartStatistics.ChartAreas.Clear()
        chartStatistics.Legends.Clear()

        Dim startDate As String = dtpStart.Value.ToString("yyyy-MM-dd")
        Dim endDate As String = dtpEnd.Value.ToString("yyyy-MM-dd")

        ' Create a new chart area
        Dim chartArea As New ChartArea()
        chartArea.AxisX.Title = "Date (Day Name)"
        chartArea.AxisY.Title = "Count"
        chartArea.AxisX.Interval = 1 ' Set interval to 1 to show each day
        chartArea.AxisX.LabelStyle.Angle = -45 ' Rotate labels to prevent overlap
        chartArea.AxisX.LabelStyle.Format = "ddd, MMM dd" ' Format to show abbreviated day name and date
        chartArea.AxisX.MajorGrid.LineColor = Color.LightGray ' Set grid line color to light gray
        chartArea.AxisY.MajorGrid.LineColor = Color.LightGray ' Set grid line color to light gray
        chartStatistics.ChartAreas.Add(chartArea)

        ' Create a new series for the chart
        Dim seriesNewPatients As New Series("New Patients")
        seriesNewPatients.ChartType = SeriesChartType.Line
        seriesNewPatients.IsValueShownAsLabel = True ' Show values on the chart
        seriesNewPatients.LabelFormat = "0" ' Ensure zero values are displayed correctly
        chartStatistics.Series.Add(seriesNewPatients)

        Dim seriesIOUs As New Series("IOUs")
        seriesIOUs.ChartType = SeriesChartType.Line
        seriesIOUs.IsValueShownAsLabel = True ' Show values on the chart
        seriesIOUs.LabelFormat = "0" ' Ensure zero values are displayed correctly
        chartStatistics.Series.Add(seriesIOUs)

        Dim seriesItems As New Series("Items")
        seriesItems.ChartType = SeriesChartType.Line
        seriesItems.IsValueShownAsLabel = True ' Show values on the chart
        seriesItems.LabelFormat = "0" ' Ensure zero values are displayed correctly
        chartStatistics.Series.Add(seriesItems)

        ' Set the legend position to bottom
        Dim legend As New Legend()
        legend.Docking = Docking.Bottom
        chartStatistics.Legends.Add(legend)

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT CAST(Timestamp AS date) AS Date, SUM(NewPatient) AS NewPatients, SUM(IOU) AS IOUs, SUM(NoOfItems) AS Items FROM `records` WHERE (CAST(Timestamp AS date) BETWEEN @startDate AND @endDate) GROUP BY Date", conn)
            cmd.Parameters.AddWithValue("@startDate", startDate)
            cmd.Parameters.AddWithValue("@endDate", endDate)
            dr = cmd.ExecuteReader

            While dr.Read()
                Dim dateValue As DateTime = dr.Item("Date")
                Dim newPatients As Integer = dr.Item("NewPatients")
                Dim ious As Integer = dr.Item("IOUs")
                Dim items As Integer = dr.Item("Items")

                ' Add data points to the series
                seriesNewPatients.Points.AddXY(dateValue, newPatients)
                seriesIOUs.Points.AddXY(dateValue, ious)
                seriesItems.Points.AddXY(dateValue, items)
            End While
            dr.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub dtpStart_ValueChanged(sender As Object, e As EventArgs) Handles dtpStart.ValueChanged
        DisplayDailyStatistics()
    End Sub

    Private Sub dtpEnd_ValueChanged(sender As Object, e As EventArgs) Handles dtpEnd.ValueChanged
        DisplayDailyStatistics()
    End Sub

End Class