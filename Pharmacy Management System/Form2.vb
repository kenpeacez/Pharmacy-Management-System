Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports Microsoft.SqlServer
Imports System.Security.Cryptography
Imports System.Globalization

Public Class Form2

    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Dim dr As MySqlDataReader

    Dim Server As String
    Dim UID As String
    Dim PWD As String
    Dim DBName As String
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDB()
        txtSearchPatientIC.Text = Form1.txtICNo.Text
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToResizeColumns = False
        DataGridView2.AllowUserToResizeRows = False
        loadpastmedintoDGV()
        DataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        'All Past Medications tab
        dgvDateSelector.AllowUserToAddRows = False
        dgvDateSelector.AllowUserToResizeColumns = False
        dgvDateSelector.AllowUserToResizeRows = False
        dgvPatientDrugHistory.AllowUserToAddRows = False
        dgvPatientDrugHistory.AllowUserToResizeColumns = False
        dgvPatientDrugHistory.AllowUserToResizeRows = False
        dgvPatientInsulinHistory.AllowUserToAddRows = False
        dgvPatientInsulinHistory.AllowUserToResizeColumns = False
        dgvPatientInsulinHistory.AllowUserToResizeRows = False
        dgvDateSelector.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvPatientDrugHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        loadDatabaseDGV()

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

    Public Sub loadpastmedintoDGV()
        DataGridView1.Rows.Clear()
        DataGridView2.Rows.Clear()

        Dim dt As New DataTable

        Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & txtSearchPatientIC.Text & "'", conn)
        Try
            Form1.clearSelectionIndex()
            Form1.clearall()
            Form1.txtICNo.Text = txtSearchPatientIC.Text
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                DataGridView1.Rows.Add("1", dr.Item("Drug1Name"), dr.Item("Drug1Strength"), dr.Item("Drug1Unit"), dr.Item("Drug1Dose"), dr.Item("Drug1Freq"), dr.Item("Drug1Duration"), dr.Item("Drug1TotalQTY"))
                DataGridView1.Rows.Add("2", dr.Item("Drug2Name"), dr.Item("Drug2Strength"), dr.Item("Drug2Unit"), dr.Item("Drug2Dose"), dr.Item("Drug2Freq"), dr.Item("Drug2Duration"), dr.Item("Drug2TotalQTY"))
                DataGridView1.Rows.Add("3", dr.Item("Drug3Name"), dr.Item("Drug3Strength"), dr.Item("Drug3Unit"), dr.Item("Drug3Dose"), dr.Item("Drug3Freq"), dr.Item("Drug3Duration"), dr.Item("Drug3TotalQTY"))
                DataGridView1.Rows.Add("4", dr.Item("Drug4Name"), dr.Item("Drug4Strength"), dr.Item("Drug4Unit"), dr.Item("Drug4Dose"), dr.Item("Drug4Freq"), dr.Item("Drug4Duration"), dr.Item("Drug4TotalQTY"))
                DataGridView1.Rows.Add("5", dr.Item("Drug5Name"), dr.Item("Drug5Strength"), dr.Item("Drug5Unit"), dr.Item("Drug5Dose"), dr.Item("Drug5Freq"), dr.Item("Drug5Duration"), dr.Item("Drug5TotalQTY"))
                DataGridView1.Rows.Add("6", dr.Item("Drug6Name"), dr.Item("Drug6Strength"), dr.Item("Drug6Unit"), dr.Item("Drug6Dose"), dr.Item("Drug6Freq"), dr.Item("Drug6Duration"), dr.Item("Drug6TotalQTY"))
                DataGridView1.Rows.Add("7", dr.Item("Drug7Name"), dr.Item("Drug7Strength"), dr.Item("Drug7Unit"), dr.Item("Drug7Dose"), dr.Item("Drug7Freq"), dr.Item("Drug7Duration"), dr.Item("Drug7TotalQTY"))
                DataGridView1.Rows.Add("8", dr.Item("Drug8Name"), dr.Item("Drug8Strength"), dr.Item("Drug8Unit"), dr.Item("Drug8Dose"), dr.Item("Drug8Freq"), dr.Item("Drug8Duration"), dr.Item("Drug8TotalQTY"))
                DataGridView1.Rows.Add("9", dr.Item("Drug9Name"), dr.Item("Drug9Strength"), dr.Item("Drug9Unit"), dr.Item("Drug9Dose"), dr.Item("Drug9Freq"), dr.Item("Drug9Duration"), dr.Item("Drug9TotalQTY"))
                DataGridView1.Rows.Add("10", dr.Item("Drug10Name"), dr.Item("Drug10Strength"), dr.Item("Drug10Unit"), dr.Item("Drug10Dose"), dr.Item("Drug10Freq"), dr.Item("Drug10Duration"), dr.Item("Drug10TotalQTY"))

                DataGridView2.Rows.Add("1", dr.Item("Insulin1Name"), dr.Item("Insulin1Strength"), dr.Item("Insulin1Unit"), dr.Item("Insulin1MorDose"), dr.Item("Insulin1NoonDose"), dr.Item("Insulin1AfternoonDose"), dr.Item("Insulin1NightDose"), dr.Item("Insulin1Freq"), dr.Item("Insulin1Duration"), dr.Item("Insulin1TotalDose"), dr.Item("Insulin1POM"), dr.Item("Insulin1CartQTY"))
                DataGridView2.Rows.Add("2", dr.Item("Insulin2Name"), dr.Item("Insulin2Strength"), dr.Item("Insulin2Unit"), dr.Item("Insulin2MorDose"), dr.Item("Insulin2NoonDose"), dr.Item("Insulin2AfternoonDose"), dr.Item("Insulin2NightDose"), dr.Item("Insulin2Freq"), dr.Item("Insulin2Duration"), dr.Item("Insulin2TotalDose"), dr.Item("Insulin2POM"), dr.Item("Insulin2CartQTY"))

                Form1.cbDrug1.Text = dr.Item("Drug1Name")
                Form1.cbDrug2.Text = dr.Item("Drug2Name")
                Form1.cbDrug3.Text = dr.Item("Drug3Name")
                Form1.cbDrug4.Text = dr.Item("Drug4Name")
                Form1.cbDrug5.Text = dr.Item("Drug5Name")
                Form1.cbDrug6.Text = dr.Item("Drug6Name")
                Form1.cbDrug7.Text = dr.Item("Drug7Name")
                Form1.cbDrug8.Text = dr.Item("Drug8Name")
                Form1.cbDrug9.Text = dr.Item("Drug9Name")
                Form1.cbDrug10.Text = dr.Item("Drug10Name")
                Form1.cbInsulin1.Text = dr.Item("Insulin1Name")
                Form1.cbInsulin2.Text = dr.Item("Insulin2Name")

                Form1.txtPatientName.Text = dr.Item("Name")

                Form1.txtDoseD1.Text = dr.Item("Drug1Dose")
                Form1.txtFreqD1.Text = dr.Item("Drug1Freq")

                Form1.txtDoseD2.Text = dr.Item("Drug2Dose")
                Form1.txtFreqD2.Text = dr.Item("Drug2Freq")

                Form1.txtDoseD3.Text = dr.Item("Drug3Dose")
                Form1.txtFreqD3.Text = dr.Item("Drug3Freq")

                Form1.txtDoseD4.Text = dr.Item("Drug4Dose")
                Form1.txtFreqD4.Text = dr.Item("Drug4Freq")

                Form1.txtDoseD5.Text = dr.Item("Drug5Dose")
                Form1.txtFreqD5.Text = dr.Item("Drug5Freq")

                Form1.txtDoseD6.Text = dr.Item("Drug6Dose")
                Form1.txtFreqD6.Text = dr.Item("Drug6Freq")

                Form1.txtDoseD7.Text = dr.Item("Drug7Dose")
                Form1.txtFreqD7.Text = dr.Item("Drug7Freq")

                Form1.txtDoseD8.Text = dr.Item("Drug8Dose")
                Form1.txtFreqD8.Text = dr.Item("Drug8Freq")

                Form1.txtDoseD9.Text = dr.Item("Drug9Dose")
                Form1.txtFreqD9.Text = dr.Item("Drug9Freq")

                Form1.txtDoseD10.Text = dr.Item("Drug10Dose")
                Form1.txtFreqD10.Text = dr.Item("Drug10Freq")

                Form1.txtIn1MorDose.Text = dr.Item("Insulin1MorDose")
                Form1.txtIn1NoonDose.Text = dr.Item("Insulin1NoonDose")
                Form1.txtIn1AfterNoonDose.Text = dr.Item("Insulin1AfternoonDose")
                Form1.txtIn1NightDose.Text = dr.Item("Insulin1NightDose")
                Form1.txtIn1TotalDose.Text = dr.Item("Insulin1TotalDose")
                Form1.txtIn1POM.Text = dr.Item("Insulin1POM")
                ' Form1.txtIn1CartQTY.Text = dr.Item("Insulin1CartQTY")

                Form1.txtIn2MorDose.Text = dr.Item("Insulin2MorDose")
                Form1.txtIn2NoonDose.Text = dr.Item("Insulin2NoonDose")
                Form1.txtIn2AfterNoonDose.Text = dr.Item("Insulin2AfternoonDose")
                Form1.txtIn2NightDose.Text = dr.Item("Insulin2NightDose")
                Form1.txtIn2TotalDose.Text = dr.Item("Insulin2TotalDose")
                Form1.txtIn2POM.Text = dr.Item("Insulin2POM")
                ' Form1.txtIn2CartQTY.Text = dr.Item("Insulin2CartQTY")

                lblDatePastMed.Text = Convert.ToDateTime(dr.Item("Date")).ToString("dddd, dd MMMM yyyy")

                lblDateCollection.Text = dr.Item("DateCollection")

                lblDateSeeDoctor.Text = dr.Item("DateSeeDoctor")

                lblPatientName.Text = dr.Item("Name")

                Me.Text = "Search Past Medication - " & lblPatientName.Text & "        IC No: " & txtSearchPatientIC.Text

            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        loadpastmedintoDGV()
    End Sub
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    End Sub
    Public Sub loadDatabaseDGV()
        dgvDateSelector.Rows.Clear()
        dgvPatientDrugHistory.Rows.Clear()
        dgvPatientInsulinHistory.Rows.Clear()
        'check if txtICDB is empty
        'If txtICNoDB.Text = "" Then
        'MsgBox("Please enter the IC No. at Search box")
        'lblPatientNameDB.Text = ""
        'Return
        'End If
        'SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752' UNION SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752'
        'Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & lblPrevSavedICNo.Text & "'", conn)
        Dim count As Integer = 0
        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtSearchPatientIC.Text & "' UNION " & "SELECT * FROM `prescribeddrugshistory` WHERE ICNo = '" & txtSearchPatientIC.Text & "'", conn)
        Try
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                count += 1
                Dim formattedDate As String = Convert.ToDateTime(dr.Item("Date")).ToString("dddd, dd MMMM, yyyy")
                dgvDateSelector.Rows.Add(count, dr.Item("ID"), formattedDate, dr.Item("DateCollection"), dr.Item("DateSeeDoctor"), dr.Item("Timestamp"))
            End While
            'timestamp = dgvDateSelector.CurrentRow.Cells(5).Value
            'lblPatientNameDB.Text = dr.Item("Name")
            dr.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
            'MessageBox.Show("IC No. not found.")
        Finally
            dr.Dispose()
            conn.Close()
        End Try
    End Sub
    Public Sub loadDatabaseDGV2()
        dgvPatientDrugHistory.Rows.Clear()
        dgvPatientInsulinHistory.Rows.Clear()
        Dim timestamp As String
        timestamp = dgvDateSelector.CurrentRow.Cells(5).Value
        Dim inputTime As String = timestamp
        Dim format As String = "d/M/yyyy h:mm:ss tt"
        Dim provider As CultureInfo = CultureInfo.InvariantCulture
        Dim parsedTime As DateTime = DateTime.ParseExact(inputTime, format, provider)
        Dim outputTime As String = parsedTime.ToString("yyyy-M-dd HH:mm:ss")
        'MsgBox(timestamp)
        Dim dt As New DataTable
        'MsgBox(outputTime)
        'SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752' UNION SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752'
        'Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & lblPrevSavedICNo.Text & "'", conn)
        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtSearchPatientIC.Text & "'" & " AND " & "Timestamp = '" & outputTime & "'" & " UNION " & "SELECT * FROM `prescribeddrugshistory` WHERE ICNo = '" & txtSearchPatientIC.Text & "'" & " AND " & "Timestamp ='" & outputTime & "' LIMIT 1", conn)
        Try
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                dgvPatientDrugHistory.Rows.Add("1", dr.Item("Drug1Name"), dr.Item("Drug1Strength"), dr.Item("Drug1Unit"), dr.Item("Drug1Dose"), dr.Item("Drug1Freq"), dr.Item("Drug1Duration"), dr.Item("Drug1TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("2", dr.Item("Drug2Name"), dr.Item("Drug2Strength"), dr.Item("Drug2Unit"), dr.Item("Drug2Dose"), dr.Item("Drug2Freq"), dr.Item("Drug2Duration"), dr.Item("Drug2TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("3", dr.Item("Drug3Name"), dr.Item("Drug3Strength"), dr.Item("Drug3Unit"), dr.Item("Drug3Dose"), dr.Item("Drug3Freq"), dr.Item("Drug3Duration"), dr.Item("Drug3TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("4", dr.Item("Drug4Name"), dr.Item("Drug4Strength"), dr.Item("Drug4Unit"), dr.Item("Drug4Dose"), dr.Item("Drug4Freq"), dr.Item("Drug4Duration"), dr.Item("Drug4TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("5", dr.Item("Drug5Name"), dr.Item("Drug5Strength"), dr.Item("Drug5Unit"), dr.Item("Drug5Dose"), dr.Item("Drug5Freq"), dr.Item("Drug5Duration"), dr.Item("Drug5TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("6", dr.Item("Drug6Name"), dr.Item("Drug6Strength"), dr.Item("Drug6Unit"), dr.Item("Drug6Dose"), dr.Item("Drug6Freq"), dr.Item("Drug6Duration"), dr.Item("Drug6TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("7", dr.Item("Drug7Name"), dr.Item("Drug7Strength"), dr.Item("Drug7Unit"), dr.Item("Drug7Dose"), dr.Item("Drug7Freq"), dr.Item("Drug7Duration"), dr.Item("Drug7TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("8", dr.Item("Drug8Name"), dr.Item("Drug8Strength"), dr.Item("Drug8Unit"), dr.Item("Drug8Dose"), dr.Item("Drug8Freq"), dr.Item("Drug8Duration"), dr.Item("Drug8TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("9", dr.Item("Drug9Name"), dr.Item("Drug9Strength"), dr.Item("Drug9Unit"), dr.Item("Drug9Dose"), dr.Item("Drug9Freq"), dr.Item("Drug9Duration"), dr.Item("Drug9TotalQTY"))
                dgvPatientDrugHistory.Rows.Add("10", dr.Item("Drug10Name"), dr.Item("Drug10Strength"), dr.Item("Drug10Unit"), dr.Item("Drug10Dose"), dr.Item("Drug10Freq"), dr.Item("Drug10Duration"), dr.Item("Drug10TotalQTY"))
                dgvPatientInsulinHistory.Rows.Add("1", dr.Item("Insulin1Name"), dr.Item("Insulin1Strength"), dr.Item("Insulin1Unit"), dr.Item("Insulin1MorDose"), dr.Item("Insulin1NoonDose"), dr.Item("Insulin1AfternoonDose"), dr.Item("Insulin1NightDose"), dr.Item("Insulin1Freq"), dr.Item("Insulin1Duration"), dr.Item("Insulin1TotalDose"), dr.Item("Insulin1POM"), dr.Item("Insulin1CartQTY"))
                dgvPatientInsulinHistory.Rows.Add("2", dr.Item("Insulin2Name"), dr.Item("Insulin2Strength"), dr.Item("Insulin2Unit"), dr.Item("Insulin2MorDose"), dr.Item("Insulin2NoonDose"), dr.Item("Insulin2AfternoonDose"), dr.Item("Insulin2NightDose"), dr.Item("Insulin2Freq"), dr.Item("Insulin2Duration"), dr.Item("Insulin2TotalDose"), dr.Item("Insulin2POM"), dr.Item("Insulin2CartQTY"))
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dr.Dispose()
            conn.Close()
        End Try
    End Sub
    Private Sub dgvDateSelector_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDateSelector.CellClick
        loadDatabaseDGV2()
    End Sub
    Private Sub dgvDateSelector_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDateSelector.CellDoubleClick
        PasteSelectedCellDataToForm1()
    End Sub
    Private Sub PasteSelectedCellDataToForm1()

        Dim timestamp As String
        timestamp = dgvDateSelector.CurrentRow.Cells(5).Value
        Dim inputTime As String = timestamp
        Dim format As String = "d/M/yyyy h:mm:ss tt"
        Dim provider As CultureInfo = CultureInfo.InvariantCulture
        Dim parsedTime As DateTime = DateTime.ParseExact(inputTime, format, provider)
        Dim outputTime As String = parsedTime.ToString("yyyy-M-dd HH:mm:ss")
        Dim dt As New DataTable

        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtSearchPatientIC.Text & "'" & " AND " & "Timestamp = '" & outputTime & "'" & " UNION " & "SELECT * FROM `prescribeddrugshistory` WHERE ICNo = '" & txtSearchPatientIC.Text & "'" & " AND " & "Timestamp ='" & outputTime & "' LIMIT 1", conn)
        Try
            Form1.clearSelectionIndex()
            Form1.clearall()
            Form1.txtICNo.Text = txtSearchPatientIC.Text
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                DataGridView1.Rows.Add("1", dr.Item("Drug1Name"), dr.Item("Drug1Strength"), dr.Item("Drug1Unit"), dr.Item("Drug1Dose"), dr.Item("Drug1Freq"), dr.Item("Drug1Duration"), dr.Item("Drug1TotalQTY"))
                DataGridView1.Rows.Add("2", dr.Item("Drug2Name"), dr.Item("Drug2Strength"), dr.Item("Drug2Unit"), dr.Item("Drug2Dose"), dr.Item("Drug2Freq"), dr.Item("Drug2Duration"), dr.Item("Drug2TotalQTY"))
                DataGridView1.Rows.Add("3", dr.Item("Drug3Name"), dr.Item("Drug3Strength"), dr.Item("Drug3Unit"), dr.Item("Drug3Dose"), dr.Item("Drug3Freq"), dr.Item("Drug3Duration"), dr.Item("Drug3TotalQTY"))
                DataGridView1.Rows.Add("4", dr.Item("Drug4Name"), dr.Item("Drug4Strength"), dr.Item("Drug4Unit"), dr.Item("Drug4Dose"), dr.Item("Drug4Freq"), dr.Item("Drug4Duration"), dr.Item("Drug4TotalQTY"))
                DataGridView1.Rows.Add("5", dr.Item("Drug5Name"), dr.Item("Drug5Strength"), dr.Item("Drug5Unit"), dr.Item("Drug5Dose"), dr.Item("Drug5Freq"), dr.Item("Drug5Duration"), dr.Item("Drug5TotalQTY"))
                DataGridView1.Rows.Add("6", dr.Item("Drug6Name"), dr.Item("Drug6Strength"), dr.Item("Drug6Unit"), dr.Item("Drug6Dose"), dr.Item("Drug6Freq"), dr.Item("Drug6Duration"), dr.Item("Drug6TotalQTY"))
                DataGridView1.Rows.Add("7", dr.Item("Drug7Name"), dr.Item("Drug7Strength"), dr.Item("Drug7Unit"), dr.Item("Drug7Dose"), dr.Item("Drug7Freq"), dr.Item("Drug7Duration"), dr.Item("Drug7TotalQTY"))
                DataGridView1.Rows.Add("8", dr.Item("Drug8Name"), dr.Item("Drug8Strength"), dr.Item("Drug8Unit"), dr.Item("Drug8Dose"), dr.Item("Drug8Freq"), dr.Item("Drug8Duration"), dr.Item("Drug8TotalQTY"))
                DataGridView1.Rows.Add("9", dr.Item("Drug9Name"), dr.Item("Drug9Strength"), dr.Item("Drug9Unit"), dr.Item("Drug9Dose"), dr.Item("Drug9Freq"), dr.Item("Drug9Duration"), dr.Item("Drug9TotalQTY"))
                DataGridView1.Rows.Add("10", dr.Item("Drug10Name"), dr.Item("Drug10Strength"), dr.Item("Drug10Unit"), dr.Item("Drug10Dose"), dr.Item("Drug10Freq"), dr.Item("Drug10Duration"), dr.Item("Drug10TotalQTY"))

                DataGridView2.Rows.Add("1", dr.Item("Insulin1Name"), dr.Item("Insulin1Strength"), dr.Item("Insulin1Unit"), dr.Item("Insulin1MorDose"), dr.Item("Insulin1NoonDose"), dr.Item("Insulin1AfternoonDose"), dr.Item("Insulin1NightDose"), dr.Item("Insulin1Freq"), dr.Item("Insulin1Duration"), dr.Item("Insulin1TotalDose"), dr.Item("Insulin1POM"), dr.Item("Insulin1CartQTY"))
                DataGridView2.Rows.Add("2", dr.Item("Insulin2Name"), dr.Item("Insulin2Strength"), dr.Item("Insulin2Unit"), dr.Item("Insulin2MorDose"), dr.Item("Insulin2NoonDose"), dr.Item("Insulin2AfternoonDose"), dr.Item("Insulin2NightDose"), dr.Item("Insulin2Freq"), dr.Item("Insulin2Duration"), dr.Item("Insulin2TotalDose"), dr.Item("Insulin2POM"), dr.Item("Insulin2CartQTY"))

                Form1.cbDrug1.Text = dr.Item("Drug1Name")
                Form1.cbDrug2.Text = dr.Item("Drug2Name")
                Form1.cbDrug3.Text = dr.Item("Drug3Name")
                Form1.cbDrug4.Text = dr.Item("Drug4Name")
                Form1.cbDrug5.Text = dr.Item("Drug5Name")
                Form1.cbDrug6.Text = dr.Item("Drug6Name")
                Form1.cbDrug7.Text = dr.Item("Drug7Name")
                Form1.cbDrug8.Text = dr.Item("Drug8Name")
                Form1.cbDrug9.Text = dr.Item("Drug9Name")
                Form1.cbDrug10.Text = dr.Item("Drug10Name")
                Form1.cbInsulin1.Text = dr.Item("Insulin1Name")
                Form1.cbInsulin2.Text = dr.Item("Insulin2Name")

                Form1.txtPatientName.Text = dr.Item("Name")

                Form1.txtDoseD1.Text = dr.Item("Drug1Dose")
                Form1.txtFreqD1.Text = dr.Item("Drug1Freq")

                Form1.txtDoseD2.Text = dr.Item("Drug2Dose")
                Form1.txtFreqD2.Text = dr.Item("Drug2Freq")

                Form1.txtDoseD3.Text = dr.Item("Drug3Dose")
                Form1.txtFreqD3.Text = dr.Item("Drug3Freq")

                Form1.txtDoseD4.Text = dr.Item("Drug4Dose")
                Form1.txtFreqD4.Text = dr.Item("Drug4Freq")

                Form1.txtDoseD5.Text = dr.Item("Drug5Dose")
                Form1.txtFreqD5.Text = dr.Item("Drug5Freq")

                Form1.txtDoseD6.Text = dr.Item("Drug6Dose")
                Form1.txtFreqD6.Text = dr.Item("Drug6Freq")

                Form1.txtDoseD7.Text = dr.Item("Drug7Dose")
                Form1.txtFreqD7.Text = dr.Item("Drug7Freq")

                Form1.txtDoseD8.Text = dr.Item("Drug8Dose")
                Form1.txtFreqD8.Text = dr.Item("Drug8Freq")

                Form1.txtDoseD9.Text = dr.Item("Drug9Dose")
                Form1.txtFreqD9.Text = dr.Item("Drug9Freq")

                Form1.txtDoseD10.Text = dr.Item("Drug10Dose")
                Form1.txtFreqD10.Text = dr.Item("Drug10Freq")

                Form1.txtIn1MorDose.Text = dr.Item("Insulin1MorDose")
                Form1.txtIn1NoonDose.Text = dr.Item("Insulin1NoonDose")
                Form1.txtIn1AfterNoonDose.Text = dr.Item("Insulin1AfternoonDose")
                Form1.txtIn1NightDose.Text = dr.Item("Insulin1NightDose")
                Form1.txtIn1TotalDose.Text = dr.Item("Insulin1TotalDose")
                Form1.txtIn1POM.Text = dr.Item("Insulin1POM")
                Form1.txtIn1CartQTY.Text = dr.Item("Insulin1CartQTY")

                Form1.txtIn2MorDose.Text = dr.Item("Insulin2MorDose")
                Form1.txtIn2NoonDose.Text = dr.Item("Insulin2NoonDose")
                Form1.txtIn2AfterNoonDose.Text = dr.Item("Insulin2AfternoonDose")
                Form1.txtIn2NightDose.Text = dr.Item("Insulin2NightDose")
                Form1.txtIn2TotalDose.Text = dr.Item("Insulin2TotalDose")
                Form1.txtIn2POM.Text = dr.Item("Insulin2POM")
                Form1.txtIn2CartQTY.Text = dr.Item("Insulin2CartQTY")
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dr.Dispose()
            conn.Close()
        End Try
    End Sub
End Class