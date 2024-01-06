Imports System.IO.Ports
Public Class sendsms

    Private Const H As String = "COM5"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        SerialPort1.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim receiverNumber As String = TextBox9.Text
        Dim messageContent As String = TextBox2.Text

        ' Check if the receiver's number and message content are not empty
        If String.IsNullOrEmpty(receiverNumber) OrElse String.IsNullOrEmpty(messageContent) Then
            MessageBox.Show("Please enter a receiver's number and a message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Format the AT command to send the message
        Dim atCommand As String = "AT+CMGS=" & """" & receiverNumber & """" & vbCr


        If SerialPort1.IsOpen = True Then
            SerialPort1.Write("AT" & vbCrLf)
            SerialPort1.Write("AT+CMGF=1" & vbCrLf)
            SerialPort1.Write(atCommand)
            Dim response As String = SerialPort1.ReadExisting()
            Do Until response.Contains(">")
                response &= SerialPort1.ReadExisting()
            Loop
            SerialPort1.Write(messageContent & Chr(26))
            System.Threading.Thread.Sleep(5000)
            Dim newresponse = SerialPort1.ReadExisting()
            If newresponse.Contains("OK") Then
                MessageBox.Show("Message sent successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to send message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Error: Invalid Port", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub sendsms_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CenterToScreen()
        SerialPort1 = New SerialPort()
        SerialPort1.PortName = H
        SerialPort1.BaudRate = 115200
        SerialPort1.Parity = Parity.None
        SerialPort1.StopBits = StopBits.One
        SerialPort1.DataBits = 8
        SerialPort1.Handshake = Handshake.None
        SerialPort1.DtrEnable = True
        SerialPort1.RtsEnable = True
        SerialPort1.NewLine = vbCrLf
        SerialPort1.Open()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox2.Clear()
        TextBox9.Text = "0"

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim message As String = "Private Village: Thank you for your patience. Your delivery and vehicle appear to be in order. Please drive safely and have a great day!"

        If CheckBox1.Checked Then
            TextBox2.Text = message
        Else
            TextBox2.Text = String.Empty
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class