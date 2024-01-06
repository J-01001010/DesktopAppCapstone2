
Imports WebCam_Capture
Imports MessagingToolkit.QRCode.Codec
Imports System.Drawing.Imaging

Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Public Class sample
    WithEvents MyWebCam As WebCamCapture
    Dim Reader As QRCodeDecoder

    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap
    Private Sub startWebCam()
        Try
            stopWebCam()
            MyWebCam = New WebCamCapture
            MyWebCam.Start(0)
        Catch ex As Exception
            MessageBox.Show("Error starting webcam: " & ex.Message)
        End Try
    End Sub

    Private Sub stopWebCam()

        Try
            If MyWebCam IsNot Nothing Then
                MyWebCam.Stop()
                MyWebCam.Dispose()
                MyWebCam = Nothing ' Set MyWebCam to Nothing to ensure proper reinitialization
            End If
        Catch ex As Exception
            MessageBox.Show("Error stopping webcam: " & ex.Message)
        End Try
    End Sub
    Private Sub MyWebCam_ImageCaptured(source As Object, e As WebcamEventArgs) Handles MyWebCam.ImageCaptured
        If PictureBox2.Image IsNot Nothing Then
            PictureBox2.Image.Dispose()
        End If
        PictureBox2.Image = e.WebCamImage
    End Sub

    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        PictureBox2.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
    End Sub
    Private Sub sample_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        startWebCam() ' Start the webcam when the form loads

        Try
            startWebCam()
            Reader = New QRCodeDecoder
        Catch ex As Exception
            LogError("Error initializing webcam and QR code reader: ")
        End Try
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = Windows.Forms.DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()

        End If

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
            If MyWebCam IsNot Nothing AndAlso MyWebCam IsNot Nothing Then
                Reader = New QRCodeDecoder
                TextBox1.Text = Reader.Decode(New Data.QRCodeBitmapImage(PictureBox2.Image))
                MsgBox("QR code has been detected.")
            Else
                MsgBox("No webcam image available. Please start the webcam.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error decoding QR code: " & ex.Message)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim SD As New SaveFileDialog
        SD.Filter = "PNG |*.png"
        If SD.ShowDialog() = DialogResult.OK Then
            If PictureBox2.Image IsNot Nothing Then
                PictureBox2.Image.Save(SD.FileName, ImageFormat.Png)
            Else
                MsgBox("No image to save. Please capture an image first.")
            End If
        End If
    End Sub

    Private Sub LogError(message As String)
        ' You can replace this with your preferred error logging mechanism.
        ' For example, write errors to a log file or display them in a TextBox.
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

End Class