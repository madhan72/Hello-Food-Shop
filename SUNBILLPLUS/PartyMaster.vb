Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports FindForm_App
Public Class PartyMaster
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da_line, da1 As SqlDataAdapter
    Dim Rowindex_t As Integer
    Dim CurrRowindex_t As Integer, Events_t As String = "", Rowcnt As Integer
    Dim ds, ds1, ds_procs, tmpds, ds_state, ds_line, ds_trans, ds_book, ds_price, ds_saleex, ds_agent, ds_pricelst, ds_acdesc, ds_Getptyp, ds_prospricelst, ds_CheckCode, ds_Discount As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim Partyid_t As Double, Stateid_t As Double, Transid_t As Double, Trans2id_t As Double, Bookingplaceid_t As Double, Agentid_t As Double, Lineid_t As Double
    Dim accountid_t As Double, Salesexecid_t As Double, Pricelistid_t As Double, ProsPricelistid_t As Double
    Dim editflag As Boolean
    Dim index As Integer, Colindex_t As Integer
    Dim Stateid As Double
    Dim Sqlstr As String, Filtercolnmae_t As String, Defaultprocess_t As String, TransSqlstr As String, BookSqlstr As String, SalexeSqlstr As String
    Const Process = "Party"
    Dim fm As New Sun_Findfrm
    Dim font1 As Font
    Dim PARTY_DISCOUNT As Integer

    Private Sub Load_combotype()
        Try
            Dim proscnt_t As Integer
            Dim tmp_t As String = "CUSTOMER"
            proscnt_t = ds_procs.Tables(0).Rows.Count
            If proscnt_t > 0 Then
                For i = 0 To proscnt_t - 1
                    cbo_Process.Items.Add(ds_procs.Tables(0).Rows(i).Item("Process").ToString)
                Next
                'cbo_Process.Text = (ds_procs.Tables(0).Rows(0).Item("Process").ToString)

                Dim cnt As Integer
                ds_procs.Tables(0).DefaultView.RowFilter = "Process = '" & tmp_t & "' "
                cnt = ds_procs.Tables(0).DefaultView.Count
                If cnt > 0 Then
                    index = ds_procs.Tables(0).Rows.IndexOf(ds_procs.Tables(0).DefaultView.Item(0).Row)
                    cbo_Process.Text = ds_procs.Tables(0).Rows(index).Item("Process").ToString
                Else
                    cbo_Process.Text = (ds_procs.Tables(0).Rows(0).Item("Process").ToString)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub dsopen()
        Try
            Dim ds_settings As New DataSet
            Dim da_settings As SqlDataAdapter

            Sqlstr = "Select Process, Seqno From Party_Process where visible =0 or visible is null Order By Process"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_procs = New DataSet
            ds_procs.Clear()
            da.Fill(ds_procs)

            Sqlstr = "Select State, Masterid,code From State_Master Order By State"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_state = New DataSet
            ds_state.Clear()
            da.Fill(ds_state)

            Sqlstr = "Select Ptyname, Ptycode From Party Where Ptytype='AGENT' Order By Ptyname"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_agent = New DataSet
            ds_agent.Clear()
            da.Fill(ds_agent)

            SalexeSqlstr = "Select Salesexecutive, Masterid From Salesexec_Master Order By Salesexecutive"
            cmd = New SqlCommand(SalexeSqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_saleex = New DataSet
            ds_saleex.Clear()
            da.Fill(ds_saleex)

            Sqlstr = "SELECT ISNULL(NUMERICVALUE,0) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS ='PARTY_DISCOUNT'"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings)

            If ds_settings.Tables(0).Rows.Count > 0 Then
                PARTY_DISCOUNT = ds_settings.Tables(0).Rows(0).Item("NUMERICVALUE")
                If PARTY_DISCOUNT = 1 Then
                    Btn_Discount.Visible = False
                Else
                End If
            Else
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BindData(Optional ByVal strSearchString As String = "")
        Try
            Call clearchars()
            Sqlstr = "Select P.Ptycode, P.Ptyname,Sm.State From Party P " _
                & " Left Join State_Master Sm On Sm.Masterid=P.Stateid " _
                & " Where P.Ptytype='" & cbo_Process.Text & "' order by P.Ptyname"
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
            '  GridView1.Columns(2).ReadOnly = True

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

            Filtercolnmae_t = "Ptyname"
            Colindex_t = 1
            If GridView1.Rows.Count > 0 Then
                If GridView1.CurrentCell Is Nothing Then
                    Partyid_t = GridView1.Item(0, 0).Value
                Else
                    Partyid_t = GridView1.Item(0, GridView1.CurrentCell.RowIndex).Value
                End If

                Call storechars(Partyid_t)
            End If

            GridView1.Columns(0).Visible = False
            GridView1.Columns(1).Width = 250
            GridView1.Columns(1).HeaderText = "Name"
            GridView1.Columns(2).Width = 120
            ' GridView1.Columns(3).Width = 150

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
            'For i = 0 To cnt - 1
            '    GridView1.Columns(i).DefaultCellStyle.Font = font
            'Next

            GridView1.ReadOnly = True

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

            Dim converter As System.ComponentModel.TypeConverter = _
System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            font1 = _
     CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))

            txt_partyname.Font = font1
            txt_address1.Font = font1
            txt_address2.Font = font1
            txt_address3.Font = font1
            txt_address4.Font = font1
            txt_place.Font = font1

            GridView1.DefaultCellStyle.Font = font1

            GridView1.AutoResizeColumns()

            Call RecordFocus()

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

    Private Sub enabdisb(ByVal Val As String)
        Try
            If UCase(Val) = "ADD" Or UCase(Val) = "EDIT" Then
                'GroupBox1.Enabled = False
                'GroupBox2.Enabled = True
                GroupBox3.Enabled = True
                GridView1.Enabled = False
                GroupBox1.Visible = False
                GroupBox2.Visible = True
            End If

            If UCase(Val) = "OK" Or UCase(Val) = "CANCEL" Then
                'GroupBox1.Enabled = True
                'GroupBox2.Enabled = False
                GroupBox3.Enabled = False
                GridView1.Enabled = True
                GroupBox1.Visible = True
                GroupBox2.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes1()

            If cbo_Process.Text <> "" And LCase(cbo_Process.Text) = LCase("customer") Then
                'txt_code.Text = AutoNum_Party(cbo_Process.Text)
                txt_code.Enabled = True
                txt_code.Focus()
            ElseIf cbo_Process.Text <> "" Then
                txt_code.Enabled = False
                txt_code.Text = AutoNum_Party(cbo_Process.Text)
                txt_partyname.Focus()
            End If

            accountid_t = 0
            Partyid_t = 0
            Agentid_t = 0
            Stateid_t = 0
            Salesexecid_t = 0

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

    Private Sub storechars(Optional ByVal Partyid_v As Double = 0)
        Try
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.StoredProcedure
            da = New SqlDataAdapter(cmd)
            cmd.CommandText = "GET_PARTY"
            cmd.Parameters.Add("@PTYCODE", SqlDbType.Float).Value = Partyid_v
            ds1 = New DataSet
            ds1.Clear()
            da.Fill(ds1)

            Dim rowid_t As Integer
            Call clearchars()
            rowid_t = ds1.Tables(0).Rows.Count

            If rowid_t <= 0 Then Exit Sub
            rowid_t = rowid_t - 1
            Partyid_t = ds1.Tables(0).Rows(rowid_t).Item("Ptycode")
            txt_partyname.Text = ds1.Tables(0).Rows(rowid_t).Item("Ptyname").ToString
            txt_code.Text = ds1.Tables(0).Rows(rowid_t).Item("Code").ToString
            txt_address1.Text = ds1.Tables(0).Rows(rowid_t).Item("Add1").ToString
            txt_address2.Text = ds1.Tables(0).Rows(rowid_t).Item("Add2").ToString
            txt_address3.Text = ds1.Tables(0).Rows(rowid_t).Item("Add3").ToString
            txt_address4.Text = ds1.Tables(0).Rows(rowid_t).Item("Add4").ToString
            Txt_creditlimit.Text = ds1.Tables(0).Rows(rowid_t).Item("CREDITLIMIT")
            txt_place.Text = ds1.Tables(0).Rows(rowid_t).Item("Place").ToString
            txt_state.Text = ds1.Tables(0).Rows(rowid_t).Item("State").ToString
            Stateid_t = ds1.Tables(0).Rows(rowid_t).Item("Stateid").ToString
            txt_line.Text = ds1.Tables(0).Rows(rowid_t).Item("LINE").ToString
            Lineid_t = ds1.Tables(0).Rows(rowid_t).Item("LINEID")
            txt_tin.Text = ds1.Tables(0).Rows(rowid_t).Item("Tin").ToString
            txt_cst.Text = ds1.Tables(0).Rows(rowid_t).Item("Cst").ToString
            txt_gstin.Text = ds1.Tables(0).Rows(rowid_t).Item("GSTIN").ToString
            txt_statecode.Text = ds1.Tables(0).Rows(rowid_t).Item("STATECODE").ToString

            If ds1.Tables(0).Rows(rowid_t).Item("Cstdate").ToString = "" Then
                DTP_Cstdate.Checked = False
            Else
                DTP_Cstdate.Checked = True
                DTP_Cstdate.Value = ds1.Tables(0).Rows(rowid_t).Item("Cstdate").ToString
            End If

            txt_contact.Text = ds1.Tables(0).Rows(rowid_t).Item("Contact").ToString
            txt_phone.Text = ds1.Tables(0).Rows(rowid_t).Item("Phone1").ToString
            txt_phone1.Text = ds1.Tables(0).Rows(rowid_t).Item("Phone2").ToString
            txt_cell.Text = ds1.Tables(0).Rows(rowid_t).Item("Cell").ToString
            txt_email.Text = ds1.Tables(0).Rows(rowid_t).Item("Email").ToString

            Dim ds_st As New DataSet
            Sqlstr = "Select State, Masterid From State_Master where state='tamilnadu' Order By State"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_st = New DataSet
            ds_st.Clear()
            da.Fill(ds_st)

            If ds_st.Tables(0).Rows.Count > 0 Then
                Stateid_t = ds_st.Tables(0).Rows(0).Item("masterid")
                If Stateid_t = 0 Or txt_state.Text = "" Then txt_state.Text = ds_st.Tables(0).Rows(0).Item("state")
            End If

            txt_partyname.BackColor = Color.White
            txt_address1.BackColor = Color.White
            txt_address2.BackColor = Color.White
            txt_address3.BackColor = Color.White
            txt_address4.BackColor = Color.White
            txt_phone.BackColor = Color.White
            txt_cst.BackColor = Color.White
            txt_tin.BackColor = Color.White
            txt_code.BackColor = Color.White
            txt_email.BackColor = Color.White

            'If Rowcnt > 0 Then
            '    GridView1.Rows(CurrRowindex_t).Selected = True
            '    GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
            'End If

            If Btn_Discount.Visible = True Then
                'Dim ds_Disc As New DataSet
                'Sqlstr = "Select * FROM Discount_detail where partyid = " & Partyid_v & ""
                'cmd = New SqlCommand(Sqlstr, conn)
                'cmd.CommandType = CommandType.Text
                'da = New SqlDataAdapter(cmd)
                'ds_Disc = New DataSet
                'ds_Disc.Clear()
                'da.Fill(ds_Disc)

                'cmd = New SqlCommand(Sqlstr, conn)
                'cmd.CommandType = CommandType.StoredProcedure
                'da = New SqlDataAdapter(cmd)
                'If ds_Disc.Tables(0).Rows.Count > 0 Then
                '    cmd.CommandText = "GET_PARTYDISCOUNT"
                'Else
                '    cmd.CommandText = "GET_ITEMDETAILS_DISCOUNT"
                'End If

                'cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
                'ds_Discount = New DataSet
                'ds_Discount.Clear()
                'da.Fill(ds_Discount)

                'If ds_Discount.Tables(0).Rows.Count > 0 Then

                '    'Dim tables As DataTableCollection = ds_Discount.Tables
                '    'Dim view1 As New DataView(tables(0))
                '    '.bs.DataSource = view1

                '    With Frm_DiscountDetail
                '        .GridView1.DataSource = Nothing
                '        .GridView1.Rows.Clear()
                '        .GridView1.Rows.Add(ds_Discount.Tables(0).Rows.Count)

                '        For i = 0 To ds_Discount.Tables(0).Rows.Count - 1
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Itemid).Value = ds_Discount.Tables(0).Rows(i).Item("Itemid")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Code).Value = ds_Discount.Tables(0).Rows(i).Item("Itemcode")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Itemname).Value = ds_Discount.Tables(0).Rows(i).Item("Itemname")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Uom).Value = ds_Discount.Tables(0).Rows(i).Item("UOM")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Decimal).Value = ds_Discount.Tables(0).Rows(i).Item("Decimal")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_SelRate).Value = ds_Discount.Tables(0).Rows(i).Item("Selrate")
                '            .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Discount).Value = ds_Discount.Tables(0).Rows(i).Item("Discount")
                '        Next
                '    End With
                'End If
            End If
          

            GridView1.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub saveproc(ByVal editflag_t As Boolean)
        Try
            Dim Code_t As String
            Dim Itemid_t, Discount_t As Double

            Code_t = txt_code.Text

            If editflag = False Then
                txt_code.Text = AutoNum_Party(cbo_Process.Text, True)
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            If LCase(cbo_Process.Text) = LCase("Customer") Then
                txt_code.Text = Code_t
            End If

            Partyid_t = Gensaveparty(IIf(editflag_t, 1, 0), Partyid_t, txt_partyname.Text, cbo_Process.Text, txt_address1.Text, txt_address2.Text, _
                                     txt_address3.Text, txt_address4.Text, txt_place.Text, Stateid_t, _
                                     txt_phone.Text, txt_phone1.Text, txt_tin.Text, txt_cst.Text, IIf(DTP_Cstdate.Checked = True, DTP_Cstdate.Value, Nothing), _
                                     txt_cell.Text, txt_email.Text, txt_code.Text, _
                                     Gencompid, Lineid_t, Val(Txt_creditlimit.Text), Trim(txt_gstin.Text))

            If Btn_Discount.Visible = True Then
                With Frm_DiscountDetail
                    For i = 0 To .GridView1.Rows.Count - 1
                        Itemid_t = .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Itemid).Value
                        Discount_t = .GridView1.Rows(i).Cells(Frm_DiscountDetail.fields1.C_Discount).Value
                        GensavePartyDiscount(Partyid_t, Itemid_t, Discount_t, i + 1)
                    Next
                End With
            End If
            
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Delteproc()
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then

                If Partyid_t = -1 Then
                    MsgBox("Could not be deleted")
                Else
                    Call Gendelparty(Partyid_t)
                    Call enabdisb("Ok")
                    Call BindData()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Call enabdisb("Ok")
            Call BindData()
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

    Private Function FindItems(ByVal strSearchString As String, ByVal ind As Integer) As Boolean
        Try
            strSearchString = Replace(strSearchString, "'", "`")
            strSearchString = Replace(strSearchString, Chr(34), "`")

            GridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            GridView1.ClearSelection()

            If strSearchString = "" Then
                GridView1.Rows(0).Selected = True
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
                If GridView1.Rows.Count = 0 Then Exit Sub

                GridView1.Rows(CurrRowindex_t).Selected = True
                GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(1)
            End If
            'above lines are used to cursor focus which record was edit,add,delete
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            Dim Cellindex As Integer

            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Filtercolnmae_t = GridView1.Columns(Colindex_t).Name
            Rowindex_t = GridView1.CurrentCell.RowIndex

            If GridView1.Rows.Count > 0 Then
                Partyid_t = GridView1.Item(0, GridView1.CurrentRow.Index).Value
                'GridView1.CurrentCell = GridView1(e.ColumnIndex, 0)
                Call storechars(Partyid_t)
            Else

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdadd.Click
        Try
            editflag = False
            Call enabdisb("Add")
            Call clearchars()

            If Rowcnt > 0 Then
                CurrRowindex_t = Rowcnt - 1
            Else
                CurrRowindex_t = 0
            End If

            Events_t = "Add"

            Dim ds_st As New DataSet
            Sqlstr = "Select State, Masterid From State_Master where state='tamilnadu' Order By State"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_st = New DataSet
            ds_st.Clear()
            da.Fill(ds_st)

            If ds_st.Tables(0).Rows.Count > 0 Then
                Stateid_t = ds_st.Tables(0).Rows(0).Item("masterid")
                txt_state.Text = ds_st.Tables(0).Rows(0).Item("state")
            End If

            txt_partyname.Focus()
            
            Btn_Discount.Enabled = False

            If PARTY_DISCOUNT = 1 Then
                Btn_Discount.Visible = False
            Else
                Btn_Discount.Visible = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdok.Click
        Try
           If txt_partyname.Text = "" Then
                MsgBox("Party name should not be empty.")
                txt_partyname.Focus()
            ElseIf Stateid_t = 0 And cbo_Process.SelectedItem = "CUSTOMER" Then
                MsgBox("State should not be empty.")
                txt_state.Focus()
            Else
                Call GetPtyType()
                If ds_Getptyp.Tables(0).Rows.Count <> 0 Then
                    If cbo_Process.Text <> ds_Getptyp.Tables(0).Rows(0).Item("Ptytype").ToString Then
                        MsgBox(UCase(String.Concat(txt_partyname.Text, " IS already in ", ds_Getptyp.Tables(0).Rows(0).Item("Ptytype").ToString)))
                    Else
                        Call saveproc(editflag)
                        Call enabdisb("Ok")
                        Call BindData()
                    End If
                Else
                    Call saveproc(editflag)
                    Call enabdisb("Ok")
                    Call BindData()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmdedit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdedit.Click
        Try
            editflag = True
            CurrRowindex_t = Rowindex_t
            Events_t = "Edit"
            Call enabdisb("Edit")
            Btn_Discount.Enabled = True

            If PARTY_DISCOUNT = 1 Then
                Btn_Discount.Visible = False
            Else
                Btn_Discount.Visible = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdcancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcancel.Click
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

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Call closeconn()
        Me.Hide()
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        CurrRowindex_t = Rowindex_t
        Events_t = "Delete"
        GridView1.Enabled = False

        Call Delteproc()
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        If GridView1.CurrentCell Is Nothing Then Exit Sub
        If GridView1.CurrentCell.RowIndex >= 0 And GridView1.CurrentCell.ColumnIndex >= 0 Then Rowindex_t = GridView1.CurrentCell.RowIndex
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        If GridView1.CurrentCell Is Nothing Then Exit Sub
        If GridView1.CurrentCell.RowIndex >= 0 And GridView1.CurrentCell.ColumnIndex >= 0 Then Rowindex_t = GridView1.CurrentCell.RowIndex
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        If GridView1.CurrentCell Is Nothing Then Exit Sub
        If GridView1.CurrentCell.RowIndex >= 0 And GridView1.CurrentCell.ColumnIndex >= 0 Then Rowindex_t = GridView1.CurrentCell.RowIndex
    End Sub

    Private Sub GridView1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.RowEnter
        Try
            Dim i As Integer
            i = e.RowIndex
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then Rowindex_t = e.RowIndex
            If GridView1.Rows.Count > 0 And i >= 0 Then
                If GridView1.Item(0, i).Value = Nothing Then
                Else
                    Partyid_t = GridView1.Item(0, i).Value
                    Call storechars(Partyid_t)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_partyname_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_partyname.GotFocus
        txt_partyname.BackColor = Color.Yellow
    End Sub

    Private Sub txt_partyname_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_partyname.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address1.Focus()
        End If
    End Sub

    Private Sub txt_partyname_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_partyname.LostFocus
        Try
            Dim cnt As Integer
            ds.Tables(0).DefaultView.RowFilter = "Ptyname = '" & txt_partyname.Text & "'  "
            cnt = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                Partyid_t = ds.Tables(0).Rows(index).Item("Ptycode").ToString
                editflag = True
                Call storechars(Partyid_t)
            Else
                index = -1
            End If
            If Trim(txt_partyname.Text) <> "" Then
                Btn_Discount.Enabled = True
                If PARTY_DISCOUNT = 1 Then
                    Btn_Discount.Visible = False
                Else
                    Btn_Discount.Visible = True
                End If
            End If
            txt_partyname.BackColor = Color.White
            Exit Sub
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_search_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_search.TextChanged

        Try
            Call Filterby()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cbo_Process_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbo_Process.SelectedIndexChanged
        Try
            Call BindData()
            If cbo_Process.Text = "CUTTING" Then
                Panel2.Visible = False
                Btn_Discount.Visible = False
            ElseIf cbo_Process.Text = "CUSTOMER" Then
                '  Label9.Text = "Customer ID *"
                Panel2.Visible = True
                Btn_Discount.Visible = True
            Else
                Btn_Discount.Visible = False
                Label9.Text = "Code"
                Panel2.Visible = False
            End If

            If PARTY_DISCOUNT = 1 Then
                Btn_Discount.Visible = False
            Else
                Btn_Discount.Visible = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txt_address1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address1.GotFocus
        txt_address1.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address2.Focus()
        End If
    End Sub

    Private Sub txt_address1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address1.LostFocus
        txt_address1.BackColor = Color.White
    End Sub

    Private Sub txt_address2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address2.GotFocus
        txt_address2.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address2.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address3.Focus()
        End If
    End Sub

    Private Sub txt_address2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address2.LostFocus
        txt_address2.BackColor = Color.White
    End Sub

    Private Sub txt_address3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address3.GotFocus
        txt_address3.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address3.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_address4.Focus()
        End If
    End Sub

    Private Sub txt_address3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address3.LostFocus
        txt_address3.BackColor = Color.White
    End Sub

    Private Sub txt_address4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address4.GotFocus
        txt_address4.BackColor = Color.Yellow
    End Sub

    Private Sub txt_address4_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_address4.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_place.Focus()
        End If
    End Sub

    Private Sub txt_address4_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_address4.LostFocus
        txt_address4.BackColor = Color.White
    End Sub

    Private Sub txt_phone_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_phone.GotFocus
        txt_phone.BackColor = Color.Yellow
    End Sub

    Private Sub txt_phone_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_phone.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_phone1.Focus()
        End If
    End Sub

    Private Sub txt_phone_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_phone.LostFocus
        txt_phone.BackColor = Color.White
    End Sub

    Private Sub txt_tin_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_tin.GotFocus
        txt_tin.BackColor = Color.Yellow
    End Sub

    Private Sub txt_tin_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_tin.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_gstin.Focus()
        End If
    End Sub

    Private Sub txt_cst_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cst.GotFocus
        txt_cst.BackColor = Color.Yellow
    End Sub

    Private Sub txt_cst_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txt_cst.KeyDown
        If e.KeyCode = Keys.Enter Then
            DTP_Cstdate.Focus()
        End If
    End Sub

    Private Sub DTP_Cstdate_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTP_Cstdate.GotFocus
        DTP_Cstdate.BackColor = Color.Yellow
    End Sub

    Private Sub DTP_Cstdate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DTP_Cstdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_contact.Focus()
        End If
    End Sub

    Private Sub txt_tin_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_tin.LostFocus
        txt_tin.BackColor = Color.White
    End Sub

    Private Sub txt_cst_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_cst.LostFocus
        txt_cst.BackColor = Color.White
    End Sub

    Private Sub DTP_Cstdate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTP_Cstdate.LostFocus
        DTP_Cstdate.BackColor = Color.White
    End Sub

    Private Sub txt_email_GotFocus(sender As Object, e As EventArgs) Handles txt_email.GotFocus
        txt_email.BackColor = Color.Yellow
    End Sub

    Private Sub txt_email_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_email.KeyDown
        If Panel2.Visible = True Then
            txt_line.Focus()
        Else
            cmdok.Focus()
        End If
    End Sub

    Private Sub txt_email_LostFocus(sender As Object, e As EventArgs) Handles txt_email.LostFocus
        txt_email.BackColor = Color.White
    End Sub

    Private Sub PartyMaster_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.Text = "Party Master"
            Call opnconn()
            Call dsopen()
            Call Load_combotype()
            Call BindData()
            enabdisb("Ok")

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            Dim converter As System.ComponentModel.TypeConverter = _
            System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            font1 = _
            CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))

            txt_partyname.Font = font1
            txt_address1.Font = font1
            txt_address2.Font = font1
            txt_address3.Font = font1
            txt_address4.Font = font1
            txt_place.Font = font1

            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next
            Btn_Discount.Enabled = False

            GridView1.AutoResizeColumns()
            txt_statecode.Enabled = False

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

            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Property Defaultprocess() As String
        Get
            Return Defaultprocess_t
        End Get
        Set(ByVal value As String)
            Defaultprocess_t = value
        End Set
    End Property

    Private Sub txt_palce_GotFocus(sender As Object, e As EventArgs) Handles txt_place.GotFocus
        txt_place.BackColor = Color.Yellow
    End Sub

    Private Sub txt_palce_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_place.KeyDown

        If e.KeyCode = Keys.Enter Then
            txt_state.Focus()
        End If
    End Sub

    Private Sub txt_palce_LostFocus(sender As Object, e As EventArgs) Handles txt_place.LostFocus
        txt_place.BackColor = Color.White
    End Sub

    Private Sub txt_state_Click(sender As Object, e As EventArgs) Handles txt_state.Click
        Call Statefindfrm()
    End Sub

    Private Sub txt_state_GotFocus(sender As Object, e As EventArgs) Handles txt_state.GotFocus
        txt_state.BackColor = Color.Yellow
    End Sub

    Private Sub txt_state_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_state.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Statefindfrm()
            txt_gstin.Focus()
        End If
    End Sub

    Private Sub txt_state_LostFocus(sender As Object, e As EventArgs) Handles txt_state.LostFocus
        txt_state.BackColor = Color.White
    End Sub

    Private Sub txt_contact_GotFocus(sender As Object, e As EventArgs) Handles txt_contact.GotFocus
        txt_contact.BackColor = Color.Yellow
    End Sub

    Private Sub txt_contact_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_contact.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_cell.Focus()
        End If
    End Sub

    Private Sub txt_contact_LostFocus(sender As Object, e As EventArgs) Handles txt_contact.LostFocus
        txt_contact.BackColor = Color.White
    End Sub

    Private Sub txt_phone1_GotFocus(sender As Object, e As EventArgs) Handles txt_phone1.GotFocus
        txt_phone1.BackColor = Color.Yellow
    End Sub

    Private Sub txt_phone1_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_phone1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_email.Focus()
        End If
    End Sub

    Private Sub txt_phone1_LostFocus(sender As Object, e As EventArgs) Handles txt_phone1.LostFocus
        txt_phone1.BackColor = Color.White
    End Sub

    Private Sub txt_cell_GotFocus(sender As Object, e As EventArgs) Handles txt_cell.GotFocus
        txt_cell.BackColor = Color.Yellow
    End Sub

    Private Sub txt_cell_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_cell.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_phone.Focus()
        End If
    End Sub

    Private Sub txt_cell_LostFocus(sender As Object, e As EventArgs) Handles txt_cell.LostFocus
        txt_cell.BackColor = Color.White
    End Sub

    Private Sub Statefindfrm()
        Try
            VisibleCols.Add("State")
            Colheads.Add("State")
            fm.Frm_Width = 300
            fm.Frm_Height = 300
            fm.Frm_Left = 550
            fm.Frm_Top = 250

            fm.MainForm = New PartyMaster
            fm.Active_ctlname = "txt_State"
            Csize.Add(275)
            tmppassstr = txt_state.Text
            If ds_state.Tables(0).Rows.Count = 1 Then
                txt_state.Text = ds_state.Tables(0).Rows(0).Item("state").ToString
                Stateid_t = ds_state.Tables(0).Rows(0).Item("masterid")
            Else
                fm.EXECUTE(conn, ds_state, VisibleCols, Colheads, Stateid_t, "", False, Csize, "", False, False, "", tmppassstr)
                txt_state.Text = fm.VarNew
                Stateid_t = fm.VarNewid
            End If

            ds_state.Tables(0).DefaultView.RowFilter = " masterid = " & Stateid_t & " "
            Dim cnt As Integer = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds_state.Tables(0).Rows.IndexOf(ds_state.Tables(0).DefaultView.Item(0).Row)
                If index >= 0 Then
                    txt_statecode.Text = ds_state.Tables(0).Rows(index).Item("code").ToString
                End If
            Else
                index = -1
            End If

            VisibleCols.Remove(1)
            Colheads.Remove(1)
            Csize.Remove(1)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Linefindfrm()
        Try
            Sqlstr = "Select LINE, Masterid From LINE_MASTER Order By LINE"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_line = New SqlDataAdapter(cmd)
            ds_line = New DataSet
            ds_line.Clear()
            da_line.Fill(ds_line)

            If ds_line.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("LINE")
                Colheads.Add("LINE")

                fm.Frm_Width = 300
                fm.Frm_Height = 300
                fm.Frm_Left = 757
                fm.Frm_Top = 444

                fm.MainForm = New PartyMaster
                fm.Active_ctlname = "txt_line"
                Csize.Add(250)
                tmppassstr = txt_line.Text
                If ds_line.Tables(0).Rows.Count = 1 Then
                    txt_line.Text = ds_line.Tables(0).Rows(0).Item("LINE").ToString
                    Lineid_t = ds_line.Tables(0).Rows(0).Item("masterid")
                Else
                    fm.EXECUTE(conn, ds_line, VisibleCols, Colheads, Lineid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    txt_line.Text = fm.VarNew
                    Lineid_t = fm.VarNewid
                End If

                If txt_code.Text = "" Or txt_code.Text Is Nothing Then
                    txt_code.Text = txt_line.Text.Substring(0, 1)
                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetPtyType()
        Sqlstr = "Select Ptyname,Ptytype from party Where ptyname='" & txt_partyname.Text & "'"
        cmd = New SqlCommand(Sqlstr, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds_Getptyp = New DataSet
        ds_Getptyp.Clear()
        da.Fill(ds_Getptyp)
    End Sub

    Private Sub txt_code_GotFocus(sender As Object, e As EventArgs) Handles txt_code.GotFocus
        txt_code.BackColor = Color.Yellow
    End Sub

    Private Sub txt_code_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_code.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim AlreadyExist_t As Boolean = True
            If txt_code.Text.Length > 0 Then
                Dim Code_t As String = txt_code.Text.ToString

                If AlreadyExist_t = CheckCodeExist(txt_code.Text.ToString) Then
                    MessageBox.Show("Customer Code already Exists.!", "User Input Error.!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'txt_code.Text = ""
                    ' txt_code.Focus()
                Else
                    Txt_creditlimit.Focus()
                End If
            Else
                Txt_creditlimit.Focus()
            End If
            Txt_creditlimit.Focus()
        End If
    End Sub

    Private Sub txt_code_LostFocus(sender As Object, e As EventArgs) Handles txt_code.LostFocus
        txt_code.BackColor = Color.White
    End Sub

    Private Function CheckCodeExist(ByVal Code_v As String)
        Try
            Sqlstr = "select code from party where code in ('" & Code_v & "') AND PTYCODE <> " & Partyid_t & ""
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_CheckCode = New DataSet
            ds_CheckCode.Clear()
            da.Fill(ds_CheckCode)

            If ds_CheckCode.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub txt_line_Click(sender As Object, e As EventArgs) Handles txt_line.Click
        If txt_line.Text <> "" Then
            Call Linefindfrm()
        End If
        If txt_line.Text = "" Then Lineid_t = 0
    End Sub

    Private Sub txt_line_GotFocus(sender As Object, e As EventArgs) Handles txt_line.GotFocus
        txt_line.BackColor = Color.Yellow
    End Sub

    Private Sub txt_line_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_line.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txt_line.Text <> "" Then
                Call Linefindfrm()
            End If
            If txt_line.Text = "" Then Lineid_t = 0
            Txt_creditlimit.Focus()
        End If
    End Sub

    Private Sub txt_line_LostFocus(sender As Object, e As EventArgs) Handles txt_line.LostFocus
        txt_line.BackColor = Color.White
    End Sub

    Private Sub Txt_creditlimit_GotFocus(sender As Object, e As EventArgs) Handles Txt_creditlimit.GotFocus
        Txt_creditlimit.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_creditlimit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_creditlimit.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdok.Focus()
        End If
    End Sub

    Private Sub Txt_creditlimit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_creditlimit.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub Txt_creditlimit_LostFocus(sender As Object, e As EventArgs) Handles Txt_creditlimit.LostFocus
        Txt_creditlimit.BackColor = Color.White
        Txt_creditlimit.Text = Format(Val(Txt_creditlimit.Text), "#######0.00")
    End Sub

    Private Sub Btn_Discount_Click(sender As Object, e As EventArgs) Handles Btn_Discount.Click
        Frm_DiscountDetail.Partyid_t = Partyid_t
        Frm_DiscountDetail.ShowInTaskbar = False
        Frm_DiscountDetail.StartPosition = FormStartPosition.CenterScreen
        Frm_DiscountDetail.ShowDialog()
    End Sub

    Private Sub txt_gstin_GotFocus(sender As Object, e As EventArgs) Handles txt_gstin.GotFocus
        txt_gstin.BackColor = Color.Yellow
    End Sub

    Private Sub txt_gstin_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_gstin.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_contact.Focus()
        End If
    End Sub

    Private Sub txt_gstin_LostFocus(sender As Object, e As EventArgs) Handles txt_gstin.LostFocus
        txt_gstin.BackColor = Color.White
    End Sub
     
End Class
