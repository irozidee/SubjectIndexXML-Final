Imports System.Text.RegularExpressions

Public Class LegisExtractTest

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim resultList As New List(Of String)
        Dim input As String = TextBox1.Text
        Dim arrInput As String() = input.Split(", ")

        For Each strInput In arrInput
            Dim pattern As String = Extractor.FindRegexPattern_Bracket(strInput)
            Dim r As New Regex(pattern)
        Next

        Try

        Catch ex As Exception

        End Try

    End Sub
End Class