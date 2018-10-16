Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class wordChanger
    Private m_wordOld As String
    Private m_wordNew As String

    Public Property wordOld() As String
        Get
            Return m_wordOld
        End Get
        Set(value As String)
            m_wordOld = value
        End Set
    End Property

    Public Property wordNew() As String
        Get
            Return m_wordNew
        End Get
        Set(value As String)
            m_wordNew = value
        End Set
    End Property

End Class
