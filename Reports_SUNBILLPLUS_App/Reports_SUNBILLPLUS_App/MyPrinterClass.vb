Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.ComponentModel
Imports System.Drawing.Printing
Public Class MyPrinterClass
    <DllImport("winspool.drv", SetLastError:=True, CharSet:=CharSet.Ansi, _
    ExactSpelling:=True, _
    CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function OpenPrinter(ByVal pPrinterName As String, _
    ByRef phPrinter As Int32, ByVal pDefault As Int32) As Boolean
    End Function

    <DllImport("winspool.drv", SetLastError:=True, _
        ExactSpelling:=True, _
        CallingConvention:=CallingConvention.StdCall)> _
    Public Shared Function ClosePrinter(ByVal hPrinter As Int32) As Boolean

    End Function

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

    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal pDst As IntPtr, _
                                                                 ByVal pSrc As IntPtr, _
                                                                 ByVal ByteLen As Long)

    
End Class
