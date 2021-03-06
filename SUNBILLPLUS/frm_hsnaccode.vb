﻿Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class frm_hsnaccode
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
            Sqlstr = "Select G.Masterid,G.hsncode as [HSN/Accounting Code],G.decription as Description From HSNACCOUNTCODE_MASTER G order by g.hsncode "

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

            GridView1.Columns.Item("HSN/Accounting Code").Width = 160
            GridView1.Columns.Item("deScription").Width = 150
            GridView1.AllowUserToAddRows = False
            GridView1.ReadOnly = True
            GridView1.Columns("HSN/Accounting Code").SortMode = DataGridViewColumnSortMode.NotSortable
            GridView1.Columns("deScription").SortMode = DataGridViewColumnSortMode.NotSortable
            GridView1.Refresh()

            Filtercolnmae_t = "HSN/Accounting Code"
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
            GridView1.ColumnHeadersHeight = 40
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
            Sqlstr = "Select G.Masterid,G.hsncode ,G.decription as Description,ISNULL(G.CGSTPERC,0) AS CGST,ISNULL(G.SGSTPERC,0) AS SGST,ISNULL(G.IGSTPERC,0) AS IGST " _
                & " From HSNACCOUNTCODE_MASTER G " _
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
            txt_hsnaccode.Text = ds1.Tables(0).Rows(rowid_t).Item("hsncode").ToString
            txt_description.Text = ds1.Tables(0).Rows(rowid_t).Item("Description").ToString
            txt_igstperc.Text = ds1.Tables(0).Rows(rowid_t).Item("IGST")
            txt_sgstperc.Text = ds1.Tables(0).Rows(rowid_t).Item("SGST")
            txt_cgstperc.Text = ds1.Tables(0).Rows(rowid_t).Item("CGST")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            Masterid_t = GensaveHSNAccode(IIf(editflag_t, 1, 0), Masterid_t, Trim(txt_hsnaccode.Text), Trim(txt_description.Text), Val(txt_cgstperc.Text), Val(txt_sgstperc.Text), Val(txt_igstperc.Text))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Call GendelHsnaccode(Masterid_t)
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
            txt_hsnaccode.Focus()
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
            If txt_hsnaccode.Text = "" Then
                MsgBox("State should not be empty.")
                txt_hsnaccode.Focus()
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

    Private Sub txt_hsnaccode_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_hsnaccode.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_description.Focus()
        End If
    End Sub

    Private Sub txt_itemname_LostFocus(sender As Object, e As EventArgs) Handles txt_hsnaccode.LostFocus
        txt_hsnaccode.BackColor = Color.White
    End Sub

    Private Sub Txt_group_GotFocus(sender As Object, e As EventArgs) Handles txt_hsnaccode.GotFocus
        txt_hsnaccode.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_group_LostFocus(sender As Object, e As EventArgs) Handles txt_hsnaccode.LostFocus
        Try
            Dim cnt As Integer
            ds.Tables(0).DefaultView.RowFilter = " [HSN/Accounting Code] = '" & Trim(txt_hsnaccode.Text) & "' "
            cnt = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                Masterid_t = ds.Tables(0).Rows(index).Item("Masterid").ToString
                editflag = True
                Call storechars(Masterid_t)
            Else
                index = -1
            End If
            txt_hsnaccode.BackColor = Color.White
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_description_GotFocus(sender As Object, e As EventArgs) Handles txt_description.GotFocus
        txt_description.BackColor = Color.Yellow
    End Sub

    Private Sub txt_description_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_description.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_igstperc.Focus()
        End If
    End Sub

    Private Sub txt_description_LostFocus(sender As Object, e As EventArgs) Handles txt_description.LostFocus
        txt_description.BackColor = Color.White
    End Sub

    Private Sub txt_cgstperc_GotFocus(sender As Object, e As EventArgs) Handles txt_cgstperc.GotFocus
        txt_cgstperc.BackColor = Color.Yellow
    End Sub

    Private Sub txt_cgstperc_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_cgstperc.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_sgstperc.Focus()
        End If
    End Sub

    Private Sub txt_cgstperc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_cgstperc.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_cgstperc_LostFocus(sender As Object, e As EventArgs) Handles txt_cgstperc.LostFocus
        txt_cgstperc.BackColor = Color.White
        txt_cgstperc.Text = Format(Val(txt_cgstperc.Text), "#######0.00")
    End Sub

    Private Sub txt_sgstperc_GotFocus(sender As Object, e As EventArgs) Handles txt_sgstperc.GotFocus
        txt_sgstperc.BackColor = Color.Yellow
    End Sub

    Private Sub txt_sgstperc_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_sgstperc.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_sgstperc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_sgstperc.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_sgstperc_LostFocus(sender As Object, e As EventArgs) Handles txt_sgstperc.LostFocus
        txt_sgstperc.BackColor = Color.White
        txt_sgstperc.Text = Format(Val(txt_sgstperc.Text), "#######0.00")
    End Sub

    Private Sub txt_igstperc_GotFocus(sender As Object, e As EventArgs) Handles txt_igstperc.GotFocus
        txt_igstperc.BackColor = Color.Yellow
    End Sub

    Private Sub txt_igstperc_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_igstperc.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim gstval As Double = Val(txt_igstperc.Text) / 2
            txt_cgstperc.Text = gstval
            txt_sgstperc.Text = gstval
            txt_cgstperc.Text = Format(Val(txt_sgstperc.Text), "#######0.00")
            txt_sgstperc.Text = Format(Val(txt_sgstperc.Text), "#######0.00")
            txt_cgstperc.Focus()
        End If
    End Sub

    Private Sub txt_igstperc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_igstperc.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_igstperc_LostFocus(sender As Object, e As EventArgs) Handles txt_igstperc.LostFocus
        txt_igstperc.BackColor = Color.White
        txt_igstperc.Text = Format(Val(txt_igstperc.Text), "#######0.00")
        Dim gstval As Double = Val(txt_igstperc.Text) / 2
        txt_cgstperc.Text = gstval
        txt_sgstperc.Text = gstval
        txt_cgstperc.Text = Format(Val(txt_sgstperc.Text), "#######0.00")
        txt_sgstperc.Text = Format(Val(txt_sgstperc.Text), "#######0.00")
    End Sub

    Private Sub txt_igstperc_TextChanged(sender As Object, e As EventArgs) Handles txt_igstperc.TextChanged

    End Sub
End Class