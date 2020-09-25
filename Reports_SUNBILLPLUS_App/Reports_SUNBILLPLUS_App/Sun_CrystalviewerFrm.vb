Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportAppServer.ClientDoc
Imports CrystalDecisions.ReportAppServer.Controllers
Imports CrystalDecisions.ReportAppServer.CommonObjectModel
Imports CrystalDecisions.ReportAppServer.CommonControls
Imports CrystalDecisions.ReportAppServer.Prompting
Imports CrystalDecisions.ReportAppServer.RASUtils
Imports System.Drawing.Printing

Public Class Sun_CrystalviewerFrm
    Dim rptDocument As New ReportDocument
    Dim Rptname_t, Server_t, Database_t, Papername_t, SelectFormula_t, PrinterName_t, Type_t, Party_t, Followedby_t, Inward_t, Dcnoid_t, Orderno_t As String
    Dim Headerid_t, Compid_t As Double
    Dim isReport_t, AcctsFlg_t As Boolean
    Dim Fromdate_t, Todate_t As DateTime

    Public Sub get_RptDetails(ByVal Rptname_v As String, ByVal headerid_v As Double, ByVal Server_v As String, ByVal Database_v As String, _
                               ByVal Papername_v As String, ByVal PrinterName_v As String, ByVal AcctsFlg_v As Boolean, ByVal isReport_v As Boolean, _
                                ByVal Fromdate_v As DateTime, ByVal Todate_v As DateTime, Optional SelectFormula_v As String = "", _
                                Optional Compid_v As Double = 0, Optional ByVal Type_v As String = "", Optional ByVal Party_v As String = "", _
                                Optional ByVal Followedby_v As String = "", Optional ByVal Inward_v As String = "", Optional ByVal Dcnoid_v As String = "", _
                                Optional ByVal Orderno_v As String = "")
        Rptname_t = Rptname_v
        Headerid_t = headerid_v
        Server_t = Server_v
        Database_t = Database_v
        Papername_t = Papername_v
        PrinterName_t = PrinterName_v
        AcctsFlg_t = AcctsFlg_v
        isReport_t = isReport_v
        Fromdate_t = Fromdate_v
        Todate_t = Todate_v
        SelectFormula_t = SelectFormula_v
        Compid_t = Compid_v
        Type_t = Type_v
        Party_t = Party_v
        Followedby_t = Followedby_v
        Inward_t = Inward_v
        Dcnoid_t = Dcnoid_v
        Orderno_t = Orderno_v
    End Sub

    Private Sub cmd_print_Click(sender As Object, e As EventArgs) Handles cmd_print.Click
        If isReport_t = False Then
            Call Showreport(Rptname_t, Headerid_t, Server_t, Database_t, AcctsFlg_t, Papername_t, , , SelectFormula_t, PrinterName_t, True)
        Else
            Call Showreport_Reports(Rptname_t, Server_t, Fromdate_t, Todate_t, SelectFormula_t, Database_t, AcctsFlg_t, Compid_t, Type_t, True)
        End If
    End Sub
End Class