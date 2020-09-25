Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports Reports_SUNBILLPLUS_App
Imports SunUtilities_APP


Public Class frm_reprot
    Dim cmd As SqlCommand
    Dim da, da_mobile As SqlDataAdapter
    Dim ds, ds_mobile, ds_party As New DataSet
    Dim bs As New BindingSource
    Dim dr As SqlDataReader
    Public Databasename_t As String = "SUNMARKETTRADE", Filtercolnmae_t As String

    Dim i As Integer
    Dim Process_t As String
    Dim Url_t As String, Username_t As String, Password_t As String, SenderName_t As String, Message_t As String



    Private Sub DtpFromDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpFromDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmd_Cancel.Focus()
        End If
    End Sub

    Private Sub cmd_Cancel_Click(sender As Object, e As EventArgs) Handles cmd_Cancel.Click
        Me.Hide()
    End Sub

     Private Sub cmd_Cancel_KeyDown(sender As Object, e As KeyEventArgs) Handles cmd_Cancel.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_load.Focus()
        End If
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click
        Try
            Dim rpt As New Frm_Reports_Init

            Cursor = Cursors.WaitCursor
            rpt.ShowInTaskbar = False
            rpt.Init(conn, "Bill Register", Servername_t, 0, DtpFromDate.Value, DtpToDate.Value, "", "SUNMARKETTRADE", _
                     Gencompid, False, "", False, Nothing, Nothing)
            rpt.StartPosition = FormStartPosition.CenterScreen
            rpt.ShowDialog()
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frm_reprot_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DtpFromDate.Value = Today
            DtpToDate.Value = Today
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Outstanding_Sms()
        Try
            'If Retailsmsflag_t = True Then

            i = 0
H:
            If i = 0 Then Process_t = "URL"
            If i = 1 Then Process_t = "USERNAME"
            If i = 2 Then Process_t = "PASSWORD"
            If i = 3 Then Process_t = "SENDERNAME"
            If i = 4 Then Process_t = "MESSAGE FOOTER"

            ds_mobile = Nothing
            cmd = Nothing
            cmd = New SqlCommand("SELECT Value3 FROM SmsSettings WHERE Process ='" & Process_t & "'", conn)
            cmd.CommandType = CommandType.Text
            da_mobile = New SqlDataAdapter(cmd)
            ds_mobile = New DataSet
            ds_mobile.Clear()
            da_mobile.Fill(ds_mobile)

            i = i + 1
            If Process_t = "URL" Then Url_t = ds_mobile.Tables(0).Rows(0).Item("VALUE3")
            If Process_t = "USERNAME" Then Username_t = ds_mobile.Tables(0).Rows(0).Item("VALUE3")
            If Process_t = "PASSWORD" Then Password_t = ds_mobile.Tables(0).Rows(0).Item("VALUE3")
            If Process_t = "SENDERNAME" Then SenderName_t = ds_mobile.Tables(0).Rows(0).Item("VALUE3")
            If Process_t = "MESSAGE FOOTER" Then Message_t = ds_mobile.Tables(0).Rows(0).Item("VALUE3")

            If i < 5 Then GoTo H

            Dim rwcnt_t As Double



            ds_party.Clear()
            ds_party = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "PARTY_PENDING_RPT"
            cmd.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = DtpFromDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = DtpToDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@Compid", SqlDbType.VarChar).Value = Gencompid
            da_mobile = New SqlDataAdapter(cmd)
            ds_party = New DataSet
            da_mobile.Fill(ds_party)

            rwcnt_t = ds_party.Tables(0).Rows.Count

            For i = 0 To rwcnt_t - 1

                Message_t = ds_party.Tables(0).Rows(i).Item("message")

                ds_mobile.Clear()
                ds_mobile = Nothing
                cmd = Nothing
                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "PARTY_MOBILENO"
                cmd.Parameters.Add("@PTYCODE", SqlDbType.VarChar).Value = ds_party.Tables(0).Rows(i).Item("PTYCODE")
                da_mobile = New SqlDataAdapter(cmd)
                ds_mobile = New DataSet
                da_mobile.Fill(ds_mobile)
                If ds_mobile.Tables(0).Rows.Count > 0 Then
                    Label3.Text = ds_party.Tables(0).Rows(i).Item("PARTYNAME")
                    Call Sms_RS_OpenProc(conn, ds_mobile, Url_t, Username_t, Password_t, SenderName_t, Message_t, "0")
                End If
                Threading.Thread.Sleep(1000)
            Next

            Label3.Text = ""
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_sms_Click(sender As Object, e As EventArgs) Handles btn_sms.Click
        Try
            Outstanding_Sms()
            MsgBox("Send Sucessfully.")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
