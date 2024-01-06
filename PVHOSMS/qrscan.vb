Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Imports System.IO.Ports
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports ZXing
Imports MessagingToolkit.QRCode.Codec
Imports System.Windows.Forms
Imports WebCam_Capture

Public Class qrscan


    Dim client As MongoClient
    Dim database As IMongoDatabase
    Dim collection As IMongoCollection(Of BsonDocument)
    Private Const V As String = "COM5"

    Dim qr_g As New MessagingToolkit.QRCode.Codec.QRCodeEncoder
    Dim dqr As New MessagingToolkit.QRCode.Codec.QRCodeDecoder
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap

    WithEvents MyWebCam As WebCamCapture
    Dim Reader As QRCodeDecoder





    Private Sub qrscan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Timer1.Enabled = True
        Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("pvhosms_db")
        collection = database.GetCollection(Of BsonDocument)("residence")
        CenterToScreen()

    End Sub

    Private Sub scanqrcode_Click(sender As Object, e As EventArgs) Handles Button8.Click
        startWebCam()
        resulttextbox.Clear()
        Button7.Enabled = True
        Button8.Enabled = False
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Button4.Enabled = True

        stopWebCam()
        Button8.Enabled = False
        Button7.Enabled = False
        Button11.Enabled = False
        PictureBox1.Visible = True
        Button1.Enabled = True
        Button6.Enabled = True
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            stopWebCam()
            Reader = New QRCodeDecoder
            resulttextbox.Text = Reader.Decode(New Data.QRCodeBitmapImage(capturedqrcode.Image))
            MsgBox("Qr code has been detected.")

            Button11.Enabled = True
        Catch ex As Exception
            startWebCam()
        End Try
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
    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        capturedqrcode.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
    End Sub
    Private Sub MyWebCam_ImageCaptured(source As Object, e As WebcamEventArgs) Handles MyWebCam.ImageCaptured
        If capturedqrcode.Image IsNot Nothing Then
            capturedqrcode.Image.Dispose()
        End If
        capturedqrcode.Image = e.WebCamImage
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dashboard.TextBox9.AppendText(resulttextbox.Text)
        dashboard.TextBox7.AppendText(TextBox5.Text)
        dashboard.TextBox9.Text = Val(resulttextbox.Text)
        dashboard.TextBox7.Text = Val(TextBox5.Text)

        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.DefaultExt = ".jpg"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
            camera.Stop()
            stopWebCam()
            Button8.Enabled = True
            Button2.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False

            Button11.Enabled = False
            Button7.Enabled = False
            PictureBox1.Visible = False
            Button3.Enabled = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenFileDialog1.ShowDialog()
        TextBox5.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = Windows.Forms.DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()
            Button4.Enabled = False
            Button5.Enabled = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PictureBox1.Image = capturedqrcode.Image
        Button2.Enabled = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox5.Text = ""
        resulttextbox.Text = ""
    End Sub

    Private Sub resulttextbox_TextChanged(sender As Object, e As EventArgs) Handles resulttextbox.TextChanged
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim qrscandForm As New dashboard()
        dashboard.TextBox9.Text = resulttextbox.Text
        dashboard.Show()
        Me.Close()
    End Sub
End Class