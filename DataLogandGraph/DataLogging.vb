Option Strict On
Option Explicit On

Imports System.Media
Imports System.Threading.Thread
Imports System.Runtime.CompilerServices
'Serial communications imports
Imports System.IO.Ports
Imports System.Net.Configuration

Public Class DataLoggingGraph
    Private LastCommand As Byte
    Private currentX As Single = 0
    Private currentY As Single = 0

    Dim DataBuffer As New Queue(Of Integer)
    'Serial Stuff----------------------------------------------------------------------------
    Sub SetDefaults() 'Set's default serial pieces and shows COM ports
        SerialPort1.Close() 'Closes the COM ports but adds settings
        ' ConnectionStatusLabel.Text = "No connection" 'No connect until port chosen
        GetPorts() 'Shows available ports
    End Sub

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

    Sub connect()

        SerialPort1.Close() 'Closes the ports 
        Try  'All these are serial port settings. 
            SerialPort1.BaudRate = 115200
            SerialPort1.Parity = Parity.None
            SerialPort1.StopBits = StopBits.One
            SerialPort1.DataBits = 8
            SerialPort1.PortName = CStr(PortsComboBox.SelectedItem)
            SerialPort1.Open()


            If IsQuietBoard() Then 'Confirms that communication is with the Quiet Board
                '     ConnectionStatusLabel.Text = $"Qy@ Connected on {SerialPort1.PortName}"
            Else
                SetDefaults() 'Resets to defaults if not quiet board
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            SetDefaults()
        End Try

    End Sub

    Function IsQuietBoard() As Boolean
        Dim data(0) As Byte 'put bytes into array
        data(0) = &B11110000 'actual data as a byte
        send(data)


        Return True

    End Function
    Sub send(data() As Byte)
        SerialPort1.ReadExisting()
        SerialPort1.Write(data, 0, UBound(data))
    End Sub

    Function recieve() As Byte() 'For every byte recieved it will be displayed as the data variable
        Dim data(SerialPort1.BytesToRead) As Byte
        SerialPort1.Read(data, 0, SerialPort1.BytesToRead)
        Return data
    End Function

    Sub writeSerial() 'This write sub is especially for handshaking
        Dim data(0) As Byte
        data(0) = &B11110000
        SerialPort1.Write(data, 0, 1)
    End Sub
    Private Sub COMButton_Click(sender As Object, e As EventArgs) Handles COMButton.Click
        'Establishes the com between the quiet board

        connect()
        writeSerial()
    End Sub
    Private Sub AnalogTimer_Tick(sender As Object, e As EventArgs) Handles AnalogTimer.Tick
        AnalogRead3() 'For every timer tick the Analog data is interpreted and read
    End Sub
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

            Dim leftValue As Integer = buffer(1)   ' left analog (X-axis) takes the bytes we want
            Dim rightValue As Integer = buffer(3)  ' right analog (Y-axis)

            ' Update the text boxes with the hex bytes (thread-safe)
            WriteToTextBox(Me.XAxisTextBox, buffer(0).ToString("X2"))
            WriteToTextBox(Me.YAxisTextBox, buffer(2).ToString("X2"))

            Me.Invoke(Sub()
                          SerialTextBox.AppendText(hexString.ToString() & vbCrLf)
                      End Sub)

            ' If PIC control mode is active, draw using values currently present in the hex text boxes
            ' If PICControlRadioButton.Checked Then
            Me.Invoke(Sub() DrawFromHexTextBoxes()) 'This invoke function safely filters the hex from the text boxes
            ' End If

        Catch ex As Exception
            Console.WriteLine($"Serial read error: {ex.Message}")
        End Try
    End Sub

    Private Sub DrawFromHexTextBoxes()
        ' Read & sanitize the hex strings
        Dim hx As String = If(XAxisTextBox?.Text, "").Trim()
        Dim hy As String = If(YAxisTextBox?.Text, "").Trim()
        If String.IsNullOrEmpty(hx) Or String.IsNullOrEmpty(hy) Then Return

        ' Normalize case and remove common prefixes
        hx = hx.ToUpperInvariant().Replace("0X", "").Replace("&H", "")
        hy = hy.ToUpperInvariant().Replace("0X", "").Replace("&H", "")

        hx = hx.Split(" "c, CChar(vbTab), CChar(vbCrLf)).FirstOrDefault()
        hy = hy.Split(" "c, CChar(vbTab), CChar(vbCrLf)).FirstOrDefault()
        If String.IsNullOrEmpty(hx) Or String.IsNullOrEmpty(hy) Then Return

        Try
            Dim valX As Integer = Convert.ToInt32(hx, 16) ' 0..255 expected
            Dim valY As Integer = Convert.ToInt32(hy, 16)

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

            ' Map raw 0..255 into the constrained rectangle
            Dim mappedX As Integer = minX + CInt(Math.Round((valX / 255.0F) * (maxX - minX)))
            Dim mappedY As Integer = minY + CInt(Math.Round((valY / 255.0F) * (maxY - minY)))

            ' Remember old position and update current position
            Dim oldX As Integer = CInt(currentX)
            Dim oldY As Integer = CInt(currentY)

            currentX = Math.Max(0, Math.Min(w, mappedX))
            currentY = Math.Max(0, Math.Min(h, mappedY))

            ' Draw on UI thread
            '   If Me.InvokeRequired Then
            '        Me.Invoke(Sub() DrawWithMouse(oldX, oldY, CInt(currentX), CInt(currentY)))
            '  Else
            ' DrawWithMouse(oldX, oldY, CInt(currentX), CInt(currentY))
            'End If
        Catch ex As Exception
            Debug.WriteLine($"DrawFromHexTextBoxes parse error: {ex.Message}")
        End Try
    End Sub

    Private Sub Swap(ByRef a As Integer, ByRef b As Integer)
        Dim t As Integer = a
        a = b
        b = t
    End Sub
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
    'Private Sub ReadAnalogButton_Click(sender As Object, e As EventArgs) Handles AnalogButton1.Click
    '    Dim data(0) As Byte
    '    data(0) = &H5F 'Hex value for reading analog quiet board values
    '    connect()
    '    SerialPort1.Write(data, 0, 1)
    'End Sub

    'Program logic---------------------------------------------------------------------------
    Function GetRandomNumberAround(thisNumber%, Optional within% = 10) As Integer
        Dim result%
        'result = thisNumber - within
        result = (GetRandomNumber(within * 2) + (thisNumber - (within)))


        Return result
    End Function

    Function GetRandomNumber(max%) As Integer
        Randomize()

        Return CInt(System.Math.Floor((Rnd() * max + 1)))
    End Function

    Sub LogData(currentSample%)
        Dim filePath$ = $"..\..\SensorData_{DateTime.Now.ToString("yyMMddhh")}.log"
        FileOpen(1, filePath, OpenMode.Append)
        write(1, "$$")
        write(1, DateTime.Now)
        write(1, DateTime.Now.Millisecond)
        WriteLine(1, currentSample)

        FileClose(1)

    End Sub

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

    Sub GetData()
        Dim _last%
        Dim sample%

        If Me.DataBuffer.Count > 0 Then
            _last = Me.DataBuffer.Last
        Else
            _last = GetRandomNumberAround(50, 50)
        End If

        If DataBuffer.Count >= 100 Then 'Keep the queue trimmed to graph x length
            DataBuffer.Dequeue()
        End If
        sample = GetRandomNumberAround(_last, 5)
        Me.DataBuffer.Enqueue(sample)
        LogData(sample)
    End Sub

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)
        Dim eraser As New Pen(Color.Black)
        'Dim scaleX! = CSng(GraphPictureBox.Width / 100)
        Dim scaleX! = CSng(GraphPictureBox.Width / Me.DataBuffer.Count)
        Dim scaleY! = CSng((GraphPictureBox.Height / 100) * -1)

        'g.Clear(Color.Black)
        g.TranslateTransform(0, GraphPictureBox.Height) 'move origin to bottom left
        g.ScaleTransform(scaleX, scaleY) 'scale to 100 x 100 units
        pen.Width = 0.25 'fix pen so it is not too thick

        Dim oldY% = 0 ' GetRandomNumberAround(50, 50)
        Dim x = -1
        For Each y In Me.DataBuffer.Reverse
            x += 1
            g.DrawLine(eraser, x, 0, x, 100)
            g.DrawLine(pen, x - 1, oldY, x, y)
            oldY = y
        Next

        g.Dispose()
        pen.Dispose()
        eraser.Dispose()

    End Sub



    'Event Handlers--------------------------------------------------------------------------
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        If SampleTimer.Enabled Then
            SampleTimer.Stop()
            SampleTimer.Enabled = False

        Else
            SampleTimer.Enabled = True
            SampleTimer.Start()
        End If

    End Sub

    Private Sub SampleTimer_Tick(sender As Object, e As EventArgs) Handles SampleTimer.Tick
        GetData()
        GraphData()

    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        ' LoadData()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        LoadData()
    End Sub

    Private Sub DataLoggingGraph_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetDefaults() 'Serial communication defaults
        AnalogTimer.Enabled = True  ' Disable hardware polling initially

        currentX = CSng(GraphPictureBox.Width / 2)
        currentY = CSng(GraphPictureBox.Height / 2)
    End Sub
End Class
