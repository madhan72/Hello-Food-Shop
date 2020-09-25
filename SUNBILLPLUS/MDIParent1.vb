Imports System.Windows.Forms
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports SingleMaster_App
Imports QUERY_APP
Imports USERS_APP
Imports SunUtilities_APP
Imports Accounts_App

Public Class MDIParent1

    Public var As String
    Public varid As Double
    Public tmppassstr As String
    Dim tmpcmd, tmpcmd1 As New SqlCommand
    Dim tmpds, tmpds1, tmpds2 As DataSet
    Dim tmpda As SqlDataAdapter
    Dim tmpsqlstr As String
    Dim cnt_t, cnt_t1 As Integer
    Dim Sm As New Sun_Singlemaster
    Dim Qm As New FrmQueryview_APP
    Dim Autonum As New Sun_Frm_Autonum
    Dim Pathopt As New Sun_Frmpathoptions
    Public ShowImage As Integer
    Dim Sortord As New Sun_Frm_Sortorder
    Dim Acledger As New Sun_Frm_AccLedger
    Dim Acent As New Sun_FrmAcctsentry
    Dim Acrpt As New Sun_FrmAccRepoptions
    Dim Expdetls As New Sun_FrmExportedDetls
    Dim IsDirectInvoiceFormat2 As Boolean
    Dim IsProcessPricelistFormat2 As Boolean
    Public AgencyBillFormat_t As Boolean
    Dim SIZE_ASSORTMENT_ENTRY As Boolean
    Public ITEMMASTER_FORMAT2 As Boolean
    Public ESTIMATE_FORMAT2 As Boolean
    'Dim DirectPackingList As Boolean

    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Create a new instance of the child form.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Make it a child of this MDI form before showing it.
        ChildForm.MdiParent = Me

        m_ChildFormNumber += 1
        ChildForm.Text = "Window " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private m_ChildFormNumber As Integer

    Private Sub MDIParent1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Call AutoBackupdata()
    End Sub

    Private Sub MDIParent1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.PageUp Then
            ' MsgBox("mdi keydown")
        End If
    End Sub

    Private Sub MDIParent1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Call opnconn()
            Call dsopen()

            Dim ImgPath_t As String = Application.StartupPath
            If ImgPath_t.IndexOf("bin\Release") <> -1 Then ImgPath_t = ImgPath_t.Replace("bin\Release", "homescreen.jpg") Else ImgPath_t = ImgPath_t + "\homescreen.jpg"

            If File.Exists(ImgPath_t) Then
                Me.BackgroundImage = System.Drawing.Image.FromFile(ImgPath_t)
            Else
                Me.BackgroundImage = Nothing
            End If

            Me.Text = Me.Text + " | " + Gencompname + " - " + Genuname

            Dim TmpDateFormat As System.Globalization.CultureInfo
            TmpDateFormat = New System.Globalization.CultureInfo("en-US")
            TmpDateFormat.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture = TmpDateFormat

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dsopen()
        Try
            Dim tmpflag_t As Boolean
            Dim tmpmenu_t, tmpsubmenu_t, tmpsubmneu_t1 As String
            Dim tmpmenuid_t As Integer
            Dim submenu_t As New ToolStripMenuItem

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'ACCS PURCHASE EXCESS PERCENTAGE' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                Accspurcexcessperc_t = tmpds.Tables(0).Rows(0).Item("Value")
            Else
                Accspurcexcessperc_t = 0
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'GENERATE EVENT LOGS' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                If tmpds.Tables(0).Rows(0).Item("Value") = 1 Then
                    Generateeventlogs_t = True
                Else
                    Generateeventlogs_t = False
                End If
            Else
                Generateeventlogs_t = False
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'AUTO ADJUST IN ACCOUNTS' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                If tmpds.Tables(0).Rows(0).Item("Value") = 1 Then
                    Autoadjustflg_t = True
                Else
                    Autoadjustflg_t = False
                End If
            Else
                Autoadjustflg_t = False
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value,Gm.Godownname As Location From Settings S " _
                & " Join Godown_Master Gm On Gm.Masterid = Isnull(Numericvalue,0) Where S.Process = 'Default Location' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                Defaultlocation_t = tmpds.Tables(0).Rows(0).Item("Location")
                Defaultlocationid_t = tmpds.Tables(0).Rows(0).Item("Value")
            Else
                Defaultlocation_t = ""
                Defaultlocationid_t = 0
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value  From Settings S Where S.Process = 'VAT PERC' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                Vatperc_t = tmpds.Tables(0).Rows(0).Item("Value")
            Else
                Vatperc_t = 0
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value  From Settings S Where S.Process = 'CST PERC' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                Cstperc_t = tmpds.Tables(0).Rows(0).Item("Value")
            Else
                Cstperc_t = 0
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value  From Settings S Where S.Process = 'AGENCY BILL FORMAT' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                AgencyBillFormat_t = tmpds.Tables(0).Rows(0).Item("Value")
            Else
                AgencyBillFormat_t = 0
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'A/c Posting Need LR/RR No' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                If tmpds.Tables(0).Rows(0).Item("Value") = 1 Then
                    Acpostingwithlrno_t = True
                Else
                    Acpostingwithlrno_t = False
                End If
            Else
                Acpostingwithlrno_t = False
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'ITEMMASTER_FORMAT2' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                If tmpds.Tables(0).Rows(0).Item("Value") = 1 Then
                    ITEMMASTER_FORMAT2 = True
                Else
                    ITEMMASTER_FORMAT2 = False
                End If
            Else
                ITEMMASTER_FORMAT2 = False
            End If

            tmpsqlstr = "Select Isnull(NumericValue,0) As Value From Settings Where Process = 'ESTIMATE_FORMAT2' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            If tmpds.Tables(0).Rows.Count > 0 Then
                If tmpds.Tables(0).Rows(0).Item("Value") = 1 Then
                    ESTIMATE_FORMAT2 = True
                Else
                    ESTIMATE_FORMAT2 = False
                End If
            Else
                ESTIMATE_FORMAT2 = False
            End If


            tmpsqlstr = "  Select m.menuid,ur.visible,m.menuitem,m.vbname from USERRIGHTS ur " _
                      & " join MENU m on m.MENUID = ur.MENUID " _
                      & " where ur.uid= " & Genuid & "  and isnull(m.UNDER,'') = '' "
            tmpcmd = New SqlCommand(tmpsqlstr, conn)
            tmpcmd.CommandType = CommandType.Text
            tmpda = New SqlDataAdapter(tmpcmd)
            tmpds = New DataSet
            tmpds.Clear()
            tmpda.Fill(tmpds)

            cnt_t = tmpds.Tables(0).Rows.Count

            If cnt_t > 0 Then

                'For i As Integer = 0 To MasterMenu.DropDownItems.Count - 1
                '    MasterMenu.DropDownItems(i).Visible = False
                'Next

                'For i As Integer = 0 To TranMenu.DropDownItems.Count - 1
                '    TranMenu.DropDownItems(i).Visible = False
                'Next

                'For i As Integer = 0 To ReportsMenu.DropDownItems.Count - 1
                '    ReportsMenu.DropDownItems(i).Visible = False
                'Next

                'For i As Integer = 0 To QueryMenu.DropDownItems.Count - 1
                '    QueryMenu.DropDownItems(i).Visible = False
                'Next

                'For i As Integer = 0 To UtilitiesMenu.DropDownItems.Count - 1
                '    UtilitiesMenu.DropDownItems(i).Visible = False
                'Next

                For i = 0 To cnt_t - 1
                    tmpmenuid_t = tmpds.Tables(0).Rows(i).Item("menuid")
                    tmpmenu_t = tmpds.Tables(0).Rows(i).Item("vbname")
                    tmpflag_t = IIf(tmpds.Tables(0).Rows(i).Item("visible").ToString = "T", True, False)
                    'Controls(tmpmenu_t).Visible = tmpflag_t
                    MenuStrip1.Items(tmpmenu_t).Visible = tmpflag_t

                    If tmpflag_t = True Then
                        tmpsqlstr = "  Select ur.visible,m.menuitem,m.vbname,M.MENUID AS UNDER from USERRIGHTS ur " _
                                  & " join MENU m on m.MENUID = ur.MENUID " _
                                  & " where ur.uid= " & Genuid & "  and isnull(m.UNDER,'') <> '' and m.under = " & tmpmenuid_t & " "
                        tmpcmd = New SqlCommand(tmpsqlstr, conn)
                        tmpcmd.CommandType = CommandType.Text
                        tmpda = New SqlDataAdapter(tmpcmd)
                        tmpds1 = New DataSet
                        tmpds1.Clear()
                        tmpda.Fill(tmpds1)

                        cnt_t1 = tmpds1.Tables(0).Rows.Count

                        If cnt_t1 > 0 Then
                            For j = 0 To cnt_t1 - 1
                                tmpsubmenu_t = tmpds1.Tables(0).Rows(j).Item("vbname")
                                tmpmenuid_t = tmpds1.Tables(0).Rows(j).Item("UNDER")
                                tmpflag_t = IIf(tmpds1.Tables(0).Rows(j).Item("visible").ToString = "T", True, False)
                                'Dim menues As New List(Of ToolStripItem)
                                'For Each t As ToolStripItem In MenuStrip1.Items
                                '        GetMenues(t, menues)
                                'Next

                                'Dim msg As String = ""
                                'For Each t As ToolStripItem In menues
                                '    msg &= t.Name & vbCrLf
                                'Next
                                'MessageBox.Show(msg)

                                'If tmpmenu_t = "TranMenu" Then
                                '    If tmpsubmenu_t = "AccessoryTransaction" Or tmpsubmenu_t = "AccountsEnt" Then
                                '        CType(MenuStrip1.Items(tmpmenu_t), ToolStripMenuItem).DropDownItems(tmpsubmenu_t).Visible = tmpflag_t
                                '    Else
                                '        CType(CType(MenuStrip1.Items(tmpmenu_t), ToolStripMenuItem).DropDownItems("AccessoryTransaction"), ToolStripMenuItem).DropDownItems(tmpsubmenu_t).Visible = tmpflag_t
                                '    End If
                                'Else

                                tmpsubmneu_t1 = tmpsubmenu_t

                                CType(MenuStrip1.Items(tmpmenu_t), ToolStripMenuItem).DropDownItems(tmpsubmenu_t).Visible = tmpflag_t
                                If tmpflag_t = True Then
                                    tmpcmd1 = Nothing
                                    tmpds2 = Nothing
                                    tmpsqlstr = "  Select ur.visible,m.menuitem,m.vbname from USERRIGHTS ur " _
                                              & " join MENU m on m.MENUID = ur.MENUID " _
                                              & " where ur.uid= " & Genuid & "  and isnull(m.UNDER,'') <> '' and m.under = " & tmpmenuid_t & " "
                                    tmpcmd1 = New SqlCommand(tmpsqlstr, conn)
                                    tmpcmd1.CommandType = CommandType.Text
                                    tmpda = New SqlDataAdapter(tmpcmd1)
                                    tmpds2 = New DataSet
                                    tmpds2.Clear()
                                    tmpda.Fill(tmpds2)

                                    cnt_t1 = tmpds2.Tables(0).Rows.Count

                                    If cnt_t1 > 0 Then
                                        For K = 0 To cnt_t1 - 1
                                            tmpsubmenu_t = tmpds2.Tables(0).Rows(K).Item("vbname")
                                            tmpflag_t = IIf(tmpds2.Tables(0).Rows(K).Item("visible").ToString = "T", True, False)
                                            CType(CType(MenuStrip1.Items(tmpmenu_t), ToolStripMenuItem).DropDownItems(tmpsubmneu_t1), ToolStripMenuItem).DropDownItems(tmpsubmenu_t).Visible = tmpflag_t
                                            'CType(MenuStrip1.Items(tmpsubmneu_t1), ToolStripMenuItem).DropDownItems(tmpsubmenu_t).Visible = tmpflag_t
                                        Next
                                    End If
                                End If
                                'End If
                            Next
                        End If
                    Else
                    End If
                Next
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetToolStripItems(dropDownItems As ToolStripItemCollection)
        Try
            For Each obj As Object In dropDownItems
                'for each object.
                Dim subMenu As ToolStripMenuItem = TryCast(obj, ToolStripMenuItem)
                'Try cast to ToolStripMenuItem as it could be toolstrip separator as well.

                If subMenu IsNot Nothing Then
                    'if we get the desired object type.
                    If subMenu.HasDropDownItems Then
                        ' if subMenu has children
                        ' Call recursive Method.
                        SetToolStripItems(subMenu.DropDownItems)
                    Else
                        ' Do the desired operations here.
                        If subMenu.Tag IsNot Nothing Then
                            ' subMenu.Visible = MenuStrip1.Where(Function(x) x.FormID = Convert.ToInt32(subMenu.Tag)).First().CanAccess
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "SetToolStripItems", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================
    Public Sub GetMenues(ByVal Current As ToolStripMenuItem, ByRef menues As List(Of ToolStripMenuItem))
        menues.Add(Current)
        For Each menu As ToolStripMenuItem In Current.DropDownItems
            GetMenues(menu, menues)
        Next
    End Sub

    Private Sub ExitMenu_Click(sender As Object, e As EventArgs) Handles ExitMenu.Click
        conn.Close()
        Call AutoBackupdata()
        End
    End Sub

    Private Sub AccountsToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        Dim frmmain = New Frmmain
        frmmain.Execute("Get_AccountsDetails", "Frmacctsentry", "Get_AccountsDetails_Columnformat", "ACCOUNTS-TRNASACTION")
        frmmain.ShowInTaskbar = False
        frmmain.Width = 900
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub UserlogonMst_Click(sender As Object, e As EventArgs) Handles UserlogonMst.Click
        Dim frm1 = New Frmlogin
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub CompanyLogonMst_Click(sender As Object, e As EventArgs) Handles CompanyLogonMst.Click
        Dim frmcompsel = New Frm_CompSelect
        frmcompsel.ShowInTaskbar = False
        frmcompsel.Init()
        frmcompsel.StartPosition = FormStartPosition.CenterScreen
        frmcompsel.ShowDialog()
    End Sub

    Private Sub CompanyMst_Click(sender As Object, e As EventArgs) Handles CompanyMst.Click
        Dim frm1 = New CompanyMaster
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub PartyMasterMst_Click(sender As Object, e As EventArgs) Handles PartyMasterMst.Click
        Dim frm1 = New PartyMaster
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub UomMst_Click(sender As Object, e As EventArgs) Handles UomMst.Click
        Dim frm1 = New FrmUomMaster
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub QuerysMenus_Click(sender As Object, e As EventArgs) Handles QuerysMenus.Click
        Qm.ShowInTaskbar = False
        Qm.Init(conn, Gencompid, Genuid)
        Qm.StartPosition = FormStartPosition.CenterScreen
        Qm.ShowDialog()
    End Sub

    Private Sub AutoNumberUti_Click(sender As Object, e As EventArgs) Handles AutoNumberUti.Click
        Autonum.AssignVal(conn, Genuname, Gencompid)
    End Sub

    Private Sub PathOptiopnsUti_Click(sender As Object, e As EventArgs) Handles PathOptiopnsUti.Click
        Pathopt.AssignVal(conn, Genuname)
    End Sub

    Private Sub BackupDataUti_Click(sender As Object, e As EventArgs) Handles BackupDataUti.Click
        Call Backupdata()
    End Sub

    Private Sub QuerySetupUti_Click(sender As Object, e As EventArgs) Handles QuerySetupUti.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("Get_QuerysetupDetails", "Frm_Querysetup", "Get_QuerysetupDetails_Columnformat")
        frmmain.Width = 750
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.ShowInTaskbar = False
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub UsersUti_Click(sender As Object, e As EventArgs) Handles UsersUti.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("Get_UserDetails", "Frm_Users", "Get_UserDetails_Columnformat")
        frmmain.ShowInTaskbar = False
        frmmain.Width = 530
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub StateMst_Click(sender As Object, e As EventArgs) Handles groupMst.Click
        Sm.AssignVal(conn, "Group", "Group_Master", "GroupName", , , , "", 200)
    End Sub

    Private Sub TransportMst_Click(sender As Object, e As EventArgs) Handles CategoryMst.Click
        Sm.AssignVal(conn, "Category", "category_Master", "Category", , , , "", 200)
    End Sub

    Private Sub BookingplaceMst_Click(sender As Object, e As EventArgs)
        Sm.AssignVal(conn, "Booking Place", "Bookingplace_Master", "Bookingplace", , , , "", 200)
    End Sub

    Private Sub Groupmst_Click(sender As Object, e As EventArgs) Handles Rakemst.Click
        Sm.AssignVal(conn, "Rake", "Rake_Master", "Rake", , , , "", 200)
    End Sub

    Private Sub SttingsUti_Click(sender As Object, e As EventArgs) Handles SttingsUti.Click
        Dim frmset = New frm_settingsnew
        frmset.ShowInTaskbar = False
        frmset.StartPosition = FormStartPosition.CenterScreen
        frmset.ShowDialog()
    End Sub

    Private Sub Exporttlyuti_Click(sender As Object, e As EventArgs) Handles Exporttlyuti.Click
        'Exporttotally(conn, Gencompid)
        ' Exporttotally_XML(conn, Gencompid)
    End Sub

    Private Sub Updateexpototallyuti_Click(sender As Object, e As EventArgs) Handles Updateexpototallyuti.Click
        Call opnconn()
        'Updateexporttally(conn, Gencompid)
    End Sub

    Private Sub Exporteddetlsuti_Click(sender As Object, e As EventArgs) Handles Exporteddetlsuti.Click
        '  Expdetls.Init(Gencompid, conn, "ACW")
    End Sub

    Private Sub ProcessPartyMst_Click(sender As Object, e As EventArgs)
        'Dim frm1 = New ProcessPartyMaster
        'frm1.ShowInTaskbar = False
        'frm1.StartPosition = FormStartPosition.CenterScreen
        'frm1.ShowDialog()
    End Sub

    Private Sub ItemTransferEnt_Click(sender As Object, e As EventArgs)
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_ITEMTRANSFERDETAILS", "Frm_itemtransferlocatn", "GET_ITEMTRANSFERDETAILS_COLUMNFORMAT", "Item Transfer ")
        frmmain.ShowInTaskbar = False
        frmmain.Width = 900
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub ItemMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemMst.Click
        If ITEMMASTER_FORMAT2 = True Then
            Dim frm1 = New Frm_itemmaster1
            frm1.ShowInTaskbar = False
            frm1.StartPosition = FormStartPosition.CenterScreen
            frm1.ShowDialog()
        Else
            Dim frm1 = New frm_itemmaster
            frm1.ShowInTaskbar = False
            frm1.StartPosition = FormStartPosition.CenterScreen
            frm1.ShowDialog()
        End If
    End Sub

    Private Sub StateMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StateMst.Click
        'Sm.AssignVal(conn, "State", "State_Master", "State", , , , "", 200)
        'Sm.AssignVal(conn, "Group", "Group_Master", "Groupname", , , , "", 200)
        Dim frmGrp = New frm_statemaster
        frmGrp.ShowInTaskbar = False
        frmGrp.StartPosition = FormStartPosition.CenterScreen
        frmGrp.ShowDialog()
    End Sub

    Private Sub TransportMasterToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Sm.AssignVal(conn, "Transport", "Transport_Master", "Transport", , , , "", 200)
    End Sub

    Private Sub AgentMasterToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Sm.AssignVal(conn, "Agent", "Agent_Master", "Agent", , , , "", 200)
    End Sub

    Private Sub InvoiceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InvoiceTran.Click
        If AgencyBillFormat_t = False Then
            Dim frmmain = New Frmmain
            frmmain.Execute("GET_INVOICEDETAILS", "frm_invoice", "GET_INVOICEDETAILS_COLUMNFORMAT", "Invoice")
            frmmain.ShowInTaskbar = False
            ' frmmain.Size = New Size(1206, 668)
            frmmain.GridView1.Width = frmmain.Width - 20
            frmmain.StartPosition = FormStartPosition.CenterScreen
            frmmain.ShowDialog()
        Else
            Dim frmmain = New Frmmain
            frmmain.Execute("GET_INVOICEDETAILS", "frm_agencybill_invoice", "GET_INVOICEDETAILS_COLUMNFORMAT", "Invoice")
            frmmain.ShowInTaskbar = False
            'frmmain.Size = New Size(1206, 668)
            frmmain.GridView1.Width = frmmain.Width - 20
            frmmain.StartPosition = FormStartPosition.CenterScreen
            frmmain.ShowDialog()
        End If
    End Sub

    Private Sub QuotationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles quotationEnt.Click
        If ESTIMATE_FORMAT2 = True Then
            Dim frmmain = New Frmmain
            frmmain.Execute("GET_QUOTATIONDETAILS2", "Frm_quotationformat", "GET_QUOTATIONDETAILS2_COLUMNFORMAT", "Quotation")
            frmmain.ShowInTaskbar = False
            'frmmain.Size = New Size(1206, 668)
            frmmain.GridView1.Width = frmmain.Width - 20
            frmmain.Text = "Quotaion"
            frmmain.StartPosition = FormStartPosition.CenterScreen
            frmmain.ShowDialog()
        Else
            Dim frmmain = New Frmmain
            frmmain.Execute("GET_QUOTATIONDETAILS", "frm_quotation", "GET_QUOTATIONDETAILS_COLUMNFORMAT", "Quotation")
            frmmain.ShowInTaskbar = False
            'frmmain.Size = New Size(1206, 668)
            frmmain.GridView1.Width = frmmain.Width - 20
            frmmain.StartPosition = FormStartPosition.CenterScreen
            frmmain.ShowDialog()
        End If
    End Sub

    Private Sub LocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocationMst.Click
        Sm.AssignVal(conn, "Location", "godown_Master", "godownname", , , , "", 200)
    End Sub

    Private Sub FontStyleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FontStyleUti.Click
        Dim frm1 = New frm_fontstyle
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub OrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_ORDERDETAILS", "frm_order", "GET_ORDERDETAILS_COLUMNFORMAT", "Order")
        frmmain.ShowInTaskbar = False
        ' frmmain.Size = New Size(1206, 668)
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.Text = "Order"
        frmmain.ShowDialog()
    End Sub

    Private Sub BillTotalEnt_Click(sender As Object, e As EventArgs) Handles BillTotalEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_BILLTOTAL_DETAILS", "frm_billtotal", "GET_BILLTOTAL_DETAILS_COLUMNFORMAT", "Bill")
        frmmain.ShowInTaskbar = False
        'frmmain.Size = New Size(frm_billtotal.Size.Width, frm_billtotal.Size.Height)
        'frmmain.GridView1.Width = frmmain.Width - 20
        'frmmain.GridView1.Height = frmmain.Height - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.Text = "Bill Total"
        frmmain.ShowDialog()
    End Sub

    Private Sub Cardnomst_Click(sender As Object, e As EventArgs) Handles Cardnomst.Click
        Dim frm1 = New frm_cardnomaster
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub LineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Linemst.Click
        Sm.AssignVal(conn, "Line", "line_Master", "Line", , , , "", 200)
    End Sub

    Private Sub AccountsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Accountsmst.Click
        Acledger.ShowInTaskbar = False
        Acledger.Init(conn, Genuid, Gencompid)
        Acledger.StartPosition = FormStartPosition.CenterScreen
        Acledger.ShowDialog()
    End Sub

    Private Sub AccountsToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles AccountsEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("Get_AccountsDetails", "Frmacctsentry", "Get_AccountsDetails_Columnformat", "ACCOUNTS-TRNASACTION")
        frmmain.ShowInTaskbar = False
        ' frmmain.Width = 900
        ' frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub UserLocationSettingsToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles UserLocationuti.Click
        Dim frm1 = New frm_userlocationsettings
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub CompanyOutstandingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyOutstandingRpt.Click
        Dim frm1 = New frm_companyoutstanding
        frm1.ShowInTaskbar = False
        frm1.StartPosition = FormStartPosition.CenterScreen
        frm1.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PurchaseEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_PURCHASEDETAILS", "frm_purchase", "GET_PURCHASEDETAILS_COLUMNFORMAT", "Purchase")
        frmmain.ShowInTaskbar = False
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.Text = "Purchase"
        frmmain.ShowDialog()
    End Sub

    Private Sub AcLedgerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcLedgerRpt.Click
        Acrpt.ShowInTaskbar = False
        Acrpt.Rpttitle(conn, "Account Ledger", Servername_t, Databasename_t, Gencompid)
        Acrpt.StartPosition = FormStartPosition.CenterScreen
        Acrpt.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(sender As Object, e As EventArgs) Handles PurchaseRetEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_PURCHASERETURNDETAILS", "frm_purchasereturn", "GET_PURCHASERETDETAILS_COLUMNFORMAT", "Purchase Return")
        frmmain.ShowInTaskbar = False
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.Text = "Purchase Return"
        frmmain.ShowDialog()
    End Sub

    Private Sub SalesreturnEnt_Click(sender As Object, e As EventArgs) Handles SalesreturnEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_SALESRETURNDETAILS", "frm_salesreturn", "GET_SALESDETAILS_COLUMNFORMAT", "Sales Return")
        frmmain.ShowInTaskbar = False
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.Text = "Sales Return"
        frmmain.ShowDialog()
    End Sub

    Private Sub StockRpt_Click(sender As Object, e As EventArgs) Handles StockRpt.Click
        Dim frmStock = New Frm_Stock
        frmStock.Text = "STOCK"
        frmStock.ShowInTaskbar = False
        frmStock.StartPosition = FormStartPosition.CenterScreen
        frmStock.ShowDialog()
    End Sub

    Private Sub LineReceiptEnt_Click(sender As Object, e As EventArgs) Handles LineReceiptEnt.Click
        Dim frmStock = New Frm_LineReceipt
        frmStock.Text = "LINE RECEIPT"
        frmStock.ShowInTaskbar = False
        frmStock.StartPosition = FormStartPosition.CenterScreen
        frmStock.ShowDialog()
    End Sub

    Private Sub InvoiceRegisterRpt_Click(sender As Object, e As EventArgs) Handles InvoiceRegisterRpt.Click
        Dim FrmInvReg = New Frm_Invregister
        FrmInvReg.Text = "Invoice Register"
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub

    Private Sub RetailBillEnt_Click(sender As Object, e As EventArgs) Handles RetailBillEnt.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_RETAILDETAILS", "frm_retailbill", "GET_RETAILDETAILS_COLUMNFORMAT", "Invoice")
        frmmain.ShowInTaskbar = False
        'frmmain.Size = New Size(1206, 668)
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.Text = "Retail Bill"
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub PrintersetupUti_Click(sender As Object, e As EventArgs) Handles PrintersetupUti.Click
        Dim frmset = New Frm_PrinterSetup
        frmset.ShowInTaskbar = False
        frmset.StartPosition = FormStartPosition.CenterScreen
        frmset.ShowDialog()
    End Sub

    Private Sub ToolStripMenuItem1_Click_2(sender As Object, e As EventArgs) Handles DespatchMst.Click
        Sm.AssignVal(conn, "Despatch", "Despatch_Master", "Despatch", , , , "", 200)
    End Sub

    Private Sub ItemAddDeductTrans_Click(sender As Object, e As EventArgs) Handles ItemAddDeductTrans.Click
        Dim frmmain = New Frmmain
        frmmain.Execute("GET_ITEMADDLESSDETAILS", "frm_itemaddless", "GET_ITEMADDLESSDETAILS_COLUMNFORMAT", "ItemAddLess")
        frmmain.ShowInTaskbar = False
        frmmain.GridView1.Width = frmmain.Width - 20
        frmmain.StartPosition = FormStartPosition.CenterScreen
        frmmain.ShowDialog()
    End Sub

    Private Sub QuotationUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuotationUpdate.Click
        Dim frmset = New Frm_quotaionbilled
        frmset.ShowInTaskbar = False
        frmset.StartPosition = FormStartPosition.CenterScreen
        frmset.ShowDialog()
    End Sub

    Private Sub SalesRegisterRpt_Click(sender As Object, e As EventArgs) Handles SalesRegisterRpt.Click
        Dim FrmInvReg = New Frm_salesregisterbillwiseRpt
        'FrmInvReg.Text = "Sales Register"
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub

    Private Sub SalesAnalysisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalesAnalysisRpt.Click
        Dim FrmInvReg = New Frm_salesAnalysisreport
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub

    Private Sub HSNAccodemst_Click(sender As Object, e As EventArgs) Handles HSNAccodemst.Click
        Dim FrmInvReg = New frm_hsnaccode
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub

    Private Sub TransportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Transportmst.Click
        Sm.AssignVal(conn, "Transport", "Transport_Master", "Transport", , , , "", 200)
    End Sub

    Private Sub OutstandingAreawiseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutstandingRpt.Click
        Dim FrmInvReg = New frm_outstandingbillwise
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub

    Private Sub BillRegisterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BillRegisterrpt.Click
        Dim FrmInvReg = New frm_reprot
        FrmInvReg.ShowInTaskbar = False
        FrmInvReg.StartPosition = FormStartPosition.CenterScreen
        FrmInvReg.ShowDialog()
    End Sub
End Class
