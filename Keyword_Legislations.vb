Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class Keyword_Legislations
    Dim strSQL As String = ""
    Dim dbConnect As DBConnect = New DBConnect()
    Public connectionStr As String = "server=localhost; user id=root; password=root123; persistsecurityinfo=True; database=consolidate_index_db; connectiontimeout=300; allowbatch=True"
    Public mySqlConn As MySqlConnection
    Public da As MySqlDataAdapter
    Public cmd As MySqlCommand
    Public dt As DataTable
    Dim dSource As BindingSource

    Private Sub DataGridView_LoadData()
        txtOldKeyword.Text = ""
        txtNewValue.Text = ""

        Dim mySqlConn As New MySqlConnection
        Dim query As String = ""
        Dim da As New MySqlDataAdapter
        Dim cmd As New MySqlCommand
        Dim dt As New DataTable
        Dim dSource As New BindingSource
        mySqlConn.ConnectionString = connectionStr
        mySqlConn.Open()
        query = "select id, old_value, new_value from keyword_legislation"
        cmd = New MySqlCommand(query, mySqlConn)
        da.SelectCommand = cmd
        da.Fill(dt)
        dSource.DataSource = dt
        dgvLegislation.DataSource = dSource
        dgvLegislation.Columns(0).Visible = False
        dgvLegislation.Update()
    End Sub
    Private Sub Keyword_Legislations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView_LoadData()
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        txtOldKeyword.Text = ""
        txtNewValue.Text = ""
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim delQuery As String = ""
        For Each selectedRow As DataGridViewRow In dgvLegislation.SelectedRows
            Dim selectedId As Integer = selectedRow.Cells("id").Value.ToString
            delQuery = "delete from keyword_legislation where id=" & selectedId
            dbConnect.RunQuery(delQuery)
        Next
        MsgBox("Record Deleted")
        DataGridView_LoadData()
    End Sub
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim updateQuery As String = ""
        For Each selectedRow As DataGridViewRow In dgvLegislation.SelectedRows
            Dim selectedId As Integer = selectedRow.Cells("id").Value.ToString
            Dim oldS As Integer = selectedRow.Cells("old_value").Value.ToString
            Dim newS As Integer = selectedRow.Cells("new_value").Value.ToString
            updateQuery = "update from keyword_legislation set old_value='" & oldS & "' and new_value='" & newS & "' where id=" & selectedId
            dbConnect.RunQuery(updateQuery)
        Next
        MsgBox("Record Updated")
        DataGridView_LoadData()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim insertSQL As String = "insert into keyword_legislation (old_value, new_value) values('" & txtOldKeyword.Text & "','" & txtNewValue.Text & "')"
        dbConnect.RunQuery(insertSQL)
        MsgBox("Record Inserted")
        DataGridView_LoadData()
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        DataGridView_LoadData()
    End Sub
End Class