Option Strict On
Option Explicit On
Imports System.Runtime.CompilerServices
Public Class DataLoggingGraph
    Dim DataBuffer As New Queue(Of Integer)
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



    Sub GetData()
        Dim _last%
        If Me.DataBuffer.Count > 0 Then
            _last = Me.DataBuffer.Last
        Else
            _last = GetRandomNumberAround(50, 50)
        End If

        If DataBuffer.Count >= 100 Then 'Keep the queue trimmed to graph x length
            DataBuffer.Dequeue()
        End If
        Me.DataBuffer.Enqueue(GetRandomNumberAround(_last, 5))

    End Sub

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)
        Dim scaleX! = CSng(GraphPictureBox.Width \ 100)
        Dim scaleY! = CSng((GraphPictureBox.Height \ 100) * -1)

        g.Clear(Color.Black)
        g.TranslateTransform(0, GraphPictureBox.Height) 'Moves origin to bottom left
        g.ScaleTransform(scaleX, scaleY) 'scale to 100 x 100 units

        Dim oldY% = 0 'eGetRandomNumberAround(50, 50)
        'Dim newY% = 50
        'pen.Width = 0.25 'Fixes pen so it isn't this

        'For x = 0 To 100
        '    newY = GetRandomNumberAround(oldY, 5)
        '    g.DrawLine(pen, x - 1, oldY, x, newY)
        '    oldY = newY
        'Next
        'For y = 1 To 100
        '    Me.DataBuffer.Enqueue(y)
        'Next
        Dim x = -1
        For Each y In Me.DataBuffer.Reverse
            x += 1
            g.DrawLine(pen, x - 1, oldY, x, y)
            oldY = y
        Next


        g.Dispose()
        pen.Dispose()


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
        GraphData()
        GetData()

    End Sub
End Class
