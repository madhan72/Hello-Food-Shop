Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports Reports_SUNBILLPLUS_App
Public Class Frm_Invregister
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds, ds_party, ds_group, ds_Line As New DataSet
    Dim bs, bs_party, bs_group, bs_Line As New BindingSource
    Dim colindex_t As Integer, Filtercolnmae_t As String, SelectedParty_t As String, SelectedGroup_t As String, SelectedLine_t As String

    Private Sub Execute()
        Try
            Dim levstr As String = ""

            ds_party = Nothing
            levstr = " SELECT PTYCODE,PTYNAME FROM PARTY WHERE PTYTYPE='CUSTOMER' ORDER BY PTYNAME"
            cmd = Nothing
            ds_party = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_party = New DataSet
            ds_party.Clear()
            da.Fill(ds_party)
            bs_party.DataSource = ds_party.Tables(0)
            Me.Chklst_Party.DataSource = ds_party
            Chklst_Party.DataSource = bs_party
            Chklst_Party.DisplayMember = "PTYNAME"
            Chklst_Party.ValueMember = "PTYCODE"

            ds_Line = Nothing
            levstr = " SELECT LINE,MASTERID FROM LINE_MASTER "
            cmd = Nothing
            ds_Line = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Line = New DataSet
            ds_Line.Clear()
            da.Fill(ds_Line)
            bs_Line.DataSource = ds_Line.Tables(0)
            Me.Chklst_line.DataSource = ds_Line
            Chklst_line.DataSource = bs_Line
            Chklst_line.DisplayMember = "LINE"
            Chklst_line.ValueMember = "MASTERID"

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
            Opt_AllParty.Checked = True
            opt_allLine.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

            If Opt_AllParty.Checked = True Then
                SelectedParty_t = "SELECT Ptycode from Party "
            Else
                SelectedParty_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_Party.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_Party.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_Party.ValueMember).ToString
                    SelectedParty_t = String.Concat(SelectedParty_t, valmember, ",")
                Next
                If SelectedParty_t.Length > 0 Then SelectedParty_t = SelectedParty_t.Substring(0, SelectedParty_t.Length - 1)
            End If

            If opt_allLine.Checked = True Then
                SelectedLine_t = "SELECT MASTERID FROM LINE_MASTER "
            Else
                SelectedLine_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_line.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_line.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_line.ValueMember).ToString
                    SelectedLine_t = String.Concat(SelectedLine_t, valmember, ",")
                Next
                If SelectedLine_t.Length > 0 Then SelectedLine_t = SelectedLine_t.Substring(0, SelectedLine_t.Length - 1)
            End If

            If SelectedLine_t = "" Or SelectedLine_t Is Nothing Then SelectedLine_t = "000"
            If SelectedParty_t = "" Or SelectedParty_t Is Nothing Then SelectedParty_t = "000"
            If SelectedGroup_t = "" Or SelectedGroup_t Is Nothing Then SelectedGroup_t = "000"

            ds = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "INVOICE_REG_GRID"
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = Dtp_Fromdate.Value.ToString("yyyy/MM/dd") ' hh:mm tt")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DtpToDate.Value.ToString("yyyy/MM/dd") ' hh:mm tt")
            cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@LINEID", SqlDbType.VarChar).Value = SelectedLine_t
            cmd.Parameters.Add("@PARTYID", SqlDbType.VarChar).Value = SelectedParty_t
            cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = SelectedGroup_t
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

            'If Rowcnt > 0 Then
            '    GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightSkyBlue
            '    GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.DarkBlue
            'End If

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

            GridView1.AutoResizeColumns()
            GridView1.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try
            If GridView1.Rows.Count = 0 Or GridView1.CurrentCell Is Nothing Then Exit Sub

            If txt_Search.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_Search.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Frm_Invregister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call Execute()
        Dtp_Fromdate.Value = Today
        DtpToDate.Value = Now
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click
        Cursor = Cursors.WaitCursor
        Call LoadGrid()
        Cursor = Cursors.Default
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles txt_Search.GotFocus
        txt_Search.BackColor = Color.Yellow
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles txt_Search.LostFocus
        txt_Search.BackColor = Color.White
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txt_Search.TextChanged
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
                        Case "locationid", "godownid"
                            Locationid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                        Case "location"
                            Location = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                        Case "item", "item name"
                            Itemname = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), 0, GridView1.Rows(Rowindex).Cells(i).Value)
                    End Select
                Next

                Dim ds_detl As New DataSet
                Dim cmd_detl As SqlCommand
                Dim da_detl As SqlDataAdapter

                If Itemid_t = 0 Or Locationid_t = 0 Then
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
                frmledger.init(Itemid_t, "Item : " + Itemname + "   Location : " + Location, Locationid_t)
                If ds_detl.Tables(0).Rows.Count > 0 Then
                    VCHDATE = ds_detl.Tables(0).Rows(0).Item("VCHDATE")
                    frmledger.DtpFromDate.Value = VCHDATE
                End If
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

    Private Sub Opt_AllParty_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_AllParty.CheckedChanged
        If Opt_AllParty.Checked = True Then
            txt_searchitem.Enabled = False
            txt_searchitem.Text = ""
            Chklst_Party.Enabled = False
        Else
            Chklst_Party.Enabled = True
            txt_searchitem.Enabled = True
            Me.Chklst_Party.DataSource = ds_party
            Chklst_Party.DataSource = bs_party
            Chklst_Party.DisplayMember = "PTYNAME"
            Chklst_Party.ValueMember = "PTYCODE"
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
        Dim indx As Integer = Chklst_Party.FindString(txt_searchitem.Text)
        If indx >= 0 Then Chklst_Party.SelectedIndex = indx
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            ' Call ItemStockLedger(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try

            Dim Rowindex_t As Double, Colindex_t As Double

            If GridView1.CurrentCell Is Nothing Then Exit Sub

            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colindex_t = GridView1.CurrentCell.ColumnIndex
            If Rowindex_t >= 0 And Colindex_t >= 0 Then
                If e.KeyCode = Keys.Enter Then
                    If Rowindex_t >= 0 And Colindex_t >= 0 Then
                        Call ItemStockLedger(Rowindex_t, Colindex_t)
                    End If
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
        Dim indx As Integer = Chklst_line.FindString(Txt_searchloc.Text)
        If indx >= 0 Then Chklst_line.SelectedIndex = indx
    End Sub

    Private Sub Opt_AllLine_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allLine.CheckedChanged
        If opt_allLine.Checked = True Then
            Txt_searchloc.Enabled = False
            Txt_searchloc.Text = ""
            Chklst_line.Enabled = False
        Else
            Chklst_line.Enabled = True
            Txt_searchloc.Enabled = True
            Me.Chklst_line.DataSource = ds_Line
            Chklst_line.DataSource = bs_Line
            Chklst_line.DisplayMember = "LINE"
            Chklst_line.ValueMember = "MASTERID"
        End If
    End Sub

    Private Sub Btn_Print_Click(sender As Object, e As EventArgs) Handles Btn_Print.Click
        Try
            Dim rpt As New Frm_Reports_Init
            Dim ProcessName As String, ToDate_t As String, Fromdate_t As String
            Cursor = Cursors.WaitCursor
            rpt.ShowInTaskbar = False
            rpt.Init(conn, "Invoice Reg", Servername_t, 0, Dtp_Fromdate.Value, DtpToDate.Value, "", "SUNBILLPLUS", _
                     Gencompid, False, "", False, SelectedParty_t, SelectedLine_t, SelectedGroup_t)
            rpt.StartPosition = FormStartPosition.CenterScreen
            rpt.ShowDialog()
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class