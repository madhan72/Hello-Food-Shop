Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports Reports_SUNBILLPLUS_App

Public Class Frm_salesregisterbillwiseRpt
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds, ds_party As New DataSet
    Dim bs, bs_party As New BindingSource
    Dim TamilFont_t As Integer
    Dim Headerid_t As Double
    Dim colindex_t As Integer, Filtercolnmae_t As String, Selectedparty_t As String

    Private Sub Execute()
        Try
            Dim levstr As String = ""

            ds_party = Nothing
            levstr = " Select PTYNAME, PTYCODE From PARTY Order By PTYNAME "
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
            Me.Chklst_party.DataSource = ds_party
            Chklst_party.DataSource = bs_party
            Chklst_party.DisplayMember = "PTYNAME"
            Chklst_party.ValueMember = "PTYCODE"

            opt_allparty.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid()
        Try
            Dim Colcnt_t As Integer, Rowcnt As Integer, Datatype_t As String

            If opt_allparty.Checked = True Then
                Selectedparty_t = "SELECT PTYCODE FROM PARTY"
            Else
                Selectedparty_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_party.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_party.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_party.ValueMember).ToString
                    Selectedparty_t = String.Concat(Selectedparty_t, valmember, ",")
                Next
                If Selectedparty_t.Length > 0 Then Selectedparty_t = Selectedparty_t.Substring(0, Selectedparty_t.Length - 1)
            End If

            If Selectedparty_t = "" Or Selectedparty_t Is Nothing Then Selectedparty_t = "000"

            ds = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SALESREGISTERBILLWISE_RPT"
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = Dtp_fromdate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DtpToDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@PARTYID", SqlDbType.VarChar).Value = Selectedparty_t
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
            Dim TYpe As String

            For i = 0 To Colcnt_t - 1
                Colname_t = GridView1.Columns(i).HeaderText

                If LCase(Colname_t).IndexOf("id") <> -1 Then GridView1.Columns(i).Visible = False
                Datatype_t = ds.Tables(0).Columns(i).DataType.ToString
                If Datatype_t = "System.Decimal" Then
                    GridView1.Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Next
            For i = 0 To GridView1.Rows.Count - 1
                '    For J = GridView1.Columns.Count - 4 To GridView1.Columns.Count - 1
                TYpe = IIf(IsDBNull(GridView1.Rows(i).Cells(GridView1.Columns.Count - 4).Value), "", (GridView1.Rows(i).Cells(GridView1.Columns.Count - 4).Value))

                If LCase(TYpe) = LCase("total") Then
                    GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightGray
                End If

                TYpe = IIf(IsDBNull(GridView1.Rows(i).Cells(GridView1.Columns.Count - 6).Value), "", (GridView1.Rows(i).Cells(GridView1.Columns.Count - 6).Value))

                If LCase(TYpe) = LCase("total") Then
                    GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                    GridView1.Rows(i).DefaultCellStyle.ForeColor = Color.DarkBlue
                End If

                '    Next
                '    TYpe = IIf(IsDBNull(GridView1.Rows(i).Cells(3).Value), "", (GridView1.Rows(i).Cells(3).Value))

                '    If LCase(TYpe) = LCase("total") Then
                '        GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                '        GridView1.Rows(i).DefaultCellStyle.ForeColor = Color.DarkBlue
                '    End If
            Next
            GridView1.AutoResizeColumns()
            GridView1.Focus()
            ' GridView1.Columns(GridView1.Columns.Count - 4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Selectedparty_t = ""

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
        Dtp_fromdate.Value = Today
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

                Headerid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(1).Value), 0, GridView1.Rows(Rowindex).Cells(1).Value)

                If Headerid_t <> 0 Then
                    Dim frmprclst = New frm_invoice
                    AllowFormEdit_t = True
                    frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    AllowFormEdit_t = False
                End If

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
        Dim Ex_Tot_pos As Integer = 0, Grndindex_t As Integer = 0, Headcol_t As Integer = 1
        Dim decformat_t As String = "", Griddisptype_t As String = ""

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

    Private Sub opt_allparty_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allparty.CheckedChanged
        If opt_allparty.Checked = True Then
            Txt_searchparty.Enabled = False
            Txt_searchparty.Text = ""
            Chklst_party.Enabled = False
        Else
            Chklst_party.Enabled = True
            Txt_searchparty.Enabled = True
            Me.Chklst_party.DataSource = ds_party
            Chklst_party.DataSource = bs_party
            Chklst_party.DisplayMember = "ptyname"
            Chklst_party.ValueMember = "ptycode"
        End If
    End Sub

    Private Sub Txt_searchparty_GotFocus(sender As Object, e As EventArgs) Handles Txt_searchparty.GotFocus
        Txt_searchparty.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_searchparty_LostFocus(sender As Object, e As EventArgs) Handles Txt_searchparty.LostFocus
        Txt_searchparty.BackColor = Color.White
    End Sub

    Private Sub Txt_searchparty_TextChanged(sender As Object, e As EventArgs) Handles Txt_searchparty.TextChanged
        Dim indx As Integer = Chklst_party.FindString(Txt_searchparty.Text)
        If indx >= 0 Then Chklst_party.SelectedIndex = indx
    End Sub

    Private Sub cmd_print_Click(sender As Object, e As EventArgs) Handles cmd_print.Click
        Try

            If opt_allparty.Checked = True Then
                Selectedparty_t = "SELECT PTYCODE FROM PARTY"
            Else
                Selectedparty_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.Chklst_party.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(Chklst_party.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(Chklst_party.ValueMember).ToString
                    Selectedparty_t = String.Concat(Selectedparty_t, valmember, ",")
                Next
                If Selectedparty_t.Length > 0 Then Selectedparty_t = Selectedparty_t.Substring(0, Selectedparty_t.Length - 1)
            End If

            If Selectedparty_t = "" Or Selectedparty_t Is Nothing Then Selectedparty_t = "000"


            Dim rpt As New Frm_Reports_Init
            Dim ProcessName As String, ToDate_t As String, Fromdate_t As String
            Cursor = Cursors.WaitCursor
            rpt.ShowInTaskbar = False
            rpt.Init(conn, "Invoice CheckList", Servername_t, 0, Dtp_fromdate.Value, DtpToDate.Value, "", Databasename_t, _
                     Gencompid, False, "", False, Selectedparty_t, "", "")
            rpt.StartPosition = FormStartPosition.CenterScreen
            rpt.ShowDialog()
            Cursor = Cursors.Default

            Selectedparty_t = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class