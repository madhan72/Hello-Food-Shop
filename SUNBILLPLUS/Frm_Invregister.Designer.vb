<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Invregister
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Btn_Print = New System.Windows.Forms.Button()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Txt_searchloc = New System.Windows.Forms.TextBox()
        Me.Chklst_line = New System.Windows.Forms.CheckedListBox()
        Me.opt_selLine = New System.Windows.Forms.RadioButton()
        Me.opt_allLine = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Dtp_Fromdate = New System.Windows.Forms.DateTimePicker()
        Me.Panel_Item = New System.Windows.Forms.Panel()
        Me.txt_searchitem = New System.Windows.Forms.TextBox()
        Me.Chklst_Party = New System.Windows.Forms.CheckedListBox()
        Me.Opt_SelParty = New System.Windows.Forms.RadioButton()
        Me.Opt_AllParty = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_Group = New System.Windows.Forms.Panel()
        Me.txt_searchgrp = New System.Windows.Forms.TextBox()
        Me.Chklst_Grp = New System.Windows.Forms.CheckedListBox()
        Me.Opt_Selgrp = New System.Windows.Forms.RadioButton()
        Me.Opt_Allgrp = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.DtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.txt_Search = New System.Windows.Forms.TextBox()
        Me.Btn_Excel = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel_Item.SuspendLayout()
        Me.Panel_Group.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Print)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1148, 579)
        Me.Panel1.TabIndex = 0
        '
        'Btn_Print
        '
        Me.Btn_Print.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Print.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Print.Location = New System.Drawing.Point(870, 547)
        Me.Btn_Print.Name = "Btn_Print"
        Me.Btn_Print.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Print.TabIndex = 0
        Me.Btn_Print.Text = "&Print"
        Me.Btn_Print.UseVisualStyleBackColor = False
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(1054, 547)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Exit.TabIndex = 206
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Txt_searchloc
        '
        Me.Txt_searchloc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_searchloc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_searchloc.Location = New System.Drawing.Point(2, 31)
        Me.Txt_searchloc.Name = "Txt_searchloc"
        Me.Txt_searchloc.Size = New System.Drawing.Size(226, 21)
        Me.Txt_searchloc.TabIndex = 5
        '
        'Chklst_line
        '
        Me.Chklst_line.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_line.CheckOnClick = True
        Me.Chklst_line.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_line.FormattingEnabled = True
        Me.Chklst_line.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_line.Name = "Chklst_line"
        Me.Chklst_line.Size = New System.Drawing.Size(226, 123)
        Me.Chklst_line.TabIndex = 4
        '
        'opt_selLine
        '
        Me.opt_selLine.AutoSize = True
        Me.opt_selLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_selLine.Location = New System.Drawing.Point(115, 11)
        Me.opt_selLine.Name = "opt_selLine"
        Me.opt_selLine.Size = New System.Drawing.Size(81, 17)
        Me.opt_selLine.TabIndex = 3
        Me.opt_selLine.TabStop = True
        Me.opt_selLine.Text = "Selected"
        Me.opt_selLine.UseVisualStyleBackColor = True
        '
        'opt_allLine
        '
        Me.opt_allLine.AutoSize = True
        Me.opt_allLine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_allLine.Location = New System.Drawing.Point(50, 11)
        Me.opt_allLine.Name = "opt_allLine"
        Me.opt_allLine.Size = New System.Drawing.Size(42, 17)
        Me.opt_allLine.TabIndex = 2
        Me.opt_allLine.TabStop = True
        Me.opt_allLine.Text = "All"
        Me.opt_allLine.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel3.Controls.Add(Me.Txt_searchloc)
        Me.Panel3.Controls.Add(Me.Chklst_line)
        Me.Panel3.Controls.Add(Me.opt_selLine)
        Me.Panel3.Controls.Add(Me.opt_allLine)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(229, 178)
        Me.Panel3.TabIndex = 211
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Line"
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
        Me.GridView1.Location = New System.Drawing.Point(2, 213)
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
        Me.GridView1.Size = New System.Drawing.Size(1144, 331)
        Me.GridView1.TabIndex = 204
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Dtp_Fromdate)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Controls.Add(Me.Panel_Item)
        Me.Panel2.Controls.Add(Me.Panel_Group)
        Me.Panel2.Controls.Add(Me.Btn_load)
        Me.Panel2.Controls.Add(Me.DtpToDate)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(-1, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1146, 183)
        Me.Panel2.TabIndex = 203
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(867, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 213
        Me.Label5.Text = "From Date"
        '
        'Dtp_Fromdate
        '
        Me.Dtp_Fromdate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.Dtp_Fromdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtp_Fromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.Dtp_Fromdate.Location = New System.Drawing.Point(950, 33)
        Me.Dtp_Fromdate.Name = "Dtp_Fromdate"
        Me.Dtp_Fromdate.Size = New System.Drawing.Size(186, 21)
        Me.Dtp_Fromdate.TabIndex = 212
        '
        'Panel_Item
        '
        Me.Panel_Item.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Item.Controls.Add(Me.txt_searchitem)
        Me.Panel_Item.Controls.Add(Me.Chklst_Party)
        Me.Panel_Item.Controls.Add(Me.Opt_SelParty)
        Me.Panel_Item.Controls.Add(Me.Opt_AllParty)
        Me.Panel_Item.Controls.Add(Me.Label1)
        Me.Panel_Item.Location = New System.Drawing.Point(234, 2)
        Me.Panel_Item.Name = "Panel_Item"
        Me.Panel_Item.Size = New System.Drawing.Size(341, 178)
        Me.Panel_Item.TabIndex = 210
        '
        'txt_searchitem
        '
        Me.txt_searchitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchitem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchitem.Location = New System.Drawing.Point(2, 31)
        Me.txt_searchitem.Name = "txt_searchitem"
        Me.txt_searchitem.Size = New System.Drawing.Size(336, 21)
        Me.txt_searchitem.TabIndex = 5
        '
        'Chklst_Party
        '
        Me.Chklst_Party.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_Party.CheckOnClick = True
        Me.Chklst_Party.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_Party.FormattingEnabled = True
        Me.Chklst_Party.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_Party.Name = "Chklst_Party"
        Me.Chklst_Party.Size = New System.Drawing.Size(336, 123)
        Me.Chklst_Party.TabIndex = 4
        '
        'Opt_SelParty
        '
        Me.Opt_SelParty.AutoSize = True
        Me.Opt_SelParty.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_SelParty.Location = New System.Drawing.Point(141, 11)
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
        Me.Opt_AllParty.Location = New System.Drawing.Point(63, 11)
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
        'Panel_Group
        '
        Me.Panel_Group.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Group.Controls.Add(Me.txt_searchgrp)
        Me.Panel_Group.Controls.Add(Me.Chklst_Grp)
        Me.Panel_Group.Controls.Add(Me.Opt_Selgrp)
        Me.Panel_Group.Controls.Add(Me.Opt_Allgrp)
        Me.Panel_Group.Controls.Add(Me.Label4)
        Me.Panel_Group.Location = New System.Drawing.Point(577, 2)
        Me.Panel_Group.Name = "Panel_Group"
        Me.Panel_Group.Size = New System.Drawing.Size(229, 178)
        Me.Panel_Group.TabIndex = 209
        '
        'txt_searchgrp
        '
        Me.txt_searchgrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchgrp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchgrp.Location = New System.Drawing.Point(2, 31)
        Me.txt_searchgrp.Name = "txt_searchgrp"
        Me.txt_searchgrp.Size = New System.Drawing.Size(226, 21)
        Me.txt_searchgrp.TabIndex = 5
        '
        'Chklst_Grp
        '
        Me.Chklst_Grp.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_Grp.CheckOnClick = True
        Me.Chklst_Grp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Chklst_Grp.FormattingEnabled = True
        Me.Chklst_Grp.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_Grp.Name = "Chklst_Grp"
        Me.Chklst_Grp.Size = New System.Drawing.Size(226, 123)
        Me.Chklst_Grp.TabIndex = 4
        '
        'Opt_Selgrp
        '
        Me.Opt_Selgrp.AutoSize = True
        Me.Opt_Selgrp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_Selgrp.Location = New System.Drawing.Point(141, 11)
        Me.Opt_Selgrp.Name = "Opt_Selgrp"
        Me.Opt_Selgrp.Size = New System.Drawing.Size(81, 17)
        Me.Opt_Selgrp.TabIndex = 3
        Me.Opt_Selgrp.TabStop = True
        Me.Opt_Selgrp.Text = "Selected"
        Me.Opt_Selgrp.UseVisualStyleBackColor = True
        '
        'Opt_Allgrp
        '
        Me.Opt_Allgrp.AutoSize = True
        Me.Opt_Allgrp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_Allgrp.Location = New System.Drawing.Point(63, 11)
        Me.Opt_Allgrp.Name = "Opt_Allgrp"
        Me.Opt_Allgrp.Size = New System.Drawing.Size(42, 17)
        Me.Opt_Allgrp.TabIndex = 2
        Me.Opt_Allgrp.TabStop = True
        Me.Opt_Allgrp.Text = "All"
        Me.Opt_Allgrp.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(2, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Group"
        '
        'Btn_load
        '
        Me.Btn_load.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_load.Location = New System.Drawing.Point(1052, 121)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(90, 28)
        Me.Btn_load.TabIndex = 191
        Me.Btn_load.Text = "&Show"
        Me.Btn_load.UseVisualStyleBackColor = False
        '
        'DtpToDate
        '
        Me.DtpToDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Me.DtpToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpToDate.Location = New System.Drawing.Point(950, 60)
        Me.DtpToDate.Name = "DtpToDate"
        Me.DtpToDate.Size = New System.Drawing.Size(186, 21)
        Me.DtpToDate.TabIndex = 189
        Me.DtpToDate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(867, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "To Date"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.lbl_Filter)
        Me.Panel4.Controls.Add(Me.txt_Search)
        Me.Panel4.Location = New System.Drawing.Point(2, 183)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(409, 26)
        Me.Panel4.TabIndex = 202
        '
        'lbl_Filter
        '
        Me.lbl_Filter.AutoSize = True
        Me.lbl_Filter.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Filter.Location = New System.Drawing.Point(2, 5)
        Me.lbl_Filter.Name = "lbl_Filter"
        Me.lbl_Filter.Size = New System.Drawing.Size(41, 18)
        Me.lbl_Filter.TabIndex = 7
        Me.lbl_Filter.Text = "Filter"
        '
        'txt_Search
        '
        Me.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Search.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Search.Location = New System.Drawing.Point(50, 2)
        Me.txt_Search.Name = "txt_Search"
        Me.txt_Search.Size = New System.Drawing.Size(316, 22)
        Me.txt_Search.TabIndex = 6
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(962, 547)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Excel.TabIndex = 205
        Me.Btn_Excel.Text = "&Excel"
        Me.Btn_Excel.UseVisualStyleBackColor = False
        '
        'Frm_Invregister
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1148, 579)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.Name = "Frm_Invregister"
        Me.Text = "Invoice Register"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel_Item.ResumeLayout(False)
        Me.Panel_Item.PerformLayout()
        Me.Panel_Group.ResumeLayout(False)
        Me.Panel_Group.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents Txt_searchloc As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_line As System.Windows.Forms.CheckedListBox
    Friend WithEvents opt_selLine As System.Windows.Forms.RadioButton
    Friend WithEvents opt_allLine As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel_Item As System.Windows.Forms.Panel
    Friend WithEvents txt_searchitem As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_Party As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_SelParty As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_AllParty As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel_Group As System.Windows.Forms.Panel
    Friend WithEvents txt_searchgrp As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_Grp As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_Selgrp As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_Allgrp As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents DtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents txt_Search As System.Windows.Forms.TextBox
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents Dtp_Fromdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Btn_Print As System.Windows.Forms.Button
End Class
