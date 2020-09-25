Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App

Public Class frm_outstandingbillwise
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim Rm As New Frm_Reports_Init
    Dim da, da_party, dA_area As SqlDataAdapter
    Dim ds, ds_party, dS_area As New DataSet
    Dim bs_party, bs, bs_process, bs_area As New BindingSource
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim Partyid_t As String, Filtercolnmae_t As String, Colname_t As String, SelectedArea_t As String
    Dim index_t As Integer, colindex_t As Integer, Rowindex_t As Integer
    Private Const HT_CAPTION As Integer = &H2
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Dim celWasEndEdit As DataGridViewCell
    Private _EnterMoveNext As Boolean = True

    Public Sub Init()
        Try
            Call Execute()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Execute()
        Try
            GridView1.AllowUserToAddRows = False
            GridView1.ReadOnly = True

            OptAll_Place.Checked = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadGrid(Optional ByVal Cnt_t As Integer = 0)
        Try
            Dim Rowcnt As Integer, Colcnt_t As Integer

            Dim k As Integer = 1, j As Integer
            Dim Griddisptype_t As String

            If OptAll_Place.Checked = True Then
                Partyid_t = "SELECT PTYCODE FROM PARTY "
            Else
                Partyid_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.ChkLst_Place.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(ChkLst_Place.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(ChkLst_Place.ValueMember).ToString
                    Partyid_t = String.Concat(Partyid_t, valmember, ",")
                Next
                If Partyid_t.Length > 0 Then Partyid_t = Partyid_t.Substring(0, Partyid_t.Length - 1)
            End If

            If Partyid_t = "" Then Partyid_t = "000"

            If opt_allarea.Checked = True Then
                SelectedArea_t = "SELECT LEFT(PTYNAME,2) AS AREA FROM PARTY  "
            End If

            'Else
            '    SelectedArea_t = ""
            '    Dim valmember As String
            '    For idx As Integer = 0 To Me.Chklst_area.CheckedItems.Count - 1
            '        Dim drv As DataRowView = CType(Chklst_area.CheckedItems(idx), DataRowView)
            '        Dim dr As DataRow = drv.Row
            '        valmember = dr(Chklst_area.ValueMember).ToString
            '        SelectedArea_t = String.Concat(SelectedArea_t, valmember, ",")
            '    Next
            '    If SelectedArea_t.Length > 0 Then SelectedArea_t = SelectedArea_t.Substring(0, SelectedArea_t.Length - 1)
            'End If

            'If SelectedArea_t = "" Then SelectedArea_t = "000"

            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "OUTSTANDING_RPTS"
            cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
            'cmd.Parameters.Add("@FROMDATE", SqlDbType.VarChar).Value = Dtp_fromdate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = Dtp_ToDate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@PARTYID", SqlDbType.VarChar).Value = Partyid_t
            cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = Cbo_type.SelectedItem
            cmd.Parameters.Add("@AREA", SqlDbType.VarChar).Value = SelectedArea_t.Substring(0, SelectedArea_t.Length - 1)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds, "Table1")

            Rowcnt = ds.Tables(0).Rows.Count
            Colcnt_t = ds.Tables(0).Columns.Count

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False
            GridView1.AutoGenerateColumns = True

            Dim tables As DataTableCollection = ds.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.AutoResizeColumns()
            GridView1.Refresh()

            For j = 0 To GridView1.Columns.Count - 1
                If Not GridView1.Columns(j).ValueType Is Nothing Then Griddisptype_t = IIf(IsDBNull(GridView1.Columns(j).ValueType.ToString), "", GridView1.Columns(j).ValueType.ToString)

                If Griddisptype_t = "System.Decimal" Then
                    GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    GridView1.Columns(j).DefaultCellStyle.Format = "#0.00"
                End If

                If LCase(GridView1.Columns(j).HeaderText).IndexOf(LCase("id")) <> -1 Then
                    GridView1.Columns(j).Visible = False
                End If
            Next

            For i = 0 To GridView1.Rows.Count - 1
                If GridView1.Rows.Count > 1 Then
                    If GridView1.Rows.Count > 1 And GridView1.Item(0, i).Value = "TOTAL" Then
                        GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                        GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightBlue
                    End If
                    If GridView1.Rows.Count > 1 And LCase(IIf(CStr(GridView1.Item(2, i).Value) Is Nothing, "", (GridView1.Item(2, i).Value))) = LCase("rate") Then
                        GridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
                        GridView1.Rows(GridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightBlue
                    End If
                End If
            Next

            If SelectedArea_t = "SELECT LEFT(PTYNAME,2) AS AREA FROM PARTY " Then
                SelectedArea_t = "'00'"
            End If

            GridView1.ReadOnly = True

        Catch ex As Exception
            If ex.Message = "Cannot find table 0." Then Exit Sub
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
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

    <System.ComponentModel.DefaultValue(True)> _
    Public Property OnEnterKeyMoveNext() As Boolean
        Get
            Return Me._EnterMoveNext
        End Get
        Set(ByVal value As Boolean)
            Me._EnterMoveNext = value
        End Set
    End Property

    Private Sub Btn_Excel_Click(sender As Object, e As EventArgs) Handles Btn_Excel.Click
        Call ExportToExcel()
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
        Dim ds_comp As New DataSet

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
                Dim Filename_t As String = "Outstanding"
                Filename_t = Filename_t + "_" + Microsoft.VisualBasic.Right(random.Next().ToString, 6)

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
                    .Value = Gencompname + vbCrLf + IIf(Cbo_type.SelectedIndex = 0, "OUTSTANDING AREAWISE" + " [ AS ON DATE : " + Dtp_ToDate.Value.ToString("dd/MM/yyyy") + "]", "OUTSTANDING BILLWISE" + " [ AS ON DATE : " + Dtp_ToDate.Value.ToString("dd/MM/yyyy") + "]")
                    .CurrentRegion.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter
                End With

                decformat_t = ".00"
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
                                xlWorkSheet.Cells(i + 3, Q - 1).NumberFormat = "##########0" + decformat_t
                                decval_t = IIf(IsDBNull(GridView1.Item(j, i).Value), 0, GridView1.Item(j, i).Value)
                            End If
                        End If
                    Next j
                Next i

                xlWorkSheet.Columns.AutoFit()
                xlWorkSheet.Rows("1:1").rowheight = 38.25
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

    Delegate Sub SetColumnIndex(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)

    Private Sub GridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEndEdit
        Try
            Dim Flg_t As String, tmpamt_t As Double
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            If Me.GridView1.CurrentCell.ColumnIndex <> Me.GridView1.ColumnCount - 1 Then
                If GridView1.CurrentCell.RowIndex = 0 Then
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{UP}")
                Else
                    'SendKeys.Send("{TAB}")
                    'SendKeys.Send("{UP}")
                End If
            Else
                SendKeys.Send("{HOME}")
                SendKeys.Send("{DOWN}")
            End If

            Dim method1 As New SetColumnIndex(AddressOf FindNextCell)
            Me.GridView1.BeginInvoke(method1, GridView1, Rowindex_t, colindex_t + 1)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FindNextCell(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
        Try
            Dim found As Boolean = False

            While dgv.RowCount > rowindex
                While dgv.Columns.Count > columnindex
                    'If Not (dgv.Rows(rowindex).Cells(columnindex)).ReadOnly Then
                    If (dgv.Rows(rowindex).Cells(columnindex)).Visible Then
                        dgv.CurrentCell = dgv.Rows(rowindex).Cells(columnindex)
                        ' dgv.BeginEdit(True)
                        Exit Sub
                    Else
                        columnindex += 1
                    End If
                    ' Else
                    'columnindex += 1
                    '  End If
                End While
                rowindex += 1
                columnindex = 0
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_searchplace_GotFocus(sender As Object, e As EventArgs) Handles txt_searchplace.GotFocus
        txt_searchplace.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchplace_LostFocus(sender As Object, e As EventArgs) Handles txt_searchplace.LostFocus
        txt_searchplace.BackColor = Color.White
    End Sub

    Private Sub txt_searchLoc_TextChanged(sender As Object, e As EventArgs) Handles txt_searchplace.TextChanged
        Dim indx As Integer = ChkLst_Place.FindString(txt_searchplace.Text)
        If indx >= 0 Then ChkLst_Place.SelectedIndex = indx
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        LoadGrid()
    End Sub

    Private Sub frm_outstandingbillwise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim levstr As String = ""
            ds_party = Nothing
            levstr = " SELECT PTYNAME,ptycode AS PTYCODE FROM party "
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
            Me.ChkLst_Place.DataSource = ds_party
            ChkLst_Place.DataSource = bs_party
            ChkLst_Place.DisplayMember = "PTYNAME"
            ChkLst_Place.ValueMember = "PTYCODE"

            OptAll_Place.Checked = True

            Cbo_type.SelectedIndex = 0

            ds_area = Nothing
            levstr = " SELECT distinct LEFT(PTYNAME,2) AS AREA FROM party ORDER BY LEFT(PTYNAME,2) "
            cmd = Nothing
            ds_area  = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            dA_area = New SqlDataAdapter(cmd)
            ds_area = New DataSet
            ds_area.Clear()
            dA_area.Fill(dS_area)
            bs_area.DataSource = dS_area.Tables(0)
            Me.Chklst_area.DataSource = dS_area
            Chklst_area.DataSource = bs_area
            Chklst_area.DisplayMember = "AREA"
            Chklst_area.ValueMember = "AREA"

            opt_allarea.Checked = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cbo_type_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cbo_type.KeyPress
        e.Handled = True
    End Sub

    Private Sub Cbo_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbo_type.SelectedIndexChanged
        'If Cbo_type.SelectedIndex = 1 Then
        '    Label1.Visible = False
        '    Dtp_fromdate.Visible = False
        'Else
        '    Label1.Visible = True
        '    Dtp_fromdate.Visible = True
        'End If
    End Sub

    Private Sub cmd_exit_Click(sender As Object, e As EventArgs) Handles cmd_exit.Click
        Me.Close()
    End Sub

    Private Sub OptAll_Place_CheckedChanged(sender As Object, e As EventArgs) Handles OptAll_Place.CheckedChanged
        Dim levstr As String
        If OptAll_Place.Checked = True Then
            txt_searchplace.Enabled = False
            txt_searchplace.Text = ""
            ChkLst_Place.Enabled = False
        Else
            Me.ChkLst_Place.DataSource = ds_party
            ChkLst_Place.DataSource = bs_party
            ChkLst_Place.DisplayMember = "ptyname"
            ChkLst_Place.ValueMember = "ptycode"
            ChkLst_Place.Enabled = True
            txt_searchplace.Enabled = True
        End If
    End Sub

    Private Sub Chklst_area_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles Chklst_area.ItemCheck
        Try
            Dim valmember As String, processt1 As String

            If opt_allarea.Checked = True Then Exit Sub

            If SelectedArea_t Is Nothing Then SelectedArea_t = "'00',"

            If Chklst_area.SelectedItems.Count > 0 Then
                If e.NewValue = CheckState.Checked Then

                    If SelectedArea_t.Length > 1 Then
                        If SelectedArea_t.IndexOf(Chklst_area.SelectedValue) = -1 Then
                            SelectedArea_t = String.Concat(SelectedArea_t, "'" + Chklst_area.SelectedValue + "'", ",")
                        End If
                    Else
                        SelectedArea_t = String.Concat(SelectedArea_t, "'" + Chklst_area.SelectedValue + "'", ",")
                    End If
                Else
                    SelectedArea_t = SelectedArea_t.Replace("'" + Convert.ToString(Chklst_area.SelectedValue) + "'" + ",", "")
                End If
            End If

            If Chklst_area.SelectedItems.Count = 0 Then SelectedArea_t = ""

            Dim levstr As String
            If SelectedArea_t.Length > 1 Then

                If SelectedArea_t.Substring(SelectedArea_t.Length - 1, 1) = "," Then
                    processt1 = SelectedArea_t.Substring(0, SelectedArea_t.Length - 1)
                End If
            Else
                processt1 = ""
            End If

            If processt1 = "" Or processt1 Is Nothing Then processt1 = "0"

            ds_party = Nothing
            levstr = " SELECT ptycode AS PTYCODE ,PTYNAME FROM party WHERE LEFT(PTYNAME,2) IN (" & processt1 & ") "
            cmd = Nothing
            ds_party = Nothing
            cmd = New SqlCommand(levstr, conn)
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            da_party = New SqlDataAdapter(cmd)
            ds_party = New DataSet
            ds_party.Clear()
            da_party.Fill(ds_party)
            bs_party.DataSource = ds_party.Tables(0)

            Me.ChkLst_Place.DataSource = Nothing
            Me.ChkLst_Place.DataSource = ds_party
            ChkLst_Place.DataSource = bs_party
            ChkLst_Place.DisplayMember = "ptyname"
            ChkLst_Place.ValueMember = "ptycode"

            'For i As Integer = 0 To Chklst_Item.Items.Count - 1
            '    Chklst_Item.SetItemChecked(i, True)
            'Next
            'For  I =0 TO Chklst_Job. 
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_searcharea_GotFocus(sender As Object, e As EventArgs) Handles txt_searcharea.GotFocus
        txt_searcharea.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searcharea_LostFocus(sender As Object, e As EventArgs) Handles txt_searcharea.LostFocus
        txt_searcharea.BackColor = Color.White
    End Sub

    Private Sub txt_searcharea_TextChanged(sender As Object, e As EventArgs) Handles txt_searcharea.TextChanged
        Dim indx As Integer = Chklst_area.FindString(txt_searcharea.Text)
        If indx >= 0 Then Chklst_area.SelectedIndex = indx
    End Sub

    Private Sub opt_allarea_CheckedChanged(sender As Object, e As EventArgs) Handles opt_allarea.CheckedChanged
        Dim levstr As String
        If opt_allarea.Checked = True Then
            txt_searcharea.Enabled = False
            txt_searcharea.Text = ""
            Chklst_area.Enabled = False

            ds_party = Nothing
            levstr = " SELECT PTYNAME,ptycode AS PTYCODE FROM party "
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

            Me.ChkLst_Place.DataSource = ds_party
            ChkLst_Place.DataSource = bs_party
            ChkLst_Place.DisplayMember = "ptyname"
            ChkLst_Place.ValueMember = "ptycode"
            ChkLst_Place.Enabled = False
            txt_searchplace.Enabled = False
        Else
            SelectedArea_t = ""

            Me.Chklst_area.DataSource = dS_area
            Chklst_area.DataSource = bs_area
            Chklst_area.DisplayMember = "area"
            Chklst_area.ValueMember = "area"
            Chklst_area.Enabled = True
            txt_searcharea.Enabled = True
        End If
    End Sub

    Private Sub cmd_print_Click(sender As Object, e As EventArgs) Handles cmd_print.Click
        Try
            Dim Rowcnt As Integer, Colcnt_t As Integer

            Dim k As Integer = 1, j As Integer
            Dim Griddisptype_t As String

            If OptAll_Place.Checked = True Then
                Partyid_t = "SELECT PTYCODE FROM PARTY "
            Else
                Partyid_t = ""
                Dim valmember As String
                For idx As Integer = 0 To Me.ChkLst_Place.CheckedItems.Count - 1
                    Dim drv As DataRowView = CType(ChkLst_Place.CheckedItems(idx), DataRowView)
                    Dim dr As DataRow = drv.Row
                    valmember = dr(ChkLst_Place.ValueMember).ToString
                    Partyid_t = String.Concat(Partyid_t, valmember, ",")
                Next
                If Partyid_t.Length > 0 Then Partyid_t = Partyid_t.Substring(0, Partyid_t.Length - 1)
            End If

            If Partyid_t = "" Then Partyid_t = "000"

            If opt_allarea.Checked = True Then
                SelectedArea_t = " SELECT LEFT(PTYNAME,2) AS AREA FROM PARTY  "
            End If

            Rm.Init(conn, "OUTSTANDING " + Cbo_type.SelectedItem, Servername_t, Nothing, Nothing, Dtp_ToDate.Value.ToString("yyyy/MM/dd"), Nothing,
                     conn.Database, Gencompid, Nothing, Nothing, Nothing, Partyid_t, Nothing, Nothing, Nothing, SelectedArea_t.Substring(0, SelectedArea_t.Length - 1))
            Rm.StartPosition = FormStartPosition.CenterScreen
            Rm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class