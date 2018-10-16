Imports System.Text.RegularExpressions
Imports System.IO
Public Class Extractor
    Public Shared Function StringRemoveTags(ByVal input As String) As String
        Dim r As New Regex("<.*?>")
        Dim m As Match = r.Match(input)
        Dim result As String = input
        If m.Success() Then
            result = r.Replace(input, "")
        End If
        Return result.Trim
    End Function
    Public Shared Function FindCitations(ByVal StringToSearch As String) 'As String
        Dim pattern As String = ""
        Dim foundCitation As String = ""
        Dim i As Integer = 1
        Dim status As Boolean = False
        Do While status = False
            Select Case i
                Case Is = 1
                    pattern = "(\([0-9]{4,4}\))\s([0-9]{1,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" '[2010] 1 MLRA 999 
                Case Is = 2
                    pattern = "(\[[0-9]{4,4}\])\s([A-Z|a-z]{2,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" '[2010] AC AC 999 
                Case Is = 3
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" '[2010] 1 MLRAU 999 
                Case Is = 4
                    pattern = "(\[[0-9]{4,4}\])\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" '[2010] MLRAU 999 
                Case Is = 5
                    pattern = "([0-9]{1,3})\s([A-Z|a-z]{2,6})\s([0-9]{1,5})" ' 2000 XXXXXX 11111
                Case Is = 6
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,4})\s([A-Z|a-z]{2,4})\s([0-9]{1,5})" ' [9999] 999 XXXX XXXX 99999
                Case Is = 7
                    pattern = "(\[[0-9]{4,4}\])\s([0-9]{1,3})\s([A-Z|a-z]{2,6})\s\([\w+]{1,10}\)\s([0-9]{1,5})"
                Case Else
                    status = True
                    foundCitation = "ERROR FOR - " & StringToSearch
                    Return foundCitation
            End Select
            Dim m As Match
            Dim r As Regex
            r = New Regex(pattern)
            m = r.Match(StringToSearch)

            If m.Success() Then
                foundCitation = m.Value
                status = True
            Else
                i = i + 1
            End If
        Loop
        Return foundCitation
    End Function
    Public Shared Function Contains_Prohibit_Citation(ByVal input As String) As Boolean
        Return input.Contains("MLJ") Or input.Contains("CLJ") Or input.Contains("AMR") Or input.Contains("LNS")
    End Function
    Public Shared Function FindCitation(ByVal StringToSearch As String) As String 'List(Of String)
        Dim resultList As List(Of String) = New List(Of String)
        Dim result As String = ""
        Dim foundCitation As String = ""
        Dim i As Integer = 1
        Dim status As Boolean = False
        Dim patternList As List(Of String) = File.ReadLines(RegexC.pattern_regex_cases).ToList
        Dim matches As MatchCollection
        Dim r As Regex
        For Each pattern As String In patternList
            r = New Regex(pattern)
            matches = r.Matches(StringToSearch)
            For Each match As Match In matches
                If Contains_Prohibit_Citation(match.Value) = False Then
                    If result = "" Then
                        result = match.Value
                    Else
                        result = result & "#" & match.Value
                    End If
                End If
                'resultList.Add(match.Value)
                StringToSearch = StringToSearch.Replace(match.Value.ToString, "")
            Next
        Next
        If result.EndsWith("#") Then
            result = result.Remove(result.Length - 1, 1)
        End If
        Return result
    End Function

    Public Class ReferredCases
        Public Shared Function FindReferredCasesType(ByVal StringToSearch As String) As String
            StringToSearch = StrConv(StringToSearch, VbStrConv.Lowercase)
            If StringToSearch.Contains("refd") Then
                Return "refd"
            ElseIf StringToSearch.Contains("folld") Then
                Return "folld"
            ElseIf StringToSearch.Contains("foll") Then
                Return "foll"
            ElseIf StringToSearch.Contains("distd") Then
                Return "distd"
            ElseIf StringToSearch.Contains("dist") Then
                Return "dist"
            ElseIf StringToSearch.Contains("ovrld") Then
                Return "ovrld"
            ElseIf StringToSearch.Contains("ovrl") Then
                Return "ovrl"
            ElseIf StringToSearch.Contains("not folld") Then
                Return "not folld"
            ElseIf StringToSearch.Contains("not foll") Then
                Return "not foll"
            ElseIf StringToSearch.Contains("dirujuk") Then
                Return "dirujuk"
            Else
                Return "refd"
            End If
        End Function
        Public Shared Function FindReferredCasesTitle(ByVal StringToSearch As String) As String
            Dim foundCitation As String = FindCitation(StringToSearch)
            Dim result As String = StringToSearch.Replace(foundCitation, "")
            Return result.Trim.Replace("<i>", "").Replace("</i>", "").Replace("amp;amp;", "amp;")
        End Function
    End Class
    Public Class ReferredLegislations
        Public Shared Function Legislation_Keyword_Found(ByVal str As String) As Boolean
            Dim result As Boolean = False
            Dim lowerSourceText = StrConv(str, VbStrConv.Lowercase).Trim
            If lowerSourceText.StartsWith("o ") Or lowerSourceText.StartsWith("s ") Or lowerSourceText.StartsWith("ss ") Or lowerSourceText.StartsWith("r ") Or lowerSourceText.StartsWith("rr ") Or lowerSourceText.StartsWith("art ") Or lowerSourceText.StartsWith("arts ") Or lowerSourceText.StartsWith("go ") Or lowerSourceText.StartsWith("reg ") Or lowerSourceText.StartsWith("regs ") Or lowerSourceText.StartsWith("title ") Then
                result = True
            Else
                If lowerSourceText.Contains("item") Or lowerSourceText.Contains("list") Or lowerSourceText.Contains("schedule") Or lowerSourceText.Contains("para") Or lowerSourceText.Contains("sch ") Then
                    result = True
                Else
                    result = False
                End If
            End If
            Return result
        End Function
        Public Shared Function Extract_Legislation_Title_Index_From_Array(ByVal list As String()) As Integer

            Dim i As Integer = 0
            Try
                For Each Str As String In list
                    If Legislation_Keyword_Found(Str) = True Then
                        Return i
                    End If
                    i = i + 1
                Next
            Catch ex As Exception
                MsgBox("Error Function Extract_Legislation_Title_Index_From_Array")
            End Try

        End Function
        Public Shared Function Extract_Legislation_Title_From_Array(ByVal titleIndex As Integer, ByVal list As String()) As String
            Dim result As String = ""
            If titleIndex = 0 Then
                result = list(0)
            Else
                For i As Integer = 0 To titleIndex
                    result = result & "," & list(i)
                Next
            End If

            If result.EndsWith(",") Then
                result = result.Remove(result.Length - 1, 1)
            End If
            Return result
        End Function
        Public Shared Function In_Legislation_Sub_Format(ByVal sourceText As String) As Boolean
            Dim result As Boolean = False
            Dim lowerSourceText = StrConv(sourceText, VbStrConv.Lowercase)
            If lowerSourceText.Contains("item") Or lowerSourceText.Contains("list") Or lowerSourceText.Contains("schedule") Or lowerSourceText.Contains("para") Or lowerSourceText.Contains("sch ") Then
                result = True
            Else
                If lowerSourceText.StartsWith("o ") Or lowerSourceText.StartsWith("s ") Or lowerSourceText.StartsWith("ss ") Or lowerSourceText.StartsWith("r ") Or lowerSourceText.StartsWith("rr ") Or lowerSourceText.StartsWith("art ") Or lowerSourceText.StartsWith("arts ") Or lowerSourceText.StartsWith("go ") Or lowerSourceText.StartsWith("reg ") Or lowerSourceText.StartsWith("regs ") Then
                    result = True
                End If
            End If
            Return result
        End Function

        Public Shared Sub Extract_Legislation_Sections_ToList(ByVal sourceText As String) ' As List(Of String)
            Dim itemList As List(Of String) = New List(Of String)
            Dim arrList As String()
            arrList = sourceText.Split({","}, StringSplitOptions.None)
            Dim actTitle As String = ""
            Dim counter As Integer = 0
            Do While Not counter = arrList.Length
                Dim tmpActTitle As String = ""
                tmpActTitle = arrList(0)
                Do While In_Legislation_Sub_Format(arrList(counter)) = False
                    tmpActTitle = tmpActTitle & ", " & arrList(counter)
                Loop
                itemList.Add(tmpActTitle)


                'If counter = 1 Then
                '    If In_Legislation_Sub_Format(arrList(counter)) = False Then
                '        tmpActTitle = tmpActTitle & ", " & arrList(counter)
                '    End If
                'End If
                'If counter = 2 Then
                '    If In_Legislation_Sub_Format(arrList(counter)) = False Then
                '        tmpActTitle = tmpActTitle & ", " & arrList(counter)
                '    End If
                'End If
                'If counter = 3 Then
                '    If In_Legislation_Sub_Format(arrList(counter)) = False Then
                '        tmpActTitle = tmpActTitle & ", " & arrList(counter)
                '    End If
                'End If


                'If counter < 3 Then
                '    If In_Legislation_Sub_Format(arrList(counter)) = False Then
                '        tmpActTitle = tmpActTitle & " " & arrList(counter)
                '    Else
                '        itemList.Add(arrList(counter))
                '    End If
                'Else
                '    itemList.Add(arrList(counter))
                'End If

                'counter = counter + 1
            Loop

            'Return itemList

            Dim list As List(Of String) = New List(Of String)

            Dim ptnOrder1 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4}))(\([0-9]{1,4}\))(\([a-z]{1,4}\)))"  '  ex : O 53 r 3(2)(b)
            Dim ptnOrder2 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4}))(\([0-9]{1,4}\)))"  '  ex : O 53 r 3(2)
            Dim ptnOrder3 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4})))" '  ex : O 53 r 3
            Dim ptnOrder4 As String = "(O\s([0-9]{1,4}))" '  ex : O 53
            Dim patternOrder As String = ptnOrder1 & "|" & ptnOrder2 & "|" & ptnOrder3 & "|" & ptnOrder4


            Dim ptnSub1 As String = ""

            ' fdasfdas, (1)(a),(),()

            Dim ptnBracketDigitChar As String = "(\([0-9]{1,5}\))|(\([0-9]{1,5}[A-Z]{1,3}\))" ' ex : (1A)
            Dim ptnBracketDigit As String = "(\([0-9]{1,4}\))" ' ex : (1)
            Dim ptnBracketChar As String = "(\([a-z]{1,4}\))" ' ex : (a)
            Dim ptnBracketRoman As String = "(\([a-z]{1,5}\))" ' ex : (iii)

            Dim secNumber As String = ""
            Dim tmpSecNumber As String = ""
            Dim regex As Regex

            If sourceText.Contains("O ") Then 'START WITH O follow by number and next....
                regex = New Regex(patternOrder)
                For Each m As Match In regex.Matches(sourceText)
                    list.Add(m.Value)
                Next
            Else

            End If ' END IF FOR SOURCETEXT.CONTAINS O



            'If sourceText.Contains(",") Then


            'Else
            '    Return list
            'End If


        End Sub
        Public Shared Function Extract_Legislation_Sub_No_Type(ByVal s As String) As String
            Dim result As String
            s = LTrim(StrConv(s, VbStrConv.Lowercase))
            If s.StartsWith("o ") And (s.Contains("r ") Or s.Contains("rr")) Then
                result = s
            Else
                If s.StartsWith("ss ") Or s.StartsWith("s ") Or s.StartsWith("section") Then
                    result = "s"

                ElseIf s.StartsWith("reg ") Or s.StartsWith("regs ") Or s.StartsWith("reg") Then
                    result = "reg"

                ElseIf s.StartsWith("art ") Or s.StartsWith("arts ") Or s.StartsWith("article") Then
                    result = "art"

                ElseIf s.StartsWith("rr ") Or s.StartsWith("r ") Or s.StartsWith("rule") Then
                    result = "r"

                ElseIf s.StartsWith("item ") Or s.StartsWith("items ") Then
                    result = "item"

                Else
                    result = s
                End If
            End If
            Return Replace_Legislation_Sub_No_Type(result)
        End Function

        Public Shared Function Replace_Legislation_Sub_No_Type(ByVal s As String) As String
            Dim result As String
            result = s.Replace("ss", "s").Replace("regs", "reg").Replace("arts", "art").Replace("rr", "r") _
                .Replace("items", "item").Replace("Items", "item")
            Return result
        End Function
    End Class
    Public Shared Function FindRegexPattern_Main(ByVal str As String) As String
        Dim resultPattern As String = ""

        Dim ptn1 As String = "^([0-9]{1,5})([A-Z]{1,2})"
        Dim ptn2 As String = "^([0-9]{1,5})"

        Dim ptnNo1 As String = "^([0-9]{1,5}[A-Z]{1,2})" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))(\([0-9]{1,10}\))"
        Dim ptnNo2 As String = "^([0-9]{1,5})" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))(\([0-9]{1,10}\))"

        Dim reg1 As Regex
        Dim reg2 As Regex

        Dim m1 As Match
        Dim m2 As Match

        reg1 = New Regex(ptn1)
        m1 = reg1.Match(str)

        If m1.Success() Then
            resultPattern = ptn1
            Return resultPattern
        Else
            reg2 = New Regex(ptn2)
            m2 = reg2.Match(str)
            If m2.Success() Then
                resultPattern = ptn2
                Return resultPattern
            End If
        End If
        Return resultPattern
    End Function

    Public Shared Function FindRegexPattern_Bracket(ByVal str As String) As String
        Dim rst As String = ""

        Dim reg1 As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))(\([0-9]{1,10}\))"
        Dim reg2 As String = "(\([0-9]{1,4}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))"
        Dim reg3 As String = "(\([a-z]{1,4}\))" '"(\([A-Z|a-z]{1,10}\))"
        Dim reg4 As String = "(\([i,x,v,I,X,V]{1,5}\))" '"(\([0-9]{1,10}\))"

        Dim ptnBracketDigitChar As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '==ex : (1A)
        Dim ptnBracketDigit As String = "(\([0-9]{1,4}\))" '===ex : (1)
        Dim ptnBracketChar As String = "(\([a-z]{1,4}\))" '===ex : (a)
        Dim ptnBracketRoman As String = "(\([a-z]{1,5}\))" '===ex : (iii)

        Dim m1 As Match
        Dim m2 As Match
        Dim m3 As Match
        Dim m4 As Match

        Dim r1 As Regex
        Dim r2 As Regex
        Dim r3 As Regex
        Dim r4 As Regex

        r1 = New Regex(reg1)
        m1 = r1.Match(str)
        If m1.Success() Then
            If IsNothing(rst) Then
                rst = reg1
            Else
                rst = rst & "|" & reg1
            End If
        End If

        r2 = New Regex(reg2)
        m2 = r2.Match(str)
        If m2.Success() Then
            If IsNothing(rst) Then
                rst = reg2
            Else
                rst = rst & "|" & reg2
            End If
        End If

        r3 = New Regex(reg3)
        m3 = r3.Match(str)
        If m3.Success() Then
            If IsNothing(rst) Then
                rst = reg3
            Else
                rst = rst & "|" & reg3
            End If
        End If

        r4 = New Regex(reg4)
        m4 = r4.Match(str)
        If m4.Success() Then
            If IsNothing(rst) Then
                rst = reg4
            Else
                rst = rst & "|" & reg4
            End If
        End If
        Return rst
    End Function

    Public Shared Function Legislation_Regex_Fix(ByVal primaryText As String, ByVal secondaryText As String) As String
        Dim result As String = ""
        Dim foundPattern As String
        Dim tempString1 As String
        Dim tempString2 As String
        Dim r As Regex
        Dim m As Match
        Try
            If primaryText = secondaryText Then
                Return secondaryText
            Else
                If IsNumeric(secondaryText(0)) = True Then
                    Return secondaryText
                Else
                    If secondaryText.StartsWith("(") Then
                        foundPattern = FindRegexPattern_Bracket(secondaryText)
                        r = New Regex(foundPattern)
                        m = r.Match(primaryText)
                        If m.Success Then
                            tempString2 = primaryText.Replace(m.Value, secondaryText)
                        End If
                        'Dim multiMatch As MatchCollection
                        'multiMatch = r.Matches(primaryText)
                        'For Each match As Match In multiMatch
                        '    tempString2 = Regex.Replace(primaryText, foundPattern, secondaryText)
                        'Next
                        result = tempString2
                        Return result
                    End If



                    '        foundPattern = FindRegexPattern_Main(secondaryText)
                    '        r = New Regex(foundPattern)
                    '        m = r.Match(primaryText)

                    '        If m.Success Then
                    '            tempString1 = Regex.Replace(primaryText, foundPattern, secondaryText)
                    '            result = tempString1 & secondaryText
                    '        End If
                    '    Else

                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Function

    Public Shared Function FindRegexPatternList(ByVal inputText As String) As List(Of String)
        Dim currentText As String = inputText
        Dim PatternList As List(Of String) = New List(Of String)
        Dim result As String = ""

        Dim MainNoChar As String = "^([0-9]{1,4})([A-Z]{1,2})"
        Dim MainNo As String = "^([0-9]{1,4})"
        'Dim MainChar As String = "([A-Z]{1,2})"

        Dim SubDigitChar As String = "(\([0-9]{1,5})(\([A-Z|a-z]{1,4}\))"
        'Dim SubDigitCharUpper As String = "(\([0-9]{1,5})(\([A-Z|a-z]{1,4}\))"
        'Dim SubDigitCharLower As String = "(\([0-9]{1,5})(\([a-z]{1,4}\))"
        Dim SubDigit As String = "(\([0-9]{1,5}\))" '==>(123)
        Dim SubChar As String = "(\([a-z]{1,4}\))" '==>(abc)
        Dim SubRoman As String = "(\([a-z]{1,5}\))" '==>(iv)(IV)

        Dim r As Regex
        Dim m As Match

        Dim arrList As String() = {MainNoChar, MainNo, SubDigitChar, SubDigit, SubChar, SubRoman}

        For Each sPattern As String In arrList

            r = New Regex(sPattern)
            m = r.Match(currentText)
            If m.Success Then
                '  MsgBox(currentText & Environment.NewLine & sPattern)
                currentText = Regex.Replace(currentText, sPattern, "")
                PatternList.Add(sPattern)
            End If
        Next
        Return PatternList
        '====================================================
    End Function
    Public Shared Function Legislation_Regex_ValueAdded(ByVal primaryText As String, ByVal secondaryText As String) As String
        Dim result As String
        Dim currentText As String = primaryText
        Dim list As List(Of String) = New List(Of String)
        If IsNumeric(secondaryText(0)) Then
            result = secondaryText
        Else
            list = Extractor.FindRegexPatternList(secondaryText)
            For Each ptn As String In list
                currentText = Regex.Replace(primaryText, ptn, "")
            Next
            result = currentText
        End If
        Return result
    End Function

End Class
