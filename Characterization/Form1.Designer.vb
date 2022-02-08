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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxSG = New System.Windows.Forms.TextBox()
        Me.DataGridViewTBP = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridViewTBP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Specific Gravity"
        '
        'TextBoxSG
        '
        Me.TextBoxSG.Location = New System.Drawing.Point(114, 11)
        Me.TextBoxSG.Name = "TextBoxSG"
        Me.TextBoxSG.Size = New System.Drawing.Size(100, 20)
        Me.TextBoxSG.TabIndex = 1
        Me.TextBoxSG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DataGridViewTBP
        '
        Me.DataGridViewTBP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewTBP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewTBP.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2})
        Me.DataGridViewTBP.Location = New System.Drawing.Point(12, 45)
        Me.DataGridViewTBP.Name = "DataGridViewTBP"
        Me.DataGridViewTBP.RowHeadersVisible = False
        Me.DataGridViewTBP.Size = New System.Drawing.Size(286, 296)
        Me.DataGridViewTBP.TabIndex = 2
        '
        'Column1
        '
        Me.Column1.HeaderText = "% weight"
        Me.Column1.Name = "Column1"
        '
        'Column2
        '
        Me.Column2.HeaderText = "Temperature (C)"
        Me.Column2.Name = "Column2"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 352)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(286, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Characterize"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(314, 386)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridViewTBP)
        Me.Controls.Add(Me.TextBoxSG)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Characterization"
        CType(Me.DataGridViewTBP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents TextBoxSG As Windows.Forms.TextBox
    Friend WithEvents DataGridViewTBP As Windows.Forms.DataGridView
    Friend WithEvents Column1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Windows.Forms.Button
End Class
