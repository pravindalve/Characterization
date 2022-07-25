<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxSG = New System.Windows.Forms.TextBox()
        Me.DataGridViewTBP = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxADJZRA = New System.Windows.Forms.CheckBox()
        Me.CheckBoxADJAF = New System.Windows.Forms.CheckBox()
        Me.ComboBoxMW = New System.Windows.Forms.ComboBox()
        Me.ComboBoxSG = New System.Windows.Forms.ComboBox()
        Me.ComboBoxAF = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPC = New System.Windows.Forms.ComboBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ComboBoxTC = New System.Windows.Forms.ComboBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prvs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.srkvs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KButton3 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.cbInputType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridViewTBP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Specific Gravity"
        '
        'TextBoxSG
        '
        Me.TextBoxSG.Location = New System.Drawing.Point(198, 36)
        Me.TextBoxSG.Name = "TextBoxSG"
        Me.TextBoxSG.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxSG.TabIndex = 1
        Me.TextBoxSG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DataGridViewTBP
        '
        Me.DataGridViewTBP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewTBP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTBP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column5})
        Me.DataGridViewTBP.Location = New System.Drawing.Point(12, 65)
        Me.DataGridViewTBP.Name = "DataGridViewTBP"
        Me.DataGridViewTBP.RowHeadersVisible = False
        Me.DataGridViewTBP.Size = New System.Drawing.Size(286, 203)
        Me.DataGridViewTBP.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 274)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(286, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Characterize"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.CheckBoxADJZRA)
        Me.GroupBox4.Controls.Add(Me.CheckBoxADJAF)
        Me.GroupBox4.Controls.Add(Me.ComboBoxMW)
        Me.GroupBox4.Controls.Add(Me.ComboBoxSG)
        Me.GroupBox4.Controls.Add(Me.ComboBoxAF)
        Me.GroupBox4.Controls.Add(Me.ComboBoxPC)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Controls.Add(Me.Label31)
        Me.GroupBox4.Controls.Add(Me.Label30)
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.ComboBoxTC)
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.Location = New System.Drawing.Point(305, 7)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(330, 290)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Methods and Correlations"
        '
        'CheckBoxADJZRA
        '
        Me.CheckBoxADJZRA.AutoSize = True
        Me.CheckBoxADJZRA.Checked = True
        Me.CheckBoxADJZRA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxADJZRA.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxADJZRA.Location = New System.Drawing.Point(6, 225)
        Me.CheckBoxADJZRA.Name = "CheckBoxADJZRA"
        Me.CheckBoxADJZRA.Size = New System.Drawing.Size(281, 17)
        Me.CheckBoxADJZRA.TabIndex = 26
        Me.CheckBoxADJZRA.Text = "Adjust Rackett Parameters to match Specific Gravities"
        Me.CheckBoxADJZRA.UseVisualStyleBackColor = True
        '
        'CheckBoxADJAF
        '
        Me.CheckBoxADJAF.AutoSize = True
        Me.CheckBoxADJAF.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.CheckBoxADJAF.Location = New System.Drawing.Point(6, 193)
        Me.CheckBoxADJAF.Name = "CheckBoxADJAF"
        Me.CheckBoxADJAF.Size = New System.Drawing.Size(209, 17)
        Me.CheckBoxADJAF.TabIndex = 25
        Me.CheckBoxADJAF.Text = "Adjust Acentric Factors to match NBPs"
        Me.CheckBoxADJAF.UseVisualStyleBackColor = True
        '
        'ComboBoxMW
        '
        Me.ComboBoxMW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxMW.Enabled = False
        Me.ComboBoxMW.FormattingEnabled = True
        Me.ComboBoxMW.Items.AddRange(New Object() {"Winn (1956)", "Riazi (1986)", "Lee-Kesler (1974)"})
        Me.ComboBoxMW.Location = New System.Drawing.Point(131, 152)
        Me.ComboBoxMW.Name = "ComboBoxMW"
        Me.ComboBoxMW.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxMW.TabIndex = 24
        '
        'ComboBoxSG
        '
        Me.ComboBoxSG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSG.Enabled = False
        Me.ComboBoxSG.FormattingEnabled = True
        Me.ComboBoxSG.Items.AddRange(New Object() {"Riazi-Al-Sahhaf (1996)"})
        Me.ComboBoxSG.Location = New System.Drawing.Point(131, 120)
        Me.ComboBoxSG.Name = "ComboBoxSG"
        Me.ComboBoxSG.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxSG.TabIndex = 23
        '
        'ComboBoxAF
        '
        Me.ComboBoxAF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAF.FormattingEnabled = True
        Me.ComboBoxAF.Items.AddRange(New Object() {"Lee-Kesler (1976)", "Korsten (2000)"})
        Me.ComboBoxAF.Location = New System.Drawing.Point(131, 89)
        Me.ComboBoxAF.Name = "ComboBoxAF"
        Me.ComboBoxAF.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxAF.TabIndex = 22
        '
        'ComboBoxPC
        '
        Me.ComboBoxPC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxPC.FormattingEnabled = True
        Me.ComboBoxPC.Items.AddRange(New Object() {"Riazi-Daubert (1985)", "Riazi (2005)", "Lee-Kesler (1976)", "Farah (2006)"})
        Me.ComboBoxPC.Location = New System.Drawing.Point(131, 58)
        Me.ComboBoxPC.Name = "ComboBoxPC"
        Me.ComboBoxPC.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxPC.TabIndex = 21
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Enabled = False
        Me.Label33.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label33.Location = New System.Drawing.Point(6, 126)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(81, 13)
        Me.Label33.TabIndex = 20
        Me.Label33.Text = "Specific Gravity"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Enabled = False
        Me.Label31.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label31.Location = New System.Drawing.Point(6, 157)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(90, 13)
        Me.Label31.TabIndex = 18
        Me.Label31.Text = "Molecular Weight"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label30.Location = New System.Drawing.Point(6, 95)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(79, 13)
        Me.Label30.TabIndex = 17
        Me.Label30.Text = "Acentric Factor"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label29.Location = New System.Drawing.Point(6, 64)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(82, 13)
        Me.Label29.TabIndex = 16
        Me.Label29.Text = "Critical Pressure"
        '
        'ComboBoxTC
        '
        Me.ComboBoxTC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTC.FormattingEnabled = True
        Me.ComboBoxTC.Items.AddRange(New Object() {"Riazi-Daubert (1985)", "Riazi (2005)", "Lee-Kesler (1976)", "Farah (2006)"})
        Me.ComboBoxTC.Location = New System.Drawing.Point(131, 28)
        Me.ComboBoxTC.Name = "ComboBoxTC"
        Me.ComboBoxTC.Size = New System.Drawing.Size(181, 21)
        Me.ComboBoxTC.TabIndex = 15
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label28.Location = New System.Drawing.Point(6, 33)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(101, 13)
        Me.Label28.TabIndex = 14
        Me.Label28.Text = "Critical Temperature"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.GroupBox1.Location = New System.Drawing.Point(12, 303)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(623, 315)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Properties"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(6, 296)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(391, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "You can edit/update the values on the above table before creating the oil stream." &
    ""
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToResizeRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.Column4, Me.DataGridViewTextBoxColumn2, Me.Column3, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.prvs, Me.srkvs})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView2.Location = New System.Drawing.Point(3, 17)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView2.Size = New System.Drawing.Size(620, 273)
        Me.DataGridView2.TabIndex = 3
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 59
        '
        'Column4
        '
        Me.Column4.HeaderText = "Molar Fraction"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 92
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "NBP"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 51
        '
        'Column3
        '
        Me.Column3.HeaderText = "SG"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 45
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "MW"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 50
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Tc"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 43
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "Pc"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 43
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "Ac. Factor"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 76
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "Visc 1"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 50
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "Visc 2"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 50
        '
        'prvs
        '
        Me.prvs.HeaderText = "PR VShift (ci/bi)"
        Me.prvs.Name = "prvs"
        Me.prvs.Width = 97
        '
        'srkvs
        '
        Me.srkvs.HeaderText = "SRK Vshift (ci/bi)"
        Me.srkvs.Name = "srkvs"
        Me.srkvs.Width = 102
        '
        'KButton3
        '
        Me.KButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.KButton3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.KButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.KButton3.Location = New System.Drawing.Point(330, 626)
        Me.KButton3.Name = "KButton3"
        Me.KButton3.Size = New System.Drawing.Size(154, 25)
        Me.KButton3.TabIndex = 18
        Me.KButton3.Text = "Create and Add Oil Stream"
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(100, 629)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(223, 20)
        Me.TextBox1.TabIndex = 17
        Me.TextBox1.Text = "OIL_1"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(14, 632)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Stream Name"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Button2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button2.Location = New System.Drawing.Point(490, 626)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 25)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Export All to JSON"
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.Description = "Select a folder where to save the JSON files."
        '
        'cbInputType
        '
        Me.cbInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbInputType.DropDownWidth = 200
        Me.cbInputType.FormattingEnabled = True
        Me.cbInputType.Items.AddRange(New Object() {"Weight Percentages", "Volume Percentages", "Weight Percentages with SG Data", "Volume Percentages with SG Data"})
        Me.cbInputType.Location = New System.Drawing.Point(141, 7)
        Me.cbInputType.Name = "cbInputType"
        Me.cbInputType.Size = New System.Drawing.Size(157, 21)
        Me.cbInputType.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(12, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Input Type"
        '
        'Column1
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column1.HeaderText = "% Weight"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column2.HeaderText = "Temperature"
        Me.Column2.Name = "Column2"
        '
        'Column5
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column5.HeaderText = "SG"
        Me.Column5.Name = "Column5"
        Me.Column5.Visible = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 663)
        Me.Controls.Add(Me.cbInputType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.KButton3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewTBP)
        Me.Controls.Add(Me.TextBoxSG)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Petroleum Characterization"
        Me.TopMost = True
        CType(Me.DataGridViewTBP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TextBoxSG As Windows.Forms.TextBox
    Friend WithEvents DataGridViewTBP As Windows.Forms.DataGridView
    Friend WithEvents Button1 As Windows.Forms.Button
    Public WithEvents GroupBox4 As Windows.Forms.GroupBox
    Public WithEvents CheckBoxADJZRA As Windows.Forms.CheckBox
    Public WithEvents CheckBoxADJAF As Windows.Forms.CheckBox
    Public WithEvents ComboBoxMW As Windows.Forms.ComboBox
    Public WithEvents ComboBoxSG As Windows.Forms.ComboBox
    Public WithEvents ComboBoxAF As Windows.Forms.ComboBox
    Public WithEvents ComboBoxPC As Windows.Forms.ComboBox
    Public WithEvents Label33 As Windows.Forms.Label
    Public WithEvents Label31 As Windows.Forms.Label
    Public WithEvents Label30 As Windows.Forms.Label
    Public WithEvents Label29 As Windows.Forms.Label
    Public WithEvents ComboBoxTC As Windows.Forms.ComboBox
    Public WithEvents Label28 As Windows.Forms.Label
    Public WithEvents GroupBox1 As Windows.Forms.GroupBox
    Public WithEvents Label6 As Windows.Forms.Label
    Public WithEvents DataGridView2 As Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prvs As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents srkvs As Windows.Forms.DataGridViewTextBoxColumn
    Public WithEvents KButton3 As Windows.Forms.Button
    Public WithEvents TextBox1 As Windows.Forms.TextBox
    Public WithEvents Label2 As Windows.Forms.Label
    Public WithEvents Button2 As Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As Windows.Forms.FolderBrowserDialog
    Public WithEvents cbInputType As Windows.Forms.ComboBox
    Public WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Column1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As Windows.Forms.DataGridViewTextBoxColumn
End Class
