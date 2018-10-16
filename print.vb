Public Class PrintingC
    Public Shared Function PageNo_From_Citation(ByVal Citation As String) As String
        Dim strInput As String = Citation.Trim
        Dim arrInput As String() = strInput.Split(" ")
        Dim IndexCount As Integer = arrInput.Length - 1
        Dim PageNo As Integer = arrInput(IndexCount)

        Return PageNo
    End Function

End Class
