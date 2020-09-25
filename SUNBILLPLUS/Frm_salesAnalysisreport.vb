Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App

Public Class Frm_salesAnalysisreport

    Dim cmd As SqlCommand
    Dim da, da_item, da_size, da_code, da_out, da_settings As SqlDataAdapter
    Dim ds, ds1, ds_head, ds_detl, ds_item, ds_Loc, ds_size, ds_out, ds_group, ds_code, ds_settings As New DataSet
    Dim bs_grp, bs_item, bs_Loc, bs, bs_group, bs_size, bs_code, bs_out As New BindingSource
    Dim SalesAnalysisitemwisereport_t As Boolean
    Dim Groupwhrcond_t As String, Itemwhrcond_t As String, Codewhrcond_t, Typewhrcond_t As String, Colname_t As String, Filtercolnmae_t As String, Locationwhrcond_t, SelectedGroup_t As String
    Dim SelectedItem_t As String, SizeWhrcond_t As String, Selectedcode_t As String
    Dim FinishedStck, CuttingStk, UnitStk, Pendingorder, miniqty, Colcnt_t, colindex_t, Rowindex_t As Integer
    Dim Rm As New Frm_Reports_Init

    Private Sub Execute()
        Try
            Dim levstr As String = ""

            cmd = Nothing
            ds = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT FROMDATE FROM USERS WHERE UID =" & Genuid & ""
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "Table1")

            If ds.Tables(0).Rows.Count > 0 Then
                Dtp_fromdate.Value = ds.Tables(0).Rows(0).Item("FROMDATE")
            End If


            cmd = Nothing
            ds_settings = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT ISNULL(NUMERICVALUE,0) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS ='SALES ANALYSIS ITEMWISE REPORT'"
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings, "Table1")

            If ds_settings.Tables(0).Rows.Count > 0 Then
                If ds_settings.Tables(0).Rows(0).Item("NUMERICVALUE") = 1 Then
                    SalesAnalysisitemwisereport_t = True
                Else
                    SalesAnalysisitemwisereport_t = False
                End If
            Else
                SalesAnalysisitemwisereport_t = False
            End If

            cmd_print.Visible = SalesAnalysisitemwisereport_t
            Cbo_type.Visible = SalesAnalysisitemwisereport_t

            ds_item = Nothing
            levstr = " SELECT ITEMID,isnull(ITEMDES,'') as ITEMDES FROM ITEM_MASTER ORDER BY ITEMDES "
            cmd = Nothing
            ds_item = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_item = New DataSet
            ds_item.Clear()
            da.Fill(ds_item)
            bs_item.DataSource = ds_item.Tables(0)
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "ITEMDES"
            Chklst_Item.ValueMember = "ITEMID"

            ds_Loc = Nothing
            levstr = " SELECT MASTERID,isnull(GODOWNNAME,'') as GODOWNNAME FROM GODOWN_MASTER ORDER BY GODOWNNAME "
            cmd = Nothing
            ds_Loc = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Loc = New DataSet
            ds_Loc.Clear()
            da.Fill(ds_Loc)
            bs_Loc.DataSource = ds_Loc.Tables(0)
            Me.Chklst_loc.DataSource = ds_Loc
            Chklst_loc.DataSource = bs_Loc
            Chklst_loc.DisplayMember = "GODOWNNAME"
            Chklst_loc.ValueMember = "MASTERID"

            ds_code = Nothing
            levstr = " SELECT ITEMID,isnull(ITEmcode,'') AS ITEMcode FROM ITEM_MASTER  WHERE ITEMCODE <> '' AND ITEMCODE IS NOT NULL ORDER BY ITEMcode "
            cmd = Nothing
            ds_code = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_code = New SqlDataAdapter(cmd)
            ds_code = New DataSet
            ds_code.Clear()
            da_code.Fill(ds_code)
            bs_code.DataSource = ds_code.Tables(0)
            Me.Chklst_code.DataSource = ds_code
            Chklst_code.DataSource = bs_code
            Chklst_code.DisplayMember = "ITEMCODE"
            Chklst_code.ValueMember = "ITEMID"

            ds_group = Nothing
            levstr = " Select Groupname, Masterid From Group_Master  Order By Groupname "
            cmd = Nothing
            ds_group = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_group = New DataSet
            ds_group.Clear()
            da.Fill(ds_group)
            bs_group.DataSource = ds_group.Tables(0)
            Me.Chklst_Grp.DataSource = ds_group
            Chklst_Grp.DataSource = bs_group
            Chklst_Grp.DisplayMember = "groupname"
            Chklst_Grp.ValueMember = "masterid"


            ds_out = Nothing
            levstr = "SELECT RTRIM( LTRIM( REPLACE(PROCESS,'SALES ANALYSIS',''))) AS RPTTYPE,ROW_NUMBER() OVER(ORDER BY PROCESS) AS ROWID FROM REPORTSETUP WHERE RPTTYPE ='SALES ANALYSIS' ORDER BY PROCESS "
            cmd = Nothing
            ds_out = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_out = New SqlDataAdapter(cmd)
            ds_out = New DataSet
            ds_out.Clear()
            da_out.Fill(ds_out)

            bs_out.DataSource = ds_out.Tables(0)
            Me.Cbo_type.DataSource = ds_out

            Cbo_type.DataSource = bs_out
            Cbo_type.DisplayMember = "RPTTYPE"
            Cbo_type.ValueMember = "ROWID"

            Opt_Allgrp.Checked = True
            Opt_AllItem.Checked = True
            opt_allcode.Checked = True
            opt_allloc.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_searchLoc_GotFocus(sender As Object, e As EventArgs) Handles txt_searchLoc.GotFocus
        txt_searchLoc.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchLoc_LostFocus(sender As Object, e As EventArgs) Handles txt_searchLoc.LostFocus
        txt_searchLoc.BackColor = Color.White
    End Sub

    Private Sub txt_searchLoc_TextChanged(sender As Object, e As EventArgs) Handles txt_searchLoc.TextChanged
        Dim indx As Integer = Chklst_loc.FindString(txt_searchLoc.Text)
        If indx >= 0 Then Chklst_loc.SelectedIndex = indx
    End Sub

    Private Sub Opt_AllLoc_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allloc.CheckedChanged
        If opt_allloc.Checked = True Then
            txt_searchLoc.Enabled = False
            txt_searchLoc.Text = ""
            Chklst_loc.Enabled = False
        Else
            Chklst_loc.Enabled = True
            txt_searchLoc.Enabled = True
            Groupwhrcond_t = ""
            Me.Chklst_loc.DataSource = ds_Loc
            Chklst_loc.DataSource = bs_Loc
            Chklst_loc.DisplayMember = "GODOWNNAME"
            Chklst_loc.ValueMember = "MASTERID"
        End If
    End Sub

    Private Sub LoadGrid(Optional ByVal Cnt_t As Integer = 0)
        Try
            Dim k As Integer = 1, j As Integer
            Dim Griddisptype_t As String
            Dim NoofDays_t As String

            Cursor = Cursors.WaitCursor

            If opt_allloc.Checked = True Then
                Locationwhrcond_t = "SELECT MASTERID FROM GODOWN_MASTER "
            Else
                Locationwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_loc.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_loc.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_loc.ValueMember).ToString
                    Locationwhrcond_t = String.Concat(Locationwhrcond_t, valmember, ",")
                Next
                If Locationwhrcond_t.Length > 0 Then Locationwhrcond_t = Locationwhrcond_t.Substring(0, Locationwhrcond_t.Length - 1)
            End If

            If Opt_Allgrp.Checked = True Then
                Groupwhrcond_t = "SELECT GROUPID FROM ITEM_MASTER "
            Else
                Groupwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Grp.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Grp.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Grp.ValueMember).ToString
                    Groupwhrcond_t = String.Concat(Groupwhrcond_t, valmember, ",")
                Next
                If Groupwhrcond_t.Length > 0 Then Groupwhrcond_t = Groupwhrcond_t.Substring(0, Groupwhrcond_t.Length - 1)
            End If

            If Opt_AllItem.Checked = True Then
                Itemwhrcond_t = "SELECT ITEMID FROM ITEM_MASTER   "

                If opt_allcode.Checked = False Then
                    Itemwhrcond_t = ""
                    Dim valmember As String
                    For idx As Integer = 0 To Me.Chklst_code.CheckedItems.Count - 1
                        Dim drv As DataRowView = CType(Chklst_code.CheckedItems(idx), DataRowView)
                        Dim dr As DataRow = drv.Row
                        valmember = dr(Chklst_code.ValueMember).ToString
                        Itemwhrcond_t = String.Concat(Itemwhrcond_t, valmember, ",")
                    Next
                    If Itemwhrcond_t.Length > 0 Then Itemwhrcond_t = Itemwhrcond_t.Substring(0, Itemwhrcond_t.Length - 1)
                End If
            Else
                Itemwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Item.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Item.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Item.ValueMember).ToString
                    Itemwhrcond_t = String.Concat(Itemwhrcond_t, valmember, ",")
                Next
                If Itemwhrcond_t.Length > 0 Then Itemwhrcond_t = Itemwhrcond_t.Substring(0, Itemwhrcond_t.Length - 1)
            End If

            If Itemwhrcond_t = "" Or Itemwhrcond_t Is Nothing Then Itemwhrcond_t = "0"
            If Groupwhrcond_t = "" Or Groupwhrcond_t Is Nothing Then Groupwhrcond_t = "0"
            If Locationwhrcond_t = "" Or Locationwhrcond_t Is Nothing Then Locationwhrcond_t = "0"


            If dd_fromdays.Value = 0 And Dd_todays.Value = 0 Then
                NoofDays_t = " >  0 "
            Else
                NoofDays_t = " BETWEEN " + dd_fromdays.Value.ToString + " AND " + Dd_todays.Value.ToString
            End If

            cmd = New SqlCommand
            cmd.CommandTimeout = 30000
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SALESANALYSIS_RPT"
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = Dtp_fromdate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DTP_Todate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
            cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = Groupwhrcond_t
            cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemwhrcond_t
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationwhrcond_t
            cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = NoofDays_t
            cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ComboBox1.SelectedIndex
            da = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            ds_detl.Clear()
            da.Fill(ds_detl, "Table1")

            Dim Rowcnt As Integer, Colcnt_t As Integer
            Rowcnt = ds_detl.Tables(0).Rows.Count
            Colcnt_t = ds_detl.Tables(0).Columns.Count

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False
            GridView1.AutoGenerateColumns = True

            Dim tables As DataTableCollection = ds_detl.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.AutoResizeColumns()
            GridView1.Refresh()
            Dim val_t As String, itemid_t, itemid_t1 As Double
            Dim headertext As String

            For j = 0 To GridView1.Columns.Count - 1
                If Not GridView1.Columns(j).ValueType Is Nothing Then Griddisptype_t = IIf(IsDBNull(GridView1.Columns(j).ValueType.ToString), "", GridView1.Columns(j).ValueType.ToString)

                If Griddisptype_t = "System.Decimal" Then
                    GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If

                If j >= 5 And j < GridView1.Columns.Count - 2 Then
                    GridView1.Columns(j).Width = 45
                End If

                If LCase(GridView1.Columns(j).HeaderText).IndexOf("id") <> -1 Then
                    GridView1.Columns(j).Visible = False
                End If

                If ComboBox1.SelectedIndex = 1 Then
                    headertext = GridView1.Columns(j).HeaderText
                    If j > 5 Then
                        If headertext.Length = 5 Then headertext = headertext.Substring(0, headertext.Length - 1)
                        GridView1.Columns(j).HeaderText = headertext
                    End If
                End If
                GridView1.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            If GridView1.Rows.Count > 1 And ComboBox1.SelectedIndex = 1 Then GridView1.Rows(0).DefaultCellStyle.BackColor = Color.BurlyWood
            If GridView1.Rows.Count > 1 And ComboBox1.SelectedIndex = 1 Then GridView1.Rows(0).Frozen = True

            GridView1.Columns(GridView1.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(GridView1.Columns.Count - 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            GridView1.AutoResizeColumns()
            GridView1.ReadOnly = True
            'GridView1.SortOrder = Windows.Forms.SortOrder.None

            Cursor = Cursors.Default

            If Groupwhrcond_t = "SELECT GROUPID FROM ITEM_MASTER " Then Groupwhrcond_t = "00"
            Groupwhrcond_t = Groupwhrcond_t + ","

            If Itemwhrcond_t = "select itemid from item_master where groupid in (" & SelectedGroup_t & ")  " Or Itemwhrcond_t = "SELECT ITEMID FROM ITEM_MASTER   " Then Itemwhrcond_t = "00"
            Itemwhrcond_t = Itemwhrcond_t + ","

            If GridView1.Rows.Count > 0 Then GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightSkyBlue
            If GridView1.Rows.Count > 0 Then GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.DarkBlue

        Catch ex As Exception
            If ex.Message = "Cannot find table 0." Then
                Cursor = Cursors.Default
                Exit Sub
            End If
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub OrderLedger(ByVal rowindex As Integer, ByVal colindex As Integer)
        Try
            Dim ItemName_t As String
            Dim Locationid_t As Double
            Dim Itemid As String, Month As String

            Itemid = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(0).Value), 0, (GridView1.Rows(rowindex).Cells(0).Value))
            If ComboBox1.SelectedIndex <> 2 Then Locationid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(1).Value), 0, (GridView1.Rows(rowindex).Cells(1).Value))
            If ComboBox1.SelectedIndex <> 2 Then Month = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(3).Value), "", (GridView1.Rows(rowindex).Cells(3).Value))

            For i = 0 To GridView1.Columns.Count - 1
                Colname_t = GridView1.Columns(i).HeaderText
                Select Case LCase(Colname_t)
                    Case "item description", "itemname", "itemdescription", "item name"
                        ItemName_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(i).Value), 0, (GridView1.Rows(rowindex).Cells(i).Value))
                End Select
            Next

            Dim NoofDays_t As String

            If dd_fromdays.Value = 0 And Dd_todays.Value = 0 Then
                NoofDays_t = " >  0 "
            Else
                NoofDays_t = " BETWEEN " + dd_fromdays.Value.ToString + " AND " + Dd_todays.Value.ToString
            End If

            Dim frm As New Frm_SalesAnalysisDetail
            If ComboBox1.SelectedIndex <> -1 Then frm.Type_v = ComboBox1.SelectedIndex
            frm.Lbl_ItemName.Text = ItemName_t
            frm.DtpFromDate.Value = Dtp_fromdate.Value
            frm.DtpToDate.Value = DTP_Todate.Value
            frm.Days_t = NoofDays_t
            frm.Selecteditemid_v = Itemid
            frm.SelectedLocationid_v = Locationid_t
            frm.Month_v = Month
            frm.StartPosition = FormStartPosition.CenterScreen
            frm.ShowInTaskbar = False
            frm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try
            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & TextBox1.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).Name
        End If
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Try
            Dim Type_t As String

            Cursor = Cursors.WaitCursor
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If ComboBox1.SelectedIndex = 1 And e.RowIndex > 0 Then Call OrderLedger(e.RowIndex, e.ColumnIndex)
                If ComboBox1.SelectedIndex = 0 And e.RowIndex >= 0 Then Call OrderLedger(e.RowIndex, e.ColumnIndex)
                If ComboBox1.SelectedIndex = 2 And e.RowIndex >= 0 Then Call OrderLedger(e.RowIndex, e.ColumnIndex)
            End If
            Cursor = Cursors.Default

        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Try
            Call LoadGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub Chklst_code_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_code.ItemCheck
        Try
            Dim valmember As String, processt1 As String
            If opt_allcode.Checked = True Then Exit Sub

            If Codewhrcond_t Is Nothing Then Codewhrcond_t = "00"

            If Chklst_code.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If Codewhrcond_t.Length > 1 Then
                        If Codewhrcond_t.IndexOf(Chklst_code.SelectedValue) = -1 Then
                            Codewhrcond_t = String.Concat(Codewhrcond_t, Chklst_code.SelectedValue, ",")
                        End If
                    Else
                        Codewhrcond_t = String.Concat(Codewhrcond_t, Chklst_code.SelectedValue, ",")
                    End If
                Else
                    Codewhrcond_t = Codewhrcond_t.Replace(Convert.ToString(Chklst_code.SelectedValue) + ",", "")
                End If
            End If

            If Chklst_code.SelectedItems.Count = 0 Then Codewhrcond_t = ""

            Dim levstr As String
            If Codewhrcond_t.Length > 1 Then
                If Codewhrcond_t.Substring(Codewhrcond_t.Length - 1, 1) = "," Then
                    processt1 = Codewhrcond_t.Substring(0, Codewhrcond_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

            Selectedcode_t = processt1

            If Selectedcode_t Is Nothing Then Selectedcode_t = "00"

            ds_item = Nothing
            levstr = " select itemid,itemdes from item_master where itemid in (" & Selectedcode_t & ") order by ITEMDES "
            cmd = Nothing
            ds_item = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_item = New SqlDataAdapter(cmd)
            ds_item = New DataSet
            ds_item.Clear()
            da_item.Fill(ds_item)
            bs_item.DataSource = ds_item.Tables(0)

            Me.Chklst_Item.DataSource = Nothing
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "itemdes"
            Chklst_Item.ValueMember = "itemid"

            Dim cnt As Integer = ds_item.Tables(0).Rows.Count

            If cnt > 0 Then
                For i = 0 To cnt - 1
                    Chklst_Item.SetItemChecked(i, True)
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub opt_allcode_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allcode.CheckedChanged
        If opt_allcode.Checked = True Then
            Txt_searchcode.Enabled = False
            Txt_searchcode.Text = ""
            Chklst_code.Enabled = False
            Codewhrcond_t = ""

            Dim levstr As String
            ds_item = Nothing
            levstr = " SELECT ITEMID , itemdes FROM ITEM_MASTER ORDER BY itemdes "
            cmd = Nothing
            ds_item = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_item = New SqlDataAdapter(cmd)
            ds_item = New DataSet
            ds_item.Clear()
            da_item.Fill(ds_item)
            bs_item.DataSource = ds_item.Tables(0)
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "itemdes"
            Chklst_Item.ValueMember = "itemid"
            Opt_AllItem.Checked = True
            Chklst_Item.Enabled = False
            txt_searchitem.Enabled = False

        Else
            Chklst_code.Enabled = True
            Txt_searchcode.Enabled = True
            Me.Chklst_code.DataSource = ds_code
            Chklst_code.DataSource = bs_code
            Chklst_code.DisplayMember = "ITEMCODE"
            Chklst_code.ValueMember = "ITEMID"
        End If
    End Sub

    Private Sub Opt_Allgrp_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_Allgrp.CheckedChanged
        Dim levstr As String

        If Opt_Allgrp.Checked = True Then
            txt_searchgrp.Enabled = False
            txt_searchgrp.Text = ""
            Chklst_Grp.Enabled = False

            ds_item = Nothing
            levstr = " SELECT ITEMID,isnull(itemdes,'') + ' - ' + isnull(itemcode,'') as itemdes FROM ITEM_MASTER ORDER BY itemdes "
            cmd = Nothing
            ds_item = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_item = New DataSet
            ds_item.Clear()
            da.Fill(ds_item)
            bs_item.DataSource = ds_item.Tables(0)
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "itemdes"
            Chklst_Item.ValueMember = "ITEMID"
        Else
            Chklst_Grp.Enabled = True
            txt_searchgrp.Enabled = True
            Me.Chklst_Grp.DataSource = ds_group
            Chklst_Grp.DataSource = bs_group
            Chklst_Grp.DisplayMember = "groupname"
            Chklst_Grp.ValueMember = "masterid"
        End If
    End Sub

    Private Sub Opt_AllItem_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_AllItem.CheckedChanged
        Dim levstr As String

        If Opt_AllItem.Checked = True Then
            txt_searchitem.Enabled = False
            txt_searchitem.Text = ""
            Chklst_Item.Enabled = False
            'ds_size = Nothing
            'levstr = " SELECT MASTERID AS SIZEID ,SIZE FROM SIZE_MASTER where type ='Item' ORDER BY SIZE "
            'cmd = Nothing
            'ds_size = Nothing
            'cmd = New SqlCommand(levstr, conn)
            'cmd.Transaction = trans
            'cmd.CommandType = CommandType.Text
            'da_size = New SqlDataAdapter(cmd)
            'ds_size = New DataSet
            'ds_size.Clear()
            'da_size.Fill(ds_size)
            'bs_size.DataSource = ds_size.Tables(0)
        Else
            Itemwhrcond_t = ""

            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "ITEMDES"
            Chklst_Item.ValueMember = "ITEMID"
            Chklst_Item.Enabled = True
            txt_searchitem.Enabled = True
        End If
    End Sub

    Private Sub txt_searchgrp_GotFocus(sender As Object, e As EventArgs) Handles txt_searchgrp.GotFocus
        txt_searchgrp.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchgrp_LostFocus(sender As Object, e As EventArgs) Handles txt_searchgrp.LostFocus
        txt_searchgrp.BackColor = Color.White
    End Sub

    Private Sub txt_searchgrp_TextChanged(sender As Object, e As EventArgs) Handles txt_searchgrp.TextChanged
        Dim indx As Integer = Chklst_Grp.FindString(txt_searchgrp.Text)
        If indx >= 0 Then Chklst_Grp.SelectedIndex = indx
    End Sub

    Private Sub txt_searchitem_GotFocus(sender As Object, e As EventArgs) Handles txt_searchitem.GotFocus
        txt_searchitem.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchitem_LostFocus(sender As Object, e As EventArgs) Handles txt_searchitem.LostFocus
        txt_searchitem.BackColor = Color.White
    End Sub

    Private Sub txt_searchitem_TextChanged(sender As Object, e As EventArgs) Handles txt_searchitem.TextChanged
        Dim ds_search As New DataSet
        Dim levstr As String, Itemcode As String
        Dim cnt As Integer

        ds_search = Nothing
        levstr = " SELECT ITEMID,isnull(ITEMDES,'') as ITEMDES,isnull(itemcode,'') as itemcode  FROM ITEM_MASTER  " _
            & " WHERE ITEMDES LIKE '%" & Trim(txt_searchitem.Text) & "%'  OR ITEMCODE LIKE '%" & Trim(txt_searchitem.Text) & "%' ORDER BY ITEMDES "
        cmd = Nothing
        ds_search = Nothing
        cmd = New SqlCommand(levstr, conn)
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds_search = New DataSet
        ds_search.Clear()
        da.Fill(ds_search)
        cnt = ds_search.Tables(0).Rows.Count

        If cnt > 0 Then
            Itemcode = ds_search.Tables(0).Rows(0).Item("ITEMDES").ToString
        End If

        Dim indx As Integer = Chklst_Item.FindString(Itemcode)
        If indx >= 0 Then Chklst_Item.SelectedIndex = indx
    End Sub

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
    End Sub

    'Private Sub ExportToExcel()
    '    Dim Ex_Tot_pos As Integer = 0, Grndindex_t As Integer = 0, Headcol_t As Integer = 1
    '    Dim decformat_t As String = "", Griddisptype_t As String = ""
    '    Try
    '        Dim ds_frmbill As New DataSet
    '        Dim da_frmbill As SqlDataAdapter

    '        If GridView1.Rows.Count > 0 Then
    '            Cursor = Cursors.WaitCursor

    '            Dim xlApp As Microsoft.Office.Interop.Excel.Application
    '            Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
    '            Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
    '            Dim oRng As Microsoft.Office.Interop.Excel.Range = Nothing
    '            Dim misValue As Object = System.Reflection.Missing.Value
    '            Dim i As Integer, j As Integer
    '            Dim P As Integer = 1, Q As Integer = 1

    '            Dim default_location As String, FromBill_t As String, ToBill_t As String
    '            Dim Filename_t As String = "QueryExcel"

    '            default_location = Application.StartupPath

    '            If Microsoft.VisualBasic.Right(default_location, 5) = "Debug" Then
    '                default_location = Replace(default_location, "bin\Debug", "")
    '            ElseIf Microsoft.VisualBasic.Right(default_location, 7) = "Release" Then
    '                default_location = Replace(default_location, "bin\Release", "")
    '            End If

    '            default_location = default_location & "\Excel" & "\" & Filename_t & ".xlsx"
    '            ' default_location = Replace(default_location, "\\", "\")

    '            xlApp = New Microsoft.Office.Interop.Excel.Application
    '            xlWorkBook = xlApp.Workbooks.Add(misValue)
    '            xlWorkSheet = xlWorkBook.Sheets("sheet1")


    '            cmd = New SqlCommand
    '            cmd.CommandTimeout = 30000
    '            cmd.Connection = conn
    '            cmd.CommandType = CommandType.Text
    '            cmd.CommandText = "SELECT MIN(VCHNUM) AS FROMBILL ,MAX(VCHNUM) AS TOBILL FROM QUOTATION_HEADER WHERE VCHDATE BETWEEN '" & Dtp_fromdate.Value.ToString("yyyy/MM/dd") & "' AND '" & DTP_Todate.Value.ToString("yyyy/MM/dd") & "' + CAST('23:59:59' AS DATETIME)"
    '            da_frmbill = New SqlDataAdapter(cmd)
    '            ds_frmbill = New DataSet
    '            ds_frmbill.Clear()
    '            da_frmbill.Fill(ds_frmbill)


    '            If ds_frmbill.Tables(0).Rows.Count > 0 Then
    '                FromBill_t = ds_frmbill.Tables(0).Rows(0).Item("FROMBILL").ToString
    '                ToBill_t = ds_frmbill.Tables(0).Rows(0).Item("TOBILL").ToString
    '            End If

    '            Dim iSheet As Integer = 1
    '            xlWorkSheet = xlWorkBook.Sheets(iSheet)
    '            Dim k, jj As Integer
    '            jj = 0
    '            Dim iCountCol As Integer = 1

    '            If ComboBox1.SelectedIndex = 2 Then jj = 2 Else jj = 1

    '            For y = 0 To GridView1.Columns.Count - 1
    '                If GridView1.Columns(y).Visible Then
    '                    xlWorkSheet.Cells(3, Headcol_t) = GridView1.Columns(y).HeaderText
    '                    '  xlWorkSheet.Cells(2, Headcol_t) = "From Bill : " & FromBill_t & "To : " & ToBill_t
    '                    Headcol_t = Headcol_t + 1
    '                End If
    '            Next
    '            If Headcol_t > 0 Then iCountCol = Headcol_t - 1

    '            If Headcol_t > 26 Then iCountCol = 25

    '            Dim str As String = ChrW(64 + 1) & 1 & ":" & ChrW(64 + iCountCol) & 1

    '            oRng = xlWorkSheet.Range(str)
    '            With oRng
    '                .MergeCells = True
    '                .Value = "ITEM STOCK POSITION" & "     [As on Date :" & " " & DTP_Todate.Value.ToString("dd/MM/yyyy") & "]"
    '                .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
    '            End With

    '            If ComboBox1.SelectedIndex = 2 Then
    '                Dim str1 As String = ChrW(64 + 1) & 2 & ":" & ChrW(64 + iCountCol) & 2

    '                oRng = xlWorkSheet.Range(str1)
    '                With oRng
    '                    .MergeCells = True
    '                    .Value = "From Bill : " & FromBill_t & "   To : " & ToBill_t
    '                    .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
    '                End With
    '            End If

    '            decformat_t = ".000"
    '            Dim decval_t As Double

    '            For i = 0 To GridView1.RowCount - 1
    '                Q = 1
    '                For j = 0 To GridView1.ColumnCount - 1
    '                    P = 1
    '                    If GridView1.Columns(j).Visible = True Then
    '                        xlWorkSheet.Cells(i + 3, Q) = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
    '                        xlWorkSheet.Cells(i + 3, Q).Interior.Color = Color.White
    '                        xlWorkSheet.Cells(i + 3, Q).borders.LineStyle = 1
    '                        Q = Q + 1
    '                        Griddisptype_t = GridView1.Columns(j).ValueType.ToString
    '                        If Griddisptype_t = "System.Decimal" Then
    '                            xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" '+ decformat_t
    '                            decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
    '                        End If
    '                    End If
    '                Next j
    '            Next i

    '            xlWorkSheet.Columns.AutoFit()
    '            xlWorkSheet.Rows("1:1").Font.FontStyle = "Bold"
    '            xlWorkSheet.Rows("1:1").Font.Size = 13
    '            xlWorkSheet.Rows("2:2").Font.FontStyle = "Bold"
    '            xlWorkSheet.Rows("2:2").Font.Size = 11
    '            xlWorkSheet.Cells.Columns.AutoFit()
    '            xlWorkSheet.Cells.Select()
    '            xlWorkSheet.Cells.EntireColumn.AutoFit()
    '            xlWorkSheet.Cells(1, 1).Select()

    '            If System.IO.File.Exists(default_location) Then
    '                System.IO.File.Delete(default_location)
    '            End If

    '            xlWorkSheet.SaveAs(default_location)
    '            xlWorkBook.Close(False, Filename_t, misValue)

    '            xlApp.Quit()

    '            releaseObject(xlWorkSheet)
    '            releaseObject(xlWorkBook)
    '            releaseObject(xlApp)

    '            Dim res As MsgBoxResult
    '            res = MsgBox("Process completed, Would you like to open file?", MsgBoxStyle.YesNo)

    '            If (res = MsgBoxResult.Yes) Then
    '                Process.Start(default_location)
    '            End If

    '            Cursor = Cursors.Default

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub


    Private Sub ExportToExcel()
        Dim Ex_Tot_pos As Integer = 0, Grndindex_t As Integer = 0, Headcol_t As Integer = 1
        Dim decformat_t As String = "", Griddisptype_t As String = ""
        Dim ds_frmbill As New DataSet
        Dim da_frmbill As SqlDataAdapter
        Dim NoofDays_t As String
        Dim Locationwhrcond_t1, Groupwhrcond_t1, Itemwhrcond_t1 As String

        If opt_allloc.Checked = True Then
            Locationwhrcond_t1 = "SELECT MASTERID FROM GODOWN_MASTER "
        Else
            Locationwhrcond_t1 = ""
            Dim valmember As String
            For idx As Integer = 0 To Me.Chklst_loc.CheckedItems.Count - 1
                Dim drv As DataRowView = CType(Chklst_loc.CheckedItems(idx), DataRowView)
                Dim dr As DataRow = drv.Row
                valmember = dr(Chklst_loc.ValueMember).ToString
                Locationwhrcond_t1 = String.Concat(Locationwhrcond_t1, valmember, ",")
            Next
            If Locationwhrcond_t1.Length > 0 Then Locationwhrcond_t1 = Locationwhrcond_t1.Substring(0, Locationwhrcond_t1.Length - 1)
        End If

        If Opt_Allgrp.Checked = True Then
            Groupwhrcond_t1 = "SELECT GROUPID FROM ITEM_MASTER "
        Else
            Groupwhrcond_t1 = ""
            Dim valmember As String
            For idx As Integer = 0 To Me.Chklst_Grp.CheckedItems.Count - 1
                Dim drv As DataRowView = CType(Chklst_Grp.CheckedItems(idx), DataRowView)
                Dim dr As DataRow = drv.Row
                valmember = dr(Chklst_Grp.ValueMember).ToString
                Groupwhrcond_t1 = String.Concat(Groupwhrcond_t1, valmember, ",")
            Next
            If Groupwhrcond_t1.Length > 0 Then Groupwhrcond_t1 = Groupwhrcond_t1.Substring(0, Groupwhrcond_t1.Length - 1)
        End If

        If Opt_AllItem.Checked = True Then
            Itemwhrcond_t1 = "SELECT ITEMID FROM ITEM_MASTER   "

            If opt_allcode.Checked = False Then
                Itemwhrcond_t1 = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_code.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_code.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_code.ValueMember).ToString
                    Itemwhrcond_t1 = String.Concat(Itemwhrcond_t1, valmember, ",")
                Next
                If Itemwhrcond_t1.Length > 0 Then Itemwhrcond_t1 = Itemwhrcond_t1.Substring(0, Itemwhrcond_t1.Length - 1)
            End If
        Else
            Itemwhrcond_t1 = ""
            Dim valmember As String
            For idx As Integer = 0 To Me.Chklst_Item.CheckedItems.Count - 1
                Dim drv As DataRowView = CType(Chklst_Item.CheckedItems(idx), DataRowView)
                Dim dr As DataRow = drv.Row
                valmember = dr(Chklst_Item.ValueMember).ToString
                Itemwhrcond_t1 = String.Concat(Itemwhrcond_t1, valmember, ",")
            Next
            If Itemwhrcond_t1.Length > 0 Then Itemwhrcond_t1 = Itemwhrcond_t1.Substring(0, Itemwhrcond_t1.Length - 1)
        End If

        If Itemwhrcond_t1 = "" Or Itemwhrcond_t1 Is Nothing Then Itemwhrcond_t1 = "0"
        If Groupwhrcond_t1 = "" Or Groupwhrcond_t1 Is Nothing Then Groupwhrcond_t1 = "0"
        If Locationwhrcond_t1 = "" Or Locationwhrcond_t1 Is Nothing Then Locationwhrcond_t1 = "0"

        Try

            If GridView1.Rows.Count > 0 Then
                Cursor = Cursors.WaitCursor

                Dim xlApp As Microsoft.Office.Interop.Excel.Application
                Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
                Dim oRng As Microsoft.Office.Interop.Excel.Range = Nothing
                Dim misValue As Object = System.Reflection.Missing.Value
                Dim i As Integer, j As Integer
                Dim P As Integer = 1, Q As Integer = 1

                Dim default_location As String, FromBill_t As String, ToBill_t As String, Totalbill_t As Double

                Dim Filename_t As String = "QueryExcel"

                default_location = Application.StartupPath

                If Microsoft.VisualBasic.Right(default_location, 5) = "Debug" Then
                    default_location = Replace(default_location, "bin\Debug", "")
                ElseIf Microsoft.VisualBasic.Right(default_location, 7) = "Release" Then
                    default_location = Replace(default_location, "bin\Release", "")
                End If

                default_location = default_location & "\Excel" & "\" & Filename_t & ".xlsx"
                ' default_location = Replace(default_location, "\\", "\")

                xlApp = New Microsoft.Office.Interop.Excel.Application
                xlWorkBook = xlApp.Workbooks.Add(misValue)
                xlWorkSheet = xlWorkBook.Sheets("sheet1")

                Dim iSheet As Integer = 1
                xlWorkSheet = xlWorkBook.Sheets(iSheet)
                Dim k, jj As Integer
                jj = 0
                Dim iCountCol As Integer = 1
                jj = 1

                If dd_fromdays.Value = 0 And Dd_todays.Value = 0 Then
                    NoofDays_t = " >  0 "
                Else
                    NoofDays_t = " BETWEEN " + dd_fromdays.Value.ToString + " AND " + Dd_todays.Value.ToString

                End If

                cmd = New SqlCommand
                cmd.CommandTimeout = 30000
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "GET_FROMBILLTOBILL"
                cmd.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = Dtp_fromdate.Value.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = DTP_Todate.Value.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Compid", SqlDbType.VarChar).Value = Gencompid
                cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = Groupwhrcond_t1
                cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemwhrcond_t1
                cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationwhrcond_t1
                cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = NoofDays_t
                cmd.Parameters.Add("@type", SqlDbType.VarChar).Value = ComboBox1.SelectedIndex
                da_frmbill = New SqlDataAdapter(cmd)
                ds_frmbill = New DataSet
                ds_frmbill.Clear()
                da_frmbill.Fill(ds_frmbill)

                If ds_frmbill.Tables(0).Rows.Count > 0 Then
                    FromBill_t = ds_frmbill.Tables(0).Rows(0).Item("FROMBILL").ToString
                    ToBill_t = ds_frmbill.Tables(0).Rows(0).Item("TOBILL").ToString
                    Totalbill_t = ds_frmbill.Tables(0).Rows(0).Item("totalbill")
                End If

                For y = 0 To GridView1.Columns.Count - 1
                    If GridView1.Columns(y).Visible Then
                        xlWorkSheet.Cells(2, Headcol_t) = GridView1.Columns(y).HeaderText

                        Headcol_t = Headcol_t + 1
                    End If
                Next
                If Headcol_t > 0 Then iCountCol = Headcol_t - 1

                If Headcol_t > 26 Then iCountCol = 25

                Dim str As String = ChrW(64 + 1) & jj & ":" & ChrW(64 + iCountCol) & jj

                oRng = xlWorkSheet.Range(str)
                With oRng
                    .MergeCells = True
                    If ComboBox1.SelectedIndex <> 2 Then .Value = Me.Text & " [As on Date :" & " " & DTP_Todate.Value.ToString("dd/MM/yyyy") & "]" Else .Value = "From Bill : " & FromBill_t & "   To : " & ToBill_t
                    .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With

                'If ComboBox1.SelectedIndex = 2 Then
                '    Dim str1 As String = ChrW(64 + 1) & 2 & ":" & ChrW(64 + iCountCol) & 2

                '    oRng = xlWorkSheet.Range(str1)
                '    With oRng
                '        .MergeCells = True
                '        .Value = "From Bill : " & FromBill_t & "   To : " & ToBill_t
                '        .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                '    End With

                'End If

                decformat_t = ".000"
                Dim decval_t As Double

                For i = 0 To GridView1.RowCount - 1
                    Q = 1
                    For j = 0 To GridView1.ColumnCount - 1
                        P = 1
                        If GridView1.Columns(j).Visible = True Then
                            xlWorkSheet.Cells(i + 3, Q) = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
                            xlWorkSheet.Cells(i + 3, Q).Interior.Color = Color.White
                            xlWorkSheet.Cells(i + 3, Q).borders.LineStyle = 1
                            Q = Q + 1
                            Griddisptype_t = GridView1.Columns(j).ValueType.ToString
                            If Griddisptype_t = "System.Decimal" Then
                                xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" '+ decformat_t
                                decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
                            End If
                        End If
                    Next j
                Next i

                If ComboBox1.SelectedIndex = 2 Then
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 3) = "Total Bills : "
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 1) = Totalbill_t
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 3).Interior.Color = Color.White
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 3).borders.LineStyle = 1
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 1).Interior.Color = Color.White
                    xlWorkSheet.Cells((GridView1.RowCount + 3), GridView1.ColumnCount - 1).borders.LineStyle = 1
                End If

                xlWorkSheet.Columns.AutoFit()
                xlWorkSheet.Rows("1:1").Font.FontStyle = "Bold"
                xlWorkSheet.Rows("1:1").Font.Size = 13
                xlWorkSheet.Rows("2:2").Font.FontStyle = "Bold"
                xlWorkSheet.Rows("2:2").Font.Size = 11
                xlWorkSheet.Cells.Columns.AutoFit()
                xlWorkSheet.Cells.Select()
                xlWorkSheet.Cells.EntireColumn.AutoFit()
                xlWorkSheet.Cells(1, 1).Select()

                If System.IO.File.Exists(default_location) Then
                    System.IO.File.Delete(default_location)
                End If

                xlWorkSheet.SaveAs(default_location)
                xlWorkBook.Close(False, Filename_t, misValue)

                xlApp.Quit()

                releaseObject(xlWorkSheet)
                releaseObject(xlWorkBook)
                releaseObject(xlApp)

                Dim res As MsgBoxResult
                res = MsgBox("Process completed, Would you like to open file?", MsgBoxStyle.YesNo)

                If (res = MsgBoxResult.Yes) Then
                    Process.Start(default_location)
                End If

                Cursor = Cursors.Default

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.Yellow
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView1.Focus()
        End If
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = Color.White
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Filterby()
    End Sub

    Private Sub Cmd_print_Click(sender As Object, e As EventArgs)
        'Dim rpt As New Frm_Reports_Init
        'Dim ProcessName As String, ToDate_t As String
        'Cursor = Cursors.WaitCursor
        'ToDate_t = DTP_Vchdate.Value.ToString("yyyy/MM/dd")
        'Cursor = Cursors.WaitCursor
        'rpt.ShowInTaskbar = False
        'rpt.Init(conn, "Itemstock", Servername_t, 0, Nothing, ToDate_t, "", "SUNDOMESTICPLUS", Gencompid, Nothing, Nothing, Nothing, _
        'Locationwhrcond_t, Nothing, Nothing, Nothing, Itemwhrcond_t, Nothing, Nothing, Groupwhrcond_t, Nothing, FinishedStck, DamageStk, LooseStk)
        'rpt.StartPosition = FormStartPosition.CenterScreen
        'rpt.ShowDialog()
        'Cursor = Cursors.Default
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            Dim Type_t As String

            If e.KeyCode = Keys.Enter Then
                Cursor = Cursors.WaitCursor
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name

                If ComboBox1.SelectedIndex = 1 And Rowindex_t > 0 Then Call OrderLedger(Rowindex_t, colindex_t)
                If ComboBox1.SelectedIndex = 0 And Rowindex_t >= 0 Then Call OrderLedger(Rowindex_t, colindex_t)

            ElseIf e.KeyCode = Keys.Back Then
                GridView1.BeginEdit(True)
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Todate.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmd_ok.Focus()
        End If
    End Sub

    Private Sub cmd_ok_KeyDown(sender As Object, e As KeyEventArgs) Handles cmd_ok.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Frm_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        Call Execute()
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   New Font("Calibri", 9.25, FontStyle.Bold), _
                                   b, _
                                   e.RowBounds.Location.X + 10, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub Chklst_Grp_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_Grp.ItemCheck
        Try
            Dim valmember As String, processt1 As String

            If Opt_Allgrp.Checked = True Then Exit Sub

            'SelectedParty_t = "0"
            'If Not SelectedFollowed_t Is Nothing Then
            '    If SelectedFollowed_t.Substring(SelectedFollowed_t.Length - 1, 1) <> "," Then
            '        SelectedFollowed_t = SelectedFollowed_t + ","
            '    End If
            'End If
            If Groupwhrcond_t Is Nothing Then Groupwhrcond_t = "00"

            If Chklst_Grp.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If Groupwhrcond_t.Length > 1 Then
                        If Groupwhrcond_t.IndexOf(Chklst_Grp.SelectedValue) = -1 Then
                            Groupwhrcond_t = String.Concat(Groupwhrcond_t, Chklst_Grp.SelectedValue, ",")
                        End If
                    Else
                        Groupwhrcond_t = String.Concat(Groupwhrcond_t, Chklst_Grp.SelectedValue, ",")
                    End If
                Else
                    Groupwhrcond_t = Groupwhrcond_t.Replace(Convert.ToString(Chklst_Grp.SelectedValue) + ",", "")
                End If
            End If

            If Chklst_Grp.SelectedItems.Count = 0 Then Groupwhrcond_t = ""

            Dim levstr As String
            If Groupwhrcond_t.Length > 1 Then

                If Groupwhrcond_t.Substring(Groupwhrcond_t.Length - 1, 1) = "," Then
                    processt1 = Groupwhrcond_t.Substring(0, Groupwhrcond_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

            SelectedGroup_t = processt1

            ds_item = Nothing
            levstr = " select itemid,isnull(ITEMDES,'')  as ITEMDES from item_master where groupid in (" & SelectedGroup_t & ") order by ITEMDES"
            cmd = Nothing
            ds_item = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_item = New SqlDataAdapter(cmd)
            ds_item = New DataSet
            ds_item.Clear()
            da_item.Fill(ds_item)
            bs_item.DataSource = ds_item.Tables(0)

            Me.Chklst_Item.DataSource = Nothing
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "ITEMDES"
            Chklst_Item.ValueMember = "itemid"


            ds_code = Nothing
            levstr = " select itemid,isnull(itemcode,'')  as itemcode from item_master where groupid in (" & SelectedGroup_t & ") and itemcode is not null and itemcode <> '' order by ITEMDES"
            cmd = Nothing
            ds_code = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_code = New SqlDataAdapter(cmd)
            ds_code = New DataSet
            ds_code.Clear()
            da_code.Fill(ds_code)
            bs_code.DataSource = ds_code.Tables(0)

            Me.Chklst_code.DataSource = Nothing
            Me.Chklst_code.DataSource = ds_code
            Chklst_code.DataSource = bs_code
            Chklst_code.DisplayMember = "itemcode"
            Chklst_code.ValueMember = "itemid"

            'For i As Integer = 0 To Chklst_Item.Items.Count - 1
            '    Chklst_Item.SetItemChecked(i, True)
            'Next
            'For  I =0 TO Chklst_Job. 
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Chklst_Item_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_Item.ItemCheck
        Try
            Dim valmember As String, processt1 As String
            If Opt_AllItem.Checked = True Then Exit Sub

            If Itemwhrcond_t Is Nothing Then Itemwhrcond_t = "00"

            If Chklst_Item.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If Itemwhrcond_t.Length > 1 Then
                        If Itemwhrcond_t.IndexOf(Chklst_Item.SelectedValue) = -1 Then
                            Itemwhrcond_t = String.Concat(Itemwhrcond_t, Chklst_Item.SelectedValue, ",")
                        End If
                    Else
                        Itemwhrcond_t = String.Concat(Itemwhrcond_t, Chklst_Item.SelectedValue, ",")
                    End If
                Else
                    Itemwhrcond_t = Itemwhrcond_t.Replace(Convert.ToString(Chklst_Item.SelectedValue) + ",", "")
                End If
            End If

            If Chklst_Item.SelectedItems.Count = 0 Then Itemwhrcond_t = ""

            Dim levstr As String
            If Itemwhrcond_t.Length > 1 Then
                If Itemwhrcond_t.Substring(Itemwhrcond_t.Length - 1, 1) = "," Then
                    processt1 = Itemwhrcond_t.Substring(0, Itemwhrcond_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Dtp_fromdate_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_fromdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            DTP_Todate.Focus()
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        e.Handled = True
        If e.KeyChar = "I" Or e.KeyChar = "i" Then
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = 1
        End If
    End Sub

    Private Sub Txt_searchcode_GotFocus(sender As Object, e As EventArgs) Handles Txt_searchcode.GotFocus
        Txt_searchcode.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_searchcode_LostFocus(sender As Object, e As EventArgs) Handles Txt_searchcode.LostFocus
        Txt_searchcode.BackColor = Color.White
    End Sub

    Private Sub Txt_searchcode_TextChanged(sender As Object, e As EventArgs) Handles Txt_searchcode.TextChanged
        Dim indx As Integer = Chklst_code.FindString(Txt_searchcode.Text)
        If indx >= 0 Then Chklst_code.SelectedIndex = indx
    End Sub

    Private Sub cmd_print_Click1(sender As Object, e As EventArgs) Handles cmd_print.Click
        Try
            Dim NoofDays_t As String

            If opt_allloc.Checked = True Then
                Locationwhrcond_t = "SELECT MASTERID FROM GODOWN_MASTER "
            Else
                Locationwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_loc.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_loc.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_loc.ValueMember).ToString
                    Locationwhrcond_t = String.Concat(Locationwhrcond_t, valmember, ",")
                Next
                If Locationwhrcond_t.Length > 0 Then Locationwhrcond_t = Locationwhrcond_t.Substring(0, Locationwhrcond_t.Length - 1)
            End If

            If Opt_Allgrp.Checked = True Then
                Groupwhrcond_t = "SELECT GROUPID FROM ITEM_MASTER "
            Else
                Groupwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Grp.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Grp.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Grp.ValueMember).ToString
                    Groupwhrcond_t = String.Concat(Groupwhrcond_t, valmember, ",")
                Next
                If Groupwhrcond_t.Length > 0 Then Groupwhrcond_t = Groupwhrcond_t.Substring(0, Groupwhrcond_t.Length - 1)
            End If

            If Opt_AllItem.Checked = True Then
                Itemwhrcond_t = "SELECT ITEMID FROM ITEM_MASTER   "

                If opt_allcode.Checked = False Then
                    Itemwhrcond_t = ""
                    Dim valmember As String
                    For idx As Integer = 0 To Me.Chklst_code.CheckedItems.Count - 1
                        Dim drv As DataRowView = CType(Chklst_code.CheckedItems(idx), DataRowView)
                        Dim dr As DataRow = drv.Row
                        valmember = dr(Chklst_code.ValueMember).ToString
                        Itemwhrcond_t = String.Concat(Itemwhrcond_t, valmember, ",")
                    Next
                    If Itemwhrcond_t.Length > 0 Then Itemwhrcond_t = Itemwhrcond_t.Substring(0, Itemwhrcond_t.Length - 1)
                End If
            Else
                Itemwhrcond_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Item.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Item.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Item.ValueMember).ToString
                    Itemwhrcond_t = String.Concat(Itemwhrcond_t, valmember, ",")
                Next
                If Itemwhrcond_t.Length > 0 Then Itemwhrcond_t = Itemwhrcond_t.Substring(0, Itemwhrcond_t.Length - 1)
            End If

            If Itemwhrcond_t = "" Or Itemwhrcond_t Is Nothing Then Itemwhrcond_t = "0"
            If Groupwhrcond_t = "" Or Groupwhrcond_t Is Nothing Then Groupwhrcond_t = "0"
            If Locationwhrcond_t = "" Or Locationwhrcond_t Is Nothing Then Locationwhrcond_t = "0"


            If dd_fromdays.Value = 0 And Dd_todays.Value = 0 Then
                NoofDays_t = " >  0 "
            Else
                NoofDays_t = " BETWEEN " + dd_fromdays.Value.ToString + " AND " + Dd_todays.Value.ToString
            End If

            Rm.Init(conn, "sales analysis " + DirectCast(Cbo_type.SelectedItem, System.Data.DataRowView).Row.ItemArray(0).ToString, Servername_t, Nothing, Dtp_fromdate.Value.ToString("yyyy/MM/dd"), DTP_Todate.Value.ToString("yyyy/MM/dd"),
                    Nothing, conn.Database, Gencompid, Nothing, Nothing, Nothing, Selectedcode_t, Nothing, Groupwhrcond_t, Me.Name, Nothing, Groupwhrcond_t, Itemwhrcond_t, Locationwhrcond_t, NoofDays_t, 3)

            Rm.StartPosition = FormStartPosition.CenterScreen
            Rm.ShowDialog()

            If Groupwhrcond_t = "SELECT GROUPID FROM ITEM_MASTER " Then Groupwhrcond_t = "00"
            Groupwhrcond_t = Groupwhrcond_t + ","

            If Itemwhrcond_t = "select itemid from item_master where groupid in (" & SelectedGroup_t & ")  " Or Itemwhrcond_t = "SELECT ITEMID FROM ITEM_MASTER   " Then Itemwhrcond_t = "00"
            Itemwhrcond_t = Itemwhrcond_t + ","
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class