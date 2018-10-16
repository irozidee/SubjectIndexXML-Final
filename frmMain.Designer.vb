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
        Me.cbTranslateSubjectIndex = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txMain = New System.Windows.Forms.TextBox()
        Me.txt3 = New System.Windows.Forms.TextBox()
        Me.btnExtractorTest = New System.Windows.Forms.Button()
        Me.txt1 = New System.Windows.Forms.TextBox()
        Me.txt2 = New System.Windows.Forms.TextBox()
        Me.cboSelection = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbSubjectIndex = New System.Windows.Forms.CheckBox()
        Me.cbRefLegislation = New System.Windows.Forms.CheckBox()
        Me.cbRefCases = New System.Windows.Forms.CheckBox()
        Me.cbSkipError = New System.Windows.Forms.CheckBox()
        Me.cbDeleteData = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnExtract = New System.Windows.Forms.Button()
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
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OPTIONSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UpdateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeywordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LegislationsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubjectIndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DEVELOPERSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXTRACTREFLEGISToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXTRACTREFCASESToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXTRACTSUBJECTINDEXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cbPCases = New System.Windows.Forms.CheckBox()
        Me.cbPLegislation = New System.Windows.Forms.CheckBox()
        Me.cbPSubjectIndex = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(22, 343)
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
        Me.lblfn.Location = New System.Drawing.Point(111, 123)
        Me.lblfn.Name = "lblfn"
        Me.lblfn.Size = New System.Drawing.Size(83, 14)
        Me.lblfn.TabIndex = 18
        Me.lblfn.Text = "* FileName : "
        '
        'btnCreateXML
        '
        Me.btnCreateXML.Location = New System.Drawing.Point(563, 474)
        Me.btnCreateXML.Name = "btnCreateXML"
        Me.btnCreateXML.Size = New System.Drawing.Size(150, 36)
        Me.btnCreateXML.TabIndex = 19
        Me.btnCreateXML.Text = "Generate XML"
        Me.btnCreateXML.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Gold
        Me.Label1.Location = New System.Drawing.Point(8, 152)
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
        Me.Label3.Location = New System.Drawing.Point(10, 282)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(201, 15)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "4. Click Button to prepare data"
        '
        'btnPath
        '
        Me.btnPath.Location = New System.Drawing.Point(598, 369)
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
        Me.Label5.Location = New System.Drawing.Point(8, 345)
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
        Me.lstProgress.Location = New System.Drawing.Point(973, 35)
        Me.lstProgress.Name = "lstProgress"
        Me.lstProgress.Size = New System.Drawing.Size(290, 646)
        Me.lstProgress.TabIndex = 27
        '
        'txtPath
        '
        Me.txtPath.BackColor = System.Drawing.Color.OrangeRed
        Me.txtPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPath.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtPath.Location = New System.Drawing.Point(112, 99)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.ReadOnly = True
        Me.txtPath.Size = New System.Drawing.Size(482, 21)
        Me.txtPath.TabIndex = 38
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(598, 98)
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
        Me.Label7.Location = New System.Drawing.Point(8, 81)
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
        Me.Label8.Location = New System.Drawing.Point(32, 106)
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
        Me.btnPrepare.Location = New System.Drawing.Point(563, 303)
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
        Me.cbTableContent.Location = New System.Drawing.Point(35, 439)
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
        Me.cbCasesJud.Location = New System.Drawing.Point(35, 464)
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
        Me.cbLegisJud.Location = New System.Drawing.Point(35, 487)
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
        Me.cbSubIndex.Location = New System.Drawing.Point(35, 512)
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
        Me.txtOutput.Location = New System.Drawing.Point(112, 370)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.ReadOnly = True
        Me.txtOutput.Size = New System.Drawing.Size(482, 21)
        Me.txtOutput.TabIndex = 49
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.Label4.Location = New System.Drawing.Point(32, 372)
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
        Me.Label9.Location = New System.Drawing.Point(24, 602)
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
        Me.lblProgressTitle.Location = New System.Drawing.Point(24, 644)
        Me.lblProgressTitle.Name = "lblProgressTitle"
        Me.lblProgressTitle.Size = New System.Drawing.Size(114, 16)
        Me.lblProgressTitle.TabIndex = 52
        Me.lblProgressTitle.Text = "Title In Progress"
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
        Me.cbOpenOutput.Location = New System.Drawing.Point(3, 540)
        Me.cbOpenOutput.Name = "cbOpenOutput"
        Me.cbOpenOutput.Size = New System.Drawing.Size(191, 17)
        Me.cbOpenOutput.TabIndex = 54
        Me.cbOpenOutput.Text = "Open Output Folder After Complete"
        Me.cbOpenOutput.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbPSubjectIndex)
        Me.GroupBox1.Controls.Add(Me.cbPLegislation)
        Me.GroupBox1.Controls.Add(Me.cbPCases)
        Me.GroupBox1.Controls.Add(Me.cbTranslateSubjectIndex)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.cboSelection)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cbSubjectIndex)
        Me.GroupBox1.Controls.Add(Me.cbRefLegislation)
        Me.GroupBox1.Controls.Add(Me.cbRefCases)
        Me.GroupBox1.Controls.Add(Me.cbSkipError)
        Me.GroupBox1.Controls.Add(Me.cbDeleteData)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnExtract)
        Me.GroupBox1.Controls.Add(Me.cbOpenOutput)
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
        Me.GroupBox1.Location = New System.Drawing.Point(9, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(959, 563)
        Me.GroupBox1.TabIndex = 55
        Me.GroupBox1.TabStop = False
        '
        'cbTranslateSubjectIndex
        '
        Me.cbTranslateSubjectIndex.AutoSize = True
        Me.cbTranslateSubjectIndex.Checked = True
        Me.cbTranslateSubjectIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbTranslateSubjectIndex.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbTranslateSubjectIndex.Location = New System.Drawing.Point(50, 253)
        Me.cbTranslateSubjectIndex.Name = "cbTranslateSubjectIndex"
        Me.cbTranslateSubjectIndex.Size = New System.Drawing.Size(224, 19)
        Me.cbTranslateSubjectIndex.TabIndex = 71
        Me.cbTranslateSubjectIndex.Text = "Include Translation for Subject Index"
        Me.cbTranslateSubjectIndex.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.txMain)
        Me.GroupBox2.Controls.Add(Me.txt3)
        Me.GroupBox2.Controls.Add(Me.btnExtractorTest)
        Me.GroupBox2.Controls.Add(Me.txt1)
        Me.GroupBox2.Controls.Add(Me.txt2)
        Me.GroupBox2.Controls.Add(Me.btnUpdate)
        Me.GroupBox2.Location = New System.Drawing.Point(737, 44)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(221, 403)
        Me.GroupBox2.TabIndex = 70
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(130, 290)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 71
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(16, 291)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 21)
        Me.TextBox1.TabIndex = 72
        '
        'txMain
        '
        Me.txMain.BackColor = System.Drawing.Color.DarkSalmon
        Me.txMain.Location = New System.Drawing.Point(16, 131)
        Me.txMain.Multiline = True
        Me.txMain.Name = "txMain"
        Me.txMain.Size = New System.Drawing.Size(189, 35)
        Me.txMain.TabIndex = 69
        '
        'txt3
        '
        Me.txt3.BackColor = System.Drawing.Color.DarkSalmon
        Me.txt3.Location = New System.Drawing.Point(17, 218)
        Me.txt3.Multiline = True
        Me.txt3.Name = "txt3"
        Me.txt3.Size = New System.Drawing.Size(189, 35)
        Me.txt3.TabIndex = 68
        '
        'btnExtractorTest
        '
        Me.btnExtractorTest.BackColor = System.Drawing.Color.SteelBlue
        Me.btnExtractorTest.Location = New System.Drawing.Point(17, 174)
        Me.btnExtractorTest.Name = "btnExtractorTest"
        Me.btnExtractorTest.Size = New System.Drawing.Size(190, 37)
        Me.btnExtractorTest.TabIndex = 67
        Me.btnExtractorTest.Text = "Legis Extract Test"
        Me.btnExtractorTest.UseVisualStyleBackColor = False
        '
        'txt1
        '
        Me.txt1.BackColor = System.Drawing.Color.LightCoral
        Me.txt1.Location = New System.Drawing.Point(17, 24)
        Me.txt1.Multiline = True
        Me.txt1.Name = "txt1"
        Me.txt1.Size = New System.Drawing.Size(189, 35)
        Me.txt1.TabIndex = 61
        Me.txt1.Text = "O 15A(1)(b)"
        '
        'txt2
        '
        Me.txt2.BackColor = System.Drawing.SystemColors.Info
        Me.txt2.Location = New System.Drawing.Point(17, 67)
        Me.txt2.Multiline = True
        Me.txt2.Name = "txt2"
        Me.txt2.Size = New System.Drawing.Size(189, 35)
        Me.txt2.TabIndex = 66
        Me.txt2.Text = "2(c)"
        '
        'cboSelection
        '
        Me.cboSelection.FormattingEnabled = True
        Me.cboSelection.Items.AddRange(New Object() {"CONSOLIDATED INDEX", "SUBJECT INDEX"})
        Me.cboSelection.Location = New System.Drawing.Point(152, 31)
        Me.cboSelection.Name = "cboSelection"
        Me.cboSelection.Size = New System.Drawing.Size(182, 23)
        Me.cboSelection.TabIndex = 67
        Me.cboSelection.Text = "CONSOLIDATED INDEX"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Gold
        Me.Label11.Location = New System.Drawing.Point(8, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(142, 15)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "1. SELECT OPTION : "
        '
        'cbSubjectIndex
        '
        Me.cbSubjectIndex.AutoSize = True
        Me.cbSubjectIndex.Checked = True
        Me.cbSubjectIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSubjectIndex.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbSubjectIndex.Location = New System.Drawing.Point(35, 229)
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
        Me.cbRefLegislation.Location = New System.Drawing.Point(35, 179)
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
        Me.cbRefCases.Location = New System.Drawing.Point(35, 204)
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
        Me.cbSkipError.Location = New System.Drawing.Point(563, 191)
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
        Me.cbDeleteData.Location = New System.Drawing.Point(563, 215)
        Me.cbDeleteData.Name = "cbDeleteData"
        Me.cbDeleteData.Size = New System.Drawing.Size(113, 19)
        Me.cbDeleteData.TabIndex = 59
        Me.cbDeleteData.Text = "Delete Old Data"
        Me.cbDeleteData.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Gold
        Me.Label6.Location = New System.Drawing.Point(8, 415)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(284, 15)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "6. Select section you want && Click Generate"
        '
        'btnExtract
        '
        Me.btnExtract.Location = New System.Drawing.Point(563, 241)
        Me.btnExtract.Name = "btnExtract"
        Me.btnExtract.Size = New System.Drawing.Size(150, 31)
        Me.btnExtract.TabIndex = 56
        Me.btnExtract.Text = "Start Extract"
        Me.btnExtract.UseVisualStyleBackColor = True
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
        Me.chkboxdeldata.Location = New System.Drawing.Point(731, 348)
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
        Me.chkboxdeldatacases.Location = New System.Drawing.Point(352, 348)
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
        Me.chkboxdeldatalegislation.Location = New System.Drawing.Point(540, 348)
        Me.chkboxdeldatalegislation.Name = "chkboxdeldatalegislation"
        Me.chkboxdeldatalegislation.Size = New System.Drawing.Size(113, 19)
        Me.chkboxdeldatalegislation.TabIndex = 61
        Me.chkboxdeldatalegislation.Text = "Delete Old Data"
        Me.chkboxdeldatalegislation.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.AutoSize = False
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OPTIONSToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.SettingToolStripMenuItem, Me.DEVELOPERSToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Padding = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MenuStrip1.Size = New System.Drawing.Size(1263, 35)
        Me.MenuStrip1.TabIndex = 65
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OPTIONSToolStripMenuItem
        '
        Me.OPTIONSToolStripMenuItem.BackColor = System.Drawing.Color.Transparent
        Me.OPTIONSToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.OPTIONSToolStripMenuItem.Name = "OPTIONSToolStripMenuItem"
        Me.OPTIONSToolStripMenuItem.Size = New System.Drawing.Size(37, 25)
        Me.OPTIONSToolStripMenuItem.Text = "&File"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(92, 22)
        Me.ExitToolStripMenuItem.Text = "&Exit"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataToolStripMenuItem, Me.KeywordsToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 25)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'DataToolStripMenuItem
        '
        Me.DataToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpdateToolStripMenuItem, Me.PreviewToolStripMenuItem})
        Me.DataToolStripMenuItem.Name = "DataToolStripMenuItem"
        Me.DataToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.DataToolStripMenuItem.Text = "&Data"
        '
        'UpdateToolStripMenuItem
        '
        Me.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem"
        Me.UpdateToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.UpdateToolStripMenuItem.Text = "&Update"
        '
        'PreviewToolStripMenuItem
        '
        Me.PreviewToolStripMenuItem.Name = "PreviewToolStripMenuItem"
        Me.PreviewToolStripMenuItem.Size = New System.Drawing.Size(115, 22)
        Me.PreviewToolStripMenuItem.Text = "&Preview"
        '
        'KeywordsToolStripMenuItem
        '
        Me.KeywordsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LegislationsToolStripMenuItem, Me.SubjectIndexToolStripMenuItem})
        Me.KeywordsToolStripMenuItem.Name = "KeywordsToolStripMenuItem"
        Me.KeywordsToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.KeywordsToolStripMenuItem.Text = "&Keywords"
        '
        'LegislationsToolStripMenuItem
        '
        Me.LegislationsToolStripMenuItem.Name = "LegislationsToolStripMenuItem"
        Me.LegislationsToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.LegislationsToolStripMenuItem.Text = "&Legislations"
        '
        'SubjectIndexToolStripMenuItem
        '
        Me.SubjectIndexToolStripMenuItem.Name = "SubjectIndexToolStripMenuItem"
        Me.SubjectIndexToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.SubjectIndexToolStripMenuItem.Text = "&Subject Index"
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfigurationToolStripMenuItem})
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(56, 25)
        Me.SettingToolStripMenuItem.Text = "&Setting"
        '
        'ConfigurationToolStripMenuItem
        '
        Me.ConfigurationToolStripMenuItem.Name = "ConfigurationToolStripMenuItem"
        Me.ConfigurationToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ConfigurationToolStripMenuItem.Text = "&Configuration"
        '
        'DEVELOPERSToolStripMenuItem
        '
        Me.DEVELOPERSToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EXTRACTREFLEGISToolStripMenuItem, Me.EXTRACTREFCASESToolStripMenuItem, Me.EXTRACTSUBJECTINDEXToolStripMenuItem})
        Me.DEVELOPERSToolStripMenuItem.Name = "DEVELOPERSToolStripMenuItem"
        Me.DEVELOPERSToolStripMenuItem.Size = New System.Drawing.Size(87, 25)
        Me.DEVELOPERSToolStripMenuItem.Text = "DEVELOPERS"
        '
        'EXTRACTREFLEGISToolStripMenuItem
        '
        Me.EXTRACTREFLEGISToolStripMenuItem.Name = "EXTRACTREFLEGISToolStripMenuItem"
        Me.EXTRACTREFLEGISToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.EXTRACTREFLEGISToolStripMenuItem.Text = "EXTRACT REF LEGIS"
        '
        'EXTRACTREFCASESToolStripMenuItem
        '
        Me.EXTRACTREFCASESToolStripMenuItem.Name = "EXTRACTREFCASESToolStripMenuItem"
        Me.EXTRACTREFCASESToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.EXTRACTREFCASESToolStripMenuItem.Text = "EXTRACT REF CASES"
        '
        'EXTRACTSUBJECTINDEXToolStripMenuItem
        '
        Me.EXTRACTSUBJECTINDEXToolStripMenuItem.Name = "EXTRACTSUBJECTINDEXToolStripMenuItem"
        Me.EXTRACTSUBJECTINDEXToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.EXTRACTSUBJECTINDEXToolStripMenuItem.Text = "EXTRACT SUBJECT INDEX"
        '
        'cbPCases
        '
        Me.cbPCases.AutoSize = True
        Me.cbPCases.Checked = True
        Me.cbPCases.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbPCases.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbPCases.Location = New System.Drawing.Point(30, 303)
        Me.cbPCases.Name = "cbPCases"
        Me.cbPCases.Size = New System.Drawing.Size(89, 19)
        Me.cbPCases.TabIndex = 72
        Me.cbPCases.Text = "Cases Data"
        Me.cbPCases.UseVisualStyleBackColor = True
        '
        'cbPLegislation
        '
        Me.cbPLegislation.AutoSize = True
        Me.cbPLegislation.Checked = True
        Me.cbPLegislation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbPLegislation.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbPLegislation.Location = New System.Drawing.Point(125, 303)
        Me.cbPLegislation.Name = "cbPLegislation"
        Me.cbPLegislation.Size = New System.Drawing.Size(115, 19)
        Me.cbPLegislation.TabIndex = 73
        Me.cbPLegislation.Text = "Legislation Data"
        Me.cbPLegislation.UseVisualStyleBackColor = True
        '
        'cbPSubjectIndex
        '
        Me.cbPSubjectIndex.AutoSize = True
        Me.cbPSubjectIndex.Checked = True
        Me.cbPSubjectIndex.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbPSubjectIndex.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cbPSubjectIndex.Location = New System.Drawing.Point(246, 303)
        Me.cbPSubjectIndex.Name = "cbPSubjectIndex"
        Me.cbPSubjectIndex.Size = New System.Drawing.Size(129, 19)
        Me.cbPSubjectIndex.TabIndex = 74
        Me.cbPSubjectIndex.Text = "Subject Index Data"
        Me.cbPSubjectIndex.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.DarkBlue
        Me.ClientSize = New System.Drawing.Size(1263, 681)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.lblProgressTitle)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lstProgress)
        Me.Controls.Add(Me.btnExtractRefCases)
        Me.Controls.Add(Me.btnExtractRefLegislation)
        Me.Controls.Add(Me.btnExtractSubjectIndex)
        Me.Controls.Add(Me.cbErrLegislation)
        Me.Controls.Add(Me.chkboxdeldata)
        Me.Controls.Add(Me.cbErrXML)
        Me.Controls.Add(Me.chkboxdeldatacases)
        Me.Controls.Add(Me.cbErrSubjectIndex)
        Me.Controls.Add(Me.chkboxdeldatalegislation)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consolidated Index Tool (Patch : 05 September 2018)"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
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
    Private WithEvents btnExtract As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents bgWork As System.ComponentModel.BackgroundWorker
    Friend WithEvents cbSkipError As System.Windows.Forms.CheckBox
    Private WithEvents cbDeleteData As System.Windows.Forms.CheckBox
    Private WithEvents btnExtractRefCases As System.Windows.Forms.Button
    Private WithEvents btnExtractRefLegislation As System.Windows.Forms.Button
    Private WithEvents btnExtractSubjectIndex As System.Windows.Forms.Button
    Friend WithEvents cbErrLegislation As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldata As System.Windows.Forms.CheckBox
    Friend WithEvents cbErrXML As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldatacases As System.Windows.Forms.CheckBox
    Friend WithEvents cbErrSubjectIndex As System.Windows.Forms.CheckBox
    Private WithEvents chkboxdeldatalegislation As System.Windows.Forms.CheckBox
    Friend WithEvents txt1 As System.Windows.Forms.TextBox
    Friend WithEvents cbSubjectIndex As System.Windows.Forms.CheckBox
    Friend WithEvents cbRefLegislation As System.Windows.Forms.CheckBox
    Friend WithEvents cbRefCases As System.Windows.Forms.CheckBox
    Friend WithEvents txt2 As System.Windows.Forms.TextBox
    Friend WithEvents btnExtractorTest As System.Windows.Forms.Button
    Friend WithEvents cboSelection As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt3 As System.Windows.Forms.TextBox
    Friend WithEvents txMain As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents OPTIONSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents DEVELOPERSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EXTRACTREFLEGISToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EXTRACTREFCASESToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EXTRACTSUBJECTINDEXToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PreviewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KeywordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LegislationsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SubjectIndexToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbTranslateSubjectIndex As System.Windows.Forms.CheckBox
    Friend WithEvents SettingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfigurationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cbPSubjectIndex As System.Windows.Forms.CheckBox
    Friend WithEvents cbPLegislation As System.Windows.Forms.CheckBox
    Friend WithEvents cbPCases As System.Windows.Forms.CheckBox

End Class
