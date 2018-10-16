Imports System.Net
Imports System.Xml
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports Newtonsoft.Json
Imports System.Xml.Serialization
Imports MySql.Data.MySqlClient.MySqlHelper

'Namespace SubjectIndexXML

Imports MySql.Data.MySqlClient
Public Class frmMain

    Dim subjectIndexCls As dbConnect = New dbConnect()
    Dim subjectIndexContent As New List(Of String)
    Dim subjectIndexText As New List(Of String)
    Dim subjectIndexPage As String

   
    Dim tableContentCases As List(Of String) = New List(Of String)
    Dim tableContentLegis As List(Of String) = New List(Of String)
    Dim tableContentSbjIndex As List(Of String) = New List(Of String)

    Dim myCourtList As List(Of String) = New List(Of String)
    Dim refCasesList As List(Of refCasesC) = New List(Of refCasesC)
    Dim refLegList As List(Of refLegC) = New List(Of refLegC)
    Dim subIndexList As List(Of subIndexC) = New List(Of subIndexC)
    Dim indentRefList As List(Of indentRef) = New List(Of indentRef)

    Dim legislationList As List(Of LegislationListC) = New List(Of LegislationListC)
    Dim casesList As List(Of CasesListC) = New List(Of CasesListC)

    Dim tableContentCasesJud As List(Of String) = New List(Of String)
    Dim tableContentLegisJud As List(Of String) = New List(Of String)
    Dim firstLetterJudCases As List(Of String) = New List(Of String)

    Dim LegisJud_SectionList As List(Of String) = New List(Of String)
    Dim subjectIndexUploadFolder As String
    Dim xmlUploadFolder As String
    Dim legislationUploadFolder

    Dim Total_XML_Files As Integer = 0
    Dim overall_XML_Files As Integer = 0
    Dim InProgress_XML_Files As Integer = 0
    Dim Current_Percentage As String = "0%"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '==================== lblPath.Text = "C:\\Users/DSSBNB011\\Desktop\\MLRA_2016_5.XML"
        If File.Exists(tmpPath) = False Then
            My.Computer.FileSystem.WriteAllText(tmpPath, "", False)
        Else
            txtPath.Text = File.ReadAllText(tmpPath)
        End If
        Call log.Clear_Log_Files()
    End Sub


    Public Function ConvertTitleCase(ByVal strToConvert As String) As String
        Return StrConv(strToConvert, VbStrConv.ProperCase)
    End Function
    Public Function ConvertUpperCase(ByVal strToConvert As String) As String
        Return StrConv(strToConvert, VbStrConv.Uppercase)
    End Function
    Public Function ConvertLowerCase(ByVal strToConvert As String) As String
        Return StrConv(strToConvert, VbStrConv.Lowercase)
    End Function

    Private Sub EnableButton(ByVal bool As Boolean)
        btnCreateXML.Enabled = bool
        btnUpdate.Enabled = bool
        btnPath.Enabled = bool
        btnExtractRefLegislation.Enabled = bool
        btnExtractSubjectIndex.Enabled = bool
        btnExtractRefCases.Enabled = bool
        btnPrepare.Enabled = bool
        chkboxdeldata.Enabled = bool
        chkboxdeldatacases.Enabled = bool
        chkboxdeldatalegislation.Enabled = bool
    End Sub

    Private Function DataTable2List(dt As DataTable) As List(Of wordChanger)
        Dim convertedList As List(Of wordChanger) = (From rw In dt.AsEnumerable() Select New wordChanger() With { _
        .wordOld = rw(0).ToString(), _
        .wordNew = rw(1).ToString() _
    }).ToList()
        Return convertedList
    End Function

    Private Shared Function capitalize(mystr As String) As String
        mystr = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(mystr.ToLower())
        Dim citarr As String() = {"Melru", "Mlrhu", "Mlrau", "Melr", "Mlrh", "Mlra", _
            "Mlrs", "Mlrf"}

        For Each cit As String In citarr
            mystr = mystr.Replace(cit, cit.ToUpper())
        Next

        mystr = mystr.Replace(" V.", " v.")
        mystr = mystr.Replace(" Lwn.", " lwn.")
        mystr = mystr.Replace("(Refd)", "(refd)")
        mystr = mystr.Replace("(Dist)", "(dist)")
        mystr = mystr.Replace("(Foll)", "(refd)")
        mystr = mystr.Replace("(Not Foll)", "(not foll)")
        mystr = mystr.Replace("Public Prosecutor", "PP")
        mystr = mystr.Replace("Pp", "PP")

        For Each word In Globals.wordChangeList
            Dim pattern As String = "\b" & word.wordOld & "\b"
            Dim replace As String = word.wordNew
            mystr = Regex.Replace(mystr, pattern, replace, RegexOptions.IgnoreCase)
        Next
        Return mystr
    End Function
    Public Sub InfoTextProgress(ByVal strToWrite As String)
        Application.DoEvents()
        Me.lstProgress.Items.Add(strToWrite & Environment.NewLine)
        Me.lstProgress.SelectedIndex = lstProgress.Items.Count - 1
        Me.lstProgress.Refresh()
        If strToWrite.Contains("successfully") Then
            EnableButton(True)
        Else
            EnableButton(False)
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        subjectIndexCls.DeleteAndClearIndex("api_cases")
        subjectIndexCls.DeleteAndClearIndex("api_legislation")
        '=========== UPDATE REFCASES, REF_LEG_TB, SUBJECTINDEXTB ======================
        InfoTextProgress("Start API Legislation download")
        '============================================================================================
        Using webClient = New System.Net.WebClient()
            Dim json = webClient.DownloadString(APIURLS.LEGISLATIONDB)

            json = json.Replace("'", "|")
            json = HELPER.ReplaceString(json, "&amp;", "&", StringComparison.CurrentCultureIgnoreCase)
            json = HELPER.ReplaceString(json, "&amp", "&", StringComparison.CurrentCultureIgnoreCase)
            legislationList = JsonConvert.DeserializeObject(Of List(Of LegislationListC))(json)

            If legislationList.Count > 10000 Then
                subjectIndexCls.Delete("api_legislation")
            End If

            Dim insSQL As String = "insert into api_legislation(datafilename, title, legislationtype) VALUES "
            Dim extendSQL As String = String.Empty
            Dim data As New List(Of String)()
            Dim counter As Integer = 0

            For Each legislation As LegislationListC In legislationList

                data.Add("('" & legislation.datafilename & "','" & legislation.title & "','" & legislation.legislationtype & "')")
                counter += 1
                If counter Mod 1000 = 0 Then
                    extendSQL = String.Join(",", data.ToArray())
                    extendSQL = extendSQL.Replace("|", """")
                    subjectIndexCls.Insert(insSQL & extendSQL)
                    Application.DoEvents()
                    InfoTextProgress(counter.ToString() & " LEGISLATION download")
                    extendSQL = String.Empty
                    data.Clear()
                End If
            Next
        End Using
        InfoTextProgress("API Legislation Download Completed")

        '=============================================================================================
        InfoTextProgress("Start API Cases Download")

        Using webClient = New System.Net.WebClient()
            Dim json = webClient.DownloadString(APIURLS.CASESDB)
            json = json.Replace("'", "|")
            json = HELPER.ReplaceString(json, "&amp;", "&", StringComparison.CurrentCultureIgnoreCase)
            json = HELPER.ReplaceString(json, "&amp", "&", StringComparison.CurrentCultureIgnoreCase)

            casesList = JsonConvert.DeserializeObject(Of List(Of CasesListC))(json)

            If casesList.Count > 50000 Then
                subjectIndexCls.Delete("api_cases")
            End If

            Dim insSQL As String = "INSERT INTO api_cases(datafilename, title, citation) VALUES "
            Dim extendSQL As String = String.Empty
            Dim data As New List(Of String)()
            Dim counter As Integer = 0

            For Each cases As CasesListC In casesList

                data.Add("('" & cases.datafilename & "','" & cases.title & "','" & cases.citation & "')")
                counter += 1
                If counter Mod 1000 = 0 Then
                    extendSQL = String.Join(",", data.ToArray())
                    extendSQL = extendSQL.Replace("|", """")
                    subjectIndexCls.Insert(insSQL & extendSQL)
                    Application.DoEvents()
                    'btnUpdate.Text = counter.ToString() & " Cases"
                    InfoTextProgress(counter.ToString() & " CASES download")
                    extendSQL = String.Empty
                    data.Clear()
                End If
            Next
        End Using
        InfoTextProgress("API Cases Download Completed")

        '===============================================================================================
        'InfoTextProgress("Start Cases Referred Update")
        'Using webClientLeg = New System.Net.WebClient()

        '    Dim json = webClientLeg.DownloadString(APIURLS.REFCASES)
        '    json = json.Replace("'", "|")
        '    json = HELPER.ReplaceString(json, "&amp;", "&", StringComparison.CurrentCultureIgnoreCase)
        '    json = HELPER.ReplaceString(json, "&amp", "&", StringComparison.CurrentCultureIgnoreCase)
        '    refCasesList = JsonConvert.DeserializeObject(Of List(Of refCasesC))(json)

        '    If refCasesList.Count > 50000 Then
        '        subjectIndexCls.Delete("refcases")
        '    End If

        '    Dim insSQL As String = "INSERT INTO refcases(rootcitation, ReferredCitation, ReferredTitle, type, referredDatafilename) VALUES "
        '    Dim extendSQL As String = String.Empty
        '    Dim data As New List(Of String)
        '    Dim counter As Integer = 0

        '    For Each REFCASES As refCasesC In refCasesList
        '        data.Add("('" & REFCASES.rootCitation & "','" & REFCASES.ReferredCitation & "','" & REFCASES.ReferredTitle & "','" & REFCASES.type & "','" & utility.PrintDatafilename(REFCASES.ReferredCitation) & "')")
        '        counter += 1
        '        If counter Mod 1000 = 0 Then
        '            extendSQL = String.Join(",", data.ToArray())
        '            extendSQL = extendSQL.Replace("|", """")
        '            subjectIndexCls.Insert(insSQL & extendSQL)
        '            Application.DoEvents()
        '            InfoTextProgress(counter.ToString() & " REFERRED_CASES insert")
        '            extendSQL = String.Empty
        '            data.Clear()
        '        End If
        '    Next
        'End Using
        'InfoTextProgress("Cases Referred Update Completed")
        ' Call subjectIndexCls.UpdateReferredCasesDatafilename()
        InfoTextProgress("Please wait while tool checking data...")
        InfoTextProgress("Done. All data successfully updated")
    End Sub
    Dim tmpPath As String = "tmpPath.txt"

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Call log.Clear_Log_Files()
        lblProgressTitle.Text = "EXTRACT CASES INFORMATION"
        If fbd.ShowDialog = Windows.Forms.DialogResult.OK Then
            InfoTextProgress("Please wait while tool Checking XML Files ...")
            Call DeleteFilesFromTempFolder()
            If chkboxdeldatacases.Checked Then
                subjectIndexCls.Delete("xml_cases")
            End If
            txtPath.Text = fbd.SelectedPath
            My.Computer.FileSystem.WriteAllText(tmpPath, fbd.SelectedPath, False)
            txtPath.Refresh()
            Call CopyFilesToTempFolder(fbd.SelectedPath) 'method to copy files from original source to temporary folder
            InfoTextProgress("Start Extracting XML Cases Information")
            Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
            Total_XML_Files = files.Length
            overall_XML_Files = Total_XML_Files * 3
            InProgress_XML_Files = 0
            Dim datafilename As String = ""
            Dim judgment_name As String = ""
            Dim judgment_language As String = ""
            Dim judge_name As String = ""
            Dim court_type As String = ""
            Dim firstLetter As String = ""
            For Each strfilename As String In files
                lblfn.Text = "FileName : " & Path.GetFileNameWithoutExtension(strfilename)
                Dim myxmlcase As New xmlcase(strfilename)
                datafilename = Path.GetFileNameWithoutExtension(strfilename)
                judgment_name = myxmlcase.JUDGMENT_NAME.Replace(Environment.NewLine, " ")
                judgment_language = myxmlcase.JUDGMENT_LANGUAGE
                judge_name = myxmlcase.JUDGE_NAME
                court_type = myxmlcase.COURT_TYPE
                Try
                    firstLetter = judgment_name(0)
                    Dim strSQL As String = "INSERT INTO xml_cases(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter) " & _
                        "VALUES ('" & datafilename & "','" & utility.PrintCitation(datafilename) & "','" & StrConv(EscapeString(judgment_name), VbStrConv.ProperCase) & "','" & judgment_language & "','" & _
                        EscapeString(judge_name) & "','" & court_type & "','" & firstLetter & "')"
                    subjectIndexCls.Insert(strSQL)
                Catch ex As Exception
                    My.Computer.FileSystem.WriteAllText(log.Error_XML, strfilename & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)

                    Continue For
                End Try
                InfoTextProgress("Extract " & datafilename & "   - done.")
                InProgress_XML_Files = InProgress_XML_Files + 1
                lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
                lblPercent.Refresh()
            Next
            InfoTextProgress("Cases Info in " & files.Length.ToString() & " files successfully inserted.")
        End If
    End Sub

    Private Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        'If cbRefCases.Checked Then
        '    Call Extract_Ref_Cases()
        'End If

        If cbRefLegislation.Checked Then
            Call Extract_Ref_Legislation()
        End If

        If cbSubjectIndex.Checked Then
            Call Extract_Subject_Index()
        End If

    End Sub
    Private Sub Extract_XML_Cases()
        InfoTextProgress("Please wait while tool Checking XML Files ...")
        Call DeleteFilesFromTempFolder()
        If chkboxdeldatacases.Checked Then
            subjectIndexCls.Delete("xml_cases")
        End If

        My.Computer.FileSystem.WriteAllText(tmpPath, fbd.SelectedPath, False)
        txtPath.Refresh()

        Call CopyFilesToTempFolder(fbd.SelectedPath) 'method to copy files from original source to temporary folder
        InfoTextProgress("Start Extracting XML Cases Information")

        Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
        Total_XML_Files = files.Length
        InProgress_XML_Files = 0

        Dim datafilename As String = ""
        Dim judgment_name As String = ""
        Dim judgment_language As String = ""
        Dim judge_name As String = ""
        Dim court_type As String = ""
        Dim firstLetter As String = ""

        For Each strfilename As String In files

            lblfn.Text = "FileName : " & Path.GetFileNameWithoutExtension(strfilename)
            Dim myxmlcase As New xmlcase(strfilename)
            datafilename = Path.GetFileNameWithoutExtension(strfilename)
            judgment_name = myxmlcase.JUDGMENT_NAME.Replace(Environment.NewLine, " ")
            judgment_language = myxmlcase.JUDGMENT_LANGUAGE
            judge_name = myxmlcase.JUDGE_NAME
            court_type = myxmlcase.COURT_TYPE

            Try
                firstLetter = judgment_name(0)
                Dim strSQL As String = "INSERT INTO xml_cases(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter) " & _
                    "VALUES ('" & datafilename & "','" & utility.PrintCitation(datafilename) & "','" & StrConv(EscapeString(judgment_name), VbStrConv.ProperCase) & "','" & judgment_language & "','" & _
                    EscapeString(judge_name) & "','" & court_type & "','" & firstLetter & "')"
                subjectIndexCls.Insert(strSQL)

            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(log.Error_XML, strfilename & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
                Continue For
            End Try

            InfoTextProgress("Extract " & datafilename & "   - done.")
            InProgress_XML_Files = InProgress_XML_Files + 1
            lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
            lblPercent.Refresh()
        Next
        InfoTextProgress("Cases Info in " & files.Length.ToString() & " files successfully inserted.")
    End Sub

    Private Sub Extract_Ref_Cases()
        My.Computer.FileSystem.WriteAllText("ref_cases.txt", "", False)
        lblProgressTitle.Text = "EXTRACT REFERRED CASES"
        lblPercent.Text = "0%"
        If chkboxdeldatacases.Checked Then
            subjectIndexCls.DeleteAndClearIndex("ref_cases")
        End If

        InfoTextProgress("Start Extracting Referred Cases From XML")
        Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
        Dim rootCitation As String = ""
        Dim referredCitation As String = ""
        Dim referredTitle As String = ""
        Dim referredType As String = ""
        Dim referredDatafilename As String = ""
        Dim insertSQL As String = ""
        Total_XML_Files = files.Length
        InProgress_XML_Files = 0

        For Each strfilename As String In files
            Dim referredCasesList As New List(Of String)
            If utility.REFERRED_CASES_TYPE(strfilename) = True Then
                referredCasesList = ExtractReferredCasesHeadnote(strfilename)
            Else
                ' ===== myfilesi = ExtractReferredCasesJudgment(strfilename) '====> SKIP FOR TEMPORARY
            End If
            For Each referredCase As String In referredCasesList
                Try
                    rootCitation = Path.GetFileNameWithoutExtension(strfilename)
                    referredCitation = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(0)
                    If referredCitation.Contains(" MLRA ") Or referredCitation.Contains(" MELR ") Or referredCitation.Contains(" MLRH ") Then
                        referredTitle = subjectIndexCls.GetJudgmentNameFromCitation(referredCitation)
                    Else
                        referredTitle = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(1)
                    End If
                    referredTitle = ReplaceProperWords(StrConv(referredTitle, VbStrConv.ProperCase))

                    My.Computer.FileSystem.WriteAllText("ref_cases.txt", insertSQL & Environment.NewLine, True)

                    referredDatafilename = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(2)
                    referredType = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(3)
                    insertSQL = "insert into ref_cases (source_citation, source_datafilename, referred_citation, referred_title, referred_type) " & _
                                            "values('" & utility.PrintCitation(referredDatafilename) & "','" & referredDatafilename & "','" & _
                                            referredCitation & "','" & EscapeString(referredTitle.Replace("()", "").Trim()) & "','" & referredType & "')"
                    subjectIndexCls.Insert(insertSQL)
                Catch ex As Exception
                    My.Computer.FileSystem.WriteAllText(log.Error_XML, strfilename & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
                    Continue For
                End Try
            Next

            InfoTextProgress("      " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
            lstProgress.Refresh()
            InProgress_XML_Files = InProgress_XML_Files + 1
            lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
            lblPercent.Refresh()

        Next
        InfoTextProgress("Referred Cases in " & files.Length.ToString() & " files successfully inserted.")

    End Sub

    Private Sub Extract_Ref_Legislation()

        lblProgressTitle.Text = "EXTRACT REFERRED LEGISLATIONS"
        lblPercent.Text = "0%"
        InfoTextProgress("Start Extracting Referred legislations")

        If chkboxdeldatalegislation.Checked Then
            subjectIndexCls.DeleteAndClearIndex("ref_legislations")
            subjectIndexCls.DeleteAndClearIndex("tmp_ref_leg_foreign")
            subjectIndexCls.DeleteAndClearIndex("tmp_ref_leg_local")
            subjectIndexCls.DeleteAndClearIndex("tmp_ref_leg")
        End If

        Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
        For Each strfilename As String In files
            RemoveTagFromXMLSource(strfilename, "//judgment//blockquote")
        Next
        Total_XML_Files = files.Length
        InProgress_XML_Files = 0

        For Each strfilename As String In files
            lblfn.Text = "FileName : " + Path.GetFileNameWithoutExtension(strfilename)
            Dim myxmlcase As New xmlcase(strfilename)

            If utility.REFERRED_LEGISLATION_TYPE(strfilename) = True Then
                Call Extract_Temp_Ref_Legislation_Headnote_To_DB(strfilename)
                InfoTextProgress("     " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
            Else
                ' ================== myfilesi = ExtractReferredLegislationJudgment(strfilename)
            End If

            InProgress_XML_Files = InProgress_XML_Files + 1
            lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
            lblPercent.Refresh()
        Next

        InfoTextProgress("Please wait while tool extraction additional data...")

        Dim query As String = ""
        Dim Local_Legis_List As List(Of String) = subjectIndexCls.Extract_Temp_Ref_Legislation_To_Table("local")
        Dim arr As String()

        For Each Local_legis As String In Local_Legis_List
            Try
                arr = Local_legis.Split("#")
            Catch ex As Exception
            End Try

            query = "insert into ref_legislations (source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
                "values('" & arr(0) & "','" & arr(1) & "','" & arr(2) & "','" & EscapeString(arr(3)) & "','" & arr(4) & "','" & Extractor.ReferredLegislations.Replace_Legislation_Sub_No_Type(arr(5)) & "','" & arr(6) & "','" & EscapeString(arr(7)) & "')"
            subjectIndexCls.Insert(query)
        Next

        Dim Foreign_Legis_List As List(Of String) = subjectIndexCls.Extract_Temp_Ref_Legislation_To_Table("foreign")
        For Each Foreign_Legis As String In Foreign_Legis_List
            Try
                arr = Foreign_Legis.Split("#")
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(log.Error_XML, Foreign_Legis & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
                Continue For
            End Try

            query = "insert into ref_legislations (source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
                "values('" & arr(0) & "','" & arr(1) & "','" & arr(2) & "','" & EscapeString(arr(3)) & "','" & arr(4) & "','" & Extractor.ReferredLegislations.Replace_Legislation_Sub_No_Type(arr(5)) & "','" & arr(6) & "','" & EscapeString(arr(7)) & "')"
            subjectIndexCls.Insert(query)
        Next

        InfoTextProgress("Legislations data in " & files.Length.ToString() & " files successfully inserted.")

    End Sub

    Private Sub Extract_Subject_Index()

        lblProgressTitle.Text = "EXTRACT SUBJECT INDEX"
        lblPercent.Text = "0%"
        My.Computer.FileSystem.WriteAllText("getfilesubjectindex.txt", "", False)
        If chkboxdeldata.Checked Then
            subjectIndexCls.DeleteAndClearIndex("subject_index")
        End If

        '=== Dim result As DialogResult = folderdialogsubjectindex.ShowDialog()
        '=== If result = DialogResult.OK Then
        '=== Call CopyFilesToTempFolder(folderdialogsubjectindex.SelectedPath) 'method to copy files from original source to temporary folder

        InfoTextProgress("Start uploading subject index")

        ' The user selected a folder and pressed the OK button.
        ' We print the number of files found.
        'Dim files As String() = Directory.GetFiles(folderdialogsubjectindex.SelectedPath, "*.xml")

        Dim files As String() = Directory.GetFiles(tempPath, "*.xml")

        Dim judgname As String = ""
        Dim judgmentname As String = ""

        Dim caselanguage As String = ""
        Dim catchwordNodeList As XmlNodeList
        Dim subjectIndexNodeList As XmlNodeList

        Dim pageno As String = ""
        Dim pagc As Integer = 0

        Dim patternSubjectIndex As Integer
        Dim xmlDoc As New XmlDocument
        Dim root As XmlNode

        Total_XML_Files = files.Length
        InProgress_XML_Files = 0

        For Each strfilename As String In files

            patternSubjectIndex = GetSubjectIndexType(strfilename)
            xmlDoc.Load(strfilename)
            root = xmlDoc.DocumentElement

            Try
                Application.DoEvents()
                lblfn.Text = "FileName : " & Path.GetFileNameWithoutExtension(strfilename)

                Dim myxmlcase As New xmlcase(strfilename)
                judgmentname = myxmlcase.JUDGMENT_NAME()
                judgmentname = judgmentname.Trim()
                caselanguage = myxmlcase.JUDGMENT_LANGUAGE()
                judgname = myxmlcase.JUDGE_NAME()
                judgname = judgnameformat(judgname, caselanguage)

                Dim myfilesi As List(Of String) = New List(Of String)
                Dim datafilename As String = Path.GetFileNameWithoutExtension(strfilename)
                Dim strsubjindx As String = ""
                Dim level1 As String = ""
                Dim level2 As String = ""
                Dim strsummary As String = ""
                Dim strCatchword As String = ""  '=== GetCatchword(strfilename)
                Dim strLanguage As String = caselanguage
                Dim strToRemove As String = ""
                Dim lstSubjectIndexNodes As XmlNodeList

                If patternSubjectIndex = 1 Then ' whenever pattern is headnote//catchword/subjectindex is detected
                    catchwordNodeList = myxmlcase.CATCHWORDSWITHP
                    For Each node As XmlNode In catchwordNodeList
                        strCatchword = node.InnerText
                        strsubjindx = strCatchword.Split({" - "}, StringSplitOptions.None)(0)
                        strToRemove = strsubjindx & " - "
                        level1 = node.InnerXml.Split({"<SUBJECT_INDEX>", "</SUBJECT_INDEX>"}, StringSplitOptions.None)(1)
                        level1 = level1.Replace(":", "").Trim
                        level2 = strCatchword.Split({":", " - "}, StringSplitOptions.None)(1).Trim
                        strsummary = strCatchword.Replace(strToRemove, "")
                        If strsummary.Contains(": ") Then
                            strsummary = strCatchword.Replace(strsubjindx, "")
                        End If
                        Dim sql As String = "INSERT INTO subject_index(source_citation, source_datafilename, subject_index, level1, level2, summary) " & _
                            "VALUES ('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & EscapeString(strsubjindx.ToUpper) & "','" & _
                            EscapeString(level1) & "','" & EscapeString(level2) & "','" & EscapeString(strsummary) & "')"
                        subjectIndexCls.Insert(sql)
                    Next

                ElseIf patternSubjectIndex = 2 Then ' subject_index tag with dash
                    subjectIndexNodeList = xmlDoc.SelectNodes(root.Name & "//SUBJECT_INDEX")
                    For Each node As XmlNode In subjectIndexNodeList
                        strCatchword = ""
                        strsummary = ""
                        strsubjindx = node.InnerText

                        If strsubjindx.Contains(" - ") Then
                            level1 = strsubjindx.Split({" - "}, StringSplitOptions.None)(0)
                            level2 = strsubjindx.Split({" - "}, StringSplitOptions.None)(1)
                        Else
                            level1 = strsubjindx
                            level2 = ""
                        End If
                        Dim sql As String = "INSERT INTO subject_index(source_citation, source_datafilename, subject_index, level1, level2, summary) " & _
                            "VALUES ('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & EscapeString(strsubjindx.ToUpper) & "','" & _
                            EscapeString(level1) & "','" & EscapeString(level2) & "','" & EscapeString(strsummary) & "')"
                        subjectIndexCls.Insert(sql)
                    Next

                Else
                    ' NO SUBJECT INDEX
                End If
                InfoTextProgress("     " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
                lstProgress.Refresh()
            Catch ex As Exception
                InfoTextProgress(" - " & Path.GetFileNameWithoutExtension(strfilename) & "   - Error / Wrong format")
                My.Computer.FileSystem.WriteAllText(log.Error_XML, Path.GetFileNameWithoutExtension(strfilename) & ", Problem : " & ex.Message.ToString, True)
                Continue For
            End Try
            InProgress_XML_Files = InProgress_XML_Files + 1
            lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
            lblPercent.Refresh()
        Next
        lblPercent.Text = "100%"
        InfoTextProgress("Subject index data in " & files.Length.ToString() & " files successfully inserted.")
        ' End If
    End Sub
    Private Sub btnExtractSubjectIndex_Click(sender As Object, e As EventArgs)
        'lblProgressTitle.Text = "EXTRACT SUBJECT INDEX"
        'lblPercent.Text = "0%"
        'My.Computer.FileSystem.WriteAllText("getfilesubjectindex.txt", "", False)
        'If chkboxdeldata.Checked Then
        '    subjectIndexCls.DeleteAndClearIndex("subject_index")
        'End If
        ''Dim result As DialogResult = folderdialogsubjectindex.ShowDialog()
        ''If result = DialogResult.OK Then
        ''Call CopyFilesToTempFolder(folderdialogsubjectindex.SelectedPath) 'method to copy files from original source to temporary folder
        'InfoTextProgress("Start uploading subject index")
        '' The user selected a folder and pressed the OK button.
        '' We print the number of files found.
        ''
        ''Dim files As String() = Directory.GetFiles(folderdialogsubjectindex.SelectedPath, "*.xml")
        'Dim files As String() = Directory.GetFiles(tempPath, "*.xml")

        'Dim judgname As String = ""
        'Dim judgmentname As String = ""
        'Dim caselanguage As String = ""
        'Dim catchwordNodeList As XmlNodeList
        'Dim subjectIndexNodeList As XmlNodeList

        'Dim pageno As String = ""
        'Dim pagc As Integer = 0

        'Dim patternSubjectIndex As Integer
        'Dim xmlDoc As New XmlDocument
        'Dim root As XmlNode

        'Total_XML_Files = files.Length
        'InProgress_XML_Files = 0

        'For Each strfilename As String In files
        '    Application.DoEvents()
        '    patternSubjectIndex = GetSubjectIndexType(strfilename)
        '    xmlDoc.Load(strfilename)
        '    root = xmlDoc.DocumentElement
        '    Try
        '        Application.DoEvents()
        '        lblfn.Text = "FileName : " & Path.GetFileNameWithoutExtension(strfilename)

        '        Dim myxmlcase As New xmlcase(strfilename)
        '        judgmentname = myxmlcase.JUDGMENT_NAME()
        '        judgmentname = judgmentname.Trim()
        '        caselanguage = myxmlcase.JUDGMENT_LANGUAGE()
        '        judgname = myxmlcase.JUDGE_NAME()
        '        judgname = judgnameformat(judgname, caselanguage)

        '        Dim myfilesi As List(Of String) = New List(Of String)

        '        Dim datafilename As String = Path.GetFileNameWithoutExtension(strfilename)

        '        Dim strsubjindx As String = ""
        '        Dim level1 As String = ""
        '        Dim level2 As String = ""
        '        Dim strsummary As String = ""
        '        Dim strCatchword As String = ""  '=== GetCatchword(strfilename)
        '        Dim strLanguage As String = caselanguage

        '        Dim strToRemove As String = ""
        '        Dim lstSubjectIndexNodes As XmlNodeList

        '        If patternSubjectIndex = 1 Then ' whenever pattern is headnote//catchword/subjectindex is detected
        '            catchwordNodeList = myxmlcase.CATCHWORDSWITHP
        '            For Each node As XmlNode In catchwordNodeList
        '                strCatchword = node.InnerText
        '                strsubjindx = strCatchword.Split({" - "}, StringSplitOptions.None)(0)
        '                strToRemove = strsubjindx & " - "
        '                level1 = node.InnerXml.Split({"<SUBJECT_INDEX>", "</SUBJECT_INDEX>"}, StringSplitOptions.None)(1)
        '                level1 = level1.Replace(":", "").Trim
        '                level2 = strCatchword.Split({":", " - "}, StringSplitOptions.None)(1).Trim
        '                strsummary = strCatchword.Replace(strToRemove, "")
        '                If strsummary.Contains(": ") Then
        '                    strsummary = strCatchword.Replace(strsubjindx, "")
        '                End If
        '                Dim sql As String = "INSERT INTO subject_index(source_citation, source_datafilename, subject_index, level1, level2, summary) " & _
        '                    "VALUES ('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & EscapeString(strsubjindx.ToUpper) & "','" & _
        '                    EscapeString(level1) & "','" & EscapeString(level2) & "','" & EscapeString(strsummary) & "')"
        '                subjectIndexCls.Insert(sql)
        '            Next
        '        ElseIf patternSubjectIndex = 2 Then ' subject_index tag with dash
        '            subjectIndexNodeList = xmlDoc.SelectNodes(root.Name & "//SUBJECT_INDEX")
        '            For Each node As XmlNode In subjectIndexNodeList
        '                strCatchword = ""
        '                strsummary = ""
        '                strsubjindx = node.InnerText

        '                If strsubjindx.Contains(" - ") Then
        '                    level1 = strsubjindx.Split({" - "}, StringSplitOptions.None)(0)
        '                    level2 = strsubjindx.Split({" - "}, StringSplitOptions.None)(1)
        '                Else
        '                    level1 = strsubjindx
        '                    level2 = ""
        '                End If
        '                Dim sql As String = "INSERT INTO subject_index(source_citation, source_datafilename, subject_index, level1, level2, summary) " & _
        '                    "VALUES ('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & EscapeString(strsubjindx.ToUpper) & "','" & _
        '                    EscapeString(level1) & "','" & EscapeString(level2) & "','" & EscapeString(strsummary) & "')"
        '                subjectIndexCls.Insert(sql)
        '            Next
        '        Else
        '            ' NO SUBJECT INDEX
        '        End If
        '        InfoTextProgress("     " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
        '        lstProgress.Refresh()
        '    Catch ex As Exception
        '        InfoTextProgress(" - " & Path.GetFileNameWithoutExtension(strfilename) & "   - Error / Wrong format")
        '        My.Computer.FileSystem.WriteAllText("errorXML.txt", Path.GetFileNameWithoutExtension(strfilename) & ", Problem : " & ex.Message.ToString, True)
        '        Continue For
        '    End Try
        '    InProgress_XML_Files = InProgress_XML_Files + 1
        '    lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
        '    lblPercent.Refresh()
        'Next
        ' ''===''====MessageBox.Show("SUBJECT INDEX : " & files.Length.ToString() & " files successfully uploaded into subject index table.")
        'InfoTextProgress("Subject index data in " & files.Length.ToString() & " files successfully inserted.")
        '' End If
    End Sub
    Public Function doReplace(originalString As String, oldValue As String, newValue As String, comparisonType As StringComparison) As String
        Dim startIndex As Integer = 0
        While True
            startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType)
            If startIndex = -1 Then
                Exit While
            End If
            originalString = (originalString.Substring(0, startIndex) & newValue) + originalString.Substring(startIndex + oldValue.Length)
            startIndex += newValue.Length
        End While
        Return originalString
    End Function
    Public Function judgnameformat(strjdjname As String, lang As String) As String
        Dim judgrankEng As String() = {"CJ", "PCA", "CJM", "CJSS", "FCJ", "FCJJ", _
            "JCA", "JJCA", "J", "JC"}
        Dim judgrankMal As String() = {"KHN", "PMR", "HBM", "HBSS", "HMP", "HHMP", _
            "HMR", "HHMR", "H", "PK"}

        If lang.ToUpper() = "ENGLISH" Then
            For Each word As String In judgrankEng
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & " ", " " & word.ToUpper() & " ", StringComparison.CurrentCultureIgnoreCase)
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & ",", " " & word.ToUpper() & ",", StringComparison.CurrentCultureIgnoreCase)
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & ")", " " & word.ToUpper() & ")", StringComparison.CurrentCultureIgnoreCase)
            Next
        Else

            For Each word As String In judgrankMal
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & " ", " " & word.ToUpper() & " ", StringComparison.CurrentCultureIgnoreCase)
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & ",", " " & word.ToUpper() & ",", StringComparison.CurrentCultureIgnoreCase)
                strjdjname = doReplace(strjdjname, (Convert.ToString(" ") & word) & ")", " " & word.ToUpper() & ")", StringComparison.CurrentCultureIgnoreCase)
            Next
        End If

        strjdjname = doReplace(strjdjname, " V.", " v.", StringComparison.CurrentCultureIgnoreCase)
        strjdjname = doReplace(strjdjname, " V ", " v ", StringComparison.CurrentCultureIgnoreCase)

        strjdjname = doReplace(strjdjname, " lwn.", " lwn.", StringComparison.CurrentCultureIgnoreCase)
        strjdjname = doReplace(strjdjname, " lwn ", " lwn ", StringComparison.CurrentCultureIgnoreCase)

        Return strjdjname
    End Function
    Private Function FormatCit(cit As String, citationtype As Integer) As String
        Dim returns As String = ""
        cit = Regex.Replace(cit, "\s+", " ")
        cit = cit.Replace(Environment.NewLine, "").ToUpper().Trim()

        If citationtype = 1 Then
            Dim s As String() = cit.Split(New String() {" "}, StringSplitOptions.None)
            If s.Count() = 4 Then
                returns = s(2).ToString() & "_" & s(0).ToString().Replace("[", "").Replace("]", "") & "_" & s(1).ToString() & "_" & s(3).ToString()
            ElseIf s.Count() = 3 Then
                returns = s(1).ToString() & "_" & s(0).ToString().Replace("[", "").Replace("]", "") & "_" & s(2).ToString()
            Else
                returns = "Error"
            End If

        Else
            Dim s As String() = cit.Split(New String() {"_"}, StringSplitOptions.None)
            If s.Count() = 4 Then
                returns = "[" & s(1).ToString() & "] " & s(2).ToString() & " " & s(0).ToString() & " " & s(3).ToString()
            ElseIf s.Count() = 3 Then
                returns = "[" & s(1).ToString() & "] " & s(0).ToString() & " " & s(2).ToString()
            Else
                returns = "Error"
            End If
        End If

        Return returns

    End Function

    Private Function GetCatchword(ByVal fileName As String) As String
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(fileName)
        Dim root As String = xmlDoc.DocumentElement.Name
        Dim catchwordsNode As XmlNode
        Dim returnValue As String = ""
        'Try
        '    catchword = xmlDoc.DocumentElement.SelectSingleNode("HEADNOTE//CATCHWORDS")
        '    returnValue = Regex.Replace(catchword.InnerXml, "<.*?>", "").Replace("<br><i>", " ").Replace("'", "")
        '    Return returnValue
        'Catch ex As Exception
        '    Return ""
        'End Try

        Dim strCatchword As String = ""
        Try
            catchwordsNode = xmlDoc.SelectSingleNode(root & "/HEADNOTE/CATCHWORDS")
            For Each subjectIndexNode As XmlNode In catchwordsNode
                strCatchword = subjectIndexNode.InnerText.ToString.Replace(":", " - ")
            Next

        Catch ex As Exception
            Dim nodeList As XmlNodeList = xmlDoc.SelectNodes(root & "/HEADNOTE/SUBJECT_INDEX")
            For Each node As XmlNode In nodeList
                strCatchword = node.InnerText
            Next

        Finally
            Dim nodeList As XmlNodeList = xmlDoc.SelectNodes(root & "/SUBJECT_INDEX")
            For Each node As XmlNode In nodeList
                strCatchword = node.InnerText
            Next
        End Try

        Return strCatchword
    End Function
    Private Function GenerateNewXMLName(ByVal LoadedFolder As String) As String
        If Directory.Exists(LoadedFolder) Then
            Dim newName As String
            Try
                Dim arrFile = LoadedFolder.Split("\")
                newName = arrFile(arrFile.Count - 1)
                Return newName
            Catch ex As Exception
                Return ""
            End Try
        End If
    End Function

    Private Function SubjectIndexSummary(ByVal xmlfile As StreamReader, ByVal startWords As String) As String
        Try
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(xmlfile)
            Dim node As XmlNode = xmlDoc.SelectSingleNode("CATCHWORDS//p")
            For Each strPara As String In node.InnerXml.ToString
                If strPara.StartsWith(startWords) = True Then
                    Return strPara
                Else
                    Return "NOT FOUND"
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    Private Function GetSubjectIndexLevelData(ByVal strSubjectIndex As String, ByVal index As Integer) As String
        Dim result As String = strSubjectIndex
        Try
            result = Trim(strSubjectIndex.Split({" - "}, StringSplitOptions.None)(index))
        Catch ex As Exception
            result = strSubjectIndex
        End Try
        Return result
    End Function
    ''' <summary>
    ''' Get the subject index Patter: 1 - With HEADNOTE and CATCHWORDS, 2 - SUBJECT_INDEX with Dash, 3 - SUBJECT_INDEX without Dash, 0 - NosubjectIndex
    ''' </summary>
    Private Function GetSubjectIndexType(ByVal xmlFilePath As String) As Integer
        Dim resultPattern As Integer
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(xmlFilePath)
        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim node As XmlNode
        Try
            node = xmlDoc.SelectSingleNode(root.Name & "//" & "HEADNOTE//CATCHWORDS//SUBJECT_INDEX")
            If IsNothing(node) Then
                node = xmlDoc.SelectSingleNode(root.Name & "//" & "SUBJECT_INDEX")

                'If node.InnerText.Contains(" - ") Then
                '    resultPattern = 2
                'Else
                '    resultPattern = 3
                'End If

                resultPattern = 2
            Else
                resultPattern = 1
            End If
        Catch ex As Exception
            resultPattern = 0
        End Try
        Return resultPattern
    End Function

    Private Function RemoveQuote(ByVal str As String) As String
        Dim result As String
        result = str.Replace("""", "'")
        result = str.Replace("'", "''")
        result = str.Replace("()", "")
        Return result
    End Function

    Public tempPath As String = "temp"
    Private Sub CopyFilesToTempFolder(ByVal selectedPath As String)
        If Directory.Exists(tempPath) Then
        Else
            Directory.CreateDirectory(tempPath)
        End If
        For Each filePath As String In Directory.GetFiles(fbd.SelectedPath)
            My.Computer.FileSystem.CopyFile(filePath, tempPath & "\" & Path.GetFileName(filePath), True)
        Next
    End Sub
    Private Sub DeleteFilesFromTempFolder()
        Try
            My.Computer.FileSystem.DeleteDirectory(tempPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnExtractCases_Click(sender As Object, e As EventArgs)
        Call DeleteFilesFromTempFolder()
        If chkboxdeldatacases.Checked Then
            subjectIndexCls.Delete("xml_cases")
        End If

        Dim result As DialogResult = saveFolderPath.ShowDialog()
        If result = DialogResult.OK Then
            Call CopyFilesToTempFolder(saveFolderPath.SelectedPath) 'method to copy files from original source to temporary folder

            InfoTextProgress("Start Extracting XML Cases Information")
            Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
            Dim datafilename As String = ""
            Dim judgment_name As String = ""
            Dim judgment_language As String = ""
            Dim judge_name As String = ""
            Dim court_type As String = ""
            Dim firstLetter As String = ""
            For Each strfilename As String In files
                'My.Computer.FileSystem.WriteAllText("txtCases.txt", strfilename + Environment.NewLine, True)

                lblfn.Text = "FileName : " & Path.GetFileNameWithoutExtension(strfilename)
                Dim myxmlcase As New xmlcase(strfilename)
                datafilename = Path.GetFileNameWithoutExtension(strfilename)
                judgment_name = myxmlcase.JUDGMENT_NAME.Replace(Environment.NewLine, " ")
                judgment_language = myxmlcase.JUDGMENT_LANGUAGE
                judge_name = myxmlcase.JUDGE_NAME
                court_type = myxmlcase.COURT_TYPE

                Try
                    firstLetter = judgment_name(0)
                    Dim strSQL As String = "INSERT INTO xml_cases(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter) " & _
                        "VALUES ('" & datafilename & "','" & utility.PrintCitation(datafilename) & "','" & EscapeString(judgment_name) & "','" & judgment_language & "','" & _
                        EscapeString(judge_name) & "','" & court_type & "','" & firstLetter & "')"
                    subjectIndexCls.Insert(strSQL)
                Catch ex As Exception
                    MsgBox(strfilename)
                    Continue For
                End Try
                InfoTextProgress("Extract " & datafilename & "   -  done.")
            Next
            InfoTextProgress("Extract Cases Info for " & files.Length.ToString() & " files successfully inserted.")
        End If
    End Sub

    Private Function GetLegislation(ByVal xmlFilename As String)
        Dim xmlreader As XmlReader
        Dim setting As XmlReaderSettings

        Dim xmldocument As New XmlDocument
        If My.Computer.FileSystem.FileExists(xmlFilename) Then
            xmldocument.Load(xmlFilename)
        End If
    End Function
    Private Function ReplaceProperWords(ByVal strReturn As String) As String
        strReturn = strReturn.Replace(" Pp ", " PP ").Replace("Pp ", "PP ").Replace(" Pp", " PP").Replace(" V. ", " v. ").Replace("V. ", "v. ").Replace("Pp[", "PP[") _
        .Replace("Lwn. ", "lwn. ").Replace("(Refd)", "(refd)").Replace("(Foll)", "(foll)").Replace(" Ii ", " II ").Replace(" Iii ", " III ") _
        .Replace("Mlra", "MLRA").Replace("Mlrh", "MLRH").Replace("Melr", "MELR").Replace("Jjca", "JJCA").Replace("Jca", "JCA").Replace("Jhc", "JHC") _
        .Replace("Jjhc", "JJHC").Replace("Mlrau", "MLRAU").Replace("Mlrhu", "MLRHU").Replace("Melru", "MELRU").Replace("Public Prosecutor", "PP").Replace("MLRAu", "MLRAU").Replace("MLRHu", "MLRHU").Replace("MELRu", "MELRU") _
        .Replace("Bsn ", "BSN").Replace(" V ", " v. ").Replace(" Yb ", " YB ").Replace("''", "' ").Replace("()", "").Replace("((", "(").Replace("))", ")").Replace("amp;amp;", "amp;") _
        .Replace("&lt;P&gt;", "").Replace("&lt;/P&gt;", "").Replace("&lt;B&gt;", "").Replace("&lt;/B&gt;", "").Replace("&lt;I&gt;", "").Replace("&lt;/I&gt;", "") _
        .Replace("<P>", "").Replace("</P>", "").Replace("<B>", "").Replace("</B>", "").Replace("<I>", "").Replace("</I>", "") _
        .Replace("&amp;amp;", "&amp;").Replace("()", "")
        Return strReturn
    End Function

    Private Function TagAndValue(ByVal Tag As String, ByVal Value As String)
        Dim returnValue As String = Nothing
        returnValue = "<" & Tag & ">" & Value & "</" & Tag & ">" & Environment.NewLine
        Return returnValue
    End Function
    Private Sub XMLWriter(ByVal w As XmlWriter, ByVal tag1 As String, ByVal tag2 As String)
        Dim strToWrite As String = Nothing
        Dim writer As XmlWriter()
        Dim xmlDocument As New XmlDocument
    End Sub

    Dim strDocument As String = ""
    Dim strCourt As String = ""
    Dim strYear As String = ""
    Dim strVolume As String = ""
    Dim strPart As String = ""
    Dim strCitation As String = ""

    Private Function GetTitleForXML(ByVal fileName As String) As String
        strCitation = UCase(fileName).Replace(".XML", "")
        Try
            strCourt = strCitation.Split("_")(0)
            strYear = strCitation.Split("_")(1)
            strVolume = strCitation.Split("_")(2)
        Catch ex As Exception
            Return strDocument
        End Try
    End Function
    Private Function GetCourtName(ByVal str As String) As String
        Select Case UCase(str)
            Case Is = "MLRA"
                Return "COURT OF APPEAL"
            Case Is = "MLRH"
                Return "HIGH COURT"
            Case Is = "MELR"
                Return "INDUSTRIAL COURT"
            Case Is = "MLRS"
                Return "SESSION COURT"
            Case Else
                Return ""
        End Select
    End Function
    Private Function RemoveBackSlash(ByVal str As String) As String
        Return str.Replace("\\", "\").Replace("\", " ").Replace("\%", " ").Replace("\ %", " ").Replace(Environment.NewLine, "").Replace("%", " ").Replace("<br />", "").Replace(Environment.NewLine, "").Replace("<br/>", " ") _
        .Replace("       ", " ") _
        .Replace("      ", " ") _
        .Replace("     ", " ") _
        .Replace("    ", " ") _
        .Replace("   ", " ") _
        .Replace("  ", " ")
        '.Replace("amp;amp;", "amp;")
    End Function
    Private Shared Function MySQLEscapee(str As String) As String
        Dim returnVal As String = ""
        Dim tmpValue As String = ""

        If str.Contains("\x00") Then
            tmpValue = str.Replace("\x00", "\\0")
        End If

        If str.Contains("\b") Then
            tmpValue = str.Replace("\b", "\\b")
        End If

        If str.Contains("\b") Then
            tmpValue = str.Replace("\b", "\\b")
        End If

        If str.Contains("\b") Then
            tmpValue = str.Replace("\b", "\\b")
        End If

        If str.Contains("\r") Then
            tmpValue = str.Replace("\r", "\\r")
        End If

        If str.Contains("\n") Then
            tmpValue = str.Replace("\n", "\\n")
        End If

        If str.Contains("\t") Then
            tmpValue = str.Replace("\t", "\\t")
        End If

        If str.Contains("\u001A") Then
            tmpValue = str.Replace("\u001A", "\\Z")
        End If
    End Function
    Private Shared Function MySQLEscapeChar(strWord As String) As String
        Return Regex.Replace(strWord, "[\x00'""\b\n\r\t\cZ\\%_]", Function(m As Match)
                                                                      Dim v As String = m.Value
                                                                      Select Case v
                                                                          Case Is = "\x00" 'ASCII NUL (0x00) character
                                                                              Return "\\0"

                                                                          Case Is = "\b" 'BACKSPACE character
                                                                              Return "\\b"

                                                                          Case Is = "\n" 'NEWLINE (linefeed) character
                                                                              Return "\\n"

                                                                          Case Is = "\r" 'CARRIAGE RETURN character
                                                                              Return "\\r"

                                                                          Case Is = "\t" 'TAB
                                                                              Return "\\t"

                                                                          Case Is = "\u001A" 'Ctrl-Z
                                                                              Return "\\Z"

                                                                          Case Is = "\" 'backslash
                                                                              Return ""

                                                                          Case Else
                                                                              Return "\\"  '& v

                                                                      End Select
                                                                  End Function)
    End Function
    Private Shared Function MySQLEscape(str As String) As String
        Return Regex.Replace(str, "[\x00'""\b\n\r\t\cZ\\%_]", Function(m As Match)
                                                                  Dim v As String = m.Value
                                                                  Select Case v
                                                                      Case Is = "\x00" 'ASCII NUL (0x00) character
                                                                          Return "\0"

                                                                      Case Is = "\b" 'BACKSPACE character
                                                                          Return "\b"

                                                                      Case Is = "\n" 'NEWLINE (linefeed) character
                                                                          Return " "

                                                                      Case "\r" 'CARRIAGE RETURN character
                                                                          Return "\r"

                                                                      Case Is = "\t" 'TAB
                                                                          Return "\t"

                                                                      Case Is = "\u001A" 'Ctrl-Z
                                                                          Return "\Z"
                                                                      Case Is = "\%"
                                                                          Return " "

                                                                      Case Else
                                                                          Return v
                                                                  End Select
                                                              End Function)
    End Function
    Private Function PrintDatafilename(ByVal T As String) As String
        Dim RC As String = ""
        If T.Contains("  ") Then
            RC = T.Replace("  ", " ")
        Else
            RC = T
        End If
        ' New Citation formate 
        Try
            If RC.Contains("_") Then
                RC = RC.Trim()
            Else
                RC = RC.Replace("[", "").Replace("]", "").Trim()
                Dim ArrTag() As String = RC.Split({" "}, StringSplitOptions.None)
                If ArrTag.Count = 4 Then
                    RC = ArrTag(2) & "_" & ArrTag(0) & "_" & ArrTag(1) & "_" & ArrTag(3)
                Else
                    RC = ArrTag(1) & "_" & ArrTag(0) & "_" & ArrTag(2)
                End If
            End If
        Catch ex As Exception
            '===== MsgBox(ex.Message & Environment.NewLine & T)
        End Try
        Return RC
    End Function

    Dim ReferredCasesExtractJudgment As String = "ReferredCasesExtractJudgment.txt"
    Private Function ExtractReferredCasesHeadnote(ByVal XMLFilePath As String) As List(Of String)
        Dim arrRefCasesList As List(Of String) = New List(Of String)
        Dim myxmlcase As New xmlcase(XMLFilePath)

        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)

        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim strToAdd As String = ""
        Dim refCaseDataFilename As String = Path.GetFileNameWithoutExtension(XMLFilePath) '#3
        Dim refCaseCitation As String = ""
        Dim refCaseTitle As String = ""
        Dim refCaseType As String = "refd"
        Dim refCaseTitleWithCitation As String = ""

        Dim linkNodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//HEADNOTE//REFERRED_CASES//p")
        For Each linkNode As XmlNode In linkNodeList 'Link
            If linkNode.InnerXml.Contains("<LINK") Or linkNode.InnerXml.Contains("<Link") Then '================CONTAINS LOCAL CASE LINKING======================================
                linkNode.InnerXml.Replace("Link", "LINK")
                Dim editedXML As New XmlDocument()
                editedXML.LoadXml(linkNode.OuterXml)
                Dim caseNodelist As XmlNodeList = editedXML.SelectNodes(editedXML.DocumentElement.Name & "//i//LINK")
                For Each casenode As XmlNode In caseNodelist
                    refCaseTitleWithCitation = casenode.InnerText
                    refCaseCitation = Extractor.FindCitation(refCaseTitleWithCitation)
                    Dim refCaseTitleWithType As String = Extractor.ReferredCases.FindReferredCasesTitle(refCaseTitleWithCitation)
                    refCaseType = Extractor.ReferredCases.FindReferredCasesType(refCaseTitleWithType)
                    refCaseTitle = refCaseTitleWithType.Replace(refCaseType, "").Replace("<p>", "").Replace("</p>", "")
                    If refCaseTitle.Length = 0 Then
                        refCaseTitle = subjectIndexCls.GetJudgmentNameFromCitation(refCaseCitation)
                    End If
                    If refCaseCitation.Contains("ERROR FOR") Then
                        ' nothing to do....
                    Else
                        strToAdd = refCaseCitation.Trim & "#" & refCaseTitle.Trim & "#" & refCaseDataFilename.Trim & "#" & refCaseType.Trim & "#" ' & refCaseTitleWithCitation & "#"
                        arrRefCasesList.Add(strToAdd)
                    End If
                Next
            Else '=============FOREIGN CASES===================
                refCaseTitleWithCitation = linkNode.OuterXml
                refCaseCitation = Extractor.FindCitation(refCaseTitleWithCitation)
                Dim refCaseTitleWithType As String = Extractor.ReferredCases.FindReferredCasesTitle(refCaseTitleWithCitation)
                refCaseType = Extractor.ReferredCases.FindReferredCasesType(refCaseTitleWithType)
                refCaseTitle = refCaseTitleWithType.Replace(refCaseType, "").Replace("<p>", "").Replace("</p>", "")
                If refCaseCitation.Contains("ERROR FOR") Then
                Else
                    'nothing to do
                    strToAdd = refCaseCitation.Trim & "#" & refCaseTitle.Trim & "#" & refCaseDataFilename.Trim & "#" & refCaseType.Trim & "#"
                    arrRefCasesList.Add(strToAdd)
                End If
            End If
        Next
        Return arrRefCasesList
    End Function

    Private Sub btnExtractRefCases_Click(sender As Object, e As EventArgs)
        'lblProgressTitle.Text = "EXTRACT REFERRED CASES"
        'lblPercent.Text = "0%"

        'If chkboxdeldatacases.Checked Then
        '    subjectIndexCls.DeleteAndClearIndex("ref_cases")
        'End If
        'InfoTextProgress("Start Extracting Referred Cases From XML")
        'Dim files As String() = Directory.GetFiles(tempPath, "*.xml")

        'Dim rootCitation As String = ""
        'Dim referredCitation As String = ""
        'Dim referredTitle As String = ""
        'Dim referredType As String = ""
        'Dim referredDatafilename As String = ""

        'Dim insertSQL As String = ""

        'Total_XML_Files = files.Length
        'InProgress_XML_Files = 0

        'For Each strfilename As String In files
        '    Dim referredCasesList As New List(Of String)
        '    If utility.REFERRED_CASES_TYPE(strfilename) = True Then
        '        referredCasesList = ExtractReferredCasesHeadnote(strfilename)
        '    Else
        '        ''''=====myfilesi = ExtractReferredCasesJudgment(strfilename) '====> SKIP FOR TEMPORARY
        '    End If

        '    For Each referredCase As String In referredCasesList
        '        Try
        '            rootCitation = Path.GetFileNameWithoutExtension(strfilename)
        '            referredCitation = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(0)
        '            referredTitle = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(1)
        '            referredDatafilename = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(2)
        '            referredType = referredCase.Split({"#"}, StringSplitOptions.RemoveEmptyEntries)(3)
        '            insertSQL = "insert into ref_cases (source_citation, source_datafilename, referred_citation, referred_title, referred_type) " & _
        '                                    "values('" & utility.PrintCitation(referredDatafilename) & "','" & referredDatafilename & "','" & _
        '                                    referredCitation & "','" & EscapeString(referredTitle.Replace("()", "")) & "','" & referredType & "')"
        '            subjectIndexCls.Insert(insertSQL)
        '        Catch ex As Exception
        '            MsgBox("Error : " & referredCase)
        '            Continue For
        '        End Try
        '    Next
        '    InfoTextProgress("      " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
        '    lstProgress.Refresh()

        '    InProgress_XML_Files = InProgress_XML_Files + 1
        '    lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
        '    lblPercent.Refresh()
        'Next
        'InfoTextProgress("Referred Cases in " & files.Length.ToString() & " files successfully inserted.")
        ''End If
    End Sub

    Public Function removeSN(ByVal str As String)
        Return str.Replace("SN", "")
    End Function

    Public Function ChangeLegislationsType(ByVal str As String) As String
        Select Case str
            Case Is = "order"
                Return " O "
            Case Is = "rule"
                Return " r "
            Case Is = "section"
                Return " s "
            Case Is = "art"
                Return " art "
            Case Is = "reg"
                Return " reg "
            Case Is = "item"
                Return " item "
            Case Else
                Return ""
        End Select
    End Function

    Public Function SortingIntoNewList(ByVal listwithcommaseparator As String) As List(Of String)
        Dim lst As New List(Of String)
        Dim arr() As String = listwithcommaseparator.Split({","}, StringSplitOptions.None)
        For Each key As String In arr
            lst.Add(key)
        Next
        Return lst
    End Function
    Dim LegislationExtJudgment As String = "LegislationExtJudgment.txt"
    Public tmpXMLFile As String = "tmpXMLFile.xml"

    Private Sub RemoveTagFromXMLSource(ByVal sourceXMLPath As String, ByVal tagName As String)
        Try
            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(sourceXMLPath)
            Dim xmlRoot As XmlNode
            xmlRoot = xmlDoc.DocumentElement
            Dim nodes As XmlNodeList
            nodes = xmlDoc.SelectNodes(xmlRoot.Name & "//JUDGMENT//blockquote")
            For Each node As XmlNode In nodes
                If node IsNot Nothing Then
                    node.RemoveAll()
                    xmlDoc.Save(sourceXMLPath)
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub
    Private Function ExtractReferredLegislationJudgment(ByVal XMLFilePath As String) As List(Of String)
        Dim myxmlcase As New xmlcase(XMLFilePath)
        Dim pageNo As String
        pageNo = myxmlcase.PAGE_NO

        My.Computer.FileSystem.WriteAllText(LegislationExtJudgment, "", False)
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)

        ' Dim judgmentNode As XmlNode
        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim linkNodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//JUDGMENT//LINK")

        Dim legisDataFilename As String = ""
        Dim legisSection As String = ""
        Dim legisTitle As String = ""
        Dim legisLink As String = ""
        Dim legisLinkLabel As String = ""
        Dim caseDataFilename As String = ""
        Dim caseTitle As String = ""
        Dim caseLink As String = ""
        Dim sectionType As String = ""
        Dim arrLstTable As New List(Of String) 'returnList
        Dim currentSectionType As String = ""
        Dim legisWithSection As String = ""


        For Each linkNode As XmlNode In linkNodeList
            Dim HREFValue As String = linkNode.Attributes("HREF").InnerText
            legisLinkLabel = linkNode.InnerText
            sectionType = LegislationCharacter(linkNode.InnerText)

            If HREFValue.StartsWith("legislation") Then
                Try
                    legisWithSection = HREFValue.Split({"?", ";;"}, StringSplitOptions.None)(1)
                    legisDataFilename = HREFValue.Split({"?", ";"}, StringSplitOptions.RemoveEmptyEntries)(1)
                    legisTitle = subjectIndexCls.getLegislationTitleByDatafilename(legisDataFilename & ".xml").Trim

                    If legisDataFilename.Contains("MY_PUAS_2012_205") Or legisDataFilename.Contains("MY_PUAS_1980_050") Then
                        legisSection = HREFValue.Split({";", ".;;"}, StringSplitOptions.RemoveEmptyEntries)(1)
                    Else
                        legisSection = HREFValue.Split({";", ".;"}, StringSplitOptions.RemoveEmptyEntries)(1)
                    End If
                    '                               0                   1               2                       3                   4                       5                           6       
                    Dim stringToWrite As String = legisTitle & "#" & utility.PrintCitation(Path.GetFileNameWithoutExtension(XMLFilePath)) & "#" & legisDataFilename & "#" & legisSection & "#" & sectionType & "#" & legisWithSection & "#" & legisLinkLabel & "#" & Environment.NewLine
                    arrLstTable.Add(stringToWrite)

                Catch ex As Exception
                    'legisSection = "AOS"
                End Try

                'My.Computer.FileSystem.WriteAllText(LegislationExtJudgment, stringToWrite, True)

            Else ' If HREFValue.StartsWith("case_notes") Then' for cases reference  
                'caseDataFilename = linkNode.InnerText
                'MsgBox(caseDataFilename)

            End If
        Next
        ListRemoveDups(arrLstTable)
        Return arrLstTable
    End Function

    Public Function ExtractReferredLegislation(ByVal xmlFileName As String) As List(Of String)
        Dim ReturnLegislationList As List(Of String) = New List(Of String)

        Dim xmlPath As String = xmlFileName
        Dim xmlDoc As XmlDocument = New XmlDocument

        If My.Computer.FileSystem.FileExists(xmlPath) = True Then
            xmlDoc.Load(xmlPath)
        End If

        'xmlDoc.InnerText = str
        Dim nodelist As XmlNodeList
        nodelist = xmlDoc.SelectNodes("//REFERRED_LEGISLATIONS//p")
        Dim txtTowrite As String = Nothing

        Dim strLegislationLink As String = ""
        Dim legislationList As String = ""

        For Each node As XmlNode In nodelist

            If node.InnerText.Contains("Legislation referred to:") Or node.InnerText.Contains("Perundangan yang dirujuk:") Then
                'do nothing ==> skip the paragraph that are only for title

            Else
                strLegislationLink = ""
                strLegislationLink = node.InnerText

                ' =========================================================
                ' ==== extract legislation title and section into this area
                ' =========================================================

                Dim strLinkType As String = "Section"
                Dim strLegislation As String = ""
                Dim sectionList As String()
                Dim strSectionList As String = ""
                Dim strSectionNo As String = ""

                Try
                    strLegislation = strLegislationLink.Split({","}, StringSplitOptions.RemoveEmptyEntries)(0)
                    strSectionList = strLegislationLink.Replace(strLegislation, "")
                    sectionList = strSectionList.Split({","}, StringSplitOptions.None)
                    sectionList.Distinct()

                    Dim currentActType As String = "NONE"
                    For Each strSectionNo In sectionList

                        If strSectionNo.Length > 0 Then
                            If strLegislation.Contains("[") Then
                            Else
                                Dim actType As String
                                If isContainMarker(strSectionNo) Then
                                    currentActType = LinkingNoType(strSectionNo)
                                    actType = currentActType
                                Else
                                    actType = currentActType
                                End If

                                If actType = "NOT SET" Then
                                    actType = currentActType
                                End If
                                ReturnLegislationList.Add(Path.GetFileNameWithoutExtension(xmlPath) + "#" + strLegislationLink + "#" + strLegislation + "#" + strSectionNo.Trim + "#" + strSectionNo + "#" + actType + Environment.NewLine)
                            End If
                        End If
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Next
        ListRemoveDups(ReturnLegislationList, False)
        Return ReturnLegislationList

    End Function

    Function StripTags(ByVal html As String) As String
        ' Remove HTML tags.
        Return Regex.Replace(html, "<.*?>", "").Replace(", ", ",")
    End Function
    Public legislationVolume As String = "legislationVolume.txt"
    Private Function ExtractReferredLegislationHeadnote(ByVal XMLFilePath As String) As List(Of String)
        Dim resultList As List(Of String) = New List(Of String)
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)
        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim nodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//HEADNOTE//REFERRED_LEGISLATIONS//p")
        Dim sectionList As List(Of String) = New List(Of String)
        For Each node As XmlNode In nodeList
            If node.InnerText.Contains("Legislation referred to:") Or node.InnerText.Contains("Perundangan yang dirujuk:") Then
                ' this ignore because its not contain any legislation
            Else
                Dim stringToWrite As String = root.Name & "#" & node.InnerText & Environment.NewLine
                My.Computer.FileSystem.WriteAllText(legislationVolume, stringToWrite, True)
                sectionList = ExtractReferredLegislationFromText(node.InnerText)
                For Each sec As String In sectionList
                    My.Computer.FileSystem.WriteAllText("legislationExtract.txt", sec & Environment.NewLine, True)
                    Dim tLegislationTitle As String = sec.Split({"#"}, StringSplitOptions.None)(0)
                    Dim tSection As String = sec.Split({"#"}, StringSplitOptions.None)(1)
                    Dim tPageno As String = utility.GetPageNo(XMLFilePath)
                    Dim tCitation As String = utility.PrintCitation(Path.GetFileNameWithoutExtension(XMLFilePath))
                    resultList.Add(tLegislationTitle & "#" & tCitation & "#" & "NOT AVAILABLE #" & tSection & "#" & tSection & "#" & tSection & "#" & tSection & "#" & Environment.NewLine)
                Next
            End If
        Next
        ListRemoveDups(resultList)
        Return resultList
    End Function

    Private Function CheckLegislationNameIsExist(ByVal strLegislation As String) As String

    End Function

    Private Sub Extract_Temp_Ref_Legislation_Headnote_To_DB(ByVal XMLFilePath As String)

        Dim insertSQL As String = ""

        Dim resultList As List(Of String) = New List(Of String)
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)
        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim nodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//HEADNOTE//REFERRED_LEGISLATIONS//p")
        Dim sectionList As List(Of String) = New List(Of String)

        Dim datafilename As String = Path.GetFileNameWithoutExtension(XMLFilePath)
        Dim legis_filename As String = ""
        Dim legis_no As String = ""
        Dim legis_text As String = ""
        Dim legis_title As String = ""
        Dim legis_origin As String = ""
        Dim legis_keyword As String = ""
        Dim legis_char As String = ""
        Dim dbName As String = ""
        For Each node As XmlNode In nodeList
            If node.InnerText.Contains("Legislation referred to:") Or node.InnerText.Contains("Perundangan yang dirujuk:") Then
                ' this ignore because its not contain any legislation
            Else
                If node.InnerXml.Contains("LINK") Or node.InnerXml.Contains("Link") Then
                    legis_origin = "local"
                Else
                    legis_origin = "foreign"
                End If

            End If

            legis_title = node.InnerText.Split(",")(0)
            legis_text = node.InnerText

            Dim hrefValue As String = ""
            Dim countSemicolon As Integer = 0
            Dim countComma As Integer = 0
            countComma = Regex.Matches(legis_text, ",").Count
            Dim tmpLegisTitle As String
            Dim tmpLegisNo As String
            Dim tmpLegisValue As String = ""
            Dim tmpLegisMainValue As String
            If countComma > 1 Then ' title and many sections
                tmpLegisTitle = legis_text.Split({","}, StringSplitOptions.None)(0)
                legis_keyword = legis_text.Split({","}, StringSplitOptions.None)(1)
                Dim strLSections As String = legis_text.Replace(tmpLegisTitle & ",", "").Trim
                Dim arrLSections As String() = strLSections.Split(",")
                For i As Integer = 0 To arrLSections.Length - 1
                    legis_title = tmpLegisTitle
                    Dim currentLegisNo = arrLSections(i).Trim

                    If legis_no.ToUpper.StartsWith("O ") Then
                        If legis_no.ToUpper.Contains(" RR ") Then ' order n rr


                        ElseIf legis_no.ToUpper.Contains(" R ") Then 'order and r

                        Else ' contains only order

                        End If
                    Else ' NOT INCLUDE Order
                        Dim strPattern As String = "^([A-Z|a-z]{1,10})\s" 'begin with any string like s,ss,r,rr,etc
                        Dim strRegex As New Regex(strPattern)
                        Dim strMatch As Match = strRegex.Match(currentLegisNo)
                        If strMatch.Success() Then
                            currentLegisNo = Regex.Replace(currentLegisNo, strPattern, "")
                            tmpLegisMainValue = currentLegisNo
                        Else
                            Dim numberPattern As String = "^([0-9]{1,4})([A-Z]{1,2})|^([0-9]{1,4})"
                            Dim numberRegex As New Regex(numberPattern)
                            Dim numberMatch As Match = numberRegex.Match(currentLegisNo)
                            If numberMatch.Success Then
                                tmpLegisMainValue = currentLegisNo
                            End If
                        End If

                    End If
                    Dim LegisExactValue As String = Extractor.ReferredLegislations.Extract_Legislation_Sub_No_Type(legis_keyword) & " " & Extractor.Legislation_Regex_Fix(tmpLegisMainValue, currentLegisNo)
                    insertSQL = "insert into tmp_ref_leg(source_citation, source_datafilename, legislink_filename, legislink_no, legislink_htext, legislink_title, legislink_type, legislink_origin, legislink_char, legislink_value) " & _
                    "values('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & legis_filename & "','" & currentLegisNo & _
                    "','" & EscapeString(legis_text) & "','" & EscapeString(legis_title) & "','" & tmpLegisMainValue & "','" & legis_origin & "','" & legis_keyword & "','" & LegisExactValue & "')"
                    subjectIndexCls.Insert(insertSQL)
                Next

            ElseIf countComma = 1 Then 'means only title and section

                tmpLegisTitle = legis_text.Split({","}, StringSplitOptions.None)(0)
                tmpLegisNo = legis_text.Split({","}, StringSplitOptions.None)(1)
                legis_keyword = legis_text.Split({","}, StringSplitOptions.None)(1)
                legis_no = tmpLegisNo
                legis_title = tmpLegisTitle

                insertSQL = "insert into tmp_ref_leg_local(source_citation, source_datafilename, legislink_filename, legislink_no, legislink_htext, legislink_title, legislink_type, legislink_origin, legislink_char) " & _
                    "values('" & utility.PrintCitation(datafilename) & "','" & datafilename & "','" & legis_filename & "','" & utility.Legislations.RemoveUnusedCharacter(legis_no) & _
                    "','" & EscapeString(legis_text) & "','" & EscapeString(legis_title) & "','" & legis_keyword & "','" & legis_origin & "','" & legis_keyword & "')"
                subjectIndexCls.Insert(insertSQL)
            End If
        Next

    End Sub

    Private Function ExtractReferredLegislationFromText(ByVal text As String) As List(Of String)

        Dim patternBoth As String = "(\([0-9]{1,3}\))(\([a-z]{1,3}\))"
        Dim patternNumber As String = "(\([0-9]{1,3}\))"
        Dim patternString As String = "(\([a-z]{1,3}\))"

        Dim regexBoth As New Regex(patternBoth)
        Dim regexNumber As New Regex(patternNumber)
        Dim regexString As New Regex(patternString)

        Dim strText As String = StripTags(text)
        Dim arrText As String() = strText.Split({","}, StringSplitOptions.None)
        Dim curMainSection As String = ""
        Dim counter As Integer = 1
        Dim arrList As List(Of String) = New List(Of String)
        Dim title As String = arrText(0)

        For counter = 1 To arrText.Length - 1 Step 1
            'If GotMainLegislationChar(arrText(counter)) = True Then

            'Else
            If arrText(counter).Trim.StartsWith("(") Then
                Dim matchBoth As Match = regexBoth.Match(arrText(counter))
                If matchBoth.Success Then
                    curMainSection = Regex.Replace(curMainSection, patternBoth, arrText(counter).Trim)
                Else
                    Dim matchNumber As Match = regexNumber.Match(arrText(counter))
                    If matchNumber.Success() Then
                        curMainSection = Regex.Replace(curMainSection, patternNumber, arrText(counter).Trim)
                    Else
                        Dim matchString As Match = regexString.Match(arrText(counter))
                        If matchString.Success() Then
                            curMainSection = Regex.Replace(curMainSection, patternString, arrText(counter).Trim)
                        End If
                    End If
                End If
            Else ' NOT START WITH BRACKET "
                curMainSection = arrText(counter)
            End If
            'End If
            arrList.Add(curMainSection)
        Next

        Dim listResult As List(Of String) = New List(Of String)
        Dim currentType As String = ""
        Dim pattern1 As String = "\w+\s([0-9|A-Z]{1,3})(\(\w+\)){0,20}"
        Dim r As New Regex(pattern1)

        For Each s As String In arrList
            Dim m As Match = r.Match(s)
            If m.Success Then
                currentType = LegislationCharacter(s)
                listResult.Add(title & "#" & s)
                Continue For
            Else
                listResult.Add(title & "#" & currentType & " " & s)
            End If
        Next
        Return listResult
    End Function

    Private Function GotMainLegislationChar(ByVal s As String) As Boolean
        Dim result As Boolean = False
        If s.Contains("O ") And s.Contains("r ") Then 'Or s.Contains("order") Or s.Contains("aturan") Then
            result = True
        ElseIf s.Contains("ss ") Or s.Contains("s ") Or s.Contains("section") Or s.Contains("seksyen") Then
            result = True
        ElseIf s.Contains("reg ") Or s.Contains("regs ") Or s.Contains("reg") Then
            result = True
        ElseIf s.Contains("art ") Or s.Contains("arts ") Or s.Contains("article") Or s.Contains("artikel") Then
            result = True
        ElseIf s.Contains("rr ") Or s.Contains("r ") Or s.Contains("rule") Or s.Contains("kaedah") Then
            result = True
        ElseIf s.Contains("item ") Or s.Contains("items ") Or s.Contains("item") Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function

    Private Function ExtractReferredCasesJudgment(ByVal XMLFilePath As String) As List(Of String)
        Dim arrRefCasesList As List(Of String) = New List(Of String)
        Dim myxmlcase As New xmlcase(XMLFilePath)

        Dim pageNo As String
        pageNo = myxmlcase.PAGE_NO

        My.Computer.FileSystem.WriteAllText(LegislationExtJudgment, "", False)
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)

        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim linkNodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//JUDGMENT//LINK")
        Dim refCaseDataFilename As String = ""
        Dim refCaseCitation As String = ""
        Dim refCaseTitle As String = ""
        Dim refCaseType As String = "refd"

        For Each linkNode As XmlNode In linkNodeList
            Dim strToAdd As String = ""
            Dim refCaseTitleWithCitation As String = linkNode.InnerText

            Dim HREFValue As String = linkNode.Attributes("HREF").InnerText
            If HREFValue.StartsWith("case_notes") Then
                If refCaseTitleWithCitation.Contains("[") Then
                    refCaseDataFilename = Path.GetFileNameWithoutExtension(XMLFilePath)
                    refCaseCitation = "[" & refCaseTitleWithCitation.Split({"["}, StringSplitOptions.None)(1)
                    Try
                        refCaseTitle = refCaseTitleWithCitation.Split({"["}, StringSplitOptions.None)(0)
                        If refCaseTitle.Length < 1 Then
                            refCaseTitle = subjectIndexCls.getCasesTitleByCitation(refCaseCitation)
                            refCaseTitle = StrConv(refCaseTitle, VbStrConv.ProperCase)
                        End If
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                Else
                    'skip due to citation format error=====================================================================
                End If
                strToAdd = refCaseCitation.Trim & "#" & refCaseTitle.Trim & "#" & refCaseDataFilename.Trim & "#" & refCaseType.Trim & "#"
                arrRefCasesList.Add(strToAdd)
            End If
        Next
        ListRemoveDups(arrRefCasesList)
        Return arrRefCasesList
    End Function



    Public Function isForeignCaseTitle(ByVal s As String) As Boolean
        If s.Contains(" v ") Then
            Return True
        ElseIf s.Contains(" v. ") Then
            Return True
        ElseIf s.Contains(" In Re ") Or s.Contains(" In re ") Then
            Return True
        ElseIf s.Contains(" Re ") Or s.Contains(" re ") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ExtractForeignCasesReferred(ByVal XMLFilePath As String) As List(Of String)
        Dim arrRefCasesList As List(Of String) = New List(Of String)
        Dim myxmlcase As New xmlcase(XMLFilePath)

        Dim pageNo As String
        pageNo = myxmlcase.PAGE_NO

        'My.Computer.FileSystem.WriteAllText(LegislationExtJudgment, "", False)
        Dim xmlDoc As New XmlDocument
        xmlDoc.Load(XMLFilePath)

        Dim root As XmlNode = xmlDoc.DocumentElement
        Dim linkNodeList As XmlNodeList = xmlDoc.SelectNodes(root.Name & "//JUDGMENT//i")

        Dim refCaseDataFilename As String = ""
        Dim refCaseCitation As String = ""
        Dim refCaseTitle As String = ""
        Dim refCaseType As String = "refd"

        For Each node As XmlNode In linkNodeList
            If isForeignCaseTitle(node.InnerXml) = True Then
                Dim strToAdd As String = ""
                Dim refCaseTitleWithCitation As String = node.InnerXml

                refCaseDataFilename = Path.GetFileNameWithoutExtension(XMLFilePath)

                refCaseCitation = refCaseTitleWithCitation.Split({"</i>"}, StringSplitOptions.None)(1)
            End If
        Next

    End Function

    Public Function GetLinkingType(ByVal linkingText As String) As String
        If linkingText.Contains("ss ") Or linkingText.Contains("s ") Then
            Return "Section"
        ElseIf linkingText.Contains("act ") Then
            Return "Act"
        ElseIf linkingText.Contains("reg") Then
            Return "Regulation"
        ElseIf linkingText.Contains("O ") Then
            Return "Order "
        Else
            Return "NOT SET"
        End If
    End Function
    Public Function LinkingNoType(ByVal linkingText As String) As String

        Dim currentNoType As String = ""

        If linkingText.Contains("ss ") Or linkingText.Contains("s ") Then
            currentNoType = "s "

        ElseIf linkingText.Contains("acts ") Or linkingText.Contains("act ") Then
            currentNoType = "act "

        ElseIf linkingText.Contains("regs ") Or linkingText.Contains("reg ") Then
            currentNoType = "reg "

        ElseIf linkingText.Contains("arts ") Or linkingText.Contains("art ") Then
            currentNoType = "art "

        Else
            currentNoType = "NOT SET"
        End If

        Return currentNoType

    End Function
    Public Function isContainMarker(ByVal linkingText As String) As Boolean
        If linkingText.Contains("ss ") Or linkingText.Contains("s ") Or linkingText.Contains("acts ") Or linkingText.Contains("act ") Or _
            linkingText.Contains("regs ") Or linkingText.Contains("reg ") Or linkingText.Contains("arts ") Or linkingText.Contains("art ") _
            Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetLegislationCitation(ByVal legislationTitle As String) As String

    End Function
    Private Function LegislationCharacter(ByVal s As String) As String
        If s.Contains("O ") And s.Contains("r ") Then 'Or s.Contains("order") Or s.Contains("aturan") Then
            Return s

        ElseIf s.Contains("ss ") Or s.Contains("s ") Or s.Contains("section") Or s.Contains("seksyen") Then
            Return "s"

        ElseIf s.Contains("reg ") Or s.Contains("regs ") Or s.Contains("reg") Then
            Return "reg"

        ElseIf s.Contains("art ") Or s.Contains("arts ") Or s.Contains("article") Or s.Contains("artikel") Then
            Return "art"

        ElseIf s.Contains("rr ") Or s.Contains("r ") Or s.Contains("rule") Or s.Contains("kaedah") Then
            Return "r"

        ElseIf s.Contains("item ") Or s.Contains("items ") Or s.Contains("item") Then
            Return "item"


        Else
            Return "N/A"
        End If

    End Function
    Public Shared Function ListRemoveDups(ByVal Source As List(Of String), Optional ByVal MatchCase As Boolean = False)
        'Sort the array
        Source.Sort()

        'Do the duplicates remove.
        For x As Integer = Source.Count - 1 To 1 Step -1
            'Check if we want 
            If MatchCase Then
                'Do the check
                If Source(x).ToLower() = Source(x - 1).ToLower() Then
                    Source.RemoveAt(x)
                End If
            Else
                'Do the check
                If Source(x) = Source(x - 1) Then
                    Source.RemoveAt(x)
                End If
            End If
        Next x
    End Function

    Private Shared Function ReplaceWordForRulesOfCourt(ByVal s As String)
        Dim returnValue As String
        returnValue = s.Replace("RC", "").Replace("ROC", "").Replace("Rules of Court 2012", "").Replace("RHC", "") _
            .Replace("rules of the high court", "").Replace("rules of high court", "") _
            .Replace(",", "").Replace("  ", " ").Replace("rr", "r").Replace("Rules", "").Replace("Arts", "art").Replace("regs", "reg")
        Return returnValue.Trim
    End Function

    Private Shared Function ValueNum(ByVal value As String) As Integer
        Dim returnVal As String = String.Empty
        Dim collection As MatchCollection = Regex.Matches(value, "\d+")
        For Each m As Match In collection
            returnVal += m.ToString()
        Next
        Return Convert.ToInt32(returnVal)
    End Function

    Public Function matchSectionValue(ByVal sourceString As String) As String
        Dim result As String = ""
        Dim regexPattern1 As String
        Dim regexPattern2 As String
        Dim regexPattern3 As String

        regexPattern1 = "^\w+\s([0-9]{1,3})([A-Z]{1,2})(\(\w+\)){0,20}" ' sample S 39B
        regexPattern2 = "^\w+\s[0-9]{1,3} \w+ [0-9]{1,3}(\(\w+\)){0,20}"
        regexPattern3 = "^\w+\s([0-9]{1,3})(\(\w+\)){0,20}"

        Dim m1 As Match = Regex.Match(sourceString, regexPattern1)
        Dim m2 As Match = Regex.Match(sourceString, regexPattern2)
        Dim m3 As Match = Regex.Match(sourceString, regexPattern3)

        If m1.Success Then
            result = m1.Value
        Else
            If m2.Success Then
                result = m2.Value
            Else
                If m3.Success Then
                    result = m3.Value
                Else
                    Return sourceString
                End If
            End If
        End If
        Return result
    End Function

    Private Function GetSectionFromString(ByVal sourceString As String) As String
        Dim txtWordToReplace As String = "wordstoreplace.txt"

        Dim result As String = ""
        If sourceString.Contains("of") Then
            sourceString = sourceString.Split({"of"}, StringSplitOptions.None)(0)

        ElseIf sourceString.Contains("Of") Then
            sourceString = sourceString.Split({"Of"}, StringSplitOptions.None)(0)

        ElseIf sourceString.Contains("Akta") Then
            sourceString = sourceString.Split({"Akta"}, StringSplitOptions.None)(0)

        Else
            sourceString = sourceString
        End If

        sourceString = sourceString.Trim 'remove leading space

        '================================================================================================
        Dim arrWords As String() = File.ReadAllLines(txtWordToReplace)
        Dim oriText As String
        Dim replaceText As String

        For Each w As String In arrWords
            oriText = w.Split("#")(0)
            replaceText = w.Split("#")(1)
            sourceString = sourceString.Replace(oriText, replaceText)
        Next
        result = matchSectionValue(sourceString)
        'result = sourceString.Replace("Section", "s").Replace("section", "s").Replace("rr", "r").Replace("ss", "s").Replace("Orders", "O").Replace("Order", "O") _
        '    .Replace("Rules", "r").Replace("Rule", "r").Replace("rules", "r").Replace("rule", "r").Replace("Articles", "art").Replace("Article", "art").Replace("subsection", "s") _
        '    .Replace("sub-section", "s").Replace("subs", "s").Replace("sub-s", "s").Replace("arts", "art")
        'Call matchSectionValue(result)
        Return result
    End Function
    Private Sub Copy_Data_From_DB_To_Legislations()
        Dim query As String = "insert into ref_legislations " & _
            "(source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
            "select source_citation, source_datafilename, legislink_origin, legislink_title, legislink_filename, legislink_no, legislink_type, legislink_htext " & _
            "from tmp_ref_leg_local where legislink_no<>'0' and legislink_no<>'' "
        subjectIndexCls.Insert(query)

        Dim query2 As String = "insert into ref_legislations " & _
            "(source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
            "select source_citation, source_datafilename, legislink_origin, legislink_title, legislink_filename, legislink_no, legislink_type, legislink_htext " & _
            "from tmp_ref_leg_foreign where legislink_no<>'0' and legislink_no<>'' "

        subjectIndexCls.Insert(query)
    End Sub

    Private Sub btnExtractRefLegislation_Click(sender As Object, e As EventArgs)
        ''On Error Resume Next
        ''My.Computer.FileSystem.WriteAllText(legislationVolume, "", False)
        ''My.Computer.FileSystem.WriteAllText("legislationExtract.txt", "", False)
        ''My.Computer.FileSystem.WriteAllText("legisLink.txt", "", False)
        ''My.Computer.FileSystem.WriteAllText("insertSQL.txt", "", False) ' try to view sql command before insert
        ''My.Computer.FileSystem.WriteAllText("myfilesi.txt", "", False)
        'lblProgressTitle.Text = "EXTRACT REFERRED LEGISLATIONS"
        'lblPercent.Text = "0%"

        'InfoTextProgress("Start Extracting Referred legislations")

        'If chkboxdeldatalegislation.Checked Then
        '    subjectIndexCls.DeleteAndClearIndex("ref_legislations")

        '    subjectIndexCls.DeleteAndClearIndex("tmp_ref_leg_foreign")
        '    subjectIndexCls.DeleteAndClearIndex("tmp_ref_leg_local")

        'End If

        'Dim files As String() = Directory.GetFiles(tempPath, "*.xml")
        'For Each strfilename As String In files
        '    RemoveTagFromXMLSource(strfilename, "//judgment//blockquote")
        'Next

        'Total_XML_Files = files.Length
        'InProgress_XML_Files = 0

        'For Each strfilename As String In files
        '    lblfn.Text = "FileName : " + Path.GetFileNameWithoutExtension(strfilename)

        '    Dim myxmlcase As New xmlcase(strfilename)
        '    If utility.REFERRED_LEGISLATION_TYPE(strfilename) = True Then
        '        Call Extract_Temp_Ref_Legislation_Headnote_To_DB(strfilename)
        '        InfoTextProgress("     " & Path.GetFileNameWithoutExtension(strfilename) & "   - done")
        '    Else
        '        ' ================== myfilesi = ExtractReferredLegislationJudgment(strfilename)
        '    End If

        '    'Call subjectIndexCls.Extract_Temp_Ref_Legislation_DB_ToList("local")
        '    'Call subjectIndexCls.Extract_Temp_Ref_Legislation_DB_ToList("foreign")

        '    'Call Copy_Data_From_DB_To_Legislations()

        '    InProgress_XML_Files = InProgress_XML_Files + 1
        '    lblPercent.Text = CInt((InProgress_XML_Files / Total_XML_Files) * 100) & "%"
        '    lblPercent.Refresh()
        'Next
        'Dim query As String = ""
        'Dim Local_Legis_List As List(Of String) = subjectIndexCls.Extract_Temp_Ref_Legislation_To_Table("local")
        'Dim arr As String()
        'For Each Local_legis As String In Local_Legis_List
        '    Try
        '        arr = Local_legis.Split("#")
        '    Catch ex As Exception

        '    End Try

        '    query = "insert into ref_legislations (source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
        '        "values('" & arr(0) & "','" & arr(1) & "','" & arr(2) & "','" & arr(3) & "','" & arr(4) & "','" & Extractor.ReferredLegislations.Replace_Legislation_Sub_No_Type(arr(5)) & "','" & arr(6) & "','" & arr(7) & "')"
        '    subjectIndexCls.Insert(query)
        'Next

        'Dim Foreign_Legis_List As List(Of String) = subjectIndexCls.Extract_Temp_Ref_Legislation_To_Table("foreign")

        'For Each Foreign_Legis As String In Foreign_Legis_List
        '    Try
        '        arr = Foreign_Legis.Split("#")
        '    Catch ex As Exception

        '    End Try

        '    query = "insert into ref_legislations (source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
        '        "values('" & arr(0) & "','" & arr(1) & "','" & arr(2) & "','" & arr(3) & "','" & arr(4) & "','" & Extractor.ReferredLegislations.Replace_Legislation_Sub_No_Type(arr(5)) & "','" & arr(6) & "','" & arr(7) & "')"
        '    subjectIndexCls.Insert(query)
        'Next

        'InfoTextProgress("Legislations data in " & files.Length.ToString() & " files successfully inserted.")

    End Sub
    Dim XML_Save_Location As String = ""

    Private Sub btnPath_Click(sender As Object, e As EventArgs) Handles btnPath.Click
        If saveFolderPath.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtOutput.Text = saveFolderPath.SelectedPath
            XML_Save_Location = saveFolderPath.SelectedPath
        End If
        'saveFolderPath = "Choose your save folder location for XML Files generated"
        ''sfd.FileName = folderdialogsubjectindex.SelectedPath
        'sfd.Filter = "XML Files(*.XML)|*.XML"
        'If sfd.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    If sfd.FileName <> "" Then
        '        File.Create(sfd.FileName).Dispose()
        '        lblPath.Text = sfd.FileName
        '    End If
        'End If
    End Sub
    Public Sub LegislationLinkChecker(ByVal filePath As String)
        Dim strError As String = ""
        Dim xDoc As New XmlDocument
        Dim Nodelst As XmlNodeList
        Dim XmlSub As New XmlDocument
        Dim xmlLink As XmlNodeList
        Dim LegislationReferred As New List(Of String)
        Dim LegislationTitle As String

        xDoc.Load(filePath)
        Nodelst = xDoc.GetElementsByTagName("REFERRED_LEGISLATIONS")
        For Each xn As XmlNode In Nodelst
            XmlSub.LoadXml(xn.OuterXml)
            xmlLink = XmlSub.GetElementsByTagName("LINK")
            ' MsgBox(xmlLink(2).InnerText)

            '   For Each node As String In xmlLink
            '   Next
            '   XmlSub.LoadXml(xn.OuterXml())
            '   xmlLink = XmlSub.GetElementsByTagName("LINK")

            'If xmlLink.Count > 0 Then
            '    For i = 0 To xmlLink.Count - 1
            '        LegislationTitle = xmlLink(i).InnerXml
            '        Dim j As Integer = 0
            '        'Do Until LegislationTitle.Length > 10
            '        Do Until Not IsNumeric(LegislationTitle(0))
            '            'to detect wether legis to link is contain act name
            '            LegislationTitle = xmlLink(i - j).InnerXml
            '            j = j + 1
            '        Loop
            '        LegislationReferred.Add(LegislationTitle & "XXX" & xmlLink(i).Attributes("HREF").Value())
            '    Next
            'End If
        Next

        ' ''''' connect to db for checking
        'Dim dTable As New DataTable
        'Dim strSQL As String

        'Dim referTitle As String = ""
        'Dim referLink As String = ""
        'Dim referCategory As String = ""
        'Dim referTitleCategory As String

        'Dim referAct As String = ""
        'Dim referNo As String = ""

        'Dim referSection As String = ""
        'Dim referRules As String = ""
        'Dim referOrder As String = ""
        'Dim referSearch As String = ""

        'Dim strAll As String = ""
        'Dim strLegisTitle As String = ""

        'For Each strLegisLink As String In LegislationReferred

        '    referTitleCategory = strLegisLink.Split({"XXX"}, StringSplitOptions.None)(0) ' store legislation title for comparison method
        '    'MsgBox(referTitleCategory)
        '    If referTitleCategory.Contains(",") Then
        '        referTitle = referTitleCategory.Split(",")(0)
        '        referCategory = referTitleCategory.Split(",")(1)
        '    Else
        '        referAct = referTitleCategory
        '        referCategory = ""
        '    End If

        '    If referCategory = "(" Then
        '        referCategory = referCategory.Remove(referCategory.Length - 3, 3)
        '    End If

        '    referLink = strLegisLink.Split({"XXX"}, StringSplitOptions.None)(1)
        '    referAct = referLink.Split("?")(1).Split(";")(0)
        '    referNo = referLink.Split(";")(1).Split(";;")(0)

        '    If referAct = "MY_ACTS_1958_81" Then
        '        strSQL = "select * from legislation where datafilename like '%" & referAct & "%'"
        '    Else

        '        If referCategory.Contains(" ss ") Or referCategory.Contains(" s ") Then
        '            referSearch = referCategory.Replace(" ss ", "").Replace(" s ", "").Replace(" ", "")
        '            referSearch = referNo
        '            strSQL = "select * from secion_tb1 where legfile like '%" & referAct & "%' and sec_no like '" & referSearch & "%'"
        '        Else

        '            strSQL = "select * from legislation where datafilename like '%" & referAct & "%'"
        '        End If

        '    End If

        '    dTable = EXcuteQuery(strSQL)

        '    If dTable.Rows.Count > 0 Then
        '        ' legislation referred found in database
        '        strError = ""
        '    Else
        '        strError = "<span style='color:Red; font-weight:700;'>Please Check Xml File Error is: Couldn't find Referred Legislation:" & referAct & "  with Section:" & referSearch & " in database.</span>"
        '        'Exit For
        '        Return strError
        '    End If
        'Next

        ''strError = strAll
        ''MsgBox(referSection)
        'Return strError
    End Sub
    
    Public Sub ExtractLegislation()
        Dim folderpath As String = saveFolderPath.SelectedPath
        folderpath = "E:\TEMP_WORK\MLRA_2011_1"
        Dim arrErrorList As New List(Of String)
        Dim counter = 0
        Dim listFile As String() = Directory.GetFiles(folderpath)
        Dim legisList As String = "legisList.txt"
        For Each strFilename As String In listFile
            Dim filename As String = folderpath & "\" & strFilename
            If File.Exists(filename) = True Then
                counter = counter + 1
                Try
                    Dim xmlDoc As New XmlDocument
                    xmlDoc.Load(filename)
                    xmlDoc.RemoveChild(xmlDoc.SelectSingleNode("BLOCKQUOTE"))
                    xmlDoc.Save("tmpFolder\\tmp_" & strFilename)

                    Dim root As XmlNode = xmlDoc.DocumentElement
                    Dim judgmentNode As XmlNodeList
                    judgmentNode = xmlDoc.SelectNodes(root.Name & "//JUDGMENT//LINK")

                    For Each node As XmlNode In judgmentNode
                        Dim strLegis As String = node.Attributes("HREF").Value
                        If strLegis.StartsWith("legislation") = True Then
                            Dim legisNo As String = strLegis.Split({"?", ";;"}, StringSplitOptions.None)(1)

                            If legisNo.EndsWith(";") Then
                                legisNo = legisNo.Remove(legisNo.Count - 1, 1)
                            End If

                            Dim strToWrite As String = filename.Replace("C:\eLaw\eLaw_Data_Files\eLaw_Data_Files\cases", "").Replace(".xml", "") & "#" & legisNo & Environment.NewLine

                            My.Computer.FileSystem.WriteAllText(legisList, strToWrite, True)
                        End If
                    Next node
                Catch ex As Exception

                    If ex.Message.ToString.Contains("Object reference not set to an instance of an object.") Then
                    Else
                        arrErrorList.Add(strFilename & "#" & ex.Message.ToString)
                    End If
                    Continue For
                End Try
            Else '==file in the list is not found in the server directory
                If My.Computer.FileSystem.FileExists("NotFound.txt") Then
                    My.Computer.FileSystem.WriteAllText("NotFound.txt", strFilename & Environment.NewLine, False)
                Else
                    My.Computer.FileSystem.WriteAllText("NotFound.txt", strFilename & Environment.NewLine, True)
                End If
            End If
        Next

        If arrErrorList.Count > 0 Then
            ' ===> File.WriteAllLines( errorFile, arrErrorList )
        End If
        MsgBox("Finished")
        Process.Start(legisList)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Call Extract_Temp_Ref_Legislation_Headnote_To_DB("C:\Users\Diddy\Desktop\MLRA_2013_1_364.xml")
        Dim arrList As List(Of String) = subjectIndexCls.Court_List()
        For Each s As String In arrList
            MsgBox(s)
        Next
    End Sub

    Private Function RegexReplace_Legislation_Sub_No(ByVal str As String) As String
        Dim p As String = "([A-Z|a-z]{1,10})"
        Dim r As New Regex(p)
        Dim newStr As String
        newStr = Regex.Replace(str, p, "")
        newStr = newStr.Replace("(", ".").Replace(")", "").Replace("  ", "").Replace(" ", "").Trim
        newStr = newStr.Replace(",", "").Replace("..", ".")
        Return newStr
    End Function
    Public Function Split_column(ByVal temp As String) As String()
        Dim i As Integer = 0
        Dim c1 As String = ""
        Dim c2 As String = ""

        If temp.Contains("(5)(5)") Then
            temp = temp.Replace("(5)(5)", "(5)")
        ElseIf temp.Contains("(10)(10)") Then
            temp = temp.Replace("(10)(10)", "(10)")
        ElseIf temp.Contains(" & ") Then
            temp = temp.Replace(" & ", ", ")
        Else
        End If

        If temp.StartsWith("(") Then
        Else
            If temp.Length > 0 Then
                If (temp.Contains(",")) Then
                    temp = temp.Substring(0, temp.IndexOf(","))
                End If
                If (temp.Contains("(")) Then
                    If Not (IsNumeric(temp.Chars(temp.IndexOf("(") - 1))) Then
                        temp = temp.Substring(0, temp.IndexOf("("))
                    End If
                End If
                If temp.Contains(")") Then
                    temp = temp.Substring(0, temp.IndexOf(")"))
                    temp = temp.Replace("(", ".")
                End If
                temp = temp.Trim
                temp = temp.Replace(" ", "")
                c1 = ""
                c2 = ""
                Dim j As Integer = 0
                For j = 0 To temp.Length - 1
                    If (IsNumeric(temp.Chars(j))) Then
                        Exit For
                    Else
                        c1 = c1 & temp.Chars(j)
                    End If
                Next

                temp = temp.Substring(j, temp.Length - j)
                If temp.Length > 0 Then
                    If Not (IsNumeric(temp.Chars(temp.Length - 1))) Then
                        temp = temp.Substring(0, temp.Length - 1)
                    End If
                    c2 = Regex.Replace(temp, "[a-zA-Z]", ".")
                End If
                Dim first_occurance As Integer = c2.IndexOf(".")
                If (first_occurance > 0) Then
                    Dim second_occurance As Integer = c2.IndexOf(".", first_occurance + 1)
                    If second_occurance > 0 Then
                        c2 = c2.Substring(0, second_occurance)
                    End If
                End If
                If c2.Contains("[") Or c2.Contains("]") Or c2.Contains("-") Then
                    c2 = "0"
                End If

                'sw.WriteLine("actual value = " & array(i) & "  converted values " & c1 & "," & c2)

            End If
        End If

        Dim cols As String = c1 & "," & c2
        Dim data As String() = cols.Split(",")
        Return data

    End Function
    Private Sub btnPrepare_Click(sender As Object, e As EventArgs) Handles btnPrepare.Click
        Dim sttt As String = "gfdsgdfsgsd"
        Dim QueryInsert As String = ""
        Dim QuerySelect As String = ""
        ' ==> CASES <==
        InfoTextProgress("Deleting Cases Data")
        subjectIndexCls.DeleteAndClearIndex("v_cases")
        InfoTextProgress("Preparing Cases Data")
        QueryInsert = "insert into v_cases(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, referred_citation, referred_title, referred_type) "
        QuerySelect = "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, referred_citation, referred_title, referred_type " & _
        "from xml_cases, ref_cases where citation=source_citation "
        subjectIndexCls.InsertSelect(QueryInsert & " " & QuerySelect)
        InfoTextProgress("Preparing Cases Data Completed ")
        InfoTextProgress("Total : " & subjectIndexCls.CountTableRow("v_cases"))

        ' ==> LEGISLATION <==
        InfoTextProgress("Deleting Legislation Data")
        subjectIndexCls.DeleteAndClearIndex("v_legislations")
        InfoTextProgress("Preparing Legislations Data")
        Dim queryLegislation As String = "insert into v_legislations(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, legis_title, legis_filename, legis_sub_no, legis_sub_type, legis_link_text) " & _
        "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text " & _
        "from xml_cases x, ref_legislations l where x.citation=l.source_citation "
        subjectIndexCls.Insert(queryLegislation)
        InfoTextProgress("Preparing Legislations Data Completed  ")
        InfoTextProgress("Total : " & subjectIndexCls.CountTableRow("v_legislations"))

        Dim LegislationList As List(Of String) = New List(Of String)
        LegislationList = subjectIndexCls.Legislation_Extract_Id_Sub_No
        Dim sId As Integer
        Dim sSub As String
        Dim updateQuery As String
        Dim counter As Integer = 0
        My.Computer.FileSystem.WriteAllText("logs\Error_Update_Legislations_Db.txt", "", False)
        For Each leg As String In LegislationList
            counter = counter + 1
            If counter Mod 1000 = 0 Then
                InfoTextProgress("Update " & counter & " legislations completed")
            End If
            sId = leg.Split("#")(0)
            sSub = leg.Split("#")(1)
            Dim data As String() = Split_column(sSub)
            Dim sort_string As String = data(0)
            Dim sort_double As Double
            Try
                If (data(1).Length > 0) Then
                    sort_double = Convert.ToDouble(data(1))
                Else
                    sort_double = 0
                End If

                updateQuery = "update v_legislations set sort_string = '" & sort_string & "' , sort_decimal=" & sort_double & _
                              " where id = '" & sId & "'"
                Call subjectIndexCls.Insert(updateQuery)

            Catch ex As Exception
                Dim errorText As String = ex.Message.ToString & Environment.NewLine & Environment.NewLine
                My.Computer.FileSystem.WriteAllText("logs\Error_Update_Legislations_Db.txt", errorText, True)
            End Try



        Next
        ' ==> SUBJECT INDEX <==
        InfoTextProgress("Deleting Subject Index Data")
        subjectIndexCls.DeleteAndClearIndex("v_subject_index")
        InfoTextProgress("Preparing Subject Index Data")
        Dim querySubjectIndex As String = "insert into v_subject_index(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, subject_index, level1, level2, summary) " & _
        "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, subject_index, level1, level2, summary " & _
        "from xml_cases, subject_index where citation=source_citation "
        subjectIndexCls.Insert(querySubjectIndex)

        InfoTextProgress("Preparing Subject Index Data Completed ")
        InfoTextProgress("Total : " & subjectIndexCls.CountTableRow("v_subject_index"))

        InfoTextProgress("Preparing Data successfully finished")
    End Sub

    Private Sub btnCreateXML_Click(sender As Object, e As EventArgs) Handles btnCreateXML.Click
        If txtOutput.Text.Length = 0 Then
            MsgBox("Set your save folder location for xml before continue.", vbExclamation, "Error save folder location")
            Exit Sub
        Else
            If Directory.Exists(txtOutput.Text) Then
                InfoTextProgress("START CREATING XML...")
                Try
                    If cbTableContent.Checked = True Then
                        InfoTextProgress("Start Writing Table Of Content")
                        Write_XML_Document()
                        InfoTextProgress("Done")
                        InfoTextProgress("Table Of Content successfully created")
                    End If

                    If cbCasesJud.Checked = True Then
                        InfoTextProgress("Start Writing Cases Judicially Considered")
                        WriteXMLCode_Cases_Judicially()
                        InfoTextProgress("Done")
                        InfoTextProgress("Cases Judicially Considered successfully")
                    End If

                    If cbLegisJud.Checked = True Then
                        InfoTextProgress("Start Writing Legislation Judicially Considered")
                        WriteXMLCode_Legislation_Judicially()
                        InfoTextProgress("Done")
                        InfoTextProgress("Legislation Judicially Considered successfully")
                    End If

                    If cbSubIndex.Checked = True Then
                        InfoTextProgress("Start Writing Subject Index")
                        WriteXMLCode_Subject_Index()
                        InfoTextProgress("Done")
                        InfoTextProgress("Subject Index successfully")
                    End If

                    InfoTextProgress("All XML successfully generated.")

                    If cbOpenOutput.Checked = True Then
                        Process.Start(txtOutput.Text)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            Else
                MsgBox("Output folder is not a valid location", vbExclamation, "Directory was not valid")
                Exit Sub
            End If
        End If
    End Sub

    Public LanguageList As String() = {"english", "malaysian"}
    Public CourtList As List(Of String) = New List(Of String)
    Dim Table_Of_Subject_Index_Level1_List As List(Of String) = New List(Of String)
    Dim Subject_Index_Level1_Citation As String
    Dim Table_Of_Cases_List As List(Of String) = New List(Of String)
    Dim Cases_Citation As String
    Dim Table_Of_Cases_Judicially_List As List(Of String) = New List(Of String)
    Dim Cases_Judicially_Citation As String
    Dim Table_Of_Legislation_Judicially_List As List(Of String) = New List(Of String)
    Dim Legislation_Judicially_Citation As String
    Private Sub Write_XML_Document()
        Dim settings As New XmlWriterSettings
        settings.Indent = True
        ' ALL PREPARATION MUST PUT HERE >>>>>>>>>>>
        CourtList = subjectIndexCls.Court_List()
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Dim XMLOutputPath As String = txtOutput.Text & "\1_TABLE_OF_CONTENT.xml"
        Dim writer As New XmlTextWriter(XMLOutputPath, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        '========================================================================
        writer.WriteStartElement("ROOT")
        writer.WriteStartElement("TABLE_OF_CONTENT")
        '---------------------------------------------------
        ' PART = TITLE TABLE OF CONTENT
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF CONTENT")
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        writer.WriteEndElement() ' END SECTION_TITLE
        writer.WriteEndElement() ' END p

        ' PART = TABLE OF CASES JUDICIALLY
        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Table of Cases Judicially Considered")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        'Table_Of_Cases_Judicially_List = subjectIndexCls.Table_Of_Content_Cases_Judicially_Title
        'For Each Cases_Judicially As String In Table_Of_Cases_Judicially_List
        '    Try
        '        writer.WriteStartElement("p")
        '        writer.WriteStartElement("ANORMAL")
        '        writer.WriteString(ReplaceProperWords(StrConv(Cases_Judicially, VbStrConv.ProperCase)) & Microsoft.VisualBasic.vbTab)
        '        writer.WriteEndElement()
        '        writer.WriteEndElement()
        '        writer.WriteString(Environment.NewLine)
        '        writer.WriteString(Environment.NewLine)
        '    Catch ex As Exception
        '    End Try
        'Next
        ' PART = TABLE OF LEGISLATION JUDICIALLY
        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Table of Legislation  Judicially Considered")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        'Table_Of_Legislation_Judicially_List = subjectIndexCls.Table_Of_Content_Legislation_Judicially_Title
        'For Each Legislation_Judicially As String In Table_Of_Legislation_Judicially_List
        '    Try
        '        writer.WriteStartElement("p")
        '        writer.WriteStartElement("ANORMAL")
        '        writer.WriteString(ReplaceProperWords(StrConv(Legislation_Judicially, VbStrConv.ProperCase)) & Microsoft.VisualBasic.vbTab)
        '        writer.WriteEndElement()
        '        writer.WriteEndElement()
        '        writer.WriteString(Environment.NewLine)
        '        writer.WriteString(Environment.NewLine)
        '    Catch ex As Exception
        '    End Try
        'Next

        ' PART = TABLE OF SUBJECT INDEX
        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Subject Index")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        For Each Language As String In LanguageList
            Try
                Table_Of_Subject_Index_Level1_List = subjectIndexCls.Table_Of_Content_Subject_Index(Language)
                For Each subject_index_level1 As String In Table_Of_Subject_Index_Level1_List
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("ANORMAL")
                    ' Subject_Index_Level1_Citation = subjectIndexCls.Table_Of_Content_Citation_Subject_Index(subject_index_level1, Language)
                    writer.WriteString(StrConv(subject_index_level1, VbStrConv.ProperCase) & Microsoft.VisualBasic.vbTab)
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)
                Next
            Catch ex As Exception
            End Try
        Next

        ' PART = TABLE OF CASES
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("TABLE OF CASES")
        writer.WriteEndElement() 'END ABOLD
        writer.WriteEndElement() 'END P
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        For Each courtName As String In CourtList
            writer.WriteStartElement("p")
            writer.WriteStartElement("ABOLD")
            writer.WriteString(StrConv(courtName, VbStrConv.Uppercase))
            writer.WriteEndElement() 'END ABOLD
            writer.WriteEndElement() 'END P
            writer.WriteString(Environment.NewLine)
            writer.WriteString(Environment.NewLine)
            Try
                Table_Of_Cases_List = subjectIndexCls.Table_Of_Content_Cases_Title(courtName)
                For Each CaseTitle As String In Table_Of_Cases_List
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("ANORMAL")
                    Cases_Citation = subjectIndexCls.Table_Of_Content_Citation_Cases(courtName, CaseTitle)
                    If cboSelection.Text = "CONSOLIDATED INDEX" Then
                        writer.WriteString(ReplaceProperWords(StrConv(CaseTitle, VbStrConv.ProperCase)) & Microsoft.VisualBasic.vbTab & StrConv(Cases_Citation, VbStrConv.Uppercase))

                    Else
                        writer.WriteString(ReplaceProperWords(StrConv(CaseTitle, VbStrConv.ProperCase)) & Microsoft.VisualBasic.vbTab & StrConv(PrintingC.PageNo_From_Citation(Cases_Citation), VbStrConv.Uppercase))

                    End If
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)
                Next
            Catch ex As Exception
            End Try
        Next
        '---------------------------------------------------
        writer.WriteEndElement() ' TABLE_OF_CONTENT END
        writer.WriteEndElement() ' ROOT END
        '========================================================================
        writer.WriteEndDocument() '=====> END WRITESTART DOCUMENT
        writer.Close()

        'Process.Start(XMLOutputPath)
    End Sub

    Dim LetterList As List(Of String) = New List(Of String)
    Dim Cases_Judicially_List As List(Of String) = New List(Of String)
    Dim Cases_Judicially_Source_List As List(Of String) = New List(Of String)
    Private Sub WriteXMLCode_Cases_Judicially()
        Dim settings As New XmlWriterSettings
        settings.Indent = True
        ' ALL PREPARATION MUST PUT HERE >>>>>>>>>>>
        CourtList = subjectIndexCls.Court_List()

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Dim XMLOutputPath As String = txtOutput.Text & "\2_CASES_JUDICIALLY_CONSIDERED.xml"
        Dim writer As New XmlTextWriter(XMLOutputPath, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        '======================================================================
        writer.WriteStartElement("ROOT")
        writer.WriteStartElement("TABLE_OF_CASES_JUDICIALLY_CONSIDERED")
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        '---------------------------------------------------
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF CASES JUDICIALLY CONSIDERED")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        LetterList = subjectIndexCls.CasesJudicial_FirstLetter
        For Each firstLetter As String In LetterList
            writer.WriteStartElement("p")
            writer.WriteStartElement("CBOLD")
            writer.WriteString(StrConv(firstLetter, VbStrConv.Uppercase))
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteString(Environment.NewLine)
            writer.WriteString(Environment.NewLine)

            Cases_Judicially_List = subjectIndexCls.CasesJudicial_Title(firstLetter)
            For Each Case_Judicially As String In Cases_Judicially_List
                Dim cTitle As String = Case_Judicially.Split({"#"}, StringSplitOptions.None)(0)
                Dim cCitation As String = Case_Judicially.Split({"#"}, StringSplitOptions.None)(1)
                Dim cReferType As String = Case_Judicially.Split({"#"}, StringSplitOptions.None)(2)
                writer.WriteStartElement("p")
                writer.WriteStartElement("CBOLD")
                writer.WriteString(cTitle & " " & cCitation & " (" & cReferType & ")")
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)

                Cases_Judicially_Source_List = subjectIndexCls.CasesJudicial_Judgment_Name(firstLetter, cTitle)
                For Each Case_Judicially_Source As String In Cases_Judicially_Source_List
                    writer.WriteStartElement("p2")
                    writer.WriteStartElement("CITALIC")
                    writer.WriteString(ReplaceProperWords(StrConv(Case_Judicially_Source, VbStrConv.ProperCase)))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                Next
                Application.DoEvents()
                writer.WriteString(Environment.NewLine)
            Next
        Next
        '---------------------------------------------------
        writer.WriteEndElement() ' TABLE_OF_CASES_JUDICIALLY_CONSIDERED END
        writer.WriteEndElement() ' ROOT END
        '========================================================================
        writer.WriteEndDocument() '=====> END WRITESTART DOCUMENT
        writer.Close()

        ' Process.Start(XMLOutputPath)
    End Sub

    Dim Legislation_Judicially_List As List(Of String) = New List(Of String)
    Dim Legislation_Judicially_Section_List As List(Of String) = New List(Of String)
    Dim Legislation_Judicially_Source_Citation As List(Of String) = New List(Of String)
    Private Sub WriteXMLCode_Legislation_Judicially()
        Dim settings As New XmlWriterSettings
        settings.Indent = True
        ' ALL PREPARATION MUST PUT HERE >>>>>>>>>>>
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Dim XMLOutputPath As String = txtOutput.Text & "\3_LEGISLATION_JUDICIALLY_CONSIDERED.xml"
        Dim writer As New XmlTextWriter(XMLOutputPath, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        '======================================================================
        writer.WriteStartElement("ROOT")
        writer.WriteStartElement("TABLE_OF_LEGISLATIONS_JUDICIALLY_CONSIDERED")
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        '---------------------------------------------------
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF LEGISLATIONS JUDICIALLY CONSIDERED")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        Legislation_Judicially_List = subjectIndexCls.Legis_Judicial_Title
        For Each Legislation_Judicially As String In Legislation_Judicially_List
            Try
                writer.WriteStartElement("p")
                writer.WriteStartElement("DBOLD")
                writer.WriteString(StrConv(Legislation_Judicially, VbStrConv.ProperCase))
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)

                Legislation_Judicially_Section_List = subjectIndexCls.Legis_Judicial_Section(Legislation_Judicially)
                For Each Legislation_Section As String In Legislation_Judicially_Section_List
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("DNORMAL")

                    Legislation_Judicially_Source_Citation = subjectIndexCls.Legis_Judicial_Citation(Legislation_Judicially, Legislation_Section)
                    If Legislation_Section.Trim = "s" Then
                    Else
                        writer.WriteString(Legislation_Section)
                    End If

                    For Each Source_Citation As String In Legislation_Judicially_Source_Citation
                        Application.DoEvents()
                        If cboSelection.Text = "CONSOLIDATED INDEX" Then
                            writer.WriteString(" " & Microsoft.VisualBasic.vbTab & Source_Citation)
                        Else
                            writer.WriteString(" " & Microsoft.VisualBasic.vbTab & PrintingC.PageNo_From_Citation(Source_Citation))
                        End If

                        writer.WriteString(Environment.NewLine)
                    Next

                    writer.WriteEndElement()
                    writer.WriteEndElement() ' end dnormal
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)
                Next
            Catch ex As Exception
            End Try
        Next
        '---------------------------------------------------
        writer.WriteEndElement() ' TABLE_OF_CASES_JUDICIALLY_CONSIDERED END
        writer.WriteEndElement() ' ROOT END
        '========================================================================
        writer.WriteEndDocument() '=====> END WRITESTART DOCUMENT
        writer.Close()

        ' Process.Start(XMLOutputPath)
    End Sub

    Dim Subject_Index_Level1_List As List(Of String) = New List(Of String)
    Dim Subject_Index_Level2_List As List(Of String) = New List(Of String)
    Dim Subject_Index_Summary_List As List(Of String) = New List(Of String)
    Dim Subject_Index_Data_Coll As List(Of String) = New List(Of String)
    Dim Subject_Index_Judgment_Name As String
    Dim Subject_Index_Judge_Name As String
    Dim Subject_Index_Citation As String
    Private Sub WriteXMLCode_Subject_Index()
        Dim settings As New XmlWriterSettings
        settings.Indent = True
        ' ALL PREPARATION MUST PUT HERE >>>>>>>>>>>
        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Dim XMLOutputPath As String = txtOutput.Text & "\3_SUBJECT_INDEX.xml"
        Dim writer As New XmlTextWriter(XMLOutputPath, System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        '======================================================================
        writer.WriteStartElement("ROOT")
        writer.WriteStartElement("SUBJECT_INDEX")
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        '---------------------------------------------------
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("SUBJECT_INDEX")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        For Each LANGUAGE As String In LanguageList
            Subject_Index_Level1_List = subjectIndexCls.SubjectIndex_Level1(LANGUAGE)
            For Each Level1 As String In Subject_Index_Level1_List
                writer.WriteStartElement("p")
                writer.WriteStartElement("EBOLD")
                writer.WriteString(StrConv(Level1, VbStrConv.Uppercase))
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)

                Subject_Index_Level2_List = subjectIndexCls.SubjectIndex_Level2(Level1, LANGUAGE)
                For Each Level2 As String In Subject_Index_Level2_List
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("EBOLD")
                    writer.WriteString(StrConv(Level2, VbStrConv.ProperCase))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)

                    Subject_Index_Summary_List = subjectIndexCls.SubjectIndex_Summary(Level1, Level2)
                    For Each Summary As String In Subject_Index_Summary_List
                        writer.WriteStartElement("p")
                        writer.WriteStartElement("ENORMAL")
                        writer.WriteString(Summary)
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)
                        'writer.WriteString(Environment.NewLine)

                        Subject_Index_Data_Coll = subjectIndexCls.SubjectIndexDataColl(Level1, Level2, Summary)
                        For Each Data_Coll In Subject_Index_Data_Coll
                            Subject_Index_Judgment_Name = Data_Coll.Split({"#"}, StringSplitOptions.None)(0)
                            Subject_Index_Judge_Name = Data_Coll.Split({"#"}, StringSplitOptions.None)(1)
                            Subject_Index_Citation = Data_Coll.Split({"#"}, StringSplitOptions.None)(2)

                            writer.WriteStartElement("p")
                            writer.WriteStartElement("EGITALIC")
                            writer.WriteString(ReplaceProperWords(StrConv(Subject_Index_Judgment_Name, VbStrConv.ProperCase)))
                            writer.WriteEndElement()
                            writer.WriteEndElement()
                            writer.WriteString(Environment.NewLine)
                            writer.WriteStartElement("p")
                            writer.WriteStartElement("EGNORMAL")
                            writer.WriteString("(" & Subject_Index_Judge_Name.Replace("&lt;NAME&gt;", "").Replace("&lt;/NAME&gt;", "").Replace("<NAME>", "").Replace("</NAME>", "") & ")")

                            If cboSelection.Text = "CONSOLIDATED INDEX" Then
                                writer.WriteString(Microsoft.VisualBasic.vbTab & Subject_Index_Citation)
                            Else ' subject index mode
                                writer.WriteString(Microsoft.VisualBasic.vbTab & PrintingC.PageNo_From_Citation(Subject_Index_Citation))
                            End If
                            writer.WriteEndElement()
                            writer.WriteEndElement()
                            writer.WriteString(Environment.NewLine)
                            writer.WriteString(Environment.NewLine)
                        Next
                    Next
                Next
            Next
        Next
        '---------------------------------------------------
        writer.WriteEndElement() ' TABLE_OF_CASES_JUDICIALLY_CONSIDERED END
        writer.WriteEndElement() ' ROOT END
        '========================================================================
        writer.WriteEndDocument() '=====> END WRITESTART DOCUMENT
        writer.Close()

        '  Process.Start(XMLOutputPath)
    End Sub

    Private Sub WriteXMLCode()
        Dim xmlFileName As String
        xmlFileName = UCase(txtOutput.Text)

        Dim writer As New XmlTextWriter(xmlFileName, Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.WriteStartElement("ROOT")
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("DOCUMENT_INFO")
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("COMPANY")

        writer.WriteString("Malaysian Law Review")
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        Dim arrFile() As String

        Try
            writer.WriteStartElement("TITLE")
            arrFile = xmlFileName.Split("\")
            Dim citation As String = arrFile(arrFile.Count - 1).Replace(".XML", "")
            writer.WriteString(citation)
            writer.WriteEndElement()
            writer.WriteString(Environment.NewLine)
            Dim subCitation() As String = citation.Split("_")

            writer.WriteStartElement("COURT")
            writer.WriteString(GetCourtName(subCitation(0)))
            writer.WriteEndElement()

            writer.WriteString(Environment.NewLine)
            writer.WriteStartElement("YEAR")
            writer.WriteString(subCitation(1))
            writer.WriteEndElement()

            writer.WriteString(Environment.NewLine)
            writer.WriteStartElement("VOLUME")
            writer.WriteString(subCitation(2))
            writer.WriteEndElement()

            writer.WriteString(Environment.NewLine)
            writer.WriteStartElement("PART")
            writer.WriteString(" ")
            writer.WriteEndElement()
            writer.WriteString(Environment.NewLine)

        Catch ex As Exception
        End Try
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        InfoTextProgress("Writing Document Info value... done")

        writer.WriteStartElement("LIST_OF_CONTENT")
        writer.WriteString(Environment.NewLine)

        '=======================================================================TABLE OF CONTENT
        writer.WriteStartElement("TABLE_OF_CONTENT") ' =================>>> AAAAAAA
        writer.WriteString(Environment.NewLine)
        '====================================================================
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF CONTENT")
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        writer.WriteEndElement()
        writer.WriteEndElement()

        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Table of Cases")
        writer.WriteEndElement()

        writer.WriteString(" " & "1")

        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Table of Cases Judicially Considered")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Table of Legislation  Judicially Considered")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        writer.WriteStartElement("p")
        writer.WriteStartElement("ABOLD")
        writer.WriteString("Subject Index")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        Dim strLanguage As String = "english"
        Try
            subjectIndexText = subjectIndexCls.SubjectIndex_Level1(strLanguage)
            For i As Integer = 0 To subjectIndexText.Count - 1
                Dim subIndex As String = subjectIndexText(i).ToString
                writer.WriteStartElement("p")
                writer.WriteStartElement("ANORMAL")

                writer.WriteString(StrConv(subIndex, VbStrConv.ProperCase))

                subjectIndexPage = subjectIndexCls.Table_Of_Content_Citation_Subject_Index(subIndex, strLanguage)
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)
            Next i
            strLanguage = "malaysian"
            subjectIndexText = subjectIndexCls.SubjectIndex_Level1(strLanguage)

            For i As Integer = 0 To subjectIndexText.Count - 1
                Dim subIndex As String = subjectIndexText(i).ToString
                writer.WriteStartElement("p")
                writer.WriteStartElement("ANORMAL")
                writer.WriteString(StrConv(subIndex, VbStrConv.ProperCase))
                subjectIndexPage = subjectIndexCls.Table_Of_Content_Citation_Subject_Index(subIndex, strLanguage)
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)
            Next i

        Catch ex As Exception
        End Try

        ' writer.WriteEndElement() ' closing table
        writer.WriteEndElement() ' end of list_of_Content
        writer.WriteEndElement() ' end of table_of_content
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        InfoTextProgress("Writing Table Of Content... done")


        '===========================================================TABLE CONTENT OF CASES============================================================
        writer.WriteStartElement("CONTENT")

        writer.WriteStartElement("TABLE_OF_CASES") '==========================>>>> BBBBBBBBBBBBBBBBBBBBBBBBBB
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF CASES")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        writer.WriteStartElement("p")
        'writer.WriteStartElement("BNORMAL")
        'writer.WriteString("The mode of citation of cases reported in this volume of the Malaysian Law Review ")
        'writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        myCourtList = subjectIndexCls.Court_List
        For Each strCourt As String In myCourtList

            writer.WriteStartElement("p")
            writer.WriteStartElement("BBOLD")
            writer.WriteString(strCourt.ToUpper)
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteString(Environment.NewLine)
            writer.WriteString(Environment.NewLine)
            tableContentCases = subjectIndexCls.Table_Of_Content_Cases_Title(strCourt)
            For Each CasesWithCitation As String In tableContentCases
                'MsgBox(CasesWithCitation)
                Try
                    Dim JudgmentName As String = CasesWithCitation.Split("#")(0)
                    Dim Citation As String = CasesWithCitation.Split("#")(1)
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("BNORMAL")
                    writer.WriteString(ReplaceProperWords(StrConv(JudgmentName.Replace(vbTab, " "), VbStrConv.ProperCase) & " " & Citation))

                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)
                Catch ex As Exception

                End Try
                
            Next
        Next
        ''''====== writer.WriteEndElement() ' closing table
        writer.WriteEndElement() ' end of table_of_cases
        writer.WriteString(Environment.NewLine)
        InfoTextProgress("Writing Table Of Cases... done")
        '===========================================================TABLE CONTENT OF CASES JUDICIALLY CONSIDERED===========================================

        writer.WriteStartElement("TABLE_OF_CASES_JUDICIALLY_CONSIDERED") '=============>>>>>> CCCCCCCCCCC
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF CASES JUDICIALLY CONSIDERED")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        myCourtList = subjectIndexCls.Court_List()

        firstLetterJudCases = subjectIndexCls.CasesJudicial_FirstLetter

        Dim SubjectIndexJudCases As List(Of String) = New List(Of String)
        Dim TitleJudCases As List(Of String) = New List(Of String)

        For Each fl As String In firstLetterJudCases
            writer.WriteStartElement("p")
            writer.WriteStartElement("CBOLD")
            writer.WriteString(fl)
            writer.WriteEndElement()
            writer.WriteEndElement()
            writer.WriteString(Environment.NewLine)
            writer.WriteString(Environment.NewLine)
            SubjectIndexJudCases = subjectIndexCls.CasesJudicial_Title(fl)

            For Each strJudCases As String In SubjectIndexJudCases
                Dim referredTitle As String
                Dim line1 As String
                Try
                    referredTitle = strJudCases.Split({"#"}, StringSplitOptions.None)(0)
                    line1 = ReplaceProperWords(strJudCases.Replace("#", " "))
                Catch ex As Exception
                    Continue For
                End Try

                writer.WriteStartElement("p")
                writer.WriteStartElement("CBOLD")
                writer.WriteString(line1)
                writer.WriteEndElement()
                writer.WriteEndElement()
                writer.WriteString(Environment.NewLine)

                TitleJudCases = subjectIndexCls.CasesJudicial_Judgment_Name(fl, referredTitle)
                Dim line2 As String = ""
                Try
                    For Each strTitle As String In TitleJudCases
                        line2 = RemoveBackSlash(strTitle)
                        writer.WriteStartElement("p2")
                        writer.WriteStartElement("CITALIC")
                        writer.WriteString(ReplaceProperWords(RemoveBackSlash(StrConv(line2, VbStrConv.ProperCase))))
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)
                    Next
                Catch ex As Exception
                    'MsgBox(fl & " " & referredTitle)
                    Continue For
                End Try

                writer.WriteString(Environment.NewLine)
            Next
        Next

        writer.WriteEndElement() ' end of table_of_cases
        writer.WriteString(Environment.NewLine)

        '=======================================================TABLE OF LEGISLATIONS JUDICIALLY CONSIDERED=========================================
        InfoTextProgress("Writing Legislations Judicially Considered... Start")
        Dim counter As Integer = 0
        writer.WriteStartElement("TABLE_OF_LEGISLATIONS_JUDICIALLY_CONSIDERED") '=======>>>DDDDD
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("TABLE OF LEGISLATIONS JUDICIALLY CONSIDERED")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)
        tableContentLegisJud = subjectIndexCls.Legis_Judicial_Title

        For Each strLegislation As String In tableContentLegisJud
            writer.WriteStartElement("p")
            writer.WriteStartElement("DBOLD")
            'Dim newLegislation As String = ReplaceProperWords(StrConv(strLegislation, VbStrConv.ProperCase))
            'If newLegislation.EndsWith(",") Or newLegislation.EndsWith(", ") Then
            '    newLegislation = Trim(newLegislation).Remove(newLegislation.Length - 1, 1)
            'End If
            writer.WriteString(ReplaceProperWords(StrConv(strLegislation, VbStrConv.ProperCase)))
            writer.WriteEndElement() ' end dbold
            writer.WriteString(Environment.NewLine)
            writer.WriteString(Environment.NewLine)

            LegisJud_SectionList = subjectIndexCls.Legis_Judicial_Section(strLegislation)
        Next

        '====writer.WriteEndElement() ' closing table
        writer.WriteEndElement() ' end of table_of_cases
        writer.WriteString(Environment.NewLine)
        InfoTextProgress("Writing Legislations Judicially Considered... done")
        '=======================================================SUBJECT INDEX================================================================================
        writer.WriteStartElement("SUBJECT_INDEX") '============>>>>>>>EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        writer.WriteString(Environment.NewLine)
        writer.WriteStartElement("p")
        writer.WriteStartElement("SECTION_TITLE")
        writer.WriteString("SUBJECT INDEX")
        writer.WriteEndElement()
        writer.WriteEndElement()
        writer.WriteString(Environment.NewLine)
        writer.WriteString(Environment.NewLine)

        Try
            'ENGLISH LANGUAGE'
            Dim si_Language = "english"
            Dim Level1 As List(Of String) = subjectIndexCls.SubjectIndex_Level1(si_Language)
            For Each strLevel1 As String In Level1
                '=========================writer.WriteString("======================================================================================")
                writer.WriteStartElement("p")
                writer.WriteStartElement("EBOLD")
                writer.WriteString(UCase(strLevel1))
                writer.WriteEndElement()
                writer.WriteEndElement()
                '=========================writer.WriteString("======================================================================================")
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)

                Dim Level2 As List(Of String) = subjectIndexCls.SubjectIndex_Level2(strLevel1, si_Language)
                For Each strLevel2 As String In Level2
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("EBOLD")
                    writer.WriteString(StrConv(strLevel2, VbStrConv.ProperCase))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)

                    Dim Summary As List(Of String) = subjectIndexCls.SubjectIndex_Summary(strLevel1, strLevel2)

                    For Each strSummary As String In Summary
                        Dim summaryToWrite As String = strSummary
                        writer.WriteStartElement("p")
                        writer.WriteStartElement("ENORMAL")
                        writer.WriteString(Trim(Trim(summaryToWrite) & " "))
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)

                        Dim dataList As List(Of String) = subjectIndexCls.SelectSubjectIndexDataColl(strLevel1, strLevel2, strSummary)
                        For Each strData In dataList
                            Dim judgmentName As String = strData.Split("#")(0)
                            writer.WriteStartElement("p")
                            writer.WriteStartElement("EGITALIC")
                            writer.WriteString(ReplaceProperWords(StrConv(judgmentName, VbStrConv.ProperCase)))
                            writer.WriteEndElement()
                            writer.WriteEndElement()
                            writer.WriteString(Environment.NewLine)

                            Dim judgeName As String = strData.Split("#")(1).Replace("''", "'")
                            writer.WriteStartElement("p")
                            writer.WriteStartElement("EGNORMAL")
                            writer.WriteString("(" & judgeName.Replace("&lt;NAME&gt;", "").Replace("&lt;/NAME&gt;", "").Replace("<NAME>", "").Replace("</NAME>", "") & ")")

                            Dim Page As String = strData.Split("#")(2)
                            writer.WriteString(" " & Page)
                            writer.WriteEndElement()
                            writer.WriteEndElement()
                            writer.WriteString(Environment.NewLine)
                            writer.WriteString(Environment.NewLine)
                        Next
                        'writer.WriteStartElement("p")
                        'writer.WriteStartElement("EGNORMAL")
                        'writer.WriteEndElement()
                        'writer.WriteEndElement()
                        'writer.WriteString(Environment.NewLine)
                        'writer.WriteString(Environment.NewLine)
                    Next strSummary
                Next strLevel2
                writer.WriteString(Environment.NewLine)
                'Next
            Next strLevel1

            'MALAY LANGUAGE'
            si_Language = "malaysian"
            Level1 = subjectIndexCls.SubjectIndex_Level1(si_Language)
            For Each strLevel1 As String In Level1
                'writer.WriteString("======================================================================================")
                'writer.WriteString(Environment.NewLine)
                writer.WriteStartElement("p")
                writer.WriteStartElement("EBOLD")
                writer.WriteString(UCase(strLevel1))
                writer.WriteEndElement()
                writer.WriteEndElement()
                'writer.WriteString(Environment.NewLine)
                'writer.WriteString("======================================================================================")
                writer.WriteString(Environment.NewLine)
                writer.WriteString(Environment.NewLine)

                Dim Level2 As List(Of String) = subjectIndexCls.SubjectIndex_Level2(strLevel1, si_Language)
                For Each strLevel2 As String In Level2
                    writer.WriteStartElement("p")
                    writer.WriteStartElement("EBOLD")
                    writer.WriteString(StrConv(strLevel2, VbStrConv.ProperCase))
                    writer.WriteEndElement()
                    writer.WriteEndElement()
                    writer.WriteString(Environment.NewLine)
                    writer.WriteString(Environment.NewLine)

                    Dim Summary As List(Of String) = subjectIndexCls.SubjectIndex_Summary(strLevel1, strLevel2)
                    For Each strSummary As String In Summary
                        Dim summaryToWrite As String = strSummary
                        summaryToWrite = summaryToWrite.Replace("\""", "'")
                        writer.WriteStartElement("p")
                        writer.WriteStartElement("ENORMAL")
                        writer.WriteString(Trim(summaryToWrite) & " ")
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)

                        Dim judgmentName As String = subjectIndexCls.SelectSubjectIndexData(strLevel1, strLevel2, "judgmentName")
                        writer.WriteStartElement("p")
                        writer.WriteStartElement("EGITALIC")
                        writer.WriteString(ReplaceProperWords(StrConv(judgmentName, VbStrConv.ProperCase)))
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)

                        Dim judgeName As String = subjectIndexCls.SelectSubjectIndexData(strLevel1, strLevel2, "judgeName")
                        writer.WriteStartElement("p")
                        writer.WriteStartElement("EGNORMAL")
                        writer.WriteString("(" & judgeName & ")")

                        Dim Page As String = subjectIndexCls.SubjectIndex_Citation(strLevel1, strLevel2, strSummary, si_Language)
                        writer.WriteString(" " & Page.Remove(Page.Length - 2, 1))
                        writer.WriteEndElement()
                        writer.WriteEndElement()
                        writer.WriteString(Environment.NewLine)
                        writer.WriteString(Environment.NewLine)

                        'writer.WriteStartElement("p")
                        'writer.WriteStartElement("EGNORMAL")
                        'writer.WriteEndElement()
                        'writer.WriteEndElement()
                        'writer.WriteString(Environment.NewLine)
                        'writer.WriteString(Environment.NewLine)
                    Next strSummary
                Next strLevel2
                writer.WriteString(Environment.NewLine)
                'Next
            Next strLevel1

        Catch ex As Exception

        End Try

        writer.WriteEndElement() ' end of subject_index
        writer.WriteEndElement() ' end of contents
        writer.WriteString(Environment.NewLine)

        InfoTextProgress("Writing Subject Index... done")
        writer.WriteEndDocument() '==> END OF ROOT
        writer.Close()

        InfoTextProgress("XML successfully written.")

        If MsgBox("XML successfully written. Do you want to open it now?", vbYesNo, vbInformation) = MsgBoxResult.Yes Then
            Process.Start(xmlFileName)
        End If

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim list As List(Of String) = subjectIndexCls.Testing
        For Each cibai As String In list
            MsgBox(cibai)
        Next
    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs)
        MsgBox(EscapeString("saya' & % ber'sama"))
    End Sub
   

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        DataPreview.Visible = True
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Button1_Click_3(sender As Object, e As EventArgs) Handles Button1.Click
        Dim input1 As String = TextBox1.Text
        Dim input2 As String = TextBox2.Text
        Dim finalOutput As String = ""

        Dim input2Pattern As String
        Dim compareRegex As Regex
        Dim compareMatch As Match

        If input2.StartsWith("(") Then
            input2Pattern = Extractor.FindRegexPattern_Bracket(input2)
            'MsgBox(input2Pattern)
            compareRegex = New Regex(input2Pattern)
            compareMatch = compareRegex.Match(input1)
            If compareMatch.Success() Then
                Dim replaceValue As String = compareMatch.Value

                Dim newValue As String = Regex.Replace(TextBox1.Text, input2Pattern, replaceValue)
                finalOutput = newValue
            End If
        End If

        Dim bracketPattern As String = Extractor.FindRegexPattern_Bracket(input1)
        Dim r As New Regex(bracketPattern)
        Dim tmpValue As String = r.Replace(input1, input2)
        Dim result As String = input1 & Environment.NewLine & tmpValue
        'MsgBox(result)
    End Sub

    Public Function LegislationSectionReplacer(ByVal input1 As String, ByVal input2 As String) As String
        Dim finalOutput As String = ""
        Dim input2Pattern As String
        Dim compareRegex As Regex
        Dim compareMatch As Match
        If input2.StartsWith("(") Then
            input2Pattern = Extractor.FindRegexPattern_Bracket(input2)
            compareRegex = New Regex(input2Pattern)
            compareMatch = compareRegex.Match(input1)
            If compareMatch.Success() Then
                Dim replaceValue As String = compareMatch.Value

                Dim newValue As String = Regex.Replace(input1, input2Pattern, replaceValue)
                finalOutput = newValue
            End If
        End If

        Dim bracketPattern As String = Extractor.FindRegexPattern_Bracket(input1)
        Dim r As New Regex(bracketPattern)
        Dim tmpValue As String = r.Replace(input1, input2)
        Dim result As String = tmpValue
        Return result
    End Function

    Private Sub btnExtractorTest_Click(sender As Object, e As EventArgs) Handles btnExtractorTest.Click
        'Dim l As List(Of String) = Extractor.FindRegexPatternList(TextBox1.Text)
        'For Each s As String In l
        '    MsgBox(s)
        'Next
        ' TextBox3.Text =
        TextBox3.Text = Extractor.Legislation_Regex_ValueAdded(TextBox1.Text, TextBox2.Text)
    End Sub

   
End Class

'''''<=====>'''''End Namespace
