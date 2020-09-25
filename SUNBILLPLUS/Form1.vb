'Public Class Form1

'    Private Sub Exporttotally_XML()
'        Try

'            'Call opnconn()

'            Dim xmlstc As String = "", xmlmasterstc As String = ""
'            Dim Headxmlstr As String = "", Headxmlmasterstc As String = ""
'            Dim mainxmlstr As String = "", mainmasterxmlstr As String = ""
'            Dim Addressxmlstr As String = ""
'            Dim Refdetlxmlstr As String = ""
'            Dim returned_value As String = "", returnedmaster_value As String = ""
'            Dim fso As New FileSystemObject
'            Dim FileVchr As TextStream

'            Dim Tmpdate_t As Date, Duedate_t As String, Effectivedate_t As String, Duedays_t As String
'            Dim Vchno_t As String, Narration_t As String, Vchtype_t As String, Ptyname_t As String, Convertamt_t As String
'            Dim Amount_t As Double, Tmpacheaderid_t As Double, Chkdrcramt_t As Double, Loanamt_t As Double, diffamt_t As Double
'            Dim Loanid_t As Integer
'            Dim YorN_t As String = "", Sqlstr As String = "", Hpnum_t As String, NewReftype_tt As String
'            Dim Headptyname_t As String, Ptytin_t As String
'            Dim Add1_t As String = "", Add2_t As String = "", Add3_t As String = "", Add4_t As String = "", Add5_t As String = ""
'            Dim Refno_t As String = "", Reftype_t As String = "", ConvertRefamt_t As String
'            Dim Refamt_t As Double, Refloanid_t As Integer, tmpcnt_t As Integer


'            Chkdrcramt_t = 0
'            Loanamt_t = 0
'            diffamt_t = 0

'            tmpsqlstr = "SELECT value from SETTINGS_TALLY WHERE PROCESS = 'TALLYPATH' "
'            tmpcmd = New SqlCommand(tmpsqlstr, conn)
'            tmpcmd.CommandType = CommandType.Text
'            tmpda = New SqlDataAdapter(tmpcmd)
'            tmpds = New DataSet
'            tmpds.Clear()
'            tmpda.Fill(tmpds)

'            If tmpds.Tables(0).Rows.Count > 0 Then
'                TallyPath = tmpds.Tables(0).Rows(0).Item("value")
'            Else
'                TallyPath = ""
'            End If

'            'TallyPath = "E:"

'            If TallyPath = "" Then
'                MsgBox("Set Tally Path.")
'                Button2.Enabled = True
'                Exit Sub
'            End If

'            ds = New DataSet
'            ds.Clear()
'            ds = Nothing
'            cmd = New SqlCommand
'            cmd.Connection = conn
'            cmd.CommandType = CommandType.StoredProcedure
'            cmd.CommandText = "EXPORTTOTALLY_DRTHANCR"
'            cmd.Parameters.Add("@Compid", SqlDbType.Float).Value = Gencompid
'            da = New SqlDataAdapter(cmd)
'            ds = New DataSet
'            da.Fill(ds)

'            If ds.Tables(0).Rows.Count > 0 Then
'                Cursor = Cursors.WaitCursor
'                Tmpacheaderid_t = 0
'                Loanid_t = 0
'                xmlstc = ""
'                xmlmasterstc = ""
'                xmlstc = "<ENVELOPE>" + Environment.NewLine + "<HEADER>" & Environment.NewLine & _
'              "<VERSION>1</VERSION>" + Environment.NewLine & "<TALLYREQUEST>Import</TALLYREQUEST>" + Environment.NewLine & _
'              "<TYPE>Data</TYPE>" + Environment.NewLine & "<ID>Vouchers</ID>" + Environment.NewLine & _
'              "</HEADER>" + Environment.NewLine & "<BODY>" + Environment.NewLine & "<DESC>" + Environment.NewLine & _
'              "</DESC>" + Environment.NewLine & "<DATA>" + Environment.NewLine & "<TALLYMESSAGE >" + Environment.NewLine


'                xmlmasterstc = "<ENVELOPE>" + Environment.NewLine + "<HEADER>" & Environment.NewLine & _
'               "<VERSION>1</VERSION>" + Environment.NewLine & "<TALLYREQUEST>Import</TALLYREQUEST>" + Environment.NewLine & _
'               "<TYPE>Data</TYPE>" + Environment.NewLine & "<ID>All Masters</ID>" + Environment.NewLine & _
'               "</HEADER>" + Environment.NewLine & "<BODY>" + Environment.NewLine & "<DESC>" + Environment.NewLine & _
'               "<STATICVARIABLES>" + Environment.NewLine & "<IMPORTDUPS>@@DUPCOMBINE</IMPORTDUPS>" + Environment.NewLine & _
'               "<SVCURRENTCOMPANY>##SVCurrentCompany</SVCURRENTCOMPANY>" + Environment.NewLine & _
'               "</STATICVARIABLES>" + Environment.NewLine & _
'               "</DESC>" + Environment.NewLine & "<DATA>" + Environment.NewLine & "<TALLYMESSAGE >" + Environment.NewLine


'                For i = 0 To ds.Tables(0).Rows.Count - 1
'                    Cursor = Cursors.WaitCursor
'                    Headxmlstr = ""
'                    Headxmlmasterstc = ""
'                    Addressxmlstr = ""
'                    mainmasterxmlstr = ""
'                    NewReftype_tt = ""
'                    Hpnum_t = ""
'                    Chkdrcramt_t = 0
'                    Loanamt_t = 0
'                    diffamt_t = 0
'                    tmpcnt_t = 0
'                    Convertamt_t = ""

'                    Loanid_t = ds.Tables(0).Rows(i).Item("Loanid")
'                    Vchno_t = ds.Tables(0).Rows(i).Item("Vchnum")
'                    Narration_t = ds.Tables(0).Rows(i).Item("Narration")
'                    Vchtype_t = ds.Tables(0).Rows(i).Item("Vchtype")
'                    Tmpdate_t = ds.Tables(0).Rows(i).Item("Vchdate")
'                    Ptytin_t = ds.Tables(0).Rows(i).Item("Tin")
'                    Headptyname_t = ds.Tables(0).Rows(i).Item("Ptyname")
'                    Effectivedate_t = ds.Tables(0).Rows(i).Item("Effectivedate")

'                    NewReftype_tt = ds.Tables(0).Rows(i).Item("Reftype")
'                    Hpnum_t = ds.Tables(0).Rows(i).Item("Hpnum")

'                    Headxmlstr = Headxmlstr +
'                    "<VOUCHER>" + Environment.NewLine & "<BASICBUYERADDRESS.LIST>" + Environment.NewLine

'                    'Headxmlmasterstc = Headxmlmasterstc +
'                    '    "<LEDGER NAME=" + "''" + Headptyname_t + "''" + " " + "ACTION=" + "''" + "CREATE" + "''" + ">" + Environment.NewLine & _
'                    '    "<NAME>" + Headptyname_t + "</NAME>" + Environment.NewLine & _
'                    '    "<PARENT>Sundry Debtors</PARENT>" + Environment.NewLine & _
'                    '    "</LEDGER>" + Environment.NewLine

'                    Headxmlmasterstc = Headxmlmasterstc +
'                      "<LEDGER>" + Environment.NewLine & _
'                      "<NAME.LIST>" + Environment.NewLine & _
'                      "<NAME>" + Headptyname_t + "</NAME>" + Environment.NewLine & _
'                      "</NAME.LIST>" + Environment.NewLine & _
'                      "<PARENT>Sundry Debtors</PARENT>" + Environment.NewLine & _
'                      "</LEDGER>" + Environment.NewLine

'                    mainmasterxmlstr = xmlmasterstc + Headxmlmasterstc + "</TALLYMESSAGE>" + Environment.NewLine & _
'                       "</DATA>" + Environment.NewLine & "</BODY>" + Environment.NewLine & "</ENVELOPE>"

'                    FileVchr = fso.OpenTextFile(TallyPath & "\TlyLdgr.xml", IOMode.ForWriting, True)
'                    FileVchr.Write(mainmasterxmlstr)
'                    FileVchr.Close()

'                    Dim responsmaseterstr, newmasterstring As String

'                    Try
'                        Dim xmlhttpmaster As Object
'                        xmlhttpmaster = CreateObject("MSXML2.ServerXMLHTTP")
'                        xmlhttpmaster.open("POST", "http://localhost:9000", False)
'                        xmlhttpmaster.send(mainmasterxmlstr)
'                        responsmaseterstr = xmlhttpmaster.ResponseText
'                        returnedmaster_value = responsmaseterstr + returnedmaster_value
'                        newmasterstring = InStrRev(responsmaseterstr, "<LINEERROR>")

'                        If newmasterstring = 0 Then

'                        Else
'                            'MsgBox("Failed to POST")
'                        End If
'                    Catch ex As Exception
'                        Button2.Enabled = True
'                        Cursor = Cursors.Default
'                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'                        Exit Sub
'                    Finally

'                    End Try

'                    FileVchr = fso.OpenTextFile(TallyPath & "\ErrLdgr.tXT", IOMode.ForWriting, True)
'                    FileVchr.Write(returnedmaster_value)
'                    FileVchr.Close()

'                    For k = 1 To 5
'                        Cursor = Cursors.WaitCursor
'                        If k = 1 Then
'                            Add1_t = ds.Tables(0).Rows(i).Item("Add1")
'                            Addressxmlstr = "<BASICBUYERADDRESS>" + Add1_t + "</BASICBUYERADDRESS>" + Environment.NewLine
'                        End If
'                        If k = 2 Then
'                            Add2_t = ds.Tables(0).Rows(i).Item("Add2")
'                            Addressxmlstr = Addressxmlstr + "<BASICBUYERADDRESS>" + Add2_t + "</BASICBUYERADDRESS>" + Environment.NewLine
'                        End If
'                        If k = 3 Then
'                            Add3_t = ds.Tables(0).Rows(i).Item("Add3")
'                            Addressxmlstr = Addressxmlstr + "<BASICBUYERADDRESS>" + Add3_t + "</BASICBUYERADDRESS>" + Environment.NewLine
'                        End If
'                        If k = 4 Then
'                            Add4_t = ds.Tables(0).Rows(i).Item("Add4")
'                            Addressxmlstr = Addressxmlstr + "<BASICBUYERADDRESS>" + Add4_t + "</BASICBUYERADDRESS>" + Environment.NewLine
'                        End If
'                        If k = 5 Then
'                            Add5_t = ds.Tables(0).Rows(i).Item("Add5")
'                            Addressxmlstr = Addressxmlstr + "<BASICBUYERADDRESS>" + Add5_t + "</BASICBUYERADDRESS>" + Environment.NewLine
'                        End If
'                    Next

'                    Headxmlstr = Headxmlstr + Addressxmlstr + " </BASICBUYERADDRESS.LIST>" + Environment.NewLine & _
'                   "<DATE>" + Tmpdate_t + "</DATE>" + Environment.NewLine & _
'                   "<NARRATION>" + Narration_t + "</NARRATION>" + Environment.NewLine & "<VOUCHERTYPENAME>" + Vchtype_t + "</VOUCHERTYPENAME>" + Environment.NewLine & _
'                   "<VOUCHERNUMBER>" + Vchno_t + "</VOUCHERNUMBER>" + Environment.NewLine & _
'                   "<EFFECTIVEDATE>" + Effectivedate_t + "</EFFECTIVEDATE>" + Environment.NewLine & _
'                   "<BASICBUYERNAME>" + Headptyname_t + "</BASICBUYERNAME>" + Environment.NewLine & _
'                   "<BASICBUYERSSALESTAXNO>" + Ptytin_t + " </BASICBUYERSSALESTAXNO>" + Environment.NewLine

'                    tmpds.Clear()
'                    tmpds = Nothing
'                    cmd = New SqlCommand
'                    cmd.Connection = conn
'                    cmd.CommandType = CommandType.StoredProcedure
'                    cmd.CommandText = "EXPORTTOTALLY_DETAIL_DRTHANCR"
'                    cmd.Parameters.Add("@Partyid", SqlDbType.Float).Value = Loanid_t
'                    da = New SqlDataAdapter(cmd)
'                    tmpds = New DataSet
'                    da.Fill(tmpds)

'                    If tmpds.Tables(0).Rows.Count > 0 Then
'                        tmpcnt_t = tmpds.Tables(0).Rows.Count
'                        Cursor = Cursors.WaitCursor
'                        For j = 0 To tmpds.Tables(0).Rows.Count - 1
'                            If j = 0 Then
'                                Loanamt_t = tmpds.Tables(0).Rows(j).Item("Amount")
'                            End If
'                            Refdetlxmlstr = ""
'                            Ptyname_t = IIf(IsDBNull(tmpds.Tables(0).Rows(j).Item("Ptyname")), "", tmpds.Tables(0).Rows(j).Item("Ptyname"))
'                            Amount_t = tmpds.Tables(0).Rows(j).Item("Amount")
'                            Refloanid_t = tmpds.Tables(0).Rows(j).Item("Loanid")
'                            If (Amount_t < 0) Then
'                                YorN_t = "Yes"
'                            ElseIf (Amount_t > 0) Then
'                                YorN_t = "No"
'                            End If


'                            If j > 0 Then
'                                Chkdrcramt_t = Chkdrcramt_t + Amount_t
'                            End If

'                            If j = tmpcnt_t - 1 Then
'                                If Chkdrcramt_t <> 0 Then
'                                    diffamt_t = (Loanamt_t * -1) - Chkdrcramt_t
'                                    If diffamt_t <> 0 Then
'                                        Convertamt_t = (Amount_t + diffamt_t).ToString("0.00")
'                                    Else
'                                        Convertamt_t = Amount_t.ToString("0.00")
'                                    End If
'                                End If
'                            Else
'                                Convertamt_t = Amount_t.ToString("0.00")
'                            End If


'                            Headxmlstr = Headxmlstr +
'                       "<ALLLEDGERENTRIES.LIST>" + Environment.NewLine & _
'                       "<LEDGERNAME>" + Ptyname_t + "</LEDGERNAME>" + Environment.NewLine & _
'                       "<ISDEEMEDPOSITIVE>" + YorN_t + "</ISDEEMEDPOSITIVE>" + Environment.NewLine & _
'                       "<AMOUNT>" + Convertamt_t + "</AMOUNT>" + Environment.NewLine

'                            dsrefdetl = Nothing
'                            cmd = New SqlCommand
'                            cmd.Connection = conn
'                            cmd.CommandType = CommandType.StoredProcedure
'                            cmd.CommandText = "EXPORTTOTALLY_REFDETAIL_DRTHANCR"
'                            cmd.Parameters.Add("@Partyid", SqlDbType.Float).Value = Refloanid_t
'                            da = New SqlDataAdapter(cmd)
'                            dsrefdetl = New DataSet
'                            dsrefdetl.Clear()
'                            da.Fill(dsrefdetl)

'                            If dsrefdetl.Tables(0).Rows.Count > 0 And Refloanid_t <> -1 Then
'                                For k = 0 To dsrefdetl.Tables(0).Rows.Count - 1
'                                    Cursor = Cursors.WaitCursor
'                                    Refno_t = dsrefdetl.Tables(0).Rows(k).Item("REFNUM")
'                                    Reftype_t = dsrefdetl.Tables(0).Rows(k).Item("REFTYPE")
'                                    Refamt_t = dsrefdetl.Tables(0).Rows(k).Item("INSTALLAMT")
'                                    Duedate_t = dsrefdetl.Tables(0).Rows(k).Item("DUEDATE")
'                                    Duedays_t = dsrefdetl.Tables(0).Rows(k).Item("DUEDAYS")

'                                    If Refamt_t <> 0 And Refno_t <> "" Then
'                                        ConvertRefamt_t = Refamt_t.ToString("0.00")
'                                        Refdetlxmlstr = Refdetlxmlstr +
'                                        "<BILLALLOCATIONS.LIST>" + Environment.NewLine & _
'                                        "<NAME>" + Refno_t + "</NAME>" + Environment.NewLine & _
'                                        "<BILLCREDITPERIOD>" + Duedays_t + "</BILLCREDITPERIOD>" + Environment.NewLine & _
'                                        "<BILLTYPE>" + Reftype_t + "</BILLTYPE>" + Environment.NewLine & _
'                                        "<AMOUNT>" + ConvertRefamt_t + "</AMOUNT>" + Environment.NewLine & _
'                                        "</BILLALLOCATIONS.LIST>" + Environment.NewLine
'                                    End If
'                                Next k
'                            End If

'                            If Refloanid_t = -1 Then
'                                Cursor = Cursors.WaitCursor
'                                Refno_t = Hpnum_t
'                                Reftype_t = NewReftype_tt
'                                Duedays_t = ""
'                                If Refno_t <> "" Then
'                                    Refdetlxmlstr = Refdetlxmlstr +
'                                    "<BILLALLOCATIONS.LIST>" + Environment.NewLine & _
'                                    "<NAME>" + Refno_t + "</NAME>" + Environment.NewLine & _
'                                    "<BILLTYPE>" + Reftype_t + "</BILLTYPE>" + Environment.NewLine & _
'                                    "<AMOUNT>" + Convertamt_t + "</AMOUNT>" + Environment.NewLine & _
'                                    "</BILLALLOCATIONS.LIST>" + Environment.NewLine
'                                End If
'                            End If

'                            If Refdetlxmlstr = "" Then
'                                Headxmlstr = Headxmlstr + "</ALLLEDGERENTRIES.LIST>" + Environment.NewLine
'                            Else
'                                Headxmlstr = Headxmlstr + Refdetlxmlstr +
'                                        "</ALLLEDGERENTRIES.LIST>" + Environment.NewLine
'                            End If

'                        Next
'                    End If

'                    Headxmlstr = Headxmlstr + "</VOUCHER>" + Environment.NewLine

'                    mainxmlstr = xmlstc + Headxmlstr + "</TALLYMESSAGE>" + Environment.NewLine & _
'                        "</DATA>" + Environment.NewLine & "</BODY>" + Environment.NewLine & "</ENVELOPE>"

'                    FileVchr = fso.OpenTextFile(TallyPath & "\TlyVchr.xml", IOMode.ForWriting, True)
'                    FileVchr.Write(mainxmlstr)
'                    FileVchr.Close()

'                    Dim responsstr, newstring As String

'                    Try
'                        Dim xmlhttp As Object
'                        xmlhttp = CreateObject("MSXML2.ServerXMLHTTP")
'                        xmlhttp.open("POST", "http://localhost:9000", False)
'                        xmlhttp.send(mainxmlstr)
'                        responsstr = xmlhttp.ResponseText
'                        returned_value = responsstr + returned_value
'                        newstring = InStrRev(responsstr, "<LINEERROR>")

'                        If newstring = 0 Then

'                            Sqlstr = "Update Loanmaster Set EXP_FLG=1 WHERE Loanid = " & Loanid_t & ""
'                            cmd1 = New SqlCommand(Sqlstr, conn)
'                            cmd1.Connection = conn
'                            cmd1.CommandType = CommandType.Text
'                            cmd1.ExecuteNonQuery()

'                        Else
'                            'MsgBox("Failed to POST")
'                        End If
'                    Catch ex As Exception
'                        Button2.Enabled = True
'                        Cursor = Cursors.Default
'                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'                        Exit Sub
'                    Finally

'                    End Try

'                Next

'                FileVchr = fso.OpenTextFile(TallyPath & "\ErrVchr.tXT", IOMode.ForWriting, True)
'                FileVchr.Write(returned_value)
'                FileVchr.Close()

'                Jrnexp_t = True
'                'Exporttotally_Rec_XML()

'                'Cursor = Cursors.Default
'                'Button2.Enabled = True
'                'MsgBox("Export Completed.")
'            Else
'                'Exit Sub
'                'Cursor = Cursors.Default
'                'Button2.Enabled = True
'                'MsgBox("No Vouchers to Export.")
'                Jrnexp_t = False
'            End If

'            Exporttotally_Rec_XML()

'        Catch ex As Exception
'            Button2.Enabled = True
'            Cursor = Cursors.Default
'            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Exit Sub
'        End Try
'    End Sub

'End Class
