<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtSearchPatientIC = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblDatePastMed = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDateCollection = New System.Windows.Forms.Label()
        Me.lblPatientName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDateSeeDoctor = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoonDose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AfternoonDose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NightDose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalDose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.POM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DrugNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DrugName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Strength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dose = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Frequency = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Duration = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalQTY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DrugNo, Me.DrugName, Me.Strength, Me.Unit, Me.Dose, Me.Frequency, Me.Duration, Me.TotalQTY})
        Me.DataGridView1.Location = New System.Drawing.Point(7, 26)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(931, 289)
        Me.DataGridView1.TabIndex = 0
        '
        'txtSearchPatientIC
        '
        Me.txtSearchPatientIC.Location = New System.Drawing.Point(205, 17)
        Me.txtSearchPatientIC.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSearchPatientIC.MaxLength = 14
        Me.txtSearchPatientIC.Name = "txtSearchPatientIC"
        Me.txtSearchPatientIC.Size = New System.Drawing.Size(138, 21)
        Me.txtSearchPatientIC.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(372, 12)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(74, 30)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(12, 19)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(140, 15)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Search for Patient IC No."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 103)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(942, 463)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Past Medication"
        '
        'lblDatePastMed
        '
        Me.lblDatePastMed.AutoSize = True
        Me.lblDatePastMed.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatePastMed.Location = New System.Drawing.Point(671, 18)
        Me.lblDatePastMed.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatePastMed.Name = "lblDatePastMed"
        Me.lblDatePastMed.Size = New System.Drawing.Size(25, 15)
        Me.lblDatePastMed.TabIndex = 5
        Me.lblDatePastMed.Text = "      "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(494, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 15)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date of Past Medication :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(508, 54)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 15)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Date of Collection :"
        '
        'lblDateCollection
        '
        Me.lblDateCollection.AutoSize = True
        Me.lblDateCollection.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateCollection.Location = New System.Drawing.Point(671, 56)
        Me.lblDateCollection.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDateCollection.Name = "lblDateCollection"
        Me.lblDateCollection.Size = New System.Drawing.Size(25, 15)
        Me.lblDateCollection.TabIndex = 8
        Me.lblDateCollection.Text = "      "
        '
        'lblPatientName
        '
        Me.lblPatientName.AutoSize = True
        Me.lblPatientName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPatientName.Location = New System.Drawing.Point(73, 64)
        Me.lblPatientName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPatientName.Name = "lblPatientName"
        Me.lblPatientName.Size = New System.Drawing.Size(25, 15)
        Me.lblPatientName.TabIndex = 10
        Me.lblPatientName.Text = "      "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 63)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(47, 15)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Name :"
        '
        'lblDateSeeDoctor
        '
        Me.lblDateSeeDoctor.AutoSize = True
        Me.lblDateSeeDoctor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDateSeeDoctor.Location = New System.Drawing.Point(671, 87)
        Me.lblDateSeeDoctor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDateSeeDoctor.Name = "lblDateSeeDoctor"
        Me.lblDateSeeDoctor.Size = New System.Drawing.Size(25, 15)
        Me.lblDateSeeDoctor.TabIndex = 12
        Me.lblDateSeeDoctor.Text = "      "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(534, 86)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 15)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Date See Doctor :"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToResizeRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.NoonDose, Me.AfternoonDose, Me.NightDose, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.TotalDose, Me.POM, Me.DataGridViewTextBoxColumn8})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.Location = New System.Drawing.Point(6, 334)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowHeadersWidth = 51
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(932, 118)
        Me.DataGridView2.TabIndex = 10
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn1.Width = 30
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Insulin Name"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 330
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Strength"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 55
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Unit"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 45
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Morning Dose"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 55
        '
        'NoonDose
        '
        Me.NoonDose.HeaderText = "Noon Dose"
        Me.NoonDose.MinimumWidth = 6
        Me.NoonDose.Name = "NoonDose"
        Me.NoonDose.ReadOnly = True
        Me.NoonDose.Width = 55
        '
        'AfternoonDose
        '
        Me.AfternoonDose.HeaderText = "Afternoon Dose"
        Me.AfternoonDose.MinimumWidth = 6
        Me.AfternoonDose.Name = "AfternoonDose"
        Me.AfternoonDose.ReadOnly = True
        Me.AfternoonDose.Width = 60
        '
        'NightDose
        '
        Me.NightDose.HeaderText = "Night Dose"
        Me.NightDose.MinimumWidth = 6
        Me.NightDose.Name = "NightDose"
        Me.NightDose.ReadOnly = True
        Me.NightDose.Width = 55
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Freq."
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 40
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Duration"
        Me.DataGridViewTextBoxColumn7.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 55
        '
        'TotalDose
        '
        Me.TotalDose.HeaderText = "Total Dose"
        Me.TotalDose.MinimumWidth = 6
        Me.TotalDose.Name = "TotalDose"
        Me.TotalDose.ReadOnly = True
        Me.TotalDose.Width = 45
        '
        'POM
        '
        Me.POM.HeaderText = "POM"
        Me.POM.MinimumWidth = 6
        Me.POM.Name = "POM"
        Me.POM.ReadOnly = True
        Me.POM.Width = 45
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Cart QTY"
        Me.DataGridViewTextBoxColumn8.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 55
        '
        'DrugNo
        '
        Me.DrugNo.HeaderText = "No."
        Me.DrugNo.MinimumWidth = 6
        Me.DrugNo.Name = "DrugNo"
        Me.DrugNo.ReadOnly = True
        Me.DrugNo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DrugNo.Width = 35
        '
        'DrugName
        '
        Me.DrugName.HeaderText = "Drug Name"
        Me.DrugName.MinimumWidth = 6
        Me.DrugName.Name = "DrugName"
        Me.DrugName.ReadOnly = True
        Me.DrugName.Width = 490
        '
        'Strength
        '
        Me.Strength.HeaderText = "Strength"
        Me.Strength.MinimumWidth = 6
        Me.Strength.Name = "Strength"
        Me.Strength.ReadOnly = True
        Me.Strength.Width = 65
        '
        'Unit
        '
        Me.Unit.HeaderText = "Unit"
        Me.Unit.MinimumWidth = 6
        Me.Unit.Name = "Unit"
        Me.Unit.ReadOnly = True
        Me.Unit.Width = 65
        '
        'Dose
        '
        Me.Dose.HeaderText = "Dose"
        Me.Dose.MinimumWidth = 6
        Me.Dose.Name = "Dose"
        Me.Dose.ReadOnly = True
        Me.Dose.Width = 60
        '
        'Frequency
        '
        Me.Frequency.HeaderText = "Frequency"
        Me.Frequency.MinimumWidth = 6
        Me.Frequency.Name = "Frequency"
        Me.Frequency.ReadOnly = True
        Me.Frequency.Width = 75
        '
        'Duration
        '
        Me.Duration.HeaderText = "Duration"
        Me.Duration.MinimumWidth = 6
        Me.Duration.Name = "Duration"
        Me.Duration.ReadOnly = True
        Me.Duration.Width = 65
        '
        'TotalQTY
        '
        Me.TotalQTY.HeaderText = "Total Quantity"
        Me.TotalQTY.MinimumWidth = 6
        Me.TotalQTY.Name = "TotalQTY"
        Me.TotalQTY.ReadOnly = True
        Me.TotalQTY.Width = 70
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(953, 572)
        Me.Controls.Add(Me.lblDateSeeDoctor)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblPatientName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblDateCollection)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDatePastMed)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearchPatientIC)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form2"
        Me.Text = "Search for Past Medications"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtSearchPatientIC As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents lblSearch As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblDatePastMed As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblDateCollection As Label
    Friend WithEvents lblPatientName As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDateSeeDoctor As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents NoonDose As DataGridViewTextBoxColumn
    Friend WithEvents AfternoonDose As DataGridViewTextBoxColumn
    Friend WithEvents NightDose As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents TotalDose As DataGridViewTextBoxColumn
    Friend WithEvents POM As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DrugNo As DataGridViewTextBoxColumn
    Friend WithEvents DrugName As DataGridViewTextBoxColumn
    Friend WithEvents Strength As DataGridViewTextBoxColumn
    Friend WithEvents Unit As DataGridViewTextBoxColumn
    Friend WithEvents Dose As DataGridViewTextBoxColumn
    Friend WithEvents Frequency As DataGridViewTextBoxColumn
    Friend WithEvents Duration As DataGridViewTextBoxColumn
    Friend WithEvents TotalQTY As DataGridViewTextBoxColumn
End Class
