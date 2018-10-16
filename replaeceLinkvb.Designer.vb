<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class replaeceLinkvb
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(replaeceLinkvb))
        Me.btnPalak = New System.Windows.Forms.Button()
        Me.rtbInput = New System.Windows.Forms.RichTextBox()
        Me.rtbOutput = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'btnPalak
        '
        Me.btnPalak.Location = New System.Drawing.Point(965, 200)
        Me.btnPalak.Name = "btnPalak"
        Me.btnPalak.Size = New System.Drawing.Size(75, 23)
        Me.btnPalak.TabIndex = 1
        Me.btnPalak.Text = "Button1"
        Me.btnPalak.UseVisualStyleBackColor = True
        '
        'rtbInput
        '
        Me.rtbInput.Location = New System.Drawing.Point(12, 12)
        Me.rtbInput.Name = "rtbInput"
        Me.rtbInput.Size = New System.Drawing.Size(1028, 177)
        Me.rtbInput.TabIndex = 2
        Me.rtbInput.Text = resources.GetString("rtbInput.Text")
        Me.rtbInput.WordWrap = False
        '
        'rtbOutput
        '
        Me.rtbOutput.Location = New System.Drawing.Point(12, 243)
        Me.rtbOutput.Name = "rtbOutput"
        Me.rtbOutput.Size = New System.Drawing.Size(1028, 219)
        Me.rtbOutput.TabIndex = 3
        Me.rtbOutput.Text = ""
        '
        'replaeceLinkvb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1052, 474)
        Me.Controls.Add(Me.rtbOutput)
        Me.Controls.Add(Me.rtbInput)
        Me.Controls.Add(Me.btnPalak)
        Me.Name = "replaeceLinkvb"
        Me.Text = "replaeceLinkvb"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPalak As System.Windows.Forms.Button
    Friend WithEvents rtbInput As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbOutput As System.Windows.Forms.RichTextBox
End Class
