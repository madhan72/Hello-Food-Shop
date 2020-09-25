Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms

Public Class Frm_stockLedger
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim bs As New BindingSource
    Dim Headerid_t As Double, Itemid_t As String, Locationid_t As String, Colindex_t As Integer
    Public Databasename_t As String = "SUNBILLPLUS", Filtercolnmae_t As String
    Dim dr As SqlDataReader

    Public Sub init(ByVal Itemid As String, ByVal Itemname As String, ByVal Lcoationid As String)
        Itemid_t = Itemid
        Lbl_ItemName.Text = Itemname
        Locationid_t = Lcoationid
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

                'decformat_t = ".000"
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
                                '  decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
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

    Public Sub gridbind()
        Try
            Dim Rowcnt As Integer, j, Colcnt_t As Integer, Colname_t As String, Griddisptype_t As String
            Dim Typecol As Integer

            cmd = Nothing
            ds = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "ITEMSTOCK_LEDGER_RPT"
            cmd.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = (DtpFromDate.Value).ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = (DtpToDate.Value).ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemid_t
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationid_t
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "Table1")

            Rowcnt = ds.Tables(0).Rows.Count
            Colcnt_t = ds.Tables(0).Columns.Count

            If Rowcnt = 0 Then
                GridView1.DataSource = Nothing
                GridView1.Rows.Clear()
                Exit Sub
            End If

            If Rowcnt > 0 Then
                GridView1.DataSource = Nothing
                GridView1.Rows.Clear()
                GridView1.AllowUserToAddRows = False
                GridView1.AutoGenerateColumns = True

                bs = Nothing
                bs = New BindingSource
                Dim tables As DataTableCollection = ds.Tables
                Dim view1 As New DataView(tables(0))
                bs.DataSource = view1
                GridView1.DataSource = view1
                GridView1.Refresh()
                GridView1.ColumnHeadersHeight = 40
                GridView1.ReadOnly = True

                If Rowcnt = 0 Then Exit Sub
                j = 0

                For j = 0 To ds.Tables(0).Columns.Count - 1
                    Colname_t = ds.Tables(0).Columns(j).ColumnName
                    If LCase(Colname_t).IndexOf("type") <> -1 Then
                        Typecol = j
                    End If
                    If Colname_t.IndexOf("@") <> -1 Or LCase(Colname_t).IndexOf("id") <> -1 Then
                        GridView1.Columns(j).Visible = False
                    End If
                    GridView1.Columns(j).SortMode = False
                Next

                For j = 0 To GridView1.Columns.Count - 1
                    Griddisptype_t = Nothing
                    If Not GridView1.Columns(j).ValueType Is Nothing Then Griddisptype_t = IIf(IsDBNull(GridView1.Columns(j).ValueType.ToString), "", GridView1.Columns(j).ValueType.ToString)
                    If Griddisptype_t = "System.Decimal" Then
                        GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End If
                Next
            End If

            For i = 0 To GridView1.Rows.Count - 1
                If LCase(GridView1.Rows(i).Cells(Typecol).Value).ToString.IndexOf("opening") <> -1 Or LCase(GridView1.Rows(i).Cells(Typecol).Value).ToString.IndexOf("closing") <> -1 Then
                    GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightSkyBlue
                    GridView1.Rows(i).DefaultCellStyle.ForeColor = Color.DarkBlue
                End If
            Next
            GridView1.AutoResizeColumns()

            GridView1.Columns(GridView1.Columns.Count - 1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(GridView1.Columns.Count - 2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.Yellow
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = Color.White
    End Sub

    Private Sub TransactionForm(ByVal Rowindex As Integer, ByVal colindex As Integer)
        Dim Headerid_t As Double, Process_t As String
        Dim Type As String, headertext As String
        Dim i As Integer
        Dim ds_form As New DataSet
        Dim colindex_t As Integer

        If Rowindex >= 0 And colindex >= 0 Then
            Cursor = Cursors.WaitCursor
            Headerid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(0).Value), 0, (GridView1.Rows(Rowindex).Cells(0).Value))
            If Headerid_t = 0 Then Exit Sub
            For i = 0 To GridView1.Columns.Count - 1
                headertext = GridView1.Columns(i).HeaderText
                If LCase(headertext) = "type" Then
                    colindex_t = i
                    Exit For
                End If
            Next

            Type = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(i).Value), "", (GridView1.Rows(Rowindex).Cells(i).Value))
            Select Case LCase(Type)
                Case "pur", "purchase"
                    AllowFormEdit_t = True
                    Dim frmpur = New Frm_purchase
                    frmpur.Init("Edit", Headerid_t, "PURCHASE", "PUR")
                    frmpur.ShowInTaskbar = False
                    frmpur.StartPosition = FormStartPosition.CenterScreen
                    'UserPermission(frmpur)
                    frmpur.ShowDialog()
                    AllowFormEdit_t = False
                Case "sal", "sales", "sales return", "salret"
                    Dim frmsalret = New frm_salesreturn
                    AllowFormEdit_t = True
                    frmsalret.Init("Edit", Headerid_t, "SALES RETURN", "SALRET")
                    frmsalret.ShowInTaskbar = False
                    frmsalret.StartPosition = FormStartPosition.CenterScreen
                    frmsalret.ShowDialog()
                    'UserPermission(frmsalret)
                    AllowFormEdit_t = False
                Case "purret", "purchase return"
                    Dim frmsalret = New Frm_purchasereturn
                    AllowFormEdit_t = True
                    frmsalret.Init("Edit", Headerid_t, "PURCHASE RETURN", "PURRET")
                    frmsalret.ShowInTaskbar = False
                    frmsalret.StartPosition = FormStartPosition.CenterScreen
                    'UserPermission(frmsalret)
                    frmsalret.ShowDialog()
                    AllowFormEdit_t = False
                Case "invoice", "inv"
                    AllowFormEdit_t = True
                    If MDIParent1.AgencyBillFormat_t = False Then
                        Dim frmsalret = New frm_invoice
                        frmsalret.Init("Edit", Headerid_t, "INVOICE", "INV")
                        frmsalret.ShowInTaskbar = False
                        frmsalret.StartPosition = FormStartPosition.CenterScreen
                        frmsalret.ShowDialog()
                    Else
                        Dim frmprclst = New frm_Agencybill_Invoice
                        frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                        frmprclst.ShowInTaskbar = False
                        frmprclst.StartPosition = FormStartPosition.CenterScreen
                        frmprclst.ShowDialog()
                    End If
                    AllowFormEdit_t = False

                Case "retail"
                    Dim frmprclst = New Frm_RetailBill
                    frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                Case "quotation", "quo"
                    AllowFormEdit_t = True
                    If MDIParent1.ESTIMATE_FORMAT2 = True Then
                        Dim frmprclst = New Frm_quotationformat2
                        frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                        frmprclst.ShowInTaskbar = False
                        frmprclst.StartPosition = FormStartPosition.CenterScreen
                        frmprclst.ShowDialog()
                    Else
                        Dim frmsalret = New frm_quotation
                        frmsalret.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                        frmsalret.ShowInTaskbar = False
                        frmsalret.StartPosition = FormStartPosition.CenterScreen
                        'UserPermission(frmsalret)
                        frmsalret.ShowDialog()
                    End If
                    AllowFormEdit_t = False
                Case "item add", "item deduct"
                    Dim frmsalret = New Frm_ItemAddless
                    AllowFormEdit_t = True
                    frmsalret.Init("Edit", Headerid_t, "ITEMADDLESS", "IAL")
                    frmsalret.ShowInTaskbar = False
                    frmsalret.StartPosition = FormStartPosition.CenterScreen
                    ' UserPermission(frmsalret)
                    frmsalret.ShowDialog()
                    AllowFormEdit_t = False
            End Select

            GridView1.Rows(Rowindex).Selected = True
            GridView1.CurrentCell = GridView1.Rows(Rowindex).Cells(2)
            Headerid_t = 0

            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub frmitemmovementanalysisledger_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' DtpFromDate.Value = Today
            ' DtpToDate.Value = Today
            Call gridbind()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click
        Call gridbind()
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Hide()
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

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   New Font(GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold), _
                                   b, _
                                   e.RowBounds.Location.X + 10, _
                                   e.RowBounds.Location.Y + 4) '15
        End Using
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(Colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Filterby()
    End Sub

    Private Sub DtpFromDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpFromDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            DtpToDate.Focus()
        End If
    End Sub

    Private Sub DtpToDate_KeyDown(sender As Object, e As KeyEventArgs) Handles DtpToDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_load.Focus()
        End If
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Call TransactionForm(e.RowIndex, e.ColumnIndex)
        End If
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Dim Rowindex_t As Integer, Colindex_t As Integer
        If e.KeyCode = Keys.Enter Then
            If GridView1.CurrentCell Is Nothing Then Exit Sub
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colindex_t = GridView1.CurrentCell.ColumnIndex
            If Rowindex_t >= 0 And Colindex_t >= 0 Then
                Call TransactionForm(Rowindex_t, Colindex_t)
            End If
        End If
    End Sub

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class