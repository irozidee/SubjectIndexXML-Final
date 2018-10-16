Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class refLegC
    Private m_RefferredTitle As String
    Private m_ReferredCitaion As String
    Private m_root_citation As String

    Public Property root_citation() As String
        Get
            Return m_root_citation
        End Get
        Set(value As String)
            m_root_citation = value
        End Set
    End Property

    Public Property ReferredCitaion() As String
        Get
            Return m_ReferredCitaion
        End Get
        Set(value As String)
            m_ReferredCitaion = value
        End Set
    End Property

    Public Property RefferredTitle() As String
        Get
            Return m_RefferredTitle
        End Get
        Set(value As String)
            m_RefferredTitle = value
        End Set
    End Property

End Class