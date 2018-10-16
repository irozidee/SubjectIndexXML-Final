
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Public Class indentRef
    Public Property citation() As String
        Get
            Return m_citation
        End Get
        Set(value As String)
            m_citation = Value
        End Set
    End Property
    Private m_citation As String
    Public Property refCitCases() As List(Of String)
        Get
            Return m_refCitCases
        End Get
        Set(value As List(Of String))
            m_refCitCases = Value
        End Set
    End Property

    Private m_refCitCases As List(Of String)

    Public Property refCitLegis() As List(Of String)

        Get
            Return m_refCitLegis
        End Get

        Set(value As List(Of String))
            m_refCitLegis = value
        End Set
    End Property

    Private m_refCitLegis As List(Of String)

    Public Sub New(_citation As String, caseIndentRefList As List(Of String), legIndentRefList As List(Of String))
        citation = _citation
        refCitCases = caseIndentRefList
        refCitLegis = legIndentRefList
    End Sub

End Class
