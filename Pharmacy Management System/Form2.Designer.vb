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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtSearchPatientIC = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.lblDatePastMed = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.DataGridView1.Location = New System.Drawing.Point(5, 29)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1022, 317)
        Me.DataGridView1.TabIndex = 0
        '
        'txtSearchPatientIC
        '
        Me.txtSearchPatientIC.Location = New System.Drawing.Point(192, 26)
        Me.txtSearchPatientIC.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtSearchPatientIC.MaxLength = 14
        Me.txtSearchPatientIC.Name = "txtSearchPatientIC"
        Me.txtSearchPatientIC.Size = New System.Drawing.Size(181, 22)
        Me.txtSearchPatientIC.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(408, 26)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(13, 29)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(151, 16)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Search for Patient IC No."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 71)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(1033, 486)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Past Medication"
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.NoonDose, Me.AfternoonDose, Me.NightDose, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.TotalDose, Me.POM, Me.DataGridViewTextBoxColumn8})
        Me.DataGridView2.Location = New System.Drawing.Point(5, 362)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.RowHeadersWidth = 51
        Me.DataGridView2.RowTemplate.Height = 24
        Me.DataGridView2.Size = New System.Drawing.Size(1022, 109)
        Me.DataGridView2.TabIndex = 7
        '
        'lblDatePastMed
        '
        Me.lblDatePastMed.AutoSize = True
        Me.lblDatePastMed.Location = New System.Drawing.Point(652, 31)
        Me.lblDatePastMed.Name = "lblDatePastMed"
        Me.lblDatePastMed.Size = New System.Drawing.Size(25, 16)
        Me.lblDatePastMed.TabIndex = 5
        Me.lblDatePastMed.Text = "      "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(489, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date of Past Medication :"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "No."
        Me.DataGridViewTextBoxColumn1.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewTextBoxColumn1.Width = 35
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Insulin Name"
        Me.DataGridViewTextBoxColumn2.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 300
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Strength"
        Me.DataGridViewTextBoxColumn3.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 65
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Unit"
        Me.DataGridViewTextBoxColumn4.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Morning Dose"
        Me.DataGridViewTextBoxColumn5.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 60
        '
        'NoonDose
        '
        Me.NoonDose.HeaderText = "Noon Dose"
        Me.NoonDose.MinimumWidth = 6
        Me.NoonDose.Name = "NoonDose"
        Me.NoonDose.ReadOnly = True
        Me.NoonDose.Width = 50
        '
        'AfternoonDose
        '
        Me.AfternoonDose.HeaderText = "Afternoon Dose"
        Me.AfternoonDose.MinimumWidth = 6
        Me.AfternoonDose.Name = "AfternoonDose"
        Me.AfternoonDose.ReadOnly = True
        Me.AfternoonDose.Width = 70
        '
        'NightDose
        '
        Me.NightDose.HeaderText = "Night Dose"
        Me.NightDose.MinimumWidth = 6
        Me.NightDose.Name = "NightDose"
        Me.NightDose.ReadOnly = True
        Me.NightDose.Width = 50
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Frequency"
        Me.DataGridViewTextBoxColumn6.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 75
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Duration"
        Me.DataGridViewTextBoxColumn7.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Width = 65
        '
        'TotalDose
        '
        Me.TotalDose.HeaderText = "Total Dose"
        Me.TotalDose.MinimumWidth = 6
        Me.TotalDose.Name = "TotalDose"
        Me.TotalDose.ReadOnly = True
        Me.TotalDose.Width = 50
        '
        'POM
        '
        Me.POM.HeaderText = "POM"
        Me.POM.MinimumWidth = 6
        Me.POM.Name = "POM"
        Me.POM.ReadOnly = True
        Me.POM.Width = 50
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Cart QTY"
        Me.DataGridViewTextBoxColumn8.MinimumWidth = 6
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 60
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
        Me.DrugName.Width = 450
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
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1050, 567)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDatePastMed)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearchPatientIC)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
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
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DrugNo As DataGridViewTextBoxColumn
    Friend WithEvents DrugName As DataGridViewTextBoxColumn
    Friend WithEvents Strength As DataGridViewTextBoxColumn
    Friend WithEvents Unit As DataGridViewTextBoxColumn
    Friend WithEvents Dose As DataGridViewTextBoxColumn
    Friend WithEvents Frequency As DataGridViewTextBoxColumn
    Friend WithEvents Duration As DataGridViewTextBoxColumn
    Friend WithEvents TotalQTY As DataGridViewTextBoxColumn
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
End Class
