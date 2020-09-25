Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App
Imports System.IO
Imports System.Collections
Imports System.Drawing.Text
'Imports MetroFramework.Forms

Public Class frm_itemmaster ': Inherits MetroFramework.Forms.MetroForm

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim installed_fonts As New InstalledFontCollection
    Dim CurrRowindex_t As Integer, Events_t As String = "", Rowcnt As Integer
    Dim genutype As String
    Dim ds_utype As New DataSet
    Dim da_utype As SqlDataAdapter
    Dim da, da1, da_freeitem, da_category, da_rake As SqlDataAdapter
    Dim ds, ds_freeitem, ds1, ds_detl, ds_size, ds_category, ds_rake, ds_hsnaccode, ds_color, ds_Group, ds_Uom, ds_itmenabl As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Formload_t As Boolean
    Dim Itemid_t As Double, Sizeid_t As Double, Colorid_t As Double, Hsnaccodeid_t, Groupid_t As Double, Uomid_t, Rakeid_t, Categoryid_t As Double, Freeitemid As Double, HsnDescription_t As String
    Dim accountid_t As Double
    Dim Enablekey As Integer = 0
    Dim Format_t As String = ""
    Dim editflag As Boolean, Isalreadyexistflag_t As Boolean = False
    Dim index As Integer, Detlcnt_t As Integer, colindex_t As Integer, Rowindex_t As Integer, Gridrowcnt_t As Integer
    Dim Sqlstr As String, SizeSqlstr As String, Colorsqlstr_t As String, GroupSqlstr_t As String, UomSqlstr_t As String, HsnAccodestr_t, Path_t As String, Filtercolnmae_t As String
    Dim font1 As Font
    Dim Val_t As Integer
    Dim Fm As New Sun_Findfrm
    Dim ds_settings As New DataSet
    Dim da_settings As SqlDataAdapter
    Dim dscnt As Integer

    Private Sub frmitemmaster1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'GridView2.AllowUserToAddRows = False
            'GridView2.Rows.Add(1)
            'GridView1.VirtualMode = True
            Formload_t = True
            Call opnconn()
            Call dsopen()
            Call BindData()
            enabdisb("Ok")

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            '        Dim converter As System.ComponentModel.TypeConverter = _
            'System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            '        font1 = _
            ' CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            '        font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
            '        'font1 = New Font(Font_m, CType(Size_m, Single))

            '        txt_tamildes.Font = font1
            '        txt_uom.Font = font1

            '        For i = 0 To GridView1.Rows.Count - 1
            '            GridView1.Rows(i).DefaultCellStyle.Font = font1
            '        Next

            'GridView1.AutoResizeColumns() '''13-04-2017
            Cbo_itemtype.SelectedIndex = 0
            Cbo_itemtype.Enabled = False
            ChangeControlLocations()
            Formload_t = False
            txt_minstock.Text = Format(Val(txt_minstock.Text), "#######0")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ChangeControlLocations()
        Try
            Dim ds_settings As New DataSet
            Dim da_settings As SqlDataAdapter
            Dim Freeqtyvisible As Boolean

            Sqlstr = "SELECT ISNULL(NUMERICVALUE,0) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS ='FREE AND OFFER QTY LOCK'"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings)

            If ds_settings.Tables(0).Rows.Count > 0 Then
                If ds_settings.Tables(0).Rows(0).Item("numericvalue") = 1 Then
                    Freeqtyvisible = True
                Else
                    Freeqtyvisible = False
                End If
            Else
                Freeqtyvisible = False
            End If

            If Freeqtyvisible = True Then
                Lbl_lastmodified.Location = Lbl_remarks.Location
                Lbl_Time.Location = txt_remakrs.Location
                Lbl_remarks.Location = Lbl_freeitem.Location
                txt_remakrs.Location = txt_freeitem.Location
                txt_freeitem.Visible = False
                Lbl_freeitem.Visible = False
                Lbl_usr.Location = Lbl_freeqty.Location
                Lbl_user.Location = txt_freeqty.Location
                Lbl_freeqty.Visible = False
                txt_freeqty.Visible = False
                txt_forqty.Visible = False
                lbl_forqty.Visible = False
                txt_freeuom.Visible = False
                lbl_uom.Visible = False
                Chk_Inactive.Location = lbl_uom.Location
                Lbl_packweight.Visible = False
                txt_PkgWt.Visible = False
                Lbl_offerqtyadd.Visible = False
                Lbl_offerqtyless.Visible = False
                lbl_add.Visible = False
                lbl_less.Visible = False
                lbl_rsadd.Visible = False
                lbl_rsless.Visible = False
                txt_ofr1.Visible = False
                txt_ofr2.Visible = False
                txt_add.Visible = False
                txt_less.Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub HSNAccodefindfrm()
        Try
            Dim cgstperc As Double, sgstperc As Double, igstperc As Double

            HsnAccodestr_t = "Select HSNCODE, Masterid,DECRIPTION,CGSTPERC,SGSTPERC,IGSTPERC From HSNACCOUNTCODE_MASTER ORDER BY HSNCODE"
            cmd = New SqlCommand(HsnAccodestr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_hsnaccode = New DataSet
            ds_hsnaccode.Clear()
            da.Fill(ds_hsnaccode)

            If ds_hsnaccode.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("HSNCODE")
                Colheads.Add("HSN/Accounting Code")

                Fm.Frm_Width = 400
                Fm.Frm_Height = 300
                Fm.Frm_Left = 650
                Fm.Frm_Top = 150

                Fm.MainForm = New frm_itemmaster
                Fm.Active_ctlname = "txt_accountingcode"
                Csize.Add(375)
                tmppassstr = txt_accountingcode.Text
                Fm.EXECUTE(conn, ds_hsnaccode, VisibleCols, Colheads, Hsnaccodeid_t, "", False, Csize, "", False, False, "", tmppassstr, HsnAccodestr_t)
                txt_accountingcode.Text = Fm.VarNew
                Hsnaccodeid_t = Fm.VarNewid

                If Hsnaccodeid_t <> 0 Then
                    ds_hsnaccode.Tables(0).DefaultView.RowFilter = "MASTERID = " & Hsnaccodeid_t & " "
                    Dim index_t As Integer = ds_hsnaccode.Tables(0).Rows.IndexOf(ds_hsnaccode.Tables(0).DefaultView.Item(0).Row)
                    If index_t >= 0 Then
                        HsnDescription_t = ds_hsnaccode.Tables(0).Rows(index_t).Item("DECRIPTION").ToString
                        cgstperc = ds_hsnaccode.Tables(0).Rows(index_t).Item("CGSTPERC").ToString
                        sgstperc = ds_hsnaccode.Tables(0).Rows(index_t).Item("SGSTPERC").ToString
                        igstperc = ds_hsnaccode.Tables(0).Rows(index_t).Item("IGSTPERC").ToString
                        txt_hsndescription.Text = HsnDescription_t
                        Lbl_cgstsgst.Text = "CGST : " + Format(cgstperc, "#0.00").ToString + "% | SGST : " + Format(sgstperc, "#0.00").ToString + "% | IGST : " + Format(igstperc, "#0.00").ToString + "% "
                    End If
                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_itemcode_Click(sender As Object, e As EventArgs) Handles txt_itemcode.Click
        Dim cnt As Integer
        Sqlstr = "Select I.Itemid From Item_Master I  WHERE ITEMCODE ='" & Trim(txt_itemcode.Text) & "'"
        cmd = New SqlCommand(Sqlstr, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds)
        cnt = ds.Tables(0).Rows.Count

        If cnt > 0 Then
            Itemid_t = ds.Tables(0).Rows(0).Item("itemid")
            Call storechars(Itemid_t)
            editflag = True
        End If
    End Sub

    Private Sub txt_itemcode_GotFocus(sender As Object, e As EventArgs) Handles txt_itemcode.GotFocus
        txt_itemcode.BackColor = Color.Yellow
    End Sub

    Private Sub txt_itemcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_itemcode.KeyDown

        If e.KeyCode = Keys.Enter Then
            Dim cnt As Integer
            Sqlstr = "Select I.Itemid From Item_Master I  WHERE ITEMCODE ='" & Trim(txt_itemcode.Text) & "'"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)
            cnt = ds.Tables(0).Rows.Count

            If cnt > 0 Then
                Itemid_t = ds.Tables(0).Rows(0).Item("itemid")
                Call storechars(Itemid_t)
                editflag = True
            End If
            txt_ItemDes.Focus()
        End If
    End Sub

    Private Sub RecordFocus()
        Try
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
                '  If Not GridView1.CurrentCell Is Nothing Then
                If GridView1.Rows.Count - 1 > CurrRowindex_t Then
                    GridView1.Rows(CurrRowindex_t).Selected = True
                    GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
                End If
                'End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_itemcode_LostFocus(sender As Object, e As EventArgs) Handles txt_itemcode.LostFocus
        txt_itemcode.BackColor = Color.White
    End Sub

    Private Sub txt_ItemDes_GotFocus(sender As Object, e As EventArgs) Handles txt_ItemDes.GotFocus
        txt_ItemDes.BackColor = Color.Yellow
    End Sub

    Private Sub txt_ItemDes_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_ItemDes.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_tamildes.Focus()
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

            HsnAccodestr_t = "Select HSNCODE, Masterid,DECRIPTION From HSNACCOUNTCODE_MASTER ORDER BY HSNCODE"
            cmd = New SqlCommand(HsnAccodestr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_hsnaccode = New DataSet
            ds_hsnaccode.Clear()
            da.Fill(ds_hsnaccode)

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
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture),
                                   GridView1.DefaultCellStyle.Font,
                                   b,
                                   e.RowBounds.Location.X + 5,
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
            cmd.CommandText = "GET_ITEMMASTER_GRID"
            cmd.Parameters.Add("@Value", SqlDbType.Int).Value = Val_t
            cmd.Parameters.Add("@inactive", SqlDbType.Int).Value = IIf(CheckBox1.Checked, 0, 1)
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
                Itemid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Itemid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.ReadOnly = True
            '  GridView1.AutoResizeColumns() ''''13-04-2017

            Dim font As New Font(
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

            GridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

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

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            Dim converter As System.ComponentModel.TypeConverter =
    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            font1 =
     CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
            'font1 = New Font(Font_m, CType(Size_m, Single))

            txt_tamildes.Font = font1
            txt_uom.Font = font1

            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next
            'GridView1.AutoResizeColumns() ''''13-04-2017
            'GridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            Call RecordFocus()

            cmd = Nothing
            ds_utype = Nothing
            cmd = New SqlCommand("SELECT USERTYPE FROM USERS WHERE UID =" & Genuid & "", conn)
            cmd.CommandType = CommandType.Text
            da_utype = New SqlDataAdapter(cmd)
            ds_utype = New DataSet
            ds_utype.Clear()
            da_utype.Fill(ds_utype)

            If ds_utype.Tables(0).Rows.Count > 0 Then
                If LCase(ds_utype.Tables(0).Rows(0).Item("usertype").ToString) = LCase("Administrator") Then
                    txt_itemcode.Enabled = True
                Else
                    txt_itemcode.Enabled = False
                End If
            End If


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
                Lbl_usr.Visible = False
                Lbl_lastmodified.Visible = False
                Lbl_Time.Visible = False
                Lbl_user.Visible = False
            Else
                Lbl_usr.Visible = True
                Lbl_lastmodified.Visible = True
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
            txt_itemcode.Enabled = True
            Path_t = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal Itemid_v As Double = 0)
        Try
            Dim Decimal_t As Double
            Dim cgstperc As Double, sgstperc As Double, igstperc As Double

            ds1.Clear()
            ds1 = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_ITEMMASTER"
            cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_t
            ds1 = New DataSet
            da.Fill(ds1)

            Dim rowid_t As Integer
            rowid_t = ds1.Tables(0).Rows.Count
            Gridrowcnt_t = 0
            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1

            ' txt_itemcode.Enabled = False
            cmd = Nothing
            ds_utype = Nothing
            cmd = New SqlCommand("SELECT USERTYPE FROM USERS WHERE UID =" & Genuid & "", conn)
            cmd.CommandType = CommandType.Text
            da_utype = New SqlDataAdapter(cmd)
            ds_utype = New DataSet
            ds_utype.Clear()
            da_utype.Fill(ds_utype)

            If LCase(ds_utype.Tables(0).Rows(0).Item("usertype").ToString) = LCase("Administrator") Then
                txt_itemcode.Enabled = True
            Else
                txt_itemcode.Enabled = False
            End If

            txt_itemcode.Text = ds1.Tables(0).Rows(rowid_t).Item("ITEMCODE").ToString
            Itemid_t = ds1.Tables(0).Rows(rowid_t).Item("ITEMID").ToString
            cgstperc = ds1.Tables(0).Rows(rowid_t).Item("cgstperc").ToString
            sgstperc = ds1.Tables(0).Rows(rowid_t).Item("sgstperc").ToString
            igstperc = ds1.Tables(0).Rows(rowid_t).Item("igstperc").ToString

            Lbl_cgstsgst.Text = "CGST : " + Format(cgstperc, "#0.00").ToString + "% | SGST : " + Format(sgstperc, "#0.00").ToString + "% | IGST : " + Format(igstperc, "#0.00").ToString + "% "

            txt_ItemDes.Text = ds1.Tables(0).Rows(rowid_t).Item("ITEMDES").ToString
            txt_tamildes.Text = ds1.Tables(0).Rows(rowid_t).Item("ITEMTAMILDES").ToString
            Uomid_t = ds1.Tables(0).Rows(rowid_t).Item("UOMID")
            txt_tax.Text = ds1.Tables(0).Rows(rowid_t).Item("TAXPERC").ToString
            txt_profit.Text = ds1.Tables(0).Rows(rowid_t).Item("PROFITPERC")
            txt_costperc.Text = ds1.Tables(0).Rows(rowid_t).Item("COSTPRICE").ToString
            txt_mrprate.Text = ds1.Tables(0).Rows(rowid_t).Item("MRPRATE").ToString
            txt_selretpric.Text = ds1.Tables(0).Rows(rowid_t).Item("SELPRICERETAIL")
            txt_selsalpric.Text = ds1.Tables(0).Rows(rowid_t).Item("SELPRICEWHOLE")
            Groupid_t = ds1.Tables(0).Rows(rowid_t).Item("GROUPID").ToString
            Hsnaccodeid_t = ds1.Tables(0).Rows(rowid_t).Item("HSNID")
            txt_group.Text = ds1.Tables(0).Rows(rowid_t).Item("GROUPNAME").ToString
            If Val_t = 0 Then txt_uom.Text = ds1.Tables(0).Rows(rowid_t).Item("UOM").ToString
            If Val_t = 1 Then txt_uom.Text = ds1.Tables(0).Rows(rowid_t).Item("TAMILUOM").ToString
            Categoryid_t = ds1.Tables(0).Rows(rowid_t).Item("CATEGORYID")
            txt_category.Text = ds1.Tables(0).Rows(rowid_t).Item("CATEGORY").ToString
            Rakeid_t = ds1.Tables(0).Rows(rowid_t).Item("RAKEID")
            txt_rake.Text = ds1.Tables(0).Rows(rowid_t).Item("RAKE").ToString
            Lbl_Time.Text = ds1.Tables(0).Rows(rowid_t).Item("MODIFIEDTIME").ToString
            Lbl_user.Text = ds1.Tables(0).Rows(rowid_t).Item("UNAME").ToString
            Lbl_supllier.Text = ds1.Tables(0).Rows(rowid_t).Item("SUPPLIER").ToString
            txt_ofr1.Text = ds1.Tables(0).Rows(rowid_t).Item("OFFERADDQTY").ToString
            txt_ofr2.Text = ds1.Tables(0).Rows(rowid_t).Item("OFFERLESSQTY").ToString
            txt_add.Text = ds1.Tables(0).Rows(rowid_t).Item("ADDQTY").ToString
            txt_less.Text = ds1.Tables(0).Rows(rowid_t).Item("LESSQTY").ToString
            txt_minstock.Text = ds1.Tables(0).Rows(rowid_t).Item("MINSTOCK").ToString
            txt_accountingcode.Text = ds1.Tables(0).Rows(rowid_t).Item("HSNACCOUNTINGCODE").ToString
            txt_hsndescription.Text = ds1.Tables(0).Rows(rowid_t).Item("HSNDESCRIPTION").ToString
            'HSNACCOUNTINGCODE
            If Lbl_supllier.Text = "" Or Lbl_supllier.Text Is Nothing Then
                Lbl_supplier.Visible = False
            Else
                Lbl_supplier.Visible = True
            End If
            If ds1.Tables(0).Rows(rowid_t).Item("Inactive") = 0 Then
                Chk_Inactive.Checked = True
            Else
                Chk_Inactive.Checked = False
            End If

            txt_PkgWt.Text = ds1.Tables(0).Rows(rowid_t).Item("Pkgwt")
            Freeitemid = ds1.Tables(0).Rows(rowid_t).Item("FREEITEM")

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

            If Val_t = 1 Then
                txt_freeitem.Text = ds1.Tables(0).Rows(rowid_t).Item("FREEITEMTAMILDES").ToString
            Else
                txt_freeitem.Text = ds1.Tables(0).Rows(rowid_t).Item("FREEITEMDES").ToString
            End If

            Decimal_t = ds1.Tables(0).Rows(rowid_t).Item("FREEdecimal")
            txt_freeuom.Text = ds1.Tables(0).Rows(rowid_t).Item("FREEUOM").ToString
            txt_freeqty.Text = ds1.Tables(0).Rows(rowid_t).Item("FREEQTY")
            txt_forqty.Text = ds1.Tables(0).Rows(rowid_t).Item("FORQTY")
            txt_remakrs.Text = ds1.Tables(0).Rows(rowid_t).Item("REMARKS").ToString
            Cbo_itemtype.SelectedItem = ds1.Tables(0).Rows(rowid_t).Item("ITEMTYPE").ToString

            Format_t = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            Decimal_tt = ""
            For k = 1 To Decimal_t
                Decimal_tt = String.Concat(Decimal_tt, "0")
            Next

            If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
            txt_freeqty.Text = Format(Val(txt_freeqty.Text), Format_t)

            txt_selsalpric.BackColor = Color.White
            txt_minstock.Text = Format(Val(txt_minstock.Text), "#######0")

            'If Rowcnt > 0 Then
            '    GridView1.Rows(CurrRowindex_t).Selected = True
            '    GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
            'End If
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
            If GridView1.Rows.Count > 0 Then
                If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then Itemid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                Call storechars(Itemid_t)
            Else

            End If

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
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
            txt_itemcode.Focus()
            Lbl_usr.Visible = False
            Lbl_lastmodified.Visible = False
            Lbl_Time.Visible = False
            Lbl_user.Visible = False
            Cbo_itemtype.SelectedIndex = 0
            txt_itemcode.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_ItemDes_LostFocus(sender As Object, e As EventArgs) Handles txt_ItemDes.LostFocus
        txt_ItemDes.BackColor = Color.White
    End Sub

    Private Sub txt_tamildes_GotFocus(sender As Object, e As EventArgs) Handles txt_tamildes.GotFocus
        txt_tamildes.BackColor = Color.Yellow
    End Sub

    Private Sub txt_tamildes_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_tamildes.KeyDown
        'If Enablekey = 0 Then SendKeys.Send("%" & Chr(51))
        'Enablekey = Enablekey + 1
        'System.Windows.Forms.SendKeys.Send("%{F1" + "1" + "}")
        If e.KeyCode = Keys.Enter Then
            txt_accountingcode.Focus()
        End If
    End Sub

    Private Sub txt_tamildes_LostFocus(sender As Object, e As EventArgs) Handles txt_tamildes.LostFocus
        txt_tamildes.BackColor = Color.White
    End Sub

    Private Sub txt_uom_Click(sender As Object, e As EventArgs) Handles txt_uom.Click
        Call Uomfindfrm()
    End Sub

    Private Sub txt_uom_GotFocus(sender As Object, e As EventArgs) Handles txt_uom.GotFocus
        txt_uom.BackColor = Color.Yellow
    End Sub

    Private Sub txt_uom_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_uom.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Uomfindfrm()
            txt_costperc.Focus()
        End If
    End Sub

    Private Sub txt_uom_LostFocus(sender As Object, e As EventArgs) Handles txt_uom.LostFocus
        txt_uom.BackColor = Color.White
    End Sub

    Private Sub txt_costperc_Click(sender As Object, e As EventArgs) Handles txt_costperc.Click
        'txt_profit.Focus()
    End Sub

    Private Sub txt_costperc_GotFocus(sender As Object, e As EventArgs) Handles txt_costperc.GotFocus
        txt_costperc.BackColor = Color.Yellow
    End Sub

    Private Sub txt_costperc_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_costperc.KeyDown
        If Not IsNumeric(txt_costperc.Text) And Not txt_costperc.Text = "" Then
            If txt_costperc.Text.IndexOf("."c) = -1 Then txt_costperc.Text = ""
            txt_costperc.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_profit.Focus()
        End If
    End Sub

    Private Sub txt_costperc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_costperc.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_costperc_LostFocus(sender As Object, e As EventArgs) Handles txt_costperc.LostFocus
        txt_costperc.BackColor = Color.White
        txt_costperc.Text = Format(Val(txt_costperc.Text), "#######0.00")
    End Sub

    Private Sub txt_tax_GotFocus(sender As Object, e As EventArgs) Handles txt_tax.GotFocus
        txt_tax.BackColor = Color.Yellow
    End Sub

    Private Sub txt_tax_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_tax.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_profit.Focus()
        End If
    End Sub

    Private Sub txt_tax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_tax.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_tax_LostFocus(sender As Object, e As EventArgs) Handles txt_tax.LostFocus
        txt_tax.BackColor = Color.White
        txt_tax.Text = Format(Val(txt_tax.Text), "#######0.00")
    End Sub

    Private Sub txt_profit_Click(sender As Object, e As EventArgs) Handles txt_profit.Click
        'txt_selretpric.Focus()
    End Sub

    Private Sub txt_profit_GotFocus(sender As Object, e As EventArgs) Handles txt_profit.GotFocus
        txt_profit.BackColor = Color.Yellow
    End Sub

    Private Sub txt_profit_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_profit.KeyDown
        If Not IsNumeric(txt_profit.Text) And Not txt_profit.Text = "" Then
            If txt_profit.Text.IndexOf("."c) = -1 Then txt_profit.Text = ""
            txt_profit.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_selretpric.Focus()
        End If
    End Sub

    Private Sub txt_profit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_profit.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_profit_LostFocus(sender As Object, e As EventArgs) Handles txt_profit.LostFocus
        txt_profit.BackColor = Color.White
        txt_profit.Text = Format(Val(txt_profit.Text), "#######0.00")
    End Sub

    Private Sub txt_mrprate_GotFocus(sender As Object, e As EventArgs) Handles txt_mrprate.GotFocus
        txt_mrprate.BackColor = Color.Yellow
    End Sub

    Private Sub txt_mrprate_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_mrprate.KeyDown
        If Not IsNumeric(txt_mrprate.Text) And Not txt_mrprate.Text = "" Then
            If txt_mrprate.Text.IndexOf("."c) = -1 Then txt_mrprate.Text = ""
            txt_mrprate.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_selsalpric.Focus()
        End If
    End Sub

    Private Sub txt_mrprate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_mrprate.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_mrprate_LostFocus(sender As Object, e As EventArgs) Handles txt_mrprate.LostFocus
        txt_mrprate.BackColor = Color.White
        txt_mrprate.Text = Format(Val(txt_mrprate.Text), "#######0.00")
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
            txt_mrprate.Focus()
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

    Private Sub txt_selsalpric_GotFocus(sender As Object, e As EventArgs) Handles txt_selsalpric.GotFocus
        txt_selsalpric.BackColor = Color.Yellow
    End Sub

    Private Sub txt_selsalpric_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_selsalpric.KeyDown
        If Not IsNumeric(txt_selsalpric.Text) And Not txt_selsalpric.Text = "" Then
            If txt_selsalpric.Text.IndexOf("."c) = -1 Then txt_selsalpric.Text = ""
            txt_selsalpric.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_group.Focus()
        End If
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
            If txt_itemcode.Text = "" Then
                MsgBox("Item Code should not be empty.")
                txt_itemcode.Focus()
            ElseIf txt_ItemDes.Text = "" Then
                MsgBox("Item Description should not be empty.")
                txt_ItemDes.Focus()
            ElseIf txt_uom.Text = "" Then
                MsgBox("Uom should not be empty.")
                txt_uom.Focus()
            ElseIf txt_group.Text = "" Then
                MsgBox("Group should not be empty.")
                txt_group.Focus()
            ElseIf txt_category.Text = "" Then
                MsgBox("Category should not be empty.")
                txt_category.Focus()
            ElseIf txt_rake.Text = "" Then
                MsgBox("Rake should not be empty.")
                txt_rake.Focus()
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
                Call GendelItem(Itemid_t)
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
            opnconn()

            Itemid_t = GensaveItem(IIf(editflag_t, 1, 0), Itemid_t, Trim(txt_itemcode.Text), txt_ItemDes.Text, txt_tamildes.Text, Uomid_t, Val(txt_tax.Text), txt_profit.Text,
                                   txt_costperc.Text, Val(txt_mrprate.Text), Val(txt_selretpric.Text), Val(txt_selsalpric.Text), Groupid_t, Categoryid_t, Rakeid_t,
                                  Freeitemid, Val(txt_freeqty.Text), Val(txt_forqty.Text), txt_remakrs.Text, Cbo_itemtype.SelectedItem,
                                  IIf(Chk_Inactive.Checked = True, 0, 1), txt_PkgWt.Text, Val(txt_ofr1.Text), Val(txt_ofr2.Text),
                                  Val(txt_add.Text), Val(txt_less.Text), Trim(txt_accountingcode.Text), Hsnaccodeid_t, Val(txt_minstock.Text))
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
            Fm.Frm_Left = 948
            Fm.Frm_Top = 402

            Fm.MainForm = New frm_itemmaster
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

    Private Sub Uomfindfrm()
        Try
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
                UomSqlstr_t = "Select Uom, Masterid From Uom_Master Order By Uom"
            Else
                UomSqlstr_t = "Select TAMILUom AS UOM, Masterid From Uom_Master Order By TAMILUom"
            End If

            cmd = New SqlCommand(UomSqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_Uom = New DataSet
            ds_Uom.Clear()
            da.Fill(ds_Uom)

            If ds_Uom.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("Uom")
                Colheads.Add("Uom")
                Fm.Frm_Width = 300
                Fm.Frm_Height = 300
                Fm.Frm_Left = 949
                Fm.Frm_Top = 280

                Fm.MainForm = New frm_itemmaster
                Fm.Active_ctlname = "txt_Uom"
                Csize.Add(250)
                tmppassstr = txt_uom.Text
                Fm.EXECUTE(conn, ds_Uom, VisibleCols, Colheads, Uomid_t, "", True, Csize, "", False, False, "", tmppassstr, UomSqlstr_t)
                txt_uom.Text = Fm.VarNew
                Uomid_t = Fm.VarNewid

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Categoryfindfrm()
        Try
            VisibleCols.Add("Category")
            Colheads.Add("Category")
            Fm.Frm_Width = 300
            Fm.Frm_Height = 300
            Fm.Frm_Left = 948
            Fm.Frm_Top = 427

            Fm.MainForm = New frm_itemmaster
            Fm.Active_ctlname = "txt_category"
            Csize.Add(250)
            tmppassstr = txt_category.Text
            Fm.EXECUTE(conn, ds_category, VisibleCols, Colheads, Categoryid_t, "", True, Csize, "", False, False, "", tmppassstr)
            txt_category.Text = Fm.VarNew
            Categoryid_t = Fm.VarNewid

            VisibleCols.Remove(1)
            Colheads.Remove(1)
            Csize.Remove(1)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Freeitemfindfrm()
        Try
            Dim ds_freeuom As New DataSet

            Dim Sqlstr_t As String

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

            If Val_t = 1 Then
                Sqlstr_t = "Select ISNULL(itemtamilDES,'') AS itemDES, itemid From item_Master Order By itemtamilDES "
            Else
                Sqlstr_t = "Select ISNULL(itemDES,'') AS itemDES, itemid From item_Master Order By itemDES "
            End If

            cmd = New SqlCommand(Sqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da_freeitem = New SqlDataAdapter(cmd)
            ds_freeitem = New DataSet
            ds_freeitem.Clear()
            da_freeitem.Fill(ds_freeitem)

            If ds_freeitem.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("itemDES")
                Colheads.Add("Item Description")
                Fm.Frm_Width = 400
                Fm.Frm_Height = 300
                Fm.Frm_Left = 824
                Fm.Frm_Top = 432

                Fm.MainForm = New frm_itemmaster
                Fm.Active_ctlname = "txt_freeitem"
                Csize.Add(350)
                tmppassstr = txt_freeitem.Text
                Fm.EXECUTE(conn, ds_freeitem, VisibleCols, Colheads, Freeitemid, "", True, Csize, "", False, False, "", tmppassstr)
                txt_freeitem.Text = Fm.VarNew
                Freeitemid = Fm.VarNewid

                ds_freeuom.Clear()
                Sqlstr_t = "Select ISNULL(im.itemDES,'') AS itemDES, im.itemid,isnull(um.uom,'') as uom,isnull(um.noofdecimal,0) as noofdecimal From item_Master im join uom_master um on um.masterid =im.uomid where im.itemid =" & Freeitemid & ""
                cmd = New SqlCommand(Sqlstr_t, conn)
                cmd.CommandType = CommandType.Text
                da_freeitem = New SqlDataAdapter(cmd)
                ds_freeuom = New DataSet
                ds_freeuom.Clear()
                da_freeitem.Fill(ds_freeuom)

                If ds_freeuom.Tables(0).Rows.Count > 0 Then
                    txt_freeuom.Text = ds_freeuom.Tables(0).Rows(0).Item("uom").ToString

                    Dim Decimal_t As Double = 0
                    Dim Decimal_tt As String = ""
                    Dim k As Integer = 0

                    Decimal_t = ds_freeuom.Tables(0).Rows(0).Item("noofdecimal")
                    Format_t = ""
                    Decimal_tt = ""
                    For k = 1 To Decimal_t
                        Decimal_tt = String.Concat(Decimal_tt, "0")
                    Next

                    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                    txt_freeqty.Text = Format(Val(txt_freeqty.Text), Format_t)
                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Rakefindfrm()
        Try
            VisibleCols.Add("Rake")
            Colheads.Add("Rake")
            Fm.Frm_Width = 300
            Fm.Frm_Height = 300
            Fm.Frm_Left = 941
            Fm.Frm_Top = 447

            Fm.MainForm = New frm_itemmaster
            Fm.Active_ctlname = "txt_rake"
            Csize.Add(250)
            tmppassstr = txt_rake.Text
            Fm.EXECUTE(conn, ds_rake, VisibleCols, Colheads, Rakeid_t, "", True, Csize, "", False, False, "", tmppassstr)
            txt_rake.Text = Fm.VarNew
            Rakeid_t = Fm.VarNewid

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
            txt_category.Focus()
        End If
    End Sub

    Private Sub txt_group_LostFocus(sender As Object, e As EventArgs) Handles txt_group.LostFocus
        txt_group.BackColor = Color.White
    End Sub

    Private Sub txt_category_Click(sender As Object, e As EventArgs) Handles txt_category.Click
        Call Categoryfindfrm()
    End Sub

    Private Sub txt_category_GotFocus(sender As Object, e As EventArgs) Handles txt_category.GotFocus
        txt_category.BackColor = Color.Yellow
    End Sub

    Private Sub txt_category_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_category.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Categoryfindfrm()
            txt_rake.Focus()
        End If
    End Sub

    Private Sub txt_category_LostFocus(sender As Object, e As EventArgs) Handles txt_category.LostFocus
        txt_category.BackColor = Color.White
    End Sub

    Private Sub txt_rake_Click(sender As Object, e As EventArgs) Handles txt_rake.Click
        Call Rakefindfrm()
    End Sub

    Private Sub txt_rake_GotFocus(sender As Object, e As EventArgs) Handles txt_rake.GotFocus
        txt_rake.BackColor = Color.Yellow
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

    Private Sub txt_rake_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_rake.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Rakefindfrm()
            If txt_freeitem.Visible = True Then
                txt_freeitem.Focus()
            Else
                txt_remakrs.Focus()
            End If

        End If
    End Sub

    Private Sub txt_rake_LostFocus(sender As Object, e As EventArgs) Handles txt_rake.LostFocus
        txt_rake.BackColor = Color.White
        'txt_rake.Text = Format(Val(txt_rake.Text), "#######0")
    End Sub

    Private Sub txt_freeitem_Click(sender As Object, e As EventArgs) Handles txt_freeitem.Click
        If txt_freeitem.Text <> "" Then
            Freeitemfindfrm()
        Else
            Freeitemid = 0
        End If
    End Sub

    Private Sub txt_freeitem_GotFocus(sender As Object, e As EventArgs) Handles txt_freeitem.GotFocus
        txt_freeitem.BackColor = Color.Yellow
    End Sub

    Private Sub txt_freeitem_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_freeitem.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txt_freeitem.Text <> "" Then
                Freeitemfindfrm()
            Else
                Freeitemid = 0
            End If
            txt_freeqty.Focus()
        End If
    End Sub

    Private Sub txt_freeitem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_freeitem.KeyPress
        'If e.KeyChar = "."c Then
        '    e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        'ElseIf e.KeyChar <> ControlChars.Back Then
        '    e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        'End If
    End Sub

    Private Sub txt_freeitem_LostFocus(sender As Object, e As EventArgs) Handles txt_freeitem.LostFocus
        txt_freeitem.BackColor = Color.White
        '   txt_freeitem.Text = Format(Val(txt_freeitem.Text), "#######0")
    End Sub

    Private Sub txt_freeqty_GotFocus(sender As Object, e As EventArgs) Handles txt_freeqty.GotFocus
        txt_freeqty.BackColor = Color.Yellow
    End Sub

    Private Sub txt_freeqty_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_freeqty.KeyDown
        If Not IsNumeric(txt_freeqty.Text) And Not txt_freeqty.Text = "" Then
            If txt_freeqty.Text.IndexOf("."c) = -1 Then txt_freeqty.Text = ""
            txt_forqty.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_forqty.Focus()
        End If
    End Sub

    Private Sub txt_freeqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_freeqty.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_freeqty_LostFocus(sender As Object, e As EventArgs) Handles txt_freeqty.LostFocus
        txt_freeqty.BackColor = Color.White
        txt_freeqty.Text = Format(Val(txt_freeqty.Text), Format_t)
    End Sub

    Private Sub txt_forqty_GotFocus(sender As Object, e As EventArgs) Handles txt_forqty.GotFocus
        txt_forqty.BackColor = Color.Yellow
    End Sub

    Private Sub txt_forqty_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_forqty.KeyDown
        If Not IsNumeric(txt_forqty.Text) And Not txt_forqty.Text = "" Then
            If txt_forqty.Text.IndexOf("."c) = -1 Then txt_forqty.Text = ""
            txt_forqty.Focus()
        End If

        If e.KeyCode = Keys.Enter Then
            txt_remakrs.Focus()
        End If
    End Sub

    Private Sub txt_forqty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_forqty.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_forqty_LostFocus(sender As Object, e As EventArgs) Handles txt_forqty.LostFocus
        txt_forqty.BackColor = Color.White
        txt_forqty.Text = Format(Val(txt_forqty.Text), "#######0")
    End Sub

    Private Sub txt_remakrs_GotFocus(sender As Object, e As EventArgs) Handles txt_remakrs.GotFocus
        txt_remakrs.BackColor = Color.Yellow
    End Sub

    Private Sub txt_remakrs_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_remakrs.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txt_PkgWt.Visible = True Then txt_PkgWt.Focus() Else cmdok.Focus()
        End If
    End Sub

    Private Sub txt_remakrs_LostFocus(sender As Object, e As EventArgs) Handles txt_remakrs.LostFocus
        txt_remakrs.BackColor = Color.White
    End Sub

    Private Sub txt_selsalpric_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_selsalpric.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub Cbo_itemtype_KeyDown(sender As Object, e As KeyEventArgs) Handles Cbo_itemtype.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
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

            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next

            'GridView1.AutoResizeColumns() '''14-03-2017
            GridView1.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cbo_itemtype_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cbo_itemtype.KeyPress
        e.Handled = True
        If LCase(e.KeyChar) = LCase("r") Then
            Cbo_itemtype.SelectedIndex = 0
        End If
        If LCase(e.KeyChar) = LCase("w") Then
            Cbo_itemtype.SelectedIndex = 1
        End If
        If LCase(e.KeyChar) = LCase("b") Then
            Cbo_itemtype.SelectedIndex = 2
        End If
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

    Private Sub txt_selsalpric_LostFocus(sender As Object, e As EventArgs) Handles txt_selsalpric.LostFocus
        txt_selsalpric.BackColor = Color.White
        txt_selsalpric.Text = Format(Val(txt_selsalpric.Text), "#######0.00")
    End Sub

    Private Sub txt_ofr1_GotFocus(sender As Object, e As EventArgs) Handles txt_ofr1.GotFocus
        txt_ofr1.BackColor = Color.Yellow
    End Sub

    Private Sub txt_ofr1_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_ofr1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_add.Focus()
        End If
    End Sub

    Private Sub txt_ofr1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_ofr1.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_ofr1_LostFocus(sender As Object, e As EventArgs) Handles txt_ofr1.LostFocus
        txt_ofr1.BackColor = Color.White
        txt_ofr1.Text = Format(Val(txt_ofr1.Text), "#######0")
    End Sub

    Private Sub txt_ofr2_GotFocus(sender As Object, e As EventArgs) Handles txt_ofr2.GotFocus
        txt_ofr2.BackColor = Color.Yellow
    End Sub

    Private Sub txt_ofr2_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_ofr2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_less.Focus()
        End If
    End Sub

    Private Sub txt_ofr2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_ofr2.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_ofr2_LostFocus(sender As Object, e As EventArgs) Handles txt_ofr2.LostFocus
        txt_ofr2.BackColor = Color.White
        txt_ofr2.Text = Format(Val(txt_ofr2.Text), "#######0")
    End Sub

    Private Sub txt_add_GotFocus(sender As Object, e As EventArgs) Handles txt_add.GotFocus
        txt_add.BackColor = Color.Yellow
    End Sub

    Private Sub txt_add_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_add.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_ofr2.Focus()
        End If
    End Sub

    Private Sub txt_add_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_add.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_add_LostFocus(sender As Object, e As EventArgs) Handles txt_add.LostFocus
        txt_add.BackColor = Color.White
        txt_add.Text = Format(Val(txt_add.Text), "#######0.00")
    End Sub

    Private Sub txt_less_GotFocus(sender As Object, e As EventArgs) Handles txt_less.GotFocus
        txt_less.BackColor = Color.Yellow
    End Sub

    Private Sub txt_less_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_less.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_minstock.Focus()
            'cmdok.Focus()
        End If
    End Sub

    Private Sub txt_less_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_less.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_less_LostFocus(sender As Object, e As EventArgs) Handles txt_less.LostFocus
        txt_less.BackColor = Color.White
        txt_less.Text = Format(Val(txt_less.Text), "#######0.00")
    End Sub

    Private Sub txt_less_TextChanged(sender As Object, e As EventArgs) Handles txt_less.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If GridView1.Enabled = True Then
                'If CheckBox1.Text = "Show InActive" a Then
                ' CheckBox1.Text = "Show Active"
                Call BindData()
                'Else
                '    CheckBox1.Text = "Show InActive"
                '     Call BindData()
                ' End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_accountingcode_Click(sender As Object, e As EventArgs) Handles txt_accountingcode.Click
        HSNAccodefindfrm()
    End Sub

    Private Sub txt_accountingcode_GotFocus(sender As Object, e As EventArgs) Handles txt_accountingcode.GotFocus
        txt_accountingcode.BackColor = Color.Yellow
    End Sub

    Private Sub txt_accountingcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_accountingcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            HSNAccodefindfrm()
            txt_uom.Focus()
        End If
    End Sub

    Private Sub txt_accountingcode_LostFocus(sender As Object, e As EventArgs) Handles txt_accountingcode.LostFocus
        txt_accountingcode.BackColor = Color.White
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FrmInvReg = New frm_hsnaccode
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()

        Dim cgstperc As Double, sgstperc As Double, igstperc As Double
        HsnAccodestr_t = "Select HSNCODE, Masterid,DECRIPTION,CGSTPERC,SGSTPERC,IGSTPERC From HSNACCOUNTCODE_MASTER WHERE MASTERID = " & Hsnaccodeid_t & " ORDER BY HSNCODE"
        cmd = New SqlCommand(HsnAccodestr_t, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds_hsnaccode = New DataSet
        ds_hsnaccode.Clear()
        da.Fill(ds_hsnaccode)

        If ds_hsnaccode.Tables(0).Rows.Count > 0 Then
            cgstperc = ds_hsnaccode.Tables(0).Rows(0).Item("CGSTPERC").ToString
            sgstperc = ds_hsnaccode.Tables(0).Rows(0).Item("SGSTPERC").ToString
            igstperc = ds_hsnaccode.Tables(0).Rows(0).Item("IGSTPERC").ToString
            txt_hsndescription.Text = HsnDescription_t
            Lbl_cgstsgst.Text = "CGST : " + Format(cgstperc, "#0.00").ToString + "% | SGST : " + Format(sgstperc, "#0.00").ToString + "% | IGST : " + Format(igstperc, "#0.00").ToString + "% "
        End If

    End Sub

    Private Sub txt_profit_TextChanged(sender As Object, e As EventArgs) Handles txt_profit.TextChanged
        Dim tot As Integer
        If Formload_t = False Then
            If Val(txt_costperc.Text) <> 0 Then txt_selretpric.Text = Val(txt_costperc.Text) + Val(txt_costperc.Text) * (Val(txt_profit.Text) / 100)
        End If
    End Sub

    Private Sub txt_costperc_TextChanged(sender As Object, e As EventArgs) Handles txt_costperc.TextChanged
        '  Dim tot As Integer
        '  If Val(txt_costperc.Text) <> 0 And editflag = False Then txt_selretpric.Text = Val(txt_costperc.Text) + Val(txt_costperc.Text) * (Val(txt_profit.Text) / 100)
    End Sub

    Private Sub txt_minstock_GotFocus(sender As Object, e As EventArgs) Handles txt_minstock.GotFocus
        txt_minstock.BackColor = Color.Yellow
    End Sub

    Private Sub txt_minstock_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_minstock.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_minstock_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_minstock.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_minstock_LostFocus(sender As Object, e As EventArgs) Handles txt_minstock.LostFocus
        txt_minstock.BackColor = Color.White
        txt_minstock.Text = Format(Val(txt_minstock.Text), "#######0")
    End Sub

End Class