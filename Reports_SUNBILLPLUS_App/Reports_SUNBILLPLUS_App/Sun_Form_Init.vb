Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Sun_Form_Init
    Dim da As SqlDataAdapter
    Dim cmd As SqlCommand
    Dim ds As DataSet
    Dim Headerid_t As Double, Compid_t As Double
    Dim Process_t As String, Sqlstr As String, Rptname_t As String, Rpttype_t As String, Servername_t As String _
        , Papername_t As String, Printername_t As String, SystemName_t As String, SelectedArea_t As String, _
        SelectedGroupid_v As String, Selecteditemid_v As String, Selectedlocationid_v As String, Betweendays_v As String, SelectedType_v As String
    Dim Width_t As Single, Height_t As Single
    Dim Selectformula_t As String, Databasename_t As String, Partyid_t As String, Lineid_t As String, Groupid_t As String
    Dim Fromdate_t As DateTime, Todate_t As DateTime
    Dim NewRB(20) As System.Windows.Forms.CheckBox
    Dim Radiocnt As Integer, NextTop As Integer
    Dim Acctsflag_t As Boolean

    Public Sub Init(ByVal conn1 As SqlConnection, ByVal Process As String, ByVal Servername As String, ByVal Headerid As Double, _
                    ByVal Fromdate As DateTime, Todate As DateTime, ByVal Selectformula As String, ByVal Databasename As String, _
                          Optional ByVal Acctsflag As Boolean = False, Optional ByVal Compid As Double = 0, Optional ByVal lineid As String = "00", _
                           Optional ByVal Partyid As String = "00", Optional ByVal Groupid As String = "00", Optional ByVal SystemName As String = "", _
                           Optional ByVal SelectedArea_tt As String = "00", Optional ByVal Selectedgroupid_t As String = "", Optional ByVal Selecteditemid_t As String = "" _
                                  , Optional ByVal SelectedLocationid_t As String = "", Optional ByVal BetweenDays_t As String = "", _
                                  Optional ByVal Type_t As String = "")
        'init function does not work when running here, its work access from other application
        Try
            conn = conn1
            Headerid_t = Headerid
            Process_t = Process
            Servername_t = Servername
            Fromdate_t = Fromdate
            Todate_t = Todate
            Selectformula_t = Selectformula
            Databasename_t = Databasename
            Acctsflag_t = Acctsflag
            Compid_t = Compid
            Partyid_t = Partyid
            Lineid_t = lineid
            Groupid_t = Groupid
            SelectedArea_t = SelectedArea_tt
            SystemName_t = SystemName
            SelectedGroupid_v = Selectedgroupid_t
            Selecteditemid_v = Selecteditemid_t
            Selectedlocationid_v = SelectedLocationid_t
            Betweendays_v = BetweenDays_t
            SelectedType_v = Type_t
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form_Init_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'Call closeconn()
    End Sub

    Private Sub Form_Init_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Radiocnt = 0
            Call dsopen()
            Call GetRptDetails()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try
            If SystemName_t = "" Or SystemName_t Is Nothing Then

                Sqlstr = "Select Process,Rpttype,Rptname,Papername,Printername,Isnull(Directprint,0) as Directprint,Isnull(Paperwidth,0) As Paperwidth, " _
                & " Isnull(Paperheight,0) As Paperheight From Reportsetup Where Process = '" & Process_t & "'  Order By Lineid "
            Else
                Sqlstr = "Select Process,Rpttype,Rptname,Papername,Printername,Isnull(Directprint,0) as Directprint,Isnull(Paperwidth,0) As Paperwidth, " _
                & " Isnull(Paperheight,0) As Paperheight From Reportsetup Where Process = '" & Process_t & "' and sysname ='" & SystemName_t & "'  Order By Lineid "
            End If

            cmd = New SqlCommand(Sqlstr, conn)
            cmd.CommandType = CommandType.Text
            da = New SqlDataAdapter(cmd)
            ds = New DataSet
            ds.Clear()
            da.Fill(ds)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GetRptDetails()
        Try
            Dim dscnt_t As Integer

            dscnt_t = ds.Tables(0).Rows.Count
            If dscnt_t > 0 Then
                For i = 0 To dscnt_t - 1
                    Rpttype_t = ds.Tables(0).Rows(i).Item("Rpttype").ToString
                    Rptname_t = ds.Tables(0).Rows(i).Item("Rptname").ToString
                    Directprintflg_t = ds.Tables(0).Rows(i).Item("Directprint")

                    If dscnt_t > 1 Then Call MakeNewRB(Rptname_t, Rpttype_t, 300, 10, 5, i)
                Next

            Else
                Call opnconn()

                cmd = Nothing                     'Added for sysname wise print insertion into rptsetup tble  19_11_2016
                cmd = New SqlCommand
                cmd.Transaction = trans
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = "CHECK_REPORTSETUP"
                cmd.Parameters.Add("@Process", SqlDbType.VarChar).Value = Process_t
                cmd.Parameters.Add("@Sysname", SqlDbType.VarChar).Value = SystemName_t
                cmd.ExecuteNonQuery()

                Call dsopen()
                Call GetRptDetails()
                 
            End If
            If dscnt_t = 1 Then
                If Directprintflg_t = True Then
                    'NewRB(0).Checked = True
                    'Call Report_Init()
                    Report_Init_WOBtns()
                    Me.Close()
                Else
                    Call Report_Init_WOBtns()
                    Me.Close()
                End If
            Else
                NewRB(0).Checked = True
            End If
            'printflg_t = False
            'Call Report_Init()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        printflg_t = False
        Call Report_Init()
    End Sub

    Private Sub Report_Init()
        Dim tmprptname_t As String = "", tmprpttype_t As String = ""
        Dim cnt As Integer, index As Integer
        Try
            For j = 0 To Radiocnt
                If NewRB(j).Checked = True Then
                    tmprptname_t = NewRB(j).Name
                    tmprpttype_t = NewRB(j).Text

                    If tmprptname_t = "" Then
                        MsgBox("Please Any one Select.")
                        Exit Sub
                    End If

                    Cursor = Cursors.WaitCursor

                    ds.Tables(0).DefaultView.RowFilter = "Rptname = '" & tmprptname_t & "' "
                    cnt = ds.Tables(0).DefaultView.Count
                    If cnt > 0 Then
                        index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                        Papername_t = ds.Tables(0).Rows(index).Item("Papername").ToString
                        Width_t = ds.Tables(0).Rows(index).Item("PaperWidth").ToString
                        Height_t = ds.Tables(0).Rows(index).Item("PaperHeight").ToString
                        Printername_t = ds.Tables(0).Rows(index).Item("PrinterName").ToString
                    Else
                        index = -1
                    End If

                    If LCase(Rptname_t) = LCase("Company_Outstand") Or LCase(Rptname_t) = LCase("Company_Outstand2") Or LCase(Rptname_t) = LCase("Company_Outstand3") Or LCase(Rpttype_t) = LCase("outstanding receivable") _
                         Or LCase(Rpttype_t) = LCase("sales analysis") Then
                        Call Show_Reports(tmprptname_t, Servername_t, Fromdate_t, Todate_t, Selectformula_t, Databasename_t, IIf(Partyid_t = "", "00", Partyid_t), IIf(Lineid_t = "", "00", Lineid_t), _
                                          Nothing, Compid_t, Rpttype_t, SelectedArea_t, SelectedGroupid_v, Selecteditemid_v, Selectedlocationid_v, Betweendays_v, SelectedType_v)
                    Else
                        If Headerid_t <> 0 Then
                            Call Showreport(tmprptname_t, Headerid_t, Servername_t, Databasename_t, Acctsflag_t, Papername_t, Width_t, Height_t, _
                                            tmprpttype_t, Printername_t)
                        Else
                            Call Showreport_Reports(tmprptname_t, Servername_t, Fromdate_t, Todate_t, Selectformula_t, Databasename_t, Acctsflag_t, _
                                                    Compid_t, "", "", "", "", "", "", IIf(Partyid_t = "0", "", Partyid_t), "", "", "")
                        End If
                    End If

                    Cursor = Cursors.Default

                End If
            Next
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Report_Init_WOBtns()
        Dim tmprptname_t As String = "", tmprpttype_t As String = ""
        Dim cnt As Integer, index As Integer
        Try
            'For j = 0 To Radiocnt
            '    If NewRB(j).Checked = True Then
            '        tmprptname_t = NewRB(j).Name
            '        tmprpttype_t = NewRB(j).Text

            '        If tmprptname_t = "" Then
            '            MsgBox("Please Any one Select.")
            '            Exit Sub
            '        End If
            tmprptname_t = Rptname_t
            tmprpttype_t = Rpttype_t

            Cursor = Cursors.WaitCursor

            ds.Tables(0).DefaultView.RowFilter = "Rptname = '" & tmprptname_t & "' "
            cnt = ds.Tables(0).DefaultView.Count
            If cnt > 0 Then
                index = ds.Tables(0).Rows.IndexOf(ds.Tables(0).DefaultView.Item(0).Row)
                Papername_t = ds.Tables(0).Rows(index).Item("Papername").ToString
                Width_t = ds.Tables(0).Rows(index).Item("PaperWidth").ToString
                Height_t = ds.Tables(0).Rows(index).Item("PaperHeight").ToString
                Printername_t = ds.Tables(0).Rows(index).Item("PrinterName").ToString
            Else
                index = -1
            End If

            If LCase(Rptname_t) = LCase("Company_Outstand") Or LCase(Rptname_t) = LCase("Company_Outstand2") Or
                LCase(Rptname_t) = LCase("Company_Outstand3") Or LCase(Rpttype_t) = LCase("outstanding receivable") _
                   Or LCase(Rpttype_t) = LCase("sales analysis") Then
                Call Show_Reports(tmprptname_t, Servername_t, Fromdate_t, Todate_t, Selectformula_t, Databasename_t, IIf(Partyid_t = "", "00", Partyid_t), IIf(Lineid_t = "", "00", Lineid_t), _
                                 Nothing, Compid_t, Rpttype_t, SelectedArea_t, SelectedGroupid_v, Selecteditemid_v, Selectedlocationid_v, Betweendays_v, SelectedType_v)
            Else
                If Headerid_t <> 0 Then
                    Call Showreport(tmprptname_t, Headerid_t, Servername_t, Databasename_t, Acctsflag_t, Papername_t, Width_t, Height_t, Selectformula_t, _
                                    Printername_t)
                Else
                    Call Showreport_Reports(tmprptname_t, Servername_t, Fromdate_t, Todate_t, Selectformula_t, Databasename_t, Acctsflag_t, Compid_t, _
                                            Lineid_t, Partyid_t, Groupid_t, "", "", "", "", "", "", "")
                End If
            End If

            Cursor = Cursors.Default 
            '    End If
            'Next 
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function MakeNewRB(lbl As String, RbText As String, Width_t As Integer, startLeft_t As Integer, Top_t As Integer, i As Integer) As GroupBox
        Try
            Radiocnt = i

            If NewRB(i) Is Nothing Then
            Else
                Me.Panel1.Controls.Remove(NewRB(i))
                NewRB(i) = Nothing
            End If

            Dim Width_tt As Integer, Left_tt As Integer
            Left_tt = startLeft_t
            Width_tt = Width_t + 40

            'NewRB(i) = New System.Windows.Forms.RadioButton
            NewRB(i) = New System.Windows.Forms.CheckBox

            If NextTop = 0 Then
                NextTop = startLeft_t
            End If

            NewRB(i).Location = New System.Drawing.Point(Left_tt, NextTop)
            NewRB(i).Name = lbl
            NewRB(i).Size = New System.Drawing.Size(Width_tt, 20)
            NewRB(i).TabIndex = 0
            NewRB(i).Tag = i
            NewRB(i).Text = RbText
            NewRB(i).Font = New System.Drawing.Font(Me.Font, FontStyle.Bold)
            NewRB(i).ForeColor = Color.Black
            NewRB(i).BackColor = Color.DeepSkyBlue
            NewRB(i).Checked = True

            Me.Panel1.Controls.Add(NewRB(i))

            NextTop = NextTop + 20

            Button1.Location = New System.Drawing.Point(Left_tt + 40, NextTop + 20)
            Btn_Directprint.Location = New System.Drawing.Point(Left_tt + 180, NextTop + 20)
            Panel1.Height = NextTop + 50
            Me.Height = NextTop + 100

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function

    Private Sub Btn_Directprint_Click(sender As Object, e As EventArgs) Handles Btn_Directprint.Click
        printflg_t = True
        Call Report_Init()
    End Sub

End Class
