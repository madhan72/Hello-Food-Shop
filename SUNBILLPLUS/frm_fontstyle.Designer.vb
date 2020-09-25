<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_fontstyle
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
        Me.txt_font = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.cmd_cancel = New System.Windows.Forms.Button()
        Me.Lst_size = New System.Windows.Forms.ListBox()
        Me.Lst_style = New System.Windows.Forms.ListBox()
        Me.Lst_font = New System.Windows.Forms.ListBox()
        Me.txt_size = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_style = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txt_font
        '
        Me.txt_font.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_font.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_font.Location = New System.Drawing.Point(4, 23)
        Me.txt_font.Name = "txt_font"
        Me.txt_font.Size = New System.Drawing.Size(178, 21)
        Me.txt_font.TabIndex = 106
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 107
        Me.Label5.Text = "Font:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Turquoise
        Me.Panel1.Controls.Add(Me.cmd_ok)
        Me.Panel1.Controls.Add(Me.cmd_cancel)
        Me.Panel1.Controls.Add(Me.Lst_size)
        Me.Panel1.Controls.Add(Me.Lst_style)
        Me.Panel1.Controls.Add(Me.Lst_font)
        Me.Panel1.Controls.Add(Me.txt_size)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txt_style)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txt_font)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(478, 202)
        Me.Panel1.TabIndex = 108
        '
        'cmd_ok
        '
        Me.cmd_ok.BackColor = System.Drawing.Color.DarkTurquoise
        Me.cmd_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ok.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ok.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_ok.Location = New System.Drawing.Point(290, 171)
        Me.cmd_ok.Name = "cmd_ok"
        Me.cmd_ok.Size = New System.Drawing.Size(90, 28)
        Me.cmd_ok.TabIndex = 136
        Me.cmd_ok.Text = "&Ok"
        Me.cmd_ok.UseVisualStyleBackColor = False
        '
        'cmd_cancel
        '
        Me.cmd_cancel.BackColor = System.Drawing.Color.DarkTurquoise
        Me.cmd_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_cancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancel.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_cancel.Location = New System.Drawing.Point(383, 171)
        Me.cmd_cancel.Name = "cmd_cancel"
        Me.cmd_cancel.Size = New System.Drawing.Size(90, 28)
        Me.cmd_cancel.TabIndex = 135
        Me.cmd_cancel.Text = "&Cancel"
        Me.cmd_cancel.UseVisualStyleBackColor = False
        '
        'Lst_size
        '
        Me.Lst_size.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lst_size.FormattingEnabled = True
        Me.Lst_size.ItemHeight = 18
        Me.Lst_size.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.Lst_size.Location = New System.Drawing.Point(367, 47)
        Me.Lst_size.Name = "Lst_size"
        Me.Lst_size.ScrollAlwaysVisible = True
        Me.Lst_size.Size = New System.Drawing.Size(106, 112)
        Me.Lst_size.TabIndex = 114
        '
        'Lst_style
        '
        Me.Lst_style.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lst_style.FormattingEnabled = True
        Me.Lst_style.ItemHeight = 18
        Me.Lst_style.Items.AddRange(New Object() {"Regular", "Bold ", "Italic", "Bold Italic"})
        Me.Lst_style.Location = New System.Drawing.Point(186, 47)
        Me.Lst_style.Name = "Lst_style"
        Me.Lst_style.ScrollAlwaysVisible = True
        Me.Lst_style.Size = New System.Drawing.Size(178, 112)
        Me.Lst_style.TabIndex = 113
        '
        'Lst_font
        '
        Me.Lst_font.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lst_font.FormattingEnabled = True
        Me.Lst_font.ItemHeight = 18
        Me.Lst_font.Location = New System.Drawing.Point(4, 47)
        Me.Lst_font.Name = "Lst_font"
        Me.Lst_font.ScrollAlwaysVisible = True
        Me.Lst_font.Size = New System.Drawing.Size(178, 112)
        Me.Lst_font.TabIndex = 112
        '
        'txt_size
        '
        Me.txt_size.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_size.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_size.Location = New System.Drawing.Point(367, 23)
        Me.txt_size.Name = "txt_size"
        Me.txt_size.Size = New System.Drawing.Size(105, 21)
        Me.txt_size.TabIndex = 110
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(369, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 111
        Me.Label2.Text = "Size :"
        '
        'txt_style
        '
        Me.txt_style.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_style.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_style.Location = New System.Drawing.Point(186, 23)
        Me.txt_style.Name = "txt_style"
        Me.txt_style.Size = New System.Drawing.Size(178, 21)
        Me.txt_style.TabIndex = 108
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(186, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 109
        Me.Label1.Text = "Font Style : "
        '
        'frm_fontstyle
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 201)
        Me.Controls.Add(Me.Panel1)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(491, 239)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(491, 239)
        Me.Name = "frm_fontstyle"
        Me.Text = "Font"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txt_font As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txt_size As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_style As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Lst_font As System.Windows.Forms.ListBox
    Friend WithEvents Lst_size As System.Windows.Forms.ListBox
    Friend WithEvents Lst_style As System.Windows.Forms.ListBox
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
End Class
