Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports FindForm_App
Imports SingleMaster_App
Imports Accounts_App
Imports Microsoft.Win32

Public Class frmAcLedgerRpt

    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim bs As New BindingSource
    Dim Detlcnt_t As Integer, Rowcnt As Integer, Colcnt_t As Integer, colindex_t As Integer
    Dim sqlstr As String
    Dim Partyid_t As Double, Compid_t As Double
    Dim ds_comp As New DataSet
    Dim da_comp As SqlDataAdapter
    Public FontColor As Boolean
    Dim bs_comp As New BindingSource
    Dim FromDate_t As DateTime, ToDate_t As DateTime
    Dim Filtercolnmae_t As String, Colname_t As String
    Dim CompName_t As String
    Public Databasename_t As String = "SUNBILLPLUS"
    Dim dr As SqlDataReader

    Public Sub Get_Partyid(ByVal Partyid As Double, ByVal Compid As Double)
        Partyid_t = Partyid
        Compid_t = Compid
    End Sub

    Private Sub gridbind()
        Try
            Dim Colcnt_t As Integer, Rowcnt As Integer
            Dim cnt As Integer = 0
            Dim Griddisptype_t As String

            If Partyid_t = 0 Then Exit Sub

            FontColor = False

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()

            ds_comp = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = (DtpFromDate.Value).ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = (DtpToDate.Value).ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Compid_t
            cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_t
            cmd.Parameters.Add("@showdetails", SqlDbType.Float).Value = IIf(chk_details.Checked = True, 1, 0)
            cmd.CommandText = "AC_LEDGER_RPT"
            da_comp = New SqlDataAdapter(cmd)
            ds_comp = New DataSet
            ds_comp.Clear()
            da_comp.Fill(ds_comp, "Table1")

            Rowcnt = ds_comp.Tables(0).Rows.Count
            Colcnt_t = ds_comp.Tables(0).Columns.Count

            If Rowcnt = 0 Then Exit Sub

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False
            GridView1.AutoGenerateColumns = True

            Dim tables As DataTableCollection = ds_comp.Tables
            Dim view1 As New DataView(tables(0))
            bs_comp.DataSource = view1
            GridView1.DataSource = view1
            GridView1.Refresh()

            GridView1.ReadOnly = True

            For j = 0 To ds_comp.Tables(0).Columns.Count - 1
                Colname_t = ds_comp.Tables(0).Columns(j).ColumnName

                If Colname_t.IndexOf("@") <> -1 Then
                    GridView1.Columns(j).Visible = False
                End If
                'GridView1.AutoResizeColumn(j)
                GridView1.Columns(j).SortMode = False
            Next

            For j = 0 To GridView1.Columns.Count - 1
                If Not GridView1.Columns(j).ValueType Is Nothing Then Griddisptype_t = IIf(IsDBNull(GridView1.Columns(j).ValueType.ToString), "", GridView1.Columns(j).ValueType.ToString)
                If Griddisptype_t = "System.Decimal" Then
                    GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
                ' GridView1.Columns(j).DefaultCellStyle.Font = New Font("Calibri", 9.25, FontStyle.Bold)
            Next

            Dim TotDr_t As Double, TotCr_t As Double, ClosingDr_t As Double, ClosingCr_t As Double

            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 2).Value = Tot_Calc1(GridView1, GridView1.Columns.Count - 2)
            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 1).Value = Tot_Calc1(GridView1, GridView1.Columns.Count - 1)

            Call Grid_Color()

            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 2).Style.Format = "#0.00"
            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 2).Style.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 1).Style.Format = "#0.00"
            GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 1).Style.Alignment = DataGridViewContentAlignment.MiddleRight

            GridView1.AutoResizeColumns()

            Dim GetWidth1 As Integer, GetWidth2 As Integer

            GetWidth1 = GridView1.Columns(GridView1.Columns.Count - 1).Width
            GetWidth2 = GridView1.Columns(GridView1.Columns.Count - 2).Width

            If GetWidth1 > GetWidth2 Then
                GridView1.Columns(GridView1.Columns.Count - 2).Width = GetWidth1
            Else
                GridView1.Columns(GridView1.Columns.Count - 1).Width = GetWidth2
            End If

            '        Dim font1 As Font

            '        Dim ds_font As New DataSet
            '        Dim da_font As SqlDataAdapter
            '        Dim cnt1 As Integer

            '        sqlstr = "SELECT ISNULL(NUMERICVALUE,0) AS SIZE,ISNULL(STRINGVALUE,'') as fONTNAME,isnull(reference,'') as Style FROM SETTINGS WHERE PROCESS='FONT STYLE' "
            '        cmd = New SqlCommand(sqlstr, conn)
            '        cmd.CommandType = CommandType.Text
            '        da_font = New SqlDataAdapter(cmd)
            '        ds_font = New DataSet
            '        ds_font.Clear()
            '        da_font.Fill(ds_font)
            '        cnt1 = ds_font.Tables(0).Rows.Count

            '        If cnt1 > 0 Then
            '            Font_m = ds_font.Tables(0).Rows(0).Item("FONTNAME").ToString
            '            Size_m = ds_font.Tables(0).Rows(0).Item("SIZE").ToString
            '            FontStyle_m = ds_font.Tables(0).Rows(0).Item("Style").ToString
            '        End If

            '        If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            '        Dim converter As System.ComponentModel.TypeConverter = _
            'System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            '        font1 = _
            ' CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            '        font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))

            '        Lbl_ItemName.Font = font1
        Catch ex As Exception
            If ex.Message <> "Cannot find table 0." Then
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub


    Public Function Tot_Calc1(ByVal DGV As DataGridView, ByVal ColNo As Integer, Optional ByVal Rowcnt As Integer = 0) As Double
        Dim i As Integer, Rowcnt_t As Integer
        Dim TotValue As Double
        TotValue = 0

        If Rowcnt = 0 Then
            Rowcnt_t = DGV.Rows.Count
        Else
            Rowcnt_t = Rowcnt
        End If

        For i = 0 To Rowcnt_t - 3
            TotValue = TotValue + IIf(IsDBNull(DGV.Item(ColNo, i).Value), 0, DGV.Item(ColNo, i).Value)
        Next i

        Tot_Calc1 = TotValue
    End Function

    Public Function Tot_Calc(ByVal DGV As DataGridView, ByVal ColNo As Integer, Optional ByVal Rowcnt As Integer = 0) As Double
        Dim i As Integer, Rowcnt_t As Integer
        Dim TotValue As Double
        TotValue = 0
        If Rowcnt = 0 Then
            Rowcnt_t = DGV.Rows.Count
        Else
            Rowcnt_t = Rowcnt
        End If

        For i = 0 To Rowcnt_t - 3
            TotValue = TotValue + IIf(IsDBNull(DGV.Item(ColNo, i).Value), 0, DGV.Item(ColNo, i).Value)
        Next i
        Tot_Calc = TotValue
    End Function

    Private Sub ExportToExcel()

        Dim ds_comp As New DataSet
        Dim da_comp As SqlDataAdapter
        Dim Compname As String, Colname As String
        Dim Ex_Tot_pos As Integer = 0, Grndindex_t As Integer = 0, Headcol_t As Integer = 1
        Dim decformat_t As String = "", Griddisptype_t As String = "", chkvalue_t As String
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
                Dim q1 As Integer
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

                'cmd = Nothing
                'ds_comp = Nothing
                'cmd = New SqlCommand
                'cmd.Connection = conn
                'cmd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                'cmd.CommandText = "GET_COMP_ADDRESS"
                'da_comp = New SqlDataAdapter(cmd)
                'ds_comp = New DataSet
                'ds_comp.Clear()
                'da_comp.Fill(ds_comp, "Table1")

                'Dim cnt As Integer
                'cnt = ds_comp.Tables(0).Rows.Count

                'If cnt > 0 Then
                Compname = Gencompname
                '  End If

                Dim str As String = ChrW(64 + 1) & jj & ":" & ChrW(64 + iCountCol) & jj

                oRng = xlWorkSheet.Range(str)

                With oRng
                    .MergeCells = True
                    .Value = Compname & vbCrLf & " Account Ledger "
                    .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                    .Font.Color = Color.DarkBlue
                End With

                decformat_t = ".00"
                Dim decval_t As Double

                For i = 0 To GridView1.RowCount - 1
                    Q = 1
                    For j = 0 To GridView1.ColumnCount - 1
                        P = 1

                        If GridView1.Columns(j).Visible = True Then
                            xlWorkSheet.Cells(i + 3, Q) = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
                            xlWorkSheet.Cells(i + 3, Q).font.size = 12

                            chkvalue_t = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
                            Colname = GridView1.Columns(j).HeaderText

                            q1 = Q

                            If UCase(chkvalue_t) = "CLOSING BALANCE" Or UCase(chkvalue_t) = "OPENING BALANCE" Or UCase(chkvalue_t) = "TOTAL" Then
                                For q1 = 1 To GridView1.ColumnCount
                                    xlWorkSheet.Cells(i + 3, q1).Font.FontStyle = "Bold"
                                    xlWorkSheet.Cells(i + 3, q1).Font.color = Color.DarkBlue
                                    xlWorkSheet.Cells(2, q1).Font.FontStyle = "Bold"
                                    xlWorkSheet.Cells(2, q1).Font.color = Color.DarkBlue
                                Next
                            End If

                            xlWorkSheet.Cells(i + 3, Q).borders.LineStyle = 1
                            Q = Q + 1

                            Griddisptype_t = IIf(CStr(GridView1.Columns(j).ValueType.ToString) Is Nothing, "", GridView1.Columns(j).ValueType.ToString)

                            If Griddisptype_t = "System.Decimal" Then
                                xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" + decformat_t
                                If IsNumeric(GridView1.Item(j, i).Value) = True Then
                                    decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
                                End If
                            End If
                        End If
                    Next j
                Next i

                xlWorkSheet.Columns.AutoFit()
                xlWorkSheet.Rows("1:1").Font.FontStyle = "Bold"
                xlWorkSheet.Rows("1:1").Font.Size = 14
                xlWorkSheet.Rows("1:1").rowheight = 40
                xlWorkSheet.Rows("2:2").Font.FontStyle = "Bold"
                xlWorkSheet.Rows("2:2").Font.Size = 13
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

    Public Sub Grid_Color()
        Dim Vchno As String, PreVchno As String
        Dim Colname As String
        Dim color As New Color
        Dim Colindex As Integer, j As Integer
        Dim Acheaderid_t As Double
        Dim k As Integer
        Try
            Colindex = 3
            k = 0

            If FontColor = False Then
                For i = 0 To GridView1.Rows.Count - 1
                    Vchno = IIf(IsDBNull(GridView1.Rows(i).Cells(3).Value), "", (GridView1.Rows(i).Cells(3).Value))

                    If Vchno = "" Then
                        GridView1.Rows(i).Cells(GridView1.Columns.Count - 3).Style.Font = New Font("calibri", 8, FontStyle.Bold)
                    End If

                Next
                FontColor = True
            End If

            'For i = 0 To GridView1.Rows.Count - 1
            '    Acheaderid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(0).Value), 0, (GridView1.Rows(i).Cells(0).Value))
            '    If Acheaderid_t <> 0 Then
            '        Vchno = IIf(IsDBNull(GridView1.Rows(i).Cells(Colindex).Value), "", (GridView1.Rows(i).Cells(Colindex).Value))
            '        If k Mod 2 = 1 Then
            '            If Vchno <> "" Then GridView1.Rows(i).DefaultCellStyle.BackColor = Drawing.Color.LightGray
            '        Else
            '            If Vchno <> "" Then GridView1.Rows(i).DefaultCellStyle.BackColor = Drawing.Color.LightCyan
            '        End If
            '        color = GridView1.Rows(i).DefaultCellStyle.BackColor
            '        For j = 0 To GridView1.Rows.Count - 1
            '            PreVchno = IIf(IsDBNull(GridView1.Rows(j).Cells(Colindex).Value), "", (GridView1.Rows(j).Cells(Colindex).Value))

            '            If Vchno <> PreVchno And i <> j And PreVchno = "" Then
            '                GridView1.Rows(j).DefaultCellStyle.BackColor = color
            '                Exit For
            '            End If
            '            k = k + 1
            '        Next


            '    End If

            'Next

            'For i = 0 To GridView1.Rows.Count - 1
            '    Vchno = IIf(IsDBNull(GridView1.Rows(i).Cells(Colindex).Value), "", (GridView1.Rows(i).Cells(Colindex).Value))

            '    If Vchno = "" Then
            '        GridView1.Rows(i).DefaultCellStyle.Font = New Font("Calibri", 7, FontStyle.Bold)
            '    End If

            'Next

            For i = 0 To GridView1.Rows.Count - 1 Step 2
                Acheaderid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(0).Value), 0, (GridView1.Rows(i).Cells(0).Value))
                If Acheaderid_t <> 0 Then
                    GridView1.Rows(i).DefaultCellStyle.BackColor = Drawing.Color.LightCyan
                End If

                If i + 1 < GridView1.Rows.Count - 1 Then
                    Acheaderid_t = IIf(IsDBNull(GridView1.Rows(i + 1).Cells(0).Value), 0, (GridView1.Rows(i + 1).Cells(0).Value))
                    If Acheaderid_t <> 0 Then
                        GridView1.Rows(i + 1).DefaultCellStyle.BackColor = Drawing.Color.Wheat
                    End If
                End If

            Next

            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Visible = False

            If GridView1.Rows.Count - 1 > 1 Then GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = color.Pink
            If GridView1.Rows.Count - 1 > 2 Then GridView1.Rows(GridView1.Rows.Count - 2).DefaultCellStyle.BackColor = Drawing.Color.SkyBlue
            If IIf(IsDBNull(GridView1.Rows(0).Cells(0).Value), 0, GridView1.Rows(0).Cells(0).Value) <= 0 Then
                GridView1.Rows(0).DefaultCellStyle.BackColor = color.Pink
            End If
            'GridView1.Rows(GridView1.Rows.Count - 2).Cells(GridView1.Columns.Count - 2).Value = Tot_Calc1(GridView1, GridView1.Columns.Count - 2)
            GridView1.AutoResizeColumns()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click
        Call gridbind()
    End Sub

    Private Sub frmAcLedgerRpt_Load(sender As Object, e As EventArgs) Handles Me.Load
        chk_details.Checked = False
        Call gridbind()
    End Sub

    Private Sub DtpFromDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpFromDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            DtpToDate.Focus()
        End If
    End Sub

    Private Sub Account_Details(ByVal rowindex As Integer, ByVal Colindex As Integer)
        Try
            Dim Headerid_t As Double

            Headerid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(0).Value), 0, (GridView1.Rows(rowindex).Cells(0).Value))
            Dim Acent As New Sun_FrmAcctsentry_Compwise

            If Headerid_t <> 0 Then
                Cursor = Cursors.WaitCursor

                Acent.Init(conn, "EDIT", Headerid_t, "ACCOUNTS-TRANSACTION", "", Genuid, Gencompid, False, True)
                Acent.ShowInTaskbar = False
                Acent.StartPosition = FormStartPosition.CenterScreen
                UserPermission(Acent)
                Acent.ShowDialog()

                Call gridbind()

                GridView1.Rows(rowindex).Selected = True
                If GridView1.Rows(rowindex).Cells(2).Visible = True Then GridView1.CurrentCell = GridView1.Rows(rowindex).Cells(2)

                Cursor = Cursors.Default

                Headerid_t = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try

            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs_comp.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & TextBox1.Text & "%'"
            Else
                bs_comp.Filter = String.Empty
            End If

            GridView1.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DtpToDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpToDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_load.Focus()
        End If
    End Sub

    Private Sub Btn_load_KeyDown(sender As Object, e As KeyEventArgs) Handles Btn_load.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView1.Focus()
        End If
    End Sub

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
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

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Account_Details(e.RowIndex, e.ColumnIndex)
    End Sub

    Private Sub GridView1_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles GridView1.CellPainting
        Dim y As Integer

        For y = 0 To GridView1.Rows.Count - 1
            If (e.ColumnIndex = 0 And e.RowIndex = y) Then
                If Not IsNumeric(GridView1(0, y).Value) Then   ' for instance i wanted to make something like band end merged with the next cell(s.t.h. like header band )
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.Single
                End If
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.Single
            End If

            If GridView1.Columns(GridView1.Columns.Count - 3).HeaderText <> "" Then GridView1.Columns(GridView1.Columns.Count - 3).HeaderText = ""

            If (e.ColumnIndex = GridView1.Columns.Count - 4 And e.RowIndex = y) Then
                If y >= 0 Then
                    e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None
                End If
            End If
        Next
    End Sub

    Private Sub GridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridView1.ColumnHeaderMouseClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim Rowindex As Integer
            Dim Colindex As Integer

            If GridView1.CurrentCell Is Nothing Then Exit Sub

            Rowindex = GridView1.CurrentCell.RowIndex
            Colindex = GridView1.CurrentCell.ColumnIndex

            Account_Details(Rowindex, Colindex)
        End If
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
        Me.Close()
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        'Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
        '    e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
        '                             Nothing, _
        '                           b, _
        '                           e.RowBounds.Location.X + 10, _
        '                           e.RowBounds.Location.Y + 4)

        '    ' GridView1.Columns(GridView1.Columns.Count - 3).DefaultCellStyle.Font = New Font("calibri", 8, FontStyle.Bold)

        'End Using
    End Sub

    Private Sub GridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles GridView1.RowsAdded
        Grid_Color()
    End Sub
End Class