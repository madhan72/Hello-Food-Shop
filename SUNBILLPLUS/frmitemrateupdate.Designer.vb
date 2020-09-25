<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmitemrateupdate
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
        Me.txt_itemdesc = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_tamildesc = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_mrp = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_costrate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_selrate = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_remarks = New System.Windows.Forms.TextBox()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.cmd_cancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txt_itemdesc
        '
        Me.txt_itemdesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_itemdesc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_itemdesc.Location = New System.Drawing.Point(124, 23)
        Me.txt_itemdesc.Name = "txt_itemdesc"
        Me.txt_itemdesc.Size = New System.Drawing.Size(311, 21)
        Me.txt_itemdesc.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(35, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Item Desc"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(35, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Tamil Desc"
        '
        'txt_tamildesc
        '
        Me.txt_tamildesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_tamildesc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_tamildesc.Location = New System.Drawing.Point(124, 50)
        Me.txt_tamildesc.Name = "txt_tamildesc"
        Me.txt_tamildesc.Size = New System.Drawing.Size(311, 21)
        Me.txt_tamildesc.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(35, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "MRP "
        '
        'txt_mrp
        '
        Me.txt_mrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_mrp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_mrp.Location = New System.Drawing.Point(124, 104)
        Me.txt_mrp.Name = "txt_mrp"
        Me.txt_mrp.Size = New System.Drawing.Size(104, 21)
        Me.txt_mrp.TabIndex = 2
        Me.txt_mrp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(35, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Cost Rate"
        '
        'txt_costrate
        '
        Me.txt_costrate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_costrate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_costrate.Location = New System.Drawing.Point(124, 77)
        Me.txt_costrate.Name = "txt_costrate"
        Me.txt_costrate.Size = New System.Drawing.Size(104, 21)
        Me.txt_costrate.TabIndex = 0
        Me.txt_costrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(242, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Selling Rate"
        '
        'txt_selrate
        '
        Me.txt_selrate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_selrate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_selrate.Location = New System.Drawing.Point(330, 79)
        Me.txt_selrate.Name = "txt_selrate"
        Me.txt_selrate.Size = New System.Drawing.Size(104, 21)
        Me.txt_selrate.TabIndex = 1
        Me.txt_selrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(35, 133)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Remarks"
        '
        'txt_remarks
        '
        Me.txt_remarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_remarks.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_remarks.Location = New System.Drawing.Point(124, 131)
        Me.txt_remarks.Name = "txt_remarks"
        Me.txt_remarks.Size = New System.Drawing.Size(310, 21)
        Me.txt_remarks.TabIndex = 15
        '
        'cmd_ok
        '
        Me.cmd_ok.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_ok.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_ok.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_ok.Location = New System.Drawing.Point(254, 168)
        Me.cmd_ok.Name = "cmd_ok"
        Me.cmd_ok.Size = New System.Drawing.Size(90, 28)
        Me.cmd_ok.TabIndex = 3
        Me.cmd_ok.Text = "&Update"
        Me.cmd_ok.UseVisualStyleBackColor = False
        '
        'cmd_cancel
        '
        Me.cmd_cancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmd_cancel.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_cancel.ForeColor = System.Drawing.SystemColors.MenuText
        Me.cmd_cancel.Location = New System.Drawing.Point(345, 168)
        Me.cmd_cancel.Name = "cmd_cancel"
        Me.cmd_cancel.Size = New System.Drawing.Size(90, 28)
        Me.cmd_cancel.TabIndex = 136
        Me.cmd_cancel.Text = "&Cancel"
        Me.cmd_cancel.UseVisualStyleBackColor = False
        '
        'frmitemrateupdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSkyBlue
        Me.ClientSize = New System.Drawing.Size(435, 198)
        Me.Controls.Add(Me.cmd_ok)
        Me.Controls.Add(Me.cmd_cancel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_remarks)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_selrate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_costrate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txt_mrp)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txt_tamildesc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_itemdesc)
        Me.Name = "frmitemrateupdate"
        Me.Text = "Item Rate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txt_itemdesc As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_tamildesc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_mrp As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_costrate As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_selrate As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_remarks As System.Windows.Forms.TextBox
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
    Friend WithEvents cmd_cancel As System.Windows.Forms.Button
End Class
