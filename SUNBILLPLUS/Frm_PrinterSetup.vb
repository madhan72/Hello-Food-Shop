Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App
Imports System.Drawing.Printing
Public Class Frm_PrinterSetup
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds1, ds_Pros, ds_Rpttype, ds_Rptname As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer, Detailid_t As Double
    Dim sqlstr As String, Filtercolnmae_t As String, SLNO_t As String
    Dim fm As New Sun_Findfrm

    Dim Printer As New System.Drawing.Printing.PrinterSettings
    Dim PaperName As New System.Drawing.Printing.PaperSize

    Private Sub Frm_PrinterSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call opnconn()
            Call BindData()
            Call dsopen()
            enabdisb("Ok")
            ' txt_code.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try
            sqlstr = "SELECT R.PROCESS FROM REPORTSETUP R GROUP BY R.PROCESS ORDER BY R.PROCESS"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Pros = New DataSet
            ds_Pros.Clear()
            da.Fill(ds_Pros, "ProcessList")

            sqlstr = "SELECT R.RPTTYPE FROM REPORTSETUP R GROUP BY R.RPTTYPE ORDER BY R.RPTTYPE"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Rpttype = New DataSet
            ds_Rpttype.Clear()
            da.Fill(ds_Rpttype, "RpttypeList")

            sqlstr = "SELECT R.RPTNAME FROM REPORTSETUP R GROUP BY R.RPTNAME ORDER BY R.RPTNAME"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Rptname = New DataSet
            ds_Rptname.Clear()
            da.Fill(ds_Rptname, "RptnameList")

            Dim ProcessCollection As New AutoCompleteStringCollection
            For i = 0 To ds_Pros.Tables(0).Rows.Count - 1
                ProcessCollection.Add(ds_Pros.Tables(0).Rows(i).Item("Process").ToString)
            Next

            Dim RpttypeCollection As New AutoCompleteStringCollection
            For i = 0 To ds_Rpttype.Tables(0).Rows.Count - 1
                RpttypeCollection.Add(ds_Rpttype.Tables(0).Rows(i).Item("Rpttype").ToString)
            Next

            Dim RptNameCollection As New AutoCompleteStringCollection
            For i = 0 To ds_Rptname.Tables(0).Rows.Count - 1
                RptNameCollection.Add(ds_Rptname.Tables(0).Rows(i).Item("Rptname").ToString)
            Next

            txt_Process.AutoCompleteCustomSource = ProcessCollection
            txt_Rpttype.AutoCompleteCustomSource = RpttypeCollection
            txt_Rptname.AutoCompleteCustomSource = RptNameCollection

            Dim textBoxes = {txt_Process, txt_Rpttype, txt_Rptname}
            For Each TextBox In textBoxes
                TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource
                TextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        Try
            If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
                Grp_Process.Enabled = True
                GridView1.Enabled = False
                Grp_Save.Visible = True
                Grp_Add.Visible = False
                Grp_Save.Enabled = True
            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
                Grp_Process.Enabled = False
                GridView1.Enabled = True
                Grp_Save.Visible = False
                Grp_Add.Visible = True
                Grp_Add.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes1()

            Cbo_PrinterName.DataSource = Nothing
            Cbo_PaperName.DataSource = Nothing
            Cbo_PrinterName.Items.Clear()
            Cbo_PaperName.Items.Clear()

            Dim InstalledPrinters As String

            For Each InstalledPrinters In PrinterSettings.InstalledPrinters
                Cbo_PrinterName.Items.Add(InstalledPrinters)
            Next

            Cbo_PrinterName.SelectedIndex = 0
            Printer.PrinterName = Cbo_PrinterName.SelectedItem().ToString

            For Each PaperName In Printer.PaperSizes()
                Cbo_PaperName.Items.Add(PaperName.PaperName)
            Next

            Cbo_PaperName.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ClearTextBoxes1(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        Try
            If ctlcol Is Nothing Then ctlcol = Me.Controls
            For Each ctl As Control In ctlcol
                If TypeOf (ctl) Is TextBox Then
                    If DirectCast(ctl, TextBox).Name = "txt_search" Then 'particular text box will not cleared
                    Else
                        DirectCast(ctl, TextBox).Clear()
                    End If
                Else
                    If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                        ClearTextBoxes1(ctl.Controls)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Try
            sqlstr = "SELECT R.DETAILID,R.PROCESS as Process,ISNULL(R.PAPERNAME,'')AS 'Paper Name',ISNULL(R.PRINTERNAME,'')AS 'Printer Name'" _
                        & " FROM REPORTSETUP R WHERE R.SYSNAME= '" & System.Environment.MachineName.ToString & "' ORDER BY R.PROCESS"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

            Dim cnt, i As Integer
            cnt = ds.Tables(0).Rows.Count
            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False

            Dim tables As DataTableCollection = ds.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.Refresh()

            txt_search.Text = ""

            If strSearchString <> "" Then
                For i = 0 To GridView1.Rows.Count - 1 'its used for focus cursor after save which name is edit
                    If InStr(1, GridView1.Rows(i).Cells(1).Value.ToString, strSearchString, CompareMethod.Text) Then
                        GridView1.Rows(i).Selected = True
                        GridView1.CurrentCell = GridView1.Rows(i).Cells(1)
                        Exit For
                    End If
                Next
            End If

            Filtercolnmae_t = "PROCESS"
            Colindex_t = 1
            If GridView1.Rows.Count > 0 Then
                If GridView1.CurrentCell Is Nothing Then
                    Detailid_t = IIf(IsDBNull(GridView1.Item(0, 0).Value), 0, (GridView1.Item(0, 0).Value))
                Else
                    Detailid_t = IIf(IsDBNull(GridView1.Item(0, GridView1.CurrentCell.RowIndex).Value), 0, (GridView1.Item(0, GridView1.CurrentCell.RowIndex).Value))
                End If
                Call storechars(Detailid_t)
            End If

            Dim font As New Font( _
                GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 28
            GridView1.ColumnHeadersDefaultCellStyle.Font = font
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            cnt = GridView1.Columns.Count

            For i = 0 To cnt - 1
                GridView1.Columns(i).DefaultCellStyle.Font = font
            Next

            GridView1.AutoResizeColumns()
            GridView1.Columns(0).Visible = False
            'GridView1.Columns(1).Width = 250
            'GridView1.Columns(2).Width = 70
            'GridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            'GridView1.Columns(2).DefaultCellStyle.Format = "#0"
            GridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal detailid As Double = 0)
        Try
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_REPORTSETUP"
            cmd.Parameters.Add("@DETAILID", SqlDbType.Float).Value = detailid
            ds1 = New DataSet
            ds1.Clear()
            da.Fill(ds1)

            Dim rowid_t As Integer, Detlcnt_t As Integer
            Call clearchars()
            rowid_t = ds1.Tables(0).Rows.Count

            If rowid_t <= 0 Then Exit Sub

            If rowid_t > 0 Then editflag = True

            rowid_t = rowid_t - 1

            txt_Process.Text = ds1.Tables(0).Rows(rowid_t).Item("Process").ToString
            txt_Rpttype.Text = ds1.Tables(0).Rows(rowid_t).Item("Rpttype").ToString
            txt_Rptname.Text = ds1.Tables(0).Rows(rowid_t).Item("Rptname").ToString
            Detailid_t = ds1.Tables(0).Rows(rowid_t).Item("detailid")

            Cbo_PrinterName.Text = ds1.Tables(0).Rows(rowid_t).Item("Printername").ToString
            Cbo_PaperName.Text = ds1.Tables(0).Rows(rowid_t).Item("Papername").ToString

            'If txt_code.Text = "" Then txt_code.Text = AutoNum("SETTINGS", False)
            'txt_string.Text = ds1.Tables(0).Rows(rowid_t).Item("stringvalue").ToString
            'txt_numeric.Text = ds1.Tables(0).Rows(rowid_t).Item("numericvalue").ToString
            'SLNO_t = ds1.Tables(0).Rows(rowid_t).Item("process").ToString

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try
            If txt_search.TextLength > 0 And Colindex_t >= 0 And Filtercolnmae_t <> "" Then

                If txt_search.TextLength > 3 Then
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"
                Else
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & txt_search.Text & "%'"
                End If
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()

            If GridView1.CurrentCell Is Nothing Then Exit Sub
            If GridView1.Rows.Count > 0 Then Detailid_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value))
            Call storechars(Detailid_t)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            editflag = False
            Call enabdisb("Add")
            Call clearchars()
            txt_process.Focus()
            ' txt_code.Text = AutoNum("SETTINGS", False)
            ' txt_code.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Call saveproc(editflag)
        Call enabdisb("Ok")
        Call BindData()
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Try
            editflag = False
            Call clearchars()
            Grp_Add.Enabled = False
            Call enabdisb("cancel")
            Call BindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Filtercolnmae_t = GridView1.Columns(e.ColumnIndex).HeaderText
                Detailid_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(0).Value), 0, (GridView1.Rows(e.RowIndex).Cells(0).Value))
                Call storechars(Detailid_t)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            Dim ROWINDEX, COLINDEX As Integer
            ROWINDEX = GridView1.CurrentCell.RowIndex
            COLINDEX = GridView1.CurrentCell.ColumnIndex
            If ROWINDEX >= 0 And COLINDEX >= 0 Then
                Filtercolnmae_t = GridView1.Columns(COLINDEX).HeaderText
                Detailid_t = IIf(IsDBNull(GridView1.Rows(ROWINDEX).Cells(0).Value), 0, (GridView1.Rows(ROWINDEX).Cells(0).Value))
                Call storechars(Detailid_t)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cbo_PrinterName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbo_PrinterName.SelectedIndexChanged
        Try
            Cbo_PaperName.DataSource = Nothing
            Cbo_PaperName.Items.Clear()

            Printer.PrinterName = Cbo_PrinterName.SelectedItem().ToString

            For Each PaperName In Printer.PaperSizes()
                Cbo_PaperName.Items.Add(PaperName.PaperName)
            Next

            Cbo_PaperName.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   GridView1.DefaultCellStyle.Font, _
                                   b, _
                                   e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)

            If Trim(txt_Process.Text) = "" Then
                MsgBox("Process Should not be Empty.!")
                txt_Process.Focus()
            ElseIf Trim(txt_Rpttype.Text) = "" Then
                MsgBox("Rpt Type Should not be Empty.!")
                txt_Rpttype.Focus()
            ElseIf Trim(txt_Rptname.Text) = "" Then
                MsgBox("Rpt Name Should not be Empty.!")
                txt_Rptname.Focus()
            Else
                GenSavePrinterSetup(IIf(editflag_t, 1, 0), Trim(txt_Process.Text), Detailid_t, Trim(txt_Rpttype.Text), Trim(txt_Rptname.Text), _
                                    Trim(Cbo_PrinterName.Text), Trim(Cbo_PaperName.Text), _
                                    IIf(Chk_DirecPrint.CheckState = CheckState.Checked, True, False), SystemName_t)
                trans.Commit()
            End If

        Catch ex As Exception
            trans.Rollback()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ProcessFindfom()
        Try
            Dim Tmpsqlstr_t As String
            Dim Processid_t As Double

            sqlstr = "SELECT R.PROCESS FROM REPORTSETUP R GROUP BY R.PROCESS ORDER BY R.PROCESS"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Pros = New DataSet
            ds_Pros.Clear()
            da.Fill(ds_Pros)

            VisibleCols.Add("Process")
            Colheads.Add("Process")

            fm.Frm_Width = 600
            fm.Frm_Height = 300
            fm.Frm_Left = 350
            fm.Frm_Top = 150
            fm.MainForm = New Frm_PrinterSetup
            fm.Active_ctlname = "txt_process"
            Csize.Add(475)

            tmppassstr = txt_Process.Text
            fm.EXECUTE(conn, ds_Pros, VisibleCols, Colheads, Processid_t, "", True, Csize, "", False, False, "", tmppassstr)
            txt_Process.Text = fm.VarNew
            Processid_t = fm.VarNewid

            VisibleCols.Remove(1)
            Colheads.Remove(1)
            Csize.Remove(1)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_Process_Click(sender As Object, e As EventArgs) Handles txt_Process.Click
        'Call ProcessFindfom()
    End Sub

    Private Sub txt_Process_GotFocus(sender As Object, e As EventArgs) Handles txt_Process.GotFocus
        txt_Process.BackColor = Color.Yellow
    End Sub

    Private Sub txt_Process_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Process.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txt_Process.Text) = "" Then
                MessageBox.Show("Process Should not be Empty.!", "User Input Error.!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_Process.Focus()
            Else
                txt_Rpttype.Focus()
            End If
        End If
    End Sub

    Private Sub txt_Process_LostFocus(sender As Object, e As EventArgs) Handles txt_Process.LostFocus
        txt_Process.BackColor = Color.White
    End Sub

    Private Sub txt_Rpttype_GotFocus(sender As Object, e As EventArgs) Handles txt_Rpttype.GotFocus
        txt_Rpttype.BackColor = Color.Yellow
    End Sub

    Private Sub txt_Rpttype_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Rpttype.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txt_Rpttype.Text) = "" Then
                MessageBox.Show("Rpt Type Should not be Empty.!", "User Input Error.!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_Rpttype.Focus()
            Else
                txt_Rptname.Focus()
            End If
        End If
    End Sub

    Private Sub txt_Rpttype_LostFocus(sender As Object, e As EventArgs) Handles txt_Rpttype.LostFocus
        txt_Rpttype.BackColor = Color.White
    End Sub

    Private Sub txt_Rptname_GotFocus(sender As Object, e As EventArgs) Handles txt_Rptname.GotFocus
        txt_Rptname.BackColor = Color.Yellow
    End Sub

    Private Sub txt_Rptname_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_Rptname.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txt_Rptname.Text) = "" Then
                MessageBox.Show("Rpt Name Should not be Empty.!", "User Input Error.!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txt_Rptname.Focus()
            Else
                Cbo_PrinterName.Focus()
            End If
        End If
    End Sub

    Private Sub txt_Rptname_LostFocus(sender As Object, e As EventArgs) Handles txt_Rptname.LostFocus
        txt_Rptname.BackColor = Color.White
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
            GenDelPrinterSetup(Detailid_t)
            BindData()
            enabdisb("ok")
        End If
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            Call enabdisb("Edit")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class