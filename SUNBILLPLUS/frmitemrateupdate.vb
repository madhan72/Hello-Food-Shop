Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports SunUtilities_APP.SunModule1

Public Class frmitemrateupdate
    Dim cmd As SqlCommand
    Dim da_head As SqlDataAdapter
    Dim bs As New BindingSource
    Dim ds_head As New DataSet
    Dim Showpartyfindform As Boolean
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim Headcnt_t As Integer
    Public Itemid_t As Double

    Private Sub clearchars()
        Try
            Call ClearTextBoxes()
            txt_mrp.Text = "0.00"
            txt_costrate.Text = "0.00"
            txt_selrate.Text = "0.00"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub storechars(Optional ByVal pos As Integer = 0)
        Try
            Call clearchars()

            cmd = New SqlCommand("SELECT ISNULL(COSTPRICE,0) AS COSTRATE,ISNULL(SELPRICERETAIL,0) AS SELRATE,ISNULL(MRPRATE,0) AS MRPRATE  , " _
            & "   ISNULL(ITEMDES ,'') AS ITEMDESC ,ISNULL(ITEMTAMILDES,'') AS TAMILDESC,REMARKS FROM ITEM_MASTER WHERE ITEMID = " & Itemid_t & " ", conn)

            cmd.CommandType = CommandType.Text
            da_head = New SqlDataAdapter(cmd)
            ds_head = New DataSet
            ds_head.Clear()
            da_head.Fill(ds_head)

            dt = New DataTable 'used for find particular rows
            da_head.Fill(dt)

            Headcnt_t = ds_head.Tables(0).Rows.Count

            If Headcnt_t > 0 Then
                txt_itemdesc.Text = ds_head.Tables(0).Rows(0).Item("itemdesc").ToString
                txt_itemdesc.ReadOnly = True
                txt_tamildesc.Text = ds_head.Tables(0).Rows(0).Item("tamildesc").ToString
                txt_tamildesc.ReadOnly = True
                txt_remarks.Text = ds_head.Tables(0).Rows(0).Item("REMARKS").ToString
                txt_remarks.ReadOnly = True
                txt_costrate.Text = ds_head.Tables(0).Rows(0).Item("costrate")
                txt_selrate.Text = ds_head.Tables(0).Rows(0).Item("selrate")
                txt_mrp.Text = ds_head.Tables(0).Rows(0).Item("mrprate")
            Else
                Call clearchars()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Saveproc()
        Try
            Dim objCmd3 As New SqlCommand("UPDATE ITEM_MASTER SET COSTPRICE =" & CDbl(txt_costrate.Text) & " ,SELPRICERETAIL =" & CDbl(txt_selrate.Text) & ",MRPRATE =" & CDbl(txt_mrp.Text) & " WHERE ITEMID = " & Itemid_t & " ", conn)
            objCmd3.CommandType = CommandType.Text
            objCmd3.ExecuteNonQuery()
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

    Private Sub frmitemrateupdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call opnconn()
            Call storechars()
            txt_costrate.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Call Saveproc()
        Me.Hide()
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub txt_costrate_GotFocus(sender As Object, e As EventArgs) Handles txt_costrate.GotFocus
        txt_costrate.BackColor = Color.Yellow
    End Sub

    Private Sub txt_costrate_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_costrate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_selrate.Focus()
        End If
    End Sub

    Private Sub txt_costrate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_costrate.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_mrp_GotFocus(sender As Object, e As EventArgs) Handles txt_mrp.GotFocus
        txt_mrp.BackColor = Color.Yellow
    End Sub

    Private Sub txt_mrp_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_mrp.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmd_ok.Focus()
        End If
    End Sub

    Private Sub txt_mrp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_mrp.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_selrate_GotFocus(sender As Object, e As EventArgs) Handles txt_selrate.GotFocus
        txt_selrate.BackColor = Color.Yellow
    End Sub

    Private Sub txt_selrate_KeyDown(sender As Object, e As KeyEventArgs) Handles txt_selrate.KeyDown
        If e.KeyCode = Keys.Enter Then
            txt_mrp.Focus()
        End If
    End Sub

    Private Sub txt_selrate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_selrate.KeyPress
        If e.KeyChar = "."c Then
            e.Handled = (CType(sender, TextBox).Text.IndexOf("."c) <> -1)
        ElseIf e.KeyChar <> ControlChars.Back Then
            e.Handled = (".0123456789".IndexOf(e.KeyChar) = -1)
        End If
    End Sub

    Private Sub txt_mrp_LostFocus(sender As Object, e As EventArgs) Handles txt_mrp.LostFocus
        txt_mrp.BackColor = Color.White
        txt_mrp.Text = Format(Val(txt_mrp.Text), "#######0.00")
    End Sub

    Private Sub txt_selrate_LostFocus(sender As Object, e As EventArgs) Handles txt_selrate.LostFocus
        txt_selrate.BackColor = Color.White
        txt_selrate.Text = Format(Val(txt_selrate.Text), "#######0.00")
    End Sub

    Private Sub txt_costrate_LostFocus(sender As Object, e As EventArgs) Handles txt_costrate.LostFocus
        txt_costrate.BackColor = Color.White
        txt_costrate.Text = Format(Val(txt_costrate.Text), "#######0.00")
    End Sub
End Class