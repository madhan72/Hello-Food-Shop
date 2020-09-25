<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_itemmaster
    Inherits System.Windows.Forms.Form
    'Inherits MetroFramework.Forms.MetroForm

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
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.txt_search = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdedit = New System.Windows.Forms.Button()
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txt_minstock = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Lbl_cgstsgst = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txt_hsndescription = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lbl_rsless = New System.Windows.Forms.Label()
        Me.lbl_rsadd = New System.Windows.Forms.Label()
        Me.txt_accountingcode = New System.Windows.Forms.TextBox()
        Me.lbl_less = New System.Windows.Forms.Label()
        Me.lbl_add = New System.Windows.Forms.Label()
        Me.Lbl_supllier = New System.Windows.Forms.Label()
        Me.txt_less = New System.Windows.Forms.TextBox()
        Me.txt_ofr2 = New System.Windows.Forms.TextBox()
        Me.Lbl_supplier = New System.Windows.Forms.Label()
        Me.txt_add = New System.Windows.Forms.TextBox()
        Me.txt_PkgWt = New System.Windows.Forms.TextBox()
        Me.txt_ofr1 = New System.Windows.Forms.TextBox()
        Me.Lbl_packweight = New System.Windows.Forms.Label()
        Me.Lbl_offerqtyless = New System.Windows.Forms.Label()
        Me.Chk_Inactive = New System.Windows.Forms.CheckBox()
        Me.Lbl_offerqtyadd = New System.Windows.Forms.Label()
        Me.lbl_uom = New System.Windows.Forms.Label()
        Me.txt_freeuom = New System.Windows.Forms.TextBox()
        Me.Lbl_Time = New System.Windows.Forms.Label()
        Me.Lbl_lastmodified = New System.Windows.Forms.Label()
        Me.Lbl_user = New System.Windows.Forms.Label()
        Me.Lbl_usr = New System.Windows.Forms.Label()
        Me.Cbo_itemtype = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Lbl_freeqty = New System.Windows.Forms.Label()
        Me.txt_freeqty = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_mrprate = New System.Windows.Forms.TextBox()
        Me.txt_profit = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lbl_forqty = New System.Windows.Forms.Label()
        Me.Lbl_remarks = New System.Windows.Forms.Label()
        Me.txt_forqty = New System.Windows.Forms.TextBox()
        Me.txt_remakrs = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txt_group = New System.Windows.Forms.TextBox()
        Me.txt_selretpric = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_rake = New System.Windows.Forms.TextBox()
        Me.txt_category = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Lbl_freeitem = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txt_freeitem = New System.Windows.Forms.TextBox()
        Me.txt_selsalpric = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_tamildes = New System.Windows.Forms.TextBox()
        Me.txt_itemcode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_tax = New System.Windows.Forms.TextBox()
        Me.txt_uom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_costperc = New System.Windows.Forms.TextBox()
        Me.txt_ItemDes = New System.Windows.Forms.TextBox()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.txt_search)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GridView1)
        Me.Panel1.Location = New System.Drawing.Point(2, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1103, 603)
        Me.Panel1.TabIndex = 2
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.CheckBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBox1.Location = New System.Drawing.Point(3, 586)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(112, 17)
        Me.CheckBox1.TabIndex = 29
        Me.CheckBox1.Text = "Show InActive "
        Me.CheckBox1.UseVisualStyleBackColor = False
        '
        'txt_search
        '
        Me.txt_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_search.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_search.Location = New System.Drawing.Point(3, 4)
        Me.txt_search.Name = "txt_search"
        Me.txt_search.Size = New System.Drawing.Size(457, 20)
        Me.txt_search.TabIndex = 28
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.cmdexit)
        Me.GroupBox1.Controls.Add(Me.cmddelete)
        Me.GroupBox1.Controls.Add(Me.cmdedit)
        Me.GroupBox1.Controls.Add(Me.cmdadd)
        Me.GroupBox1.Location = New System.Drawing.Point(689, 563)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(412, 42)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.Location = New System.Drawing.Point(329, 11)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(80, 30)
        Me.cmdexit.TabIndex = 8
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmddelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmddelete.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.ForeColor = System.Drawing.Color.Black
        Me.cmddelete.Location = New System.Drawing.Point(242, 11)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(80, 30)
        Me.cmddelete.TabIndex = 7
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdedit
        '
        Me.cmdedit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdedit.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.ForeColor = System.Drawing.Color.Black
        Me.cmdedit.Location = New System.Drawing.Point(156, 11)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(80, 30)
        Me.cmdedit.TabIndex = 6
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = False
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdadd.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.Color.Black
        Me.cmdadd.Location = New System.Drawing.Point(70, 11)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(80, 30)
        Me.cmdadd.TabIndex = 5
        Me.cmdadd.Text = "&Add"
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.cmdcancel)
        Me.GroupBox2.Controls.Add(Me.cmdok)
        Me.GroupBox2.Location = New System.Drawing.Point(689, 563)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(411, 42)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcancel.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.Location = New System.Drawing.Point(329, 10)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(80, 30)
        Me.cmdcancel.TabIndex = 8
        Me.cmdcancel.Text = "&Cancel"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdok.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.Location = New System.Drawing.Point(243, 10)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(80, 30)
        Me.cmdok.TabIndex = 7
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.txt_minstock)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Lbl_cgstsgst)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Controls.Add(Me.txt_hsndescription)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.lbl_rsless)
        Me.GroupBox3.Controls.Add(Me.lbl_rsadd)
        Me.GroupBox3.Controls.Add(Me.txt_accountingcode)
        Me.GroupBox3.Controls.Add(Me.lbl_less)
        Me.GroupBox3.Controls.Add(Me.lbl_add)
        Me.GroupBox3.Controls.Add(Me.Lbl_supllier)
        Me.GroupBox3.Controls.Add(Me.txt_less)
        Me.GroupBox3.Controls.Add(Me.txt_ofr2)
        Me.GroupBox3.Controls.Add(Me.Lbl_supplier)
        Me.GroupBox3.Controls.Add(Me.txt_add)
        Me.GroupBox3.Controls.Add(Me.txt_PkgWt)
        Me.GroupBox3.Controls.Add(Me.txt_ofr1)
        Me.GroupBox3.Controls.Add(Me.Lbl_packweight)
        Me.GroupBox3.Controls.Add(Me.Lbl_offerqtyless)
        Me.GroupBox3.Controls.Add(Me.Chk_Inactive)
        Me.GroupBox3.Controls.Add(Me.Lbl_offerqtyadd)
        Me.GroupBox3.Controls.Add(Me.lbl_uom)
        Me.GroupBox3.Controls.Add(Me.txt_freeuom)
        Me.GroupBox3.Controls.Add(Me.Lbl_Time)
        Me.GroupBox3.Controls.Add(Me.Lbl_lastmodified)
        Me.GroupBox3.Controls.Add(Me.Lbl_user)
        Me.GroupBox3.Controls.Add(Me.Lbl_usr)
        Me.GroupBox3.Controls.Add(Me.Cbo_itemtype)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.Lbl_freeqty)
        Me.GroupBox3.Controls.Add(Me.txt_freeqty)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txt_mrprate)
        Me.GroupBox3.Controls.Add(Me.txt_profit)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.lbl_forqty)
        Me.GroupBox3.Controls.Add(Me.Lbl_remarks)
        Me.GroupBox3.Controls.Add(Me.txt_forqty)
        Me.GroupBox3.Controls.Add(Me.txt_remakrs)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txt_group)
        Me.GroupBox3.Controls.Add(Me.txt_selretpric)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txt_rake)
        Me.GroupBox3.Controls.Add(Me.txt_category)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Lbl_freeitem)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.txt_freeitem)
        Me.GroupBox3.Controls.Add(Me.txt_selsalpric)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.txt_tamildes)
        Me.GroupBox3.Controls.Add(Me.txt_itemcode)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txt_tax)
        Me.GroupBox3.Controls.Add(Me.txt_uom)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txt_costperc)
        Me.GroupBox3.Controls.Add(Me.txt_ItemDes)
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(466, 26)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(621, 535)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'txt_minstock
        '
        Me.txt_minstock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_minstock.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_minstock.Location = New System.Drawing.Point(220, 529)
        Me.txt_minstock.Name = "txt_minstock"
        Me.txt_minstock.Size = New System.Drawing.Size(81, 22)
        Me.txt_minstock.TabIndex = 76
        Me.txt_minstock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(19, 534)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(107, 14)
        Me.Label11.TabIndex = 77
        Me.Label11.Text = "Minimum Stock"
        '
        'Lbl_cgstsgst
        '
        Me.Lbl_cgstsgst.AutoSize = True
        Me.Lbl_cgstsgst.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_cgstsgst.ForeColor = System.Drawing.Color.DarkGreen
        Me.Lbl_cgstsgst.Location = New System.Drawing.Point(309, 124)
        Me.Lbl_cgstsgst.Name = "Lbl_cgstsgst"
        Me.Lbl_cgstsgst.Size = New System.Drawing.Size(0, 14)
        Me.Lbl_cgstsgst.TabIndex = 75
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(558, 92)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(64, 24)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "&HSN"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txt_hsndescription
        '
        Me.txt_hsndescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_hsndescription.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_hsndescription.Location = New System.Drawing.Point(308, 93)
        Me.txt_hsndescription.Name = "txt_hsndescription"
        Me.txt_hsndescription.Size = New System.Drawing.Size(249, 23)
        Me.txt_hsndescription.TabIndex = 73
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(18, 95)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(169, 16)
        Me.Label30.TabIndex = 72
        Me.Label30.Text = "HSN/Accounting Code"
        '
        'lbl_rsless
        '
        Me.lbl_rsless.AutoSize = True
        Me.lbl_rsless.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rsless.Location = New System.Drawing.Point(376, 507)
        Me.lbl_rsless.Name = "lbl_rsless"
        Me.lbl_rsless.Size = New System.Drawing.Size(75, 16)
        Me.lbl_rsless.TabIndex = 70
        Me.lbl_rsless.Text = "Rs.   Less"
        '
        'lbl_rsadd
        '
        Me.lbl_rsadd.AutoSize = True
        Me.lbl_rsadd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_rsadd.Location = New System.Drawing.Point(374, 480)
        Me.lbl_rsadd.Name = "lbl_rsadd"
        Me.lbl_rsadd.Size = New System.Drawing.Size(78, 16)
        Me.lbl_rsadd.TabIndex = 69
        Me.lbl_rsadd.Text = "Rs.    Add "
        '
        'txt_accountingcode
        '
        Me.txt_accountingcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_accountingcode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_accountingcode.Location = New System.Drawing.Point(219, 93)
        Me.txt_accountingcode.Name = "txt_accountingcode"
        Me.txt_accountingcode.Size = New System.Drawing.Size(86, 23)
        Me.txt_accountingcode.TabIndex = 71
        '
        'lbl_less
        '
        Me.lbl_less.AutoSize = True
        Me.lbl_less.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_less.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.lbl_less.Location = New System.Drawing.Point(281, 507)
        Me.lbl_less.Name = "lbl_less"
        Me.lbl_less.Size = New System.Drawing.Size(30, 16)
        Me.lbl_less.TabIndex = 61
        Me.lbl_less.Text = "<="
        '
        'lbl_add
        '
        Me.lbl_add.AutoSize = True
        Me.lbl_add.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_add.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.lbl_add.Location = New System.Drawing.Point(281, 478)
        Me.lbl_add.Name = "lbl_add"
        Me.lbl_add.Size = New System.Drawing.Size(30, 16)
        Me.lbl_add.TabIndex = 60
        Me.lbl_add.Text = ">="
        '
        'Lbl_supllier
        '
        Me.Lbl_supllier.AutoSize = True
        Me.Lbl_supllier.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_supllier.ForeColor = System.Drawing.Color.Black
        Me.Lbl_supllier.Location = New System.Drawing.Point(217, 452)
        Me.Lbl_supllier.Name = "Lbl_supllier"
        Me.Lbl_supllier.Size = New System.Drawing.Size(67, 16)
        Me.Lbl_supllier.TabIndex = 68
        Me.Lbl_supllier.Text = "Supplier"
        '
        'txt_less
        '
        Me.txt_less.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_less.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_less.Location = New System.Drawing.Point(319, 503)
        Me.txt_less.Name = "txt_less"
        Me.txt_less.Size = New System.Drawing.Size(55, 23)
        Me.txt_less.TabIndex = 23
        Me.txt_less.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ofr2
        '
        Me.txt_ofr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_ofr2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ofr2.Location = New System.Drawing.Point(220, 503)
        Me.txt_ofr2.Name = "txt_ofr2"
        Me.txt_ofr2.Size = New System.Drawing.Size(55, 23)
        Me.txt_ofr2.TabIndex = 22
        Me.txt_ofr2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Lbl_supplier
        '
        Me.Lbl_supplier.AutoSize = True
        Me.Lbl_supplier.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_supplier.ForeColor = System.Drawing.Color.Black
        Me.Lbl_supplier.Location = New System.Drawing.Point(18, 452)
        Me.Lbl_supplier.Name = "Lbl_supplier"
        Me.Lbl_supplier.Size = New System.Drawing.Size(71, 16)
        Me.Lbl_supplier.TabIndex = 67
        Me.Lbl_supplier.Text = "Supplier "
        '
        'txt_add
        '
        Me.txt_add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_add.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_add.Location = New System.Drawing.Point(319, 476)
        Me.txt_add.Name = "txt_add"
        Me.txt_add.Size = New System.Drawing.Size(55, 23)
        Me.txt_add.TabIndex = 21
        Me.txt_add.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_PkgWt
        '
        Me.txt_PkgWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_PkgWt.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_PkgWt.Location = New System.Drawing.Point(219, 424)
        Me.txt_PkgWt.Name = "txt_PkgWt"
        Me.txt_PkgWt.Size = New System.Drawing.Size(100, 22)
        Me.txt_PkgWt.TabIndex = 19
        '
        'txt_ofr1
        '
        Me.txt_ofr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_ofr1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ofr1.Location = New System.Drawing.Point(220, 476)
        Me.txt_ofr1.Name = "txt_ofr1"
        Me.txt_ofr1.Size = New System.Drawing.Size(55, 23)
        Me.txt_ofr1.TabIndex = 20
        Me.txt_ofr1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Lbl_packweight
        '
        Me.Lbl_packweight.AutoSize = True
        Me.Lbl_packweight.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_packweight.Location = New System.Drawing.Point(18, 429)
        Me.Lbl_packweight.Name = "Lbl_packweight"
        Me.Lbl_packweight.Size = New System.Drawing.Size(118, 14)
        Me.Lbl_packweight.TabIndex = 65
        Me.Lbl_packweight.Text = "Package Weight "
        '
        'Lbl_offerqtyless
        '
        Me.Lbl_offerqtyless.AutoSize = True
        Me.Lbl_offerqtyless.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_offerqtyless.Location = New System.Drawing.Point(19, 504)
        Me.Lbl_offerqtyless.Name = "Lbl_offerqtyless"
        Me.Lbl_offerqtyless.Size = New System.Drawing.Size(74, 16)
        Me.Lbl_offerqtyless.TabIndex = 55
        Me.Lbl_offerqtyless.Text = "Offer Qty"
        '
        'Chk_Inactive
        '
        Me.Chk_Inactive.AutoSize = True
        Me.Chk_Inactive.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_Inactive.Location = New System.Drawing.Point(444, 376)
        Me.Chk_Inactive.Name = "Chk_Inactive"
        Me.Chk_Inactive.Size = New System.Drawing.Size(80, 18)
        Me.Chk_Inactive.TabIndex = 18
        Me.Chk_Inactive.Text = "Inactive"
        Me.Chk_Inactive.UseVisualStyleBackColor = True
        '
        'Lbl_offerqtyadd
        '
        Me.Lbl_offerqtyadd.AutoSize = True
        Me.Lbl_offerqtyadd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_offerqtyadd.Location = New System.Drawing.Point(19, 478)
        Me.Lbl_offerqtyadd.Name = "Lbl_offerqtyadd"
        Me.Lbl_offerqtyadd.Size = New System.Drawing.Size(74, 16)
        Me.Lbl_offerqtyadd.TabIndex = 54
        Me.Lbl_offerqtyadd.Text = "Offer Qty"
        '
        'lbl_uom
        '
        Me.lbl_uom.AutoSize = True
        Me.lbl_uom.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_uom.Location = New System.Drawing.Point(479, 324)
        Me.lbl_uom.Name = "lbl_uom"
        Me.lbl_uom.Size = New System.Drawing.Size(41, 16)
        Me.lbl_uom.TabIndex = 63
        Me.lbl_uom.Text = "Uom"
        '
        'txt_freeuom
        '
        Me.txt_freeuom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_freeuom.Enabled = False
        Me.txt_freeuom.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_freeuom.Location = New System.Drawing.Point(522, 323)
        Me.txt_freeuom.Name = "txt_freeuom"
        Me.txt_freeuom.Size = New System.Drawing.Size(74, 23)
        Me.txt_freeuom.TabIndex = 15
        '
        'Lbl_Time
        '
        Me.Lbl_Time.AutoSize = True
        Me.Lbl_Time.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_Time.ForeColor = System.Drawing.Color.Black
        Me.Lbl_Time.Location = New System.Drawing.Point(219, 405)
        Me.Lbl_Time.Name = "Lbl_Time"
        Me.Lbl_Time.Size = New System.Drawing.Size(82, 16)
        Me.Lbl_Time.TabIndex = 61
        Me.Lbl_Time.Text = "Item Type"
        '
        'Lbl_lastmodified
        '
        Me.Lbl_lastmodified.AutoSize = True
        Me.Lbl_lastmodified.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_lastmodified.ForeColor = System.Drawing.Color.Black
        Me.Lbl_lastmodified.Location = New System.Drawing.Point(18, 405)
        Me.Lbl_lastmodified.Name = "Lbl_lastmodified"
        Me.Lbl_lastmodified.Size = New System.Drawing.Size(199, 16)
        Me.Lbl_lastmodified.TabIndex = 60
        Me.Lbl_lastmodified.Text = "Last Modified Date/Time  :"
        '
        'Lbl_user
        '
        Me.Lbl_user.AutoSize = True
        Me.Lbl_user.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_user.ForeColor = System.Drawing.Color.Black
        Me.Lbl_user.Location = New System.Drawing.Point(220, 376)
        Me.Lbl_user.Name = "Lbl_user"
        Me.Lbl_user.Size = New System.Drawing.Size(82, 16)
        Me.Lbl_user.TabIndex = 59
        Me.Lbl_user.Text = "Item Type"
        '
        'Lbl_usr
        '
        Me.Lbl_usr.AutoSize = True
        Me.Lbl_usr.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_usr.ForeColor = System.Drawing.Color.Black
        Me.Lbl_usr.Location = New System.Drawing.Point(18, 376)
        Me.Lbl_usr.Name = "Lbl_usr"
        Me.Lbl_usr.Size = New System.Drawing.Size(54, 16)
        Me.Lbl_usr.TabIndex = 58
        Me.Lbl_usr.Text = "User  :"
        '
        'Cbo_itemtype
        '
        Me.Cbo_itemtype.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cbo_itemtype.FormattingEnabled = True
        Me.Cbo_itemtype.Items.AddRange(New Object() {"Retail", "Wholesale", "Both"})
        Me.Cbo_itemtype.Location = New System.Drawing.Point(219, 372)
        Me.Cbo_itemtype.Name = "Cbo_itemtype"
        Me.Cbo_itemtype.Size = New System.Drawing.Size(141, 24)
        Me.Cbo_itemtype.TabIndex = 17
        Me.Cbo_itemtype.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(18, 375)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 16)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "Item Type"
        Me.Label13.Visible = False
        '
        'Lbl_freeqty
        '
        Me.Lbl_freeqty.AutoSize = True
        Me.Lbl_freeqty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_freeqty.Location = New System.Drawing.Point(18, 324)
        Me.Lbl_freeqty.Name = "Lbl_freeqty"
        Me.Lbl_freeqty.Size = New System.Drawing.Size(71, 16)
        Me.Lbl_freeqty.TabIndex = 56
        Me.Lbl_freeqty.Text = "Free Qty"
        '
        'txt_freeqty
        '
        Me.txt_freeqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_freeqty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_freeqty.Location = New System.Drawing.Point(219, 322)
        Me.txt_freeqty.Name = "txt_freeqty"
        Me.txt_freeqty.Size = New System.Drawing.Size(96, 23)
        Me.txt_freeqty.TabIndex = 13
        Me.txt_freeqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(326, 173)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 16)
        Me.Label15.TabIndex = 55
        Me.Label15.Text = "MRP Rate"
        '
        'txt_mrprate
        '
        Me.txt_mrprate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_mrprate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_mrprate.Location = New System.Drawing.Point(412, 172)
        Me.txt_mrprate.Name = "txt_mrprate"
        Me.txt_mrprate.Size = New System.Drawing.Size(81, 23)
        Me.txt_mrprate.TabIndex = 6
        Me.txt_mrprate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_profit
        '
        Me.txt_profit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_profit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_profit.Location = New System.Drawing.Point(412, 147)
        Me.txt_profit.Name = "txt_profit"
        Me.txt_profit.Size = New System.Drawing.Size(81, 23)
        Me.txt_profit.TabIndex = 4
        Me.txt_profit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(326, 149)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(68, 16)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Profit %"
        '
        'lbl_forqty
        '
        Me.lbl_forqty.AutoSize = True
        Me.lbl_forqty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_forqty.Location = New System.Drawing.Point(326, 324)
        Me.lbl_forqty.Name = "lbl_forqty"
        Me.lbl_forqty.Size = New System.Drawing.Size(62, 16)
        Me.lbl_forqty.TabIndex = 53
        Me.lbl_forqty.Text = "For Qty"
        '
        'Lbl_remarks
        '
        Me.Lbl_remarks.AutoSize = True
        Me.Lbl_remarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_remarks.Location = New System.Drawing.Point(18, 351)
        Me.Lbl_remarks.Name = "Lbl_remarks"
        Me.Lbl_remarks.Size = New System.Drawing.Size(71, 16)
        Me.Lbl_remarks.TabIndex = 52
        Me.Lbl_remarks.Text = "Remarks"
        '
        'txt_forqty
        '
        Me.txt_forqty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_forqty.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_forqty.Location = New System.Drawing.Point(393, 323)
        Me.txt_forqty.Name = "txt_forqty"
        Me.txt_forqty.Size = New System.Drawing.Size(82, 23)
        Me.txt_forqty.TabIndex = 14
        Me.txt_forqty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_remakrs
        '
        Me.txt_remakrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_remakrs.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_remakrs.Location = New System.Drawing.Point(219, 347)
        Me.txt_remakrs.Name = "txt_remakrs"
        Me.txt_remakrs.Size = New System.Drawing.Size(366, 23)
        Me.txt_remakrs.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 224)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 16)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Group"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(18, 174)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(159, 16)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Selling Price (Retail) "
        '
        'txt_group
        '
        Me.txt_group.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_group.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_group.Location = New System.Drawing.Point(219, 222)
        Me.txt_group.Name = "txt_group"
        Me.txt_group.Size = New System.Drawing.Size(222, 23)
        Me.txt_group.TabIndex = 9
        '
        'txt_selretpric
        '
        Me.txt_selretpric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_selretpric.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_selretpric.Location = New System.Drawing.Point(219, 172)
        Me.txt_selretpric.Name = "txt_selretpric"
        Me.txt_selretpric.Size = New System.Drawing.Size(86, 23)
        Me.txt_selretpric.TabIndex = 7
        Me.txt_selretpric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(18, 274)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 16)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Rake"
        '
        'txt_rake
        '
        Me.txt_rake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_rake.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_rake.Location = New System.Drawing.Point(219, 272)
        Me.txt_rake.Name = "txt_rake"
        Me.txt_rake.Size = New System.Drawing.Size(222, 23)
        Me.txt_rake.TabIndex = 11
        '
        'txt_category
        '
        Me.txt_category.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_category.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_category.Location = New System.Drawing.Point(219, 247)
        Me.txt_category.Name = "txt_category"
        Me.txt_category.Size = New System.Drawing.Size(222, 23)
        Me.txt_category.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 247)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 16)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Category"
        '
        'Lbl_freeitem
        '
        Me.Lbl_freeitem.AutoSize = True
        Me.Lbl_freeitem.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_freeitem.Location = New System.Drawing.Point(18, 297)
        Me.Lbl_freeitem.Name = "Lbl_freeitem"
        Me.Lbl_freeitem.Size = New System.Drawing.Size(80, 16)
        Me.Lbl_freeitem.TabIndex = 41
        Me.Lbl_freeitem.Text = "Free Item"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 199)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(190, 16)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "Selling Price(Whole Sale)"
        '
        'txt_freeitem
        '
        Me.txt_freeitem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_freeitem.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_freeitem.Location = New System.Drawing.Point(219, 297)
        Me.txt_freeitem.Name = "txt_freeitem"
        Me.txt_freeitem.Size = New System.Drawing.Size(339, 23)
        Me.txt_freeitem.TabIndex = 12
        '
        'txt_selsalpric
        '
        Me.txt_selsalpric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_selsalpric.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_selsalpric.Location = New System.Drawing.Point(219, 197)
        Me.txt_selsalpric.Name = "txt_selsalpric"
        Me.txt_selsalpric.Size = New System.Drawing.Size(86, 23)
        Me.txt_selsalpric.TabIndex = 8
        Me.txt_selsalpric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(18, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(172, 16)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Item Tamil Description"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 16)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Item Code"
        '
        'txt_tamildes
        '
        Me.txt_tamildes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_tamildes.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tamildes.Location = New System.Drawing.Point(219, 62)
        Me.txt_tamildes.Name = "txt_tamildes"
        Me.txt_tamildes.Size = New System.Drawing.Size(339, 23)
        Me.txt_tamildes.TabIndex = 1
        '
        'txt_itemcode
        '
        Me.txt_itemcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_itemcode.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_itemcode.Location = New System.Drawing.Point(219, 12)
        Me.txt_itemcode.Name = "txt_itemcode"
        Me.txt_itemcode.Size = New System.Drawing.Size(186, 23)
        Me.txt_itemcode.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(18, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 16)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "TAX %"
        Me.Label2.Visible = False
        '
        'txt_tax
        '
        Me.txt_tax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_tax.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tax.Location = New System.Drawing.Point(219, 147)
        Me.txt_tax.Name = "txt_tax"
        Me.txt_tax.Size = New System.Drawing.Size(86, 23)
        Me.txt_tax.TabIndex = 3
        Me.txt_tax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txt_tax.Visible = False
        '
        'txt_uom
        '
        Me.txt_uom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_uom.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_uom.Location = New System.Drawing.Point(219, 120)
        Me.txt_uom.Name = "txt_uom"
        Me.txt_uom.Size = New System.Drawing.Size(86, 23)
        Me.txt_uom.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 16)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "UOM"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 149)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 16)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Cost Price"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 16)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Item Description"
        '
        'txt_costperc
        '
        Me.txt_costperc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_costperc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_costperc.Location = New System.Drawing.Point(219, 147)
        Me.txt_costperc.Name = "txt_costperc"
        Me.txt_costperc.Size = New System.Drawing.Size(86, 23)
        Me.txt_costperc.TabIndex = 5
        Me.txt_costperc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txt_ItemDes
        '
        Me.txt_ItemDes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_ItemDes.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_ItemDes.Location = New System.Drawing.Point(219, 37)
        Me.txt_ItemDes.Name = "txt_ItemDes"
        Me.txt_ItemDes.Size = New System.Drawing.Size(339, 23)
        Me.txt_ItemDes.TabIndex = 0
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        Me.GridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView1.ColumnHeadersHeight = 25
        Me.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(0, 26)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.Size = New System.Drawing.Size(460, 555)
        Me.GridView1.TabIndex = 1
        '
        'frm_itemmaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1108, 608)
        Me.Controls.Add(Me.Panel1)
        Me.MaximumSize = New System.Drawing.Size(1124, 647)
        Me.Name = "frm_itemmaster"
        Me.Padding = New System.Windows.Forms.Padding(5, 60, 5, 5)
        Me.Text = "Item Master"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txt_search As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmddelete As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_tamildes As System.Windows.Forms.TextBox
    Friend WithEvents txt_itemcode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_tax As System.Windows.Forms.TextBox
    Friend WithEvents txt_uom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_costperc As System.Windows.Forms.TextBox
    Friend WithEvents txt_ItemDes As System.Windows.Forms.TextBox
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Lbl_freeqty As System.Windows.Forms.Label
    Friend WithEvents txt_freeqty As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_mrprate As System.Windows.Forms.TextBox
    Friend WithEvents txt_profit As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lbl_forqty As System.Windows.Forms.Label
    Friend WithEvents Lbl_remarks As System.Windows.Forms.Label
    Friend WithEvents txt_forqty As System.Windows.Forms.TextBox
    Friend WithEvents txt_remakrs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txt_group As System.Windows.Forms.TextBox
    Friend WithEvents txt_selretpric As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_rake As System.Windows.Forms.TextBox
    Friend WithEvents txt_category As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Lbl_freeitem As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_freeitem As System.Windows.Forms.TextBox
    Friend WithEvents txt_selsalpric As System.Windows.Forms.TextBox
    Friend WithEvents Cbo_itemtype As System.Windows.Forms.ComboBox
    Friend WithEvents Lbl_Time As System.Windows.Forms.Label
    Friend WithEvents Lbl_lastmodified As System.Windows.Forms.Label
    Friend WithEvents Lbl_user As System.Windows.Forms.Label
    Friend WithEvents Lbl_usr As System.Windows.Forms.Label
    Friend WithEvents lbl_uom As System.Windows.Forms.Label
    Friend WithEvents txt_freeuom As System.Windows.Forms.TextBox
    Friend WithEvents txt_PkgWt As System.Windows.Forms.TextBox
    Friend WithEvents Lbl_packweight As System.Windows.Forms.Label
    Friend WithEvents Chk_Inactive As System.Windows.Forms.CheckBox
    Friend WithEvents Lbl_supllier As System.Windows.Forms.Label
    Friend WithEvents Lbl_supplier As System.Windows.Forms.Label
    Friend WithEvents Lbl_offerqtyless As System.Windows.Forms.Label
    Friend WithEvents Lbl_offerqtyadd As System.Windows.Forms.Label
    Friend WithEvents txt_less As System.Windows.Forms.TextBox
    Friend WithEvents txt_add As System.Windows.Forms.TextBox
    Friend WithEvents txt_ofr1 As System.Windows.Forms.TextBox
    Friend WithEvents txt_ofr2 As System.Windows.Forms.TextBox
    Friend WithEvents lbl_less As System.Windows.Forms.Label
    Friend WithEvents lbl_add As System.Windows.Forms.Label
    Friend WithEvents lbl_rsadd As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txt_accountingcode As System.Windows.Forms.TextBox
    Friend WithEvents txt_hsndescription As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbl_rsless As System.Windows.Forms.Label
    Friend WithEvents Lbl_cgstsgst As System.Windows.Forms.Label
    Friend WithEvents txt_minstock As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
