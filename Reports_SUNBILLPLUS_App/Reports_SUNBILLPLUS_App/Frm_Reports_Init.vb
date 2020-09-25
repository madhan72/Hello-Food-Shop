Imports System
Imports System.Data
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
'Imports SunUtilities_APP

Public Class Frm_Reports_Init
    Dim Headerid_t As Double, Compid_t As Double
    Dim Process_t As String, Sqlstr As String, Rptname_t As String, Rpttype_t As String, Servername_t As String, SystemName_t As String
    Dim Partyid_t As String, Lineid_t As String, Groupid_t As String, SelectedArea_t As String, _
         SelectedGroupid_v As String, Selecteditemid_v As String, Selectedlocationid_v As String, Betweendays_v As String, SelectedType_v As String
    Dim Selectformula_t As String, Databasename_t As String, Papername_t As String, Width_t As String, Height_t As String, tmprpttype_t As String
    Dim Fromdate_t As DateTime, Todate_t As DateTime
    Dim Acctsflg_t As Boolean
    Dim Rm As New Sun_Form_Init

    Public Sub Init(ByVal conn1 As SqlConnection, ByVal Process As String, ByVal Servername As String, ByVal Headerid As Double, _
                    ByVal Fromdate As DateTime, Todate As DateTime, ByVal Selectformula As String, ByVal Databasename As String, _
                    Optional ByVal Compid As Double = 0, Optional ByVal Acctsflg As Boolean = False, Optional ByVal Rptname As String = "", _
                    Optional ByVal Directpreview As Boolean = False, Optional ByVal Partyid As String = "00", Optional ByVal Lineid As String = "00", _
                    Optional ByVal Groupid As String = "00", Optional ByVal SystemName As String = "", Optional ByVal selectedarea_tt As String = "00", Optional ByVal Selectedgroupid_t As String = "", Optional ByVal Selecteditemid_t As String = "" _
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
            Compid_t = Compid
            Acctsflg_t = False
            Rptname_t = Rptname
            Directpreview_t = Directpreview
            Partyid_t = Partyid
            Lineid_t = Lineid
            Groupid_t = Groupid
            SystemName_t = SystemName
            SelectedArea_t = selectedarea_tt
            SelectedGroupid_v = Selectedgroupid_t
            Selecteditemid_v = Selecteditemid_t
            Selectedlocationid_v = SelectedLocationid_t
            Betweendays_v = BetweenDays_t
            SelectedType_v = Type_t
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Cursor = Cursors.WaitCursor

            'Call InitialValues() 'comment this function when access from other application, bcoz tat time values came from main app

            '  If Rptname_t = "" Then
            Rm.ShowInTaskbar = False
            Rm.Init(conn, Process_t, Servername_t, Headerid_t, Fromdate_t, Todate_t, Selectformula_t, _
                    Databasename_t, Acctsflg_t, Compid_t, Lineid_t, Partyid_t, Groupid_t, SystemName_t, SelectedArea_t _
                    , SelectedGroupid_v, Selecteditemid_v, Selectedlocationid_v, Betweendays_v, SelectedType_v)
            Rm.StartPosition = FormStartPosition.CenterScreen
            Rm.ShowDialog()
            Me.Close()
            '  Else
            '   Call Showreport(Rptname_t, Headerid_t, Servername_t, Databasename_t, Acctsflg_t, Papername_t, Width_t, Height_t, tmprpttype_t)
            '   Me.Close()
            '   End If
            Cursor = Cursors.Default
            'Call Showreport("Accessory_Delivery", 2, "ADMIN-PC\SQLR2", "SUNGARMENTS", False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitialValues()
        Try
            Fromdate_t = "2011/01/01"
            Todate_t = "2019/12/12"
            Servername_t = "MADHAN\SQL2014"
            Process_t = "RETAIL"
            Partyid_t = " SELECT PTYCODE FROM PARTY "
            Databasename_t = "MARKETTRADE"
            Headerid_t = 283206
            '  SelectedArea_t = "'A2'"
            Partyid_t = "select ptycode from party"
            Groupid_t = "select masterid from group_master"
            SelectedGroupid_v = "select masterid from group_master"
            Selectedlocationid_v = " select masterid from godown_master "
            Betweendays_v = " > 0 "
            SelectedType_v = "3"
            'Selectformula_t = ""
            'SelectedType_t = "'CARD','CASH','CREDIT'"
            '  Partyid_t = "SELECT PTYCODE FROM PARTY"
            ' Lineid_t = "select masterid from line_master"
            ' Groupid_t = "select masterid from Group_master"
            Compid_t = 1
            SystemName_t = "MADHAN"

            Call main()
            Call opnconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class