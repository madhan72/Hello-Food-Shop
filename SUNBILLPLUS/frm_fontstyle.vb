Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports FindForm_App
Imports SunUtilities_APP.SunModule1
Imports System.Drawing.Text

Public Class frm_fontstyle
    Dim fonts As New InstalledFontCollection
    Dim Font_tt As String, Style_tt As String, Size_tt As Double

    Private Sub FontLoad()
        Try
            For Each one As FontFamily In fonts.Families
                Lst_font.Items.Add(one.Name)
            Next

            Dim ds_font As New DataSet
            Dim da_font As SqlDataAdapter
            Dim cnt1 As Integer
            Dim Sqlstr As String
            Dim cmd As SqlCommand

            Sqlstr = "SELECT CONVERT(NUMERIC(9,0), ISNULL(NUMERICVALUE,0)) AS SIZE,ISNULL(STRINGVALUE,'') as fONTNAME,isnull(reference,'') as style FROM SETTINGS WHERE PROCESS='FONT STYLE' "
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
                If ds_font.Tables(0).Rows(0).Item("Style").ToString = 1 Then
                    Style_tt = "bold"
                ElseIf ds_font.Tables(0).Rows(0).Item("Style").ToString = 2 Then
                    Style_tt = "italic"
                ElseIf ds_font.Tables(0).Rows(0).Item("Style").ToString = 0 Then
                    Style_tt = "regular"
                End If
            End If

            txt_font.Text = Font_m
            txt_size.Text = Size_m
            txt_style.Text = Style_tt

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frm_fontstyle_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Call closeconn()
    End Sub

    Private Sub frm_fontstyle_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call opnconn()
        Call FontLoad()
    End Sub

    Private Sub cmd_cancel_Click(sender As Object, e As EventArgs) Handles cmd_cancel.Click
        Me.Hide()
    End Sub

    Private Sub Lst_font_Click(sender As Object, e As EventArgs) Handles Lst_font.Click
        Font_tt = Lst_font.SelectedItem
    End Sub

    Private Sub Lst_font_KeyDown(sender As Object, e As KeyEventArgs) Handles Lst_font.KeyDown
        Font_tt = Lst_font.SelectedItem
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Font_tt = Lst_font.SelectedItem
        Style_tt = Lst_style.SelectedItem
        Size_tt = Lst_size.SelectedItem

        Font_m = Font_tt
        FontStyle_m = Style_tt
        Size_m = Size_tt

        If LCase(Style_tt).IndexOf(LCase("regular")) <> -1 Then
            FontStyle_m = 0
        ElseIf LCase(Style_tt).IndexOf(LCase("bold")) <> -1 Then
            FontStyle_m = 1
        ElseIf LCase(Style_tt).IndexOf(LCase("italic")) <> -1 Then
            FontStyle_m = 2
        End If

        Dim converter As System.ComponentModel.TypeConverter = _
 System.ComponentModel.TypeDescriptor.GetConverter(GetType(Font))
        Dim font1 As Font = _
 CType(converter.ConvertFromString(Font_m & " ," & Size_m), Font)
        Try
            font1 = New Font(Font_m, CType(Size_m, Single), CType(FontStyle_m, System.Drawing.FontStyle))
        Catch ex As Exception
            If ex.Message = "Font '" & font1.Name & "' does not support style 'Regular'." Then
                font1 = New Font(Font_m, CType(Size_m, Single), CType(1, System.Drawing.FontStyle))
            ElseIf ex.Message = "Font '" & font1.Name & "' does not support style 'Bold'." Then
                font1 = New Font(Font_m, CType(Size_m, Single), CType(0, System.Drawing.FontStyle))
            End If
        End Try

        Dim objCmd3 As New SqlCommand("update SETTINGS SET STRINGVALUE = '" & font1.Name & "' , numericvalue='" & font1.Size & "',reference ='" & FontStyle_m & "' where process='font style'", conn)
        objCmd3.CommandType = CommandType.Text
        objCmd3.ExecuteNonQuery()

        Me.Hide()
    End Sub

    Private Sub Lst_font_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lst_font.KeyPress
        Font_tt = Lst_font.SelectedItem
    End Sub

    Private Sub Lst_font_KeyUp(sender As Object, e As KeyEventArgs) Handles Lst_font.KeyUp
        Font_tt = Lst_font.SelectedItem
    End Sub

    Private Sub txt_font_GotFocus(sender As Object, e As EventArgs) Handles txt_font.GotFocus
        txt_font.BackColor = Color.Yellow
    End Sub

    Private Sub txt_font_LostFocus(sender As Object, e As EventArgs) Handles txt_font.LostFocus
        txt_font.BackColor = Color.White
    End Sub

    Private Sub txt_font_TextChanged(sender As Object, e As EventArgs) Handles txt_font.TextChanged
        Dim indx As Integer = Lst_font.FindString(txt_font.Text)
        If indx >= 0 Then Lst_font.SelectedIndex = indx
    End Sub

    Private Sub txt_size_GotFocus(sender As Object, e As EventArgs) Handles txt_size.GotFocus
        txt_size.BackColor = Color.Yellow
    End Sub

    Private Sub txt_size_LostFocus(sender As Object, e As EventArgs) Handles txt_size.LostFocus
        txt_size.BackColor = Color.White
    End Sub

    Private Sub txt_size_TextChanged(sender As Object, e As EventArgs) Handles txt_size.TextChanged
        Dim indx As Integer = Lst_size.FindString(txt_size.Text)
        If indx >= 0 Then Lst_size.SelectedIndex = indx
    End Sub

    Private Sub txt_style_GotFocus(sender As Object, e As EventArgs) Handles txt_style.GotFocus
        txt_style.BackColor = Color.Yellow
    End Sub

    Private Sub txt_style_LostFocus(sender As Object, e As EventArgs) Handles txt_style.LostFocus
        txt_style.BackColor = Color.White
    End Sub

    Private Sub txt_style_TextChanged(sender As Object, e As EventArgs) Handles txt_style.TextChanged
        Dim indx As Integer = Lst_style.FindString(txt_style.Text)
        If indx >= 0 Then Lst_style.SelectedIndex = indx
    End Sub

    Private Sub Lst_size_Click(sender As Object, e As EventArgs) Handles Lst_size.Click
        Size_tt = Lst_size.SelectedItem
    End Sub

    Private Sub Lst_size_KeyDown(sender As Object, e As KeyEventArgs) Handles Lst_size.KeyDown
        Size_tt = Lst_size.SelectedItem
    End Sub

    Private Sub Lst_size_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lst_size.KeyPress
        Size_tt = Lst_size.SelectedItem
    End Sub

    Private Sub Lst_size_KeyUp(sender As Object, e As KeyEventArgs) Handles Lst_size.KeyUp
        Size_tt = Lst_size.SelectedItem
    End Sub

    Private Sub Lst_style_Click(sender As Object, e As EventArgs) Handles Lst_style.Click
        Style_tt = Lst_style.SelectedItem
    End Sub

    Private Sub Lst_style_KeyDown(sender As Object, e As KeyEventArgs) Handles Lst_style.KeyDown
        Style_tt = Lst_style.SelectedItem
    End Sub

    Private Sub Lst_style_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Lst_style.KeyPress
        Style_tt = Lst_style.SelectedItem
    End Sub

    Private Sub Lst_style_KeyUp(sender As Object, e As KeyEventArgs) Handles Lst_style.KeyUp
        Style_tt = Lst_style.SelectedItem
    End Sub

End Class