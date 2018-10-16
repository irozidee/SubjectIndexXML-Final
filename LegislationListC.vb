Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Public Class LegislationListC

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

    Public Property legislationtype() As String
        Get
            Return m_legislationtype
        End Get

        Set(value As String)
            m_legislationtype = value
        End Set
    End Property

    Private m_legislationtype As String
End Class
