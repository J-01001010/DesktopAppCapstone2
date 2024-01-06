Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.IO.Ports
Public Class add_non_resident
    Dim WithEvents serialport As SerialPort
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DisconnectFromGSMModule()
        dashboard.Show()
        Button6.Enabled = True
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox2.Text = ""
        ComboBox1.Text = ""
        TextBox7.Text = ""
        TextBox6.Text = ""
        DateTimePicker1.Text = ""
        TextBox8.Text = ""

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        OpenFileDialog1.ShowDialog()
        TextBox8.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If (System.IO.File.Exists(TextBox8.Text)) Then
            PictureBox1.Image = Image.FromFile(TextBox8.Text)

        End If
        If TextBox8.Text = "" Then
            PictureBox1.Hide()
        Else
            PictureBox1.Show()
        End If
    End Sub
    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        PictureBox3.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog() = Windows.Forms.DialogResult.OK Then
            camera = cameras.VideoDevice
            AddHandler camera.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            camera.Start()
            Button6.Enabled = False
            PictureBox3.Visible = True
            Button7.Enabled = True
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PictureBox1.Image = PictureBox3.Image
        PictureBox3.Show()
        Button8.Enabled = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        SaveFileDialog1.DefaultExt = ".jpg"
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image.Save(SaveFileDialog1.FileName, Imaging.ImageFormat.Jpeg)
            camera.Stop()
            Button7.Enabled = False
            Button5.Enabled = True
            Button8.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if any of the required textboxes is empty
        If String.IsNullOrWhiteSpace(TextBox15.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
       String.IsNullOrWhiteSpace(ComboBox2.Text) OrElse
       String.IsNullOrWhiteSpace(ComboBox1.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox6.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox7.Text) OrElse
       String.IsNullOrWhiteSpace(TextBox8.Text) Then

            ' Display error messages for empty textboxes
            MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")

        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("non-residence")



        Dim document As BsonDocument = New BsonDocument()

        document.Add("Non resident id (QRcode_no))", TextBox15.Text)
        document.Add("first name", TextBox1.Text)

        document.Add("middle name", TextBox2.Text)
        document.Add("last name", TextBox3.Text)

        document.Add("purpose)", ComboBox2.Text)
        document.Add("type of id presented", ComboBox1.Text)

        document.Add("id number", TextBox6.Text)
        document.Add("cp_no", TextBox7.Text)
        document.Add("date of visit", DateTimePicker1.Text)

        document.Add("photo", TextBox8.Text)


        Try

            If TextBox15.Text = "" Then

                TextBox15.BackColor = Color.LightSkyBlue
                TextBox15.Text = "*required"
                TextBox15.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False

            End If

            If TextBox1.Text = "" Then

                TextBox1.BackColor = Color.LightSkyBlue
                TextBox1.Text = "*required"
                TextBox1.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If TextBox2.Text = "" Then

                TextBox2.BackColor = Color.LightSkyBlue
                TextBox2.Text = "*required"
                TextBox2.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If TextBox3.Text = "" Then

                TextBox3.BackColor = Color.LightSkyBlue
                TextBox3.Text = "*required"
                TextBox3.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If ComboBox2.Text = "" Then

                ComboBox2.BackColor = Color.LightSkyBlue
                ComboBox2.Text = "*required"
                ComboBox2.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If ComboBox1.Text = "" Then

                ComboBox1.BackColor = Color.LightSkyBlue
                ComboBox1.Text = "*required"
                ComboBox1.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If TextBox6.Text = "" Then
                TextBox6.BackColor = Color.LightSkyBlue

                TextBox6.Text = "*required"
                TextBox6.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If TextBox7.Text = "" Then
                TextBox7.BackColor = Color.LightSkyBlue

                TextBox7.Text = "*required"
                TextBox7.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False
            End If
            If TextBox8.Text = "" Then

                TextBox8.BackColor = Color.LightSkyBlue
                TextBox8.Text = "*required"
                TextBox8.ForeColor = Color.Red
                TextBox8.Text = ""
                TextBox8.Enabled = False

            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("Non-Resident registered successfully!")
                Try
                    If SerialPort IsNot Nothing AndAlso SerialPort.IsOpen Then
                        Dim phoneNumber As String = TextBox7.Text.Trim()
                        If Not String.IsNullOrEmpty(phoneNumber) Then
                            ' Set up event handler for SMS completion
                            AddHandler SerialPort.DataReceived, AddressOf SmsSentHandler

                            ' Send the SMS
                            SerialPort.WriteLine("AT+CMGF=1")
                            System.Threading.Thread.Sleep(1000)
                            SerialPort.WriteLine($"AT+CMGS=""{phoneNumber}""")
                            System.Threading.Thread.Sleep(1000)

                            Dim message As String = "You are now registered as Non-Residence  "
                            serialport.Write(message & Label19.Text & Chr(26))
                            TextBox7.Text = ""
                        Else
                            MessageBox.Show("Error: Please enter a valid phone number.")
                        End If
                    Else
                        MessageBox.Show("Error: GSM module not connected.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox2.Text = ""
                ComboBox1.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox15.Text = ""
                TextBox1.BackColor = Color.White
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                ComboBox2.BackColor = Color.White
                ComboBox1.BackColor = Color.White
                TextBox6.BackColor = Color.White
                TextBox8.BackColor = Color.White
                TextBox15.BackColor = Color.White
                Button1.Enabled = False
                Button4.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Console.ReadLine()
    End Sub
    Private Sub TextBox15_GotFocus(sender As Object, e As EventArgs) Handles TextBox15.GotFocus
        TextBox15.BackColor = Color.White
        TextBox15.Text = ""

        TextBox15.ForeColor = Color.Black
    End Sub
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.White
        TextBox1.Text = ""

        TextBox1.ForeColor = Color.Black

    End Sub
    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.BackColor = Color.White
        TextBox2.Text = ""

        TextBox2.ForeColor = Color.Black
    End Sub
    Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
        TextBox3.BackColor = Color.White
        TextBox3.Text = ""

        TextBox3.ForeColor = Color.Black
    End Sub
    Private Sub ComboBox2_GotFocus(sender As Object, e As EventArgs)
        ComboBox2.BackColor = Color.White
        ComboBox2.Text = ""

        ComboBox2.ForeColor = Color.Black
    End Sub
    Private Sub Combobox1_GotFocus(sender As Object, e As EventArgs) Handles ComboBox1.GotFocus
        ComboBox1.BackColor = Color.White

        ComboBox1.Text = ""
        ComboBox1.ForeColor = Color.Black
    End Sub

    Private Sub TextBox6_GotFocus(sender As Object, e As EventArgs) Handles TextBox6.GotFocus
        TextBox6.BackColor = Color.White
        TextBox6.Text = ""

        TextBox6.ForeColor = Color.Black
    End Sub
    Private Sub TextBox7_GotFocus(sender As Object, e As EventArgs) Handles TextBox7.GotFocus
        TextBox7.BackColor = Color.White
        TextBox7.Text = ""

        TextBox7.ForeColor = Color.Black
    End Sub

    Private Sub TextBox8_GotFocus(sender As Object, e As EventArgs) Handles TextBox8.GotFocus
        TextBox8.BackColor = Color.White
        TextBox8.Text = ""

        TextBox8.ForeColor = Color.Black
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        camera.Stop()
        PictureBox3.Visible = False
        Button4.Enabled = True
        Button5.Enabled = False
    End Sub



    Private lastRandomNumber As Integer = 0

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            If lastRandomNumber = 0 Then
                GenerateAndSetRandomNumber()
            End If
        Else
            TextBox15.Clear()
            lastRandomNumber = 0
        End If
    End Sub

    Private Sub GenerateAndSetRandomNumber()
        lastRandomNumber = New Random().Next(1, 1001)

        Dim standardNumber As String = "2023-" & lastRandomNumber.ToString("D4")

        TextBox15.Text = standardNumber
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            GenerateAndSetRandomNumberForRadioButton2()
        Else
            TextBox15.Clear()
        End If
    End Sub

    Private Sub GenerateAndSetRandomNumberForRadioButton2()
        lastRandomNumber = New Random().Next(1001, 9001)
        Dim standardNumber As String = "2023-" & lastRandomNumber.ToString("D4")
        TextBox15.Text = standardNumber
    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        Button1.Enabled = True
    End Sub

    Private Sub add_non_resident_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConnectToGSMModule()
    End Sub
    Private Function ConnectToGSMModule() As Boolean
        ' Search for available COM ports
        Dim availablePorts As String() = SerialPort.GetPortNames()

        ' Try connecting to each port to find the one with the GSM module
        For Each portName As String In availablePorts
            Try
                ' Attempt to open the port
                SerialPort = New SerialPort(portName)
                SerialPort.Open()

                ' Send a command to check if the GSM module is responding
                SerialPort.WriteLine("AT")
                System.Threading.Thread.Sleep(1000)

                ' Check the response
                Dim response As String = SerialPort.ReadExisting()
                If response.Contains("OK") Then
                    ' GSM module found on this port
                    Return True
                End If

                ' Close the port if the module is not found
                SerialPort.Close()
            Catch ex As Exception
                ' Ignore exceptions and try the next port
            End Try
        Next

        ' No GSM module found on any port
        Return False
    End Function
    Private Sub DisconnectFromGSMModule()
        ' Check if the serial port is open before attempting to close it
        If SerialPort IsNot Nothing AndAlso SerialPort.IsOpen Then
            ' Send a command to the GSM module to disconnect (you may need to adjust the command)
            SerialPort.WriteLine("AT+CGATT=0")
            System.Threading.Thread.Sleep(1000)

            ' Close the serial port
            SerialPort.Close()
        End If
    End Sub
    Private Sub ReconnectToGSMModule()
        ' Disconnect from the GSM module before attempting to reconnect
        DisconnectFromGSMModule()

        ' Attempt to reconnect to the GSM module
        If Not ConnectToGSMModule() Then
            ' Handle the case when the module is not found
            MessageBox.Show("GSM module not found.")
        End If
    End Sub

    Private Sub SmsSentHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Dim response As String = serialport.ReadExisting()
        RemoveHandler SerialPort.DataReceived, AddressOf SmsSentHandler
    End Sub
End Class