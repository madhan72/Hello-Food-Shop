Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports Reports_SUNBILLPLUS_App

Public Class frm_companyoutstanding
    Dim cmd As SqlCommand
    Dim rm As New Frm_Reports_Init
    Dim da As SqlDataAdapter
    Dim ds, ds_party, ds_line As New DataSet
    Dim bs, bs_party, bs_line As New BindingSource
    Dim colindex_t As Integer, Filtercolname_t As String, SelectedParty_t As String, SelectedLine_t As String

    Private Sub Execute()
        Try
            Dim levstr As String = ""

            ds_party = Nothing
            levstr = " SELECT PTYNAME,PTYCODE FROM PARTY ORDER BY PTYNAME "
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

            ds_line = Nothing
            levstr = "SELECT LINE,MASTERID FROM LINE_MASTER ORDER BY LINE "
            cmd = Nothing
            ds_line = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_line = New DataSet
            ds_line.Clear()
            da.Fill(ds_line)
            bs_line.DataSource = ds_line.Tables(0)
            Me.Chklst_line.DataSource = ds_line
            Chklst_line.DataSource = bs_line
            Chklst_line.DisplayMember = "LINE"
            Chklst_line.ValueMember = "MASTERID"

            Opt_AllLine.Checked = True
            Opt_AllParty.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            Dim Colcnt_t As Integer, Rowcnt As Integer, Datatype_t As String

            If Opt_AllLine.Checked = True Then
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

            If Opt_AllParty.Checked = True Then
                SelectedParty_t = "SELECT PTYCODE FROM PARTY "
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

            If SelectedLine_t = "" Or SelectedLine_t Is Nothing Then SelectedLine_t = "00"
            If SelectedParty_t = "" Or SelectedParty_t Is Nothing Then SelectedParty_t = "00"

            ds = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "COMPANY_OUTSTANDING"
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = Dtp_ToDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = Dtp_fromdate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
            cmd.Parameters.Add("@LINEID", SqlDbType.VarChar).Value = SelectedLine_t
            cmd.Parameters.Add("@PARTY", SqlDbType.VarChar).Value = SelectedParty_t
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

            Dim font As New Font( _
            GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 30

            Dim Colname_t As String

            For i = 0 To Colcnt_t - 1
                Colname_t = GridView1.Columns(i).HeaderText

                If LCase(Colname_t).IndexOf("id") <> -1 Or LCase(Colname_t).IndexOf(LCase("code")) <> -1 Or LCase(Colname_t).IndexOf(LCase("compname")) <> -1 Then GridView1.Columns(i).Visible = False
                'GridView1.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                Datatype_t = ds.Tables(0).Columns(i).DataType.ToString
                If Datatype_t = "System.Decimal" Then
                    GridView1.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    'GridView1.Columns(i).DefaultCellStyle.Format = "#"
                End If
            Next

            GridView1.Columns(GridView1.Columns.Count - 2).HeaderText = ""

            GridView1.AutoResizeColumns()
            GridView1.Columns(GridView1.Columns.Count - 2).Width = 40
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Filterby()
        Try
            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolname_t <> "" Then
                bs.Filter = "convert([" & Filtercolname_t & "],'System.String') LIKE '%" & TextBox1.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frm_companyoutstanding(sender As Object, e As EventArgs) Handles Me.Load
        Call Execute()
        Dtp_fromdate.Value = Today
        Dtp_ToDate.Value = Today
        Cbo_type.SelectedIndex = 0
        Me.Text = "Outstanding"
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
                Filtercolname_t = GridView1.Columns(colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DtpToDate_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_ToDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_load.Focus()
        End If
    End Sub

    Private Sub Opt_Allgrp_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_AllLine.CheckedChanged
        If Opt_AllLine.Checked = True Then
            txt_searchline.Enabled = False
            txt_searchline.Text = ""
            Chklst_line.Enabled = False
            SelectedLine_t = ""
        Else
            SelectedLine_t = ""
            Chklst_line.Enabled = True
            txt_searchline.Enabled = True
            Me.Chklst_line.DataSource = ds_line
            Chklst_line.DataSource = bs_line
            Chklst_line.DisplayMember = "LINE"
            Chklst_line.ValueMember = "MASTERID"
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

    Private Sub Opt_AllItem_CheckedChanged(sender As Object, e As EventArgs) Handles Opt_AllParty.CheckedChanged
        If Opt_AllParty.Checked = True Then
            txt_searchparty.Enabled = False
            txt_searchparty.Text = ""
            Chklst_Party.Enabled = False

            Dim levstr As String = ""
            ds_party = Nothing

            If SelectedLine_t = "" Or SelectedLine_t Is Nothing Then SelectedLine_t = "00"
            If Opt_AllLine.Checked = True Then
                levstr = "SELECT ptyname,ptycode FROM party ORDER BY ptyname "
            Else
                levstr = "SELECT ptyname,ptycode FROM party where lineid in (" & SelectedLine_t & ") ORDER BY ptyname "
            End If

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
            Chklst_Party.DisplayMember = "ptyname"
            Chklst_Party.ValueMember = "ptycode"

        Else
            Chklst_Party.Enabled = True
            txt_searchparty.Enabled = True
            Me.Chklst_Party.DataSource = ds_party
            Chklst_Party.DataSource = bs_party
            Chklst_Party.DisplayMember = "PTYNAME"
            Chklst_Party.ValueMember = "PTYCODE"
        End If
    End Sub

    Private Sub txt_searchgrp_GotFocus(sender As Object, e As EventArgs) Handles txt_searchline.GotFocus
        txt_searchline.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchgrp_LostFocus(sender As Object, e As EventArgs) Handles txt_searchline.LostFocus
        txt_searchline.BackColor = Color.White
    End Sub

    Private Sub txt_searchgrp_TextChanged(sender As Object, e As EventArgs) Handles txt_searchline.TextChanged
        Dim indx As Integer = Chklst_line.FindString(txt_searchline.Text)
        If indx >= 0 Then Chklst_line.SelectedIndex = indx
    End Sub

    Private Sub txt_searchitem_GotFocus(sender As Object, e As EventArgs) Handles txt_searchparty.GotFocus
        txt_searchparty.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchitem_LostFocus(sender As Object, e As EventArgs) Handles txt_searchparty.LostFocus
        txt_searchparty.BackColor = Color.White
    End Sub

    Private Sub txt_searchitem_TextChanged(sender As Object, e As EventArgs) Handles txt_searchparty.TextChanged
        Dim indx As Integer = Chklst_Party.FindString(txt_searchparty.Text)
        If indx >= 0 Then Chklst_Party.SelectedIndex = indx
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Try
            AC_Ledger(e.RowIndex, e.ColumnIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AC_Ledger(ByVal rowindex As Integer, ByVal colindex As Integer)
        Try
            Dim Party_t As String
            Dim colname_t As String, Company_t As String, Compname As String
            Dim cnt As Integer
            Dim vchdate As DateTime
            Dim Partyid_t As Double, Compid_t As Double

            Partyid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(1).Value), 0, (GridView1.Rows(rowindex).Cells(1).Value))
            For i = 0 To GridView1.Columns.Count - 1
                colname_t = GridView1.Columns(i).HeaderText
                Select Case colname_t
                    Case "Party", "PARTY"
                        Party_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(i).Value), 0, (GridView1.Rows(rowindex).Cells(i).Value))
                End Select
            Next

            ' If Party_t = "" Then Exit Sub

            Cursor = Cursors.WaitCursor

            frmAcLedgerRpt.DtpToDate.Value = Dtp_ToDate.Value
            frmAcLedgerRpt.DtpFromDate.Value = Dtp_fromdate.Value
            frmAcLedgerRpt.Get_Partyid(Partyid_t, Gencompid)
            frmAcLedgerRpt.FontColor = False
            frmAcLedgerRpt.Lbl_ItemName.Text = "Account : " & Party_t
            frmAcLedgerRpt.Lbl_company.Text = "Company : " & Gencompname
            frmAcLedgerRpt.GridView1.DataSource = Nothing
            frmAcLedgerRpt.GridView1.Refresh()
            frmAcLedgerRpt.StartPosition = FormStartPosition.CenterScreen
            frmAcLedgerRpt.ShowInTaskbar = False
            frmAcLedgerRpt.ShowDialog()

            Cursor = Cursors.Default

            Partyid_t = 0
            Party_t = ""
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
         Dim Rowindex_t1 As Integer, Colindex_t1 As Integer
        If e.KeyCode = Keys.Enter Then
            Rowindex_t1 = GridView1.CurrentCell.RowIndex
            Colindex_t1 = GridView1.CurrentCell.ColumnIndex
            AC_Ledger(Rowindex_t1, Colindex_t1)
        End If
    End Sub

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
    End Sub

    Private Sub Dtp_fromdate_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_fromdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dtp_ToDate.Focus()
        End If
    End Sub

    Private Sub Chklst_line_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_line.ItemCheck
        Try
            Dim Level As Integer
            Dim Lev As String
            If SelectedLine_t <> "" Then
                SelectedLine_t = String.Concat(SelectedLine_t, "", ",")
            End If
            If Opt_AllLine.Checked = True Then Exit Sub

            For i = 0 To Chklst_line.SelectedItems.Count - 1
                Dim DRV As DataRowView = CType(Chklst_line.SelectedItems(0), DataRowView)
                Dim DR As DataRow = DRV.Row
                Dim DisplayMember As String = DR(Chklst_line.DisplayMember).ToString()
                Level = DR(Chklst_line.ValueMember)
                Lev = Level

                If e.NewValue = CheckState.Unchecked Then
                    SelectedLine_t = SelectedLine_t.Replace(Lev & ",", "")
                Else 
                    SelectedLine_t = String.Concat(SelectedLine_t, Level, ",")
                End If

                If SelectedLine_t = "" Then
                    SelectedLine_t = String.Concat(SelectedLine_t, "0", ",")
                End If
            Next
        
            SelectedLine_t = SelectedLine_t.Substring(0, SelectedLine_t.Length - 1)

            Dim levstr As String = ""
            ds_party = Nothing
            levstr = "SELECT ptyname,ptycode FROM party  where lineid in (" & SelectedLine_t & ") ORDER BY ptyname "
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
            Chklst_Party.DisplayMember = "ptyname"
            Chklst_Party.ValueMember = "ptycode"

            Opt_AllParty.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cmd_print_Click(sender As Object, e As EventArgs) Handles Cmd_print.Click

        If Cbo_type.SelectedIndex = 0 Then
            rm.Init(conn, "outstanding2", Servername_t, Nothing, Dtp_fromdate.Value, Dtp_ToDate.Value, "", Databasename_t, Gencompid, False, "", Nothing, SelectedParty_t, SelectedLine_t)
        ElseIf Cbo_type.SelectedIndex = 1 Then
            rm.Init(conn, "outstanding", Servername_t, Nothing, Dtp_fromdate.Value, Dtp_ToDate.Value, "", Databasename_t, Gencompid, False, "", Nothing, SelectedParty_t, SelectedLine_t)
        Else
            rm.Init(conn, "outstanding3", Servername_t, Nothing, Dtp_fromdate.Value, Dtp_ToDate.Value, "", Databasename_t, Gencompid, False, "", Nothing, SelectedParty_t, SelectedLine_t)
        End If
        'Dim tmpvchstng As String
        'Dim Tmplen As Double
        Cursor = Cursors.WaitCursor
        'Rm.ShowInTaskbar = False 'CALL OUTSIDE APPLICATION(REPORTS_APP)
        'Call GetVchnum(Headerid_v)
        'Call Getautoflds()
        'Tmplen = Suffix_t.Length
        'tmpvchstng = Vchnum_t.Remove(Vchnum_t.Length - Suffix_t.Length)
        'tmpvchstng = Vchnum_t.Remove(tmpvchstng.Length - Noofdigit_t)
        'Call CheckRptname(tmpvchstng)

        rm.StartPosition = FormStartPosition.CenterScreen
        rm.ShowDialog()
        Cursor = Cursors.Default
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

    Private Sub Cbo_type_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cbo_type.KeyPress
        e.Handled = True
    End Sub
End Class