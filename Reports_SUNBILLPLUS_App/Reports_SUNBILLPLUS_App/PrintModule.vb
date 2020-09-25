Imports System.Drawing.Printing
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Security
Imports System.ComponentModel
Module PrintModule
    Const WM_SETTINGCHANGE As Integer = &H1A
    Const HWND_BROADCAST As Integer = &HFFFF

    Private Declare Auto Function DocumentProperties Lib "winspool.drv" _
        (ByVal hWnd As IntPtr, ByVal hPrinter As IntPtr, ByVal pDeviceName As String, _
         ByVal pDevModeOutput As IntPtr, ByVal pDevModeInput As IntPtr, ByVal fMode As Int32) As Integer

    Public Declare Function GetPrinter Lib "winspool.drv" Alias "GetPrinterW" _
     (ByVal hPrinter As IntPtr, ByVal Level As Integer, ByVal pPrinter As IntPtr, _
      ByVal cbBuf As Integer, ByRef pcbNeeded As Integer) As Integer

    Private Declare Function SetPrinter Lib "winspool.drv" Alias "SetPrinterA" _
    (ByVal hPrinter As IntPtr, ByVal level As Integer, ByVal pPrinterInfoIn As IntPtr, _
     ByVal command As Int32) As Boolean

    <DllImport("winspool.Drv", EntryPoint:="SetPrinterA", SetLastError:=True, CharSet:=CharSet.Auto, ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall), SuppressUnmanagedCodeSecurityAttribute> _
    Friend Function SetPrinter_New(hPrinter As IntPtr, <MarshalAs(UnmanagedType.I4)> level As Integer, pPrinter As IntPtr, <MarshalAs(UnmanagedType.I4)> command As Integer) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="OpenPrinterA", ExactSpelling:=True, _
     SetLastError:=True, CallingConvention:=CallingConvention.StdCall, _
     CharSet:=CharSet.Ansi)> _
    Private Function OpenPrinter(ByVal pPrinterName As String, _
  ByRef hPrinter As IntPtr, ByRef pDefault As PRINTER_DEFAULTS) As Boolean
    End Function

    <DllImport("winspool.drv", EntryPoint:="ClosePrinter", SetLastError:=True, ExactSpelling:=True, _
     CallingConvention:=CallingConvention.StdCall)> _
    Private Function ClosePrinter(ByVal hPrinter As Int32) As Boolean
    End Function

    Declare Function GetDefaultPrinter Lib "winspool.drv" Alias "GetDefaultPrinterA" _
     (ByVal pszBuffer As System.Text.StringBuilder, ByRef pcchBuffer As Int32) As Boolean

    Declare Function SetDefaultPrinter Lib "winspool.drv" Alias "SetDefaultPrinterA" _
     (ByVal pszPrinter As String) As Boolean

    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" _
     (ByVal hpvDest As IntPtr, ByVal hpvSource As IntPtr, ByVal cbCopy As Long)

    Private Structure PRINTER_DEFAULTS
        Dim pDatatype As String
        Dim pDevMode As Long
        Dim pDesiredAccess As Long
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Friend Structure structPrinterDefaults
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public pDatatype As [String]
        Public pDevMode As IntPtr
        <MarshalAs(UnmanagedType.I4)> _
        Public DesiredAccess As Integer
    End Structure

    Private Const STANDARD_RIGHTS_REQUIRED = &HF0000
    Private Const PRINTER_ACCESS_ADMINISTER = &H4
    Private Const PRINTER_ACCESS_USE = &H8
    Private Const PRINTER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED Or PRINTER_ACCESS_ADMINISTER Or PRINTER_ACCESS_USE)

    Private Const DM_IN_BUFFER As Integer = 8
    Private Const DM_IN_PROMPT As Integer = 4
    Private Const DM_OUT_BUFFER As Integer = 2

    Private Structure PRINTER_INFO_9
        Dim pDevMode As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure PRINTER_INFO_2
        <MarshalAs(UnmanagedType.LPTStr)> Public pServerName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pPrinterName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pShareName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pPortName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pDriverName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pComment As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pLocation As String

        Public pDevMode As IntPtr

        <MarshalAs(UnmanagedType.LPTStr)> Public pSepFile As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pPrintProcessor As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pDatatype As String
        <MarshalAs(UnmanagedType.LPTStr)> Public pParameters As String

        Public pSecurityDescriptor As IntPtr
        Public Attributes As Integer
        Public Priority As Integer
        Public DefaultPriority As Integer
        Public StartTime As Integer
        Public UntilTime As Integer
        Public Status As Integer
        Public cJobs As Integer
        Public AveragePPM As Integer
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Public Structure DEVMODE
        <MarshalAs(UnmanagedType.ByValTStr, Sizeconst:=32)> Public pDeviceName As String
        Public dmSpecVersion As Short
        Public dmDriverVersion As Short
        Public dmSize As Short
        Public dmDriverExtra As Short
        Public dmFields As Integer
        Public dmOrientation As Short
        Public dmPaperSize As Short
        Public dmPaperLength As Short
        Public dmPaperWidth As Short
        Public dmScale As Short
        Public dmCopies As Short
        Public dmDefaultSource As Short
        Public dmPrintQuality As Short
        Public dmColor As Short
        Public dmDuplex As Short
        Public dmYResolution As Short
        Public dmTTOption As Short
        Public dmCollate As Short
        <MarshalAs(UnmanagedType.ByValTStr, Sizeconst:=32)> Public dmFormName As String
        Public dmUnusedPadding As Short
        Public dmBitsPerPel As Integer
        Public dmPelsWidth As Integer
        Public dmPelsHeight As Integer
        Public dmNup As Integer
        Public dmDisplayFrequency As Integer
        Public dmICMMethod As Integer
        Public dmICMIntent As Integer
        Public dmMediaType As Integer
        Public dmDitherType As Integer
        Public dmReserved1 As Integer
        Public dmReserved2 As Integer
        Public dmPanningWidth As Integer
        Public dmPanningHeight As Integer
    End Structure

    Private pOriginalDEVMODE As IntPtr


    Public Sub SavePrinterSettings(ByVal printerName As String)
        Dim Needed As Integer
        Dim hPrinter As IntPtr
        If printerName = "" Then Exit Sub

        Try
            If OpenPrinter(printerName, hPrinter, Nothing) = False Then Exit Sub
            'Save original printer settings data (DEVMODE structure)
            Needed = DocumentProperties(Sun_CrystalviewerFrm.Handle, hPrinter, printerName, Nothing, Nothing, 0)
            Dim pFullDevMode As IntPtr = Marshal.AllocHGlobal(Needed) 'buffer for DEVMODE structure
            DocumentProperties(Sun_CrystalviewerFrm.Handle, hPrinter, printerName, pFullDevMode, Nothing, DM_OUT_BUFFER)
            pOriginalDEVMODE = Marshal.AllocHGlobal(Needed)
            CopyMemory(pOriginalDEVMODE, pFullDevMode, Needed)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub RestorePrinterSettings(ByVal printerName As String)
        Dim hPrinter As IntPtr
        If printerName = "" Then Exit Sub

        Try
            If OpenPrinter(printerName, hPrinter, Nothing) = False Then Exit Sub
            Dim PI9 As New PRINTER_INFO_9
            PI9.pDevMode = pOriginalDEVMODE
            Dim pPI9 As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PI9))
            Marshal.StructureToPtr(PI9, pPI9, True)
            SetPrinter(hPrinter, 9, pPI9, 0&)
            Marshal.FreeHGlobal(pPI9) 'pOriginalDEVMODE will be free too
            ClosePrinter(hPrinter)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Function GetPrinterName() As String
        Dim buffer As New System.Text.StringBuilder(256)
        Dim PrinterName As String = String.Empty
        'Get default printer's name
        GetDefaultPrinter(buffer, 256)
        PrinterName = buffer.ToString
        If PrinterName = "" Then
            MsgBox("Can't find default printer.")
        End If
        Return PrinterName
    End Function

    Sub SetTray(ByVal printerName As String, ByVal trayNumber As Integer)
        Dim hPrinter As IntPtr
        Dim Needed As Integer

        OpenPrinter(printerName, hPrinter, Nothing)

        'Get original printer settings data (DEVMODE structure)
        Needed = DocumentProperties(IntPtr.Zero, hPrinter, printerName, Nothing, Nothing, 0)
        Dim pFullDevMode As IntPtr = Marshal.AllocHGlobal(Needed) 'buffer for DEVMODE structure
        DocumentProperties(IntPtr.Zero, hPrinter, printerName, pFullDevMode, Nothing, DM_OUT_BUFFER)

        Dim pDevMode9 As DEVMODE = Marshal.PtrToStructure(pFullDevMode, GetType(DEVMODE))

        ' Tray change
        pDevMode9.dmDefaultSource = trayNumber


        Marshal.StructureToPtr(pDevMode9, pFullDevMode, True)

        Dim PI9 As New PRINTER_INFO_9
        PI9.pDevMode = pFullDevMode

        Dim pPI9 As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PI9))
        Marshal.StructureToPtr(PI9, pPI9, True)
        SetPrinter(hPrinter, 9, pPI9, 0&)
        Marshal.FreeHGlobal(pPI9) 'pFullDevMode will be free too

        ClosePrinter(hPrinter)

    End Sub

    Public Sub SetPaperSize(printerName As String, paperName As String, widthMm As Single, heightMm As Single)
        If PlatformID.Win32NT = Environment.OSVersion.Platform Then
            ' The code to add a custom paper size is different for Windows NT then it is
            ' for previous versions of windows

            Const PRINTER_ACCESS_USE As Integer = &H8
            Const PRINTER_ACCESS_ADMINISTER As Integer = &H4
            Const FORM_PRINTER As Integer = &H2

            Dim defaults As New structPrinterDefaults()
            defaults.pDatatype = Nothing
            defaults.pDevMode = IntPtr.Zero
            defaults.DesiredAccess = PRINTER_ACCESS_ADMINISTER Or PRINTER_ACCESS_USE

            'Dim hPrinter As IntPtr = IntPtr.Zero
            Dim hPrinter As IntPtr

            ' Open the printer.
            OpenPrinter(printerName, hPrinter, Nothing)
            If OpenPrinter(printerName, hPrinter, Nothing) Then
                Try
                    ' delete the form incase it already exists
                    DeleteForm(hPrinter, paperName)
                    ' create and initialize the FORM_INFO_1 structure

                    Dim formInfo As New FormInfo1()
                    formInfo.Flags = 0
                    formInfo.pName = paperName
                    ' all sizes in 1000ths of millimeters
                    formInfo.Size.width = CInt(widthMm * 1000.0)
                    formInfo.Size.height = CInt(heightMm * 1000.0)
                    formInfo.ImageableArea.left = 0
                    formInfo.ImageableArea.right = formInfo.Size.width
                    formInfo.ImageableArea.top = 0
                    formInfo.ImageableArea.bottom = formInfo.Size.height
                    If Not AddForm(hPrinter, 1, formInfo) Then
                        Dim strBuilder As New StringBuilder()
                        strBuilder.AppendFormat("Failed to add the custom paper size {0} to the printer {1}, System error number: {2}", paperName, printerName, GetLastError())
                        Throw New ApplicationException(strBuilder.ToString())
                    End If

                    ' INIT
                    Const DM_OUT_BUFFER As Integer = 2
                    Const DM_IN_BUFFER As Integer = 8
                    Dim devMode As New structDevMode()
                    Dim hPrinterInfo As IntPtr, hDummy As IntPtr
                    Dim printerInfo As PRINTER_INFO_9
                    printerInfo.pDevMode = IntPtr.Zero
                    Dim iPrinterInfoSize As Integer, iDummyInt As Integer


                    ' GET THE SIZE OF THE DEV_MODE BUFFER
                    Dim iDevModeSize As Integer = DocumentProperties(IntPtr.Zero, hPrinter, printerName, IntPtr.Zero, IntPtr.Zero, 0)

                    If iDevModeSize < 0 Then
                        Throw New ApplicationException("Cannot get the size of the DEVMODE structure.")
                    End If

                    ' ALLOCATE THE BUFFER
                    Dim hDevMode As IntPtr = Marshal.AllocCoTaskMem(iDevModeSize + 100)

                    ' GET A POINTER TO THE DEV_MODE BUFFER 
                    Dim iRet As Integer = DocumentProperties(IntPtr.Zero, hPrinter, printerName, hDevMode, IntPtr.Zero, DM_OUT_BUFFER)

                    If iRet < 0 Then
                        Throw New ApplicationException("Cannot get the DEVMODE structure.")
                    End If

                    ' FILL THE DEV_MODE STRUCTURE
                    devMode = CType(Marshal.PtrToStructure(hDevMode, devMode.[GetType]()), structDevMode)

                    ' SET THE FORM NAME FIELDS TO INDICATE THAT THIS FIELD WILL BE MODIFIED
                    devMode.dmFields = &H10000
                    ' DM_FORMNAME 
                    ' SET THE FORM NAME
                    devMode.dmFormName = paperName

                    ' PUT THE DEV_MODE STRUCTURE BACK INTO THE POINTER
                    Marshal.StructureToPtr(devMode, hDevMode, True)

                    ' MERGE THE NEW CHAGES WITH THE OLD
                    iRet = DocumentProperties(IntPtr.Zero, hPrinter, printerName, printerInfo.pDevMode, printerInfo.pDevMode, DM_IN_BUFFER Or DM_OUT_BUFFER)

                    If iRet < 0 Then
                        Throw New ApplicationException("Unable to set the orientation setting for this printer.")
                    End If

                    ' GET THE PRINTER INFO SIZE
                    GetPrinter(hPrinter, 9, IntPtr.Zero, 0, iPrinterInfoSize)
                    If iPrinterInfoSize = 0 Then
                        Throw New ApplicationException("GetPrinter failed. Couldn't get the # bytes needed for shared PRINTER_INFO_9 structure")
                    End If

                    ' ALLOCATE THE BUFFER
                    hPrinterInfo = Marshal.AllocCoTaskMem(iPrinterInfoSize + 100)

                    ' GET A POINTER TO THE PRINTER INFO BUFFER
                    Dim bSuccess As Boolean = GetPrinter(hPrinter, 9, hPrinterInfo, iPrinterInfoSize, iDummyInt)

                    If Not bSuccess Then
                        Throw New ApplicationException("GetPrinter failed. Couldn't get the shared PRINTER_INFO_9 structure")
                    End If

                    ' FILL THE PRINTER INFO STRUCTURE
                    printerInfo = CType(Marshal.PtrToStructure(hPrinterInfo, printerInfo.[GetType]()), PRINTER_INFO_9)
                    printerInfo.pDevMode = hDevMode

                    ' GET A POINTER TO THE PRINTER INFO STRUCTURE
                    Marshal.StructureToPtr(printerInfo, hPrinterInfo, True)

                    ' SET THE PRINTER SETTINGS
                    bSuccess = SetPrinter(hPrinter, 9, hPrinterInfo, 0)

                    If Not bSuccess Then
                        Throw New Win32Exception(Marshal.GetLastWin32Error(), "SetPrinter() failed.  Couldn't set the printer settings")
                    End If

                    ' Tell all open programs that this change occurred.
                    SendMessageTimeout(New IntPtr(HWND_BROADCAST), WM_SETTINGCHANGE, IntPtr.Zero, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NORMAL, 1000, _
                        hDummy)
                Finally
                    ClosePrinter(hPrinter)
                End Try
            Else
                Dim strBuilder As New StringBuilder()
                strBuilder.AppendFormat("Failed to open the {0} printer, System error number: {1}", printerName, GetLastError())
                Throw New ApplicationException(strBuilder.ToString())
            End If
        Else
            Dim pDevMode As New structDevMode()
            Dim hDC As IntPtr = CreateDC(Nothing, printerName, Nothing, pDevMode)
            If hDC <> IntPtr.Zero Then
                Const DM_PAPERSIZE As Long = &H2L
                Const DM_PAPERLENGTH As Long = &H4L
                Const DM_PAPERWIDTH As Long = &H8L
                pDevMode.dmFields = CInt(DM_PAPERSIZE Or DM_PAPERWIDTH Or DM_PAPERLENGTH)
                pDevMode.dmPaperSize = 256
                pDevMode.dmPaperWidth = CShort(widthMm * 1000.0)
                pDevMode.dmPaperLength = CShort(heightMm * 1000.0)
                ResetDC(hDC, pDevMode)
                DeleteDC(hDC)
            End If
        End If
    End Sub
End Module
