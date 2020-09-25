Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class frm_cardnomaster
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim Rowindex_t As Integer
    Dim Cardid_t As Double
    Dim CurrRowindex_t As Integer, Events_t As String = "", Rowcnt As Integer
    Dim ds, ds1, ds_procs, tmpds, ds_state, ds_trans, ds_book, ds_price, ds_saleex, ds_agent, ds_pricelst, ds_acdesc, ds_Getptyp, ds_prospricelst, ds_CheckCode As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Partyid_t As Double, Stateid_t As Double, Transid_t As Double, Trans2id_t As Double, Bookingplaceid_t As Double, Agentid_t As Double
    Dim accountid_t As Double, Salesexecid_t As Double, Pricelistid_t As Double, ProsPricelistid_t As Double
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer
    Dim Stateid As Double
    Dim Sqlstr As String, Filtercolnmae_t As String, Defaultprocess_t As String, TransSqlstr As String, BookSqlstr As String, SalexeSqlstr As String
    Const Process = "Party"
    Dim fm As New Sun_Findfrm
    Dim font1 As Font

    Private Sub BindData()
        Try
            Call clearchars()

            Sqlstr = "SELECT CARDID,CARDNO,NAME,MOBILENO FROM CARDNO_MASTER ORDER BY CARDNO"

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

            If cnt > 0 Then
                Cardid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Cardid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.ReadOnly = True
           
            Dim font As New Font( _
                GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 25
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            cnt = GridView1.Columns.Count
            For i = 0 To cnt - 1
                GridView1.Columns(i).DefaultCellStyle.Font = font
            Next

            GridView1.AutoResizeColumns()

            Call RecordFocus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RecordFocus()
        Try
            Rowcnt = GridView1.Rows.Count - 1
            'following lines are used to cursor focus which record was edit,add,delete,print
            If Events_t = "Add" Then
                If CurrRowindex_t < Rowcnt - 1 Then
                    CurrRowindex_t = CurrRowindex_t + 1 'new record add focus to tat last record
                End If
            ElseIf Events_t = "Edit" Then 'no change for currrowindex
                ' CurrRowindex_t = CurrRowindex_t
            ElseIf Events_t = "Delete" Then
                If CurrRowindex_t >= Rowcnt Then
                    CurrRowindex_t = CurrRowindex_t - 1 'delete last record focus to prev record
                End If
            ElseIf Events_t = "Print" Then
            Else
            End If

            If Rowcnt > 0 Then
                GridView1.Rows(CurrRowindex_t).Selected = True
                GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
            End If

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
            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
                GroupBox3.Enabled = False
                GridView1.Enabled = True
                GroupBox1.Visible = True
                GroupBox2.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            txt_cardno.Enabled = True
            dtp_cardate.Checked = False
            DTP_dob.Checked = False
            DTP_weddingdt.Checked = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal Itemid_v As Double = 0)
        Try
            ds1.Clear()
            ds1 = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_CARDNO"
            cmd.Parameters.Add("@CARDID", SqlDbType.Float).Value = Cardid_t
            ds1 = New DataSet
            da.Fill(ds1)

            Dim rowid_t As Integer
            rowid_t = ds1.Tables(0).Rows.Count
            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1

            txt_cardno.Enabled = False

            txt_cardno.Text = ds1.Tables(0).Rows(rowid_t).Item("CARDNO").ToString
            Cardid_t = ds1.Tables(0).Rows(rowid_t).Item("CARDID")
            txt_name.Text = ds1.Tables(0).Rows(rowid_t).Item("NAME").ToString
            txt_address1.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDRESS1").ToString
            txt_address2.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDRESS2").ToString
            txt_address3.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDRESS3")
            txt_address4.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDRESS4").ToString
            txt_mobile.Text = ds1.Tables(0).Rows(rowid_t).Item("MOBILENO").ToString
            txt_email.Text = ds1.Tables(0).Rows(rowid_t).Item("EMAILID")
            txt_notes.Text = ds1.Tables(0).Rows(rowid_t).Item("NOTES")
            txt_addpoint.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDPOINT")
            txt_addpoint.Text = CDbl(txt_addpoint.Text).ToString("#######")
            tx_openingpoint.Text = ds1.Tables(0).Rows(rowid_t).Item("OPNPOINT")
            tx_openingpoint.Text = CDbl(tx_openingpoint.Text).ToString("########")
            txt_lesspoint.Text = ds1.Tables(0).Rows(rowid_t).Item("LESSPOINT")
            txt_lesspoint.Text = CDbl(txt_lesspoint.Text).ToString("##########")

            If ds1.Tables(0).Rows(rowid_t).Item("CARDDATE").ToString <> "" Then
                dtp_cardate.Value = ds1.Tables(0).Rows(rowid_t).Item("CARDDATE").ToString
                dtp_cardate.Checked = True
            Else
                dtp_cardate.Checked = False
            End If

            If ds1.Tables(0).Rows(rowid_t).Item("DOB").ToString <> "" Then
                DTP_dob.Value = ds1.Tables(0).Rows(rowid_t).Item("DOB").ToString
                DTP_dob.Checked = True
            Else
                DTP_dob.Checked = False
            End If

            If ds1.Tables(0).Rows(rowid_t).Item("WEDDINGDT").ToString <> "" Then
                DTP_weddingdt.Value = ds1.Tables(0).Rows(rowid_t).Item("WEDDINGDT").ToString
                DTP_weddingdt.Checked = True
            Else
                DTP_weddingdt.Checked = False
            End If
            'dtp_cardate.Value = ds1.Tables(0).Rows(rowid_t).Item("CARDDATE")
            'DTP_dob.Value = ds1.Tables(0).Rows(rowid_t).Item("DOB")
            'DTP_weddingdt.Value = ds1.Tables(0).Rows(rowid_t).Item("WEDDINGDT")

            ' GridView1.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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


    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If GridView1.Rows.Count > 0 Then
                Cardid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Cardid_t)
            Else
            End If

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Filtercolnmae_t = GridView1.Columns(e.ColumnIndex).HeaderText
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.RowEnter
        Try
            Dim i As Integer
            i = e.RowIndex
            'Call loadtextboxes1(i)
            If GridView1.Rows.Count > 0 And i >= 0 Then
                If GridView1.Item(0, i).Value = Nothing Then
                Else
                    Cardid_t = GridView1.Item(0, i).Value
                    Call storechars(Cardid_t)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try

            If Rowcnt > 0 Then
                CurrRowindex_t = Rowcnt - 1
            Else
                CurrRowindex_t = 0
            End If

            Events_t = "Add"
            editflag = False
            Call enabdisb("Add")
            Events_t = "Add"
            Call clearchars()
            txt_cardno.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            CurrRowindex_t = Rowindex_t
            Events_t = "Edit"
            Call enabdisb("Edit")
            dtp_cardate.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        CurrRowindex_t = Rowindex_t
        Events_t = "Delete"
        GridView1.Enabled = False
        Call Delteproc()
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            If txt_cardno.Text = "" Then
                MsgBox("Card No should not be empty.")
                txt_cardno.Focus()
            ElseIf txt_mobile.Text = "" Then
                MsgBox("MobileNo should not be empty.")
                txt_mobile.Focus()
            Else
                Call saveproc(editflag)
                Call enabdisb("Ok")
                Call BindData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Call GendelCard(Cardid_t)
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

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            Cardid_t = GensaveCard(IIf(editflag_t, 1, 0), Cardid_t, txt_cardno.Text, IIf(dtp_cardate.Checked = True, dtp_cardate.Value, Nothing), txt_name.Text, txt_address1.Text,
                                   txt_address2.Text, txt_address3.Text, txt_address4.Text, txt_mobile.Text, IIf(DTP_dob.Checked = True, DTP_dob.Value, Nothing),
                                  IIf(DTP_weddingdt.Checked = True, DTP_weddingdt.Value, Nothing), txt_email.Text, txt_notes.Text,
                                  Val(tx_openingpoint.Text), Val(txt_addpoint.Text), Val(txt_lesspoint.Text))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
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

    Private Sub FindNextCell(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
        Try
            Dim found As Boolean = False

            While dgv.RowCount > rowindex
                While dgv.Columns.Count > columnindex
                    If Not (dgv.Rows(rowindex).Cells(columnindex)).ReadOnly Then
                        If (dgv.Rows(rowindex).Cells(columnindex)).Visible Then
                            dgv.CurrentCell = dgv.Rows(rowindex).Cells(columnindex)
                            ' dgv.BeginEdit(True)
                            Exit Sub
                        Else
                            columnindex += 1
                        End If
                    Else
                        columnindex += 1
                    End If
                End While
                rowindex += 1
                columnindex = 0
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Filterby()
        Try
            If txt_search.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & txt_search.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Try
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).HeaderText
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        Try
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).HeaderText
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_cardno_Click(sender As Object, e As EventArgs) Handles txt_cardno.Click
        Dim cnt As Integer
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Sqlstr = "Select I.CARDID From CARDNO_Master I  WHERE CARDNO ='" & Trim(txt_cardno.Text) & "'"
        cmd = New SqlCommand(Sqlstr, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        cnt = ds.Tables(0).Rows.Count

        If cnt > 0 Then
            Cardid_t = ds.Tables(0).Rows(0).Item("CARDID")
            Call storechars(Cardid_t)
            editflag = True
        End If
    End Sub

    Private Sub txt_cardno_GotFocus(sender As Object, e As EventArgs) Handles txt_cardno.GotFocus
        txt_cardno.BackColor = Color.Yellow
    End Sub

    Private Sub txt_cardno_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_cardno.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim cnt As Integer
            Dim ds As New DataSet
            Dim da As SqlDataAdapter
            Sqlstr = "Select I.CARDID From CARDNO_Master I  WHERE cardno  ='" & Trim(txt_cardno.Text) & "'"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)
            cnt = ds.Tables(0).Rows.Count

            If cnt > 0 Then
                Cardid_t = ds.Tables(0).Rows(0).Item("CARDID")
                Call storechars(Cardid_t)
                editflag = True
            End If

            dtp_cardate.Focus()
        End If
    End Sub

    Private Sub txt_cardno_LostFocus(sender As Object, e As EventArgs) Handles txt_cardno.LostFocus
        txt_cardno.BackColor = Color.White
        Dim cnt As Integer
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Sqlstr = "Select I.CARDID From CARDNO_Master I  WHERE CARDNO ='" & Trim(txt_cardno.Text) & "'"
        cmd = New SqlCommand(Sqlstr, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        cnt = ds.Tables(0).Rows.Count

        If cnt > 0 Then
            Cardid_t = ds.Tables(0).Rows(0).Item("CARDID")
            Call storechars(Cardid_t)
            editflag = True
        End If
    End Sub

    Private Sub dtp_cardate_KeyDown(sender As Object, e As KeyEventArgs) Handles dtp_cardate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_name.Focus()
        End If
    End Sub

    Private Sub txt_name_GotFocus(sender As Object, e As EventArgs) Handles txt_name.GotFocus
        txt_name.BackColor = Color.Yellow
    End Sub

    Private Sub txt_name_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_name.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address1.Focus()
        End If
    End Sub

    Private Sub txt_name_LostFocus(sender As Object, e As EventArgs) Handles txt_name.LostFocus
        txt_name.BackColor = Color.White
    End Sub

    Private Sub txt_name_StyleChanged(sender As Object, e As EventArgs) Handles txt_name.StyleChanged

    End Sub

    Private Sub txt_address1_GotFocus(sender As Object, e As EventArgs) Handles txt_address1.GotFocus
        txt_address1.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address1_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_address1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address2.Focus()
        End If
    End Sub

    Private Sub txt_address1_LostFocus(sender As Object, e As EventArgs) Handles txt_address1.LostFocus
        txt_address1.BackColor = Color.White
    End Sub

    Private Sub txt_address2_GotFocus(sender As Object, e As EventArgs) Handles txt_address2.GotFocus
        txt_address2.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address2_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_address2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address3.Focus()
        End If
    End Sub

    Private Sub txt_address2_LostFocus(sender As Object, e As EventArgs) Handles txt_address2.LostFocus
        txt_address2.BackColor = Color.White
    End Sub

    Private Sub txt_address3_GotFocus(sender As Object, e As EventArgs) Handles txt_address3.GotFocus
        txt_address3.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address3_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_address3.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address4.Focus()
        End If
    End Sub

    Private Sub txt_address3_LostFocus(sender As Object, e As EventArgs) Handles txt_address3.LostFocus
        txt_address3.BackColor = Color.White
    End Sub

    Private Sub txt_address4_GotFocus(sender As Object, e As EventArgs) Handles txt_address4.GotFocus
        txt_address4.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address4_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_address4.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_mobile.Focus()
        End If
    End Sub

    Private Sub txt_address4_LostFocus(sender As Object, e As EventArgs) Handles txt_address4.LostFocus
        txt_address4.BackColor = Color.White
    End Sub

    Private Sub txt_mobile_GotFocus(sender As Object, e As EventArgs) Handles txt_mobile.GotFocus
        txt_mobile.BackColor = Color.Yellow
    End Sub

    Private Sub txt_mobile_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_mobile.KeyDown
        If e.KeyCode = Keys.Enter Then
            DTP_dob.Focus()
        End If
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                  GridView1.DefaultCellStyle.Font, _
                                    Brushes.Black, e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub txt_mobile_LostFocus(sender As Object, e As EventArgs) Handles txt_mobile.LostFocus
        If txt_mobile.Text = "" Then txt_mobile.Focus()
        txt_mobile.BackColor = Color.White
    End Sub

    Private Sub DTP_dob_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_dob.KeyDown
        If e.KeyCode = Keys.Enter Then
            DTP_weddingdt.Focus()
        End If
    End Sub

    Private Sub DTP_weddingdt_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_weddingdt.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_email.Focus()
        End If
    End Sub

    Private Sub txt_email_GotFocus(sender As Object, e As EventArgs) Handles txt_email.GotFocus
        txt_email.BackColor = Color.Yellow
    End Sub

    Private Sub txt_email_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_email.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_notes.Focus()
        End If
    End Sub

    Private Sub txt_email_LostFocus(sender As Object, e As EventArgs) Handles txt_email.LostFocus
        txt_email.BackColor = Color.White
    End Sub

    Private Sub txt_notes_GotFocus(sender As Object, e As EventArgs) Handles txt_notes.GotFocus
        txt_notes.BackColor = Color.Yellow
    End Sub

    Private Sub txt_notes_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_notes.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_notes_LostFocus(sender As Object, e As EventArgs) Handles txt_notes.LostFocus
        txt_notes.BackColor = Color.White
    End Sub

    Private Sub frm_cardnomaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call opnconn()
            Call BindData()
            enabdisb("Ok")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_search_GotFocus(sender As Object, e As EventArgs) Handles txt_search.GotFocus
        txt_search.BackColor = Color.Yellow
    End Sub

    Private Sub txt_search_LostFocus(sender As Object, e As EventArgs) Handles txt_search.LostFocus
        txt_search.BackColor = Color.White
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        Call Filterby()
    End Sub

    Private Sub tx_openingpoint_GotFocus(sender As Object, e As EventArgs) Handles tx_openingpoint.GotFocus
        tx_openingpoint.BackColor = Color.Yellow
    End Sub

    Private Sub tx_openingpoint_KeyDown(sender As Object, e As KeyEventArgs) Handles tx_openingpoint.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_addpoint.Focus()
        End If
    End Sub

    Private Sub txt_addpoint_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_addpoint.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_lesspoint.Focus()
        End If
    End Sub

    Private Sub txt_lesspoint_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_lesspoint.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub tx_openingpoint_LostFocus(sender As Object, e As EventArgs) Handles tx_openingpoint.LostFocus
        tx_openingpoint.BackColor = Color.White
    End Sub

    Private Sub txt_addpoint_LostFocus(sender As Object, e As EventArgs) Handles txt_addpoint.LostFocus
        txt_addpoint.BackColor = Color.White
    End Sub

    Private Sub txt_addpoint_TextChanged(sender As Object, e As EventArgs) Handles txt_addpoint.TextChanged
        txt_balance.Text = Val(txt_addpoint.Text) + Val(tx_openingpoint.Text) - Val(txt_lesspoint.Text)
    End Sub

    Private Sub tx_openingpoint_TextChanged(sender As Object, e As EventArgs) Handles tx_openingpoint.TextChanged
        txt_balance.Text = Val(txt_addpoint.Text) + Val(tx_openingpoint.Text) - Val(txt_lesspoint.Text)
    End Sub

    Private Sub txt_lesspoint_TextChanged(sender As Object, e As EventArgs) Handles txt_lesspoint.TextChanged
        txt_balance.Text = Val(txt_addpoint.Text) + Val(tx_openingpoint.Text) - Val(txt_lesspoint.Text)
    End Sub

End Class