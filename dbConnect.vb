Imports MySql.Data
Imports MySql.Data.Entity
Imports MySql.Data.MySqlClient
Imports MySql.Data.MySqlClient.MySqlHelper
Imports System.IO
Imports System.Xml
Imports System.Text.RegularExpressions
Public Class DBConnect
    Private connection As MySqlConnection
    Private server As String
    Private database As String
    Private uid As String
    Private password As String

    Public Shared separator As String = "     " ' separator = 5x space
    Private newLine As String = Environment.NewLine
    '============================== Constructor
#Region "DATABASE UTILITY & FUNCTION"
    Public Sub New()
        Initialize()
    End Sub
    Private Sub Initialize()
        server = "localhost"
        database = "consolidate_index_db"
        uid = "root"
        password = "root123"
        Dim connectionString As String
        connectionString = "SERVER=" & server & ";" & "DATABASE=" & database & ";" & "UID=" & uid & ";" & "PASSWORD=" & password & ";" & "Connect Timeout = 60000 ;"
        connection = New MySqlConnection(My.Settings.dsn_data)
    End Sub
    Private Function OpenConnection() As Boolean
        Try
            If connection.State = 1 Then
                connection.Close()
            Else
                connection.Open()
                Return True
            End If

        Catch ex As MySqlException
            'When handling errors, you can your application's response based 
            'on the error number.
            'The two most common error numbers when connecting are as follows:
            '0: Cannot connect to server.
            '1045: Invalid user name and/or password.
            Select Case ex.Number
                Case 0
                    MessageBox.Show("Cannot connect to server.  Contact Administrator.")
                    Return False
                    Exit Select
                Case 1045
                    MessageBox.Show("Invalid username/password, please try again")
                    Return False
                    Exit Select
                Case Else
                    Return False
            End Select
            Return False
        End Try
    End Function
    'Close connection
    Private Function CloseConnection() As Boolean
        Try
            connection.Close()
            Return True
        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function

    'Close connection

    Public Sub RunQuery(query As String)
        'string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
        'open connection

        If Me.OpenConnection() = True Then
            'create command and assign the query and connection from the constructor
            Try
                Dim cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(log.Error_SQL, query & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
            End Try
            Me.CloseConnection()
        End If
    End Sub
    ' ==== Insert statement
    Public Sub Insert(query As String)
        'string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
        'open connection

        If Me.OpenConnection() = True Then
            'create command and assign the query and connection from the constructor
            Try
                Dim cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(log.Error_SQL, query & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
            End Try
            Me.CloseConnection()
        End If
    End Sub
    Public Sub InsertSelect(query As String)
        'string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";
        'open connection

        If Me.OpenConnection() = True Then
            'create command and assign the query and connection from the constructor
            Try
                Dim cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                My.Computer.FileSystem.WriteAllText(log.Error_SQL, query & Environment.NewLine & ex.Message.ToString & Environment.NewLine & Environment.NewLine, True)
            End Try
            Me.CloseConnection()
        End If
    End Sub
    Public Sub DeleteDatabaseTable(ByVal tableName As String)
        Dim query As String = ""
        query = "delete from " & tableName
        If Me.OpenConnection = True Then
            Dim cmd As New MySqlCommand(query, connection)
            cmd.ExecuteNonQuery()
        End If
        Me.CloseConnection()
    End Sub
    Public Sub RefillDatabaseTable(ByVal tableName As String, ByVal sqlString As String)

        Dim query As String = ""
        query = "insert into " & tableName & " " & sqlString
        If Me.OpenConnection = True Then
            Dim cmd As New MySqlCommand(query, connection)
            cmd.ExecuteNonQuery()
        End If
        Me.CloseConnection()
    End Sub
    Public Sub Delete(tbname As String)
        Dim query As String = "DELETE FROM " & tbname
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            cmd.ExecuteNonQuery()
            Me.CloseConnection()
        End If
    End Sub
    Public Sub DeleteAndClearIndex(tbname As String)
        Call Delete(tbname)
        'Dim query As String = "DELETE FROM " & tbname
        'If Me.OpenConnection() = True Then
        '    Dim cmd As New MySqlCommand(query, connection)
        '    cmd.ExecuteNonQuery()
        '    Me.CloseConnection()
        'End If

        Dim queryReset As String = "ALTER TABLE " & tbname & " AUTO_INCREMENT=0;"
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(queryReset, connection)
            cmd.ExecuteNonQuery()
            Me.CloseConnection()
        End If
    End Sub
    Public Function CountTableRow(ByVal TableName As String) As Integer
        Dim query As String = "SELECT COUNT(*) FROM " & TableName
        Dim TotalRow As Integer = 0
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                TotalRow = dataReader(0).ToString
            End While
            'close Data Reader
            dataReader.Close()
            'close Connection
            Me.CloseConnection()
            'return list to be displayed
            Return TotalRow
        Else
            Return TotalRow
        End If
    End Function
#End Region

#Region "REPORT DATA"
    Public Sub Report_Subject_Index()
        Dim query As String = "SELECT * FROM xml_cases, subject_index  where xml_cases.datafilename = subject_index.source_datafilename"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("level1").ToString()))
            End While
            dataReader.Close()
            Me.CloseConnection()
        Else

        End If
    End Sub
    Public Sub ReportData_Cases()
        Application.DoEvents()
        Dim query As String = "insert into v_cases(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, referred_citation, referred_title, referred_type) " & _
        "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, referred_citation, referred_title, referred_type " & _
        "from xml_cases, ref_cases where citation=source_citation "
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            dataReader.Close()
            Me.CloseConnection()
        End If
    End Sub
    Public Sub ReportData_Legislations()
        Dim query As String = "insert into v_legislations(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, legis_title, legis_filename, legis_sub_no) " & _
        "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, legis_title, legis_filename, legis_sub_no " & _
        "from xml_cases, ref_legislations where citation=source_citation "
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            Dim counterData As Integer = 1
            While dataReader.Read()
                list.Add(counterData)
                counterData = counterData + 1
            End While
            dataReader.Close()
            Me.CloseConnection()
        End If
    End Sub
    Public Sub ReportData_Subject_Index()
        Application.DoEvents()
        Dim query As String = "insert into v_subject_index(datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, subject_index, level1, level2, summary) " & _
        "select datafilename, citation, judgment_name, judgment_language, judge_name, court_type, firstletter, subject_index, level1, level2, summary " & _
        "from xml_cases, subject_index where citation=source_citation "
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            dataReader.Close()
            Me.CloseConnection()
        End If
    End Sub
#End Region

#Region "SUBJECT INDEX"
    Public Function SubjectIndex_Level2(ByVal level1 As String) As List(Of String)
        Dim query As String = "SELECT level2  FROM v_subject_index where level1 = '" & EscapeString(level1) & "' group by level2 order by level2"
        'Create a list to store the resul
        Dim list As List(Of String) = New List(Of String)
        'list(0) = New List(Of String)(0)
        'list(1) = New List(Of String)
        'Open connection
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("level2").ToString))
            End While
            'close Data Reader
            dataReader.Close()
            'close Connection
            Me.CloseConnection()
            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function
    Public Function SubjectIndex_Summary(ByVal level1 As String, ByVal level2 As String) As List(Of String)
        Dim query As String = "SELECT summary FROM v_subject_index where level1 = '" & EscapeString(level1) & "' and level2='" & EscapeString(level2) & "' group by summary order by summary "
        Dim returnData As List(Of String) = New List(Of String)

        If Me.OpenConnection() = True Then
            Try
                Dim cmd As New MySqlCommand(query, connection)
                Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
                While dataReader.Read()
                    'Dim strSummary As String = dataReader("summary").ToString '.Replace("/", " ")
                    returnData.Add(utility.CorrectTextFormat(dataReader("summary").ToString)) '.Remove(0, strSummary.IndexOf("-") + 1))
                End While
                dataReader.Close()
                Me.CloseConnection()
            Catch ex As Exception

            End Try
            Return returnData
        Else
            Return returnData
        End If
    End Function
    Public Function SubjectIndexDataColl(ByVal level1 As String, ByVal level2 As String, ByVal summary As String) As List(Of String)
        Dim query As String = "SELECT * FROM v_subject_index where level1 = '" & EscapeString(level1) & "' and level2 = '" & EscapeString(level2) & "' and summary like '%" & EscapeString(summary) & "%' group by citation"
        Dim returnData As New List(Of String)
        'Open connection

        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                returnData.Add(dataReader("judgment_name").ToString & "#" & dataReader("judge_name").ToString & "#" & dataReader("citation").ToString)
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return returnData
        Else
            Return returnData
        End If
    End Function


    Public Function SubjectIndex_Citation(ByVal level1 As String, ByVal level2 As String, ByVal summary As String, ByVal language As String) As String
        Dim query As String = "SELECT pages FROM subject_index where level1 = '" & level1 & "' and level2 = '" & level2 & "' and summary = '" & EscapeString(summary) & "' and language ='" & language & "'  order by pages" 'group by pages
        Dim pageNo As String = ""
        'Open connection
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                pageNo &= dataReader("pages").ToString & ", "
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return pageNo
        Else
            Return pageNo
            '===================== 
            'THE RETURN VALUE WILL HAVE SYMBOL ", " AT THE END IF RECORD MORE THAN 1
        End If
    End Function
    Public Function SelectSubjectIndexData(ByVal level1 As String, ByVal level2 As String, ByVal columnName As String) As String
        Dim query As String = "SELECT " & columnName & " FROM v_subject_index where level1 = '" & level1 & "' and level2 = '" & level2 & "' group by subject_index"
        Dim returnData As String = ""
        'Open connection

        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                returnData = dataReader(columnName).ToString
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return returnData
        Else
            Return returnData
        End If
    End Function
    Public Function SelectSubjectIndexDataColl(ByVal level1 As String, ByVal level2 As String, ByVal summary As String) As List(Of String)
        Dim query As String = "SELECT * FROM v_subject_index where level1 = '" & level1 & "' and level2 = '" & level2 & "' and summary like '%" & EscapeString(summary) & "%' group by citation"
        Dim returnData As New List(Of String)
        'Open connection

        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            'Read the data and store them in the list
            While dataReader.Read()
                returnData.Add(dataReader("judgment_name").ToString & "#" & dataReader("judge_name").ToString & "#" & dataReader("citation").ToString)
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return returnData
        Else
            Return returnData
        End If
    End Function
    Public Function SubjectIndex_Level1() As List(Of String)
        Dim query As String = ""
        query = "SELECT level1 FROM v_subject_index  group by level1 order by level1"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("level1").ToString))
            End While

            'close Data Reader
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
#End Region

#Region "TABLE OF CONTENT LIST DATA"

    Public Function Table_Of_Content_Cases_Title_With_Citation() As List(Of String)
        Dim query As String = "SELECT judgment_name, citation, court_type FROM xml_cases order by judgment_name" 'where court_type = '" & COURT & "
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("judgment_name").ToString) & "#" & dataReader("citation").ToString & "#" & Court_To_Bracket(dataReader("court_type").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Table_Of_Content_Cases_Title(ByVal COURT As String) As List(Of String)
        Dim query As String = "SELECT judgment_name FROM xml_cases where court_type = '" & COURT & "' group by judgment_name order by judgment_name"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("judgment_name").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            'Return list
        End If
    End Function

    Public Function Court_To_Bracket(ByVal strCourt As String) As String
        Dim lowerCourt As String = StrConv(strCourt, VbStrConv.Lowercase)
        If lowerCourt.Contains("federal") Then
            Return "[FC]"
        ElseIf lowerCourt.Contains("appeal") Then
            Return "[CA]"
        ElseIf lowerCourt.Contains("supreme") Then
            Return "[SC]"
        ElseIf lowerCourt.Contains("privy") Then
            Return "[PC]"
        ElseIf lowerCourt.Contains("high") Then
            Return "[HC]"
        ElseIf lowerCourt.Contains("tinggi") Then
            Return "[MT]"
        ElseIf lowerCourt.Contains("rayuan") Then
            Return "[MR]"
        End If
    End Function


    Public Function Table_Of_Content_Citation_Cases(ByVal COURT As String, ByVal JUDGMENT_NAME As String) As String
        Dim query As String = "SELECT citation FROM xml_cases where judgment_name = '" & EscapeString(JUDGMENT_NAME) & "' and court_type='" & COURT & "' group by citation order by citation"
        Dim list As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                list &= (dataReader("citation").ToString & "")
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Cases_Judicially_Title() As List(Of String) 'REFCASES TITLE LIST
        Dim query As String = "SELECT referred_title FROM ref_cases group by referred_title order by referred_title"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("referred_title").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Citation_Cases_Judicially(ByVal REFERRED_TITLE As String) As String
        Dim query As String = "SELECT referred_citation FROM ref_cases where referred_title = '" & EscapeString(REFERRED_TITLE).Trim & "' group by referred_citation order by referred_citation"
        Dim list As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                list &= (dataReader("referred_citation").ToString & ", ")
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Legislation_Judicially_Title() As List(Of String) 'REFCASES TITLE LIST
        Dim query As String = "SELECT legis_title FROM ref_legislations  group by legis_title order by legis_title"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("legis_title").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Citation_Legislation_Judicially_(ByVal REFERRED_LEGIS_TITLE As String) As String
        Dim query As String = "SELECT REFERRED_LEGIS_TITLE FROM ref_cases where referred_title = '" & EscapeString(REFERRED_LEGIS_TITLE) & "' group by referred_citation order by referred_citation"
        Dim list As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                list &= (dataReader("referred_citation").ToString & ", ")
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Subject_Index() As List(Of String) ' previous with language
        Dim query As String = "SELECT level1 FROM v_subject_index group by level1 order by level1"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("level1").ToString))
            End While
            'close Data Reader
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If

    End Function
    Public Function Table_Of_Content_Citation_Subject_Index(ByVal strSubjectIndex As String) As String ' , ByVal strLanguage As String
        Dim query As String = "SELECT citation FROM v_subject_index where level1 = '" & EscapeString(strSubjectIndex) & "' group by citation order by citation"
        Dim list As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                list &= (dataReader("citation").ToString & ", ")
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function
#End Region

#Region "CASES JUDICIALLY LIST DATA"
    Public Function CasesJudicial_FirstLetter() As List(Of String)
        'Dim arrList As String() = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
        'Dim list As List(Of String) = arrList.ToList

        Dim query As String = "SELECT firstletter from v_cases where firstletter<>'' group by firstletter order by firstletter"
        Dim list As List(Of String) = New List(Of String)

        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("firstletter").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
    Public Function CasesJudicial_Title(ByVal firstLetter As String) As List(Of String)
        Dim query As String = ""
        query = "select referred_title, referred_citation, referred_type from v_cases where referred_title like '" & firstLetter & "%' " & _
            "group by referred_title order by referred_title"

        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("referred_title").ToString.Trim) & "#" & dataReader("referred_citation").ToString & "#" & dataReader("referred_type").ToString)
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
    Public Function CasesJudicial_Citations(ByVal title As String) As String
        Dim query As String = ""
        query = "select referred_citation from v_cases where referred_title ='" & EscapeString(title) & "' group by referred_citation order by referred_citation "

        Dim strCitation As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                If strCitation = "" Then
                    strCitation = dataReader("referred_citation").ToString
                Else
                    strCitation = strCitation & "; " & dataReader("referred_citation").ToString
                End If
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return strCitation
        Else
            Return strCitation
        End If
    End Function

    Public Function CasesJudicial_Judgment_Name(ByVal firstLetter As String, ByVal title As String) As List(Of String)
        Dim list As List(Of String) = New List(Of String)
        Try
            Dim query As String = ""
            query = "select judgment_name, citation from v_cases " & _
                "where referred_title like '" & EscapeString(utility.CorrectTextFormat(title)) & "%' group by citation order by judgment_name"

            If Me.OpenConnection() = True Then
                Dim cmd As New MySqlCommand(query, connection)
                Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
                While dataReader.Read()
                    Dim col1 As String = " [" & dataReader("judgment_name").ToString & " " & utility.CorrectTextFormat(dataReader("citation").ToString) & "]"
                    If dataReader("judgment_name").ToString.Trim = "" Or dataReader("citation").ToString.Trim = "" Then
                    Else
                        list.Add(col1.Trim)
                    End If
                End While
                dataReader.Close()
                Me.CloseConnection()
            Else
                Return list
            End If
        Catch ex As Exception
            ' MsgBox(firstLetter & " " & subjectIndex)
        End Try
        Return list
    End Function
#End Region

#Region "LEGISLATION JUDICIALLY DATA"
    Public Function Legis_Judicial_Title() As List(Of String)
        Dim query As String = "SELECT legis_title from v_legislations group by legis_title order by legis_title" ' limit 3"
        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("legis_title").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Legis_Judicial_Section(ByVal legislation As String) As List(Of String)
        Dim query As String = "SELECT legis_sub_no from v_legislations where legis_title='" & EscapeString(legislation) & "' group by legis_sub_no order by sort_string, sort_decimal, legis_sub_no"
        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            Dim legis_sub_no As String = ""
            Dim legis_sub_type As String = ""
            Dim legis_sub_no_with_type As String = ""
            While dataReader.Read()
                legis_sub_no = dataReader("legis_sub_no").ToString
                If legis_sub_no.Contains(" 0 ") Then
                Else
                    list.Add(legis_sub_no)
                End If
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Legislation_Extract_Id_Sub_No() As List(Of String)
        Dim query As String = "SELECT * from v_legislations " ' where legis_title='" & EscapeString(legislation) & "' group by legis_sub_no order by legis_sub_no" 'order by stringvalue, sortValue, section"
        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            Dim ID As Integer
            Dim legis_sub_no As String = ""

            While dataReader.Read()
                ID = dataReader("id").ToString
                legis_sub_no = dataReader("legis_sub_no").ToString
                list.Add(ID & "#" & legis_sub_no)
            End While

            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function
    Public Function Table_Of_Content_Legislations() As List(Of String)
        Dim query As String = "SELECT legis_title, citation FROM v_legislations"
        Dim list As List(Of String) = New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("legis_title").ToString) & separator & utility.CorrectTextFormat(dataReader("citation").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Legis_Judicial_Citation(ByVal legislationTitle As String, ByVal section As String) As List(Of String)
        Dim query As String = "select citation from v_legislations where legis_title  ='" & EscapeString(legislationTitle) & "' and legis_sub_no = '" & EscapeString(section) & "' group by citation order by citation"
        Dim list As New List(Of String)
        Dim strCitation As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("citation").ToString()))
            End While
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            'Dim counter As Integer = 1
            'For Each pgNumber As String In list
            '    If counter = 1 Then
            '        strCitation = pgNumber
            '        'ElseIf counter = totalPageNo Then
            '        '    pageNo &= pgNumber
            '    Else
            '        strCitation &= Environment.NewLine & pgNumber
            '    End If
            '    counter = counter + 1
            'Next
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Legislation_Section_ReplaceValue(ByVal input1 As String, ByVal input2 As String) As String

        Dim finalOutput As String = ""
        Dim input2Pattern As String
        Dim comparePattern As String
        Dim compareRegex As Regex
        Dim compareMatch As Match
        Dim input2Regex As Regex
        Dim input2Match As Match
        Dim replaceValue As String

        If input2.StartsWith("(") Then

            input2Pattern = Extractor.FindRegexPattern_Bracket(input2)
            input2Regex = New Regex(input2Pattern)
            input2Match = input2Regex.Match(input2)

            comparePattern = Extractor.FindRegexPattern_Bracket(input1)
            compareRegex = New Regex(input2Pattern)
            compareMatch = compareRegex.Match(input1)

            If compareMatch.Success() Then
                Dim newValue As String
                replaceValue = input2Match.Value
                newValue = Regex.Replace(input1, comparePattern, replaceValue)
                finalOutput = newValue

            Else 'if not found any match with bracket
                Dim newValue = input1 & input2
                finalOutput = newValue

            End If
        End If

        'Dim bracketPattern As String = Extractor.FindRegexPattern_Bracket(input2)
        'Dim r As New Regex(bracketPattern)

        'Dim tmpValue As String
        'tmpValue = r.Replace(input1, input2)

        'Dim result As String
        'result = tmpValue
        Return finalOutput.Trim

    End Function

    Public Function Extract_Temp_Ref_Legislation_To_Table(ByVal referredLegisType As String) As List(Of String)
        My.Computer.FileSystem.WriteAllText("tmp_to_db_legis_local.txt", "", False)
        My.Computer.FileSystem.WriteAllText("tmp_to_db_legis_foreign.txt", "", False)
        Dim tableName As String = ""
        Dim origin As String = ""
        If referredLegisType = "local" Then
            tableName = "tmp_ref_leg_local"
            origin = "local"
        Else
            tableName = "tmp_ref_leg_foreign"
            origin = "foreign"
        End If
        Dim source_citation As String
        Dim source_datafilename As String
        Dim legislink_origin As String
        Dim legislink_title As String
        Dim legislink_filename As String
        Dim legislink_no As String
        Dim legislink_type As String
        Dim legislink_htext As String
        Dim legislink_char As String
        Dim legislink_fullType As String
        Dim line As String
        Dim query As String = "select source_citation, source_datafilename, legislink_origin, legislink_title, legislink_filename, " & _
            "legislink_no, legislink_type, legislink_htext, legislink_char from " & tableName
        Dim list As List(Of String) = New List(Of String)

        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                source_citation = dataReader("source_citation").ToString
                source_datafilename = dataReader("source_datafilename").ToString
                legislink_origin = dataReader("legislink_origin").ToString
                legislink_title = dataReader("legislink_title").ToString
                legislink_filename = dataReader("legislink_filename").ToString
                legislink_fullType = dataReader("legislink_type").ToString
                legislink_type = Extractor.ReferredLegislations.Extract_Legislation_Sub_No_Type(dataReader("legislink_char").ToString)
                legislink_no = dataReader("legislink_no").ToString
                legislink_htext = dataReader("legislink_htext").ToString

                If legislink_type = legislink_no Then
                    'LEAVE ME ALONE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Else
                    If legislink_no.Contains("O ") Then
                        legislink_no = legislink_no.Trim

                    Else 'other than Order and rule type
                        If legislink_no = "" Then
                            legislink_no = legislink_type
                        Else
                            If IsNumeric(legislink_no(0)) = True Then
                                legislink_no = Extractor.ReferredLegislations.Extract_Legislation_Sub_No_Type(legislink_type) & " " & legislink_no
                            Else
                                If legislink_no.Trim.StartsWith("(") Then
                                    legislink_no = Legislation_Section_ReplaceValue(legislink_fullType, legislink_no).Trim
                                Else
                                    legislink_no = legislink_no
                                End If
                            End If
                        End If
                    End If
                End If

                line = source_citation & "#" & source_datafilename & "#" & legislink_origin & "#" & legislink_title & "#" & legislink_filename & "#" & legislink_no & "#" & legislink_type & "#" & legislink_htext
                list.Add(source_citation & "#" & source_datafilename & "#" & legislink_origin & "#" & legislink_title & "#" & legislink_filename & "#" & legislink_no & "#" & legislink_type & "#" & legislink_htext)

                If origin = "local" Then
                    My.Computer.FileSystem.WriteAllText("tmp_to_db_legis_local.txt", line & Environment.NewLine, True)
                Else
                    My.Computer.FileSystem.WriteAllText("tmp_to_db_legis_foreign.txt", line & Environment.NewLine, True)
                End If
            End While

            dataReader.Close()
            Me.CloseConnection()
            Return list
        End If
    End Function

    '====================================================REGION ENDED============================
#End Region
    Public Function ActInfo() As List(Of String)
        Dim query As String = ""
        Dim list As New List(Of String)
        query = "select filename, title from rulestb group by filename"
        If Me.OpenConnection = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(dataReader("filename").ToString & "#" & dataReader("title").ToString)
            End While
            dataReader.Close()
            Me.CloseConnection()
        End If
        Return list
    End Function

    Public Sub UpdateActInfo()
        Dim query As String = ""
        Dim list As New List(Of String)
        list = ActInfo()
        If Me.OpenConnection = True Then
            For Each actNo As String In list
                Dim filename As String = actNo.Split("#")(0).Replace(";", "")
                Dim title As String = actNo.Split("#")(1)

                query = "update acttiletb set citation = '" & filename & "' where title = '" & title & "'"
                Dim cmd As New MySqlCommand(query, connection)
                cmd.ExecuteNonQuery()
            Next
            Me.CloseConnection()
        End If

    End Sub

    Public Function GetLegislationActType(ByVal LegislationCitation As String) As String
        Dim query As String = "select legType from act_type where legCitation like '" & LegislationCitation & "%' limit 1"
        Dim datareader As MySqlDataReader
        Dim result As String = "section "
        If Me.OpenConnection = True Then
            Dim cmd As New MySqlCommand(query, connection)
            datareader = cmd.ExecuteReader
            While datareader.Read
                result = " " & datareader("legType").ToString.ToLower & " "
            End While
            datareader.Close()
        End If
        Me.CloseConnection()
        Return result.Trim()
    End Function

    'Update statement
    Public Sub CompareRefCases()

        Dim txtFile As String = "listToRemove.txt"
        Dim query As String = "select r.rootCitation, r.ReferredCitation, r.ReferredTitle, r.type from refcases r, tmp_refcases t where r.RootCitation=t.RootCitation and r.ReferredCitation = t.ReferredCitation and r.type = 'refd'"
        Dim list As List(Of String) = New List(Of String)

        If Me.OpenConnection() = True Then

            Dim cmd As New MySqlCommand(query, connection)
            cmd.CommandTimeout = 0
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            My.Computer.FileSystem.WriteAllText(txtFile, "", False)

            While dataReader.Read()
                Dim rCit As String = dataReader("rootCitation").ToString
                Dim refCit = dataReader("referredCitation").ToString
                Dim refTitle = dataReader("referredTitle").ToString
                Dim type = dataReader("type").ToString
                Dim strToWrite As String = rCit & "#" & refCit & "#" & refTitle & "#" & type & Environment.NewLine
                My.Computer.FileSystem.WriteAllText(txtFile, strToWrite, True)
            End While

            dataReader.Close()

            For Each strRow As String In File.ReadAllLines(txtFile)
                Dim rootCitation As String = strRow.Split("#")(0)
                Dim referredCitation As String = strRow.Split("#")(1)
                Dim referredTitle As String = strRow.Split("#")(2)
                Dim type As String = strRow.Split("#")(3)
                Dim sql As String = "delete from refCases where " & _
                    "rootCitation ='" & rootCitation & "' and " & _
                    "referredCitation = '" & referredCitation & "' and " & _
                    "referredTitle = '" & referredTitle & "' and " & _
                    "type = '" & type & "'"

                Dim cmdDelete As New MySqlCommand(sql, connection)
                cmdDelete.CommandTimeout = 0
                cmdDelete.ExecuteNonQuery()
            Next

            ' Dim counter As Integer = 1
            ' For Each str As String In list
            '    Dim rootCitation As String = str.Split("#")(0)
            '    Dim referCitation As String = str.Split("#")(1)
            '    Dim delSQL As String = "delete refCases where rootCitation ='" & rootCitation & "' and " & _
            '                    "referredCitation like '%" & referCitation & "%' and type='refd'"

            '    Dim cmdDelete As New MySqlCommand(delSQL, connection)
            '    Try
            '        cmdDelete.ExecuteNonQuery()
            '    Catch ex As Exception
            '        MsgBox(ex.Message)
            '    End Try
            '    cmdDelete.Dispose()
            '    counter = counter + 1
            '    If counter Mod 100 = 0 Then
            '        MsgBox(counter)
            '    End If
            'Next
            Me.CloseConnection()
        Else

        End If
    End Sub
    'Delete statement
    Public Function Selects() As List(Of String)
        Dim query As String = "SELECT subject_index, summary, citation FROM subject_index order by subject_index"
        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                list.Add(dataReader("subject_index").ToString & " [summary] " & dataReader("summary").ToString & " [pages] " & dataReader("pages").ToString)
                'list(0).Add(dataReader("subject_index") & "")
                'list(1).Add(dataReader("summary") & "")
                'list(2).Add(dataReader("pages") & "")
            End While

            'close Data Reader
            dataReader.Close()

            'close Connection
            Me.CloseConnection()

            'return list to be displayed
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Court_List() As List(Of String)
        Dim query As String = "SELECT distinct court_type from xml_cases order by case " & _
                              "when court_type like '%federal%' then 0 WHEN  court_type like '%Appeal%' then 1  " & _
                              "WHEN  court_type like '%High%' then 2 else 3 end"
        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(dataReader(0).ToString())
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Sub Extract_Temp_Ref_Legislation_DB_ToList(ByVal referredType As String) ' As List(Of String)
        Dim tableName As String = ""
        Dim origin As String = ""
        If referredType = "local" Then
            tableName = "tmp_ref_leg_local"
            origin = "local"
        Else
            tableName = "tmp_ref_leg_foreign"
            origin = "foreign"
        End If
        Dim query As String = "insert into ref_legislations (source_citation, source_datafilename, legis_origin, legis_title, legis_filename, legis_sub_no, legis_sub_no_type, legis_link_text) " & _
            "select source_citation, source_datafilename, legislink_origin, legislink_title, legislink_filename, legislink_no, legislink_type, legislink_htext " & _
            "from " & tableName

        Dim list As New List(Of String)
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            dataReader.Close()

            Me.CloseConnection()

        End If
    End Sub

    '========================GET FUNCTION
    Public Function getCasesTitleByCitation(citation As String) As String
        citation = citation.Replace("  ", " ").Trim
        Dim datafilename As String = utility.PrintDatafilename(citation)

        Dim query As String = "select title from api_cases where datafilename = '" & datafilename & ".xml' limit 1"
        'Create a list to store the result
        Dim list As New List(Of String)
        Dim caseTitle As String = "NO TITLE"
        'Open connection
        If Me.OpenConnection() = True Then
            'Create Command
            Dim cmd As New MySqlCommand(query, connection)
            'Create a data reader and Execute the command
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()

            'Read the data and store them in the list
            While dataReader.Read()
                ' legCitation = dataReader("datafilename").ToString().Replace(".xml", "").Replace(Environment.NewLine, "").Replace(vbLf, "").Replace(vbCr, "").Replace(vbLf & vbCr, "")
                'Exit While
                caseTitle = dataReader("title").ToString
            End While

            dataReader.Close()
            'close Connection
            Me.CloseConnection()
            'return list to be displayed
            Return caseTitle.Trim()
        End If
        Return caseTitle
    End Function

    Public Function GetLegislationTitleByDatafilename(datafilename As String) As String
        Dim query As String = (Convert.ToString("select title from api_legislation where datafilename like '%") & datafilename) & "%' limit 1"
        'Create a list to store the result
        Dim list As New List(Of String)
        Dim legTitle As String = "NO TITLE"
        'Open connection
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                legTitle = dataReader("title").ToString.Replace("  ", " ")
            End While

            dataReader.Close()
            'close Connection
            Me.CloseConnection()

            legTitle = legTitle.Replace("INSOLVENCY ACT 1967", "BANKRUPTCY ACT 1967")
            'return list to be displayed
            Return legTitle
        End If
        Return legTitle
    End Function

    Public Function GetJudgmentNameFromCitation(ByVal Citation As String) As String
        Dim currentCitation As String = ""
        If Citation.Contains(";") Then
            currentCitation = Citation.Split(";")(0).Trim
        Else
            currentCitation = Citation.Trim
        End If
        Dim query As String = "select title from api_cases where datafilename ='" & utility.PrintDatafilename(currentCitation) & ".xml' limit 1"
        Dim CaseTitle As String = ""
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                CaseTitle = dataReader("title").ToString.Replace("  ", " ")
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return CaseTitle
        End If
        Return CaseTitle
    End Function

    Private Function CheckLegislationNameIsExist(ByVal strLegislation As String, ByVal legisType As String, ByVal legFilename As String) As String
        Dim tableName As String
        Dim query As String = ""
        If legisType = "local" Then
            query = "select legislink_title from tmp_ref_leg_local  where legislink_filename ='" & legFilename & "' "
        Else
            query = "select legislink_title from tmp_ref_leg_foreign where "
        End If

        Dim CaseTitle As String = "NO TITLE"
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                CaseTitle = dataReader("title").ToString
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return CaseTitle
        End If
        Return CaseTitle
    End Function

    '==========================================TESTING AREA
    Public Function Testing() As List(Of String)
        Dim query As String = "SELECT * FROM `ref_cases` where referred_title like '%\'0%'"
        Dim list As List(Of String) = New List(Of String)

        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                list.Add(utility.CorrectTextFormat(dataReader("referred_title").ToString))
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return list
        Else
            Return list
        End If
    End Function

    Public Function Translator_Legislation(ByVal inputString As String) As String
        Dim query As String = "SELECT * FROM keyword_legislation where old_value='" & inputString & "' limit 1 "
        Dim result As String = inputString
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                result = dataReader("new_value").ToString
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return result
        Else
            Return result
        End If
    End Function

    Public Function Translator_Subject_Index(ByVal inputString As String) As String
        Dim query As String = "SELECT * FROM keyword_subject_index where old_value='" & inputString & "' limit 1 "
        Dim result As String = inputString
        If Me.OpenConnection() = True Then
            Dim cmd As New MySqlCommand(query, connection)
            Dim dataReader As MySqlDataReader = cmd.ExecuteReader()
            While dataReader.Read()
                result = dataReader("new_value").ToString
            End While
            dataReader.Close()
            Me.CloseConnection()
            Return result
        Else
            Return result
        End If
    End Function

End Class
