<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_billtotal
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DTP_Vchdate = New System.Windows.Forms.DateTimePicker()
        Me.txt_vchnum = New System.Windows.Forms.TextBox()
        Me.lbl_no = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmd_cancel = New System.Windows.Forms.Button()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.txt_totbill = New System.Windows.Forms.TextBox()
        Me.Txt_Totrcvdamnt = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Customer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PartyId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_billamnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_rcptno = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.c_rcptid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C_rcvdamnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Discount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DTP_Vchdate)
        Me.Panel1.Controls.Add(Me.txt_vchnum)
        Me.Panel1.Controls.Add(Me.lbl_no)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1066, 53)
        Me.Panel1.TabIndex = 0
        '
        'DTP_Vchdate
        '
        Me.DTP_Vchdate.CustomFormat = "dd/MM/yyyy"
        Me.DTP_Vchdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTP_Vchdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTP_Vchdate.Location = New System.Drawing.Point(57, 27)
        Me.DTP_Vchdate.Name = "DTP_Vchdate"
        Me.DTP_Vchdate.Size = New System.Drawing.Size(130, 21)
        Me.DTP_Vchdate.TabIndex = 0
        '
        'txt_vchnum
        '
        Me.txt_vchnum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_vchnum.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_vchnum.Location = New System.Drawing.Point(57, 3)
        Me.txt_vchnum.Name = "txt_vchnum"
        Me.txt_vchnum.Size = New System.Drawing.Size(145, 21)
        Me.txt_vchnum.TabIndex = 104
        '
        'lbl_no
        '
        Me.lbl_no.AutoSize = True
        Me.lbl_no.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_no.Location = New System.Drawing.Point(6, 8)
        Me.lbl_no.Name = "lbl_no"
        Me.lbl_no.Size = New System.Drawing.Size(24, 13)
        Me.lbl_no.TabIndex = 102
        Me.lbl_no.Text = "No"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 103
        Me.Label2.Text = "Date"
        '
        'cmd_cancel
        '
        Me.cmd_cancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_cancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancel.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_cancel.Location = New System.Drawing.Point(975, 537)
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
        Me.cmd_ok.Location = New System.Drawing.Point(884, 537)
        Me.cmd_ok.Name = "cmd_ok"
        Me.cmd_ok.Size = New System.Drawing.Size(90, 28)
        Me.cmd_ok.TabIndex = 143
        Me.cmd_ok.Text = "&Ok"
        Me.cmd_ok.UseVisualStyleBackColor = False
        '
        'GridView1
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.GridView1.ColumnHeadersHeight = 30
        Me.GridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Customer, Me.PartyId, Me.C_no, Me.c_billamnt, Me.c_rcptno, Me.c_rcptid, Me.C_rcvdamnt, Me.Discount})
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(0, 54)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.GridView1.Size = New System.Drawing.Size(1066, 411)
        Me.GridView1.TabIndex = 145
        '
        'txt_totbill
        '
        Me.txt_totbill.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txt_totbill.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_totbill.Location = New System.Drawing.Point(805, 469)
        Me.txt_totbill.Name = "txt_totbill"
        Me.txt_totbill.ReadOnly = True
        Me.txt_totbill.Size = New System.Drawing.Size(116, 24)
        Me.txt_totbill.TabIndex = 147
        Me.txt_totbill.Text = "0.00"
        Me.txt_totbill.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txt_Totrcvdamnt
        '
        Me.Txt_Totrcvdamnt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Txt_Totrcvdamnt.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txt_Totrcvdamnt.Location = New System.Drawing.Point(924, 466)
        Me.Txt_Totrcvdamnt.Name = "Txt_Totrcvdamnt"
        Me.Txt_Totrcvdamnt.ReadOnly = True
        Me.Txt_Totrcvdamnt.Size = New System.Drawing.Size(137, 24)
        Me.Txt_Totrcvdamnt.TabIndex = 148
        Me.Txt_Totrcvdamnt.Text = "0.00"
        Me.Txt_Totrcvdamnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Green
        Me.Label9.Location = New System.Drawing.Point(736, 469)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 23)
        Me.Label9.TabIndex = 146
        Me.Label9.Text = "Total"
        '
        'Customer
        '
        Me.Customer.Frozen = True
        Me.Customer.HeaderText = "Party"
        Me.Customer.Name = "Customer"
        Me.Customer.Width = 400
        '
        'PartyId
        '
        Me.PartyId.HeaderText = "PartyId"
        Me.PartyId.Name = "PartyId"
        Me.PartyId.Visible = False
        '
        'C_no
        '
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C_no.DefaultCellStyle = DataGridViewCellStyle2
        Me.C_no.HeaderText = "BILL NO"
        Me.C_no.Name = "C_no"
        Me.C_no.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_no.Visible = False
        Me.C_no.Width = 150
        '
        'c_billamnt
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.c_billamnt.DefaultCellStyle = DataGridViewCellStyle3
        Me.c_billamnt.HeaderText = "Bill Amount"
        Me.c_billamnt.Name = "c_billamnt"
        Me.c_billamnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.c_billamnt.Width = 140
        '
        'c_rcptno
        '
        Me.c_rcptno.HeaderText = "Receipt"
        Me.c_rcptno.Name = "c_rcptno"
        Me.c_rcptno.Visible = False
        Me.c_rcptno.Width = 150
        '
        'c_rcptid
        '
        Me.c_rcptid.HeaderText = "Rcptid"
        Me.c_rcptid.Name = "c_rcptid"
        Me.c_rcptid.Visible = False
        '
        'C_rcvdamnt
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.C_rcvdamnt.DefaultCellStyle = DataGridViewCellStyle4
        Me.C_rcvdamnt.HeaderText = "Rcvd Amount"
        Me.C_rcvdamnt.Name = "C_rcvdamnt"
        Me.C_rcvdamnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.C_rcvdamnt.Width = 140
        '
        'Discount
        '
        Me.Discount.HeaderText = "Discount"
        Me.Discount.Name = "Discount"
        '
        'frm_billtotal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1067, 568)
        Me.Controls.Add(Me.txt_totbill)
        Me.Controls.Add(Me.Txt_Totrcvdamnt)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.GridView1)
        Me.Controls.Add(Me.cmd_cancel)
        Me.Controls.Add(Me.cmd_ok)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frm_billtotal"
        Me.Text = "Bill Total"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DTP_Vchdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txt_vchnum As System.Windows.Forms.TextBox
    Friend WithEvents lbl_no As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txt_totbill As System.Windows.Forms.TextBox
    Friend WithEvents Txt_Totrcvdamnt As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartyId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_billamnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_rcptno As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c_rcptid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C_rcvdamnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Discount As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
