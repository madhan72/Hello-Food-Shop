Imports System
Imports System.Data
Imports System.Xml 
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportAppServer.ClientDoc
'Imports CrystalDecisions.ReportAppServer.ReportDefModel 'New namespace
Imports CrystalDecisions.ReportAppServer 'New namespace
Imports CrystalDecisions.ReportAppServer.Controllers
Imports CrystalDecisions.ReportAppServer.ObjectFactory 'New namespace
Imports CrystalDecisions.ReportAppServer.DataSetConversion  'New namespace
Imports CrystalDecisions.Windows.Forms  'New namespace
Imports CrystalDecisions.ReportAppServer.CommonObjectModel
Imports CrystalDecisions.ReportAppServer.CommonControls
Imports CrystalDecisions.ReportAppServer.Prompting
Imports CrystalDecisions.ReportAppServer.RASUtils
Imports System.Drawing.Printing

Imports System.Net.Mail
Imports CrystalDecisions
Imports System.Collections

Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.ComponentModel

Module RptModule1

    Public conn As New SqlConnection
    Public trans As SqlTransaction

    Dim cmd As New SqlCommand
    Dim tmpds As DataSet
    Dim tmpda As SqlDataAdapter
    Dim tmpsqlstr As String
    Dim Idkey As Integer
    Public editflag As Boolean

    Dim Fromaddr_t As String, Fromuname_t As String, Frompwd_t As String, Toaddr_t As String, Cc_t As String

    Dim strReportName As String, App_Path As String, Pdffile As String, Pdf_Path As String
    Dim Servername_t As String

    Dim paramField As New ParameterField()
    Dim paramFields As New ParameterFields()
    Dim paramDiscreteValue As New ParameterDiscreteValue()
    Dim rptDocument As New ReportDocument

    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    Dim crParameterFieldDefinition As ParameterFieldDefinition
    Dim crParameterFieldLocation As ParameterFieldDefinition
    Dim crParameterValues As New ParameterValues
    Dim crParameterDiscreteValue As New ParameterDiscreteValue
    Dim crParameterDiscreteValue1 As New ParameterDiscreteValue
    Dim crParameterRangeValue As New ParameterRangeValue

    Dim crConnectionInfo As New ConnectionInfo
    Dim crtableLogoninfos As New TableLogOnInfos
    Dim crtableLogoninfo As New TableLogOnInfo
    Dim CrTables As Tables
    Dim CrTable As Table

    Dim crFormulaFieldDefinitions As FormulaFieldDefinitions
    Dim crFornulaFieldDefinition As FormulaFieldDefinition

    Public printflg_t, Directprintflg_t, Directpreview_t As Boolean
    'Dim rptClientDoc As New ReportClientDocument

    Dim Headerid_t As Double
    Dim Process_t As String

    <DllImport("winspool.drv", EntryPoint:="OpenPrinterA", ExactSpelling:=True, _
   SetLastError:=True, CallingConvention:=CallingConvention.StdCall, _
   CharSet:=CharSet.Ansi)> _
    Private Function OpenPrinter(ByVal pPrinterName As String, _
  ByRef hPrinter As IntPtr, ByRef pDefault As PRINTER_DEFAULTS) As Boolean
    End Function

    '<DllImport("winspool.drv", SetLastError:=True, CharSet:=CharSet.Ansi, _
    'ExactSpelling:=True, _
    'CallingConvention:=CallingConvention.StdCall)> _
    'Public Function OpenPrinter(ByVal pPrinterName As String, _
    'ByRef phPrinter As Int32, ByVal pDefault As Int32) As Boolean
    'End Function

    '<DllImport("winspool.drv", SetLastError:=True, _
    '    ExactSpelling:=True, _
    '    CallingConvention:=CallingConvention.StdCall)> _
    'Public Function ClosePrinter(ByVal hPrinter As Int32) As Boolean
    'End Function

    <DllImport("winspool.drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, _
 CallingConvention:=CallingConvention.StdCall)> _
    Private Function ClosePrinter(ByVal hPrinter As Int32) As Boolean
    End Function

    Private Structure PRINTER_DEFAULTS
        Dim pDatatype As String
        Dim pDevMode As Long
        Dim pDesiredAccess As Long
    End Structure

    Declare Function SetForm Lib "winspool.drv" Alias "SetFormA" ( _
                    ByVal hPrinter As Integer, ByVal pFormName As String, _
                    ByVal Level As Integer, ByRef pForm As Byte) As Integer

    Public Structure SIZEL

        Dim cx As Long

        Dim cy As Long
    End Structure

    Public Structure FORM_INFO_1

        Dim Flags As Long

        Dim pName As String

        Dim size As SIZEL

    End Structure

    'Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As IntPtr, _
    '                                                             ByVal pSrc As IntPtr, _
    '                                                             ByVal ByteLen As Long)
    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" _
    (ByVal hpvDest As IntPtr, ByVal hpvSource As IntPtr, ByVal cbCopy As Long)


    Public Sub main()
        Dim connsting As String

        Try

            connsting = System.Configuration.ConfigurationManager.AppSettings("SQLConnection").ToString

            conn = New SqlConnection(connsting)

        Catch ex As ArgumentException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As System.IO.FileNotFoundException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As NullReferenceException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As UnauthorizedAccessException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As System.IO.IOException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception

            MsgBox(Err.Description)

            End
        End Try

    End Sub
    Public Sub opnconn()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
    Public Sub closeconn()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub
    Public Sub shrep(ByVal Rptname_t As String, ByVal Headerid_t As Double, ByVal Servername As String, ByVal Databasename As String, _
                         Optional ByVal Acctsflag_t As Boolean = False, Optional ByVal Papername_t As String = "", _
                         Optional ByVal Width_t As Single = 0.0F, Optional ByVal Height_t As Single = 0.0F)
        strReportName = Rptname_t
        Servername_t = Servername
        App_Path = Application.StartupPath
        If Right(App_Path, 5) = "Debug" Then
            App_Path = Replace(App_Path, "bin\Debug", "")
        ElseIf Right(App_Path, 7) = "Release" Then
            App_Path = Replace(App_Path, "bin\Release", "")
        End If

        App_Path = App_Path & "\Reports_Imports" & "\" & strReportName & ".rpt"

        App_Path = Replace(App_Path, "\\", "\")

        If Not IO.File.Exists(App_Path) Then
            Throw (New Exception("Unable to locate report file:" & vbCrLf & App_Path))
        End If

        Dim repOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions

        With rptDocument

            repOptions = .PrintOptions

            With repOptions

                '.PaperOrientation = CrystalDecision.Shared.PaperOrientation.Portrait

                .PaperSize = GetPapersizeID("EPSON FX-2175", "10x12")

                .PrinterName = "EPSON FX-2175"

            End With

            ' Set the Report Option first before loading the report

            ' or else settings won't take effect

            .Load(App_Path, CrystalDecisions.Shared.OpenReportMethod.OpenReportByDefault)
            With crConnectionInfo
                .ServerName = Servername_t
                .DatabaseName = Databasename
                .UserID = "sa"
                .Password = "admin123"
            End With

            CrTables = rptDocument.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next
            rptDocument.SetDatabaseLogon("sa", "admin123", Servername_t, Databasename)

            Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument

            Sun_CrystalviewerFrm.ShowInTaskbar = False
            Sun_CrystalviewerFrm.ShowDialog()
            Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
            rptDocument.Close()


            '.PrintToPrinter(NumberOFCopies, Collated, StartPage, EndPage)

        End With
    End Sub

    Public Sub Showreport(ByVal Rptname_t As String, ByVal Headerid_t As Double, ByVal Servername As String, ByVal Databasename As String, _
                         Optional ByVal Acctsflag_t As Boolean = False, Optional ByVal Papername_t As String = "", _
                         Optional ByVal Width_t As Single = 0.0F, Optional ByVal Height_t As Single = 0.0F, Optional Formulafield As String = "", _
                         Optional ByVal Printername_t As String = "", Optional ByVal InnerBtn_t As Boolean = False)
        Try

            Dim repOptions As CrystalDecisions.CrystalReports.Engine.PrintOptions

            strReportName = Rptname_t
            Servername_t = Servername

            'App_Path = New System.IO.FileInfo(Application.StartupPath).DirectoryName
            'App_Path = Replace(App_Path, "bin", "")

            'MsgBox(Application.ExecutablePath)
            'MsgBox(Application.CommonAppDataPath)
            'MsgBox(Application.CommonAppDataRegistry.ToString)
            'MsgBox(Application.ExecutablePath)
            'MsgBox(Application.LocalUserAppDataPath)
            'MsgBox(Application.StartupPath)
            'MsgBox(Application.UserAppDataPath)
            'MsgBox(My.Application.Info.AssemblyName)
            'MsgBox(AppDomain.CurrentDomain.BaseDirectory)

            App_Path = Application.StartupPath
            If Right(App_Path, 5) = "Debug" Then
                App_Path = Replace(App_Path, "bin\Debug", "")
            ElseIf Right(App_Path, 7) = "Release" Then
                App_Path = Replace(App_Path, "bin\Release", "")
            End If

            'App_Path = App_Path & "\Reports" & "\" & strReportName & ".rpt"
            If Acctsflag_t = False Then
                App_Path = App_Path & "\Reports_Imports" & "\" & strReportName & ".rpt"
            Else
                App_Path = App_Path & "\Accounts_Reports" & "\" & strReportName & ".rpt"
            End If

            App_Path = Replace(App_Path, "\\", "\")

            If Not IO.File.Exists(App_Path) Then
                Throw (New Exception("Unable to locate report file:" & vbCrLf & App_Path))
            End If

            rptDocument.Load(App_Path)

            With crConnectionInfo
                .ServerName = Servername_t
                .DatabaseName = Databasename
                .UserID = "sa"
                .Password = "admin123"
            End With

            CrTables = rptDocument.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            If Servername_t <> "" Then

                rptDocument.SetDatabaseLogon("sa", "admin123", Servername_t, Databasename)
                'CrystalviewerFrm.CrystalReportViewer1.Refresh()
                'CrystalviewerFrm.CrystalReportViewer1.RefreshReport()
                Sun_CrystalviewerFrm.CrystalReportViewer1.SelectionFormula = ""

                crParameterDiscreteValue.Value = Convert.ToInt32(Headerid_t)
                crParameterFieldDefinitions = rptDocument.DataDefinition.ParameterFields
                crParameterFieldDefinition = crParameterFieldDefinitions.Item(0)
                crParameterValues = crParameterFieldDefinition.CurrentValues

                crParameterValues.Clear()
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues)

                rptDocument.DataDefinition.FormulaFields.Item("Rpttypecaption").Text = "'" + Formulafield + "'"

                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowRefreshButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowCloseButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGroupTreeButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGotoPageButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPageNavigateButtons = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowTextSearchButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPrintButton = True

                '-----------------------CHECKING PRINT CUSTOM PAPER SIZE IN CRYSTAL REPORT ----------------

                'rptDocument.PrintOptions.PaperSize = rptDocument.PrintOptions.PaperSize
                'Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument
                'rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
                'rptDocument.PrintToPrinter(1, True, 1, 1)
                'rptDocument.PrintOptions.PaperSize = CType(122, CrystalDecisions.Shared.PaperSize)

                ''rptDocument.PrintOptions.PrinterName = "EPSON FX Series 1 (136)"
                ''rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait
                ''Dim paper_size As Integer = GetPapersizeID("EPSON FX Series 1 (136)", "US Std Fanfold")
                ''rptDocument.PrintOptions.PaperSize = CType(paper_size.ToString, CrystalDecisions.Shared.PaperSize)

                'Dim myPaperSource As New System.Drawing.Printing.PaperSource()
                'rptDocument.PrintOptions.PrinterName = ""
                'Dim tmpPaperSource As New System.Drawing.Printing.PaperSource()
                'tmpPaperSource.SourceName = "Bypass Tray"
                'myPaperSource = tmpPaperSource
                'rptDocument.PrintOptions.PaperSource = PaperSource.Upper

                'Dim pDoc As New System.Drawing.Printing.PrintDocument()
                'Dim PrintLayout As New CrystalDecisions.Shared.PrintLayoutSettings()
                'Dim printerSettings As New System.Drawing.Printing.PrinterSettings()
                'Dim pSettings As New System.Drawing.Printing.PageSettings(printerSettings)
                'Dim pageSettings As New System.Drawing.Printing.PageSettings(printerSettings)

                'If pDoc.DefaultPageSettings.PaperSize.Height > pDoc.DefaultPageSettings.PaperSize.Width Then
                ' rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
                'rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait
                'Else
                'rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
                'rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Landscape
                'End If

                'Dim paper_size As Integer = GetPapersizeID("EPSON FX-2175", "A5")

                'Dim paper_source As Integer = GetPapersourceID("\\SERVER\epson fx-2175", "Sheet Feeder - Bin1")
                ' rptDocument.PrintOptions.PaperSource = CType(paper_source.ToString, CrystalDecisions.Shared.PaperSource)


                'Dim pd As System.Drawing.Printing.PrintDocument = New System.Drawing.Printing.PrintDocument()
                'Dim pg As System.Drawing.Printing.PageSettings = pd.DefaultPageSettings
                'MsgBox(pg.PaperSource.SourceName)
                'pg.PaperSource.RawKind = 271 'GetPapersourceID("\\SERVER\epson fx-2175", "Sheet Feeder - Bin1")
                'MsgBox(pg.PaperSource.SourceName)
                'rptDocument.PrintToPrinter(printerSettings, pSettings, False, PrintLayout)
                'MessageBox.Show(rptDocument.PrintOptions.PrinterName.ToString())

                'pd.DefaultPageSettings.PaperSource = pd.PrinterSettings.PaperSources.Item(2)
                'GetAvailableBinNames("\\SERVER\epson fx-2175")

                'If Left(sPrinterName, 2) = "\\" Then

                'Else
                'If UCase(Rptname_t) = "PACKINGLIST" Then
                '    AddPaperSizeToDefaultPrinter(sPrinterName, Papername_t, Width_t, Height_t) 'only use custom papersize,not an a4 ,a5
                'End If
                'End If

                'SetPaperSize(sPrinterName, Papername_t, Width_t, Height_t) 'only use custom papersize,not an a4 ,a5

                'Tray Change
                'SetTray(sPrinterName, 0) 'if u need uncomment
                'Call tttt(Papername_t, Width_t, Height_t)

                '--------------------------------------------------------------------------------------

                '-------------------- FOR CHECKING EXPORT PDF AND PRINT ------------
                ''Call Testpdf()
                ''Call test1()
                '--------------------------------------------------------------------

                'Dim i As Integer
                'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                'doctoprint.PrinterSettings.PrinterName = Printername_t '"EPSON LQ-1150 /II ESC/P 2"
                'Dim rawKind As Integer
                'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = Papername_t Then
                '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                '        Exit For
                '    End If
                'Next
                'rptDocument.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                'rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait

                'following lines are comment on 20-04-2017
                'If Directpreview_t = False Then
                '    If printflg_t = True Or Directprintflg_t = True Then
                '        Dim pd As New PrintDocument()
                '        Dim sPrinterName As String = pd.PrinterSettings.PrinterName
                '        sPrinterName = Printername_t

                '        rptDocument.PrintOptions.PrinterName = sPrinterName
                '        Dim paper_size As Integer = GetPapersizeID(sPrinterName, Papername_t)
                '        rptDocument.PrintOptions.PaperSize = CType(paper_size.ToString, CrystalDecisions.Shared.PaperSize)
                '        rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = True
                '        rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Landscape
                '        rptDocument.PrintToPrinter(1, False, 1, 1000)
                '        Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                '        rptDocument.Close()
                '    Else
                '        Call PrintPreview()
                '    End If
                'Else
                '    Call PrintPreview()
                'End If

                If InnerBtn_t = False Then

                    If printflg_t = True Or Directprintflg_t = True Then
                        Dim pd As New PrintDocument()
                        Dim sPrinterName As String = pd.PrinterSettings.PrinterName
                        sPrinterName = Printername_t

                        rptDocument.PrintOptions.PrinterName = sPrinterName
                        Dim paper_size As Integer = GetPapersizeID(sPrinterName, Papername_t)
                        rptDocument.PrintOptions.PaperSize = CType(paper_size.ToString, CrystalDecisions.Shared.PaperSize)
                        rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = False
                        rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait
                        rptDocument.PrintOptions.PaperSource = [Shared].PaperSource.Manual
                        rptDocument.PrintToPrinter(1, False, 1, 1000)

                        Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                        rptDocument.Close()
                    Else
                        PrintPreview(Rptname_t, Headerid_t, Servername_t, Databasename,
                                                            Papername_t, Printername_t, Acctsflag_t, False, Date.Now, Date.Now, "", 0)
                    End If
                Else
                    Dim pd As New PrintDocument()
                    Dim sPrinterName As String = pd.PrinterSettings.PrinterName
                    sPrinterName = Printername_t

                    rptDocument.PrintOptions.PrinterName = sPrinterName
                    Dim paper_size As Integer = GetPapersizeID(sPrinterName, Papername_t)
                    rptDocument.PrintOptions.PaperSize = CType(paper_size.ToString, CrystalDecisions.Shared.PaperSize)
                    rptDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = False
                    rptDocument.PrintOptions.PaperOrientation = PaperOrientation.Portrait
                    rptDocument.PrintOptions.PaperSource = [Shared].PaperSource.Manual
                    rptDocument.PrintToPrinter(1, True, 1, 1000)
                    'rptDocument.PrintToPrinter(1, False, 1, 1000)
                    Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                    rptDocument.Close()
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            'COMMENT IF SUCCESS 
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub PrintPreview()  'this comment on 20-04-2017
    '    Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument
    '    Sun_CrystalviewerFrm.ShowInTaskbar = False
    '    Sun_CrystalviewerFrm.ShowDialog()
    '    Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
    '    rptDocument.Close()
    'End Sub

    Private Sub PrintPreview(ByVal Rptname_v As String, ByVal Headerid_v As Double, ByVal Servername_v As String, ByVal Database_v As String, _
                            ByVal Papername_v As String, ByVal Printername_v As String, ByVal Acctsflag_v As Boolean, ByVal isReport_v As Boolean, _
                            ByVal Fromdate_v As Date, ByVal Todate_v As Date, ByVal SelctFromula_v As String, ByVal compid_v As Double)
        Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument
        Sun_CrystalviewerFrm.get_RptDetails(Rptname_v, Headerid_v, Servername_v, Database_v, _
                                                            Papername_v, Printername_v, Acctsflag_v, False, Date.Now, Date.Now, "", 0, "", _
                                                            "", "", "", "", "")
        Sun_CrystalviewerFrm.ShowInTaskbar = False
        Sun_CrystalviewerFrm.ShowDialog()
        Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
        rptDocument.Close()
    End Sub


    Private pOriginalDEVMODE As IntPtr

    Private Sub Testpdf()
        Try
            Pdf_Path = Application.StartupPath
            If Right(Pdf_Path, 5) = "Debug" Then
                Pdf_Path = Replace(App_Path, "bin\Debug", "")
            ElseIf Right(Pdf_Path, 7) = "Release" Then
                Pdf_Path = Replace(Pdf_Path, "bin\Release", "")
            End If
            Pdf_Path = Pdf_Path & "\Pdffiles" & "\" & "TEST" & ".pdf"

            If System.IO.File.Exists(Pdf_Path) Then
                System.IO.File.Delete(Pdf_Path)
            End If

            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New  _
            DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
            CrDiskFileDestinationOptions.DiskFileName = Pdf_Path
            CrExportOptions = rptDocument.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With
            rptDocument.Export()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub test1()
        'Dim MyProcess As New Process
        'MyProcess.StartInfo.CreateNoWindow = False
        'MyProcess.StartInfo.Verb = "print"
        'MyProcess.StartInfo.FileName = "E:\Pdffiles\test.pdf"
        'MyProcess.Start()
        'MyProcess.WaitForExit(10000)
        'MyProcess.CloseMainWindow()
        'MyProcess.Close()
        Dim MyProcess As New Process
        MyProcess.StartInfo.CreateNoWindow = True
        MyProcess.StartInfo.Verb = "print"
        'MyProcess.StartInfo.FileName = "E:\Pdffiles\test.pdf"
        MyProcess.StartInfo.FileName = Pdf_Path
        MyProcess.Start()
        MyProcess.WaitForExit(8000)
        MyProcess.CloseMainWindow()
        MyProcess.Kill()
    End Sub

    Public Sub tttt(Optional ByVal Papername_t As String = "", _
                         Optional ByVal Width_t As Single = 0.0F, Optional ByVal Height_t As Single = 0.0F)
        Const DM_OUT_BUFFER As Integer = 2
        Dim printerName As String = "EPSON FX-2175"
        'Dim pd As New PrintDocument()
        'Dim PrinterName As String = pd.PrinterSettings.PrinterName

        Dim hPrinter As IntPtr

        Dim FI1 As FORM_INFO_1

        Dim aFI1() As Byte

        Dim RetVal As Integer

        Dim ptrFI As IntPtr

        Try

            OpenPrinter(printerName, hPrinter, Nothing)

            With FI1
                .Flags = 0

                .pName = "10x8"

                .size.cx = 100

                .size.cy = 80

            End With

            ReDim aFI1(Len(FI1))

            ptrFI = Marshal.AllocHGlobal(Marshal.SizeOf(FI1))

            'Call CopyMemory(aFI1(0), ptrFI, Len(FI1))

            'Dim Needed As Integer

            'Dim pFullDevMode As IntPtr = Marshal.AllocHGlobal(Needed) 'buffer for DEVMODE structure
            'DocumentProperties(Form1.Handle, hPrinter, printerName, pFullDevMode, Nothing, DM_OUT_BUFFER)
            'pOriginalDEVMODE = Marshal.AllocHGlobal(Needed)
            'CopyMemory(pOriginalDEVMODE, pFullDevMode, Needed)
            RetVal = SetForm(hPrinter, printerName, 2, aFI1(0))

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally

            ClosePrinter(hPrinter)

        End Try
    End Sub


    Public Function GetPapersizeID(ByVal PrinterName As String, ByVal PaperSizeName As String) As Integer

        Dim doctoprint As New System.Drawing.Printing.PrintDocument()

        Dim PaperSizeID As Integer = 0

        Dim ppname As String = ""

        Dim s As String = ""

        doctoprint.PrinterSettings.PrinterName = PrinterName '(ex."EpsonSQ-1170ESC/P2")

        For i As Integer = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1

            Dim rawKind As Integer

            ppname = PaperSizeName

            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = ppname Then

                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))

                PaperSizeID = rawKind
                
                Exit For

            End If

        Next

        Return PaperSizeID

    End Function

    Public Function GetPapersourceID(ByVal PrinterName As String, ByVal PaperSourceName As String) As Integer

        Dim doctoprint As New System.Drawing.Printing.PrintDocument()

        Dim PaperSizeID As Integer = 0

        Dim ppname As String = ""

        Dim s As String = ""

        doctoprint.PrinterSettings.PrinterName = PrinterName '(ex."EpsonSQ-1170ESC/P2")

        For i As Integer = 0 To doctoprint.PrinterSettings.PaperSources.Count - 1

            Dim rawKind As Integer

            ppname = PaperSourceName

            If doctoprint.PrinterSettings.PaperSources(i).SourceName = ppname Then

                rawKind = CInt(doctoprint.PrinterSettings.PaperSources(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSources(i)))

                PaperSizeID = rawKind
                Exit For

            End If

        Next

        Return PaperSizeID


    End Function

    Public Sub Show_Reports(ByVal Rptname_t As String, ByVal Servername As String, ByVal Fromdate_t As Date, ByVal Todate_t As Date, _
                                 ByVal Selctformula_t As String, ByVal Databasename_t As String, ByVal Party_t As String, ByVal Lineid_t As String, _
                                  Optional ByVal Acctsflag_t As Boolean = False, Optional ByVal Compid_t As Double = 0, Optional ByVal Rpttype_t As String = "", _
                                  Optional ByVal Area_t As String = "", Optional ByVal Selectedgroupid_t As String = "", Optional ByVal Selecteditemid_t As String = "" _
                                  , Optional ByVal SelectedLocationid_t As String = "", Optional ByVal BetweenDays_t As String = "", Optional ByVal Type_t As String = "")

        Try

            Dim crParameterFieldDefinitions1 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition1 As ParameterFieldDefinition
            Dim crParameterValues1 As New ParameterValues
            Dim crParameterDiscreteValue1 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions2 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition2 As ParameterFieldDefinition
            Dim crParameterValues2 As New ParameterValues
            Dim crParameterDiscreteValue2 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions3 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition3 As ParameterFieldDefinition
            Dim crParameterValues3 As New ParameterValues
            Dim crParameterDiscreteValue3 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions4 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition4 As ParameterFieldDefinition
            Dim crParameterValues4 As New ParameterValues
            Dim crParameterDiscreteValue4 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions5 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition5 As ParameterFieldDefinition
            Dim crParameterValues5 As New ParameterValues
            Dim crParameterDiscreteValue5 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions6 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition6 As ParameterFieldDefinition
            Dim crParameterValues6 As New ParameterValues
            Dim crParameterDiscreteValue6 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions7 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition7 As ParameterFieldDefinition
            Dim crParameterValues7 As New ParameterValues
            Dim crParameterDiscreteValue7 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions8 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition8 As ParameterFieldDefinition
            Dim crParameterValues8 As New ParameterValues
            Dim crParameterDiscreteValue8 As New ParameterDiscreteValue


            strReportName = Rptname_t
            Servername_t = Servername

            'App_Path = New System.IO.FileInfo(Application.StartupPath).DirectoryName
            'App_Path = Replace(App_Path, "bin", "")

            App_Path = Application.StartupPath
            If Right(App_Path, 5) = "Debug" Then
                App_Path = Replace(App_Path, "bin\Debug", "")
            ElseIf Right(App_Path, 7) = "Release" Then
                App_Path = Replace(App_Path, "bin\Release", "")
            End If

            'App_Path = App_Path & "\Reports" & "\" & strReportName & ".rpt"
            If Acctsflag_t = False Then
                App_Path = App_Path & "\Reports_Imports" & "\" & strReportName & ".rpt"
            Else
                App_Path = App_Path & "\Accounts_Reports" & "\" & strReportName & ".rpt"
            End If

            'MessageBox.Show(App_Path)

            App_Path = Replace(App_Path, "\\", "\")

            'MessageBox.Show(App_Path)

            'Check file exists
            If Not IO.File.Exists(App_Path) Then
                Throw (New Exception("Unable to locate report file:" & vbCrLf & App_Path))
            End If

            rptDocument.Load(App_Path)

            With crConnectionInfo
                .ServerName = Servername_t
                .DatabaseName = Databasename_t
                .UserID = "sa"
                .Password = "admin123"
            End With

            CrTables = rptDocument.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'rptDocument.SetDatabaseLogon("sa", "sbva/tech", "server\sql2005", "SUNRECPAY")
            If Servername_t <> "" Then
                'MsgBox(Servername_t)
                rptDocument.SetDatabaseLogon("sa", "admin123", Servername_t, Databasename_t)

                If LCase(Rpttype_t) = LCase("outstanding receivable") Then

                    crParameterDiscreteValue1.Value = Todate_t
                    crParameterFieldDefinitions1 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition1 = crParameterFieldDefinitions1.Item(0)
                    crParameterValues1 = crParameterFieldDefinition1.CurrentValues

                    crParameterDiscreteValue2.Value = Compid_t
                    crParameterFieldDefinitions2 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item(1)
                    crParameterValues2 = crParameterFieldDefinition2.CurrentValues

                    crParameterDiscreteValue3.Value = Party_t
                    crParameterFieldDefinitions3 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition3 = crParameterFieldDefinitions3.Item(2)
                    crParameterValues3 = crParameterFieldDefinition3.CurrentValues

                    crParameterDiscreteValue4.Value = Area_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues

                    crParameterValues1.Clear()
                    crParameterValues1.Add(crParameterDiscreteValue1)
                    crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)

                    crParameterValues2.Clear()
                    crParameterValues2.Add(crParameterDiscreteValue2)
                    crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

                    crParameterValues3.Clear()
                    crParameterValues3.Add(crParameterDiscreteValue3)
                    crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)

                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                ElseIf LCase(Rpttype_t) = LCase("sales analysis") Then
                    crParameterDiscreteValue1.Value = Fromdate_t
                    crParameterFieldDefinitions1 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition1 = crParameterFieldDefinitions1.Item(0)
                    crParameterValues1 = crParameterFieldDefinition1.CurrentValues

                    crParameterDiscreteValue2.Value = Todate_t
                    crParameterFieldDefinitions2 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item(1)
                    crParameterValues2 = crParameterFieldDefinition2.CurrentValues

                    crParameterDiscreteValue3.Value = Compid_t
                    crParameterFieldDefinitions3 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition3 = crParameterFieldDefinitions3.Item(2)
                    crParameterValues3 = crParameterFieldDefinition3.CurrentValues

                    crParameterDiscreteValue4.Value = Selectedgroupid_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues

                    crParameterDiscreteValue5.Value = Selecteditemid_t
                    crParameterFieldDefinitions5 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition5 = crParameterFieldDefinitions5.Item(4)
                    crParameterValues5 = crParameterFieldDefinition5.CurrentValues

                    crParameterDiscreteValue6.Value = SelectedLocationid_t
                    crParameterFieldDefinitions6 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition6 = crParameterFieldDefinitions6.Item(5)
                    crParameterValues6 = crParameterFieldDefinition6.CurrentValues

                    crParameterDiscreteValue7.Value = BetweenDays_t
                    crParameterFieldDefinitions7 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition7 = crParameterFieldDefinitions7.Item(6)
                    crParameterValues7 = crParameterFieldDefinition7.CurrentValues

                    crParameterDiscreteValue8.Value = Type_t
                    crParameterFieldDefinitions8 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition8 = crParameterFieldDefinitions8.Item(7)
                    crParameterValues8 = crParameterFieldDefinition8.CurrentValues

                    crParameterValues1.Clear()
                    crParameterValues1.Add(crParameterDiscreteValue1)
                    crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)

                    crParameterValues2.Clear()
                    crParameterValues2.Add(crParameterDiscreteValue2)
                    crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

                    crParameterValues3.Clear()
                    crParameterValues3.Add(crParameterDiscreteValue3)
                    crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)

                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                    crParameterValues5.Clear()
                    crParameterValues5.Add(crParameterDiscreteValue5)
                    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)

                    crParameterValues6.Clear()
                    crParameterValues6.Add(crParameterDiscreteValue6)
                    crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)

                    crParameterValues7.Clear()
                    crParameterValues7.Add(crParameterDiscreteValue7)
                    crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7)

                    crParameterValues8.Clear()
                    crParameterValues8.Add(crParameterDiscreteValue8)
                    crParameterFieldDefinition8.ApplyCurrentValues(crParameterValues8)

                Else
                    crParameterDiscreteValue1.Value = Fromdate_t
                    crParameterFieldDefinitions1 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition1 = crParameterFieldDefinitions1.Item(0)
                    crParameterValues1 = crParameterFieldDefinition1.CurrentValues

                    crParameterDiscreteValue2.Value = Todate_t
                    crParameterFieldDefinitions2 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item(1)
                    crParameterValues2 = crParameterFieldDefinition2.CurrentValues

                    crParameterDiscreteValue3.Value = Compid_t
                    crParameterFieldDefinitions3 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition3 = crParameterFieldDefinitions3.Item(2)
                    crParameterValues3 = crParameterFieldDefinition3.CurrentValues

                    crParameterDiscreteValue4.Value = Party_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues

                    crParameterDiscreteValue5.Value = Lineid_t
                    crParameterFieldDefinitions5 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition5 = crParameterFieldDefinitions5.Item(4)
                    crParameterValues5 = crParameterFieldDefinition5.CurrentValues

                    crParameterValues1.Clear()
                    crParameterValues1.Add(crParameterDiscreteValue1)
                    crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)

                    crParameterValues2.Clear()
                    crParameterValues2.Add(crParameterDiscreteValue2)
                    crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

                    crParameterValues3.Clear()
                    crParameterValues3.Add(crParameterDiscreteValue3)
                    crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)

                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                    crParameterValues5.Clear()
                    crParameterValues5.Add(crParameterDiscreteValue5)
                    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
                End If

                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowRefreshButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowCloseButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGroupTreeButton = False
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGotoPageButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPageNavigateButtons = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowTextSearchButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPrintButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = Nothing
                Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument

                'Selctformula_t = "LEDGER_RPT.PTYNAME = 'GK MARKETTING-00003' "
                If Selctformula_t = "" Then
                    Sun_CrystalviewerFrm.CrystalReportViewer1.SelectionFormula = ""
                Else
                    Sun_CrystalviewerFrm.CrystalReportViewer1.SelectionFormula = Selctformula_t
                End If

                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")

                Sun_CrystalviewerFrm.ShowInTaskbar = False
                Sun_CrystalviewerFrm.ShowDialog()
                Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                rptDocument.Close()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Showreport_Reports(ByVal Rptname_t As String, ByVal Servername As String, ByVal Fromdate_t As DateTime, ByVal Todate_t As DateTime, _
                                  ByVal Selctformula_t As String, ByVal Databasename_t As String, _
                                   Optional ByVal Acctsflag_t As Boolean = False, Optional ByVal Compid_t As Double = 0, _
                                   Optional ByVal Lineid_t As String = "", Optional ByVal SelPartyid_t As String = "", _
                                   Optional ByVal Groupid_t As String = "", Optional ByVal Accolorid_t As String = "", Optional ByVal Itemid_t As String = "", _
                                   Optional ByVal Processid_t As String = "", Optional ByVal Partyid_t As String = "", Optional ByVal Agendid_t As String = "", _
                                   Optional ByVal Billnoid_t As String = "", Optional ByVal Type_t As String = "")
        Try
            Dim crParameterFieldDefinitions1 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition1 As ParameterFieldDefinition
            Dim crParameterValues1 As New ParameterValues
            Dim crParameterDiscreteValue1 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions2 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition2 As ParameterFieldDefinition
            Dim crParameterValues2 As New ParameterValues
            Dim crParameterDiscreteValue2 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions3 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition3 As ParameterFieldDefinition
            Dim crParameterValues3 As New ParameterValues
            Dim crParameterDiscreteValue3 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions4 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition4 As ParameterFieldDefinition
            Dim crParameterValues4 As New ParameterValues
            Dim crParameterDiscreteValue4 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions5 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition5 As ParameterFieldDefinition
            Dim crParameterValues5 As New ParameterValues
            Dim crParameterDiscreteValue5 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions6 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition6 As ParameterFieldDefinition
            Dim crParameterValues6 As New ParameterValues
            Dim crParameterDiscreteValue6 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions7 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition7 As ParameterFieldDefinition
            Dim crParameterValues7 As New ParameterValues
            Dim crParameterDiscreteValue7 As New ParameterDiscreteValue

            Dim crParameterFieldDefinitions8 As ParameterFieldDefinitions
            Dim crParameterFieldDefinition8 As ParameterFieldDefinition
            Dim crParameterValues8 As New ParameterValues
            Dim crParameterDiscreteValue8 As New ParameterDiscreteValue

            strReportName = Rptname_t
            Servername_t = Servername

            'App_Path = New System.IO.FileInfo(Application.StartupPath).DirectoryName
            'App_Path = Replace(App_Path, "bin", "")

            App_Path = Application.StartupPath
            If Right(App_Path, 5) = "Debug" Then
                App_Path = Replace(App_Path, "bin\Debug", "")
            ElseIf Right(App_Path, 7) = "Release" Then
                App_Path = Replace(App_Path, "bin\Release", "")
            End If

            'App_Path = App_Path & "\Reports" & "\" & strReportName & ".rpt"
            If Acctsflag_t = False Then
                App_Path = App_Path & "\Reports_Imports" & "\" & strReportName & ".rpt"
            Else
                App_Path = App_Path & "\Accounts_Reports" & "\" & strReportName & ".rpt"
            End If

            'App_Path = Replace(App_Path, "\\", "\")
            'Check file exists

            If Not IO.File.Exists(App_Path) Then
                Throw (New Exception("Unable to locate report file:" & vbCrLf & App_Path))
            End If

            rptDocument.Load(App_Path)

            With crConnectionInfo
                .ServerName = Servername_t
                .DatabaseName = Databasename_t
                .UserID = "sa"
                .Password = "admin123"
            End With

            CrTables = rptDocument.Database.Tables
            For Each CrTable In CrTables
                crtableLogoninfo = CrTable.LogOnInfo
                crtableLogoninfo.ConnectionInfo = crConnectionInfo
                CrTable.ApplyLogOnInfo(crtableLogoninfo)
            Next

            'rptDocument.SetDatabaseLogon("sa", "sbva/tech", "server\sql2005", "SUNRECPAY")

            If Servername_t <> "" Then
                'MsgBox(Servername_t)
                rptDocument.SetDatabaseLogon("sa", "admin123", Servername_t, Databasename_t)

                crParameterDiscreteValue1.Value = Fromdate_t '.ToString("yyyy/MM/dd")
                crParameterFieldDefinitions1 = rptDocument.DataDefinition.ParameterFields()
                crParameterFieldDefinition1 = crParameterFieldDefinitions1.Item(0)
                crParameterValues1 = crParameterFieldDefinition1.CurrentValues

                crParameterDiscreteValue2.Value = Todate_t '.ToString("yyyy/MM/dd")
                crParameterFieldDefinitions2 = rptDocument.DataDefinition.ParameterFields()
                crParameterFieldDefinition2 = crParameterFieldDefinitions2.Item(1)
                crParameterValues2 = crParameterFieldDefinition2.CurrentValues

                crParameterDiscreteValue3.Value = Compid_t
                crParameterFieldDefinitions3 = rptDocument.DataDefinition.ParameterFields()
                crParameterFieldDefinition3 = crParameterFieldDefinitions3.Item(2)
                crParameterValues3 = crParameterFieldDefinition3.CurrentValues

                If Rptname_t = "InvoiceChecklist" Then
                    crParameterDiscreteValue4.Value = SelPartyid_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues
                End If

                '    If LCase(Rptname_t) = "retail_bill_reg" Then
                '    crParameterDiscreteValue4.Value = Locationid_t
                '    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                '    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                '    crParameterValues4 = crParameterFieldDefinition4.CurrentValues
                '    crParameterDiscreteValue5.Value = Billnoid_t
                '    crParameterFieldDefinitions5 = rptDocument.DataDefinition.ParameterFields()
                '    crParameterFieldDefinition5 = crParameterFieldDefinitions5.Item(4)
                '    crParameterValues5 = crParameterFieldDefinition5.CurrentValues
                '    crParameterDiscreteValue6.Value = Type_t
                '    crParameterFieldDefinitions6 = rptDocument.DataDefinition.ParameterFields()
                '    crParameterFieldDefinition6 = crParameterFieldDefinitions6.Item(5)
                '    crParameterValues6 = crParameterFieldDefinition6.CurrentValues
                '    End If

                If Rptname_t = "Outstanding_Agewise" Then

                    If Partyid_t = "" Or Partyid_t Is Nothing Then
                        Partyid_t = "Select ptycode from party"
                    End If

                    If Agendid_t = "" Or Agendid_t Is Nothing Then
                        Agendid_t = "Select ptycode from party where ptytype='Agent'"
                    End If

                    crParameterDiscreteValue4.Value = Partyid_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues

                    crParameterDiscreteValue5.Value = Agendid_t
                    crParameterFieldDefinitions5 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition5 = crParameterFieldDefinitions5.Item(4)
                    crParameterValues5 = crParameterFieldDefinition5.CurrentValues

                ElseIf LCase(Rptname_t) = LCase("Invoicereg") Then
                    crParameterDiscreteValue4.Value = Lineid_t
                    crParameterFieldDefinitions4 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition4 = crParameterFieldDefinitions4.Item(3)
                    crParameterValues4 = crParameterFieldDefinition4.CurrentValues

                    crParameterDiscreteValue5.Value = SelPartyid_t
                    crParameterFieldDefinitions5 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition5 = crParameterFieldDefinitions5.Item(4)
                    crParameterValues5 = crParameterFieldDefinition5.CurrentValues

                    crParameterDiscreteValue6.Value = Groupid_t
                    crParameterFieldDefinitions6 = rptDocument.DataDefinition.ParameterFields()
                    crParameterFieldDefinition6 = crParameterFieldDefinitions6.Item(5)
                    crParameterValues6 = crParameterFieldDefinition6.CurrentValues
                End If

                crParameterValues1.Clear()
                crParameterValues1.Add(crParameterDiscreteValue1)
                crParameterFieldDefinition1.ApplyCurrentValues(crParameterValues1)

                crParameterValues2.Clear()
                crParameterValues2.Add(crParameterDiscreteValue2)
                crParameterFieldDefinition2.ApplyCurrentValues(crParameterValues2)

                crParameterValues3.Clear()
                crParameterValues3.Add(crParameterDiscreteValue3)
                crParameterFieldDefinition3.ApplyCurrentValues(crParameterValues3)

                If Rptname_t = "InvoiceChecklist" Then
                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
                End If

                '    If LCase(Rptname_t) = "retail_bill_reg" Then
                '    crParameterValues4.Clear()
                '    crParameterValues4.Add(crParameterDiscreteValue4)
                '    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)
                '    crParameterValues5.Clear()
                '    crParameterValues5.Add(crParameterDiscreteValue5)
                '    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)
                '    crParameterValues6.Clear()
                '    crParameterValues6.Add(crParameterDiscreteValue6)
                '    crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)
                '    End If

                If Rptname_t = "AccessoryLedger" Or Rptname_t = "AccessoryStock" Or Rptname_t = "ProcessLedger" Or Rptname_t = "PRODUCTION_RPT" Then
                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                    crParameterValues5.Clear()
                    crParameterValues5.Add(crParameterDiscreteValue5)
                    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)

                    crParameterValues6.Clear()
                    crParameterValues6.Add(crParameterDiscreteValue6)
                    crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)

                    crParameterValues7.Clear()
                    crParameterValues7.Add(crParameterDiscreteValue7)
                    crParameterFieldDefinition7.ApplyCurrentValues(crParameterValues7)
                End If

                If Rptname_t = "Outstanding_Agewise" Then
                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                    crParameterValues5.Clear()
                    crParameterValues5.Add(crParameterDiscreteValue5)
                    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)

                ElseIf LCase(Rptname_t) = LCase("Invoicereg") Then
                    crParameterValues4.Clear()
                    crParameterValues4.Add(crParameterDiscreteValue4)
                    crParameterFieldDefinition4.ApplyCurrentValues(crParameterValues4)

                    crParameterValues5.Clear()
                    crParameterValues5.Add(crParameterDiscreteValue5)
                    crParameterFieldDefinition5.ApplyCurrentValues(crParameterValues5)

                    crParameterValues6.Clear()
                    crParameterValues6.Add(crParameterDiscreteValue6)
                    crParameterFieldDefinition6.ApplyCurrentValues(crParameterValues6)
                End If

                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowRefreshButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowCloseButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGroupTreeButton = False
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowGotoPageButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPageNavigateButtons = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowTextSearchButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.ShowPrintButton = True
                Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = Nothing
                Sun_CrystalviewerFrm.CrystalReportViewer1.ReportSource = rptDocument

                'Selctformula_t = "LEDGER_RPT.PTYNAME = 'GK MARKETTING-00003' "
                If Selctformula_t = "" Then
                    Sun_CrystalviewerFrm.CrystalReportViewer1.SelectionFormula = ""
                Else
                    Sun_CrystalviewerFrm.CrystalReportViewer1.SelectionFormula = Selctformula_t
                End If

                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")
                SendKeys.Send("{ENTER}")

                Sun_CrystalviewerFrm.ShowInTaskbar = False
                Sun_CrystalviewerFrm.ShowDialog()
                Sun_CrystalviewerFrm.CrystalReportViewer1.Refresh()
                rptDocument.Close()
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Module
