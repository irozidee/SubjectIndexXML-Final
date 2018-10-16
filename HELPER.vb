Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports MySql.Data.MySqlClient.MySqlHelper
Module HELPER
    Public Function EscapeCharacter(ByVal SourceString As String) As String
        Dim returnString As String
        returnString = EscapeString(SourceString)
        Return returnString
    End Function

    Public Function ReplaceString(ByVal str As String, ByVal oldValue As String, ByVal newValue As String, ByVal comparison As StringComparison)
        Dim sb As StringBuilder = New StringBuilder()
        Dim previousIndex = 0
        Dim index As Integer = str.IndexOf(oldValue, comparison)
        While Not (index = -1)
            sb.Append(str.Substring(previousIndex, index - previousIndex))
            sb.Append(newValue)
            index += oldValue.Length
            previousIndex = index
            index = str.IndexOf(oldValue, index, comparison)
        End While

        sb.Append(str.Substring(previousIndex))
        Return sb.ToString

    End Function


End Module
