Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class DataPreview
    Public connectionStr As String = "server=localhost;user id=root;password=root123;persistsecurityinfo=True;database=consolidate_index_db;connectiontimeout=300;allowbatch=True"
    Private Sub DataPreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboData.SelectedIndex = 0
    End Sub
    Private Sub cboData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboData.SelectedIndexChanged
        ' Call InitiateDatabaseData(DataSelection(cboData.Text))
    End Sub
    Public DatabaseSelection As String = ""
    Public Function DataSelection(ByVal str As String)
        Dim result As String = ""
        Select Case str
            Case Is = "Cases Judicially Data"
                result = "v_cases"
            Case Is = "Legislations Judicially Data"
                result = "v_legislations"
            Case Is = "Subject Index Data"
                result = "v_subject_index"
            Case Else
                str = "Select From List"
                result = "NOSELECTION"
        End Select
        DatabaseSelection = result
        Return result
    End Function

    Public Function QuerySelection(ByVal selection As String) As String
        Dim result As String = ""
        Dim column As String = "id, datafilename, judgment_name, court_type, judge_name, judgment_language, citation"

        If selection = "v_cases" Then
            result = "select " & column & ", referred_citation, referred_title, referred_type from v_cases"
        ElseIf selection = "v_legislations" Then
            result = "select " & column & ", legis_title, legis_filename, legis_sub_no, legis_link_text from v_legislations"
        ElseIf selection = "v_subject_index" Then
            result = "select " & column & ", summary, subject_index, level1, level2 from v_subject_index"
        Else
            result = ""
        End If
        Return result
    End Function
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Call InitiateDatabaseData(DataSelection(cboData.Text))
    End Sub
    Private Sub InitiateDatabaseData(ByVal selection As String)

        Dim mySqlConn As New MySqlConnection
        Dim query As String = ""
        Dim da As New MySqlDataAdapter
        Dim cmd As New MySqlCommand
        Dim dt As New DataTable
        Dim dSource As New BindingSource
        mySqlConn.ConnectionString = connectionStr

        Try
            If DatabaseSelection = "v_cases" Then
                Label1.Text = "Referred Citation : "
                Label2.Text = "Referred Title : "
                Label3.Text = "Referred Type : "
                Label4.Visible = False
                TextBox4.Visible = False

            ElseIf DatabaseSelection = "v_legislations" Then
                Label4.Visible = True
                TextBox4.Visible = True
                Label1.Text = "Legislation Title : "
                Label2.Text = "Legislation Filename : "
                Label3.Text = "Legislation Sub No : "
                Label4.Text = "Legislation Link Text : "
            Else
                Label4.Visible = True
                TextBox4.Visible = True
                Label1.Text = "Summary : "
                Label2.Text = "Subject Index : "
                Label3.Text = "Keyword 1 : "
                Label4.Text = "Keyword 2 : "
            End If
            mySqlConn.Open()
            query = QuerySelection(selection)
            cmd = New MySqlCommand(query, mySqlConn)
            da.SelectCommand = cmd
            da.Fill(dt)
            dSource.DataSource = dt
            dataGrid.DataSource = dSource
            HideColumn({2, 3, 4, 5, 6})
            dataGrid.Update()
        Catch ex As Exception
        Finally
            mySqlConn.Dispose()
        End Try
    End Sub

    Public Sub HideColumn(ByVal arrNo As String())
        For Each no As Integer In arrNo
            dataGrid.Columns(no).Visible = False
        Next
    End Sub

    Private Sub dataGrid_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataGrid.CellContentDoubleClick
        Dim datafilename As String = dataGrid.Item(1, e.RowIndex).Value
        Dim id As String = dataGrid.Item(0, e.RowIndex).Value
        Dim value1 As String
        Dim value2 As String
        Dim value3 As String
        Dim value4 As String

        If DatabaseSelection = "v_cases" Then
            value1 = dataGrid.Item("referred_citation", e.RowIndex).Value
            value2 = dataGrid.Item("referred_title", e.RowIndex).Value
            value3 = dataGrid.Item("referred_type", e.RowIndex).Value
            'value4 = dataGrid.Item(dataGrid.ColumnCount - 3, e.RowIndex).Value

        ElseIf DatabaseSelection = "v_legislations" Then
            value1 = dataGrid.Item("legis_title", e.RowIndex).Value
            value2 = dataGrid.Item("legis_sub_type", e.RowIndex).Value
            value3 = dataGrid.Item("legis_sub_no", e.RowIndex).Value
            value4 = dataGrid.Item("legis_link_text", e.RowIndex).Value

        ElseIf DatabaseSelection = "v_subject_index" Then
            value1 = dataGrid.Item("summary", e.RowIndex).Value
            value2 = dataGrid.Item("subject_index", e.RowIndex).Value
            value3 = dataGrid.Item("level1", e.RowIndex).Value
            value4 = dataGrid.Item("level2", e.RowIndex).Value
        Else

        End If
        txtDatafilename.Text = datafilename
        txtId.Text = id
        TextBox1.Text = value1
        TextBox2.Text = value2
        TextBox3.Text = value3
        TextBox4.Text = value4
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim QueryUpdate As String = ""

        If DatabaseSelection = "v_cases" Then
            QueryUpdate = "update v_cases set referred_citation ='" & TextBox1.Text & "', referred_title='" & TextBox2.Text & "', " & _
                "referred_type ='" & TextBox3.Text & "' " & " where id=" & txtId.Text

        ElseIf DatabaseSelection = "v_legislations" Then
            QueryUpdate = "update v_legislations set legis_title ='" & TextBox1.Text & "', legis_filename='" & TextBox2.Text & "', " & _
                 "legis_sub_no ='" & TextBox3.Text & "', legis_link_text='" & TextBox4.Text & "' " & " where id=" & txtId.Text

        ElseIf DatabaseSelection = "v_subject_index" Then
            QueryUpdate = "update v_subject_index set summary ='" & TextBox1.Text & "', subject_index='" & TextBox2.Text & "', " & _
                "level1 ='" & TextBox3.Text & "', level2='" & TextBox4.Text & "' " & " where id=" & txtId.Text

        Else

        End If

        Dim mysqlConn As New MySqlConnection
        mysqlConn.ConnectionString = connectionStr

        mysqlConn.Open()
        Dim cmd As New MySqlCommand(QueryUpdate, mysqlConn)
        cmd.ExecuteNonQuery()
        mysqlConn.Dispose()

        MsgBox("Record successfully update")

    End Sub



End Class