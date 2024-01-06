Imports WebCam_Capture
Imports MessagingToolkit.QRCode.Codec
Public Class Form2
    WithEvents MyWebCam As WebCamCapture
    Dim Reader As QRCodeDecoder

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub startWebCam()
        Try
            stopWebCam()
            MyWebCam = New WebCamCapture
            MyWebCam.Start(0)
        Catch ex As Exception
            MsgBox("Error starting webcam: " & ex.Message)
        End Try
    End Sub
    Private Sub stopWebCam()
        Try
            If MyWebCam IsNot Nothing Then
                MyWebCam.Stop()
                MyWebCam.Dispose()
            End If
        Catch ex As Exception
            MsgBox("Error stopping webcam: " & ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MyWebCam_ImageCaptured(source As Object, e As WebcamEventArgs) Handles MyWebCam.ImageCaptured
        If PictureBox2.Image IsNot Nothing Then
            PictureBox2.Image.Dispose()
        End If
        PictureBox2.Image = e.WebCamImage
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        startWebCam()
        TextBox1.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        stopWebCam()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            stopWebCam()
            Reader = New QRCodeDecoder
            TextBox1.Text = Reader.Decode(New Data.QRCodeBitmapImage(PictureBox2.Image))
            MsgBox("Qr code has been detected.")
        Catch ex As Exception
            startWebCam()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SD As New SaveFileDialog
        SD.Filter = "PNG |* .png"
        If SD.ShowDialog() = DialogResult.OK Then
            PictureBox2.Image.Save(SD.FileName, Imaging.ImageFormat.Png)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click


    End Sub
End Class
