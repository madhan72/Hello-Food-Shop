<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Stock
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
        Me.DtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_searchgrp = New System.Windows.Forms.TextBox()
        Me.Chklst_Grp = New System.Windows.Forms.CheckedListBox()
        Me.Opt_Selgrp = New System.Windows.Forms.RadioButton()
        Me.Opt_Allgrp = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel_Group = New System.Windows.Forms.Panel()
        Me.txt_searchitem = New System.Windows.Forms.TextBox()
        Me.Chklst_Item = New System.Windows.Forms.CheckedListBox()
        Me.Opt_Selitem = New System.Windows.Forms.RadioButton()
        Me.Opt_AllItem = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_Item = New System.Windows.Forms.Panel()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chk_reorder = New System.Windows.Forms.CheckBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Txt_searchcode = New System.Windows.Forms.TextBox()
        Me.Chklst_code = New System.Windows.Forms.CheckedListBox()
        Me.opt_selcode = New System.Windows.Forms.RadioButton()
        Me.opt_allcode = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Txt_searchloc = New System.Windows.Forms.TextBox()
        Me.Chklst_loc = New System.Windows.Forms.CheckedListBox()
        Me.opt_selloc = New System.Windows.Forms.RadioButton()
        Me.opt_allloc = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.Panel_Group.SuspendLayout()
        Me.Panel_Item.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(989, 502)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(90, 28)
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
        Me.Btn_load.Location = New System.Drawing.Point(1087, 119)
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
        Me.DtpToDate.Location = New System.Drawing.Point(1062, 94)
        Me.DtpToDate.Name = "DtpToDate"
        Me.DtpToDate.Size = New System.Drawing.Size(115, 21)
        Me.DtpToDate.TabIndex = 189
        Me.DtpToDate.Value = New Date(2016, 8, 29, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(977, 98)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 13)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "As on Date"
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
        Me.Panel2.Location = New System.Drawing.Point(3, 157)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(369, 29)
        Me.Panel2.TabIndex = 193
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
        Me.Chklst_Grp.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_Grp.FormattingEnabled = True
        Me.Chklst_Grp.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_Grp.Name = "Chklst_Grp"
        Me.Chklst_Grp.Size = New System.Drawing.Size(226, 95)
        Me.Chklst_Grp.TabIndex = 4
        '
        'Opt_Selgrp
        '
        Me.Opt_Selgrp.AutoSize = True
        Me.Opt_Selgrp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_Selgrp.Location = New System.Drawing.Point(141, 13)
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
        Me.Opt_Allgrp.Location = New System.Drawing.Point(63, 13)
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
        Me.Label4.Location = New System.Drawing.Point(2, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Group"
        '
        'Panel_Group
        '
        Me.Panel_Group.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Group.Controls.Add(Me.txt_searchgrp)
        Me.Panel_Group.Controls.Add(Me.Chklst_Grp)
        Me.Panel_Group.Controls.Add(Me.Opt_Selgrp)
        Me.Panel_Group.Controls.Add(Me.Opt_Allgrp)
        Me.Panel_Group.Controls.Add(Me.Label4)
        Me.Panel_Group.Location = New System.Drawing.Point(234, 3)
        Me.Panel_Group.Name = "Panel_Group"
        Me.Panel_Group.Size = New System.Drawing.Size(229, 149)
        Me.Panel_Group.TabIndex = 209
        '
        'txt_searchitem
        '
        Me.txt_searchitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_searchitem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_searchitem.Location = New System.Drawing.Point(2, 31)
        Me.txt_searchitem.Name = "txt_searchitem"
        Me.txt_searchitem.Size = New System.Drawing.Size(330, 21)
        Me.txt_searchitem.TabIndex = 5
        '
        'Chklst_Item
        '
        Me.Chklst_Item.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_Item.CheckOnClick = True
        Me.Chklst_Item.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_Item.FormattingEnabled = True
        Me.Chklst_Item.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_Item.Name = "Chklst_Item"
        Me.Chklst_Item.Size = New System.Drawing.Size(330, 95)
        Me.Chklst_Item.TabIndex = 4
        '
        'Opt_Selitem
        '
        Me.Opt_Selitem.AutoSize = True
        Me.Opt_Selitem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_Selitem.Location = New System.Drawing.Point(141, 13)
        Me.Opt_Selitem.Name = "Opt_Selitem"
        Me.Opt_Selitem.Size = New System.Drawing.Size(81, 17)
        Me.Opt_Selitem.TabIndex = 3
        Me.Opt_Selitem.TabStop = True
        Me.Opt_Selitem.Text = "Selected"
        Me.Opt_Selitem.UseVisualStyleBackColor = True
        '
        'Opt_AllItem
        '
        Me.Opt_AllItem.AutoSize = True
        Me.Opt_AllItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Opt_AllItem.Location = New System.Drawing.Point(63, 13)
        Me.Opt_AllItem.Name = "Opt_AllItem"
        Me.Opt_AllItem.Size = New System.Drawing.Size(42, 17)
        Me.Opt_AllItem.TabIndex = 2
        Me.Opt_AllItem.TabStop = True
        Me.Opt_AllItem.Text = "All"
        Me.Opt_AllItem.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(2, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item"
        '
        'Panel_Item
        '
        Me.Panel_Item.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel_Item.Controls.Add(Me.txt_searchitem)
        Me.Panel_Item.Controls.Add(Me.Chklst_Item)
        Me.Panel_Item.Controls.Add(Me.Opt_Selitem)
        Me.Panel_Item.Controls.Add(Me.Opt_AllItem)
        Me.Panel_Item.Controls.Add(Me.Label1)
        Me.Panel_Item.Location = New System.Drawing.Point(640, 3)
        Me.Panel_Item.Name = "Panel_Item"
        Me.Panel_Item.Size = New System.Drawing.Size(333, 149)
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
        Me.GridView1.Location = New System.Drawing.Point(0, 185)
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
        Me.GridView1.Size = New System.Drawing.Size(1180, 314)
        Me.GridView1.TabIndex = 199
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.chk_reorder)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel_Item)
        Me.Panel1.Controls.Add(Me.Panel_Group)
        Me.Panel1.Controls.Add(Me.Btn_load)
        Me.Panel1.Controls.Add(Me.DtpToDate)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1180, 153)
        Me.Panel1.TabIndex = 198
        '
        'chk_reorder
        '
        Me.chk_reorder.AutoSize = True
        Me.chk_reorder.BackColor = System.Drawing.SystemColors.Highlight
        Me.chk_reorder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_reorder.Location = New System.Drawing.Point(1068, 71)
        Me.chk_reorder.Name = "chk_reorder"
        Me.chk_reorder.Size = New System.Drawing.Size(106, 17)
        Me.chk_reorder.TabIndex = 202
        Me.chk_reorder.Text = "Reorder Level"
        Me.chk_reorder.UseVisualStyleBackColor = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel5.Controls.Add(Me.Txt_searchcode)
        Me.Panel5.Controls.Add(Me.Chklst_code)
        Me.Panel5.Controls.Add(Me.opt_selcode)
        Me.Panel5.Controls.Add(Me.opt_allcode)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Location = New System.Drawing.Point(463, 2)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(176, 149)
        Me.Panel5.TabIndex = 212
        '
        'Txt_searchcode
        '
        Me.Txt_searchcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txt_searchcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_searchcode.Location = New System.Drawing.Point(2, 31)
        Me.Txt_searchcode.Name = "Txt_searchcode"
        Me.Txt_searchcode.Size = New System.Drawing.Size(172, 21)
        Me.Txt_searchcode.TabIndex = 5
        '
        'Chklst_code
        '
        Me.Chklst_code.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_code.CheckOnClick = True
        Me.Chklst_code.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_code.FormattingEnabled = True
        Me.Chklst_code.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_code.Name = "Chklst_code"
        Me.Chklst_code.Size = New System.Drawing.Size(173, 95)
        Me.Chklst_code.TabIndex = 4
        '
        'opt_selcode
        '
        Me.opt_selcode.AutoSize = True
        Me.opt_selcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_selcode.Location = New System.Drawing.Point(87, 13)
        Me.opt_selcode.Name = "opt_selcode"
        Me.opt_selcode.Size = New System.Drawing.Size(81, 17)
        Me.opt_selcode.TabIndex = 3
        Me.opt_selcode.TabStop = True
        Me.opt_selcode.Text = "Selected"
        Me.opt_selcode.UseVisualStyleBackColor = True
        '
        'opt_allcode
        '
        Me.opt_allcode.AutoSize = True
        Me.opt_allcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_allcode.Location = New System.Drawing.Point(42, 13)
        Me.opt_allcode.Name = "opt_allcode"
        Me.opt_allcode.Size = New System.Drawing.Size(42, 17)
        Me.opt_allcode.TabIndex = 2
        Me.opt_allcode.TabStop = True
        Me.opt_allcode.Text = "All"
        Me.opt_allcode.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(2, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Code"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.SkyBlue
        Me.Panel3.Controls.Add(Me.Txt_searchloc)
        Me.Panel3.Controls.Add(Me.Chklst_loc)
        Me.Panel3.Controls.Add(Me.opt_selloc)
        Me.Panel3.Controls.Add(Me.opt_allloc)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(229, 149)
        Me.Panel3.TabIndex = 211
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
        'Chklst_loc
        '
        Me.Chklst_loc.BackColor = System.Drawing.Color.SkyBlue
        Me.Chklst_loc.CheckOnClick = True
        Me.Chklst_loc.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chklst_loc.FormattingEnabled = True
        Me.Chklst_loc.Location = New System.Drawing.Point(2, 53)
        Me.Chklst_loc.Name = "Chklst_loc"
        Me.Chklst_loc.Size = New System.Drawing.Size(226, 95)
        Me.Chklst_loc.TabIndex = 4
        '
        'opt_selloc
        '
        Me.opt_selloc.AutoSize = True
        Me.opt_selloc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_selloc.Location = New System.Drawing.Point(141, 13)
        Me.opt_selloc.Name = "opt_selloc"
        Me.opt_selloc.Size = New System.Drawing.Size(81, 17)
        Me.opt_selloc.TabIndex = 3
        Me.opt_selloc.TabStop = True
        Me.opt_selloc.Text = "Selected"
        Me.opt_selloc.UseVisualStyleBackColor = True
        '
        'opt_allloc
        '
        Me.opt_allloc.AutoSize = True
        Me.opt_allloc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.opt_allloc.Location = New System.Drawing.Point(63, 13)
        Me.opt_allloc.Name = "opt_allloc"
        Me.opt_allloc.Size = New System.Drawing.Size(42, 17)
        Me.opt_allloc.TabIndex = 2
        Me.opt_allloc.TabStop = True
        Me.opt_allloc.Text = "All"
        Me.opt_allloc.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Location"
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(1086, 502)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Exit.TabIndex = 201
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Frm_Stock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1182, 534)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Name = "Frm_Stock"
        Me.Text = "Stock"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel_Group.ResumeLayout(False)
        Me.Panel_Group.PerformLayout()
        Me.Panel_Item.ResumeLayout(False)
        Me.Panel_Item.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents DtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txt_searchgrp As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_Grp As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_Selgrp As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_Allgrp As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel_Group As System.Windows.Forms.Panel
    Friend WithEvents txt_searchitem As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_Item As System.Windows.Forms.CheckedListBox
    Friend WithEvents Opt_Selitem As System.Windows.Forms.RadioButton
    Friend WithEvents Opt_AllItem As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel_Item As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Txt_searchloc As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_loc As System.Windows.Forms.CheckedListBox
    Friend WithEvents opt_selloc As System.Windows.Forms.RadioButton
    Friend WithEvents opt_allloc As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Txt_searchcode As System.Windows.Forms.TextBox
    Friend WithEvents Chklst_code As System.Windows.Forms.CheckedListBox
    Friend WithEvents opt_selcode As System.Windows.Forms.RadioButton
    Friend WithEvents opt_allcode As System.Windows.Forms.RadioButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chk_reorder As System.Windows.Forms.CheckBox
End Class
