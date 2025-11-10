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
        Me.components = New System.ComponentModel.Container()
        Me.GraphPictureBox = New System.Windows.Forms.PictureBox()
        Me.ButtonGroupBox = New System.Windows.Forms.GroupBox()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.GraphButton = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OpenTopMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.FilePathStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ComStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SampleTimer = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.PortsComboBox = New System.Windows.Forms.ComboBox()
        Me.PortsLabel = New System.Windows.Forms.Label()
        Me.COMButton = New System.Windows.Forms.Button()
        Me.AnalogTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SerialTextBox = New System.Windows.Forms.TextBox()
        Me.YAxisTextBox = New System.Windows.Forms.TextBox()
        Me.SerialLabel = New System.Windows.Forms.Label()
        Me.YAxisLabel = New System.Windows.Forms.Label()
        Me.XAxisTextBox = New System.Windows.Forms.TextBox()
        Me.XAxisLabel = New System.Windows.Forms.Label()
        Me.SampleRateLabel = New System.Windows.Forms.Label()
        Me.SampleRateComboBox = New System.Windows.Forms.ComboBox()
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ButtonGroupBox.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
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
        Me.ButtonGroupBox.Location = New System.Drawing.Point(1040, 721)
        Me.ButtonGroupBox.Name = "ButtonGroupBox"
        Me.ButtonGroupBox.Size = New System.Drawing.Size(731, 154)
        Me.ButtonGroupBox.TabIndex = 1
        Me.ButtonGroupBox.TabStop = False
        '
        'ExitButton
        '
        Me.ExitButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExitButton.Location = New System.Drawing.Point(492, 45)
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.Size = New System.Drawing.Size(137, 63)
        Me.ExitButton.TabIndex = 2
        Me.ExitButton.Text = "&Exit"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'GraphButton
        '
        Me.GraphButton.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GraphButton.Location = New System.Drawing.Point(263, 36)
        Me.GraphButton.Name = "GraphButton"
        Me.GraphButton.Size = New System.Drawing.Size(166, 81)
        Me.GraphButton.TabIndex = 1
        Me.GraphButton.Text = "&Graph"
        Me.GraphButton.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(942, 283)
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
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenTopMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(2135, 33)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OpenTopMenuItem
        '
        Me.OpenTopMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem})
        Me.OpenTopMenuItem.Name = "OpenTopMenuItem"
        Me.OpenTopMenuItem.Size = New System.Drawing.Size(54, 32)
        Me.OpenTopMenuItem.Text = "&File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(158, 34)
        Me.OpenToolStripMenuItem.Text = "&Open"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FilePathStatusLabel, Me.ComStatusLabel})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 992)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(2135, 32)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'FilePathStatusLabel
        '
        Me.FilePathStatusLabel.Name = "FilePathStatusLabel"
        Me.FilePathStatusLabel.Size = New System.Drawing.Size(101, 25)
        Me.FilePathStatusLabel.Text = "Log: (none)"
        '
        'ComStatusLabel
        '
        Me.ComStatusLabel.Name = "ComStatusLabel"
        Me.ComStatusLabel.Size = New System.Drawing.Size(169, 25)
        Me.ComStatusLabel.Text = "COM: Disconnected"
        '
        'SampleTimer
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'SerialPort1
        '
        '
        'PortsComboBox
        '
        Me.PortsComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PortsComboBox.FormattingEnabled = True
        Me.PortsComboBox.Location = New System.Drawing.Point(46, 801)
        Me.PortsComboBox.Name = "PortsComboBox"
        Me.PortsComboBox.Size = New System.Drawing.Size(121, 28)
        Me.PortsComboBox.TabIndex = 4
        '
        'PortsLabel
        '
        Me.PortsLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PortsLabel.AutoSize = True
        Me.PortsLabel.Location = New System.Drawing.Point(42, 778)
        Me.PortsLabel.Name = "PortsLabel"
        Me.PortsLabel.Size = New System.Drawing.Size(86, 20)
        Me.PortsLabel.TabIndex = 5
        Me.PortsLabel.Text = "COM Ports"
        '
        'COMButton
        '
        Me.COMButton.Location = New System.Drawing.Point(268, 538)
        Me.COMButton.Name = "COMButton"
        Me.COMButton.Size = New System.Drawing.Size(119, 51)
        Me.COMButton.TabIndex = 6
        Me.COMButton.Text = "Button2"
        Me.COMButton.UseVisualStyleBackColor = True
        '
        'AnalogTimer
        '
        '
        'SerialTextBox
        '
        Me.SerialTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SerialTextBox.Location = New System.Drawing.Point(356, 801)
        Me.SerialTextBox.Name = "SerialTextBox"
        Me.SerialTextBox.Size = New System.Drawing.Size(266, 26)
        Me.SerialTextBox.TabIndex = 7
        '
        'YAxisTextBox
        '
        Me.YAxisTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.YAxisTextBox.Location = New System.Drawing.Point(356, 879)
        Me.YAxisTextBox.Name = "YAxisTextBox"
        Me.YAxisTextBox.Size = New System.Drawing.Size(266, 26)
        Me.YAxisTextBox.TabIndex = 8
        '
        'SerialLabel
        '
        Me.SerialLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SerialLabel.AutoSize = True
        Me.SerialLabel.Location = New System.Drawing.Point(352, 766)
        Me.SerialLabel.Name = "SerialLabel"
        Me.SerialLabel.Size = New System.Drawing.Size(88, 20)
        Me.SerialLabel.TabIndex = 9
        Me.SerialLabel.Text = "Serial Data"
        '
        'YAxisLabel
        '
        Me.YAxisLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.YAxisLabel.AutoSize = True
        Me.YAxisLabel.Location = New System.Drawing.Point(352, 845)
        Me.YAxisLabel.Name = "YAxisLabel"
        Me.YAxisLabel.Size = New System.Drawing.Size(90, 20)
        Me.YAxisLabel.TabIndex = 10
        Me.YAxisLabel.Text = "Y Axis label"
        '
        'XAxisTextBox
        '
        Me.XAxisTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XAxisTextBox.Location = New System.Drawing.Point(356, 937)
        Me.XAxisTextBox.Name = "XAxisTextBox"
        Me.XAxisTextBox.Size = New System.Drawing.Size(266, 26)
        Me.XAxisTextBox.TabIndex = 11
        '
        'XAxisLabel
        '
        Me.XAxisLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XAxisLabel.AutoSize = True
        Me.XAxisLabel.Location = New System.Drawing.Point(352, 914)
        Me.XAxisLabel.Name = "XAxisLabel"
        Me.XAxisLabel.Size = New System.Drawing.Size(53, 20)
        Me.XAxisLabel.TabIndex = 12
        Me.XAxisLabel.Text = "X Axis"
        '
        'SampleRateLabel
        '
        Me.SampleRateLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SampleRateLabel.AutoSize = True
        Me.SampleRateLabel.Location = New System.Drawing.Point(46, 730)
        Me.SampleRateLabel.Name = "SampleRateLabel"
        Me.SampleRateLabel.Size = New System.Drawing.Size(102, 20)
        Me.SampleRateLabel.TabIndex = 13
        Me.SampleRateLabel.Text = "Sample Rate"
        '
        'SampleRateComboBox
        '
        Me.SampleRateComboBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SampleRateComboBox.FormattingEnabled = True
        Me.SampleRateComboBox.Location = New System.Drawing.Point(46, 752)
        Me.SampleRateComboBox.Name = "SampleRateComboBox"
        Me.SampleRateComboBox.Size = New System.Drawing.Size(170, 28)
        Me.SampleRateComboBox.TabIndex = 14
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(121, 32)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(121, 32)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(121, 32)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartToolStripMenuItem, Me.StopToolStripMenuItem, Me.SaveToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(122, 100)
        '
        'DataLoggingGraph
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2135, 1024)
        Me.Controls.Add(Me.SampleRateComboBox)
        Me.Controls.Add(Me.SampleRateLabel)
        Me.Controls.Add(Me.XAxisLabel)
        Me.Controls.Add(Me.XAxisTextBox)
        Me.Controls.Add(Me.YAxisLabel)
        Me.Controls.Add(Me.SerialLabel)
        Me.Controls.Add(Me.YAxisTextBox)
        Me.Controls.Add(Me.SerialTextBox)
        Me.Controls.Add(Me.PortsLabel)
        Me.Controls.Add(Me.PortsComboBox)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ButtonGroupBox)
        Me.Controls.Add(Me.GraphPictureBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.COMButton)
        Me.Controls.Add(Me.Button1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "DataLoggingGraph"
        Me.Text = "Graph the Data"
        CType(Me.GraphPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ButtonGroupBox.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GraphPictureBox As PictureBox
    Friend WithEvents ButtonGroupBox As GroupBox
    Friend WithEvents ExitButton As Button
    Friend WithEvents GraphButton As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents OpenTopMenuItem As ToolStripMenuItem
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents FilePathStatusLabel As ToolStripStatusLabel
    Friend WithEvents ComStatusLabel As ToolStripStatusLabel
    Friend WithEvents SampleTimer As Timer
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SerialPort1 As IO.Ports.SerialPort
    Friend WithEvents PortsComboBox As ComboBox
    Friend WithEvents PortsLabel As Label
    Friend WithEvents COMButton As Button
    Friend WithEvents AnalogTimer As Timer
    Friend WithEvents SerialTextBox As TextBox
    Friend WithEvents YAxisTextBox As TextBox
    Friend WithEvents SerialLabel As Label
    Friend WithEvents YAxisLabel As Label
    Friend WithEvents XAxisTextBox As TextBox
    Friend WithEvents XAxisLabel As Label
    Friend WithEvents SampleRateLabel As Label
    Friend WithEvents SampleRateComboBox As ComboBox
    Friend WithEvents StartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
End Class
