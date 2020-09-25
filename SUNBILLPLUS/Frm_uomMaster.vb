Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class FrmUomMaster
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da_uom, da_delete As SqlDataAdapter
    Dim ds, ds_uom, ds_delete As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Uomid_t As Double
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer
    Dim Sqlstr As String, Filtercolnmae_t As String, Defaultprocess_t As String
    Dim fm As New Sun_Findfrm

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            editflag = False
            Call enabdisb("Add")
            Call clearchars()
            txt_uomname.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            Call enabdisb("Edit")
            txt_uomname.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        GridView1.Enabled = False
        Call Delteproc()
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
            If Trim(txt_uomname.Text) = "" Then
                MsgBox("Uom Name should not be empty.")
                txt_uomname.Focus()
            Else
                Call saveproc(editflag)
                Call clearchars()
                Call BindData()
                Call enabdisb("OK")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Delteproc()
        Try
            Dim cnt As Integer
            Dim i As Integer = 0

            If i = 0 Then Sqlstr = "select * from ITEM_MASTER where UOMID = " & Uomid_t & ""
            ds = Nothing
            cmd = Nothing
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

            cnt = ds.Tables(0).Rows.Count

            If cnt = 0 Then
                If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                    Call GendelUom(Uomid_t)
                    Call enabdisb("Ok")
                    Call BindData()
                End If
            Else
                MsgBox("Can't Delete.")
                Call enabdisb("Ok")
                Call BindData()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Call enabdisb("Ok")
            Call BindData()
        End Try
    End Sub

    Private Sub cmdcancel_Click(sender As Object, e As EventArgs) Handles cmdcancel.Click
        Try
            editflag = False
            Call clearchars()
            GroupBox3.Enabled = False
            Call enabdisb("cancel")
            Call BindData()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Call ClearTextBoxes1()
        Uomid_t = 0
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
            GroupBox3.Enabled = True
            GridView1.Enabled = False
            GroupBox1.Visible = False
            GroupBox2.Visible = True
        End If

        If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
            GroupBox3.Enabled = False
            GridView1.Enabled = True
            GroupBox1.Visible = True
            GroupBox2.Visible = False
        End If

    End Sub

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Call clearchars()

        Dim ds_settings As New DataSet
        Dim da_settings As SqlDataAdapter
        Dim dscnt As Integer
        Dim Val_t As Integer

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
        Else

            Val_t = 0
        End If

        If Val_t = 0 Then
            Sqlstr = "select MASTERID,UOM,NOOFDECIMAL from UOM_MASTER order by UOM "
        Else
            Sqlstr = "select MASTERID,ISNULL(TAMILUOM,'') AS UOM,NOOFDECIMAL from UOM_MASTER order by UOM "
        End If

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

        GridView1.Columns(0).ReadOnly = True
        GridView1.Columns(1).ReadOnly = True
        GridView1.Columns(2).ReadOnly = True

        txt_search.Text = ""

        If strSearchString <> "" Then
            For i = 0 To GridView1.Rows.Count - 1 'its used for focus cursor after save which name is edit
                If InStr(1, GridView1.Rows(i).Cells(0).Value.ToString, strSearchString, CompareMethod.Text) Then
                    GridView1.Rows(i).Selected = True
                    GridView1.CurrentCell = GridView1.Rows(i).Cells(1)
                    Exit For
                End If
            Next
        End If

        Filtercolnmae_t = "Uom"
        Colindex_t = 1
        If GridView1.Rows.Count > 0 Then
            If GridView1.CurrentCell Is Nothing Then
                Uomid_t = GridView1.Item(0, 0).Value
            Else
                Uomid_t = GridView1.Item(0, GridView1.CurrentCell.RowIndex).Value
            End If

            Call storechars(Uomid_t)
        End If

        GridView1.Columns(0).Visible = False
        GridView1.Columns(1).Width = 150
        GridView1.Columns(1).HeaderText = "Uom"
        GridView1.Columns(2).Width = 100
        GridView1.Columns(2).HeaderText = "No of Decimal"

        Dim font As New Font( _
            GridView1.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)

        GridView1.EnableHeadersVisualStyles = False
        GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
        GridView1.ColumnHeadersHeight = 35
        GridView1.ColumnHeadersDefaultCellStyle.Font = font
        GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
        GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
        GridView1.RowsDefaultCellStyle.BackColor = Color.White
        GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

        cnt = GridView1.Columns.Count
        For i = 0 To cnt - 1
            GridView1.Columns(i).DefaultCellStyle.Font = font
        Next

        Dim ds_font As New DataSet
        Dim da_font As SqlDataAdapter
        Dim cnt1 As Integer

        Sqlstr = "SELECT ISNULL(NUMERICVALUE,0) AS SIZE,ISNULL(STRINGVALUE,'') as fONTNAME,isnull(reference,'') as Style FROM SETTINGS WHERE PROCESS='FONT STYLE' "
        cmd = New SqlCommand(Sqlstr, conn)
        cmd.CommandType = CommandType.Text
        da_font = New SqlDataAdapter(cmd)
        ds_font = New DataSet
        ds_font.Clear()
        da_font.Fill(ds_font)
        cnt1 = ds_font.Tables(0).Rows.Count

        If cnt1 > 0 Then
            Font_m = ds_font.Tables(0).Rows(0).Item("FONTNAME").ToString
            Size_m = ds_font.Tables(0).Rows(0).Item("SIZE").ToString
            FontStyle_m = ds_font.Tables(0).Rows(0).Item("Style").ToString
        End If

    End Sub

    Private Sub filterby()
        Try
            Filtercolnmae_t = "Uom"
            If txt_search.TextLength > 0 And Colindex_t >= 0 And Filtercolnmae_t <> "" Then

                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"

                If Not GridView1.CurrentCell Is Nothing Then Uomid_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value))
                Call storechars(Uomid_t)
            Else
                bs.Filter = String.Empty
                Call BindData()
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ClearTextBoxes1(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
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
    End Sub

    Private Sub storechars(Optional ByVal Partyid_v As Double = 0)
        Try
            ds_uom.Clear()
            ds_uom = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_UOMMASTER"
            cmd.Parameters.Add("@MASTERID", SqlDbType.Float).Value = Uomid_t
            da = New SqlDataAdapter(cmd)
            ds_uom = New DataSet
            da.Fill(ds_uom)

            Dim rowid_t As Integer
            Call clearchars()
            rowid_t = ds_uom.Tables(0).Rows.Count

            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1

            Uomid_t = ds_uom.Tables(0).Rows(rowid_t).Item("MASTERID")
            txt_uomname.Text = ds_uom.Tables(0).Rows(rowid_t).Item("UOM").ToString
            txt_tamiluom.Text = ds_uom.Tables(0).Rows(rowid_t).Item("tamiluom").ToString

            txt_decimal.BackColor = Color.White
            txt_uomname.BackColor = Color.White

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            Uomid_t = GensaveUom(IIf(editflag_t, 1, 0), Uomid_t, Val(txt_decimal.Text), Trim(txt_uomname.Text), txt_tamiluom.Text)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call opnconn()
            Call BindData()
            enabdisb("Ok")

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            Dim converter As System.ComponentModel.TypeConverter = _
    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            Dim font1 As Font = _
     CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))

            txt_uomname.Font = font1
            txt_tamiluom.Font = font1

            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(Colindex_t).Name

                If GridView1.Rows.Count > 0 Then
                    Uomid_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(0).Value))
                    Call storechars(Uomid_t)
                Else
                End If
            End If
        Catch ex As Exception
            If ex.Message <> "Object reference not set to an instance of an object." Then
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            Dim Rowindex_t As Double

            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex

            If Rowindex_t >= 0 And Colindex_t >= 0 Then
                Dim i As Integer
                i = Rowindex_t
                If GridView1.Rows.Count > 0 And i >= 0 Then
                    If GridView1.Item(0, i).Value = Nothing Then
                    Else
                        Uomid_t = GridView1.Item(0, i).Value
                        Call storechars(Uomid_t)
                    End If
                End If
            End If

        Catch ex As Exception
            If ex.Message <> "Object reference not set to an instance of an object." Then
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End Try
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        Try
            Dim Rowindex_t As Double

            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex

            If Rowindex_t >= 0 And Colindex_t >= 0 Then
                Dim i As Integer
                i = Rowindex_t
                If GridView1.Rows.Count > 0 And i >= 0 Then
                    If GridView1.Item(0, i).Value = Nothing Then
                    Else
                        Uomid_t = GridView1.Item(0, i).Value
                        Call storechars(Uomid_t)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message <> "Object reference not set to an instance of an object." Then
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

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

    Private Sub txt_uomname_GotFocus(sender As Object, e As EventArgs) Handles txt_uomname.GotFocus
        txt_uomname.BackColor = Color.Yellow
    End Sub

    Private Sub txt_uomname_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_uomname.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_tamiluom.Focus()
        End If
    End Sub

    Private Sub txt_uomname_LostFocus(sender As Object, e As EventArgs) Handles txt_uomname.LostFocus
        txt_uomname.BackColor = Color.White
    End Sub

    Private Sub txt_decimal_GotFocus(sender As Object, e As EventArgs) Handles txt_decimal.GotFocus
        txt_decimal.BackColor = Color.Yellow
    End Sub

    Private Sub txt_decimal_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_decimal.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_decimal_LostFocus(sender As Object, e As EventArgs) Handles txt_decimal.LostFocus
        txt_decimal.BackColor = Color.White
    End Sub

    Private Sub txt_search_GotFocus(sender As Object, e As EventArgs) Handles txt_search.GotFocus
        txt_search.BackColor = Color.Yellow
    End Sub

    Private Sub txt_search_LostFocus(sender As Object, e As EventArgs) Handles txt_search.LostFocus
        txt_search.BackColor = Color.White
    End Sub

    Private Sub txt_search_TextChanged(sender As Object, e As EventArgs) Handles txt_search.TextChanged
        Call filterby()
    End Sub

    Private Sub txt_tamiluom_GotFocus(sender As Object, e As EventArgs) Handles txt_tamiluom.GotFocus
        txt_tamiluom.BackColor = Color.Yellow
    End Sub

    Private Sub txt_tamiluom_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_tamiluom.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_decimal.Focus()
        End If
    End Sub

    Private Sub txt_tamiluom_LostFocus(sender As Object, e As EventArgs) Handles txt_tamiluom.LostFocus
        txt_tamiluom.BackColor = Color.White
    End Sub
End Class