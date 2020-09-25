Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App

Public Class frm_Agencybill_Invoice
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da, da_head, da_detl, da_party, da_line, da_location, da_freeitem, da_stock As SqlDataAdapter
    Dim bs As New BindingSource
    Dim ds, ds_party, ds_location, ds_line, ds1, ds_head, ds_detl, ds_stock, ds_item, ds_acdesc, ds_freeitem As New DataSet
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim Locationid_t, Itemid_t, AcDescid_t, Lineid_t As Double, Formshown_t, Isalreadyexistflag_t As Boolean, SavechhkFlg As Boolean
    Dim Controlno_t As String, Process As String, Trntype_t As String, Sqlstr As String, Colname_t As String, Partyid_t As Double, Stateid_t As Double
    Dim Headerid_t As Double
    Dim Addlessformulaflg_t As Boolean
    Dim index_t As Integer, Rowindex_t As Integer, Headcnt_t As Integer, Detlcnt_t As Integer, colindex_t As Integer, Code_t As String
    Dim fm As New Sun_Findfrm
    Dim VatCalculation As Boolean
    Dim celWasEndEdit As DataGridViewCell
    Dim Formload_t As Boolean
    Dim font1 As Font
    Dim rm As New Frm_Reports_Init
    Dim val_t As Integer

    Enum Fields1
        c1_itemcode = 0
        c1_itemid = 1
        c1_itemdes = 2
        c1_hsnaccountcode = 3
        c1_hsnaccid = 4
        c1_qty = 5
        c1_Freeqty = 6
        c1_stockqty = 7
        c1_uom = 8
        c1_uomid = 9
        c1_decimal = 10
        c1_rate = 11
        c1_costrate = 12
        c1_Discount = 13
        c1_Discamt = 14
        c1_vatper = 15
        c1_exrate = 16
        c1_vatamt = 17
        c1_vatcalc = 18
        c1_amount = 19
        c1_taxablevalue = 20
        c1_cgstperc = 21
        c1_cgstamount = 22
        c1_sgstperc = 23
        c1_sgstamount = 24
        c1_igstperc = 25
        c1_igstamount = 26
        c1_totalamount = 27
        c1_remarks = 28
        c1_Freeitem = 29
        c1_Forqty = 30
        c1_offeraddqty = 31
        c1_offerlessqty = 32
        c1_addrs = 33
        c1_lessrs = 34
        c1_selrate = 35
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
            GridView1.AllowUserToAddRows = False

            Call dsopen()

            Call Headercall(Headerid_t)

            Headcnt_t = ds_head.Tables(0).Rows.Count

            Call gridreadonly(GridView1, True, "c_stock", "C_Itemdes", "c_vatper", "c_uom", "c_vatamt", "c_exrate", "C_Amount", "C_DiscAmt", "c_cgstamount", "c_sgstamount", "c_igstamount", "c_totalamount", "C_taxablevalue", "c_hsnaccountingcode", "c_cgstperc", "c_sgstperc", "c_igstperc")
            Call gridreadonly_Color(GridView1, Readonlycolor_t, "c_stock", "C_Itemdes", "c_vatper", "c_uom", "c_vatamt", "c_exrate", "C_Amount", "C_DiscAmt", "c_cgstamount", "c_sgstamount", "c_igstamount", "c_totalamount", "C_taxablevalue", "c_hsnaccountingcode", "c_cgstperc", "c_sgstperc", "c_igstperc")
            Call gridvisible(GridView1, False, "C_Itemid", "C_uomid", "c_decimal", "c_vatper", "c_vatamt", "c_exrate", "c_vatcalc", "c_offeraddqty", "c_offerlessqty", "c_addrs", "c_lessrs", "c_selrate", "c_hsnaccid")
            Call gridvisible(GridView2, False, "C_Descid")

            GridView1.Columns(Fields1.c1_stockqty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_cgstamount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_cgstperc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_sgstamount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_sgstperc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_igstamount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_igstperc).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_totalamount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_taxablevalue).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            Dim ds_settings As New DataSet
            Dim da_settings As SqlDataAdapter
            Dim dscnt As Integer

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

            'Dim Vatvisible As Integer
            'ds_settings = Nothing
            '' ds_settings.Clear()
            'Sqlstr = "SELECT ISNULL(NUMERICVALUE,0 ) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS  ='INVOICE_VATPERC' "
            'cmd = New SqlCommand(Sqlstr, conn)
            'cmd.CommandType = CommandType.Text
            'da_settings = New SqlDataAdapter(cmd)
            'ds_settings = New DataSet
            'ds_settings.Clear()
            'da_settings.Fill(ds_settings)
            'dscnt = ds_settings.Tables(0).Rows.Count

            'If dscnt > 0 Then
            '    Vatvisible = ds_settings.Tables(0).Rows(0).Item("numericvalue")
            '    If Vatvisible = 0 Then Call gridvisible(GridView1, False, "c_vatamt", "c_vatper", "c_exrate")
            '    If Vatvisible = 1 Then Call gridvisible(GridView1, True, "c_vatamt", "c_vatper", "c_exrate")
            'Else
            '    Vatvisible = 0
            '    If Vatvisible = 0 Then Call gridvisible(GridView1, False, "c_vatamt", "c_vatper", "c_exrate")
            'End If

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

            If Headcnt_t > 0 Then
                Call storechars(Headcnt_t)
            Else
                Call clearchars()
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

            If txt_location.Text Is Nothing Or txt_location.Text = "" Or Locationid_t = 0 Then
                ' If Locationid_t = 0 Then
                Sqlstr = "SELECT GM.GODOWNNAME,gm.masterid FROM GODOWN_Master GM  WHERE GM.MASTERID =" & Locationid_t & " ORDER BY GM.GODOWNNAME  "
                ' End If
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da_location = New SqlDataAdapter(cmd)
                ds_location = New DataSet
                ds_location.Clear()
                da_location.Fill(ds_location)

                If ds_location.Tables(0).Rows.Count > 0 Then
                    txt_location.Text = ds_location.Tables(0).Rows(0).Item("godownname").ToString
                    Locationid_t = ds_location.Tables(0).Rows(0).Item("masterid")
                End If
            End If

            val_t = 0
            ds_settings = Nothing
            ' ds_settings.Clear()
            Sqlstr = "SELECT ISNULL(NUMERICVALUE,0 ) AS NUMERICVALUE FROM SETTINGS WHERE PROCESS  ='INVOICERATE_LOCK' "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_settings = New SqlDataAdapter(cmd)
            ds_settings = New DataSet
            ds_settings.Clear()
            da_settings.Fill(ds_settings)
            dscnt = ds_settings.Tables(0).Rows.Count

            If dscnt > 0 Then
                val_t = ds_settings.Tables(0).Rows(0).Item("numericvalue")
            Else
                val_t = 0
            End If

            If val_t = 1 Then
                GridView1.Columns(Fields1.c1_rate).ReadOnly = True
                GridView1.Columns(Fields1.c1_rate).DefaultCellStyle.BackColor = Readonlycolor_t
            Else
                GridView1.Columns(Fields1.c1_rate).ReadOnly = False
            End If

            'txt_vchnum.Enabled = False
            'txt_vchnum.BackColor = Color.White
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try
            Dim ds_defaultloc As New DataSet
            Dim da_defaultloc As SqlDataAdapter

            Sqlstr = "Select Ptyname,Partyid From Account  Where Groupid in('-151','-152','-256')  Order By Ptyname"
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_acdesc = New DataSet
            ds_acdesc.Clear()
            da.Fill(ds_acdesc)

            
            ds_defaultloc.Clear()
            Sqlstr = "SELECT ISNULL(STRINGVALUE,0) AS STRINGVALUE from SETTINGS WHERE PROCESS ='DEFAULT LOCATION' AND NUMERICVALUE =1 "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_defaultloc = New SqlDataAdapter(cmd)
            ds_defaultloc = New DataSet
            ds_defaultloc.Clear()
            da_defaultloc.Fill(ds_defaultloc)

            If ds_defaultloc.Tables(0).Rows.Count > 0 Then
                Locationid_t = ds_defaultloc.Tables(0).Rows(0).Item("STRINGVALUE")
            Else
                Locationid_t = 0
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            Txt_Totamt.Text = "0.00"
            txt_totqty.Text = "0"

            txt_vchnum.Text = AutoNum(Process)
            txt_vchnum.Enabled = False
            DTP_Vchdate.Enabled = False
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
            cmd = New SqlCommand("SELECT H.HEADERID,H.VCHNUM,p.stateid,(select max(state) from state_master where masterid = p.stateid ) as state,H.VCHDATE,H.PARTYID,ISNULL(DBO.GETLEDGERBALANCE('" & DTP_Vchdate.Value.ToString("yyyy/MM/dd") & "'," & Gencompid & ",P.PTYCODE,0),0) AS OUTSTANDING" _
                                 & "  , ISNULL(DBO.GETLEDGERBALANCE('" & DTP_Vchdate.Value.ToString("yyyy/MM/dd") & "'," & Gencompid & ",P.PTYCODE,1),0) AS CRDR ,ISNULL(LM.LINE,'') AS LINE,ISNULL(GM.GODOWNNAME,'') AS LOCATION,ISNULL(H.LOCATIONID,0) AS LOCATIONID " _
                                 & ", ISNULL(H.LINEID,0) AS LINEID,ISNULL(H.REFERENCE,'') AS REFERENCE,ISNULL(P.CREDITLIMIT,0) AS CREDITLIMIT ,ISNULL(H.VEHICLENO,'') AS VEHICLENO,   P.Ptyname as party,H.NARRATION,H.TOTAMOUNT,ISNULL(P.ADD1,'') AS ADD1,ISNULL(P.ADD2,'') AS ADD2,ISNULL(P.ADD3,'') AS ADD3 ,ISNULL(P.ADD4,'') AS ADD4 ,ISNULL(P.TIN,'') AS TIN  " _
                                 & " FROM INVOICE_HEADER H JOIN PARTY P ON P.PTYCODE = H.PARTYID  LEFT JOIN LINE_MASTER LM ON LM.MASTERID =P.LINEID " _
                                 & "  LEFT JOIN GODOWN_MASTER GM ON GM.MASTERID  =h.LOCATIONID  WHERE H.HEADERID = " & Headerid_v & " ", conn)
            cmd.CommandType = CommandType.Text
            da_head = New SqlDataAdapter(cmd)
            ds_head = New DataSet
            ds_head.Clear()
            da_head.Fill(ds_head)

            dt = New DataTable 'used for find particular rows
            da_head.Fill(dt)

            Headcnt_t = ds_head.Tables(0).Rows.Count - 1

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FindNextCell(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
        Try
            Dim found As Boolean = False

            While dgv.RowCount > rowindex

                While dgv.Columns.Count > columnindex

                    If (Not (dgv.Rows(rowindex).Cells(columnindex)).ReadOnly) Then
                        If (dgv.Rows(rowindex).Cells(columnindex)).Visible Then
                            If (GridView1.CurrentCell.ColumnIndex = Fields1.c1_qty And GridView1.Columns(Fields1.c1_rate).ReadOnly = True) Then
                                If GridView1.Rows.Count - 1 < rowindex + 1 Then GridView1.Rows.Add(1)
                                If GridView1.Rows(rowindex + 1).Cells(0).ReadOnly = True Then
                                    dgv.CurrentCell = dgv.Rows(rowindex + 2).Cells(0)
                                Else
                                    dgv.CurrentCell = dgv.Rows(rowindex + 1).Cells(0)
                                End If
                            Else
                                dgv.CurrentCell = dgv.Rows(rowindex).Cells(columnindex)
                                ' columnindex = Fields1.c1_itemcode
                                ' Exit While
                            End If

                            ' dgv.BeginEdit(True)
                            Exit Sub
                        Else
                            columnindex += 1
                        End If
                    Else
                        'If (GridView1.CurrentCell.ColumnIndex = Fields1.c1_qty And GridView1.Columns(Fields1.c1_rate).ReadOnly = True) Then
                        '    If rowindex + 1 < GridView1.Rows.Count - 1 Then
                        '        GridView1.Rows.Add(1)
                        '    End If
                        '    rowindex += 1

                        'Else
                        columnindex += 1
                    End If
                    'End If
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
            Dim ds_gst As New DataSet
            Dim rowid_t As Integer, Decimal_t As Double
            Dim Format_t As String = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            Call clearchars()

            rowid_t = ds_head.Tables(0).Rows.Count
            If rowid_t <= 0 Then Exit Sub

            txt_vchnum.Enabled = False

            rowid_t = rowid_t - 1
            Headerid_t = ds_head.Tables(0).Rows(rowid_t).Item("Headerid")
            txt_vchnum.Text = ds_head.Tables(0).Rows(rowid_t).Item("Vchnum").ToString
            DTP_Vchdate.Value = ds_head.Tables(0).Rows(rowid_t).Item("Vchdate").ToString
            txt_narration.Text = ds_head.Tables(0).Rows(rowid_t).Item("Narration").ToString
            txt_customer.Text = ds_head.Tables(0).Rows(rowid_t).Item("party").ToString
            Partyid_t = ds_head.Tables(0).Rows(rowid_t).Item("partyid")
            txt_state.Text = ds_head.Tables(0).Rows(rowid_t).Item("state").ToString
            Stateid_t = ds_head.Tables(0).Rows(rowid_t).Item("stateid")
            txt_location.Text = ds_head.Tables(0).Rows(rowid_t).Item("LOCATION").ToString
            Txt_line.Text = ds_head.Tables(0).Rows(rowid_t).Item("LINE").ToString
            Txt_reference.Text = ds_head.Tables(0).Rows(rowid_t).Item("REFERENCE").ToString
            Txt_vehicle.Text = ds_head.Tables(0).Rows(rowid_t).Item("VEHICLENO").ToString
            Txt_creditlimit.Text = ds_head.Tables(0).Rows(rowid_t).Item("CREDITLIMIT").ToString
            Txt_creditlimit.Text = Format(Val(Txt_creditlimit.Text), "#0.00")
            Txt_creditlimit.TextAlign = HorizontalAlignment.Right

            Label17.Text = ""

            If ds_head.Tables(0).Rows(0).Item("CRDR") = 2 Then
                Label17.Text = "CR"
            ElseIf ds_head.Tables(0).Rows(0).Item("CRDR") = 1 Then
                Label17.Text = "DR"
            End If
            Txt_outstanding.Text = ds_head.Tables(0).Rows(0).Item("OUTSTANDING").ToString
            Txt_outstanding.TextAlign = HorizontalAlignment.Right
            Txt_outstanding.Text = Format(Val(Txt_outstanding.Text), "#0.00")

            Lineid_t = ds_head.Tables(0).Rows(rowid_t).Item("LINEID")
            Locationid_t = ds_head.Tables(0).Rows(rowid_t).Item("LOCATIONID")

            ds_detl.Clear()
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_INVOICEADDLESS"
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

                    If ds_detl.Tables(0).Rows(j).Item("Perc") = 14.5 Or ds_detl.Tables(0).Rows(j).Item("Perc") = 5 Or _
                        ds_detl.Tables(0).Rows(j).Item("Perc") = 0 Then
                        'If j = 0 Or j = 1 Then GridView2.Rows(j).ReadOnly = True
                        'If j = 0 Or j = 1 Then GridView2.Rows(j).DefaultCellStyle.BackColor = Readonlycolor_t
                        GridView2.Rows(j).ReadOnly = True
                        GridView2.Rows(j).DefaultCellStyle.BackColor = Readonlycolor_t
                    End If

                    GridView2.Rows(j).Cells(fields2.c2_Amount).Value = ds_detl.Tables(0).Rows(j).Item("Amount")
                Next

            End If

            ds_detl.Clear()
            ds_detl = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_INVOICEDETAIL"
            cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            da_detl = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            da_detl.Fill(ds_detl)

            GridView1.Columns(Fields1.c1_qty).DefaultCellStyle.Format = "#"
            GridView1.Columns(Fields1.c1_rate).DefaultCellStyle.Format = "#.00"
            GridView1.Columns(Fields1.c1_amount).DefaultCellStyle.Format = "#.00"

            Detlcnt_t = ds_detl.Tables(0).Rows.Count

            If Detlcnt_t > 0 Then
                GridView1.Rows.Add(Detlcnt_t + 1)
                For i = 0 To Detlcnt_t - 1
                    GridView1.Rows(i).Cells(Fields1.c1_itemcode).Value = ds_detl.Tables(0).Rows(i).Item("Itemcode")
                    GridView1.Rows(i).Cells(Fields1.c1_itemid).Value = ds_detl.Tables(0).Rows(i).Item("Itemid")
                    GridView1.Rows(i).Cells(Fields1.c1_qty).Value = ds_detl.Tables(0).Rows(i).Item("Qty")
                    GridView1.Rows(i).Cells(Fields1.c1_rate).Value = ds_detl.Tables(0).Rows(i).Item("Rate")
                    GridView1.Rows(i).Cells(Fields1.c1_amount).Value = ds_detl.Tables(0).Rows(i).Item("Amount")
                    If val_t = 0 Then GridView1.Rows(i).Cells(Fields1.c1_itemdes).Value = ds_detl.Tables(0).Rows(i).Item("itemdes")
                    If val_t = 1 Then GridView1.Rows(i).Cells(Fields1.c1_itemdes).Value = ds_detl.Tables(0).Rows(i).Item("ITEMTAMILDES")
                    If val_t = 0 Then GridView1.Rows(i).Cells(Fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("uom")
                    If val_t = 1 Then GridView1.Rows(i).Cells(Fields1.c1_uom).Value = ds_detl.Tables(0).Rows(i).Item("TAMILuom")
                    GridView1.Rows(i).Cells(Fields1.c1_uomid).Value = ds_detl.Tables(0).Rows(i).Item("uomid")
                    GridView1.Rows(i).Cells(Fields1.c1_offeraddqty).Value = ds_detl.Tables(0).Rows(i).Item("OFFERADDQTY")
                    GridView1.Rows(i).Cells(Fields1.c1_offerlessqty).Value = ds_detl.Tables(0).Rows(i).Item("OFFERLESSQTY")
                    GridView1.Rows(i).Cells(Fields1.c1_addrs).Value = ds_detl.Tables(0).Rows(i).Item("ADDRS")
                    GridView1.Rows(i).Cells(Fields1.c1_lessrs).Value = ds_detl.Tables(0).Rows(i).Item("LESSRS")
                    GridView1.Rows(i).Cells(Fields1.c1_rate).Value = ds_detl.Tables(0).Rows(i).Item("RATE")
                    GridView1.Rows(i).Cells(Fields1.c1_selrate).Value = ds_detl.Tables(0).Rows(i).Item("SELRATE")
                    'GridView1.Rows(i).Cells(Fields1.c1_vatper).Value = ds_detl.Tables(0).Rows(i).Item("TAXPER")
                    GridView1.Rows(i).Cells(Fields1.c1_remarks).Value = ds_detl.Tables(0).Rows(i).Item("REMARKS")
                    GridView1.Rows(i).Cells(Fields1.c1_exrate).Value = ds_detl.Tables(0).Rows(i).Item("EXRATE")
                    GridView1.Rows(i).Cells(Fields1.c1_vatper).Value = ds_detl.Tables(0).Rows(i).Item("VATPERC")
                    GridView1.Rows(i).Cells(Fields1.c1_vatamt).Value = ds_detl.Tables(0).Rows(i).Item("VATAMOUNT")
                    GridView1.Rows(i).Cells(Fields1.c1_decimal).Value = ds_detl.Tables(0).Rows(i).Item("NOOFDECIMAL")
                    GridView1.Rows(i).Cells(Fields1.c1_Discount).Value = ds_detl.Tables(0).Rows(i).Item("Discperc")
                    GridView1.Rows(i).Cells(Fields1.c1_Discamt).Value = ds_detl.Tables(0).Rows(i).Item("Discamount")
                    GridView1.Rows(i).Cells(Fields1.c1_Freeqty).Value = ds_detl.Tables(0).Rows(i).Item("Freeqty")
                    GridView1.Rows(i).Cells(Fields1.c1_Freeitem).Value = ds_detl.Tables(0).Rows(i).Item("Freeitem")
                    GridView1.Rows(i).Cells(Fields1.c1_Forqty).Value = ds_detl.Tables(0).Rows(i).Item("Forqty")
                    GridView1.Rows(i).Cells(Fields1.c1_Freeqty).Value = ds_detl.Tables(0).Rows(i).Item("Freeqty")
                    GridView1.Rows(i).Cells(Fields1.c1_hsnaccountcode).Value = ds_detl.Tables(0).Rows(i).Item("hsnaccountingcode").ToString
                    GridView1.Rows(i).Cells(Fields1.c1_cgstperc).Value = ds_detl.Tables(0).Rows(i).Item("cgstperc")
                    GridView1.Rows(i).Cells(Fields1.c1_cgstamount).Value = ds_detl.Tables(0).Rows(i).Item("cgstamount")
                    GridView1.Rows(i).Cells(Fields1.c1_sgstperc).Value = ds_detl.Tables(0).Rows(i).Item("sgstperc")
                    GridView1.Rows(i).Cells(Fields1.c1_sgstamount).Value = ds_detl.Tables(0).Rows(i).Item("sgstamount")
                    GridView1.Rows(i).Cells(Fields1.c1_igstperc).Value = ds_detl.Tables(0).Rows(i).Item("igstperc")
                    GridView1.Rows(i).Cells(Fields1.c1_igstamount).Value = ds_detl.Tables(0).Rows(i).Item("igstamount")

                    ds_stock.Clear()
                    cmd = Nothing
                    cmd = New SqlCommand
                    cmd.Connection = conn
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "ITEMSTOCK_RPT_BILL"
                    cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid.ToString
                    cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = ds_detl.Tables(0).Rows(i).Item("Itemid").ToString
                    cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationid_t.ToString
                    cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = "select groupid from item_master where itemid =" & ds_detl.Tables(0).Rows(i).Item("Itemid") & ""
                    cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DTP_Vchdate.Value.ToString("yyyy/MM/dd")
                    da_stock = New SqlDataAdapter(cmd)
                    ds_stock = New DataSet
                    da_stock.Fill(ds_stock)

                    Detlcnt_t = ds_stock.Tables(0).Rows.Count
                    If Detlcnt_t > 0 Then
                        If ds_stock.Tables(0).Rows(0).Item("ITEMID") <> 0 Then GridView1.Rows(i).Cells(Fields1.c1_stockqty).Value = ds_stock.Tables(0).Rows(0).Item("BALANCE")
                    End If

                    GridView1.Rows(i).DefaultCellStyle.Font = font1

                    Decimal_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_decimal).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_decimal).Value))

                    Decimal_tt = ""
                    For k = 1 To Decimal_t
                        Decimal_tt = String.Concat(Decimal_tt, "0")
                    Next

                    If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                    GridView1.Rows(i).Cells(Fields1.c1_qty).Style.Format = Format_t
                    GridView1.Rows(i).Cells(Fields1.c1_Freeqty).Style.Format = Format_t
                    GridView1.Rows(i).Cells(Fields1.c1_stockqty).Style.Format = Format_t



                    Itemid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_itemid).Value))

                    Sqlstr = "SELECT ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.IGSTPERC,0) AS IGST,ISNULL(HM.SGSTPERC,0) AS SGST FROM ITEM_MASTER IM LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE =IM.HSNACCOUNTINGCODE WHERE IM.ITEMID = " & Itemid_t & " "
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_gst = New DataSet
                    ds_gst.Clear()
                    da.Fill(ds_gst)

                    If ds_gst.Tables(0).Rows.Count > 0 Then
                        If LCase(txt_state.Text) = LCase("tamilnadu") Then
                            GridView1.Rows(i).Cells(Fields1.c1_cgstperc).Value = ds_gst.Tables(0).Rows(0).Item("cgst")
                            GridView1.Rows(i).Cells(Fields1.c1_sgstperc).Value = ds_gst.Tables(0).Rows(0).Item("sgst")
                            GridView1.Rows(i).Cells(Fields1.c1_igstperc).Value = 0
                        Else
                            GridView1.Rows(i).Cells(Fields1.c1_igstperc).Value = ds_gst.Tables(0).Rows(0).Item("igst")
                            GridView1.Rows(i).Cells(Fields1.c1_sgstperc).Value = 0
                            GridView1.Rows(i).Cells(Fields1.c1_cgstperc).Value = 0
                        End If
                    End If

                Next
            End If

            If LCase(txt_state.Text) = LCase("tamilnadu") Then
                GridView1.Columns(Fields1.c1_igstperc).Visible = False
                GridView1.Columns(Fields1.c1_igstamount).Visible = False
                GridView1.Columns(Fields1.c1_cgstperc).Visible = True
                GridView1.Columns(Fields1.c1_sgstperc).Visible = True
                GridView1.Columns(Fields1.c1_cgstamount).Visible = True
                GridView1.Columns(Fields1.c1_sgstamount).Visible = True
            Else
                GridView1.Columns(Fields1.c1_igstperc).Visible = True
                GridView1.Columns(Fields1.c1_cgstperc).Visible = False
                GridView1.Columns(Fields1.c1_sgstperc).Visible = False
                GridView1.Columns(Fields1.c1_igstamount).Visible = True
                GridView1.Columns(Fields1.c1_cgstamount).Visible = False
                GridView1.Columns(Fields1.c1_sgstamount).Visible = False
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

    Private Sub GridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView2.CellClick
        VatCalculation = False
        'If GridView2.Rows.Count = 2 Then
        '    If GridView2.Rows(0).ReadOnly = True And GridView2.Rows(1).ReadOnly = True Then
        '        GridView2.Rows.Add(1)
        '    End If
        'End If
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
            Dim ds_vatcalc As New DataSet
            Dim cnt As Integer
            Dim tmpamt_t As Double, tmpperc_t As Double
            Dim Flg_t As String, tmpformulaamt_t As Double, Tmpdescid_t As Double
            Dim Tmpflg_t As Boolean
            'Dim l As Integer


            'ds_vatcalc.Clear()
            'ds_vatcalc = Nothing
            'cmd = New SqlCommand
            'cmd.Connection = conn
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.CommandText = "VATPERCCALC"
            'da_detl = New SqlDataAdapter(cmd)
            'ds_vatcalc = New DataSet
            'da_detl.Fill(ds_vatcalc)
            'cnt = ds_vatcalc.Tables(0).Rows.Count


            'If cnt = 0 Then l = 0 Else l = 2

            If VatCalculation = False And (e.ColumnIndex = fields2.c2_Perc Or e.ColumnIndex = fields2.c2_Type Or e.ColumnIndex = fields2.c2_Descid) And e.RowIndex >= 0 Then
                For g As Integer = 2 To GridView2.Rows.Count - 1
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
                                    tmpamt_t = (Val(Txt_Totamt.Text) * tmpperc_t / 100)
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

                VatCalculation = False
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
                            amt_t = (Val(Txt_Totamt.Text) * perc_t / 100)
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

    Private Sub GridView2_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles GridView2.DataError
        'GridView2.Rows(GridView2.CurrentRow.Index).Cells(GridView2.CurrentCell.ColumnIndex).Value = 0
        'GridView2.Columns(GridView2.CurrentCell.ColumnIndex).DefaultCellStyle.Format = "#.00"
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
                            Tmpamt_t = Val(Txt_Totamt.Text)
                        ElseIf s = "+" Or s = "-" Then
                            Plusorminus_t = s
                        Else
                            For j = 2 To GridView2.Rows.Count - 1
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

    Private Sub calcnetamt()
        Try
            Dim itemcnt As Integer

            txt_totqty.Text = Format(Tot_Calc(GridView1, Fields1.c1_qty), "#######0")
            Txt_Totamt.Text = Format(Tot_Calc(GridView1, Fields1.c1_amount), "#######0.00")
            txt_totaddless.Text = Format(Tot_Calc(GridView2, fields2.c2_Amount), "#######0.00")
            txt_totDiscount.Text = Format(Tot_Calc(GridView1, Fields1.c1_Discamt), "#######0.00")
            txt_netamt.Text = Format((Val(Txt_Totamt.Text)) + (Val(txt_totaddless.Text)), "#######0")
            'Txt_roundedoff.Text  == Format(Val(txt_netamt.Text) - (Val(txt_subtoatal.Text) + Val(Txt_Totamt.Text)), "##########0.00")
            'txt_round.Text = Format(Tot_Calc(GridView1, Fields1.c1_amount), "#######0.00")
            ' If Math.Sign((((Val(Txt_Totamt.Text) + Val(txt_totaddless.Text)) - Val(txt_netamt.Text)))) = -1 Then
            Txt_roundedoff.Text = Format((Val(txt_netamt.Text) - (Val(Txt_Totamt.Text) + Val(txt_totaddless.Text))), "##########0.00")
            'End If
            txt_netamt.Text = Format(Val(txt_netamt.Text), "#######0.00")

            For i = 0 To GridView1.Rows.Count - 1
                Itemid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_itemid).Value))

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
            Dim Costrate_t As Double
            Dim Tmpitemcode_t As String, Tmpuomid_t As Double, tmptaxperc_t, Tmpdecimal_t As Double, tmpuom_t As String, Tmpitemid_t As Double
            Dim Tmpitemdes_t As String, activerow_tmp As Integer, Rate_t As Double, Discount_t As Double, Freeqty_t As Double, Freeitem_t As Double, forqty_t As Double
            Dim TmpHsncode_t As String
            Dim Tmpcgst_t As Double, Tmpigst_t As Double, Tmpsgst_t As Double
            If activecol < 0 Or activerow < 0 Then Exit Sub
            Dim ds_itm As New DataSet
            Dim OfferAddQty_t As Double, OfferlessQty_t As Double, Addqty_t As Double, LessQty_t As Double

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t

                Case Fields1.c1_itemcode

                    Dim Nextcond_t As String = "", Nextcond_t1 As String = ""
                    For i = 0 To GridView1.Rows.Count - 1
                        Itemid_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_itemid, i).Value), 0, GridView1.Item(Fields1.c1_itemid, i).Value)
                        If Itemid_t <> 0 And i <> activerow Then
                            Nextcond_t = String.Concat(Nextcond_t, Itemid_t, ",")
                        End If
                    Next

                    If Nextcond_t <> "" Then
                        Nextcond_t = Nextcond_t.Remove(Nextcond_t.Length - 1)
                    Else
                        Nextcond_t = "00"
                    End If

                    If val_t = 0 Then
                        Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.SGSTPERC,0) AS SGST,ISNULL(HM.IGSTPERC,0) AS IGST,IM.ITEMDES,IM.ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,UM.UOM,IM.UOMID," _
                            & " ISNULL(IM.COSTPRICE,0) AS COSTRATE,ISNULL(IM.TAXPERC,0) AS TAXPERC,isnull(um.NOOFDECIMAL,0) as NOOFDECIMAL,ISNULL(D.DISCOUNT,0)AS DISCOUNT" _
                            & ",ISNULL(IM.FREEITEM,0)AS FREEITEM,ISNULL(IM.FREEQTY,0)AS FREEQTY,ISNULL(IM.FORQTY,0)AS FORQTY,isnull(im.HSNACCOUNTINGCODE,'') as HSNCODE " _
                            & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY   FROM ITEM_MASTER IM " _
                            & " LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE = IM.HSNACCOUNTINGCODE  LEFT JOIN ( SELECT ITEMID,DISCOUNT FROM DISCOUNT_DETAIL WHERE PARTYID = " & Partyid_t & " )D ON D.ITEMID = IM.ITEMID " _
                            & "ORDER BY IM.ITEMCODE"
                        'commented for show all item even it present in List
                        '& " WHERE IM.ITEMID NOT IN (" & Nextcond_t & ") ORDER BY IM.ITEMCODE "
                    Else
                        Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.SGSTPERC,0) AS SGST,ISNULL(HM.IGSTPERC,0) AS IGST,IM.ITEMDES as ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL,IM.ITEMTAMILDES  as ITEMDES," _
                            & "ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.TAMILUOM AS UOM,IM.UOMID,ISNULL(IM.TAXPERC,0) AS TAXPERC,isnull(um.NOOFDECIMAL,0) as NOOFDECIMAL, " _
                            & "ISNULL(D.DISCOUNT,0)AS DISCOUNT,ISNULL(IM.FREEITEM,0)AS FREEITEM,ISNULL(IM.FREEQTY,0)AS FREEQTY,ISNULL(IM.FORQTY,0)AS FORQTY ,isnull(im.HSNACCOUNTINGCODE,'') as HSNCODE " _
                            & " ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID " _
                            & " LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE = IM.HSNACCOUNTINGCODE LEFT JOIN ( SELECT ITEMID,DISCOUNT FROM DISCOUNT_DETAIL WHERE PARTYID = " & Partyid_t & " )D ON D.ITEMID = IM.ITEMID  " _
                            & "ORDER BY IM.ITEMCODE"
                        'commented for show all item even it present in List
                        '& "WHERE IM.ITEMID NOT IN (" & Nextcond_t & ") ORDER BY IM.ITEMCODE "
                    End If

                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da = New SqlDataAdapter(cmd)
                    ds_item = New DataSet
                    ds_item.Clear()
                    da.Fill(ds_item)

                    If ds_item.Tables(0).Rows.Count > 0 Then
                        VisibleCols.Add("ITEMCODE")
                        VisibleCols.Add("ITEMDES")
                        VisibleCols.Add("UOM")

                        Colheads.Add("ItemCode")
                        Colheads.Add("Item Desc")
                        Colheads.Add("Uom")

                        fm.Frm_Width = 550
                        fm.Frm_Height = 400
                        fm.Frm_Left = 320
                        fm.Frm_Top = 280

                        fm.MainForm = New frm_Agencybill_Invoice
                        fm.Active_ctlname = "Gridview1"

                        Csize.Add(150)
                        Csize.Add(250)
                        Csize.Add(100)

                        If ds_item.Tables(0).Rows.Count = 1 Then
                            GridView1.Rows(activerow).Cells(activecol).Value = ds_item.Tables(0).Rows(0).Item("ITEMCODE").ToString
                            GridView1.Rows(activerow).Cells(activecol + 1).Value = ds_item.Tables(0).Rows(0).Item("ITEMID")
                            Tmpitemcode_t = ds_item.Tables(0).Rows(0).Item("ITEMCODE").ToString
                            Itemid_t = ds_item.Tables(0).Rows(0).Item("ITEMID")
                        Else
                            tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))

                            If val_t = 0 Then
                                Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.SGSTPERC,0) AS SGST,ISNULL(HM.IGSTPERC,0) AS IGST,IM.ITEMDES,IM.ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL," _
                                    & "ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.UOM,IM.UOMID,ISNULL(IM.TAXPERC,0) AS TAXPERC,isnull(um.NOOFDECIMAL,0) as NOOFDECIMAL," _
                                    & "ISNULL(D.DISCOUNT,0)AS DISCOUNT,ISNULL(IM.FREEITEM,0)AS FREEITEM,ISNULL(IM.FREEQTY,0)AS FREEQTY,ISNULL(IM.FORQTY,0)AS FORQTY,isnull(im.HSNACCOUNTINGCODE,'') as HSNCODE " _
                                    & "  ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  " _
                                    & "FROM ITEM_MASTER IM LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE = IM.HSNACCOUNTINGCODE " _
                                    & "LEFT JOIN (SELECT ITEMID,DISCOUNT FROM DISCOUNT_DETAIL WHERE PARTYID = " & Partyid_t & ") D ON D.ITEMID = IM.ITEMID " _
                                    & "ORDER BY IM.ITEMCODE"
                                'commented for show all item even it present in List
                                '& "WHERE IM.ITEMID NOT IN (" & Nextcond_t & ")  and im.itemcode ='" & tmppassstr & "' ORDER BY IM.ITEMCODE "
                            Else
                                Sqlstr = "SELECT IM.ITEMCODE,IM.ITEMID,ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.SGSTPERC,0) AS SGST,ISNULL(HM.IGSTPERC,0) AS IGST,IM.ITEMDES as ITEMTAMILDES,isnull(im.SELPRICERETAIL,0) as SELPRICERETAIL," _
                                    & "IM.ITEMTAMILDES as ITEMDES,ISNULL(IM.COSTPRICE,0) AS COSTRATE,UM.TAMILUOM AS UOM,IM.UOMID,ISNULL(IM.TAXPERC,0) AS TAXPERC," _
                                    & "ISNULL(um.NOOFDECIMAL,0) as NOOFDECIMAL,ISNULL(D.DISCOUNT,0)AS DISCOUNT,ISNULL(IM.FREEITEM,0)AS FREEITEM, " _
                                    & "ISNULL(IM.FREEQTY,0)AS FREEQTY,ISNULL(IM.FORQTY,0)AS FORQTY ,isnull(im.HSNACCOUNTINGCODE,'') as HSNCODE " _
                                    & "  ,ISNULL(IM.OFFERADDQTY,0) AS OFFERADDQTY,ISNULL(IM.OFFERLESSQTY,0) AS OFFERLESSQTY,ISNULL(IM.ADDQTY,0) AS ADDQTY,ISNULL(IM.LESSQTY,0) AS LESSQTY  FROM ITEM_MASTER IM " _
                                    & "LEFT JOIN UOM_MASTER UM ON UM.MASTERID = IM.UOMID LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE = IM.HSNACCOUNTINGCODE LEFT JOIN (SELECT ITEMID,DISCOUNT FROM DISCOUNT_DETAIL " _
                                    & "WHERE PARTYID = " & Partyid_t & ") D ON D.ITEMID = IM.ITEMID  ORDER BY IM.ITEMCODE " _
                                    'commented for show all item even it present in List
                                '& "WHERE IM.ITEMID NOT IN (" & Nextcond_t & ") and im.itemcode ='" & tmppassstr & "' ORDER BY IM.ITEMCODE "
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
                            fm.VarNew = ""
                            fm.VarNewid = 0
                            fm.EXECUTE(conn, ds_item, VisibleCols, Colheads, Itemid_t, "", False, Csize, "", False, False, "", tmppassstr)
                            GridView1.Rows(activerow).Cells(activecol).Value = fm.VarNew
                            GridView1.Rows(activerow).Cells(activecol + 1).Value = fm.VarNewid
                            Tmpitemcode_t = fm.VarNew
                            Itemid_t = fm.VarNewid
                        End If

                        Formshown_t = fm.Formshown

                        Dim tmprow_t As Integer
                        tmprow_t = GridView1.Rows.Count
                        activerow_tmp = activerow + 1

                        If ds_item.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
                            'If Formshown_t = True And ds_item.Tables(0).Rows.Count > 0 And fm.VarNewid <> 0 Then
                            ds_item.Tables(0).DefaultView.RowFilter = "itemid = " & Itemid_t & " "
                            index_t = ds_item.Tables(0).Rows.IndexOf(ds_item.Tables(0).DefaultView.Item(0).Row)

                            'If tmprow_t = activerow_tmp Then
                            '    GridView1.Rows.Add(ds_item.Tables(0).DefaultView.Count)
                            'Else
                            '    ' GridView1.Rows.RemoveAt(activerow)
                            '    ' GridView1.Rows.Insert(activerow, ds_item.Tables(0).DefaultView.Count)
                            'End If

                            For i = 0 To ds_item.Tables(0).DefaultView.Count - 1
                                Tmpitemcode_t = ds_item.Tables(0).Rows(i + index_t).Item("itemcode").ToString
                                Tmpitemdes_t = ds_item.Tables(0).Rows(i + index_t).Item("itemdes").ToString
                                TmpHsncode_t = ds_item.Tables(0).Rows(i + index_t).Item("hsncode").ToString
                                Tmpitemid_t = ds_item.Tables(0).Rows(i + index_t).Item("itemid")
                                Tmpsgst_t = ds_item.Tables(0).Rows(i + index_t).Item("sgst")
                                Tmpigst_t = ds_item.Tables(0).Rows(i + index_t).Item("igst")
                                Tmpcgst_t = ds_item.Tables(0).Rows(i + index_t).Item("cgst")
                                tmpuom_t = ds_item.Tables(0).Rows(i + index_t).Item("uom").ToString
                                Tmpuomid_t = ds_item.Tables(0).Rows(i + index_t).Item("uomid")
                                Costrate_t = ds_item.Tables(0).Rows(i + index_t).Item("costrate")
                                tmptaxperc_t = ds_item.Tables(0).Rows(i + index_t).Item("taxperc")
                                Rate_t = ds_item.Tables(0).Rows(i + index_t).Item("SELPRICERETAIL")
                                Tmpdecimal_t = ds_item.Tables(0).Rows(i + index_t).Item("NOOFDECIMAL")
                                Discount_t = ds_item.Tables(0).Rows(i + index_t).Item("Discount")
                                Freeitem_t = ds_item.Tables(0).Rows(i + index_t).Item("Freeitem")
                                Freeqty_t = ds_item.Tables(0).Rows(i + index_t).Item("Freeqty")
                                forqty_t = ds_item.Tables(0).Rows(i + index_t).Item("forqty")
                                OfferAddQty_t = ds_item.Tables(0).Rows(i + index_t).Item("offeraddqty")
                                OfferlessQty_t = ds_item.Tables(0).Rows(i + index_t).Item("offerlessqty")
                                Addqty_t = ds_item.Tables(0).Rows(i + index_t).Item("addqty")
                                LessQty_t = ds_item.Tables(0).Rows(i + index_t).Item("lessqty")

                                'NOOFDECIMAL
                                If Tmpitemid_t <> 0 Then
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_itemcode).Value = Tmpitemcode_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_itemdes).Value = Tmpitemdes_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_vatper).Value = tmptaxperc_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_itemid).Value = Tmpitemid_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_uom).Value = tmpuom_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_uomid).Value = Tmpuomid_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_offeraddqty).Value = OfferAddQty_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_hsnaccountcode).Value = TmpHsncode_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_offerlessqty).Value = OfferlessQty_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_addrs).Value = Addqty_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_lessrs).Value = LessQty_t

                                    'If (IIf(IsDBNull(GridView1.Rows(activerow + i).Cells(Fields1.c1_rate).Value), 0, (GridView1.Rows(activerow + i).Cells(Fields1.c1_rate).Value))) = 0 Then
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_rate).Value = Rate_t
                                    'End If

                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_selrate).Value = Rate_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_costrate).Value = Costrate_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_decimal).Value = Tmpdecimal_t
                                    GridView1.Rows(activerow + i).Cells(Fields1.c1_Discount).Value = Discount_t

                                    If LCase(txt_state.Text) = LCase("tamilnadu") Then
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_cgstperc).Value = Tmpcgst_t
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_sgstperc).Value = Tmpsgst_t
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_igstperc).Value = 0
                                        GridView1.Columns(Fields1.c1_igstperc).Visible = False
                                        GridView1.Columns(Fields1.c1_igstamount).Visible = False
                                        GridView1.Columns(Fields1.c1_cgstperc).Visible = True
                                        GridView1.Columns(Fields1.c1_sgstperc).Visible = True
                                        GridView1.Columns(Fields1.c1_cgstamount).Visible = True
                                        GridView1.Columns(Fields1.c1_sgstamount).Visible = True
                                    Else
                                        GridView1.Columns(Fields1.c1_igstperc).Visible = True
                                        GridView1.Columns(Fields1.c1_cgstperc).Visible = False
                                        GridView1.Columns(Fields1.c1_sgstperc).Visible = False
                                        GridView1.Columns(Fields1.c1_igstamount).Visible = True
                                        GridView1.Columns(Fields1.c1_cgstamount).Visible = False
                                        GridView1.Columns(Fields1.c1_sgstamount).Visible = False
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_igstperc).Value = Tmpigst_t
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_sgstperc).Value = 0
                                        GridView1.Rows(activerow + i).Cells(Fields1.c1_cgstperc).Value = 0
                                    End If
                                    'GridView1.Rows(activerow + i).Cells(Fields1.c1_Freeqty).Value = Freeqty_t
                                    'GridView1.Rows(activerow + i).Cells(Fields1.c1_Freeitem).Value = Freeitem_t
                                    'GridView1.Rows(activerow + i).Cells(Fields1.c1_Forqty).Value = forqty_t
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

            If Not GridView1.CurrentCell Is Nothing Then
                Itemid_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_itemid, GridView1.CurrentCell.RowIndex).Value), 0, GridView1.Item(Fields1.c1_itemid, GridView1.CurrentCell.RowIndex).Value)
                ds_stock.Clear()
                cmd = Nothing
                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "ITEMSTOCK_RPT_BILL"
                cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid.ToString
                cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemid_t.ToString
                cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationid_t.ToString
                cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = "select groupid from item_master where itemid =" & Itemid_t & ""
                cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DTP_Vchdate.Value.ToString("yyyy/MM/dd")
                da_stock = New SqlDataAdapter(cmd)
                ds_stock = New DataSet
                da_stock.Fill(ds_stock)

                Detlcnt_t = ds_stock.Tables(0).Rows.Count
                If Detlcnt_t > 0 Then
                    If ds_stock.Tables(0).Rows(0).Item("ITEMID") <> 0 Then GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(Fields1.c1_stockqty).Value = ds_stock.Tables(0).Rows(0).Item("BALANCE")
                End If
            End If
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
            Dim Rate_t As Double, amount_t As Double, Qty_t As Double
            Dim Remarks_t As String
            Dim Adtype_t As String, Adamt_t As Double, Adperc_t As Double, Addescid_t As Double, Freeqty_t As Double, Freeitem_t As Double, Forqty_t As Double
            Dim uomid_v As Double, Vatperc_t As Double, Exrate_v As Double, Vatamount_t As Double, CostRate_t As Double, Discperc_t As Double, Discamt_t As Double
            Dim CgstAmount_t As Double, Cgstperc_t As Double, Sgstperc_t As Double, SgstAmount_t As Double, _
                IGstAmount_t As Double, Igstperc_t As Double, HSNAccode_t As String

            If editflag = False Then
                txt_vchnum.Text = AutoNum(Process, True) 'if editflag trie begin tran start in autonum fun.
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            SavechhkFlg = False
            Headerid_t = GensaveInvoiceHead(IIf(editflag, 1, 0), Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Gencompid, Partyid_t, _
                                            txt_narration.Text, Val(txt_netamt.Text), Locationid_t, Lineid_t, Txt_reference.Text, _
                                            Txt_vehicle.Text, Val(txt_totaddless.Text), Val(txt_totDiscount.Text), Val(Txt_outstanding.Text), 0, 0, "", "", "", Nothing, 0)

            For i = 0 To GridView1.Rows.Count - 1

                Itemid_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_itemid, i).Value), 0, GridView1.Item(Fields1.c1_itemid, i).Value)
                uomid_v = IIf(IsDBNull(GridView1.Item(Fields1.c1_uomid, i).Value), 0, GridView1.Item(Fields1.c1_uomid, i).Value)
                'Taxper_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, i).Value), 0, GridView1.Item(Fields1.c1_vatper, i).Value)
                'Taxper_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, i).Value), 0, GridView1.Item(Fields1.c1_vatper, i).Value)
                Qty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, i).Value), 0, GridView1.Item(Fields1.c1_qty, i).Value)
                Exrate_v = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, i).Value), 0, GridView1.Item(Fields1.c1_exrate, i).Value)
                CostRate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_costrate, i).Value), 0, GridView1.Item(Fields1.c1_costrate, i).Value)
                Vatamount_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatamt, i).Value), 0, GridView1.Item(Fields1.c1_vatamt, i).Value)
                Vatperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, i).Value), 0, GridView1.Item(Fields1.c1_vatper, i).Value)
                Rate_t = IIf(IsDBNull(GridView1.Item(fields1.c1_Rate, i).Value), 0, GridView1.Item(fields1.c1_Rate, i).Value)
                amount_t = IIf(IsDBNull(GridView1.Item(fields1.c1_Amount, i).Value), 0, GridView1.Item(fields1.c1_Amount, i).Value)
                Remarks_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_remarks, i).Value), 0, GridView1.Item(Fields1.c1_remarks, i).Value)
                Discperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Discount, i).Value), 0, GridView1.Item(Fields1.c1_Discount, i).Value)
                Discamt_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Discamt, i).Value), 0, GridView1.Item(Fields1.c1_Discamt, i).Value)
                Freeqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Freeqty, i).Value), 0, GridView1.Item(Fields1.c1_Freeqty, i).Value)
                Freeitem_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Freeitem, i).Value), 0, GridView1.Item(Fields1.c1_Freeitem, i).Value)
                Forqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Forqty, i).Value), 0, GridView1.Item(Fields1.c1_Forqty, i).Value)
                CgstAmount_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_cgstamount, i).Value), 0, GridView1.Item(Fields1.c1_cgstamount, i).Value)
                Cgstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_cgstperc, i).Value), 0, GridView1.Item(Fields1.c1_cgstperc, i).Value)
                SgstAmount_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_sgstamount, i).Value), 0, GridView1.Item(Fields1.c1_sgstamount, i).Value)
                Sgstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_sgstperc, i).Value), 0, GridView1.Item(Fields1.c1_sgstperc, i).Value)
                IGstAmount_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_igstamount, i).Value), 0, GridView1.Item(Fields1.c1_igstamount, i).Value)
                Igstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_igstperc, i).Value), 0, GridView1.Item(Fields1.c1_igstperc, i).Value)
                HSNAccode_t = IIf(CStr(GridView1.Item(Fields1.c1_hsnaccountcode, i).Value) Is Nothing, "", GridView1.Item(Fields1.c1_hsnaccountcode, i).Value)

                If Remarks_t Is Nothing Then Remarks_t = ""
                If Itemid_t <> 0 And amount_t <> 0 And Qty_t <> 0 Or Freeqty_t <> 0 Then
                    Gensaveinvoicedetl(Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Gencompid, i + 1, Itemid_t, uomid_v, Qty_t, Rate_t, _
                                       Exrate_v, Vatamount_t, Remarks_t, Vatperc_t, amount_t, CostRate_t, Discperc_t, Discamt_t, Freeqty_t, Freeitem_t, Forqty_t, _
                                       HSNAccode_t, Cgstperc_t, CgstAmount_t, Sgstperc_t, SgstAmount_t, Igstperc_t, IGstAmount_t)
                    SavechhkFlg = True
                End If
            Next

            For j = 0 To GridView2.Rows.Count - 1
                Adtype_t = GridView2.Item(fields2.c2_Type, j).Value
                Addescid_t = GridView2.Item(fields2.c2_Descid, j).Value
                Adperc_t = GridView2.Item(fields2.c2_Perc, j).Value
                Adamt_t = GridView2.Item(fields2.c2_Amount, j).Value

                If Adtype_t <> "" And Addescid_t <> 0 And Adamt_t <> 0 Then
                    Gensaveinvoiceaddlessdetl(Headerid_t, j + 1, Addescid_t, Adperc_t, Adamt_t, Adtype_t, Gencompid)
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

            If Headerid_t <> 0 Then
                cmd = Nothing
                cmd = New SqlCommand("AC_SALE_UPD", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trans
                cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
                cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd.ExecuteNonQuery()
            End If

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
            Dim Text As String = DirectCast(sender, TextBox).Text
            Dim Decimal_t As Double
            If Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar) And e.KeyChar <> "." Then
                e.Handled = True
            End If

            If Text.Contains(".") AndAlso e.KeyChar = "."c Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                If Text.IndexOf(".") <> -1 Then
                    If GridView1.CurrentCell.ColumnIndex = Fields1.c1_rate Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                    ElseIf GridView1.CurrentCell.ColumnIndex = Fields1.c1_qty Then
                        Decimal_t = IIf(IsDBNull(GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(Fields1.c1_decimal).Value), 0, (GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(Fields1.c1_decimal).Value))
                        If Text.Length >= Text.IndexOf(".") + Decimal_t + 2 Then
                            e.Handled = True
                        End If
                    ElseIf GridView1.CurrentCell.ColumnIndex = Fields1.c1_amount Or GridView1.CurrentCell.ColumnIndex = Fields1.c1_exrate Then
                        If Text.Length >= Text.IndexOf(".") + 4 Then
                            e.Handled = True
                        End If
                    End If
                End If
            End If

            If GridView1.CurrentCell.ColumnIndex = Fields1.c1_itemcode Or GridView1.CurrentCell.ColumnIndex = Fields1.c1_remarks Or _
                GridView1.CurrentCell.ColumnIndex = Fields1.c1_itemdes Or GridView1.CurrentCell.ColumnIndex = Fields1.c1_hsnaccountcode Then
                e.Handled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Colindex_t As Integer, Rowindex_t As Integer
            Dim TmpAmt_t As Double, TmpQty_t As Double, TmpexRate_t As Double, tmprate_t As Double, Tmpvat_t As Double
            Dim Tmpvatamt_t As Double

            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex

            If GridView1.CurrentCell.ColumnIndex = Fields1.c1_amount Then
                If Not CType(sender, TextBox).Text.Trim.Length = 0 Then
                    TmpAmt_t = CDec(CType(sender, TextBox).Text)
                    TmpexRate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, Rowindex_t).Value), 0, GridView1.Item(Fields1.c1_exrate, Rowindex_t).Value)
                    TmpQty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, Rowindex_t).Value), 0, GridView1.Item(Fields1.c1_qty, Rowindex_t).Value)
                    Tmpvat_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, Rowindex_t).Value), 0, GridView1.Item(Fields1.c1_vatper, Rowindex_t).Value)
                    Tmpvatamt_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatamt, Rowindex_t).Value), 0, GridView1.Item(Fields1.c1_vatamt, Rowindex_t).Value)

                    If TmpQty_t <> 0 Then GridView1.Rows(Rowindex_t).Cells(Fields1.c1_exrate).Value = TmpAmt_t / TmpQty_t Else GridView1.Rows(Rowindex_t).Cells(Fields1.c1_exrate).Value = 0
                    TmpexRate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, Rowindex_t).Value), 0, GridView1.Item(Fields1.c1_exrate, Rowindex_t).Value)

                    GridView1.Item(Fields1.c1_vatamt, Rowindex_t).Value = (TmpexRate_t * Tmpvat_t) / 100

                    tmprate_t = (TmpexRate_t / 100) * ((100 + Tmpvat_t))
                    GridView1.Item(Fields1.c1_vatcalc, Rowindex_t).Value = TmpQty_t * Tmpvatamt_t

                    GridView1.Rows(Rowindex_t).Cells(Fields1.c1_rate).Value = tmprate_t
                End If
            End If
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
            Dim ds_itemdet As New DataSet
            Dim da_itemdet As SqlDataAdapter

            If Rowindex_t >= 0 And colindex_t >= 0 Then
                Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value))

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
            GridView1.Columns(Fields1.c1_qty).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_stockqty).ValueType = GetType(Decimal)
            GridView1.Columns(fields1.c1_Rate).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_amount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_exrate).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatper).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatamt).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatcalc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_cgstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_cgstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_sgstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_sgstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_igstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_igstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_taxablevalue).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_totalamount).ValueType = GetType(Decimal)

            GridView1.Columns(Fields1.c1_qty).DefaultCellStyle.Format = "#0"
            GridView1.Columns(Fields1.c1_stockqty).DefaultCellStyle.Format = "#0"
            GridView1.Columns(Fields1.c1_rate).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_amount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_cgstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_cgstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_sgstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_sgstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_igstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_igstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_taxablevalue).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_totalamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_exrate).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatper).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatamt).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatcalc).DefaultCellStyle.Format = "#0.00"

            Dim Decimal_t As Double = 0
            Dim Format_t As String = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_decimal).Value))

                Decimal_tt = ""
                For k = 1 To Decimal_t
                    Decimal_tt = String.Concat(Decimal_tt, "0")
                Next

                If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_qty).Style.Format = Format_t
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_stockqty).Style.Format = Format_t
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_Freeqty).Style.Format = Format_t
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
                    If IsValidRow(GridView1, "C_Itemid", "C_Itemcode", "C_Beforeexcessqty") Then
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
                'Call FreeqtyInsertion(e.RowIndex, GridView1)
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellEnter
        Try
            Dim Decimal_t As Double = 0
            Dim Format_t As String = ""
            Dim Decimal_tt As String = ""
            Dim k As Integer = 0

            GridView1.Columns(Fields1.c1_qty).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_stockqty).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_Freeqty).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_rate).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_amount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_exrate).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatper).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatamt).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_vatcalc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_Discount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_Discamt).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_cgstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_cgstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_sgstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_sgstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_igstamount).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_igstperc).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_taxablevalue).ValueType = GetType(Decimal)
            GridView1.Columns(Fields1.c1_totalamount).ValueType = GetType(Decimal)

            GridView1.Columns(Fields1.c1_qty).DefaultCellStyle.Format = "#0"
            GridView1.Columns(Fields1.c1_stockqty).DefaultCellStyle.Format = "#0"
            GridView1.Columns(Fields1.c1_Freeqty).DefaultCellStyle.Format = "#0"
            GridView1.Columns(Fields1.c1_rate).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_amount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_exrate).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatper).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatamt).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_vatcalc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_Discount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_Discamt).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_cgstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_cgstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_sgstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_sgstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_igstamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_igstperc).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_taxablevalue).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(Fields1.c1_totalamount).DefaultCellStyle.Format = "#0.00"

            GridView1.Columns(Fields1.c1_Freeqty).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_Discount).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            GridView1.Columns(Fields1.c1_Discamt).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Decimal_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_decimal).Value), 0, (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_decimal).Value))

                Decimal_tt = ""

                For k = 1 To Decimal_t
                    Decimal_tt = String.Concat(Decimal_tt, "0")
                Next

                If Decimal_t = 0 Then Format_t = "#0" Else Format_t = "#0" & "." & Decimal_tt
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_qty).Style.Format = Format_t
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_stockqty).Style.Format = Format_t
                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_Freeqty).Style.Format = Format_t
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        Try
            Dim tmpqty_t As Double, tmprate_t As Double, tmpexrate_t As Double, tmpamt_t As Double, Tmpvat_t As Double
            Dim Tmpvatamt_t As Double, tmpDisc_t As Double, tmpDiscAmt_t As Double
            Dim tmpofferaddqty_t As Double, tmpOfferlessqty_t As Double, tmpaddrs_t As Double, tmplessrs_t As Double

            If (e.ColumnIndex = Fields1.c1_qty Or e.ColumnIndex = Fields1.c1_itemcode Or e.ColumnIndex = Fields1.c1_selrate) And e.RowIndex >= 0 Then
                tmpqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_qty, e.RowIndex).Value)
                tmpofferaddqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_offeraddqty, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_offeraddqty, e.RowIndex).Value)
                tmpOfferlessqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_offerlessqty, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_offerlessqty, e.RowIndex).Value)
                tmpaddrs_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_addrs, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_addrs, e.RowIndex).Value)
                tmplessrs_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_lessrs, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_lessrs, e.RowIndex).Value)
                tmprate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_selrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_selrate, e.RowIndex).Value)

                GridView1.Rows(e.RowIndex).Cells(Fields1.c1_rate).Value = tmprate_t

                If tmpqty_t <= tmpofferaddqty_t Then
                    GridView1.Rows(e.RowIndex).Cells(Fields1.c1_rate).Value = tmprate_t + tmpaddrs_t
                End If

                If tmpqty_t >= tmpOfferlessqty_t Then
                    GridView1.Rows(e.RowIndex).Cells(Fields1.c1_rate).Value = tmprate_t - tmplessrs_t
                End If
            End If

            If (e.ColumnIndex = Fields1.c1_qty Or e.ColumnIndex = Fields1.c1_rate Or e.ColumnIndex = Fields1.c1_vatper Or e.ColumnIndex = Fields1.c1_Discount) And e.RowIndex >= 0 And e.ColumnIndex >= 0 Then

                tmpqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_qty, e.RowIndex).Value)
                Tmpvatamt_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatamt, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_vatamt, e.RowIndex).Value)
                tmpexrate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value)
                tmpDisc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Discount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_Discount, e.RowIndex).Value)

                tmprate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_rate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_rate, e.RowIndex).Value)
                Tmpvat_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_vatper, e.RowIndex).Value)

                GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value = (tmprate_t / (100 + Tmpvat_t)) * 100
                tmpexrate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value)

                tmpamt_t = tmpqty_t * tmpexrate_t
                tmpDiscAmt_t = (tmpDisc_t / 100) * tmpamt_t

                GridView1.Item(Fields1.c1_amount, e.RowIndex).Value = tmpamt_t
                GridView1.Item(Fields1.c1_Discamt, e.RowIndex).Value = tmpDiscAmt_t

                GridView1.Item(Fields1.c1_vatcalc, e.RowIndex).Value = tmpqty_t * Tmpvatamt_t

                tmpexrate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value)
                GridView1.Item(Fields1.c1_vatamt, e.RowIndex).Value = (tmpexrate_t * Tmpvat_t) / 100

                'Call FreeqtyInsertion(e.RowIndex, GridView1)

                Call calcnetamt()
                'VatCalculation = True
                'VatPercCalc()
            End If

            If (e.ColumnIndex = Fields1.c1_qty Or e.ColumnIndex = Fields1.c1_itemid Or e.ColumnIndex = Fields1.c1_rate Or e.ColumnIndex = Fields1.c1_igstperc Or e.ColumnIndex = Fields1.c1_sgstperc _
                Or e.ColumnIndex = Fields1.c1_cgstperc Or e.ColumnIndex = Fields1.c1_cgstamount Or e.ColumnIndex = Fields1.c1_sgstamount _
                Or e.ColumnIndex = Fields1.c1_igstamount _
                Or e.ColumnIndex = Fields1.c1_amount Or e.ColumnIndex = Fields1.c1_Discount Or e.ColumnIndex = Fields1.c1_Discamt) And e.RowIndex >= 0 Then
                Dim CGst_t As Double, Sgst_t As Double, Igst_t As Double, Cgstperc_t As Double, Sgstperc_t As Double, IGstperc_t As Double, TaxableValue_t As Double, Tmpdiscount_t As Double

                tmpamt_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_amount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_amount, e.RowIndex).Value)
                Tmpdiscount_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_Discamt, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_Discamt, e.RowIndex).Value)
                Cgstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_cgstperc, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_cgstperc, e.RowIndex).Value)
                Sgstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_sgstperc, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_sgstperc, e.RowIndex).Value)
                IGstperc_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_igstperc, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_igstperc, e.RowIndex).Value)

                GridView1.Item(Fields1.c1_taxablevalue, e.RowIndex).Value = tmpamt_t - Tmpdiscount_t

                TaxableValue_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_taxablevalue, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_taxablevalue, e.RowIndex).Value)

                GridView1.Item(Fields1.c1_cgstamount, e.RowIndex).Value = TaxableValue_t * (Cgstperc_t / 100)
                GridView1.Item(Fields1.c1_sgstamount, e.RowIndex).Value = TaxableValue_t * (Sgstperc_t / 100)
                GridView1.Item(Fields1.c1_igstamount, e.RowIndex).Value = TaxableValue_t * (IGstperc_t / 100)

                CGst_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_cgstamount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_cgstamount, e.RowIndex).Value)
                Sgst_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_sgstamount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_sgstamount, e.RowIndex).Value)
                Igst_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_igstamount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_igstamount, e.RowIndex).Value)

                GridView1.Item(Fields1.c1_totalamount, e.RowIndex).Value = CGst_t + Sgst_t + Igst_t + TaxableValue_t
                ' Call Addless_Calc()
                Dim Descid_t As Double
                Dim Cgst As Boolean = False, sgst As Boolean = False, igst As Boolean = False
                'Dim Table(3, 1, 1) As Array
                'Dim k As Integer

                For I = 0 To GridView2.Rows.Count - 1
                    Descid_t = IIf(IsDBNull(GridView2.Rows(I).Cells(fields2.c2_Descid).Value), 0, (GridView2.Rows(I).Cells(fields2.c2_Descid).Value))
                    Select Case Descid_t
                        Case -29  'CGST
                            'Table(0, 1, 1) = {-29, True, I}
                            Cgst = True
                            GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_cgstamount), "#######0.00")
                        Case -30  'SGST
                            'Table(1, 1, 1) = {-30, True, I}
                            sgst = True
                            GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_sgstamount), "#######0.00")
                        Case -31  'IGST
                            'Table(2, 1, 1) = {-31, True, I}
                            igst = True
                            GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_igstamount), "#######0.00")
                    End Select
                    'If GridView2.Item(fields2.c2_Type, I).Value = "" Then
                    'End If
                Next

                If Cgst = False And Tot_Calc(GridView1, Fields1.c1_cgstamount) <> 0 Then
                    GridView2.Rows.Add(1)
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "CGST"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_cgstamount), "#######0.00")
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -29
                End If

                If sgst = False And Tot_Calc(GridView1, Fields1.c1_sgstamount) <> 0 Then
                    GridView2.Rows.Add(1)
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "SGST"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_sgstamount), "#######0.00")
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -30
                End If

                If igst = False And Tot_Calc(GridView1, Fields1.c1_igstamount) <> 0 Then
                    GridView2.Rows.Add(1)
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "IGST"
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_igstamount), "#######0.00")
                    GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -31
                End If

            End If
            If e.ColumnIndex = Fields1.c1_itemid Or e.ColumnIndex = Fields1.c1_itemcode Or e.ColumnIndex = Fields1.c1_itemdes Then
                If Not GridView1.CurrentCell Is Nothing Then
                    Itemid_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_itemid, GridView1.CurrentCell.RowIndex).Value), 0, GridView1.Item(Fields1.c1_itemid, GridView1.CurrentCell.RowIndex).Value)

                    If Itemid_t <> 0 And Locationid_t <> 0 Then
                        ds_stock.Clear()
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "ITEMSTOCK_RPT_bill"
                        cmd.Parameters.Add("@COMPID", SqlDbType.VarChar).Value = Gencompid.ToString
                        cmd.Parameters.Add("@ITEMID", SqlDbType.VarChar).Value = Itemid_t.ToString
                        cmd.Parameters.Add("@LOCATIONID", SqlDbType.VarChar).Value = Locationid_t.ToString
                        cmd.Parameters.Add("@GROUPID", SqlDbType.VarChar).Value = "select groupid from item_master where itemid =" & Itemid_t & ""
                        cmd.Parameters.Add("@TODATE", SqlDbType.VarChar).Value = DTP_Vchdate.Value.ToString("yyyy/MM/dd")
                        da_stock = New SqlDataAdapter(cmd)
                        ds_stock = New DataSet
                        da_stock.Fill(ds_stock)

                        Detlcnt_t = ds_stock.Tables(0).Rows.Count
                        If Detlcnt_t > 0 Then
                            If ds_stock.Tables(0).Rows(0).Item("ITEMID") <> 0 Then GridView1.Rows(GridView1.CurrentCell.RowIndex).Cells(Fields1.c1_stockqty).Value = ds_stock.Tables(0).Rows(0).Item("BALANCE")
                        End If
                    End If

                End If
            End If

            'If e.ColumnIndex = Fields1.c1_amount And e.ColumnIndex >= 0 And e.RowIndex >= 0 Then

            '    '  tmpamt_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_amount, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_amount, e.RowIndex).Value)
            '    '  tmpqty_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_qty, e.RowIndex).Value)

            '    '  If tmpqty_t <> 0 Then GridView1.Rows(e.RowIndex).Cells(Fields1.c1_rate).Value = tmpamt_t / tmpqty_t Else GridView1.Rows(e.RowIndex).Cells(Fields1.c1_rate).Value = 0

            '    ''tmpexrate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value)
            '    'tmprate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_rate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_rate, e.RowIndex).Value)
            '    'Tmpvat_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_vatper, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_vatper, e.RowIndex).Value)

            '    'tmpexrate_t = tmpamt_t / tmpqty_t

            '    'GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value = tmpexrate_t
            '    'GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value = (tmprate_t / (100 + Tmpvat_t)) * 100
            '    'GridView1.Item(Fields1.c1_vatcalc, e.RowIndex).Value = tmpqty_t * Tmpvatamt_t

            '    'tmpexrate_t = IIf(IsDBNull(GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value), 0, GridView1.Item(Fields1.c1_exrate, e.RowIndex).Value)
            '    'GridView1.Item(Fields1.c1_vatamt, e.RowIndex).Value = (tmpexrate_t * Tmpvat_t) / 100
            'End If

            If e.RowIndex >= 0 And (e.ColumnIndex = Fields1.c1_vatcalc Or e.ColumnIndex = Fields1.c1_qty Or e.ColumnIndex = Fields1.c1_rate Or e.ColumnIndex = Fields1.c1_Discount) Then
                VatCalculation = True
                VatPercCalc()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FreeqtyInsertion(ByVal Rowindex As Integer, ByVal gridview As DataGridView)
        Try
            Dim InsertAt_t As Integer
            If GridView1.Item(Fields1.c1_Freeitem, Rowindex).Value <> 0 Then
                If IIf(IsDBNull(GridView1.Item(Fields1.c1_qty, Rowindex).Value), 0, GridView1.Item(Fields1.c1_qty, Rowindex).Value) >= GridView1.Item(Fields1.c1_Forqty, Rowindex).Value Then

                    If GridView1.Rows.Count = 1 Then
                        GridView1.Rows.Add(1)
                    Else
                        If IIf(IsNothing(GridView1.Item(Fields1.c1_itemcode, Rowindex + 1).Value), "", GridView1.Item(Fields1.c1_itemcode, Rowindex + 1).Value) <> "" Then
                            'GridView1.Rows.Add(1)
                            InsertAt_t = Rowindex + 1
                        Else
                            InsertAt_t = GridView1.Rows.Count - 1
                        End If
                    End If

                    Dim Bal_t As Double, Qty_t As Double, t_freeqty As Double
                    Bal_t = (GridView1.Item(Fields1.c1_qty, Rowindex).Value) Mod (GridView1.Item(Fields1.c1_Forqty, Rowindex).Value)
                    Qty_t = (GridView1.Item(Fields1.c1_qty, Rowindex).Value) - Bal_t

                    t_freeqty = (Qty_t / GridView1.Item(Fields1.c1_Forqty, Rowindex).Value)

                    If t_freeqty = 0 Then
                        t_freeqty = 1
                    Else
                        t_freeqty = Math.Round(t_freeqty)
                    End If

                    If IIf(IsNothing(GridView1.Item(Fields1.c1_itemdes, Rowindex + 1).Value), "", GridView1.Item(Fields1.c1_itemdes, Rowindex + 1).Value) <> "" And editflag = True Then
                        InsertAt_t = GridView1.Rows.Count - 1
                    End If

                    GridView1.Item(Fields1.c1_Freeqty, InsertAt_t).Value = (t_freeqty * GridView1.Item(Fields1.c1_Freeqty, Rowindex).Value)
                    'GridView1.Item(Fields1.c1_itemcode, InsertAt_t).Value = "01"
                    'Get values from table by ds

                    If InsertAt_t = GridView1.Rows.Count - 1 Then

                        Sqlstr = "Select * from Item_master I left join Uom_master u on u.masterid = i.UOMID Where Itemid = " & GridView1.Item(Fields1.c1_Freeitem, Rowindex).Value & ""
                        cmd = New SqlCommand(Sqlstr, conn)
                        cmd.CommandType = CommandType.Text
                        da_freeitem = New SqlDataAdapter(cmd)
                        ds_freeitem = New DataSet
                        ds_freeitem.Clear()
                        da_freeitem.Fill(ds_freeitem)

                        If ds_freeitem.Tables(0).Rows.Count > 0 Then
                            GridView1.Item(Fields1.c1_itemcode, InsertAt_t).Value = ds_freeitem.Tables(0).Rows(0).Item("Itemcode")
                            GridView1.Item(Fields1.c1_itemdes, InsertAt_t).Value = ds_freeitem.Tables(0).Rows(0).Item("Itemtamildes")
                            GridView1.Item(Fields1.c1_itemid, InsertAt_t).Value = ds_freeitem.Tables(0).Rows(0).Item("Itemid")
                            GridView1.Item(Fields1.c1_uom, InsertAt_t).Value = ds_freeitem.Tables(0).Rows(0).Item("TamilUom")
                            GridView1.Item(Fields1.c1_uomid, InsertAt_t).Value = ds_freeitem.Tables(0).Rows(0).Item("Uomid")
                            GridView1.Item(Fields1.c1_rate, InsertAt_t).Value = 0
                            GridView1.Item(Fields1.c1_Discamt, InsertAt_t).Value = 0
                            GridView1.Item(Fields1.c1_Discount, InsertAt_t).Value = 0
                            GridView1.Item(Fields1.c1_itemcode, InsertAt_t).ReadOnly = True

                            GridView1.Item(Fields1.c1_Forqty, InsertAt_t).Value = -1

                            If GridView1.Item(Fields1.c1_Forqty, InsertAt_t).Value = -1 Then
                                GridView1.Rows.Add(1)
                            End If

                        End If
                    End If
                Else
                    If GridView1.Rows.Count > 1 Then
                        Dim cnt As Integer = GridView1.Rows.Count - 1
                        For i = InsertAt_t To cnt
                            If GridView1.Item(Fields1.c1_Freeitem, InsertAt_t).Value = GridView1.Rows(i).Cells(Fields1.c1_Freeitem).Value Then
                                If GridView1.Rows(i + 1).Cells(Fields1.c1_qty).Value = 0 Then
                                    GridView1.Rows.RemoveAt(i + 1)
                                    Exit For
                                End If
                            ElseIf GridView1.Item(Fields1.c1_Freeitem, Rowindex).Value = GridView1.Rows(i).Cells(Fields1.c1_itemid).Value Then
                                If GridView1.Rows(i).Cells(Fields1.c1_qty).Value = 0 Then
                                    GridView1.Rows.RemoveAt(i)
                                    Exit For
                                End If
                            End If
                        Next
                    ElseIf GridView1.Rows.Count > 1 And editflag = True Then
                        'GridView1.Rows.RemoveAt(GridView1.Rows.Count - 1)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error.!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_DataMemberChanged(sender As Object, e As EventArgs) Handles GridView1.DataMemberChanged

    End Sub

    Private Sub GridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Dim tb As TextBox = CType(e.Control, TextBox)
        Select Case GridView1.CurrentCell.ColumnIndex
            Case Fields1.c1_qty, Fields1.c1_rate, Fields1.c1_amount, Fields1.c1_itemcode, Fields1.c1_remarks, Fields1.c1_hsnaccountcode
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
                AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        End Select
    End Sub

    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        Try
            Dim tmpval_t As Double

            If e.Control And e.KeyCode.ToString = "R" Then
                Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value))

                If Itemid_t <> 0 Then
                    Dim frmrateupd As New frmitemrateupdate
                    frmrateupd.ShowInTaskbar = False
                    frmrateupd.StartPosition = FormStartPosition.CenterScreen
                    frmrateupd.Itemid_t = Itemid_t
                    frmrateupd.ShowDialog()
                End If

            End If

            If e.KeyCode = Keys.Enter Then

                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name

                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)

                e.SuppressKeyPress = True

                If colindex_t = fields1.c1_Qty Then
                    tmpval_t = GridView1.Item(colindex_t, Rowindex_t).Value
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
            ElseIf Partyid_t = 0 Or txt_customer.Text = "" Then
                MsgBox("Party Should not be Empty.")
                txt_customer.Focus()
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
                'da.Fill(ds_tmppono)

                'If ds_tmppono.Tables(0).Rows.Count > 0 Then
                '    MsgBox("Could not Be Deleted, Becaues it Could Be Referenced in Purchase Entry.")
                '    Exit Sub
                'Else
                'If Generateeventlogs_t = True Then
                '    GensaveEventlogs(Event_Conn, "INVOICE", Headerid_v, Genuid, "Delete", Now, Gencompid)
                'End If
                Call GendelInvoice(Headerid_v)
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
            rm.Init(conn, "invoice", Servername_t, Headerid_v, Nothing, Nothing, "", Databasename_t, 0, False)
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
            Call gridreadonly(GridView1, Not visflag_t, "C_Itemcode", "C_Itemdes", "C_Forqty", "c_remarks", "C_Discount", "C_DiscAmt", "c_Rate", "C_Freeitem", "C_Freeqty", "c_uom")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub partyfindfrm()
        Try
            Dim ds_gst As New DataSet

            '  If Lineid_t = 0 Then Exit Sub
            Dim ds_credit As New DataSet

            ds_party.Clear()
            'Sqlstr = "Select P.Ptyname as Party ,P.Ptycode  Party From Party P Where P.Ptytype='CUSTOMER'  and p.lineid =" & Lineid_t & " Order By P.Ptyname "
            Sqlstr = "Select P.Ptyname as Party ,P.Ptycode,p.stateid,s.state From Party P LEFT JOIN STATE_MASTER s on s.masterid = p.stateid Where P.Ptytype='CUSTOMER' Order By P.Ptyname "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_party = New DataSet
            ds_party.Clear()
            da.Fill(ds_party)

            If ds_party.Tables(0).Rows.Count > 0 Then
                ' VisibleCols.Add("CODE")
                VisibleCols.Add("PARTY")

                '  Colheads.Add("Code")
                Colheads.Add("Party")

                fm.Frm_Width = 400
                fm.Frm_Height = 300
                fm.Frm_Left = 217
                fm.Frm_Top = 198
                fm.MainForm = New frm_Agencybill_Invoice
                fm.Active_ctlname = "txt_customer"

                '   Csize.Add(200)
                Csize.Add(350)

                If txt_customer.Text <> "" Then
                    If txt_customer.TextLength > 0 Then
                        If txt_customer.Text.IndexOf(" ") <> -1 Then
                            Code_t = txt_customer.Text.Substring(0, txt_customer.Text.IndexOf(" "))
                        End If
                    End If
                Else
                    Code_t = ""
                End If

                'tmppassstr = Code_t ' txt_customer.Text
                tmppassstr = txt_customer.Text
                fm.EXECUTE(conn, ds_party, VisibleCols, Colheads, Partyid_t, "", False, Csize, "", False, False, "", tmppassstr)
                txt_customer.Text = fm.VarNew
                Partyid_t = fm.VarNewid

                VisibleCols.Remove(1)
                Colheads.Remove(1)
                Csize.Remove(1)

                'VisibleCols.Remove(1)
                'Colheads.Remove(1)
                'Csize.Remove(1)

                Sqlstr = "Select isnull(p.creditlimit,0) as creditlimit,isnull(p.code,'') as code,ISNULL(DBO.GETLEDGERBALANCE('" & DTP_Vchdate.Value.ToString("yyyy/MM/dd") & "'," & Gencompid & "," & Partyid_t & ",1),0) AS CRDR, " _
                    & " ISNULL(DBO.GETLEDGERBALANCE('" & DTP_Vchdate.Value.ToString("yyyy/MM/dd") & "'," & Gencompid & "," & Partyid_t & ",0),0) AS OUTSTANDING,P.STATEID,S.STATE,P.Ptyname as ptyname From Party P LEFT JOIN STATE_MASTER S ON S.MASTERID = P.STATEID Where p.ptycode =" & Partyid_t & " "
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da = New SqlDataAdapter(cmd)
                ds_credit = New DataSet
                ds_credit.Clear()
                da.Fill(ds_credit)

                If ds_credit.Tables(0).Rows.Count > 0 Then
                    Txt_creditlimit.Text = ds_credit.Tables(0).Rows(0).Item("creditlimit")
                    txt_customer.Text = ds_credit.Tables(0).Rows(0).Item("ptyname").ToString
                    txt_state.Text = ds_credit.Tables(0).Rows(0).Item("STATE").ToString
                    Stateid_t = ds_credit.Tables(0).Rows(0).Item("stateid")
                    Txt_outstanding.Text = ds_credit.Tables(0).Rows(0).Item("OUTSTANDING").ToString
                    Label17.Text = ""

                    If ds_credit.Tables(0).Rows(0).Item("CRDR") = 2 Then
                        Label17.Text = "CR"
                    ElseIf ds_credit.Tables(0).Rows(0).Item("CRDR") = 1 Then
                        Label17.Text = "DR"
                    End If

                    ' Code_t = ds_credit.Tables(0).Rows(0).Item("code").ToString
                    Txt_creditlimit.TextAlign = HorizontalAlignment.Right
                    Txt_creditlimit.Text = Format(Val(Txt_creditlimit.Text), "#0.00")
                    Txt_outstanding.TextAlign = HorizontalAlignment.Right
                    Txt_outstanding.Text = Format(Val(Txt_outstanding.Text), "#0.00")
                End If

                'Dim ds As New DataSet
                'If Partyid_t = 0 Then Exit Sub

                'Sqlstr = "Select P.Ptyname Party,P.Ptycode,isnull(p.add1,'') as add1,isnull(p.add2,'') as add2,isnull(p.add3,'') as add3,isnull(p.add4,'') as add4,isnull(p.tin,'') as tin From Party P Where P.Ptytype='CUSTOMER' and p.ptycode = " & Partyid_t & " Order By P.Ptyname "
                'cmd = New SqlCommand(Sqlstr, conn)
                'cmd.CommandType = CommandType.Text
                'da = New SqlDataAdapter(cmd)
                'ds = New DataSet
                'ds.Clear()
                'da.Fill(ds)

                'If ds.Tables(0).Rows.Count > 0 Then
                '    txt_add1.Text = ds.Tables(0).Rows(0).Item("add1").ToString
                '    txt_add2.Text = ds.Tables(0).Rows(0).Item("add2").ToString
                '    Txt_add3.Text = ds.Tables(0).Rows(0).Item("add3").ToString
                '    txt_add4.Text = ds.Tables(0).Rows(0).Item("add4").ToString
                '    txt_tin.Text = ds.Tables(0).Rows(0).Item("tin").ToString
                'End If
            End If

            ds_gst.Clear()
            'Sqlstr = "Select P.Ptyname as Party ,P.Ptycode  Party From Party P Where P.Ptytype='CUSTOMER'  and p.lineid =" & Lineid_t & " Order By P.Ptyname "

            For i = 0 To GridView1.Rows.Count - 1
                Itemid_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_itemid).Value))

                Sqlstr = "SELECT ISNULL(HM.CGSTPERC,0) AS CGST,ISNULL(HM.IGSTPERC,0) AS IGST,ISNULL(HM.SGSTPERC,0) AS SGST FROM ITEM_MASTER IM LEFT JOIN HSNACCOUNTCODE_MASTER HM ON HM.HSNCODE =IM.HSNACCOUNTINGCODE WHERE IM.ITEMID = " & Itemid_t & " "
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da = New SqlDataAdapter(cmd)
                ds_gst = New DataSet
                ds_gst.Clear()
                da.Fill(ds_gst)

                If ds_gst.Tables(0).Rows.Count > 0 Then
                    If LCase(txt_state.Text) = LCase("tamilnadu") Then
                        GridView1.Rows(i).Cells(Fields1.c1_cgstperc).Value = ds_gst.Tables(0).Rows(0).Item("cgst")
                        GridView1.Rows(i).Cells(Fields1.c1_sgstperc).Value = ds_gst.Tables(0).Rows(0).Item("sgst")
                        GridView1.Rows(i).Cells(Fields1.c1_igstperc).Value = 0
                    Else
                        GridView1.Rows(i).Cells(Fields1.c1_igstperc).Value = ds_gst.Tables(0).Rows(0).Item("igst")
                        GridView1.Rows(i).Cells(Fields1.c1_sgstperc).Value = 0
                        GridView1.Rows(i).Cells(Fields1.c1_cgstperc).Value = 0
                    End If
                End If
            Next

            If LCase(txt_state.Text) = LCase("tamilnadu") Then
                GridView1.Columns(Fields1.c1_igstperc).Visible = False
                GridView1.Columns(Fields1.c1_igstamount).Visible = False
                GridView1.Columns(Fields1.c1_cgstperc).Visible = True
                GridView1.Columns(Fields1.c1_sgstperc).Visible = True
                GridView1.Columns(Fields1.c1_cgstamount).Visible = True
                GridView1.Columns(Fields1.c1_sgstamount).Visible = True
            Else
                GridView1.Columns(Fields1.c1_igstperc).Visible = True
                GridView1.Columns(Fields1.c1_cgstperc).Visible = False
                GridView1.Columns(Fields1.c1_sgstperc).Visible = False
                GridView1.Columns(Fields1.c1_igstamount).Visible = True
                GridView1.Columns(Fields1.c1_cgstamount).Visible = False
                GridView1.Columns(Fields1.c1_sgstamount).Visible = False
            End If
            ' calcnetamt()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Locationfindfrm()
        Try
            ds_location.Clear()
            'If Locationid_t = 0 Then
            '    Sqlstr = "SELECT GM.GODOWNNAME,gm.masterid FROM GODOWN_Master GM ORDER BY GM.GODOWNNAME  "
            'Else
            '    Sqlstr = "SELECT GM.GODOWNNAME,gm.masterid FROM GODOWN_Master GM  WHERE GM.MASTERID =" & Locationid_t & " ORDER BY GM.GODOWNNAME  "
            'End If
            Sqlstr = "SELECT GM.GODOWNNAME,gm.masterid FROM GODOWN_Master GM ORDER BY GM.GODOWNNAME  "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_location = New SqlDataAdapter(cmd)
            ds_location = New DataSet
            ds_location.Clear()
            da_location.Fill(ds_location)

            If ds_location.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("GODOWNNAME")
                Colheads.Add("Location")

                fm.Frm_Width = 300
                fm.Frm_Height = 300
                fm.Frm_Left = 934
                fm.Frm_Top = 130
                fm.MainForm = New frm_Agencybill_Invoice
                fm.Active_ctlname = "txt_location"
                Csize.Add(250)

                If ds_location.Tables(0).Rows.Count = 1 Then
                    txt_location.Text = ds_location.Tables(0).Rows(0).Item("GODOWNNAME").ToString
                    Locationid_t = ds_location.Tables(0).Rows(0).Item("MASTERID")
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

    Private Sub Linefindfrm()
        Try
            ds_line.Clear()
            Sqlstr = "Select line,masterid from line_master order by line "
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da_line = New SqlDataAdapter(cmd)
            ds_line = New DataSet
            ds_line.Clear()
            da_line.Fill(ds_line)

            If ds_line.Tables(0).Rows.Count > 0 Then
                VisibleCols.Add("line")
                Colheads.Add("Line")

                fm.Frm_Width = 300
                fm.Frm_Height = 300
                fm.Frm_Left = 157
                fm.Frm_Top = 143
                fm.MainForm = New frm_Agencybill_Invoice
                fm.Active_ctlname = "Txt_line"
                Csize.Add(250)

                tmppassstr = Txt_line.Text
                fm.EXECUTE(conn, ds_line, VisibleCols, Colheads, Lineid_t, "", False, Csize, "", False, False, "", tmppassstr)
                Txt_line.Text = fm.VarNew
                Lineid_t = fm.VarNewid

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
            'txt_add1.Enabled = False
            'txt_add2.Enabled = False
            'Txt_add3.Enabled = False
            'txt_add4.Enabled = False
            'txt_tin.Enabled = False

            Formload_t = True

            Call opnconn()
            Call Execute()

            Formload_t = False

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

            txt_customer.Font = font1
            'txt_add1.Font = font1
            'txt_add2.Font = font1
            'Txt_add3.Font = font1
            'txt_add4.Font = font1

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

            Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value))

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
            Itemid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_itemid).Value))

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

    Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
        Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
                                  font1, _
                                   b, e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Vchdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Txt_line.Visible = True Then Txt_line.Focus() Else txt_customer.Focus()
        End If
    End Sub

    Private Sub txt_customer_Click(sender As Object, e As EventArgs) Handles txt_customer.Click
        Call partyfindfrm()
    End Sub

    Private Sub txt_customer_GotFocus(sender As Object, e As EventArgs) Handles txt_customer.GotFocus
        txt_customer.BackColor = Color.Yellow
    End Sub

    Private Sub txt_customer_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_customer.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call partyfindfrm()
            '    txt_location.Focus()
            GridView1.Focus()
        End If
    End Sub

    Private Sub txt_customer_LostFocus(sender As Object, e As EventArgs) Handles txt_customer.LostFocus
        txt_customer.BackColor = Color.White
    End Sub

    Private Sub VatPercCalc()
        Try
            Dim VatPerc_t As Double, TmpvatPerc_t As Double, TmpVatAmt_t As Double, TotalAmt_t As Double
            Dim Tmpptyname_t As String, Tmpvatid_t As Double, tmpType_t As String
            Dim Descid_t As Double
            Dim tmpqty_t As Double, VatPerc_tt As Double

            Dim ds_vatcalc As New DataSet, ds_vat As New DataSet

            Dim j As Integer, cnt As Integer, k As Integer

            If Formload_t = True Then Exit Sub

            ds_vatcalc.Clear()
            ds_vatcalc = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "VATPERCCALC"
            da_detl = New SqlDataAdapter(cmd)
            ds_vatcalc = New DataSet
            da_detl.Fill(ds_vatcalc)
            cnt = ds_vatcalc.Tables(0).Rows.Count

            For j = 0 To cnt - 1
                If cnt > 0 Then
                    Tmpvatid_t = ds_vatcalc.Tables(0).Rows(j).Item("taxid")
                    VatPerc_t = ds_vatcalc.Tables(0).Rows(j).Item("taxperc")
                    Tmpptyname_t = ds_vatcalc.Tables(0).Rows(j).Item("ptyname")
                    tmpType_t = ds_vatcalc.Tables(0).Rows(j).Item("Type")

                    For i = 0 To GridView1.Rows.Count - 1
                        If LCase(Tmpptyname_t) = LCase("discount") Then
                            TmpvatPerc_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_Discount).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_Discount).Value))
                            TmpVatAmt_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_Discamt).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_Discamt).Value))
                            tmpqty_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_qty).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_qty).Value))

                            If VatPerc_t = 0 Then
                                TotalAmt_t = TotalAmt_t + TmpVatAmt_t
                            Else
                                TmpVatAmt_t = tmpqty_t * TmpVatAmt_t
                                TotalAmt_t = TotalAmt_t + TmpVatAmt_t
                            End If
                        Else
                            TmpvatPerc_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_vatper).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_vatper).Value))
                            TmpVatAmt_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_vatamt).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_vatamt).Value))
                            tmpqty_t = IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_qty).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_qty).Value))

                            TmpVatAmt_t = tmpqty_t * TmpVatAmt_t

                            If TmpvatPerc_t = VatPerc_t And TmpvatPerc_t <> 0 Then
                                TotalAmt_t = TotalAmt_t + TmpVatAmt_t            'IIf(IsDBNull(GridView1.Rows(i).Cells(Fields1.c1_vatcalc).Value), 0, (GridView1.Rows(i).Cells(Fields1.c1_vatcalc).Value))
                            End If
                        End If
                    Next

                    If GridView2.Rows.Count = 0 Then
                        GridView2.Rows.Add(cnt + 1)
                    ElseIf GridView2.Rows.Count = 1 Then
                        GridView2.Rows.Add(cnt)
                    End If

                    If GridView2.Rows.Count = 2 And editflag = True Then
                        GridView2.Rows.Add(1)
                    End If

                    'For l = 0 To cnt - 1
                    '    VatPerc_tt = IIf(IsDBNull(GridView2.Rows(l).Cells(fields2.c2_Perc).Value), 0, (GridView2.Rows(l).Cells(fields2.c2_Perc).Value))
                    '    Descid_t = IIf(IsDBNull(GridView2.Rows(l).Cells(fields2.c2_Descid).Value), 0, (GridView2.Rows(l).Cells(fields2.c2_Descid).Value))
                    '    If VatPerc_t = VatPerc_tt And Descid_t = Tmpvatid_t And VatPerc_t <> 0 Then
                    '        k = l
                    '        Exit For
                    '    ElseIf IIf(IsDBNull(GridView2.Rows(0).Cells(fields2.c2_Perc).Value), 0, (GridView2.Rows(0).Cells(fields2.c2_Perc).Value)) = 0 Then
                    '        k = 0
                    '        Exit For
                    '    ElseIf IIf(IsDBNull(GridView2.Rows(1).Cells(fields2.c2_Perc).Value), 0, (GridView2.Rows(1).Cells(fields2.c2_Perc).Value)) = 0 Then
                    '        k = 1
                    '        Exit For
                    '    ElseIf IIf(IsDBNull(GridView2.Rows(2).Cells(fields2.c2_Perc).Value), 0, (GridView2.Rows(2).Cells(fields2.c2_Perc).Value)) = 0 Then
                    '        k = 2
                    '        Exit For
                    '    End If
                    'Next

                    'GridView2.Rows(k).Cells(fields2.c2_Type).Value = tmpType_t
                    'GridView2.Rows(k).ReadOnly = True
                    'GridView2.Rows(k).DefaultCellStyle.BackColor = Readonlycolor_t
                    'GridView2.Rows(k).Cells(fields2.c2_Amount).Value = TotalAmt_t
                    'GridView2.Rows(k).Cells(fields2.c2_Desc).Value = Tmpptyname_t
                    'GridView2.Rows(k).Cells(fields2.c2_Perc).Value = VatPerc_t
                    'GridView2.Rows(k).Cells(fields2.c2_Descid).Value = Tmpvatid_t

                    GridView2.Rows(j).Cells(fields2.c2_Type).Value = tmpType_t
                    GridView2.Rows(j).ReadOnly = True
                    GridView2.Rows(j).DefaultCellStyle.BackColor = Readonlycolor_t
                    If LCase(tmpType_t) = LCase("Less") Then
                        GridView2.Rows(j).Cells(fields2.c2_Amount).Value = -(TotalAmt_t)
                    Else
                        GridView2.Rows(j).Cells(fields2.c2_Amount).Value = TotalAmt_t
                    End If
                    GridView2.Rows(j).Cells(fields2.c2_Desc).Value = Tmpptyname_t
                    GridView2.Rows(j).Cells(fields2.c2_Perc).Value = VatPerc_t
                    GridView2.Rows(j).Cells(fields2.c2_Descid).Value = Tmpvatid_t
                End If
                TotalAmt_t = 0
            Next

            Dim Descid_t1 As Double
            Dim Cgst As Boolean = False, sgst As Boolean = False, igst As Boolean = False
            'Dim Table(3, 1, 1) As Array
            'Dim k As Integer

            For I = 0 To GridView2.Rows.Count - 1
                Descid_t1 = IIf(IsDBNull(GridView2.Rows(I).Cells(fields2.c2_Descid).Value), 0, (GridView2.Rows(I).Cells(fields2.c2_Descid).Value))
                Select Case Descid_t1
                    Case -29  'CGST
                        'Table(0, 1, 1) = {-29, True, I}
                        Cgst = True
                        GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_cgstamount), "#######0.00")
                    Case -30  'SGST
                        'Table(1, 1, 1) = {-30, True, I}
                        sgst = True
                        GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_sgstamount), "#######0.00")
                    Case -31  'IGST
                        'Table(2, 1, 1) = {-31, True, I}
                        igst = True
                        GridView2.Rows(I).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_igstamount), "#######0.00")
                End Select
                'If GridView2.Item(fields2.c2_Type, I).Value = "" Then
                'End If
            Next

            If Cgst = False And Tot_Calc(GridView1, Fields1.c1_cgstamount) <> 0 Then
                GridView2.Rows.Add(1)
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "CGST"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_cgstamount), "#######0.00")
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -29
            End If

            If sgst = False And Tot_Calc(GridView1, Fields1.c1_sgstamount) <> 0 Then
                GridView2.Rows.Add(1)
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "SGST"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_sgstamount), "#######0.00")
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -30
            End If

            If igst = False And Tot_Calc(GridView1, Fields1.c1_igstamount) <> 0 Then
                GridView2.Rows.Add(1)
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Type).Value = "Add"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Desc).Value = "IGST"
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Amount).Value = Format(Tot_Calc(GridView1, Fields1.c1_igstamount), "#######0.00")
                GridView2.Rows(GridView2.Rows.Count - 2).Cells(fields2.c2_Descid).Value = -31
            End If

            'If GridView2.Rows.Count = 2 Then
            '    If GridView2.Rows(1).ReadOnly = True And GridView2.Rows(0).ReadOnly = True Then GridView2.Rows.Add(1)
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_Leave(sender As Object, e As EventArgs) Handles GridView1.Leave
        Panel3.Visible = False
        'LineShape1.Visible = False
        'LineShape2.Visible = False
        'LineShape3.Visible = False
        'LineShape5.Visible = False
    End Sub

    Private Sub txt_location_Click(sender As Object, e As EventArgs) Handles txt_location.Click
        Call Locationfindfrm()
    End Sub

    Private Sub txt_location_GotFocus(sender As Object, e As EventArgs) Handles txt_location.GotFocus
        txt_location.BackColor = Color.Yellow
    End Sub

    Private Sub txt_location_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_location.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Locationfindfrm()
            Txt_reference.Focus()
        End If
    End Sub

    Private Sub txt_location_LostFocus(sender As Object, e As EventArgs) Handles txt_location.LostFocus
        txt_location.BackColor = Color.White
    End Sub

    Private Sub Txt_line_Click(sender As Object, e As EventArgs) Handles Txt_line.Click
        Call Linefindfrm()
    End Sub

    Private Sub Txt_line_GotFocus(sender As Object, e As EventArgs) Handles Txt_line.GotFocus
        Txt_line.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_line_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_line.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call Linefindfrm()
            txt_customer.Focus()
        End If
    End Sub

    Private Sub Txt_line_LostFocus(sender As Object, e As EventArgs) Handles Txt_line.LostFocus
        Txt_line.BackColor = Color.White
    End Sub

    Private Sub Txt_reference_GotFocus(sender As Object, e As EventArgs) Handles Txt_reference.GotFocus
        Txt_reference.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_reference_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_reference.KeyDown
        If e.KeyCode = Keys.Enter Then
            Txt_vehicle.Focus()
        End If
    End Sub

    Private Sub Txt_reference_LostFocus(sender As Object, e As EventArgs) Handles Txt_reference.LostFocus
        Txt_reference.BackColor = Color.White
    End Sub

    Private Sub Txt_vehicle_GotFocus(sender As Object, e As EventArgs) Handles Txt_vehicle.GotFocus
        Txt_vehicle.BackColor = Color.Yellow
    End Sub

    Private Sub Txt_vehicle_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_vehicle.KeyDown
        If e.KeyCode = Keys.Enter Then
            GridView1.Focus()
        End If
    End Sub

    Private Sub Txt_vehicle_LostFocus(sender As Object, e As EventArgs) Handles Txt_vehicle.LostFocus
        Txt_vehicle.BackColor = Color.White
    End Sub

    Private Sub Btn_Saveprint_Click(sender As Object, e As EventArgs) Handles Btn_Saveprint.Click
        Try
            If Trim(txt_vchnum.Text) = "" Then
                MsgBox("Quotation No Should not be Empty.")
                txt_vchnum.Focus()
            ElseIf Partyid_t = 0 Or txt_customer.Text = "" Then
                MsgBox("Party Should not be Empty.")
                txt_customer.Focus()
            Else
                Call Saveproc(editflag)
                Me.Hide()
                If SavechhkFlg = True Then
                    Call Print_Rpt(Headerid_t)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class