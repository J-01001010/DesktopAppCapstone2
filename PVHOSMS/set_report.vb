Public Class set_report
    Dim rand As Random = New Random()
    Dim x As String = ""
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Button5.Enabled = True
        print_report.RichTextBox1.Text = ""
        TextBox1.Text = ""

        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        print_report.ShowDialog()
        Button5.Enabled = True



    End Sub

    Private Sub set_report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        generate()
    End Sub
    Private Sub generate()

        For y As Integer = 1 To 6
            x += Convert.ToString(rand.Next(0, 9))

        Next

        TextBox15.Text = DateTime.Now.Year & "-" & x & ""
        If TextBox15.Text = TextBox15.Text Then
            x = Nothing

        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Button5.Enabled = False
        print_report.RichTextBox1.AppendText(Label16.Text + "    " + TextBox15.Text + "")

        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label12.Text + "    " + TextBox1.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label3.Text + "    " + DateTimePicker1.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label2.Text + "    " + TextBox3.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label4.Text + "    " + TextBox4.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label5.Text + "    " + TextBox5.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label6.Text + "    " + TextBox6.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label7.Text + "    " + TextBox7.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label8.Text + "    " + TextBox8.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label1.Text + "    " + TextBox9.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label10.Text + "    " + TextBox10.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label11.Text + "    " + TextBox11.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label17.Text + "    " + TextBox12.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label13.Text + "    " + TextBox13.Text + "")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Environment.NewLine + "                   ")
        print_report.RichTextBox1.AppendText(Label14.Text + "    " + TextBox14.Text + "")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        TextBox11.Text = Val(TextBox5.Text) + Val(TextBox3.Text)

        TextBox12.Text = Val(TextBox4.Text) + Val(TextBox6.Text)
        TextBox13.Text = Val(TextBox7.Text) + Val(TextBox9.Text)
        TextBox14.Text = Val(TextBox8.Text) + Val(TextBox10.Text)
    End Sub


End Class