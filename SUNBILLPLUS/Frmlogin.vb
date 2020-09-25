Imports System.Data.SqlClient
Imports System.Configuration
Imports USERS_APP

Public Class Frmlogin

    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim dr As SqlDataReader

    Private Sub cmd_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancel.Click
        Call closeconn()
        End
    End Sub

    Private Sub cmd_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ok.Click
        Dim Password_t As String

        ''************* FOR SELVAS LOCAL DB ONLY ********************
        If LCase(txt_username.Text) = "ups" And UCase(Appversion_t) = "VERSION1" Then
            MsgBox("Login is Failed...Try again !", MsgBoxStyle.Critical, "Login Denied")
            txt_password.Text = ""
            txt_username.Text = ""
            txt_username.Focus()
            Exit Sub
        End If

        ' ************* FOR SELVAS EXTERNAL HDD DB ONLY ********************
        If LCase(txt_username.Text) <> "ups" And UCase(Appversion_t) <> "VERSION1" Then
            MsgBox("Login is Failed...Try again !", MsgBoxStyle.Critical, "Login Denied")
            txt_password.Text = ""
            txt_username.Text = ""
            txt_username.Focus()
            Exit Sub
        End If

        Call opnconn()

        If Len(Trim(txt_username.Text)) = 0 Then
            MessageBox.Show("Please enter user name", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_username.Focus()
            Exit Sub
        End If

        Try
            'cmd = New SqlCommand("SELECT Uname,pwd,uid,isnull(noofdays,0) as Noofdays,Isnull(Fromdate,'') As Fromdate,Isnull(Todate,''), " _
            '                     & " Isnull(Usertype,'') As Usertype FROM Users WHERE uname = @username AND isnull(pwd,'') = @UserPassword", conn)
            'Dim uName As New SqlParameter("@username", SqlDbType.NChar)
            'Dim uPassword As New SqlParameter("@UserPassword", SqlDbType.NChar)
            'uName.Value = txt_username.Text
            'uPassword.Value = txt_password.Text

            'cmd.Parameters.Add(uName)
            'cmd.Parameters.Add(uPassword)

            'cmd.Connection = conn

            cmd = New SqlCommand("SELECT Uname,Pwd,uid,isnull(noofdays,0) as Noofdays,Isnull(Fromdate,'') As Fromdate,Isnull(Todate,'') AS Todate,Usertype FROM Users WHERE uname = @username", conn)
            Dim uName As New SqlParameter("@username", SqlDbType.NChar)
            'Dim uPassword As New SqlParameter("@UserPassword", SqlDbType.NChar)
            Genuname = txt_username.Text
            uName.Value = txt_username.Text
            'uPassword.Value = txt_password.Text

            cmd.Parameters.Add(uName)
            'cmd.Parameters.Add(uPassword)

            cmd.Connection = conn


            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            Dim Login As Object = 0

            If dr.HasRows Then
                dr.Read()

                Password_t = UserModule1.Decrypt(dr.GetValue(1))

                If Password_t = txt_password.Text Then
                    Genuname = dr.GetValue(0)
                    Genuid = dr.GetValue(2)
                    Gendays = dr.GetValue(3)
                    GenFromdate = dr.GetDateTime(4)
                    If GenFromdate = Nothing Then
                    Else
                        GenFromdate = GenFromdate.ToString("dd/MM/yyyy")
                    End If
                    GenTodate = dr.GetDateTime(5)
                    If GenTodate = Nothing Then
                    Else
                        GenTodate = GenTodate.ToString("dd/MM/yyyy")
                    End If
                    Genutype = dr.GetValue(6)
                    Login = dr(Login)
                Else
                    Login = 0
                End If


            End If

            If Login = Nothing Then

                MsgBox("Login is Failed...Try again !", MsgBoxStyle.Critical, "Login Denied")

                txt_username.Clear()
                txt_password.Clear()
                txt_username.Focus()

            Else

                ProgressBar1.Visible = True
                ProgressBar1.Maximum = 5000
                ProgressBar1.Minimum = 0
                ProgressBar1.Value = 4
                ProgressBar1.Step = 1

                For i = 0 To 5000
                    ProgressBar1.PerformStep()
                Next

                'FrmMain.ToolStripStatusLabel2.Text = UserName.Text
                dr.Close()
                Me.Hide()

                'MDIParent1.Close() 'bcoz userlogon click after mdiform show, does not effect menu visible false,true(bcoz its run in mdiform load event),
                'so here close form and open
                'MDIParent1.Show()

                Dim frmcompsel = New Frm_CompSelect
                frmcompsel.ShowInTaskbar = False
                frmcompsel.Init()
                frmcompsel.StartPosition = FormStartPosition.CenterScreen
                frmcompsel.ShowDialog()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Call closeconn()
    End Sub

    Private Sub Frmlogin_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Call closeconn()
    End Sub

    Private Sub Frmlogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call main()
            'Me.StartPosition = FormStartPosition.CenterScreen
            Call CenterThisForm(Me)
            txt_username.Focus()
            'following lines are used to change system date format at runtinme
            Dim TmpDateFormat As System.Globalization.CultureInfo
            TmpDateFormat = New System.Globalization.CultureInfo("en-US")
            TmpDateFormat.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture = TmpDateFormat
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_username_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_username.GotFocus
        txt_username.BackColor = Color.CadetBlue
    End Sub

    Private Sub txt_username_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_username.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_password.Focus()
        End If
    End Sub

    Private Sub txt_password_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_password.GotFocus
        txt_password.BackColor = Color.CadetBlue
    End Sub

    Private Sub txt_password_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_password.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmd_ok.Focus()
        End If
    End Sub

    Private Sub txt_username_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_username.LostFocus
        txt_username.BackColor = Color.White
    End Sub

    Private Sub txt_password_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_password.LostFocus
        txt_password.BackColor = Color.White
    End Sub

    Private Sub txt_username_Click(sender As Object, e As EventArgs) Handles txt_username.Click

    End Sub
End Class
