<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DataLoggingGraph
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GraphPictureBox = New System.Windows.Forms.PictureBox()
        Me.ButtonGroupBox = New System.Windows.Forms.GroupBox()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.GraphButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.FilePathStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ButtonGroupBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'GraphPictureBox
        '
        Me.GraphPictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GraphPictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.GraphPictureBox.Location = New System.Drawing.Point(29, 67)
        Me.GraphPictureBox.Name = "GraphPictureBox"
        Me.GraphPictureBox.Size = New System.Drawing.Size(2056, 648)
        Me.GraphPictureBox.TabIndex = 0
        Me.GraphPictureBox.TabStop = False
        '
        'ButtonGroupBox
        '
        Me.ButtonGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonGroupBox.Controls.Add(Me.ExitButton)
        Me.ButtonGroupBox.Controls.Add(Me.GraphButton)
        Me.ButtonGroupBox.Controls.Add(Me.Button1)
        Me.ButtonGroupBox.Location = New System.Drawing.Point(1040, 721)
        Me.ButtonGroupBox.Name = "ButtonGroupBox"
        Me.ButtonGroupBox.Size = New System.Drawing.Size(731, 154)
        Me.ButtonGroupBox.TabIndex = 1
        Me.ButtonGroupBox.TabStop = False
        '
        'ExitButton
        '
        Me.ExitButton.Location = New System.Drawing.Point(492, 45)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(137, 63)
        Me.ExitButton.TabIndex = 2
        Me.ExitButton.Text = "&Exit"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'GraphButton
        '
        Me.GraphButton.Location = New System.Drawing.Point(263, 36)
        Me.GraphButton.Name = "GraphButton"
        Me.GraphButton.Size = New System.Drawing.Size(166, 81)
        Me.GraphButton.TabIndex = 1
        Me.GraphButton.Text = "&Graph"
        Me.GraphButton.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 36)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(168, 81)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.GripMargin = New System.Windows.Forms.Padding(2, 2, 0, 2)
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(2135, 33)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(54, 29)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilePathStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 992)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(2135, 32)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'FilePathStatusLabel
        '
        Me.FilePathStatusLabel.Name = "FilePathStatusLabel"
        Me.FilePathStatusLabel.Size = New System.Drawing.Size(180, 25)
        Me.FilePathStatusLabel.Text = "ToolStripStatusLabel1"
        '
        'DataLoggingGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2135, 1024)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ButtonGroupBox)
        Me.Controls.Add(Me.GraphPictureBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "DataLoggingGraph"
        Me.Text = "Graph the Data"
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ButtonGroupBox.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GraphPictureBox As PictureBox
    Friend WithEvents ButtonGroupBox As GroupBox
    Friend WithEvents ExitButton As Button
    Friend WithEvents GraphButton As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents FilePathStatusLabel As ToolStripStatusLabel
End Class
