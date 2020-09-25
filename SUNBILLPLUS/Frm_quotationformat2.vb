Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App

Public Class Frm_quotationformat2

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da, da_head, da_detl, da_party, da_despatch As SqlDataAdapter
    Dim bs As New BindingSource
    Dim ds, ds_location, ds_party, ds_despatch, ds1, ds_head, ds_detl, ds_item, ds_acdesc As New DataSet
    Dim Despatchid_t As Double
    Dim Showpartyfindform As Boolean
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim Locationid_t, Itemid_t, Partyid_t As Double, Formshown_t, Isalreadyexistflag_t As Boolean, SavechhkFlg As Boolean, Addlessformulaflg_t As Boolean
    Dim Controlno_t As String, Process As String, Trntype_t As String, Sqlstr As String, Colname_t As String
    Dim Headerid_t As Double, AcDescid_t As Double, Cardid_t As Double
    Dim index_t As Integer, Rowindex_t As Integer, Headcnt_t As Integer, Detlcnt_t As Integer, colindex_t As Integer
    Dim fm As New Sun_Findfrm
    Dim rm As New Frm_Reports_Init
    Dim celWasEndEdit As DataGridViewCell

    Dim ds_settings As New DataSet
    Dim da_settings As SqlDataAdapter
    Dim dscnt As Integer
    Dim Val_t As Integer
    Dim font1 As Font

    Enum fields3
        c2_quono = 0
        c2_quodate = 1
        c2_customer = 2
        c2_despatch = 3
    End Enum

    Enum fields1
        c1_itemdesc = 0
        c1_itemid = 1
        c1_group = 2
        c1_groupid = 3
        c1_qty = 4
        c1_rate = 5
        c1_amount = 6
        c1_remarks = 7
    End Enum

    Enum fields2
        c2_Type = 0
        c2_Desc = 1
        c2_Descid = 2
        c2_Perc = 3
        c2_Amount = 4
    End Enum

    Public Sub Init(ByVal controlno As String, Optional ByVal headid As Double = 0, Optional Process_t As String = "",
                   Optional Trntype As String = "")
        Try
            Controlno_t = controlno
            Headerid_t = headid
            Process = Process_t
            Trntype_t = Trntype
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Execute()
        Try
            Dim ds_loc1, ds_settings As New DataSet
            Dim da_loc1, da_settings As SqlDataAdapter
            Dim cnt1 As Integer

            GridView1.AllowUserToAddRows = False

            Call dsopen()

            Call Headercall(Headerid_t)

            Headcnt_t = ds_head.Tables(0).Rows.Count

            Call gridreadonly(GridView1, True, "c_group", "c_amount")
            Call gridreadonly_Color(GridView1, Readonlycolor_t, "c_group", "c_amount")

            GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            'ds_settings = Nothing
            'ds_settings.Clear()
            'Sqlstr = "SELECT ISNULL(NUMERICVALUE,0 ) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS  ='TAMIL FONT' "
            'cmd = New SqlCommand(Sqlstr, conn)
            'cmd.CommandType = CommandType.Text
            'da_settings = New SqlDataAdapter(cmd)
            'ds_settings = New DataSet
            'ds_settings.Clear()
            'da_settings.Fill(ds_settings)
            'dscnt = ds_settings.Tables(0).Rows.Count
            'If dscnt > 0 Then
            'Val_t = ds_settings.Tables(0).Rows(0).Item("numericvalue")
            'Else
            'Val_t = 0
            'End If

            If Headcnt_t > 0 Then
                Call storechars(Headcnt_t)
            Else
                Call clearchars()

                'ds_loc1.Clear()
                'ds_loc1 = Nothing
                'cmd = New SqlCommand
                'cmd.Connection = conn
                'cmd.CommandType = CommandType.StoredProcedure
                'cmd.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
                'cmd.CommandText = "GET_DEFAULTLOCATION"
                'da_loc1 = New SqlDataAdapter(cmd)
                'ds_loc1 = New DataSet
                'da_loc1.Fill(ds_loc1)

                'cnt1 = ds_loc1.Tables(0).Rows.Count

                'If cnt1 > 0 Then
                '    Locationid_t = ds_loc1.Tables(0).Rows(0).Item("LOCATIONID")
                '    txt_location.Text = ds_loc1.Tables(0).Rows(0).Item("GODOWNNAME").ToString
                'End If


                ' Isalreadyexistflag_t = Isalreadyexists()
                If Isalreadyexistflag_t = True Then
                    Call Headercall(Headerid_t)
                    Headcnt_t = ds_head.Tables(0).Rows.Count
                    If Headcnt_t > 0 Then
                        editflag = True
                        Call storechars(Headcnt_t)
                    Else
                        Call clearchars()
                        editflag = False
                    End If
                Else
                    '
                End If
            End If

            Call Enabledisablechars(True)

            If GridView1.Rows.Count > 0 Then
            Else
                GridView1.Rows.Add(1)
            End If

            If editflag = True Then
                Call setUserpermission()
            End If

            Dim val As Integer
            ds_settings = Nothing
            cmd = New SqlCommand("SELECT ISNULL(NUMERICVALUE,0) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS='QUOTATION_PARTYFINDFORM' ", conn)
            cmd.CommandType = CommandType.Text
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings)
            cnt1 = ds_settings.Tables(0).Rows.Count

            If cnt1 > 0 Then
                val = ds_settings.Tables(0).Rows(0).Item("NUMERICVALUE")

                If val = 1 Then
                    Showpartyfindform = True
                Else
                    Showpartyfindform = False
                End If
            End If

            Dim ds_font As New DataSet
            Dim da_font As SqlDataAdapter

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

            'txt_vchnum.Enabled = False
            'txt_vchnum.BackColor = Color.White

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try
            'Sqlstr = "Select P.Ptyname Partyname,P.Ptycode,P.Pricelistid,Ph.Refno As Pricelistno,Isnull(P.Transid,0) As Transid, P.Place, " _
            '    & "MT.Transport,Isnull(P.Bookplaceid,0) As Bookplaceid,MB.Bookingplace,Isnull(P.Agentid,0) As Agentid,PA.Ptyname As Agent, " _
            '    & "Ms.State From Party P Left Join Pricelisthead Ph On Ph.Pricelistid=P.Pricelistid " _
            '    & "Left Join Transport_Master MT On MT.Masterid=P.Transid Left Join Bookingplace_Master MB On MB.Masterid=P.Bookplaceid " _
            '    & "Left Join Party PA On PA.Ptycode = P.Agentid Left Join State_Master Ms On Ms.Masterid = P.Stateid " _
            '    & "Where P.Ptytype='CUSTOMER' Order By P.Ptyname "
            'cmd = New SqlCommand(Sqlstr, conn)
            'cmd.CommandType = CommandType.Text
            'da = New SqlDataAdapter(cmd)
            'ds = New DataSet
            'ds.Clear()
            'da.Fill(ds)

            Sqlstr = "Select Ptyname,Partyid From Account  Where Groupid in('-151','-152','-256')  Order By Ptyname"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_acdesc = New DataSet
            ds_acdesc.Clear()
            da.Fill(ds_acdesc)

            Sqlstr = "Select despatch,masterid From despatch_master Order By despatch "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_despatch = New SqlDataAdapter(cmd)
            ds_despatch = New DataSet
            ds_despatch.Clear()
            da_despatch.Fill(ds_despatch)

            Sqlstr = "select godownname,masterid from godown_master order by godownname "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_location = New DataSet
            ds_location.Clear()
            da.Fill(ds_location)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            ' txt_totamount.Text = "0.00"
            txt_totqty.Text = "0.00"

            txt_vchnum.Text = AutoNum(Process)
            txt_vchnum.Enabled = False

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

    Private Sub Enabledisablechars(ByVal val As Boolean)
        Try
            Panel1.Enabled = val
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Headercall(ByVal Headerid_v As Double)
        Try
            cmd = New SqlCommand("SELECT H.VCHNUM,H.HEADERID,H.VCHDATE,H.NARRATION,ISNULL(H.PARTYID,0) AS PARTYID,ISNULL(U.UNAME,'') AS USERNAME, p.PTYNAME as Customer, isnull(dm.despatch,'') as despatch,isnull(h.despatchid,0) as despatchid  FROM QUOTATION_HEADER2 H " _
                                 & " LEFT JOIN despatch_MASTER dM ON dM.masterid = H.despatchid LEFT JOIN PARTY P ON P.PTYCODE = H.PARTYID LEFT JOIN USERS U ON U.UID = H.UID WHERE H.HEADERID = " & Headerid_v & " ", conn)
            cmd.CommandType = CommandType.Text
            da_head = New SqlDataAdapter(cmd)
            ds_head = New DataSet
            ds_head.Clear()
            da_head.Fill(ds_head)

            dt = New DataTable 'used for find particular rows
            da_head.Fill(dt)

            Headcnt_t = ds_head.Tables(0).Rows.Count
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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

    Private Sub storechars(Optional ByVal pos As Integer = 0)
        Try
            Dim rowid_t As Integer, Decimal_t As Double, Decimal_tt As String = "", Format_t As String

            Call clearchars()

            rowid_t = ds_head.Tables(0).Rows.Count
            If rowid_t <= 0 Then Exit Sub

            txt_vchnum.Enabled = False

            rowid_t = rowid_t - 1
            Headerid_t = ds_head.Tables(0).Rows(rowid_t).Item("Headerid")
            txt_vchnum.Text = ds_head.Tables(0).Rows(rowid_t).Item("Vchnum").ToString
            DTP_Vchdate.Value = ds_head.Tables(0).Rows(rowid_t).Item("Vchdate").ToString
            txt_narration.Text = ds_head.Tables(0).Rows(rowid_t).Item("Narration").ToString
            txt_party.Text = ds_head.Tables(0).Rows(rowid_t).Item("CUSTOMER").ToString
            Txt_despatch.Text = ds_head.Tables(0).Rows(rowid_t).Item("despatch").ToString
            Partyid_t = ds_head.Tables(0).Rows(rowid_t).Item("PARTYID")
            Despatchid_t = ds_head.Tables(0).Rows(rowid_t).Item("despatchid")
            Me.Text = Me.Text + Space(220) + "  Created By  " + UCase(ds_head.Tables(0).Rows(rowid_t).Item("username").ToString)

            ds_detl.Clear()
            ds_detl = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_QUOTATION_DETAIL2"
            cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            da_detl = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            da_detl.Fill(ds_detl)

            'GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#"
            GridView1.Columns(fields1.c1_rate).DefaultCellStyle.Format = "#.00"
            ' GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"

            Detlcnt_t = ds_detl.Tables(0).Rows.Count

            If Detlcnt_t > 0 Then
                GridView1.Rows.Add(Detlcnt_t + 1)
                For i = 0 To Detlcnt_t - 1
                    ' GridView1.Rows(i).Cells(fields1.c1_itemcode).Value = ds_detl.Tables(0).Rows(i).Item("Itemcode")
                    GridView1.Rows(i).Cells(fields1.c1_itemid).Value = ds_detl.Tables(0).Rows(i).Item("Itemid")
                    GridView1.Rows(i).Cells(fields1.c1_qty).Value = ds_detl.Tables(0).Rows(i).Item("Qty")
                    GridView1.Rows(i).Cells(fields1.c1_rate).Value = ds_detl.Tables(0).Rows(i).Item("Rate")
                    GridView1.Rows(i).Cells(fields1.c1_amount).Value = ds_detl.Tables(0).Rows(i).Item("Amount")
                    GridView1.Rows(i).Cells(fields1.c1_itemdesc).Value = ds_detl.Tables(0).Rows(i).Item("ITEMDESCRIPTION")
                    GridView1.Rows(i).Cells(fields1.c1_remarks).Value = ds_detl.Tables(0).Rows(i).Item("REMARKS")
                    GridView1.Rows(i).Cells(fields1.c1_group).Value = ds_detl.Tables(0).Rows(i).Item("GROUPNAME")
                    GridView1.Rows(i).Cells(fields1.c1_groupid).Value = ds_detl.Tables(0).Rows(i).Item("GROUPID")
                    '        If Val_t = 0 Then GridView1.Rows(i).Cells(fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("uom")
                    '        If Val_t = 1 Then GridView1.Rows(i).Cells(fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("tamiluom")
                    '        GridView1.Rows(i).Cells(fields1.c1_uomid).Value = ds_detl.Tables(0).Rows(i).Item("uomid")
                    '        GridView1.Rows(i).Cells(fields1.c1_decimal).Value = ds_detl.Tables(0).Rows(i).Item("noofdecimal")
                    '        GridView1.Rows(i).Cells(fields1.c1_costrate).Value = ds_detl.Tables(0).Rows(i).Item("COSTRATE")
                    'Decimal_t = IIf(IsDBNull(GridView1.Rows(i).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(i).Cells(fields1.c1_decimal).Value))'
                    'Decimal_tt = ""
                    'For k = 1 To Decimal_t
                    'Decimal_tt = String.Concat(Decimal_tt, "0")
                    'Next
                    'If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                    'GridView1.Rows(i).Cells(fields1.c1_qty).Style.Format = Format_t
                Next
            End If

            'ds_detl.Clear()
            'cmd = Nothing
            'cmd = New SqlCommand
            'cmd.Connection = conn
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "GET_QUOTATIONADDLESS"
            'cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            'da_detl = New SqlDataAdapter(cmd)
            'ds_detl = New DataSet
            'da_detl.Fill(ds_detl)

            'Detlcnt_t = ds_detl.Tables(0).Rows.Count

            'Dim rwcnt_t As Double

            'rwcnt_t = GridView2.Rows.Count

            'If Detlcnt_t > 0 Then
            '    GridView2.Rows.Add(Detlcnt_t)

            '    For j = 0 To Detlcnt_t - 1
            '        GridView2.Rows(j).Cells(fields2.c2_Type).Value = ds_detl.Tables(0).Rows(j).Item("Altype")
            '        GridView2.Rows(j).Cells(fields2.c2_Desc).Value = ds_detl.Tables(0).Rows(j).Item("ALDESCRIPTION")
            '        GridView2.Rows(j).Cells(fields2.c2_Descid).Value = ds_detl.Tables(0).Rows(j).Item("Descid")
            '        GridView2.Rows(j).Cells(fields2.c2_Perc).Value = ds_detl.Tables(0).Rows(j).Item("Perc")
            '        GridView2.Rows(j).Cells(fields2.c2_Amount).Value = ds_detl.Tables(0).Rows(j).Item("Amount")
            '    Next
            'End If

            Call calcnetamt()

            Dim ds_prevparty As New DataSet
            Dim da_prevparty As SqlDataAdapter
            Dim cnt As Integer

            If Partyid_t <> 0 Then
                ds_prevparty.Clear()
                ds_prevparty = Nothing
                Sqlstr = "SELECT TOP 4 H.VCHNUM AS QUONO,CONVERT(VARCHAR(10),H.VCHDATE,103) AS QUODATE,P.PTYNAME AS PARTY,DM.DESPATCH AS DESPATCH FROM QUOTATION_HEADER2 H JOIN PARTY P ON P.PTYCODE = H.PARTYID LEFT JOIN despatch_master DM ON DM.MASTERID = H.DESPATCHID where h.partyid =" & Partyid_t & " AND H.HEADERID <> " & Headerid_t & " AND H.COMPID = " & Gencompid & " ORDER BY H.VCHDATE DESC "
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da_prevparty = New SqlDataAdapter(cmd)
                ds_prevparty = New DataSet
                ds_prevparty.Clear()
                da_prevparty.Fill(ds_prevparty)
                cnt = ds_prevparty.Tables(0).Rows.Count

                Gridview2.Rows.Clear()
                If cnt > 0 Then
                    Gridview2.Rows.Add(cnt)

                    For i = 0 To Gridview2.Rows.Count - 2
                        Gridview2.Rows(i).Cells(fields3.c2_quono).Value = ds_prevparty.Tables(0).Rows(i).Item("quono").ToString
                        Gridview2.Rows(i).Cells(fields3.c2_quodate).Value = ds_prevparty.Tables(0).Rows(i).Item("quodate").ToString
                        Gridview2.Rows(i).Cells(fields3.c2_customer).Value = ds_prevparty.Tables(0).Rows(i).Item("party").ToString
                        Gridview2.Rows(i).Cells(fields3.c2_despatch).Value = ds_prevparty.Tables(0).Rows(i).Item("despatch").ToString
                    Next
                End If
            End If

            Gridview2.ReadOnly = True
            Gridview2.ReadOnly = True
            Gridview2.AllowUserToResizeColumns = False
            Gridview2.AllowUserToResizeRows = False
            Gridview2.Columns(0).Width = 100
            Gridview2.Columns(1).Width = 90

            txt_vchnum.BackColor = Color.White
            DTP_Vchdate.CalendarMonthBackground = Color.White
            txt_vchnum.ForeColor = Color.Black

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub GridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GridView1.DataError
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If e.Exception.Message = "Input string was not in a correct format." Then
                    e.Cancel = True
                    Me.GridView1.Item((e.ColumnIndex), (e.RowIndex)).Value = "0"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Private Sub GridView2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GridView2.DataError
    '    Try
    '        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
    '            If e.Exception.Message = "Input string was not in a correct format." Then
    '                e.Cancel = True
    '                Me.GridView2.Item((e.ColumnIndex), (e.RowIndex)).Value = "0"
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub calcnetamt()
        Try
            Dim itemcnt As Integer = 0

            txt_totqty.Text = Format(Tot_Calc(GridView1, fields1.c1_qty), "#######0")
            txt_totamount.Text = Format(Tot_Calc(GridView1, fields1.c1_amount), "#######0.00")

            For i = 0 To GridView1.Rows.Count - 1
                Itemid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(i).Cells(fields1.c1_itemid).Value))
                If Itemid_t <> 0 Then
                    itemcnt = itemcnt + 1
                End If
            Next
            txt_totitem.Text = itemcnt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Gridfindfom(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try
            Dim ds_itm As New DataSet, ds_itemdet As New DataSet
            Dim Tmpitemcode_t As String, tmpuom_t As String, Tmpitemid_t As Double, Tmpitemdes_t As String, activerow_tmp As Integer, Decimal_t As Double
            Dim TmpGroup_t As String, TmpGRoupid_t As Double
            Dim Costrate_t As Double, Rate_t As Double

            If activecol < 0 Or activerow < 0 Then Exit Sub

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t
                Case fields1.c1_itemdesc

                    Dim Nextcond_t As String = "", Nextcond_t1 As String = ""
                    For i = 0 To GridView1.Rows.Count - 1
                        Itemid_t = IIf(IsDBNull(GridView1.Item(fields1.c1_itemid, i).Value), 0, GridView1.Item(fields1.c1_itemid, i).Value)
                        If Itemid_t <> 0 And i <> activerow Then
                            Nextcond_t = String.Concat(Nextcond_t, Itemid_t, ",")
                        End If
                    Next

                    If Nextcond_t <> "" Then
                        Nextcond_t = Nextcond_t.Remove(Nextcond_t.Length - 1)
                    Else
                        Nextcond_t = "00"
                    End If

                    Sqlstr = "SELECT IM.ITEMDESCRIPTION AS ITEMDES,IM.ITEMID,isnull(im.SELLPRICE,0) as SELPRICERETAIL,ISNULL(IM.COSTPRICE,0) AS COSTRATE,ISNULL(GM.GROUPNAME,'') AS GROUPNAME,ISNULL(IM.GROUPID,0) AS GROUPID FROM ITEM_MASTERFORMAT2 IM " _
                        & " LEFT JOIN GROUP_MASTER GM ON GM.MASTERID = IM.GROUPID  where im.itemid not in (" & Nextcond_t & ") and im.compid = " & Gencompid & ""

                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_item = New DataSet
                    ds_item.Clear()
                    da.Fill(ds_item)

                    If ds_item.Tables(0).Rows.Count > 0 Then

                        VisibleCols.Add("ITEMDES")
                        Colheads.Add("Item Desc")

                        fm.Frm_Width = 400
                        fm.Frm_Height = 400
                        fm.Frm_Left = 127
                        fm.Frm_Top = 245

                        fm.MainForm = New Frm_quotationformat2
                        fm.Active_ctlname = "Gridview1"

                        Csize.Add(350)

                        If ds_item.Tables(0).Rows.Count = 1 Then
                            GridView1.Rows(activerow).Cells(activecol).Value = ds_item.Tables(0).Rows(0).Item("ITEMDES").ToString
                            GridView1.Rows(activerow).Cells(activecol + 1).Value = ds_item.Tables(0).Rows(0).Item("ITEMID")
                            Tmpitemcode_t = ds_item.Tables(0).Rows(0).Item("ITEMDES").ToString
                            Itemid_t = ds_item.Tables(0).Rows(0).Item("ITEMID")
                        Else
                            tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))

                            Sqlstr = "SELECT IM.ITEMDESCRIPTION AS ITEMDES,IM.ITEMID,isnull(im.SELLPRICE,0) as SELPRICERETAIL ,ISNULL(IM.COSTPRICE,0) AS COSTRATE,ISNULL(GM.GROUPNAME,'') AS GROUPNAME,ISNULL(IM.GROUPID,0) AS GROUPID FROM ITEM_MASTERFORMAT2 IM " _
                  & " LEFT JOIN group_master gm on gm.masterid = im.groupid  WHERE IM.ITEMID NOT IN (" & Nextcond_t & ")  and im.ITEMDESCRIPTION ='" & tmppassstr & "'  and  im.compid = " & Gencompid & " ORDER BY IM.ITEMDESCRIPTION "

                            cmd = New SqlCommand(Sqlstr, conn)
                            cmd.CommandType = CommandType.Text
                            da = New SqlDataAdapter(cmd)
                            ds_itm = New DataSet
                            ds_itm.Clear()
                            da.Fill(ds_itm)

                            If ds_itm.Tables(0).Rows.Count = 1 Then
                                Itemid_t = ds_itm.Tables(0).Rows(0).Item("itemid")
                            End If
                            tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))
                            fm.VarNew = ""
                            fm.VarNewid = 0
                            fm.EXECUTE(conn, ds_item, VisibleCols, Colheads, Itemid_t, "", False, Csize, "", False, False, "", tmppassstr)
                            GridView1.Rows(activerow).Cells(activecol).Value = fm.VarNew
                            GridView1.Rows(activerow).Cells(activecol + 1).Value = fm.VarNewid
                            Tmpitemcode_t = fm.VarNew
                            Itemid_t = fm.VarNewid
                            Formshown_t = fm.Formshown
                        End If

                        Dim tmprow_t As Integer
                        tmprow_t = GridView1.Rows.Count
                        activerow_tmp = activerow + 1

                        If ds_item.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
                            ds_item.Tables(0).DefaultView.RowFilter = "itemid = '" & Itemid_t & "' "
                            index_t = ds_item.Tables(0).Rows.IndexOf(ds_item.Tables(0).DefaultView.Item(0).Row)

                            For i = 0 To ds_item.Tables(0).DefaultView.Count - 1
                                'Tmpitemcode_t = ds_item.Tables(0).Rows(i + index_t).Item("itemcode").ToString
                                Tmpitemdes_t = ds_item.Tables(0).Rows(i + index_t).Item("itemdes").ToString
                                Tmpitemid_t = ds_item.Tables(0).Rows(i + index_t).Item("itemid").ToString
                                TmpGroup_t = ds_item.Tables(0).Rows(i + index_t).Item("groupname").ToString
                                TmpGRoupid_t = ds_item.Tables(0).Rows(i + index_t).Item("groupid").ToString
                                Costrate_t = ds_item.Tables(0).Rows(i + index_t).Item("COSTRATE")
                                Rate_t = ds_item.Tables(0).Rows(i + index_t).Item("SELPRICERETAIL")
                                If Tmpitemid_t <> 0 Then
                                    'GridView1.Rows(activerow + i).Cells(fields1.c1_itemcode).Value = Tmpitemcode_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_itemdesc).Value = Tmpitemdes_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_rate).Value = Rate_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_group).Value = TmpGroup_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_groupid).Value = TmpGRoupid_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_itemid).Value = Tmpitemid_t
                                End If
                            Next
                        End If

                        VisibleCols.Remove(1)
                        Colheads.Remove(1)
                        Csize.Remove(1)

                    End If

                    If GridView1.Rows.Count - 1 = activerow Then
                        If IsValidRow(GridView1, "C_Itemdes") Then
                            GridView1.Rows.Add(1)
                        End If
                    End If

            End Select
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

    Private Sub Saveproc(ByVal editflag_t As Boolean)
        Try
            Dim i As Integer
            Dim Rate_t As Double, amount_t As Double, Qty_t As Double, remarks_v As String
            Dim Groupid_t As Double
            Dim Adtype_t As String, Adamt_t As Double, Adperc_t As Double, Addescid_t As Double, CostRate_t As Double

            If editflag = False Then
                txt_vchnum.Text = AutoNum(Process, True) 'if editflag trie begin tran start in autonum fun.
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If
            SavechhkFlg = False
            Headerid_t = GensaveQuotationHead2(IIf(editflag, 1, 0), Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Partyid_t, Despatchid_t, txt_narration.Text, Gencompid)

            For i = 0 To GridView1.Rows.Count - 1
                Itemid_t = IIf(IsDBNull(GridView1.Item(fields1.c1_itemid, i).Value), 0, GridView1.Item(fields1.c1_itemid, i).Value)
                Groupid_t = IIf(IsDBNull(GridView1.Item(fields1.c1_groupid, i).Value), 0, GridView1.Item(fields1.c1_groupid, i).Value)
                Qty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_qty, i).Value), 0, GridView1.Item(fields1.c1_qty, i).Value)
                '             CostRate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_costrate, i).Value), 0, GridView1.Item(fields1.c1_costrate, i).Value)
                Rate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rate, i).Value), 0, GridView1.Item(fields1.c1_rate, i).Value)
                amount_t = IIf(IsDBNull(GridView1.Item(fields1.c1_amount, i).Value), 0, GridView1.Item(fields1.c1_amount, i).Value)
                remarks_v = IIf(IsDBNull(GridView1.Item(fields1.c1_remarks, i).Value), "", GridView1.Item(fields1.c1_remarks, i).Value)
                If Itemid_t <> 0 And Rate_t <> 0 And Groupid_t <> 0 Then
                    If remarks_v = "" Or remarks_v Is Nothing Then remarks_v = "  "
                    GensaveQuotationdetl2(Headerid_t, i + 1, Rate_t, Itemid_t, remarks_v, Groupid_t, Qty_t, amount_t)
                    SavechhkFlg = True
                End If
            Next

            If SavechhkFlg = True Then
                'PKId_t = Headerid_t
                If Generateeventlogs_t = True Then
                    'GensaveEventlogs(Event_Conn, Process, Headerid_t, Genuid, IIf(editflag_t = True, "Edit", "Add"), Now, Gencompid)
                End If
                trans.Commit()
            Else
                trans.Rollback()
            End If

        Catch ex As Exception
            trans.Rollback()
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
                    If GridView1.CurrentCell.ColumnIndex = fields1.c1_rate Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                    ElseIf GridView1.CurrentCell.ColumnIndex = fields1.c1_qty Then
                        ' Decimal_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value))
                        If Text.Length >= Text.IndexOf(".") + 2 Then
                            e.Handled = True
                        End If
                    ElseIf GridView1.CurrentCell.ColumnIndex = fields1.c1_amount Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                        End If
                End If
            End If

            If GridView1.CurrentCell.ColumnIndex = fields1.c1_itemdesc Or GridView1.CurrentCell.ColumnIndex = fields1.c1_remarks Then
                e.Handled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Tmpbalqty_t, TmpEditqty_t As Double
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
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub AllowOnlyNumeric(ByRef e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal AllowedChar As String = "")
        Try
            Dim strAllowed As String() = AllowedChar.Split(",")
            Dim ienum As IEnumerator = strAllowed.GetEnumerator

            While (ienum.MoveNext)
                If e.KeyChar.ToString().ToLower = ienum.Current.ToString().ToLower Then
                    Return
                End If
            End While

            If Not (IsNumeric(e.KeyChar) Or Asc(e.KeyChar) = 8) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            'If e.KeyCode = Keys.F6 And Panel3.Visible = True Then
            '    Panel3.Visible = False
            '    LineShape1.Visible = False
            '    LineShape2.Visible = False
            '    LineShape3.Visible = False
            '    LineShape4.Visible = False
            '    LineShape5.Visible = False
            'ElseIf e.KeyCode = Keys.F6 And Panel3.Visible = False Then
            '    Panel3.Visible = True
            '    LineShape1.Visible = True
            '    LineShape2.Visible = True
            '    LineShape3.Visible = True
            '    LineShape4.Visible = False
            '    LineShape5.Visible = True
            'End If

            Dim ds_itemdet As New DataSet
            Dim da_itemdet As SqlDataAdapter

            'Panel3.Visible = False
            'LineShape1.Visible = False
            'LineShape2.Visible = False
            'LineShape3.Visible = False
            'LineShape5.Visible = False

            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            If Rowindex_t >= 0 And colindex_t >= 0 Then
                Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value))

                Dim cnt As Integer
                Sqlstr = "SELECT isnull(gm.GROUPNAME,'') as GROUPNAME " _
                    & "  , " _
                    & "    isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTERFORMAT2  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                    & "  where im.itemid =" & Itemid_t & ""

                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da_itemdet = New SqlDataAdapter(cmd)
                ds_itemdet = New DataSet
                ds_itemdet.Clear()
                da_itemdet.Fill(ds_itemdet)
                cnt = ds_itemdet.Tables(0).Rows.Count

                If cnt > 0 Then
                    'Panel3.Visible = True
                    'LineShape1.Visible = True
                    'LineShape2.Visible = True
                    'LineShape3.Visible = True
                    'LineShape5.Visible = True
                    'txt_group.Text = ds_itemdet.Tables(0).Rows(0).Item("GROUPNAME").ToString
                    'txt_itemtype.Text = ds_itemdet.Tables(0).Rows(0).Item("ITEMTYPE").ToString
                    'txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                    'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                    'txt_taxperc.Text = ds_itemdet.Tables(0).Rows(0).Item("taxperc").ToString
                    txt_costrate.Text = ds_itemdet.Tables(0).Rows(0).Item("COSTPRICE").ToString
                End If
            End If

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = e.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name
                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellContentClick
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                'GridView1.Columns(fields1.c1_qty).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_qty).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                ' GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).DefaultCellStyle.Format = "#.00"
                GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#0"
                GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"
                '  GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#"
                '  GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"
            End If

            Dim Decimal_t As Double = 0
            Dim Format_t As String = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            'If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            '    Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value))

            '    Decimal_tt = ""
            '    For k = 1 To Decimal_t
            '        Decimal_tt = String.Concat(Decimal_tt, "0")
            '    Next

            '    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
            '    GridView1.Rows(e.RowIndex).Cells(fields1.c1_qty).Style.Format = Format_t
            'End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Delegate Sub SetColumnIndex(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
    Delegate Sub SetColumnIndex1(ByVal rowindex As Integer, ByVal columnindex As Integer, ByVal dgv As DataGridView)

    Private Sub GridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellEndEdit
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name

                Me.celWasEndEdit = GridView1(e.ColumnIndex, e.RowIndex)

                Dim method2 As New SetColumnIndex1(AddressOf Gridfindfom)
                Me.GridView1.BeginInvoke(method2, Rowindex_t, colindex_t, GridView1)

                If GridView1.Rows.Count - 1 = e.RowIndex Then
                    If IsValidRow(GridView1, "C_Itemdes") Then
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
                GridView1.Columns(fields1.c1_qty).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).DefaultCellStyle.Format = "#.00"
                GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#"
                GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"

                'Dim Decimal_t As Double = 0
                'Dim Format_t As String = ""
                'Dim Decimal_tt As String = ""
                'Dim k As Integer = 0

                'If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                '    Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value))
                '    Decimal_tt = ""
                '    For k = 1 To Decimal_t
                '        Decimal_tt = String.Concat(Decimal_tt, "0")
                '    Next
                '    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                '    GridView1.Rows(e.RowIndex).Cells(fields1.c1_qty).Style.Format = Format_t
                'End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        Try
            Dim tmpqty_t As Double, tmprate_t As Double, tmpamt_t As Double

            If (e.ColumnIndex = fields1.c1_rate Or e.ColumnIndex = fields1.c1_qty Or e.ColumnIndex = fields1.c1_amount) And e.RowIndex >= 0 Then
                tmpqty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_qty, e.RowIndex).Value)
                tmprate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rate, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_rate, e.RowIndex).Value)

                tmpamt_t = tmpqty_t * tmprate_t

                GridView1.Item(fields1.c1_amount, e.RowIndex).Value = tmpamt_t

                Call calcnetamt()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Dim tb As TextBox = CType(e.Control, TextBox)
        Select Case GridView1.CurrentCell.ColumnIndex
            Case fields1.c1_rate, fields1.c1_itemdesc, fields1.c1_qty, fields1.c1_amount
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
                AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End Select
    End Sub

    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        Try
            If e.Control And e.KeyCode.ToString = "R" Then
                Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value))
                If Itemid_t <> 0 Then
                    Dim frmrateupd As New frmitemrateupdate
                    frmrateupd.ShowInTaskbar = False
                    frmrateupd.StartPosition = FormStartPosition.CenterScreen
                    frmrateupd.Itemid_t = Itemid_t
                    frmrateupd.ShowDialog()
                End If
            End If
            'If e.KeyCode = Keys.F6 And Panel3.Visible = True Then
            '    Panel3.Visible = False
            '    LineShape1.Visible = False
            '    LineShape2.Visible = False
            '    LineShape3.Visible = False
            '    LineShape4.Visible = False
            '    LineShape5.Visible = False
            'ElseIf e.KeyCode = Keys.F6 And Panel3.Visible = False Then
            '    Panel3.Visible = True
            '    LineShape1.Visible = True
            '    LineShape2.Visible = True
            '    LineShape3.Visible = True
            '    LineShape4.Visible = False
            '    LineShape5.Visible = True
            'End If

            Dim tmpval_t As Double

            Dim ds_itemdet As New DataSet
            Dim da_itemdet As SqlDataAdapter

            'Panel3.Visible = False
            'LineShape1.Visible = False
            'LineShape2.Visible = False
            'LineShape3.Visible = False
            'LineShape5.Visible = False

            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            If Rowindex_t >= 0 And colindex_t >= 0 Then
                Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value))

                Dim cnt As Integer
                Sqlstr = "SELECT isnull(gm.GROUPNAME,'') as GROUPNAME , isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTERFORMAT2 im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                    & "  where im.itemid =" & Itemid_t & ""

                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da_itemdet = New SqlDataAdapter(cmd)
                ds_itemdet = New DataSet
                ds_itemdet.Clear()
                da_itemdet.Fill(ds_itemdet)
                cnt = ds_itemdet.Tables(0).Rows.Count

                If cnt > 0 Then
                    'Panel3.Visible = True
                    'LineShape1.Visible = True
                    'LineShape2.Visible = True
                    'LineShape3.Visible = True
                    'LineShape5.Visible = True
                    'txt_group.Text = ds_itemdet.Tables(0).Rows(0).Item("GROUPNAME").ToString
                    'txt_itemtype.Text = ds_itemdet.Tables(0).Rows(0).Item("ITEMTYPE").ToString
                    'txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                    'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                    'txt_taxperc.Text = ds_itemdet.Tables(0).Rows(0).Item("taxperc").ToString
                    txt_costrate.Text = ds_itemdet.Tables(0).Rows(0).Item("COSTPRICE").ToString
                End If
            End If

            If e.KeyCode = Keys.Enter Then

                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)

                e.SuppressKeyPress = True

                'If colindex_t = fields1.c1_qty Then
                '    tmpval_t = IIf(IsDBNull(GridView1.Item(colindex_t, Rowindex_t).Value), 0, (GridView1.Item(colindex_t, Rowindex_t).Value))
                '    If tmpval_t <> 0 Then
                '        FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                '    End If
                'Else
                FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                'End If
            ElseIf e.KeyCode = Keys.Back Then
            GridView1.BeginEdit(True)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub cmd_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ok.Click
        Try
            If Trim(txt_vchnum.Text) = "" Then
                MsgBox("Quotation No Should not be Empty.")
                txt_vchnum.Focus()
            Else
                Call Saveproc(editflag)
                Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Deleteproc(ByVal Headerid_v As Double)
        Dim ds_tmppono As New DataSet
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                'Sqlstr = "Select Pono From Accs_Purchase_Detail  Where Pono = (Select H.Vchnum From Accs_Purorder_Header H Where H.Headerid =" & Headerid_v & ")  "
                'cmd = New SqlCommand(Sqlstr, conn)
                'cmd.CommandType = CommandType.Text
                'da = New SqlDataAdapter(cmd)
                'ds_tmppono = New DataSet
                'ds_tmppono.Clear()
                'da.Fill(ds_tmppono)

                Call GendelQuotation2(Headerid_v)

                'If ds_tmppono.Tables(0).Rows.Count > 0 Then
                '    MsgBox("Could not Be Deleted, Becaues it Could Be Referenced in Purchase Entry.")
                '    Exit Sub
                'Else
                'If Generateeventlogs_t = True Then
                '    GensaveEventlogs(Event_Conn, "INVOICE", Headerid_v, Genuid, "Delete", Now, Gencompid)
                'End If
                '  Call GendelQuotation(Headerid_v)
                'End If
                'Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Print_Rpt(ByVal Headerid_v As Double)
        Try
            'Dim tmpvchstng As String
            'Dim Tmplen As Double
            Cursor = Cursors.WaitCursor
            'Rm.ShowInTaskbar = False 'CALL OUTSIDE APPLICATION(REPORTS_APP)
            'Call GetVchnum(Headerid_v)
            'Call Getautoflds()
            'Tmplen = Suffix_t.Length
            'tmpvchstng = Vchnum_t.Remove(Vchnum_t.Length - Suffix_t.Length)
            'tmpvchstng = Vchnum_t.Remove(tmpvchstng.Length - Noofdigit_t)
            'Call CheckRptname(tmpvchstng)
            rm.Init(conn, "QUOTATION", Servername_t, Headerid_v, Nothing, Nothing, "", Databasename_t, 0, False, , , , , , SystemName_t)
            rm.StartPosition = FormStartPosition.CenterScreen
            rm.ShowDialog()
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub setUserpermission()
        Try
            If AllowFormEdit_t = True Then
                UserPermission(Me)
                GridView1.Enabled = True
                GridView1.ReadOnly = True
                Gridview2.Enabled = True
                Gridview2.ReadOnly = True
                Exit Sub
            End If

            If UCase(Genuname) = "ADMIN" Then
                Exit Sub
            End If
            Dim Allowadd_t As Integer, Allowedit_t As Integer, Allowdel_t As Integer
            Dim diffdays_t As Integer
            Dim regDate As Date = Date.Now()
            diffdays_t = DateDiff(DateInterval.Day, DTP_Vchdate.Value, regDate)
            Sqlstr = " Select Process, ISNULL(ALLOWADD,0) As Allowadd, ISNULL(ALLOWEDIT,0) As Allowedit, " _
                   & " ISNULL(ALLOWDEL,0) As Allowdel From USERPERMISSION where PROCESS = '" & Process & "' and ISNULL(uid,0)= " & Genuid & " "

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                Do While dr.Read()
                    Allowadd_t = (dr.GetValue(1))
                    Allowedit_t = (dr.GetValue(2))
                    Allowdel_t = (dr.GetValue(3))

                    If Allowedit_t = 1 Then
                        If diffdays_t > Gendays Then
                            Call Buttonsettings(False, "Close")
                        Else
                            Call Buttonsettings(True, "Cancel")
                        End If
                    Else
                        Call Buttonsettings(False, "Close")
                    End If
                Loop
            End If
            dr.Close()

            If ShowFormFromReport = True Then
                Me.cmd_ok.Visible = False
                Me.cmd_cancel.Text = "&Close"
                Me.GridView1.ReadOnly = True
                Me.txt_party.ReadOnly = True
                Me.Txt_despatch.ReadOnly = True
                Me.txt_narration.ReadOnly = True
                Me.DTP_Vchdate.Enabled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Buttonsettings(ByVal visflag_t As Boolean, ByVal cap_t As String)
        Try
            cmd_ok.Visible = visflag_t
            cmd_cancel.Text = cap_t
            Panel2.Enabled = visflag_t
            txt_narration.Enabled = visflag_t
            Call gridreadonly(GridView1, Not visflag_t, "C_Itemdes", "c_Rate", "C_Amount")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub Locationfindfrm()
    '    Try

    '        Dim cnt As Integer

    '        cnt = ds_location.Tables(0).Rows.Count

    '        If cnt > 0 Then
    '            VisibleCols.Add("godownname")
    '            Colheads.Add("Location")

    '            fm.Frm_Width = 400
    '            fm.Frm_Height = 300
    '            fm.Frm_Left = 350
    '            fm.Frm_Top = 150
    '            fm.MainForm = New frm_quotation
    '            fm.Active_ctlname = "txt_location"
    '            Csize.Add(275)

    '            If cnt = 1 Then
    '                txt_location.Text = ds_location.Tables(0).Rows(0).Item("godownname").ToString
    '                Locationid_t = ds_location.Tables(0).Rows(0).Item("masterid")
    '            Else
    '                tmppassstr = txt_location.Text
    '                fm.EXECUTE(conn, ds_location, VisibleCols, Colheads, Locationid_t, "", False, Csize, "", False, False, "", tmppassstr)
    '                txt_location.Text = fm.VarNew
    '                Locationid_t = fm.VarNewid
    '            End If

    '            VisibleCols.Remove(1)
    '            Colheads.Remove(1)
    '            Csize.Remove(1)
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub Partyfindfrm()
        Try
            Dim cnt As Integer
            Dim ds_settings As New DataSet
            Dim ds_prevparty As New DataSet
            Dim da_prevparty As SqlDataAdapter
            Dim da_settings As SqlDataAdapter
            Dim dscnt As Integer
            Dim Val_t As Integer
            'Sqlstr = "SELECT ISNULL(NUMERICVALUE,0 ) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS  ='TAMIL FONT' "
            'cmd = New SqlCommand(Sqlstr, conn)
            'cmd.CommandType = CommandType.Text
            'da_settings = New SqlDataAdapter(cmd)
            'ds_settings = New DataSet
            'ds_settings.Clear()
            'da_settings.Fill(ds_settings)
            'dscnt = ds_settings.Tables(0).Rows.Count
            'If dscnt > 0 Then
            '    Val_t = ds_settings.Tables(0).Rows(0).Item("numericvalue")
            'Else
            '    Val_t = 0
            'End If
            'If Val_t = 0 Then
            '    ds_party.Clear()
            '    ds_party = Nothing
            '    Sqlstr = "Select Ptyname,ptycode From PARTY Order By Ptyname"
            '    cmd = New SqlCommand(Sqlstr, conn)
            '    cmd.CommandType = CommandType.Text
            '    da_party = New SqlDataAdapter(cmd)
            '    ds_party = New DataSet
            '    ds_party.Clear()
            '    da_party.Fill(ds_party)
            '    cnt = ds_party.Tables(0).Rows.Count
            'Else
            ds_party.Clear()
            ds_party = Nothing
            Sqlstr = "Select Ptyname,ptycode From PARTY Order By Ptyname"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_party = New SqlDataAdapter(cmd)
            ds_party = New DataSet
            ds_party.Clear()
            da_party.Fill(ds_party)
            cnt = ds_party.Tables(0).Rows.Count
            ' End If

            If cnt = 0 Then Partyid_t = 0

            If cnt > 0 Then
                VisibleCols.Add("PTYNAME")
                Colheads.Add("Customer")

                fm.Frm_Width = 400
                fm.Frm_Height = 300
                fm.Frm_Left = 880
                fm.Frm_Top = 101
                fm.MainForm = New frm_quotation
                fm.Active_ctlname = "txt_party"
                Csize.Add(350)

                If cnt = 1 Then
                    txt_party.Text = ds_party.Tables(0).Rows(0).Item("party").ToString
                    Partyid_t = ds_party.Tables(0).Rows(0).Item("ptycode")
                Else
                    tmppassstr = txt_party.Text
                    fm.EXECUTE(conn, ds_party, VisibleCols, Colheads, Partyid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    txt_party.Text = fm.VarNew
                    Partyid_t = fm.VarNewid
                End If
                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

            ds_prevparty.Clear()
            ds_prevparty = Nothing
            Sqlstr = "SELECT TOP 4 H.VCHNUM AS QUONO,CONVERT(VARCHAR(10),H.VCHDATE,103) AS QUODATE,P.PTYNAME AS PARTY,DM.DESPATCH AS DESPATCH FROM QUOTATION_HEADER2 H JOIN PARTY P ON P.PTYCODE = H.PARTYID LEFT JOIN despatch_master DM ON DM.MASTERID = H.DESPATCHID where h.partyid =" & Partyid_t & " AND H.HEADERID <> " & Headerid_t & " ORDER BY H.VCHDATE DESC "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_prevparty = New SqlDataAdapter(cmd)
            ds_prevparty = New DataSet
            ds_prevparty.Clear()
            da_prevparty.Fill(ds_prevparty)
            cnt = ds_prevparty.Tables(0).Rows.Count

            Gridview2.Rows.Clear()

            If cnt > 0 Then
                Gridview2.Rows.Add(cnt)

                For i = 0 To Gridview2.Rows.Count - 2
                    Gridview2.Rows(i).Cells(fields3.c2_quono).Value = ds_prevparty.Tables(0).Rows(i).Item("quono").ToString
                    Gridview2.Rows(i).Cells(fields3.c2_quodate).Value = ds_prevparty.Tables(0).Rows(i).Item("quodate").ToString
                    Gridview2.Rows(i).Cells(fields3.c2_customer).Value = ds_prevparty.Tables(0).Rows(i).Item("party").ToString
                    Gridview2.Rows(i).Cells(fields3.c2_despatch).Value = ds_prevparty.Tables(0).Rows(i).Item("despatch").ToString
                Next
            End If

            Gridview2.ReadOnly = True
            Gridview2.AllowUserToResizeColumns = False
            Gridview2.AllowUserToResizeRows = False
            Gridview2.Columns(0).Width = 100
            Gridview2.Columns(1).Width = 90
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DespatchFindfrm()
        Try
            Dim cnt As Integer
            Dim ds_settings As New DataSet
            Dim da_settings As SqlDataAdapter
            Dim dscnt As Integer
            Dim Val_t As Integer

            cnt = ds_despatch.Tables(0).Rows.Count

            If cnt > 0 Then
                VisibleCols.Add("despatch")
                Colheads.Add("Despatch")

                fm.Frm_Width = 400
                fm.Frm_Height = 300
                fm.Frm_Left = 880
                fm.Frm_Top = 101
                fm.MainForm = New frm_quotation
                fm.Active_ctlname = "txt_party"
                Csize.Add(350)

                If cnt = 1 Then
                    Txt_despatch.Text = ds_despatch.Tables(0).Rows(0).Item("despatch").ToString
                    Despatchid_t = ds_despatch.Tables(0).Rows(0).Item("masterid")
                Else
                    tmppassstr = Txt_despatch.Text
                    fm.EXECUTE(conn, ds_despatch, VisibleCols, Colheads, Despatchid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    Txt_despatch.Text = fm.VarNew
                    Despatchid_t = fm.VarNewid
                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub Cardfindfrm()
    '    Try
    '        Dim cnt As Integer
    '        Dim dscnt As Integer
    '        Dim sqlstr As String
    '        Dim Val_t As Integer
    '        Dim ds_card As New DataSet
    '        Dim da_card As SqlDataAdapter

    '        ds_card.Clear()
    '        ds_card = Nothing
    '        sqlstr = "Select mobileno,cardid,cardno,name From cardno_master Order By cardno"
    '        cmd = New SqlCommand(Sqlstr, conn)
    '        cmd.CommandType = CommandType.Text
    '        da_card = New SqlDataAdapter(cmd)
    '        ds_card = New DataSet
    '        ds_card.Clear()
    '        da_card.Fill(ds_card)
    '        cnt = ds_card.Tables(0).Rows.Count

    '        If cnt > 0 Then
    '            VisibleCols.Add("mobileno")
    '            VisibleCols.Add("cardno")
    '            VisibleCols.Add("name")

    '            Colheads.Add("Mobile No")
    '            Colheads.Add("Card No")
    '            Colheads.Add("Name")

    '            fm.Frm_Width = 500
    '            fm.Frm_Height = 300
    '            fm.Frm_Left = 770
    '            fm.Frm_Top = 172

    '            fm.MainForm = New frm_quotation
    '            fm.Active_ctlname = "Txt_custno"

    '            Csize.Add(100)
    '            Csize.Add(200)
    '            Csize.Add(150)

    '            If cnt = 1 Then
    '                Txt_custno.Text = ds_card.Tables(0).Rows(0).Item("cardno").ToString
    '                txt_name.Text = ds_card.Tables(0).Rows(0).Item("name").ToString
    '                Cardid_t = ds_card.Tables(0).Rows(0).Item("cardid")
    '            Else
    '                tmppassstr = Txt_custno.Text
    '                fm.EXECUTE(conn, ds_card, VisibleCols, Colheads, Cardid_t, "", False, Csize, "", False, False, "", tmppassstr, sqlstr)
    '                Txt_custno.Text = fm.VarNew
    '                Cardid_t = fm.VarNewid

    '                If ds_card.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
    '                    'If Formshown_t = True And ds_item.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
    '                    ds_card.Tables(0).DefaultView.RowFilter = " cardid = " & Cardid_t & " "
    '                    index_t = ds_card.Tables(0).Rows.IndexOf(ds_card.Tables(0).DefaultView.Item(0).Row)
    '                    txt_name.Text = ds_card.Tables(0).Rows(index_t).Item("name").ToString
    '                    Txt_custno.Text = ds_card.Tables(0).Rows(index_t).Item("CARDNO").ToString
    '                Else
    '                    txt_name.Text = ""
    '                End If

    '            End If

    '            VisibleCols.Remove(1)
    '            Colheads.Remove(1)
    '            Csize.Remove(1)

    '            VisibleCols.Remove(1)
    '            Colheads.Remove(1)
    '            Csize.Remove(1)

    '            VisibleCols.Remove(1)
    '            Colheads.Remove(1)
    '            Csize.Remove(1)

    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub frm_quotation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Controlno_t = "Add" Then
                editflag = False
            ElseIf Controlno_t = "Edit" Then
                editflag = True
            End If

            ' Cbo_itemtype.SelectedIndex = 0
            Call opnconn()
            Call Execute()

            Me.Show()
            txt_party.Focus()
            Panel3.Visible = False
            'LineShape1.Visible = False
            'LineShape2.Visible = False
            'LineShape3.Visible = False
            'LineShape5.Visible = False

            '        If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            '        Dim converter As System.ComponentModel.TypeConverter = _
            'System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            '        font1 = _
            ' CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            '        font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
            '        'font1 = New Font(Font_m, CType(Size_m, Single))

            '        txt_party.Font = font1
            '        For i = 0 To GridView1.Rows.Count - 1
            '            GridView1.Rows(i).DefaultCellStyle.Font = font1
            '        Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        Dim ds_itemdet As New DataSet
        Dim da_itemdet As SqlDataAdapter
        'Panel3.Visible = False
        'LineShape1.Visible = False
        'LineShape2.Visible = False
        'LineShape3.Visible = False
        'LineShape5.Visible = False
        colindex_t = GridView1.CurrentCell.ColumnIndex
        Rowindex_t = GridView1.CurrentCell.RowIndex
        Colname_t = GridView1.Columns(colindex_t).Name

        If Rowindex_t >= 0 And colindex_t >= 0 Then
            Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value))

            Dim cnt As Integer
            Sqlstr = "SELECT isnull(gm.GROUPNAME,'') as GROUPNAME ,isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTERFORMAT2  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                & "   where im.itemid =" & Itemid_t & ""

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_itemdet = New SqlDataAdapter(cmd)
            ds_itemdet = New DataSet
            ds_itemdet.Clear()
            da_itemdet.Fill(ds_itemdet)
            cnt = ds_itemdet.Tables(0).Rows.Count

            If cnt > 0 Then
                'Panel3.Visible = True
                'LineShape1.Visible = True
                'LineShape2.Visible = True
                'LineShape3.Visible = True
                'LineShape5.Visible = True
                ' txt_group.Text = ds_itemdet.Tables(0).Rows(0).Item("GROUPNAME").ToString
                ' txt_itemtype.Text = ds_itemdet.Tables(0).Rows(0).Item("ITEMTYPE").ToString
                'txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                ' txt_taxperc.Text = ds_itemdet.Tables(0).Rows(0).Item("taxperc").ToString
                txt_costrate.Text = ds_itemdet.Tables(0).Rows(0).Item("COSTPRICE").ToString
            End If
        End If
    End Sub

    Private Sub GridView1_KeyUp(sender As Object, e As KeyEventArgs) Handles GridView1.KeyUp
        If e.KeyCode = Keys.F6 And Panel3.Visible = True Then
            Panel3.Visible = False
            'LineShape1.Visible = False
            'LineShape2.Visible = False
            'LineShape3.Visible = False
            'LineShape4.Visible = False
            'LineShape5.Visible = False
        ElseIf e.KeyCode = Keys.F6 And Panel3.Visible = False Then
            Panel3.Visible = True
            'LineShape1.Visible = True
            'LineShape2.Visible = True
            'LineShape3.Visible = True
            'LineShape4.Visible = False
            'LineShape5.Visible = True
        End If

        Dim ds_itemdet As New DataSet
        Dim da_itemdet As SqlDataAdapter

        'Panel3.Visible = False
        'LineShape1.Visible = False
        'LineShape2.Visible = False
        'LineShape3.Visible = False
        'LineShape5.Visible = False

        colindex_t = GridView1.CurrentCell.ColumnIndex
        Rowindex_t = GridView1.CurrentCell.RowIndex
        Colname_t = GridView1.Columns(colindex_t).Name

        If Rowindex_t >= 0 And colindex_t >= 0 Then
            Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(fields1.c1_itemid).Value))

            Dim cnt As Integer
            Sqlstr = "SELECT isnull(gm.GROUPNAME,'') as GROUPNAME , isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTERFORMAT2  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                & "   where im.itemid =" & Itemid_t & ""

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_itemdet = New SqlDataAdapter(cmd)
            ds_itemdet = New DataSet
            ds_itemdet.Clear()
            da_itemdet.Fill(ds_itemdet)
            cnt = ds_itemdet.Tables(0).Rows.Count

            If cnt > 0 Then
                'Panel3.Visible = True
                'LineShape1.Visible = True
                'LineShape2.Visible = True
                'LineShape3.Visible = True
                'LineShape5.Visible = True
                ' txt_group.Text = ds_itemdet.Tables(0).Rows(0).Item("GROUPNAME").ToString
                ' txt_itemtype.Text = ds_itemdet.Tables(0).Rows(0).Item("ITEMTYPE").ToString
                'txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                ' txt_taxperc.Text = ds_itemdet.Tables(0).Rows(0).Item("taxperc").ToString
                txt_costrate.Text = ds_itemdet.Tables(0).Rows(0).Item("COSTPRICE").ToString
            End If
        End If
    End Sub

    Private Sub GridView1_Leave(sender As Object, e As EventArgs) Handles GridView1.Leave
        Panel3.Visible = False
        'LineShape1.Visible = False
        'LineShape2.Visible = False
        'LineShape3.Visible = False
        'LineShape5.Visible = False
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

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Vchdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_party.Focus()
        End If
    End Sub

    Private Sub txt_party_Click(sender As Object, e As EventArgs) Handles txt_party.Click
        If Showpartyfindform = True Then Call Partyfindfrm()
        If txt_party.Text = "" Then Partyid_t = 0
    End Sub

    Private Sub txt_party_GotFocus(sender As Object, e As EventArgs) Handles txt_party.GotFocus
        txt_party.BackColor = Color.Yellow
    End Sub

    Private Sub txt_party_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_party.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Showpartyfindform = True Then Call Partyfindfrm()
            If txt_party.Text = "" Then Partyid_t = 0
            Txt_despatch.Focus()
        End If
    End Sub

    Private Sub txt_party_LostFocus(sender As Object, e As EventArgs) Handles txt_party.LostFocus
        txt_party.BackColor = Color.White
    End Sub

    Private Sub Txt_despatch_Click(sender As Object, e As EventArgs) Handles Txt_despatch.Click
        Call DespatchFindfrm()
    End Sub

    Private Sub Txt_despatch_GotFocus(sender As Object, e As EventArgs) Handles Txt_despatch.GotFocus
        Txt_despatch.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_despatch_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_despatch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call DespatchFindfrm()
            GridView1.Focus()
        End If
    End Sub

    Private Sub Txt_despatch_LostFocus(sender As Object, e As EventArgs) Handles Txt_despatch.LostFocus
        Txt_despatch.BackColor = Color.White
    End Sub

End Class