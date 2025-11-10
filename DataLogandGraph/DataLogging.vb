'Jacob Horsley
'RCET 3371
'Datalogging.
'GitHub: https://github.com/horsjaco117/DataLogandGraph/tree/main


Option Strict On
Option Explicit On

Imports System.Media
Imports System.Threading.Thread
Imports System.Runtime.CompilerServices
'Serial communications imports
Imports System.IO.Ports
Imports System.Net.Configuration
Imports System.IO
Imports System.Threading.Tasks
Imports System.Linq

Public Class DataLoggingGraph
    Private LastCommand As Byte
    Private currentX As Single = 0
    Private currentY As Single = 0

    Dim DataBuffer As New Queue(Of Integer)

    ' Thread-safe status updates
    ' Update the COM status label on the UI thread
    Private Sub UpdateComStatus(text As String)
        If Me.IsHandleCreated AndAlso Me.ComStatusLabel IsNot Nothing Then
            If Me.ComStatusLabel.GetCurrentParent().InvokeRequired Then
                Me.ComStatusLabel.GetCurrentParent().Invoke(New Action(Sub() Me.ComStatusLabel.Text = text))
            Else
                Me.ComStatusLabel.Text = text
            End If
        End If
    End Sub

    ' Update the file path status label on the UI thread
    Private Sub UpdateFilePathStatus(path As String)
        If Me.IsHandleCreated AndAlso Me.FilePathStatusLabel IsNot Nothing Then
            Dim txt = If(String.IsNullOrEmpty(path), "Log: (none)", $"Log: {path}")
            If Me.FilePathStatusLabel.GetCurrentParent().InvokeRequired Then
                Me.FilePathStatusLabel.GetCurrentParent().Invoke(New Action(Sub() Me.FilePathStatusLabel.Text = txt))
            Else
                Me.FilePathStatusLabel.Text = txt
            End If
        End If
    End Sub

    'Serial Stuff----------------------------------------------------------------------------
    ' Reset serial port and UI to default disconnected state
    Sub SetDefaults() 'Set's default serial pieces and shows COM ports
        Try
            If SerialPort1.IsOpen Then SerialPort1.Close() 'Closes the COM ports but adds settings
        Catch ex As Exception
        End Try

        ' Update COM status on status strip
        UpdateComStatus("COM: Disconnected")

        GetPorts() 'Shows available ports
    End Sub

    ' Populate the ports combobox with available serial ports
    Sub GetPorts()
#Disable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance
        Dim ports() = SerialPort1.GetPortNames() 'Available ports
#Enable Warning BC42025 ' Access of shared member, constant member, enum member or nested type through an instance

        PortsComboBox.Items.Clear() 'Clears the ports

        For Each port In ports 'For every port available add it to the combobox
            PortsComboBox.Items.Add(port)
        Next

        Try
            PortsComboBox.SelectedIndex = 0
        Catch ex As Exception
            'No results
        End Try
    End Sub

    ' Open the selected serial port and verify device
    Sub connect()
        Try
            If SerialPort1.IsOpen Then SerialPort1.Close() 'Closes the ports 
        Catch ex As Exception
        End Try

        Try 'All these are serial port settings. 
            SerialPort1.BaudRate = 115200
            SerialPort1.Parity = Parity.None
            SerialPort1.StopBits = StopBits.One
            SerialPort1.DataBits = 8
            SerialPort1.PortName = CStr(PortsComboBox.SelectedItem)
            SerialPort1.Open()

            ' Update COM status
            UpdateComStatus($"COM: Connected ({SerialPort1.PortName})")

            If IsQuietBoard() Then 'Confirms that communication is with the Quiet Board
                '     ConnectionStatusLabel.Text = $"Qy@ Connected on {SerialPort1.PortName}"
            Else
                SetDefaults() 'Resets to defaults if not quiet board
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            SetDefaults()
            UpdateComStatus($"COM: Error")
        End Try

    End Sub

    Function IsQuietBoard() As Boolean
        Dim data(0) As Byte 'put bytes into array
        data(0) = &B11110000 'actual data as a byte
        send(data)


        Return True

    End Function
    ' Send raw bytes to the serial port (non-blocking)
    Sub send(data() As Byte)
        SerialPort1.ReadExisting()
        SerialPort1.Write(data, 0, UBound(data))
    End Sub

    Function recieve() As Byte() 'For every byte recieved it will be displayed as the data variable
        Dim data(SerialPort1.BytesToRead) As Byte
        SerialPort1.Read(data, 0, SerialPort1.BytesToRead)
        Return data
    End Function

    ' Send handshake byte to the device
    Sub writeSerial() 'This write sub is especially for handshaking
        Dim data(0) As Byte
        data(0) = &B11110000
        SerialPort1.Write(data, 0, 1)
    End Sub
    ' Handler to connect and initiate handshake when button clicked
    Private Sub COMButton_Click(sender As Object, e As EventArgs) Handles COMButton.Click
        'Establishes the com between the quiet board

        connect()
        writeSerial()
    End Sub
    ' Timer tick handler that requests analog data
    Private Sub AnalogTimer_Tick(sender As Object, e As EventArgs) Handles AnalogTimer.Tick
        AnalogRead3() 'For every timer tick the Analog data is interpreted and read
    End Sub
    ' Request analog data from the device and clear previous text
    Sub AnalogRead3() 'This analog test worked best
        ' Clear the previous data output for a clean read
        If SerialTextBox IsNot Nothing Then
            SerialTextBox.Text = String.Empty
        End If

        ' Clear the specific byte display too
        If YAxisTextBox IsNot Nothing Then
            YAxisTextBox.Text = String.Empty
        End If

        Dim data(0) As Byte
        data(0) = &H53 ' This command tells the device what data to send back
        connect()
        SerialPort1.Write(data, 0, 1)
    End Sub

    ' Handle incoming serial data, parse bytes and update UI
    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        Try
            Dim bytesToRead As Integer = SerialPort1.BytesToRead
            If bytesToRead < 4 Then Exit Sub                 ' not enough data yet

            Dim buffer(bytesToRead - 1) As Byte
            SerialPort1.Read(buffer, 0, bytesToRead)

            Dim hexString As New System.Text.StringBuilder()
            For Each b As Byte In buffer
                hexString.Append(b.ToString("X2") & " ")
            Next

            ' Note: device sends multiple bytes; the ADC's 10-bit value is split across two bytes.
            ' Combine the two bytes for the Y (right) channel into a single 10-bit sample.
            Dim yLow As Integer = If(buffer.Length > 2, CInt(buffer(2)), 1)
            Dim yHigh As Integer = If(buffer.Length > 3, CInt(buffer(3)), 0)
            Dim ySample10 As Integer = ((yHigh << 8) Or yLow) And &H3FF 'mask to 10 bits

            ' Update the text boxes with the hex bytes (thread-safe)
            WriteToTextBox(Me.XAxisTextBox, buffer(0).ToString("X2"))

            ' Show only the last 2 hex byte values from the received buffer in YAxisTextBox
            Dim lastCount As Integer = Math.Min(2, buffer.Length)
            Dim lastBytes = buffer.Skip(buffer.Length - lastCount).Take(lastCount).Select(Function(b) b.ToString("X2"))
            Dim lastHex As String = String.Join(" ", lastBytes)
            WriteToTextBox(Me.YAxisTextBox, lastHex)

            Me.Invoke(Sub()
                          SerialTextBox.AppendText(hexString.ToString() & vbCrLf)
                      End Sub)

            ' Update drawing using values currently present in the hex textboxes
            Me.Invoke(Sub() DrawFromHexTextBoxes()) 'This invoke function safely filters the hex from the text boxes

        Catch ex As Exception
            Console.WriteLine($"Serial read error: {ex.Message}")
        End Try
    End Sub

    ' Parse hex values from textboxes and compute drawing coordinates
    Private Sub DrawFromHexTextBoxes()
        ' Read & sanitize the hex strings
        Dim hx As String = If(XAxisTextBox?.Text, "").Trim()
        Dim hy As String = If(YAxisTextBox?.Text, "").Trim()
        If String.IsNullOrEmpty(hx) Or String.IsNullOrEmpty(hy) Then Return

        ' Normalize case and remove common prefixes
        hx = hx.ToUpperInvariant().Replace("0X", "").Replace("&H", "")

        hx = hx.Split(" "c, CChar(vbTab), CChar(vbCrLf)).FirstOrDefault()
        If String.IsNullOrEmpty(hx) Then Return

        Try
            Dim valX As Integer = Convert.ToInt32(hx, 16) 'supports up to 3 hex digits if supplied
            ' Use combined last two tokens from YAxisTextBox to compute 10-bit sample
            Dim valY As Integer = GetHexValueFromTextBox(Me.YAxisTextBox, 0)

            ' --- DRAWING LIMITS CONFIG ---
            ' Change these four values to move the drawing area.
            Dim leftRatio As Single = 0.1F   ' 10% from left
            Dim rightRatio As Single = 0.9F  ' 10% from right
            Dim topRatio As Single = 0.1F    ' 10% from top
            Dim bottomRatio As Single = 0.9F ' 10% from bottom

            Dim w As Integer = Math.Max(1, GraphPictureBox.Width - 1)
            Dim h As Integer = Math.Max(1, GraphPictureBox.Height - 1)

            Dim minX As Integer = CInt(leftRatio * w)
            Dim maxX As Integer = CInt(rightRatio * w)
            Dim minY As Integer = CInt(topRatio * h)
            Dim maxY As Integer = CInt(bottomRatio * h)

            ' Prevent inverted ranges
            If maxX < minX Then Swap(minX, maxX)
            If maxY < minY Then Swap(minY, maxY)

            ' Use per-axis normalization: if value exceeds 8-bit range, assume 10-bit (0..1023)
            Dim denomX As Single = If(valX > 255, 1023.0F, 255.0F)
            Dim denomY As Single = If(valY > 255, 1023.0F, 255.0F)

            ' Map raw into the constrained rectangle
            Dim mappedX As Integer = minX + CInt(Math.Round((valX / denomX) * (maxX - minX)))
            Dim mappedY As Integer = minY + CInt(Math.Round((valY / denomY) * (maxY - minY)))

            ' Remember old position and update current position
            Dim oldX As Integer = CInt(currentX)
            Dim oldY As Integer = CInt(currentY)

            currentX = Math.Max(0, Math.Min(w, mappedX))
            currentY = Math.Max(0, Math.Min(h, mappedY))

        Catch ex As Exception
            Debug.WriteLine($"DrawFromHexTextBoxes parse error: {ex.Message}")
        End Try
    End Sub

    ' Swap two integer values (utility)
    Private Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim t As Integer = a
        a = b
        b = t
    End Sub
    ' Thread-safe textbox writer
    Public Sub WriteToTextBox(ByVal targetTextBox As System.Windows.Forms.TextBox, ByVal content As String)
        ' Check if the call is coming from a different thread than the one that created the control
        If targetTextBox.InvokeRequired Then
            ' If it is, use Invoke to marshal the call back to the UI thread
            targetTextBox.Invoke(New Action(Sub()
                                                targetTextBox.Text = content
                                            End Sub))
        Else
            ' If it is the UI thread, update directly
            targetTextBox.Text = content
        End If
    End Sub

    'Program logic---------------------------------------------------------------------------
    ' Asynchronously append sensor sample to log file
    Sub LogData(currentSample%)
        ' Offload file IO to a background task to avoid blocking the UI timer
        Try
            Dim fileName = $"SensorData_{DateTime.Now.ToString("yyMMddhh")}.log"
            Dim filePath$ = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName)

            ' Build formatted sample line
            ' Example: "$$AN0 AA BB230501123045123" (yyMMddHHmmssfff)
            Dim highByte As Integer = (currentSample >> 8) And &HFF
            Dim lowByte As Integer = currentSample And &HFF
            Dim timestamp As String = DateTime.Now.ToString("yyMMddHHmmssfff")
            Dim line As String = $"$$AN0 {highByte:X2} {lowByte:X2} {timestamp}{Environment.NewLine}"

            Task.Run(Sub()
                         Try
                             File.AppendAllText(filePath, line)
                             UpdateFilePathStatus(filePath) ' Update the status label with the file path
                         Catch ioEx As Exception
                             Debug.WriteLine("LogData IO error: " & ioEx.Message)
                         End Try
                     End Sub)
        Catch ex As Exception
            Debug.WriteLine("LogData error: " & ex.Message)
        End Try

    End Sub

    ' Show open file dialog and load logged data into buffer
    Sub LoadData()
        Dim choice As DialogResult
        Dim fileNumber% = FreeFile()
        Dim currentRecord$
        Dim temp() As String

        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "log file (*.log)|*.log"
        choice = OpenFileDialog1.ShowDialog()
        If choice = DialogResult.OK Then
            ' MsgBox(OpenFileDialog1.FileName)
            Try
                FileOpen(fileNumber, OpenFileDialog1.FileName, OpenMode.Input)
                Me.DataBuffer.Clear()
                Do Until EOF(fileNumber)
                    currentRecord = LineInput(fileNumber)
                    temp = Split(currentRecord, ",")
                    Me.DataBuffer.Enqueue(CInt(temp(temp.GetUpperBound(0))))
                Loop
                FileClose(fileNumber)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            'cancel
            'MsgBox("Canceled")
        End If
        GraphData()
    End Sub

    Private Function GetHexValueFromTextBox(tb As System.Windows.Forms.TextBox, Optional defaultValue As Integer = 0) As Integer
        If tb Is Nothing Then Return defaultValue
        Dim s As String = tb.Text.Trim()
        If String.IsNullOrEmpty(s) Then Return defaultValue

        ' Normalize and strip common prefixes
        s = s.ToUpperInvariant().Replace("0X", "").Replace("&H", "")
        Dim tokens = s.Split({" "c, vbTab, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        If tokens.Length = 0 Then Return defaultValue

        Try
            If tokens.Length >= 2 Then
                ' combine last two tokens as low, high -> 10-bit sample
                Dim lowS = tokens(tokens.Length - 2)
                Dim highS = tokens(tokens.Length - 1)
                Dim low As Integer = Convert.ToInt32(lowS, 16)
                Dim high As Integer = Convert.ToInt32(highS, 16)
                Dim combined As Integer = ((high << 8) Or low) And &H3FF
                Return combined
            Else
                ' single token -> parse as hex value
                Dim v As Integer = Convert.ToInt32(tokens(0), 16)
                If v < 0 Then v = 0
                If v > 1023 Then v = 1023
                Return v
            End If
        Catch ex As Exception
            ' parsing failed -> return default
            Return defaultValue
        End Try
    End Function

    ' Read the Y-axis hex value and use it as the sample (now supports up to 10-bit values from last two bytes)
    Sub GetData()
        ' Read the Y-axis hex value and use it as the sample (now supports up to 10-bit values from last two bytes)
        Dim sample As Integer = GetHexValueFromTextBox(Me.YAxisTextBox, 0)

        ' Maintain buffer length
        If DataBuffer.Count >= 100 Then
            DataBuffer.Dequeue()
        End If

        Me.DataBuffer.Enqueue(sample)
        LogData(sample)
    End Sub


    ' Render the data buffer into PictureBox image
    Sub GraphData()
        ' Draw into an off-screen bitmap and assign to PictureBox.Image to avoid CreateGraphics resource issues
        Dim w As Integer = Math.Max(1, GraphPictureBox.Width)
        Dim h As Integer = Math.Max(1, GraphPictureBox.Height)

        Dim bmp As New Bitmap(w, h)
        Try
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.Black)
                ' Translate and scale to match old behaviour
                g.TranslateTransform(0, h)
                Dim scaleX! = If(Me.DataBuffer.Count > 0, CSng(w) / Me.DataBuffer.Count, 1.0F)
                Dim adcMax As Integer = 1023 ' 10-bit ADC
                Dim scaleY! = CSng((h / adcMax) * -4)
                g.ScaleTransform(scaleX, scaleY)

                Using pen As New Pen(Color.Lime), eraser As New Pen(Color.Black)
                    pen.Width = 0.25F 'fix pen so it is not too thick

                    Dim oldY% = 0
                    Dim x = -1
                    For Each y In Me.DataBuffer.Reverse
                        x += 1
                        g.DrawLine(eraser, x, 0, x, adcMax)
                        g.DrawLine(pen, x - 1, oldY, x, y)
                        oldY = y
                    Next
                End Using
            End Using

            ' Swap image on UI thread and dispose previous image
            Dim prevImage As Image = Nothing
            If GraphPictureBox.InvokeRequired Then
                GraphPictureBox.Invoke(Sub()
                                           prevImage = GraphPictureBox.Image
                                           GraphPictureBox.Image = bmp
                                       End Sub)
            Else
                prevImage = GraphPictureBox.Image
                GraphPictureBox.Image = bmp
            End If

            If prevImage IsNot Nothing Then
                Try
                    prevImage.Dispose()
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            bmp.Dispose()
            Debug.WriteLine("GraphData error: " & ex.Message)
        End Try

    End Sub



    'Event Handlers--------------------------------------------------------------------------
    ' Close the application window
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    ' Toggle the sampling timer when graph button clicked
    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        If SampleTimer.Enabled Then
            SampleTimer.Stop()
            SampleTimer.Enabled = False

        Else
            SampleTimer.Enabled = True
            SampleTimer.Start()
        End If

    End Sub

    ' Timer tick to capture data and refresh graph
    Private Sub SampleTimer_Tick(sender As Object, e As EventArgs) Handles SampleTimer.Tick
        GetData()
        GraphData()

    End Sub

    ' OpenFileDialog OK handler (unused placeholder)
    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ' LoadData()
    End Sub

    ' Menu item to load data from file
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        LoadData()
    End Sub

    ' Form load initialization: setup serial, timers, and UI controls
    Private Sub DataLoggingGraph_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDefaults() 'Serial communication defaults
        AnalogTimer.Enabled = True  ' Disable hardware polling initially

        currentX = CSng(GraphPictureBox.Width / 2)
        currentY = CSng(GraphPictureBox.Height / 2)

        SampleRateComboBox.Items.Clear()
        SampleRateComboBox.Items.AddRange(New Object() {
          "Minutes:10", "Minutes:5", "Minutes:1",
          "Seconds:30", "Seconds:10", "Seconds:5", "Seconds:1",
          "Millis:500", "Millis:200", "Millis:100"})
        SampleRateComboBox.SelectedIndex = 6
        AddHandler SampleRateComboBox.SelectedIndexChanged, AddressOf SampleRateComboBox_SelectedIndexChanged
    End Sub

    ' Update sample timer interval based on combo selection
    Private Sub SampleRateComboBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim cb = TryCast(sender, ComboBox)
        If cb Is Nothing OrElse cb.SelectedItem Is Nothing Then Return

        Dim text = cb.SelectedItem.ToString()
        Dim ms As Integer = ParseIntervalToMilliseconds(text)
        If ms <= 0 Then Return

        ' Apply interval (must be >=1)
        Dim interval = Math.Max(1, ms)
        Try
            SampleTimer.Interval = interval
            If SampleTimer.Enabled Then
                ' restart to apply immediately
                SampleTimer.Stop()
                SampleTimer.Start()
            End If
        Catch ex As Exception
            Debug.WriteLine($"Failed to set SampleTimer interval: {ex.Message}")
        End Try
    End Sub

    Private Function ParseIntervalToMilliseconds(label As String) As Integer
        If String.IsNullOrEmpty(label) Then Return 0
        Dim parts = label.Split({":"c}, StringSplitOptions.RemoveEmptyEntries).Select(Function(s) s.Trim()).ToArray()
        If parts.Length <> 2 Then Return 0
        Dim unit = parts(0).ToLowerInvariant()
        Dim value As Integer
        If Not Integer.TryParse(parts(1), value) Then Return 0

        Select Case unit
            Case "minutes"
                ' clamp to avoid overflow
                If value <= 0 Then Return 0
                Dim ms = value * 60 * 1000
                Return If(ms < 1, 0, ms)
            Case "seconds"
                If value <= 0 Then Return 0
                Return value * 1000
            Case "millis"
                If value <= 0 Then Return 0
                Return value
            Case Else
                Return 0
        End Select
    End Function

    ' Normalize and clamp user input for Y axis as hex
    Private Sub YAxisTextBox_TextChanged(sender As Object, e As EventArgs) Handles YAxisTextBox.TextChanged
        Dim tb = DirectCast(sender, TextBox)

        ' If textbox contains multiple tokens (space-separated hex bytes) do not try to reformat
        If tb.Text.IndexOf(" "c) >= 0 Then
            Return
        End If

        ' Normalize and strip common prefixes
        Dim s As String = If(tb.Text, String.Empty).ToUpperInvariant()
        s = s.Replace("0X", "").Replace("&H", "")

        ' Keep only hex digits
        Dim sb As New System.Text.StringBuilder()
        For Each ch As Char In s
            If "0123456789ABCDEF".IndexOf(ch) >= 0 Then sb.Append(ch)
        Next

        Dim hex = sb.ToString()
        If String.IsNullOrEmpty(hex) Then
            ' empty input -> keep empty
            If tb.Text <> String.Empty Then tb.Text = String.Empty
            Return
        End If

        ' Limit to three hex digits (0..3FF)
        If hex.Length > 3 Then hex = hex.Substring(0, 3)

        ' Parse and clamp to 0..1023, then format as three uppercase hex digits
        Dim val As Integer
        If Integer.TryParse(hex, Globalization.NumberStyles.HexNumber, Globalization.CultureInfo.InvariantCulture, val) Then
            If val < 0 Then val = 0
            If val > 1023 Then val = 1023
            hex = val.ToString("X3")
        Else
            hex = "000"
        End If

        ' Only update textbox if different to avoid re-entrancy; preserve caret as best-effort
        If tb.Text <> hex Then
            Dim sel = tb.SelectionStart
            tb.Text = hex
            tb.SelectionStart = Math.Min(sel, tb.Text.Length)
        End If
    End Sub

    ' Menu item to start sampling
    Private Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click
        If SampleTimer.Enabled Then
            SampleTimer.Stop()
            SampleTimer.Enabled = False

        Else
            SampleTimer.Enabled = True
            SampleTimer.Start()
        End If
    End Sub

    ' Menu item to stop sampling
    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        If SampleTimer.Enabled Then
            SampleTimer.Stop()
            SampleTimer.Enabled = False

        Else
            SampleTimer.Enabled = True
            SampleTimer.Start()
        End If
    End Sub

    ' Menu item to load/save data (calls LoadData)
    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        LoadData()
    End Sub
End Class