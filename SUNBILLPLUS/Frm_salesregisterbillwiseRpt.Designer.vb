<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_salesregisterbillwiseRpt
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
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Txt_searchparty = New System.Windows.Forms.TextBox()
        Me.Chklst_party = New System.Windows.Forms.CheckedListBox()
        Me.opt_selparty = New System.Windows.Forms.RadioButton()
        Me.opt_allparty = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Dtp_fromdate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.DtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Btn_Excel = New System.Windows.Forms.Button()
        Me.cmd_print = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(1085, 500)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Exit.TabIndex = 206
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Txt_searchparty
        '
        Me.Txt_searchparty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_searchparty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_searchparty.Location = New System.Drawing.Point(2, 31)
        Me.Txt_searchparty.Name = "Txt_searchparty"
        Me.Txt_searchparty.Size = New System.Drawing.Size(322, 21)
        Me.Txt_searchparty.TabIndex = 5
        '
        'Chklst_party
        '
        Me.Chklst_party.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_party.CheckOnClick = True
        Me.Chklst_party.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_party.FormattingEnabled = True
        Me.Chklst_party.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_party.Name = "Chklst_party"
        Me.Chklst_party.Size = New System.Drawing.Size(322, 95)
        Me.Chklst_party.TabIndex = 4
        '
        'opt_selparty
        '
        Me.opt_selparty.AutoSize = True
        Me.opt_selparty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_selparty.Location = New System.Drawing.Point(162, 13)
        Me.opt_selparty.Name = "opt_selparty"
        Me.opt_selparty.Size = New System.Drawing.Size(81, 17)
        Me.opt_selparty.TabIndex = 3
        Me.opt_selparty.TabStop = True
        Me.opt_selparty.Text = "Selected"
        Me.opt_selparty.UseVisualStyleBackColor = True
        '
        'opt_allparty
        '
        Me.opt_allparty.AutoSize = True
        Me.opt_allparty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_allparty.Location = New System.Drawing.Point(84, 13)
        Me.opt_allparty.Name = "opt_allparty"
        Me.opt_allparty.Size = New System.Drawing.Size(42, 17)
        Me.opt_allparty.TabIndex = 2
        Me.opt_allparty.TabStop = True
        Me.opt_allparty.Text = "All"
        Me.opt_allparty.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel3.Controls.Add(Me.Txt_searchparty)
        Me.Panel3.Controls.Add(Me.Chklst_party)
        Me.Panel3.Controls.Add(Me.opt_selparty)
        Me.Panel3.Controls.Add(Me.opt_allparty)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(328, 149)
        Me.Panel3.TabIndex = 211
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Party"
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.GridView1.ColumnHeadersHeight = 35
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(-1, 183)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GridView1.Size = New System.Drawing.Size(1180, 314)
        Me.GridView1.TabIndex = 204
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Dtp_fromdate)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Btn_load)
        Me.Panel1.Controls.Add(Me.DtpToDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(-1, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1180, 153)
        Me.Panel1.TabIndex = 203
        '
        'Dtp_fromdate
        '
        Me.Dtp_fromdate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_fromdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_fromdate.Location = New System.Drawing.Point(1061, 66)
        Me.Dtp_fromdate.Name = "Dtp_fromdate"
        Me.Dtp_fromdate.Size = New System.Drawing.Size(115, 21)
        Me.Dtp_fromdate.TabIndex = 212
        Me.Dtp_fromdate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(976, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 213
        Me.Label1.Text = "From Date"
        '
        'Btn_load
        '
        Me.Btn_load.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_load.Location = New System.Drawing.Point(1087, 121)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(90, 28)
        Me.Btn_load.TabIndex = 191
        Me.Btn_load.Text = "&Show"
        Me.Btn_load.UseVisualStyleBackColor = False
        '
        'DtpToDate
        '
        Me.DtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpToDate.Location = New System.Drawing.Point(1062, 93)
        Me.DtpToDate.Name = "DtpToDate"
        Me.DtpToDate.Size = New System.Drawing.Size(115, 21)
        Me.DtpToDate.TabIndex = 189
        Me.DtpToDate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(977, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "To Date"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_Filter)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Location = New System.Drawing.Point(2, 152)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(369, 29)
        Me.Panel2.TabIndex = 202
        '
        'lbl_Filter
        '
        Me.lbl_Filter.AutoSize = True
        Me.lbl_Filter.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Filter.Location = New System.Drawing.Point(2, 7)
        Me.lbl_Filter.Name = "lbl_Filter"
        Me.lbl_Filter.Size = New System.Drawing.Size(41, 18)
        Me.lbl_Filter.TabIndex = 7
        Me.lbl_Filter.Text = "Filter"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(50, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(316, 23)
        Me.TextBox1.TabIndex = 6
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(990, 500)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Excel.TabIndex = 205
        Me.Btn_Excel.Text = "&Excel"
        Me.Btn_Excel.UseVisualStyleBackColor = False
        '
        'cmd_print
        '
        Me.cmd_print.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_print.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_print.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd_print.Location = New System.Drawing.Point(894, 500)
        Me.cmd_print.Name = "cmd_print"
        Me.cmd_print.Size = New System.Drawing.Size(90, 28)
        Me.cmd_print.TabIndex = 207
        Me.cmd_print.Text = "&Print"
        Me.cmd_print.UseVisualStyleBackColor = False
        '
        'Frm_salesregisterbillwiseRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1180, 529)
        Me.Controls.Add(Me.cmd_print)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Name = "Frm_salesregisterbillwiseRpt"
        Me.Text = "Bill Check List"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents Txt_searchparty As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_party As System.Windows.Forms.CheckedListBox
    Friend WithEvents opt_selparty As System.Windows.Forms.RadioButton
    Friend WithEvents opt_allparty As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents DtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents Dtp_fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_print As System.Windows.Forms.Button
End Class
