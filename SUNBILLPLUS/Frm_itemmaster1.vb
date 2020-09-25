Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App
Imports System.IO
Imports System.Collections
Imports System.Drawing.Text

Public Class Frm_itemmaster1

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim installed_fonts As New InstalledFontCollection
    Dim CurrRowindex_t As Integer, Events_t As String = "", Rowcnt As Integer
    Dim da, da1, da_freeitem, da_category, da_rake As SqlDataAdapter
    Dim ds, ds_freeitem, ds1, ds_detl, ds_size, ds_category, ds_rake, ds_color, ds_Group, ds_Uom, ds_itmenabl As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Itemid_t As Double, Sizeid_t As Double, Colorid_t As Double, Groupid_t As Double, Uomid_t, Rakeid_t, Categoryid_t As Double, Freeitemid As Double
    Dim accountid_t As Double
    Dim Enablekey As Integer = 0
    Dim Format_t As String = ""
    Dim editflag As Boolean, Isalreadyexistflag_t As Boolean = False
    Dim index As Integer, Detlcnt_t As Integer, colindex_t As Integer, Rowindex_t As Integer, Gridrowcnt_t As Integer
    Dim Sqlstr As String, SizeSqlstr As String, Colorsqlstr_t As String, GroupSqlstr_t As String, UomSqlstr_t As String, Path_t As String, Filtercolnmae_t As String
    Dim font1 As Font
    Dim Val_t As Integer
    Dim Fm As New Sun_Findfrm
    Dim ds_settings As New DataSet
    Dim da_settings As SqlDataAdapter
    Dim dscnt As Integer

    Private Sub frmitemmaster1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call opnconn()
            Call dsopen()
            Call BindData()
            enabdisb("Ok")
            '' If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RecordFocus()
        Try
            If GridView1.Rows.Count = 0 Then Exit Sub
            Rowcnt = GridView1.Rows.Count - 1
            'following lines are used to cursor focus which record was edit,add,delete,print
            If Events_t = "Add" Then
                If CurrRowindex_t < Rowcnt - 1 Then
                    CurrRowindex_t = CurrRowindex_t + 1 'new record add focus to tat last record
                End If
            ElseIf Events_t = "Edit" Then 'no change for currrowindex
                ' CurrRowindex_t = CurrRowindex_t
            ElseIf Events_t = "Delete" Then
                If CurrRowindex_t >= Rowcnt Then
                    CurrRowindex_t = CurrRowindex_t - 1 'delete last record focus to prev record
                End If
            ElseIf Events_t = "Print" Then
            Else
            End If

            If Rowcnt > 0 Then
                GridView1.Rows(CurrRowindex_t).Selected = True
                GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
     
    Private Sub txt_ItemDes_GotFocus(sender As Object, e As EventArgs) Handles txt_ItemDes.GotFocus
        txt_ItemDes.BackColor = Color.Yellow
    End Sub

    Private Sub txt_ItemDes_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_ItemDes.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_selretpric.Focus()
        End If
    End Sub

    Private Sub dsopen()
        Try
            Dim Sqlstr_t As String
            GroupSqlstr_t = "Select Groupname, Masterid From Group_Master Order By Groupname"
            cmd = New SqlCommand(GroupSqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Group = New DataSet
            ds_Group.Clear()
            da.Fill(ds_Group)

            UomSqlstr_t = "Select Uom, Masterid From Uom_Master Order By Uom"
            cmd = New SqlCommand(UomSqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Uom = New DataSet
            ds_Uom.Clear()
            da.Fill(ds_Uom)

            Sqlstr_t = "Select category, Masterid From category_Master Order By category "
            cmd = New SqlCommand(Sqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da_category = New SqlDataAdapter(cmd)
            ds_category = New DataSet
            ds_category.Clear()
            da_category.Fill(ds_category)


            Sqlstr_t = "Select Rake, Masterid From rake_Master Order By rake "
            cmd = New SqlCommand(Sqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da_rake = New SqlDataAdapter(cmd)
            ds_rake = New DataSet
            ds_rake.Clear()
            da_rake.Fill(ds_rake)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            If GridView1.CurrentCell Is Nothing Then Exit Sub
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).HeaderText
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                   GridView1.DefaultCellStyle.Font, _
                                   b, _
                                   e.RowBounds.Location.X + 5, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub BindData()
        Try
            Call clearchars()

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

            'If Val_t = 1 Then
            '    Sqlstr = "Select I.Itemid, I.ItemCode AS CODE, ISNULL(I.ITEMTAMILDES,'') AS ITEMDESC ,ISNULL(UM.TAMILUOM,'') AS UOM,ISNULL(I.SELPRICERETAIL,0) AS RATE,  ISNULL(MG.GROUPNAME,'') AS GROUPNAME, " _
            '       & "  ISNULL(CM.CATEGORY,'') AS CATEGORY, ISNULL(I.REMARKS,'') AS REMARKS   " _
            '       & "  From Item_Master I Left Join Group_Master Mg On Mg.Masterid = I.Groupid  " _
            '       & "  Left Join CATEGORY_Master CM On CM.Masterid = I.CATEGORYID " _
            '       & "  Left Join UOM_Master UM On UM.Masterid = I.UOMID "
            'Else
            '    Sqlstr = "Select I.Itemid, I.ItemCode AS CODE, ISNULL(I.ITEMDES,'') AS ITEMDESC,ISNULL(UM.UOM,'') AS UOM,ISNULL(I.SELPRICERETAIL,0) AS RATE, ISNULL(MG.GROUPNAME,'') AS GROUPNAME , " _
            '           & "  ISNULL(CM.CATEGORY,'') AS CATEGORY, ISNULL(I.REMARKS,'') AS REMARKS   " _
            '           & "  From Item_Master I Left Join Group_Master Mg On Mg.Masterid = I.Groupid  " _
            '           & "  Left Join CATEGORY_Master CM On CM.Masterid = I.CATEGORYID " _
            '           & "  Left Join UOM_Master UM On UM.Masterid = I.UOMID "
            'End If

            ds.Clear()
            ds = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_ITEMMASTER2_GRID"
            cmd.Parameters.Add("@Value", SqlDbType.Float).Value = Gencompid
            ds = New DataSet
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

            If cnt > 0 Then
                If Not GridView1.CurrentCell Is Nothing Then
                    Itemid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                End If
                Call storechars(Itemid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Width = 300
            GridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.ReadOnly = True
            '  GridView1.AutoResizeColumns() ''''13-04-2017

            Dim font As New Font( _
                GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 25
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            cnt = GridView1.Columns.Count
            For i = 0 To cnt - 1
                GridView1.Columns(i).DefaultCellStyle.Font = font
                'GridView1.AutoResizeColumn(i)
            Next

            ' GridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Call RecordFocus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub enabdisb(ByVal Val As String)
        Try
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

            If UCase(Val) = "ADD" Then
                Label19.Visible = False
                Label21.Visible = False
                Lbl_Time.Visible = False
                Lbl_user.Visible = False
            Else
                Label19.Visible = True
                Label21.Visible = True
                Lbl_Time.Visible = True
                Lbl_user.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            'txt_itemcode.Enabled = True
            Path_t = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal Itemid_v As Double = 0)
        Try
            Dim Decimal_t As Double

            ds1.Clear()
            ds1 = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_ITEMMASTER2"
            cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_t
            ds1 = New DataSet
            da.Fill(ds1)

            Dim rowid_t As Integer
            rowid_t = ds1.Tables(0).Rows.Count
            Gridrowcnt_t = 0
            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1

            'txt_itemcode.Enabled = False
            'txt_itemcode.Text = ds1.Tables(0).Rows(rowid_t).Item("ITEMCODE").ToString

            Itemid_t = ds1.Tables(0).Rows(rowid_t).Item("ITEMID").ToString
            txt_ItemDes.Text = ds1.Tables(0).Rows(rowid_t).Item("ITEMDESCRIPTION").ToString
            txt_selretpric.Text = ds1.Tables(0).Rows(rowid_t).Item("SELLPRICE")
            Groupid_t = ds1.Tables(0).Rows(rowid_t).Item("GROUPID").ToString
            txt_group.Text = ds1.Tables(0).Rows(rowid_t).Item("GROUPNAME").ToString
            Txt_costrate.Text = ds1.Tables(0).Rows(rowid_t).Item("COSTPRICE").ToString
            Lbl_Time.Text = ds1.Tables(0).Rows(rowid_t).Item("MODIFIEDDATE").ToString
            Lbl_user.Text = ds1.Tables(0).Rows(rowid_t).Item("UNAME").ToString

            If ds1.Tables(0).Rows(rowid_t).Item("Inactive") = 0 Then
                Chk_Inactive.Checked = True
            Else
                Chk_Inactive.Checked = False
            End If
       
            'Decimal_t = ds1.Tables(0).Rows(rowid_t).Item("FREEdecimal")

            txt_remakrs.Text = ds1.Tables(0).Rows(rowid_t).Item("REMARKS").ToString
            GridView1.Focus()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub ClearTextBoxes(Optional ByVal ctlcol As Control.ControlCollection = Nothing)
        Try
            If ctlcol Is Nothing Then ctlcol = Me.Controls
            For Each ctl As Control In ctlcol
                If TypeOf (ctl) Is TextBox Then
                    DirectCast(ctl, TextBox).Clear()
                Else
                    If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                        ClearTextBoxes(ctl.Controls)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try

            

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If GridView1.Rows.Count > 0 Then
                    Itemid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                    Call storechars(Itemid_t)
                Else

                End If
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Filtercolnmae_t = GridView1.Columns(e.ColumnIndex).HeaderText
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.RowEnter
        Try
            Dim i As Integer
            i = e.RowIndex
            'Call loadtextboxes1(i)

            If GridView1.Rows.Count > 0 And i >= 0 Then
                If GridView1.Item(0, i).Value = Nothing Then
                Else
                    Itemid_t = GridView1.Item(0, i).Value
                    Call storechars(Itemid_t)
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(sender As Object, e As EventArgs) Handles cmdadd.Click
        Try
            If Rowcnt > 0 Then
                CurrRowindex_t = Rowcnt - 1
            Else
                CurrRowindex_t = 0
            End If

            Events_t = "Add"
            editflag = False
            Call enabdisb("Add")
            Events_t = "Add"
            Call clearchars()
            'txt_itemcode.Focus()
            Label19.Visible = False
            Label21.Visible = False
            Lbl_Time.Visible = False
            Lbl_user.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_ItemDes_LostFocus(sender As Object, e As EventArgs) Handles txt_ItemDes.LostFocus
        Try
            Dim ds_detail As New DataSet

            Dim cnt As Integer
            Sqlstr = "Select I.Itemid From Item_Master I  WHERE I.ITEMDES ='" & Trim(txt_ItemDes.Text) & "'"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_detail = New DataSet
            ds_detail.Clear()
            da.Fill(ds_detail)
            cnt = ds_detail.Tables(0).Rows.Count

            If cnt > 0 Then
                Itemid_t = ds_detail.Tables(0).Rows(0).Item("itemid")
                Call storechars(Itemid_t)
                editflag = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        txt_ItemDes.BackColor = Color.White
    End Sub

    Private Sub txt_selretpric_GotFocus(sender As Object, e As EventArgs) Handles txt_selretpric.GotFocus
        txt_selretpric.BackColor = Color.Yellow
    End Sub

    Private Sub txt_selretpric_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_selretpric.KeyDown
        If Not IsNumeric(txt_selretpric.Text) And Not txt_selretpric.Text = "" Then
            If txt_selretpric.Text.IndexOf("."c) = -1 Then txt_selretpric.Text = ""
            txt_selretpric.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            Txt_costrate.Focus()
        End If
    End Sub

    Private Sub txt_selretpric_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_selretpric.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_selretpric_LostFocus(sender As Object, e As EventArgs) Handles txt_selretpric.LostFocus
        txt_selretpric.Text = Format(Val(txt_selretpric.Text), "#######0.00")
        txt_selretpric.BackColor = Color.White
    End Sub

    Private Sub cmdedit_Click(sender As Object, e As EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            CurrRowindex_t = Rowindex_t
            Events_t = "Edit"
            Call enabdisb("Edit")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmddelete_Click(sender As Object, e As EventArgs) Handles cmddelete.Click
        CurrRowindex_t = Rowindex_t
        Events_t = "Delete"
        GridView1.Enabled = False
        Call Delteproc()
    End Sub

    Private Sub cmdexit_Click(sender As Object, e As EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub cmdok_Click(sender As Object, e As EventArgs) Handles cmdok.Click
        Try
           If txt_ItemDes.Text = "" Then
                MsgBox("Item Description should not be empty.")
                txt_ItemDes.Focus()
            ElseIf txt_group.Text = "" Then
                MsgBox("Group should not be empty.")
                txt_group.Focus()
            Else
                Call saveproc(editflag)
                Call enabdisb("Ok")
                Call BindData()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Call GendelItem2(Itemid_t)
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

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            Itemid_t = GensaveItem2(IIf(editflag_t, 1, 0), Itemid_t, txt_ItemDes.Text, _
            Val(Txt_costrate.Text), Val(txt_selretpric.Text), Groupid_t, txt_remakrs.Text, IIf(Chk_Inactive.Checked = True, 0, 1), Gencompid)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub Groupfindfrm()
        Try
            VisibleCols.Add("Groupname")
            Colheads.Add("Group")
            Fm.Frm_Width = 300
            Fm.Frm_Height = 300
            Fm.Frm_Left = 845
            Fm.Frm_Top = 239

            Fm.MainForm = New Frm_Itemmaster
            Fm.Active_ctlname = "txt_Group"
            Csize.Add(250)
            tmppassstr = txt_group.Text
            Fm.EXECUTE(conn, ds_Group, VisibleCols, Colheads, Groupid_t, "", True, Csize, "", False, False, "", tmppassstr)
            txt_group.Text = Fm.VarNew
            Groupid_t = Fm.VarNewid

            VisibleCols.Remove(1)
            Colheads.Remove(1)
            Csize.Remove(1)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_group_Click(sender As Object, e As EventArgs) Handles txt_group.Click
        Call Groupfindfrm()
    End Sub

    Private Sub txt_group_GotFocus(sender As Object, e As EventArgs) Handles txt_group.GotFocus
        txt_group.BackColor = Color.Yellow
    End Sub

    Private Sub txt_group_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_group.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Groupfindfrm()
            txt_remakrs.Focus()
        End If
    End Sub

    Private Sub txt_group_LostFocus(sender As Object, e As EventArgs) Handles txt_group.LostFocus
        txt_group.BackColor = Color.White
    End Sub

    Enum fields1
        c1_Itemid = 0
        c1_Itemcode = 1
        c1_Itemdes = 2
        c1_Group = 3
        c1_uom = 4
        c1_category = 5
        c1_remarks = 6
    End Enum

    Private Sub txt_remakrs_GotFocus(sender As Object, e As EventArgs) Handles txt_remakrs.GotFocus
        txt_remakrs.BackColor = Color.Yellow
    End Sub

    Private Sub txt_remakrs_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_remakrs.KeyDown
        If e.KeyCode = Keys.Enter Then
            Chk_Inactive.Focus()
        End If
    End Sub

    Private Sub txt_remakrs_LostFocus(sender As Object, e As EventArgs) Handles txt_remakrs.LostFocus
        txt_remakrs.BackColor = Color.White
    End Sub

    Private Sub Filterby()
        Try
            If txt_search.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                If txt_search.TextLength > 3 Then
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & txt_search.Text & "%'"
                Else
                    bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '" & txt_search.Text & "%'"
                End If
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()

            'For i = 0 To GridView1.Rows.Count - 1
            '    GridView1.Rows(i).DefaultCellStyle.Font = font1
            'Next
            ' GridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Try
            If GridView1.CurrentCell Is Nothing Then Exit Sub
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).HeaderText
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        Try
            If GridView1.CurrentCell Is Nothing Then Exit Sub
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Filtercolnmae_t = GridView1.Columns(colindex_t).HeaderText
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Chk_Inactive_KeyDown(sender As Object, e As KeyEventArgs) Handles Chk_Inactive.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub Txt_costrate_GotFocus(sender As Object, e As EventArgs) Handles Txt_costrate.GotFocus
        Txt_costrate.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_costrate_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_costrate.KeyDown
        If Not IsNumeric(Txt_costrate.Text) And Not Txt_costrate.Text = "" Then
            If Txt_costrate.Text.IndexOf("."c) = -1 Then Txt_costrate.Text = ""
            Txt_costrate.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_group.Focus()
        End If
    End Sub

    Private Sub Txt_costrate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_costrate.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub Txt_costrate_LostFocus(sender As Object, e As EventArgs) Handles Txt_costrate.LostFocus
        Txt_costrate.Text = Format(Val(Txt_costrate.Text), "#######0.00")
        Txt_costrate.BackColor = Color.White
    End Sub

End Class