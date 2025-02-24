
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports System.Threading
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Reflection


Public Class Form1

    Dim conn As New MySql.Data.MySqlClient.MySqlConnection
    Dim myConnectionString As String
    Dim dr As MySqlDataReader

    Dim Server As String
    Dim UID As String
    Dim PWD As String
    Dim DBName As String

    Dim DBStatus As Boolean

    Private currentPage As Integer = 1
    Private currentPageInsulin As Integer = 1

    Dim NoOfItemsRecord As Integer = 0
    Dim NoOfItemsRecordInsulin As Integer = 0

    Dim RemarkD1 As String
    Dim RemarkD2 As String
    Dim RemarkD3 As String
    Dim RemarkD4 As String
    Dim RemarkD5 As String
    Dim RemarkD6 As String
    Dim RemarkD7 As String
    Dim RemarkD8 As String
    Dim RemarkD9 As String
    Dim RemarkD10 As String
    Dim RemarkIn1 As String
    Dim RemarkIn2 As String

    Dim DefaultMaxQTYD1 As Integer = 0
    Dim DefaultMaxQTYD2 As Integer = 0
    Dim DefaultMaxQTYD3 As Integer = 0
    Dim DefaultMaxQTYD4 As Integer = 0
    Dim DefaultMaxQTYD5 As Integer = 0
    Dim DefaultMaxQTYD6 As Integer = 0
    Dim DefaultMaxQTYD7 As Integer = 0
    Dim DefaultMaxQTYD8 As Integer = 0
    Dim DefaultMaxQTYD9 As Integer = 0
    Dim DefaultMaxQTYD10 As Integer = 0



    Dim ConsumeMethodD1 As String
    Dim ConsumeUnitD1 As String
    Dim ConsumeMethodD2 As String
    Dim ConsumeUnitD2 As String
    Dim ConsumeMethodD3 As String
    Dim ConsumeUnitD3 As String
    Dim ConsumeMethodD4 As String
    Dim ConsumeUnitD4 As String
    Dim ConsumeMethodD5 As String
    Dim ConsumeUnitD5 As String
    Dim ConsumeMethodD6 As String
    Dim ConsumeUnitD6 As String
    Dim ConsumeMethodD7 As String
    Dim ConsumeUnitD7 As String
    Dim ConsumeMethodD8 As String
    Dim ConsumeUnitD8 As String
    Dim ConsumeMethodD9 As String
    Dim ConsumeUnitD9 As String
    Dim ConsumeMethodD10 As String
    Dim ConsumeUnitD10 As String
    Dim ConsumeMethodIn1 As String
    Dim ConsumeUnitIn1 As String
    Dim ConsumeMethodIn2 As String
    Dim ConsumeUnitIn2 As String

    Dim Drug1Selected As Boolean = False
    Dim Drug2Selected As Boolean = False
    Dim Drug3Selected As Boolean = False
    Dim Drug4Selected As Boolean = False
    Dim Drug5Selected As Boolean = False
    Dim Drug6Selected As Boolean = False
    Dim Drug7Selected As Boolean = False
    Dim Drug8Selected As Boolean = False
    Dim Drug9Selected As Boolean = False
    Dim Drug10Selected As Boolean = False
    Dim Insulin1Selected As Boolean = False
    Dim Insulin2Selected As Boolean = False


    Private disableTextChanged As Boolean = False
    Private disableTextChangedDB As Boolean = False

    Dim notyetinitialize As Boolean = True

    Dim overwriten As Boolean = False

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Enable double buffering
        Me.SetStyle(ControlStyles.DoubleBuffer Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint, True)
        Me.UpdateStyles()

    End Sub



    Public Sub SetDoubleBuffered(dgv As DataGridView)
        Dim dgvType As Type = dgv.GetType()
        Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        pi.SetValue(dgv, True, Nothing)
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Form Initialization / First Load
        'Utility.FitFormToScreen(Me, 1200, 1920)
        'Me.CenterToScreen()


        InitializeAll()

        'First Function, Check for DB Connection Status
        checkDB()
        If checkDB() = False Then
            Return 'Exit from Function due to Database Initialization error
        End If
        'Load windows forms data
        'Method to Tabulate Data from Database to Drug Tab Table 
        DGV_Load()
        'Method to load autocomplete for Patient Name and IC No
        loadDBDataforPatientInfo()
        'Method to load autocomplete and drug names into Drug Combo Boxes
        loaddatafromdb()
        'Method to load autocomplete and insulin names into Insulin Combo Boxes
        loadInsulindatafromdb()
        'Method to load Log Datagridview for previous patient
        loadLogDGV()
        'Method to load Log > Records tab for Daily Records Data
        loadDGVRecords()
        '


        notyetinitialize = False
    End Sub

    Public Sub InitializeAll()

        SetDoubleBuffered(dgvRecords)

        dtpDateSeeDoctor.Value = Today

        dtpRecordsDateSelector.Value = Today
        dtpRecordsDateSelector.MaxDate = Today
        dtpRecordsDateSelectorEnd.MaxDate = Today
        dtpRecordsDateSelectorEnd.Value = Today

        dtpDrugQty1.Value = Today
        dtpDrugQty2.Value = Today

        dtpAllDrugsQty1.Value = Today
        dtpAllDrugsQty2.Value = Today

        cboxEnablePrintPDF.Checked = My.Settings.EnablePrintAfterSave
        cboxAutoClear.Checked = My.Settings.AutoClear
        cboxDisplayDateTime.Checked = My.Settings.EnableTime

        txtLabelHeight.Text = My.Settings.LabelHeight
        txtLabelWidth.Text = My.Settings.LabelWidth

        btnIOU.Enabled = False
        cbAddDays.SelectedIndex = 3
        GetDefaultPrinterName()
        getDefaultPrinters()
        disabledrug2to10()
        getUserSavedSettingsData()
        btnCheckICMySPR.Enabled = False
        InitializeDB()
        DataGridViewDrug.AllowUserToAddRows = False
        DataGridViewInsulin.AllowUserToAddRows = False
        dgvPatientDrugHistory.AllowUserToAddRows = False
        dgvPatientInsulinHistory.AllowUserToAddRows = False

        If cboxEnablePrintPDF.Checked Then
            btnSave.Text = "Save and Print"
            btnSave.BackColor = Color.GreenYellow
        End If
        If cboxEnablePrintPDF.Checked = False Then
            btnSave.Text = "Save only"
            btnSave.BackColor = Color.Azure
        End If

        DisplayDateTime()


        DevMode()
        ' Inside form load event
        ' Send the width and height of the screen you designed the form for
        populateDosageForms()

        SetupTooltips()

        'DataGridViewInsulin.Columns("Insulin Name").DefaultCellStyle.WrapMode = DataGridViewTriState.True

        ' Optionally, adjust row height to fit wrapped text
        DataGridViewInsulin.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells



    End Sub

    Private Sub SetandSaveDBSettings()

        Dim e As New EventArgs

        'Server settings
        Dim DBServerAddress As String = My.Settings.dbServerAddress
        Dim DBServerAddressNew As String
        DBServerAddressNew = txtDBServerAddress.Text
        My.Settings.dbServerAddress = DBServerAddressNew
        'My.Settings.Save()

        Dim DBUserID As String = My.Settings.dbUserID
        Dim DBUserIDNew As String
        DBUserIDNew = txtDBUserID.Text
        My.Settings.dbUserID = DBUserIDNew
        'My.Settings.Save()

        Dim DBPassword As String = My.Settings.dbPassword
        Dim DBPasswordNew As String
        DBPasswordNew = txtDBPassword.Text
        My.Settings.dbPassword = DBPasswordNew
        ' My.Settings.Save()

        Dim DBName As String = My.Settings.dbName
        Dim DBNameNew As String
        DBNameNew = txtDBName.Text
        My.Settings.dbName = DBNameNew

        My.Settings.Save()

        MsgBox("Saved Database Settings")
        MsgBox("Attempting to Reload Application..")
        Form1_Load(Me, e)
    End Sub
    Private Sub SetandSavePrinterSettings()
        If txtLabelHeight.Text < 30 Then
            MsgBox("Minimum Height is 30. Please try again.")
            Return
        End If
        If txtLabelHeight.Text > 100 Then
            MsgBox("Maximum Height is 100. Please try again.")
            Return
        End If
        If txtLabelWidth.Text < 40 Then
            MsgBox("Minimum Width is 40. Please try again.")
            Return
        End If
        If txtLabelWidth.Text > 100 Then
            MsgBox("Maximum Width is 100. Please try again.")
            Return
        End If

        Dim ClinicName As String = My.Settings.ClinicName
        Dim ClinicNameNew As String
        ClinicNameNew = txtClinicName.Text
        My.Settings.ClinicName = ClinicNameNew

        Dim LabelHeight As Integer = My.Settings.LabelHeight
        Dim LabelHeightNew As Integer
        LabelHeightNew = txtLabelHeight.Text
        My.Settings.LabelHeight = LabelHeightNew

        Dim LabelWidth As Integer = My.Settings.LabelWidth
        Dim LabelWidthNew As Integer
        LabelWidthNew = txtLabelWidth.Text
        My.Settings.LabelWidth = LabelWidthNew

        My.Settings.Save()
        'Set Default Printer
        Dim selectedPrinter As String = cboxDefaultPrinters.SelectedItem
        If selectedPrinter IsNot Nothing Then
            If SetDefaultPrinter(selectedPrinter) Then
                MessageBox.Show($"Default printer set to: {selectedPrinter}")
            Else
                MessageBox.Show("Failed to set default printer.")
            End If
        Else
            MessageBox.Show("Please select a printer from the list.")
        End If
        MsgBox("Saved Printer Settings")
        GetDefaultPrinterName()
    End Sub
    Public Sub getUserSavedSettingsData()
        txtDBServerAddress.Text = My.Settings.dbServerAddress
        txtDBUserID.Text = My.Settings.dbUserID
        txtDBPassword.Text = My.Settings.dbPassword
        txtDBName.Text = My.Settings.dbName
        txtClinicName.Text = My.Settings.ClinicName
    End Sub
    Public Sub calculateDurationMaster()
        Dim days
        'Initialize Duration at NEW Patient Tab

        days = dtpDateCollection.Value - dtpDateSaved.Value
        txtDurationMaster.Text = days.days + 1 & " days"
    End Sub

    Private Sub InitializeDB()

        Server = txtDBServerAddress.Text
        UID = txtDBUserID.Text
        PWD = txtDBPassword.Text
        DBName = txtDBName.Text

        myConnectionString = "server=" & Server & ";" _
                & "uid=" & UID & ";" _
                & "pwd=" & PWD & ";" _
                & "database=" & DBName
        conn.ConnectionString = myConnectionString
    End Sub
    Private Function checkDB() As Boolean
        Try
            conn.Open()
            pbrDatabaseConnection.Value = 100
            Return True

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            pbrDatabaseConnection.Value = 0
            Return False

        Finally
            conn.Close()
        End Try
    End Function

    Public Sub SetupTooltips()
        ToolTip1.SetToolTip(btnCopyDurationtoDoctor, "Click here to Copy Collection Date to Doctor Date")

        ToolTip1.SetToolTip(btnCheckICMySPR, "Press CTRL+V at SPR IC Form to paste IC No")
        ToolTip1.SetToolTip(btnIOU, "Paste the latest Patient Past Medication record to application")
        ToolTip1.SetToolTip(btnSave, "Saves Patient Data to Database and Print if Enabled")
        ToolTip1.SetToolTip(cboxIOU, "Tick here if Patient is IOU")
        ToolTip1.SetToolTip(btnClearAll, "Reset all the input forms")
    End Sub

    Private Sub GetDefaultPrinterName()
        Dim printerSettings As New PrinterSettings()
        Dim defaultPrinterName As String = printerSettings.PrinterName

        stlbPrinterName.Text = defaultPrinterName
        lblDefaultPrinterAtSetting.Text = defaultPrinterName
    End Sub
    Public Sub printPreview()
        'check for validations
        If checkICNewPatient() = False Then
            Return
        End If

        If checkDrugsInput() = False Then
            Return
        End If
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth



        If cbDrug1.Text = "" AndAlso cbInsulin1.Text = "" Then
            MsgBox("Nothing to print")
            Return
        End If

        If Drug1Selected Then
            PrintDoc.DefaultPageSettings.PaperSize = New PaperSize("Label Size", LabelHeightScaled, LabelWidthScaled) 'width, height in inch, 1 inch = 1000, 78mm = 3.07 inch, 48mm = 1.89 inch
            PrintDoc.DefaultPageSettings.Landscape = True


            CType(PPD.Controls(1), ToolStrip).Items(0).Enabled = False
            PPD.Document = PrintDoc
            currentPage = 1
            PPD.ShowDialog()


            'PrintDoc.Print()
        End If

        If Insulin1Selected Then
            printPreviewInsulin()
        End If
    End Sub

    Private Sub btnPrintPreview_Click(sender As Object, e As EventArgs) Handles btnPrintPreview.Click

        printPreview()
    End Sub

    Public Sub print()
        'check for validations
        If checkICNewPatient() = False Then
            Return
        End If

        If checkDrugsInput() = False Then
            Return
        End If
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

        If cbDrug1.Text = "" AndAlso cbInsulin1.Text = "" Then
            MsgBox("Nothing to print")
            Return
        End If

        If Drug1Selected Then

            PrintDoc.DefaultPageSettings.PaperSize = New PaperSize("Label Size", LabelHeightScaled, LabelWidthScaled) 'width, height
            PrintDoc.DefaultPageSettings.Landscape = True

            PrintDoc.DefaultPageSettings.Margins = New Margins(1, 1, 1, 1)
            PrintDoc.OriginAtMargins = True
            PrintDoc.PrintController = New StandardPrintController()


            'PPD.Document = PrintDoc
            'PPD.ShowDialog()
            currentPage = 1

            PrintDoc.Print()
        End If

        If Insulin1Selected Then
            printInsulin()
        End If
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

        print()
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDoc.PrintPage
        'Dim margins As Rectangle = e.MarginBounds

        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100
        'MessageBox.Show(margins.ToString() + "//Height: " + LabelHeight.ToString + "//Width: " + LabelWidth.ToString)
        'Return
        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth
        Dim RationalizedScale As Double = (ScaleHeight + ScaleWidth) / 2

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

        'Set Custom Names
        Dim ClinicName As String = txtClinicName.Text
        'Set Control Print Variable
        Dim stopprintflag As Boolean = False
        'Set Fonts
        Dim fontsize1 As Single = Math.Round(8 * RationalizedScale)
        Dim fontsize2 As Single = Math.Round(13 * RationalizedScale)
        Dim fontsize3 As Single = Math.Round(6 * RationalizedScale)
        Dim fontsize4 As Single = Math.Round(5 * RationalizedScale)
        'Dim fontsize3 As Single = Math.Round(8 * RationalizedScale)
        Dim f8 As New Font("Arial", fontsize1, FontStyle.Italic)
        Dim f8a As New Font("Arial", fontsize1, FontStyle.Bold)
        Dim f8b As New Font("Arial", fontsize2, FontStyle.Bold)
        Dim f4 As New Font("Arial", fontsize3, FontStyle.Bold)
        Dim f4b As New Font("Arial", fontsize4, FontStyle.Bold)
        'Set Alignments
        Dim left As New StringFormat
        Dim centre As New StringFormat
        Dim right As New StringFormat

        left.Alignment = StringAlignment.Near
        centre.Alignment = StringAlignment.Center
        right.Alignment = StringAlignment.Far

        ''Draw Rectangles FOR Drug Prescriptions
        'Dim Rect1 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(5 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(178 * ScaleHeight)) '( x margin, y margin, width, height) 'Border
        'Dim Rect2 As New Rectangle(Math.Round(6 * ScaleWidth), 20, 295, 45)
        'Dim Rect3 As New Rectangle(Math.Round(6 * ScaleWidth), 45, 295, 20)

        'Dim Rect4 As New Rectangle(Math.Round(7 * ScaleWidth), 70, 293, 25) 'medicine name 
        'Dim Rect5 As New Rectangle(Math.Round(6 * ScaleWidth), 96, 295, 25)
        'Dim Rect6 As New Rectangle(Math.Round(7 * ScaleWidth), 100, 290, 20)

        'Dim Rect7 As New Rectangle(Math.Round(6 * ScaleWidth), 121, 295, 40)
        'Dim Rect8 As New Rectangle(Math.Round(7 * ScaleWidth), 128, 290, 30) 'Remark margin

        'Dim Rect9 As New Rectangle(Math.Round(201 * ScaleWidth), 170, 70, 12) 'Jumlah

        'Draw Rectangles FOR Drug Prescriptions
        Dim Rect1 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(5 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(178 * ScaleHeight)) '( x margin, y margin, width, height) 'Border
        Dim Rect2 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(20 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(45 * ScaleHeight))
        Dim Rect3 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(45 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(20 * ScaleHeight))

        Dim Rect4 As New Rectangle(Math.Round(8 * ScaleWidth), Math.Round(68 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(25 * ScaleHeight)) 'medicine name 
        Dim Rect5 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(96 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(27 * ScaleHeight))
        Dim Rect6 As New Rectangle(Math.Round(8 * ScaleWidth), Math.Round(100 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(20 * ScaleHeight))

        Dim Rect7 As New Rectangle(Math.Round(6 * ScaleWidth), Math.Round(123 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(40 * ScaleHeight))
        Dim Rect8 As New Rectangle(Math.Round(8 * ScaleWidth), Math.Round(130 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(30 * ScaleHeight)) 'Remark margin

        Dim Rect9 As New Rectangle(Math.Round(201 * ScaleWidth), Math.Round(167 * ScaleHeight), Math.Round(75 * ScaleWidth), Math.Round(12 * ScaleHeight)) 'Jumlah

        Dim Rect10 As New Rectangle(Math.Round(10 * ScaleWidth), Math.Round(169 * ScaleHeight), Math.Round(100 * ScaleWidth), Math.Round(12 * ScaleHeight)) 'Ubat Terkawal

        Dim Rect11 As New Rectangle(Math.Round(110 * ScaleWidth), Math.Round(166 * ScaleHeight), Math.Round(75 * ScaleWidth), Math.Round(14 * ScaleHeight)) 'Jauhi daripada Kanak-Kanak

        Try
            If cbDrug1.Text = "" Then
                e.HasMorePages = False
                Return
            End If

            e.Graphics.DrawRectangle(Pens.Black, Rect1)
            e.Graphics.DrawRectangle(Pens.Black, Rect2)
            e.Graphics.DrawRectangle(Pens.Black, Rect3)
            e.Graphics.DrawRectangle(Pens.White, Rect4) 'medicine name
            e.Graphics.DrawRectangle(Pens.Black, Rect5)
            e.Graphics.DrawRectangle(Pens.White, Rect6)
            e.Graphics.DrawRectangle(Pens.Black, Rect7)
            e.Graphics.DrawRectangle(Pens.White, Rect8) 'Remark margin
            e.Graphics.DrawRectangle(Pens.White, Rect9)


            'e.Graphics.Clear(Color.White)


            e.Graphics.DrawString(ClinicName, f8, Brushes.Black, Rect1, centre)

            e.Graphics.DrawString("NAMA : " & txtPatientName.Text, f8a, Brushes.Black, Rect2, left)
            e.Graphics.DrawString("TARIKH : " & dtpDateSaved.Value.ToString("dd MMMM yyyy"), f8a, Brushes.Black, Rect3, left)


            'Print Selection of currentPage
            Select Case currentPage
                Case 1
                    If cbDrug2.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD1.Contains("Minum ") And ConsumeUnitD1.Contains("ml") Then
                        Dim consumedose = txtDoseD1.Text / lblStrD1.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD1.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD1.Text / lblStrD1.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If



                    'Check for Blank Selection of Drug


                    e.Graphics.DrawString(cbDrug1.Text, f8a, Brushes.Black, Rect4, centre)
                    ' Only execute if either ConsumeMethodD1 or ConsumeUnitD1 is not empty
                    If Not String.IsNullOrEmpty(ConsumeMethodD1) Or Not String.IsNullOrEmpty(ConsumeUnitD1) Then
                        e.Graphics.DrawString(ConsumeMethodD1 & consumedosefinal & ConsumeUnitD1 & txtFreqD1.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    End If


                    e.Graphics.DrawString(RemarkD1, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD1.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)


                Case 2
                    'Check for Blank Selection of Drug
                    If cbDrug3.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True
                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD2.Contains("Minum ") And ConsumeUnitD2.Contains("ml") Then
                        Dim consumedose = txtDoseD2.Text / lblStrD2.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD2.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD2.Text / lblStrD2.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug2.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD2 & consumedosefinal & ConsumeUnitD2 & txtFreqD2.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD2, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD2.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 3
                    'Check for Blank Selection of Drug
                    If cbDrug4.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD3.Contains("Minum ") And ConsumeUnitD3.Contains("ml") Then 
                        Dim consumedose = txtDoseD3.Text / lblStrD3.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD3.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD3.Text / lblStrD3.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug3.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD3 & consumedosefinal & ConsumeUnitD3 & txtFreqD3.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD3, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD3.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 4
                    'Check for Blank Selection of Drug
                    If cbDrug5.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD4.Contains("Minum ") And ConsumeUnitD4.Contains("ml") Then
                        Dim consumedose = txtDoseD4.Text / lblStrD4.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD4.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD4.Text / lblStrD4.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug4.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD4 & consumedosefinal & ConsumeUnitD4 & txtFreqD4.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD4, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD4.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 5
                    'Check for Blank Selection of Drug
                    If cbDrug6.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD5.Contains("Minum ") And ConsumeUnitD5.Contains("ml") Then
                        Dim consumedose = txtDoseD5.Text / lblStrD5.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD5.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD5.Text / lblStrD5.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug5.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD5 & consumedosefinal & ConsumeUnitD5 & txtFreqD5.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD5, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD5.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 6
                    'Check for Blank Selection of Drug
                    If cbDrug7.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD6.Contains("Minum ") And ConsumeUnitD6.Contains("ml") Then
                        Dim consumedose = txtDoseD6.Text / lblStrD6.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD6.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD6.Text / lblStrD6.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug6.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD6 & consumedosefinal & ConsumeUnitD6 & txtFreqD6.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD6, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD6.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 7
                    'Check for Blank Selection of Drug
                    If cbDrug8.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD7.Contains("Minum ") And ConsumeUnitD7.Contains("ml") Then
                        Dim consumedose = txtDoseD7.Text / lblStrD7.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD7.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD7.Text / lblStrD7.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug7.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD7 & consumedosefinal & ConsumeUnitD7 & txtFreqD7.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD7, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD7.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 8
                    'Check for Blank Selection of Drug
                    If cbDrug9.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD8.Contains("Minum ") And ConsumeUnitD8.Contains("ml") Then
                        Dim consumedose = txtDoseD8.Text / lblStrD8.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD8.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD8.Text / lblStrD8.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug8.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD8 & consumedosefinal & ConsumeUnitD8 & txtFreqD8.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD8, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD8.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 9
                    'Check for Blank Selection of Drug
                    If cbDrug10.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD9.Contains("Minum ") And ConsumeUnitD9.Contains("ml") Then
                        Dim consumedose = txtDoseD9.Text / lblStrD9.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD9.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD9.Text / lblStrD9.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug9.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD9 & consumedosefinal & ConsumeUnitD9 & txtFreqD9.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD9, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD9.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)
                Case 10
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD10.Contains("Minum ") And ConsumeUnitD10.Contains("ml") Then
                        Dim consumedose = txtDoseD10.Text / lblStrD10.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    ElseIf ConsumeMethodD10.Contains("Sapu ") Then
                        consumedosefinal = ""
                    Else
                        Dim consumedose = txtDoseD10.Text / lblStrD10.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    'MsgBox("Drug Items: " & NoOfItemsRecord)
                    e.Graphics.DrawString(cbDrug10.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD10 & consumedosefinal & ConsumeUnitD10 & txtFreqD10.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD10, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD10.Text, f8a, Brushes.Black, Rect9, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)

            End Select
            ' Increment the page counter
            currentPage += 1
            ' e.Graphics.Clear(Color.White) 'Clear the print page

            If currentPage <= 10 And stopprintflag = False Then
                ' Set to true to continue printing
                e.HasMorePages = True
            Else

                ' Set to false to stop printing
                e.HasMorePages = False

                Return
            End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkCancel, "Print Error")

        End Try


    End Sub
    Function ConvertToFraction(ByVal consumedose As Double) As String
        consumedose = Math.Round(consumedose, 2)
        If consumedose Mod 1 = 0.5 Then
            If consumedose < 1 Then
                Return String.Format("½", Math.Floor(consumedose))
            End If
            ' Return the integer part and 1/2
            Return String.Format("{0} ½", Math.Floor(consumedose))
        ElseIf consumedose Mod 1 = 0.75 Then
            If consumedose < 1 Then
                Return String.Format("¾", Math.Floor(consumedose))
            End If
            Return String.Format("{0} ¾", Math.Floor(consumedose))
        ElseIf consumedose Mod 1 = 0.25 Then
            If consumedose < 1 Then
                Return String.Format("¼", Math.Floor(consumedose))
            End If
            Return String.Format("{0} ¼", Math.Floor(consumedose))
        Else
            Return consumedose.ToString
        End If
    End Function
    Private Sub PrintDocInsulin_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocInsulin.PrintPage
        Try
            Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
            Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

            Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
            Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth
            Dim RationalizedScale As Double = (ScaleHeight + ScaleWidth) / 2

            Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
            Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

            Dim fontscale1 As Single = Math.Round(RationalizedScale * 8)
            Dim fontscale2 As Single = Math.Round(RationalizedScale * 10)
            Dim fontscale3 As Single = Math.Round(6 * RationalizedScale)
            Dim fontscale4 As Single = Math.Round(5 * RationalizedScale)

            'Perform checking on insulin selections
            If Insulin1Selected = False Then
                Return
            End If
            Dim stopprintflag As Boolean = False
            If Insulin2Selected = False Then
                stopprintflag = True
            End If

            'Initialize printing parameters
            'Set Custom Names
            Dim ClinicName As String = txtClinicName.Text
            'Set Control Print Variable

            'Set Fonts
            Dim f8 As New Font("Arial", fontscale1, FontStyle.Italic)
            Dim f8a As New Font("Arial", fontscale1, FontStyle.Bold)
            Dim f8b As New Font("Arial", fontscale2, FontStyle.Bold)
            Dim f4 As New Font("Arial", fontscale3, FontStyle.Bold)
            Dim f4b As New Font("Arial", fontscale4, FontStyle.Bold)
            'Set Alignments
            Dim left As New StringFormat
            Dim centre As New StringFormat
            Dim right As New StringFormat

            left.Alignment = StringAlignment.Near
            centre.Alignment = StringAlignment.Center
            right.Alignment = StringAlignment.Far
            Dim Rect1a As New Rectangle(Math.Round(5 * ScaleWidth), Math.Round(4 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(181 * ScaleHeight)) '(x location, y location, width, height) 'Border
            Dim Rect2a As New Rectangle(Math.Round(5 * ScaleWidth), Math.Round(20 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(45 * ScaleHeight))

            Dim Rect3a As New Rectangle(Math.Round(5 * ScaleWidth), Math.Round(45 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(20 * ScaleHeight)) 'insulin rectangle
            Dim Rect4a As New Rectangle(Math.Round(7 * ScaleWidth), Math.Round(70 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(25 * ScaleHeight)) 'insulin name margin

            Dim Rect5a As New Rectangle(Math.Round(5 * ScaleWidth), Math.Round(99 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(42 * ScaleHeight)) 'Cara 
            Dim Rect6a As New Rectangle(Math.Round(7 * ScaleWidth), Math.Round(101 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(40 * ScaleHeight)) 'Cara margin

            Dim Rect7a As New Rectangle(Math.Round(5 * ScaleWidth), Math.Round(141 * ScaleHeight), Math.Round(295 * ScaleWidth), Math.Round(25 * ScaleHeight))
            Dim Rect8a As New Rectangle(Math.Round(7 * ScaleWidth), Math.Round(145 * ScaleHeight), Math.Round(290 * ScaleWidth), Math.Round(18 * ScaleHeight)) 'Remark margin


            Dim Rect9a As New Rectangle(Math.Round(190 * ScaleWidth), Math.Round(170 * ScaleHeight), Math.Round(105 * ScaleWidth), Math.Round(14 * ScaleHeight)) 'Jumlah

            Dim Rect10 As New Rectangle(Math.Round(10 * ScaleWidth), Math.Round(171 * ScaleHeight), Math.Round(100 * ScaleWidth), Math.Round(12 * ScaleHeight)) 'Ubat Terkawal

            Dim Rect11 As New Rectangle(Math.Round(110 * ScaleWidth), Math.Round(168 * ScaleHeight), Math.Round(75 * ScaleWidth), Math.Round(14 * ScaleHeight)) 'Jauhi daripada Kanak-Kanak

            'Insulin 1
            Dim combinedwords As String = "Suntik "
            Dim unitpagi As String = " unit pagi"
            Dim unittghari As String = " unit tengahari"
            Dim unitpetang As String = " unit petang"
            Dim unitmalam As String = " unit malam"

            Dim In1MorDose = txtIn1MorDose.Text
            Dim In1NoonDose = txtIn1NoonDose.Text
            Dim In1AfterNoonDose = txtIn1AfterNoonDose.Text
            Dim In1NightDose = txtIn1NightDose.Text

            Dim doses As New List(Of String)

            ' Add doses to the list if they are not zero or empty
            If In1MorDose <> "" AndAlso Val(In1MorDose) > 0 Then
                doses.Add(In1MorDose & unitpagi)
            End If

            If In1NoonDose <> "" AndAlso Val(In1NoonDose) > 0 Then
                doses.Add(In1NoonDose & unittghari)
            End If

            If In1AfterNoonDose <> "" AndAlso Val(In1AfterNoonDose) > 0 Then
                doses.Add(In1AfterNoonDose & unitpetang)
            End If

            If In1NightDose <> "" AndAlso Val(In1NightDose) > 0 Then
                doses.Add(In1NightDose & unitmalam)
            End If

            ' Join the doses with a comma, except for the last one
            combinedwords &= String.Join(", ", doses)

            ' If no doses are added, set combinedwords to an empty string
            If combinedwords = "Suntik " Then
                combinedwords = ""
            End If


            'Insulin 2

            'Insulin 2
            Dim combinedwords2 As String = "Suntik "
            Dim unitpagi2 As String = " unit pagi"
            Dim unittghari2 As String = " unit tengahari"
            Dim unitpetang2 As String = " unit petang"
            Dim unitmalam2 As String = " unit malam"

            Dim In2MorDose = txtIn2MorDose.Text
            Dim In2NoonDose = txtIn2NoonDose.Text
            Dim In2AfterNoonDose = txtIn2AfterNoonDose.Text
            Dim In2NightDose = txtIn2NightDose.Text

            Dim doses2 As New List(Of String)

            ' Add doses to the list if they are not zero or empty
            If In2MorDose <> "" AndAlso Val(In2MorDose) > 0 Then
                doses2.Add(In2MorDose & unitpagi2)
            End If

            If In2NoonDose <> "" AndAlso Val(In2NoonDose) > 0 Then
                doses2.Add(In2NoonDose & unittghari2)
            End If

            If In2AfterNoonDose <> "" AndAlso Val(In2AfterNoonDose) > 0 Then
                doses2.Add(In2AfterNoonDose & unitpetang2)
            End If

            If In2NightDose <> "" AndAlso Val(In2NightDose) > 0 Then
                doses2.Add(In2NightDose & unitmalam2)
            End If

            ' Join the doses with a comma, except for the last one
            combinedwords2 &= String.Join(", ", doses2)

            ' If no doses are added, set combinedwords2 to an empty string
            If combinedwords2 = "Suntik " Then
                combinedwords2 = ""
            End If




            e.Graphics.DrawRectangle(Pens.Black, Rect1a) 'clinic name, 
            e.Graphics.DrawRectangle(Pens.Black, Rect2a) 'patient name
            e.Graphics.DrawRectangle(Pens.Black, Rect3a) 'insulin rectangle
            e.Graphics.DrawRectangle(Pens.White, Rect4a) 'insulin name margin
            e.Graphics.DrawRectangle(Pens.Black, Rect5a) 'Cara 
            e.Graphics.DrawRectangle(Pens.White, Rect6a) 'Cara margin
            e.Graphics.DrawRectangle(Pens.Black, Rect7a) 'Remark
            e.Graphics.DrawRectangle(Pens.White, Rect8a) 'Remark margin
            e.Graphics.DrawRectangle(Pens.White, Rect9a)

            'e.Graphics.Clear(Color.White)


            e.Graphics.DrawString(ClinicName, f8, Brushes.Black, Rect1a, centre)

            e.Graphics.DrawString("NAMA : " & txtPatientName.Text, f8a, Brushes.Black, Rect2a, left)
            e.Graphics.DrawString("TARIKH : " & dtpDateSaved.Value.ToString("dd MMMM yyyy"), f8a, Brushes.Black, Rect3a, left)

            Select Case currentPageInsulin
                Case 1

                    'MsgBox("Insulin Items: " & NoOfItemsRecord)
                    e.Graphics.DrawString(cbInsulin1.Text, f8a, Brushes.Black, Rect4a, centre)
                    e.Graphics.DrawString(combinedwords, f8b, Brushes.Black, Rect6a, centre)
                    e.Graphics.DrawString(RemarkIn1, f8a, Brushes.Black, Rect8a, centre)
                    e.Graphics.DrawString("Jumlah Katrij: " & txtIn1CartQTY.Text, f8a, Brushes.Black, Rect9a, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)

                Case 2

                    e.Graphics.DrawString(cbInsulin2.Text, f8a, Brushes.Black, Rect4a, centre)
                    e.Graphics.DrawString(combinedwords2, f8b, Brushes.Black, Rect6a, centre)
                    e.Graphics.DrawString(RemarkIn2, f8a, Brushes.Black, Rect8a, centre)
                    e.Graphics.DrawString("Jumlah Katrij: " & txtIn2CartQTY.Text, f8a, Brushes.Black, Rect9a, left)
                    e.Graphics.DrawString("UBAT TERKAWAL", f4, Brushes.Black, Rect10, left)
                    e.Graphics.DrawString("JAUHI DARIPADA KANAK - KANAK", f4b, Brushes.Black, Rect11, left)

            End Select

            currentPageInsulin += 1
            'Checking for more pages
            If currentPageInsulin <= 2 And stopprintflag = False Then
                ' Set to true to continue printing
                e.HasMorePages = True

                'MsgBox("Insulin Items: " & NoOfItemsRecord)
            Else

                ' Set to false to stop printing
                e.HasMorePages = False

                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub printInsulin()
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

        PrintDocInsulin.DefaultPageSettings.PaperSize = New PaperSize("Insulin Label Size", LabelHeightScaled, LabelWidthScaled) 'width, height
        PrintDocInsulin.DefaultPageSettings.Landscape = True

        PrintDocInsulin.DefaultPageSettings.Margins = New Margins(1, 1, 1, 1)
        PrintDocInsulin.OriginAtMargins = True
        PrintDocInsulin.PrintController = New StandardPrintController()



        'PPD.Document = PrintDocInsulin
        'PPD.ShowDialog()
        currentPageInsulin = 1
        PrintDocInsulin.Print()


    End Sub
    Public Sub printPreviewInsulin()
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

        PrintDocInsulin.DefaultPageSettings.PaperSize = New PaperSize("Label Size", LabelHeightScaled, LabelWidthScaled) 'width, height
        PrintDocInsulin.DefaultPageSettings.Landscape = True

        CType(PPD.Controls(1), ToolStrip).Items(0).Enabled = False
        PPD.Document = PrintDocInsulin
        currentPageInsulin = 1
        PPD.ShowDialog()

        'PrintDocInsulin.Print()


    End Sub

    ' Import the necessary methods to set the default printer
    <DllImport("winspool.drv", CharSet:=CharSet.Auto, SetLastError:=True)>
    Public Shared Function SetDefaultPrinter(ByVal pszPrinter As String) As Boolean
    End Function

    Public Sub getDefaultPrinters()

        ' Populate the ComboBox with installed printers
        For Each printer As String In PrinterSettings.InstalledPrinters
            cboxDefaultPrinters.Items.Add(printer)
        Next
        ' Optionally, select the current default printer
        cboxDefaultPrinters.SelectedItem = New PrinterSettings().PrinterName
    End Sub

    Public Function checkICNewPatient() As Boolean
        Dim stPatientName As String

        Dim stIC As String
        Dim dtDateRegister As Date
        Dim dtDateSeeDoctor As Date

        stPatientName = txtPatientName.Text
        stIC = txtICNo.Text
        dtDateRegister = dtpDateSaved.Text
        dtDateSeeDoctor = dtpDateCollection.Text

        Dim ICRegexPattern As String = "^((\d{2}(?!0229))|([02468][048]|[13579][26])(?=0229))(0[1-9]|1[0-2])(0[1-9]|[12]\d|(?<!02)30|(?<!02|4|6|9|11)31)-(\d{2})-(\d{4})$"

        Try
            'Patient Name validation
            If chboxNoICNumber.Checked = False Then
                If stPatientName = "" Then
                    MsgBox("Error. Name cannot be empty")
                    Return False
                End If
                'Patient IC Validation
                If stIC.Length < 14 Then
                    MsgBox("Error. IC No. must be 12 digits")
                    Return False
                ElseIf Regex.IsMatch(stIC, ICRegexPattern) = False Then
                    MsgBox("Error. IC No. Regex Formatting is Invalid. Please check and try again")
                    Return False
                End If


            End If
        Catch
            Return False
        End Try
        Return True
    End Function





    ' Reusable function to validate drug inputs
    Private Function ValidateDrug(drugNumber As Integer, selectedIndex As Integer, drugText As String, doseText As String, freqText As String, durationText As String, qtyText As String) As Boolean
        If selectedIndex >= 0 Then
            If doseText.Length <= 0 Or freqText.Length <= 0 Or durationText.Length <= 0 Or qtyText.Length <= 0 Or qtyText.Length <= 0 Then
                MsgBox("Drug " & drugNumber & " Error input")
                Return False
            End If
        ElseIf drugText.Length > 0 Then
            MsgBox("Drug " & drugNumber & " No Selection")
            Return False
        End If
        Return True
    End Function

    ' Reusable function to validate insulin inputs
    Private Function ValidateInsulin(insulinNumber As Integer, selectedIndex As Integer, totalDoseText As String, cartQtyText As String) As Boolean
        If selectedIndex >= 0 Then
            If totalDoseText.Length <= 0 Or totalDoseText.Length <= 0 Or cartQtyText.Length <= 0 Then
                MsgBox("Insulin " & insulinNumber & " input error")
                Return False
            End If
        End If
        Return True
    End Function

    ' Main validation function
    Public Function checkDrugsInput() As Boolean
        Try
            ' Validate drugs 1 to 10
            If Not ValidateDrug(1, cbDrug1.SelectedIndex, cbDrug1.Text, txtDoseD1.Text, txtFreqD1.Text, txtDurationD1.Text, txtQTYD1.Text) Then Return False
            If Not ValidateDrug(2, cbDrug2.SelectedIndex, cbDrug2.Text, txtDoseD2.Text, txtFreqD2.Text, txtDurationD2.Text, txtQTYD2.Text) Then Return False
            If Not ValidateDrug(3, cbDrug3.SelectedIndex, cbDrug3.Text, txtDoseD3.Text, txtFreqD3.Text, txtDurationD3.Text, txtQTYD3.Text) Then Return False
            If Not ValidateDrug(4, cbDrug4.SelectedIndex, cbDrug4.Text, txtDoseD4.Text, txtFreqD4.Text, txtDurationD4.Text, txtQTYD4.Text) Then Return False
            If Not ValidateDrug(5, cbDrug5.SelectedIndex, cbDrug5.Text, txtDoseD5.Text, txtFreqD5.Text, txtDurationD5.Text, txtQTYD5.Text) Then Return False
            If Not ValidateDrug(6, cbDrug6.SelectedIndex, cbDrug6.Text, txtDoseD6.Text, txtFreqD6.Text, txtDurationD6.Text, txtQTYD6.Text) Then Return False
            If Not ValidateDrug(7, cbDrug7.SelectedIndex, cbDrug7.Text, txtDoseD7.Text, txtFreqD7.Text, txtDurationD7.Text, txtQTYD7.Text) Then Return False
            If Not ValidateDrug(8, cbDrug8.SelectedIndex, cbDrug8.Text, txtDoseD8.Text, txtFreqD8.Text, txtDurationD8.Text, txtQTYD8.Text) Then Return False
            If Not ValidateDrug(9, cbDrug9.SelectedIndex, cbDrug9.Text, txtDoseD9.Text, txtFreqD9.Text, txtDurationD9.Text, txtQTYD9.Text) Then Return False
            If Not ValidateDrug(10, cbDrug10.SelectedIndex, cbDrug10.Text, txtDoseD10.Text, txtFreqD10.Text, txtDurationD10.Text, txtQTYD10.Text) Then Return False

            ' Check if at least one drug or insulin is selected
            If cbDrug1.SelectedIndex < 0 And cbDrug2.SelectedIndex < 0 And cbDrug3.SelectedIndex < 0 And
           cbDrug4.SelectedIndex < 0 And cbDrug5.SelectedIndex < 0 And cbDrug6.SelectedIndex < 0 And
           cbDrug7.SelectedIndex < 0 And cbDrug8.SelectedIndex < 0 And cbDrug9.SelectedIndex < 0 And
           cbDrug10.SelectedIndex < 0 And cbInsulin1.SelectedIndex < 0 And cbInsulin2.SelectedIndex < 0 Then
                MsgBox("No Drug or Insulin selected")
                Return False
            End If

            ' Validate insulin inputs
            If Not ValidateInsulin(1, cbInsulin1.SelectedIndex, txtIn1TotalDose.Text, txtIn1CartQTY.Text) Then Return False
            If Not ValidateInsulin(2, cbInsulin2.SelectedIndex, txtIn2TotalDose.Text, txtIn2CartQTY.Text) Then Return False

            Return True

        Catch ex As Exception
            MsgBox("Input error: " & ex.Message)
            Return False
        End Try
    End Function

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'NEW PATIENT TAB
        'SAVE BUTTON
        Dim stPatientName As String

        Dim stIC As String
        Dim dtDateRegister As Date
        Dim dtDateSeeDoctor As Date

        stPatientName = txtPatientName.Text
        stIC = txtICNo.Text
        dtDateRegister = dtpDateSaved.Text
        dtDateSeeDoctor = dtpDateCollection.Text

        Dim ICRegexPattern As String = "^((\d{2}(?!0229))|([02468][048]|[13579][26])(?=0229))(0[1-9]|1[0-2])(0[1-9]|[12]\d|(?<!02)30|(?<!02|4|6|9|11)31)-(\d{2})-(\d{4})$"

        If checkICNewPatient() = False Then
            Return
        End If

        If checkDrugsInput() = False Then
            Return
        End If



        'MsgBox("Saved data for " & stPatientName & ", IC No.: " & stIC)

        'For Testing validations, enable return
        'Return
        Try

            conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `prescribeddrugs` 
            (`Name`,`ICNo`,`Date`,`DateCollection`,`DateSeeDoctor`,
            `Drug1Name`,`Drug1Strength`,`Drug1Unit`,`Drug1Dose`,`Drug1Freq`,`Drug1Duration`,`Drug1TotalQTY`,
            `Drug2Name`,`Drug2Strength`,`Drug2Unit`,`Drug2Dose`,`Drug2Freq`,`Drug2Duration`,`Drug2TotalQTY`,
            `Drug3Name`,`Drug3Strength`,`Drug3Unit`,`Drug3Dose`,`Drug3Freq`,`Drug3Duration`,`Drug3TotalQTY`,
            `Drug4Name`,`Drug4Strength`,`Drug4Unit`,`Drug4Dose`,`Drug4Freq`,`Drug4Duration`,`Drug4TotalQTY`,
            `Drug5Name`,`Drug5Strength`,`Drug5Unit`,`Drug5Dose`,`Drug5Freq`,`Drug5Duration`,`Drug5TotalQTY`,
            `Drug6Name`,`Drug6Strength`,`Drug6Unit`,`Drug6Dose`,`Drug6Freq`,`Drug6Duration`,`Drug6TotalQTY`,
            `Drug7Name`,`Drug7Strength`,`Drug7Unit`,`Drug7Dose`,`Drug7Freq`,`Drug7Duration`,`Drug7TotalQTY`,
            `Drug8Name`,`Drug8Strength`,`Drug8Unit`,`Drug8Dose`,`Drug8Freq`,`Drug8Duration`,`Drug8TotalQTY`,
            `Drug9Name`,`Drug9Strength`,`Drug9Unit`,`Drug9Dose`,`Drug9Freq`,`Drug9Duration`,`Drug9TotalQTY`,
            `Drug10Name`,`Drug10Strength`,`Drug10Unit`,`Drug10Dose`,`Drug10Freq`,`Drug10Duration`,`Drug10TotalQTY`,
            `Insulin1Name`,`Insulin1Strength`,`Insulin1Unit`,`Insulin1MorDose`,`Insulin1NoonDose`,`Insulin1AfternoonDose`,`Insulin1NightDose`,`Insulin1Freq`,`Insulin1Duration`,`Insulin1TotalDose`,`Insulin1POM`,`Insulin1CartQTY`,
            `Insulin2Name`,`Insulin2Strength`,`Insulin2Unit`,`Insulin2MorDose`,`Insulin2NoonDose`,`Insulin2AfternoonDose`,`Insulin2NightDose`,`Insulin2Freq`,`Insulin2Duration`,`Insulin2TotalDose`,`Insulin2POM`,`Insulin2CartQTY`)
                                         VALUES 
            (@Name,@ICNo,@Date,@DateCollection,@DateSeeDoctor,
            @Drug1Name,@Drug1Strength,@Drug1Unit,@Drug1Dose,@Drug1Freq,@Drug1Duration,@Drug1TotalQTY,
            @Drug2Name,@Drug2Strength,@Drug2Unit,@Drug2Dose,@Drug2Freq,@Drug2Duration,@Drug2TotalQTY,
            @Drug3Name,@Drug3Strength,@Drug3Unit,@Drug3Dose,@Drug3Freq,@Drug3Duration,@Drug3TotalQTY,
            @Drug4Name,@Drug4Strength,@Drug4Unit,@Drug4Dose,@Drug4Freq,@Drug4Duration,@Drug4TotalQTY,
            @Drug5Name,@Drug5Strength,@Drug5Unit,@Drug5Dose,@Drug5Freq,@Drug5Duration,@Drug5TotalQTY,
            @Drug6Name,@Drug6Strength,@Drug6Unit,@Drug6Dose,@Drug6Freq,@Drug6Duration,@Drug6TotalQTY,
            @Drug7Name,@Drug7Strength,@Drug7Unit,@Drug7Dose,@Drug7Freq,@Drug7Duration,@Drug7TotalQTY,
            @Drug8Name,@Drug8Strength,@Drug8Unit,@Drug8Dose,@Drug8Freq,@Drug8Duration,@Drug8TotalQTY,
            @Drug9Name,@Drug9Strength,@Drug9Unit,@Drug9Dose,@Drug9Freq,@Drug9Duration,@Drug9TotalQTY,
            @Drug10Name,@Drug10Strength,@Drug10Unit,@Drug10Dose,@Drug10Freq,@Drug10Duration,@Drug10TotalQTY,
            @Insulin1Name,@Insulin1Strength,@Insulin1Unit,@Insulin1MorDose,@Insulin1NoonDose,@Insulin1AfternoonDose,@Insulin1NightDose,@Insulin1Freq,@Insulin1Duration,@Insulin1TotalDose,@Insulin1POM,@Insulin1CartQTY,
            @Insulin2Name,@Insulin2Strength,@Insulin2Unit,@Insulin2MorDose,@Insulin2NoonDose,@Insulin2AfternoonDose,@Insulin2NightDose,@Insulin2Freq,@Insulin2Duration,@Insulin2TotalDose,@Insulin2POM,@Insulin2CartQTY)
            ", conn)
            'Patient Info
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Name", txtPatientName.Text)
            cmd.Parameters.AddWithValue("@ICNo", txtICNo.Text)
            cmd.Parameters.AddWithValue("@Date", dtpDateSaved.Text)
            cmd.Parameters.AddWithValue("@DateCollection", dtpDateCollection.Text)
            cmd.Parameters.AddWithValue("@DateSeeDoctor", dtpDateSeeDoctor.Text)

            'Drug 1
            cmd.Parameters.AddWithValue("@Drug1Name", cbDrug1.Text)
            cmd.Parameters.AddWithValue("@Drug1Strength", lblStrD1.Text)
            cmd.Parameters.AddWithValue("@Drug1Unit", lblUnitD1.Text)
            cmd.Parameters.AddWithValue("@Drug1Dose", txtDoseD1.Text)
            cmd.Parameters.AddWithValue("@Drug1Freq", txtFreqD1.Text)
            cmd.Parameters.AddWithValue("@Drug1Duration", txtDurationD1.Text)
            cmd.Parameters.AddWithValue("@Drug1TotalQTY", txtQTYD1.Text)

            'Drug 2
            cmd.Parameters.AddWithValue("@Drug2Name", cbDrug2.Text)
            cmd.Parameters.AddWithValue("@Drug2Strength", lblStrD2.Text)
            cmd.Parameters.AddWithValue("@Drug2Unit", lblUnitD2.Text)
            cmd.Parameters.AddWithValue("@Drug2Dose", txtDoseD2.Text)
            cmd.Parameters.AddWithValue("@Drug2Freq", txtFreqD2.Text)
            cmd.Parameters.AddWithValue("@Drug2Duration", txtDurationD2.Text)
            cmd.Parameters.AddWithValue("@Drug2TotalQTY", txtQTYD2.Text)

            'Drug 3
            cmd.Parameters.AddWithValue("@Drug3Name", cbDrug3.Text)
            cmd.Parameters.AddWithValue("@Drug3Strength", lblStrD3.Text)
            cmd.Parameters.AddWithValue("@Drug3Unit", lblUnitD3.Text)
            cmd.Parameters.AddWithValue("@Drug3Dose", txtDoseD3.Text)
            cmd.Parameters.AddWithValue("@Drug3Freq", txtFreqD3.Text)
            cmd.Parameters.AddWithValue("@Drug3Duration", txtDurationD3.Text)
            cmd.Parameters.AddWithValue("@Drug3TotalQTY", txtQTYD3.Text)

            'Drug 4
            cmd.Parameters.AddWithValue("@Drug4Name", cbDrug4.Text)
            cmd.Parameters.AddWithValue("@Drug4Strength", lblStrD4.Text)
            cmd.Parameters.AddWithValue("@Drug4Unit", lblUnitD4.Text)
            cmd.Parameters.AddWithValue("@Drug4Dose", txtDoseD4.Text)
            cmd.Parameters.AddWithValue("@Drug4Freq", txtFreqD4.Text)
            cmd.Parameters.AddWithValue("@Drug4Duration", txtDurationD4.Text)
            cmd.Parameters.AddWithValue("@Drug4TotalQTY", txtQTYD4.Text)

            'Drug 5
            cmd.Parameters.AddWithValue("@Drug5Name", cbDrug5.Text)
            cmd.Parameters.AddWithValue("@Drug5Strength", lblStrD5.Text)
            cmd.Parameters.AddWithValue("@Drug5Unit", lblUnitD5.Text)
            cmd.Parameters.AddWithValue("@Drug5Dose", txtDoseD5.Text)
            cmd.Parameters.AddWithValue("@Drug5Freq", txtFreqD5.Text)
            cmd.Parameters.AddWithValue("@Drug5Duration", txtDurationD5.Text)
            cmd.Parameters.AddWithValue("@Drug5TotalQTY", txtQTYD5.Text)

            'Drug 6
            cmd.Parameters.AddWithValue("@Drug6Name", cbDrug6.Text)
            cmd.Parameters.AddWithValue("@Drug6Strength", lblStrD6.Text)
            cmd.Parameters.AddWithValue("@Drug6Unit", lblUnitD6.Text)
            cmd.Parameters.AddWithValue("@Drug6Dose", txtDoseD6.Text)
            cmd.Parameters.AddWithValue("@Drug6Freq", txtFreqD6.Text)
            cmd.Parameters.AddWithValue("@Drug6Duration", txtDurationD6.Text)
            cmd.Parameters.AddWithValue("@Drug6TotalQTY", txtQTYD6.Text)

            'Drug 7
            cmd.Parameters.AddWithValue("@Drug7Name", cbDrug7.Text)
            cmd.Parameters.AddWithValue("@Drug7Strength", lblStrD7.Text)
            cmd.Parameters.AddWithValue("@Drug7Unit", lblUnitD7.Text)
            cmd.Parameters.AddWithValue("@Drug7Dose", txtDoseD7.Text)
            cmd.Parameters.AddWithValue("@Drug7Freq", txtFreqD7.Text)
            cmd.Parameters.AddWithValue("@Drug7Duration", txtDurationD7.Text)
            cmd.Parameters.AddWithValue("@Drug7TotalQTY", txtQTYD7.Text)

            'Drug 8
            cmd.Parameters.AddWithValue("@Drug8Name", cbDrug8.Text)
            cmd.Parameters.AddWithValue("@Drug8Strength", lblStrD8.Text)
            cmd.Parameters.AddWithValue("@Drug8Unit", lblUnitD8.Text)
            cmd.Parameters.AddWithValue("@Drug8Dose", txtDoseD8.Text)
            cmd.Parameters.AddWithValue("@Drug8Freq", txtFreqD8.Text)
            cmd.Parameters.AddWithValue("@Drug8Duration", txtDurationD8.Text)
            cmd.Parameters.AddWithValue("@Drug8TotalQTY", txtQTYD8.Text)

            'Drug 9
            cmd.Parameters.AddWithValue("@Drug9Name", cbDrug9.Text)
            cmd.Parameters.AddWithValue("@Drug9Strength", lblStrD9.Text)
            cmd.Parameters.AddWithValue("@Drug9Unit", lblUnitD9.Text)
            cmd.Parameters.AddWithValue("@Drug9Dose", txtDoseD9.Text)
            cmd.Parameters.AddWithValue("@Drug9Freq", txtFreqD9.Text)
            cmd.Parameters.AddWithValue("@Drug9Duration", txtDurationD9.Text)
            cmd.Parameters.AddWithValue("@Drug9TotalQTY", txtQTYD9.Text)

            'Drug 10
            cmd.Parameters.AddWithValue("@Drug10Name", cbDrug10.Text)
            cmd.Parameters.AddWithValue("@Drug10Strength", lblStrD10.Text)
            cmd.Parameters.AddWithValue("@Drug10Unit", lblUnitD10.Text)
            cmd.Parameters.AddWithValue("@Drug10Dose", txtDoseD10.Text)
            cmd.Parameters.AddWithValue("@Drug10Freq", txtFreqD10.Text)
            cmd.Parameters.AddWithValue("@Drug10Duration", txtDurationD10.Text)
            cmd.Parameters.AddWithValue("@Drug10TotalQTY", txtQTYD10.Text)

            'Insulin 1
            cmd.Parameters.AddWithValue("@Insulin1Name", cbInsulin1.Text)
            cmd.Parameters.AddWithValue("@Insulin1Strength", lblStrInsulin1.Text)
            cmd.Parameters.AddWithValue("@Insulin1Unit", lblUnitInsulin1.Text)
            cmd.Parameters.AddWithValue("@Insulin1MorDose", txtIn1MorDose.Text)
            cmd.Parameters.AddWithValue("@Insulin1NoonDose", txtIn1NoonDose.Text)
            cmd.Parameters.AddWithValue("@Insulin1AfternoonDose", txtIn1AfterNoonDose.Text)
            cmd.Parameters.AddWithValue("@Insulin1NightDose", txtIn1NightDose.Text)
            cmd.Parameters.AddWithValue("@Insulin1Freq", txtIn1Freq.Text)
            cmd.Parameters.AddWithValue("@Insulin1Duration", txtIn1Duration.Text)
            cmd.Parameters.AddWithValue("@Insulin1TotalDose", txtIn1TotalDose.Text)
            cmd.Parameters.AddWithValue("@Insulin1POM", txtIn1POM.Text)
            cmd.Parameters.AddWithValue("@Insulin1CartQTY", txtIn1CartQTY.Text)

            'Insulin 2
            cmd.Parameters.AddWithValue("@Insulin2Name", cbInsulin2.Text)
            cmd.Parameters.AddWithValue("@Insulin2Strength", lblStrInsulin2.Text)
            cmd.Parameters.AddWithValue("@Insulin2Unit", lblUnitInsulin2.Text)
            cmd.Parameters.AddWithValue("@Insulin2MorDose", txtIn2MorDose.Text)
            cmd.Parameters.AddWithValue("@Insulin2NoonDose", txtIn2NoonDose.Text)
            cmd.Parameters.AddWithValue("@Insulin2AfternoonDose", txtIn2AfterNoonDose.Text)
            cmd.Parameters.AddWithValue("@Insulin2NightDose", txtIn2NightDose.Text)
            cmd.Parameters.AddWithValue("@Insulin2Freq", txtIn2Freq.Text)
            cmd.Parameters.AddWithValue("@Insulin2Duration", txtIn2Duration.Text)
            cmd.Parameters.AddWithValue("@Insulin2TotalDose", txtIn2TotalDose.Text)
            cmd.Parameters.AddWithValue("@Insulin2POM", txtIn2POM.Text)
            cmd.Parameters.AddWithValue("@Insulin2CartQTY", txtIn2CartQTY.Text)

            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                conn.Close()
                If cboxEnablePrintPDF.Checked Then
                    print()
                    Thread.Sleep(250) 'Give time for printer to process into printer pool
                End If
                addRecordTab()
                'MsgBox("Saved data for " & stPatientName & ", IC No.: " & stIC)
                stlbMainStatus.Text = "Saved Successfully for " & stPatientName & ", IC No: " & stIC & " at " & Now()
                If stlbMainStatus.Text.Length > 50 Then
                    stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 9)
                ElseIf stlbMainStatus.Text.Length < 50 Then
                    stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 10)
                End If
                loadLogDGV()
                loadDBDataforPatientInfo() 'Refresh IC Textbox Autocomplete
                chboxNoICNumber.Checked = False
                If cboxAutoClear.Checked Then
                    clearall()
                End If
                Dim btnsavetemp As String
                btnsavetemp = btnSave.Text
                btnSave.Text = "Saved!"

                Await Task.Delay(3000)
                btnSave.Text = btnsavetemp
            Else
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            conn.Close()
            'MsgBox(ex.Message)
            If ex.Message.Contains("Duplicate") Then
                Select Case MsgBox("Existing Patient. Do you want to overwrite with current data? Choosing No or Cancel will not print label.", MsgBoxStyle.YesNoCancel, "Confirmation")
                    Case MsgBoxResult.Yes
                        Try

                            'Saves the old data to history database in prescribeddrugshistory table
                            Dim cmd As New MySqlCommand("INSERT INTO `prescribeddrugshistory` SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtICNo.Text & "'", conn)
                            conn.Open()
                            cmd.Parameters.Clear()
                            Dim i2 = cmd.ExecuteNonQuery
                            If i2 > 0 Then
                                conn.Close()

                                'MsgBox("Old Data saved to History for " & stPatientName & ", IC No.: " & stIC)
                                'stlbMainStatus.Text = "Old Data Saved for " & stPatientName & ", IC No: " & stIC

                            Else
                                MsgBox("Save to `prescribeddrugshistory` table failed.")
                                conn.Close()
                                Return
                            End If

                        Catch exx As Exception
                            conn.Close()
                            MsgBox(exx.Message)
                            If exx.Message.Contains("Duplicate") Then 'Duplicated ID detected
                                'goto here
Redo:
                                Dim getDuplicatedID As String = exx.Message
                                Dim getNewID As Integer
                                Dim keepAddcount As Boolean = True
                                While keepAddcount 'Attempt to keep increasing ID value until can UPDATE MySQL
                                    Try
                                        getDuplicatedID = getNumeric(getDuplicatedID) 'get the id value
                                        MsgBox("Duplicated ID: " & getDuplicatedID)
                                        getNewID = getDuplicatedID + 1 'increment id value
                                        MsgBox("New ID: " & getNewID)
                                        Dim cmd3 As New MySqlCommand("UPDATE `prescribeddrugshistory` SET `ID` = '" & getNewID & "' WHERE `prescribeddrugshistory`.`ID` = " & getDuplicatedID, conn)
                                        conn.Open()
                                        cmd3.Parameters.Clear()
                                        Dim i3 = cmd3.ExecuteNonQuery
                                        If i3 > 0 Then
                                            conn.Close()
                                            keepAddcount = False
                                            MsgBox("Resolved Duplicated ID")
                                            'stlbMainStatus.Text = "Old Data Saved for " & stPatientName & ", IC No: " & stIC

                                        Else
                                            MsgBox("Resolving ID Failed.")
                                            conn.Close()
                                            Return
                                        End If
                                    Catch ex1 As Exception
                                        MsgBox(ex1.Message)
                                        getDuplicatedID += 1 'increment new id again since id still occupied
                                        'keepAddcount = True
                                        conn.Close()
                                    End Try
                                End While
                                Try
                                    'Saves the old data to history database in prescribeddrugshistory table
                                    Dim cmd As New MySqlCommand("INSERT INTO `prescribeddrugshistory` SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtICNo.Text & "'", conn)
                                    conn.Open()
                                    cmd.Parameters.Clear()
                                    Dim i2 = cmd.ExecuteNonQuery
                                    If i2 > 0 Then
                                        conn.Close()

                                        'MsgBox("Old Data saved to History for " & stPatientName & ", IC No.: " & stIC)
                                        'stlbMainStatus.Text = "Old Data Saved for " & stPatientName & ", IC No: " & stIC
                                        'If stlbMainStatus.Text.Length > 50 Then
                                        'stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 9)
                                        'ElseIf stlbMainStatus.Text.Length < 50 Then
                                        'stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 10)
                                        'End If
                                    Else
                                        MsgBox("Save Failed.")
                                        conn.Close()
                                        Return
                                    End If
                                Catch ex2 As Exception
                                    conn.Close()
                                    GoTo Redo
                                End Try

                            End If

                            conn.Close()

                        End Try

                        Try
                            'Overwrite the current data in prescribeddrugs table
                            'UPDATE `prescribeddrugshistory` SET `ID` = '4' WHERE `prescribeddrugshistory`.`ID` = 5;
                            '"UPDATE `drugtable` SET `Strength`=@Strength,`Unit`=@Unit,`DosageForm`=@DosageForm,`PrescriberCategory`=@PrescriberCategory,`Remark`=@Remark WHERE `DrugName`=@DrugName", conn
                            Dim cmd2 As New MySqlCommand("UPDATE `prescribeddrugs` SET 
                            `Name`=@Name,`Date`=@Date,`DateCollection`=@DateCollection,`DateSeeDoctor`=@DateSeeDoctor,
                            `Drug1Name`=@Drug1Name,`Drug1Strength`=@Drug1Strength,`Drug1Unit`=@Drug1Unit,`Drug1Dose`=@Drug1Dose,`Drug1Freq`=@Drug1Freq,`Drug1Duration`=@Drug1Duration,`Drug1TotalQTY`=@Drug1TotalQTY,
                            `Drug2Name`=@Drug2Name,`Drug2Strength`=@Drug2Strength,`Drug2Unit`=@Drug2Unit,`Drug2Dose`=@Drug2Dose,`Drug2Freq`=@Drug2Freq,`Drug2Duration`=@Drug2Duration,`Drug2TotalQTY`=@Drug2TotalQTY,
                            `Drug3Name`=@Drug3Name,`Drug3Strength`=@Drug3Strength,`Drug3Unit`=@Drug3Unit,`Drug3Dose`=@Drug3Dose,`Drug3Freq`=@Drug3Freq,`Drug3Duration`=@Drug3Duration,`Drug3TotalQTY`=@Drug3TotalQTY,
                            `Drug4Name`=@Drug4Name,`Drug4Strength`=@Drug4Strength,`Drug4Unit`=@Drug4Unit,`Drug4Dose`=@Drug4Dose,`Drug4Freq`=@Drug4Freq,`Drug4Duration`=@Drug4Duration,`Drug4TotalQTY`=@Drug4TotalQTY,
                            `Drug5Name`=@Drug5Name,`Drug5Strength`=@Drug5Strength,`Drug5Unit`=@Drug5Unit,`Drug5Dose`=@Drug5Dose,`Drug5Freq`=@Drug5Freq,`Drug5Duration`=@Drug5Duration,`Drug5TotalQTY`=@Drug5TotalQTY,
                            `Drug6Name`=@Drug6Name,`Drug6Strength`=@Drug6Strength,`Drug6Unit`=@Drug6Unit,`Drug6Dose`=@Drug6Dose,`Drug6Freq`=@Drug6Freq,`Drug6Duration`=@Drug6Duration,`Drug6TotalQTY`=@Drug6TotalQTY,
                            `Drug7Name`=@Drug7Name,`Drug7Strength`=@Drug7Strength,`Drug7Unit`=@Drug7Unit,`Drug7Dose`=@Drug7Dose,`Drug7Freq`=@Drug7Freq,`Drug7Duration`=@Drug7Duration,`Drug7TotalQTY`=@Drug7TotalQTY,
                            `Drug8Name`=@Drug8Name,`Drug8Strength`=@Drug8Strength,`Drug8Unit`=@Drug8Unit,`Drug8Dose`=@Drug8Dose,`Drug8Freq`=@Drug8Freq,`Drug8Duration`=@Drug8Duration,`Drug8TotalQTY`=@Drug8TotalQTY,
                            `Drug9Name`=@Drug9Name,`Drug9Strength`=@Drug9Strength,`Drug9Unit`=@Drug9Unit,`Drug9Dose`=@Drug9Dose,`Drug9Freq`=@Drug9Freq,`Drug9Duration`=@Drug9Duration,`Drug9TotalQTY`=@Drug9TotalQTY,
                            `Drug10Name`=@Drug10Name,`Drug10Strength`=@Drug10Strength,`Drug10Unit`=@Drug10Unit,`Drug10Dose`=@Drug10Dose,`Drug10Freq`=@Drug10Freq,`Drug10Duration`=@Drug10Duration,`Drug10TotalQTY`=@Drug10TotalQTY,
                            `Insulin1Name`=@Insulin1Name,`Insulin1Strength`=@Insulin1Strength,`Insulin1Unit`=@Insulin1Unit,`Insulin1MorDose`=@Insulin1MorDose,`Insulin1NoonDose`=@Insulin1NoonDose,`Insulin1AfternoonDose`=@Insulin1AfternoonDose,`Insulin1NightDose`=@Insulin1NightDose,`Insulin1Freq`=@Insulin1Freq,`Insulin1Duration`=@Insulin1Duration,`Insulin1TotalDose`=@Insulin1TotalDose,`Insulin1POM`=@Insulin1POM,`Insulin1CartQTY`=@Insulin1CartQTY,
                            `Insulin2Name`=@Insulin2Name,`Insulin2Strength`=@Insulin2Strength,`Insulin2Unit`=@Insulin2Unit,`Insulin2MorDose`=@Insulin2MorDose,`Insulin2NoonDose`=@Insulin2NoonDose,`Insulin2AfternoonDose`=@Insulin2AfternoonDose,`Insulin2NightDose`=@Insulin2NightDose,`Insulin2Freq`=@Insulin2Freq,`Insulin2Duration`=@Insulin2Duration,`Insulin2TotalDose`=@Insulin2TotalDose,`Insulin2POM`=@Insulin2POM,`Insulin2CartQTY`=@Insulin2CartQTY,`Timestamp`=CURRENT_TIMESTAMP WHERE `ICNo`=@ICNo", conn)
                            cmd2.Parameters.Clear()
                            cmd2.Parameters.AddWithValue("@Name", txtPatientName.Text)
                            cmd2.Parameters.AddWithValue("@ICNo", txtICNo.Text)
                            cmd2.Parameters.AddWithValue("@Date", dtpDateSaved.Text)
                            cmd2.Parameters.AddWithValue("@DateCollection", dtpDateCollection.Text)
                            cmd2.Parameters.AddWithValue("@DateSeeDoctor", dtpDateSeeDoctor.Text)
                            'Drug 1
                            cmd2.Parameters.AddWithValue("@Drug1Name", cbDrug1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1Strength", lblStrD1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1Unit", lblUnitD1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1Dose", txtDoseD1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1Freq", txtFreqD1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1Duration", txtDurationD1.Text)
                            cmd2.Parameters.AddWithValue("@Drug1TotalQTY", txtQTYD1.Text)
                            'Drug 2
                            cmd2.Parameters.AddWithValue("@Drug2Name", cbDrug2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2Strength", lblStrD2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2Unit", lblUnitD2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2Dose", txtDoseD2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2Freq", txtFreqD2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2Duration", txtDurationD2.Text)
                            cmd2.Parameters.AddWithValue("@Drug2TotalQTY", txtQTYD2.Text)
                            'Drug 3
                            cmd2.Parameters.AddWithValue("@Drug3Name", cbDrug3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3Strength", lblStrD3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3Unit", lblUnitD3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3Dose", txtDoseD3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3Freq", txtFreqD3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3Duration", txtDurationD3.Text)
                            cmd2.Parameters.AddWithValue("@Drug3TotalQTY", txtQTYD3.Text)
                            'Drug 4
                            cmd2.Parameters.AddWithValue("@Drug4Name", cbDrug4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4Strength", lblStrD4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4Unit", lblUnitD4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4Dose", txtDoseD4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4Freq", txtFreqD4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4Duration", txtDurationD4.Text)
                            cmd2.Parameters.AddWithValue("@Drug4TotalQTY", txtQTYD4.Text)
                            'Drug 5
                            cmd2.Parameters.AddWithValue("@Drug5Name", cbDrug5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5Strength", lblStrD5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5Unit", lblUnitD5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5Dose", txtDoseD5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5Freq", txtFreqD5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5Duration", txtDurationD5.Text)
                            cmd2.Parameters.AddWithValue("@Drug5TotalQTY", txtQTYD5.Text)
                            'Drug 6
                            cmd2.Parameters.AddWithValue("@Drug6Name", cbDrug6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6Strength", lblStrD6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6Unit", lblUnitD6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6Dose", txtDoseD6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6Freq", txtFreqD6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6Duration", txtDurationD6.Text)
                            cmd2.Parameters.AddWithValue("@Drug6TotalQTY", txtQTYD6.Text)
                            'Drug 7
                            cmd2.Parameters.AddWithValue("@Drug7Name", cbDrug7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7Strength", lblStrD7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7Unit", lblUnitD7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7Dose", txtDoseD7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7Freq", txtFreqD7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7Duration", txtDurationD7.Text)
                            cmd2.Parameters.AddWithValue("@Drug7TotalQTY", txtQTYD7.Text)
                            'Drug 8
                            cmd2.Parameters.AddWithValue("@Drug8Name", cbDrug8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8Strength", lblStrD8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8Unit", lblUnitD8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8Dose", txtDoseD8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8Freq", txtFreqD8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8Duration", txtDurationD8.Text)
                            cmd2.Parameters.AddWithValue("@Drug8TotalQTY", txtQTYD8.Text)
                            'Drug 9
                            cmd2.Parameters.AddWithValue("@Drug9Name", cbDrug9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9Strength", lblStrD9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9Unit", lblUnitD9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9Dose", txtDoseD9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9Freq", txtFreqD9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9Duration", txtDurationD9.Text)
                            cmd2.Parameters.AddWithValue("@Drug9TotalQTY", txtQTYD9.Text)
                            'Drug 10
                            cmd2.Parameters.AddWithValue("@Drug10Name", cbDrug10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10Strength", lblStrD10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10Unit", lblUnitD10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10Dose", txtDoseD10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10Freq", txtFreqD10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10Duration", txtDurationD10.Text)
                            cmd2.Parameters.AddWithValue("@Drug10TotalQTY", txtQTYD10.Text)
                            'Insulin 1
                            cmd2.Parameters.AddWithValue("@Insulin1Name", cbInsulin1.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1Strength", lblStrInsulin1.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1Unit", lblUnitInsulin1.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1MorDose", txtIn1MorDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1NoonDose", txtIn1NoonDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1AfternoonDose", txtIn1AfterNoonDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1NightDose", txtIn1NightDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1Freq", txtIn1Freq.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1Duration", txtIn1Duration.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1TotalDose", txtIn1TotalDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1POM", txtIn1POM.Text)
                            cmd2.Parameters.AddWithValue("@Insulin1CartQTY", txtIn1CartQTY.Text)
                            'Insulin 2
                            cmd2.Parameters.AddWithValue("@Insulin2Name", cbInsulin2.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2Strength", lblStrInsulin2.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2Unit", lblUnitInsulin2.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2MorDose", txtIn2MorDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2NoonDose", txtIn2NoonDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2AfternoonDose", txtIn2AfterNoonDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2NightDose", txtIn2NightDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2Freq", txtIn2Freq.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2Duration", txtIn2Duration.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2TotalDose", txtIn2TotalDose.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2POM", txtIn2POM.Text)
                            cmd2.Parameters.AddWithValue("@Insulin2CartQTY", txtIn2CartQTY.Text)

                            conn.Open()
                            Dim i = cmd2.ExecuteNonQuery
                            If i > 0 Then

                                conn.Close()
                                If cboxEnablePrintPDF.Checked Then
                                    print()
                                    Thread.Sleep(250)
                                End If

                                addRecordTab()
                                'MsgBox("Successfully Updated Data.")
                                'MsgBox("Overwritten data for " & stPatientName & ", IC No.: " & stIC)
                                stlbMainStatus.Text = "Overwrite Successfully for " & stPatientName & ", IC No: " & stIC & " at " & Now()
                                If stlbMainStatus.Text.Length > 50 Then
                                    stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 9)
                                ElseIf stlbMainStatus.Text.Length < 50 Then
                                    stlbMainStatus.Font = New Font(stlbMainStatus.Font.FontFamily, 10)
                                End If
                                loadLogDGV()
                                'loadDBDataforPatientInfo() 'Refresh IC Textbox Autocomplete
                                chboxNoICNumber.Checked = False
                                If cboxAutoClear.Checked Then
                                    clearall()
                                End If
                                overwriten = True

                            End If
                        Catch exxx As Exception
                            MsgBox(exxx.Message)
                        End Try


                    Case MsgBoxResult.No
                        conn.Close()
                        Return
                    Case MsgBoxResult.Cancel
                        conn.Close()
                        Return
                End Select


            End If
            conn.Close()
        Finally

        End Try
        If overwriten Then
            Dim btnsavetemp As String
            btnsavetemp = btnSave.Text
            btnSave.Text = "Overwritten!"
            Await Task.Delay(3000)
            btnSave.Text = btnsavetemp
            overwriten = False
        End If


    End Sub
    Public Sub addRecordTab()
        Dim newpatientBool As Integer = 0
        Dim IOUBool As Integer = 0
        'MsgBox("Drug Items : " & NoOfItemsRecord)
        'MsgBox("Insulin Items : " & NoOfItemsRecordInsulin)
        NoOfItemsRecord = 0
        NoOfItemsRecordInsulin = 0
        If cbDrug1.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug2.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug3.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug4.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug5.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug6.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug7.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug8.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug9.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbDrug10.SelectedIndex > 0 Then
            NoOfItemsRecord += 1
        End If
        If cbInsulin1.SelectedIndex > 0 Then
            NoOfItemsRecordInsulin += 1
        End If
        If cbInsulin2.SelectedIndex > 0 Then
            NoOfItemsRecordInsulin += 1
        End If
        Dim totalitems As Integer = NoOfItemsRecord + NoOfItemsRecordInsulin
        If cboxIOU.Checked Then
            IOUBool = 1
            cboxIOU.Checked = False
        Else
            newpatientBool = 1
        End If
        'Add button at Drugs Tab to Save Data entered in the Text Boxes
        Try
            conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `records` (`Name`,`ICNo`,`NewPatient`,`IOU`,`NoOfItems`,`DateCollection`,`DateSeeDoctor`) VALUES (@Name,@ICNo,@NewPatient,@IOU,@NoOfItems,@DateCollection,@DateSeeDoctor)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Name", txtPatientName.Text)
            cmd.Parameters.AddWithValue("@ICNo", txtICNo.Text)
            cmd.Parameters.AddWithValue("@NewPatient", newpatientBool)
            cmd.Parameters.AddWithValue("@IOU", IOUBool)
            cmd.Parameters.AddWithValue("@NoOfItems", totalitems)
            cmd.Parameters.AddWithValue("@DateCollection", dtpDateCollection.Text)
            cmd.Parameters.AddWithValue("@DateSeeDoctor", dtpDateSeeDoctor.Text)

            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                'MsgBox("Records Successfully Saved.")
                conn.Close()
                loadDGVRecords()
            Else
                'MsgBox("Records Save Failed.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub loadDGVRecords()
        dgvRecords.Rows.Clear()
        dgvRecords.Visible = False ' Hide the DataGridView

        ' Data Grid View Method to Get Data from MYSQL Database
        Dim count As Integer = 0
        ' Parse the input date string
        dtpRecordsDateSelectorEnd.MinDate = dtpRecordsDateSelector.Value
        Dim originalDateString As String = dtpRecordsDateSelector.Value
        Dim parsedDate As DateTime = DateTime.Parse(originalDateString)
        Dim startdate As String = parsedDate.ToString("yyyy-MM-dd")

        Dim originalDateString2 As String = dtpRecordsDateSelectorEnd.Value
        Dim parsedDate2 As DateTime = DateTime.Parse(originalDateString2)
        Dim enddate As String = parsedDate2.ToString("yyyy-MM-dd")
        ' Declare initial count variables for new patients, iou, no of items
        Dim newpatientcount = 0
        Dim ioucount = 0
        Dim noofitems = 0

        Try
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM `records` WHERE (CAST(Timestamp AS date) BETWEEN '" & startdate & "' AND '" & enddate & "')", conn)
            dr = cmd.ExecuteReader

            While dr.Read
                count += 1
                dgvRecords.Rows.Add(count, dr.Item("ID"), dr.Item("Name"), dr.Item("ICNo"), dr.Item("NewPatient"), dr.Item("IOU"), dr.Item("NoOfItems"), dr.Item("DateCollection"), dr.Item("DateSeeDoctor"), dr.Item("Timestamp"))
                newpatientcount = newpatientcount + CInt(dr.Item("NewPatient"))
                ioucount = ioucount + CInt(dr.Item("IOU"))
                noofitems = noofitems + CInt(dr.Item("NoOfItems"))
            End While
            dr.Dispose()
            lblNewPatientTotal.Text = newpatientcount
            lblIOUTotal.Text = ioucount
            lblNoOfItemsTotal.Text = noofitems

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
            dgvRecords.Visible = True ' Show the DataGridView after processing
        End Try
    End Sub

    Public Function getNumeric(value As String) As String
        Dim output As StringBuilder = New StringBuilder
        For i = 0 To value.Length - 1
            If IsNumeric(value(i)) Then
                output.Append(value(i))
            End If
        Next
        Return output.ToString()
    End Function

    Public Sub AddDrug() 'Add Drug
        'Add button at Drugs Tab to Save Data entered in the Text Boxes
        Try
            If txtDrugName.Text = "" Then
                MsgBox("Error. Drug Name cannot be empty")
                Return
            End If

            If cboxDosageForm.SelectedIndex = -1 Then
                ' Display an error message if no item is selected
                MessageBox.Show("Please select the Dosage Form from the list.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `drugtable` (`DrugName`,`Strength`,`Unit`,`DosageForm`,`PrescriberCategory`,`DefaultMaxQTY`,`Remark`) VALUES (@DrugName,@Strength,@Unit,@DosageForm,@PrescriberCategory,@DefaultMaxQTY,@Remark)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DrugName", txtDrugName.Text)
            cmd.Parameters.AddWithValue("@Strength", CDbl(txtStrength.Text))
            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@DosageForm", cboxDosageForm.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@PrescriberCategory", txtPrescriberCategory.Text)
            cmd.Parameters.AddWithValue("@DefaultMaxQTY", txtDefaultMaxQTY.Text)
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text)

            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Successfully Saved.")
                conn.Close()

                drugclear()
                DGV_Load()
                loaddatafromdb()
                loadInsulindatafromdb()

                'checkforselecteddrugs()
                'clearall()
            Else
                conn.Close()
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            conn.Close()
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub drugclear()
        txtDrugName.Clear()
        txtStrength.Clear()
        txtUnit.Clear()
        cboxDosageForm.SelectedIndex = -1
        txtPrescriberCategory.Clear()
        txtDefaultMaxQTY.Clear()
        txtRemark.Clear()
        txtSearchDrug.Clear()
        btnAddDrug.Enabled = True
        txtDrugName.ReadOnly = False
    End Sub

    Public Sub DGV_Load()
        DataGridView1.Rows.Clear()
        'Data Grid View Method to Get Data from MYSQL Database
        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable", conn)
            dr = cmd.ExecuteReader

            While dr.Read
                DataGridView1.Rows.Add(dr.Item("DrugName"), dr.Item("Strength"), dr.Item("Unit"), dr.Item("DosageForm"), dr.Item("PrescriberCategory"), dr.Item("DefaultMaxQTY"), dr.Item("Remark"))

            End While
            dr.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Public Sub Edit()
        Dim sender As Object = Nothing
        Dim e As EventArgs = Nothing
        'Add button at Database Tab to Save Data entered into Text Boxes
        Try
            If txtDrugName.Text = "" Then
                MsgBox("Error. Drug Name cannot be empty")
                Return
            End If

            conn.Open()

            Dim cmd As New MySqlCommand("UPDATE `drugtable` SET `Strength`=@Strength,`Unit`=@Unit,`DosageForm`=@DosageForm,`PrescriberCategory`=@PrescriberCategory,`DefaultMaxQTY`=@DefaultMaxQTY,`Remark`=@Remark WHERE `DrugName`=@DrugName", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DrugName", txtDrugName.Text)
            cmd.Parameters.AddWithValue("@Strength", CDec(txtStrength.Text))
            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@DosageForm", cboxDosageForm.SelectedItem.ToString())
            cmd.Parameters.AddWithValue("@PrescriberCategory", txtPrescriberCategory.Text)
            cmd.Parameters.AddWithValue("@DefaultMaxQTY", txtDefaultMaxQTY.Text)
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text)

            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Successfully Updated.")
                conn.Close()

                drugclear()
                DGV_Load()
                txtSearchDrug_TextChanged(sender, e)
                txtDrugName.ReadOnly = False
                btnAddDrug.Enabled = True

            Else
                conn.Close()
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            conn.Close()

            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub btnAddDrug_Click(sender As Object, e As EventArgs) Handles btnAddDrug.Click
        AddDrug()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Edit()
    End Sub

    Private Sub DataGridView1_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        Modify()
    End Sub
    Public Sub Modify()
        Select Case MsgBox("Do you want to Modify the Selected Item?", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes
                'Take values from the DGV table
                txtDrugName.Text = DataGridView1.CurrentRow.Cells(0).Value
                txtStrength.Text = DataGridView1.CurrentRow.Cells(1).Value
                txtUnit.Text = DataGridView1.CurrentRow.Cells(2).Value
                cboxDosageForm.SelectedItem = DataGridView1.CurrentRow.Cells(3).Value.ToString()
                txtPrescriberCategory.Text = DataGridView1.CurrentRow.Cells(4).Value
                txtDefaultMaxQTY.Text = DataGridView1.CurrentRow.Cells(5).Value
                txtRemark.Text = DataGridView1.CurrentRow.Cells(6).Value

                txtDrugName.ReadOnly = True
                btnAddDrug.Enabled = False


            Case MsgBoxResult.Cancel
                Return
            Case MsgBoxResult.No
                Return
        End Select
    End Sub
    Public Sub delete()
        Select Case MsgBox("Do you want to Delete the Selected Row?", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes
                'Delete button at Database Tab to Delete Database Row Selected at DataGridView Table
                Try


                    conn.Open()

                    Dim cmd As New MySqlCommand("DELETE FROM `drugtable` WHERE `DrugName`=@DrugName", conn)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@DrugName", DataGridView1.CurrentRow.Cells(0).Value)


                    Dim i = cmd.ExecuteNonQuery
                    If i > 0 Then
                        MsgBox("Successfully Deleted.")
                        conn.Close()

                        drugclear()
                        DGV_Load()


                    Else
                        conn.Close()
                        MsgBox("Delete Failed.")
                    End If
                Catch ex As Exception
                    conn.Close()
                    MsgBox(ex.Message)
                End Try
            Case MsgBoxResult.Cancel
                Return
            Case MsgBoxResult.No
                Return
        End Select
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        delete()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        drugclear()
    End Sub

    Private Sub txtSearchDrug_TextChanged(sender As Object, e As EventArgs) Handles txtSearchDrug.TextChanged
        DataGridView1.Rows.Clear()
        'Search Function uses Data Grid View Method to Get Data from MYSQL Database
        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName Like '%" & txtSearchDrug.Text & "%'", conn)
            dr = cmd.ExecuteReader

            While dr.Read
                DataGridView1.Rows.Add(dr.Item("DrugName"), dr.Item("Strength"), dr.Item("Unit"), dr.Item("DosageForm"), dr.Item("PrescriberCategory"), dr.Item("DefaultMaxQTY"), dr.Item("Remark"))

            End While
            dr.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Public Sub loadDBDataforPatientInfo()
        Dim cmd As New MySqlCommand("SELECT ICNo , Name FROM prescribeddrugs", conn)
        Dim dt As New DataTable
        Dim da As New MySqlDataAdapter(cmd)
        Dim col As New AutoCompleteStringCollection
        Dim col2 As New AutoCompleteStringCollection

        Try
            da.Fill(dt)
            For i = 0 To dt.Rows.Count - 1
                col.Add(dt.Rows(i)("ICNo").ToString())
                col2.Add(dt.Rows(i)("Name").ToString())
                'DBPatientName = dt.Rows(i)("Name").ToString()
            Next
            conn.Close()
            da.Dispose()



            txtICNo.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtICNo.AutoCompleteCustomSource = col
            txtICNo.AutoCompleteMode = AutoCompleteMode.Suggest

            txtICNoDB.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtICNoDB.AutoCompleteCustomSource = col
            txtICNoDB.AutoCompleteMode = AutoCompleteMode.Suggest

            txtPatientName.AutoCompleteSource = AutoCompleteSource.CustomSource
            txtPatientName.AutoCompleteCustomSource = col2
            txtPatientName.AutoCompleteMode = AutoCompleteMode.Suggest
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub checkICfromDB()
        Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & txtICNo.Text & "'", conn)

        Try
            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                If dr.Item("ICNo") = txtICNo.Text Then
                    btnIOU.Enabled = True
                    lblExistingPatient.Text = "Existing Patient Found!"
                    txtPatientName.Text = dr.Item("Name")
                End If
            End While
            conn.Close()
        Catch ex As Exception
            conn.Close()
        End Try



    End Sub

    Public Sub checkNamefromDB()
        Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE Name = @name", conn)
        cmd.Parameters.AddWithValue("@name", txtPatientName.Text.ToString()) ' Use parameterized query

        Try
            conn.Open()
            Using dr As MySqlDataReader = cmd.ExecuteReader()
                If dr.Read() Then ' Move to the first row
                    txtICNo.Text = dr("ICNo").ToString()
                    btnIOU.Enabled = True
                    lblExistingPatient.Text = "Existing Patient Found!"

                Else
                    lblExistingPatient.Text = "No matching record found."
                End If
            End Using ' This ensures the reader is closed properly
            dr.Close()
            conn.Close()

        Catch ex As Exception
            conn.Close()
            ' MsgBox("Error: " & ex.Message) ' Show error message for debugging
        End Try

        ' Ensure IC number check happens after database operations
        If txtICNo.TextLength = 14 Then
            Try
                btnCheckICMySPR.Enabled = True
                Dim DOB As String = ExtractDOB()
                Dim DOBDate As Date = DateTime.Parse(DOB)
                Dim currentDate As Date = Date.Today
                Dim age As Integer = currentDate.Year - DOBDate.Year

                ' Adjust if birthday hasn't occurred yet this year
                If currentDate < DOBDate.AddYears(age) Then
                    age -= 1
                End If

                lblAge.Text = "Age: " & age
                lblGender.Text = "Gender: " & ExtractGender()

            Catch ex As Exception
                MsgBox("Error processing DOB/Gender: " & ex.Message)
            End Try
        End If
    End Sub


    Private Sub txtPatientName_TextChanged(sender As Object, e As EventArgs) Handles txtPatientName.TextChanged
        btnIOU.Enabled = False
        lblExistingPatient.Text = ""
        lblAge.Text = "Age"
        lblGender.Text = "Gender"
        checkNamefromDB()
    End Sub
    Public Sub loadInsulindatafromdb()
        Dim tempcbInsulin1 = cbInsulin1.SelectedIndex
        Dim tempcbInsulin2 = cbInsulin2.SelectedIndex
        Try
            cbInsulin1.Items.Clear()
            cbInsulin2.Items.Clear()

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT DrugName FROM drugtable WHERE DrugName like '%Insulin%'", conn)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            Dim col As New AutoCompleteStringCollection
            Dim drugnamedb As String
            da.Fill(dt)
            For i = 0 To dt.Rows.Count - 1
                col.Add(dt.Rows(i)("DrugName").ToString())
                drugnamedb = dt.Rows(i)("DrugName").ToString()
                cbInsulin1.Items.Add(drugnamedb)
                cbInsulin2.Items.Add(drugnamedb)

            Next
            conn.Close()
            da.Dispose()



            cbInsulin1.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbInsulin1.AutoCompleteCustomSource = col
            cbInsulin1.AutoCompleteMode = AutoCompleteMode.Suggest

            cbInsulin2.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbInsulin2.AutoCompleteCustomSource = col
            cbInsulin2.AutoCompleteMode = AutoCompleteMode.Suggest

            'sets the previous selected insulin
            cbInsulin1.SelectedIndex = tempcbInsulin1
            cbInsulin2.SelectedIndex = tempcbInsulin2

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Public Sub loaddatafromdb()
        'run the search drug name combo box also

        Dim tempcbDrug1 = cbDrug1.SelectedIndex
        Dim tempcbDrug2 = cbDrug2.SelectedIndex
        Dim tempcbDrug3 = cbDrug3.SelectedIndex
        Dim tempcbDrug4 = cbDrug4.SelectedIndex
        Dim tempcbDrug5 = cbDrug5.SelectedIndex
        Dim tempcbDrug6 = cbDrug6.SelectedIndex
        Dim tempcbDrug7 = cbDrug7.SelectedIndex
        Dim tempcbDrug8 = cbDrug8.SelectedIndex
        Dim tempcbDrug9 = cbDrug9.SelectedIndex
        Dim tempcbDrug10 = cbDrug10.SelectedIndex

        Dim tempcbDrugQty = cbDrugQty.SelectedIndex

        Try
            cbDrug1.Items.Clear()
            cbDrug2.Items.Clear()
            cbDrug3.Items.Clear()
            cbDrug4.Items.Clear()
            cbDrug5.Items.Clear()
            cbDrug6.Items.Clear()
            cbDrug7.Items.Clear()
            cbDrug8.Items.Clear()
            cbDrug9.Items.Clear()
            cbDrug10.Items.Clear()
            cbDrugQty.Items.Clear()

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT DrugName FROM drugtable WHERE DrugName not like '%Insulin%'", conn)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            Dim col As New AutoCompleteStringCollection
            Dim drugnamedb As String
            da.Fill(dt)
            For i = 0 To dt.Rows.Count - 1
                col.Add(dt.Rows(i)("DrugName").ToString())
                drugnamedb = dt.Rows(i)("DrugName").ToString()
                cbDrug1.Items.Add(drugnamedb)
                cbDrug2.Items.Add(drugnamedb)
                cbDrug3.Items.Add(drugnamedb)
                cbDrug4.Items.Add(drugnamedb)
                cbDrug5.Items.Add(drugnamedb)
                cbDrug6.Items.Add(drugnamedb)
                cbDrug7.Items.Add(drugnamedb)
                cbDrug8.Items.Add(drugnamedb)
                cbDrug9.Items.Add(drugnamedb)
                cbDrug10.Items.Add(drugnamedb)
                cbDrugQty.Items.Add(drugnamedb)
            Next
            conn.Close()
            da.Dispose()



            cbDrug1.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug1.AutoCompleteCustomSource = col
            cbDrug1.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug2.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug2.AutoCompleteCustomSource = col
            cbDrug2.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug3.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug3.AutoCompleteCustomSource = col
            cbDrug3.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug4.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug4.AutoCompleteCustomSource = col
            cbDrug4.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug5.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug5.AutoCompleteCustomSource = col
            cbDrug5.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug6.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug6.AutoCompleteCustomSource = col
            cbDrug6.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug7.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug7.AutoCompleteCustomSource = col
            cbDrug7.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug8.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug8.AutoCompleteCustomSource = col
            cbDrug8.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug9.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug9.AutoCompleteCustomSource = col
            cbDrug9.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrug10.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrug10.AutoCompleteCustomSource = col
            cbDrug10.AutoCompleteMode = AutoCompleteMode.Suggest

            cbDrugQty.AutoCompleteSource = AutoCompleteSource.CustomSource
            cbDrugQty.AutoCompleteCustomSource = col
            cbDrugQty.AutoCompleteMode = AutoCompleteMode.Suggest

            'sets the drug from previously selected
            cbDrug1.SelectedIndex = tempcbDrug1
            cbDrug2.SelectedIndex = tempcbDrug2
            cbDrug3.SelectedIndex = tempcbDrug3
            cbDrug4.SelectedIndex = tempcbDrug4
            cbDrug5.SelectedIndex = tempcbDrug5
            cbDrug6.SelectedIndex = tempcbDrug6
            cbDrug7.SelectedIndex = tempcbDrug7
            cbDrug8.SelectedIndex = tempcbDrug8
            cbDrug9.SelectedIndex = tempcbDrug9
            cbDrug10.SelectedIndex = tempcbDrug10
            cbDrugQty.SelectedIndex = tempcbDrugQty

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub populateDosageForms()

        ' Set the combo box to only allow selection of predefined items
        cboxDosageForm.DropDownStyle = ComboBoxStyle.DropDownList

        ' Clear the combo box before adding new items (optional)
        cboxDosageForm.Items.Clear()

        ' Add items to the combo box
        cboxDosageForm.Items.Add("Tablet")
        cboxDosageForm.Items.Add("Fridge Item")
        cboxDosageForm.Items.Add("Syrup")
        cboxDosageForm.Items.Add("Gargle")
        cboxDosageForm.Items.Add("Inhaler")
        cboxDosageForm.Items.Add("Internal")
        cboxDosageForm.Items.Add("Cream")
        cboxDosageForm.Items.Add("Dropper")
        cboxDosageForm.Items.Add("Suppository")
        cboxDosageForm.Items.Add("")
    End Sub

    Public Sub populatevaluesInsulin1()
        Try
            If cbInsulin1.Text Is "" Then
                lblStrInsulin1.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbInsulin1.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrInsulin1.Text = dr.Item("Strength")
                lblUnitInsulin1.Text = dr.Item("Unit")

                RemarkIn1 = dr.Item("Remark")

                If dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodIn1 = "Suntik "
                    ConsumeUnitIn1 = " unit "

                End If
            End While
            dr.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrInsulin1.Text = ""
            lblUnitInsulin1.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub
    Public Sub populatevaluesInsulin2()
        Try
            If cbInsulin2.Text Is "" Then
                lblStrInsulin2.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbInsulin2.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrInsulin2.Text = dr.Item("Strength")
                lblUnitInsulin2.Text = dr.Item("Unit")

                RemarkIn2 = dr.Item("Remark")

                If dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodIn2 = "Suntik "
                    ConsumeUnitIn2 = " unit "

                End If
            End While
            dr.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrInsulin2.Text = ""
            lblUnitInsulin2.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populateValues(ByVal drugName As String, ByRef lblStrength As Label, ByRef lblUnit As Label,
                          ByRef lblPrescriberCategory As Label, ByRef consumeMethod As String,
                          ByRef consumeUnit As String, ByRef remark As String,
                          ByRef defaultMaxQty As Integer)

        Try
            ' If the drug name is empty, clear labels and exit
            If drugName = "" Then
                lblStrength.Text = ""
                lblUnit.Text = ""
                lblPrescriberCategory.Text = ""
                Return
            End If

            ' Open the database connection
            conn.Open()
            ' Prepare the SQL command to fetch data based on the provided drug name
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = @DrugName", conn)
            cmd.Parameters.AddWithValue("@DrugName", drugName)
            dr = cmd.ExecuteReader()

            ' Read the data from the database
            While dr.Read()
                ' Populate labels and variables based on the database values
                lblStrength.Text = dr.Item("Strength").ToString()
                lblUnit.Text = dr.Item("Unit").ToString()
                lblPrescriberCategory.Text = dr.Item("PrescriberCategory").ToString()
                remark = dr.Item("Remark").ToString()

                ' Determine consumption method and unit based on DosageForm
                Select Case dr.Item("DosageForm").ToString()
                    Case "Tablet"
                        consumeMethod = "Makan "
                        consumeUnit = " biji "
                    Case "Fridge Item"
                        consumeMethod = "Suntik "
                        consumeUnit = " unit "
                    Case "Syrup"
                        consumeMethod = "Minum "
                        consumeUnit = " ml "
                    Case "Gargle"
                        consumeMethod = "Kumur "
                        consumeUnit = " ml "
                    Case "Inhaler"
                        consumeMethod = "Ambil "
                        consumeUnit = " sedutan "
                    Case "Internal", "Packet"
                        consumeMethod = "Minum "
                        consumeUnit = " paket "
                    Case "Cream"
                        consumeMethod = "Sapu "
                        consumeUnit = ""
                    Case "Dropper"
                        consumeMethod = ""
                        consumeUnit = " titis "
                    Case "Suppository"
                        consumeMethod = "Ambil "
                        consumeUnit = " biji "
                    Case Else
                        consumeMethod = ""
                        consumeUnit = ""
                End Select

                ' Check for default max QTY and set the value
                If dr.Item("DefaultMaxQTY").ToString() <> "" Then
                    defaultMaxQty = CInt(dr.Item("DefaultMaxQTY"))
                Else
                    defaultMaxQty = 0
                End If
            End While

            dr.Close()

        Catch ex As Exception
            ' Handle errors and clear labels if necessary
            MsgBox(ex.Message)
            lblStrength.Text = ""
            lblUnit.Text = ""
            lblPrescriberCategory.Text = ""

        Finally
            ' Clean up resources
            dr.Dispose()
            conn.Close()
        End Try
    End Sub



    Public Sub calculatedrugD1()
        Dim TotalQTYD1 As Double

        Try
            TotalQTYD1 = (CDbl(txtDoseD1.Text) * CDbl(txtFreqD1.Text) * CDbl(txtDurationD1.Text)) / CDbl(lblStrD1.Text)
            txtQTYD1.Text = Math.Round(TotalQTYD1, 2)

            If DefaultMaxQTYD1 > 0 Then
                txtQTYD1.Text = DefaultMaxQTYD1
            End If

        Catch ex As Exception
            txtQTYD1.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD2()
        Dim TotalQTYD2 As Double

        Try
            TotalQTYD2 = (CDbl(txtDoseD2.Text) * CDbl(txtFreqD2.Text) * CDbl(txtDurationD2.Text)) / CDbl(lblStrD2.Text)
            txtQTYD2.Text = Math.Round(TotalQTYD2, 2)

            If DefaultMaxQTYD2 > 0 Then
                txtQTYD2.Text = DefaultMaxQTYD2
            End If

        Catch ex As Exception
            txtQTYD2.Text = ""
        End Try
    End Sub
    Public Sub calculatedrugD3()
        Dim TotalQTYD3 As Double

        Try
            TotalQTYD3 = (CDbl(txtDoseD3.Text) * CDbl(txtFreqD3.Text) * CDbl(txtDurationD3.Text)) / CDbl(lblStrD3.Text)
            txtQTYD3.Text = Math.Round(TotalQTYD3, 2)

            If DefaultMaxQTYD3 > 0 Then
                txtQTYD3.Text = DefaultMaxQTYD3
            End If
        Catch ex As Exception
            txtQTYD3.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD4()
        Dim TotalQTYD4 As Double

        Try
            TotalQTYD4 = (CDbl(txtDoseD4.Text) * CDbl(txtFreqD4.Text) * CDbl(txtDurationD4.Text)) / CDbl(lblStrD4.Text)
            txtQTYD4.Text = Math.Round(TotalQTYD4, 2)

            If DefaultMaxQTYD4 > 0 Then
                txtQTYD4.Text = DefaultMaxQTYD4
            End If
        Catch ex As Exception
            txtQTYD4.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD5()
        Dim TotalQTYD5 As Double

        Try
            TotalQTYD5 = (CDbl(txtDoseD5.Text) * CDbl(txtFreqD5.Text) * CDbl(txtDurationD5.Text)) / CDbl(lblStrD5.Text)
            txtQTYD5.Text = Math.Round(TotalQTYD5, 2)

            If DefaultMaxQTYD5 > 0 Then
                txtQTYD5.Text = DefaultMaxQTYD5
            End If
        Catch ex As Exception
            txtQTYD5.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD6()
        Dim TotalQTYD6 As Double

        Try
            TotalQTYD6 = (CDbl(txtDoseD6.Text) * CDbl(txtFreqD6.Text) * CDbl(txtDurationD6.Text)) / CDbl(lblStrD6.Text)
            txtQTYD6.Text = Math.Round(TotalQTYD6, 2)

            If DefaultMaxQTYD6 > 0 Then
                txtQTYD6.Text = DefaultMaxQTYD6
            End If

        Catch ex As Exception
            txtQTYD6.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD7()
        Dim TotalQTYD7 As Double

        Try
            TotalQTYD7 = (CDbl(txtDoseD7.Text) * CDbl(txtFreqD7.Text) * CDbl(txtDurationD7.Text)) / CDbl(lblStrD7.Text)
            txtQTYD7.Text = Math.Round(TotalQTYD7, 2)

            If DefaultMaxQTYD7 > 0 Then
                txtQTYD7.Text = DefaultMaxQTYD7
            End If

        Catch ex As Exception
            txtQTYD7.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD8()
        Dim TotalQTYD8 As Double

        Try
            TotalQTYD8 = (CDbl(txtDoseD8.Text) * CDbl(txtFreqD8.Text) * CDbl(txtDurationD8.Text)) / CDbl(lblStrD8.Text)
            txtQTYD8.Text = Math.Round(TotalQTYD8, 2)

            If DefaultMaxQTYD8 > 0 Then
                txtQTYD8.Text = DefaultMaxQTYD8
            End If

        Catch ex As Exception
            txtQTYD8.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD9()
        Dim TotalQTYD9 As Double

        Try
            TotalQTYD9 = (CDbl(txtDoseD9.Text) * CDbl(txtFreqD9.Text) * CDbl(txtDurationD9.Text)) / CDbl(lblStrD9.Text)
            txtQTYD9.Text = Math.Round(TotalQTYD9, 2)

            If DefaultMaxQTYD9 > 0 Then
                txtQTYD9.Text = DefaultMaxQTYD9
            End If
        Catch ex As Exception
            txtQTYD9.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD10()
        Dim TotalQTYD10 As Double

        Try
            TotalQTYD10 = (CDbl(txtDoseD10.Text) * CDbl(txtFreqD10.Text) * CDbl(txtDurationD10.Text)) / CDbl(lblStrD10.Text)
            txtQTYD10.Text = Math.Round(TotalQTYD10, 2)

            If DefaultMaxQTYD10 > 0 Then
                txtQTYD10.Text = DefaultMaxQTYD10
            End If

        Catch ex As Exception
            txtQTYD10.Text = ""
        End Try
    End Sub
    Public Sub calculatedrugIn1()
        Dim In1TotalDose As Double
        Dim In1CartQTY As Double
        Dim In1MorDose As Double
        Dim In1NoonDose As Double
        Dim In1AfterNoonDose As Double
        Dim In1NightDose As Double
        If txtIn1MorDose.Text = "" Then
            In1MorDose = -2
        Else
            In1MorDose = txtIn1MorDose.Text
        End If
        If txtIn1NoonDose.Text = "" Then
            In1NoonDose = -2
        Else
            In1NoonDose = txtIn1NoonDose.Text
        End If
        If txtIn1AfterNoonDose.Text = "" Then
            In1AfterNoonDose = -2
        Else
            In1AfterNoonDose = txtIn1AfterNoonDose.Text
        End If
        If txtIn1NightDose.Text = "" Then
            In1NightDose = -2
        Else
            In1NightDose = txtIn1NightDose.Text
        End If


        Try

            In1TotalDose = ((In1MorDose + 2) * CDbl(txtIn1Duration.Text)) + ((In1NoonDose + 2) * CDbl(txtIn1Duration.Text)) + ((In1AfterNoonDose + 2) * CDbl(txtIn1Duration.Text)) + ((In1NightDose + 2) * CDbl(txtIn1Duration.Text))
            txtIn1TotalDose.Text = In1TotalDose
            In1CartQTY = In1TotalDose / CDbl(lblStrInsulin1.Text)
            If In1TotalDose = 0 Then
                txtIn1CartQTY.Clear()
                txtIn1TotalDose.Clear()
            Else txtIn1CartQTY.Text = Math.Ceiling(In1CartQTY)
            End If

        Catch ex As Exception
            'If In1TotalDose <= 0 Or ToString(In1TotalDose) = "" Then
            txtIn1CartQTY.Clear()
            'End If

        End Try
    End Sub
    Public Sub calculatedrugIn2()
        Dim In2TotalDose As Double
        Dim In2CartQTY As Double
        Dim In2MorDose As Double
        Dim In2NoonDose As Double
        Dim In2AfterNoonDose As Double
        Dim In2NightDose As Double
        If txtIn2MorDose.Text = "" Then
            In2MorDose = -2
        Else
            In2MorDose = txtIn2MorDose.Text
        End If
        If txtIn2NoonDose.Text = "" Then
            In2NoonDose = -2
        Else
            In2NoonDose = txtIn2NoonDose.Text
        End If
        If txtIn2AfterNoonDose.Text = "" Then
            In2AfterNoonDose = -2
        Else
            In2AfterNoonDose = txtIn2AfterNoonDose.Text
        End If
        If txtIn2NightDose.Text = "" Then
            In2NightDose = -2
        Else
            In2NightDose = txtIn2NightDose.Text
        End If
        Try
            In2TotalDose = ((In2MorDose + 2) * CDbl(txtIn2Duration.Text)) + ((In2NoonDose + 2) * CDbl(txtIn2Duration.Text)) + ((In2AfterNoonDose + 2) * CDbl(txtIn2Duration.Text)) + ((In2NightDose + 2) * CDbl(txtIn2Duration.Text))
            txtIn2TotalDose.Text = In2TotalDose
            In2CartQTY = In2TotalDose / CDbl(lblStrInsulin2.Text)
            If In2TotalDose = 0 Then
                txtIn2CartQTY.Clear()
                txtIn2TotalDose.Clear()
            Else txtIn2CartQTY.Text = Math.Ceiling(In2CartQTY)
            End If


        Catch ex As Exception
            txtIn2CartQTY.Clear()
        End Try
    End Sub
    Public Sub calculateDurationD1()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"

            txtDurationD1.Text = DurationDays.Days + 1


        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD2()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD2.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD3()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD3.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD4()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD4.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD5()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD5.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD6()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD6.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD7()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD7.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD8()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD8.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD9()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD9.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationD10()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtDurationD10.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationIn1()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtIn1Duration.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Sub calculateDurationIn2()
        Try
            Dim DurationDays As TimeSpan 'Declare Variable named Duration with TimeSpan as the datatype
            DurationDays = dtpDateCollection.Value - dtpDateSaved.Value 'Perform the calculation of two dates
            txtDurationMaster.Text = DurationDays.Days + 1 & " days"
            txtIn2Duration.Text = DurationDays.Days + 1
        Catch ex As Exception

        End Try
    End Sub



    Private Sub dtpDateCollection_ValueChanged(sender As Object, e As EventArgs) Handles dtpDateCollection.ValueChanged
        calculateDurationMaster()
        If Drug1Selected Then
            calculateDurationD1()
        End If
        If Drug2Selected Then
            calculateDurationD2()
        End If
        If Drug3Selected Then
            calculateDurationD3()
        End If
        If Drug4Selected Then
            calculateDurationD4()
        End If
        If Drug5Selected Then
            calculateDurationD5()
        End If
        If Drug6Selected Then
            calculateDurationD6()
        End If
        If Drug7Selected Then
            calculateDurationD7()
        End If
        If Drug8Selected Then
            calculateDurationD8()
        End If
        If Drug9Selected Then
            calculateDurationD9()
        End If
        If Drug10Selected Then
            calculateDurationD10()
        End If
        If Insulin1Selected Then
            calculateDurationIn1()
        End If
        If Insulin2Selected Then
            calculateDurationIn2()
        End If

    End Sub

    Public Sub cleardruginputsD1()
        'Clear the drugs text boxes
        'Drug 1
        lblStrD1.Text = ""
        lblUnitD1.Text = ""
        lblPreCatagoryD1.Text = ""
        txtDoseD1.Clear()
        txtFreqD1.Clear()
        txtDurationD1.Clear()
        txtQTYD1.Clear()
        Drug1Selected = False
    End Sub

    Public Sub cleardruginputsD2()
        'Clear the drugs text boxes
        'Drug 2
        lblStrD2.Text = ""
        lblUnitD2.Text = ""
        lblPreCatagoryD2.Text = ""
        txtDoseD2.Clear()
        txtFreqD2.Clear()
        txtDurationD2.Clear()
        txtQTYD2.Clear()
        Drug2Selected = False
    End Sub

    Public Sub cleardruginputsD3()
        'Clear the drugs text boxes
        'Drug 3
        lblStrD3.Text = ""
        lblUnitD3.Text = ""
        lblPreCatagoryD3.Text = ""
        txtDoseD3.Clear()
        txtFreqD3.Clear()
        txtDurationD3.Clear()
        txtQTYD3.Clear()
        Drug3Selected = False
    End Sub

    Public Sub cleardruginputsD4()
        'Clear the drugs text boxes
        'Drug 4
        lblStrD4.Text = ""
        lblUnitD4.Text = ""
        lblPreCatagoryD4.Text = ""
        txtDoseD4.Clear()
        txtFreqD4.Clear()
        txtDurationD4.Clear()
        txtQTYD4.Clear()
        Drug4Selected = False
    End Sub

    Public Sub cleardruginputsD5()
        'Clear the drugs text boxes
        'Drug 5
        lblStrD5.Text = ""
        lblUnitD5.Text = ""
        lblPreCatagoryD5.Text = ""
        txtDoseD5.Clear()
        txtFreqD5.Clear()
        txtDurationD5.Clear()
        txtQTYD5.Clear()
        Drug5Selected = False
    End Sub

    Public Sub cleardruginputsD6()
        'Clear the drugs text boxes
        'Drug 6
        lblStrD6.Text = ""
        lblUnitD6.Text = ""
        lblPreCatagoryD6.Text = ""
        txtDoseD6.Clear()
        txtFreqD6.Clear()
        txtDurationD6.Clear()
        txtQTYD6.Clear()
        Drug6Selected = False
    End Sub

    Public Sub cleardruginputsD7()
        'Clear the drugs text boxes
        'Drug 7
        lblStrD7.Text = ""
        lblUnitD7.Text = ""
        lblPreCatagoryD7.Text = ""
        txtDoseD7.Clear()
        txtFreqD7.Clear()
        txtDurationD7.Clear()
        txtQTYD7.Clear()
        Drug7Selected = False
    End Sub

    Public Sub cleardruginputsD8()
        'Clear the drugs text boxes
        'Drug 8
        lblStrD8.Text = ""
        lblUnitD8.Text = ""
        lblPreCatagoryD8.Text = ""
        txtDoseD8.Clear()
        txtFreqD8.Clear()
        txtDurationD8.Clear()
        txtQTYD8.Clear()
        Drug8Selected = False
    End Sub

    Public Sub cleardruginputsD9()
        'Clear the drugs text boxes
        'Drug 9
        lblStrD9.Text = ""
        lblUnitD9.Text = ""
        lblPreCatagoryD9.Text = ""
        txtDoseD9.Clear()
        txtFreqD9.Clear()
        txtDurationD9.Clear()
        txtQTYD9.Clear()
        Drug9Selected = False
    End Sub

    Public Sub cleardruginputsD10()
        'Clear the drugs text boxes
        'Drug 10
        lblStrD10.Text = ""
        lblUnitD10.Text = ""
        lblPreCatagoryD10.Text = ""
        txtDoseD10.Clear()
        txtFreqD10.Clear()
        txtDurationD10.Clear()
        txtQTYD10.Clear()
        Drug10Selected = False
    End Sub
    Public Sub cleardruginputsIn1()
        'Clear the drugs text boxes
        'Insulin 1
        lblStrInsulin1.Text = ""
        lblUnitInsulin1.Text = ""
        txtIn1MorDose.Clear()
        txtIn1NoonDose.Clear()
        txtIn1AfterNoonDose.Clear()
        txtIn1NightDose.Clear()
        txtIn1Duration.Clear()
        txtIn1POM.Clear()
        Insulin1Selected = False
    End Sub
    Public Sub cleardruginputsIn2()
        'Clear the drugs text boxes
        'Insulin 2
        lblStrInsulin2.Text = ""
        lblUnitInsulin2.Text = ""
        txtIn2MorDose.Clear()
        txtIn2NoonDose.Clear()
        txtIn2AfterNoonDose.Clear()
        txtIn2NightDose.Clear()
        txtIn2Duration.Clear()
        txtIn2POM.Clear()
        Insulin2Selected = False
    End Sub
    'Disable Drug 2 to 10 input for control
    Public Sub disabledrug2to10()
        cbDrug2.Enabled = False
        cbDrug3.Enabled = False
        cbDrug4.Enabled = False
        cbDrug5.Enabled = False
        cbDrug6.Enabled = False
        cbDrug7.Enabled = False
        cbDrug8.Enabled = False
        cbDrug9.Enabled = False
        cbDrug10.Enabled = False
        cbInsulin2.Enabled = False

    End Sub
    Public Sub clearSelectionIndex()
        cbDrug1.SelectedIndex = -1
        cbDrug2.SelectedIndex = -1
        cbDrug3.SelectedIndex = -1
        cbDrug4.SelectedIndex = -1
        cbDrug5.SelectedIndex = -1
        cbDrug6.SelectedIndex = -1
        cbDrug7.SelectedIndex = -1
        cbDrug8.SelectedIndex = -1
        cbDrug9.SelectedIndex = -1
        cbDrug10.SelectedIndex = -1
        cbInsulin1.SelectedIndex = -1
        cbInsulin2.SelectedIndex = -1
    End Sub
    Private Sub Form1_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        unhighlightallcb()
        'MsgBox("Executed")
        cbDrug1.SelectionLength = 0
    End Sub
    'All Drugs
    Public Sub PopulateAllDrugValues()
        ' Call populateValues for each drug and its respective labels
        populateValues(cbDrug1.Text, lblStrD1, lblUnitD1, lblPreCatagoryD1, ConsumeMethodD1, ConsumeUnitD1, RemarkD1, DefaultMaxQTYD1)
        populateValues(cbDrug2.Text, lblStrD2, lblUnitD2, lblPreCatagoryD2, ConsumeMethodD2, ConsumeUnitD2, RemarkD2, DefaultMaxQTYD2)
        populateValues(cbDrug3.Text, lblStrD3, lblUnitD3, lblPreCatagoryD3, ConsumeMethodD3, ConsumeUnitD3, RemarkD3, DefaultMaxQTYD3)
        populateValues(cbDrug4.Text, lblStrD4, lblUnitD4, lblPreCatagoryD4, ConsumeMethodD4, ConsumeUnitD4, RemarkD4, DefaultMaxQTYD4)
        populateValues(cbDrug5.Text, lblStrD5, lblUnitD5, lblPreCatagoryD5, ConsumeMethodD5, ConsumeUnitD5, RemarkD5, DefaultMaxQTYD5)
        populateValues(cbDrug6.Text, lblStrD6, lblUnitD6, lblPreCatagoryD6, ConsumeMethodD6, ConsumeUnitD6, RemarkD6, DefaultMaxQTYD6)
        populateValues(cbDrug7.Text, lblStrD7, lblUnitD7, lblPreCatagoryD7, ConsumeMethodD7, ConsumeUnitD7, RemarkD7, DefaultMaxQTYD7)
        populateValues(cbDrug8.Text, lblStrD8, lblUnitD8, lblPreCatagoryD8, ConsumeMethodD8, ConsumeUnitD8, RemarkD8, DefaultMaxQTYD8)
        populateValues(cbDrug9.Text, lblStrD9, lblUnitD9, lblPreCatagoryD9, ConsumeMethodD9, ConsumeUnitD9, RemarkD9, DefaultMaxQTYD9)
        populateValues(cbDrug10.Text, lblStrD10, lblUnitD10, lblPreCatagoryD10, ConsumeMethodD10, ConsumeUnitD10, RemarkD10, DefaultMaxQTYD10)
    End Sub

    'Drug 1
    Private Sub cbDrug1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug1.SelectedIndexChanged
        cbDrug1.SelectionLength = cbDrug1.Text.Length
        unhighlightallcb()
        populateValues(cbDrug1.Text, lblStrD1, lblUnitD1, lblPreCatagoryD1, ConsumeMethodD1, ConsumeUnitD1, RemarkD1, DefaultMaxQTYD1)

        calculatedrugD1()
        calculateDurationD1()
        Drug1Selected = True
        cbDrug2.Enabled = True
        txtDoseD1.Focus()
    End Sub

    Private Sub cbDrug1_TextChanged(sender As Object, e As EventArgs) Handles cbDrug1.TextChanged
        cleardruginputsD1()
    End Sub
    'Drug 2
    Private Sub cbDrug2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug2.SelectedIndexChanged
        populateValues(cbDrug2.Text, lblStrD2, lblUnitD2, lblPreCatagoryD2, ConsumeMethodD2, ConsumeUnitD2, RemarkD2, DefaultMaxQTYD2)

        calculatedrugD2()
        unhighlightallcb()
        calculateDurationD2()
        txtDoseD2.Focus()
        Drug2Selected = True
        cbDrug3.Enabled = True
    End Sub

    Private Sub cbDrug2_TextChanged(sender As Object, e As EventArgs) Handles cbDrug2.TextChanged
        cleardruginputsD2()
    End Sub
    'Drug 3
    Private Sub cbDrug3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug3.SelectedIndexChanged
        populateValues(cbDrug3.Text, lblStrD3, lblUnitD3, lblPreCatagoryD3, ConsumeMethodD3, ConsumeUnitD3, RemarkD3, DefaultMaxQTYD3)

        calculatedrugD3()
        unhighlightallcb()
        calculateDurationD3()
        txtDoseD3.Focus()
        Drug3Selected = True
        cbDrug4.Enabled = True
    End Sub
    Private Sub cbDrug3_TextChanged(sender As Object, e As EventArgs) Handles cbDrug3.TextChanged
        cleardruginputsD3()
    End Sub

    'Drug 4
    Private Sub cbDrug4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug4.SelectedIndexChanged
        populateValues(cbDrug4.Text, lblStrD4, lblUnitD4, lblPreCatagoryD4, ConsumeMethodD4, ConsumeUnitD4, RemarkD4, DefaultMaxQTYD4)

        calculatedrugD4()
        lbDrugNumber4.Focus()
        unhighlightallcb()
        calculateDurationD4()
        txtDoseD4.Focus()
        Drug4Selected = True
        cbDrug5.Enabled = True
    End Sub

    Private Sub cbDrug4_TextChanged(sender As Object, e As EventArgs) Handles cbDrug4.TextChanged
        cleardruginputsD4()
    End Sub
    'Drug 5
    Private Sub cbDrug5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug5.SelectedIndexChanged
        populateValues(cbDrug5.Text, lblStrD5, lblUnitD5, lblPreCatagoryD5, ConsumeMethodD5, ConsumeUnitD5, RemarkD5, DefaultMaxQTYD5)

        calculatedrugD5()
        unhighlightallcb()
        calculateDurationD5()
        txtDoseD5.Focus()
        Drug5Selected = True
        cbDrug6.Enabled = True
    End Sub

    Private Sub cbDrug5_TextChanged(sender As Object, e As EventArgs) Handles cbDrug5.TextChanged
        cleardruginputsD5()
    End Sub
    'Drug 6
    Private Sub cbDrug6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug6.SelectedIndexChanged
        populateValues(cbDrug6.Text, lblStrD6, lblUnitD6, lblPreCatagoryD6, ConsumeMethodD6, ConsumeUnitD6, RemarkD6, DefaultMaxQTYD6)

        calculatedrugD6()
        unhighlightallcb()
        calculateDurationD6()
        txtDoseD6.Focus()
        Drug6Selected = True
        cbDrug7.Enabled = True
    End Sub

    Private Sub cbDrug6_TextChanged(sender As Object, e As EventArgs) Handles cbDrug6.TextChanged
        cleardruginputsD6()
    End Sub
    'Drug 7
    Private Sub cbDrug7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug7.SelectedIndexChanged
        populateValues(cbDrug7.Text, lblStrD7, lblUnitD7, lblPreCatagoryD7, ConsumeMethodD7, ConsumeUnitD7, RemarkD7, DefaultMaxQTYD7)

        calculatedrugD7()
        unhighlightallcb()
        calculateDurationD7()
        txtDoseD7.Focus()
        Drug7Selected = True
        cbDrug8.Enabled = True
    End Sub

    Private Sub cbDrug7_TextChanged(sender As Object, e As EventArgs) Handles cbDrug7.TextChanged
        cleardruginputsD7()
    End Sub
    'Drug 8
    Private Sub cbDrug8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug8.SelectedIndexChanged
        populateValues(cbDrug8.Text, lblStrD8, lblUnitD8, lblPreCatagoryD8, ConsumeMethodD8, ConsumeUnitD8, RemarkD8, DefaultMaxQTYD8)

        calculatedrugD8()
        unhighlightallcb()
        calculateDurationD8()
        txtDoseD8.Focus()
        Drug8Selected = True
        cbDrug9.Enabled = True
    End Sub
    Private Sub cbDrug8_TextChanged(sender As Object, e As EventArgs) Handles cbDrug8.TextChanged
        cleardruginputsD8()
    End Sub

    'Drug 9
    Private Sub cbDrug9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug9.SelectedIndexChanged
        populateValues(cbDrug9.Text, lblStrD9, lblUnitD9, lblPreCatagoryD9, ConsumeMethodD9, ConsumeUnitD9, RemarkD9, DefaultMaxQTYD9)

        calculatedrugD9()
        unhighlightallcb()
        calculateDurationD9()
        txtDoseD9.Focus()
        Drug9Selected = True
        cbDrug10.Enabled = True
    End Sub

    Private Sub cbDrug9_TextChanged(sender As Object, e As EventArgs) Handles cbDrug9.TextChanged
        cleardruginputsD9()
    End Sub
    'Drug 10
    Private Sub cbDrug10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug10.SelectedIndexChanged
        populateValues(cbDrug10.Text, lblStrD10, lblUnitD10, lblPreCatagoryD10, ConsumeMethodD10, ConsumeUnitD10, RemarkD10, DefaultMaxQTYD10)

        calculatedrugD10()
        unhighlightallcb()
        calculateDurationD10()
        txtDoseD10.Focus()
        Drug10Selected = True
    End Sub

    Private Sub cbDrug10_TextChanged(sender As Object, e As EventArgs) Handles cbDrug10.TextChanged
        cleardruginputsD10()
    End Sub
    Public Sub resetselecteddrugindex()
        cbDrug1.SelectedIndex = -1
        cbDrug2.SelectedIndex = -1
        cbDrug3.SelectedIndex = -1
        cbDrug4.SelectedIndex = -1
        cbDrug5.SelectedIndex = -1
        cbDrug6.SelectedIndex = -1
        cbDrug7.SelectedIndex = -1
        cbDrug8.SelectedIndex = -1
        cbDrug9.SelectedIndex = -1
        cbDrug10.SelectedIndex = -1
        cbInsulin1.SelectedIndex = -1
        cbInsulin2.SelectedIndex = -1
    End Sub

    Public Sub CheckForSelectedDrugsnew()
        ' Arrays for drug ComboBoxes and their corresponding selection flags
        Dim drugComboBoxes = {cbDrug1, cbDrug2, cbDrug3, cbDrug4, cbDrug5, cbDrug6, cbDrug7, cbDrug8, cbDrug9, cbDrug10}
        Dim drugSelectedFlags = {Drug1Selected, Drug2Selected, Drug3Selected, Drug4Selected, Drug5Selected, Drug6Selected, Drug7Selected, Drug8Selected, Drug9Selected, Drug10Selected}

        ' Loop through the drug ComboBoxes
        For i As Integer = 0 To drugComboBoxes.Length - 1
            ' Check if the ComboBox is selected
            If drugComboBoxes(i).SelectedIndex >= 0 Then
                drugSelectedFlags(i) = True


                drugComboBoxes(i + 1).Enabled = True

            Else
                drugSelectedFlags(i) = False

            End If
        Next

        ' Handle insulin ComboBoxes
        If cbInsulin1.SelectedIndex >= 0 Then
            Insulin1Selected = True
            cbInsulin2.Enabled = True
        Else
            Insulin1Selected = False
            cbInsulin2.Enabled = False
        End If

        If cbInsulin2.SelectedIndex >= 0 Then
            Insulin2Selected = True
        Else
            Insulin2Selected = False
        End If
    End Sub







    'Check for Validations 'START
    'Drug 1
    Private Sub txtDoseD1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD1.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            'MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD1.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            'MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD1.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            'MessageBox.Show("Please enter numbers only")
            e.Handled = True
        End If
    End Sub
    'Drug 2
    Private Sub txtDoseD2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD2.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD2.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD2.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 3
    Private Sub txtDoseD3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD3.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD3.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD3.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 4
    Private Sub txtDoseD4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD4.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD4.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD4.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 5
    Private Sub txtDoseD5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD5.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD5.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD5.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 6
    Private Sub txtDoseD6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD6.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD6.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD6.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 7
    Private Sub txtDoseD7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD7.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD7.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD7.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 8
    Private Sub txtDoseD8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD8.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD8.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD8.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 9
    Private Sub txtDoseD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD9.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD9.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD9.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug 10
    Private Sub txtDoseD10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDoseD10.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFreqD10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFreqD10.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtQTYD10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQTYD10.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Insulin 1
    Private Sub txtIn1MorDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1MorDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1NoonDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1NoonDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1AfterNoonDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1AfterNoonDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1NightDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1NightDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1Duration_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1Duration.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1TotalDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1TotalDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1POM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1POM.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn1CartQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn1CartQTY.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Insulin 2
    Private Sub txtIn2MorDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2MorDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2NoonDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2NoonDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2AfterNoonDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2AfterNoonDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2NightDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2NightDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2Duration_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2Duration.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2TotalDose_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2TotalDose.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2POM_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2POM.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtIn2CartQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIn2CartQTY.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Drug Tab
    Private Sub txtStrength_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStrength.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtDefaultMaxQTY_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDefaultMaxQTY.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Print Label Textbox
    Private Sub txtLabelHeight_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabelHeight.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Asc(e.KeyChar) <> 46 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtLabelWidth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLabelWidth.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    'Check for Validations 'END
    'Check for Text Changes 'Start
    'Drug 1
    Private Sub txtDoseD1_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD1.TextChanged
        calculatedrugD1()
    End Sub

    Private Sub txtFreqD1_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD1.TextChanged
        calculatedrugD1()
    End Sub

    Private Sub txtDurationD1_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD1.TextChanged
        calculatedrugD1()
    End Sub
    'Drug 2
    Private Sub txtDoseD2_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD2.TextChanged
        calculatedrugD2()
    End Sub

    Private Sub txtFreqD2_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD2.TextChanged
        calculatedrugD2()
    End Sub

    Private Sub txtDurationD2_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD2.TextChanged
        calculatedrugD2()
    End Sub
    'Drug 3
    Private Sub txtDoseD3_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD3.TextChanged
        calculatedrugD3()
    End Sub

    Private Sub txtFreqD3_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD3.TextChanged
        calculatedrugD3()
    End Sub

    Private Sub txtDurationD3_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD3.TextChanged
        calculatedrugD3()
    End Sub
    'Drug 4
    Private Sub txtDoseD4_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD4.TextChanged
        calculatedrugD4()
    End Sub

    Private Sub txtFreqD4_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD4.TextChanged
        calculatedrugD4()
    End Sub

    Private Sub txtDurationD4_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD4.TextChanged
        calculatedrugD4()
    End Sub
    'Drug 5
    Private Sub txtDoseD5_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD5.TextChanged
        calculatedrugD5()
    End Sub

    Private Sub txtFreqD5_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD5.TextChanged
        calculatedrugD5()
    End Sub

    Private Sub txtDurationD5_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD5.TextChanged
        calculatedrugD5()
    End Sub
    'Drug 6
    Private Sub txtDoseD6_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD6.TextChanged
        calculatedrugD6()
    End Sub

    Private Sub txtFreqD6_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD6.TextChanged
        calculatedrugD6()
    End Sub

    Private Sub txtDurationD6_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD6.TextChanged
        calculatedrugD6()
    End Sub
    'Drug 7
    Private Sub txtDoseD7_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD7.TextChanged
        calculatedrugD7()
    End Sub

    Private Sub txtFreqD7_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD7.TextChanged
        calculatedrugD7()
    End Sub

    Private Sub txtDurationD7_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD7.TextChanged
        calculatedrugD7()
    End Sub
    'Drug 8
    Private Sub txtDoseD8_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD8.TextChanged
        calculatedrugD8()
    End Sub

    Private Sub txtFreqD8_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD8.TextChanged
        calculatedrugD8()
    End Sub

    Private Sub txtDurationD8_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD8.TextChanged
        calculatedrugD8()
    End Sub
    'Drug 9
    Private Sub txtDoseD9_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD9.TextChanged
        calculatedrugD9()
    End Sub

    Private Sub txtFreqD9_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD9.TextChanged
        calculatedrugD9()
    End Sub

    Private Sub txtDurationD9_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD9.TextChanged
        calculatedrugD9()
    End Sub
    'Drug 10
    Private Sub txtDoseD10_TextChanged(sender As Object, e As EventArgs) Handles txtDoseD10.TextChanged
        calculatedrugD10()
    End Sub

    Private Sub txtFreqD10_TextChanged(sender As Object, e As EventArgs) Handles txtFreqD10.TextChanged
        calculatedrugD10()
    End Sub

    Private Sub txtDurationD10_TextChanged(sender As Object, e As EventArgs) Handles txtDurationD10.TextChanged
        calculatedrugD10()
    End Sub

    Private Sub btnClearAll_Click(sender As Object, e As EventArgs) Handles btnClearAll.Click
        clearall()
    End Sub

    'Check for Text Changes 'End
    Public Sub clearall()
        txtPatientName.Clear()
        txtICNo.Clear()
        cbAddDays.SelectedIndex = 3
        chboxNoICNumber.Checked = False
        cleardruginputsD1()
        cleardruginputsD2()
        cleardruginputsD3()
        cleardruginputsD4()
        cleardruginputsD5()
        cleardruginputsD6()
        cleardruginputsD7()
        cleardruginputsD8()
        cleardruginputsD9()
        cleardruginputsD10()
        cleardruginputsIn1()
        cleardruginputsIn2()

        cbDrug1.Text = ""
        cbDrug2.Text = ""
        cbDrug3.Text = ""
        cbDrug4.Text = ""
        cbDrug5.Text = ""
        cbDrug6.Text = ""
        cbDrug7.Text = ""
        cbDrug8.Text = ""
        cbDrug9.Text = ""
        cbDrug10.Text = ""
        cbInsulin1.Text = ""
        cbInsulin2.Text = ""
        resetselecteddrugindex()
        CheckForSelectedDrugsnew()
        disabledrug2to10()

        txtDurationD1.Clear()
        txtDurationD2.Clear()
        txtDurationD3.Clear()
        txtDurationD4.Clear()
        txtDurationD5.Clear()
        txtDurationD6.Clear()
        txtDurationD7.Clear()
        txtDurationD8.Clear()
        txtDurationD9.Clear()
        txtDurationD10.Clear()
        txtIn1Duration.Clear()
        txtIn2Duration.Clear()
        txtIn1TotalDose.Clear()
        txtIn2TotalDose.Clear()

        txtICNo.Focus()

    End Sub
    Public Sub unhighlightallcb()
        cbDrug1.SelectionLength = 0
        cbDrug2.SelectionLength = 0
        cbDrug3.SelectionLength = 0
        cbDrug4.SelectionLength = 0
        cbDrug5.SelectionLength = 0
        cbDrug6.SelectionLength = 0
        cbDrug7.SelectionLength = 0
        cbDrug8.SelectionLength = 0
        cbDrug9.SelectionLength = 0
        cbDrug10.SelectionLength = 0
    End Sub

    Private Sub txtICNo_TextChanged(sender As Object, e As EventArgs) Handles txtICNo.TextChanged
        ' Check if the length of the textbox text is equal to or greater than 6 or 9
        btnIOU.Enabled = False
        lblExistingPatient.Text = ""
        lblAge.Text = "Age"
        lblGender.Text = "Gender"
        If chboxNoICNumber.Checked = False Then
            ' Get the current position of the cursor
            Dim currentPosition As Integer = txtICNo.SelectionStart

            ' Remove the existing hyphens to simplify processing
            Dim text As String = txtICNo.Text.Replace("-", String.Empty)

            ' Reinsert the hyphens at the 7th and 9th positions if the text is long enough
            If text.Length > 6 Then
                text = text.Insert(6, "-")
            End If
            If text.Length > 9 Then
                text = text.Insert(9, "-")
            End If

            ' Update the TextBox text without triggering another TextChanged event
            RemoveHandler txtICNo.TextChanged, AddressOf txtICNo_TextChanged
            txtICNo.Text = text
            AddHandler txtICNo.TextChanged, AddressOf txtICNo_TextChanged

            ' Adjust the cursor position
            If currentPosition <= 6 Then
                txtICNo.SelectionStart = currentPosition
            ElseIf currentPosition > 6 AndAlso currentPosition <= 8 Then
                txtICNo.SelectionStart = currentPosition + 1
            ElseIf currentPosition > 8 Then
                txtICNo.SelectionStart = currentPosition + 2
            End If
            If txtICNo.TextLength < 14 Then
                btnCheckICMySPR.Enabled = False
            End If
            If txtICNo.TextLength = 14 Then
                Try
                    checkICfromDB()
                    'Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE Name = '" & txtPatientName.Text.ToString() & "'", conn)
                    Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE Name = @name", conn)
                    cmd.Parameters.AddWithValue("@name", txtPatientName.Text.ToString()) ' Use parameterized query

                    Try
                        conn.Open()
                        dr = cmd.ExecuteReader
                        While dr.Read()
                            If dr.Item("Name").ToString() = txtPatientName.Text.ToString() And dr.Item("ICNo") = txtICNo.Text Then
                                btnIOU.Enabled = True
                                lblExistingPatient.Text = "Existing Patient Found!"
                                'txtICNo.Text = dr.Item("ICNo")
                            End If
                        End While
                        conn.Close()
                    Catch ex As Exception
                        conn.Close()
                    End Try

                    Dim ICRegexPattern As String = "^((\d{2}(?!0229))|([02468][048]|[13579][26])(?=0229))(0[1-9]|1[0-2])(0[1-9]|[12]\d|(?<!02)30|(?<!02|4|6|9|11)31)-(\d{2})-(\d{4})$"
                    If Regex.IsMatch(txtICNo.Text, ICRegexPattern) = False Then
                        MsgBox("IC Number incorrect Regex format. Please check again.")
                    End If
                    btnCheckICMySPR.Enabled = True
                    Dim DOB As String
                    Dim DOBDate As Date
                    Dim Gender As String

                    DOB = ExtractDOB()

                    DOBDate = DateTime.Parse(DOB)
                    Dim currentDate As Date = Date.Today

                    ' Calculate the difference in years
                    Dim age As Integer = currentDate.Year - DOBDate.Year

                    ' Check if the birthday has occurred this year
                    If currentDate < DOBDate.AddYears(age) Then
                        age -= 1
                    End If
                    lblAge.Text = "Age: " & age
                    Gender = ExtractGender()
                    lblGender.Text = "Gender: " & Gender

                Catch ex As Exception
                    'MsgBox(ex.Message)'
                End Try

            End If
        End If


    End Sub


    Private Sub txtICNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtICNo.KeyPress
        ' Only allow numeric input and handle Backspace
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) AndAlso Not chboxNoICNumber.Checked Then
            e.Handled = True
        End If

        ' Handle Backspace to prevent disabling text change
        If Asc(e.KeyChar) = 8 Then
            disableTextChanged = True
        End If

        ' Automatically convert to uppercase if it's a letter
        If Char.IsLetter(e.KeyChar) Then
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End If
    End Sub




    Private Sub btnIOU_Click(sender As Object, e As EventArgs) Handles btnIOU.Click
        Dim newForm As New Form2()
        newForm.Show()
        txtICNoDB.Text = txtICNo.Text
        'Form2.Show()
    End Sub

    Private Sub btnCheckICMySPR_Click(sender As Object, e As EventArgs) Handles btnCheckICMySPR.Click
        Dim weburl As String
        weburl = "https://mysprsemak.spr.gov.my/semakan/daftarPemilih"
        Dim originalString As String = txtICNo.Text
        Dim stringWithoutDash As String = originalString.Replace("-", "")
        Process.Start(weburl)
        My.Computer.Clipboard.SetText(stringWithoutDash)
    End Sub

    Public Function ExtractDOB() As String
        Dim originalString As String = txtICNo.Text
        Dim ICstringWithoutDash As String = originalString.Replace("-", "")
        Dim strIC As String
        strIC = ICstringWithoutDash
        Dim strDob As String, strDay As String, strMonth As String, strYear As String
        strDay = strIC.Substring(4, 2)
        strMonth = strIC.Substring(2, 2)
        Select Case strMonth
            Case "01"
                strMonth = "JANUARY"
            Case "02"
                strMonth = "FEBRUARY"
            Case "03"
                strMonth = "MARCH"
            Case "04"
                strMonth = "APRIL"
            Case "05"
                strMonth = "MAY"
            Case "06"
                strMonth = "JUNE"
            Case "07"
                strMonth = "JULY"
            Case "08"
                strMonth = "AUGUST"
            Case "09"
                strMonth = "SEPTEMBER"
            Case "10"
                strMonth = "OCTOBER"
            Case "11"
                strMonth = "NOVEMBER"
            Case "12"
                strMonth = "DECEMBER"
        End Select

        strYear = strIC.Substring(0, 2)
        If strYear <= 25 Then
            strYear = "20" & strYear
        ElseIf strYear > 25 Then
            strYear = "19" & strYear
        End If

        strDob = strYear & "-" & strMonth & "-" & strDay


        Return strDob

    End Function

    Public Function ExtractGender() As String
        Dim originalString As String = txtICNo.Text
        Dim ICstringWithoutDash As String = originalString.Replace("-", "")
        Dim strIC As String
        strIC = ICstringWithoutDash
        Dim strGender As String
        Dim strGenderCode As Integer = CInt(strIC.Substring(11, 1))
        If strGenderCode Mod 2 <> 0 Then
            strGender = "MALE"
        Else
            strGender = "FEMALE"
        End If
        Return strGender
    End Function

    Private Sub cboxEnablePrintPDF_CheckedChanged(sender As Object, e As EventArgs) Handles cboxEnablePrintPDF.CheckedChanged
        If cboxEnablePrintPDF.Checked Then
            btnSave.Text = "Save and Print"
            btnSave.BackColor = Color.GreenYellow
        End If
        If cboxEnablePrintPDF.Checked = False Then
            btnSave.Text = "Save only"
            btnSave.BackColor = Color.Azure
        End If
        Dim EnablePrintAfterSave As Boolean = My.Settings.EnablePrintAfterSave
        Dim EnablePrintAfterSaveNew As Boolean
        EnablePrintAfterSaveNew = cboxEnablePrintPDF.Checked
        My.Settings.EnablePrintAfterSave = EnablePrintAfterSaveNew
        My.Settings.Save()

    End Sub

    Private Sub cbInsulin1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbInsulin1.SelectedIndexChanged
        populatevaluesInsulin1()
        Insulin1Selected = True
        cbInsulin2.Enabled = True
        calculateDurationIn1()
    End Sub

    Private Sub cbInsulin2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbInsulin2.SelectedIndexChanged
        populatevaluesInsulin2()
        Insulin2Selected = True
        calculateDurationIn2()
    End Sub

    Private Sub cbInsulin1_TextChanged(sender As Object, e As EventArgs) Handles cbInsulin1.TextChanged
        cleardruginputsIn1()
    End Sub

    Private Sub cbInsulin2_TextChanged(sender As Object, e As EventArgs) Handles cbInsulin2.TextChanged
        cleardruginputsIn2()
    End Sub
    'Insulin 1 Calculate
    Private Sub txtIn1MorDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn1MorDose.TextChanged
        calculatedrugIn1()
    End Sub

    Private Sub txtIn1NoonDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn1NoonDose.TextChanged
        calculatedrugIn1()
    End Sub

    Private Sub txtIn1AfterNoonDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn1AfterNoonDose.TextChanged
        calculatedrugIn1()
    End Sub

    Private Sub txtIn1NightDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn1NightDose.TextChanged
        calculatedrugIn1()
    End Sub

    Private Sub txtIn1Duration_TextChanged(sender As Object, e As EventArgs) Handles txtIn1Duration.TextChanged
        calculatedrugIn1()
    End Sub
    'Insulin 2 Calculate
    Private Sub txtIn2MorDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn2MorDose.TextChanged
        calculatedrugIn2()
    End Sub

    Private Sub txtIn2NoonDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn2NoonDose.TextChanged
        calculatedrugIn2()
    End Sub

    Private Sub txtIn2AfterNoonDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn2AfterNoonDose.TextChanged
        calculatedrugIn2()
    End Sub

    Private Sub txtIn2NightDose_TextChanged(sender As Object, e As EventArgs) Handles txtIn2NightDose.TextChanged
        calculatedrugIn2()
    End Sub

    Private Sub txtIn2Duration_TextChanged(sender As Object, e As EventArgs) Handles txtIn2Duration.TextChanged
        calculatedrugIn2()
    End Sub

    Private Sub cbAddDays_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAddDays.SelectedIndexChanged
        Dim days
        If cbAddDays.SelectedIndex = 0 Then
            dtpDateCollection.Value = Now().Date.AddDays(7)
        End If
        If cbAddDays.SelectedIndex = 1 Then
            dtpDateCollection.Value = Now().Date.AddDays(14)
        End If
        If cbAddDays.SelectedIndex = 2 Then
            dtpDateCollection.Value = Now().Date.AddDays(21)
        End If
        If cbAddDays.SelectedIndex = 3 Then
            dtpDateCollection.Value = Now().Date.AddDays(30)
        End If
        If cbAddDays.SelectedIndex = 4 Then
            dtpDateCollection.Value = Now().Date.AddDays(40)
        End If
        If cbAddDays.SelectedIndex = 5 Then
            dtpDateCollection.Value = Now().Date.AddDays(50)
        End If
        If cbAddDays.SelectedIndex = 6 Then
            dtpDateCollection.Value = Now().Date.AddDays(60)
        End If
        If cbAddDays.SelectedIndex = 7 Then
            dtpDateCollection.Value = Now().Date.AddDays(90)
        End If


        days = dtpDateCollection.Value - dtpDateSaved.Value
        txtDurationMaster.Text = days.days + 1 & " days"

    End Sub

    Private Sub btnSaveDBSettings_Click(sender As Object, e As EventArgs) Handles btnSaveDBSettings.Click
        SetandSaveDBSettings()
    End Sub

    Private Sub btnRestart_Click(sender As Object, e As EventArgs) Handles btnRestart.Click
        Application.Restart()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        SetandSavePrinterSettings()
    End Sub

    Private Sub txtIn1POM_TextChanged(sender As Object, e As EventArgs) Handles txtIn1POM.TextChanged
        calculatedrugIn1()
        Try
            If txtIn1POM.Text = "" Then
                calculatedrugIn1()
            End If
            txtIn1CartQTY.Text = txtIn1CartQTY.Text - txtIn1POM.Text
            If txtIn1CartQTY.Text < 0 Then
                txtIn1CartQTY.Text = 0
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtIn2POM_TextChanged(sender As Object, e As EventArgs) Handles txtIn2POM.TextChanged
        calculatedrugIn2()
        Try
            If txtIn1POM.Text = "" Then
                calculatedrugIn2()
            End If
            txtIn2CartQTY.Text = txtIn2CartQTY.Text - txtIn2POM.Text
            If txtIn2CartQTY.Text < 0 Then
                txtIn2CartQTY.Text = 0
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cbAddDays_TextChanged(sender As Object, e As EventArgs) Handles cbAddDays.TextChanged
        Try
            Dim days
            dtpDateCollection.Value = Now().Date.AddDays(cbAddDays.Text)
            days = dtpDateCollection.Value - dtpDateSaved.Value
            txtDurationMaster.Text = days.days + 1 & " days"
        Catch ex As Exception
            Dim days
            dtpDateCollection.Value = Now().Date.AddDays(0)
            days = dtpDateCollection.Value - dtpDateSaved.Value
            txtDurationMaster.Text = days.days + 1 & " days"
        End Try

    End Sub

    Private Sub cbAddDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbAddDays.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    'LOG TAB
    Public Sub loadLogDGV()
        DataGridViewDrug.Rows.Clear()
        DataGridViewInsulin.Rows.Clear()
        Dim dt As New DataTable

        'Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & lblPrevSavedICNo.Text & "'", conn)
        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE (SELECT MAX(Timestamp) FROM `prescribeddrugs`) ORDER BY Timestamp DESC LIMIT 1; ", conn)
        Try

            conn.Open()
            dr = cmd.ExecuteReader
            While dr.Read()
                DataGridViewDrug.Rows.Add("1", dr.Item("Drug1Name"), dr.Item("Drug1Strength"), dr.Item("Drug1Unit"), dr.Item("Drug1Dose"), dr.Item("Drug1Freq"), dr.Item("Drug1Duration"), dr.Item("Drug1TotalQTY"))
                DataGridViewDrug.Rows.Add("2", dr.Item("Drug2Name"), dr.Item("Drug2Strength"), dr.Item("Drug2Unit"), dr.Item("Drug2Dose"), dr.Item("Drug2Freq"), dr.Item("Drug2Duration"), dr.Item("Drug2TotalQTY"))
                DataGridViewDrug.Rows.Add("3", dr.Item("Drug3Name"), dr.Item("Drug3Strength"), dr.Item("Drug3Unit"), dr.Item("Drug3Dose"), dr.Item("Drug3Freq"), dr.Item("Drug3Duration"), dr.Item("Drug3TotalQTY"))
                DataGridViewDrug.Rows.Add("4", dr.Item("Drug4Name"), dr.Item("Drug4Strength"), dr.Item("Drug4Unit"), dr.Item("Drug4Dose"), dr.Item("Drug4Freq"), dr.Item("Drug4Duration"), dr.Item("Drug4TotalQTY"))
                DataGridViewDrug.Rows.Add("5", dr.Item("Drug5Name"), dr.Item("Drug5Strength"), dr.Item("Drug5Unit"), dr.Item("Drug5Dose"), dr.Item("Drug5Freq"), dr.Item("Drug5Duration"), dr.Item("Drug5TotalQTY"))
                DataGridViewDrug.Rows.Add("6", dr.Item("Drug6Name"), dr.Item("Drug6Strength"), dr.Item("Drug6Unit"), dr.Item("Drug6Dose"), dr.Item("Drug6Freq"), dr.Item("Drug6Duration"), dr.Item("Drug6TotalQTY"))
                DataGridViewDrug.Rows.Add("7", dr.Item("Drug7Name"), dr.Item("Drug7Strength"), dr.Item("Drug7Unit"), dr.Item("Drug7Dose"), dr.Item("Drug7Freq"), dr.Item("Drug7Duration"), dr.Item("Drug7TotalQTY"))
                DataGridViewDrug.Rows.Add("8", dr.Item("Drug8Name"), dr.Item("Drug8Strength"), dr.Item("Drug8Unit"), dr.Item("Drug8Dose"), dr.Item("Drug8Freq"), dr.Item("Drug8Duration"), dr.Item("Drug8TotalQTY"))
                DataGridViewDrug.Rows.Add("9", dr.Item("Drug9Name"), dr.Item("Drug9Strength"), dr.Item("Drug9Unit"), dr.Item("Drug9Dose"), dr.Item("Drug9Freq"), dr.Item("Drug9Duration"), dr.Item("Drug9TotalQTY"))
                DataGridViewDrug.Rows.Add("10", dr.Item("Drug10Name"), dr.Item("Drug10Strength"), dr.Item("Drug10Unit"), dr.Item("Drug10Dose"), dr.Item("Drug10Freq"), dr.Item("Drug10Duration"), dr.Item("Drug10TotalQTY"))
                DataGridViewInsulin.Rows.Add("1", dr.Item("Insulin1Name"), dr.Item("Insulin1Strength"), dr.Item("Insulin1Unit"), dr.Item("Insulin1MorDose"), dr.Item("Insulin1NoonDose"), dr.Item("Insulin1AfternoonDose"), dr.Item("Insulin1NightDose"), dr.Item("Insulin1Freq"), dr.Item("Insulin1Duration"), dr.Item("Insulin1TotalDose"), dr.Item("Insulin1POM"), dr.Item("Insulin1CartQTY"))
                DataGridViewInsulin.Rows.Add("2", dr.Item("Insulin2Name"), dr.Item("Insulin2Strength"), dr.Item("Insulin2Unit"), dr.Item("Insulin2MorDose"), dr.Item("Insulin2NoonDose"), dr.Item("Insulin2AfternoonDose"), dr.Item("Insulin2NightDose"), dr.Item("Insulin2Freq"), dr.Item("Insulin2Duration"), dr.Item("Insulin2TotalDose"), dr.Item("Insulin2POM"), dr.Item("Insulin2CartQTY"))

                lblPrevSavedName.Text = dr.Item("Name")
                lblPrevSavedICNo.Text = dr.Item("ICNo")
                lblLogPrevPatientTimestamp.Text = dr.Item("Timestamp")
                lblLogPrevPatientDateCollection.Text = dr.Item("DateCollection")
                lblLogPrevPatientDateSeeDoctor.Text = dr.Item("DateSeeDoctor")


            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            dr.Dispose()
            conn.Close()
        End Try


    End Sub

    Public Sub loadDrugQtyDGV()
        dgvDrugQty.Rows.Clear() ' Clear the dgvDrugQty DataGridView

        Dim dt As New DataTable
        Dim count As Integer = 0
        Dim totalQty As Integer = 0 ' Initialize total quantity
        Dim startDate As String = dtpDrugQty1.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim endDate As String = dtpDrugQty2.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim cmd As New MySqlCommand("
        SELECT 
            ID,
            Date,
            Name,
            ICNo,
            DateCollection,
            DateSeeDoctor,
            Timestamp,
            Drug1Name,
            Drug1TotalQTY,
            Drug2Name,
            Drug2TotalQTY,
            Drug3Name,
            Drug3TotalQTY,
            Drug4Name,
            Drug4TotalQTY,
            Drug5Name,
            Drug5TotalQTY,
            Drug6Name,
            Drug6TotalQTY,
            Drug7Name,
            Drug7TotalQTY,
            Drug8Name,
            Drug8TotalQTY,
            Drug9Name,
            Drug9TotalQTY,
            Drug10Name,
            Drug10TotalQTY,
            Insulin1Name,
            Insulin1CartQTY,
            Insulin2Name,
            Insulin2CartQTY
        FROM 
            `prescribeddrugs`
        WHERE 
            (Drug1Name = @drugName 
            OR Drug2Name = @drugName 
            OR Drug3Name = @drugName 
            OR Drug4Name = @drugName 
            OR Drug5Name = @drugName 
            OR Drug6Name = @drugName 
            OR Drug7Name = @drugName 
            OR Drug8Name = @drugName 
            OR Drug9Name = @drugName 
            OR Drug10Name = @drugName 
            OR Insulin1Name = @drugName 
            OR Insulin2Name = @drugName) 
            AND Timestamp BETWEEN @startDate AND @endDate
        UNION
        SELECT 
            ID,
            Date,
            Name,
            ICNo,
            DateCollection,
            DateSeeDoctor,
            Timestamp,
            Drug1Name,
            Drug1TotalQTY,
            Drug2Name,
            Drug2TotalQTY,
            Drug3Name,
            Drug3TotalQTY,
            Drug4Name,
            Drug4TotalQTY,
            Drug5Name,
            Drug5TotalQTY,
            Drug6Name,
            Drug6TotalQTY,
            Drug7Name,
            Drug7TotalQTY,
            Drug8Name,
            Drug8TotalQTY,
            Drug9Name,
            Drug9TotalQTY,
            Drug10Name,
            Drug10TotalQTY,
            Insulin1Name,
            Insulin1CartQTY,
            Insulin2Name,
            Insulin2CartQTY
        FROM 
            `prescribeddrugshistory`
        WHERE 
            (Drug1Name = @drugName 
            OR Drug2Name = @drugName 
            OR Drug3Name = @drugName 
            OR Drug4Name = @drugName 
            OR Drug5Name = @drugName 
            OR Drug6Name = @drugName 
            OR Drug7Name = @drugName 
            OR Drug8Name = @drugName 
            OR Drug9Name = @drugName 
            OR Drug10Name = @drugName 
            OR Insulin1Name = @drugName 
            OR Insulin2Name = @drugName) 
            AND Timestamp BETWEEN @startDate AND @endDate", conn)

        cmd.Parameters.AddWithValue("@drugName", cbDrugQty.Text)
        cmd.Parameters.AddWithValue("@startDate", startDate)
        cmd.Parameters.AddWithValue("@endDate", endDate)

        Try
            conn.Open()
            dr = cmd.ExecuteReader

            While dr.Read()
                count += 1
                Dim currentQty As Integer = 0

                ' Determine which drug matches and get its total quantity
                If dr.Item("Drug1Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug1TotalQTY")
                ElseIf dr.Item("Drug2Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug2TotalQTY")
                ElseIf dr.Item("Drug3Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug3TotalQTY")
                ElseIf dr.Item("Drug4Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug4TotalQTY")
                ElseIf dr.Item("Drug5Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug5TotalQTY")
                ElseIf dr.Item("Drug6Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug6TotalQTY")
                ElseIf dr.Item("Drug7Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug7TotalQTY")
                ElseIf dr.Item("Drug8Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug8TotalQTY")
                ElseIf dr.Item("Drug9Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug9TotalQTY")
                ElseIf dr.Item("Drug10Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Drug10TotalQTY")
                ElseIf dr.Item("Insulin1Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Insulin1CartQTY")
                ElseIf dr.Item("Insulin2Name") = cbDrugQty.Text Then
                    currentQty = dr.Item("Insulin2CartQTY")
                End If

                ' Add the current quantity to the total
                totalQty += currentQty

                ' Add the main data and the matching drug's total quantity to the dgvDrugQty DataGridView
                dgvDrugQty.Rows.Add(count, dr.Item("ID"), dr.Item("Date"), dr.Item("Name"), dr.Item("ICNo"), currentQty, dr.Item("DateCollection"), dr.Item("DateSeeDoctor"), dr.Item("Timestamp"))
            End While

            ' Update the label with the total quantity
            lblDrugTotalQty.Text = totalQty

            dr.Close()
        Catch ex As Exception
            ' MsgBox(ex.Message) ' Handle exceptions if necessary
        Finally
            dr.Dispose()
            conn.Close()
        End Try
    End Sub

    Public Sub loadAllDrugsQtyDGV()
        dgvAllDrugsQty.Rows.Clear() ' Clear the dgvAllDrugsQty DataGridView

        Dim dt As New DataTable
        Dim startDate As String = dtpAllDrugsQty1.Value.ToString("yyyy-MM-dd HH:mm:ss")
        Dim endDate As String = dtpAllDrugsQty2.Value.ToString("yyyy-MM-dd HH:mm:ss")

        Dim cmd As New MySqlCommand("SELECT 
    DrugName, 
    SUM(TotalQTY) AS TotalQuantity 
    FROM (
        SELECT Drug1Name AS DrugName, Drug1TotalQTY AS TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug2Name, Drug2TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug3Name, Drug3TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug4Name, Drug4TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug5Name, Drug5TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug6Name, Drug6TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug7Name, Drug7TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug8Name, Drug8TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug9Name, Drug9TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug10Name, Drug10TotalQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Insulin1Name, Insulin1CartQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Insulin2Name, Insulin2CartQTY FROM prescribeddrugs WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug1Name, Drug1TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug2Name, Drug2TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug3Name, Drug3TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug4Name, Drug4TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug5Name, Drug5TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug6Name, Drug6TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug7Name, Drug7TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug8Name, Drug8TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug9Name, Drug9TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Drug10Name, Drug10TotalQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Insulin1Name, Insulin1CartQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
        UNION ALL
        SELECT Insulin2Name, Insulin2CartQTY FROM prescribeddrugshistory WHERE Timestamp BETWEEN @startDate AND @endDate
    ) AS AllDrugs
    GROUP BY DrugName", conn)

        cmd.Parameters.AddWithValue("@startDate", startDate)
        cmd.Parameters.AddWithValue("@endDate", endDate)

        Try
            conn.Open()
            dr = cmd.ExecuteReader

            While dr.Read()
                dgvAllDrugsQty.Rows.Add(dr.Item("DrugName"), dr.Item("TotalQuantity"))
            End While

            dr.Close()
        Catch ex As Exception
            ' MsgBox(ex.Message)
        Finally
            dr.Dispose()
            conn.Close()
        End Try
    End Sub

    Public Sub loadDatabaseDGV()
        dgvDateSelector.Rows.Clear()
        dgvPatientDrugHistory.Rows.Clear()
        dgvPatientInsulinHistory.Rows.Clear()
        'check if txtICDB is empty
        If txtICNoDB.Text = "" Then
            MsgBox("Please enter the IC No. at Search box")
            lblPatientNameDB.Text = ""
            Return
        End If


        Dim dt As New DataTable
        'SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752' UNION SELECT * FROM prescribeddrugs  WHERE ICNo = '111111-11-1115' and Timestamp = '2024-05-17 03:18:57.995752'
        'Dim cmd As New MySqlCommand("SELECT * FROM prescribeddrugs WHERE ICNo = '" & lblPrevSavedICNo.Text & "'", conn)
        Dim count As Integer = 0
        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtICNoDB.Text & "' UNION " & "SELECT * FROM `prescribeddrugshistory` WHERE ICNo = '" & txtICNoDB.Text & "'", conn)
        Try

            conn.Open()
            dr = cmd.ExecuteReader

            While dr.Read()
                count += 1
                dgvDateSelector.Rows.Add(count, dr.Item("ID"), dr.Item("Date"), dr.Item("DateCollection"), dr.Item("DateSeeDoctor"), dr.Item("Timestamp"))

            End While
            'timestamp = dgvDateSelector.CurrentRow.Cells(5).Value


            lblPatientNameDB.Text = dr.Item("Name")
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
        Dim cmd As New MySqlCommand(" SELECT * FROM `prescribeddrugs` WHERE ICNo = '" & txtICNoDB.Text & "'" & " AND " & "Timestamp = '" & outputTime & "'" & " UNION " & "SELECT * FROM `prescribeddrugshistory` WHERE ICNo = '" & txtICNoDB.Text & "'" & " AND " & "Timestamp ='" & outputTime & "' LIMIT 1", conn)
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

    Public Sub UpdateDataInDatabase()
        'Dim currentcelladd = dgvDateSelector.CurrentCellAddress

        Dim currentcell = dgvDateSelector.CurrentCell
        Dim timestamp As String = dgvDateSelector.CurrentRow.Cells(5).Value
        Dim inputTime As String = timestamp
        Dim format As String = "d/M/yyyy h:mm:ss tt"
        Dim provider As CultureInfo = CultureInfo.InvariantCulture
        Dim parsedTime As DateTime = DateTime.ParseExact(inputTime, format, provider)
        Dim outputTime As String = parsedTime.ToString("yyyy-M-dd HH:mm:ss")

        Dim ColumnIndex = dgvDateSelector.CurrentCell.ColumnIndex
        Dim RowIndex = dgvDateSelector.CurrentCell.RowIndex

        Try
            conn.Open()
            Dim cmd2 As New MySqlCommand("UPDATE `prescribeddrugs` SET 
                            `Drug1Name`=@Drug1Name,`Drug1Strength`=@Drug1Strength,`Drug1Unit`=@Drug1Unit,`Drug1Dose`=@Drug1Dose,`Drug1Freq`=@Drug1Freq,`Drug1Duration`=@Drug1Duration,`Drug1TotalQTY`=@Drug1TotalQTY,
                            `Drug2Name`=@Drug2Name,`Drug2Strength`=@Drug2Strength,`Drug2Unit`=@Drug2Unit,`Drug2Dose`=@Drug2Dose,`Drug2Freq`=@Drug2Freq,`Drug2Duration`=@Drug2Duration,`Drug2TotalQTY`=@Drug2TotalQTY,
                            `Drug3Name`=@Drug3Name,`Drug3Strength`=@Drug3Strength,`Drug3Unit`=@Drug3Unit,`Drug3Dose`=@Drug3Dose,`Drug3Freq`=@Drug3Freq,`Drug3Duration`=@Drug3Duration,`Drug3TotalQTY`=@Drug3TotalQTY,
                            `Drug4Name`=@Drug4Name,`Drug4Strength`=@Drug4Strength,`Drug4Unit`=@Drug4Unit,`Drug4Dose`=@Drug4Dose,`Drug4Freq`=@Drug4Freq,`Drug4Duration`=@Drug4Duration,`Drug4TotalQTY`=@Drug4TotalQTY,
                            `Drug5Name`=@Drug5Name,`Drug5Strength`=@Drug5Strength,`Drug5Unit`=@Drug5Unit,`Drug5Dose`=@Drug5Dose,`Drug5Freq`=@Drug5Freq,`Drug5Duration`=@Drug5Duration,`Drug5TotalQTY`=@Drug5TotalQTY,
                            `Drug6Name`=@Drug6Name,`Drug6Strength`=@Drug6Strength,`Drug6Unit`=@Drug6Unit,`Drug6Dose`=@Drug6Dose,`Drug6Freq`=@Drug6Freq,`Drug6Duration`=@Drug6Duration,`Drug6TotalQTY`=@Drug6TotalQTY,
                            `Drug7Name`=@Drug7Name,`Drug7Strength`=@Drug7Strength,`Drug7Unit`=@Drug7Unit,`Drug7Dose`=@Drug7Dose,`Drug7Freq`=@Drug7Freq,`Drug7Duration`=@Drug7Duration,`Drug7TotalQTY`=@Drug7TotalQTY,
                            `Drug8Name`=@Drug8Name,`Drug8Strength`=@Drug8Strength,`Drug8Unit`=@Drug8Unit,`Drug8Dose`=@Drug8Dose,`Drug8Freq`=@Drug8Freq,`Drug8Duration`=@Drug8Duration,`Drug8TotalQTY`=@Drug8TotalQTY,
                            `Drug9Name`=@Drug9Name,`Drug9Strength`=@Drug9Strength,`Drug9Unit`=@Drug9Unit,`Drug9Dose`=@Drug9Dose,`Drug9Freq`=@Drug9Freq,`Drug9Duration`=@Drug9Duration,`Drug9TotalQTY`=@Drug9TotalQTY,
                            `Drug10Name`=@Drug10Name,`Drug10Strength`=@Drug10Strength,`Drug10Unit`=@Drug10Unit,`Drug10Dose`=@Drug10Dose,`Drug10Freq`=@Drug10Freq,`Drug10Duration`=@Drug10Duration,`Drug10TotalQTY`=@Drug10TotalQTY,
                            `Insulin1Name`=@Insulin1Name,`Insulin1Strength`=@Insulin1Strength,`Insulin1Unit`=@Insulin1Unit,`Insulin1MorDose`=@Insulin1MorDose,`Insulin1NoonDose`=@Insulin1NoonDose,`Insulin1AfternoonDose`=@Insulin1AfternoonDose,`Insulin1NightDose`=@Insulin1NightDose,`Insulin1Freq`=@Insulin1Freq,`Insulin1Duration`=@Insulin1Duration,`Insulin1TotalDose`=@Insulin1TotalDose,`Insulin1POM`=@Insulin1POM,`Insulin1CartQTY`=@Insulin1CartQTY,
                            `Insulin2Name`=@Insulin2Name,`Insulin2Strength`=@Insulin2Strength,`Insulin2Unit`=@Insulin2Unit,`Insulin2MorDose`=@Insulin2MorDose,`Insulin2NoonDose`=@Insulin2NoonDose,`Insulin2AfternoonDose`=@Insulin2AfternoonDose,`Insulin2NightDose`=@Insulin2NightDose,`Insulin2Freq`=@Insulin2Freq,`Insulin2Duration`=@Insulin2Duration,`Insulin2TotalDose`=@Insulin2TotalDose,`Insulin2POM`=@Insulin2POM,`Insulin2CartQTY`=@Insulin2CartQTY,
                            Timestamp=Timestamp WHERE `ICNo`=@ICNo AND `Timestamp`=@Timestamp", conn)
            cmd2.Parameters.Clear()
            'cmd2.Parameters.AddWithValue("@Name", txtPatientName.Text)
            cmd2.Parameters.AddWithValue("@ICNo", txtICNoDB.Text)
            'cmd2.Parameters.AddWithValue("@Date", dtpDateSaved.Text)
            'cmd2.Parameters.AddWithValue("@DateCollection", dtpDateCollection.Text)
            'cmd2.Parameters.AddWithValue("@DateSeeDoctor", dtpDateSeeDoctor.Text)
            cmd2.Parameters.AddWithValue("@Timestamp", outputTime)
            'Drug 1

            cmd2.Parameters.AddWithValue("@Drug1Name", dgvPatientDrugHistory.Rows(0).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug1Strength", dgvPatientDrugHistory.Rows(0).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug1Unit", dgvPatientDrugHistory.Rows(0).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug1Dose", dgvPatientDrugHistory.Rows(0).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug1Freq", dgvPatientDrugHistory.Rows(0).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug1Duration", dgvPatientDrugHistory.Rows(0).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug1TotalQTY", dgvPatientDrugHistory.Rows(0).Cells(7).Value)
            'Drug 2
            cmd2.Parameters.AddWithValue("@Drug2Name", dgvPatientDrugHistory.Rows(1).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug2Strength", dgvPatientDrugHistory.Rows(1).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug2Unit", dgvPatientDrugHistory.Rows(1).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug2Dose", dgvPatientDrugHistory.Rows(1).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug2Freq", dgvPatientDrugHistory.Rows(1).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug2Duration", dgvPatientDrugHistory.Rows(1).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug2TotalQTY", dgvPatientDrugHistory.Rows(1).Cells(7).Value)
            'Drug 3
            cmd2.Parameters.AddWithValue("@Drug3Name", dgvPatientDrugHistory.Rows(2).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug3Strength", dgvPatientDrugHistory.Rows(2).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug3Unit", dgvPatientDrugHistory.Rows(2).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug3Dose", dgvPatientDrugHistory.Rows(2).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug3Freq", dgvPatientDrugHistory.Rows(2).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug3Duration", dgvPatientDrugHistory.Rows(2).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug3TotalQTY", dgvPatientDrugHistory.Rows(2).Cells(7).Value)
            'Drug 4
            cmd2.Parameters.AddWithValue("@Drug4Name", dgvPatientDrugHistory.Rows(3).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug4Strength", dgvPatientDrugHistory.Rows(3).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug4Unit", dgvPatientDrugHistory.Rows(3).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug4Dose", dgvPatientDrugHistory.Rows(3).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug4Freq", dgvPatientDrugHistory.Rows(3).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug4Duration", dgvPatientDrugHistory.Rows(3).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug4TotalQTY", dgvPatientDrugHistory.Rows(3).Cells(7).Value)
            'Drug 5
            cmd2.Parameters.AddWithValue("@Drug5Name", dgvPatientDrugHistory.Rows(4).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug5Strength", dgvPatientDrugHistory.Rows(4).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug5Unit", dgvPatientDrugHistory.Rows(4).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug5Dose", dgvPatientDrugHistory.Rows(4).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug5Freq", dgvPatientDrugHistory.Rows(4).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug5Duration", dgvPatientDrugHistory.Rows(4).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug5TotalQTY", dgvPatientDrugHistory.Rows(4).Cells(7).Value)
            'Drug 6
            cmd2.Parameters.AddWithValue("@Drug6Name", dgvPatientDrugHistory.Rows(5).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug6Strength", dgvPatientDrugHistory.Rows(5).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug6Unit", dgvPatientDrugHistory.Rows(5).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug6Dose", dgvPatientDrugHistory.Rows(5).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug6Freq", dgvPatientDrugHistory.Rows(5).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug6Duration", dgvPatientDrugHistory.Rows(5).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug6TotalQTY", dgvPatientDrugHistory.Rows(5).Cells(7).Value)
            'Drug 7
            cmd2.Parameters.AddWithValue("@Drug7Name", dgvPatientDrugHistory.Rows(6).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug7Strength", dgvPatientDrugHistory.Rows(6).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug7Unit", dgvPatientDrugHistory.Rows(6).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug7Dose", dgvPatientDrugHistory.Rows(6).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug7Freq", dgvPatientDrugHistory.Rows(6).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug7Duration", dgvPatientDrugHistory.Rows(6).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug7TotalQTY", dgvPatientDrugHistory.Rows(6).Cells(7).Value)
            'Drug 8
            cmd2.Parameters.AddWithValue("@Drug8Name", dgvPatientDrugHistory.Rows(7).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug8Strength", dgvPatientDrugHistory.Rows(7).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug8Unit", dgvPatientDrugHistory.Rows(7).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug8Dose", dgvPatientDrugHistory.Rows(7).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug8Freq", dgvPatientDrugHistory.Rows(7).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug8Duration", dgvPatientDrugHistory.Rows(7).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug8TotalQTY", dgvPatientDrugHistory.Rows(7).Cells(7).Value)
            'Drug 9
            cmd2.Parameters.AddWithValue("@Drug9Name", dgvPatientDrugHistory.Rows(8).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug9Strength", dgvPatientDrugHistory.Rows(8).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug9Unit", dgvPatientDrugHistory.Rows(8).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug9Dose", dgvPatientDrugHistory.Rows(8).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug9Freq", dgvPatientDrugHistory.Rows(8).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug9Duration", dgvPatientDrugHistory.Rows(8).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug9TotalQTY", dgvPatientDrugHistory.Rows(8).Cells(7).Value)
            'Drug 10
            cmd2.Parameters.AddWithValue("@Drug10Name", dgvPatientDrugHistory.Rows(9).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug10Strength", dgvPatientDrugHistory.Rows(9).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug10Unit", dgvPatientDrugHistory.Rows(9).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug10Dose", dgvPatientDrugHistory.Rows(9).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug10Freq", dgvPatientDrugHistory.Rows(9).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug10Duration", dgvPatientDrugHistory.Rows(9).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug10TotalQTY", dgvPatientDrugHistory.Rows(9).Cells(7).Value)
            'Insulin 1
            cmd2.Parameters.AddWithValue("@Insulin1Name", dgvPatientInsulinHistory.Rows(0).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Strength", dgvPatientInsulinHistory.Rows(0).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Unit", dgvPatientInsulinHistory.Rows(0).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Insulin1MorDose", dgvPatientInsulinHistory.Rows(0).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Insulin1NoonDose", dgvPatientInsulinHistory.Rows(0).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Insulin1AfternoonDose", dgvPatientInsulinHistory.Rows(0).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Insulin1NightDose", dgvPatientInsulinHistory.Rows(0).Cells(7).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Freq", dgvPatientInsulinHistory.Rows(0).Cells(8).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Duration", dgvPatientInsulinHistory.Rows(0).Cells(9).Value)
            cmd2.Parameters.AddWithValue("@Insulin1TotalDose", dgvPatientInsulinHistory.Rows(0).Cells(10).Value)
            cmd2.Parameters.AddWithValue("@Insulin1POM", dgvPatientInsulinHistory.Rows(0).Cells(11).Value)
            cmd2.Parameters.AddWithValue("@Insulin1CartQTY", dgvPatientInsulinHistory.Rows(0).Cells(12).Value)
            'Insulin 2
            cmd2.Parameters.AddWithValue("@Insulin2Name", dgvPatientInsulinHistory.Rows(1).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Strength", dgvPatientInsulinHistory.Rows(1).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Unit", dgvPatientInsulinHistory.Rows(1).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Insulin2MorDose", dgvPatientInsulinHistory.Rows(1).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Insulin2NoonDose", dgvPatientInsulinHistory.Rows(1).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Insulin2AfternoonDose", dgvPatientInsulinHistory.Rows(1).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Insulin2NightDose", dgvPatientInsulinHistory.Rows(1).Cells(7).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Freq", dgvPatientInsulinHistory.Rows(1).Cells(8).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Duration", dgvPatientInsulinHistory.Rows(1).Cells(9).Value)
            cmd2.Parameters.AddWithValue("@Insulin2TotalDose", dgvPatientInsulinHistory.Rows(1).Cells(10).Value)
            cmd2.Parameters.AddWithValue("@Insulin2POM", dgvPatientInsulinHistory.Rows(1).Cells(11).Value)
            cmd2.Parameters.AddWithValue("@Insulin2CartQTY", dgvPatientInsulinHistory.Rows(1).Cells(12).Value)

            'cmd2.ExecuteNonQuery()
            Dim i As Integer = cmd2.ExecuteNonQuery()
            If i > 0 Then
                MessageBox.Show("Updated Successfully.")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        Try
            conn.Open()
            Dim cmd2 As New MySqlCommand("UPDATE `prescribeddrugshistory` SET 
                            `Drug1Name`=@Drug1Name,`Drug1Strength`=@Drug1Strength,`Drug1Unit`=@Drug1Unit,`Drug1Dose`=@Drug1Dose,`Drug1Freq`=@Drug1Freq,`Drug1Duration`=@Drug1Duration,`Drug1TotalQTY`=@Drug1TotalQTY,
                            `Drug2Name`=@Drug2Name,`Drug2Strength`=@Drug2Strength,`Drug2Unit`=@Drug2Unit,`Drug2Dose`=@Drug2Dose,`Drug2Freq`=@Drug2Freq,`Drug2Duration`=@Drug2Duration,`Drug2TotalQTY`=@Drug2TotalQTY,
                            `Drug3Name`=@Drug3Name,`Drug3Strength`=@Drug3Strength,`Drug3Unit`=@Drug3Unit,`Drug3Dose`=@Drug3Dose,`Drug3Freq`=@Drug3Freq,`Drug3Duration`=@Drug3Duration,`Drug3TotalQTY`=@Drug3TotalQTY,
                            `Drug4Name`=@Drug4Name,`Drug4Strength`=@Drug4Strength,`Drug4Unit`=@Drug4Unit,`Drug4Dose`=@Drug4Dose,`Drug4Freq`=@Drug4Freq,`Drug4Duration`=@Drug4Duration,`Drug4TotalQTY`=@Drug4TotalQTY,
                            `Drug5Name`=@Drug5Name,`Drug5Strength`=@Drug5Strength,`Drug5Unit`=@Drug5Unit,`Drug5Dose`=@Drug5Dose,`Drug5Freq`=@Drug5Freq,`Drug5Duration`=@Drug5Duration,`Drug5TotalQTY`=@Drug5TotalQTY,
                            `Drug6Name`=@Drug6Name,`Drug6Strength`=@Drug6Strength,`Drug6Unit`=@Drug6Unit,`Drug6Dose`=@Drug6Dose,`Drug6Freq`=@Drug6Freq,`Drug6Duration`=@Drug6Duration,`Drug6TotalQTY`=@Drug6TotalQTY,
                            `Drug7Name`=@Drug7Name,`Drug7Strength`=@Drug7Strength,`Drug7Unit`=@Drug7Unit,`Drug7Dose`=@Drug7Dose,`Drug7Freq`=@Drug7Freq,`Drug7Duration`=@Drug7Duration,`Drug7TotalQTY`=@Drug7TotalQTY,
                            `Drug8Name`=@Drug8Name,`Drug8Strength`=@Drug8Strength,`Drug8Unit`=@Drug8Unit,`Drug8Dose`=@Drug8Dose,`Drug8Freq`=@Drug8Freq,`Drug8Duration`=@Drug8Duration,`Drug8TotalQTY`=@Drug8TotalQTY,
                            `Drug9Name`=@Drug9Name,`Drug9Strength`=@Drug9Strength,`Drug9Unit`=@Drug9Unit,`Drug9Dose`=@Drug9Dose,`Drug9Freq`=@Drug9Freq,`Drug9Duration`=@Drug9Duration,`Drug9TotalQTY`=@Drug9TotalQTY,
                            `Drug10Name`=@Drug10Name,`Drug10Strength`=@Drug10Strength,`Drug10Unit`=@Drug10Unit,`Drug10Dose`=@Drug10Dose,`Drug10Freq`=@Drug10Freq,`Drug10Duration`=@Drug10Duration,`Drug10TotalQTY`=@Drug10TotalQTY,
                            `Insulin1Name`=@Insulin1Name,`Insulin1Strength`=@Insulin1Strength,`Insulin1Unit`=@Insulin1Unit,`Insulin1MorDose`=@Insulin1MorDose,`Insulin1NoonDose`=@Insulin1NoonDose,`Insulin1AfternoonDose`=@Insulin1AfternoonDose,`Insulin1NightDose`=@Insulin1NightDose,`Insulin1Freq`=@Insulin1Freq,`Insulin1Duration`=@Insulin1Duration,`Insulin1TotalDose`=@Insulin1TotalDose,`Insulin1POM`=@Insulin1POM,`Insulin1CartQTY`=@Insulin1CartQTY,
                            `Insulin2Name`=@Insulin2Name,`Insulin2Strength`=@Insulin2Strength,`Insulin2Unit`=@Insulin2Unit,`Insulin2MorDose`=@Insulin2MorDose,`Insulin2NoonDose`=@Insulin2NoonDose,`Insulin2AfternoonDose`=@Insulin2AfternoonDose,`Insulin2NightDose`=@Insulin2NightDose,`Insulin2Freq`=@Insulin2Freq,`Insulin2Duration`=@Insulin2Duration,`Insulin2TotalDose`=@Insulin2TotalDose,`Insulin2POM`=@Insulin2POM,`Insulin2CartQTY`=@Insulin2CartQTY,
                            Timestamp=Timestamp WHERE `ICNo`=@ICNo AND `Timestamp`=@Timestamp", conn)
            cmd2.Parameters.Clear()
            'cmd2.Parameters.AddWithValue("@Name", txtPatientName.Text)
            cmd2.Parameters.AddWithValue("@ICNo", txtICNoDB.Text)
            'cmd2.Parameters.AddWithValue("@Date", dtpDateSaved.Text)
            'cmd2.Parameters.AddWithValue("@DateCollection", dtpDateCollection.Text)
            'cmd2.Parameters.AddWithValue("@DateSeeDoctor", dtpDateSeeDoctor.Text)
            cmd2.Parameters.AddWithValue("@Timestamp", outputTime)
            'Drug 1

            cmd2.Parameters.AddWithValue("@Drug1Name", dgvPatientDrugHistory.Rows(0).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug1Strength", dgvPatientDrugHistory.Rows(0).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug1Unit", dgvPatientDrugHistory.Rows(0).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug1Dose", dgvPatientDrugHistory.Rows(0).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug1Freq", dgvPatientDrugHistory.Rows(0).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug1Duration", dgvPatientDrugHistory.Rows(0).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug1TotalQTY", dgvPatientDrugHistory.Rows(0).Cells(7).Value)
            'Drug 2
            cmd2.Parameters.AddWithValue("@Drug2Name", dgvPatientDrugHistory.Rows(1).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug2Strength", dgvPatientDrugHistory.Rows(1).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug2Unit", dgvPatientDrugHistory.Rows(1).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug2Dose", dgvPatientDrugHistory.Rows(1).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug2Freq", dgvPatientDrugHistory.Rows(1).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug2Duration", dgvPatientDrugHistory.Rows(1).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug2TotalQTY", dgvPatientDrugHistory.Rows(1).Cells(7).Value)
            'Drug 3
            cmd2.Parameters.AddWithValue("@Drug3Name", dgvPatientDrugHistory.Rows(2).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug3Strength", dgvPatientDrugHistory.Rows(2).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug3Unit", dgvPatientDrugHistory.Rows(2).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug3Dose", dgvPatientDrugHistory.Rows(2).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug3Freq", dgvPatientDrugHistory.Rows(2).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug3Duration", dgvPatientDrugHistory.Rows(2).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug3TotalQTY", dgvPatientDrugHistory.Rows(2).Cells(7).Value)
            'Drug 4
            cmd2.Parameters.AddWithValue("@Drug4Name", dgvPatientDrugHistory.Rows(3).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug4Strength", dgvPatientDrugHistory.Rows(3).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug4Unit", dgvPatientDrugHistory.Rows(3).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug4Dose", dgvPatientDrugHistory.Rows(3).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug4Freq", dgvPatientDrugHistory.Rows(3).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug4Duration", dgvPatientDrugHistory.Rows(3).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug4TotalQTY", dgvPatientDrugHistory.Rows(3).Cells(7).Value)
            'Drug 5
            cmd2.Parameters.AddWithValue("@Drug5Name", dgvPatientDrugHistory.Rows(4).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug5Strength", dgvPatientDrugHistory.Rows(4).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug5Unit", dgvPatientDrugHistory.Rows(4).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug5Dose", dgvPatientDrugHistory.Rows(4).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug5Freq", dgvPatientDrugHistory.Rows(4).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug5Duration", dgvPatientDrugHistory.Rows(4).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug5TotalQTY", dgvPatientDrugHistory.Rows(4).Cells(7).Value)
            'Drug 6
            cmd2.Parameters.AddWithValue("@Drug6Name", dgvPatientDrugHistory.Rows(5).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug6Strength", dgvPatientDrugHistory.Rows(5).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug6Unit", dgvPatientDrugHistory.Rows(5).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug6Dose", dgvPatientDrugHistory.Rows(5).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug6Freq", dgvPatientDrugHistory.Rows(5).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug6Duration", dgvPatientDrugHistory.Rows(5).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug6TotalQTY", dgvPatientDrugHistory.Rows(5).Cells(7).Value)
            'Drug 7
            cmd2.Parameters.AddWithValue("@Drug7Name", dgvPatientDrugHistory.Rows(6).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug7Strength", dgvPatientDrugHistory.Rows(6).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug7Unit", dgvPatientDrugHistory.Rows(6).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug7Dose", dgvPatientDrugHistory.Rows(6).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug7Freq", dgvPatientDrugHistory.Rows(6).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug7Duration", dgvPatientDrugHistory.Rows(6).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug7TotalQTY", dgvPatientDrugHistory.Rows(6).Cells(7).Value)
            'Drug 8
            cmd2.Parameters.AddWithValue("@Drug8Name", dgvPatientDrugHistory.Rows(7).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug8Strength", dgvPatientDrugHistory.Rows(7).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug8Unit", dgvPatientDrugHistory.Rows(7).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug8Dose", dgvPatientDrugHistory.Rows(7).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug8Freq", dgvPatientDrugHistory.Rows(7).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug8Duration", dgvPatientDrugHistory.Rows(7).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug8TotalQTY", dgvPatientDrugHistory.Rows(7).Cells(7).Value)
            'Drug 9
            cmd2.Parameters.AddWithValue("@Drug9Name", dgvPatientDrugHistory.Rows(8).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug9Strength", dgvPatientDrugHistory.Rows(8).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug9Unit", dgvPatientDrugHistory.Rows(8).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug9Dose", dgvPatientDrugHistory.Rows(8).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug9Freq", dgvPatientDrugHistory.Rows(8).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug9Duration", dgvPatientDrugHistory.Rows(8).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug9TotalQTY", dgvPatientDrugHistory.Rows(8).Cells(7).Value)
            'Drug 10
            cmd2.Parameters.AddWithValue("@Drug10Name", dgvPatientDrugHistory.Rows(9).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Drug10Strength", dgvPatientDrugHistory.Rows(9).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Drug10Unit", dgvPatientDrugHistory.Rows(9).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Drug10Dose", dgvPatientDrugHistory.Rows(9).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Drug10Freq", dgvPatientDrugHistory.Rows(9).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Drug10Duration", dgvPatientDrugHistory.Rows(9).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Drug10TotalQTY", dgvPatientDrugHistory.Rows(9).Cells(7).Value)
            'Insulin 1
            cmd2.Parameters.AddWithValue("@Insulin1Name", dgvPatientInsulinHistory.Rows(0).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Strength", dgvPatientInsulinHistory.Rows(0).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Unit", dgvPatientInsulinHistory.Rows(0).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Insulin1MorDose", dgvPatientInsulinHistory.Rows(0).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Insulin1NoonDose", dgvPatientInsulinHistory.Rows(0).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Insulin1AfternoonDose", dgvPatientInsulinHistory.Rows(0).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Insulin1NightDose", dgvPatientInsulinHistory.Rows(0).Cells(7).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Freq", dgvPatientInsulinHistory.Rows(0).Cells(8).Value)
            cmd2.Parameters.AddWithValue("@Insulin1Duration", dgvPatientInsulinHistory.Rows(0).Cells(9).Value)
            cmd2.Parameters.AddWithValue("@Insulin1TotalDose", dgvPatientInsulinHistory.Rows(0).Cells(10).Value)
            cmd2.Parameters.AddWithValue("@Insulin1POM", dgvPatientInsulinHistory.Rows(0).Cells(11).Value)
            cmd2.Parameters.AddWithValue("@Insulin1CartQTY", dgvPatientInsulinHistory.Rows(0).Cells(12).Value)
            'Insulin 2
            cmd2.Parameters.AddWithValue("@Insulin2Name", dgvPatientInsulinHistory.Rows(1).Cells(1).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Strength", dgvPatientInsulinHistory.Rows(1).Cells(2).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Unit", dgvPatientInsulinHistory.Rows(1).Cells(3).Value)
            cmd2.Parameters.AddWithValue("@Insulin2MorDose", dgvPatientInsulinHistory.Rows(1).Cells(4).Value)
            cmd2.Parameters.AddWithValue("@Insulin2NoonDose", dgvPatientInsulinHistory.Rows(1).Cells(5).Value)
            cmd2.Parameters.AddWithValue("@Insulin2AfternoonDose", dgvPatientInsulinHistory.Rows(1).Cells(6).Value)
            cmd2.Parameters.AddWithValue("@Insulin2NightDose", dgvPatientInsulinHistory.Rows(1).Cells(7).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Freq", dgvPatientInsulinHistory.Rows(1).Cells(8).Value)
            cmd2.Parameters.AddWithValue("@Insulin2Duration", dgvPatientInsulinHistory.Rows(1).Cells(9).Value)
            cmd2.Parameters.AddWithValue("@Insulin2TotalDose", dgvPatientInsulinHistory.Rows(1).Cells(10).Value)
            cmd2.Parameters.AddWithValue("@Insulin2POM", dgvPatientInsulinHistory.Rows(1).Cells(11).Value)
            cmd2.Parameters.AddWithValue("@Insulin2CartQTY", dgvPatientInsulinHistory.Rows(1).Cells(12).Value)

            'cmd2.ExecuteNonQuery()
            Dim i As Integer = cmd2.ExecuteNonQuery()
            If i > 0 Then
                MessageBox.Show("Updated Successfully.")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try

        loadDatabaseDGV()
        'dgvDateSelector.CurrentCell = currentcell
        dgvDateSelector.CurrentCell = dgvDateSelector(ColumnIndex, RowIndex)
        loadDatabaseDGV2()


    End Sub





    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDateSelector.CellClick
        'txtDrugName.Text = dgvDateSelector.CurrentRow.Cells(0).Value
        'txtStrength.Text = dgvDateSelector.CurrentRow.Cells(1).Value
        'txtUnit.Text = dgvDateSelector.CurrentRow.Cells(2).Value
        'txtDosageForm.Text = dgvDateSelector.CurrentRow.Cells(3).Value
        'Dim timestamp As String
        'timestamp = dgvDateSelector.CurrentRow.Cells(4).Value
        'MsgBox(timestamp)
        loadDatabaseDGV2()

    End Sub

    Private Sub btnSearchDB_Click(sender As Object, e As EventArgs) Handles btnSearchDB.Click
        loadDatabaseDGV()
    End Sub

    Private Sub txtICNoDB_TextChanged(sender As Object, e As EventArgs) Handles txtICNoDB.TextChanged
        lblPatientNameDB.Text = ""
        ' Get the current position of the cursor
        Dim currentPosition As Integer = txtICNoDB.SelectionStart

        ' Remove the existing hyphens to simplify processing
        Dim text As String = txtICNoDB.Text.Replace("-", String.Empty)

        ' Reinsert the hyphens at the 7th and 9th positions if the text is long enough
        If text.Length > 6 Then
            text = text.Insert(6, "-")
        End If
        If text.Length > 9 Then
            text = text.Insert(9, "-")
        End If

        ' Update the TextBox text without triggering another TextChanged event
        RemoveHandler txtICNoDB.TextChanged, AddressOf txtICNoDB_TextChanged
        txtICNoDB.Text = text
        AddHandler txtICNoDB.TextChanged, AddressOf txtICNoDB_TextChanged

        ' Adjust the cursor position
        If currentPosition <= 6 Then
            txtICNoDB.SelectionStart = currentPosition
        ElseIf currentPosition > 6 AndAlso currentPosition <= 8 Then
            txtICNoDB.SelectionStart = currentPosition + 1
        ElseIf currentPosition > 8 Then
            txtICNoDB.SelectionStart = currentPosition + 2
        End If
    End Sub
    Private Sub txtICNoDB_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtICNoDB.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) = 8 Then
            disableTextChangedDB = True
        End If
    End Sub

    Private Sub dtpRecordsDateSelector_ValueChanged(sender As Object, e As EventArgs) Handles dtpRecordsDateSelector.ValueChanged
        If notyetinitialize Then
            Return
        Else
            loadDGVRecords()
        End If


    End Sub

    Private Sub cboxAutoClear_CheckedChanged(sender As Object, e As EventArgs) Handles cboxAutoClear.CheckedChanged
        Dim AutoClear As Boolean = My.Settings.AutoClear
        Dim AutoClearNew As Boolean
        AutoClearNew = cboxAutoClear.Checked
        My.Settings.AutoClear = AutoClearNew
        My.Settings.Save()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim emailAddress As String = "kenpeacezx@gmail.com"
        Dim subject As String = "Inquiry on the Pharmacy Management System / Github"

        ' Create the mailto link
        Dim mailtoLink As String = $"mailto:{emailAddress}?subject={Uri.EscapeDataString(subject)}"

        ' Open the default email client with the mailto link
        Process.Start(New ProcessStartInfo(mailtoLink) With {.UseShellExecute = True})
    End Sub

    Private Sub dtpRecordsDateSelectorEnd_ValueChanged(sender As Object, e As EventArgs) Handles dtpRecordsDateSelectorEnd.ValueChanged
        If notyetinitialize Then
            Return
        Else
            loadDGVRecords()
        End If
    End Sub

    Private Sub btnDoseGuide_Click(sender As Object, e As EventArgs) Handles btnDoseGuide.Click
        Form3.Show()
    End Sub

    Private Sub btnDeleteRecord_Click(sender As Object, e As EventArgs) Handles btnDeleteRecord.Click
        deleteRecordRow()
    End Sub

    Public Sub deleteRecordRow()
        Select Case MsgBox("Do you want to Delete the Selected Row?", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes
                'Delete button at Database Tab to Delete Database Row Selected at DataGridView Table
                Try


                    conn.Open()

                    Dim cmd As New MySqlCommand("DELETE FROM `records` WHERE `ID`=@ID", conn)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@ID", dgvRecords.CurrentRow.Cells(1).Value)


                    Dim i = cmd.ExecuteNonQuery
                    If i > 0 Then
                        MsgBox("Successfully Deleted.")
                        conn.Close()

                        loadDGVRecords()

                    Else
                        MsgBox("Delete Failed.")
                    End If
                Catch ex As Exception
                    conn.Close()
                    MsgBox("Please select the row first.")
                End Try
            Case MsgBoxResult.Cancel
                Return
            Case MsgBoxResult.No
                Return
        End Select
    End Sub

    Private Sub btnChangeRecord_Click(sender As Object, e As EventArgs) Handles btnChangeRecord.Click
        If dgvRecords.ReadOnly Then
            lblEditModeRecords.Text = "EDIT MODE, You may edit any cells and press Save"
            dgvRecords.ReadOnly = False
            btnChangeRecord.Text = "Save"
            btnChangeRecord.BackColor = Color.LightGreen
        ElseIf btnChangeRecord.Text = "Save" Then
            changeRecords()
            lblEditModeRecords.Text = ""
            dgvRecords.ReadOnly = True
            btnChangeRecord.Text = "Edit"
            btnChangeRecord.BackColor = Color.Transparent
        End If


    End Sub

    Public Sub changeRecords()

        Try
            conn.Open()
            Dim ID As String
            '"UPDATE `drugtable` SET `Strength`=@Strength,`Unit`=@Unit,`DosageForm`=@DosageForm,`PrescriberCategory`=@PrescriberCategory,`Remark`=@Remark WHERE `DrugName`=@DrugName", conn




            For Each row As DataGridViewRow In dgvRecords.Rows
                ID = row.Cells("IDRecords").Value
                Dim cmd As New MySqlCommand("UPDATE `records` SET `Name`=@Name,`ICNo`=@ICNo,`NewPatient`=@NewPatient,`IOU`=@IOU,`NoOfItems`=@NoOfItems,`DateCollection`=@DateCollection,`DateSeeDoctor`=@DateSeeDoctor WHERE ID='" & ID & "'", conn)
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@Name", row.Cells("NameOfPatient").Value)
                cmd.Parameters.AddWithValue("@ICNo", row.Cells("ICNoOfPatient").Value)
                cmd.Parameters.AddWithValue("@NewPatient", row.Cells("NewPatient").Value)
                cmd.Parameters.AddWithValue("@IOU", row.Cells("IOU").Value)
                cmd.Parameters.AddWithValue("@NoOfItems", row.Cells("NoOfItems").Value)
                cmd.Parameters.AddWithValue("@DateCollection", row.Cells("DateCollectionRecord").Value)
                cmd.Parameters.AddWithValue("@DateSeeDoctor", row.Cells("DateSeeDoctorRecord").Value)
                cmd.ExecuteNonQuery()
            Next

            MsgBox("Successfully Updated.")
            conn.Close()

            loadDGVRecords()




        Catch ex As Exception
            conn.Close()
            MsgBox("Update Failed.")
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnDeleteAllprescribeddrugs_Click(sender As Object, e As EventArgs) Handles btnDeleteAllPatientRecords.Click
        deleteAllPatientRecords()

    End Sub

    Public Sub deleteAllPatientRecords()
        Select Case MsgBox("Do you want to Delete All the Patient Records? This operation cannot be undone. This will restart the application after completed.", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes
                'Delete button at Database Tab to Delete Database Row Selected at DataGridView Table
                Try



                    'Dim cmd As New MySqlCommand("TRUNCATE TABLE prescribeddrugs;TRUNCATE TABLE prescribeddrugshistory;TRUNCATE TABLE records", conn)
                    Dim cmd As New MySqlCommand("TRUNCATE TABLE prescribeddrugs", conn)
                    cmd.Parameters.Clear()

                    conn.Open()
                    cmd.ExecuteNonQuery()
                    conn.Close()

                    Dim cmd2 As New MySqlCommand("TRUNCATE TABLE prescribeddrugshistory", conn)
                    cmd2.Parameters.Clear()

                    conn.Open()
                    cmd2.ExecuteNonQuery()
                    conn.Close()

                    Dim cmd3 As New MySqlCommand("TRUNCATE TABLE records", conn)
                    cmd3.Parameters.Clear()

                    conn.Open()
                    cmd3.ExecuteNonQuery()
                    conn.Close()

                    Application.Restart()


                Catch ex As Exception
                    conn.Close()
                    MsgBox(ex.Message)
                End Try
            Case MsgBoxResult.Cancel
                Return
            Case MsgBoxResult.No
                Return
        End Select
    End Sub

    Public Sub DevMode()
        If My.Settings.DevMode = True Then
            PictureBox1.Visible = True
        Else
            PictureBox1.Visible = False
        End If
    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click
        If My.Settings.DevMode = False Then
            My.Settings.DevMode = True
            PictureBox1.Visible = True
            My.Settings.Save()

        Else
            My.Settings.DevMode = False
            PictureBox1.Visible = False
            My.Settings.Save()
        End If
    End Sub

    Private Sub btnEditPatientDB_Click(sender As Object, e As EventArgs) Handles btnEditPatientDB.Click
        If dgvPatientDrugHistory.ReadOnly And dgvPatientInsulinHistory.ReadOnly Then
            lblEditModeDB.Text = "EDIT MODE"
            lblEditModeDB.BackColor = Color.LightGreen
            dgvDateSelector.Enabled = False
            dgvPatientDrugHistory.ReadOnly = False
            dgvPatientInsulinHistory.ReadOnly = False
            txtICNoDB.ReadOnly = True
            btnSearchDB.Enabled = False
            btnEditPatientDB.Text = "Save"
            btnEditPatientDB.BackColor = Color.LightGreen
        ElseIf btnEditPatientDB.Text = "Save" Then
            UpdateDataInDatabase()
            dgvDateSelector.Enabled = True
            lblEditModeDB.Text = ""
            lblEditModeDB.BackColor = Color.Transparent
            dgvPatientDrugHistory.ReadOnly = True
            dgvPatientInsulinHistory.ReadOnly = True
            txtICNoDB.ReadOnly = False
            btnSearchDB.Enabled = True
            btnEditPatientDB.Text = "Edit"
            btnEditPatientDB.BackColor = Color.Transparent
        End If
    End Sub

    Public autotext As DataGridViewTextBoxEditingControl
    Public coltitle As String

    Private Sub dgvPatientDrugHistory_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvPatientDrugHistory.EditingControlShowing
        autotext = TryCast(e.Control, DataGridViewTextBoxEditingControl)
        If coltitle.Equals("Drug Name") Then
            autocomplete("drugname")
        End If
    End Sub

    Private Sub dgvPatientInsulinHistory_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvPatientInsulinHistory.EditingControlShowing
        autotext = TryCast(e.Control, DataGridViewTextBoxEditingControl)
        If coltitle.Equals("Insulin Name") Then
            autocomplete("insulinname")
        End If
    End Sub

    Public Sub autocomplete(ByVal type As String)
        'If autotext IsNot Nothing Then


        If type = "drugname" Then
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT DrugName FROM drugtable WHERE DrugName not like '%Insulin%'", conn)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            Dim col As New AutoCompleteStringCollection

            da.Fill(dt)
            For i = 0 To dt.Rows.Count - 1
                col.Add(dt.Rows(i)("DrugName").ToString())
                'drugnamedb = dt.Rows(i)("DrugName").ToString()
                'cbDrug1.Items.Add(drugnamedb)
            Next
            conn.Close()
            da.Dispose()

            autotext.AutoCompleteSource = AutoCompleteSource.CustomSource
            autotext.AutoCompleteCustomSource = col
            autotext.AutoCompleteMode = AutoCompleteMode.Suggest
        End If

        If type = "insulinname" Then
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT DrugName FROM drugtable WHERE DrugName like '%Insulin%'", conn)
            Dim dt As New DataTable
            Dim da As New MySqlDataAdapter(cmd)
            Dim col As New AutoCompleteStringCollection

            da.Fill(dt)
            For i = 0 To dt.Rows.Count - 1
                col.Add(dt.Rows(i)("DrugName").ToString())
                'drugnamedb = dt.Rows(i)("DrugName").ToString()
                'cbDrug1.Items.Add(drugnamedb)
            Next
            conn.Close()
            da.Dispose()

            autotext.AutoCompleteSource = AutoCompleteSource.CustomSource
            autotext.AutoCompleteCustomSource = col
            autotext.AutoCompleteMode = AutoCompleteMode.Suggest
        End If
        'End If
    End Sub

    Private Sub dgvPatientDrugHistory_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvPatientDrugHistory.CellBeginEdit
        coltitle = dgvPatientDrugHistory.Columns(dgvPatientDrugHistory.SelectedCells(0).ColumnIndex).HeaderText
    End Sub

    Private Sub dgvPatientInsulinHistory_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvPatientInsulinHistory.CellBeginEdit
        coltitle = dgvPatientInsulinHistory.Columns(dgvPatientInsulinHistory.SelectedCells(0).ColumnIndex).HeaderText
    End Sub

    Private Sub btnDeletePatientDB_Click(sender As Object, e As EventArgs) Handles btnDeletePatientDB.Click
        deletePatientDB()
    End Sub

    Public Sub deletePatientDB()
        Select Case MsgBox("Do you want to Delete the Selected Row? This operation cannot be undone.", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes

                Dim timestamp As String = dgvDateSelector.CurrentRow.Cells(5).Value
                Dim inputTime As String = timestamp
                Dim format As String = "d/M/yyyy h:mm:ss tt"
                Dim provider As CultureInfo = CultureInfo.InvariantCulture
                Dim parsedTime As DateTime = DateTime.ParseExact(inputTime, format, provider)
                Dim outputTime As String = parsedTime.ToString("yyyy-M-dd HH:mm:ss")
                'Delete button at Database Tab to Delete Database Row Selected at DataGridView Table
                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand("DELETE FROM `prescribeddrugs` WHERE `ICNo`=@ICNo AND `Timestamp`=@Timestamp", conn)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@ICNo", txtICNoDB.Text)
                    cmd.Parameters.AddWithValue("@Timestamp", outputTime)

                    Dim i = cmd.ExecuteNonQuery
                    If i > 0 Then
                        MsgBox("Lastest Patient Data Successfully Deleted.")

                    Else
                        'MsgBox("Delete Failed.")
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                    'MsgBox("Please select the row first.")
                Finally
                    conn.Close()
                End Try

                Try
                    conn.Open()
                    Dim cmd As New MySqlCommand("DELETE FROM `prescribeddrugshistory` WHERE `ICNo`=@ICNo AND `Timestamp`=@Timestamp", conn)
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@ICNo", txtICNoDB.Text)
                    cmd.Parameters.AddWithValue("@Timestamp", outputTime)

                    Dim i = cmd.ExecuteNonQuery
                    If i > 0 Then
                        MsgBox("History Patient Data Successfully Deleted.")


                    Else
                        'MsgBox("Delete Failed.")
                    End If

                Catch ex As Exception
                    'MsgBox("Please select the row first.")
                Finally
                    conn.Close()
                    dgvDateSelector.Refresh()
                    dgvPatientDrugHistory.Refresh()
                    dgvPatientInsulinHistory.Refresh()
                    loadDatabaseDGV()
                    loaddatafromdb()
                    loadDBDataforPatientInfo()


                End Try
            Case MsgBoxResult.Cancel
                Return
            Case MsgBoxResult.No
                Return
        End Select
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Update the label with the current time every tick (every second)
        lblTime.Text = DateTime.Now.ToString("h:mm:ss tt")
        lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy")
    End Sub

    Private Sub cboxDisplayDateTime_CheckedChanged(sender As Object, e As EventArgs) Handles cboxDisplayDateTime.CheckedChanged
        Dim DisplayTime As Boolean = My.Settings.EnableTime
        Dim DisplayTimeNew As Boolean
        DisplayTimeNew = cboxDisplayDateTime.Checked
        My.Settings.EnableTime = DisplayTimeNew
        My.Settings.Save()
        DisplayDateTime()
    End Sub

    Public Sub DisplayDateTime()
        lblTime.Text = DateTime.Now.ToString("h:mm:ss tt")
        lblDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy")
        If cboxDisplayDateTime.Checked Then
            lblTime.Visible = True
            lblDate.Visible = True
            Timer1.Enabled = True
        End If
        If cboxDisplayDateTime.Checked = False Then
            lblTime.Visible = False
            lblDate.Visible = False
            Timer1.Enabled = False
        End If

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim url As String = "https://paypal.me/kenpeacez?country.x=MY&locale.x=en_US"

        Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
    End Sub



    Private Sub dtpDateSeeDoctor_ValueChanged(sender As Object, e As EventArgs) Handles dtpDateSeeDoctor.ValueChanged
        ' Calculate the difference in days between the two dates
        Dim duration As TimeSpan = dtpDateSeeDoctor.Value - dtpDateSaved.Value
        ' Add 1 to the number of days and concatenate with "days"
        txtDurationDoctor.Text = (duration.Days + 1).ToString() & " days"
    End Sub

    Private Async Sub btnGetQuote_Click(sender As Object, e As EventArgs) Handles btnGetQuote.Click
        Dim quote As String = Await GetMotivationalQuote()
        If Not String.IsNullOrEmpty(quote) Then
            lblQuote.Text = quote
        Else
            lblQuote.Text = "No quote found."
        End If
    End Sub

    Private Async Function GetMotivationalQuote() As Task(Of String)
        Dim quote As String = ""
        Try
            ' Create an HttpClient instance
            Dim client As New HttpClient()

            ' Make a GET request to the ZenQuotes API
            Dim response As HttpResponseMessage = Await client.GetAsync("https://zenquotes.io/api/random")

            If response.IsSuccessStatusCode Then
                ' Read the response content
                Dim jsonString As String = Await response.Content.ReadAsStringAsync()

                ' Parse the JSON response
                Dim jsonArray As JArray = JArray.Parse(jsonString)
                Dim quoteText As String = jsonArray(0)("q").ToString()
                Dim author As String = jsonArray(0)("a").ToString()

                ' Construct the final quote and format it to break lines
                quote = $"""{InsertNewLines(quoteText, 90)}""" & vbCrLf & $"- {author}"
            Else
                quote = "Failed to fetch the quote."
            End If
        Catch ex As Exception
            'MessageBox.Show("Error fetching quote: " & ex.Message)
            quote = "Failed to fetch the quote." & "Error: " & ex.Message
        End Try
        Return quote
    End Function

    ' Function to insert new lines after a certain number of characters
    Private Function InsertNewLines(text As String, maxLineLength As Integer) As String
        Dim result As New System.Text.StringBuilder()

        Dim words As String() = text.Split(" "c)
        Dim currentLineLength As Integer = 0

        For Each word In words
            ' If adding the next word exceeds the line limit, add a new line
            If currentLineLength + word.Length + 1 > maxLineLength Then
                result.AppendLine()
                currentLineLength = 0
            End If

            ' Append the word to the result
            result.Append(word & " ")
            currentLineLength += word.Length + 1 ' Account for space
        Next

        Return result.ToString().TrimEnd() ' Remove any trailing spaces or line breaks
    End Function

    Private Sub btnCopyDurationtoDoctor_Click(sender As Object, e As EventArgs) Handles btnCopyDurationtoDoctor.Click
        dtpDateSeeDoctor.Value = dtpDateCollection.Value
    End Sub

    Private Sub cbDrugQty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrugQty.SelectedIndexChanged
        loadDrugQtyDGV()
    End Sub

    Private Sub dtpDrugQty2_ValueChanged(sender As Object, e As EventArgs) Handles dtpDrugQty2.ValueChanged
        If notyetinitialize Then
            Return
        Else
            loadDrugQtyDGV()
        End If
    End Sub

    Private Sub dtpDrugQty1_ValueChanged(sender As Object, e As EventArgs) Handles dtpDrugQty1.ValueChanged
        If notyetinitialize Then
            Return
        Else
            loadDrugQtyDGV()
        End If

    End Sub

    Private Sub dtpAllDrugsQty1_ValueChanged(sender As Object, e As EventArgs) Handles dtpAllDrugsQty1.ValueChanged
        If notyetinitialize Then
            Return
        Else
            dtpAllDrugsQty2.MinDate = dtpAllDrugsQty1.Value
            loadAllDrugsQtyDGV()
        End If
    End Sub

    Private Sub dtpAllDrugsQty2_ValueChanged(sender As Object, e As EventArgs) Handles dtpAllDrugsQty2.ValueChanged
        If notyetinitialize Then
            Return
        Else
            loadAllDrugsQtyDGV()
        End If
    End Sub

    Private Sub btnOpenStatistics_Click(sender As Object, e As EventArgs) Handles btnOpenStatistics.Click
        Form4.Show()
    End Sub

End Class
