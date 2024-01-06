Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Imports MessagingToolkit.QRCode.Codec

Public Class scan
    Dim qr_g As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim dqr As New MessagingToolkit.QRCode.Codec.QRCodeDecoder
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            PictureBox3.Image = qr_g.Encode(TextBox1.Text)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveFileDialog1.ShowDialog()

    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim img As New Bitmap(PictureBox3.Image)
            img.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            Dim od As New OpenFileDialog()
            od.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop

            If od.ShowDialog() = DialogResult.OK Then
                PictureBox3.Load(od.FileName)
                Dim dqr As New MessagingToolkit.QRCode.Codec.QRCodeDecoder()
                TextBox2.Text = dqr.Decode(New MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(PictureBox3.Image))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub scan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = Windows.Forms.DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PictureBox4.Image = PictureBox1.Image
        PictureBox1.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SaveFileDialog2.DefaultExt = ".jpg"
        If SaveFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox4.Image.Save(SaveFileDialog2.FileName, Imaging.ImageFormat.Jpeg)
            camera.Stop()
        End If
    End Sub


End Class