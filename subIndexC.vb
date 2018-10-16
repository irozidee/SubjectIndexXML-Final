Public Class subIndexC
    Private _mlrcitation As String
    Private m_level1 As String
    Private m_level2 As String
    Public Property mlrcitation() As String
        Get
            Return _mlrcitation
        End Get
        Set(value As String)
            Me._mlrcitation = value.ToString().ToUpper().Replace(".XML", "")
        End Set
    End Property

    Public Property level1() As String
        Get
            Return m_level1
        End Get
        Set(value As String)
            m_level1 = Value
        End Set
    End Property

    Public Property level2() As String
        Get
            Return m_level2
        End Get
        Set(value As String)
            m_level2 = Value
        End Set
    End Property
End Class
