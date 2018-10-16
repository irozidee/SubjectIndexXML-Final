Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class CasesListC
    Public Property datafilename() As String
        Get
            Return m_datafilename
        End Get
        Set(value As String)
            m_datafilename = value
        End Set
    End Property
    Private m_datafilename As String
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(value As String)
            m_title = value
        End Set
    End Property
    Private m_title As String

    Public Property citation() As String
        Get
            Return m_citation
        End Get

        Set(value As String)
            m_citation = value
        End Set
    End Property

    Private m_citation As String
End Class
