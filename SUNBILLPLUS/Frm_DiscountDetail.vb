Imports System.Data
Imports System.Data.SqlClient

Public Class Frm_DiscountDetail
    Dim Colindex_t As Integer
    Dim Rowindex_t As Integer
    Dim Filtercolnmae_t As String
    Public bs As BindingSource
    Public Partyid_t As Double

    Enum fields1
        C_Itemid = 0
        C_Code = 1
        C_Itemname = 2
        C_Uom = 3
        C_Decimal = 4
        C_Discount = 5
        C_SelRate = 6
    End Enum

    Private Sub Frm_DiscountDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call opnconn()
            Call gridvisible(GridView1, False, "C_Itemid", "C_decimal")
            Call gridreadonly(GridView1, True, "C_Code", "C_Itemname", "C_Uom", "C_Selrate")
            Call gridreadonly_Color(GridView1, Readonlycolor_t, "C_Code", "C_Itemname", "C_Uom", "C_Selrate")
            Call StoreChars(Partyid_t)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error.!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub StoreChars(ByVal Partyid_v As Double)
        Try
            Dim ds_Disc, ds_Discount As New DataSet
            Dim Sqlstr As String
            Dim cmd As New SqlCommand
            Dim da As SqlDataAdapter

            'Dim cmd As New sqlc
            Sqlstr = "Select * FROM Discount_detail where partyid = " & Partyid_v & ""
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Disc = New DataSet
            ds_Disc.Clear()
            da.Fill(ds_Disc)

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            If ds_Disc.Tables(0).Rows.Count > 0 Then
                cmd.CommandText = "GET_PARTYDISCOUNT"
            Else
                cmd.CommandText = "GET_ITEMDETAILS_DISCOUNT"
            End If

            cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
            ds_Discount = New DataSet
            ds_Discount.Clear()
            da.Fill(ds_Discount)

            If ds_Discount.Tables(0).Rows.Count > 0 Then

                'Dim tables As DataTableCollection = ds_Discount.Tables
                'Dim view1 As New DataView(tables(0))
                '.bs.DataSource = view1

                With Me
                    .GridView1.DataSource = Nothing
                    .GridView1.Rows.Clear()
                    .GridView1.Rows.Add(ds_Discount.Tables(0).Rows.Count)

                    For i = 0 To ds_Discount.Tables(0).Rows.Count - 1
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Itemid).Value = ds_Discount.Tables(0).Rows(i).Item("Itemid")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Code).Value = ds_Discount.Tables(0).Rows(i).Item("Itemcode")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Itemname).Value = ds_Discount.Tables(0).Rows(i).Item("Itemname")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Uom).Value = ds_Discount.Tables(0).Rows(i).Item("UOM")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Decimal).Value = ds_Discount.Tables(0).Rows(i).Item("Decimal")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_SelRate).Value = ds_Discount.Tables(0).Rows(i).Item("Selrate")
                        .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Discount).Value = ds_Discount.Tables(0).Rows(i).Item("Discount")
                    Next
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Colindex_t = GridView1.CurrentCell.ColumnIndex
        Filtercolnmae_t = GridView1.Columns(Colindex_t).Name
        Rowindex_t = GridView1.CurrentCell.RowIndex
    End Sub

    Private Sub GridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEnter
        GridView1.Columns(fields1.C_Discount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        GridView1.Columns(fields1.C_SelRate).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        GridView1.Columns(fields1.C_Discount).DefaultCellStyle.Format = "###.00"
        GridView1.Columns(fields1.C_SelRate).DefaultCellStyle.Format = "###.00"

        GridView1.Columns(fields1.C_Discount).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub GridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Try
            Dim tb As TextBox = CType(e.Control, TextBox)
            Select Case GridView1.CurrentCell.ColumnIndex
                Case fields1.C_Discount
                    AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
                    AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            '   Dim Tmpbalqty_t, TmpEditqty_t As Double
            'If Ordervalition_t = True Then
            '    If GridView1.CurrentCell.ColumnIndex = fields1.c1_qty Then
            '        If Not CType(sender, TextBox).Text.Trim.Length = 0 Then
            '            If CDec(CType(sender, TextBox).Text > (Tmpbalqty_t + TmpEditqty_t)) Then
            '                MsgBox("Cannot exceeds.", MsgBoxStyle.Critical)
            '                CType(sender, TextBox).Text = ""
            '            End If
            '        End If
            '    End If
            'End If
            If GridView1.CurrentCell.ColumnIndex = fields1.C_Discount Then
                If Not CType(sender, TextBox).Text.Trim.Length = 0 Then
                    If CDec(CType(sender, TextBox).Text > 100) Then
                        CType(sender, TextBox).Text = 0
                        'GridView1.CurrentRow.Cells(fields1.C_Discount).Value = 0
                        MessageBox.Show("Discount should not Exceed 100%", "User Alert.!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) 'this function used for cell allow only dec with two digit
        Try
            ' Dim Decimal_t As Double
            Dim Text As String = DirectCast(sender, TextBox).Text
            If Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar) And e.KeyChar <> "." Then
                e.Handled = True
            End If
            If Text.Contains(".") AndAlso e.KeyChar = "."c Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                If Text.IndexOf(".") <> -1 Then
                    If GridView1.CurrentCell.ColumnIndex = fields1.C_Discount Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                        'ElseIf GridView1.CurrentCell.ColumnIndex = fields1.c1_qty Then
                        '    Decimal_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value))
                        '    If Text.Length >= Text.IndexOf(".") + Decimal_t + 2 Then
                        '        e.Handled = True
                        '    End If
                        'ElseIf GridView1.CurrentCell.ColumnIndex = fields1.c1_amount Then
                        '    If Text.Length >= Text.IndexOf(".") + 4 Then
                        '        e.Handled = True
                        '    End If
                    End If
                End If
            End If

            'If GridView1.CurrentCell.ColumnIndex = fields1.c1_itemcode Or GridView1.CurrentCell.ColumnIndex = fields1.c1_remarks Then
            '    e.Handled = False
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                     GridView1.DefaultCellStyle.Font, _
                                   b, _
                                   e.RowBounds.Location.X + 10, _
                                   e.RowBounds.Location.Y + 4)

            ' GridView1.Columns(GridView1.Columns.Count - 3).DefaultCellStyle.Font = New Font("calibri", 8, FontStyle.Bold)
        End Using
    End Sub

    Private Sub Btn_Return_Click(sender As Object, e As EventArgs) Handles Btn_Return.Click
        Me.Hide()
    End Sub

    Private Sub Filterby()
        Try
            If txt_Search.TextLength > 0 And Colindex_t >= 0 And Filtercolnmae_t <> "" Then
                If txt_Search.TextLength > 3 Then
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_Search.Text & "%'"
                Else
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & txt_Search.Text & "%'"
                End If
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_Search_TextChanged(sender As Object, e As EventArgs) Handles txt_Search.TextChanged
        'Call Filterby()
        Call FindItems(txt_Search.Text, Colindex_t)
    End Sub

    Private Function FindItems(ByVal strSearchString As String, ByVal ind As Integer) As Boolean
        Try
            strSearchString = Replace(strSearchString, "'", "`")
            strSearchString = Replace(strSearchString, Chr(34), "`")

            GridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridView1.ClearSelection()

            If strSearchString = "" Then
                GridView1.Rows(0).Selected = True
                GridView1.SelectionMode = DataGridViewSelectionMode.CellSelect
                Exit Function
            End If

            Dim intCount As Integer = 0
            Dim intCell As Integer = 0

            Dim ResetCellStyle As New DataGridViewCellStyle
            ResetCellStyle.BackColor = Color.White
            Dim FoundMatchCellStyle As New DataGridViewCellStyle
            FoundMatchCellStyle.BackColor = Color.Yellow

            For i As Integer = 0 To GridView1.Rows.Count - 1
                If InStr(1, GridView1.Rows(i).Cells(ind).Value.ToString, strSearchString, CompareMethod.Text) Then
                    GridView1.Rows(i).Selected = True
                    GridView1.CurrentCell = GridView1.Rows(i).Cells(ind)
                    FindItems = True
                    Exit Function
                End If
            Next

            Return False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
End Class