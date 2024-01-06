Public Class print_report
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        PrintPreviewDialog1.ShowDialog()
        PrintDocument1.Print()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        ' Create a margin of 1 inch
        Dim margin As Integer = 100 ' 1 inch = 100 units (assuming 100 units per inch)

        ' Set the font and font size
        Dim font As New Font("Arial", 12)

        ' Calculate the printable area of the page
        Dim printArea As New RectangleF(margin, margin, e.PageBounds.Width - 2 * margin, e.PageBounds.Height - 2 * margin)

        ' Create a Graphics object to draw on the page
        Using g As Graphics = e.Graphics
            ' Measure the size of the text to be printed
            Dim textSize As SizeF = g.MeasureString(RichTextBox1.Text, font, printArea.Size)

            ' Calculate the scaling factor to fit the text within the printable area
            Dim scaleX As Single = printArea.Width / textSize.Width
            Dim scaleY As Single = printArea.Height / textSize.Height
            Dim scale As Single = Math.Min(scaleX, scaleY)

            ' Set the font with the scaled size
            Dim scaledFont As New Font(font.FontFamily, font.Size)

            ' Create a rectangle to position the text
            Dim textRect As New RectangleF(printArea.Left, printArea.Top, textSize.Width * scale, textSize.Height * scale)

            ' Draw the text on the page
            g.DrawString(RichTextBox1.Text, scaledFont, Brushes.Black, textRect)
        End Using
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class