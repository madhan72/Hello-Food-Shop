Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration

Public Class Frm_CompSelect
    Dim VisibleCols As New Collection  ' for search visible coloumn details
    Dim Colheads As New Collection     ' for search coloumn heading details
    Dim Csize As New Collection
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds, ds_comp, ds_vchnum As New DataSet
    Dim dt As New DataTable
    Dim bs As New BindingSource
    Dim VchDate_t As Date
    Dim Sqlstr_t As String
    Dim Detlcnt_t As Integer, colindex_t As Integer, Rowindex_t As Integer

    Enum fields1
        c1_Compid = 0
        c1_Companyname = 1
    End Enum

    Public Sub Init()
        Try
            Call gridreadonly(GridView1, True, "c_Compname")
            Call gridvisible(GridView1, False, "c_Compid")
            Call Execute()
            Call Storechars()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Execute()
        Try
            ' If UCase(Genuname) = "UPS" Then
            '    Sqlstr_t = "Select C.Compid,C.Compname From Company C Where Isnull(C.Showcomplog,0)=1 and right(C.Compname,3) in('***') "
            ' Else
            Sqlstr_t = "Select C.Compid,C.Compname From Company C "
            ' End If

            cmd = New SqlCommand(Sqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Storechars()
        Try
            Detlcnt_t = ds.Tables(0).Rows.Count
            If Detlcnt_t > 0 Then
                GridView1.Rows.Add(Detlcnt_t)
                For i = 0 To Detlcnt_t - 1
                    GridView1.Rows(i).Cells(fields1.c1_Compid).Value = ds.Tables(0).Rows(i).Item("Compid")
                    GridView1.Rows(i).Cells(fields1.c1_Companyname).Value = ds.Tables(0).Rows(i).Item("Compname")
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Frm_CompSelect_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GridView1.AllowUserToAddRows = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        'Call Getcolunmheaderid()
        'Call Getcompanyname()
        'MDIParent1.Close() 'bcoz userlogon click after mdiform show, does not effect menu visible false,true(bcoz its run in mdiform load event),
        ''so here close form and open
        ''MsgBox(Gencompid)
        'MDIParent1.Show()

        'FOR DEMO
        'Call Demo()
        'FOR DEMO

        'FOR WRKING
        'Dim tmpsqlstr As String
        'Dim tmpcmd As New SqlCommand
        'Dim tmpds As New DataSet
        'Dim tmpda As SqlDataAdapter
        'Dim ShowImage As Integer

        'tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'SHOWIMAGE' "
        'tmpcmd = New SqlCommand(tmpsqlstr, conn)
        'tmpcmd.CommandType = CommandType.Text
        'tmpda = New SqlDataAdapter(tmpcmd)
        'tmpds = New DataSet
        'tmpds.Clear()
        'tmpda.Fill(tmpds)

        'If tmpds.Tables(0).Rows.Count > 0 Then
        '    ShowImage = tmpds.Tables(0).Rows(0).Item("Value")
        '    If ShowImage = 1 Then
        '    Else
        '        MDIParent1.BackgroundImage = Nothing
        '    End If
        'End If

        Call Loadmdiform()
        Me.Hide()
        'FOR WRKING
    End Sub

    Private Sub Demo()
        Dim TmpDateFormat As System.Globalization.CultureInfo
        TmpDateFormat = New System.Globalization.CultureInfo("en-US")
        TmpDateFormat.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture = TmpDateFormat
        Call Getvchdate()
        If Now.Date > "2016/02/23" Or VchDate_t >= "2016/02/23" Then
            'If VchDate_t >= "2016/02/12" Then
            MsgBox("UNABLE TO LOAD DATABASE.", MsgBoxStyle.Critical)
            End
        Else
            Call Loadmdiform()
            Me.Hide()
        End If
    End Sub

    Private Sub Getvchdate()
        Sqlstr_t = "SELECT CONVERT(VARCHAR(10),MAX(VCHDATE),103) AS DATE FROM DIRECTINVOICE_HEADER "
        cmd = New SqlCommand(Sqlstr_t, conn)
        cmd.CommandType = CommandType.Text
        da = New SqlDataAdapter(cmd)
        ds_vchnum = New DataSet
        ds_vchnum.Clear()
        da.Fill(ds_vchnum)

        If ds_vchnum.Tables(0).Rows.Count <> 0 Then
            VchDate_t = ds_vchnum.Tables(0).Rows(0).Item("DATE")
            'VchDate_t = Date.Now
        End If
    End Sub

    Private Sub GridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellClick
        Try
            Call Getcolunmheaderid()
            colindex_t = GridView1.CurrentCell.ColumnIndex
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Getcolunmheaderid()
        Try
            Rowindex_t = GridView1.CurrentCell.RowIndex
            Gencompid = GridView1.Item(0, Rowindex_t).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub GridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles GridView1.CellDoubleClick
        Try
            Call Getcolunmheaderid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Getcompanyname()
        Try
            Sqlstr_t = "Select C.Compid,C.Compname From Company C Where C.Compid= " & Gencompid & " "
            cmd = New SqlCommand(Sqlstr_t, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds_comp = New DataSet
            ds_comp.Clear()
            da.Fill(ds_comp)
            If ds_comp.Tables(0).Rows.Count > 0 Then
                Gencompname = ds_comp.Tables(0).Rows(0).Item("Compname").ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles GridView1.ColumnHeaderMouseClick
        Try
            If GridView1.Rows.Count > 0 Then
                GridView1.CurrentCell = GridView1(e.ColumnIndex, 0)
                Call Getcolunmheaderid()
                colindex_t = GridView1.CurrentCell.ColumnIndex
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Space Then
                'Call Demo()
                'Call Getcolunmheaderid()
                'Call Loadmdiform()
                'Me.Hide()

                'FOR DEMO
                'Call Demo()
                'FOR DEMO

                'FOR WRKING
                Call Getcolunmheaderid()
                Call Loadmdiform()
                Me.Hide()
                'FOR WRKING
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub Loadmdiform()
        Try
            Call Getcolunmheaderid()
            Call Getcompanyname()
            MDIParent1.Close() 'bcoz userlogon click after mdiform show, does not effect menu visible false,true(bcoz its run in mdiform load event),
            'so here close form and open
            'MsgBox(Gencompid)

            MDIParent1.Show()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class