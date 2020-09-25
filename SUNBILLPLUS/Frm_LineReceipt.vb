Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1

Public Class Frm_LineReceipt
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da, da_ac, da_party, da_line As SqlDataAdapter
    Dim bs As New BindingSource
    Dim ds, ds_party, ds_ac, ds_line As New DataSet
    Dim dt As New DataTable
    Dim Lineid_t, Descid_t As Double
    Dim index_t As Integer, Rowindex_t As Integer, Headcnt_t As Integer, Detlcnt_t As Integer, colindex_t As Integer, Colname_t As String
    Dim dr As SqlDataReader
    Dim fm As New Sun_Findfrm
    Dim celWasEndEdit As DataGridViewCell

    Enum fields1
        c1_code = 0
        c1_party = 1
        c1_partyid = 2
        c1_amount = 3
        c1_description = 4
        c1_descid = 5
    End Enum

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            calcnetamt()

        End If
    End Sub

    Private Sub GridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellContentClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Delegate Sub SetColumnIndex(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
    Delegate Sub SetColumnIndex1(ByVal rowindex As Integer, ByVal columnindex As Integer, ByVal dgv As DataGridView)

    Private Sub GridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEndEdit
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name

                Me.celWasEndEdit = GridView1(e.ColumnIndex, e.RowIndex)

                Dim method2 As New SetColumnIndex1(AddressOf Gridfindfom)
                Me.GridView1.BeginInvoke(method2, Rowindex_t, colindex_t, GridView1)

                If GridView1.Rows.Count - 1 = e.RowIndex Then
                    If IsValidRow(GridView1, "c_adless", "c_party", "c_partyid") Then
                        GridView1.Rows.Add(1)
                    End If
                End If

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

                'FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                Call calcnetamt()
                Dim method1 As New SetColumnIndex(AddressOf FindNextCell)
                Me.GridView1.BeginInvoke(method1, GridView1, Rowindex_t, colindex_t + 1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellEnter
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Gridfindfom(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try
            Dim ds_itm As New DataSet, ds_itemdet As New DataSet
            Dim Tmpitemcode_t As String, tmpuom_t As String, Tmpitemid_t As Double, Tmpitemdes_t As String, activerow_tmp As Integer, Decimal_t As Double
            Dim Costrate_t As Double, Rate_t As Double
            Dim Sqlstr As String
            If activecol < 0 Or activerow < 0 Then Exit Sub

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t

                Case fields1.c1_description
                    ds_ac.Clear()
                    'Sqlstr = "Select Ptyname,Partyid From Account  Where Groupid in('-151','-152','-256')  Order By Ptyname"
                    Sqlstr = "Select Ptyname,Partyid From Account  Order By Ptyname"
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da_ac = New SqlDataAdapter(cmd)
                    ds_ac = New DataSet
                    ds_ac.Clear()
                    da_ac.Fill(ds_ac)

                    If ds_ac.Tables(0).Rows.Count > 0 Then
                        VisibleCols.Add("Ptyname")
                        Colheads.Add("Description")

                        fm.Frm_Width = 300
                        fm.Frm_Height = 300
                        fm.Frm_Left = 693
                        fm.Frm_Top = 234

                        fm.MainForm = New Frm_LineReceipt
                        fm.Active_ctlname = "Gridview1"

                        Csize.Add(250)
                        tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))
                        fm.VarNew = ""
                        fm.VarNewid = 0
                        fm.EXECUTE(conn, ds_ac, VisibleCols, Colheads, Descid_t, "", False, Csize, "", False, False, "", tmppassstr)
                        GridView1.Rows(activerow).Cells(activecol).Value = fm.VarNew
                        GridView1.Rows(activerow).Cells(activecol + 1).Value = fm.VarNewid
                        Lineid_t = fm.VarNewid

                      
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) 'this function used for cell allow only dec with two digit
        Try
            Dim Decimal_t As Double
            Dim Text As String = DirectCast(sender, TextBox).Text
            If Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar) And e.KeyChar <> "." Then
                e.Handled = True
            End If
            If Text.Contains(".") AndAlso e.KeyChar = "."c Then
                e.Handled = True
            End If
            If Char.IsDigit(e.KeyChar) Then
                If Text.IndexOf(".") <> -1 Then

                    If GridView1.CurrentCell.ColumnIndex = fields1.c1_amount Then
                        If Text.Length >= Text.IndexOf(".") + 5 Then
                            e.Handled = True
                        End If

                    End If
                End If
            End If

            If GridView1.CurrentCell.ColumnIndex = fields1.c1_code Or GridView1.CurrentCell.ColumnIndex = fields1.c1_description _
                Or GridView1.CurrentCell.ColumnIndex = fields1.c1_party Then
                e.Handled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub calcnetamt()
        Try
            Txt_Totamt.Text = Format(Tot_Calc(GridView1, fields1.c1_amount), "#######0.00")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function IsValidRow(ByVal SprCtrl As DataGridView, ByVal ParamArray cols() As String) As Boolean
        Try
            Dim i As Object
            Dim tmpstr As String
            IsValidRow = True
            For Each i In cols
                tmpstr = SprCtrl.Item(i, SprCtrl.CurrentCell.RowIndex).value
                If Trim(tmpstr) = "" Or Trim(tmpstr) = "0" Or Trim(tmpstr) = "0.0" Or Trim(tmpstr) = "0.00" Then
                    IsValidRow = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub FindNextCell(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
        Try
            Dim found As Boolean = False

            While dgv.RowCount > rowindex
                While dgv.Columns.Count > columnindex
                    If Not (dgv.Rows(rowindex).Cells(columnindex)).ReadOnly And Not (LCase(dgv.Columns(columnindex).HeaderText) = LCase("remarks")) Then
                        If (dgv.Rows(rowindex).Cells(columnindex)).Visible Then
                            dgv.CurrentCell = dgv.Rows(rowindex).Cells(columnindex)
                            ' dgv.BeginEdit(True)
                            Exit Sub
                        Else
                            columnindex += 1
                        End If
                    Else
                        columnindex += 1
                    End If
                End While
                rowindex += 1
                columnindex = 0
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Call calcnetamt()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Dim tb As TextBox = CType(e.Control, TextBox)
        Select Case GridView1.CurrentCell.ColumnIndex
            Case fields1.c1_amount, fields1.c1_code, fields1.c1_party, fields1.c1_description
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End Select
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try 
            Dim tmpval_t As Double 
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            If e.KeyCode = Keys.Enter Then

                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)

                e.SuppressKeyPress = True

                If colindex_t = fields1.c1_amount Then
                    tmpval_t = IIf(IsDBNull(GridView1.Item(colindex_t, Rowindex_t).Value), 0, (GridView1.Item(colindex_t, Rowindex_t).Value))
                    If tmpval_t <> 0 Then
                        FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                    End If
                Else
                    FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                End If
            ElseIf e.KeyCode = Keys.Back Then
                GridView1.BeginEdit(True)
            End If

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

    Private Sub Execute()
        Try
            Call gridreadonly(GridView1, True, "c_party", "c_code")
            Call gridreadonly_Color(GridView1, Readonlycolor_t, "c_party", "c_code")
            GridView1.Columns(fields1.c1_code).Visible = False
            GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim Sqlstr As String
         
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Frm_LineReceipt_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        conn.Close()
    End Sub

    Private Sub Frm_LineReceipt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        conn.Close()
    End Sub

    Private Sub Frm_LineReceipt_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            conn.Open()
            Call Execute()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Linefindfrm()
        Try
            Dim cnt As Integer, Sqlstr As String

            ds_line.Clear()
            ds_line = Nothing
            Sqlstr = "Select LINE,MASTERID FROM LINE_MASTER ORDER BY LINE "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_line = New SqlDataAdapter(cmd)
            ds_line = New DataSet
            ds_line.Clear()
            da_line.Fill(ds_line)

            cnt = ds_line.Tables(0).Rows.Count

            If cnt > 0 Then
                VisibleCols.Add("LINE")
                Colheads.Add("Line")

                fm.Frm_Width = 400
                fm.Frm_Height = 300
                fm.Frm_Left = 350
                fm.Frm_Top = 150
                fm.MainForm = New Frm_LineReceipt
                fm.Active_ctlname = "txt_line"
                Csize.Add(275)

                If cnt = 1 Then
                    txt_line.Text = ds_line.Tables(0).Rows(0).Item("line").ToString
                    Lineid_t = ds_line.Tables(0).Rows(0).Item("masterid")
                Else
                    tmppassstr = txt_line.Text
                    fm.EXECUTE(conn, ds_line, VisibleCols, Colheads, Lineid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    txt_line.Text = fm.VarNew
                    Lineid_t = fm.VarNewid
                End If

                If Lineid_t <> 0 Then
                    ds_party.Clear()
                    ds_party = Nothing
                    Sqlstr = "SELECT A.PTYNAME,A.PARTYID AS REFID,ISNULL(P.CODE,'') AS CODE FROM PARTY P  JOIN ACCOUNT A ON A.REFID = P.PTYCODE WHERE P.LINEID  =" & Lineid_t & "ORDER BY PTYNAME"
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da_party = New SqlDataAdapter(cmd)
                    ds_party = New DataSet
                    ds_party.Clear()
                    da_party.Fill(ds_party)

                    If ds_party.Tables(0).Rows.Count > 0 Then
                        GridView1.Rows.Clear()
                        GridView1.Rows.Add(ds_party.Tables(0).Rows.Count)
                        For I = 0 To ds_party.Tables(0).Rows.Count - 1
                            GridView1.Rows(I).Cells(fields1.c1_partyid).Value = ds_party.Tables(0).Rows(I).Item("REFID")
                            GridView1.Rows(I).Cells(fields1.c1_party).Value = ds_party.Tables(0).Rows(I).Item("PTYNAME").ToString
                            GridView1.Rows(I).Cells(fields1.c1_code).Value = ds_party.Tables(0).Rows(I).Item("CODE").ToString
                        Next
                    End If

                    ds_ac.Clear()
                    Sqlstr = "Select Ptyname,Partyid From Account WHERE PTYNAME ='CASH' Order By Ptyname"
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da_ac = New SqlDataAdapter(cmd)
                    ds_ac = New DataSet
                    ds_ac.Clear()
                    da_ac.Fill(ds_ac)

                    For i = 0 To GridView1.Rows.Count - 1
                        GridView1.Rows(i).Cells(fields1.c1_description).Value = ds_ac.Tables(0).Rows(0).Item("PTYNAME").ToString
                        GridView1.Rows(i).Cells(fields1.c1_descid).Value = ds_ac.Tables(0).Rows(0).Item("PARTYID")
                    Next


                End If
                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_line_Click(sender As Object, e As EventArgs) Handles txt_line.Click
        Call Linefindfrm()
    End Sub

    Private Sub txt_line_GotFocus(sender As Object, e As EventArgs) Handles txt_line.GotFocus
        txt_line.BackColor = Color.Yellow
    End Sub

    Private Sub txt_line_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_line.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Linefindfrm()
            GridView1.Focus()
        End If
    End Sub

    Private Sub txt_line_LostFocus(sender As Object, e As EventArgs) Handles txt_line.LostFocus
        txt_line.BackColor = Color.White
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        ' AutoNum('A/C RECEIPT', True)
        Dim Partyid_t As Double, Accsid_t As Double, Amount_t As Double

        For I = 0 To GridView1.Rows.Count - 1
            Partyid_t = IIf(IsDBNull(GridView1.Rows(I).Cells(fields1.c1_partyid).Value), 0, (GridView1.Rows(I).Cells(fields1.c1_partyid).Value))
            Accsid_t = IIf(IsDBNull(GridView1.Rows(I).Cells(fields1.c1_descid).Value), 0, (GridView1.Rows(I).Cells(fields1.c1_descid).Value))
            Amount_t = IIf(IsDBNull(GridView1.Rows(I).Cells(fields1.c1_amount).Value), 0, (GridView1.Rows(I).Cells(fields1.c1_amount).Value))

            If Partyid_t <> 0 And Accsid_t <> 0 And Amount_t <> 0 Then
                cmd = Nothing
                cmd = New SqlCommand("LINERECEIPT_ACSAVE_UPD", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trans
                cmd.Parameters.Add("@PTYID", SqlDbType.Float).Value = Partyid_t
                cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd.Parameters.Add("@SALES_AC", SqlDbType.Float).Value = Accsid_t
                cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_t
                cmd.Parameters.Add("@VCHDATE", SqlDbType.VarChar).Value = DTP_Vchdate.Value.ToString("yyyy/MM/dd")
                cmd.ExecuteNonQuery()
            End If
        Next
        Me.Hide()
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Vchdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_line.Focus()
        End If
    End Sub

End Class