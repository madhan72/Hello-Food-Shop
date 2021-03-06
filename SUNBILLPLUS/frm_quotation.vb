﻿Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App

Public Class frm_quotation

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da, da_head, da_detl, da_party As SqlDataAdapter
    Dim bs As New BindingSource
    Dim ds, ds_location, ds_party, ds1, ds_head, ds_detl, ds_item, ds_acdesc As New DataSet
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

    Enum fields1
        c1_itemcode = 0
        c1_itemid = 1
        c1_itemdesc = 2
        c1_uom = 3
        c1_uomid = 4
        c1_qty = 5
        c1_decimal = 6
        c1_rate = 7
        c1_costrate = 8
        c1_amount = 9
        c1_remarks = 10
        c1_offeraddqty = 11
        c1_offerlessqty = 12
        c1_addrs = 13
        c1_lessrs = 14
        c1_selrate = 15
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

            Call gridreadonly(GridView1, True, "C_Itemdes", "c_uom", "c_uomid", "C_Amount")
            Call gridreadonly_Color(GridView1, Readonlycolor_t, "C_Itemdes", "c_uom", "c_uomid", "C_Amount")
            Call gridvisible(GridView1, False, "C_Itemid", "c_uomid", "c_decimal", "c_offeraddqty", "c_offerlessqty", "c_addrs", "c_lessrs", "c_selrate")
            Call gridvisible(GridView2, False, "C_Descid")

            ds_settings = Nothing
            ' ds_settings.Clear()
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

            If Headcnt_t > 0 Then
                Call storechars(Headcnt_t)
            Else
                Call clearchars()

                ds_loc1.Clear()
                ds_loc1 = Nothing
                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
                cmd.CommandText = "GET_DEFAULTLOCATION"
                da_loc1 = New SqlDataAdapter(cmd)
                ds_loc1 = New DataSet
                da_loc1.Fill(ds_loc1)

                cnt1 = ds_loc1.Tables(0).Rows.Count

                If cnt1 > 0 Then
                    Locationid_t = ds_loc1.Tables(0).Rows(0).Item("LOCATIONID")
                    txt_location.Text = ds_loc1.Tables(0).Rows(0).Item("GODOWNNAME").ToString
                End If


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
            txt_totamount.Text = "0.00"
            txt_totqty.Text = "0"

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
            cmd = New SqlCommand("SELECT H.VCHNUM,H.HEADERID,H.VCHDATE,H.ITEMTYPE,H.NARRATION,ISNULL(H.PARTYID,0) AS PARTYID,case when h.partyid=0 or h.partyid is null then H.CUSTOMER else p.PTYNAME end as Customer,H.REFRENCE,isnull(cm.CARDNO,'') as CARDNO,isnull(CM.NAME,'') as NAME,isnull(H.CARDID,0) as CARDID,ISNULL(H.LOCATIONID,0) AS LOCATIONID,ISNULL(GM.GODOWNNAME,'') AS LOCATION FROM QUOTATION_HEADER H " _
                                 & "LEFT JOIN GODOWN_MASTER GM ON GM.MASTERID = H.LOCATIONID LEFT JOIN cardno_MASTER cM ON cM.cardid = H.cardid LEFT JOIN PARTY P ON P.PTYCODE = H.PARTYID WHERE H.HEADERID = " & Headerid_v & " ", conn)
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
            Txt_custno.Text = ds_head.Tables(0).Rows(rowid_t).Item("CARDNO").ToString
            Cardid_t = ds_head.Tables(0).Rows(rowid_t).Item("CARDID")
            txt_name.Text = ds_head.Tables(0).Rows(rowid_t).Item("NAME").ToString
            'Txt_custno.Text = ds_head.Tables(0).Rows(rowid_t).Item("CUSTNO").ToString
            Locationid_t = ds_head.Tables(0).Rows(rowid_t).Item("LOCATIONID")
            Partyid_t = ds_head.Tables(0).Rows(rowid_t).Item("PARTYID")
            txt_location.Text = ds_head.Tables(0).Rows(rowid_t).Item("LOCATION").ToString
            txt_reference.Text = ds_head.Tables(0).Rows(rowid_t).Item("REFRENCE").ToString
            Cbo_itemtype.SelectedItem = ds_head.Tables(0).Rows(rowid_t).Item("ITEMTYPE").ToString

            ds_detl.Clear()
            ds_detl = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_QUOTATION_DETAIL"
            cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            da_detl = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            da_detl.Fill(ds_detl)

            GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#"
            GridView1.Columns(fields1.c1_rate).DefaultCellStyle.Format = "#.00"
            GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"

            Detlcnt_t = ds_detl.Tables(0).Rows.Count

            If Detlcnt_t > 0 Then
                GridView1.Rows.Add(Detlcnt_t + 1)
                For i = 0 To Detlcnt_t - 1
                    GridView1.Rows(i).Cells(fields1.c1_itemcode).Value = ds_detl.Tables(0).Rows(i).Item("Itemcode")
                    GridView1.Rows(i).Cells(fields1.c1_itemid).Value = ds_detl.Tables(0).Rows(i).Item("Itemid")
                    GridView1.Rows(i).Cells(fields1.c1_qty).Value = ds_detl.Tables(0).Rows(i).Item("Qty")
                    GridView1.Rows(i).Cells(fields1.c1_offeraddqty).Value = ds_detl.Tables(0).Rows(i).Item("OFFERADDQTY")
                    GridView1.Rows(i).Cells(fields1.c1_offerlessqty).Value = ds_detl.Tables(0).Rows(i).Item("OFFERLESSQTY")
                    GridView1.Rows(i).Cells(fields1.c1_addrs).Value = ds_detl.Tables(0).Rows(i).Item("ADDQTY")
                    GridView1.Rows(i).Cells(fields1.c1_lessrs).Value = ds_detl.Tables(0).Rows(i).Item("LESSQTY")
                    GridView1.Rows(i).Cells(fields1.c1_rate).Value = ds_detl.Tables(0).Rows(i).Item("Rate")
                    GridView1.Rows(i).Cells(fields1.c1_selrate).Value = ds_detl.Tables(0).Rows(i).Item("selrate")
                    GridView1.Rows(i).Cells(fields1.c1_amount).Value = ds_detl.Tables(0).Rows(i).Item("Amount")
                    If Val_t = 1 Then GridView1.Rows(i).Cells(fields1.c1_itemdesc).Value = ds_detl.Tables(0).Rows(i).Item("ITEMTAMILDES")
                    If Val_t = 0 Then GridView1.Rows(i).Cells(fields1.c1_itemdesc).Value = ds_detl.Tables(0).Rows(i).Item("ITEMDES")
                    GridView1.Rows(i).Cells(fields1.c1_remarks).Value = ds_detl.Tables(0).Rows(i).Item("REMARKS")
                    If Val_t = 0 Then GridView1.Rows(i).Cells(fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("uom")
                    If Val_t = 1 Then GridView1.Rows(i).Cells(fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("tamiluom")
                    GridView1.Rows(i).Cells(fields1.c1_uomid).Value = ds_detl.Tables(0).Rows(i).Item("uomid")
                    GridView1.Rows(i).Cells(fields1.c1_decimal).Value = ds_detl.Tables(0).Rows(i).Item("noofdecimal")
                    GridView1.Rows(i).Cells(fields1.c1_costrate).Value = ds_detl.Tables(0).Rows(i).Item("COSTRATE")

                    Decimal_t = IIf(IsDBNull(GridView1.Rows(i).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(i).Cells(fields1.c1_decimal).Value))

                    Decimal_tt = ""
                    For k = 1 To Decimal_t
                        Decimal_tt = String.Concat(Decimal_tt, "0")
                    Next

                    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                    GridView1.Rows(i).Cells(fields1.c1_qty).Style.Format = Format_t

                Next
            End If

            ds_detl.Clear()
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_QUOTATIONADDLESS"
            cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            da_detl = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            da_detl.Fill(ds_detl)

            Detlcnt_t = ds_detl.Tables(0).Rows.Count

            Dim rwcnt_t As Double

            rwcnt_t = GridView2.Rows.Count


            If Detlcnt_t > 0 Then
                GridView2.Rows.Add(Detlcnt_t)

                For j = 0 To Detlcnt_t - 1
                    GridView2.Rows(j).Cells(fields2.c2_Type).Value = ds_detl.Tables(0).Rows(j).Item("Altype")
                    GridView2.Rows(j).Cells(fields2.c2_Desc).Value = ds_detl.Tables(0).Rows(j).Item("ALDESCRIPTION")
                    GridView2.Rows(j).Cells(fields2.c2_Descid).Value = ds_detl.Tables(0).Rows(j).Item("Descid")
                    GridView2.Rows(j).Cells(fields2.c2_Perc).Value = ds_detl.Tables(0).Rows(j).Item("Perc")
                    GridView2.Rows(j).Cells(fields2.c2_Amount).Value = ds_detl.Tables(0).Rows(j).Item("Amount")
                Next
            End If

            Call calcnetamt()

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

    Private Sub GridView2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GridView2.DataError
        Try
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                If e.Exception.Message = "Input string was not in a correct format." Then
                    e.Cancel = True
                    Me.GridView2.Item((e.ColumnIndex), (e.RowIndex)).Value = "0"
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub calcnetamt()
        Try
            Dim itemcnt As Integer = 0

            txt_totqty.Text = Format(Tot_Calc(GridView1, fields1.c1_qty), "#######0")
            txt_totamount.Text = Format(Tot_Calc(GridView1, fields1.c1_amount), "#######0.00")
            txt_totaddless.Text = Format(Tot_Calc(GridView2, fields2.c2_Amount), "#######0.00")
            txt_netamt.Text = Format((Val(txt_totamount.Text)) + (Val(txt_totaddless.Text)), "#######0")
            Txt_roundedoff.Text = Format((Val(txt_netamt.Text) - (Val(txt_totamount.Text) + Val(txt_totaddless.Text))), "##########0.00")
            txt_netamt.Text = Format(Val(txt_netamt.Text), "#######0.00")

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

    Private Sub GridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellClick
        Try
            colindex_t = GridView2.CurrentCell.ColumnIndex
            Rowindex_t = e.RowIndex
            Colname_t = GridView2.Columns(colindex_t).Name
            Call Addlessfindfrm(Rowindex_t, colindex_t, GridView2)
            calcnetamt()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellContentClick
        Try
            GridView2.Columns(fields2.c2_Perc).ValueType = GetType(Decimal)
            GridView2.Columns(fields2.c2_Amount).ValueType = GetType(Decimal)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellEndEdit
        Try
            Dim Flg_t As String, tmpamt_t As Double
            colindex_t = GridView2.CurrentCell.ColumnIndex
            Rowindex_t = GridView2.CurrentCell.RowIndex
            Colname_t = GridView2.Columns(colindex_t).Name

            Call Addlessfindfrm(Rowindex_t, colindex_t, GridView2)

            If (e.ColumnIndex = fields2.c2_Amount) And e.RowIndex >= 0 Then
                Flg_t = GridView2.Item(fields2.c2_Type, e.RowIndex).Value
                If Flg_t <> "" Then
                    tmpamt_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Amount, e.RowIndex).Value), 0, GridView2.Item(fields2.c2_Amount, e.RowIndex).Value)
                    If UCase(Flg_t) = "ADD" Then
                        tmpamt_t = tmpamt_t
                    Else
                        tmpamt_t = -1 * tmpamt_t
                    End If
                    GridView2.Item(fields2.c2_Amount, e.RowIndex).Value = tmpamt_t
                End If
                Call calcnetamt()
            End If

            If GridView2.Rows.Count - 1 = e.RowIndex Then
                If IsValidRow(GridView2, "C_Select", "C_Desc", "c_Adamount") Then
                    GridView2.Rows.Add(1)
                End If
            End If

            If Me.GridView2.CurrentCell.ColumnIndex <> Me.GridView2.ColumnCount - 1 Then
                If GridView2.CurrentCell.RowIndex = 0 Then
                    'SendKeys.Send("{TAB}")
                    SendKeys.Send("{UP}")
                Else
                    'SendKeys.Send("{TAB}")
                    SendKeys.Send("{UP}")
                End If
            Else
                SendKeys.Send("{HOME}")
                SendKeys.Send("{DOWN}")
            End If
            'FindNextCell(GridView2, Rowindex_t, colindex_t + 1)  'checking from Next 
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellEnter
        Try
            GridView2.Columns(fields2.c2_Perc).ValueType = GetType(Decimal)
            GridView2.Columns(fields2.c2_Amount).ValueType = GetType(Decimal)
            GridView2.Columns(fields2.c2_Perc).DefaultCellStyle.Format = "#.00"
            GridView2.Columns(fields2.c2_Amount).DefaultCellStyle.Format = "#.00"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellValueChanged
        Try
            Dim tmpamt_t As Double, tmpperc_t As Double
            Dim Flg_t As String, tmpformulaamt_t As Double, Tmpdescid_t As Double
            Dim Tmpflg_t As Boolean

            If (e.ColumnIndex = fields2.c2_Perc Or e.ColumnIndex = fields2.c2_Type Or e.ColumnIndex = fields2.c2_Descid) And e.RowIndex >= 0 Then
                For g As Integer = 0 To GridView2.Rows.Count - 1
                    Flg_t = GridView2.Item(fields2.c2_Type, g).Value
                    If Flg_t <> "" Then
                        tmpperc_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Perc, g).Value), 0, GridView2.Item(fields2.c2_Perc, g).Value)
                        Tmpdescid_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Descid, g).Value), 0, GridView2.Item(fields2.c2_Descid, g).Value)
                        tmpamt_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Amount, g).Value), 0, GridView2.Item(fields2.c2_Amount, g).Value)

                        If CInt(tmpamt_t) < 0 Then
                            tmpamt_t = -1 * tmpamt_t
                        Else
                            tmpamt_t = 1 * tmpamt_t
                        End If

                        Tmpflg_t = False
                        If tmpamt_t <> 0 And tmpperc_t = 0 Then
                            If UCase(Flg_t) = "ADD" Then
                                tmpamt_t = CUInt(tmpamt_t)
                            Else
                                tmpamt_t = -1 * tmpamt_t
                            End If
                            Tmpflg_t = True
                        End If
                        If Tmpflg_t = False Then
                            tmpformulaamt_t = Addlessformulacalc(Tmpdescid_t)
                            If Tmpdescid_t = -100 Or Tmpdescid_t = -101 Then
                                ' tmpamt_t = Val(Txt_Discamt.Text)
                            End If
                            If Addlessformulaflg_t = True Then
                                If tmpperc_t <> 0 Then
                                    tmpamt_t = (tmpformulaamt_t * tmpperc_t / 100)
                                End If
                            Else
                                If tmpperc_t <> 0 Then
                                    tmpamt_t = (Val(txt_totamount.Text) * tmpperc_t / 100)
                                End If
                            End If
                            If UCase(Flg_t) = "ADD" Then
                                tmpamt_t = tmpamt_t
                            Else
                                tmpamt_t = -1 * tmpamt_t
                            End If
                        End If
                        GridView2.Item(fields2.c2_Amount, g).Value = tmpamt_t
                    End If
                Next

            End If

            Call calcnetamt()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Addless_Calc()
        Try
            Dim perc_t As Double, amt_t As Double, Tmpdescid_t As Double, tmpamt_t As Double, tmpperc_t As Double
            Dim Flg_t As String, tmpformulaamt_t As Double, Tmpflg_t As Boolean
            For i = 0 To GridView2.Rows.Count - 1
                Flg_t = GridView2.Item(fields2.c2_Type, i).Value
                If Flg_t <> "" Then
                    perc_t = GridView2.Item(fields2.c2_Perc, i).Value
                    Tmpdescid_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Descid, i).Value), 0, GridView2.Item(fields2.c2_Descid, i).Value)
                    tmpperc_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Perc, i).Value), 0, GridView2.Item(fields2.c2_Perc, i).Value)
                    tmpamt_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Amount, i).Value), 0, GridView2.Item(fields2.c2_Amount, i).Value)

                    If CInt(tmpamt_t) < 0 Then
                        tmpamt_t = -1 * tmpamt_t
                    Else
                        tmpamt_t = 1 * tmpamt_t
                    End If

                    Tmpflg_t = False
                    If tmpamt_t <> 0 And tmpperc_t = 0 Then
                        'If Tmpdescid_t = -100 Or Tmpdescid_t = -101 Then
                        '    tmpamt_t = Val(Txt_Disca.Text)
                        'End If
                        If UCase(Flg_t) = "ADD" Then
                            tmpamt_t = tmpamt_t
                        Else
                            tmpamt_t = -1 * tmpamt_t
                        End If
                        Tmpflg_t = True
                        If Tmpdescid_t = -5 Or Tmpdescid_t = -15 Then
                            GridView2.Item(fields2.c2_Amount, i).Value = Math.Round(tmpamt_t, 0)
                        Else
                            GridView2.Item(fields2.c2_Amount, i).Value = tmpamt_t
                        End If
                    End If
                    If Tmpflg_t = False Then
                        tmpformulaamt_t = Addlessformulacalc(Tmpdescid_t)
                        If Addlessformulaflg_t = True Then
                            amt_t = (tmpformulaamt_t * perc_t / 100)
                        Else
                            amt_t = (Val(txt_totamount.Text) * perc_t / 100)
                        End If

                        If UCase(Flg_t) = "ADD" Then
                            amt_t = amt_t
                        Else
                            amt_t = -1 * amt_t
                        End If

                        ''If Tmpdescid_t = -5 Or Tmpdescid_t = -15 Then
                        ''If amt_t <> 0 Then
                        ''    GridView2.Item(fields2.c2_Amount, i).Value = amt_t
                        ''End If
                        ' If ds_addrndchk.Tables(0).Rows.Count > 0 Then
                        ' If ds_addrndchk.Tables(0).Rows(0).Item("NUMERICVALUE") = 1 Then
                        '     GridView2.Item(fields2.c2_Amount, i).Value = Math.Round(amt_t, 0)
                        ' Else
                        GridView2.Item(fields2.c2_Amount, i).Value = amt_t
                        ' End If
                    End If

                    'End If
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView2_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles GridView2.EditingControlShowing
        Select Case GridView2.CurrentCell.ColumnIndex
            Case fields2.c2_Desc
                RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            Case fields2.c2_Perc, fields2.c2_Amount
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End Select
    End Sub

    Private Sub GridView2_GotFocus(sender As Object, e As EventArgs) Handles GridView2.GotFocus

    End Sub

    Private Sub GridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView2.KeyDown
        Try
            Dim tmpval_t As Double
            If e.KeyCode = Keys.Enter Then

                colindex_t = GridView2.CurrentCell.ColumnIndex
                Rowindex_t = GridView2.CurrentCell.RowIndex
                Colname_t = GridView2.Columns(colindex_t).Name

                Call Addlessfindfrm(Rowindex_t, colindex_t, GridView2)

                e.SuppressKeyPress = True

                If colindex_t = fields2.c2_Amount Then
                    tmpval_t = GridView2.Item(colindex_t, Rowindex_t).Value
                    If tmpval_t <> 0 Then
                        FindNextCell(GridView2, Rowindex_t, colindex_t + 1)  'checking from Next 
                    End If
                Else
                    FindNextCell(GridView2, Rowindex_t, colindex_t + 1)  'checking from Next 
                End If
            ElseIf e.KeyCode = Keys.Back Then
                GridView2.BeginEdit(True)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Addlessformulacalc(ByVal Tmpdescid_v As Double) As Double
        Try
            Dim ds_addlesformula As New DataSet
            Dim Formula_t As String, Tmpamt_t As Double, Plusorminus_t As String
            Dim Resultformula_t As String()
            Dim Griddescid_t As String, Griddescamt_t As Double
            Sqlstr = "Select Formula From Addlessformula Where Descid = " & Tmpdescid_v & "  And Trnprocess = '" & Process & "'  "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_addlesformula = New DataSet
            ds_addlesformula.Clear()
            da.Fill(ds_addlesformula)

            If ds_addlesformula.Tables(0).Rows.Count > 0 Then
                For y As Integer = 0 To ds_addlesformula.Tables(0).Rows.Count - 1
                    Addlessformulaflg_t = True
                    Formula_t = ds_addlesformula.Tables(0).Rows(y).Item("Formula")
                    Resultformula_t = Formula_t.Split(New String() {","}, StringSplitOptions.None)
                    For Each s As String In Resultformula_t
                        'MessageBox.Show(s)
                        If UCase(s) = "GV" Then
                            Tmpamt_t = Val(txt_totamount.Text)
                        ElseIf s = "+" Or s = "-" Then
                            Plusorminus_t = s
                        Else
                            For j = 0 To GridView2.Rows.Count - 1
                                Griddescid_t = GridView2.Item(fields2.c2_Descid, j).Value
                                If Griddescid_t = s Then
                                    Griddescamt_t = GridView2.Item(fields2.c2_Amount, j).Value
                                    Griddescamt_t = IIf(Griddescamt_t < 0, -1 * Griddescamt_t, Griddescamt_t)
                                    'Tmpamt_t = Tmpamt_t & Plusorminus_t & Griddescamt_t
                                    'Tmpamt_t = String.Concat(Tmpamt_t, Plusorminus_t) & Griddescamt_t
                                    If Plusorminus_t = "+" Then
                                        Tmpamt_t = Tmpamt_t + Griddescamt_t
                                        Return Tmpamt_t
                                        Exit Function
                                    ElseIf Plusorminus_t = "-" Then
                                        Tmpamt_t = Tmpamt_t - Griddescamt_t
                                        Return Tmpamt_t
                                        Exit Function
                                    End If

                                    Exit For
                                End If
                            Next
                        End If
                    Next
                Next
            Else
                Addlessformulaflg_t = False
            End If

            Return Tmpamt_t

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub Addlessfindfrm(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try

            If activecol < 0 Or activerow < 0 Then Exit Sub

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t

                Case fields2.c2_Desc

                    Dim Nextcond_t As String = "", Nextcond_t1 As String = ""
                    For i = 0 To GridView2.Rows.Count - 1
                        AcDescid_t = IIf(IsDBNull(GridView2.Item(fields2.c2_Descid, i).Value), 0, GridView2.Item(fields2.c2_Descid, i).Value)
                        If AcDescid_t <> 0 And i <> activerow Then
                            Nextcond_t = String.Concat(Nextcond_t, AcDescid_t, ",")
                        End If
                    Next

                    If Nextcond_t <> "" Then
                        Nextcond_t = Nextcond_t.Remove(Nextcond_t.Length - 1)
                    Else
                        Nextcond_t = "00"
                    End If

                    ds_acdesc = Nothing
                    cmd = Nothing
                    Sqlstr = "Select Ptyname,Partyid From Account  Where Groupid in('-151','-152','-256')  AND PARTYID NOT IN (" & Nextcond_t & ") Order By Ptyname"
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_acdesc = New DataSet
                    ds_acdesc.Clear()
                    da.Fill(ds_acdesc)

                    VisibleCols.Add("Ptyname")
                    Colheads.Add("Description")

                    fm.Frm_Width = 400
                    fm.Frm_Height = 400
                    fm.Frm_Left = 320
                    fm.Frm_Top = 280

                    fm.MainForm = New frm_invoice
                    fm.Active_ctlname = "Gridview2"
                    Csize.Add(335)

                    tmppassstr = (GridView2.Rows(activerow).Cells(activecol).Value)
                    fm.VarNew = ""
                    fm.VarNewid = 0
                    fm.EXECUTE(conn, ds_acdesc, VisibleCols, Colheads, AcDescid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    GridView2.Rows(activerow).Cells(activecol).Value = fm.VarNew
                    GridView2.Rows(activerow).Cells(activecol + 1).Value = fm.VarNewid
                    AcDescid_t = fm.VarNewid

                    VisibleCols.Remove(1)
                    Colheads.Remove(1)
                    Csize.Remove(1)

            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Gridfindfom(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try
            Dim ds_itm As New DataSet, ds_itemdet As New DataSet
            Dim Tmpitemcode_t As String, tmpuom_t As String, Tmpitemid_t As Double, Tmpitemdes_t As String, activerow_tmp As Integer, Decimal_t As Double
            Dim Costrate_t As Double, Rate_t As Double
            Dim OfferAddQty_t As Double, OfferlessQty_t As Double, Addqty_t As Double, LessQty_t As Double

            If activecol < 0 Or activerow < 0 Then Exit Sub

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t
                Case fields1.c1_itemcode
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

                    If Val_t = 0 Then
                        Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,IM.ITEMDES as ITEMTAMILDES,IM.ITEMTAMILDES as ITEMDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL " _
                            & " , ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.UOM,isnull(um.noofdecimal,0) as noofdecimal ,ISNULL(IM.UOMID,0) AS UOMID " _
                            & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM " _
                        & " LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID  where im.itemid not in (" & Nextcond_t & ") ORDER BY IM.ITEMCODE "
                    Else
                        Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,IM.ITEMDES,IM.ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL, " _
                            & " ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.TAMILUOM AS UOM,isnull(um.noofdecimal,0) as noofdecimal ,ISNULL(IM.UOMID,0) AS UOMID " _
                            & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM " _
                        & " LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID  where im.itemid not in (" & Nextcond_t & ") ORDER BY IM.ITEMCODE "
                    End If


                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_item = New DataSet
                    ds_item.Clear()
                    da.Fill(ds_item)

                    If ds_item.Tables(0).Rows.Count > 0 Then

                        VisibleCols.Add("ITEMCODE")
                        VisibleCols.Add("ITEMTAMILDES")
                        VisibleCols.Add("UOM")

                        Colheads.Add("ItemCode")
                        Colheads.Add("Item Desc")
                        Colheads.Add("Uom")

                        fm.Frm_Width = 550
                        fm.Frm_Height = 400
                        fm.Frm_Left = 127
                        fm.Frm_Top = 245

                        fm.MainForm = New frm_quotation
                        fm.Active_ctlname = "Gridview1"

                        Csize.Add(105)
                        Csize.Add(300)
                        Csize.Add(100)

                        If ds_item.Tables(0).Rows.Count = 1 Then
                            GridView1.Rows(activerow).Cells(activecol).Value = ds_item.Tables(0).Rows(0).Item("ITEMCODE").ToString
                            GridView1.Rows(activerow).Cells(activecol + 1).Value = ds_item.Tables(0).Rows(0).Item("ITEMID")
                            Tmpitemcode_t = ds_item.Tables(0).Rows(0).Item("ITEMCODE").ToString
                            Itemid_t = ds_item.Tables(0).Rows(0).Item("ITEMID")
                        Else
                            tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))

                            If Val_t = 0 Then
                                Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,IM.ITEMDES as ITEMTAMILDES,IM.ITEMTAMILDES as ITEMDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL " _
                                    & " ,ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.UOM,IM.UOMID,ISNULL(IM.TAXPERC,0) AS TAXPERC,isnull(um.NOOFDECIMAL,0) as NOOFDECIMAL " _
                                    & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM " _
                      & " LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID  WHERE IM.ITEMID NOT IN (" & Nextcond_t & ")  and im.itemcode ='" & tmppassstr & "' ORDER BY IM.ITEMCODE "
                            Else
                                Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,IM.ITEMDES,IM.ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL ,ISNULL(IM.COSTPRICE,0) AS COSTRATE " _
                                    & " ,UM.TAMILUOM AS UOM, IM.UOMID,ISNULL(IM.TAXPERC,0) AS TAXPERC,isnull(um.NOOFDECIMAL,0) as NOOFDECIMAL " _
                                    & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM " _
                      & " LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID  WHERE IM.ITEMID NOT IN (" & Nextcond_t & ")  and im.itemcode ='" & tmppassstr & "' ORDER BY IM.ITEMCODE "
                            End If

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

                            'If tmprow_t = activerow_tmp Then
                            '    GridView1.Rows.Add(ds_item.Tables(0).DefaultView.Count)
                            'Else

                            '    GridView1.Rows.RemoveAt(activerow)
                            '    GridView1.Rows.Insert(activerow, ds_item.Tables(0).DefaultView.Count)
                            'End If

                            For i = 0 To ds_item.Tables(0).DefaultView.Count - 1
                                Tmpitemcode_t = ds_item.Tables(0).Rows(i + index_t).Item("itemcode").ToString
                                Tmpitemdes_t = ds_item.Tables(0).Rows(i + index_t).Item("ITEMTAMILDES").ToString
                                Tmpitemid_t = ds_item.Tables(0).Rows(i + index_t).Item("itemid").ToString
                                Costrate_t = ds_item.Tables(0).Rows(i + index_t).Item("COSTRATE")
                                Rate_t = ds_item.Tables(0).Rows(i + index_t).Item("SELPRICERETAIL")
                                tmpuom_t = ds_item.Tables(0).Rows(i + index_t).Item("uom").ToString
                                Decimal_t = ds_item.Tables(0).Rows(i + index_t).Item("noofdecimal").ToString
                                OfferAddQty_t = ds_item.Tables(0).Rows(i + index_t).Item("offeraddqty")
                                OfferlessQty_t = ds_item.Tables(0).Rows(i + index_t).Item("offerlessqty")
                                Addqty_t = ds_item.Tables(0).Rows(i + index_t).Item("addqty")
                                LessQty_t = ds_item.Tables(0).Rows(i + index_t).Item("lessqty")

                                If Tmpitemid_t <> 0 Then
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_offeraddqty).Value = OfferAddQty_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_offerlessqty).Value = OfferlessQty_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_addrs).Value = Addqty_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_lessrs).Value = LessQty_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_selrate).Value = Rate_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_itemcode).Value = Tmpitemcode_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_itemdesc).Value = Tmpitemdes_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_rate).Value = Rate_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_itemid).Value = Tmpitemid_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_uom).Value = tmpuom_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_decimal).Value = Decimal_t
                                    GridView1.Rows(activerow + i).Cells(fields1.c1_costrate).Value = Costrate_t
                                    GridView1.Rows(activerow + i).DefaultCellStyle.Font = font1
                                End If
                            Next
                        End If

                        VisibleCols.Remove(1)
                        Colheads.Remove(1)
                        Csize.Remove(1)

                        VisibleCols.Remove(1)
                        Colheads.Remove(1)
                        Csize.Remove(1)

                        VisibleCols.Remove(1)
                        Colheads.Remove(1)
                        Csize.Remove(1)
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
            Dim Adtype_t As String, Adamt_t As Double, Adperc_t As Double, Addescid_t As Double, CostRate_t As Double

            If editflag = False Then
                txt_vchnum.Text = AutoNum(Process, True) 'if editflag trie begin tran start in autonum fun.
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If
            SavechhkFlg = False
            Headerid_t = GensaveQuotationHead(IIf(editflag, 1, 0), Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Cbo_itemtype.SelectedItem, _
                                              txt_party.Text, txt_reference.Text, Txt_custno.Text, Locationid_t, Gencompid, txt_narration.Text, _
                                              Val(txt_netamt.Text), Partyid_t, Cardid_t, Trntype_t, Genuid, Nothing)

            For i = 0 To GridView1.Rows.Count - 1

                Itemid_t = IIf(IsDBNull(GridView1.Item(fields1.c1_itemid, i).Value), 0, GridView1.Item(fields1.c1_itemid, i).Value)
                Qty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_qty, i).Value), 0, GridView1.Item(fields1.c1_qty, i).Value)
                CostRate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_costrate, i).Value), 0, GridView1.Item(fields1.c1_costrate, i).Value)
                Rate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rate, i).Value), 0, GridView1.Item(fields1.c1_rate, i).Value)
                amount_t = IIf(IsDBNull(GridView1.Item(fields1.c1_amount, i).Value), 0, GridView1.Item(fields1.c1_amount, i).Value)
                remarks_v = IIf(IsDBNull(GridView1.Item(fields1.c1_remarks, i).Value), "", GridView1.Item(fields1.c1_remarks, i).Value)
                If Itemid_t <> 0 And Qty_t <> 0 And amount_t <> 0 Then
                    If remarks_v = "" Or remarks_v Is Nothing Then remarks_v = "  "
                    GensaveQuotationdetl(Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, i + 1, Rate_t, amount_t, Gencompid, Itemid_t, Qty_t, remarks_v, CostRate_t, Nothing)
                    SavechhkFlg = True
                End If
            Next

            For j = 0 To GridView2.Rows.Count - 1
                Adtype_t = GridView2.Item(fields2.c2_Type, j).Value
                Addescid_t = GridView2.Item(fields2.c2_Descid, j).Value
                Adperc_t = GridView2.Item(fields2.c2_Perc, j).Value
                Adamt_t = GridView2.Item(fields2.c2_Amount, j).Value

                If Adtype_t <> "" And Addescid_t <> 0 And Adamt_t <> 0 Then
                    GensaveQUOTATIONaddlessdetl(Headerid_t, j + 1, Addescid_t, Adperc_t, Adamt_t, Adtype_t, Gencompid)
                    SavechhkFlg = True
                End If
            Next

            'If Acflag_t = True Then
            '    If Headerid_t <> 0 Then
            '        If Acpostingwithlrno_t = True Then
            '            If Trim(txt_Lrno.Text) <> "" Then
            '                cmd1 = Nothing
            '                cmd1 = New SqlCommand("QUOTATIONAC_SALE_UPD", conn)
            '                cmd1.CommandType = CommandType.StoredProcedure
            '                cmd1.Transaction = trans
            '                cmd1.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            '                cmd1.Parameters.Add("@Compid", SqlDbType.Float).Value = Billcompid_t
            '                cmd1.ExecuteNonQuery()
            '            End If
            '        Else
            '            cmd1 = Nothing
            '            cmd1 = New SqlCommand("QUOTATIONAC_SALE_UPD", conn)
            '            cmd1.CommandType = CommandType.StoredProcedure
            '            cmd1.Transaction = trans
            '            cmd1.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            '            cmd1.Parameters.Add("@Compid", SqlDbType.Float).Value = Billcompid_t
            '            cmd1.ExecuteNonQuery()
            '        End If
            '    Else

            '    End If
            'End If

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
                        Decimal_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(fields1.c1_decimal).Value))
                        If Text.Length >= Text.IndexOf(".") + Decimal_t + 2 Then
                            e.Handled = True
                        End If
                    ElseIf GridView1.CurrentCell.ColumnIndex = fields1.c1_amount Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                    End If
                End If
            End If

            If GridView1.CurrentCell.ColumnIndex = fields1.c1_itemcode Or GridView1.CurrentCell.ColumnIndex = fields1.c1_remarks Then
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
                Sqlstr = "SELECT isnull(cm.CATEGORY,'') as CATEGORY ,isnull(gm.GROUPNAME,'') as GROUPNAME ,isnull(im.MRPRATE,0) as MRPRATE  " _
                    & "  ,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,isnull(im.ITEMTYPE,'') as ITEMTYPE, " _
                    & "    isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTER  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                    & "  left join CATEGORY_MASTER cm on cm.MASTERID =im.CATEGORYID   where im.itemid =" & Itemid_t & ""

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
                    txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                    'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                    txt_selrate.Text = ds_itemdet.Tables(0).Rows(0).Item("SELPRICERETAIL").ToString
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
                GridView1.Columns(fields1.c1_qty).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_amount).ValueType = GetType(Decimal)
                GridView1.Columns(fields1.c1_rate).DefaultCellStyle.Format = "#.00"
                GridView1.Columns(fields1.c1_qty).DefaultCellStyle.Format = "#"
                GridView1.Columns(fields1.c1_amount).DefaultCellStyle.Format = "#.00"
            End If

            Dim Decimal_t As Double = 0
            Dim Format_t As String = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value))

                Decimal_tt = ""
                For k = 1 To Decimal_t
                    Decimal_tt = String.Concat(Decimal_tt, "0")
                Next

                If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                GridView1.Rows(e.RowIndex).Cells(fields1.c1_qty).Style.Format = Format_t
            End If

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
                    If IsValidRow(GridView1, "C_Itemcode") Then
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

                Dim Decimal_t As Double = 0
                Dim Format_t As String = ""
                Dim Decimal_tt As String = ""
                Dim k As Integer = 0

                If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                    Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(fields1.c1_decimal).Value))
                    Decimal_tt = ""
                    For k = 1 To Decimal_t
                        Decimal_tt = String.Concat(Decimal_tt, "0")
                    Next
                    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                    GridView1.Rows(e.RowIndex).Cells(fields1.c1_qty).Style.Format = Format_t
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        Try
            Dim tmpqty_t As Double, tmprate_t As Double, tmpamt_t As Double
            Dim tmpofferaddqty_t As Double, tmpOfferlessqty_t As Double, tmpaddrs_t As Double, tmplessrs_t As Double

            If (e.ColumnIndex = fields1.c1_qty Or e.ColumnIndex = fields1.c1_itemcode Or e.ColumnIndex = fields1.c1_selrate) And e.RowIndex >= 0 Then
                tmpqty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_qty, e.RowIndex).Value)
                tmpofferaddqty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_offeraddqty, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_offeraddqty, e.RowIndex).Value)
                tmpOfferlessqty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_offerlessqty, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_offerlessqty, e.RowIndex).Value)
                tmpaddrs_t = IIf(IsDBNull(GridView1.Item(fields1.c1_addrs, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_addrs, e.RowIndex).Value)
                tmplessrs_t = IIf(IsDBNull(GridView1.Item(fields1.c1_lessrs, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_lessrs, e.RowIndex).Value)
                tmprate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_selrate, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_selrate, e.RowIndex).Value)

                GridView1.Rows(e.RowIndex).Cells(fields1.c1_rate).Value = tmprate_t

                If tmpqty_t <= tmpofferaddqty_t Then
                    GridView1.Rows(e.RowIndex).Cells(fields1.c1_rate).Value = tmprate_t + tmpaddrs_t
                End If

                If tmpqty_t >= tmpOfferlessqty_t Then
                    GridView1.Rows(e.RowIndex).Cells(fields1.c1_rate).Value = tmprate_t - tmplessrs_t
                End If
            End If

            If (e.ColumnIndex = fields1.c1_qty Or e.ColumnIndex = fields1.c1_rate) And e.RowIndex >= 0 Then
                tmpqty_t = IIf(IsDBNull(GridView1.Item(fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_qty, e.RowIndex).Value)
                tmprate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rate, e.RowIndex).Value), 0, GridView1.Item(fields1.c1_rate, e.RowIndex).Value)

                tmpamt_t = tmpqty_t * tmprate_t

                GridView1.Item(fields1.c1_amount, e.RowIndex).Value = tmpamt_t

                Call calcnetamt()
                Call Addless_Calc()
                Call calcnetamt()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Dim tb As TextBox = CType(e.Control, TextBox)
        Select Case GridView1.CurrentCell.ColumnIndex
            Case fields1.c1_qty, fields1.c1_rate, fields1.c1_amount, fields1.c1_itemcode
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
                Sqlstr = "SELECT isnull(cm.CATEGORY,'') as CATEGORY ,isnull(gm.GROUPNAME,'') as GROUPNAME ,isnull(im.MRPRATE,0) as MRPRATE  " _
                    & "  ,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,isnull(im.ITEMTYPE,'') as ITEMTYPE, " _
                    & "    isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTER  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                    & "  left join CATEGORY_MASTER cm on cm.MASTERID =im.CATEGORYID   where im.itemid =" & Itemid_t & ""

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
                    txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                    'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                    txt_selrate.Text = ds_itemdet.Tables(0).Rows(0).Item("SELPRICERETAIL").ToString
                    txt_costrate.Text = ds_itemdet.Tables(0).Rows(0).Item("COSTPRICE").ToString
                End If
            End If

            If e.KeyCode = Keys.Enter Then

                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)

                e.SuppressKeyPress = True

                If colindex_t = fields1.c1_qty Then
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

                Call GendelQuotation(Headerid_v)

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
                GridView2.Enabled = True
                GridView2.ReadOnly = True
                'cmd_ok.Visible = False
                'cmd_cancel.Text = "&Close"
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
            Call gridreadonly(GridView1, Not visflag_t, "C_Itemcode", "C_Itemdes", "c_Rate", "C_Amount")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Locationfindfrm()
        Try

            Dim cnt As Integer

            cnt = ds_location.Tables(0).Rows.Count

            If cnt > 0 Then
                VisibleCols.Add("godownname")
                Colheads.Add("Location")

                fm.Frm_Width = 400
                fm.Frm_Height = 300
                fm.Frm_Left = 350
                fm.Frm_Top = 150
                fm.MainForm = New frm_quotation
                fm.Active_ctlname = "txt_location"
                Csize.Add(275)

                If cnt = 1 Then
                    txt_location.Text = ds_location.Tables(0).Rows(0).Item("godownname").ToString
                    Locationid_t = ds_location.Tables(0).Rows(0).Item("masterid")
                Else
                    tmppassstr = txt_location.Text
                    fm.EXECUTE(conn, ds_location, VisibleCols, Colheads, Locationid_t, "", False, Csize, "", False, False, "", tmppassstr)
                    txt_location.Text = fm.VarNew
                    Locationid_t = fm.VarNewid
                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Partyfindfrm()
        Try
            Dim cnt As Integer
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
            Else
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
            End If


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

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Cardfindfrm()
        Try
            Dim cnt As Integer
            Dim dscnt As Integer
            Dim sqlstr As String
            Dim Val_t As Integer
            Dim ds_card As New DataSet
            Dim da_card As SqlDataAdapter

            ds_card.Clear()
            ds_card = Nothing
            sqlstr = "Select mobileno,cardid,cardno,name From cardno_master Order By cardno"
            cmd = New SqlCommand(sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_card = New SqlDataAdapter(cmd)
            ds_card = New DataSet
            ds_card.Clear()
            da_card.Fill(ds_card)
            cnt = ds_card.Tables(0).Rows.Count

            If cnt > 0 Then
                VisibleCols.Add("mobileno")
                VisibleCols.Add("cardno")
                VisibleCols.Add("name")

                Colheads.Add("Mobile No")
                Colheads.Add("Card No")
                Colheads.Add("Name")

                fm.Frm_Width = 500
                fm.Frm_Height = 300
                fm.Frm_Left = 770
                fm.Frm_Top = 172

                fm.MainForm = New frm_quotation
                fm.Active_ctlname = "Txt_custno"

                Csize.Add(100)
                Csize.Add(200)
                Csize.Add(150)

                If cnt = 1 Then
                    Txt_custno.Text = ds_card.Tables(0).Rows(0).Item("cardno").ToString
                    txt_name.Text = ds_card.Tables(0).Rows(0).Item("name").ToString
                    Cardid_t = ds_card.Tables(0).Rows(0).Item("cardid")
                Else

                    tmppassstr = Txt_custno.Text
                    fm.EXECUTE(conn, ds_card, VisibleCols, Colheads, Cardid_t, "", False, Csize, "", False, False, "", tmppassstr, sqlstr)
                    Txt_custno.Text = fm.VarNew
                    Cardid_t = fm.VarNewid

                    If ds_card.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
                        'If Formshown_t = True And ds_item.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
                        ds_card.Tables(0).DefaultView.RowFilter = " cardid = " & Cardid_t & " "
                        index_t = ds_card.Tables(0).Rows.IndexOf(ds_card.Tables(0).DefaultView.Item(0).Row)
                        txt_name.Text = ds_card.Tables(0).Rows(index_t).Item("name").ToString
                        Txt_custno.Text = ds_card.Tables(0).Rows(index_t).Item("CARDNO").ToString
                    Else
                        txt_name.Text = ""
                    End If

                End If

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frm_quotation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Controlno_t = "Add" Then
                editflag = False
            ElseIf Controlno_t = "Edit" Then
                editflag = True
            End If

            Cbo_itemtype.SelectedIndex = 0
            Call opnconn()
            Call Execute()

            Me.Show()
            txt_party.Focus()
            Panel3.Visible = False
            'LineShape1.Visible = False
            'LineShape2.Visible = False
            'LineShape3.Visible = False
            'LineShape5.Visible = False

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            Dim converter As System.ComponentModel.TypeConverter = _
    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            font1 = _
     CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
            'font1 = New Font(Font_m, CType(Size_m, Single))

            txt_party.Font = font1
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).DefaultCellStyle.Font = font1
            Next

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
            Sqlstr = "SELECT isnull(cm.CATEGORY,'') as CATEGORY ,isnull(gm.GROUPNAME,'') as GROUPNAME ,isnull(im.MRPRATE,0) as MRPRATE  " _
                & "  ,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,isnull(im.ITEMTYPE,'') as ITEMTYPE, " _
                & "    isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTER  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                & "  left join CATEGORY_MASTER cm on cm.MASTERID =im.CATEGORYID   where im.itemid =" & Itemid_t & ""

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
                txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                txt_selrate.Text = ds_itemdet.Tables(0).Rows(0).Item("SELPRICERETAIL").ToString
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
            Sqlstr = "SELECT isnull(cm.CATEGORY,'') as CATEGORY ,isnull(gm.GROUPNAME,'') as GROUPNAME ,isnull(im.MRPRATE,0) as MRPRATE  " _
                & "  ,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,isnull(im.ITEMTYPE,'') as ITEMTYPE, " _
                & "    isnull(im.COSTPRICE,0) as COSTPRICE  FROM ITEM_MASTER  im join GROUP_MASTER  gm on gm.MASTERID =im.GROUPID  " _
                & "  left join CATEGORY_MASTER cm on cm.MASTERID =im.CATEGORYID   where im.itemid =" & Itemid_t & ""

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
                txt_mrprate.Text = ds_itemdet.Tables(0).Rows(0).Item("MRPRATE").ToString
                'txt_category.Text = ds_itemdet.Tables(0).Rows(0).Item("CATEGORY").ToString
                txt_selrate.Text = ds_itemdet.Tables(0).Rows(0).Item("SELPRICERETAIL").ToString
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
                                   font1, _
                                   b, _
                                   e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub Cbo_itemtype_KeyDown(sender As Object, e As KeyEventArgs) Handles Cbo_itemtype.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_location.Focus()
        End If
    End Sub

    Private Sub Cbo_itemtype_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cbo_itemtype.KeyPress
        e.Handled = True
        If LCase(e.KeyChar) = LCase("r") Then
            Cbo_itemtype.SelectedIndex = 0
        ElseIf LCase(e.KeyChar) = LCase("w") Then
            Cbo_itemtype.SelectedIndex = 1
        ElseIf LCase(e.KeyChar) = LCase("b") Then
            Cbo_itemtype.SelectedIndex = 2
        End If
    End Sub

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Vchdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_location.Focus()
        End If
    End Sub

    Private Sub txt_location_GotFocus(sender As Object, e As EventArgs) Handles txt_location.GotFocus
        txt_location.BackColor = Color.Yellow
    End Sub

    Private Sub txt_location_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_location.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Locationfindfrm()
            txt_party.Focus()
        End If
    End Sub

    Private Sub txt_location_LostFocus(sender As Object, e As EventArgs) Handles txt_location.LostFocus
        txt_location.BackColor = Color.White
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
            txt_reference.Focus()
        End If
    End Sub

    Private Sub txt_party_LostFocus(sender As Object, e As EventArgs) Handles txt_party.LostFocus
        txt_party.BackColor = Color.White
    End Sub

    Private Sub txt_reference_GotFocus(sender As Object, e As EventArgs) Handles txt_reference.GotFocus
        txt_reference.BackColor = Color.Yellow
    End Sub

    Private Sub txt_reference_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_reference.KeyDown
        If e.KeyCode = Keys.Enter Then
            Txt_custno.Focus()
        End If
    End Sub

    Private Sub txt_reference_LostFocus(sender As Object, e As EventArgs) Handles txt_reference.LostFocus
        txt_reference.BackColor = Color.White
    End Sub

    Private Sub Txt_custno_Click(sender As Object, e As EventArgs) Handles Txt_custno.Click
        'Call Cardfindfrm()
    End Sub

    Private Sub Txt_custno_GotFocus(sender As Object, e As EventArgs) Handles Txt_custno.GotFocus
        Txt_custno.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_custno_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_custno.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(Txt_custno.Text) <> "" Then Call Cardfindfrm()
            GridView1.Focus()
        End If
    End Sub

    Private Sub Txt_custno_LostFocus(sender As Object, e As EventArgs) Handles Txt_custno.LostFocus
        Txt_custno.BackColor = Color.White
        If Txt_custno.Text = "" Then
            txt_name.Text = ""
            Cardid_t = 0
        End If
    End Sub

    Private Sub cmd_ok_Click_1(sender As Object, e As EventArgs) Handles cmd_ok.Click

    End Sub
End Class