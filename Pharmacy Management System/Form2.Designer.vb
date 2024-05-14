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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtSearchPatientIC = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblDatePastMed = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DrugNo, Me.DrugName, Me.Strength, Me.Unit, Me.Dose, Me.Frequency, Me.Duration, Me.TotalQTY})
        Me.DataGridView1.Location = New System.Drawing.Point(6, 21)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersWidth = 51
        Me.DataGridView1.RowTemplate.Height = 24
        Me.DataGridView1.Size = New System.Drawing.Size(1268, 344)
        Me.DataGridView1.TabIndex = 0
        '
        'txtSearchPatientIC
        '
        Me.txtSearchPatientIC.Location = New System.Drawing.Point(192, 3)
        Me.txtSearchPatientIC.MaxLength = 14
        Me.txtSearchPatientIC.Name = "txtSearchPatientIC"
        Me.txtSearchPatientIC.Size = New System.Drawing.Size(181, 22)
        Me.txtSearchPatientIC.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(408, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(14, 9)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(151, 16)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Search for Patient IC No."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1285, 413)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Past Medication"
        '
        'lblDatePastMed
        '
        Me.lblDatePastMed.AutoSize = True
        Me.lblDatePastMed.Location = New System.Drawing.Point(650, 5)
        Me.lblDatePastMed.Name = "lblDatePastMed"
        Me.lblDatePastMed.Size = New System.Drawing.Size(36, 16)
        Me.lblDatePastMed.TabIndex = 5
        Me.lblDatePastMed.Text = "Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(489, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 16)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date of Past Medication :"
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
        Me.Strength.Width = 60
        '
        'Unit
        '
        Me.Unit.HeaderText = "Unit"
        Me.Unit.MinimumWidth = 6
        Me.Unit.Name = "Unit"
        Me.Unit.ReadOnly = True
        Me.Unit.Width = 45
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
        Me.Frequency.Width = 60
        '
        'Duration
        '
        Me.Duration.HeaderText = "Duration"
        Me.Duration.MinimumWidth = 6
        Me.Duration.Name = "Duration"
        Me.Duration.ReadOnly = True
        Me.Duration.Width = 60
        '
        'TotalQTY
        '
        Me.TotalQTY.HeaderText = "Total Quantity"
        Me.TotalQTY.MinimumWidth = 6
        Me.TotalQTY.Name = "TotalQTY"
        Me.TotalQTY.ReadOnly = True
        Me.TotalQTY.Width = 50
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1305, 676)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblDatePastMed)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtSearchPatientIC)
        Me.Name = "Form2"
        Me.Text = "Form2"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
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
    Friend WithEvents DrugNo As DataGridViewTextBoxColumn
    Friend WithEvents DrugName As DataGridViewTextBoxColumn
    Friend WithEvents Strength As DataGridViewTextBoxColumn
    Friend WithEvents Unit As DataGridViewTextBoxColumn
    Friend WithEvents Dose As DataGridViewTextBoxColumn
    Friend WithEvents Frequency As DataGridViewTextBoxColumn
    Friend WithEvents Duration As DataGridViewTextBoxColumn
    Friend WithEvents TotalQTY As DataGridViewTextBoxColumn
End Class
