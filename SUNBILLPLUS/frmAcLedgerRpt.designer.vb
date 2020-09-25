<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAcLedgerRpt
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
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.Panel_Group = New System.Windows.Forms.Panel()
        Me.Lbl_company = New System.Windows.Forms.Label()
        Me.chk_details = New System.Windows.Forms.CheckBox()
        Me.Lbl_ItemName = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_Filter = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.DtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.Btn_Excel = New System.Windows.Forms.Button()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Group.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridView1
        '
        Me.GridView1.AllowUserToAddRows = False
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.BurlyWood
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.InfoText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.GridView1.ColumnHeadersHeight = 35
        Me.GridView1.EnableHeadersVisualStyles = False
        Me.GridView1.Location = New System.Drawing.Point(1, 90)
        Me.GridView1.Name = "GridView1"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.Tan
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.GridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridView1.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.GridView1.Size = New System.Drawing.Size(1109, 478)
        Me.GridView1.TabIndex = 201
        '
        'Panel_Group
        '
        Me.Panel_Group.BackColor = System.Drawing.Color.LightSkyBlue
        Me.Panel_Group.Controls.Add(Me.Lbl_company)
        Me.Panel_Group.Controls.Add(Me.chk_details)
        Me.Panel_Group.Controls.Add(Me.Lbl_ItemName)
        Me.Panel_Group.Controls.Add(Me.Panel2)
        Me.Panel_Group.Controls.Add(Me.Btn_load)
        Me.Panel_Group.Controls.Add(Me.DtpToDate)
        Me.Panel_Group.Controls.Add(Me.Label2)
        Me.Panel_Group.Controls.Add(Me.DtpFromDate)
        Me.Panel_Group.Controls.Add(Me.Label1)
        Me.Panel_Group.Location = New System.Drawing.Point(2, 2)
        Me.Panel_Group.Name = "Panel_Group"
        Me.Panel_Group.Size = New System.Drawing.Size(1110, 86)
        Me.Panel_Group.TabIndex = 202
        '
        'Lbl_company
        '
        Me.Lbl_company.AutoSize = True
        Me.Lbl_company.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_company.ForeColor = System.Drawing.Color.DarkBlue
        Me.Lbl_company.Location = New System.Drawing.Point(3, 27)
        Me.Lbl_company.Name = "Lbl_company"
        Me.Lbl_company.Size = New System.Drawing.Size(98, 23)
        Me.Lbl_company.TabIndex = 200
        Me.Lbl_company.Text = "Item Name"
        Me.Lbl_company.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk_details
        '
        Me.chk_details.AutoSize = True
        Me.chk_details.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.chk_details.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_details.Location = New System.Drawing.Point(898, 57)
        Me.chk_details.Name = "chk_details"
        Me.chk_details.Size = New System.Drawing.Size(95, 17)
        Me.chk_details.TabIndex = 199
        Me.chk_details.Text = "With &Details"
        Me.chk_details.UseVisualStyleBackColor = False
        '
        'Lbl_ItemName
        '
        Me.Lbl_ItemName.AutoSize = True
        Me.Lbl_ItemName.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl_ItemName.ForeColor = System.Drawing.Color.DarkBlue
        Me.Lbl_ItemName.Location = New System.Drawing.Point(3, 1)
        Me.Lbl_ItemName.Name = "Lbl_ItemName"
        Me.Lbl_ItemName.Size = New System.Drawing.Size(98, 23)
        Me.Lbl_ItemName.TabIndex = 198
        Me.Lbl_ItemName.Text = "Item Name"
        Me.Lbl_ItemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_Filter)
        Me.Panel2.Controls.Add(Me.TextBox1)
        Me.Panel2.Location = New System.Drawing.Point(4, 53)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(319, 29)
        Me.Panel2.TabIndex = 197
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
        'Btn_load
        '
        Me.Btn_load.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_load.Location = New System.Drawing.Point(1012, 49)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(90, 28)
        Me.Btn_load.TabIndex = 196
        Me.Btn_load.Text = "&Show"
        Me.Btn_load.UseVisualStyleBackColor = False
        '
        'DtpToDate
        '
        Me.DtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpToDate.Location = New System.Drawing.Point(986, 7)
        Me.DtpToDate.Name = "DtpToDate"
        Me.DtpToDate.Size = New System.Drawing.Size(117, 21)
        Me.DtpToDate.TabIndex = 194
        Me.DtpToDate.Value = New Date(2016, 3, 5, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(918, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 195
        Me.Label2.Text = "To Date"
        '
        'DtpFromDate
        '
        Me.DtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpFromDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpFromDate.Location = New System.Drawing.Point(790, 7)
        Me.DtpFromDate.Name = "DtpFromDate"
        Me.DtpFromDate.Size = New System.Drawing.Size(117, 21)
        Me.DtpFromDate.TabIndex = 192
        Me.DtpFromDate.Value = New Date(2016, 3, 5, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(711, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 193
        Me.Label1.Text = "From Date"
        '
        'Btn_Exit
        '
        Me.Btn_Exit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Exit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Exit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Exit.Location = New System.Drawing.Point(1020, 571)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Exit.TabIndex = 204
        Me.Btn_Exit.Text = "E&xit"
        Me.Btn_Exit.UseVisualStyleBackColor = False
        '
        'Btn_Excel
        '
        Me.Btn_Excel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_Excel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Excel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Excel.Location = New System.Drawing.Point(923, 571)
        Me.Btn_Excel.Name = "Btn_Excel"
        Me.Btn_Excel.Size = New System.Drawing.Size(90, 28)
        Me.Btn_Excel.TabIndex = 203
        Me.Btn_Excel.Text = "&Excel"
        Me.Btn_Excel.UseVisualStyleBackColor = False
        '
        'frmAcLedgerRpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClientSize = New System.Drawing.Size(1111, 604)
        Me.Controls.Add(Me.Btn_Exit)
        Me.Controls.Add(Me.Btn_Excel)
        Me.Controls.Add(Me.Panel_Group)
        Me.Controls.Add(Me.GridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAcLedgerRpt"
        Me.Text = "AC Ledger"
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Group.ResumeLayout(False)
        Me.Panel_Group.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel_Group As System.Windows.Forms.Panel
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents DtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Filter As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Lbl_ItemName As System.Windows.Forms.Label
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents Btn_Excel As System.Windows.Forms.Button
    Friend WithEvents chk_details As System.Windows.Forms.CheckBox
    Friend WithEvents Lbl_company As System.Windows.Forms.Label
End Class
