Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class frm_statemaster
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Public tmppassstr As String
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds1, ds_uom As New DataSet
    Dim bs As New BindingSource
    Dim dt As New DataTable
    Dim Masterid_t As Double
    Dim editflag As Boolean
    Dim index As Integer
    Dim Stateid, uomid_t As Double
    Dim Sqlstr As String
    Dim Fm As New Sun_Findfrm
    Dim flag As Boolean
    Dim Filtercolnmae_t As String
    Dim colindex_t As Integer

    Private Sub Frm_Item_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Call closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Frm_Item_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call opnconn()
            Call BindData()
            Call gridvisible(GridView1, False, "Masterid")
            enabdisb("Ok")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindData()
        Try
            Call clearchars()
            Sqlstr = "Select G.Masterid,G.state as State,G.code as Code From state_Master G order by g.state "

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

            Dim tables As DataTableCollection = ds.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1

            GridView1.Columns.Item("state").Width = 160
            GridView1.Columns.Item("code").Width = 150
            GridView1.AllowUserToAddRows = False
            GridView1.ReadOnly = True
            GridView1.Columns("state").SortMode = DataGridViewColumnSortMode.NotSortable
            GridView1.Columns("code").SortMode = DataGridViewColumnSortMode.NotSortable
            GridView1.Refresh()

            Filtercolnmae_t = "State"
            If GridView1.Rows.Count > 0 Then
                Masterid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Masterid_t)
            Else
            End If

            Dim font As New Font( _
          GridView1.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 28
            GridView1.ColumnHeadersDefaultCellStyle.Font = font
            GridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        Try

            If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
                GroupBox3.Enabled = True
                GridView1.Enabled = False
                txt_search.Enabled = False
                cmdadd.Visible = False
                cmdedit.Visible = False
                cmddelete.Visible = False
                cmdexit.Visible = False

                cmdok.Visible = True
                cmdcancel.Visible = True
            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then

                GroupBox3.Enabled = False
                GridView1.Enabled = True
                txt_search.Enabled = True

                cmdadd.Visible = True
                cmdedit.Visible = True
                cmddelete.Visible = True
                cmdexit.Visible = True

                cmdok.Visible = False
                cmdcancel.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal Masterid_v As Double = 0)
        Try

            Sqlstr = "Select G.Masterid,G.state,G.code From state_Master G " _
                     & " Where G.Masterid = " & Masterid_v & "  "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds1 = New DataSet
            ds1.Clear()
            da.Fill(ds1)

            Dim rowid_t As Integer
            'Call clearchars()
            rowid_t = ds1.Tables(0).Rows.Count

            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1
            Masterid_t = ds1.Tables(0).Rows(rowid_t).Item("Masterid")
            txt_state.Text = ds1.Tables(0).Rows(rowid_t).Item("state").ToString
            txt_code.Text = ds1.Tables(0).Rows(rowid_t).Item("code").ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            Masterid_t = GensaveState(IIf(editflag_t, 1, 0), Masterid_t, Trim(txt_state.Text), Trim(txt_code.Text))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Call GendelState(Masterid_t)
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

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try

            If GridView1.Rows.Count > 0 Then
                Masterid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Masterid_t)
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.RowEnter
        Try
            Dim i As Integer
            i = e.RowIndex

            If GridView1.Rows.Count > 0 And i >= 0 Then
                If GridView1.Item(0, i).Value = Nothing Then
                Else
                    Masterid_t = GridView1.Item(0, i).Value
                    Call storechars(Masterid_t)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_search_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_search.TextChanged
        Try
            Call filterby()
            If GridView1.Rows.Count > 0 Then
                Masterid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Masterid_t)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub filterby()
        Try
            If txt_search.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            editflag = False
            Call enabdisb("Add")
            Call clearchars()
            txt_state.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            Call enabdisb("Edit")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        Try
            GridView1.Enabled = False
            Call Delteproc()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Try
            Call closeconn()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            If txt_state.Text = "" Then
                MsgBox("State should not be empty.")
                txt_state.Focus()
                'ElseIf txt_code.Text = "" Then
                '    MsgBox("Code should not be empty.")
                '    txt_code.Focus()
            Else
                Call saveproc(editflag)
                Call enabdisb("Ok")
                Call BindData()
            End If
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

    Private Sub txt_search_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_search.GotFocus
        txt_search.BackColor = Color.Yellow
    End Sub

    Private Sub txt_search_lostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_search.LostFocus
        txt_search.BackColor = Color.White
    End Sub

    Private Sub txt_state_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_state.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_code.Focus()
        End If
    End Sub

    Private Sub txt_itemname_LostFocus(sender As Object, e As EventArgs) Handles txt_state.LostFocus
        txt_state.BackColor = Color.White
    End Sub

    Private Sub Txt_group_GotFocus(sender As Object, e As EventArgs) Handles txt_state.GotFocus
        txt_state.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_group_LostFocus(sender As Object, e As EventArgs) Handles txt_state.LostFocus
        Try
            Dim cnt As Integer
            ds.Tables(0).DefaultView.RowFilter = " state = '" & Trim(txt_state.Text) & "' And code ='" & Trim(txt_code.Text) & "' "
            cnt = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                Masterid_t = ds.Tables(0).Rows(index).Item("Masterid").ToString
                editflag = True
                Call storechars(Masterid_t)
            Else
                index = -1
            End If
            txt_state.BackColor = Color.White
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_code_GotFocus(sender As Object, e As EventArgs) Handles txt_code.GotFocus
        txt_code.BackColor = Color.Yellow
    End Sub

    Private Sub txt_code_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_code.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_code_LostFocus(sender As Object, e As EventArgs) Handles txt_code.LostFocus
        txt_code.BackColor = Color.White
    End Sub

End Class