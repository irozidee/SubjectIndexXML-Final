Imports System.Text.RegularExpressions
Imports System.IO
Public Class utility
    Public Class Legislations
        Public Shared Function RemoveUnusedCharacter(ByVal str As String) As String
            Dim result As String
            result = str.Replace(";", "").Replace(".", "").Replace("Order", "").Trim
            Return result
        End Function
    End Class
    Public Shared Function RemoveBracketFromString(ByVal str As String) As String
        Dim result As String
        result = str.Replace("(", "").Replace(")", "")
        Return result
    End Function
    Public Shared Sub ListRemoveDups(ByVal Source As List(Of String), Optional ByVal MatchCase As Boolean = False)
        Source.Sort()
        For x As Integer = Source.Count - 1 To 1 Step -1
            If MatchCase Then
                If Source(x).ToLower() = Source(x - 1).ToLower() Then
                    Source.RemoveAt(x)
                End If
            Else
                If Source(x) = Source(x - 1) Then
                    Source.RemoveAt(x)
                End If
            End If
        Next x
    End Sub
    Public Shared Function CorrectTextFormatSQL(ByVal text As String) As String
        Dim result As String = text
        Try
            result = result.Trim()
            'result = result.Replace("""", "'")
            result = result.Replace("    ", " ").Replace("   ", " ").Replace("  ", " ") '.Replace("'", "\'")
            result = result.Replace("<br/>", "").Replace("amp;amp", "amp;")
            result = result.Replace("<P>", "").Replace("</P>", "").Replace("<B>", "").Replace("</B>", "").Replace("<I>", "").Replace("</I>", "")
            result = result.Replace("()", "")
            result = RemoveTagFromString(result)
        Catch ex As Exception
            result = "NULL TEXT"
        End Try
        Return result
    End Function
    Public Shared Function Spacing_Correction(ByVal str As String) As String
        Dim result As String
        result = str.Replace("     ", " ").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ").Trim
        Return result
    End Function

    Public Shared Function CorrectTextFormat(ByVal text As String) As String
        Dim result As String = text
        Try
            result = result.Replace(" ()", "").Replace("    ", " ").Replace("   ", " ").Replace("  ", " ")
            result = result.Replace("<br/>", "").Replace("amp;amp;", "amp;") '.Replace("\'", "'")
            result = result.Replace("<P>", "").Replace("</P>", "").Replace("<B>", "").Replace("</B>", "").Replace("<I>", "").Replace("</I>", "")
            result = RemoveTagFromString(result)
            result = result.Trim()
        Catch ex As Exception
            'result = "NULL TEXT"
        End Try
        Return result
    End Function

    Public Shared Function PrintCitation(Datafilename As String) As String
        Dim strreturn As String = ""
        Dim pdfcit As String() = Datafilename.Split(New String() {"_"}, StringSplitOptions.None)
        Dim dtlowername As String = StrConv(Datafilename, VbStrConv.Lowercase)
        If dtlowername.Contains("mlra") Or dtlowername.Contains("mlrh") Or dtlowername.Contains("melr") Or dtlowername.Contains("mlrs") Or dtlowername.Contains("sslr") Then
            Try
                strreturn = "[" & pdfcit(1) & "] " & pdfcit(2) & " " & pdfcit(0) & " " & pdfcit(3)
                Return strreturn
            Catch ex As Exception
                Try
                    strreturn = "[" & pdfcit(1) & "] " & pdfcit(0) & " " & pdfcit(2)
                    Return strreturn
                Catch ex2 As Exception
                    strreturn = Datafilename
                End Try
            End Try
        Else
            strreturn = Datafilename
        End If
        
        Return strreturn
    End Function
    Public Shared Function PrintDatafilename(ByVal Citation As String) As String
        Dim sourceText As String = CorrectTextFormat(Citation)
        Dim returnString As String = "CITATION_WRONG"
        Try
            sourceText = sourceText.Replace("[", "").Replace("]", "")
            Dim arrText As String() = sourceText.Split({" "}, StringSplitOptions.None)
            If arrText.Count = 4 Then
                returnString = arrText(2) & "_" & arrText(0) & "_" & arrText(1) & "_" & arrText(3)
            Else
                returnString = arrText(1) & "_" & arrText(0) & "_" & arrText(2)
            End If
        Catch ex As Exception
        End Try
        Return returnString
    End Function

    Public Shared Function CorrectWord_Legislation(ByVal LegislationTitle As String) As String
        Dim result As String = ""
        LegislationTitle = LegislationTitle.Replace("Kaedah-Kaedah Mahkamah 2012", "").Replace("regulation", "reg").Replace("regs", "reg")
        result = LegislationTitle
        Return result
    End Function

    Public Shared Function RemoveTagFromString(ByVal str As String) As String
        Dim result As String = Regex.Replace(str, "</?.*?>", "")
        Return result
    End Function

    Public Shared Function RemoveNumberFromInput(ByVal input As String) As String
        Dim result As String
        Dim regex As New Regex("[0-9\(\)\.]")
        result = regex.Replace(input, "")
        If result.Contains(" ") Then
            result = result.Split(" ")(0)
        End If
        Return result
    End Function
    Public Shared Function RemoveStringFromInput(ByVal input As String) As String
        Dim result As String
        Dim regex As New Regex("[A-Za-z\)\.]")
        result = regex.Replace(input, "").Replace("(", ".")
        Return result
    End Function
    Public Shared Function GetPageNo(ByVal XMLFilePath) As String
        Try
            Dim bsfn As String = System.IO.Path.GetFileNameWithoutExtension(XMLFilePath)
            Return bsfn.Substring(bsfn.LastIndexOf("_") + 1)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function REFERRED_LEGISLATION_TYPE(ByVal XMLFILEPATH As String) As Boolean
        Dim str As String = File.ReadAllText(XMLFILEPATH)
        If str.Contains("REFERRED_LEGISLATIONS") Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function REFERRED_CASES_TYPE(ByVal XMLFILEPATH As String) As Boolean
        Dim str As String = File.ReadAllText(XMLFILEPATH)
        If str.Contains("REFERRED_CASES") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Extract_String(ByVal str As String, ByVal symbol As String, ByVal indexNo As Integer)
        Dim result As String = ""
        result = str.Split({symbol}, StringSplitOptions.None)(indexNo)
        Return result
    End Function


End Class
