Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms

Public Class Frm_Stock
    Dim cmd As SqlCommand
    Dim da, da_code As SqlDataAdapter
    Dim ds, ds_item, ds_group, ds_loc, ds_code As New DataSet
    Dim bs, bs_item, bs_group, bs_loc, bs_code As New BindingSource
    Dim TamilFont_t As Integer
    Dim colindex_t As Integer, Filtercolnmae_t As String, SelectedItem_t As String, SelectedGroup_t As String, SelectedLoc_t As String, Selectedcode_t As String

    Private Sub Execute()
        Try
            Dim levstr As String = ""
            Dim ds_settings As New DataSet
            Dim da_settings As SqlDataAdapter
            Dim sqlstr As String
            Dim dscnt As Integer, Val_t As Double

            ds_settings = Nothing
            ' ds_settings.Clear()
            Sqlstr = "SELECT ISNULL(NUMERICVALUE,0 ) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS  ='TAMIL FONT' "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings)
            dscnt = ds_settings.Tables(0).Rows.Count

            If dscnt > 0 Then
                Val_t = ds_settings.Tables(0).Rows(0).Item("numericvalue")
                TamilFont_t = Val_t
            Else
                Val_t = 0
                TamilFont_t = 0
            End If

            ds_item = Nothing
            If Val_t = 1 Then
                levstr = " SELECT ITEMID,isnull(itemtamildes,'')   as ITEMNAME FROM ITEM_MASTER ORDER BY itemtamildes "
                Chklst_Item.Font = New Font("Verdana", 9, FontStyle.Bold)
            Else
                levstr = " SELECT ITEMID,isnull(ITEMDES,'') + ' - ' + isnull(itemcode,'') as ITEMNAME FROM ITEM_MASTER ORDER BY ITEMNAME "
            End If

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
            Chklst_Item.DisplayMember = "ITEMNAME"
            Chklst_Item.ValueMember = "ITEMID"

            ds_loc = Nothing
            levstr = " SELECT MASTERID,GODOWNNAME FROM GODOWN_MASTER ORDER BY GODOWNNAME "
            cmd = Nothing
            ds_loc = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_loc = New DataSet
            ds_loc.Clear()
            da.Fill(ds_loc)
            bs_loc.DataSource = ds_loc.Tables(0)
            Me.Chklst_loc.DataSource = ds_loc
            Chklst_loc.DataSource = bs_loc
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

            Opt_Allgrp.Checked = True
            Opt_AllItem.Checked = True
            opt_allloc.Checked = True
            opt_allcode.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Chklst_code_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_code.ItemCheck
        Try
            Dim valmember As String, processt1 As String, Codewhrcond_t As String
            If opt_allcode.Checked = True Then Exit Sub

            If Selectedcode_t Is Nothing Then Selectedcode_t = "00"

            If Chklst_code.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If Selectedcode_t.Length > 1 Then
                        If Selectedcode_t.IndexOf(Chklst_code.SelectedValue) = -1 Then
                            Selectedcode_t = String.Concat(Selectedcode_t, Chklst_code.SelectedValue, ",")
                        End If
                    Else
                        Selectedcode_t = String.Concat(Selectedcode_t, Chklst_code.SelectedValue, ",")
                    End If
                Else
                    Selectedcode_t = Selectedcode_t.Replace(Convert.ToString(Chklst_code.SelectedValue) + ",", "")
                End If
            End If

            If Chklst_code.SelectedItems.Count = 0 Then Selectedcode_t = ""

            Dim levstr As String
            If Selectedcode_t.Length > 1 Then
                If Selectedcode_t.Substring(Selectedcode_t.Length - 1, 1) = "," Then
                    processt1 = Selectedcode_t.Substring(0, Selectedcode_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

            ' Selectedcode_t = processt1

            If processt1 Is Nothing Then processt1 = "00"

            ds_item = Nothing
            levstr = " select itemid,itemdes from item_master where itemid in (" & processt1 & ") order by ITEMDES "
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

            Me.Chklst_Item.DataSource = Nothing
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "itemdes"
            Chklst_Item.ValueMember = "itemid"

            ' Dim cnt As Integer = ds_item.Tables(0).Rows.Count

            'If cnt > 0 Then
            '    For i = 0 To cnt - 1
            '        Chklst_Item.SetItemChecked(i, True)
            '    Next
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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


    Private Sub opt_allcode_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allcode.CheckedChanged
        If opt_allcode.Checked = True Then
            Txt_searchcode.Enabled = False
            Txt_searchcode.Text = ""
            Chklst_code.Enabled = False

            Dim levstr As String
            ds_item = Nothing
            levstr = " SELECT ITEMID , itemdes FROM ITEM_MASTER ORDER BY itemdes "
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

    Private Sub LoadGrid()
        Try
            Dim Colcnt_t As Integer, Rowcnt As Integer, Datatype_t As String

            If Opt_Allgrp.Checked = True Then
                SelectedGroup_t = "SELECT GROUPID FROM ITEM_MASTER "
            Else
                SelectedGroup_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Grp.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Grp.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Grp.ValueMember).ToString
                    SelectedGroup_t = String.Concat(SelectedGroup_t, valmember, ",")
                Next
                If SelectedGroup_t.Length > 0 Then SelectedGroup_t = SelectedGroup_t.Substring(0, SelectedGroup_t.Length - 1)
            End If

            If Opt_AllItem.Checked = True Then
                SelectedItem_t = "SELECT itemid FROM ITEM_MASTER "

                If opt_allcode.Checked = False Then
                    SelectedItem_t = ""
                    Dim valmember As String
                    For idx As Integer = 0 To Me.Chklst_code.CheckedItems.Count - 1
                        Dim drv As DataRowView = CType(Chklst_code.CheckedItems(idx), DataRowView)
                        Dim dr As DataRow = drv.Row
                        valmember = dr(Chklst_code.ValueMember).ToString
                        SelectedItem_t = String.Concat(SelectedItem_t, valmember, ",")
                    Next
                    If SelectedItem_t.Length > 0 Then SelectedItem_t = SelectedItem_t.Substring(0, SelectedItem_t.Length - 1)
                End If
            Else
                SelectedItem_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Item.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Item.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Item.ValueMember).ToString
                    SelectedItem_t = String.Concat(SelectedItem_t, valmember, ",")
                Next
                If SelectedItem_t.Length > 0 Then SelectedItem_t = SelectedItem_t.Substring(0, SelectedItem_t.Length - 1)
            End If

            If opt_allloc.Checked = True Then
                SelectedLoc_t = "SELECT MASTERID FROM GODOWN_MASTER "
            Else
                SelectedLoc_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_loc.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_loc.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_loc.ValueMember).ToString
                    SelectedLoc_t = String.Concat(SelectedLoc_t, valmember, ",")
                Next
                If SelectedLoc_t.Length > 0 Then SelectedLoc_t = SelectedLoc_t.Substring(0, SelectedLoc_t.Length - 1)
            End If

            If SelectedLoc_t = "" Or SelectedLoc_t Is Nothing Then SelectedLoc_t = "000"
            If SelectedItem_t = "" Or SelectedItem_t Is Nothing Then SelectedItem_t = "000"
            If SelectedGroup_t = "" Or SelectedGroup_t Is Nothing Then SelectedGroup_t = "000"

            ds = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "ITEMSTOCK_RPT"
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DtpToDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = SelectedLoc_t
            cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = SelectedItem_t
            cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = SelectedGroup_t
            cmd.Parameters.Add("@tamilfont", SqlDbType.VarChar).Value = TamilFont_t
            cmd.Parameters.Add("@reorder", SqlDbType.Bit).Value = IIf(chk_reorder.Checked = True, 1, 0)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "Table1")

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False
            GridView1.AutoGenerateColumns = True

            Dim tables As DataTableCollection = ds.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.Refresh()
            GridView1.ReadOnly = True

            Rowcnt = ds.Tables(0).Rows.Count
            Colcnt_t = ds.Tables(0).Columns.Count

            If Rowcnt > 0 Then
                GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightSkyBlue
                GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.DarkBlue
            End If

            Dim font As New Font( _
            GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 30

            Dim Colname_t As String

            For i = 0 To Colcnt_t - 1
                Colname_t = GridView1.Columns(i).HeaderText

                If LCase(Colname_t).IndexOf("id") <> -1 Then GridView1.Columns(i).Visible = False
                'GridView1.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Datatype_t = ds.Tables(0).Columns(i).DataType.ToString
                If Datatype_t = "System.Decimal" Then
                    GridView1.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    'GridView1.Columns(i).DefaultCellStyle.Format = "#"
                End If
            Next

            If SelectedGroup_t = "SELECT GROUPID FROM ITEM_MASTER " Then SelectedGroup_t = "00"
            SelectedGroup_t = SelectedGroup_t + ","

            If SelectedItem_t = "select itemid from item_master where groupid in (" & SelectedGroup_t & ")  " Or SelectedItem_t = "SELECT ITEMID FROM ITEM_MASTER   " Then SelectedItem_t = "00"
            SelectedItem_t = SelectedItem_t + ","

            GridView1.AutoResizeColumns()
            GridView1.Focus()
            GridView1.Columns(GridView1.Columns.Count - 4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try 
    End Sub

    Private Sub Filterby()
        Try
            If GridView1.Rows.Count = 0 Or GridView1.CurrentCell Is Nothing Then Exit Sub

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

    Private Sub ItemMovementAnalysis_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call Execute()
        DtpToDate.Value = Today
        'Call LoadGrid()
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click
        Cursor = Cursors.WaitCursor
        Call LoadGrid()
        Cursor = Cursors.Default
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.Yellow
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = Color.White
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Filterby()
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Hide()
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ItemStockLedger(ByVal Rowindex As Integer, ByVal Colindex As Integer)
        Try
            Dim Itemid_t As String = Nothing, Sizeid_t As String = Nothing, Locationid_t As String = Nothing, Colname_t As String = Nothing
            Dim Location As String = Nothing, Itemname As String = Nothing, Size As String = Nothing

            Cursor = Cursors.WaitCursor

            If Rowindex >= 0 And Colindex >= 0 Then
                For i = 0 To GridView1.Columns.Count - 1
                    Colname_t = GridView1.Columns(i).HeaderText
                    Colname_t = LCase(Colname_t)

                    Select Case LCase(Colname_t)
                        Case "itemid"
                            Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                            'Case "locationid", "godownid"
                            '    Locationid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                        Case "location"
                            Location = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                        Case "item", "item name"
                            Itemname = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                    End Select
                Next

                Dim ds_detl As New DataSet
                Dim cmd_detl As SqlCommand
                Dim da_detl As SqlDataAdapter

                If opt_allloc.Checked = True Then
                    Locationid_t = "SELECT MASTERID FROM GODOWN_MASTER "
                Else
                    Locationid_t = ""
                    Dim valmember As String
                    For idx As Integer = 0 To Me.Chklst_loc.CheckedItems.Count - 1
                        Dim drv As DataRowView = CType(Chklst_loc.CheckedItems(idx), DataRowView)
                        Dim dr As DataRow = drv.Row
                        valmember = dr(Chklst_loc.ValueMember).ToString
                        Locationid_t = String.Concat(Locationid_t, valmember, ",")
                    Next
                    If Locationid_t.Length > 0 Then Locationid_t = Locationid_t.Substring(0, Locationid_t.Length - 1)
                End If

                If Locationid_t = "" Or Locationid_t Is Nothing Then Locationid_t = "000"

                If Itemid_t = 0 Then
                    Exit Sub
                    Cursor = Cursors.Default
                End If

                ds_detl = Nothing
                cmd_detl = Nothing
                cmd_detl = New SqlCommand
                cmd_detl.Connection = conn
                cmd_detl.CommandType = CommandType.StoredProcedure
                cmd_detl.CommandText = "GET_ITEMSTOCK_VCHDATE"
                cmd_detl.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
                cmd_detl.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemid_t
                da_detl = New SqlDataAdapter(cmd_detl)
                ds_detl = New DataSet
                da_detl.Fill(ds_detl)

                Dim VCHDATE As Date
                Dim frmledger As New Frm_stockLedger
                ' If Size Is Nothing Then Location = "Item : " + Location + "   Group : " + Itemname Else Location = "Item : " + Location + "   Group : " + Itemname + "   Size : " + Size
                frmledger.init(Itemid_t, "Item : " + Itemname, Locationid_t)
                If ds_detl.Tables(0).Rows.Count > 0 Then
                    VCHDATE = ds_detl.Tables(0).Rows(0).Item("VCHDATE")
                    frmledger.DtpFromDate.Value = VCHDATE
                End If
                frmledger.DtpToDate.Value = DtpToDate.Value
                frmledger.ShowInTaskbar = False
                frmledger.StartPosition = FormStartPosition.CenterScreen
                frmledger.ShowDialog()
            End If

            Cursor = Cursors.Default

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   New Font(GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold), _
                                   b, _
                                   e.RowBounds.Location.X + 10, _
                                   e.RowBounds.Location.Y + 4) '15
        End Using
    End Sub

    Private Sub DtpToDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpToDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_load.Focus()
        End If
    End Sub

    Private Sub Opt_Allgrp_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_Allgrp.CheckedChanged
        If Opt_Allgrp.Checked = True Then
            txt_searchgrp.Enabled = False
            txt_searchgrp.Text = ""
            Chklst_Grp.Enabled = False
        Else
            Chklst_Grp.Enabled = True
            txt_searchgrp.Enabled = True
            Me.Chklst_Grp.DataSource = ds_group
            Chklst_Grp.DataSource = bs_group
            Chklst_Grp.DisplayMember = "groupname"
            Chklst_Grp.ValueMember = "masterid"
        End If
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

    Private Sub ExportToExcel()
        Dim Ex_Tot_pos As Integer = 0, Grndindex_t As Integer = 0, dec_t As Integer, Tmpdscnt_ As Integer, Tmpindex_t As Integer, Headcol_t As Integer = 1
        Dim decformat_t As String = "", Gridcolhead_t As String, Griddisptype_t As String = ""
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

                Dim default_location As String
                Dim Filename_t As String = "QueryExcel"

                default_location = Application.StartupPath

                If Microsoft.VisualBasic.Right(default_location, 5) = "Debug" Then
                    default_location = Replace(default_location, "bin\Debug", "")
                ElseIf Microsoft.VisualBasic.Right(default_location, 7) = "Release" Then
                    default_location = Replace(default_location, "bin\Release", "")
                End If

                default_location = default_location & "\Excel" & "\" & Filename_t & ".xlsx"
                default_location = Replace(default_location, "\\", "\")

                xlApp = New Microsoft.Office.Interop.Excel.Application
                xlWorkBook = xlApp.Workbooks.Add(misValue)
                xlWorkSheet = xlWorkBook.Sheets("sheet1")

                Dim iSheet As Integer = 1
                xlWorkSheet = xlWorkBook.Sheets(iSheet)
                Dim k, jj As Integer
                jj = 0
                Dim iCountCol As Integer = 1
                jj = 1

                For y = 0 To GridView1.Columns.Count - 1
                    If GridView1.Columns(y).Visible Then
                        xlWorkSheet.Cells(2, Headcol_t) = GridView1.Columns(y).HeaderText
                        Headcol_t = Headcol_t + 1
                    End If
                Next
                If Headcol_t > 0 Then iCountCol = Headcol_t - 1
                Dim str As String = ChrW(64 + 1) & jj & ":" & ChrW(64 + iCountCol) & jj

                oRng = xlWorkSheet.Range(str)
                With oRng
                    .MergeCells = True
                    .Value = Me.Text

                    .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With

                decformat_t = ".000"
                Dim decval_t As Double

                For i = 0 To GridView1.RowCount - 1
                    Q = 1
                    For j = 0 To GridView1.ColumnCount - 1
                        P = 1

                        If GridView1.Columns(j).Visible = True Then
                            xlWorkSheet.Cells(i + 3, Q) = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
                            ' xlWorkSheet.Cells(i + 3, Q).Interior.Color = Color.LightGreen
                            xlWorkSheet.Cells(i + 3, Q).borders.LineStyle = 1
                            Q = Q + 1
                            Griddisptype_t = GridView1.Columns(j).ValueType.ToString
                            If Griddisptype_t = "System.Decimal" Then
                                ' xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" + decformat_t
                                ' decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
                            End If 
                        End If
                    Next j
                Next i

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

    Private Sub Opt_AllItem_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_AllItem.CheckedChanged
        If Opt_AllItem.Checked = True Then
            txt_searchitem.Enabled = False
            txt_searchitem.Text = ""
            Chklst_Item.Enabled = False
        Else
            Chklst_Item.Enabled = True
            txt_searchitem.Enabled = True
            '  Me.Chklst_Item.DataSource = ds_item
            ' Chklst_Item.DataSource = bs_item
            ' Chklst_Item.DisplayMember = "ITEMNAME"
            ' Chklst_Item.ValueMember = "ITEMID"
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
        Dim indx As Integer = Chklst_Item.FindString(txt_searchitem.Text)
        If indx >= 0 Then Chklst_Item.SelectedIndex = indx
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Call ItemStockLedger(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            Dim Rowindex_t As Double, Colindex_t As Double

            If GridView1.CurrentCell Is Nothing Then Exit Sub

            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colindex_t = GridView1.CurrentCell.ColumnIndex
            If e.KeyCode = Keys.Enter Then
                If Rowindex_t >= 0 And Colindex_t >= 0 Then
                    Call ItemStockLedger(Rowindex_t, Colindex_t)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
    End Sub

    Private Sub Txt_searchloc_GotFocus(sender As Object, e As EventArgs) Handles Txt_searchloc.GotFocus
        Txt_searchloc.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_searchloc_LostFocus(sender As Object, e As EventArgs) Handles Txt_searchloc.LostFocus
        Txt_searchloc.BackColor = Color.White
    End Sub

    Private Sub Txt_searchloc_TextChanged(sender As Object, e As EventArgs) Handles Txt_searchloc.TextChanged
        Dim indx As Integer = Chklst_loc.FindString(Txt_searchloc.Text)
        If indx >= 0 Then Chklst_loc.SelectedIndex = indx
    End Sub

    Private Sub opt_allloc_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allloc.CheckedChanged
        If opt_allloc.Checked = True Then
            Txt_searchloc.Enabled = False
            Txt_searchloc.Text = ""
            Chklst_loc.Enabled = False
        Else
            Chklst_loc.Enabled = True
            Txt_searchloc.Enabled = True
            Me.Chklst_loc.DataSource = ds_loc
            Chklst_loc.DataSource = bs_loc
            Chklst_loc.DisplayMember = "GODOWNNAME"
            Chklst_loc.ValueMember = "MASTERID"
        End If
    End Sub

    Private Sub Chklst_Grp_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_Grp.ItemCheck
        Try
            Dim valmember As String, processt1 As String, Groupwhrcond_t As String

            If Opt_Allgrp.Checked = True Then Exit Sub

            'SelectedParty_t = "0"
            'If Not SelectedFollowed_t Is Nothing Then
            '    If SelectedFollowed_t.Substring(SelectedFollowed_t.Length - 1, 1) <> "," Then
            '        SelectedFollowed_t = SelectedFollowed_t + ","
            '    End If
            'End If
            If SelectedGroup_t Is Nothing Then SelectedGroup_t = "00"

            If Chklst_Grp.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If SelectedGroup_t.Length > 1 Then
                        If SelectedGroup_t.IndexOf(Chklst_Grp.SelectedValue) = -1 Then
                            SelectedGroup_t = String.Concat(SelectedGroup_t, Chklst_Grp.SelectedValue, ",")
                        End If
                    Else
                        SelectedGroup_t = String.Concat(SelectedGroup_t, Chklst_Grp.SelectedValue, ",")
                    End If
                Else
                    SelectedGroup_t = SelectedGroup_t.Replace(Convert.ToString(Chklst_Grp.SelectedValue) + ",", "")
                End If
            End If

            If Chklst_Grp.SelectedItems.Count = 0 Then SelectedGroup_t = ""

            Dim levstr As String
            If SelectedGroup_t.Length > 1 Then

                If SelectedGroup_t.Substring(SelectedGroup_t.Length - 1, 1) = "," Then
                    processt1 = SelectedGroup_t.Substring(0, SelectedGroup_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

            'SelectedGroup_t = processt1

            ds_item = Nothing
            levstr = " select itemid,isnull(ITEMDES,'')  as ITEMDES from item_master where groupid in (" & processt1 & ") order by ITEMDES"
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

            Me.Chklst_Item.DataSource = Nothing
            Me.Chklst_Item.DataSource = ds_item
            Chklst_Item.DataSource = bs_item
            Chklst_Item.DisplayMember = "ITEMDES"
            Chklst_Item.ValueMember = "itemid"


            ds_code = Nothing
            levstr = " select itemid,isnull(itemcode,'')  as itemcode from item_master where groupid in (" & processt1 & ") and itemcode is not null and itemcode <> '' order by ITEMDES"
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
End Class