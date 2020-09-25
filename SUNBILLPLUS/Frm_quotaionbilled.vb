Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports QUERY_APP
Imports USERS_APP
Imports Accounts_App
Imports SunUtilities_APP

Public Class Frm_quotaionbilled
    Dim VisibleCols As New Collection
    Dim Colheads As New Collection
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim Filtercolnmae_t As String
    Dim Colindex_t As Integer
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim bs As New BindingSource
    Dim Billuid_t As Double, Headerid_t As Double

    Enum Fields1
        c1_headerid = 0
        c1_quono = 1
        c1_quodate = 2
        c1_party = 3
        c1_value = 4
        c1_createdby = 5
        c1_bill = 6
        c1_billeduser = 7
    End Enum

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Try
            Dim BillType_t As String
            For I = 0 To GridView1.Rows.Count - 2
                Headerid_t = IIf(IsDBNull(GridView1.Rows(I).Cells(Fields1.c1_headerid).Value), 0, (GridView1.Rows(I).Cells(Fields1.c1_headerid).Value))
                BillType_t = IIf(CStr(GridView1.Rows(I).Cells(Fields1.c1_bill).Value) Is Nothing, "", (GridView1.Rows(I).Cells(Fields1.c1_bill).Value))

                If Headerid_t <> 0 And BillType_t <> "" Then
                    cmd = Nothing
                    cmd = New SqlCommand
                    cmd.Connection = conn
                    cmd.Transaction = trans
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.CommandText = "QUOTATION_HEADERBILL_SAVE_UPD"
                    cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_t
                    cmd.Parameters.Add("@billuid", SqlDbType.Int).Value = Genuid
                    cmd.Parameters.Add("@billtype", SqlDbType.VarChar).Value = BillType_t
                    cmd.ExecuteNonQuery()
                End If
            Next

            MsgBox("Details Updated!")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Colindex_t = GridView1.CurrentCell.ColumnIndex
            Filtercolnmae_t = GridView1.Columns(Colindex_t).Name
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        GetDetails()
    End Sub

    Private Sub GetDetails()
        Try
            Dim Rowcnt As Integer, Colcnt_t As Integer
            Dim SelectedType_t As String, valmember As String

            GridView1.Rows.Clear()

            If opt_alltype.Checked = True Then
                SelectedType_t = "'PENDING','CLOSED','BILLED','CONFIRMED' "
            Else
                For idx As Integer = 0 To Me.chklst_type.CheckedItems.Count - 1
                    Select Case LCase(chklst_type.CheckedItems(idx))
                        Case LCase("pending")
                            valmember = "pending"
                            SelectedType_t = String.Concat(SelectedType_t, "'" + valmember + "'", ",")
                        Case LCase("billed")
                            valmember = "Billed"
                            SelectedType_t = String.Concat(SelectedType_t, "'" + valmember + "'", ",")
                        Case LCase("closed")
                            valmember = "Closed"
                            SelectedType_t = String.Concat(SelectedType_t, "'" + valmember + "'", ",")
                        Case LCase("confirmed")
                            valmember = "confirmed"
                            SelectedType_t = String.Concat(SelectedType_t, "'" + valmember + "'", ",")
                    End Select
                Next
                If SelectedType_t <> "" And Not SelectedType_t Is Nothing Then SelectedType_t = SelectedType_t.Substring(0, SelectedType_t.Length - 1)
            End If

            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GET_QUOTATIONUPDATE"
            cmd.Parameters.Add("@FROMDATE", System.Data.SqlDbType.VarChar).Value = Dtp_Fromdate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@TODATE", System.Data.SqlDbType.VarChar).Value = Dtp_Todate.Value.ToString("yyyy/MM/dd")
            cmd.Parameters.Add("@COMPID", System.Data.SqlDbType.VarChar).Value = Gencompid
            cmd.Parameters.Add("@BILLTYPE", System.Data.SqlDbType.VarChar).Value = SelectedType_t
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            da.Fill(ds)
            'cmd.Parameters.Add("@billuid", System.Data.SqlDbType.Float).Value = Billuid_t
            'cmd.Parameters.Add("@billtype", System.Data.SqlDbType.Float).Value = Billuid_t
            'cmd.Parameters.Add("@headerid", System.Data.SqlDbType.Float).Value = Headerid_t
            'cmd.ExecuteNonQuery()
            Rowcnt = ds.Tables(0).Rows.Count
            Colcnt_t = ds.Tables(0).Columns.Count

            If Rowcnt = 0 Then Exit Sub

            GridView1.Rows.Add(Rowcnt)

            For i = 0 To GridView1.Rows.Count - 2
                GridView1.Rows(i).Cells(Fields1.c1_quono).Value = ds.Tables(0).Rows(i).Item("vchnum").ToString
                GridView1.Rows(i).Cells(Fields1.c1_quono).ReadOnly = True
                GridView1.Rows(i).Cells(Fields1.c1_quodate).Value = ds.Tables(0).Rows(i).Item("vchdate").ToString
                GridView1.Rows(i).Cells(Fields1.c1_quodate).ReadOnly = True
                GridView1.Rows(i).Cells(Fields1.c1_headerid).Value = ds.Tables(0).Rows(i).Item("headerid").ToString
                GridView1.Rows(i).Cells(Fields1.c1_party).Value = ds.Tables(0).Rows(i).Item("PARTY").ToString
                GridView1.Rows(i).Cells(Fields1.c1_party).ReadOnly = True
                GridView1.Rows(i).Cells(Fields1.c1_createdby).Value = ds.Tables(0).Rows(i).Item("createdby").ToString
                GridView1.Rows(i).Cells(Fields1.c1_createdby).ReadOnly = True
                GridView1.Rows(i).Cells(Fields1.c1_value).Value = ds.Tables(0).Rows(i).Item("value")
                GridView1.Rows(i).Cells(Fields1.c1_value).ReadOnly = True
                GridView1.Rows(i).Cells(Fields1.c1_value).Style.Alignment = DataGridViewContentAlignment.MiddleRight
                GridView1.Rows(i).Cells(Fields1.c1_bill).Value = ds.Tables(0).Rows(i).Item("bill").ToString
                GridView1.Rows(i).Cells(Fields1.c1_billeduser).Value = ds.Tables(0).Rows(i).Item("billuser").ToString
                GridView1.Rows(i).Cells(Fields1.c1_billeduser).ReadOnly = True
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Frm_quotaionbilled_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call opnconn()
            Dtp_Fromdate.Value = Today.AddDays(-1000)
            Dtp_Fromdate.Focus()
            opt_alltype.Checked = True
            GetDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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

    Private Sub Filterby()
        Try
            If TextBox1.TextLength > 0 And colindex_t >= 0 And Filtercolnmae_t <> "" Then
                bs.Filter = "convert([" & Filtercolnmae_t & "],'System.String') LIKE '%" & TextBox1.Text & "%'"
            Else
                bs.Filter = String.Empty
            End If
            GridView1.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub opt_alltype_CheckedChanged(sender As Object, e As EventArgs) Handles opt_alltype.CheckedChanged
        If opt_alltype.Checked = True Then
            txt_searchType.Enabled = False
            txt_searchType.Text = ""
            chklst_type.Enabled = False
        Else
            chklst_type.Enabled = True
            txt_searchType.Enabled = True
        End If
    End Sub

    Private Sub txt_searchType_GotFocus(sender As Object, e As EventArgs) Handles txt_searchType.GotFocus
        txt_searchType.BackColor = Color.Yellow
    End Sub

    Private Sub txt_searchType_LostFocus(sender As Object, e As EventArgs) Handles txt_searchType.LostFocus
        txt_searchType.BackColor = Color.White
    End Sub

    Private Sub txt_searchType_TextChanged(sender As Object, e As EventArgs) Handles txt_searchType.TextChanged
        Dim indx As Integer = chklst_type.FindString(txt_searchType.Text)
        If indx >= 0 Then chklst_type.SelectedIndex = indx
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.Yellow
    End Sub

    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs) Handles TextBox1.LostFocus
        TextBox1.BackColor = Color.White
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Filterby()
    End Sub

    Private Sub GridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellContentClick
        'Dim TYpe_t As String
        'Dim ds As New DataSet
        'Dim da As SqlDataAdapter
        'Dim cmd As New SqlCommand
        'Dim CNT As Integer

        'If e.RowIndex >= 0 And (e.ColumnIndex >= 0 Or e.ColumnIndex = Fields1.c1_bill) Then
        '    TYpe_t = IIf(CStr(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_bill).Value) Is Nothing, "", (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_bill).Value))
        '    Headerid_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_headerid).Value), 0, (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_headerid).Value))

        '    If (TYpe_t <> "" Or Not TYpe_t Is Nothing) And Headerid_t <> 0 Then
        '        cmd = Nothing
        '        cmd = New SqlCommand
        '        cmd.Connection = conn
        '        cmd.CommandType = CommandType.Text
        '        cmd.CommandText = "SELECT ISNULL(BU.UNAME,'') AS UNAME,ISNULL(BU.UID,0) AS UID FROM QUOTATION_HEADER2 H  " _
        '            & " JOIN USERS BU ON BU.UID =H.BILLEDUSERID WHERE H.BILLTYPE ='" & TYpe_t & "' AND HEADERID = " & Headerid_t & ""
        '        da = New SqlDataAdapter(cmd)
        '        ds = New DataSet
        '        da.Fill(ds)
        '        CNT = ds.Tables(0).Rows.Count
        '        If CNT > 0 Then
        '            GridView1.Rows(e.RowIndex).Cells(Fields1.c1_billeduser).Value = ds.Tables(0).Rows(0).Item("UNAME").ToString
        '        Else
        '            GridView1.Rows(e.RowIndex).Cells(Fields1.c1_billeduser).Value = Nothing
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick

        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then
            Headerid_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(0).Value), 0, (GridView1.Rows(e.RowIndex).Cells(0).Value))

            If Headerid_t <> 0 Then
                Dim frmprclst = New Frm_quotationformat2
                frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                frmprclst.ShowInTaskbar = False
                frmprclst.StartPosition = FormStartPosition.CenterScreen
                frmprclst.cmd_ok.Visible = False
                ShowFormFromReport = True
                frmprclst.ShowDialog()
                ShowFormFromReport = False
            End If

        End If
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellValueChanged
        'Dim TYpe_t As String
        'Dim ds As New DataSet
        'Dim da As SqlDataAdapter
        'Dim cmd As New SqlCommand
        'Dim CNT As Integer

        'If e.RowIndex >= 0 And (e.ColumnIndex >= 0 Or e.ColumnIndex = Fields1.c1_bill) Then
        '    TYpe_t = IIf(CStr(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_bill).Value) Is Nothing, "", (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_bill).Value))
        '    Headerid_t = IIf(IsDBNull(GridView1.Rows(e.RowIndex).Cells(Fields1.c1_headerid).Value), 0, (GridView1.Rows(e.RowIndex).Cells(Fields1.c1_headerid).Value))

        '    If (TYpe_t <> "" Or Not TYpe_t Is Nothing) And Headerid_t <> 0 Then
        '        cmd = Nothing
        '        cmd = New SqlCommand
        '        cmd.Connection = conn
        '        cmd.CommandType = CommandType.Text
        '        cmd.CommandText = "SELECT ISNULL(BU.UNAME,'') AS UNAME,ISNULL(BU.UID,0) AS UID FROM QUOTATION_HEADER2 H  " _
        '            & " JOIN USERS BU ON BU.UID =H.BILLEDUSERID WHERE H.BILLTYPE ='" & TYpe_t & "' AND HEADERID = " & Headerid_t & ""
        '        da = New SqlDataAdapter(cmd)
        '        ds = New DataSet
        '        da.Fill(ds)

        '        CNT = ds.Tables(0).Rows.Count
        '        If CNT > 0 Then
        '            GridView1.Rows(e.RowIndex).Cells(Fields1.c1_billeduser).Value = ds.Tables(0).Rows(0).Item("UNAME").ToString
        '        Else
        '            GridView1.Rows(e.RowIndex).Cells(Fields1.c1_billeduser).Value = Nothing
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub GridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
        'Dim tb As TextBox = CType(e.Control, TextBox)
        'Select Case GridView1.CurrentCell.ColumnIndex
        '    Case Fields1.c1_bill, Fields1.c1_billeduser, Fields1.c1_createdby, Fields1.c1_party, Fields1.c1_quodate, Fields1.c1_quono, Fields1.c1_value
        '        AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
        'End Select
    End Sub

      Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Tmpbalqty_t, TmpEditqty_t As Double
            Dim TYpe_t As String
            Dim Rowindex_t As Integer, Colindex_t As Integer
            Dim cnt As Integer

            If GridView1.CurrentCell Is Nothing Then Exit Sub

            Rowindex_t = GridView1.CurrentCell.RowIndex
            Colindex_t = GridView1.CurrentCell.ColumnIndex

            'If Ordervalition_t = True Then
            If GridView1.CurrentCell.ColumnIndex = Fields1.c1_bill Then
                If Not CType(sender, TextBox).Text.Trim.Length = 0 Then
                    'If CDec(CType(sender, TextBox).Text > (Tmpbalqty_t + TmpEditqty_t)) Then
                    '    MsgBox("Cannot exceeds.", MsgBoxStyle.Critical)
                    '    CType(sender, TextBox).Text = ""
                    'End If
                    TYpe_t = IIf(CStr(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_bill).Value) Is Nothing, "", (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_bill).Value))
                    Headerid_t = IIf(IsDBNull(GridView1.Rows(Rowindex_t).Cells(Fields1.c1_headerid).Value), 0, (GridView1.Rows(Rowindex_t).Cells(Fields1.c1_headerid).Value))

                    If (TYpe_t <> "" Or Not TYpe_t Is Nothing) And Headerid_t <> 0 Then
                        cmd = Nothing
                        cmd = New SqlCommand
                        cmd.Connection = conn
                        cmd.CommandType = CommandType.Text
                        cmd.CommandText = "SELECT ISNULL(BU.UNAME,'') AS UNAME,ISNULL(BU.UID,0) AS UID FROM QUOTATION_HEADER2 H  " _
                            & " JOIN USERS BU ON BU.UID =H.BILLEDUSERID WHERE H.BILLTYPE ='" & CType(sender, TextBox).Text & "' AND HEADERID = " & Headerid_t & ""
                        da = New SqlDataAdapter(cmd)
                        ds = New DataSet
                        da.Fill(ds)

                        CNT = ds.Tables(0).Rows.Count
                        If CNT > 0 Then
                            GridView1.Rows(Rowindex_t).Cells(Fields1.c1_billeduser).Value = ds.Tables(0).Rows(0).Item("UNAME").ToString
                        End If
                    End If
                End If
            End If
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Dim Rowindex As Integer

        If GridView1.CurrentCell Is Nothing Then Exit Sub

        Rowindex = GridView1.CurrentCell.RowIndex

        If e.KeyCode = Keys.Enter Then
            Headerid_t = IIf(IsDBNull(GridView1.Rows(Rowindex).Cells(0).Value), 0, (GridView1.Rows(Rowindex).Cells(0).Value))
            If Headerid_t <> 0 Then
                Dim frmprclst = New Frm_quotationformat2
                frmprclst.Init("Edit", Headerid_t, "QUOTATION", "QUO")
                ShowFormFromReport = True
                frmprclst.ShowInTaskbar = False
                frmprclst.StartPosition = FormStartPosition.CenterScreen
                frmprclst.ShowDialog()
                ShowFormFromReport = True
            End If
        End If
    End Sub

    Private Sub GridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles GridView1.KeyPress
        'Dim TYpe_t As String
        'Dim ds As New DataSet
        'Dim da As SqlDataAdapter
        'Dim cmd As New SqlCommand
        'Dim CNT As Integer
        'Dim rowindex As Integer
        'Dim colindex As Integer

        'If GridView1.CurrentCell Is Nothing Then Exit Sub
        'rowindex = GridView1.CurrentCell.RowIndex
        'colindex = GridView1.CurrentCell.ColumnIndex

        'If rowindex >= 0 And (colindex >= 0 Or colindex = Fields1.c1_bill) Then
        '    TYpe_t = IIf(CStr(GridView1.Rows(rowindex).Cells(Fields1.c1_bill).Value) Is Nothing, "", (GridView1.Rows(rowindex).Cells(Fields1.c1_bill).Value))
        '    Headerid_t = IIf(IsDBNull(GridView1.Rows(rowindex).Cells(Fields1.c1_headerid).Value), 0, (GridView1.Rows(rowindex).Cells(Fields1.c1_headerid).Value))

        '    If (TYpe_t <> "" Or Not TYpe_t Is Nothing) And Headerid_t <> 0 Then
        '        cmd = Nothing
        '        cmd = New SqlCommand
        '        cmd.Connection = conn
        '        cmd.CommandType = CommandType.Text
        '        cmd.CommandText = "SELECT ISNULL(BU.UNAME,'') AS UNAME,ISNULL(BU.UID,0) AS UID FROM QUOTATION_HEADER2 H  " _
        '            & " JOIN USERS BU ON BU.UID =H.BILLEDUSERID WHERE H.BILLTYPE ='" & TYpe_t & "' AND HEADERID = " & Headerid_t & ""
        '        da = New SqlDataAdapter(cmd)
        '        ds = New DataSet
        '        da.Fill(ds)

        '        CNT = ds.Tables(0).Rows.Count
        '        If CNT > 0 Then
        '            GridView1.Rows(rowindex).Cells(Fields1.c1_billeduser).Value = ds.Tables(0).Rows(0).Item("UNAME").ToString
        '        Else
        '            GridView1.Rows(rowindex).Cells(Fields1.c1_billeduser).Value = Nothing
        '        End If
        '    End If
        'End If
    End Sub

End Class