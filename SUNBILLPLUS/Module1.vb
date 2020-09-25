Imports System
Imports System.Data
Imports System.Xml
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports System.Net.Mail

Module Module1
    Public random As New Random()
    Public conn As New SqlConnection
    Public Event_Conn As New SqlConnection
    Public trans As SqlTransaction
    Public var As String
    Public varid As Double
    Public tmppassstr As String
    Public Genuid As Integer
    Public Genuname As String
    Public Gendays As Integer
    Public Gencompid As Double
    Public Gencompname As String
    Public Genutype As String
    Public ShowFormFromReport As Boolean
    Public BillBreakupFlag As Boolean
    Public Autoadjustflg_t As Boolean
    Public GenFromdate As DateTime
    Public GenTodate As DateTime
    Public Selectedprocess_t As String = ""
    Public Font_m As String, FontStyle_m As String
    Public AllowFormEdit_t As Boolean
    Public SystemName_t As String = System.Environment.MachineName.ToString
    'Dim FontStyle_m As FontStyle
    Public Size_m As String  'GraphicsUnit = GraphicsUnit.Point '= Drawing.FontStyle.Regular ', FontStyle_m As String, Size_m As Single
    Public Accspurcexcessperc_t As Double
    Public Generateeventlogs_t As Boolean
    Public Defaultlocation_t As String
    Public Defaultlocationid_t As Double
    Public Vatcstflag_t As Boolean
    Public Vatperc_t As Double
    Public Cstperc_t As Double
    Public Vatid_t As Double = -15
    Public Cstid_t As Double = -5
    Public Acpostingwithlrno_t As Boolean
    Public SIZE_ASSORTMENT_ENTRY As Boolean

    Public Const ACCT_CASH = -1
    Public Const GRP_CASH = -201
    Public Const GRP_BANK = -202
    Public Const GRP_SUNDRY_DEBTORS = -203
    Public Const GRP_SUNDRY_CREDITORS_TRADE = -251
    Public Const GRP_SUNDRY_CREDITORS_EXPN = -252
    Public Const GRP_SUNDRY_CREDITORS_OTHERS = -253


    Dim cmd As New SqlCommand
    Dim tmpds As DataSet
    Dim tmpda As SqlDataAdapter
    Dim tmpsqlstr As String
    Dim Idkey As Integer
    Public editflag As Boolean
    Public ds_Gridcptn As New DataSet

    Dim Fromaddr_t As String, Fromuname_t As String, Frompwd_t As String, Toaddr_t As String, Cc_t As String

    Dim strReportName As String, App_Path As String, Pdffile As String
    Public Servername_t As String, RptProcess_t As String
    Public Databasename_t As String
    Public Appversion_t As String
    Public Readonlycolor_t As Color = Color.LightBlue
    Public Normalcolor_t As Color = Color.White

    'Dim paramField As New ParameterField()
    'Dim paramFields As New ParameterFields()
    'Dim paramDiscreteValue As New ParameterDiscreteValue()
    'Dim rptDocument As New ReportDocument
    'Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    'Dim crParameterFieldDefinition As ParameterFieldDefinition
    'Dim crParameterFieldLocation As ParameterFieldDefinition
    'Dim crParameterValues As New ParameterValues
    'Dim crParameterDiscreteValue As New ParameterDiscreteValue
    'Dim crParameterDiscreteValue1 As New ParameterDiscreteValue
    'Dim crParameterRangeValue As New ParameterRangeValue

    'Dim crConnectionInfo As New ConnectionInfo
    'Dim crtableLogoninfos As New TableLogOnInfos
    'Dim crtableLogoninfo As New TableLogOnInfo
    'Dim CrTables As Tables
    'Dim CrTable As Table

    Public Receiptflg_t As Boolean
    Public Gridcaption_t As String
    Public Grid As New DataGridView

    Public Enum SaveOption
        Insert = 0
        Update
        Delete
    End Enum

    Public Function GensaveSettings(ByVal editflag_v As SaveOption, ByVal Process_v As String, ByVal DETAILID_V As Double, ByVal numeric_v As String, _
                                    ByVal stringvalue_v As String, ByVal Datevalue_v As Date, ByVal Reference_v As String, ByVal Sino_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SETTINGSNEW_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        cmd.Parameters.Add("@PROCESS", SqlDbType.VarChar).Value = Process_v
        cmd.Parameters.Add("@DETAILID", SqlDbType.Float).Value = DETAILID_V
        cmd.Parameters.Add("@NUMERICVALUE", SqlDbType.VarChar).Value = numeric_v
        cmd.Parameters.Add("@STRINGVALUE", SqlDbType.VarChar).Value = stringvalue_v
        cmd.Parameters.Add("@DATEVALUE", SqlDbType.DateTime).Value = IIf(Datevalue_v = Nothing, DBNull.Value, Datevalue_v)
        cmd.Parameters.Add("@REFERENCE", SqlDbType.VarChar).Value = Reference_v
        cmd.Parameters.Add("@sLno", SqlDbType.VarChar).Value = Sino_v
        cmd.ExecuteNonQuery()

    End Function

    Public Sub UserPermission(ByVal frm As Form)
        Try
            'Dim cmd As SqlCommand
            'Dim ds_user As New DataSet
            'Dim da_user As SqlDataAdapter
            'Dim cnt As Integer

            'cmd = New SqlCommand("select * from users where uid = " & Genu & " ", conn)
            'cmd.CommandType = CommandType.Text
            'da_user = New SqlDataAdapter(cmd)
            'ds_user = New DataSet
            'ds_user.Clear()
            'da_user.Fill(ds_user)


            'cnt = ds_user.Tables(0).Rows.Count

            'If LCase(Genutype) <> ("administrator") Then
            For Each C As Control In frm.Controls
                If TypeOf C Is Button Then
                    If LCase(C.Text).IndexOf(LCase("ok")) <> -1 Or LCase(C.Text).IndexOf(LCase("save")) <> -1 Then
                        C.Visible = False
                    End If
                    If LCase(C.Text).IndexOf(LCase("cancel")) <> -1 Then
                        C.Text = "&Close"
                    End If
                Else
                    'If TypeOf C Is Panel Then
                    'Else
                    C.Enabled = False
                    'End If

                End If


            Next
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub CopyPaste(e As KeyPressEventArgs, ByVal Curentrow As Integer, ByVal Grd As DataGridView, ByVal ParamArray col() As Integer)
        Try
            'If e.KeyC
            'If e.ke
            Dim CellValue_t As String
            Dim row As Integer
            If Curentrow = 0 Then Exit Sub

            row = Curentrow - 1

            If LCase(e.KeyChar) = "c" And LCase(e.KeyChar) = "p" Then
                For Each i In col
                    CellValue_t = IIf(IsDBNull(Grd.Item(i, row).Value), 0, Grd.Item(i, row).Value)
                    If CellValue_t = "" Or CellValue_t Is Nothing Then
                    Else
                        If i <= Grd.Rows.Count - 1 Then Grd.Item(i, Curentrow).Value = CellValue_t
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub main()
        Dim connsting, Eventconnstring As String
        Try

            'MsgBox(System.Configuration.ConfigurationSettings.AppSettings("Sqlconnection1").ToString)
            'MsgBox(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)

            Call getservername()

            'Call UpdateAppSettings("Sqlconnection", "Database")

            'filetext_t = "SUNSETUP"

            'App_Path = Application.StartupPath
            'If Right(App_Path, 5) = "Debug" Then
            ' App_Path = Replace(App_Path, "bin\Debug", "")
            ' Else

            'End If

            'App_Path = App_Path & "\" & filetext_t & ".txt"

            'App_Path = Replace(App_Path, "\\", "\")

            'Servername_t = IO.File.ReadAllLines(App_Path)(0) '0 means first line, 1 means 2nd line,....

            'Dim path As String
            'path = Application.CommonAppDataPath
            'path = Application.CommonAppDataRegistry.ToString
            'path = Application.ExecutablePath
            'path = Application.LocalUserAppDataPath
            'path = Application.StartupPath
            'path = Application.UserAppDataPath
            'path = My.Application.Info.AssemblyName


            'connsting = ConfigurationSettings.AppSettings("SQLConnection").ToString()

            connsting = System.Configuration.ConfigurationManager.AppSettings("SQLConnection").ToString

            'connsting = "Server=SERVER\SQL2005;Database=SUNRECPAY;Integrated Security=false;UID=sa;pwd=sbva/tech;"
            'connsting = "Server='" & Servername_t & "';Database=SUNRECPAY;Integrated Security=false;UID=sa;pwd=sbva/tech;"

            conn = New SqlConnection(connsting)

            Eventconnstring = System.Configuration.ConfigurationManager.AppSettings("EVENTSQLConnection").ToString
            Event_Conn = New SqlConnection(Eventconnstring)


            'Catch ex As Exception

            'MsgBox(ex.Message)

        Catch ex As ArgumentException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As System.IO.FileNotFoundException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As NullReferenceException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As UnauthorizedAccessException
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)


        Catch ex As System.IO.IOException
            ' A generic exception handler, for any IO error
            ' that hasn't been caught yet. Here, it ought
            ' to just be that the drive isn't ready.
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Catch ex As Exception
            'MsgBox(ex.InnerException.Message)
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

    Public Sub gridheadername(ByVal DGV As DataGridView, ByVal val As Boolean, ByVal COLNAME As String, ByVal ParamArray cols() As String)
        Dim i As Object
        For Each i In cols
            Call Gridcaption(COLNAME)
            DGV.Columns(i).HeaderCell.Value = Gridcaption_t
        Next
    End Sub

    Public Function GensaveQuotationHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
              ByVal Vchdate_v As Date, ByVal ItemType_v As String, ByVal Customer_v As String, ByVal Refernce_v As String, _
              ByVal Custno_v As String, ByVal Locationid_v As Double, ByVal compid_v As Double, ByVal Narration_v As String, _
              ByVal totamount_v As Double, ByVal Partyid_v As Double, ByVal cardid_v As Double, ByVal Trntype_v As String, _
              ByVal Uid_v As Double, ByVal Despatchid_v As Double, Optional ByVal Cashrecvd_v As Double = 0, _
              Optional ByVal Deliverytoid_v As Double = 0) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_HEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = ItemType_v
        cmd.Parameters.Add("@CUSTOMER", SqlDbType.VarChar).Value = Customer_v
        cmd.Parameters.Add("@REFRENCE", SqlDbType.VarChar).Value = Refernce_v
        cmd.Parameters.Add("@CUSTNO", SqlDbType.VarChar).Value = Custno_v
        If Locationid_v = 0 Then
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = IIf(IsDBNull(Locationid_v), DBNull.Value, Locationid_v)
        End If

        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@CARDID", SqlDbType.Float).Value = IIf(cardid_v = 0, DBNull.Value, cardid_v)
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_v = 0, DBNull.Value, Partyid_v)
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = totamount_v
        cmd.Parameters.Add("@TRNTYPE", SqlDbType.VarChar).Value = Trntype_v
        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = Uid_v
        cmd.Parameters.Add("@DELIVERYTOID", SqlDbType.Float).Value = IIf(Deliverytoid_v = 0, DBNull.Value, Deliverytoid_v)
        cmd.Parameters.Add("@CASHRECVD", SqlDbType.Float).Value = Cashrecvd_v

        If Despatchid_v = 0 Then
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = IIf(IsDBNull(Despatchid_v), DBNull.Value, Despatchid_v)
        End If

        'DESPATCHID
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function



    Public Function GensaveRetailHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
            ByVal Vchdate_v As Date, ByVal ItemType_v As String, ByVal Customer_v As String, ByVal Refernce_v As String, _
            ByVal Custno_v As String, ByVal Locationid_v As Double, ByVal compid_v As Double, ByVal Narration_v As String, _
            ByVal totamount_v As Double, ByVal Partyid_v As Double, ByVal cardid_v As Double, ByVal Trntype_v As String, _
            ByVal Uid_v As Double, ByVal Despatchid_v As Double, Optional ByVal Cashrecvd_v As Double = 0, _
            Optional ByVal deliverytoid_v As Double = 0) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_HEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = ItemType_v
        cmd.Parameters.Add("@CUSTOMER", SqlDbType.NVarChar).Value = Customer_v
        cmd.Parameters.Add("@REFRENCE", SqlDbType.VarChar).Value = Refernce_v
        cmd.Parameters.Add("@CUSTNO", SqlDbType.VarChar).Value = Custno_v
        If Locationid_v = 0 Then
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = IIf(IsDBNull(Locationid_v), DBNull.Value, Locationid_v)
        End If

        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@CARDID", SqlDbType.Float).Value = IIf(cardid_v = 0, DBNull.Value, cardid_v)
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_v = 0, DBNull.Value, Partyid_v)
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = totamount_v
        cmd.Parameters.Add("@TRNTYPE", SqlDbType.VarChar).Value = Trntype_v
        cmd.Parameters.Add("@UID", SqlDbType.VarChar).Value = Uid_v
        cmd.Parameters.Add("@DELIVERYTOID", SqlDbType.Float).Value = IIf(deliverytoid_v = 0, DBNull.Value, deliverytoid_v)
        cmd.Parameters.Add("@CASHRECVD", SqlDbType.Float).Value = Cashrecvd_v

        If Despatchid_v = 0 Then
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = IIf(IsDBNull(Despatchid_v), DBNull.Value, Despatchid_v)
        End If

        'DESPATCHID
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function

    Public Function GensaveQuotationHead2(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
              ByVal Vchdate_v As Date, ByVal Partyid_v As Double, ByVal Despatchid_v As Double, ByVal Narration_v As String, _
              ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_HEADER2_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        'cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = ItemType_v
        'cmd.Parameters.Add("@CUSTOMER", SqlDbType.VarChar).Value = Customer_v
        'cmd.Parameters.Add("@REFRENCE", SqlDbType.VarChar).Value = Refernce_v
        'cmd.Parameters.Add("@CUSTNO", SqlDbType.VarChar).Value = Custno_v
        'If Locationid_v = 0 Then
        ' cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = DBNull.Value
        ' Else
        ' cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = IIf(IsDBNull(Locationid_v), DBNull.Value, Locationid_v)
        ' End If
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        'cmd.Parameters.Add("@CARDID", SqlDbType.Float).Value = IIf(cardid_v = 0, DBNull.Value, cardid_v)
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_v = 0, DBNull.Value, Partyid_v)
        'cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = totamount_v
        'cmd.Parameters.Add("@TRNTYPE", SqlDbType.VarChar).Value = Trntype_v
        cmd.Parameters.Add("@UID", SqlDbType.Int).Value = Genuid
        'cmd.Parameters.Add("@CASHRECVD", SqlDbType.Float).Value = Cashrecvd_v
        If Despatchid_v = 0 Then
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@DESPATCHID", SqlDbType.Float).Value = IIf(IsDBNull(Despatchid_v), DBNull.Value, Despatchid_v)
        End If

        'DESPATCHID
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function

    Public Function GensaveSalesHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
              ByVal Vchdate_v As Date, ByVal ItemType_v As String, ByVal Customer_v As String, ByVal Refernce_v As String, _
              ByVal Custno_v As String, ByVal Locationid_v As Double, ByVal compid_v As Double, ByVal Narration_v As String, _
              ByVal totamount_v As Double, ByVal Partyid_v As Double, ByVal cardid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SALESRETURN_HEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = ItemType_v
        cmd.Parameters.Add("@CUSTOMER", SqlDbType.VarChar).Value = Customer_v
        cmd.Parameters.Add("@REFRENCE", SqlDbType.VarChar).Value = Refernce_v
        cmd.Parameters.Add("@CUSTNO", SqlDbType.VarChar).Value = Custno_v
        cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = Locationid_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@CARDID", SqlDbType.Float).Value = IIf(cardid_v = 0, DBNull.Value, cardid_v)
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_v = 0, DBNull.Value, Partyid_v)
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = totamount_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function

    Public Function GensavePurchaseHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
          ByVal Vchdate_v As Date, ByVal Partyid_v As String, ByVal Refernce_v As String, ByVal Pdcdate_v As String, _
          ByVal Pdcno_v As String, ByVal compid_v As Double, ByVal Narration_v As String, _
          ByVal Netamount_v As Double, ByVal Vehicleno_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASEHEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        'cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = Locationid_v
        cmd.Parameters.Add("@PDCNO", SqlDbType.VarChar).Value = Pdcno_v
        cmd.Parameters.Add("@PDCDATE", SqlDbType.DateTime).Value = IIf(Pdcdate_v Is Nothing, DBNull.Value, Pdcdate_v)
        cmd.Parameters.Add("@VEHICLENO", SqlDbType.VarChar).Value = Vehicleno_v
        cmd.Parameters.Add("@REFERENCE", SqlDbType.VarChar).Value = Refernce_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@NETAMOUNT", SqlDbType.Float).Value = Netamount_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@HEADERID").Value)
        Return Idkey
    End Function

    Public Function GensaveItemAddLessHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
              ByVal Vchdate_v As Date, ByVal Reference_v As String, ByVal Narration_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMADDDEDUCT_HEADER_SAVEUPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@REFERENCE", SqlDbType.VarChar).Value = Reference_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Gencompid
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@HEADERID").Value)
        Return Idkey
    End Function



    Public Function GensavePurchaseReturnHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
          ByVal Vchdate_v As Date, ByVal Partyid_v As String, ByVal Refernce_v As String, ByVal Pdcdate_v As String, _
          ByVal Pdcno_v As String, ByVal Locationid_v As Double, ByVal compid_v As Double, ByVal Narration_v As String, _
          ByVal Netamount_v As Double, ByVal Vehicleno_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASERETURNHEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = Locationid_v
        cmd.Parameters.Add("@PDCNO", SqlDbType.VarChar).Value = Pdcno_v
        cmd.Parameters.Add("@PDCDATE", SqlDbType.DateTime).Value = IIf(Pdcdate_v Is Nothing, DBNull.Value, Pdcdate_v)
        cmd.Parameters.Add("@VEHICLENO", SqlDbType.VarChar).Value = Vehicleno_v
        cmd.Parameters.Add("@REFERENCE", SqlDbType.VarChar).Value = Refernce_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@NETAMOUNT", SqlDbType.Float).Value = Netamount_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@HEADERID").Value)
        Return Idkey

    End Function
    Public Function GensaveOrderHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
             ByVal Vchdate_v As Date, ByVal Customer_v As String, ByVal Refernce_v As String, _
             ByVal compid_v As Double, ByVal Narration_v As String, _
             ByVal totamount_v As Double, ByVal Partyid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ORDERHEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@CUSTOMER", SqlDbType.VarChar).Value = Customer_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(Partyid_v = 0, DBNull.Value, Partyid_v)
        cmd.Parameters.Add("@NETAMOUNT", SqlDbType.Float).Value = totamount_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function


    Public Function GensaveInvoiceHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
                                       ByVal Vchdate_v As DateTime, ByVal compid_v As Double, ByVal Partyid_v As Double, _
                                       ByVal Narration_v As String, ByVal Totamount_v As Double, ByVal Locationid_v As Double, _
                                       ByVal Lineid_v As Double, ByVal Reference_v As String, ByVal Vehicleno_v As String, _
                                       ByVal Addlessamt_v As Double, ByVal Totdiscamt_v As Double, ByVal OutStndAmt_v As Double, _
                                       ByVal Transportid_v As Double, ByVal Shippedtoid_v As Double, ByVal Pono_v As String, _
                                       ByVal Ewaybillno_v As String, ByVal lrno_v As String, ByVal lrdate_v As String, _
                                       ByVal noofbundles_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "INVOICEHEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@VCHNUM", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@VCHDATE", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        cmd.Parameters.Add("@NARRATION", SqlDbType.VarChar).Value = Narration_v
        cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = IIf(Locationid_v = 0, DBNull.Value, Locationid_v)
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = IIf(Lineid_v = 0, DBNull.Value, Lineid_v)
        cmd.Parameters.Add("@REFERENCE", SqlDbType.VarChar).Value = Reference_v
        cmd.Parameters.Add("@VEHICLENO", SqlDbType.VarChar).Value = Vehicleno_v
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = Totamount_v
        cmd.Parameters.Add("@Addlessamount", SqlDbType.Float).Value = Addlessamt_v
        cmd.Parameters.Add("@Totdiscamount", SqlDbType.Float).Value = Totdiscamt_v
        cmd.Parameters.Add("@Outstandamount", SqlDbType.Float).Value = OutStndAmt_v
        cmd.Parameters.Add("@TRANSPORTID", SqlDbType.Float).Value = IIf(Transportid_v = 0, DBNull.Value, Transportid_v)
        cmd.Parameters.Add("@SHIPPEDTOID", SqlDbType.Float).Value = IIf(Shippedtoid_v = 0, DBNull.Value, Shippedtoid_v)
        cmd.Parameters.Add("@PONO", SqlDbType.VarChar).Value = Pono_v
        cmd.Parameters.Add("@EWAYBILLNO", SqlDbType.VarChar).Value = Ewaybillno_v
        cmd.Parameters.Add("@LRNO", SqlDbType.VarChar).Value = lrno_v
        cmd.Parameters.Add("@lrdate", SqlDbType.DateTime).Value = IIf(lrdate_v Is Nothing, DBNull.Value, lrdate_v)
        cmd.Parameters.Add("@NOOFBUNDLES", SqlDbType.Float).Value = noofbundles_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function


    Public Function GensaveBillTotHead(ByVal editflag_v As SaveOption, ByVal Headerid_v As Double, ByVal Vchnum_v As String, _
                                       ByVal Vchdate_v As DateTime, ByVal compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "BILLTOTAL_HEADER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@HEADERID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Headerid_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = compid_v
        cmd.Parameters.Add("@VCHNUM", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@VCHDATE", SqlDbType.DateTime).Value = Vchdate_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Headerid").Value)
        Return Idkey
    End Function

    Public Function GensaveOrderdetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
                 ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
                 ByVal Qty_v As Double, ByVal remarks_v As String, ByVal uomid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ORDERDETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@uomid", SqlDbType.Float).Value = uomid_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.ExecuteNonQuery()
    End Function


    Public Function GensaveQuotationdetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
                 ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
                ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Costrate_v As Double, ByVal groupid_v As Double, Optional ByVal Mrprate_v As Double = 0) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_DETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@COSTRATE", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.Parameters.Add("@Mrprate", SqlDbType.Float).Value = Mrprate_v
        cmd.Parameters.Add("@cgstperc", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@freeqty", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@DISCPERC", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@DISCAMOUNT", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@forqty", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@cgstamount", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@sgstperc", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@sgstamount", SqlDbType.Float).Value = DBNull.Value
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = DBNull.Value
        If groupid_v = 0 Then
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = IIf(IsDBNull(groupid_v), DBNull.Value, groupid_v)
        End If
        cmd.ExecuteNonQuery()
    End Function


    Public Function GensaveRetaildetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
                ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
               ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Costrate_v As Double, ByVal groupid_v As Double, _
               ByVal cgstperc_v As Double, ByVal cgstamount_v As Double, ByVal sgstperc_v As Double, ByVal sgstamount_v As Double, _
               ByVal Freeqty_v As Double, ByVal ForQty_v As Double, ByVal Discperc_v As Double, ByVal DiscAmount_v As Double, ByVal TotalAmount_v As Double, _
               Optional ByVal Mrprate_v As Double = 0) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_DETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@COSTRATE", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.Parameters.Add("@Mrprate", SqlDbType.Float).Value = Mrprate_v
        cmd.Parameters.Add("@cgstperc", SqlDbType.Float).Value = cgstperc_v
        cmd.Parameters.Add("@freeqty", SqlDbType.Float).Value = Freeqty_v
        cmd.Parameters.Add("@DISCPERC", SqlDbType.Float).Value = Discperc_v
        cmd.Parameters.Add("@DISCAMOUNT", SqlDbType.Float).Value = DiscAmount_v
        cmd.Parameters.Add("@forqty", SqlDbType.Float).Value = ForQty_v
        cmd.Parameters.Add("@cgstamount", SqlDbType.Float).Value = cgstamount_v
        cmd.Parameters.Add("@sgstperc", SqlDbType.Float).Value = sgstperc_v
        cmd.Parameters.Add("@sgstamount", SqlDbType.Float).Value = sgstamount_v
        cmd.Parameters.Add("@TOTAMOUNT", SqlDbType.Float).Value = TotalAmount_v
        If groupid_v = 0 Then
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = IIf(IsDBNull(groupid_v), DBNull.Value, groupid_v)
        End If
        cmd.ExecuteNonQuery()
    End Function


    Public Function GensaveQuotationdetl2(ByVal Headerid_v As Double, ByVal lineid_v As Double, _
                 ByVal rate_v As Double, ByVal Itemid_v As Double, _
                ByVal remarks_v As String, ByVal groupid_v As Double, ByVal Qty_t As Double, ByVal Amount_t As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_DETAIL2_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Gencompid
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@qty", SqlDbType.Float).Value = Qty_t
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_t
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        If groupid_v = 0 Then
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@groupid", SqlDbType.Float).Value = IIf(IsDBNull(groupid_v), DBNull.Value, groupid_v)
        End If
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensaveSalesdetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
                 ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
                 ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Costrate_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SALESRETURN_DETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@COSTRATE", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensavePurchasedetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
               ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
               ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Costrate_v As Double, ByVal Locationid_v As Double, _
               ByVal Hsnid_v As Double, Optional ByVal Cgstperc_v As Double = 0, Optional ByVal Cgstamt_v As Double = 0, Optional ByVal Sgstperc_v As Double = 0, _
               Optional ByVal Sgstamt_v As Double = 0) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASE_DETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@COSTRATE", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = Locationid_v
        cmd.Parameters.Add("@HSNID", SqlDbType.Float).Value = IIf(Hsnid_v = 0, DBNull.Value, Hsnid_v)
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.Parameters.Add("@Cgstperc", SqlDbType.Float).Value = Cgstperc_v
        cmd.Parameters.Add("@Cgstamt", SqlDbType.Float).Value = Cgstamt_v
        cmd.Parameters.Add("@Sgstperc", SqlDbType.Float).Value = Sgstperc_v
        cmd.Parameters.Add("@Sgstamt", SqlDbType.Float).Value = Sgstamt_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensaveItemAddDeductdetl(ByVal Headerid_v As Double, ByVal lineid_v As Double, _
              ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
              ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Locationid_v As Double, ByVal Uomid_v As Double, _
              ByVal Type_v As String, ByVal Selrate_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMADDDEDUCT_DETAIL_SAVEUPD"
        cmd.Parameters.Add("@HEADERID", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@RATE", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@UOMID", SqlDbType.Float).Value = Uomid_v
        cmd.Parameters.Add("@TYPE", SqlDbType.VarChar).Value = Type_v
        cmd.Parameters.Add("@QTY", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@LOCATIONID", SqlDbType.Float).Value = Locationid_v
        cmd.Parameters.Add("@SELLRATE", SqlDbType.Float).Value = Selrate_v
        cmd.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = remarks_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensavePurchaseReturndetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal lineid_v As Double, _
             ByVal rate_v As Double, ByVal Amount_v As Double, ByVal Compid_v As Double, ByVal Itemid_v As Double, _
             ByVal Qty_v As Double, ByVal remarks_v As String, ByVal Costrate_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASERETURN_DETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@Vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@Vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@COSTRATE", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@Qty", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = remarks_v
        cmd.ExecuteNonQuery()
    End Function



    Public Function GensaveHSNAccode(ByVal editflag_v As SaveOption, ByVal Masterid_v As Double, ByVal State_v As String, _
                       ByVal Code_v As String, ByVal Cgst_v As Double, sgst_v As Double, Igst_v As Double) As Double
        Try
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "HSNACCOUNTCODE_MASTER_SAVEUPD"
            cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

            Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Masterid", SqlDbType.Int)
            myparm16.Direction = ParameterDirection.InputOutput
            myparm16.Value = Masterid_v

            cmd.Parameters.Add("@hsncode", SqlDbType.VarChar).Value = State_v
            cmd.Parameters.Add("@decription", SqlDbType.VarChar).Value = Code_v
            cmd.Parameters.Add("@CGST", SqlDbType.Float).Value = Cgst_v
            cmd.Parameters.Add("@IGST", SqlDbType.Float).Value = Igst_v
            cmd.Parameters.Add("@SGST", SqlDbType.Float).Value = sgst_v
            cmd.ExecuteNonQuery()
            Idkey = System.Convert.ToInt32(cmd.Parameters("@Masterid").Value)
            Return Idkey
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Function GendelHsnaccode(ByVal Masterid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "HSNCODEDELETEUPD"
        cmd.Parameters.Add("@MASTERID", SqlDbType.Float).Value = Masterid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function Gensaveinvoicedetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal Compid_v As Double, _
                                         ByVal lineid_v As Double, ByVal Itemid_v As Double, ByVal Uomid_v As Double, ByVal Qty_v As Double, _
                                         ByVal Rate_v As Double, ByVal Exrate_v As Double, ByVal vatamount_v As Double, _
                                         ByVal Remarks_v As String, ByVal Vatperc_v As Double, ByVal Amount_v As Double, ByVal Costrate_v As Double, _
                                         ByVal Discperc_v As Double, ByVal Discamt_v As Double, ByVal Free_v As Double, _
                                         ByVal Freeitem_v As Double, ByVal ForQty_v As Double, ByVal hsnaccountingcode_v As String, _
                                         ByVal cgstperc_v As Double, ByVal cgstamount_v As Double, ByVal sgstperc_v As Double, _
                                         ByVal sgstamount_v As Double, ByVal igstperc_v As Double, ByVal igstamount_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "INVOICEDETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@itemid", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@uomid", SqlDbType.Float).Value = Uomid_v
        cmd.Parameters.Add("@QTY", SqlDbType.Float).Value = Qty_v
        cmd.Parameters.Add("@rate", SqlDbType.Float).Value = Rate_v
        cmd.Parameters.Add("@amount", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@VATPERC", SqlDbType.Float).Value = Vatperc_v
        cmd.Parameters.Add("@EXRATE", SqlDbType.Float).Value = Exrate_v
        cmd.Parameters.Add("@VATAMOUNT", SqlDbType.Float).Value = vatamount_v
        cmd.Parameters.Add("@costrate", SqlDbType.Float).Value = Costrate_v
        cmd.Parameters.Add("@remarks", SqlDbType.VarChar).Value = Remarks_v
        cmd.Parameters.Add("@vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@Discperc", SqlDbType.Float).Value = Discperc_v
        cmd.Parameters.Add("@Discamount", SqlDbType.Float).Value = Discamt_v
        cmd.Parameters.Add("@Freeqty", SqlDbType.Float).Value = Free_v
        cmd.Parameters.Add("@Freeitem", SqlDbType.Float).Value = Freeitem_v
        cmd.Parameters.Add("@Forqty", SqlDbType.Float).Value = ForQty_v
        cmd.Parameters.Add("@cgstamount", SqlDbType.Float).Value = cgstamount_v
        cmd.Parameters.Add("@cgstperc", SqlDbType.Float).Value = cgstperc_v
        cmd.Parameters.Add("@igstamount", SqlDbType.Float).Value = igstamount_v
        cmd.Parameters.Add("@igstperc", SqlDbType.Float).Value = igstperc_v
        cmd.Parameters.Add("@sgstamount", SqlDbType.Float).Value = sgstamount_v
        cmd.Parameters.Add("@sgstperc", SqlDbType.Float).Value = sgstperc_v
        cmd.Parameters.Add("@hsnaccountingcode", SqlDbType.VarChar).Value = hsnaccountingcode_v

        cmd.ExecuteNonQuery()
    End Function


    Public Function GensaveBillTotdetl(ByVal Headerid_v As Double, ByVal Vchnum_v As String, ByVal Vchdate_v As Date, ByVal Compid_v As Double, _
                                         ByVal lineid_v As Double, ByVal BillAmount_v As Double, _
                                         ByVal RcvdAmount_v As Double, ByVal BillNo_v As String, ByVal PartyId_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "BILLDETAIL_SAVE_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = lineid_v
        cmd.Parameters.Add("@BILLAMOUNT", SqlDbType.Float).Value = BillAmount_v
        cmd.Parameters.Add("@RCVDAMOUNT", SqlDbType.VarChar).Value = RcvdAmount_v
        cmd.Parameters.Add("@vchnum", SqlDbType.VarChar).Value = Vchnum_v
        cmd.Parameters.Add("@billno", SqlDbType.VarChar).Value = BillNo_v
        cmd.Parameters.Add("@vchdate", SqlDbType.DateTime).Value = Vchdate_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = IIf(PartyId_v = 0, DBNull.Value, PartyId_v)
        cmd.ExecuteNonQuery()
    End Function
    Public Function Gensaveinvoiceaddlessdetl(ByVal Headerid_v As Double, ByVal Lineid_v As Double, ByVal Descid_v As Double, ByVal Perc_v As Double, _
                                              ByVal Amount_v As Double, ByVal Altype_v As String, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "INVOICE_ADDLESS_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = Descid_v
        cmd.Parameters.Add("@perc", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.ExecuteNonQuery()
    End Function


    Public Function GensaveQUOTATIONaddlessdetl(ByVal Headerid_v As Double, ByVal Lineid_v As Double, ByVal Descid_v As Double, ByVal Perc_v As Double, _
                                             ByVal Amount_v As Double, ByVal Altype_v As String, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATION_ADDLESS_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = Descid_v
        cmd.Parameters.Add("@perc", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensaveSALESaddlessdetl(ByVal Headerid_v As Double, ByVal Lineid_v As Double, ByVal Descid_v As Double, ByVal Perc_v As Double, _
                                           ByVal Amount_v As Double, ByVal Altype_v As String, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SALESRETURN_ADDLESS_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = Descid_v
        cmd.Parameters.Add("@perc", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.ExecuteNonQuery()
    End Function


    Public Function GensavePURCHASEaddlessdetl(ByVal Headerid_v As Double, ByVal Lineid_v As Double, ByVal Descid_v As Double, ByVal Perc_v As Double, _
                                           ByVal Amount_v As Double, ByVal Altype_v As String, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASE_ADDLESS_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = Descid_v
        cmd.Parameters.Add("@perc", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensavePURCHASEREtaddlessdetl(ByVal Headerid_v As Double, ByVal Lineid_v As Double, ByVal Descid_v As Double, ByVal Perc_v As Double, _
                                          ByVal Amount_v As Double, ByVal Altype_v As String, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASERETURN_ADDLESS_UPD"
        cmd.Parameters.Add("@Headerid", SqlDbType.Float).Value = Headerid_v
        cmd.Parameters.Add("@compid", SqlDbType.Float).Value = Compid_v
        cmd.Parameters.Add("@lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = Descid_v
        cmd.Parameters.Add("@perc", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = Amount_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensaveState(ByVal editflag_v As SaveOption, ByVal Masterid_v As Double, ByVal State_v As String, _
                     ByVal Code_v As String) As Double
        Try
            cmd = Nothing
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "STATEUPD"
            cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

            Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Masterid", SqlDbType.Int)
            myparm16.Direction = ParameterDirection.InputOutput
            myparm16.Value = Masterid_v

            cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = State_v
            cmd.Parameters.Add("@CODE", SqlDbType.VarChar).Value = Code_v
            cmd.ExecuteNonQuery()
            Idkey = System.Convert.ToInt32(cmd.Parameters("@Masterid").Value)
            Return Idkey
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Function GendelState(ByVal Masterid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "STATEDELETEUPD"
        cmd.Parameters.Add("@MASTERID", SqlDbType.Float).Value = Masterid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function Gensaveparty(ByVal editflag_v As SaveOption, ByVal Partyid_v As Double, ByVal Partyname_v As String, _
                            ByVal Partytype_v As String, ByVal Add1_v As String, ByVal Add2_v As String, ByVal Add3_v As String, _
                            ByVal Add4_v As String, ByVal Place_v As String, ByVal Stateid_v As Double, _
                            ByVal Phone1_v As String, ByVal Phone2_v As String, ByVal Tin_v As String, _
                            ByVal Cst_v As String, ByVal Cstdate_v As Date, ByVal Cell_v As String, _
                            ByVal Email_v As String, ByVal Code_v As String, ByVal Compid_v As Double, _
                            ByVal Lineid_v As Double, ByVal CreditAmount_v As Double, ByVal Gstin_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "Partyupd"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Partyid", SqlDbType.Int)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Partyid_v

        cmd.Parameters.Add("@PartyName", SqlDbType.NVarChar).Value = Partyname_v
        cmd.Parameters.Add("@Partytype", SqlDbType.VarChar).Value = Partytype_v
        cmd.Parameters.Add("@add1", SqlDbType.NVarChar).Value = Add1_v
        cmd.Parameters.Add("@add2", SqlDbType.NVarChar).Value = Add2_v
        cmd.Parameters.Add("@add3", SqlDbType.NVarChar).Value = Add3_v
        cmd.Parameters.Add("@add4", SqlDbType.NVarChar).Value = Add4_v
        cmd.Parameters.Add("@Place", SqlDbType.NVarChar).Value = Place_v
        cmd.Parameters.Add("@Stateid", SqlDbType.Float).Value = IIf(Stateid_v = 0, DBNull.Value, Stateid_v)
        cmd.Parameters.Add("@Phone1", SqlDbType.VarChar).Value = Phone1_v
        cmd.Parameters.Add("@Phone2", SqlDbType.VarChar).Value = Phone2_v
        cmd.Parameters.Add("@Tin", SqlDbType.VarChar).Value = Tin_v
        cmd.Parameters.Add("@Cst", SqlDbType.VarChar).Value = Cst_v
        cmd.Parameters.Add("@Cstdate", SqlDbType.DateTime).Value = IIf(Cstdate_v = Nothing, DBNull.Value, Cstdate_v)
        cmd.Parameters.Add("@Cell", SqlDbType.VarChar).Value = Cell_v
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email_v
        cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = Code_v
        cmd.Parameters.Add("@gstin", SqlDbType.VarChar).Value = Gstin_v
        cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
        cmd.Parameters.Add("@LINEID", SqlDbType.Float).Value = IIf(Lineid_v = 0, DBNull.Value, Lineid_v)
        cmd.Parameters.Add("@CREDITLIMIT", SqlDbType.Float).Value = CreditAmount_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Partyid").Value)
        Return Idkey
    End Function

    Public Function GensavePartyAddlessdetl(ByVal Partyid_v As Double, ByVal Altype_v As String, _
                     ByVal Descid_v As Double, ByVal Perc_v As Double, ByVal Lineid_v As Double, ByVal Remarks_v As String)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "PARTY_ADDLESSUPD"
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        cmd.Parameters.Add("@ALTYPE", SqlDbType.VarChar).Value = Altype_v
        cmd.Parameters.Add("@DESCID", SqlDbType.Float).Value = IIf(Descid_v = 0, DBNull.Value, Descid_v)
        cmd.Parameters.Add("@PERCENTAGE", SqlDbType.Float).Value = Perc_v
        cmd.Parameters.Add("@AMOUNT", SqlDbType.Float).Value = 0
        cmd.Parameters.Add("@Lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensavePartyDeptdetl(ByVal Lineid_v As Double, ByVal Partyid_v As Double, ByVal Deptid_v As Double, ByVal ContactPerson_v As String, ByVal Mobno_v As String, _
                                         ByVal Email_v As String, ByVal Remarks_v As String)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "PARTY_DEPT_DETL_UPD"
        cmd.Parameters.Add("@Lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        cmd.Parameters.Add("@DEPTID", SqlDbType.Float).Value = Deptid_v
        cmd.Parameters.Add("@CONTACTPERSON", SqlDbType.VarChar).Value = ContactPerson_v
        cmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = Mobno_v
        cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = Email_v
        cmd.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = Remarks_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function Gendelparty(ByVal Partyid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Partydeleteupd"
        cmd.Parameters.Add("@Partyid", SqlDbType.Float).Value = Partyid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelInvoice(ByVal Invoiceid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "INVOICEDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Invoiceid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelBillTot(ByVal Invoiceid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "BILLTOTDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Invoiceid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelQuotation(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATIONDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelQuotation2(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "QUOTATIONDELETEUPD2"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelSales(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "SALESRETURNDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelPurchase(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASEDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelPurchaseReturn(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "PURCHASERETURNDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelOrder(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ORDERDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GensaveUom(ByVal editflag_v As SaveOption, ByVal Uomid_v As Double, ByVal NoofDecimal As Double, _
                               ByVal Uom_v As String, ByVal Tamiluom_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "UOM_MASTER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@masterID", SqlDbType.Int)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Uomid_v

        cmd.Parameters.Add("@UOM", SqlDbType.VarChar).Value = Uom_v
        cmd.Parameters.Add("@NOOFDECIMAL", SqlDbType.Float).Value = NoofDecimal
        cmd.Parameters.Add("@TAMILUOM", SqlDbType.NVarChar).Value = Tamiluom_v

        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@masterID").Value)
        Return Idkey
    End Function

    Public Function GendelUom(ByVal Uomid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "GENERAL_ITEM_DELETE"
        cmd.Parameters.Add("@MASTERID", SqlDbType.Float).Value = Uomid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelItemAddDeduct(ByVal Headerid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMADDLESSDELETEUPD"
        cmd.Parameters.Add("@headerid", SqlDbType.Float).Value = Headerid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelItem(ByVal Itemid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMDELETEUPD"
        cmd.Parameters.Add("@itemid", SqlDbType.Float).Value = Itemid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelItem2(ByVal Itemid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMMASTER2DELETEUPD"
        cmd.Parameters.Add("@itemid", SqlDbType.Float).Value = Itemid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GendelCard(ByVal Cardid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "CARDDELETEUPD"
        cmd.Parameters.Add("@cardid", SqlDbType.Float).Value = Cardid_v
        cmd.ExecuteNonQuery()
    End Function

    Public Sub Gridcaption(ByVal Process_v As String)
        tmpsqlstr = "SELECT PROCESS,STRINGVALUE FROM SETTINGS WHERE PROCESS='" & Process_v & "'"
        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        ds_Gridcptn = New DataSet
        ds_Gridcptn.Clear()
        tmpda.Fill(ds_Gridcptn)

        If ds_Gridcptn.Tables(0).Rows.Count <> 0 Then
            Gridcaption_t = ds_Gridcptn.Tables(0).Rows(0).Item("STRINGVALUE")
        End If
    End Sub

    Public Function Gensaveaccount(ByVal editflag_v As SaveOption, ByVal Accid_v As Double, ByVal Accname_v As String, _
                                 ByVal Accgroup_v As String) As Long
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "Accountupd"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)


        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Accountid", SqlDbType.Int)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Accid_v
        cmd.Parameters.Add("@AccName", SqlDbType.VarChar).Value = Accname_v
        cmd.Parameters.Add("@Accgroup", SqlDbType.VarChar).Value = Accgroup_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Accountid").Value)
        Return Idkey
    End Function

    Public Function GensaveCompany(ByVal editflag_v As SaveOption, ByVal Compid_v As Double, ByVal Compname_v As String, _
                             ByVal Add1_v As String, ByVal Add2_v As String, ByVal Add3_v As String, _
                             ByVal Add4_v As String, ByVal Phone_v As String, ByVal Tngst_v As String, ByVal Cst_v As String, _
                             ByVal Cstdate_v As Date, ByVal Email_v As String, ByVal Prefix_v As String, ByVal No_v As Integer, _
                             ByVal Suffix_v As String, ByVal Noofdigit_v As Integer, ByVal Showcomplog_v As Boolean, _
                             ByVal Frommailid_v As String, ByVal Password_v As String, ByVal Gstin_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "COMPANYUPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Compid", SqlDbType.Int)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Compid_v

        cmd.Parameters.Add("@CompName", SqlDbType.NVarChar).Value = Compname_v
        cmd.Parameters.Add("@add1", SqlDbType.NVarChar).Value = Add1_v
        cmd.Parameters.Add("@add2", SqlDbType.NVarChar).Value = Add2_v
        cmd.Parameters.Add("@add3", SqlDbType.NVarChar).Value = Add3_v
        cmd.Parameters.Add("@add4", SqlDbType.NVarChar).Value = Add4_v
        cmd.Parameters.Add("@Phone", SqlDbType.VarChar).Value = Phone_v
        cmd.Parameters.Add("@Tin", SqlDbType.VarChar).Value = Tngst_v
        cmd.Parameters.Add("@Cst", SqlDbType.VarChar).Value = Cst_v
        cmd.Parameters.Add("@Cstdate", SqlDbType.DateTime).Value = IIf(Cstdate_v = Nothing, DBNull.Value, Cstdate_v)
        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email_v
        cmd.Parameters.Add("@Prefix", SqlDbType.VarChar).Value = Prefix_v
        cmd.Parameters.Add("@SeqNo", SqlDbType.Int).Value = No_v
        cmd.Parameters.Add("@Suffix", SqlDbType.VarChar).Value = Suffix_v
        cmd.Parameters.Add("@Noofdigits", SqlDbType.Int).Value = Noofdigit_v
        cmd.Parameters.Add("@Showcomplog", SqlDbType.Bit).Value = Showcomplog_v
        cmd.Parameters.Add("@Frommail", SqlDbType.VarChar).Value = Frommailid_v
        cmd.Parameters.Add("@Mailpassword", SqlDbType.VarChar).Value = Password_v
        cmd.Parameters.Add("@gstin", SqlDbType.VarChar).Value = Gstin_v
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Compid").Value)
        Return Idkey
    End Function

    Public Function GensaveItem(ByVal editflag_v As SaveOption, ByVal Itemid_v As Double, ByVal Itemcode_v As String, _
                       ByVal itemdes_v As String, ByVal itemtamildes_v As String, ByVal uomid_v As Double, ByVal Taxperc_v As Double, _
                       ByVal profitperc_v As Double, costperc_v As Double, mrprate_v As Double, selpriceretail_v As Double, _
                       ByVal selpricewhole_v As Double, ByVal groupid_v As Double, ByVal categoryid_v As Double, _
                       ByVal Rakeid_v As Double, ByVal Freeitem_v As Double, ByVal Freeqty_v As Double, ByVal forQty_v As Double, _
                       ByVal Remakrs_v As String, ByVal Itemtype_v As String, ByVal Inactive_v As Boolean, ByVal Pkgwt_v As String, _
                       ByVal OfferAddQty_v As Double, ByVal OfferLessQty_v As Double, ByVal AddQty_v As Double, LessQty_v As Double, _
                       ByVal hsnAccode_v As String, ByVal Hsnid_v As Double, ByVal MinStock_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEM_MASTER_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Itemid", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Itemid_v
        cmd.Parameters.Add("@ITEMCODE", SqlDbType.VarChar).Value = Itemcode_v
        cmd.Parameters.Add("@ITEMDES", SqlDbType.VarChar).Value = itemdes_v
        cmd.Parameters.Add("@ITEMTAMILDES", SqlDbType.NVarChar).Value = itemtamildes_v
        If uomid_v = 0 Then
            cmd.Parameters.Add("@UOMID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@UOMID", SqlDbType.Float).Value = IIf(IsDBNull(uomid_v), DBNull.Value, uomid_v)
        End If

        cmd.Parameters.Add("@TAXPERC", SqlDbType.Float).Value = Taxperc_v
        cmd.Parameters.Add("@PROFITPERC", SqlDbType.Float).Value = profitperc_v
        cmd.Parameters.Add("@COSTPRICE", SqlDbType.Float).Value = costperc_v
        cmd.Parameters.Add("@MRPRATE", SqlDbType.Float).Value = mrprate_v
        cmd.Parameters.Add("@SELPRICERETAIL", SqlDbType.Float).Value = selpriceretail_v
        cmd.Parameters.Add("@SELPRICEWHOLE", SqlDbType.Float).Value = selpricewhole_v
        cmd.Parameters.Add("@GROUPID", SqlDbType.Float).Value = groupid_v
        If categoryid_v = 0 Then
            cmd.Parameters.Add("@CATEGORYID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@CATEGORYID", SqlDbType.Float).Value = IIf(IsDBNull(categoryid_v), DBNull.Value, categoryid_v)
        End If
        If Rakeid_v = 0 Then
            cmd.Parameters.Add("@RAKEID", SqlDbType.Float).Value = DBNull.Value
        Else
            cmd.Parameters.Add("@RAKEID", SqlDbType.Float).Value = IIf(IsDBNull(Rakeid_v), DBNull.Value, Rakeid_v)
        End If

        cmd.Parameters.Add("@FREEITEM", SqlDbType.Float).Value = Freeitem_v
        cmd.Parameters.Add("@FREEQTY", SqlDbType.Float).Value = Freeqty_v
        cmd.Parameters.Add("@MODIFIEDTIME", SqlDbType.DateTime).Value = Now
        cmd.Parameters.Add("@MODIFIEDUID", SqlDbType.Int).Value = Genuid
        cmd.Parameters.Add("@FORQTY", SqlDbType.Float).Value = forQty_v
        cmd.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = Remakrs_v
        cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = Itemtype_v
        cmd.Parameters.Add("Inactive", SqlDbType.Bit).Value = Inactive_v
        cmd.Parameters.Add("Pkgwt", SqlDbType.VarChar).Value = Pkgwt_v
        cmd.Parameters.Add("@OFFERLESSQTY", SqlDbType.Float).Value = OfferLessQty_v
        cmd.Parameters.Add("@OFFERADDQTY", SqlDbType.Float).Value = OfferAddQty_v
        cmd.Parameters.Add("@ADDQTY", SqlDbType.Float).Value = AddQty_v
        cmd.Parameters.Add("@LESSQTY", SqlDbType.Float).Value = LessQty_v
        cmd.Parameters.Add("@HSNID", SqlDbType.Float).Value = IIf(Hsnid_v = 0, DBNull.Value, Hsnid_v)
        cmd.Parameters.Add("@HSNACCOUNTINGCODE", SqlDbType.VarChar).Value = hsnAccode_v
        cmd.Parameters.Add("@minstock", SqlDbType.Float).Value = MinStock_v
        'INACTIV
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Itemid").Value)
        Return Idkey
    End Function

    Public Function GensaveItem2(ByVal editflag_v As SaveOption, ByVal Itemid_v As Double, _
                       ByVal itemdes_v As String, costperc_v As Double, selpriceretail_v As Double, _
                       ByVal groupid_v As Double, ByVal Remakrs_v As String, ByVal Inactive_v As Boolean, ByVal Compid_v As Double) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "ITEMMASTERFORMAT2_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@Itemid", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Itemid_v
        'cmd.Parameters.Add("@ITEMCODE", SqlDbType.VarChar).Value = Itemcode_v
        cmd.Parameters.Add("@ITEMDESCRIPTION", SqlDbType.VarChar).Value = itemdes_v
        ' cmd.Parameters.Add("@ITEMTAMILDES", SqlDbType.NVarChar).Value = itemtamildes_v
        ' If uomid_v = 0 Then
        ' cmd.Parameters.Add("@UOMID", SqlDbType.Float).Value = DBNull.Value
        ' Else
        ' cmd.Parameters.Add("@UOMID", SqlDbType.Float).Value = IIf(IsDBNull(uomid_v), DBNull.Value, uomid_v)
        ' End If

        'cmd.Parameters.Add("@TAXPERC", SqlDbType.Float).Value = Taxperc_v
        'cmd.Parameters.Add("@PROFITPERC", SqlDbType.Float).Value = profitperc_v
        cmd.Parameters.Add("@COSTPRICE", SqlDbType.Float).Value = costperc_v
        'cmd.Parameters.Add("@MRPRATE", SqlDbType.Float).Value = mrprate_v
        cmd.Parameters.Add("@SELLPRICE", SqlDbType.Float).Value = selpriceretail_v
        'cmd.Parameters.Add("@SELPRICEWHOLE", SqlDbType.Float).Value = selpricewhole_v
        cmd.Parameters.Add("@GROUPID", SqlDbType.Float).Value = groupid_v
        cmd.Parameters.Add("@COMPID", SqlDbType.Float).Value = Compid_v
        'If categoryid_v = 0 Then
        ' cmd.Parameters.Add("@CATEGORYID", SqlDbType.Float).Value = DBNull.Value
        ' Else
        ' cmd.Parameters.Add("@CATEGORYID", SqlDbType.Float).Value = IIf(IsDBNull(categoryid_v), DBNull.Value, categoryid_v)
        ' End If
        'If Rakeid_v = 0 Then
        ' cmd.Parameters.Add("@RAKEID", SqlDbType.Float).Value = DBNull.Value
        ' Else
        ' cmd.Parameters.Add("@RAKEID", SqlDbType.Float).Value = IIf(IsDBNull(Rakeid_v), DBNull.Value, Rakeid_v)
        ' End If

        '        cmd.Parameters.Add("@FREEITEM", SqlDbType.Float).Value = Freeitem_v
        '        cmd.Parameters.Add("@FREEQTY", SqlDbType.Float).Value = Freeqty_v
        cmd.Parameters.Add("@MODIFIEDDATE", SqlDbType.DateTime).Value = Now
        cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Genuid
        '        cmd.Parameters.Add("@FORQTY", SqlDbType.Float).Value = forQty_v
        cmd.Parameters.Add("@REMARKS", SqlDbType.VarChar).Value = Remakrs_v
        '        cmd.Parameters.Add("@ITEMTYPE", SqlDbType.VarChar).Value = Itemtype_v
        cmd.Parameters.Add("@INACTIVE", SqlDbType.Bit).Value = Inactive_v
        '        cmd.Parameters.Add("Pkgwt", SqlDbType.VarChar).Value = Pkgwt_v
        'INACTIV
        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@Itemid").Value)
        Return Idkey
    End Function

    Public Function GensaveCard(ByVal editflag_v As SaveOption, ByVal Cardid_v As Double, ByVal Cardno_v As String, _
                                ByVal CardDate_v As Date, ByVal Name_v As String, ByVal Address1_v As String, _
                                ByVal Address2_v As String, ByVal address3_v As String, ByVal Address4_v As String, _
                                ByVal Mobileno_v As String, ByVal DOB_v As Date, ByVal WeddingDt_v As Date, _
                                ByVal Emaild_v As String, ByVal Notes_v As String, ByVal Opnpoint_v As Double, _
                                ByVal Addpoint_v As Double, ByVal Lesspoint_v As Double) As Double

        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "CARDNO_SAVE_UPD"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@CARDID", SqlDbType.Float)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = Cardid_v
        cmd.Parameters.Add("@CARDDATE", SqlDbType.DateTime).Value = IIf(CardDate_v = Nothing, DBNull.Value, CardDate_v)
        cmd.Parameters.Add("@CARDNO", SqlDbType.VarChar).Value = Cardno_v
        cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = Name_v
        cmd.Parameters.Add("@ADDRESS1", SqlDbType.VarChar).Value = Address1_v
        cmd.Parameters.Add("@ADDRESS2", SqlDbType.VarChar).Value = Address2_v
        cmd.Parameters.Add("@ADDRESS3", SqlDbType.VarChar).Value = address3_v
        cmd.Parameters.Add("@ADDRESS4", SqlDbType.VarChar).Value = Address4_v
        cmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = Mobileno_v
        cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = IIf(DOB_v = Nothing, DBNull.Value, DOB_v)
        cmd.Parameters.Add("@WEDDINGDT", SqlDbType.DateTime).Value = IIf(WeddingDt_v = Nothing, DBNull.Value, WeddingDt_v)
        cmd.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = Emaild_v
        cmd.Parameters.Add("@NOTES", SqlDbType.VarChar).Value = Notes_v
        cmd.Parameters.Add("@OPNPOINT", SqlDbType.Float).Value = Opnpoint_v
        cmd.Parameters.Add("@ADDPOINT", SqlDbType.Float).Value = Addpoint_v
        cmd.Parameters.Add("@LESSPOINT", SqlDbType.Float).Value = Lesspoint_v

        cmd.ExecuteNonQuery()
        Idkey = System.Convert.ToInt32(cmd.Parameters("@CARDID").Value)
        Return Idkey
    End Function


    Public Function GetEditFlag(ByVal SaveOption_v As SaveOption) As String
        Select Case SaveOption_v
            Case SaveOption.Insert
                GetEditFlag = "I"
            Case SaveOption.Update
                GetEditFlag = "U"
            Case SaveOption.Delete
                GetEditFlag = "D"
            Case Else
                GetEditFlag = ""
        End Select
    End Function

    Public Function CenterThisForm(ByVal formname As Form)
        With formname
            .Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (.Width / 2)
            .Top = (Screen.PrimaryScreen.Bounds.Height / 2 - 400) - (.Height / 2) + IIf(Not formname.MdiParent Is Nothing, 0, 400)
        End With
    End Function

    Public Function AutoNum(ByVal ProcName As String, Optional ByVal Addnumber As Boolean = False)
        tmpsqlstr = "SELECT isnull(Prefix, '') + REPLICATE('0', ISNULL(Noofdigit,0) - len(SEQNO)) + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno " _
                  & "FROM Autonumber WHERE process = '" & ProcName & "' And Compid = " & Gencompid & "  "
        'tmprs.Open("SELECT isnull(Prefix, '') + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno FROM Autonumber WHERE process = '" & ProcName & "'", conn, adOpenDynamic, adLockReadOnly)
        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        tmpds = New DataSet
        tmpds.Clear()
        tmpda.Fill(tmpds)
        AutoNum = tmpds.Tables(0).Rows(0).Item("Seqno").ToString

        If Addnumber Then
            tmpsqlstr = "UPDATE autonumber SET seqno = seqno + 1 WHERE process = '" & ProcName & "' And Compid = " & Gencompid & "  "
            'conn.EXECUTE("UPDATE autonumber SET seqno = seqno + 1 WHERE process = '" & ProcName & "'")
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            cmd.Transaction = trans
            cmd = New SqlCommand(tmpsqlstr, conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
        End If

    End Function

    Public Function AutoNum_type(ByVal ProcName As String, ByVal Prostype As String, Optional ByVal Prefix As String = "", Optional ByVal Addnumber As Boolean = False, Optional ByVal Suffix As String = "")
        tmpsqlstr = "SELECT isnull(Prefix, '') + REPLICATE('0', ISNULL(Noofdigit,0) - len(SEQNO)) + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno " _
                  & "FROM Autonumber WHERE process = '" & ProcName & "' And Isnull(Prefix,'') = '" & Prefix & "' And Isnull(Processtype,'') = '" & Prostype & "' And Isnull(Suffix,'') = '" & Suffix & "'  And Compid = " & Gencompid & "  "

        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        tmpds = New DataSet
        tmpds.Clear()
        tmpda.Fill(tmpds)
        AutoNum_type = tmpds.Tables(0).Rows(0).Item("Seqno").ToString

        If Addnumber Then
            tmpsqlstr = "UPDATE autonumber SET seqno = seqno + 1 WHERE process = '" & ProcName & "' And Isnull(Prefix,'') = '" & Prefix & "' And Isnull(Processtype,'') = '" & Prostype & "' And Isnull(Suffix,'') = '" & Suffix & "'  And Compid = " & Gencompid & "  "
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            cmd.Transaction = trans
            cmd = New SqlCommand(tmpsqlstr, conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
        End If

    End Function

    Public Function AutoNum_Party(ByVal ProcName As String, Optional ByVal Addnumber As Boolean = False)
        tmpsqlstr = "SELECT isnull(Prefix, '') + REPLICATE('0', 5 - len(SEQNO)) + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno FROM Party_Process WHERE process = '" & ProcName & "'"
        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        tmpds = New DataSet
        tmpds.Clear()
        tmpda.Fill(tmpds)
        AutoNum_Party = tmpds.Tables(0).Rows(0).Item("Seqno").ToString

        If Addnumber Then
            tmpsqlstr = "UPDATE Party_Process SET seqno = seqno + 1 WHERE process = '" & ProcName & "'"
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            cmd.Transaction = trans
            cmd = New SqlCommand(tmpsqlstr, conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
        End If
    End Function

    Public Function AutoNum_Company(ByVal Compid_v As Double, Optional ByVal Addnumber As Boolean = False)
        tmpsqlstr = "SELECT isnull(Prefix, '') + REPLICATE('0', ISNULL(Noofdigits,0) - len(SEQNO)) + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno FROM Company WHERE Compid = " & Compid_v & ""
        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        tmpds = New DataSet
        tmpds.Clear()
        tmpda.Fill(tmpds)
        AutoNum_Company = tmpds.Tables(0).Rows(0).Item("Seqno").ToString

        If Addnumber Then
            tmpsqlstr = "UPDATE Company SET seqno = seqno + 1 WHERE Compid = " & Compid_v & ""
            trans = conn.BeginTransaction(IsolationLevel.ReadCommitted)
            cmd.Transaction = trans
            cmd = New SqlCommand(tmpsqlstr, conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
        End If
    End Function

    Public Function AutoNum_Accs(ByVal ProcName As String, Optional ByVal Addnumber As Boolean = False)
        tmpsqlstr = "SELECT isnull(Prefix, '') + REPLICATE('0', ISNULL(Noofdigit,0) - len(SEQNO)) + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno " _
                  & "FROM Autonumber WHERE process = '" & ProcName & "' And Compid = " & Gencompid & "  "
        'tmprs.Open("SELECT isnull(Prefix, '') + cast(seqno as varchar(30)) + isnull(Suffix,'') as seqno FROM Autonumber WHERE process = '" & ProcName & "'", conn, adOpenDynamic, adLockReadOnly)

        cmd = New SqlCommand(tmpsqlstr, conn)
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        tmpda = New SqlDataAdapter(cmd)
        tmpds = New DataSet
        tmpds.Clear()
        tmpda.Fill(tmpds)
        AutoNum_Accs = tmpds.Tables(0).Rows(0).Item("Seqno").ToString

        If Addnumber Then
            tmpsqlstr = "UPDATE autonumber SET seqno = seqno + 1 WHERE process = '" & ProcName & "' And Compid = " & Gencompid & "  "
            'conn.EXECUTE("UPDATE autonumber SET seqno = seqno + 1 WHERE process = '" & ProcName & "'")
            'trans = conn.BeginTransaction(IsolationLevel.ReadUncommitted)
            'cmd.Transaction = trans
            cmd = New SqlCommand(tmpsqlstr, conn)
            cmd.Connection = conn
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.ExecuteNonQuery()
        End If
    End Function

    Public Sub gridreadonly(ByVal DGV As DataGridView, ByVal val As Boolean, ByVal ParamArray cols() As String)
        Dim i As Object
        For Each i In cols
            DGV.Columns(i).readonly = val
        Next
    End Sub

    Public Sub gridreadonly_Color(ByVal DGV As DataGridView, ByVal colorval As System.Drawing.Color, ByVal ParamArray cols() As String)
        Dim i As Object
        For Each i In cols
            DGV.Columns(i).DefaultCellStyle.BackColor = colorval
        Next
    End Sub

    Public Sub gridreadonly1(ByVal DGV As DataGridView, ByVal val As Boolean, ByVal ParamArray cols() As Integer)
        Dim i As Object
        For Each i In cols
            DGV.Columns(i).readonly = val
        Next
    End Sub

    Public Sub gridvisible(ByVal DGV As DataGridView, ByVal val As Boolean, ByVal ParamArray cols() As String)
        Dim i As Object
        For Each i In cols
            DGV.Columns(i).visible = val
        Next
    End Sub

    Public Function CheckEmptyControls(ByVal ParamArray Ctrl() As TextBox) As Boolean
        Dim C As TextBox
        CheckEmptyControls = False

        For Each C In Ctrl
            If C.Text = "" Then
                MsgBox("Cannot Save Empty Values", vbInformation + vbOKOnly, "Warning..")
                C.Focus()
                CheckEmptyControls = False
                Exit Function
            Else
                CheckEmptyControls = True
            End If
        Next
    End Function

    Public Function Tot_Calc(ByVal DGV As DataGridView, ByVal ColNo As Integer, Optional ByVal Rowcnt As Integer = 0) As Double
        Dim i As Integer, Rowcnt_t As Integer
        Dim TotValue As Double
        TotValue = 0
        If Rowcnt = 0 Then
            Rowcnt_t = DGV.Rows.Count
        Else
            Rowcnt_t = Rowcnt
        End If

        For i = 0 To Rowcnt_t - 1
            TotValue = TotValue + IIf(IsDBNull(DGV.Item(ColNo, i).Value), 0, DGV.Item(ColNo, i).Value)
        Next i
        Tot_Calc = TotValue
    End Function

    Public Sub UpdateAppSettings(ByVal KeyName As String, ByVal KeyValue As String)
        '  AppDomain.CurrentDomain.SetupInformation.ConfigurationFile 
        ' This will get the app.config file path from Current application Domain
        Dim XmlDoc As New XmlDocument
        ' Load XML Document
        XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
        ' Navigate Each XML Element of app.Config file
        For Each xElement As XmlElement In XmlDoc.DocumentElement
            If xElement.Name = "appSettings" Then
                ' Loop each node of appSettings Element 
                ' xNode.Attributes(0).Value , Mean First Attributes of Node , 
                ' KeyName Portion
                ' xNode.Attributes(1).Value , Mean Second Attributes of Node,
                ' KeyValue Portion
                For Each xNode As XmlNode In xElement.ChildNodes
                    If xNode.Attributes(1).Value = KeyName Then
                        xNode.Attributes(1).Value = KeyValue
                    End If
                Next
            End If
        Next
        ' Save app.config file
        'XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
    End Sub

    Public Sub getservername()

        'Dim SearchChar As String = ";"
        'Dim SearchChar1 As String = "="
        'Dim TestPos, Testpos1 As Integer
        'Dim r1 As Integer

        Dim r As String = (System.Configuration.ConfigurationSettings.AppSettings("Sqlconnection1").ToString)
        Databasename_t = (System.Configuration.ConfigurationSettings.AppSettings("Dbname").ToString)
        Appversion_t = (System.Configuration.ConfigurationManager.AppSettings("Appversion").ToString)

        'r1 = r.IndexOf("Server")

        'TestPos = InStr(1, r, SearchChar, CompareMethod.Text)
        'Testpos1 = InStr(1, r, SearchChar1, CompareMethod.Text)
        'If TestPos > 0 And Testpos1 > 0 Then
        ''Servername_t = Mid(r, Testpos1 + 1, TestPos - 1)
        'End If

        'Servername_t = "NCOMPUTING\SQL2005"
        Servername_t = r

    End Sub

    Public Sub CheckNumeric(ByRef txt_General As TextBox, Optional ByVal NoofDecimals As Integer = 0)
        If txt_General.Text = "" Then Exit Sub

        If Not IsNumeric(txt_General.Text) Then
            Select Case NoofDecimals 'IIf(NoofDecimals = 0, NoofDecimals_t, NoofDecimals)
                Case 0
                    txt_General.Text = "0"
                Case 2
                    txt_General.Text = "0.00"
                Case 3, -1
                    txt_General.Text = "0.000"
            End Select
        Else
            Select Case NoofDecimals 'IIf(NoofDecimals = 0, NoofDecimals_t, NoofDecimals)
                Case 0
                    'txt_General.Text = Format(txt_General.Text, "##########0")
                    txt_General.Text = CDbl(txt_General.Text).ToString("##########")
                Case 2
                    'txt_General.TEXT = Format(txt_General.TEXT, "##########0.00")
                    txt_General.Text = CDbl(txt_General.Text).ToString("##########.00")
                Case 3, -1
                    'txt_General.TEXT = Format(txt_General.TEXT, "##########0.000")
                    txt_General.Text = CDbl(txt_General.Text).ToString("##########.000")
            End Select
            If InStr(1, txt_General.Text, "-") > 0 Then
                txt_General.Text = "-" & Replace(txt_General.Text, "-", "")
                'txt_billamount.Text = CDbl(txt_billamount.Text).ToString("########.00")
            End If
        End If
    End Sub

    Public Sub Backupdata()
        Dim ReturnPath As String

        Dim connsting As String = ConfigurationSettings.AppSettings("SQLConnection").ToString()
        Dim sqlConnection1 As New SqlConnection(connsting)
        Dim cmd As New SqlCommand
        cmd.CommandText = "BACKUPUPD"
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Connection = sqlConnection1
        sqlConnection1.Open()

        Dim myparm16 As SqlParameter = cmd.Parameters.Add("@RETURN", SqlDbType.VarChar, 250)
        myparm16.Direction = ParameterDirection.InputOutput
        myparm16.Value = ""

        cmd.ExecuteNonQuery()
        ReturnPath = (cmd.Parameters("@RETURN").Value).ToString

        MsgBox("Backed up to " & ReturnPath & " on the server system.")
        sqlConnection1.Close()
    End Sub

    Public Sub AutoBackupdata()
        Try
            Dim ReturnPath As String

            Dim cmd As New SqlCommand
            cmd.CommandText = "AUTOBACKUPUPD"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = conn
            conn.Open()

            Dim myparm16 As SqlParameter = cmd.Parameters.Add("@RETURN", SqlDbType.VarChar, 250)
            myparm16.Direction = ParameterDirection.InputOutput
            myparm16.Value = ""

            cmd.ExecuteNonQuery()
            ReturnPath = (cmd.Parameters("@RETURN").Value).ToString
            If ReturnPath = "" Or ReturnPath Is Nothing Then ReturnPath = Application.StartupPath
            conn.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GensavePartyDiscount(ByVal Partyid_v As Double, ByVal Itemid_v As Double, ByVal Discount_v As Double, ByVal Lineid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Transaction = trans
        cmd.CommandText = "PARTY_DISCOUNT_UPD"
        cmd.Parameters.Add("@Lineid", SqlDbType.Float).Value = Lineid_v
        cmd.Parameters.Add("@PARTYID", SqlDbType.Float).Value = Partyid_v
        cmd.Parameters.Add("@ITEMID", SqlDbType.Float).Value = Itemid_v
        cmd.Parameters.Add("@DISCOUNT", SqlDbType.Float).Value = Discount_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GenSavePrinterSetup(ByVal editflag_v As SaveOption, ByVal Process_v As String, ByVal Detailid_v As Double, ByVal Rpttype_v As String, _
                                      ByVal Rptname_v As String, ByVal PrinterName_v As String, ByVal PaperName_v As String, ByVal Directprint_v As Boolean, _
                                      ByVal Sysname_v As String) As Double
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "GEN_SAVE_REPORTSETUP"
        cmd.Parameters.Add("@Editflag", System.Data.SqlDbType.VarChar).Value = GetEditFlag(editflag_v)
        cmd.Parameters.Add("@PROCESS", SqlDbType.VarChar).Value = Process_v
        cmd.Parameters.Add("@DETAILID", SqlDbType.Float).Value = Detailid_v
        cmd.Parameters.Add("@Rpttype", SqlDbType.VarChar).Value = Rpttype_v
        cmd.Parameters.Add("@Rptname", SqlDbType.VarChar).Value = Rptname_v
        cmd.Parameters.Add("@Printername", SqlDbType.VarChar).Value = PrinterName_v
        cmd.Parameters.Add("@Papername", SqlDbType.VarChar).Value = PaperName_v
        cmd.Parameters.Add("@Directprint", SqlDbType.Bit).Value = IIf(Directprint_v = True, 1, 0)
        cmd.Parameters.Add("@Sysname", SqlDbType.VarChar).Value = Sysname_v
        cmd.ExecuteNonQuery()
    End Function

    Public Function GenDelPrinterSetup(ByVal Detailid_v As Double)
        cmd = Nothing
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "GEN_DEL_REPORTSETUP"
        cmd.Parameters.Add("@DetaiLid", SqlDbType.Float).Value = Detailid_v
        cmd.ExecuteNonQuery()
    End Function

End Module
