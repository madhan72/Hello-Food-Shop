<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_outstandingbillwise
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
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_searchplace = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cmd_exit = New System.Windows.Forms.Button()
        Me.Optsel_Place = New System.Windows.Forms.RadioButton()
        Me.ChkLst_Place = New System.Windows.Forms.CheckedListBox()
        Me.Btn_Excel = New System.Windows.Forms.Button()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.OptAll_Place = New System.Windows.Forms.RadioButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel_Group = New System.Windows.Forms.Panel()
        Me.Chklst_area = New System.Windows.Forms.CheckedListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.opt_selarea = New System.Windows.Forms.RadioButton()
        Me.opt_allarea = New System.Windows.Forms.RadioButton()
        Me.txt_searcharea = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Refresh = New System.Windows.Forms.Button()
        Me.Cbo_type = New System.Windows.Forms.ComboBox()
        Me.cmd_print = New System.Windows.Forms.Button()
        Me.Panel5.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel_Group.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(265, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 204
        Me.Label3.Text = "Party"
        '
        'txt_searchplace
        '
        Me.txt_searchplace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchplace.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchplace.Location = New System.Drawing.Point(265, 31)
        Me.txt_searchplace.Name = "txt_searchplace"
        Me.txt_searchplace.Size = New System.Drawing.Size(257, 21)
        Me.txt_searchplace.TabIndex = 205
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.lbl_Filter)
        Me.Panel5.Controls.Add(Me.TextBox1)
        Me.Panel5.Location = New System.Drawing.Point(0, 243)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(380, 29)
        Me.Panel5.TabIndex = 219
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
        'cmd_exit
        '
        Me.cmd_exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd_exit.Location = New System.Drawing.Point(975, 576)
        Me.cmd_exit.Name = "cmd_exit"
        Me.cmd_exit.Size = New System.Drawing.Size(87, 28)
        Me.cmd_exit.TabIndex = 220
        Me.cmd_exit.Text = "E&xit"
        Me.cmd_exit.UseVisualStyleBackColor = False
        '
        'Optsel_Place
        '
        Me.Optsel_Place.AutoSize = True
        Me.Optsel_Place.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Optsel_Place.Location = New System.Drawing.Point(81, 6)
        Me.Optsel_Place.Name = "Optsel_Place"
        Me.Optsel_Place.Size = New System.Drawing.Size(81, 17)
        Me.Optsel_Place.TabIndex = 3
        Me.Optsel_Place.TabStop = True
        Me.Optsel_Place.Text = "Selected"
        Me.Optsel_Place.UseVisualStyleBackColor = True
        '
        'ChkLst_Place
        '
        Me.ChkLst_Place.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ChkLst_Place.CheckOnClick = True
        Me.ChkLst_Place.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLst_Place.FormattingEnabled = True
        Me.ChkLst_Place.Location = New System.Drawing.Point(265, 54)
        Me.ChkLst_Place.Name = "ChkLst_Place"
        Me.ChkLst_Place.Size = New System.Drawing.Size(257, 180)
        Me.ChkLst_Place.TabIndex = 207
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(882, 576)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Excel.TabIndex = 218
        Me.Btn_Excel.Text = "&Excel"
        Me.Btn_Excel.UseVisualStyleBackColor = False
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(975, 576)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Exit.TabIndex = 217
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.GridView1.ColumnHeadersHeight = 35
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(0, 276)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle17
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle18
        Me.GridView1.Size = New System.Drawing.Size(1064, 297)
        Me.GridView1.TabIndex = 213
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Optsel_Place)
        Me.Panel4.Controls.Add(Me.OptAll_Place)
        Me.Panel4.Location = New System.Drawing.Point(319, 1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(164, 26)
        Me.Panel4.TabIndex = 206
        '
        'OptAll_Place
        '
        Me.OptAll_Place.AutoSize = True
        Me.OptAll_Place.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll_Place.Location = New System.Drawing.Point(16, 6)
        Me.OptAll_Place.Name = "OptAll_Place"
        Me.OptAll_Place.Size = New System.Drawing.Size(42, 17)
        Me.OptAll_Place.TabIndex = 2
        Me.OptAll_Place.TabStop = True
        Me.OptAll_Place.Text = "All"
        Me.OptAll_Place.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(775, 252)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 215
        Me.Label9.Text = "To Date"
        '
        'Dtp_ToDate
        '
        Me.Dtp_ToDate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_ToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_ToDate.Location = New System.Drawing.Point(839, 248)
        Me.Dtp_ToDate.Name = "Dtp_ToDate"
        Me.Dtp_ToDate.Size = New System.Drawing.Size(117, 21)
        Me.Dtp_ToDate.TabIndex = 214
        Me.Dtp_ToDate.Value = New Date(2018, 1, 24, 0, 0, 0, 0)
        '
        'Panel_Group
        '
        Me.Panel_Group.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Panel_Group.Controls.Add(Me.Chklst_area)
        Me.Panel_Group.Controls.Add(Me.Panel1)
        Me.Panel_Group.Controls.Add(Me.txt_searcharea)
        Me.Panel_Group.Controls.Add(Me.Label1)
        Me.Panel_Group.Controls.Add(Me.ChkLst_Place)
        Me.Panel_Group.Controls.Add(Me.Panel4)
        Me.Panel_Group.Controls.Add(Me.txt_searchplace)
        Me.Panel_Group.Controls.Add(Me.Label3)
        Me.Panel_Group.Location = New System.Drawing.Point(2, 1)
        Me.Panel_Group.Name = "Panel_Group"
        Me.Panel_Group.Size = New System.Drawing.Size(527, 238)
        Me.Panel_Group.TabIndex = 212
        '
        'Chklst_area
        '
        Me.Chklst_area.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Chklst_area.CheckOnClick = True
        Me.Chklst_area.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_area.FormattingEnabled = True
        Me.Chklst_area.Location = New System.Drawing.Point(3, 54)
        Me.Chklst_area.Name = "Chklst_area"
        Me.Chklst_area.Size = New System.Drawing.Size(257, 180)
        Me.Chklst_area.TabIndex = 211
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.opt_selarea)
        Me.Panel1.Controls.Add(Me.opt_allarea)
        Me.Panel1.Location = New System.Drawing.Point(57, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(164, 26)
        Me.Panel1.TabIndex = 210
        '
        'opt_selarea
        '
        Me.opt_selarea.AutoSize = True
        Me.opt_selarea.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_selarea.Location = New System.Drawing.Point(81, 6)
        Me.opt_selarea.Name = "opt_selarea"
        Me.opt_selarea.Size = New System.Drawing.Size(81, 17)
        Me.opt_selarea.TabIndex = 3
        Me.opt_selarea.TabStop = True
        Me.opt_selarea.Text = "Selected"
        Me.opt_selarea.UseVisualStyleBackColor = True
        '
        'opt_allarea
        '
        Me.opt_allarea.AutoSize = True
        Me.opt_allarea.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_allarea.Location = New System.Drawing.Point(16, 6)
        Me.opt_allarea.Name = "opt_allarea"
        Me.opt_allarea.Size = New System.Drawing.Size(42, 17)
        Me.opt_allarea.TabIndex = 2
        Me.opt_allarea.TabStop = True
        Me.opt_allarea.Text = "All"
        Me.opt_allarea.UseVisualStyleBackColor = True
        '
        'txt_searcharea
        '
        Me.txt_searcharea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searcharea.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searcharea.Location = New System.Drawing.Point(3, 31)
        Me.txt_searcharea.Name = "txt_searcharea"
        Me.txt_searcharea.Size = New System.Drawing.Size(257, 21)
        Me.txt_searcharea.TabIndex = 209
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 208
        Me.Label1.Text = "Area"
        '
        'Btn_Refresh
        '
        Me.Btn_Refresh.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Refresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Refresh.Location = New System.Drawing.Point(972, 241)
        Me.Btn_Refresh.Name = "Btn_Refresh"
        Me.Btn_Refresh.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Refresh.TabIndex = 216
        Me.Btn_Refresh.Text = "&Show"
        Me.Btn_Refresh.UseVisualStyleBackColor = False
        '
        'Cbo_type
        '
        Me.Cbo_type.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cbo_type.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cbo_type.FormattingEnabled = True
        Me.Cbo_type.Items.AddRange(New Object() {"AREAWISE", "BILLWISE", "PARTYWISE"})
        Me.Cbo_type.Location = New System.Drawing.Point(861, 214)
        Me.Cbo_type.Name = "Cbo_type"
        Me.Cbo_type.Size = New System.Drawing.Size(201, 21)
        Me.Cbo_type.TabIndex = 224
        '
        'cmd_print
        '
        Me.cmd_print.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_print.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_print.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd_print.Location = New System.Drawing.Point(805, 576)
        Me.cmd_print.Name = "cmd_print"
        Me.cmd_print.Size = New System.Drawing.Size(74, 28)
        Me.cmd_print.TabIndex = 225
        Me.cmd_print.Text = "&Print"
        Me.cmd_print.UseVisualStyleBackColor = False
        '
        'frm_outstandingbillwise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1066, 605)
        Me.Controls.Add(Me.cmd_print)
        Me.Controls.Add(Me.Cbo_type)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.cmd_exit)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Dtp_ToDate)
        Me.Controls.Add(Me.Panel_Group)
        Me.Controls.Add(Me.Btn_Refresh)
        Me.Name = "frm_outstandingbillwise"
        Me.Text = "Outstanding"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel_Group.ResumeLayout(False)
        Me.Panel_Group.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_searchplace As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents cmd_exit As System.Windows.Forms.Button
    Friend WithEvents Optsel_Place As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLst_Place As System.Windows.Forms.CheckedListBox
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents OptAll_Place As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Dtp_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel_Group As System.Windows.Forms.Panel
    Friend WithEvents Btn_Refresh As System.Windows.Forms.Button
    Friend WithEvents Cbo_type As System.Windows.Forms.ComboBox
    Friend WithEvents Chklst_area As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents opt_selarea As System.Windows.Forms.RadioButton
    Friend WithEvents opt_allarea As System.Windows.Forms.RadioButton
    Friend WithEvents txt_searcharea As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_print As System.Windows.Forms.Button
End Class
