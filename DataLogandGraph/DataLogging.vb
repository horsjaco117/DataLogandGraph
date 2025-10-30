Option Strict On
Option Explicit On
Imports System.Runtime.CompilerServices
Public Class DataLoggingGraph

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

    Function GetData() As Integer
        Return 5
    End Function

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)
        Dim scaleX! = CSng(GraphPictureBox.Width \ 100)
        Dim scaleY! = CSng((GraphPictureBox.Height \ 100) * -1)
        Dim oldY% = 50
        Dim newY% = 50


        g.TranslateTransform(0, GraphPictureBox.Height) 'Moves origin to bottom left
        g.ScaleTransform(scaleX, scaleY) 'scale to 100 x 100 units

        pen.Width = 0.25 'Fiexes pen so it isn't this

        For x = 0 To 100
            newY = GetRandomNumberAround(oldY, 5)
            g.DrawLine(pen, x - 1, oldY, x, newY)
            oldY = newY


        Next

        g.Dispose()
        pen.Dispose()


    End Sub


    'Event Handlers--------------------------------------------------------------------------
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        GraphData()
        For i = 1 To 100
            Console.WriteLine(GetRandomNumberAround(50, 50))
        Next
    End Sub
End Class
