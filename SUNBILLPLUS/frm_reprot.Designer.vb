<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_reprot
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
        Me.DtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_load = New System.Windows.Forms.Button()
        Me.cmd_Cancel = New System.Windows.Forms.Button()
        Me.btn_sms = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DtpToDate
        '
        Me.DtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpToDate.Location = New System.Drawing.Point(104, 32)
        Me.DtpToDate.Name = "DtpToDate"
        Me.DtpToDate.Size = New System.Drawing.Size(117, 21)
        Me.DtpToDate.TabIndex = 198
        Me.DtpToDate.Value = New Date(2016, 3, 5, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 199
        Me.Label2.Text = "To Date"
        '
        'DtpFromDate
        '
        Me.DtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.DtpFromDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpFromDate.Location = New System.Drawing.Point(104, 4)
        Me.DtpFromDate.Name = "DtpFromDate"
        Me.DtpFromDate.Size = New System.Drawing.Size(117, 21)
        Me.DtpFromDate.TabIndex = 196
        Me.DtpFromDate.Value = New Date(2016, 3, 5, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 197
        Me.Label1.Text = "From Date"
        '
        'Btn_load
        '
        Me.Btn_load.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.Btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Btn_load.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_load.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_load.Location = New System.Drawing.Point(12, 69)
        Me.Btn_load.Name = "Btn_load"
        Me.Btn_load.Size = New System.Drawing.Size(90, 28)
        Me.Btn_load.TabIndex = 200
        Me.Btn_load.Text = "&Show"
        Me.Btn_load.UseVisualStyleBackColor = False
        '
        'cmd_Cancel
        '
        Me.cmd_Cancel.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.cmd_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmd_Cancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmd_Cancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmd_Cancel.Location = New System.Drawing.Point(108, 69)
        Me.cmd_Cancel.Name = "cmd_Cancel"
        Me.cmd_Cancel.Size = New System.Drawing.Size(90, 28)
        Me.cmd_Cancel.TabIndex = 201
        Me.cmd_Cancel.Text = "&Cancel"
        Me.cmd_Cancel.UseVisualStyleBackColor = False
        '
        'btn_sms
        '
        Me.btn_sms.BackColor = System.Drawing.Color.DeepSkyBlue
        Me.btn_sms.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_sms.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_sms.ForeColor = System.Drawing.Color.Black
        Me.btn_sms.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_sms.Location = New System.Drawing.Point(204, 69)
        Me.btn_sms.Name = "btn_sms"
        Me.btn_sms.Size = New System.Drawing.Size(80, 26)
        Me.btn_sms.TabIndex = 202
        Me.btn_sms.Text = "&Sms"
        Me.btn_sms.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 13)
        Me.Label3.TabIndex = 204
        '
        'frm_reprot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(297, 135)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_sms)
        Me.Controls.Add(Me.cmd_Cancel)
        Me.Controls.Add(Me.Btn_load)
        Me.Controls.Add(Me.DtpToDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DtpFromDate)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frm_reprot"
        Me.Text = "Bill Register"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Btn_load As System.Windows.Forms.Button
    Friend WithEvents cmd_Cancel As System.Windows.Forms.Button
    Friend WithEvents btn_sms As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
