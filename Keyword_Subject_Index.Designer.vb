<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Keyword_Subject_Index
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.dgvLegislation = New System.Windows.Forms.DataGridView()
        Me.KeywordlegislationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Consolidate_index_dbDataSet = New SubjectIndexXML.consolidate_index_dbDataSet()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtOldKeyword = New System.Windows.Forms.TextBox()
        Me.txtNewValue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.Keyword_legislationTableAdapter = New SubjectIndexXML.consolidate_index_dbDataSetTableAdapters.keyword_legislationTableAdapter()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.dgvLegislation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.KeywordlegislationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Consolidate_index_dbDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvLegislation
        '
        Me.dgvLegislation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvLegislation.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvLegislation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLegislation.Location = New System.Drawing.Point(12, 28)
        Me.dgvLegislation.Name = "dgvLegislation"
        Me.dgvLegislation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvLegislation.Size = New System.Drawing.Size(409, 181)
        Me.dgvLegislation.TabIndex = 0
        '
        'KeywordlegislationBindingSource
        '
        Me.KeywordlegislationBindingSource.DataMember = "keyword_legislation"
        Me.KeywordlegislationBindingSource.DataSource = Me.Consolidate_index_dbDataSet
        '
        'Consolidate_index_dbDataSet
        '
        Me.Consolidate_index_dbDataSet.DataSetName = "consolidate_index_dbDataSet"
        Me.Consolidate_index_dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 268)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Old Value"
        '
        'txtOldKeyword
        '
        Me.txtOldKeyword.Location = New System.Drawing.Point(74, 265)
        Me.txtOldKeyword.Name = "txtOldKeyword"
        Me.txtOldKeyword.Size = New System.Drawing.Size(347, 20)
        Me.txtOldKeyword.TabIndex = 2
        '
        'txtNewValue
        '
        Me.txtNewValue.Location = New System.Drawing.Point(74, 291)
        Me.txtNewValue.Name = "txtNewValue"
        Me.txtNewValue.Size = New System.Drawing.Size(347, 20)
        Me.txtNewValue.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 294)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "New Value"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(346, 326)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(12, 215)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(174, 215)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 7
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(93, 215)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 8
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'Keyword_legislationTableAdapter
        '
        Me.Keyword_legislationTableAdapter.ClearBeforeFill = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(346, 215)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Keyword_Subject_Index
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 366)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtNewValue)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtOldKeyword)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvLegislation)
        Me.Name = "Keyword_Subject_Index"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Keyword_Subject_Index"
        CType(Me.dgvLegislation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.KeywordlegislationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Consolidate_index_dbDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvLegislation As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOldKeyword As System.Windows.Forms.TextBox
    Friend WithEvents txtNewValue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents Consolidate_index_dbDataSet As SubjectIndexXML.consolidate_index_dbDataSet
    Friend WithEvents KeywordlegislationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Keyword_legislationTableAdapter As SubjectIndexXML.consolidate_index_dbDataSetTableAdapters.keyword_legislationTableAdapter
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
End Class
