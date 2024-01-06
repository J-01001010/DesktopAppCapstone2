Public Class logtime
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Entering.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Exiting.ShowDialog()

    End Sub
End Class