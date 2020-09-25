<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_quotaionbilled
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Dtp_Fromdate = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Lbl_Fromdate = New System.Windows.Forms.Label()
        Me.Dtp_Todate = New System.Windows.Forms.DateTimePicker()
        Me.Lbl_Todate = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txt_searchType = New System.Windows.Forms.TextBox()
        Me.chklst_type = New System.Windows.Forms.CheckedListBox()
        Me.opt_seltype = New System.Windows.Forms.RadioButton()
        Me.opt_alltype = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.cmd_cancel = New System.Windows.Forms.Button()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.c_headerid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_quono = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_quodate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_party = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_value = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_createdby = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_bill = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.c_billuser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Dtp_Fromdate)
        Me.Panel4.Controls.Add(Me.Btn_Refresh)
        Me.Panel4.Controls.Add(Me.Lbl_Fromdate)
        Me.Panel4.Controls.Add(Me.Dtp_Todate)
        Me.Panel4.Controls.Add(Me.Lbl_Todate)
        Me.Panel4.Location = New System.Drawing.Point(893, 53)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(178, 91)
        Me.Panel4.TabIndex = 16
        '
        'Dtp_Fromdate
        '
        Me.Dtp_Fromdate.CalendarMonthBackground = System.Drawing.Color.Gainsboro
        Me.Dtp_Fromdate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_Fromdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Fromdate.Location = New System.Drawing.Point(75, 5)
        Me.Dtp_Fromdate.Name = "Dtp_Fromdate"
        Me.Dtp_Fromdate.Size = New System.Drawing.Size(95, 22)
        Me.Dtp_Fromdate.TabIndex = 13
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.Turquoise
        Me.Btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Refresh.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.SystemColors.InfoText
        Me.Btn_Refresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Refresh.Location = New System.Drawing.Point(90, 61)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(80, 26)
        Me.Btn_Refresh.TabIndex = 15
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Lbl_Fromdate
        '
        Me.Lbl_Fromdate.AutoSize = True
        Me.Lbl_Fromdate.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Fromdate.Location = New System.Drawing.Point(1, 7)
        Me.Lbl_Fromdate.Name = "Lbl_Fromdate"
        Me.Lbl_Fromdate.Size = New System.Drawing.Size(71, 18)
        Me.Lbl_Fromdate.TabIndex = 11
        Me.Lbl_Fromdate.Text = "From date"
        '
        'Dtp_Todate
        '
        Me.Dtp_Todate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_Todate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Todate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Todate.Location = New System.Drawing.Point(75, 33)
        Me.Dtp_Todate.Name = "Dtp_Todate"
        Me.Dtp_Todate.Size = New System.Drawing.Size(95, 22)
        Me.Dtp_Todate.TabIndex = 14
        '
        'Lbl_Todate
        '
        Me.Lbl_Todate.AutoSize = True
        Me.Lbl_Todate.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Todate.Location = New System.Drawing.Point(1, 35)
        Me.Lbl_Todate.Name = "Lbl_Todate"
        Me.Lbl_Todate.Size = New System.Drawing.Size(54, 18)
        Me.Lbl_Todate.TabIndex = 12
        Me.Lbl_Todate.Text = "To Date"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.GridView1)
        Me.Panel1.Controls.Add(Me.cmd_cancel)
        Me.Panel1.Controls.Add(Me.cmd_ok)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Location = New System.Drawing.Point(2, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1068, 547)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_Filter)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Location = New System.Drawing.Point(212, 114)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(319, 29)
        Me.Panel2.TabIndex = 197
        Me.Panel2.Visible = False
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
        Me.TextBox1.Size = New System.Drawing.Size(267, 23)
        Me.TextBox1.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel3.Controls.Add(Me.txt_searchType)
        Me.Panel3.Controls.Add(Me.chklst_type)
        Me.Panel3.Controls.Add(Me.opt_seltype)
        Me.Panel3.Controls.Add(Me.opt_alltype)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(3, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(205, 142)
        Me.Panel3.TabIndex = 196
        '
        'txt_searchType
        '
        Me.txt_searchType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchType.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchType.Location = New System.Drawing.Point(2, 23)
        Me.txt_searchType.Name = "txt_searchType"
        Me.txt_searchType.Size = New System.Drawing.Size(201, 21)
        Me.txt_searchType.TabIndex = 5
        '
        'chklst_type
        '
        Me.chklst_type.BackColor = System.Drawing.Color.SkyBlue
        Me.chklst_type.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chklst_type.FormattingEnabled = True
        Me.chklst_type.Items.AddRange(New Object() {"Pending", "Billed", "Closed", "Confirmed"})
        Me.chklst_type.Location = New System.Drawing.Point(2, 45)
        Me.chklst_type.Name = "chklst_type"
        Me.chklst_type.Size = New System.Drawing.Size(201, 95)
        Me.chklst_type.TabIndex = 4
        '
        'opt_seltype
        '
        Me.opt_seltype.AutoSize = True
        Me.opt_seltype.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_seltype.Location = New System.Drawing.Point(120, 5)
        Me.opt_seltype.Name = "opt_seltype"
        Me.opt_seltype.Size = New System.Drawing.Size(81, 17)
        Me.opt_seltype.TabIndex = 3
        Me.opt_seltype.TabStop = True
        Me.opt_seltype.Text = "Selected"
        Me.opt_seltype.UseVisualStyleBackColor = True
        '
        'opt_alltype
        '
        Me.opt_alltype.AutoSize = True
        Me.opt_alltype.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_alltype.Location = New System.Drawing.Point(66, 5)
        Me.opt_alltype.Name = "opt_alltype"
        Me.opt_alltype.Size = New System.Drawing.Size(42, 17)
        Me.opt_alltype.TabIndex = 2
        Me.opt_alltype.TabStop = True
        Me.opt_alltype.Text = "All"
        Me.opt_alltype.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Type"
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
        Me.GridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.c_headerid, Me.c_quono, Me.c_quodate, Me.c_party, Me.c_value, Me.c_createdby, Me.c_bill, Me.c_billuser})
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(3, 145)
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
        Me.GridView1.RowHeadersWidth = 40
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.Size = New System.Drawing.Size(1060, 368)
        Me.GridView1.TabIndex = 145
        '
        'cmd_cancel
        '
        Me.cmd_cancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_cancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancel.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_cancel.Location = New System.Drawing.Point(975, 517)
        Me.cmd_cancel.Name = "cmd_cancel"
        Me.cmd_cancel.Size = New System.Drawing.Size(90, 28)
        Me.cmd_cancel.TabIndex = 144
        Me.cmd_cancel.Text = "&Cancel"
        Me.cmd_cancel.UseVisualStyleBackColor = False
        '
        'cmd_ok
        '
        Me.cmd_ok.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ok.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ok.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_ok.Location = New System.Drawing.Point(884, 517)
        Me.cmd_ok.Name = "cmd_ok"
        Me.cmd_ok.Size = New System.Drawing.Size(90, 28)
        Me.cmd_ok.TabIndex = 143
        Me.cmd_ok.Text = "&Save"
        Me.cmd_ok.UseVisualStyleBackColor = False
        '
        'c_headerid
        '
        Me.c_headerid.HeaderText = "Headerid"
        Me.c_headerid.Name = "c_headerid"
        Me.c_headerid.Visible = False
        '
        'c_quono
        '
        Me.c_quono.HeaderText = "Quo No"
        Me.c_quono.Name = "c_quono"
        '
        'c_quodate
        '
        Me.c_quodate.HeaderText = "Quo Date"
        Me.c_quodate.Name = "c_quodate"
        '
        'c_party
        '
        Me.c_party.HeaderText = "Party"
        Me.c_party.Name = "c_party"
        Me.c_party.Width = 300
        '
        'c_value
        '
        Me.c_value.HeaderText = "Value"
        Me.c_value.Name = "c_value"
        '
        'c_createdby
        '
        Me.c_createdby.HeaderText = "Createdby"
        Me.c_createdby.Name = "c_createdby"
        '
        'c_bill
        '
        Me.c_bill.HeaderText = "Type"
        Me.c_bill.Items.AddRange(New Object() {"Pending", "Billed", "Closed", "Confirmed"})
        Me.c_bill.Name = "c_bill"
        Me.c_bill.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.c_bill.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'c_billuser
        '
        Me.c_billuser.HeaderText = "Billed User"
        Me.c_billuser.Name = "c_billuser"
        '
        'Frm_quotaionbilled
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1068, 547)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_quotaionbilled"
        Me.Text = "Quotation Update"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Dtp_Fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Lbl_Fromdate As System.Windows.Forms.Label
    Friend WithEvents Dtp_Todate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Lbl_Todate As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents txt_searchType As System.Windows.Forms.TextBox
    Friend WithEvents chklst_type As System.Windows.Forms.CheckedListBox
    Friend WithEvents opt_seltype As System.Windows.Forms.RadioButton
    Friend WithEvents opt_alltype As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents c_headerid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_quono As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_quodate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_party As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_value As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_createdby As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_bill As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents c_billuser As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
