Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Windows.Forms
Imports FindForm_App
Imports SingleMaster_App
Imports Microsoft.Win32

Public Class Frm_SalesAnalysisDetail
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim bs As New BindingSource
    Dim Headerid_t As Double
    Public Month_v As String, Selecteditemid_v As Double, Type_v As Integer, SelectedLocationid_v As Double, Days_t As String
    Dim Detlcnt_t As Integer, Rowcnt As Integer, Colcnt_t As Integer, colindex_t As Integer
    Dim Filtercolnmae_t As String, Colname_t As String
    Public Databasename_t As String = "SUNBILLPLUS"

    Dim dr As SqlDataReader
    Private Const AW_HOR_POSITIVE = 1
    Private Const AW_HOR_NEGATIVE = 2
    Private Const AW_VER_POSITIVE = 4
    Private Const AW_VER_NEGATIVE = 8
    Private Const AW_HIDE = 65536
    Private Const AW_ACTIVATE = 131072
    Private Const AW_SLIDE = 262144
    Private Const AW_BLEND = 524288

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
    End Sub

    Public Sub gridbind(ByVal itemid As String)
        Try
            Dim Griddisptype_t As String
            Dim j As Integer

            cmd = Nothing
            ds = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "SALESANALYSIS_LEDGER"
            cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Selecteditemid_v
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = SelectedLocationid_v
            cmd.Parameters.Add("@MONTH", SqlDbType.VarChar).Value = IIf(Month_v Is Nothing, "___", Month_v)
            cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = Type_v
            cmd.Parameters.Add("@days", SqlDbType.VarChar).Value = Days_t
            cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = (DtpFromDate.Value).ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = (DtpToDate.Value).ToString("yyyy/MM/dd")
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "Table1")

            Rowcnt = ds.Tables(0).Rows.Count
            Colcnt_t = ds.Tables(0).Columns.Count

            If Rowcnt = 0 Then Exit Sub

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

            For j = 0 To GridView1.Columns.Count - 1
                GridView1.Columns(j).Name = GridView1.Columns(j).HeaderText
                If Not GridView1.Columns(j).ValueType Is Nothing Then Griddisptype_t = IIf(IsDBNull(GridView1.Columns(j).ValueType.ToString), "", GridView1.Columns(j).ValueType.ToString)

                If Griddisptype_t = "System.Decimal" Then
                    GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Next

            GridView1.Columns(0).Visible = False
            GridView1.AutoResizeColumns()

            If GridView1.Rows.Count > 0 Then GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightSkyBlue
            If GridView1.Rows.Count > 0 Then GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.DarkBlue

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Exit_Click(sender As Object, e As EventArgs) Handles Btn_Exit.Click
        Me.Close()
    End Sub


    Public Sub LockControls(Optional ByVal EditMode As Boolean = False, Optional ByVal ctlcol As Control.ControlCollection = Nothing, Optional ByVal Formname As Form = Nothing)
        Try
            If ctlcol Is Nothing Then ctlcol = Formname.Controls
            For Each ctl As Control In ctlcol
                If TypeOf (ctl) Is TextBox Then
                    DirectCast(ctl, TextBox).Enabled = False
                Else
                    If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                        LockControls(Nothing, ctl.Controls, Formname)
                    End If
                End If
                ctl.Enabled = False
                If TypeOf (ctl) Is Button Then
                    Select Case ctl.Text
                        Case ctl.Text = "&Cancel", ctl.Text = "Cancel"
                            ctl.Text = "&Close"
                        Case "&ok", "&Ok", "ok", "OK"
                            ctl.Visible = EditMode
                        Case Else
                            If EditMode = True Then ctl.Enabled = True Else ctl.Enabled = False
                    End Select
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ExportToExcel()
        Dim ds_comp As New DataSet
        Dim da_comp As SqlDataAdapter
        Dim Compname As String, Add1 As String, Add2 As String, Add3 As String, Add4 As String, Phone As String, Email As String, Cst As String, CstDate As String
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

                cmd = Nothing
                ds_comp = Nothing
                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd.CommandText = "GET_COMP_ADDRESS"
                da_comp = New SqlDataAdapter(cmd)
                ds_comp = New DataSet
                ds_comp.Clear()
                da_comp.Fill(ds_comp, "Table1")

                Dim cnt As Integer
                cnt = ds_comp.Tables(0).Rows.Count

                If cnt > 0 Then
                    Compname = ds_comp.Tables(0).Rows(0).Item("COMPNAME").ToString
                End If

                Dim str As String = ChrW(64 + 1) & jj & ":" & ChrW(64 + iCountCol) & jj

                oRng = xlWorkSheet.Range(str)

                With oRng
                    .MergeCells = True
                    .Value = Compname & vbCrLf & Lbl_ItemName.Text
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
                            chkvalue_t = IIf(IsDBNull(GridView1.Item(j, i).Value), "", GridView1.Item(j, i).Value)
                            If chkvalue_t = "Total" Or chkvalue_t = "Closing" Or chkvalue_t = "Opening" Then
                                q1 = Q
                                For q1 = 1 To GridView1.ColumnCount
                                    xlWorkSheet.Cells(i + 3, q1).Font.FontStyle = "Bold"
                                    xlWorkSheet.Cells(i + 3, q1).Font.color = Color.Maroon
                                    xlWorkSheet.Cells(2, q1).Font.FontStyle = "Bold"
                                    xlWorkSheet.Cells(2, q1).Font.color = Color.DarkBlue
                                Next
                            End If

                            xlWorkSheet.Cells(i + 3, Q).borders.LineStyle = 1
                            Q = Q + 1

                            Griddisptype_t = IIf(CStr(GridView1.Columns(j).ValueType.ToString) Is Nothing, "", GridView1.Columns(j).ValueType.ToString)

                            If Griddisptype_t = "System.Decimal" Then
                                xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" '+ decformat_t
                                If IsNumeric(GridView1.Item(j, i).Value) = True Then
                                    decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
                                End If
                            End If
                        End If

                    Next j
                Next i

                xlWorkSheet.Columns.AutoFit()
                xlWorkSheet.Rows("1:1").Font.FontStyle = "Bold"
                xlWorkSheet.Rows("1:1").Font.Size = 13
                xlWorkSheet.Rows("1:1").rowheight = 40
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

    Private Sub Filterby()
        Try
            'If GridView1.CurrentCell Is Nothing Then Exit Sub
            'Filtercolnmae_t = GridView1.Columns(GridView1.CurrentCell.ColumnIndex).HeaderText

            'If Filtercolnmae_t.IndexOf("Delivery") <> -1 Or Filtercolnmae_t.IndexOf("Inward") <> -1 Then
            '    Filtercolnmae_t = "Type1"
            'End If
            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & TextBox1.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If

            GridView1.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_load_Click(sender As Object, e As EventArgs) Handles Btn_load.Click, Me.Load
        Call gridbind(0)
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).Name
        End If
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Try
            Call TransactionForm(e.RowIndex, e.ColumnIndex)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TransactionForm(ByVal Rowindex As Integer, ByVal colindex As Integer)
        Dim Type As String, headertext As String
        Dim i As Integer
        Dim ds_form As New DataSet
        Dim ctlcol As Control.ControlCollection
        Dim colindex_t As Integer

        If Rowindex >= 0 And colindex >= 0 Then
        End If

        Headerid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(0).Value), 0, (GridView1.Rows(Rowindex).Cells(0).Value))
        Type = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(GridView1.Columns.Count - 4).Value), 0, (GridView1.Rows(Rowindex).Cells(GridView1.Columns.Count - 4).Value))

        Select Case LCase(Type)
            Case "inv"
                AllowFormEdit_t = True
                Dim frmprclst = New frm_invoice
                frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                frmprclst.ShowInTaskbar = False
                frmprclst.StartPosition = FormStartPosition.CenterScreen
                'UserPermission(frm_invoice)
                frmprclst.ShowDialog()
                AllowFormEdit_t = False
            Case "quo"
                AllowFormEdit_t = True
                Dim frmprclst = New frm_quotation
                frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                frmprclst.ShowInTaskbar = False
                'UserPermission(frm_quotation)
                frmprclst.StartPosition = FormStartPosition.CenterScreen
                frmprclst.ShowDialog()
                AllowFormEdit_t = False
        End Select
        
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

    Private Sub Btn_load_KeyDown(sender As Object, e As KeyEventArgs) Handles Btn_load.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView1.Focus()
        End If
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
        Dim rowindex As Integer, colindex As Integer
        If GridView1.CurrentCell Is Nothing Then Exit Sub
        rowindex = GridView1.CurrentCell.RowIndex
        colindex = GridView1.CurrentCell.ColumnIndex
        If e.KeyCode = Keys.Enter Then
            TransactionForm(rowindex, colindex)
        End If
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

    Private Sub GridView1_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles GridView1.RowsAdded
        For j = 0 To ds.Tables(0).Columns.Count - 1
            Colname_t = ds.Tables(0).Columns(j).ColumnName

            If Colname_t.IndexOf("@") <> -1 Then
                GridView1.Columns(j).Visible = False
                ' Exit For
            End If
        Next
        Dim Type_t As String
        For I = 0 To GridView1.Rows.Count - 2
            Type_t = IIf(IsDBNull(GridView1.Rows(I).Cells(1).Value), 0, (GridView1.Rows(I).Cells(1).Value))

            If LCase(Type_t).IndexOf("order close") <> -1 Then
                GridView1.Rows(I).DefaultCellStyle.BackColor = Color.PeachPuff
            End If
        Next
        'GridView1.Rows(GridView1.Rows.Count - 1).Cells(GridView1.Columns.Count - 1).Style.Format = "#0.00"
    End Sub

End Class