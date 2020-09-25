<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_DiscountDetail
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
        Me.Btn_Return = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txt_Search = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.C_Itemid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_code = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Itemname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Decimal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Discount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_Selrate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Btn_Return)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(622, 413)
        Me.Panel1.TabIndex = 0
        '
        'Btn_Return
        '
        Me.Btn_Return.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Return.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Return.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Return.Location = New System.Drawing.Point(525, 382)
        Me.Btn_Return.Name = "Btn_Return"
        Me.Btn_Return.Size = New System.Drawing.Size(93, 28)
        Me.Btn_Return.TabIndex = 2
        Me.Btn_Return.Text = "&Return"
        Me.Btn_Return.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GridView1)
        Me.Panel3.Location = New System.Drawing.Point(0, 26)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(622, 354)
        Me.Panel3.TabIndex = 1
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.C_Itemid, Me.C_code, Me.C_Itemname, Me.C_UOM, Me.C_Decimal, Me.C_Discount, Me.C_Selrate})
        Me.GridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(0, 0)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.GridView1.RowHeadersWidth = 33
        Me.GridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.GridView1.Size = New System.Drawing.Size(622, 354)
        Me.GridView1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txt_Search)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(622, 27)
        Me.Panel2.TabIndex = 0
        '
        'txt_Search
        '
        Me.txt_Search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Search.Location = New System.Drawing.Point(44, 4)
        Me.txt_Search.Name = "txt_Search"
        Me.txt_Search.Size = New System.Drawing.Size(368, 20)
        Me.txt_Search.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Filter"
        '
        'C_Itemid
        '
        Me.C_Itemid.HeaderText = "Itemid"
        Me.C_Itemid.Name = "C_Itemid"
        Me.C_Itemid.Visible = False
        '
        'C_code
        '
        Me.C_code.HeaderText = "Code"
        Me.C_code.Name = "C_code"
        Me.C_code.Width = 70
        '
        'C_Itemname
        '
        Me.C_Itemname.HeaderText = "Item Name"
        Me.C_Itemname.Name = "C_Itemname"
        Me.C_Itemname.Width = 295
        '
        'C_UOM
        '
        Me.C_UOM.HeaderText = "UOM"
        Me.C_UOM.Name = "C_UOM"
        Me.C_UOM.Width = 60
        '
        'C_Decimal
        '
        Me.C_Decimal.HeaderText = "Decimal"
        Me.C_Decimal.Name = "C_Decimal"
        Me.C_Decimal.Visible = False
        '
        'C_Discount
        '
        Me.C_Discount.HeaderText = "Discount (%)"
        Me.C_Discount.Name = "C_Discount"
        Me.C_Discount.Width = 70
        '
        'C_Selrate
        '
        Me.C_Selrate.HeaderText = "Selling Rate - Retail "
        Me.C_Selrate.Name = "C_Selrate"
        Me.C_Selrate.Width = 70
        '
        'Frm_DiscountDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(622, 413)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Frm_DiscountDetail"
        Me.Text = "Discount Details"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txt_Search As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_Return As System.Windows.Forms.Button
    Friend WithEvents C_Itemid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_code As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Itemname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Decimal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Discount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_Selrate As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
