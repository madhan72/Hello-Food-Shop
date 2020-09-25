<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmmain
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel_trntype = New System.Windows.Forms.Panel()
        Me.Chklst_Trntype = New System.Windows.Forms.CheckedListBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Dtp_Fromdate = New System.Windows.Forms.DateTimePicker()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Lbl_Fromdate = New System.Windows.Forms.Label()
        Me.Dtp_Todate = New System.Windows.Forms.DateTimePicker()
        Me.Lbl_Todate = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GridView2 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cmd_Exit = New System.Windows.Forms.Button()
        Me.cmd_Print = New System.Windows.Forms.Button()
        Me.cmd_Delete = New System.Windows.Forms.Button()
        Me.cmd_Edit = New System.Windows.Forms.Button()
        Me.cmd_Add = New System.Windows.Forms.Button()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.Panel_trntype.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.Panel_trntype)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.cmd_Exit)
        Me.Panel1.Controls.Add(Me.cmd_Print)
        Me.Panel1.Controls.Add(Me.cmd_Delete)
        Me.Panel1.Controls.Add(Me.cmd_Edit)
        Me.Panel1.Controls.Add(Me.cmd_Add)
        Me.Panel1.Controls.Add(Me.GridView1)
        Me.Panel1.Location = New System.Drawing.Point(0, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1068, 556)
        Me.Panel1.TabIndex = 0
        '
        'Panel_trntype
        '
        Me.Panel_trntype.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Panel_trntype.Controls.Add(Me.Chklst_Trntype)
        Me.Panel_trntype.Location = New System.Drawing.Point(394, 39)
        Me.Panel_trntype.Name = "Panel_trntype"
        Me.Panel_trntype.Size = New System.Drawing.Size(179, 24)
        Me.Panel_trntype.TabIndex = 211
        '
        'Chklst_Trntype
        '
        Me.Chklst_Trntype.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Chklst_Trntype.CheckOnClick = True
        Me.Chklst_Trntype.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_Trntype.FormattingEnabled = True
        Me.Chklst_Trntype.Location = New System.Drawing.Point(2, 2)
        Me.Chklst_Trntype.Name = "Chklst_Trntype"
        Me.Chklst_Trntype.Size = New System.Drawing.Size(173, 121)
        Me.Chklst_Trntype.TabIndex = 4
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Dtp_Fromdate)
        Me.Panel4.Controls.Add(Me.Btn_Refresh)
        Me.Panel4.Controls.Add(Me.Lbl_Fromdate)
        Me.Panel4.Controls.Add(Me.Dtp_Todate)
        Me.Panel4.Controls.Add(Me.Lbl_Todate)
        Me.Panel4.Location = New System.Drawing.Point(779, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(285, 60)
        Me.Panel4.TabIndex = 16
        '
        'Dtp_Fromdate
        '
        Me.Dtp_Fromdate.CalendarMonthBackground = System.Drawing.Color.Gainsboro
        Me.Dtp_Fromdate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_Fromdate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Fromdate.Location = New System.Drawing.Point(81, 3)
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
        Me.Btn_Refresh.Location = New System.Drawing.Point(186, 11)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(80, 37)
        Me.Btn_Refresh.TabIndex = 15
        Me.Btn_Refresh.Text = "&Refresh"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Lbl_Fromdate
        '
        Me.Lbl_Fromdate.AutoSize = True
        Me.Lbl_Fromdate.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Fromdate.Location = New System.Drawing.Point(7, 4)
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
        Me.Dtp_Todate.Location = New System.Drawing.Point(81, 33)
        Me.Dtp_Todate.Name = "Dtp_Todate"
        Me.Dtp_Todate.Size = New System.Drawing.Size(95, 22)
        Me.Dtp_Todate.TabIndex = 14
        '
        'Lbl_Todate
        '
        Me.Lbl_Todate.AutoSize = True
        Me.Lbl_Todate.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Todate.Location = New System.Drawing.Point(7, 33)
        Me.Lbl_Todate.Name = "Lbl_Todate"
        Me.Lbl_Todate.Size = New System.Drawing.Size(54, 18)
        Me.Lbl_Todate.TabIndex = 12
        Me.Lbl_Todate.Text = "To Date"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GridView2)
        Me.Panel3.Location = New System.Drawing.Point(904, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(25, 62)
        Me.Panel3.TabIndex = 9
        '
        'GridView2
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Aqua
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.GridView2.DefaultCellStyle = DataGridViewCellStyle2
        Me.GridView2.EnableHeadersVisualStyles = False
        Me.GridView2.Location = New System.Drawing.Point(3, 0)
        Me.GridView2.Name = "GridView2"
        Me.GridView2.ReadOnly = True
        Me.GridView2.RowHeadersVisible = False
        Me.GridView2.Size = New System.Drawing.Size(297, 60)
        Me.GridView2.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_Filter)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Location = New System.Drawing.Point(10, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(380, 29)
        Me.Panel2.TabIndex = 8
        '
        'lbl_Filter
        '
        Me.lbl_Filter.AutoSize = True
        Me.lbl_Filter.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Filter.Location = New System.Drawing.Point(2, 4)
        Me.lbl_Filter.Name = "lbl_Filter"
        Me.lbl_Filter.Size = New System.Drawing.Size(41, 18)
        Me.lbl_Filter.TabIndex = 7
        Me.lbl_Filter.Text = "Filter"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(47, 3)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(328, 23)
        Me.TextBox1.TabIndex = 6
        '
        'cmd_Exit
        '
        Me.cmd_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_Exit.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Exit.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmd_Exit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Exit.Location = New System.Drawing.Point(410, 6)
        Me.cmd_Exit.Name = "cmd_Exit"
        Me.cmd_Exit.Size = New System.Drawing.Size(80, 30)
        Me.cmd_Exit.TabIndex = 5
        Me.cmd_Exit.Text = "E&xit"
        Me.cmd_Exit.UseVisualStyleBackColor = False
        '
        'cmd_Print
        '
        Me.cmd_Print.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Print.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Print.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmd_Print.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Print.Location = New System.Drawing.Point(312, 6)
        Me.cmd_Print.Name = "cmd_Print"
        Me.cmd_Print.Size = New System.Drawing.Size(80, 30)
        Me.cmd_Print.TabIndex = 4
        Me.cmd_Print.Text = "&Print"
        Me.cmd_Print.UseVisualStyleBackColor = False
        '
        'cmd_Delete
        '
        Me.cmd_Delete.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Delete.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Delete.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmd_Delete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Delete.Location = New System.Drawing.Point(211, 6)
        Me.cmd_Delete.Name = "cmd_Delete"
        Me.cmd_Delete.Size = New System.Drawing.Size(80, 30)
        Me.cmd_Delete.TabIndex = 3
        Me.cmd_Delete.Text = "&Delete"
        Me.cmd_Delete.UseVisualStyleBackColor = False
        '
        'cmd_Edit
        '
        Me.cmd_Edit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_Edit.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Edit.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmd_Edit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Edit.Location = New System.Drawing.Point(110, 6)
        Me.cmd_Edit.Name = "cmd_Edit"
        Me.cmd_Edit.Size = New System.Drawing.Size(80, 30)
        Me.cmd_Edit.TabIndex = 2
        Me.cmd_Edit.Text = "&Edit"
        Me.cmd_Edit.UseVisualStyleBackColor = False
        '
        'cmd_Add
        '
        Me.cmd_Add.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_Add.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Add.ForeColor = System.Drawing.SystemColors.InfoText
        Me.cmd_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmd_Add.Location = New System.Drawing.Point(12, 6)
        Me.cmd_Add.Name = "cmd_Add"
        Me.cmd_Add.Size = New System.Drawing.Size(80, 30)
        Me.cmd_Add.TabIndex = 0
        Me.cmd_Add.Text = "&New"
        Me.cmd_Add.UseVisualStyleBackColor = False
        '
        'GridView1
        '
        Me.GridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.ColumnHeadersHeight = 25
        Me.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridView1.Location = New System.Drawing.Point(3, 70)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.GridView1.Size = New System.Drawing.Size(1062, 483)
        Me.GridView1.TabIndex = 5
        '
        'Frmmain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 557)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "Frmmain"
        Me.Text = "Dashboard"
        Me.Panel1.ResumeLayout(False)
        Me.Panel_trntype.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents cmd_Exit As System.Windows.Forms.Button
    Friend WithEvents cmd_Print As System.Windows.Forms.Button
    Friend WithEvents cmd_Delete As System.Windows.Forms.Button
    Friend WithEvents cmd_Edit As System.Windows.Forms.Button
    Friend WithEvents cmd_Add As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Lbl_Todate As System.Windows.Forms.Label
    Friend WithEvents Lbl_Fromdate As System.Windows.Forms.Label
    Friend WithEvents Dtp_Todate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Dtp_Fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel_trntype As System.Windows.Forms.Panel
    Friend WithEvents Chklst_Trntype As System.Windows.Forms.CheckedListBox
End Class
