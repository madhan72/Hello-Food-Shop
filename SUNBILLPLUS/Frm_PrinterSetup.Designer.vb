<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PrinterSetup
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
        Me.BasePanel = New System.Windows.Forms.Panel()
        Me.Grp_Add = New System.Windows.Forms.GroupBox()
        Me.cmddelete = New System.Windows.Forms.Button()
        Me.cmdexit = New System.Windows.Forms.Button()
        Me.cmdadd = New System.Windows.Forms.Button()
        Me.cmdedit = New System.Windows.Forms.Button()
        Me.Grp_Process = New System.Windows.Forms.GroupBox()
        Me.Chk_DirecPrint = New System.Windows.Forms.CheckBox()
        Me.Cbo_PaperName = New System.Windows.Forms.ComboBox()
        Me.Cbo_PrinterName = New System.Windows.Forms.ComboBox()
        Me.txt_Rptname = New System.Windows.Forms.TextBox()
        Me.txt_Rpttype = New System.Windows.Forms.TextBox()
        Me.txt_Process = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GridPanel = New System.Windows.Forms.Panel()
        Me.GridView1 = New System.Windows.Forms.DataGridView()
        Me.FilterPanel = New System.Windows.Forms.Panel()
        Me.txt_search = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Grp_Save = New System.Windows.Forms.GroupBox()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdok = New System.Windows.Forms.Button()
        Me.BasePanel.SuspendLayout()
        Me.Grp_Add.SuspendLayout()
        Me.Grp_Process.SuspendLayout()
        Me.GridPanel.SuspendLayout()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FilterPanel.SuspendLayout()
        Me.Grp_Save.SuspendLayout()
        Me.SuspendLayout()
        '
        'BasePanel
        '
        Me.BasePanel.BackColor = System.Drawing.Color.Gainsboro
        Me.BasePanel.Controls.Add(Me.Grp_Add)
        Me.BasePanel.Controls.Add(Me.Grp_Process)
        Me.BasePanel.Controls.Add(Me.GridPanel)
        Me.BasePanel.Controls.Add(Me.Grp_Save)
        Me.BasePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BasePanel.Location = New System.Drawing.Point(0, 0)
        Me.BasePanel.Name = "BasePanel"
        Me.BasePanel.Size = New System.Drawing.Size(879, 445)
        Me.BasePanel.TabIndex = 1
        '
        'Grp_Add
        '
        Me.Grp_Add.BackColor = System.Drawing.Color.Gainsboro
        Me.Grp_Add.Controls.Add(Me.cmddelete)
        Me.Grp_Add.Controls.Add(Me.cmdexit)
        Me.Grp_Add.Controls.Add(Me.cmdadd)
        Me.Grp_Add.Controls.Add(Me.cmdedit)
        Me.Grp_Add.Location = New System.Drawing.Point(505, 392)
        Me.Grp_Add.Name = "Grp_Add"
        Me.Grp_Add.Size = New System.Drawing.Size(371, 42)
        Me.Grp_Add.TabIndex = 35
        Me.Grp_Add.TabStop = False
        '
        'cmddelete
        '
        Me.cmddelete.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmddelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmddelete.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmddelete.Location = New System.Drawing.Point(214, 10)
        Me.cmddelete.Name = "cmddelete"
        Me.cmddelete.Size = New System.Drawing.Size(75, 30)
        Me.cmddelete.TabIndex = 13
        Me.cmddelete.Text = "&Delete"
        Me.cmddelete.UseVisualStyleBackColor = False
        '
        'cmdexit
        '
        Me.cmdexit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdexit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdexit.ForeColor = System.Drawing.Color.Black
        Me.cmdexit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdexit.Location = New System.Drawing.Point(291, 10)
        Me.cmdexit.Name = "cmdexit"
        Me.cmdexit.Size = New System.Drawing.Size(75, 30)
        Me.cmdexit.TabIndex = 12
        Me.cmdexit.Text = "E&xit"
        Me.cmdexit.UseVisualStyleBackColor = False
        '
        'cmdadd
        '
        Me.cmdadd.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdadd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdadd.ForeColor = System.Drawing.Color.Black
        Me.cmdadd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdadd.Location = New System.Drawing.Point(58, 10)
        Me.cmdadd.Name = "cmdadd"
        Me.cmdadd.Size = New System.Drawing.Size(75, 30)
        Me.cmdadd.TabIndex = 9
        Me.cmdadd.Text = "&Add"
        Me.cmdadd.UseVisualStyleBackColor = False
        '
        'cmdedit
        '
        Me.cmdedit.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdedit.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdedit.ForeColor = System.Drawing.Color.Black
        Me.cmdedit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdedit.Location = New System.Drawing.Point(136, 10)
        Me.cmdedit.Name = "cmdedit"
        Me.cmdedit.Size = New System.Drawing.Size(75, 30)
        Me.cmdedit.TabIndex = 10
        Me.cmdedit.Text = "&Edit"
        Me.cmdedit.UseVisualStyleBackColor = False
        '
        'Grp_Process
        '
        Me.Grp_Process.BackColor = System.Drawing.Color.Gainsboro
        Me.Grp_Process.Controls.Add(Me.Chk_DirecPrint)
        Me.Grp_Process.Controls.Add(Me.Cbo_PaperName)
        Me.Grp_Process.Controls.Add(Me.Cbo_PrinterName)
        Me.Grp_Process.Controls.Add(Me.txt_Rptname)
        Me.Grp_Process.Controls.Add(Me.txt_Rpttype)
        Me.Grp_Process.Controls.Add(Me.txt_Process)
        Me.Grp_Process.Controls.Add(Me.Label6)
        Me.Grp_Process.Controls.Add(Me.Label5)
        Me.Grp_Process.Controls.Add(Me.Label4)
        Me.Grp_Process.Controls.Add(Me.Label2)
        Me.Grp_Process.Controls.Add(Me.Label1)
        Me.Grp_Process.Location = New System.Drawing.Point(506, 1)
        Me.Grp_Process.Name = "Grp_Process"
        Me.Grp_Process.Size = New System.Drawing.Size(367, 390)
        Me.Grp_Process.TabIndex = 1
        Me.Grp_Process.TabStop = False
        '
        'Chk_DirecPrint
        '
        Me.Chk_DirecPrint.AutoSize = True
        Me.Chk_DirecPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chk_DirecPrint.Location = New System.Drawing.Point(106, 166)
        Me.Chk_DirecPrint.Name = "Chk_DirecPrint"
        Me.Chk_DirecPrint.Size = New System.Drawing.Size(90, 17)
        Me.Chk_DirecPrint.TabIndex = 10
        Me.Chk_DirecPrint.Text = "Direct Print"
        Me.Chk_DirecPrint.UseVisualStyleBackColor = True
        '
        'Cbo_PaperName
        '
        Me.Cbo_PaperName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Cbo_PaperName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cbo_PaperName.FormattingEnabled = True
        Me.Cbo_PaperName.Location = New System.Drawing.Point(103, 132)
        Me.Cbo_PaperName.Name = "Cbo_PaperName"
        Me.Cbo_PaperName.Size = New System.Drawing.Size(186, 21)
        Me.Cbo_PaperName.TabIndex = 9
        '
        'Cbo_PrinterName
        '
        Me.Cbo_PrinterName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Cbo_PrinterName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cbo_PrinterName.FormattingEnabled = True
        Me.Cbo_PrinterName.Location = New System.Drawing.Point(103, 103)
        Me.Cbo_PrinterName.Name = "Cbo_PrinterName"
        Me.Cbo_PrinterName.Size = New System.Drawing.Size(259, 21)
        Me.Cbo_PrinterName.TabIndex = 8
        '
        'txt_Rptname
        '
        Me.txt_Rptname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Rptname.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Rptname.Location = New System.Drawing.Point(103, 74)
        Me.txt_Rptname.Name = "txt_Rptname"
        Me.txt_Rptname.Size = New System.Drawing.Size(259, 21)
        Me.txt_Rptname.TabIndex = 7
        '
        'txt_Rpttype
        '
        Me.txt_Rpttype.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Rpttype.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Rpttype.Location = New System.Drawing.Point(103, 48)
        Me.txt_Rpttype.Name = "txt_Rpttype"
        Me.txt_Rpttype.Size = New System.Drawing.Size(259, 21)
        Me.txt_Rpttype.TabIndex = 6
        '
        'txt_Process
        '
        Me.txt_Process.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_Process.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_Process.Location = New System.Drawing.Point(103, 22)
        Me.txt_Process.Name = "txt_Process"
        Me.txt_Process.Size = New System.Drawing.Size(259, 21)
        Me.txt_Process.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 14)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Paper Name"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 14)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Printer Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Rpt Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Rpt Type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Process"
        '
        'GridPanel
        '
        Me.GridPanel.Controls.Add(Me.GridView1)
        Me.GridPanel.Controls.Add(Me.FilterPanel)
        Me.GridPanel.Location = New System.Drawing.Point(0, 0)
        Me.GridPanel.Name = "GridPanel"
        Me.GridPanel.Size = New System.Drawing.Size(500, 442)
        Me.GridPanel.TabIndex = 0
        '
        'GridView1
        '
        Me.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridView1.Location = New System.Drawing.Point(3, 35)
        Me.GridView1.Name = "GridView1"
        Me.GridView1.Size = New System.Drawing.Size(494, 404)
        Me.GridView1.TabIndex = 1
        '
        'FilterPanel
        '
        Me.FilterPanel.Controls.Add(Me.txt_search)
        Me.FilterPanel.Controls.Add(Me.Label3)
        Me.FilterPanel.Location = New System.Drawing.Point(3, 3)
        Me.FilterPanel.Name = "FilterPanel"
        Me.FilterPanel.Size = New System.Drawing.Size(494, 30)
        Me.FilterPanel.TabIndex = 0
        '
        'txt_search
        '
        Me.txt_search.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_search.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_search.Location = New System.Drawing.Point(41, 5)
        Me.txt_search.Name = "txt_search"
        Me.txt_search.Size = New System.Drawing.Size(448, 21)
        Me.txt_search.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Filter"
        '
        'Grp_Save
        '
        Me.Grp_Save.BackColor = System.Drawing.Color.Gainsboro
        Me.Grp_Save.Controls.Add(Me.cmdcancel)
        Me.Grp_Save.Controls.Add(Me.cmdok)
        Me.Grp_Save.Location = New System.Drawing.Point(504, 392)
        Me.Grp_Save.Name = "Grp_Save"
        Me.Grp_Save.Size = New System.Drawing.Size(369, 42)
        Me.Grp_Save.TabIndex = 34
        Me.Grp_Save.TabStop = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdcancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.Color.Black
        Me.cmdcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdcancel.Location = New System.Drawing.Point(290, 10)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.Size = New System.Drawing.Size(75, 30)
        Me.cmdcancel.TabIndex = 6
        Me.cmdcancel.Text = "&Cancel"
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'cmdok
        '
        Me.cmdok.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmdok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdok.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdok.ForeColor = System.Drawing.Color.Black
        Me.cmdok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdok.Location = New System.Drawing.Point(213, 10)
        Me.cmdok.Name = "cmdok"
        Me.cmdok.Size = New System.Drawing.Size(75, 30)
        Me.cmdok.TabIndex = 5
        Me.cmdok.Text = "&Save"
        Me.cmdok.UseVisualStyleBackColor = False
        '
        'Frm_PrinterSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 445)
        Me.Controls.Add(Me.BasePanel)
        Me.Name = "Frm_PrinterSetup"
        Me.Text = "Frm_PrinterSetup"
        Me.BasePanel.ResumeLayout(False)
        Me.Grp_Add.ResumeLayout(False)
        Me.Grp_Process.ResumeLayout(False)
        Me.Grp_Process.PerformLayout()
        Me.GridPanel.ResumeLayout(False)
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FilterPanel.ResumeLayout(False)
        Me.FilterPanel.PerformLayout()
        Me.Grp_Save.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BasePanel As System.Windows.Forms.Panel
    Friend WithEvents Grp_Add As System.Windows.Forms.GroupBox
    Friend WithEvents cmddelete As System.Windows.Forms.Button
    Friend WithEvents cmdexit As System.Windows.Forms.Button
    Friend WithEvents cmdadd As System.Windows.Forms.Button
    Friend WithEvents cmdedit As System.Windows.Forms.Button
    Friend WithEvents Grp_Process As System.Windows.Forms.GroupBox
    Friend WithEvents Chk_DirecPrint As System.Windows.Forms.CheckBox
    Friend WithEvents Cbo_PaperName As System.Windows.Forms.ComboBox
    Friend WithEvents Cbo_PrinterName As System.Windows.Forms.ComboBox
    Friend WithEvents txt_Rptname As System.Windows.Forms.TextBox
    Friend WithEvents txt_Rpttype As System.Windows.Forms.TextBox
    Friend WithEvents txt_Process As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GridPanel As System.Windows.Forms.Panel
    Friend WithEvents GridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents FilterPanel As System.Windows.Forms.Panel
    Friend WithEvents txt_search As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Grp_Save As System.Windows.Forms.GroupBox
    Friend WithEvents cmdcancel As System.Windows.Forms.Button
    Friend WithEvents cmdok As System.Windows.Forms.Button
End Class
