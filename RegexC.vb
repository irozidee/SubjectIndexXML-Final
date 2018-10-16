Imports MySql.Data
Imports MySql.Data.Entity
Imports MySql.Data.MySqlClient
Imports MySql.Data.MySqlClient.MySqlHelper
Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions
Public Class RegexC
    Public Shared pattern_regex_cases As String = "regex\\regex_pattern_referred_cases.txt"
    Public Shared pattern_regex_legislation As String = "regex\\regex_pattern_referred_legislations.txt"
    Public Class Legislation
        Public Shared ptnOrder1 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4}))(\([0-9]{1,4}\))(\([a-z]{1,4}\)))"  '  ex : O 53 r 3(2)(b)
        Public Shared ptnOrder2 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4}))(\([0-9]{1,4}\)))"  '  ex : O 53 r 3(2)
        Public Shared ptnOrder3 As String = "(O\s([0-9]{1,4})\s(r?r\s([0-9]{1,4})))" '  ex : O 53 r 3
        Public Shared ptnOrder4 As String = "(O\s([0-9]{1,4}))" '  ex : O 53
        Public Shared ptnOrder As String = ptnOrder1 & "|" & ptnOrder2 & "|" & ptnOrder3 & "|" & ptnOrder4

        Public Shared ptnMainStringNoChar As String = "^([A-Z|a-z]{1,10})\s([0-9]{1,4})([A-Z]{1,2})" ' ex : ss 123A
        Public Shared ptnMainStringNo As String = "^([A-Z|a-z]{1,10})\s([0-9]{1,4})" ' ex : ss 123
        Public Shared ptnMainString As String = ptnMainStringNoChar & "|" & ptnMainStringNo

        Public Shared ptnMainNoChar As String = "^([0-9]{1,4})([A-Z]{1,2})"
        Public Shared ptnMainNo As String = "^([0-9]{1,4})"
        Public Shared ptnMain As String = ptnMainNoChar & "|" & ptnMainNo

        Public Shared ptnBracketDigitChar As String = "(\([0-9]{1,5})(\([A-Z|a-z]{1,4}\))"
        Public Shared ptnBracketDigit As String = "(\([0-9]{1,5}\))" '==>(123)
        Public Shared ptnBracketChar As String = "(\([a-z]{1,4}\))" '==>(abc)
        Public Shared ptnBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))" '==>(iv)(IV)
        Public Shared ptnBracket As String = ptnBracketDigitChar & "|" & ptnBracketDigit & "|" & ptnBracketChar & "|" & ptnBracketRoman

        Public Shared Function Type(ByVal strValue As String) As String
            Dim result As String = ""

            Return result
        End Function

        Public Shared Function isOrderType(ByVal strInput As String) As Boolean
            Dim r As New Regex(ptnOrder)
            Dim m As Match = r.Match(strInput)
            If m.Success Then
                Return True
            Else : Return False
            End If
        End Function

        Public Shared Function isMainType(ByVal strInput As String) As Boolean
            Dim r As New Regex(ptnMain)
            Dim m As Match = r.Match(strInput)
            If m.Success Then
                Return True
            Else : Return False
            End If
        End Function

        Public Shared Function isBracketType(ByVal strInput As String) As Boolean
            Dim r As New Regex(ptnBracket)
            Dim m As Match = r.Match(strInput)
            If m.Success Then
                Return True
            Else : Return False
            End If
        End Function
        Public Shared Function StartWithOrder(ByVal strInput As String) As Boolean
            Try
                If strInput.Trim.StartsWith("O ") = True Or strInput.Trim.StartsWith("o ") = True Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Shared Function StartWithStringOrNumber(ByVal strInput As String) As Boolean
            Dim pattern As String = ptnMainString & "|" & ptnMainNo
            Dim r As Regex
            Dim m As Match
            Try
                r = New Regex(pattern)
                m = r.Match(strInput)
                If m.Success() = True Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function StartWithBracket(ByVal strInput As String) As Boolean
            Try
                If strInput.Trim.StartsWith("(") = True Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function RegexPattern_Bracket_Integer(ByVal str As String) As String
            Dim intResult As String

            Dim ptnBracketDigitChar As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '==ex : (1A)
            Dim ptnBracketDigit As String = "(\([0-9]{1,4}\))" '===ex : (1)
            Dim ptnBracketChar As String = "(\([a-z]{1,4}\))" '===ex : (a)
            Dim ptnBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))" '===ex : (iii)

            Dim m1 As Match
            Dim m2 As Match
            Dim m3 As Match
            Dim m4 As Match

            Dim r1 As Regex
            Dim r2 As Regex
            Dim r3 As Regex
            Dim r4 As Regex

            r1 = New Regex(ptnBracketRoman)
            m1 = r1.Match(str)
            If m1.Success() Then
                intResult = 3 & "#" & ptnBracketRoman
                Return intResult
            End If

            r2 = New Regex(ptnBracketChar)
            m2 = r2.Match(str)
            If m2.Success() Then
                intResult = 2 & "#" & ptnBracketChar
                Return intResult
            End If

            r3 = New Regex(ptnBracketDigit & "|" & ptnBracketDigitChar)
            m3 = r3.Match(str)
            If m3.Success() Then
                intResult = 1 & "#" & ptnBracketDigit & "|" & ptnBracketDigitChar
                Return intResult
            End If

            'Return intResult
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
            Dim ptnBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))" '===ex : (iii)

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

        Public Shared Function Find_Regex_Pattern_Values(ByVal input As String, ByVal pattern As String) As String
            Dim result As String = ""
            Dim r As New Regex(pattern)
            Dim m As Match = r.Match(input)
            If m.Success() Then
                result = m.Value
            End If
            Return result.Trim
        End Function

        Public Shared Function FindRegexPattern_Bracket_To_Array(ByVal str As String) As String()

            Dim currentText As String = str
            Dim arrList(2) As String

            Dim reg0 As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))(\([0-9]{1,10}\))"'==ex : (1A)
            'Dim reg1 As String = "(\([0-9]{1,4}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))"'===ex : (1)
            Dim reg1 As String = "(\([a-z|A-Z]{1,4}\))" '"(\([A-Z|a-z]{1,10}\))" '===ex : (a)
            Dim reg2 As String = "(\([i,x,v,I,X,V]{1,5}\))" '"(\([0-9]{1,10}\))"'===ex : (iii)

            Dim ptnBracketDigitChar As String = "(\([0-9]{1,5}[A-Z]{1,3}\))"
            Dim ptnBracketDigit As String = "(\([0-9]{1,4}\))"
            Dim ptnBracketChar As String = "(\([a-z]{1,4}\))"
            Dim ptnBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))"

            Dim m0 As Match
            Dim m1 As Match
            Dim m2 As Match

            Dim r0 As Regex
            Dim r1 As Regex
            Dim r2 As Regex

            r0 = New Regex(reg0)
            m0 = r0.Match(str)
            If m0.Success() Then
                arrList(0) = m0.Value
                currentText = currentText.Replace(m0.Value, "")
            Else
                reg0 = "(\([0-9]{1,4}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))"'===ex : (1)
                r0 = New Regex(reg0)
                m0 = r0.Match(str)
                If m0.Success() Then
                    arrList(0) = m0.Value
                    currentText = currentText.Replace(m0.Value, "")
                Else
                    arrList(0) = ""
                End If
            End If

            r1 = New Regex(reg1)
            m1 = r1.Match(str)
            If m1.Success() Then
                arrList(1) = m1.Value
                currentText = currentText.Replace(m1.Value, "")
            Else
                arrList(1) = ""
            End If

            r2 = New Regex(reg2)
            m2 = r2.Match(str)
            If m2.Success() Then
                arrList(2) = m2.Value
                currentText = currentText.Replace(m2.Value, "")
            Else
                arrList(2) = ""
            End If

            Return arrList
        End Function

        Public Shared Function Parse_RegexPattern_Bracket_To_Array(ByVal str As String) As String()
            Dim rst As String = ""
            Dim arrValue(3) As String
            Dim reg1 As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))(\([0-9]{1,10}\))"
            Dim reg2 As String = "(\([0-9]{1,4}\))" '"(\([0-9]{1,10}\))(\([A-Z|a-z]{1,10}\))"
            Dim reg3 As String = "(\([a-z]{1,4}\))" '"(\([A-Z|a-z]{1,10}\))"
            Dim reg4 As String = "(\([i,x,v,I,X,V]{1,5}\))" '"(\([0-9]{1,10}\))"

            Dim ptnBracketDigitChar As String = "(\([0-9]{1,5}[A-Z]{1,3}\))" '==ex : (1A)
            Dim ptnBracketDigit As String = "(\([0-9]{1,4}\))" '===ex : (1)
            Dim ptnBracketChar As String = "(\([a-z]{1,4}\))" '===ex : (a)
            Dim ptnBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))" '===ex : (iii)

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
                arrValue(0) = m1.Value
            End If

            r2 = New Regex(reg2)
            m2 = r2.Match(str)
            If m2.Success() Then
                arrValue(1) = m2.Value
            End If

            r3 = New Regex(reg3)
            m3 = r3.Match(str)
            If m3.Success() Then
                arrValue(2) = m3.Value
            End If

            r4 = New Regex(reg4)
            m4 = r4.Match(str)
            If m4.Success() Then
                arrValue(3) = m4.Value
            End If
            Return arrValue
        End Function

        Public Shared Function Parse_ArrayTo_String(ByVal arr As String()) As String
            Dim result As String = ""

            For Each str As String In arr
                result = result & str
            Next

            Return result
        End Function
        Public Shared Function Parse_Legislation_Value(ByVal value1 As String, ByVal value2 As String) As String
            ' === using array
            ' === for example arr(0) = pattern1
            ' === arrValue(0) = pattern1 match value 
            value1 = value1.Trim
            value2 = value2.Trim
            Dim result As String = ""
            Dim pattern As String = ""
            Dim mainText As String = ""

            If StartWithBracket(value2) Then
                pattern = "^[A-Z|a-z]{1,10}\s[0-9]{1,4}[A-Z]{1,2}|^[A-Z|a-z]{1,10}\s[0-9]{1,4}|^[0-9]{1,4}[A-Z]{1,2}|^[0-9]{1,4}"
                mainText = Find_Regex_Pattern_Values(value1, pattern)
                value1 = value1.Replace(mainText, "")
                If value1.Contains("(") And value1.Contains(")") Then
                    Dim list1 As String() = FindRegexPattern_Bracket_To_Array(value1)
                    Dim list2 As String() = FindRegexPattern_Bracket_To_Array(value2)
                    Dim strValue As String = ""
                    Dim l0 As String = list1(0)
                    Dim l1 As String = list1(1)
                    Dim l2 As String = list1(2)

                    If list2(0).Length > 0 Then
                        strValue = Parse_ArrayTo_String(list2)
                        result = strValue
                    Else
                        If l0.Length > 0 Then
                            strValue = l0
                        End If

                        If list2(1).Length > 0 Then
                            strValue = strValue & list2(1)
                        Else
                            If l1.Length > 0 Then
                                strValue = l1
                            End If
                            If list2(2).Length > 0 Then
                                strValue = strValue & list2(2)
                            End If
                        End If
                    End If
                    If mainText <> "" Then
                        result = mainText & strValue
                    Else
                        result = strValue
                    End If
                Else
                    If value1.Length > 0 Then

                        result = value1 & value2
                    Else
                        result = mainText & value2
                    End If
                End If

                Return result
            Else

                result = value2
                Return result
            End If

            Return result
        End Function
        Public Shared Function Parse_Legislation_Order_Value(ByVal value1 As String, ByVal value2 As String) As String
            Dim result As String = ""
            Dim resultOrder As String = ""

            Dim ptnoBracketDigitChar As String = "(\([0-9]{1,5}\))?(\([A-Z|a-z]{1,4}\))?"
            Dim ptnoBracketDigit As String = "(\([0-9]{1,5}\))?" '==>(123)
            Dim ptnoBracketChar As String = "(\([A-Z|a-z]{1,4}\))?" '==>(abc)
            Dim ptnoBracketRoman As String = "(\([i,x,v,I,X,V]{1,5}\))?" '==>(iv)(IV)
            Dim ptnoBracket As String = ptnoBracketDigitChar & "|" & ptnoBracketDigit & "|" & ptnoBracketChar & "|" & ptnoBracketRoman
            ' FOR ORDER AND RULE REPLACEMENT
            ' === O 13A rr 32(1)(b)(iii)
            ' === arrValue(0) = pattern1 match value 
            value1 = value1.Trim
            value2 = value2.Trim

            Dim pattern As String = ""
            Dim mainText As String = ""
            Dim ptnOrder As String = "([O|o])\s([0-9]{1,4})([A-Z]{1,2})?\s" & ptnoBracket '& "|^([O|o])\s([0-9]{1,4})\s" & ptnoBracket '====> O 15A  | O 12

            Dim regexOrder As New Regex(ptnOrder)
            Dim matchOrder As Match = regexOrder.Match(value1)
            Dim StringLeft As String = ""
            Dim SourceRule As String = ""
            If matchOrder.Success() Then
                resultOrder = matchOrder.Value
                StringLeft = value1.Replace(resultOrder, "").Trim

                Dim keyword As String = ""
                If value1.Contains(" rr ") Or value1.Contains(" r ") Then
                    keyword = "r "

                    SourceRule = Parse_Legislation_Value(StringLeft, value2)
                    result = resultOrder & " " & SourceRule
                    result = result.Replace("  ", " r ").Replace("r r", "r")
                    Return result
                Else
                    ' this mean no other keyword except O
                    result = Parse_Legislation_Value(resultOrder, value2)
                    Return result
                End If

            End If

            Return result
        End Function
        Public Shared Function Extract_Legislation_Character_From_String(ByVal str As String) As String
            Dim p As String = "^([A-Z|a-z]{1,10})\s"
            Dim result As String = ""
            Dim r As New Regex(p)
            Dim m As Match = r.Match(str)
            If m.Success() Then
                result = m.Value
            End If
            Return result
        End Function
    End Class

    Public Class Cases
        Public Shared Function Find_Referred_Cases_Type_Regex(ByVal input As String) As String
            Dim result As String
            Dim s As String = ""
            Dim arrKeyword As String() = {"not folld", "distd", "ovrl", "folld", "refd"}
            input = StrConv(input, VbStrConv.Lowercase).Replace("Dirujuk", "refd").Replace("dirujuk", "refd")
            For Each keyword As String In arrKeyword
                Dim pattern As String = "(" & keyword & ")"
                Dim r As New Regex(pattern)
                Dim m As Match = r.Match(input)
                If m.Success() Then
                    If m.Value.Contains("rujuk") Then
                        result = "(refd)"
                        Return result
                    Else
                        result = "(" & m.Value & ")"
                        Return result
                    End If
                    'Exit For
                Else
                    result = "(refd)"
                End If
            Next
            Return result
        End Function
    End Class
End Class
