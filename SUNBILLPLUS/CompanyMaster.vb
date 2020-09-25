Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Public Class CompanyMaster
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds1, ds_procs, tmpds As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Compid_t As Double
    Dim accountid_t As Double
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer
    Dim Stateid As Double
    Dim Sqlstr As String, Filtercolnmae_t As String, Defaultprocess_t As String
    Const Process = "Party"

    Private Sub Load_combotype()
        Try
            Dim proscnt_t As Integer
            proscnt_t = ds_procs.Tables(0).Rows.Count
            If proscnt_t > 0 Then
                For i = 0 To proscnt_t - 1
                    'cbo_Process.Items.Add(ds_procs.Tables(0).Rows(i).Item("Process").ToString)
                Next
                'cbo_Process.Text = (ds_procs.Tables(0).Rows(0).Item("Process").ToString)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try

            'Sqlstr = "Select Process, Seqno From Party_Process  Order By Process"
            'cmd = New SqlCommand(Sqlstr, conn)
            'cmd.CommandType = CommandType.Text
            'da = New SqlDataAdapter(cmd)
            'ds_procs = New DataSet
            'ds_procs.Clear()
            'da.Fill(ds_procs)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Try
            Call clearchars()
            Sqlstr = "Select C.Compid, C.Compname From Company C "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

            Dim cnt, i As Integer
            cnt = ds.Tables(0).Rows.Count
            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False


            Dim tables As DataTableCollection = ds.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.Refresh()

            GridView1.Columns(0).ReadOnly = True
            GridView1.Columns(1).ReadOnly = True

            txt_search.Text = ""

            If strSearchString <> "" Then
                For i = 0 To GridView1.Rows.Count - 1 'its used for focus cursor after save which name is edit
                    If InStr(1, GridView1.Rows(i).Cells(0).Value.ToString, strSearchString, CompareMethod.Text) Then
                        GridView1.Rows(i).Selected = True
                        GridView1.CurrentCell = GridView1.Rows(i).Cells(1)
                        Exit For
                    End If
                Next
            End If

            Filtercolnmae_t = "Compname"
            Colindex_t = 1
            If GridView1.Rows.Count > 0 Then
                If GridView1.CurrentCell Is Nothing Then
                    Compid_t = GridView1.Item(0, 0).Value
                Else
                    Compid_t = GridView1.Item(0, GridView1.CurrentCell.RowIndex).Value
                End If

                Call storechars(Compid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Width = 380
            GridView1.Columns(1).HeaderText = "Name"

            Dim font As New Font( _
                GridView1.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 28
            GridView1.ColumnHeadersDefaultCellStyle.Font = font
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            cnt = GridView1.Columns.Count
            For i = 0 To cnt - 1
                GridView1.Columns(i).DefaultCellStyle.Font = font
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        Try
            If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
                GroupBox3.Enabled = True
                GridView1.Enabled = False

                GroupBox1.Visible = False
                GroupBox2.Visible = True
                cmddelete.Enabled = False

            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
                GroupBox3.Enabled = False
                GridView1.Enabled = True

                GroupBox1.Visible = True
                GroupBox2.Visible = False
                cmddelete.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()

        'Call ClearTextBoxes()
        Call ClearTextBoxes1()
        accountid_t = 0
        Compid_t = 0

    End Sub

    Public Sub ClearTextBoxes1(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        Try

            If ctlcol Is Nothing Then ctlcol = Me.Controls
            For Each ctl As Control In ctlcol
                If TypeOf (ctl) Is TextBox Then
                    If DirectCast(ctl, TextBox).Name = "txt_search" Then 'particular text box will not cleared
                    Else
                        DirectCast(ctl, TextBox).Clear()
                    End If
                Else
                    If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                        ClearTextBoxes1(ctl.Controls)
                    End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub storechars(Optional ByVal Compid_v As Double = 0)
        Try
            Sqlstr = "Select C.Compid, C.Compname, C.Add1, C.Add2, C.Add3,isnull(c.gstin,'') as GSTIN, C.Add4, C.Phone, C.Tngst, C.Cst, C.Cstdate AS CSTDATE ,C.Email,C.Frommail,C.Mailpassword, " _
                   & "C.Prefix,C.Suffix,Isnull(C.Seqno,0) As Seqno,Isnull(C.Noofdigits,0) As Noofdigit,Isnull(C.Showcomplog,0) As Showcomplog " _
                   & "From Company C Where C.Compid = " & Compid_v & " "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds1 = New DataSet
            ds1.Clear()
            da.Fill(ds1)

            Dim rowid_t As Integer
            Call clearchars()
            rowid_t = ds1.Tables(0).Rows.Count

            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1
            Compid_t = ds1.Tables(0).Rows(rowid_t).Item("Compid")
            txt_companyname.Text = ds1.Tables(0).Rows(rowid_t).Item("Compname").ToString

            If ds1.Tables(0).Rows(rowid_t).Item("Cstdate").ToString = "" Then
                DTP_Cstdate.Checked = False
            Else
                DTP_Cstdate.Checked = True
                DTP_Cstdate.Value = ds1.Tables(0).Rows(rowid_t).Item("Cstdate").ToString
            End If

            txt_address1.Text = ds1.Tables(0).Rows(rowid_t).Item("Add1").ToString
            txt_address2.Text = ds1.Tables(0).Rows(rowid_t).Item("Add2").ToString
            txt_address3.Text = ds1.Tables(0).Rows(rowid_t).Item("Add3").ToString
            txt_address4.Text = ds1.Tables(0).Rows(rowid_t).Item("Add4").ToString
            txt_phone.Text = ds1.Tables(0).Rows(rowid_t).Item("Phone").ToString
            txt_tin.Text = ds1.Tables(0).Rows(rowid_t).Item("Tngst").ToString
            txt_cst.Text = ds1.Tables(0).Rows(rowid_t).Item("Cst").ToString
            txt_email.Text = ds1.Tables(0).Rows(rowid_t).Item("Email").ToString
            txt_prefix.Text = ds1.Tables(0).Rows(rowid_t).Item("Prefix").ToString
            txt_suffix.Text = ds1.Tables(0).Rows(rowid_t).Item("Suffix").ToString
            txt_seqno.Text = ds1.Tables(0).Rows(rowid_t).Item("Seqno")
            txt_noofdigit.Text = ds1.Tables(0).Rows(rowid_t).Item("Noofdigit")
            Txt_fromemail.Text = ds1.Tables(0).Rows(rowid_t).Item("Frommail").ToString
            Txt_Password.Text = ds1.Tables(0).Rows(rowid_t).Item("mailpassword").ToString
            txt_gstin.Text = ds1.Tables(0).Rows(rowid_t).Item("GSTIN").ToString

            If ds1.Tables(0).Rows(rowid_t).Item("Showcomplog") = "1" Then
                Chk_Showcomplog.Checked = True
            Else
                Chk_Showcomplog.Checked = False
            End If

            txt_companyname.BackColor = Color.White
            txt_address1.BackColor = Color.White
            txt_address2.BackColor = Color.White
            txt_address3.BackColor = Color.White
            txt_address4.BackColor = Color.White
            txt_phone.BackColor = Color.White
            txt_cst.BackColor = Color.White
            txt_tin.BackColor = Color.White
            txt_email.BackColor = Color.White
            txt_prefix.BackColor = Color.White
            txt_suffix.BackColor = Color.White
            txt_seqno.BackColor = Color.White
            txt_noofdigit.BackColor = Color.White
            Chk_Showcomplog.BackColor = Color.AliceBlue

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            Compid_t = GensaveCompany(IIf(editflag_t, 1, 0), Compid_t, txt_companyname.Text, txt_address1.Text, txt_address2.Text, _
                                     txt_address3.Text, txt_address4.Text, txt_phone.Text, txt_tin.Text, txt_cst.Text, IIf(DTP_Cstdate.Checked = True, DTP_Cstdate.Value, Nothing), _
                                     txt_email.Text, txt_prefix.Text, Val(txt_seqno.Text), txt_suffix.Text, Val(txt_noofdigit.Text), _
                                     IIf(Chk_Showcomplog.Checked = True, 1, 0), Txt_fromemail.Text, Txt_Password.Text, Trim(txt_gstin.Text))
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                'Call Gendelparty(Compid_t)
                Call enabdisb("Ok")
                Call BindData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Call enabdisb("Ok")
            Call BindData()
        End Try

    End Sub

    Public Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        Try
            If ctlcol Is Nothing Then ctlcol = Me.Controls
            For Each ctl As Control In ctlcol
                If TypeOf (ctl) Is TextBox Then
                    DirectCast(ctl, TextBox).Clear()
                Else
                    If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                        ClearTextBoxes(ctl.Controls)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function FindItems(ByVal strSearchString As String, ByVal ind As Integer) As Boolean
        Try
            strSearchString = Replace(strSearchString, "'", "`")
            strSearchString = Replace(strSearchString, Chr(34), "`")

            GridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridView1.ClearSelection()

            If strSearchString = "" Then
                GridView1.Rows(0).Selected = True
                Exit Function
            End If

            Dim intCount As Integer = 0
            Dim intCell As Integer = 0


            Dim ResetCellStyle As New DataGridViewCellStyle
            ResetCellStyle.BackColor = Color.White
            Dim FoundMatchCellStyle As New DataGridViewCellStyle
            FoundMatchCellStyle.BackColor = Color.Yellow

            For i As Integer = 0 To GridView1.Rows.Count - 1
                If InStr(1, GridView1.Rows(i).Cells(ind).Value.ToString, strSearchString, CompareMethod.Text) Then
                    GridView1.Rows(i).Selected = True
                    GridView1.CurrentCell = GridView1.Rows(i).Cells(ind)
                    FindItems = True
                    Exit Function
                End If
            Next

            Return False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Filtercolnmae_t = GridView1.Columns(Colindex_t).Name

            If GridView1.Rows.Count > 0 Then
                Compid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Compid_t)
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            editflag = False
            Call enabdisb("Add")
            Call clearchars()
            txt_companyname.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
            If txt_companyname.Text = "" Then
                MsgBox("Company name should not be empty.")
                txt_companyname.Focus()
            Else
                Call saveproc(editflag)
                Call enabdisb("Ok")
                Call BindData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            Call enabdisb("Edit")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
        Try
            editflag = False
            Call clearchars()
            GroupBox3.Enabled = False
            Call enabdisb("cancel")
            Call BindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        GridView1.Enabled = False
        Call Delteproc()
    End Sub

    Private Sub GridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.RowEnter
        Try
            Dim i As Integer
            i = e.RowIndex
            If GridView1.Rows.Count > 0 And i >= 0 Then
                If GridView1.Item(0, i).Value = Nothing Then
                Else
                    Compid_t = GridView1.Item(0, i).Value
                    Call storechars(Compid_t)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_companyname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_companyname.GotFocus
        txt_companyname.BackColor = Color.Yellow
    End Sub

    Private Sub txt_companyname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_companyname.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_address1.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_companyname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_companyname.LostFocus
        Try
            Dim cnt As Integer
            ds.Tables(0).DefaultView.RowFilter = "Compname = '" & txt_companyname.Text & "'"
            cnt = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                Compid_t = ds.Tables(0).Rows(index).Item("Compid").ToString
                editflag = True
                Call storechars(Compid_t)
            Else
                index = -1
            End If
            txt_companyname.BackColor = Color.White
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    
    Private Sub txt_search_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_search.TextChanged

        Try
            Call Filterby()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txt_address1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address1.GotFocus
        txt_address1.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_address2.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_address1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address1.LostFocus
        txt_address1.BackColor = Color.White
    End Sub

    Private Sub txt_address2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address2.GotFocus
        txt_address2.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_address3.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_address2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address2.LostFocus
        txt_address2.BackColor = Color.White
    End Sub

    Private Sub txt_address3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address3.GotFocus
        txt_address3.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address3.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_address4.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txt_address3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address3.LostFocus
        txt_address3.BackColor = Color.White
    End Sub

    Private Sub txt_address4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address4.GotFocus
        txt_address4.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address4.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_phone.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub txt_address4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address4.LostFocus
        txt_address4.BackColor = Color.White
    End Sub

    Private Sub txt_phone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_phone.GotFocus
        txt_phone.BackColor = Color.Yellow
    End Sub

    Private Sub txt_phone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_phone.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_gstin.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_phone_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_phone.LostFocus
        txt_phone.BackColor = Color.White
    End Sub

    Private Sub txt_tin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_tin.GotFocus
        txt_tin.BackColor = Color.Yellow
    End Sub

    Private Sub txt_tin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_tin.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_gstin.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_cst_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cst.GotFocus
        txt_cst.BackColor = Color.Yellow
    End Sub

    Private Sub txt_cst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_cst.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_email.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DTP_Cstdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTP_Cstdate.GotFocus
        DTP_Cstdate.BackColor = Color.Yellow
    End Sub

    Private Sub DTP_Cstdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DTP_Cstdate.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdok.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_tin_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_tin.LostFocus
        txt_tin.BackColor = Color.White
    End Sub

    Private Sub txt_cst_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cst.LostFocus
        txt_cst.BackColor = Color.White
    End Sub

    Private Sub DTP_Cstdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTP_Cstdate.LostFocus
        DTP_Cstdate.BackColor = Color.White
    End Sub

    Private Sub txt_email_GotFocus(sender As Object, e As EventArgs) Handles txt_email.GotFocus
        txt_email.BackColor = Color.Yellow
    End Sub

    Private Sub txt_email_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_email.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Txt_fromemail.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_email_LostFocus(sender As Object, e As EventArgs) Handles txt_email.LostFocus
        txt_email.BackColor = Color.White
    End Sub

    Private Sub PartyMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Text = "Company Master"
            Call opnconn()
            'Call dsopen()
            'Call Load_combotype()
            Call BindData()
            enabdisb("Ok")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try

            If txt_search.TextLength > 0 And Colindex_t >= 0 And Filtercolnmae_t <> "" Then

                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"

            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Property Defaultprocess() As String
        Get
            Return Defaultprocess_t
        End Get
        Set(ByVal value As String)
            Defaultprocess_t = value
        End Set

    End Property

    Private Sub txt_seqno_GotFocus(sender As Object, e As EventArgs) Handles txt_seqno.GotFocus
        txt_seqno.BackColor = Color.Yellow
    End Sub

    Private Sub txt_prefix_GotFocus(sender As Object, e As EventArgs) Handles txt_prefix.GotFocus
        txt_prefix.BackColor = Color.Yellow
    End Sub

    Private Sub txt_suffix_GotFocus(sender As Object, e As EventArgs) Handles txt_suffix.GotFocus
        txt_suffix.BackColor = Color.Yellow
    End Sub

    Private Sub txt_seqno_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_seqno.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_suffix.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_seqno_LostFocus(sender As Object, e As EventArgs) Handles txt_seqno.LostFocus
        txt_seqno.BackColor = Color.White
    End Sub

    Private Sub txt_prefix_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_prefix.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_seqno.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_prefix_LostFocus(sender As Object, e As EventArgs) Handles txt_prefix.LostFocus
        txt_prefix.BackColor = Color.White
    End Sub

    Private Sub txt_suffix_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_suffix.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                txt_noofdigit.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_suffix_LostFocus(sender As Object, e As EventArgs) Handles txt_suffix.LostFocus
        txt_suffix.BackColor = Color.White
    End Sub

    Private Sub txt_noofdigit_GotFocus(sender As Object, e As EventArgs) Handles txt_noofdigit.GotFocus
        txt_noofdigit.BackColor = Color.Yellow
    End Sub

    Private Sub txt_noofdigit_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_noofdigit.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdok.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_noofdigit_LostFocus(sender As Object, e As EventArgs) Handles txt_noofdigit.LostFocus
        txt_noofdigit.BackColor = Color.White
    End Sub

    Private Sub Txt_fromemail_GotFocus(sender As Object, e As EventArgs) Handles Txt_fromemail.GotFocus
        Txt_fromemail.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_fromemail_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_fromemail.KeyDown
        If e.KeyCode = Keys.Enter Then
            Txt_Password.Focus()
        End If
    End Sub

    Private Sub Txt_Password_GotFocus(sender As Object, e As EventArgs) Handles Txt_Password.GotFocus
        Txt_Password.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_Password_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Password.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_prefix.Focus()
        End If
    End Sub

    Private Sub Txt_fromemail_LostFocus(sender As Object, e As EventArgs) Handles Txt_fromemail.LostFocus
        Txt_fromemail.BackColor = Color.White
    End Sub

    Private Sub Txt_Password_LostFocus(sender As Object, e As EventArgs) Handles Txt_Password.LostFocus
        Txt_Password.BackColor = Color.White
    End Sub

    Private Sub txt_gstin_GotFocus(sender As Object, e As EventArgs) Handles txt_gstin.GotFocus
        txt_gstin.BackColor = Color.Yellow
    End Sub

    Private Sub txt_gstin_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_gstin.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_email.Focus()
        End If
    End Sub

    Private Sub txt_gstin_LostFocus(sender As Object, e As EventArgs) Handles txt_gstin.LostFocus
        txt_gstin.BackColor = Color.White
    End Sub

    Private Sub txt_phone_TextChanged(sender As Object, e As EventArgs) Handles txt_phone.TextChanged

    End Sub

    Private Sub txt_email_TextChanged(sender As Object, e As EventArgs) Handles txt_email.TextChanged

    End Sub
End Class
