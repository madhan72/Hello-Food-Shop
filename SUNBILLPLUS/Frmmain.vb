Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports QUERY_APP
Imports USERS_APP
Imports Accounts_App
Imports SunUtilities_APP

Public Class Frmmain

    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd, cmd1 As SqlCommand
    Dim da, da1 As SqlDataAdapter
    Dim ds, ds1, ds_event, ds_mail, ds_vchnum, ds_test As New DataSet
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim bs, bs_event As New BindingSource
    Dim Headerid_t As Double
    Dim CurrRowindex_t As Integer

    Dim index As Integer, Chkcount As Integer
    Dim Stateid As Double, Tmpvchnum_t As String, PrintHeaderid_t As String
    Dim Sqlstr As String, Uname_t As String, Processname_t As String, Filtercolnmae_t As String, Events_t As String = ""
    Dim spname_t As String, formname_t As String, formno_t As String, spname_dup_t As String, tblname_t As String, Colname_t As String, Tomailid_t As String
    Dim Rowcnt As Integer, Colcnt_t As Integer, Rowindex_t As Integer, colindex_t As Integer, Rowcnt_DUP As Integer, Colcnt_t_DUP As Integer
    Dim formnormalwidth_t As Double = 900, grid1normalwidth_t As Double = 880, panel1normalwidth_t As Double = 1052, formnormalleft_t As Double, formnormaltop_t As Double
    Dim formextendwidth_t As Double = 300, grid1extendwidth_t As Double = 100, panel1extendwidth_t As Double = 200, formeventleft_t As Double, formeventtop_t As Double
    Dim Showflg_t As Boolean
    Dim ds_trntype As New DataSet
    Dim da_trntype As SqlDataAdapter
    Dim bs_trntype As New BindingSource

    Private Const HT_CAPTION As Integer = &H2
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Dim Acent As New Sun_FrmAcctsentry_Compwise
    Dim Rm As New Sun_FrmMail
    Dim ArrayHeaderid As String()

    Public Sub Execute(ByVal spname As String, ByVal formname As String, ByVal spnamedup As String, Optional ByVal process As String = "",
                       Optional ByVal tblname As String = "", Optional ByVal colname As String = "")
        Try
            spname_t = spname
            formname_t = formname
            spname_dup_t = spnamedup
            Processname_t = process

            If (LCase(process) = "invoice") Then process = "Cash Sales"

            Me.Text = Me.Text + "-" + process
            Panel3.Visible = False
            tblname_t = tblname
            Colname_t = colname

            If LCase(spname_t) = "get_accountsdetails" Then
                ds_trntype = Nothing
                Dim levstr As String = "   SELECT DISTINCT CASE WHEN (H.TRNTYPE) = 'SAL' THEN 'SALES'  WHEN (H.TRNTYPE)='CTN' THEN 'CREDIT NOTE' WHEN (H.TRNTYPE)='DTN' THEN 'DEBIT NOTE'         " _
                & " WHEN (H.TRNTYPE)='PUR' THEN 'PURCHASE' WHEN (H.TRNTYPE)='JRN' THEN 'JOURNAL'  ELSE  'RECEIPT' END AS REFTYPE,RTRIM(LTRIM(H.TRNTYPE)) AS TRNTYPE FROM ACHEADER H WHERE H.TRNTYPE <> 'REF'"
                cmd = Nothing
                ds_trntype = Nothing
                cmd = New SqlCommand(levstr, conn)
                cmd.Transaction = trans
                cmd.CommandType = CommandType.Text
                da_trntype = New SqlDataAdapter(cmd)
                ds_trntype = New DataSet
                ds_trntype.Clear()
                da_trntype.Fill(ds_trntype)
                bs_trntype.DataSource = ds_trntype.Tables(0)
                Me.Chklst_Trntype.DataSource = ds_trntype
                Chklst_Trntype.DataSource = bs_trntype
                Chklst_Trntype.DisplayMember = "REFTYPE"
                Chklst_Trntype.ValueMember = "TRNTYPE"
                Panel_trntype.Visible = True
            Else
                Panel_trntype.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RecordFocus()
        Try
            'following lines are used to cursor focus which record was edit,add,delete,print
            If Events_t = "Add" Then
                If CurrRowindex_t < Rowcnt - 1 Then
                    CurrRowindex_t = CurrRowindex_t + 1 'new record add focus to tat last record
                End If
            ElseIf Events_t = "Edit" Then 'no change for currrowindex
            ElseIf Events_t = "Delete" Then
                If CurrRowindex_t >= Rowcnt Then
                    CurrRowindex_t = CurrRowindex_t - 1 'delete last record focus to prev record
                End If
            ElseIf Events_t = "Print" Then
            Else
            End If

            If Rowcnt > 0 Then
                If GridView1.Rows.Count > 0 Then GridView1.Rows(CurrRowindex_t).Selected = True
                If Not GridView1.CurrentCell Is Nothing Then GridView1.CurrentCell = GridView1.Rows(CurrRowindex_t).Cells(0)
            End If
            'above lines are used to cursor focus which record was edit,add,delete
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Geteventlogs()
        Try
            If Generateeventlogs_t = True Then
                cmd = New SqlCommand
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@PROCESS", SqlDbType.VarChar).Value = Processname_t
                cmd.Parameters.Add("@HEADERID", SqlDbType.Float).Value = Headerid_t
                cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
                cmd.CommandText = "GET_EVENTLOGDETAILS"
                da = New SqlDataAdapter(cmd)
                ds_event = New DataSet
                ds_event.Clear()
                da.Fill(ds_event)

                If ds_event.Tables(0).Rows.Count > 0 Then
                    Call Eventgridbind()
                Else
                    GridView2.DataSource = Nothing
                    GridView2.Rows.Clear()
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Sub Eventgridbind()
        Try
            Rowcnt = ds_event.Tables(0).Rows.Count
            Colcnt_t = ds_event.Tables(0).Columns.Count

            GridView2.DataSource = Nothing
            GridView2.Rows.Clear()
            GridView2.AllowUserToAddRows = False
            GridView2.AutoGenerateColumns = True

            Dim tables As DataTableCollection = ds_event.Tables
            Dim view1 As New DataView(tables(0))
            bs_event.DataSource = view1
            GridView2.DataSource = view1
            GridView2.Refresh()

            GridView2.Columns(1).Visible = False

            'Call Fromresizes(True)
            '        Dim font As New Font( _
            'GridView2.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)
            '        GridView2.EnableHeadersVisualStyles = False
            '        GridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            '        GridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            '        GridView2.ColumnHeadersHeight = 25
            '        GridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '        GridView2.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            '        GridView2.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            '        GridView2.RowsDefaultCellStyle.BackColor = Color.White
            '        GridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            For i = 0 To Colcnt_t - 1
                'GridView2.Columns(i).DefaultCellStyle.Font = Font
                'GridView1.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                GridView2.AutoResizeColumn(i)
            Next

            'cmd1 = New SqlCommand
            'cmd1.Connection = conn
            'cmd1.CommandType = CommandType.StoredProcedure
            'cmd1.CommandText = spname_dup_t
            'cmd1.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = GenFromdate.ToString("yyyy/MM/dd")
            'cmd1.Parameters.Add("@Todate", SqlDbType.VarChar).Value = GenTodate.ToString("yyyy/MM/dd")
            'cmd1.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
            'da1 = New SqlDataAdapter(cmd1)
            'ds1 = New DataSet
            'ds1.Clear()
            'da1.Fill(ds1, "Table2")

            'Rowcnt_DUP = ds1.Tables(0).Rows.Count
            'Colcnt_t_DUP = ds1.Tables(0).Columns.Count

            'For j = 0 To ds1.Tables(0).Columns.Count - 1
            '    colname_t = ds1.Tables(0).Columns(j).ColumnName
            '    For k = 0 To ds.Tables(0).Columns.Count - 1
            '        colname_dup_t = ds.Tables(0).Columns(k).ColumnName
            '        If colname_t = colname_dup_t Then
            '            GridView1.Columns(k).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '            'GridView1.Columns(k).DefaultCellStyle.Format = "#.000"
            '        End If
            '    Next
            'Next


        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Fromresizes(ByVal Eventvisible_t As Boolean)
        Try
            If Generateeventlogs_t = True Then
                If Eventvisible_t = True Then
                    Me.Width = formnormalwidth_t + formextendwidth_t
                    Panel1.Width = panel1normalwidth_t + panel1extendwidth_t
                    Me.Location = New Point(100, 70)
                    Panel3.Location = New Point(GridView1.Width + 3, 1)
                    Panel3.Height = GridView1.Height + 80
                    GridView2.Height = GridView1.Height + 70
                Else
                    Me.Width = formnormalwidth_t
                    Panel1.Width = panel1normalwidth_t
                    GridView1.Width = grid1normalwidth_t
                    Me.Location = New Point(formnormalleft_t, formnormaltop_t)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Frmmain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Call closeconn()
    End Sub

    Private Sub Frmmain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Call closeconn()
    End Sub

    Private Sub Frmmain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
        Call opnconn()
        'Call test()
        Dtp_Fromdate.Value = GenFromdate
        Dtp_Todate.Value = GenTodate

        Call gridbind()
        formnormalleft_t = Me.Left
        formnormaltop_t = Me.Top

        ' Btn_Mail.Visible = False
        ' cmd_Exit.Location = New Point(408, 6)

        If Processname_t = "QUOTATION" Then
            'Btn_Mail.Visible = True
            'Btn_Mail.Location = New Point(408, 6)
            '    cmd_Exit.Location = New Point(504, 6)
        ElseIf Processname_t = "Packing List" Then
            ' Btn_Mail.Visible = True
            ' Btn_Mail.Location = New Point(408, 6)
            '   cmd_Exit.Location = New Point(504, 6)
        ElseIf Processname_t = "Invoice" Then
            '  Btn_Mail.Visible = True
            '  Btn_Mail.Location = New Point(408, 6)
            '  cmd_Exit.Location = New Point(504, 6)
        ElseIf Processname_t = "Direct Invoice" Then
            ' Btn_Mail.Visible = True
            ' Btn_Mail.Location = New Point(408, 6)
            ' cmd_Exit.Location = New Point(504, 6)
        End If

        ' Panel_trntype.Visible = False

    End Sub

    Private Sub gridbind()
        Try
            Dim da_new, da1_new As SqlDataAdapter
            Dim ds_new, ds1_new As New DataSet
            Dim cmd As SqlCommand

            Dim colname_t As String, colname_dup_t As String, Selectedtrntype_t As String, valmember As String
            'SELECT DISTINCT CASE WHEN (H.TRNTYPE) = 'SAL' THEN 'SALES'  WHEN (H.TRNTYPE)='CTN' THEN 'CREDIT NOTE' WHEN (H.TRNTYPE)='DTN' THEN 'DEBIT NOTE'         
            'WHEN (H.TRNTYPE)='PUR' THEN 'PURCHASE' WHEN (H.TRNTYPE)='JRN' THEN 'JOURNAL'  ELSE  'RECEIPT' END AS REFTYPE,H.TRNTYPE AS TRNTYPE FROM ACHEADER H
            For idx As Integer = 0 To Me.Chklst_Trntype.CheckedItems.Count - 1
                Dim drv As DataRowView = CType(Chklst_Trntype.CheckedItems(idx), DataRowView)
                Dim dr As DataRow = drv.Row
                valmember = dr(Chklst_Trntype.ValueMember).ToString
                Selectedtrntype_t = String.Concat(Selectedtrntype_t, "'" + valmember + "'", ",")
            Next
            If Selectedtrntype_t = "" Or Selectedtrntype_t Is Nothing Then Selectedtrntype_t = " SELECT DISTINCT TRNTYPE FROM ACHEADER  "

            If Selectedtrntype_t.Length > 0 Then Selectedtrntype_t = Selectedtrntype_t.Substring(0, Selectedtrntype_t.Length - 1)

            da_new = Nothing
            ds_new = Nothing
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            'cmd.CommandTimeout = 100000
            cmd.CommandType = CommandType.StoredProcedure

            If Showflg_t = False Then
                cmd.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = GenFromdate.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = GenTodate.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
                If LCase(spname_t) = "get_accountsdetails" Then cmd.Parameters.Add("@TRNTYPE", SqlDbType.VarChar).Value = Selectedtrntype_t
            ElseIf Showflg_t = True Then
                cmd.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = Dtp_Fromdate.Value.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Todate", SqlDbType.VarChar).Value = Dtp_Todate.Value.ToString("yyyy/MM/dd")
                cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
                If LCase(spname_t) = "get_accountsdetails" Then cmd.Parameters.Add("@TRNTYPE", SqlDbType.VarChar).Value = Selectedtrntype_t
            End If

            cmd.CommandText = spname_t
            da_new = New SqlDataAdapter(cmd)
            ds_new = New DataSet
            ' ds_new.Clear()
            da_new.Fill(ds_new)

            Rowcnt = ds_new.Tables(0).Rows.Count
            Colcnt_t = ds_new.Tables(0).Columns.Count

            GridView1.DataSource = Nothing
            GridView1.Rows.Clear()
            GridView1.AllowUserToAddRows = False
            GridView1.AutoGenerateColumns = True
            'GridView1.DataSource = ds
            'GridView1.DataMember = "Table1"
            'GridView1.ReadOnly = True

            'If formname_t = "frm_packinglist" Or formname_t = "frm_invoice" Then
            '    If GridView1.Columns.Count = 0 Then
            '        Dim Checkboxcolumn As New DataGridViewCheckBoxColumn
            '        GridView1.Columns.Insert(0, Checkboxcolumn)
            '    End If
            'End If

            Dim tables As DataTableCollection = ds_new.Tables
            Dim view1 As New DataView(tables(0))
            bs.DataSource = view1
            GridView1.DataSource = view1
            GridView1.Refresh()

            GridView1.Columns(1).Visible = False

            'If Rowcnt <> 0 Then
            '    Call Getcolunmheaderid()
            'End If

            Dim font As New Font( _
    GridView1.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold)

            GridView1.EnableHeadersVisualStyles = False
            GridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
            GridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.ColumnHeadersHeight = 25
            GridView1.RowHeadersWidth = 50
            'GridView1.ColumnHeadersDefaultCellStyle.Font = font
            GridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            GridView1.RowHeadersDefaultCellStyle.BackColor = Color.BurlyWood
            GridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Yellow
            GridView1.RowsDefaultCellStyle.BackColor = Color.White
            GridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke

            ' GridView1.DefaultCellStyle.Font = font
            'For i = 0 To Colcnt_t - 1
            '    GridView1.Columns(i).DefaultCellStyle.Font = font
            '    'GridView1.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            '    GridView1.AutoResizeColumn(i)
            'Next
            '  GridView1.AutoResizeColumns()
            da1_new = Nothing
            ds1_new = Nothing
            cmd1 = Nothing
            cmd1 = New SqlCommand
            cmd1.Connection = conn
            cmd1.CommandType = CommandType.StoredProcedure
            'cmd1.CommandTimeout = 100000
            cmd1.CommandText = spname_dup_t

            If Showflg_t = False Then
                cmd1.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = GenFromdate.ToString("yyyy/MM/dd")
                cmd1.Parameters.Add("@Todate", SqlDbType.VarChar).Value = GenTodate.ToString("yyyy/MM/dd")
                cmd1.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd1.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
            ElseIf Showflg_t = True Then
                cmd1.Parameters.Add("@Fromdate", SqlDbType.VarChar).Value = Dtp_Fromdate.Value.ToString("yyyy/MM/dd")
                cmd1.Parameters.Add("@Todate", SqlDbType.VarChar).Value = Dtp_Todate.Value.ToString("yyyy/MM/dd")
                cmd1.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
                cmd1.Parameters.Add("@UID", SqlDbType.Float).Value = Genuid
            End If
            da1_new = New SqlDataAdapter(cmd1)
            ds1_new = New DataSet
            ' ds1_new.Clear()
            da1_new.Fill(ds1_new)

            Rowcnt_DUP = ds1_new.Tables(0).Rows.Count
            Colcnt_t_DUP = ds1_new.Tables(0).Columns.Count

            For j = 0 To ds1_new.Tables(0).Columns.Count - 1
                colname_t = ds1_new.Tables(0).Columns(j).ColumnName
                'If spname_t = "GET_RETAILDETAILS" Then
                '    If LCase(colname_t).IndexOf(LCase("qty")) <> -1 Or LCase(colname_t).IndexOf(LCase("amount")) <> -1 Or LCase(colname_t).IndexOf(LCase("rate")) <> -1 Then
                '        GridView1.Columns(j).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '    End If
                'Else
                For k = 0 To ds_new.Tables(0).Columns.Count - 1
                    colname_dup_t = ds_new.Tables(0).Columns(k).ColumnName
                    If colname_t = colname_dup_t Then
                        GridView1.Columns(k).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        'GridView1.Columns(k).DefaultCellStyle.Format = "#.000"
                    End If
                Next
                'End If
            Next

            GridView1.ReadOnly = True

            If formname_t = "frm_packinglist" Or formname_t = "frm_invoice" Then
                For i = 1 To GridView1.Columns.Count - 1
                    GridView1.Columns(i).ReadOnly = True
                Next
                GridView1.Columns(0).ReadOnly = False
            Else
                GridView1.ReadOnly = True
            End If

            Call RecordFocus()

            If Rowcnt <> 0 Then
                Call Getcolunmheaderid()
            End If

            Call setUserpermission()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Add.Click
        Try
            If Rowcnt > 0 Then
                CurrRowindex_t = Rowcnt - 1
            Else
                CurrRowindex_t = 0
            End If

            Events_t = "Add"

            If LCase(formname_t) = "frmacctsentry" Then
                Acent.Init(conn, "ADD", 0, "ACCOUNTS-TRANSACTION", "", Genuid, Gencompid, Autoadjustflg_t)
                Acent.ShowInTaskbar = False
                Acent.StartPosition = FormStartPosition.CenterScreen
                Acent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_querysetup" Then
                'Me.Hide()
                Dim frmqset = New Frm_Querysetup_APP
                frmqset.Init(conn, "Add", 0, Gencompid)
                frmqset.ShowInTaskbar = False
                frmqset.StartPosition = FormStartPosition.CenterScreen
                frmqset.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_users" Then
                'Me.Hide()
                Dim frmusr = New Frm_Users
                frmusr.Init(conn, "Add")
                frmusr.ShowInTaskbar = False
                frmusr.StartPosition = FormStartPosition.CenterScreen
                frmusr.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_invoice" Then
                Dim frmordent = New frm_invoice
                frmordent.Init("Add", 0, "INVOICE", "INV")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_agencybill_invoice" Then
                Dim frmordent = New frm_Agencybill_Invoice
                frmordent.Init("Add", 0, "INVOICE", "INV")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If


            If LCase(formname_t) = "frm_billtotal" Then
                Dim frmordent = New frm_billtotal
                frmordent.Init("Add", 0, "BILL", "BIL")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_order" Then
                Dim frmordent = New frm_order
                frmordent.Init("Add", 0, "ORDER", "ORD")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_quotation" Then
                Dim frmordent = New frm_quotation
                frmordent.Init("Add", 0, "QUOTATION", "QUO")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = LCase("frm_quotationformat") Then
                Dim frmordent = New Frm_quotationformat2
                frmordent.Init("Add", 0, "QUOTATION", "QUO")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_purchase" Then
                Dim frmordent = New Frm_purchase
                frmordent.Init("Add", 0, "PURCHASE", "PUR")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_purchasereturn" Then
                Dim frmordent = New Frm_purchasereturn
                frmordent.Init("Add", 0, "PURCHASE RETURN", "PURRET")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_salesreturn" Then
                Dim frmordent = New frm_salesreturn
                frmordent.Init("Add", 0, "SALES RETURN", "SALRET")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_retailbill" Then
                Dim frmordent = New Frm_RetailBill
                frmordent.Init("Add", 0, "INVOICE", "INV")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

            If LCase(formname_t) = "frm_itemaddless" Then
                Dim frmordent = New Frm_ItemAddless
                frmordent.Init("Add", 0, "ITEMADDLESS", "IAL")
                frmordent.ShowInTaskbar = False
                frmordent.StartPosition = FormStartPosition.CenterScreen
                frmordent.ShowDialog()
                Call gridbind()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Edit.Click
        Call Editevent()
    End Sub

    Private Sub GridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            'If formname_t = "frm_packinglist" Or formname_t = "frm_invoice" Then
            '    Call Buttonfalse()
            'End If
            If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
                Call Getcolunmheaderid()
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Getcolunmheaderid()
        Try
            If GridView1.CurrentCell Is Nothing Then Exit Sub

            Rowindex_t = GridView1.CurrentCell.RowIndex
            Headerid_t = GridView1.Item(1, Rowindex_t).Value
            Uname_t = GridView1.Item(0, Rowindex_t).Value
            Call Geteventlogs()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Buttonfalse()
        Dim Chk As Boolean
        Chkcount = 0
        For i = 0 To GridView1.Rows.Count - 1
            Chk = GridView1.Item(0, i).Value
            If Chk = True Then
                Chkcount = Chkcount + 1
                If Chkcount > 1 Then
                    ' Btn_Mail.Enabled = False
                    cmd_Add.Enabled = False
                    cmd_Edit.Enabled = False
                    cmd_Delete.Enabled = False
                Else
                    'Btn_Mail.Enabled = True
                    cmd_Add.Enabled = True
                    cmd_Edit.Enabled = True
                    cmd_Delete.Enabled = True
                End If
                If Chkcount > 1 Then Exit Sub
            End If
        Next
    End Sub

    Private Sub cmd_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Exit.Click
        Events_t = ""
        Me.Hide()
    End Sub

    Private Sub cmd_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Delete.Click
        Try
            If GridView1.Rows.Count > 0 Then
                Events_t = "Delete"
                CurrRowindex_t = Rowindex_t

                If LCase(formname_t) = "frmacctsentry" Then
                    Acent.Init(conn, "DELETE", Headerid_t, "ACCOUNTS-TRANSACTION", "", Genuid, Gencompid, Autoadjustflg_t)
                    Acent.Deleteproc(Headerid_t)
                    Call gridbind()
                End If
                If LCase(formname_t) = "frm_querysetup" Then
                    Dim frmqset = New Frm_Querysetup_APP
                    frmqset.Deleteproc(conn, Headerid_t)
                    Call gridbind()
                End If
                If LCase(formname_t) = "frm_users" Then
                    Dim frmusr = New Frm_Users
                    frmusr.Init(conn, "Delete")
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_invoice" Then
                    Dim frmusr = New frm_invoice
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_agencybill_invoice" Then
                    Dim frmusr = New frm_Agencybill_Invoice
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_order" Then
                    Dim frmusr = New frm_order
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_billtotal" Then
                    Dim frmusr = New frm_billtotal
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_quotation" Then
                    Dim frmusr = New frm_quotation
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_quotationformat" Then
                    Dim frmusr = New Frm_quotationformat2
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_purchase" Then
                    Dim frmusr = New Frm_purchase
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_purchasereturn" Then
                    Dim frmusr = New Frm_purchasereturn
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_salesreturn" Then
                    Dim frmusr = New frm_salesreturn
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_retailbill" Then
                    Dim frmusr = New Frm_RetailBill
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_itemaddless" Then
                    Dim frmusr = New Frm_ItemAddless
                    frmusr.Deleteproc(Headerid_t)
                    Call gridbind()
                End If

            Else
                MsgBox("No Records to Delete.")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmd_Print.Click
        Try

            Cursor = Cursors.WaitCursor

            Events_t = "Print"
            If GridView1.Rows.Count > 0 Then

                If LCase(formname_t) = "frm_invoice" Then
                    Dim frmdirinv = New frm_invoice
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_agencybill_invoice" Then
                    Dim frmdirinv = New frm_Agencybill_Invoice
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_quotation" Then
                    Dim frmdirinv = New frm_quotation
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_quotationformat" Then
                    Dim frmdirinv = New Frm_quotationformat2
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_order" Then
                    Dim frmdirinv = New frm_order
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_billtotal" Then
                    Dim frmdirinv = New frm_billtotal
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_billtotal" Then
                    Dim frmdirinv = New frm_billtotal
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_purchase" Then
                    Dim FrmPurchase = New Frm_purchase
                    FrmPurchase.Init("Edit", Headerid_t, "PURCHASE", "PUR")
                    FrmPurchase.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_retailbill" Then
                    Dim frmdirinv = New Frm_RetailBill
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_itemaddless" Then
                    Dim frmdirinv = New Frm_ItemAddless
                    frmdirinv.Init("Edit", Headerid_t, "ITEMADDLESS", "IAL")
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_salesreturn" Then
                    Dim frmdirinv = New frm_salesreturn
                    frmdirinv.Init("Edit", Headerid_t, "SALES RETURN", "SALRET")
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                If LCase(formname_t) = "frm_purchasereturn" Then
                    Dim frmdirinv = New Frm_purchasereturn
                    frmdirinv.Init("Edit", Headerid_t, "PURCHASE RETURN", "PURRET")
                    frmdirinv.Print_Rpt(Headerid_t)
                    Cursor = Cursors.Default
                    Exit Sub
                End If

                '    frmprclst.Init("Edit", Headerid_t, "PURCHASE RETURN", "PURRET")

            Else
                MsgBox("No Records to Print.")
                Cursor = Cursors.Default
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Cursor = Cursors.Default
        End Try
    End Sub

    'Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    '    If m.Msg = WM_NCLBUTTONDOWN Then
    '        If m.WParam = New IntPtr(HT_CAPTION) Then
    '            Return
    '        End If
    '    End If
    '    MyBase.WndProc(m)
    'End Sub

    Private Sub Editevent()
        Try
            If GridView1.Rows.Count > 0 Then
                Events_t = "Edit"
                CurrRowindex_t = Rowindex_t 'this index is used for cursor focus to which record was edit

                If LCase(formname_t) = "frmacctsentry" Then
                    Acent.Init(conn, "EDIT", Headerid_t, "ACCOUNTS-TRANSACTION", "", Genuid, Gencompid, Autoadjustflg_t)
                    Acent.ShowInTaskbar = False
                    Acent.StartPosition = FormStartPosition.CenterScreen
                    Acent.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_querysetup" Then
                    Dim frmqset = New Frm_Querysetup_APP
                    frmqset.Init(conn, "Edit", Headerid_t)
                    frmqset.ShowInTaskbar = False
                    frmqset.StartPosition = FormStartPosition.CenterScreen
                    frmqset.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_users" Then
                    Dim frmusr = New Frm_Users
                    frmusr.Init(conn, "Edit", Headerid_t)
                    frmusr.ShowInTaskbar = False
                    frmusr.StartPosition = FormStartPosition.CenterScreen
                    frmusr.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_invoice" Then
                    Dim frmprclst = New frm_invoice
                    frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_agencybill_invoice" Then
                    Dim frmprclst = New frm_Agencybill_Invoice
                    frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_billtotal" Then
                    Dim frmprclst = New frm_billtotal
                    frmprclst.Init("Edit", Headerid_t, "BILL", "BIL")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_order" Then
                    Dim frmprclst = New frm_order
                    frmprclst.Init("Edit", Headerid_t, "ORDER", "ORD")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_quotation" Then
                    Dim frmprclst = New frm_quotation
                    frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = LCase("frm_quotationformat") Then
                    Dim frmprclst = New Frm_quotationformat2
                    frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_purchase" Then
                    Dim frmprclst = New Frm_purchase
                    frmprclst.Init("Edit", Headerid_t, "PURCHASE", "PUR")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_purchasereturn" Then
                    Dim frmprclst = New Frm_purchasereturn
                    frmprclst.Init("Edit", Headerid_t, "PURCHASE RETURN", "PURRET")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_salesreturn" Then
                    Dim frmprclst = New frm_salesreturn
                    frmprclst.Init("Edit", Headerid_t, "SALES RETURN", "SALRET")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_retailbill" Then
                    Dim frmprclst = New Frm_RetailBill
                    frmprclst.Init("Edit", Headerid_t, "INVOICE", "INV")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If

                If LCase(formname_t) = "frm_itemaddless" Then
                    Dim frmprclst = New Frm_ItemAddless
                    frmprclst.Init("Edit", Headerid_t, "ITEMADDLESS", "IAL")
                    frmprclst.ShowInTaskbar = False
                    frmprclst.StartPosition = FormStartPosition.CenterScreen
                    frmprclst.ShowDialog()
                    Call gridbind()
                End If
            Else
                MsgBox("No Records to Edit.")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Try
            Call Getcolunmheaderid()
            Call Editevent()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub GridView1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles GridView1.RowPostPaint
    '    'this.dgvUserDetails.RowPostPaint += New System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint)
    '    Using b As SolidBrush = New SolidBrush(GridView1.RowHeadersDefaultCellStyle.ForeColor)
    '        e.Graphics.DrawString(e.RowIndex + 1.ToString(System.Globalization.CultureInfo.CurrentUICulture), _
    '                               New Font(GridView1.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Bold), _
    '                               b, _
    '                               e.RowBounds.Location.X + 15, _
    '                               e.RowBounds.Location.Y + 4) '15
    '    End Using
    'End Sub

    Private Sub GridView1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellEnter
        Call Getcolunmheaderid()

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellValueChanged

    End Sub

    Private Sub GridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridView1.ColumnHeaderMouseClick
        Try
            If GridView1.Rows.Count > 0 Then
                GridView1.CurrentCell = GridView1(e.ColumnIndex, 0)
                Call Getcolunmheaderid()
                colindex_t = GridView1.CurrentCell.ColumnIndex
                Filtercolnmae_t = GridView1.Columns(colindex_t).Name
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles GridView1.CurrentCellDirtyStateChanged
        Dim Chk As Boolean
        Dim Tmpheaderid_t As String
        Rowindex_t = GridView1.CurrentCell.RowIndex
        Chk = GridView1.Item(0, Rowindex_t).Value
        If Chk = True Then
            Chkcount = Chkcount + 1
            PrintHeaderid_t = String.Concat(PrintHeaderid_t, Headerid_t, ",")
        Else
            If PrintHeaderid_t <> "" Then
                Tmpheaderid_t = String.Concat(Headerid_t, ",")
                PrintHeaderid_t = PrintHeaderid_t.Replace(Tmpheaderid_t, "")
            End If
        End If
        If formname_t = "frm_packinglist" Or formname_t = "frm_invoice" Then
            Call Buttonfalse()
        End If

    End Sub

    Private Sub GridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridView1.KeyDown
        Try
            Call Getcolunmheaderid()
            If e.KeyCode = Keys.Enter Then
                Call Editevent()
            End If
            If e.KeyCode = Keys.F5 And UCase(Genutype) = "ADMINISTRATOR" Then
                If Panel3.Visible = True Then
                    Panel3.Visible = False
                    Fromresizes(False)
                Else
                    Panel3.Visible = True
                    Fromresizes(True)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub setUserpermission()
        Try
            Dim Allowadd_t As Integer, Allowedit_t As Integer, Allowdel_t As Integer

            Sqlstr = " Select Process, ISNULL(ALLOWADD,0) As Allowadd, ISNULL(ALLOWEDIT,0) As Allowedit, " _
                   & " ISNULL(ALLOWDEL,0) As Allowdel From USERPERMISSION where PROCESS = '" & Processname_t & "' and ISNULL(uid,0)= " & Genuid & " "
            cmd = Nothing
            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            If dr.HasRows Then
                Do While dr.Read()
                    Allowadd_t = (dr.GetValue(1))
                    Allowedit_t = (dr.GetValue(2))
                    Allowdel_t = (dr.GetValue(3))
                    If Allowadd_t = 1 Then
                        cmd_Add.Enabled = True
                    Else
                        cmd_Add.Enabled = False
                    End If

                    If Allowdel_t = 1 Then
                        cmd_Delete.Enabled = True
                    Else
                        cmd_Delete.Enabled = False
                    End If
                Loop
            End If
            dr.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Filterby()
        Try
            'bs.Filter = "[Project] = '" & TextBox1.Text & "'"
            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then

                'bs.Filter = String.Format("Ptyname Like '%" & TextBox1.Text) & "%'" 'work
                'bs.Filter = String.Format(" " & Filtercolnmae_t & " Like '%" & TextBox1.Text) & "%'" 'always work , but not applied for decimal

                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & TextBox1.Text & "%'"
                'bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') = '" & TextBox1.Text & "'"

            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Filterby()
    End Sub

    Private Sub Btn_Mail_Click(sender As Object, e As EventArgs)
        Cursor = Cursors.WaitCursor
        Call GetMailids()
        Call Getvchnum()
        Rm.ShowInTaskbar = False  'CALL OUTSIDE APPLICATION(REPORTS_APP)      
        'If Processname_t <> "Purchase Order" Then
        'Rm.Init(conn, Processname_t, Tomailid_t, Servername_t, Headerid_t, Nothing, Nothing, "", Databasename_t, False, Processname_t + " " + Tmpvchnum_t)
        Rm.Init(conn, Processname_t, Tomailid_t, Servername_t, Headerid_t, Nothing, Nothing, "", Databasename_t, Gencompid, False, Processname_t + " " + Tmpvchnum_t, False)
        ' Else
        'Rm.Init(conn, "Purchase_Order", Tomailid_t, Servername_t, Headerid_t, Nothing, Nothing, "", Databasename_t, False, Processname_t + " " + Tmpvchnum_t)
        'End If
        Rm.StartPosition = FormStartPosition.CenterScreen
        Rm.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub GetMailids()
        Try
            If tblname_t <> "" And Colname_t <> "" Then
                Sqlstr = "Select P.Email From Party P Join" + "  " & tblname_t + "  " + "H On H." & Colname_t + "=P.Ptycode Where H.Headerid=" & Headerid_t
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da = New SqlDataAdapter(cmd)
                ds_mail = New DataSet
                ds_mail.Clear()
                da.Fill(ds_mail)
                If ds_mail.Tables(0).Rows.Count > 0 Then
                    Tomailid_t = ds_mail.Tables(0).Rows(0).Item("Email").ToString
                Else
                    Tomailid_t = ""
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Getvchnum()
        Try
            If tblname_t <> "" And Colname_t <> "" Then
                'If Processname_t <> "Purchase Order" Then
                Sqlstr = "Select vchnum From " & tblname_t & " where headerid='" & Headerid_t & "'"
                'Else
                'Sqlstr = "Select Pono From " & tblname_t & " where headerid='" & Headerid_t & "'"
                'End If
                cmd = New SqlCommand(Sqlstr, conn)
                cmd.CommandType = CommandType.Text
                da = New SqlDataAdapter(cmd)
                ds_vchnum = New DataSet
                ds_vchnum.Clear()
                da.Fill(ds_vchnum)
                If ds_vchnum.Tables(0).Rows.Count > 0 Then
                    'If Processname_t <> "Purchase Order" Then
                    Tmpvchnum_t = ds_vchnum.Tables(0).Rows(0).Item("Vchnum").ToString
                    'Else
                    'Tmpvchnum_t = ds_vchnum.Tables(0).Rows(0).Item("Pono").ToString
                    'End If
                Else
                    Tmpvchnum_t = ""
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Rowcnt = 0
        If Rowcnt > 0 Then
            CurrRowindex_t = Rowcnt - 1
        Else
            CurrRowindex_t = 0
        End If
        Showflg_t = True
        Call gridbind()
        Showflg_t = False
    End Sub

    Private Sub Dtp_Fromdate_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_Fromdate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dtp_Todate.Focus()
        End If
    End Sub

    Private Sub Dtp_Todate_KeyDown(sender As Object, e As KeyEventArgs) Handles Dtp_Todate.KeyDown
        If e.KeyCode = Keys.Enter Then
            Btn_Refresh.Focus()
        End If
    End Sub

    Private Sub Chklst_Item_Click(sender As Object, e As EventArgs) Handles Chklst_Trntype.Click
        Panel_trntype.Height = 124
        Chklst_Trntype.Height = 121
    End Sub

    Private Sub Chklst_Trntype_LostFocus(sender As Object, e As EventArgs) Handles Chklst_Trntype.LostFocus
        Panel_trntype.Height = 30
        Chklst_Trntype.Height = 30
    End Sub
End Class