
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Globalization
Imports System.Runtime.InteropServices


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


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Form Initialization / First Load
        InitializeAll()

        'First Function, Check for DB Connection Status
        checkDB()
        If DBStatus = False Then
            Return 'Exit from Function due to Database Initialization error
        End If

        'Second Funtion, Tabulate Data from Database to Drug Tab Table 
        DGV_Load()

        'Third Function
        loadDBDataforPatientInfo()

        'Fourth Function
        loaddatafromdb()
        loadInsulindatafromdb()
        'Fiveth Function
        loadLogDGV()
        loadDGVRecords()

        notyetinitialize = False
    End Sub

    Public Sub InitializeAll()

        dtpRecordsDateSelector.Value = Today
        dtpRecordsDateSelector.MaxDate = Today
        dtpRecordsDateSelectorEnd.MaxDate = Today
        dtpRecordsDateSelectorEnd.Value = Today

        cboxEnablePrintPDF.Checked = My.Settings.EnablePrintAfterSave
        cboxAutoClear.Checked = My.Settings.AutoClear

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
    End Sub

    Private Sub SetandSaveDBSettings()
        Dim sender As Object
        Dim e As EventArgs
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
        Form1_Load(sender, e)
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
    Private Sub checkDB()
        Try
            conn.Open()
            pbrDatabaseConnection.Value = 100
            DBStatus = True

        Catch ex As MySql.Data.MySqlClient.MySqlException
            MessageBox.Show(ex.Message)
            pbrDatabaseConnection.Value = 0
            DBStatus = False
            Return

        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub GetDefaultPrinterName()
        Dim printerSettings As New PrinterSettings()
        Dim defaultPrinterName As String = printerSettings.PrinterName

        stlbPrinterName.Text = defaultPrinterName
        lblDefaultPrinterAtSetting.Text = defaultPrinterName
    End Sub
    Public Sub printPreview()
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
            PPD.ShowDialog()
            currentPage = 1

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
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

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
        'Dim fontsize3 As Single = Math.Round(8 * RationalizedScale)
        Dim f8 As New Font("Arial", fontsize1, FontStyle.Italic)
        Dim f8a As New Font("Arial", fontsize1, FontStyle.Bold)
        Dim f8b As New Font("Arial", fontsize2, FontStyle.Bold)
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

        Dim Rect9 As New Rectangle(Math.Round(201 * ScaleWidth), Math.Round(167 * ScaleHeight), Math.Round(70 * ScaleWidth), Math.Round(12 * ScaleHeight)) 'Jumlah



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
                    Else
                        Dim consumedose = txtDoseD1.Text / lblStrD1.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If



                    'Check for Blank Selection of Drug


                    e.Graphics.DrawString(cbDrug1.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD1 & consumedosefinal & ConsumeUnitD1 & txtFreqD1.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD1, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD1.Text, f8a, Brushes.Black, Rect9, left)

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
                    Else
                        Dim consumedose = txtDoseD2.Text / lblStrD2.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug2.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD2 & consumedosefinal & ConsumeUnitD2 & txtFreqD2.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD2, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD2.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD3.Text / lblStrD3.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug3.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD3 & consumedosefinal & ConsumeUnitD3 & txtFreqD3.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD3, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD3.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD4.Text / lblStrD4.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug4.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD4 & consumedosefinal & ConsumeUnitD4 & txtFreqD4.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD4, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD4.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD5.Text / lblStrD5.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug5.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD5 & consumedosefinal & ConsumeUnitD5 & txtFreqD5.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD5, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD5.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD6.Text / lblStrD6.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug6.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD6 & consumedosefinal & ConsumeUnitD6 & txtFreqD6.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD6, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD6.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD7.Text / lblStrD7.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug7.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD7 & consumedosefinal & ConsumeUnitD7 & txtFreqD7.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD7, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD7.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD8.Text / lblStrD8.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug8.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD8 & consumedosefinal & ConsumeUnitD8 & txtFreqD8.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD8, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD8.Text, f8a, Brushes.Black, Rect9, left)
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
                    Else
                        Dim consumedose = txtDoseD9.Text / lblStrD9.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    e.Graphics.DrawString(cbDrug9.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD9 & consumedosefinal & ConsumeUnitD9 & txtFreqD9.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD9, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD9.Text, f8a, Brushes.Black, Rect9, left)
                Case 10
                    Dim consumedosefinal As String = ""
                    If ConsumeMethodD10.Contains("Minum ") And ConsumeUnitD10.Contains("ml") Then
                        Dim consumedose = txtDoseD10.Text / lblStrD10.Text
                        consumedosefinal = Math.Round(consumedose, 1)
                    Else
                        Dim consumedose = txtDoseD10.Text / lblStrD10.Text
                        consumedosefinal = ConvertToFraction(consumedose)
                    End If

                    'MsgBox("Drug Items: " & NoOfItemsRecord)
                    e.Graphics.DrawString(cbDrug10.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD10 & consumedosefinal & ConsumeUnitD10 & txtFreqD10.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD10, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD10.Text, f8a, Brushes.Black, Rect9, left)

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


            Dim Rect9a As New Rectangle(Math.Round(170 * ScaleWidth), Math.Round(168 * ScaleHeight), Math.Round(110 * ScaleWidth), Math.Round(15 * ScaleHeight)) 'Jumlah
            'Insulin 1
            Dim combinedwords As String
            Dim comamorning As String = " "
            Dim comanoon As String = " "
            Dim comaafternoon As String = " "
            Dim unitpagi As String = " unit pagi"
            Dim unittghari As String = " unit tengahari"
            Dim unitpetang As String = " unit petang"
            Dim unitmalam As String = " unit malam"

            Dim In1MorDose = txtIn1MorDose.Text
            Dim In1NoonDose = txtIn1NoonDose.Text
            Dim In1AfterNoonDose = txtIn1AfterNoonDose.Text
            Dim In1NightDose = txtIn1NightDose.Text

            If In1MorDose <> "" Then
                If In1NoonDose <> "" Then
                    comamorning = ", "
                End If
                If In1AfterNoonDose <> "" Then
                    comamorning = ", "
                End If
                If In1NightDose <> "" Then
                    comamorning = ", "
                End If

            End If
            If In1NoonDose <> "" Then
                If In1AfterNoonDose <> "" Then
                    comanoon = ", " & vbNewLine
                End If
                If In1NightDose <> "" Then
                    comanoon = ", " & vbNewLine
                End If
            End If
            If In1AfterNoonDose <> "" Then
                If In1NightDose <> "" Then
                    comaafternoon = ", "
                End If
            End If
            unitpagi = unitpagi & comamorning
            unittghari = unittghari & comanoon
            unitpetang = unitpetang & comaafternoon

            combinedwords = "Suntik " & In1MorDose & unitpagi & In1NoonDose & unittghari & In1AfterNoonDose & unitpetang & In1NightDose & unitmalam

            If In1MorDose = "" Then
                unitpagi = Nothing
                In1MorDose = Nothing
                combinedwords = "Suntik " & In1NoonDose & unittghari & In1AfterNoonDose & unitpetang & In1NightDose & unitmalam
            End If
            If In1NoonDose = "" Then
                unittghari = Nothing
                In1NoonDose = Nothing
                combinedwords = "Suntik " & In1MorDose & unitpagi & In1AfterNoonDose & unitpetang & In1NightDose & unitmalam
            End If
            If In1AfterNoonDose = "" Then
                unitpetang = Nothing
                In1AfterNoonDose = Nothing
                combinedwords = "Suntik " & In1MorDose & unitpagi & In1NoonDose & unittghari & In1NightDose & unitmalam
            End If
            If In1NightDose = "" Then
                unitmalam = Nothing
                In1NightDose = Nothing
                combinedwords = "Suntik " & In1MorDose & unitpagi & In1NoonDose & unittghari & In1AfterNoonDose & unitpetang
            End If
            'Insulin 2

            Dim combinedwords2 As String
            Dim comamorning2 As String = " "
            Dim comanoon2 As String = " "
            Dim comaafternoon2 As String = " "
            Dim unitpagi2 As String = " unit pagi"
            Dim unittghari2 As String = " unit tengahari"
            Dim unitpetang2 As String = " unit petang"
            Dim unitmalam2 As String = " unit malam"

            Dim In2MorDose = txtIn2MorDose.Text
            Dim In2NoonDose = txtIn2NoonDose.Text
            Dim In2AfterNoonDose = txtIn2AfterNoonDose.Text
            Dim In2NightDose = txtIn2NightDose.Text

            If In2MorDose <> "" Then
                If In2NoonDose <> "" Then
                    comamorning2 = ", "
                End If
                If In2AfterNoonDose <> "" Then
                    comamorning2 = ", "
                End If
                If In2NightDose <> "" Then
                    comamorning2 = ", "
                End If

            End If
            If In2NoonDose <> "" Then
                If In2AfterNoonDose <> "" Then
                    comanoon2 = ", " & vbNewLine
                End If
                If In2NightDose <> "" Then
                    comanoon2 = ", " & vbNewLine
                End If
            End If
            If In2AfterNoonDose <> "" Then
                If In2NightDose <> "" Then
                    comaafternoon2 = ", "
                End If
            End If
            unitpagi2 = unitpagi2 & comamorning2
            unittghari2 = unittghari2 & comanoon2
            unitpetang2 = unitpetang2 & comaafternoon2

            combinedwords2 = "Suntik " & In2MorDose & unitpagi2 & In2NoonDose & unittghari2 & In2AfterNoonDose & unitpetang2 & In2NightDose & unitmalam2

            If In2MorDose = "" Then
                unitpagi2 = Nothing
                In2MorDose = Nothing
                combinedwords2 = "Suntik " & In2NoonDose & unittghari2 & In2AfterNoonDose & unitpetang2 & In2NightDose & unitmalam2
            End If
            If In2NoonDose = "" Then
                unittghari2 = Nothing
                In2NoonDose = Nothing
                combinedwords2 = "Suntik " & In2MorDose & unitpagi2 & In2AfterNoonDose & unitpetang2 & In2NightDose & unitmalam2
            End If
            If In2AfterNoonDose = "" Then
                unitpetang2 = Nothing
                In2AfterNoonDose = Nothing
                combinedwords2 = "Suntik " & In2MorDose & unitpagi2 & In2NoonDose & unittghari2 & In2NightDose & unitmalam2
            End If
            If In2NightDose = "" Then
                unitmalam2 = Nothing
                In2NightDose = Nothing
                combinedwords2 = "Suntik " & In2MorDose & unitpagi2 & In2NoonDose & unittghari2 & In2AfterNoonDose & unitpetang2
            End If




            e.Graphics.DrawRectangle(Pens.Black, Rect1a)
            e.Graphics.DrawRectangle(Pens.Black, Rect2a)
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

                Case 2

                    e.Graphics.DrawString(cbInsulin2.Text, f8a, Brushes.Black, Rect4a, centre)
                    e.Graphics.DrawString(combinedwords2, f8b, Brushes.Black, Rect6a, centre)
                    e.Graphics.DrawString(RemarkIn2, f8a, Brushes.Black, Rect8a, centre)
                    e.Graphics.DrawString("Jumlah Katrij: " & txtIn2CartQTY.Text, f8a, Brushes.Black, Rect9a, left)

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
    Public Sub checkforItemsToCount()
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
    End Sub
    Public Sub printInsulin()
        Dim LabelHeight As Double = (50 - 2) / 25.4 * 100
        Dim LabelWidth As Double = (80 - 2) / 25.4 * 100

        Dim ScaleHeight As Double = ((txtLabelHeight.Text - 2) / 25.4 * 100) / LabelHeight
        Dim ScaleWidth As Double = ((txtLabelWidth.Text - 2) / 25.4 * 100) / LabelWidth

        Dim LabelHeightScaled As Double = LabelHeight * ScaleHeight
        Dim LabelWidthScaled As Double = LabelWidth * ScaleWidth

        PrintDocInsulin.DefaultPageSettings.PaperSize = New PaperSize("Label Size", LabelHeightScaled, LabelWidthScaled) 'width, height
        PrintDocInsulin.DefaultPageSettings.Landscape = True


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
        PPD.ShowDialog()
        currentPageInsulin = 1
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



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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

        Try
            'Patient Name validation
            If chboxNoICNumber.Checked = False Then
                If stPatientName = "" Then
                    MsgBox("Error. Name cannot be empty")
                    Return
                End If
                'Patient IC Validation
                If stIC.Length < 14 Then
                    MsgBox("Error. IC No. must be 12 digits")
                    Return
                ElseIf Regex.IsMatch(stIC, ICRegexPattern) = False Then
                    MsgBox("Error. IC No. Regex Formatting is Invalid. Please check and try again")
                    Return
                End If
            End If

            'Patient Drug Prescription Validation
            If cbDrug1.SelectedIndex >= 0 Then
                If txtDoseD1.Text = "" Or txtFreqD1.Text = "" Or txtDurationD1.Text = "" Or txtQTYD1.Text = "" Or CDbl(txtQTYD1.Text) = 0 Then
                    MsgBox("Drug 1 Error input")
                    Return
                End If
            End If
            If cbDrug2.SelectedIndex >= 0 Then
                If txtDoseD2.Text = "" Or txtFreqD2.Text = "" Or txtDurationD2.Text = "" Or txtQTYD2.Text = "" Or CDbl(txtQTYD2.Text) = 0 Then
                    MsgBox("Drug 2 Error input")
                    Return
                End If
            End If
            If cbDrug3.SelectedIndex >= 0 Then
                If txtDoseD3.Text = "" Or txtFreqD3.Text = "" Or txtDurationD3.Text = "" Or txtQTYD3.Text = "" Or CDbl(txtQTYD3.Text) = 0 Then
                    MsgBox("Drug 3 Error input")
                    Return
                End If
            End If

            If cbDrug4.SelectedIndex >= 0 Then
                If txtDoseD4.Text = "" Or txtFreqD4.Text = "" Or txtDurationD4.Text = "" Or txtQTYD4.Text = "" Or CDbl(txtQTYD4.Text) = 0 Then
                    MsgBox("Drug 4 Error input")
                    Return
                End If
            End If
            If cbDrug5.SelectedIndex >= 0 Then
                If txtDoseD5.Text = "" Or txtFreqD5.Text = "" Or txtDurationD5.Text = "" Or txtQTYD5.Text = "" Or CDbl(txtQTYD5.Text) = 0 Then
                    MsgBox("Drug 5 Error input")
                    Return
                End If

            End If
            If cbDrug6.SelectedIndex >= 0 Then
                If txtDoseD6.Text = "" Or txtFreqD6.Text = "" Or txtDurationD6.Text = "" Or txtQTYD6.Text = "" Or CDbl(txtQTYD6.Text) = 0 Then
                    MsgBox("Drug 6 Error input")
                    Return
                End If

            End If
            If cbDrug7.SelectedIndex >= 0 Then
                If txtDoseD7.Text = "" Or txtFreqD7.Text = "" Or txtDurationD7.Text = "" Or txtQTYD7.Text = "" Or CDbl(txtQTYD7.Text) = 0 Then
                    MsgBox("Drug 7 Error input")
                    Return
                End If

            End If
            If cbDrug8.SelectedIndex >= 0 Then
                If txtDoseD8.Text = "" Or txtFreqD8.Text = "" Or txtDurationD8.Text = "" Or txtQTYD8.Text = "" Or CDbl(txtQTYD8.Text) = 0 Then
                    MsgBox("Drug 8 Error input")
                    Return
                End If

            End If
            If cbDrug9.SelectedIndex >= 0 Then
                If txtDoseD9.Text = "" Or txtFreqD9.Text = "" Or txtDurationD9.Text = "" Or txtQTYD9.Text = "" Or CDbl(txtQTYD9.Text) = 0 Then
                    MsgBox("Drug 9 Error input")
                    Return
                End If

            End If
            If cbDrug10.SelectedIndex >= 0 Then
                If txtDoseD10.Text = "" Or txtFreqD10.Text = "" Or txtDurationD10.Text = "" Or txtQTYD10.Text = "" Or CDbl(txtQTYD10.Text) = 0 Then
                    MsgBox("Drug 10 Error input")
                    Return
                End If
            End If

            'MsgBox("Saved data for " & stPatientName & ", IC No.: " & stIC)

            'For Testing validations, enable return
            'Return


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
                End If
                addRecordTab()
                MsgBox("Saved data for " & stPatientName & ", IC No.: " & stIC)
                stlbMainStatus.Text = "Saved Successfully for " & stPatientName & ", IC No: " & stIC
                loadLogDGV()
                loadDBDataforPatientInfo() 'Refresh IC Textbox Autocomplete
                chboxNoICNumber.Checked = False
                If cboxAutoClear.Checked Then
                    clearall()
                End If
            Else
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            conn.Close()
            'MsgBox(ex.Message)
            If ex.Message.Contains("Duplicate") Then
                Select Case MsgBox("Existing Patient. Do you want to overwrite with current data?", MsgBoxStyle.YesNo, "Confirmation")
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

                                        MsgBox("Old Data saved to History for " & stPatientName & ", IC No.: " & stIC)
                                        stlbMainStatus.Text = "Old Data Saved for " & stPatientName & ", IC No: " & stIC

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
                            `Insulin2Name`=@Insulin2Name,`Insulin2Strength`=@Insulin2Strength,`Insulin2Unit`=@Insulin2Unit,`Insulin2MorDose`=@Insulin2MorDose,`Insulin2NoonDose`=@Insulin2NoonDose,`Insulin2AfternoonDose`=@Insulin2AfternoonDose,`Insulin2NightDose`=@Insulin2NightDose,`Insulin2Freq`=@Insulin2Freq,`Insulin2Duration`=@Insulin2Duration,`Insulin2TotalDose`=@Insulin2TotalDose,`Insulin2POM`=@Insulin2POM,`Insulin2CartQTY`=@Insulin2CartQTY WHERE `ICNo`=@ICNo", conn)
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
                                End If
                                addRecordTab()
                                'MsgBox("Successfully Updated Data.")
                                MsgBox("Overwritten data for " & stPatientName & ", IC No.: " & stIC)
                                stlbMainStatus.Text = "Overwrite Successfully for " & stPatientName & ", IC No: " & stIC
                                loadLogDGV()
                                'loadDBDataforPatientInfo() 'Refresh IC Textbox Autocomplete
                                chboxNoICNumber.Checked = False
                                If cboxAutoClear.Checked Then
                                    clearall()
                                End If

                            End If
                        Catch exxx As Exception
                            MsgBox(exxx.Message)
                        End Try


                    Case MsgBoxResult.No
                        conn.Close()
                        Return
                End Select


            End If
            conn.Close()
        Finally

        End Try
    End Sub
    Public Sub addRecordTab()
        Dim newpatientBool As Integer = 0
        Dim IOUBool As Integer = 0
        'MsgBox("Drug Items : " & NoOfItemsRecord)
        'MsgBox("Insulin Items : " & NoOfItemsRecordInsulin)
        checkforItemsToCount()
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
        'Data Grid View Method to Get Data from MYSQL Database
        Dim count As Integer = 0
        'Parse the input date string
        dtpRecordsDateSelectorEnd.MinDate = dtpRecordsDateSelector.Value
        Dim originalDateString As String = dtpRecordsDateSelector.Value
        Dim parsedDate As DateTime = DateTime.Parse(originalDateString)
        Dim startdate As String = parsedDate.ToString("yyyy-MM-dd")

        Dim originalDateString2 As String = dtpRecordsDateSelectorEnd.Value
        Dim parsedDate2 As DateTime = DateTime.Parse(originalDateString2)
        Dim enddate As String = parsedDate2.ToString("yyyy-MM-dd")
        'Declare initial count variables for new patients, iou, no of items
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

    Public Sub Add()
        'Add button at Drugs Tab to Save Data entered in the Text Boxes
        Try
            If txtDrugName.Text = "" Then
                MsgBox("Error. Drug Name cannot be empty")
                Return
            End If

            conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `drugtable` (`DrugName`,`Strength`,`Unit`,`DosageForm`,`PrescriberCategory`,`DefaultMaxQTY`,`Remark`) VALUES (@DrugName,@Strength,@Unit,@DosageForm,@PrescriberCategory,@DefaultMaxQTY,@Remark)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DrugName", txtDrugName.Text)
            cmd.Parameters.AddWithValue("@Strength", CDbl(txtStrength.Text))
            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@DosageForm", txtDosageForm.Text)
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
        txtDosageForm.Clear()
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
        Dim sender As Object
        Dim e As EventArgs
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
            cmd.Parameters.AddWithValue("@DosageForm", txtDosageForm.Text)
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
        Add()
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
                txtDosageForm.Text = DataGridView1.CurrentRow.Cells(3).Value
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
                End If
            End While
            conn.Close()
        Catch ex As Exception

        End Try



    End Sub
    Public Sub loadInsulindatafromdb()
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub loaddatafromdb()
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

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
    Public Sub populatevaluesD1()

        Try

            If cbDrug1.Text Is "" Then
                lblStrD1.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug1.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD1.Text = dr.Item("Strength")
                lblUnitD1.Text = dr.Item("Unit")
                lblPreCatagoryD1.Text = dr.Item("PrescriberCategory")
                RemarkD1 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD1 = "Makan "
                    ConsumeUnitD1 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD1 = "Suntik "
                    ConsumeUnitD1 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD1 = "Minum "
                    ConsumeUnitD1 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD1 = "Kumur "
                    ConsumeUnitD1 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD1 = "Ambil "
                    ConsumeUnitD1 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD1 = "Minum "
                    ConsumeUnitD1 = " paket "
                ElseIf dr.Item("DosageForm") = "Packet" Then
                    ConsumeMethodD1 = "Minum "
                    ConsumeUnitD1 = " paket "
                End If

                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD1 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD1 = 0
                End If

            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD1.Text = ""
            lblUnitD1.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD2()

        Try

            If cbDrug2.Text Is "" Then
                lblStrD2.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug2.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD2.Text = dr.Item("Strength")
                lblUnitD2.Text = dr.Item("Unit")
                lblPreCatagoryD2.Text = dr.Item("PrescriberCategory")
                RemarkD2 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD2 = "Makan "
                    ConsumeUnitD2 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD2 = "Suntik "
                    ConsumeUnitD2 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD2 = "Minum "
                    ConsumeUnitD2 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD2 = "Kumur "
                    ConsumeUnitD2 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD2 = "Ambil "
                    ConsumeUnitD2 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD2 = "Minum "
                    ConsumeUnitD2 = " paket "
                End If

                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD2 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD2 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD2.Text = ""
            lblUnitD2.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub


    Public Sub PopulatevaluesD3()

        Try

            If cbDrug3.Text Is "" Then
                lblStrD3.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug3.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD3.Text = dr.Item("Strength")

                lblUnitD3.Text = dr.Item("Unit")
                lblPreCatagoryD3.Text = dr.Item("PrescriberCategory")
                RemarkD3 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD3 = "Makan "
                    ConsumeUnitD3 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD3 = "Suntik "
                    ConsumeUnitD3 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD3 = "Minum "
                    ConsumeUnitD3 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD3 = "Kumur "
                    ConsumeUnitD3 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD3 = "Ambil "
                    ConsumeUnitD3 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD3 = "Minum "
                    ConsumeUnitD3 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD3 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD3 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD3.Text = ""

            lblUnitD3.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD4()

        Try

            If cbDrug4.Text Is "" Then
                lblStrD4.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug4.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD4.Text = dr.Item("Strength")
                lblUnitD4.Text = dr.Item("Unit")
                lblPreCatagoryD4.Text = dr.Item("PrescriberCategory")
                RemarkD4 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD4 = "Makan "
                    ConsumeUnitD4 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD4 = "Suntik "
                    ConsumeUnitD4 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD4 = "Minum "
                    ConsumeUnitD4 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD4 = "Kumur "
                    ConsumeUnitD4 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD4 = "Ambil "
                    ConsumeUnitD4 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD4 = "Minum "
                    ConsumeUnitD4 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD4 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD4 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD4.Text = ""
            lblUnitD4.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD5()

        Try

            If cbDrug5.Text Is "" Then
                lblStrD5.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug5.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD5.Text = dr.Item("Strength")
                lblUnitD5.Text = dr.Item("Unit")
                lblPreCatagoryD5.Text = dr.Item("PrescriberCategory")
                RemarkD5 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD5 = "Makan "
                    ConsumeUnitD5 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD5 = "Suntik "
                    ConsumeUnitD5 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD5 = "Minum "
                    ConsumeUnitD5 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD5 = "Kumur "
                    ConsumeUnitD5 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD5 = "Ambil "
                    ConsumeUnitD5 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD5 = "Minum "
                    ConsumeUnitD5 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD5 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD5 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD5.Text = ""
            lblUnitD5.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD6()

        Try

            If cbDrug6.Text Is "" Then
                lblStrD6.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug6.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD6.Text = dr.Item("Strength")
                lblUnitD6.Text = dr.Item("Unit")
                lblPreCatagoryD6.Text = dr.Item("PrescriberCategory")
                RemarkD6 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD6 = "Makan "
                    ConsumeUnitD6 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD6 = "Suntik "
                    ConsumeUnitD6 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD6 = "Minum "
                    ConsumeUnitD6 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD6 = "Kumur "
                    ConsumeUnitD6 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD6 = "Ambil "
                    ConsumeUnitD6 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD6 = "Minum "
                    ConsumeUnitD6 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD6 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD6 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD6.Text = ""
            lblUnitD6.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD7()

        Try

            If cbDrug7.Text Is "" Then
                lblStrD7.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug7.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD7.Text = dr.Item("Strength")
                lblUnitD7.Text = dr.Item("Unit")
                lblPreCatagoryD7.Text = dr.Item("PrescriberCategory")
                RemarkD7 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD7 = "Makan "
                    ConsumeUnitD7 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD7 = "Suntik "
                    ConsumeUnitD7 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD7 = "Minum "
                    ConsumeUnitD7 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD7 = "Kumur "
                    ConsumeUnitD7 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD7 = "Ambil "
                    ConsumeUnitD7 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD7 = "Minum "
                    ConsumeUnitD7 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD7 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD7 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD7.Text = ""
            lblUnitD7.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD8()

        Try

            If cbDrug8.Text Is "" Then
                lblStrD8.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug8.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD8.Text = dr.Item("Strength")
                lblUnitD8.Text = dr.Item("Unit")
                lblPreCatagoryD8.Text = dr.Item("PrescriberCategory")
                RemarkD8 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD8 = "Makan "
                    ConsumeUnitD8 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD8 = "Suntik "
                    ConsumeUnitD8 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD8 = "Minum "
                    ConsumeUnitD8 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD8 = "Kumur "
                    ConsumeUnitD8 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD8 = "Ambil "
                    ConsumeUnitD8 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD8 = "Minum "
                    ConsumeUnitD8 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD8 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD8 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD8.Text = ""
            lblUnitD8.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD9()

        Try

            If cbDrug9.Text Is "" Then
                lblStrD9.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug9.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD9.Text = dr.Item("Strength")
                lblUnitD9.Text = dr.Item("Unit")
                lblPreCatagoryD9.Text = dr.Item("PrescriberCategory")
                RemarkD9 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD9 = "Makan "
                    ConsumeUnitD9 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD9 = "Suntik "
                    ConsumeUnitD9 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD9 = "Minum "
                    ConsumeUnitD9 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD9 = "Kumur "
                    ConsumeUnitD9 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD9 = "Ambil "
                    ConsumeUnitD9 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD9 = "Minum "
                    ConsumeUnitD9 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD9 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD9 = 0
                End If
            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD9.Text = ""
            lblUnitD9.Text = ""

        Finally
            dr.Dispose()
            conn.Close()
        End Try

    End Sub

    Public Sub populatevaluesD10()

        Try

            If cbDrug10.Text Is "" Then
                lblStrD10.Text = ""
                Return
            End If

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable WHERE DrugName = '" & cbDrug10.Text & "'", conn)
            dr = cmd.ExecuteReader
            While dr.Read()

                lblStrD10.Text = dr.Item("Strength")
                lblUnitD10.Text = dr.Item("Unit")
                lblPreCatagoryD10.Text = dr.Item("PrescriberCategory")
                RemarkD10 = dr.Item("Remark")
                If dr.Item("DosageForm") = "Tablet" Then
                    ConsumeMethodD10 = "Makan "
                    ConsumeUnitD10 = " biji "
                ElseIf dr.Item("DosageForm") = "Fridge Item" Then
                    ConsumeMethodD10 = "Suntik "
                    ConsumeUnitD10 = " unit "
                ElseIf dr.Item("DosageForm") = "Syrup" Then
                    ConsumeMethodD10 = "Minum "
                    ConsumeUnitD10 = " ml "
                ElseIf dr.Item("DosageForm") = "Gargle" Then
                    ConsumeMethodD10 = "Kumur "
                    ConsumeUnitD10 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD10 = "Ambil "
                    ConsumeUnitD10 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD10 = "Minum "
                    ConsumeUnitD10 = " paket "
                End If
                'check for max default QTY if present
                If dr.Item("DefaultMaxQTY") <> "" Then
                    DefaultMaxQTYD10 = CInt(dr.Item("DefaultMaxQTY"))
                Else DefaultMaxQTYD10 = 0
                End If

            End While
            dr.Close()


        Catch ex As Exception
            MsgBox(ex.Message)
            lblStrD10.Text = ""
            lblUnitD10.Text = ""

        Finally
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
    'Drug 1
    Private Sub cbDrug1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug1.SelectedIndexChanged
        cbDrug1.SelectionLength = cbDrug1.Text.Length
        unhighlightallcb()
        populatevaluesD1()
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
        populatevaluesD2()
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
        PopulatevaluesD3()
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
        populatevaluesD4()
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
        populatevaluesD5()
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
        populatevaluesD6()
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
        populatevaluesD7()
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
        populatevaluesD8()
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
        populatevaluesD9()
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
        populatevaluesD10()
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
    Public Sub checkforselecteddrugs()
        If cbDrug1.SelectedIndex >= 0 Then
            Drug1Selected = True
        Else Drug1Selected = False

        End If
        If cbDrug2.SelectedIndex >= 0 Then
            Drug2Selected = True
            cbDrug2.Enabled = True
        Else Drug2Selected = False
            cbDrug2.Enabled = False

        End If
        If cbDrug3.SelectedIndex >= 0 Then
            Drug3Selected = True
            cbDrug3.Enabled = True
        Else Drug3Selected = False
            cbDrug3.Enabled = False

        End If
        If cbDrug4.SelectedIndex >= 0 Then
            Drug4Selected = True
            cbDrug4.Enabled = True
        Else Drug4Selected = False
            cbDrug4.Enabled = False

        End If
        If cbDrug5.SelectedIndex >= 0 Then
            Drug5Selected = True
            cbDrug5.Enabled = True
        Else Drug5Selected = False
            cbDrug5.Enabled = False

        End If
        If cbDrug6.SelectedIndex >= 0 Then
            Drug6Selected = True
            cbDrug6.Enabled = True
        Else Drug6Selected = False
            cbDrug6.Enabled = False

        End If
        If cbDrug7.SelectedIndex >= 0 Then
            Drug7Selected = True
            cbDrug7.Enabled = True

        End If
        If cbDrug8.SelectedIndex >= 0 Then
            Drug8Selected = True
            cbDrug8.Enabled = True
        Else Drug8Selected = False
            cbDrug8.Enabled = False

        End If
        If cbDrug9.SelectedIndex >= 0 Then
            Drug9Selected = True
            cbDrug9.Enabled = True
        Else Drug9Selected = False
            cbDrug9.Enabled = False

        End If
        If cbDrug10.SelectedIndex >= 0 Then
            Drug10Selected = True
            cbDrug10.Enabled = True
        Else Drug10Selected = False
            cbDrug10.Enabled = False

        End If
        If cbInsulin1.SelectedIndex >= 0 Then
            Insulin1Selected = True
            cbInsulin1.Enabled = True
        Else Insulin1Selected = False
            'cbInsulin1.Enabled = False

        End If
        If cbInsulin2.SelectedIndex >= 0 Then
            Insulin2Selected = True
            cbInsulin2.Enabled = True
        Else Insulin2Selected = False
            cbInsulin2.Enabled = False

        End If
        'Enable next drug selection
        If cbDrug1.SelectedIndex >= 0 Then
            cbDrug2.Enabled = True
        End If
        If cbDrug2.SelectedIndex >= 0 Then
            cbDrug3.Enabled = True
        End If
        If cbDrug3.SelectedIndex >= 0 Then
            cbDrug4.Enabled = True
        End If
        If cbDrug4.SelectedIndex >= 0 Then
            cbDrug5.Enabled = True
        End If
        If cbDrug5.SelectedIndex >= 0 Then
            cbDrug6.Enabled = True
        End If
        If cbDrug6.SelectedIndex >= 0 Then
            cbDrug7.Enabled = True
        End If
        If cbDrug7.SelectedIndex >= 0 Then
            cbDrug8.Enabled = True
        End If
        If cbDrug8.SelectedIndex >= 0 Then
            cbDrug9.Enabled = True
        End If
        If cbDrug9.SelectedIndex >= 0 Then
            cbDrug10.Enabled = True
        End If
        If cbInsulin1.SelectedIndex >= 0 Then
            cbInsulin2.Enabled = True
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
        checkforselecteddrugs()
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
            If Not disableTextChanged Then
                If txtICNo.TextLength = 6 Then
                    ' Insert "-" at the position after the sixth and ninth character
                    txtICNo.Text = txtICNo.Text.Insert(6, "-")

                    ' Set the cursor position to after the "-" character
                    txtICNo.SelectionStart = txtICNo.TextLength
                End If
                If txtICNo.TextLength = 9 Then
                    ' Insert "-" at the position after the sixth and ninth character
                    txtICNo.Text = txtICNo.Text.Insert(9, "-")

                    ' Set the cursor position to after the "-" character
                    txtICNo.SelectionStart = txtICNo.TextLength

                End If

            End If
            If txtICNo.TextLength < 5 Then
                disableTextChanged = False
            End If
            If txtICNo.TextLength < 14 Then
                btnCheckICMySPR.Enabled = False
            End If
            If txtICNo.TextLength = 14 Then
                Try
                    checkICfromDB()
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
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
        If Asc(e.KeyChar) = 8 Then
            disableTextChanged = True
        End If
    End Sub



    Private Sub btnIOU_Click(sender As Object, e As EventArgs) Handles btnIOU.Click
        Form2.Show()
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
        End If
        If cboxEnablePrintPDF.Checked = False Then
            btnSave.Text = "Save only"
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

            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
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
            MsgBox(ex.Message)
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
        If Not disableTextChangedDB Then
            If txtICNoDB.TextLength = 6 Then
                ' Insert "-" at the position after the sixth and ninth character
                txtICNoDB.Text = txtICNoDB.Text.Insert(6, "-")

                ' Set the cursor position to after the "-" character
                txtICNoDB.SelectionStart = txtICNoDB.TextLength
            End If
            If txtICNoDB.TextLength = 9 Then
                ' Insert "-" at the position after the sixth and ninth character
                txtICNoDB.Text = txtICNoDB.Text.Insert(9, "-")

                ' Set the cursor position to after the "-" character
                txtICNoDB.SelectionStart = txtICNoDB.TextLength

            End If

        End If
        If txtICNoDB.TextLength < 5 Then
            disableTextChangedDB = False
        End If
    End Sub
    Private Sub txtICNoDB_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtICNoDB.KeyPress
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
            MsgBox("Delete Failed.")
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
End Class
