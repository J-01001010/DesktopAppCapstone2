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

Public Class Entering
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


    Private Sub Entering_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox4.Text = "NONE"

        Timer1.Enabled = True
        Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("pvhosms_db")
        collection = database.GetCollection(Of BsonDocument)("residence")
        CenterToScreen()
        SerialPort1 = New SerialPort()
        SerialPort1.PortName = V
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = Date.Now.ToString("dd-MM-yyy hh:mm:ss ")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")

        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("time_login")



        Dim document As BsonDocument = New BsonDocument()

        document.Add("_rno", TextBox1.Text)
        document.Add("_pnov", TextBox4.Text)
        document.Add("_ti", Label2.Text)

        Try

            If TextBox1.Text = "" Then

                TextBox1.BackColor = Color.LightSkyBlue
                MsgBox("*Please Tap RFID Card")
                TextBox1.ForeColor = Color.Red

            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("save succesfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                Button13.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Console.ReadLine()
    End Sub
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.White
        TextBox1.Text = ""

        TextBox1.ForeColor = Color.Black
        Button13.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim receiverNumber As String = Label1.Text
        Dim messageContent As String = "You are entered in the private village of monte brisa"

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
            SerialPort1.Write(messageContent & Label2.Text & Chr(26))
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


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("Resident id(RFID no)", TextBox1.Text)
        Dim r As BsonDocument = collection.Find(filter).FirstOrDefault()

        If r IsNot Nothing Then
            TextBox1.Text = r("Resident id(RFID no)").ToString()

            Label1.Text = r("contact no").ToString()
            MsgBox("Valid")
        Else
            MessageBox.Show("No data found for the given RFID.")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SerialPort1.Close()
        Me.Hide()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        scan.ShowDialog()
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try
            ' Set a character limit for input text (e.g., 1000 characters)
            Dim maxCharacters As Integer = 1000

            ' Check if the input text exceeds the character limit
            If TextBox3.Text.Length > maxCharacters Then
                ' Truncate the input text to the maximum allowed length
                Dim truncatedText As String = TextBox3.Text.Substring(0, maxCharacters)

                ' Generate the QR code using the truncated input text
                PictureBox3.Image = qr_g.Encode(truncatedText)

                ' Display a message box informing the user that the input text has been truncated
                MsgBox("Input text has been truncated to the maximum allowed length.")
            Else
                ' Generate the QR code using the original input text
                PictureBox3.Image = qr_g.Encode(TextBox3.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        SaveFileDialog1.ShowDialog()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

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
    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim SD As New SaveFileDialog
        SD.Filter = "PNG |* .png"
        If SD.ShowDialog() = DialogResult.OK Then
            PictureBox2.Image.Save(SD.FileName, Imaging.ImageFormat.Png)
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            Dim img As New Bitmap(PictureBox3.Image)
            img.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Png)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        stopWebCam()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim scanner As New BarcodeReader()
        Dim result As Result = scanner.Decode(New Bitmap("C:\Users\acedn\OneDrive\Desktop\qrcode.png"))

        If result IsNot Nothing Then
            Dim content As String = result.Text
            TextBox2.Text = content
        Else
            MessageBox.Show("QR code not found or could not be decoded.")
        End If
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")

        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("non_resident_time_login")



        Dim document As BsonDocument = New BsonDocument()


        document.Add("Non-Resident id(QRcode no.)", TextBox2.Text)
        document.Add("Plate no. of Vehicle)", TextBox5.Text)

        document.Add("time_in", Label2.Text)


        Try



            If TextBox2.Text = "" Then

                TextBox2.BackColor = Color.LightSkyBlue
                MsgBox("*INCOMPLETE DATA")
                TextBox2.ForeColor = Color.Red



            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("save succesfully")
                TextBox2.Text = ""
                TextBox5.Text = ""
                Button15.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Console.ReadLine()
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

    Private Sub MyWebCam_ImageCaptured(source As Object, e As WebcamEventArgs) Handles MyWebCam.ImageCaptured
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = e.WebCamImage
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        startWebCam()
        TextBox2.Clear()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            stopWebCam()
            Reader = New QRCodeDecoder
            TextBox2.Text = Reader.Decode(New Data.QRCodeBitmapImage(PictureBox1.Image))
            MsgBox("Qr code has been detected.")
        Catch ex As Exception
            startWebCam()
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
End Class