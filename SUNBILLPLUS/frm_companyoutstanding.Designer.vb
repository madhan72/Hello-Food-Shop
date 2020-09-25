<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_companyoutstanding
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
        Me.Btn_Excel = New System.Windows.Forms.Button()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.Dtp_ToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_searchline = New System.Windows.Forms.TextBox()
        Me.Chklst_line = New System.Windows.Forms.CheckedListBox()
        Me.Opt_SelLine = New System.Windows.Forms.RadioButton()
        Me.Opt_AllLine = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel_Group = New System.Windows.Forms.Panel()
        Me.txt_searchparty = New System.Windows.Forms.TextBox()
        Me.Chklst_Party = New System.Windows.Forms.CheckedListBox()
        Me.Opt_SelParty = New System.Windows.Forms.RadioButton()
        Me.Opt_AllParty = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_Item = New System.Windows.Forms.Panel()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Dtp_fromdate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Cmd_print = New System.Windows.Forms.Button()
        Me.Cbo_type = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel_Group.SuspendLayout()
        Me.Panel_Item.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(818, 502)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(85, 28)
        Me.Btn_Excel.TabIndex = 200
        Me.Btn_Excel.Text = "&Excel"
        Me.Btn_Excel.UseVisualStyleBackColor = False
        '
        'Btn_load
        '
        Me.Btn_load.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_load.Location = New System.Drawing.Point(895, 157)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(90, 28)
        Me.Btn_load.TabIndex = 191
        Me.Btn_load.Text = "&Show"
        Me.Btn_load.UseVisualStyleBackColor = False
        '
        'Dtp_ToDate
        '
        Me.Dtp_ToDate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_ToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_ToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_ToDate.Location = New System.Drawing.Point(868, 130)
        Me.Dtp_ToDate.Name = "Dtp_ToDate"
        Me.Dtp_ToDate.Size = New System.Drawing.Size(117, 21)
        Me.Dtp_ToDate.TabIndex = 189
        Me.Dtp_ToDate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(783, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "To Date"
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_Filter)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Location = New System.Drawing.Point(0, 153)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(369, 29)
        Me.Panel2.TabIndex = 193
        '
        'txt_searchline
        '
        Me.txt_searchline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchline.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchline.Location = New System.Drawing.Point(2, 31)
        Me.txt_searchline.Name = "txt_searchline"
        Me.txt_searchline.Size = New System.Drawing.Size(226, 21)
        Me.txt_searchline.TabIndex = 5
        '
        'Chklst_line
        '
        Me.Chklst_line.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_line.CheckOnClick = True
        Me.Chklst_line.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_line.FormattingEnabled = True
        Me.Chklst_line.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_line.Name = "Chklst_line"
        Me.Chklst_line.Size = New System.Drawing.Size(226, 95)
        Me.Chklst_line.TabIndex = 4
        '
        'Opt_SelLine
        '
        Me.Opt_SelLine.AutoSize = True
        Me.Opt_SelLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_SelLine.Location = New System.Drawing.Point(141, 13)
        Me.Opt_SelLine.Name = "Opt_SelLine"
        Me.Opt_SelLine.Size = New System.Drawing.Size(81, 17)
        Me.Opt_SelLine.TabIndex = 3
        Me.Opt_SelLine.TabStop = True
        Me.Opt_SelLine.Text = "Selected"
        Me.Opt_SelLine.UseVisualStyleBackColor = True
        '
        'Opt_AllLine
        '
        Me.Opt_AllLine.AutoSize = True
        Me.Opt_AllLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_AllLine.Location = New System.Drawing.Point(63, 13)
        Me.Opt_AllLine.Name = "Opt_AllLine"
        Me.Opt_AllLine.Size = New System.Drawing.Size(42, 17)
        Me.Opt_AllLine.TabIndex = 2
        Me.Opt_AllLine.TabStop = True
        Me.Opt_AllLine.Text = "All"
        Me.Opt_AllLine.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Line"
        '
        'Panel_Group
        '
        Me.Panel_Group.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Group.Controls.Add(Me.txt_searchline)
        Me.Panel_Group.Controls.Add(Me.Chklst_line)
        Me.Panel_Group.Controls.Add(Me.Opt_SelLine)
        Me.Panel_Group.Controls.Add(Me.Opt_AllLine)
        Me.Panel_Group.Controls.Add(Me.Label4)
        Me.Panel_Group.Location = New System.Drawing.Point(3, 3)
        Me.Panel_Group.Name = "Panel_Group"
        Me.Panel_Group.Size = New System.Drawing.Size(229, 149)
        Me.Panel_Group.TabIndex = 209
        '
        'txt_searchparty
        '
        Me.txt_searchparty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchparty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchparty.Location = New System.Drawing.Point(2, 31)
        Me.txt_searchparty.Name = "txt_searchparty"
        Me.txt_searchparty.Size = New System.Drawing.Size(336, 21)
        Me.txt_searchparty.TabIndex = 5
        '
        'Chklst_Party
        '
        Me.Chklst_Party.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_Party.CheckOnClick = True
        Me.Chklst_Party.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_Party.FormattingEnabled = True
        Me.Chklst_Party.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_Party.Name = "Chklst_Party"
        Me.Chklst_Party.Size = New System.Drawing.Size(336, 89)
        Me.Chklst_Party.TabIndex = 4
        '
        'Opt_SelParty
        '
        Me.Opt_SelParty.AutoSize = True
        Me.Opt_SelParty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_SelParty.Location = New System.Drawing.Point(141, 13)
        Me.Opt_SelParty.Name = "Opt_SelParty"
        Me.Opt_SelParty.Size = New System.Drawing.Size(81, 17)
        Me.Opt_SelParty.TabIndex = 3
        Me.Opt_SelParty.TabStop = True
        Me.Opt_SelParty.Text = "Selected"
        Me.Opt_SelParty.UseVisualStyleBackColor = True
        '
        'Opt_AllParty
        '
        Me.Opt_AllParty.AutoSize = True
        Me.Opt_AllParty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_AllParty.Location = New System.Drawing.Point(63, 13)
        Me.Opt_AllParty.Name = "Opt_AllParty"
        Me.Opt_AllParty.Size = New System.Drawing.Size(42, 17)
        Me.Opt_AllParty.TabIndex = 2
        Me.Opt_AllParty.TabStop = True
        Me.Opt_AllParty.Text = "All"
        Me.Opt_AllParty.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Party"
        '
        'Panel_Item
        '
        Me.Panel_Item.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Item.Controls.Add(Me.txt_searchparty)
        Me.Panel_Item.Controls.Add(Me.Chklst_Party)
        Me.Panel_Item.Controls.Add(Me.Opt_SelParty)
        Me.Panel_Item.Controls.Add(Me.Opt_AllParty)
        Me.Panel_Item.Controls.Add(Me.Label1)
        Me.Panel_Item.Location = New System.Drawing.Point(234, 4)
        Me.Panel_Item.Name = "Panel_Item"
        Me.Panel_Item.Size = New System.Drawing.Size(341, 149)
        Me.Panel_Item.TabIndex = 210
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView1.ColumnHeadersHeight = 35
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(0, 187)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold)
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.Size = New System.Drawing.Size(985, 313)
        Me.GridView1.TabIndex = 199
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Panel_Item)
        Me.Panel1.Controls.Add(Me.Panel_Group)
        Me.Panel1.Location = New System.Drawing.Point(0, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(992, 153)
        Me.Panel1.TabIndex = 198
        '
        'Dtp_fromdate
        '
        Me.Dtp_fromdate.CustomFormat = "dd/MM/yyyy"
        Me.Dtp_fromdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_fromdate.Location = New System.Drawing.Point(868, 103)
        Me.Dtp_fromdate.Name = "Dtp_fromdate"
        Me.Dtp_fromdate.Size = New System.Drawing.Size(117, 21)
        Me.Dtp_fromdate.TabIndex = 211
        Me.Dtp_fromdate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(783, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 212
        Me.Label3.Text = "From Date"
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(907, 502)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(78, 28)
        Me.Btn_Exit.TabIndex = 201
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Cmd_print
        '
        Me.Cmd_print.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Cmd_print.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Cmd_print.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmd_print.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Cmd_print.Location = New System.Drawing.Point(729, 502)
        Me.Cmd_print.Name = "Cmd_print"
        Me.Cmd_print.Size = New System.Drawing.Size(85, 28)
        Me.Cmd_print.TabIndex = 202
        Me.Cmd_print.Text = "&Print"
        Me.Cmd_print.UseVisualStyleBackColor = False
        '
        'Cbo_type
        '
        Me.Cbo_type.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Cbo_type.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cbo_type.FormattingEnabled = True
        Me.Cbo_type.Items.AddRange(New Object() {"Closing only", "All", "Bill"})
        Me.Cbo_type.Location = New System.Drawing.Point(612, 505)
        Me.Cbo_type.Name = "Cbo_type"
        Me.Cbo_type.Size = New System.Drawing.Size(111, 23)
        Me.Cbo_type.TabIndex = 203
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(567, 508)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 204
        Me.Label5.Text = "Type"
        '
        'frm_companyoutstanding
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(986, 532)
        Me.Controls.Add(Me.Dtp_fromdate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Cbo_type)
        Me.Controls.Add(Me.Cmd_print)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Controls.Add(Me.Btn_load)
        Me.Controls.Add(Me.Dtp_ToDate)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Name = "frm_companyoutstanding"
        Me.Text = "Company Outstanding"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel_Group.ResumeLayout(False)
        Me.Panel_Group.PerformLayout()
        Me.Panel_Item.ResumeLayout(False)
        Me.Panel_Item.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents Dtp_ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txt_searchline As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_line As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_SelLine As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_AllLine As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel_Group As System.Windows.Forms.Panel
    Friend WithEvents txt_searchparty As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_Party As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_SelParty As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_AllParty As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel_Item As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents Dtp_fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Cmd_print As System.Windows.Forms.Button
    Friend WithEvents Cbo_type As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
