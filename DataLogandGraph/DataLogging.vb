Public Class DataLoggingGraph

    'Program logic---------------------------------------------------------------------------

    Function GetData() As Integer
        Return 5
    End Function

    Sub GraphData()
        Dim g As Graphics = GraphPictureBox.CreateGraphics
        Dim pen As New Pen(Color.Lime)

        g.TranslateTransform(0, GraphPictureBox.Height)
        g.ScaleTransform(100, -1)

        g.DrawLine(pen, 0, 5, 1, 5)

        g.Dispose()
        pen.Dispose()


    End Sub


    'Event Handlers--------------------------------------------------------------------------
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub GraphButton_Click(sender As Object, e As EventArgs) Handles GraphButton.Click
        GraphData()
    End Sub
End Class
