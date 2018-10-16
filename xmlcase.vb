Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports System.Text.RegularExpressions
Public Class xmlcase
    Dim strFileName As String
    Dim xmlFile As XmlDocument = New XmlDocument

    Public Sub New(ByVal filename As String)
        strFileName = filename
        Try
            xmlFile.Load(strFileName)
        Catch ex As Exception
            MsgBox("Error in " & strFileName & "\n" & ex.Message)
        End Try
    End Sub
    Public Function JUDGE_NAME() As String
        Try
            Dim jnameXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("JUDGE_NAME")
            Return jnameXML.InnerXml.ToString().Replace("'", "''")
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function Parse_Judge_Name(ByVal strJudgename As String) As String
        Dim currentString As String = ""
        currentString = StrConv(strJudgename, VbStrConv.ProperCase)
        Return currentString
    End Function
    Public Function COURT_TYPE() As String
        Dim arrCourts As String() = {"APPEAL", "HIGH", "FEDERAL", "INDUSTRIAL", "PERSEKUTUAN", "RAYUAN", "TINGGI", "PERUSAHAAN"}
        Dim arrCourtsreturned As String() = {"Court of Appeal", "High Court", "Federal Court", "Industrial Court", "Federal Court", "Court of Appeal", "High Court", "Industrial Court"}
        Dim strCourt As String = String.Empty

        Try
            Dim ctameXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("COURT_TYPE")
            strCourt = ctameXML.InnerText.Trim()
            Dim courtCounter As Integer = 0

            For Each court As String In arrCourts
                If (strCourt.ToUpper().Contains(court)) Then
                    Return arrCourtsreturned(courtCounter)
                End If
                courtCounter = courtCounter + 1

            Next court

            If ctameXML.InnerXml.ToString().Contains(",") Then
                Return ctameXML.InnerXml.ToString().Substring(0, ctameXML.InnerXml.ToString().IndexOf(",")).Trim()
            Else
                Return ctameXML.InnerXml.ToString().Trim()
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function JUDGMENT_NAME() As String
        Try
            Dim caseName As String = String.Empty
            Dim jdmentnameXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("JUDGMENT_NAME")
            caseName = jdmentnameXML.InnerXml.ToString().Replace("'", "'")
            caseName = HELPER.ReplaceString(caseName, "&amp;", "&", StringComparison.CurrentCultureIgnoreCase)
            caseName = HELPER.ReplaceString(caseName, "&amp", "&", StringComparison.CurrentCultureIgnoreCase)
            Return caseName
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function JUDGMENT_LANGUAGE() As String
        Try
            Dim JUDGMENT_LANGUAGEXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("JUDGMENT_LANGUAGE")
            Return JUDGMENT_LANGUAGEXML.InnerXml.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function CATCHWORDS() As XmlNode
        Try
            Dim catchwordsXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//CATCHWORDS")
            Return catchwordsXML
        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("No catch words in \n " & strFileName)
            Dim catchwordsXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//CATCHWORDS")
            Return catchwordsXML
        End Try
    End Function

    Public Function CATCHWORDSWITHP() As XmlNodeList
        Try
            Dim catchwordsXML As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//CATCHWORDS//p")
            Return catchwordsXML

        Catch ex As Exception
            System.Windows.Forms.MessageBox.Show("No catch words in \n " & strFileName)
            Dim catchwordsXML As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//CATCHWORDS//p")
            Return catchwordsXML
        End Try
    End Function
    Public Function REFERRED_CASES() As XmlNode
        Try
            Dim REFERRED_CASESXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//REFERRED_CASES")
            Return REFERRED_CASESXML
        Catch ex As Exception
            Dim catchwordsXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//REFERRED_CASES")
            Return catchwordsXML
        End Try
    End Function
    Public Function F_REFERRED_CASES() As List(Of String)
        Dim foreignCases As New List(Of String)
        Try
            Dim F_REFERRED_CASESXML1 As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//REFERRED_CASES//P")
            Dim F_REFERRED_CASESXML2 As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//REFERRED_CASES//p")

            For Each fcase As XmlNode In F_REFERRED_CASESXML1
                Dim fp As String = fcase.InnerXml.ToUpper
                If fp.Contains("LINK") = False And fp.Contains("REFERRED TO") = False Then
                    foreignCases.Add(fp)
                End If
            Next
            For Each fcase As XmlNode In F_REFERRED_CASESXML2
                Dim fp As String = fcase.InnerXml.ToUpper
                If fp.Contains("LINK") = False And fp.Contains("REFERRED TO") = False Then
                    foreignCases.Add(fp)
                End If
            Next
            Return foreignCases
        Catch ex As Exception
            Return foreignCases
        End Try
    End Function
    Public Function REFERRED_LEGISLATIONS() As XmlNode
        Try
            Dim REFERRED_LEGISLATIONSSXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//REFERRED_LEGISLATIONS")
            Return REFERRED_LEGISLATIONSSXML
        Catch ex As Exception
            Dim REFERRED_LEGISLATIONSSXML As XmlNode = xmlFile.DocumentElement.SelectSingleNode("HEADNOTE//REFERRED_LEGISLATIONS")
            Return REFERRED_LEGISLATIONSSXML
        End Try

    End Function

    Public Function F_REFERRED_LEGISLATIONS() As List(Of String)
        Dim foreignLeg As New List(Of String)
        Try
            Dim F_REFERRED_LEGISLATIONSXML1 As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//REFERRED_LEGISLATIONS//P") ' must add i tag
            Dim F_REFERRED_LEGISLATIONSXML2 As XmlNodeList = xmlFile.DocumentElement.SelectNodes("HEADNOTE//REFERRED_LEGISLATIONS//P")
            For Each flegis As XmlNode In F_REFERRED_LEGISLATIONSXML1

                Dim fp As String = flegis.InnerXml.ToUpper
                If fp.Contains("LINK") = False And fp.Contains("LEGISLATION REFERRED") = False Then
                    foreignLeg.Add(fp)
                End If
            Next

            For Each flegis As XmlNode In F_REFERRED_LEGISLATIONSXML2
                Dim fp As String = flegis.InnerXml.ToUpper
                If fp.Contains("LINK") = False And fp.Contains("LEGISLATION REFERRED") = False Then
                    foreignLeg.Add(fp)
                End If
            Next
            Return foreignLeg

        Catch ex As Exception
            Return foreignLeg
        End Try

    End Function

    Public Function PAGE_NO() As String
        Try
            Dim bsfn As String = System.IO.Path.GetFileNameWithoutExtension(strFileName)
            Return bsfn.Substring(bsfn.LastIndexOf("_") + 1)

        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function subjectindex() As String
        Try
            Dim bsfn As String = System.IO.Path.GetFileNameWithoutExtension(strFileName)
            Return bsfn.Substring(bsfn.LastIndexOf("_") + 1)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function getcitation(ByVal str As String) As String
        Dim sIndex As Integer
        Dim endIndex As Integer
        sIndex = str.IndexOf("=")
        endIndex = str.IndexOf(";")
        If sIndex <> -1 And endIndex <> -1 Then
            str = str.Substring(sIndex + 1, endIndex - sIndex).Trim().ToUpper()
            If str.Length > 8 Then
                Return str.Replace(";", "")
            End If
        End If
        Return ""
    End Function

    Public Function getlegcitation(ByVal str As String) As String
        Dim sIndex As Integer
        sIndex = str.IndexOf("?")
        If sIndex <> -1 Then
            str = str.Substring(sIndex + 1).Trim().ToUpper()
            If str.Length > 8 Then
                Return str
            End If
        End If
        Return ""
    End Function

    Public Function getCitIndentRef()
        Dim doc As XmlDocument = New XmlDocument
        doc.LoadXml(xmlFile.OuterXml.ToLower())

        Dim citation As String = Path.GetFileNameWithoutExtension(strFileName)
        Dim caseIndent As List(Of String) = New List(Of String)
        Dim legIndent As List(Of String) = New List(Of String)

        Dim blockq As XmlNodeList
        Dim links As XmlNodeList

        Dim listref As List(Of String) = New List(Of String)
        Dim listrefbq As List(Of String) = New List(Of String)

        Dim listrefleg As List(Of String) = New List(Of String)
        Dim listrefbqleg As List(Of String) = New List(Of String)

        blockq = doc.GetElementsByTagName("blockquote")
        Dim refCitation As String = ""
        For Each item As XmlNode In doc.SelectNodes("//blockquote//link")
            Try
                refCitation = item.Attributes("href").Value
                If refCitation.Contains("showcase") Then
                    If getcitation(refCitation) <> "" Then
                        listrefbq.Add(getcitation(refCitation))
                    End If
                End If
            Catch ex As Exception
                File.AppendAllText("linkserrors.txt", citation & "\t" & ex.Message & "\n")
                Continue For
            End Try
        Next

        For Each item As XmlNode In doc.SelectNodes("//blockquote//link")
            Try
                refCitation = item.Attributes("href").Value
                If refCitation.Contains("legis") Then
                    If getlegcitation(refCitation) <> "" Then
                        listrefbqleg.Add(getlegcitation(refCitation))
                    End If
                End If

            Catch ex As Exception
                File.AppendAllText("linkserrors.txt", citation & "\t" & ex.Message & "\n")
                Continue For
            End Try
        Next

        ' ============ removing from blockquote ==============
        Try
            For i As Integer = 0 To blockq.Count - 1
                blockq(i).RemoveAll()
            Next i
        Catch ex As Exception
        End Try

        For Each item As XmlNode In doc.SelectNodes("//link")
            Try
                refCitation = item.Attributes("href").Value
                If refCitation.Contains("showcase") Then
                    If getcitation(refCitation) <> "" Then
                        listref.Add(getcitation(refCitation))
                    End If
                End If
            Catch ex As Exception
                File.AppendAllText("linkserrors.txt", citation & "\t" & ex.Message & "\n")
                Continue For
            End Try
        Next

        For Each item As XmlNode In doc.SelectNodes("//link")
            Try
                refCitation = item.Attributes("href").Value
                If refCitation.Contains("legis") Then
                    If getlegcitation(refCitation) <> "" Then
                        listrefleg.Add(getlegcitation(refCitation))
                    End If
                End If
            Catch ex As Exception
                File.AppendAllText("linkserrors.txt", citation & "\t" & ex.Message & "\n")
                Continue For
            End Try
        Next

        Dim refbq As List(Of String) = listrefbq.Distinct().ToList()
        Dim refcases As List(Of String) = listref.Distinct().ToList()

        Dim listDeleted As List(Of String) = refbq.Except(refcases).ToList()
        Dim realrefcases As List(Of String) = refcases.Except(listDeleted).ToList()

        Dim refbqleg As List(Of String) = listrefbqleg.Distinct().ToList()
        Dim refcasesleg As List(Of String) = listrefleg.Distinct().ToList()

        'Dim listDeletedleg As List(Of String) = refbqleg.Except(refcasesleg).ToList()
        'Dim realrefleg As List(Of String) = refcasesleg.Except(listDeletedleg).ToList()

        Dim listDeletedleg As List(Of String) = refbqleg.Except(refcasesleg).ToList()
        Dim realrefleg As List(Of String) = refcasesleg.ToList()

        Dim citRef As New indentRef(citation, listDeleted, listDeletedleg)
        Return citRef

    End Function
End Class
