
Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions


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


    Private disableTextChanged As Boolean = False

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

    End Sub

    Public Sub InitializeAll()

        'Initialize Duration at NEW Patient Tab
        dtpDateCollection.Value = DateAdd("m", 1, Now().Date)
        btnIOU.Enabled = False
        InitializeDB()
        GetDefaultPrinterName()
        disabledrug2to10()

        btnCheckICMySPR.Enabled = False
    End Sub

    Private Sub InitializeDB()

        Server = txtServerName.Text
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
    End Sub

    Public Sub print()
        If cbDrug1.Text = "" AndAlso cbInsulin1.Text = "" Then
            MsgBox("Nothing to print")
            Return
             
        End If

        PrintDoc.DefaultPageSettings.PaperSize = New PaperSize("Label Size", 314.97, 196.85) 'width, height
        PrintDoc.DefaultPageSettings.Landscape = False

        PPD.Document = PrintDoc
        PPD.ShowDialog()
        currentPage = 1
        'PrintDoc.Print()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        print()
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDoc.PrintPage
        'Set Custom Names
        Dim ClinicName As String = "FARMASI KLINIK KESIHATAN "
        'Set Control Print Variable
        Dim stopprintflag As Boolean = False
        'Set Fonts
        Dim f8 As New Font("Arial", 8, FontStyle.Italic)
        Dim f8a As New Font("Arial", 8, FontStyle.Bold)
        Dim f8b As New Font("Arial", 13, FontStyle.Bold)
        'Set Alignments
        Dim left As New StringFormat
        Dim centre As New StringFormat
        Dim right As New StringFormat

        left.Alignment = StringAlignment.Near
        centre.Alignment = StringAlignment.Center
        right.Alignment = StringAlignment.Far

        'Draw Rectangles
        Dim Rect1 As New Rectangle(5, 5, 305, 185) '(margin, margin, width, height) 'Border
        Dim Rect2 As New Rectangle(5, 20, 305, 45)
        Dim Rect3 As New Rectangle(5, 45, 305, 20)

        Dim Rect4 As New Rectangle(6, 70, 300, 25)
        Dim Rect5 As New Rectangle(5, 96, 305, 25)
        Dim Rect6 As New Rectangle(6, 100, 300, 20)

        Dim Rect7 As New Rectangle(5, 121, 305, 40)
        Dim Rect8 As New Rectangle(6, 128, 300, 30) 'Remark margin


        Dim Rect9 As New Rectangle(200, 175, 80, 12) 'Jumlah



        Try
            If cbDrug1.Text = "" Then
                e.HasMorePages = False
                Return
            End If

            e.Graphics.DrawRectangle(Pens.Black, Rect1)
            e.Graphics.DrawRectangle(Pens.Black, Rect2)
            e.Graphics.DrawRectangle(Pens.Black, Rect3)
            e.Graphics.DrawRectangle(Pens.White, Rect4)
            e.Graphics.DrawRectangle(Pens.Black, Rect5)
            e.Graphics.DrawRectangle(Pens.White, Rect6)
            e.Graphics.DrawRectangle(Pens.Black, Rect7)
            e.Graphics.DrawRectangle(Pens.White, Rect8) 'Remark margin
            e.Graphics.DrawRectangle(Pens.White, Rect9)

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
                    'Check for Blank Selection of Drug


                    e.Graphics.DrawString(cbDrug1.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD1 & (CDbl(txtDoseD1.Text) / CDbl(lblStrD1.Text)) & ConsumeUnitD1 & txtFreqD1.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD1, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD1.Text, f8a, Brushes.Black, Rect9, left)

                Case 2
                    'Check for Blank Selection of Drug
                    If cbDrug3.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug2.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD2 & (CDbl(txtDoseD2.Text) / CDbl(lblStrD2.Text)) & ConsumeUnitD2 & txtFreqD2.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD2, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD2.Text, f8a, Brushes.Black, Rect9, left)
                Case 3
                    'Check for Blank Selection of Drug
                    If cbDrug4.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug3.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD3 & (CDbl(txtDoseD3.Text) / CDbl(lblStrD3.Text)) & ConsumeUnitD3 & txtFreqD3.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD3, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD3.Text, f8a, Brushes.Black, Rect9, left)
                Case 4
                    'Check for Blank Selection of Drug
                    If cbDrug5.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug4.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD4 & (CDbl(txtDoseD4.Text) / CDbl(lblStrD4.Text)) & ConsumeUnitD4 & txtFreqD4.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD4, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD4.Text, f8a, Brushes.Black, Rect9, left)
                Case 5
                    'Check for Blank Selection of Drug
                    If cbDrug6.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug5.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD5 & (CDbl(txtDoseD5.Text) / CDbl(lblStrD5.Text)) & ConsumeUnitD5 & txtFreqD5.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD5, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD5.Text, f8a, Brushes.Black, Rect9, left)
                Case 6
                    'Check for Blank Selection of Drug
                    If cbDrug7.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug6.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD6 & (CDbl(txtDoseD6.Text) / CDbl(lblStrD6.Text)) & ConsumeUnitD6 & txtFreqD6.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD6, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD6.Text, f8a, Brushes.Black, Rect9, left)
                Case 7
                    'Check for Blank Selection of Drug
                    If cbDrug8.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug7.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD7 & (CDbl(txtDoseD7.Text) / CDbl(lblStrD7.Text)) & ConsumeUnitD7 & txtFreqD7.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD7, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD7.Text, f8a, Brushes.Black, Rect9, left)
                Case 8
                    'Check for Blank Selection of Drug
                    If cbDrug9.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug8.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD8 & (CDbl(txtDoseD8.Text) / CDbl(lblStrD8.Text)) & ConsumeUnitD8 & txtFreqD8.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD8, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD8.Text, f8a, Brushes.Black, Rect9, left)
                Case 9
                    'Check for Blank Selection of Drug
                    If cbDrug10.Text = "" Then
                        currentPage += 1
                        e.HasMorePages = False
                        stopprintflag = True

                    End If

                    e.Graphics.DrawString(cbDrug9.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD9 & (CDbl(txtDoseD9.Text) / CDbl(lblStrD9.Text)) & ConsumeUnitD9 & txtFreqD9.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD9, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD9.Text, f8a, Brushes.Black, Rect9, left)
                Case 10

                    e.Graphics.DrawString(cbDrug10.Text, f8a, Brushes.Black, Rect4, centre)
                    e.Graphics.DrawString(ConsumeMethodD10 & (CDbl(txtDoseD10.Text) / CDbl(lblStrD10.Text)) & ConsumeUnitD10 & txtFreqD10.Text & " kali sehari", f8b, Brushes.Black, Rect6, centre)
                    e.Graphics.DrawString(RemarkD10, f8a, Brushes.Black, Rect8, centre)
                    e.Graphics.DrawString("Jumlah: " & txtQTYD10.Text, f8a, Brushes.Black, Rect9, left)

            End Select
            ' Increment the page counter
            currentPage += 1

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
            `Drug10Name`,`Drug10Strength`,`Drug10Unit`,`Drug10Dose`,`Drug10Freq`,`Drug10Duration`,`Drug10TotalQTY`)
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
            @Drug10Name,@Drug10Strength,@Drug10Unit,@Drug10Dose,@Drug10Freq,@Drug10Duration,@Drug10TotalQTY)", conn)
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




            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                conn.Close()

                MsgBox("Saved data for " & stPatientName & ", IC No.: " & stIC)
                stlbMainStatus.Text = "Saved Successfully for " & stPatientName & ", IC No: " & stIC
                loadDBDataforPatientInfo() 'Refresh IC Textbox Autocomplete




            Else
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            conn.Close()

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

                                MsgBox("Old Data saved to History for " & stPatientName & ", IC No.: " & stIC)
                                stlbMainStatus.Text = "Old Data Saved for " & stPatientName & ", IC No: " & stIC


                            Else
                                MsgBox("Save Failed.")
                                conn.Close()
                                Return
                            End If

                            'Overwrite the current data in prescribeddrugs table

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
                            `Drug10Name`=@Drug10Name,`Drug10Strength`=@Drug10Strength,`Drug10Unit`=@Drug10Unit,`Drug10Dose`=@Drug10Dose,`Drug10Freq`=@Drug10Freq,`Drug10Duration`=@Drug10Duration,`Drug10TotalQTY`=@Drug10TotalQTY WHERE `ICNo`=@ICNo", conn)
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

                            conn.Open()
                            Dim i = cmd2.ExecuteNonQuery
                            If i > 0 Then
                                conn.Close()
                                MsgBox("Successfully Updated Data.")
                                MsgBox("Overwritten data for " & stPatientName & ", IC No.: " & stIC)
                                stlbMainStatus.Text = "Overwrite Successfully for " & stPatientName & ", IC No: " & stIC

                            End If



                        Catch exx As Exception
                            MsgBox(exx.Message)
                            conn.Close()
                            Return
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

    Public Sub Add()
        'Add button at Drugs Tab to Save Data entered in the Text Boxes
        Try
            If txtDrugName.Text = "" Then
                MsgBox("Error. Drug Name cannot be empty")
                Return
            End If

            conn.Open()

            Dim cmd As New MySqlCommand("INSERT INTO `drugtable` (`DrugName`,`Strength`,`Unit`,`DosageForm`,`PrescriberCategory`,`Remark`) VALUES (@DrugName,@Strength,@Unit,@DosageForm,@PrescriberCategory,@Remark)", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DrugName", txtDrugName.Text)
            cmd.Parameters.AddWithValue("@Strength", CDbl(txtStrength.Text))
            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@DosageForm", txtDosageForm.Text)
            cmd.Parameters.AddWithValue("@PrescriberCategory", txtPrescriberCategory.Text)
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
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Sub drugclear()
        txtDrugName.Clear()
        txtStrength.Clear()
        txtUnit.Clear()
        txtDosageForm.Clear()
        txtPrescriberCategory.Clear()
        txtRemark.Clear()
    End Sub

    Public Sub DGV_Load()
        DataGridView1.Rows.Clear()
        'Data Grid View Method to Get Data from MYSQL Database
        Try

            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM drugtable", conn)
            dr = cmd.ExecuteReader

            While dr.Read
                DataGridView1.Rows.Add(dr.Item("DrugName"), dr.Item("Strength"), dr.Item("Unit"), dr.Item("DosageForm"), dr.Item("PrescriberCategory"), dr.Item("Remark"))

            End While
            dr.Dispose()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub


    Public Sub Edit()
        'Add button at Database Tab to Save Data entered into Text Boxes
        Try
            If txtDrugName.Text = "" Then
                MsgBox("Error. Drug Name cannot be empty")
                Return
            End If

            conn.Open()

            Dim cmd As New MySqlCommand("UPDATE `drugtable` SET `Strength`=@Strength,`Unit`=@Unit,`DosageForm`=@DosageForm,`PrescriberCategory`=@PrescriberCategory,`Remark`=@Remark WHERE `DrugName`=@DrugName", conn)
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@DrugName", txtDrugName.Text)
            cmd.Parameters.AddWithValue("@Strength", CDec(txtStrength.Text))
            cmd.Parameters.AddWithValue("@Unit", txtUnit.Text)
            cmd.Parameters.AddWithValue("@DosageForm", txtDosageForm.Text)
            cmd.Parameters.AddWithValue("@PrescriberCategory", txtPrescriberCategory.Text)
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text)

            Dim i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Successfully Updated.")
                conn.Close()

                drugclear()
                DGV_Load()
                txtDrugName.ReadOnly = False
                btnAddDrug.Enabled = True

            Else
                MsgBox("Save Failed.")
            End If
        Catch ex As Exception
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
        Select Case MsgBox("Do you want to Modify the Selected Item?", MsgBoxStyle.YesNoCancel, "Confirmation")
            Case MsgBoxResult.Yes
                'Take values from the DGV table
                txtDrugName.Text = DataGridView1.CurrentRow.Cells(0).Value
                txtStrength.Text = DataGridView1.CurrentRow.Cells(1).Value
                txtUnit.Text = DataGridView1.CurrentRow.Cells(2).Value
                txtDosageForm.Text = DataGridView1.CurrentRow.Cells(3).Value
                txtPrescriberCategory.Text = DataGridView1.CurrentRow.Cells(4).Value
                txtRemark.Text = DataGridView1.CurrentRow.Cells(5).Value

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
                        MsgBox("Delete Failed.")
                    End If
                Catch ex As Exception
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
                DataGridView1.Rows.Add(dr.Item("DrugName"), dr.Item("Strength"), dr.Item("Unit"), dr.Item("DosageForm"), dr.Item("PrescriberCategory"), dr.Item("Remark"))

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
        Dim DBPatientName As String
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
                    ConsumeMethodD1 = "Kumur dalam mulut "
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
                    ConsumeMethodD1 = "Kumur dalam mulut "
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
                    ConsumeMethodD2 = "Kumur dalam mulut "
                    ConsumeUnitD2 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD2 = "Ambil "
                    ConsumeUnitD2 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD2 = "Minum "
                    ConsumeUnitD2 = " paket "
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
                    ConsumeMethodD3 = "Kumur dalam mulut "
                    ConsumeUnitD3 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD3 = "Ambil "
                    ConsumeUnitD3 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD3 = "Minum "
                    ConsumeUnitD3 = " paket "
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
                    ConsumeMethodD4 = "Kumur dalam mulut "
                    ConsumeUnitD4 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD4 = "Ambil "
                    ConsumeUnitD4 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD4 = "Minum "
                    ConsumeUnitD4 = " paket "
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
                    ConsumeMethodD5 = "Kumur dalam mulut "
                    ConsumeUnitD5 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD5 = "Ambil "
                    ConsumeUnitD5 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD5 = "Minum "
                    ConsumeUnitD5 = " paket "
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
                    ConsumeMethodD6 = "Kumur dalam mulut "
                    ConsumeUnitD6 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD6 = "Ambil "
                    ConsumeUnitD6 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD6 = "Minum "
                    ConsumeUnitD6 = " paket "
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
                    ConsumeMethodD7 = "Kumur dalam mulut "
                    ConsumeUnitD7 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD7 = "Ambil "
                    ConsumeUnitD7 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD7 = "Minum "
                    ConsumeUnitD7 = " paket "
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
                    ConsumeMethodD8 = "Kumur dalam mulut "
                    ConsumeUnitD8 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD8 = "Ambil "
                    ConsumeUnitD8 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD8 = "Minum "
                    ConsumeUnitD8 = " paket "
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
                    ConsumeMethodD9 = "Kumur dalam mulut "
                    ConsumeUnitD9 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD9 = "Ambil "
                    ConsumeUnitD9 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD9 = "Minum "
                    ConsumeUnitD9 = " paket "
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
                    ConsumeMethodD10 = "Kumur dalam mulut "
                    ConsumeUnitD10 = " ml "
                ElseIf dr.Item("DosageForm") = "Inhaler" Then
                    ConsumeMethodD10 = "Ambil "
                    ConsumeUnitD10 = " sedutan "
                ElseIf dr.Item("DosageForm") = "Internal" Then
                    ConsumeMethodD10 = "Minum "
                    ConsumeUnitD10 = " paket "
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

        Catch ex As Exception
            txtQTYD1.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD2()
        Dim TotalQTYD2 As Double

        Try
            TotalQTYD2 = (CDbl(txtDoseD2.Text) * CDbl(txtFreqD2.Text) * CDbl(txtDurationD2.Text)) / CDbl(lblStrD2.Text)
            txtQTYD2.Text = Math.Round(TotalQTYD2, 2)

        Catch ex As Exception
            txtQTYD2.Text = ""
        End Try
    End Sub
    Public Sub calculatedrugD3()
        Dim TotalQTYD3 As Double

        Try
            TotalQTYD3 = (CDbl(txtDoseD3.Text) * CDbl(txtFreqD3.Text) * CDbl(txtDurationD3.Text)) / CDbl(lblStrD3.Text)
            txtQTYD3.Text = Math.Round(TotalQTYD3, 2)

        Catch ex As Exception
            txtQTYD3.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD4()
        Dim TotalQTYD4 As Double

        Try
            TotalQTYD4 = (CDbl(txtDoseD4.Text) * CDbl(txtFreqD4.Text) * CDbl(txtDurationD4.Text)) / CDbl(lblStrD4.Text)
            txtQTYD4.Text = Math.Round(TotalQTYD4, 2)

        Catch ex As Exception
            txtQTYD4.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD5()
        Dim TotalQTYD5 As Double

        Try
            TotalQTYD5 = (CDbl(txtDoseD5.Text) * CDbl(txtFreqD5.Text) * CDbl(txtDurationD5.Text)) / CDbl(lblStrD5.Text)
            txtQTYD5.Text = Math.Round(TotalQTYD5, 2)

        Catch ex As Exception
            txtQTYD5.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD6()
        Dim TotalQTYD6 As Double

        Try
            TotalQTYD6 = (CDbl(txtDoseD6.Text) * CDbl(txtFreqD6.Text) * CDbl(txtDurationD6.Text)) / CDbl(lblStrD6.Text)
            txtQTYD6.Text = Math.Round(TotalQTYD6, 2)

        Catch ex As Exception
            txtQTYD6.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD7()
        Dim TotalQTYD7 As Double

        Try
            TotalQTYD7 = (CDbl(txtDoseD7.Text) * CDbl(txtFreqD7.Text) * CDbl(txtDurationD7.Text)) / CDbl(lblStrD7.Text)
            txtQTYD7.Text = Math.Round(TotalQTYD7, 2)

        Catch ex As Exception
            txtQTYD7.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD8()
        Dim TotalQTYD8 As Double

        Try
            TotalQTYD8 = (CDbl(txtDoseD8.Text) * CDbl(txtFreqD8.Text) * CDbl(txtDurationD8.Text)) / CDbl(lblStrD8.Text)
            txtQTYD8.Text = Math.Round(TotalQTYD8, 2)

        Catch ex As Exception
            txtQTYD8.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD9()
        Dim TotalQTYD9 As Double

        Try
            TotalQTYD9 = (CDbl(txtDoseD9.Text) * CDbl(txtFreqD9.Text) * CDbl(txtDurationD9.Text)) / CDbl(lblStrD9.Text)
            txtQTYD9.Text = Math.Round(TotalQTYD9, 2)

        Catch ex As Exception
            txtQTYD9.Text = ""
        End Try
    End Sub

    Public Sub calculatedrugD10()
        Dim TotalQTYD10 As Double

        Try
            TotalQTYD10 = (CDbl(txtDoseD10.Text) * CDbl(txtFreqD10.Text) * CDbl(txtDurationD10.Text)) / CDbl(lblStrD10.Text)
            txtQTYD10.Text = Math.Round(TotalQTYD10, 2)

        Catch ex As Exception
            txtQTYD10.Text = ""
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



    Private Sub dtpDateCollection_ValueChanged(sender As Object, e As EventArgs) Handles dtpDateCollection.ValueChanged
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

    End Sub

    Public Sub cleardruginputsD1()
        'Clear the drugs text boxes
        'Drug 1
        lblStrD1.Text = ""
        lblUnitD1.Text = ""
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
        txtDoseD10.Clear()
        txtFreqD10.Clear()
        txtDurationD10.Clear()
        txtQTYD10.Clear()
        Drug10Selected = False
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

    End Sub
    'Drug 1
    Private Sub cbDrug1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug1.SelectedIndexChanged
        populatevaluesD1()
        calculatedrugD1()
        lbDrugNumber1.Focus()
        unhighlightallcb()
        calculateDurationD1()
        Drug1Selected = True
        cbDrug2.Enabled = True
    End Sub

    Private Sub cbDrug1_TextChanged(sender As Object, e As EventArgs) Handles cbDrug1.TextChanged
        cleardruginputsD1()
    End Sub
    'Drug 2
    Private Sub cbDrug2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbDrug2.SelectedIndexChanged
        populatevaluesD2()
        calculatedrugD2()
        lbDrugNumber2.Focus()
        unhighlightallcb()
        calculateDurationD2()
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
        lbDrugNumber3.Focus()
        unhighlightallcb()
        calculateDurationD3()
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
        lbDrugNumber5.Focus()
        unhighlightallcb()
        calculateDurationD5()
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
        lbDrugNumber6.Focus()
        unhighlightallcb()
        calculateDurationD6()
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
        lbDrugNumber7.Focus()
        unhighlightallcb()
        calculateDurationD7()
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
        lbDrugNumber8.Focus()
        unhighlightallcb()
        calculateDurationD8()
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
        lbDrugNumber9.Focus()
        unhighlightallcb()
        calculateDurationD9()
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
        lbDrugNumber10.Focus()
        unhighlightallcb()
        Drug10Selected = True
        calculateDurationD10()
    End Sub

    Private Sub cbDrug10_TextChanged(sender As Object, e As EventArgs) Handles cbDrug10.TextChanged
        cleardruginputsD10()
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
        lblAge.Text = "Age"
        lblGender.Text = "Gender"
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
    End Sub
End Class
