Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports Reports_SUNBILLPLUS_App
Imports System.IO
Imports System.Collections
Imports System.Drawing.Text

Public Class frm_billtotal
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim Headerid_t As Double, Compid As Double
    Dim ds, ds_party, ds_location, ds1, ds_head, ds_detl, ds_item, ds_acdesc As New DataSet
    Dim da, da_detail, da_detl, da_head As SqlDataAdapter
    Dim Partyid_t As Double
    Dim Formshown_t As Boolean, Showpartyfindform As Boolean
    Dim cmd As SqlCommand
    Dim dt As New DataTable
    Dim rm As New Frm_Reports_Init
    Dim celWasEndEdit As DataGridViewCell
    Dim fm As New Sun_Findfrm
    Dim dr As SqlDataReader
    Dim Controlno_t As String, Process As String, Trntype_t As String, Sqlstr As String, Colname_t As String, SavechhkFlg As Boolean
    Dim index_t As Integer, Rowindex_t As Integer, Headcnt_t As Integer, Detlcnt_t As Integer, colindex_t As Integer
    Dim Isalreadyexistflag_t As Boolean
    Dim font1 As Font

    Enum fields1
        c1_Party = 0
        c1_Partyid = 1
        c1_no = 2
        c1_billamount = 3
        c1_rcptno = 4
        c1_rcptid = 5
        c1_rcvdamnt = 6
        c1_Discamnt = 7
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

    Private Sub calcnetamt()
        Try
            txt_totbill.Text = Format(Tot_Calc(GridView1, fields1.c1_billamount), "#######0.00")
            Txt_Totrcvdamnt.Text = Format(Tot_Calc(GridView1, fields1.c1_rcvdamnt), "#######0.00")
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

            Call Headercall(Headerid_t)

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



            Headcnt_t = ds_head.Tables(0).Rows.Count

            If Headcnt_t > 0 Then
                Call storechars(Headcnt_t)
            Else
                Call clearchars()

                Call Headercall(Headerid_t)
                Headcnt_t = ds_head.Tables(0).Rows.Count
                If Headcnt_t > 0 Then
                    editflag = True
                    Call storechars(Headcnt_t)
                Else
                    Call clearchars()
                    editflag = False
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

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            txt_totbill.Text = "0.00"
            Txt_Totrcvdamnt.Text = "0"

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

    Private Sub cmd_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub cmd_ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_ok.Click
        Try
            If Trim(txt_vchnum.Text) = "" Then
                MsgBox("BillNo Should not be Empty.")
                txt_vchnum.Focus()
            ElseIf Partyid_t = 0 Then
                MsgBox("Party Should not be Empty.")
                GridView1.Focus()
            Else
                Call Saveproc(editflag)
                Me.Hide()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Headercall(ByVal Headerid_v As Double)
        Try
            cmd = New SqlCommand("SELECT H.HEADERID,H.VCHNUM,H.VCHDATE,ISNULL(P.PTYNAME,'') AS PARTY,ISNULL(H.PARTYID,0) AS PARTYID FROM BILLTOTAL_HEADER  H LEFT JOIN PARTY P ON P.PTYCODE =H.PARTYID WHERE H.HEADERID = " & Headerid_v & " ", conn)
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

    Public Sub Deleteproc(ByVal Headerid_v As Double)
        Dim ds_tmppono As New DataSet
        Try
            If MsgBox("Are you sure ?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Delete") = MsgBoxResult.Yes Then
                Call GendelBillTot(Headerid_v)
                Exit Sub
            End If
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

    Private Sub Saveproc(ByVal editflag_t As Boolean)
        Try
            Dim Billno_t As String, BillAmnt_t As Double, RcvdAmnt_t As Double, Rcvd_No_t As String, DiscAmnt_t As Double

            If editflag = False Then
                txt_vchnum.Text = AutoNum(Process, True) 'if editflag trie begin tran start in autonum fun.
            Else
                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            End If

            SavechhkFlg = False
            Headerid_t = GensaveBillTotHead(IIf(editflag, 1, 0), Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Gencompid)

            For i = 0 To GridView1.Rows.Count - 1
                BillAmnt_t = IIf(IsDBNull(GridView1.Item(fields1.c1_billamount, i).Value), 0, GridView1.Item(fields1.c1_billamount, i).Value)
                RcvdAmnt_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rcvdamnt, i).Value), 0, GridView1.Item(fields1.c1_rcvdamnt, i).Value)
                DiscAmnt_t = IIf(IsDBNull(GridView1.Item(fields1.c1_Discamnt, i).Value), 0, GridView1.Item(fields1.c1_Discamnt, i).Value)

                Partyid_t = IIf(IsDBNull(GridView1.Item(fields1.c1_Partyid, i).Value), 0, GridView1.Item(fields1.c1_Partyid, i).Value)
                Billno_t = IIf(IsDBNull(GridView1.Item(fields1.c1_no, i).Value), "", GridView1.Item(fields1.c1_no, i).Value)
                Rcvd_No_t = IIf(IsDBNull(GridView1.Item(fields1.c1_rcptno, i).Value), "", GridView1.Item(fields1.c1_rcptno, i).Value)

                If Partyid_t <> 0 Or BillAmnt_t <> 0 Or RcvdAmnt_t <> 0 Or DiscAmnt_t <> 0 Then
                    GensaveBillTotdetl(Headerid_t, txt_vchnum.Text, DTP_Vchdate.Value, Gencompid, i + 1, BillAmnt_t, RcvdAmnt_t, IIf(Billno_t = "", "", Billno_t), Partyid_t)
                    If BillAmnt_t <> 0 Then
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.Transaction = trans
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "BILL_AC_SALE_UPD"
                        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = 0
                        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Gencompid
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = BillAmnt_t
                        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_t = 0, DBNull.Value, Partyid_t)
                        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = IIf(Billno_t = "", "", Billno_t)
                        cmd.Parameters.Add("@VCHDATE", SqlDbType.DateTime).Value = DTP_Vchdate.Value
                        cmd.ExecuteNonQuery()
                    End If

                    If RcvdAmnt_t <> 0 Then

                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.Transaction = trans
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "TOT_REC_AC_SALE_UPD"
                        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = 0
                        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Gencompid
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = RcvdAmnt_t
                        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_t = 0, DBNull.Value, Partyid_t)
                        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = IIf(Rcvd_No_t = "", "", Rcvd_No_t)
                        cmd.Parameters.Add("@VCHDATE", SqlDbType.DateTime).Value = DTP_Vchdate.Value
                        cmd.ExecuteNonQuery()

                    End If

                    If DiscAmnt_t <> 0 Then
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.Transaction = trans
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "TOT_DISC_AC_SALE_UPD"
                        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = 0
                        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Gencompid
                        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = DiscAmnt_t
                        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_t = 0, DBNull.Value, Partyid_t)
                        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = IIf(Rcvd_No_t = "", "", Rcvd_No_t)
                        cmd.Parameters.Add("@VCHDATE", SqlDbType.DateTime).Value = DTP_Vchdate.Value
                        cmd.ExecuteNonQuery()

                    End If


                    SavechhkFlg = True
                End If
            Next

            If SavechhkFlg = True Then
                trans.Commit()
            Else
                trans.Rollback()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal pos As Integer = 0)
        Try
            Dim rowid_t As Integer

            Call clearchars()

            rowid_t = ds_head.Tables(0).Rows.Count
            If rowid_t <= 0 Then Exit Sub

            txt_vchnum.Enabled = False

            rowid_t = rowid_t - 1
            Headerid_t = ds_head.Tables(0).Rows(rowid_t).Item("Headerid")
            txt_vchnum.Text = ds_head.Tables(0).Rows(rowid_t).Item("Vchnum").ToString
            Partyid_t = ds_head.Tables(0).Rows(rowid_t).Item("partyid")
            DTP_Vchdate.Value = ds_head.Tables(0).Rows(rowid_t).Item("Vchdate").ToString

            ds_detl.Clear()
            ds_detl = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_BILLDETAIL"
            cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_t
            da_detl = New SqlDataAdapter(cmd)
            ds_detl = New DataSet
            da_detl.Fill(ds_detl)

            GridView1.Columns(fields1.c1_billamount).DefaultCellStyle.Format = "#.00"
            GridView1.Columns(fields1.c1_rcvdamnt).DefaultCellStyle.Format = "#.00"
            GridView1.Columns(fields1.c1_Discamnt).DefaultCellStyle.Format = "#.00"

            Detlcnt_t = ds_detl.Tables(0).Rows.Count

            If Detlcnt_t > 0 Then
                GridView1.Rows.Add(Detlcnt_t + 1)
                For i = 0 To Detlcnt_t - 1
                    GridView1.Rows(i).Cells(fields1.c1_billamount).Value = ds_detl.Tables(0).Rows(i).Item("BILLAMOUNT")
                    GridView1.Rows(i).Cells(fields1.c1_no).Value = ds_detl.Tables(0).Rows(i).Item("billno")
                    GridView1.Rows(i).Cells(fields1.c1_rcvdamnt).Value = ds_detl.Tables(0).Rows(i).Item("RCVDAMOUNT")
                    GridView1.Rows(i).Cells(fields1.c1_Discamnt).Value = ds_detl.Tables(0).Rows(i).Item("DISCAMOUNT")
                    GridView1.Rows(i).Cells(fields1.c1_rcptno).Value = ds_detl.Tables(0).Rows(i).Item("RCPTNO")
                    GridView1.Rows(i).Cells(fields1.c1_rcptid).Value = ds_detl.Tables(0).Rows(i).Item("RCPTID")
                Next
            End If

            GridView1.DefaultCellStyle.Font = font1
            calcnetamt()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub setUserpermission()
        Try
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
            Panel1.Enabled = visflag_t
            Call gridreadonly(GridView1, Not visflag_t, "C_no", "c_billamnt", "C_rcvdamnt")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = e.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name
            Call Gridfindfom(Rowindex_t, colindex_t, GridView1)
        End If
    End Sub

    Private Sub GridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellContentClick
        Try
            GridView1.Columns(fields1.c1_billamount).ValueType = GetType(Decimal)
            GridView1.Columns(fields1.c1_rcvdamnt).ValueType = GetType(Decimal)
            GridView1.Columns(fields1.c1_billamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(fields1.c1_rcvdamnt).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(fields1.c1_Discamnt).DefaultCellStyle.Format = "#0.00"

       
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Delegate Sub SetColumnIndex(ByVal dgv As DataGridView, ByVal rowindex As Integer, ByVal columnindex As Integer)
    Delegate Sub SetColumnIndex1(ByVal rowindex As Integer, ByVal columnindex As Integer, ByVal dgv As DataGridView)

    Private Sub GridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEndEdit
        Try
            colindex_t = GridView1.CurrentCell.ColumnIndex
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colname_t = GridView1.Columns(colindex_t).Name

            Me.celWasEndEdit = GridView1(e.ColumnIndex, e.RowIndex)

            Dim method2 As New SetColumnIndex1(AddressOf Gridfindfom)
            Me.GridView1.BeginInvoke(method2, Rowindex_t, colindex_t, GridView1)


            If GridView1.Rows.Count - 1 = e.RowIndex Then
                If IsValidRow(GridView1, "c_billamnt") Or IsValidRow(GridView1, "C_rcvdamnt") Then
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

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Print_Rpt(ByVal Headerid_v As Double)
        Try
            Cursor = Cursors.WaitCursor
            rm.Init(conn, "BILL TOTAL", Servername_t, Headerid_v, Nothing, Nothing, "", Databasename_t, 0, False)
            rm.StartPosition = FormStartPosition.CenterScreen
            rm.ShowDialog()
            Cursor = Cursors.Default
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

    Private Sub GridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEnter
        Try
            GridView1.Columns(fields1.c1_billamount).ValueType = GetType(Decimal)
            GridView1.Columns(fields1.c1_rcvdamnt).ValueType = GetType(Decimal)
            GridView1.Columns(fields1.c1_Discamnt).ValueType = GetType(Decimal)

            GridView1.Columns(fields1.c1_billamount).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(fields1.c1_rcvdamnt).DefaultCellStyle.Format = "#0.00"
            GridView1.Columns(fields1.c1_Discamnt).DefaultCellStyle.Format = "#0.00"


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        Dim Val As String
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Val = IIf(IsDBNull((GridView1.Rows(e.RowIndex).Cells(fields1.c1_rcptno).Value)), "", (GridView1.Rows(e.RowIndex).Cells(fields1.c1_rcptno).Value))

            If Val Is Nothing Or Val = "" Then
                GridView1.Rows(e.RowIndex).Cells(fields1.c1_rcptid).Value = 0
            End If
            GridView1.DefaultCellStyle.Font = font1
            calcnetamt()
        End If
    End Sub

    Private Sub GridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        Dim tb As TextBox = CType(e.Control, TextBox)
        Select Case GridView1.CurrentCell.ColumnIndex
            Case fields1.c1_billamount, fields1.c1_rcvdamnt, fields1.c1_Discamnt
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
        End Select
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

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) 'this function used for cell allow only dec with two digit
        Try
            Dim Text As String = DirectCast(sender, TextBox).Text

            If Not Char.IsControl(e.KeyChar) And Not Char.IsDigit(e.KeyChar) And e.KeyChar <> "." Then
                e.Handled = True
            End If

            If Text.Contains(".") AndAlso e.KeyChar = "."c Then
                e.Handled = True
            End If

            If Char.IsDigit(e.KeyChar) Then
                If Text.IndexOf(".") <> -1 Then
                    If Text.Length >= Text.IndexOf(".") + 4 Then
                        e.Handled = True
                    End If
                End If
            End If

            If GridView1.CurrentCell.ColumnIndex = fields1.c1_no Or GridView1.CurrentCell.ColumnIndex = fields1.c1_rcptno Then
                e.Handled = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Gridfindfom(ByVal activerow As Integer, ByVal activecol As Integer, ByVal Dgv As DataGridView)
        Try
            Dim ds_rcptno As New DataSet
            Dim da_rcptno As SqlDataAdapter
            Dim cnt As Integer = 0
            Dim Val As String

            If activecol < 0 Or activerow < 0 Then Exit Sub

            If (Dgv.Rows(activerow).Cells(activecol)).ReadOnly Then
                Exit Sub
            End If

            Select Case colindex_t
                Case fields1.c1_Party
                    Val = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))

                    'If Val Is Nothing Or Val = "" Then Exit Sub

                    Sqlstr = "SELECT PTYNAME,PTYCODE FROM PARTY WHERE PTYTYPE='CUSTOMER' ORDER BY PTYNAME  "
                    cmd = New SqlCommand(Sqlstr, conn)
                    cmd.CommandType = CommandType.Text
                    da_rcptno = New SqlDataAdapter(cmd)
                    ds_rcptno = New DataSet
                    ds_rcptno.Clear()
                    da_rcptno.Fill(ds_rcptno)
                    cnt = ds_rcptno.Tables(0).Rows.Count

                    If cnt > 0 Then
                        VisibleCols.Add("PTYNAME")
                        Colheads.Add("Party")

                        fm.Frm_Width = 300
                        fm.Frm_Height = 300
                        fm.Frm_Left = 528
                        fm.Frm_Top = 244

                        fm.MainForm = New frm_billtotal
                        fm.Active_ctlname = "Gridview1"

                        Csize.Add(250)

                        If cnt = 1 Then
                            GridView1.Rows(Rowindex_t).Cells(fields1.c1_Party).Value = ds_rcptno.Tables(0).Rows(0).Item("ptyname").ToString
                            GridView1.Rows(Rowindex_t).Cells(fields1.c1_Partyid).Value = ds_rcptno.Tables(0).Rows(0).Item("ptycode")
                        Else
                            fm.Font = font1
                            tmppassstr = IIf(IsDBNull((GridView1.Rows(activerow).Cells(activecol).Value)), "", (GridView1.Rows(activerow).Cells(activecol).Value))
                            fm.VarNew = ""
                            fm.VarNewid = 0
                            fm.EXECUTE(conn, ds_rcptno, VisibleCols, Colheads, Partyid_t, "", False, Csize, "", False, False, "", tmppassstr)
                            GridView1.Rows(Rowindex_t).Cells(fields1.c1_Party).Value = fm.VarNew
                            GridView1.Rows(Rowindex_t).Cells(fields1.c1_Partyid).Value = fm.VarNewid
                            ' GridView1.Rows(Rowindex_t).DefaultCellStyle.Font = font1
                            Partyid_t = fm.VarNewid
                            Formshown_t = fm.Formshown
                        End If

                        'For i = 0 To GridView1.Rows.Count - 1
                        'GridView1.DefaultCellStyle.Font = font1
                        'Next
                    End If
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                colindex_t = GridView1.CurrentCell.ColumnIndex
                Rowindex_t = GridView1.CurrentCell.RowIndex
                Colname_t = GridView1.Columns(colindex_t).Name

                Call Gridfindfom(Rowindex_t, colindex_t, GridView1)

                e.SuppressKeyPress = True

                FindNextCell(GridView1, Rowindex_t, colindex_t + 1)  'checking from Next 
                GridView1.DefaultCellStyle.Font = font1

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
                                  font1, _
                                   b, e.RowBounds.Location.X + 15, _
                                   e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DTP_Vchdate_KeyDown(sender As Object, e As KeyEventArgs) Handles DTP_Vchdate.KeyDown
        If e.KeyValue = Keys.Enter Then
            GridView1.Focus()

        End If
    End Sub

    Private Sub frm_billtotal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Controlno_t = "Add" Then
                editflag = False
            ElseIf Controlno_t = "Edit" Then
                editflag = True
            End If

            Call opnconn()
            Call Execute()

            If Font_m Is Nothing Or Font_m = "" Or Size_m = "" Then Exit Sub

            Dim converter As System.ComponentModel.TypeConverter = _
    System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
            font1 = _
     CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
            'font1 = New Font(Font_m, CType(Size_m, Single))

            'For i = 0 To GridView1.Rows.Count - 1
            '    GridView1.Rows(i).DefaultCellStyle.Font = font1
            'Next

            GridView1.DefaultCellStyle.Font = font1
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


End Class