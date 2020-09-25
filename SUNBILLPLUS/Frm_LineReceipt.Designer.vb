<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_LineReceipt
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.DTP_Vchdate = New System.Windows.Forms.DateTimePicker()
        Me.txt_line = New System.Windows.Forms.TextBox()
        Me.lbl_no = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.cmd_cancel = New System.Windows.Forms.Button()
        Me.Txt_Totamt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.c_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_party = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_partyid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_adless = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_accid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.DTP_Vchdate)
        Me.Panel2.Controls.Add(Me.txt_line)
        Me.Panel2.Controls.Add(Me.lbl_no)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(1, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(839, 52)
        Me.Panel2.TabIndex = 1
        '
        'DTP_Vchdate
        '
        Me.DTP_Vchdate.CustomFormat = "dd/MM/yyyy"
        Me.DTP_Vchdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_Vchdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_Vchdate.Location = New System.Drawing.Point(91, 3)
        Me.DTP_Vchdate.Name = "DTP_Vchdate"
        Me.DTP_Vchdate.Size = New System.Drawing.Size(136, 21)
        Me.DTP_Vchdate.TabIndex = 0
        '
        'txt_line
        '
        Me.txt_line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_line.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_line.Location = New System.Drawing.Point(91, 28)
        Me.txt_line.Name = "txt_line"
        Me.txt_line.Size = New System.Drawing.Size(145, 21)
        Me.txt_line.TabIndex = 100
        '
        'lbl_no
        '
        Me.lbl_no.AutoSize = True
        Me.lbl_no.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_no.Location = New System.Drawing.Point(5, 33)
        Me.lbl_no.Name = "lbl_no"
        Me.lbl_no.Size = New System.Drawing.Size(34, 13)
        Me.lbl_no.TabIndex = 3
        Me.lbl_no.Text = "Line"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(5, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Date"
        '
        'GridView1
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView1.ColumnHeadersHeight = 35
        Me.GridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.c_code, Me.c_party, Me.c_partyid, Me.c_amount, Me.c_adless, Me.c_accid})
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(1, 57)
        Me.GridView1.MultiSelect = False
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridView1.RowHeadersWidth = 45
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.Size = New System.Drawing.Size(839, 414)
        Me.GridView1.TabIndex = 6
        '
        'cmd_ok
        '
        Me.cmd_ok.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ok.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ok.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_ok.Location = New System.Drawing.Point(659, 474)
        Me.cmd_ok.Name = "cmd_ok"
        Me.cmd_ok.Size = New System.Drawing.Size(90, 28)
        Me.cmd_ok.TabIndex = 134
        Me.cmd_ok.Text = "&Ok"
        Me.cmd_ok.UseVisualStyleBackColor = False
        '
        'cmd_cancel
        '
        Me.cmd_cancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_cancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancel.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_cancel.Location = New System.Drawing.Point(751, 474)
        Me.cmd_cancel.Name = "cmd_cancel"
        Me.cmd_cancel.Size = New System.Drawing.Size(90, 28)
        Me.cmd_cancel.TabIndex = 146
        Me.cmd_cancel.Text = "&Cancel"
        Me.cmd_cancel.UseVisualStyleBackColor = False
        '
        'Txt_Totamt
        '
        Me.Txt_Totamt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Txt_Totamt.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Totamt.Location = New System.Drawing.Point(455, 476)
        Me.Txt_Totamt.Name = "Txt_Totamt"
        Me.Txt_Totamt.ReadOnly = True
        Me.Txt_Totamt.Size = New System.Drawing.Size(114, 15)
        Me.Txt_Totamt.TabIndex = 148
        Me.Txt_Totamt.Text = "0.00"
        Me.Txt_Totamt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(376, 475)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 16)
        Me.Label9.TabIndex = 147
        Me.Label9.Text = "Total"
        '
        'c_code
        '
        Me.c_code.HeaderText = "Code"
        Me.c_code.Name = "c_code"
        '
        'c_party
        '
        Me.c_party.HeaderText = "Name"
        Me.c_party.Name = "c_party"
        Me.c_party.Width = 350
        '
        'c_partyid
        '
        Me.c_partyid.HeaderText = "Partyid"
        Me.c_partyid.Name = "c_partyid"
        Me.c_partyid.Visible = False
        '
        'c_amount
        '
        Me.c_amount.HeaderText = "Amount"
        Me.c_amount.Name = "c_amount"
        '
        'c_adless
        '
        Me.c_adless.HeaderText = "Cash/Cheque"
        Me.c_adless.Name = "c_adless"
        Me.c_adless.Width = 320
        '
        'c_accid
        '
        Me.c_accid.HeaderText = "Accid"
        Me.c_accid.Name = "c_accid"
        Me.c_accid.Visible = False
        '
        'Frm_LineReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(843, 503)
        Me.Controls.Add(Me.Txt_Totamt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmd_cancel)
        Me.Controls.Add(Me.cmd_ok)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "Frm_LineReceipt"
        Me.Text = "Line Receipt"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DTP_Vchdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_line As System.Windows.Forms.TextBox
    Friend WithEvents lbl_no As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
    Friend WithEvents Txt_Totamt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents c_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_party As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_partyid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_adless As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_accid As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
