Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class refCasesC
    Public Property rootCitation() As String
        Get
            Return m_rootCitation
        End Get
        Set(value As String)
            m_rootCitation = Value
        End Set
    End Property
    Private m_rootCitation As String
    Public Property ReferredCitation() As String
        Get
            Return m_ReferredCitation
        End Get
        Set(value As String)
            m_ReferredCitation = value
        End Set
    End Property
    Private m_ReferredCitation As String

    Public Property ReferredTitle() As String
        Get
            Return m_ReferredTitle
        End Get

        Set(value As String)
            m_ReferredTitle = value
        End Set
    End Property

    Private m_ReferredTitle As String
    Public Property type() As String
        Get
            Return m_type
        End Get
        Set(value As String)
            m_type = value
        End Set
    End Property
    Private m_type As String

End Class
