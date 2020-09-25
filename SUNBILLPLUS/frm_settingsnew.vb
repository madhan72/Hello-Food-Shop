Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class frm_settingsnew
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds1 As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer, Detailid_t As Double
    Dim sqlstr As String, Filtercolnmae_t As String, SLNO_t As String
    Dim fm As New Sun_Findfrm

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Try
            sqlstr = "SELECT detailid,ISNULL(PROCESS,'') AS PROCESS,ISNULL(NUMERICVALUE,'') AS NUMERIC,ISNULL(STRINGVALUE,'') AS STRING,CONVERT(VARCHAR(10),DATEVALUE,103) AS DATE,isnull(reference,'') as Remarks FROM SETTINGS ORDER BY PROCESS "
            cmd = New SqlCommand(Sqlstr, conn)
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
            GridView1.Columns(1).Width = 250
            GridView1.Columns(2).Width = 70
            GridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(2).DefaultCellStyle.Format = "#0"
            GridView1.ReadOnly = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            If editflag = False Then
                txt_code.Text = AutoNum("SETTINGS", True)
                'Process_t = txt_process.Text
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If
            GensaveSettings(IIf(editflag_t, 1, 0), Trim(txt_process.Text), Detailid_t, Trim(txt_numeric.Text), Trim(txt_string.Text), IIf(Dtp_date.Checked = True, Dtp_date.Value, Nothing), Trim(Txt_reference.Text), Trim(txt_code.Text))
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        Try
            If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
                GroupBox3.Enabled = True
                GridView1.Enabled = False
                GroupBox1.Visible = True
                GroupBox2.Visible = False
                GroupBox1.Enabled = True
            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
                GroupBox3.Enabled = False
                GridView1.Enabled = True
                GroupBox1.Visible = False
                GroupBox2.Visible = True
                GroupBox2.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes1()
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

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            Call enabdisb("Edit")
            txt_numeric.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Try
            editflag = False
            Call clearchars()
            GroupBox1.Enabled = False
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

    Private Sub cmddelete_Click(sender As Object, e As EventArgs)
        'If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
        '    Call Gendelsettings(SLNO_t)
        '    BindData()
        '    enabdisb("ok")
        'End If
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

    Private Sub storechars(Optional ByVal detailid As Double = 0)
        Try
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "SETTINGSNEW_DETAILS"
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

            txt_code.Text = ds1.Tables(0).Rows(rowid_t).Item("sino").ToString
            Txt_reference.Text = ds1.Tables(0).Rows(rowid_t).Item("reference").ToString
            Detailid_t = ds1.Tables(0).Rows(rowid_t).Item("detailid")

            If ds1.Tables(0).Rows(rowid_t).Item("datevalue").ToString <> "" Then
                Dtp_date.Value = ds1.Tables(0).Rows(rowid_t).Item("datevalue").ToString
                Dtp_date.Checked = True
            Else
                Dtp_date.Checked = False
            End If

            'If txt_code.Text = "" Then txt_code.Text = AutoNum("SETTINGS", False)
            txt_process.Text = ds1.Tables(0).Rows(rowid_t).Item("process").ToString
            txt_string.Text = ds1.Tables(0).Rows(rowid_t).Item("stringvalue").ToString
            txt_numeric.Text = ds1.Tables(0).Rows(rowid_t).Item("numericvalue").ToString
            SLNO_t = ds1.Tables(0).Rows(rowid_t).Item("process").ToString

            If txt_process.Text = "Font style" Then
                txt_process.Enabled = False
                txt_string.Enabled = False
                txt_numeric.Enabled = False
                Txt_reference.Enabled = False
                txt_code.Enabled = False
            Else
                txt_process.Enabled = True
                txt_string.Enabled = True
                txt_numeric.Enabled = True
                Txt_reference.Enabled = True
                txt_code.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try
            If txt_search.TextLength > 0 And Colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"
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

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Call saveproc(editflag)
        Call enabdisb("Ok")
        Call BindData()
    End Sub

    Private Sub frm_settingsnew_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call opnconn()
            Call BindData()
            enabdisb("Ok")
            ' txt_code.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_code_GotFocus(sender As Object, e As EventArgs) Handles txt_code.GotFocus
        txt_code.BackColor = Color.Yellow
    End Sub

    Private Sub txt_code_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_code.KeyDown

        If e.KeyCode = Keys.Enter Then
            If editflag = False Then
                storechars(Trim(txt_code.Text))
            End If
            txt_process.Focus()
        End If
    End Sub

    Private Sub txt_code_LostFocus(sender As Object, e As EventArgs) Handles txt_code.LostFocus
        txt_code.BackColor = Color.White
    End Sub

    Private Sub txt_numeric_GotFocus(sender As Object, e As EventArgs) Handles txt_numeric.GotFocus
        txt_numeric.BackColor = Color.Yellow
    End Sub

    Private Sub txt_numeric_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_numeric.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_string.Focus()
        End If
    End Sub

    Private Sub txt_numeric_LostFocus(sender As Object, e As EventArgs) Handles txt_numeric.LostFocus
        txt_numeric.BackColor = Color.White
    End Sub

    Private Sub txt_process_GotFocus(sender As Object, e As EventArgs) Handles txt_process.GotFocus
        txt_process.BackColor = Color.Yellow
    End Sub

    Private Sub txt_process_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_process.KeyDown
        Dim da_head As SqlDataAdapter
        Dim ds_head As New DataSet
        If e.KeyCode = Keys.Enter Then
            If editflag = False Then
                cmd = New SqlCommand("select isnull(detailid,0) as detailid from settings  where process ='" & Trim(txt_process.Text) & "' ", conn)
                cmd.CommandType = CommandType.Text
                da_head = New SqlDataAdapter(cmd)
                ds_head = New DataSet
                ds_head.Clear()
                da_head.Fill(ds_head)
                If ds_head.Tables(0).Rows.Count > 0 Then
                    Detailid_t = ds_head.Tables(0).Rows(0).Item("detailid")
                    Call storechars(Detailid_t)
                End If
            End If
            txt_numeric.Focus()
        End If
    End Sub

    Private Sub txt_process_LostFocus(sender As Object, e As EventArgs) Handles txt_process.LostFocus
        Dim da_head As SqlDataAdapter
        Dim ds_head As New DataSet
        txt_process.BackColor = Color.White
        If editflag = False Then
            cmd = New SqlCommand("select isnull(detailid,0) as detailid from settings  where process ='" & Trim(txt_process.Text) & "' ", conn)
            cmd.CommandType = CommandType.Text
            da_head = New SqlDataAdapter(cmd)
            ds_head = New DataSet
            ds_head.Clear()
            da_head.Fill(ds_head)
            If ds_head.Tables(0).Rows.Count > 0 Then
                Detailid_t = ds_head.Tables(0).Rows(0).Item("detailid")
                Call storechars(Detailid_t)
            End If
        End If
    End Sub

    Private Sub txt_string_GotFocus(sender As Object, e As EventArgs) Handles txt_string.GotFocus
        txt_string.BackColor = Color.Yellow
    End Sub

    Private Sub txt_string_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_string.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dtp_date.Focus()
        End If
    End Sub

    Private Sub txt_string_LostFocus(sender As Object, e As EventArgs) Handles txt_string.LostFocus
        txt_string.BackColor = Color.White
    End Sub

    Private Sub Dtp_date_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_date.KeyDown
        If e.KeyCode = Keys.Enter Then
            Txt_reference.Focus()
        End If
    End Sub

    Private Sub Txt_reference_GotFocus(sender As Object, e As EventArgs) Handles Txt_reference.GotFocus
        Txt_reference.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_reference_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_reference.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub Txt_reference_LostFocus(sender As Object, e As EventArgs) Handles Txt_reference.LostFocus
        Txt_reference.BackColor = Color.White
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

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Try
            Dim ROWINDEX As Integer
            ROWINDEX = GridView1.CurrentCell.RowIndex
            Detailid_t = IIf(IsDBNull(GridView1.Rows(ROWINDEX).Cells(0).Value), 0, (GridView1.Rows(ROWINDEX).Cells(0).Value))
            Call storechars(SLNO_t)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        Try
            Dim ROWINDEX As Integer
            ROWINDEX = GridView1.CurrentCell.RowIndex
            Detailid_t = IIf(IsDBNull(GridView1.Rows(ROWINDEX).Cells(0).Value), 0, (GridView1.Rows(ROWINDEX).Cells(0).Value))
            Call storechars(Detailid_t)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_search_GotFocus(sender As Object, e As EventArgs) Handles txt_search.GotFocus
        txt_search.BackColor = Color.Yellow
    End Sub

    Private Sub txt_search_LostFocus(sender As Object, e As EventArgs) Handles txt_search.LostFocus
        txt_search.BackColor = Color.White
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        Call Filterby()
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


End Class