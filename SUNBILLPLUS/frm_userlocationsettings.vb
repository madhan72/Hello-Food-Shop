Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App

Public Class frm_userlocationsettings

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds_loc As New DataSet
    Dim Locationid_t As Double, Userid_t As Double
    Dim Rowindex_t, Colindex_t, Colindex_t1 As Integer
    Dim fm As New Sun_Findfrm
    Dim sqlstr As String
    Dim Filtercolnmae_t As String
    Dim bs As New BindingSource

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Try
            sqlstr = " SELECT UID,UNAME as USERS FROM USERS ORDER BY UNAME  "
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

            GridView1.Columns(0).ReadOnly = True
            GridView1.Columns(1).ReadOnly = True
            ' GridView1.Columns(2).ReadOnly = True

            If strSearchString <> "" Then
                For i = 0 To GridView1.Rows.Count - 1 'its used for focus cursor after save which name is edit
                    If InStr(1, GridView1.Rows(i).Cells(0).Value.ToString, strSearchString, CompareMethod.Text) Then
                        GridView1.Rows(i).Selected = True
                        GridView1.CurrentCell = GridView1.Rows(i).Cells(1)
                        Exit For
                    End If
                Next
            End If

            Filtercolnmae_t = "USERS"
            Colindex_t = 1
            If GridView1.Rows.Count > 0 Then
                If GridView1.CurrentCell Is Nothing Then
                    Userid_t = GridView1.Item(0, 0).Value
                Else
                    Userid_t = GridView1.Item(0, 0).Value
                End If
                Call storechars(Userid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Width = 210
            GridView1.Columns(1).HeaderText = "USER"

            Dim font As New Font( _
                GridView1.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)

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
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Gridfindfom(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try
            If activecol < 0 Or activerow < 0 Then Exit Sub
            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Dim Locid_t As Double
            Dim loc_t As String

            Select Case Colindex_t1
                Case 1
                    For i = 0 To GridView2.Rows.Count - 1
                        Locid_t = IIf(IsDBNull(GridView2.Item(0, i).Value), 0, GridView2.Item(0, i).Value)
                        If Locid_t <> 0 And i <> Rowindex_t Then
                            loc_t = String.Concat(loc_t, String.Concat(Locid_t), ",")
                        End If
                    Next

                    If loc_t <> "" Then
                        loc_t = loc_t.Remove(loc_t.Length - 1)
                    Else
                        loc_t = "00"
                    End If

                    sqlstr = "Select godownname,masterid From godown_master where masterid not in (" & loc_t & ") Order By godownname "
                    cmd = New SqlCommand(sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_loc = New DataSet
                    ds_loc.Clear()
                    da.Fill(ds_loc)

                    If ds_loc.Tables(0).Rows.Count > 0 Then
                        VisibleCols.Add("godownname")
                        Colheads.Add("Location")

                        fm.Frm_Width = 275
                        fm.Frm_Height = 300
                        fm.Frm_Left = 642
                        fm.Frm_Top = 367

                        fm.MainForm = New frm_userlocationsettings
                        fm.Active_ctlname = "Gridview2"

                        Csize.Add(230)

                        tmppassstr = IIf(IsDBNull((GridView2.Rows(activerow).Cells(activecol).Value)), "", (GridView2.Rows(activerow).Cells(activecol).Value))
                        fm.VarNew = ""
                        fm.VarNewid = 0
                        fm.EXECUTE(conn, ds_loc, VisibleCols, Colheads, Locationid_t, "", False, Csize, "", False, False, "", tmppassstr)
                        GridView2.Rows(activerow).Cells(1).Value = fm.VarNew
                        GridView2.Rows(activerow).Cells(0).Value = fm.VarNewid
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal Userid As Double = 0)
        Try 
            Dim cnt As Integer

            sqlstr = "Select isnull(gm.godownname,'') as Location,isnull(u.locationid,0) as locationid from USERLOC_DETAILS u left join godown_master gm on gm.masterid  = u.locationid where u.u_id= " & Userid & " "
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

            cnt = ds.Tables(0).Rows.Count

            GridView2.Rows.Clear()

            txt_uname.Enabled = False

            If cnt > 0 Then
                txt_uname.Text = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(1).Value), "", (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(1).Value))
                Userid_t = Userid
                GridView2.Rows.Add(cnt)
                For i = 0 To cnt - 1
                    GridView2.Rows(i).Cells(1).Value = ds.Tables(0).Rows(i).Item("location").ToString
                    GridView2.Rows(i).Cells(0).Value = ds.Tables(0).Rows(i).Item("locationid")
                Next
            Else
                txt_uname.Text = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(1).Value), "", (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(1).Value))
                Userid_t = Userid

                sqlstr = " SELECT ISNULL(GODOWNNAME,'') AS LOCATION,ISNULL(MASTERID,0) AS LOCATIONID FROM GODOWN_MASTER ORDER BY GODOWNNAME  "
                cmd = New SqlCommand(sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da = New SqlDataAdapter(cmd)
                ds = New DataSet
                ds.Clear()
                da.Fill(ds)

                cnt = ds.Tables(0).Rows.Count
                GridView2.Rows.Clear()
                GridView2.Rows.Add(cnt)
                For i = 0 To cnt - 1
                    GridView2.Rows(i).Cells(1).Value = ds.Tables(0).Rows(i).Item("location").ToString
                    GridView2.Rows(i).Cells(0).Value = ds.Tables(0).Rows(i).Item("locationid")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveProc()
        Try
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "DELETE FROM USERLOC_DETAILS WHERE U_ID = " & Userid_t & ""
            cmd.ExecuteNonQuery()

            If txt_uname.Text = "" Then Locationid_t = 0
            For I = 0 To GridView2.Rows.Count - 1
                Locationid_t = IIf(IsDBNull(GridView2.Rows(I).Cells(0).Value), 0, (GridView2.Rows(I).Cells(0).Value))
                If Locationid_t <> 0 And Userid_t <> 0 Then
                    cmd = Nothing
                    cmd = New SqlCommand
                    cmd.Connection = conn
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "USER_LOCUPD"
                    cmd.Parameters.Add("@USERID", SqlDbType.Float).Value = Userid_t
                    cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = IIf(Locationid_t <> 0, Locationid_t, DBNull.Value)
                    cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = I + 1
                    cmd.ExecuteNonQuery()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LocationFindFrm()
        Try
            sqlstr = "Select godownname,masterid From godown_master Order By godownname "
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_loc = New DataSet
            ds_loc.Clear()
            da.Fill(ds_loc)

            If ds_loc.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("godownname")
                Colheads.Add("godownname")
                fm.Frm_Width = 250
                fm.Frm_Height = 300
                fm.Frm_Left = 700
                fm.Frm_Top = 350

                fm.MainForm = New frm_userlocationsettings
                fm.Active_ctlname = "txt_defaultlocation"
                Csize.Add(200)
                tmppassstr = txt_uname.Text
                If tmppassstr = "" Then Exit Sub
                fm.EXECUTE(conn, ds_loc, VisibleCols, Colheads, Locationid_t, "", False, Csize, "", False, False, "", tmppassstr)
                txt_uname.Text = fm.VarNew
                Locationid_t = fm.VarNewid

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Call SaveProc()
        cmd_edit.Visible = True
        txt_uname.Enabled = False
        GridView1.Enabled = True
        cmd_exit.Visible = True
        cmd_cancel.Visible = False
        GridView2.Enabled = False
        GridView1.Focus()
        'Call BindData()
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        'Me.Hide()
        txt_uname.Enabled = False
        GridView1.Enabled = True
        cmd_exit.Visible = True
        cmd_cancel.Visible = False
        cmd_edit.Visible = True
        cmd_ok.Visible = False
        GridView2.Enabled = False
        Call BindData()
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                txt_uname.Text = ""
                Userid_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(0).Value), 0, (GridView1.Rows(e.RowIndex).Cells(0).Value))
                storechars(Userid_t)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Dim rowindex As Integer
                rowindex = GridView1.CurrentCell.RowIndex

                txt_uname.Text = ""
                Userid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(0).Value), 0, (GridView1.Rows(rowindex).Cells(0).Value))
                storechars(Userid_t)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Try
            Dim rowindex As Integer
            rowindex = GridView1.CurrentCell.RowIndex

            txt_uname.Text = ""
            Userid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(0).Value), 0, (GridView1.Rows(rowindex).Cells(0).Value))
            storechars(Userid_t)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        Try
            Dim rowindex As Integer
            rowindex = GridView1.CurrentCell.RowIndex

            txt_uname.Text = ""
            Userid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(0).Value), 0, (GridView1.Rows(rowindex).Cells(0).Value))
            storechars(Userid_t)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frm_userlocationsettings_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call opnconn()
        Call BindData()
        txt_uname.Enabled = False
    End Sub

    Private Sub txt_defaultlocation_Click(sender As Object, e As EventArgs) Handles txt_uname.Click
        'Call LocationFindFrm()
    End Sub

    Private Sub txt_defaultlocation_GotFocus(sender As Object, e As EventArgs) Handles txt_uname.GotFocus
        txt_uname.BackColor = Color.Yellow
    End Sub

    Private Sub txt_defaultlocation_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_uname.KeyDown
        If e.KeyCode = Keys.Enter Then
            ' Call LocationFindFrm()
            cmd_ok.Focus()
        End If
    End Sub

    Private Sub txt_defaultlocation_LostFocus(sender As Object, e As EventArgs) Handles txt_uname.LostFocus
        txt_uname.BackColor = Color.White
    End Sub

    Private Sub txt_defaultlocation_TextChanged(sender As Object, e As EventArgs) Handles txt_uname.TextChanged
        'Call LocationFindFrm()
    End Sub

    Private Sub cmd_edit_Click(sender As Object, e As EventArgs) Handles cmd_edit.Click
        cmd_ok.Visible = True
        'txt_uname.Enabled = True
        GridView1.Enabled = False
        cmd_cancel.Visible = True
        cmd_exit.Visible = False
        cmd_edit.Visible = False
    End Sub

    Private Sub cmd_exit_Click(sender As Object, e As EventArgs) Handles cmd_exit.Click
        Me.Hide()
    End Sub

    Private Sub GridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellClick
        Try
            Colindex_t1 = GridView2.CurrentCell.ColumnIndex
            Rowindex_t = e.RowIndex
            Call Gridfindfom(Rowindex_t, Colindex_t1, GridView2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Delegate Sub SetColumnIndex(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
    Delegate Sub SetColumnIndex1(ByVal rowindex As Integer, ByVal columnindex As Integer, ByVal dgv As DataGridView)

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

    Private Sub GridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellEndEdit
        Try
            Dim Flg_t As String, tmpamt_t As Double
            Colindex_t1 = GridView2.CurrentCell.ColumnIndex
            Rowindex_t = GridView2.CurrentCell.RowIndex

            Dim method2 As New SetColumnIndex1(AddressOf Gridfindfom)
            Me.GridView2.BeginInvoke(method2, Rowindex_t, Colindex_t1, GridView2)

            If GridView2.Rows.Count - 1 = e.RowIndex Then
                If IsValidRow(GridView2, "c_location") Then
                    GridView2.Rows.Add(1)
                End If
            End If

            If Me.GridView2.CurrentCell.ColumnIndex <> Me.GridView2.ColumnCount - 1 Then
                If GridView2.CurrentCell.RowIndex = 0 Then
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

            Dim method1 As New SetColumnIndex(AddressOf FindNextCell)
            Me.GridView2.BeginInvoke(method1, GridView2, Rowindex_t, Colindex_t1 + 1)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FindNextCell(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
        Try
            Dim found As Boolean = False

            While dgv.RowCount > rowindex
                While dgv.Columns.Count > columnindex
                    If Not (dgv.Rows(rowindex).Cells(columnindex)).ReadOnly Then
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

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Try
            Dim tmpval_t As Double
            If e.KeyCode = Keys.Enter Then

                Colindex_t1 = GridView2.CurrentCell.ColumnIndex
                Rowindex_t = GridView2.CurrentCell.RowIndex
               
                Call Gridfindfom(Rowindex_t, Colindex_t1, GridView2)

                e.SuppressKeyPress = True

                FindNextCell(GridView2, Rowindex_t, Colindex_t1 + 1)  'checking from Next 
            ElseIf e.KeyCode = Keys.Back Then
                GridView2.BeginEdit(True)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView2.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   GridView1.DefaultCellStyle.Font, _
                                   b, _
                                   e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

End Class