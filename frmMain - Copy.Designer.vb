<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.lblfn = New System.Windows.Forms.Label()
        Me.saveFolderPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnCreateXML = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnPath = New System.Windows.Forms.Button()
        Me.sfd = New System.Windows.Forms.SaveFileDialog()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lstProgress = New System.Windows.Forms.ListBox()
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.btnPrepare = New System.Windows.Forms.Button()
        Me.cbTableContent = New System.Windows.Forms.CheckBox()
        Me.cbCasesJud = New System.Windows.Forms.CheckBox()
        Me.cbLegisJud = New System.Windows.Forms.CheckBox()
        Me.cbSubIndex = New System.Windows.Forms.CheckBox()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblProgressTitle = New System.Windows.Forms.Label()
        Me.lblPercent = New System.Windows.Forms.Label()
        Me.cbOpenOutput = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.cboSelection = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbSubjectIndex = New System.Windows.Forms.CheckBox()
        Me.cbRefLegislation = New System.Windows.Forms.CheckBox()
        Me.cbRefCases = New System.Windows.Forms.CheckBox()
        Me.cbSkipError = New System.Windows.Forms.CheckBox()
        Me.cbDeleteData = New System.Windows.Forms.CheckBox()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnExtract = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnExtractorTest = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.bgWork = New System.ComponentModel.BackgroundWorker()
        Me.btnExtractRefCases = New System.Windows.Forms.Button()
        Me.btnExtractRefLegislation = New System.Windows.Forms.Button()
        Me.btnExtractSubjectIndex = New System.Windows.Forms.Button()
        Me.cbErrLegislation = New System.Windows.Forms.CheckBox()
        Me.chkboxdeldata = New System.Windows.Forms.CheckBox()
        Me.cbErrXML = New System.Windows.Forms.CheckBox()
        Me.chkboxdeldatacases = New System.Windows.Forms.CheckBox()
        Me.cbErrSubjectIndex = New System.Windows.Forms.CheckBox()
        Me.chkboxdeldatalegislation = New System.Windows.Forms.CheckBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(35, 65)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(150, 35)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "UPDATE DATA"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'lblfn
        '
        Me.lblfn.AutoSize = True
        Me.lblfn.Font = New System.Drawing.Font("Tahoma", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfn.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblfn.Location = New System.Drawing.Point(128, 168)
        Me.lblfn.Name = "lblfn"
        Me.lblfn.Size = New System.Drawing.Size(83, 14)
        Me.lblfn.TabIndex = 18
        Me.lblfn.Text = "* FileName : "
        '
        'btnCreateXML
        '
        Me.btnCreateXML.Location = New System.Drawing.Point(35, 519)
        Me.btnCreateXML.Name = "btnCreateXML"
        Me.btnCreateXML.Size = New System.Drawing.Size(160, 36)
        Me.btnCreateXML.TabIndex = 19
        Me.btnCreateXML.Text = "Generate XML"
        Me.btnCreateXML.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gold
        Me.Label1.Location = New System.Drawing.Point(8, 194)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(344, 15)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "3. Click each button below if path selected is correct."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Gold
        Me.Label3.Location = New System.Drawing.Point(8, 309)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 15)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "4. Click Button to prepare data"
        '
        'btnPath
        '
        Me.btnPath.Location = New System.Drawing.Point(631, 416)
        Me.btnPath.Name = "btnPath"
        Me.btnPath.Size = New System.Drawing.Size(78, 23)
        Me.btnPath.TabIndex = 24
        Me.btnPath.Text = "Browse"
        Me.btnPath.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Gold
        Me.Label5.Location = New System.Drawing.Point(8, 394)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(343, 15)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "5. Set your save location (XML File) && Generate XML"
        '
        'lstProgress
        '
        Me.lstProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lstProgress.Dock = System.Windows.Forms.DockStyle.Right
        Me.lstProgress.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstProgress.ForeColor = System.Drawing.Color.PaleGreen
        Me.lstProgress.FormattingEnabled = True
        Me.lstProgress.ItemHeight = 15
        Me.lstProgress.Location = New System.Drawing.Point(974, 0)
        Me.lstProgress.Name = "lstProgress"
        Me.lstProgress.Size = New System.Drawing.Size(290, 681)
        Me.lstProgress.TabIndex = 27
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.Color.OrangeRed
        Me.txtPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPath.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPath.Location = New System.Drawing.Point(131, 144)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(494, 21)
        Me.txtPath.TabIndex = 38
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(631, 143)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(69, 23)
        Me.btnBrowse.TabIndex = 39
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Gold
        Me.Label7.Location = New System.Drawing.Point(8, 118)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(190, 15)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "2. Select XML Source Folder"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label8.Location = New System.Drawing.Point(43, 147)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 15)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Path : "
        '
        'fbd
        '
        Me.fbd.SelectedPath = "C:\Users\Diddy\Desktop\MLRA_MLRH_2012_17\MLRA"
        '
        'btnPrepare
        '
        Me.btnPrepare.Location = New System.Drawing.Point(35, 337)
        Me.btnPrepare.Name = "btnPrepare"
        Me.btnPrepare.Size = New System.Drawing.Size(150, 35)
        Me.btnPrepare.TabIndex = 43
        Me.btnPrepare.Text = "Prepare Data"
        Me.btnPrepare.UseVisualStyleBackColor = True
        '
        'cbTableContent
        '
        Me.cbTableContent.AutoSize = True
        Me.cbTableContent.Checked = True
        Me.cbTableContent.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbTableContent.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbTableContent.Location = New System.Drawing.Point(35, 492)
        Me.cbTableContent.Name = "cbTableContent"
        Me.cbTableContent.Size = New System.Drawing.Size(115, 19)
        Me.cbTableContent.TabIndex = 44
        Me.cbTableContent.Text = "Table of Content"
        Me.cbTableContent.UseVisualStyleBackColor = True
        '
        'cbCasesJud
        '
        Me.cbCasesJud.AutoSize = True
        Me.cbCasesJud.Checked = True
        Me.cbCasesJud.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbCasesJud.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbCasesJud.Location = New System.Drawing.Point(152, 492)
        Me.cbCasesJud.Name = "cbCasesJud"
        Me.cbCasesJud.Size = New System.Drawing.Size(113, 19)
        Me.cbCasesJud.TabIndex = 45
        Me.cbCasesJud.Text = "Cases Judicially"
        Me.cbCasesJud.UseVisualStyleBackColor = True
        '
        'cbLegisJud
        '
        Me.cbLegisJud.AutoSize = True
        Me.cbLegisJud.Checked = True
        Me.cbLegisJud.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbLegisJud.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbLegisJud.Location = New System.Drawing.Point(271, 492)
        Me.cbLegisJud.Name = "cbLegisJud"
        Me.cbLegisJud.Size = New System.Drawing.Size(139, 19)
        Me.cbLegisJud.TabIndex = 46
        Me.cbLegisJud.Text = "Legislation Judicially"
        Me.cbLegisJud.UseVisualStyleBackColor = True
        '
        'cbSubIndex
        '
        Me.cbSubIndex.AutoSize = True
        Me.cbSubIndex.Checked = True
        Me.cbSubIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSubIndex.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.cbSubIndex.Location = New System.Drawing.Point(416, 492)
        Me.cbSubIndex.Name = "cbSubIndex"
        Me.cbSubIndex.Size = New System.Drawing.Size(100, 19)
        Me.cbSubIndex.TabIndex = 47
        Me.cbSubIndex.Text = "Subject Index"
        Me.cbSubIndex.UseVisualStyleBackColor = True
        '
        'txtOutput
        '
        Me.txtOutput.BackColor = System.Drawing.Color.OrangeRed
        Me.txtOutput.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtOutput.Location = New System.Drawing.Point(131, 416)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(494, 21)
        Me.txtOutput.TabIndex = 49
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(43, 419)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 15)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Save Path : "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Gold
        Me.Label9.Location = New System.Drawing.Point(24, 595)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(133, 16)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Current Progress : "
        '
        'lblProgressTitle
        '
        Me.lblProgressTitle.AutoSize = True
        Me.lblProgressTitle.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgressTitle.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblProgressTitle.Location = New System.Drawing.Point(24, 635)
        Me.lblProgressTitle.Name = "lblProgressTitle"
        Me.lblProgressTitle.Size = New System.Drawing.Size(107, 16)
        Me.lblProgressTitle.TabIndex = 52
        Me.lblProgressTitle.Text = "lblProgressTitle"
        Me.lblProgressTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPercent
        '
        Me.lblPercent.AutoSize = True
        Me.lblPercent.Font = New System.Drawing.Font("Tahoma", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPercent.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblPercent.Location = New System.Drawing.Point(153, 595)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(115, 39)
        Me.lblPercent.TabIndex = 53
        Me.lblPercent.Text = "100%"
        '
        'cbOpenOutput
        '
        Me.cbOpenOutput.AutoSize = True
        Me.cbOpenOutput.Checked = True
        Me.cbOpenOutput.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbOpenOutput.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbOpenOutput.ForeColor = System.Drawing.SystemColors.ActiveBorder
        Me.cbOpenOutput.Location = New System.Drawing.Point(201, 531)
        Me.cbOpenOutput.Name = "cbOpenOutput"
        Me.cbOpenOutput.Size = New System.Drawing.Size(156, 17)
        Me.cbOpenOutput.TabIndex = 54
        Me.cbOpenOutput.Text = "Open Folder After Complete"
        Me.cbOpenOutput.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.TextBox3)
        Me.GroupBox1.Controls.Add(Me.cboSelection)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cbSubjectIndex)
        Me.GroupBox1.Controls.Add(Me.cbRefLegislation)
        Me.GroupBox1.Controls.Add(Me.cbRefCases)
        Me.GroupBox1.Controls.Add(Me.cbSkipError)
        Me.GroupBox1.Controls.Add(Me.cbDeleteData)
        Me.GroupBox1.Controls.Add(Me.btnPreview)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnExtract)
        Me.GroupBox1.Controls.Add(Me.cbOpenOutput)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnUpdate)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtPath)
        Me.GroupBox1.Controls.Add(Me.cbSubIndex)
        Me.GroupBox1.Controls.Add(Me.btnCreateXML)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cbLegisJud)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cbCasesJud)
        Me.GroupBox1.Controls.Add(Me.txtOutput)
        Me.GroupBox1.Controls.Add(Me.cbTableContent)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.lblfn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnPrepare)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnPath)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(959, 580)
        Me.GroupBox1.TabIndex = 55
        Me.GroupBox1.TabStop = False
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(447, 290)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(480, 21)
        Me.TextBox3.TabIndex = 68
        '
        'cboSelection
        '
        Me.cboSelection.FormattingEnabled = True
        Me.cboSelection.Items.AddRange(New Object() {"CONSOLIDATED INDEX", "SUBJECT INDEX"})
        Me.cboSelection.Location = New System.Drawing.Point(755, 20)
        Me.cboSelection.Name = "cboSelection"
        Me.cboSelection.Size = New System.Drawing.Size(183, 23)
        Me.cboSelection.TabIndex = 67
        Me.cboSelection.Text = "CONSOLIDATED INDEX"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Gold
        Me.Label11.Location = New System.Drawing.Point(632, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(126, 15)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "SELECT OPTION : "
        '
        'cbSubjectIndex
        '
        Me.cbSubjectIndex.AutoSize = True
        Me.cbSubjectIndex.Checked = True
        Me.cbSubjectIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSubjectIndex.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbSubjectIndex.Location = New System.Drawing.Point(290, 212)
        Me.cbSubjectIndex.Name = "cbSubjectIndex"
        Me.cbSubjectIndex.Size = New System.Drawing.Size(100, 19)
        Me.cbSubjectIndex.TabIndex = 65
        Me.cbSubjectIndex.Text = "Subject Index"
        Me.cbSubjectIndex.UseVisualStyleBackColor = True
        '
        'cbRefLegislation
        '
        Me.cbRefLegislation.AutoSize = True
        Me.cbRefLegislation.Checked = True
        Me.cbRefLegislation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbRefLegislation.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbRefLegislation.Location = New System.Drawing.Point(152, 212)
        Me.cbRefLegislation.Name = "cbRefLegislation"
        Me.cbRefLegislation.Size = New System.Drawing.Size(137, 19)
        Me.cbRefLegislation.TabIndex = 64
        Me.cbRefLegislation.Text = "Referred Legislation"
        Me.cbRefLegislation.UseVisualStyleBackColor = True
        '
        'cbRefCases
        '
        Me.cbRefCases.AutoSize = True
        Me.cbRefCases.Checked = True
        Me.cbRefCases.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbRefCases.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbRefCases.Location = New System.Drawing.Point(35, 212)
        Me.cbRefCases.Name = "cbRefCases"
        Me.cbRefCases.Size = New System.Drawing.Size(111, 19)
        Me.cbRefCases.TabIndex = 63
        Me.cbRefCases.Text = "Referred Cases"
        Me.cbRefCases.UseVisualStyleBackColor = True
        '
        'cbSkipError
        '
        Me.cbSkipError.AutoSize = True
        Me.cbSkipError.Checked = True
        Me.cbSkipError.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSkipError.Enabled = False
        Me.cbSkipError.ForeColor = System.Drawing.Color.Red
        Me.cbSkipError.Location = New System.Drawing.Point(35, 278)
        Me.cbSkipError.Name = "cbSkipError"
        Me.cbSkipError.Size = New System.Drawing.Size(80, 19)
        Me.cbSkipError.TabIndex = 60
        Me.cbSkipError.Text = "Skip Error"
        Me.cbSkipError.UseVisualStyleBackColor = True
        '
        'cbDeleteData
        '
        Me.cbDeleteData.AutoSize = True
        Me.cbDeleteData.Checked = True
        Me.cbDeleteData.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbDeleteData.ForeColor = System.Drawing.Color.Red
        Me.cbDeleteData.Location = New System.Drawing.Point(121, 278)
        Me.cbDeleteData.Name = "cbDeleteData"
        Me.cbDeleteData.Size = New System.Drawing.Size(113, 19)
        Me.cbDeleteData.TabIndex = 59
        Me.cbDeleteData.Text = "Delete Old Data"
        Me.cbDeleteData.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(191, 337)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(150, 35)
        Me.btnPreview.TabIndex = 58
        Me.btnPreview.Text = "Preview Data"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Gold
        Me.Label6.Location = New System.Drawing.Point(8, 464)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(284, 15)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "6. Select section you want && Click Generate"
        '
        'btnExtract
        '
        Me.btnExtract.Location = New System.Drawing.Point(34, 234)
        Me.btnExtract.Name = "btnExtract"
        Me.btnExtract.Size = New System.Drawing.Size(150, 35)
        Me.btnExtract.TabIndex = 56
        Me.btnExtract.Text = "Start Extract"
        Me.btnExtract.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Gold
        Me.Label2.Location = New System.Drawing.Point(8, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 15)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "1. "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Snow
        Me.Label10.Location = New System.Drawing.Point(652, 599)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(20, 29)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = ","
        '
        'btnExtractorTest
        '
        Me.btnExtractorTest.Location = New System.Drawing.Point(456, 640)
        Me.btnExtractorTest.Name = "btnExtractorTest"
        Me.btnExtractorTest.Size = New System.Drawing.Size(190, 37)
        Me.btnExtractorTest.TabIndex = 67
        Me.btnExtractorTest.Text = "Legis Extract Test"
        Me.btnExtractorTest.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(676, 599)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(190, 35)
        Me.TextBox2.TabIndex = 66
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(872, 599)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 35)
        Me.Button1.TabIndex = 62
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(456, 599)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(190, 35)
        Me.TextBox1.TabIndex = 61
        '
        'btnExtractRefCases
        '
        Me.btnExtractRefCases.Location = New System.Drawing.Point(352, 293)
        Me.btnExtractRefCases.Name = "btnExtractRefCases"
        Me.btnExtractRefCases.Size = New System.Drawing.Size(182, 50)
        Me.btnExtractRefCases.TabIndex = 56
        Me.btnExtractRefCases.Text = "Extract Referred Cases"
        Me.btnExtractRefCases.UseVisualStyleBackColor = True
        '
        'btnExtractRefLegislation
        '
        Me.btnExtractRefLegislation.Location = New System.Drawing.Point(540, 293)
        Me.btnExtractRefLegislation.Name = "btnExtractRefLegislation"
        Me.btnExtractRefLegislation.Size = New System.Drawing.Size(182, 50)
        Me.btnExtractRefLegislation.TabIndex = 57
        Me.btnExtractRefLegislation.Text = "Extract Referred Legislation"
        Me.btnExtractRefLegislation.UseVisualStyleBackColor = True
        '
        'btnExtractSubjectIndex
        '
        Me.btnExtractSubjectIndex.Location = New System.Drawing.Point(731, 293)
        Me.btnExtractSubjectIndex.Name = "btnExtractSubjectIndex"
        Me.btnExtractSubjectIndex.Size = New System.Drawing.Size(182, 50)
        Me.btnExtractSubjectIndex.TabIndex = 58
        Me.btnExtractSubjectIndex.Text = "Extract Subject Index"
        Me.btnExtractSubjectIndex.UseVisualStyleBackColor = True
        '
        'cbErrLegislation
        '
        Me.cbErrLegislation.AutoSize = True
        Me.cbErrLegislation.Checked = True
        Me.cbErrLegislation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbErrLegislation.Enabled = False
        Me.cbErrLegislation.Location = New System.Drawing.Point(540, 369)
        Me.cbErrLegislation.Name = "cbErrLegislation"
        Me.cbErrLegislation.Size = New System.Drawing.Size(80, 19)
        Me.cbErrLegislation.TabIndex = 64
        Me.cbErrLegislation.Text = "Skip Error"
        Me.cbErrLegislation.UseVisualStyleBackColor = True
        '
        'chkboxdeldata
        '
        Me.chkboxdeldata.AutoSize = True
        Me.chkboxdeldata.Checked = True
        Me.chkboxdeldata.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxdeldata.ForeColor = System.Drawing.Color.Red
        Me.chkboxdeldata.Location = New System.Drawing.Point(731, 349)
        Me.chkboxdeldata.Name = "chkboxdeldata"
        Me.chkboxdeldata.Size = New System.Drawing.Size(113, 19)
        Me.chkboxdeldata.TabIndex = 59
        Me.chkboxdeldata.Text = "Delete Old Data"
        Me.chkboxdeldata.UseVisualStyleBackColor = True
        '
        'cbErrXML
        '
        Me.cbErrXML.AutoSize = True
        Me.cbErrXML.Checked = True
        Me.cbErrXML.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbErrXML.Enabled = False
        Me.cbErrXML.Location = New System.Drawing.Point(352, 369)
        Me.cbErrXML.Name = "cbErrXML"
        Me.cbErrXML.Size = New System.Drawing.Size(80, 19)
        Me.cbErrXML.TabIndex = 63
        Me.cbErrXML.Text = "Skip Error"
        Me.cbErrXML.UseVisualStyleBackColor = True
        '
        'chkboxdeldatacases
        '
        Me.chkboxdeldatacases.AutoSize = True
        Me.chkboxdeldatacases.Checked = True
        Me.chkboxdeldatacases.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxdeldatacases.ForeColor = System.Drawing.Color.Red
        Me.chkboxdeldatacases.Location = New System.Drawing.Point(352, 349)
        Me.chkboxdeldatacases.Name = "chkboxdeldatacases"
        Me.chkboxdeldatacases.Size = New System.Drawing.Size(113, 19)
        Me.chkboxdeldatacases.TabIndex = 60
        Me.chkboxdeldatacases.Text = "Delete Old Data"
        Me.chkboxdeldatacases.UseVisualStyleBackColor = True
        '
        'cbErrSubjectIndex
        '
        Me.cbErrSubjectIndex.AutoSize = True
        Me.cbErrSubjectIndex.Checked = True
        Me.cbErrSubjectIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbErrSubjectIndex.Enabled = False
        Me.cbErrSubjectIndex.Location = New System.Drawing.Point(731, 369)
        Me.cbErrSubjectIndex.Name = "cbErrSubjectIndex"
        Me.cbErrSubjectIndex.Size = New System.Drawing.Size(80, 19)
        Me.cbErrSubjectIndex.TabIndex = 62
        Me.cbErrSubjectIndex.Text = "Skip Error"
        Me.cbErrSubjectIndex.UseVisualStyleBackColor = True
        '
        'chkboxdeldatalegislation
        '
        Me.chkboxdeldatalegislation.AutoSize = True
        Me.chkboxdeldatalegislation.Checked = True
        Me.chkboxdeldatalegislation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxdeldatalegislation.ForeColor = System.Drawing.Color.Red
        Me.chkboxdeldatalegislation.Location = New System.Drawing.Point(540, 349)
        Me.chkboxdeldatalegislation.Name = "chkboxdeldatalegislation"
        Me.chkboxdeldatalegislation.Size = New System.Drawing.Size(113, 19)
        Me.chkboxdeldatalegislation.TabIndex = 61
        Me.chkboxdeldatalegislation.Text = "Delete Old Data"
        Me.chkboxdeldatalegislation.UseVisualStyleBackColor = True
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(620, 514)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(266, 21)
        Me.TextBox4.TabIndex = 69
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkBlue
        Me.ClientSize = New System.Drawing.Size(1264, 681)
        Me.Controls.Add(Me.btnExtractorTest)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.lblProgressTitle)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lstProgress)
        Me.Controls.Add(Me.btnExtractRefCases)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnExtractRefLegislation)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnExtractSubjectIndex)
        Me.Controls.Add(Me.cbErrLegislation)
        Me.Controls.Add(Me.chkboxdeldata)
        Me.Controls.Add(Me.cbErrXML)
        Me.Controls.Add(Me.chkboxdeldatacases)
        Me.Controls.Add(Me.cbErrSubjectIndex)
        Me.Controls.Add(Me.chkboxdeldatalegislation)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consolidated Index Tool (Patch : 25 May 2015)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Private WithEvents lblfn As System.Windows.Forms.Label
    Private WithEvents saveFolderPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnCreateXML As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnPath As System.Windows.Forms.Button
    Friend WithEvents sfd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lstProgress As System.Windows.Forms.ListBox
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents btnPrepare As System.Windows.Forms.Button
    Friend WithEvents cbTableContent As System.Windows.Forms.CheckBox
    Friend WithEvents cbCasesJud As System.Windows.Forms.CheckBox
    Friend WithEvents cbLegisJud As System.Windows.Forms.CheckBox
    Friend WithEvents cbSubIndex As System.Windows.Forms.CheckBox
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents lblProgressTitle As System.Windows.Forms.Label
    Private WithEvents lblPercent As System.Windows.Forms.Label
    Friend WithEvents cbOpenOutput As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btnExtract As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents bgWork As System.ComponentModel.BackgroundWorker
    Friend WithEvents cbSkipError As System.Windows.Forms.CheckBox
    Private WithEvents cbDeleteData As System.Windows.Forms.CheckBox
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Private WithEvents btnExtractRefCases As System.Windows.Forms.Button
    Private WithEvents btnExtractRefLegislation As System.Windows.Forms.Button
    Private WithEvents btnExtractSubjectIndex As System.Windows.Forms.Button
    Friend WithEvents cbErrLegislation As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldata As System.Windows.Forms.CheckBox
    Friend WithEvents cbErrXML As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldatacases As System.Windows.Forms.CheckBox
    Friend WithEvents cbErrSubjectIndex As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldatalegislation As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents cbSubjectIndex As System.Windows.Forms.CheckBox
    Friend WithEvents cbRefLegislation As System.Windows.Forms.CheckBox
    Friend WithEvents cbRefCases As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnExtractorTest As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboSelection As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox

End Class
