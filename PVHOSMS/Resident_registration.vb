Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports System.IO
Imports System.IO.Ports



Public Class Resident_registration
    Dim WithEvents serialport As SerialPort
    Dim camera As VideoCaptureDevice
    Dim bmp As Bitmap


    Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DisconnectFromGSMModule()
        dashboard.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        ComboBox1.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        If (System.IO.File.Exists(TextBox12.Text)) Then
            PictureBox1.Image = Image.FromFile(TextBox12.Text)

        End If
        If TextBox12.Text = "" Then
            PictureBox1.Hide()
        Else
            PictureBox1.Show()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        OpenFileDialog1.ShowDialog()
        TextBox12.Text = OpenFileDialog1.FileName
        TextBox12.BackColor = Color.White
        Button1.Enabled = True
    End Sub

    Private Function ConnectToGSMModule() As Boolean
        ' Search for available COM ports
        Dim availablePorts As String() = SerialPort.GetPortNames()

        ' Try connecting to each port to find the one with the GSM module
        For Each portName As String In availablePorts
            Try
                ' Attempt to open the port
                serialport = New SerialPort(portName)
                serialport.Open()

                ' Send a command to check if the GSM module is responding
                serialport.WriteLine("AT")
                System.Threading.Thread.Sleep(1000)

                ' Check the response
                Dim response As String = serialport.ReadExisting()
                If response.Contains("OK") Then
                    ' GSM module found on this port
                    Return True
                End If

                ' Close the port if the module is not found
                serialport.Close()
            Catch ex As Exception
                ' Ignore exceptions and try the next port
            End Try
        Next

        ' No GSM module found on any port
        Return False
    End Function
    Private Sub DisconnectFromGSMModule()
        ' Check if the serial port is open before attempting to close it
        If serialport IsNot Nothing AndAlso serialport.IsOpen Then
            ' Send a command to the GSM module to disconnect (you may need to adjust the command)
            serialport.WriteLine("AT+CGATT=0")
            System.Threading.Thread.Sleep(1000)

            ' Close the serial port
            serialport.Close()
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
        RemoveHandler serialport.DataReceived, AddressOf SmsSentHandler
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("residence")
        Dim document As BsonDocument = New BsonDocument()

        document.Add("Resident id(RFID no)", TextBox15.Text)
        document.Add("first name", TextBox1.Text)
        document.Add("middle name", TextBox2.Text)
        document.Add("last name", TextBox3.Text)
        document.Add("block no)", TextBox4.Text)
        document.Add("lot no", TextBox5.Text)
        document.Add("street", TextBox6.Text)
        document.Add("phase no", TextBox7.Text)
        document.Add("email", TextBox8.Text)
        document.Add("contact no", TextBox9.Text)
        document.Add("no of each residence", TextBox10.Text)
        document.Add("residence category", ComboBox1.Text)
        document.Add("photo", TextBox12.Text)
        document.Add("username", TextBox13.Text)
        document.Add("password", TextBox14.Text)
        ' Check if any of the required textboxes is empty
        If AreRequiredFieldsEmpty() Then
            ' Display error messages for empty textboxes
            MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Return
        End If

        Try

            If TextBox15.Text = "" Then

                TextBox15.BackColor = Color.LightSkyBlue
                TextBox15.Text = "*required"
                TextBox15.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox1.Text = "" Then
                TextBox1.BackColor = Color.LightSkyBlue
                TextBox1.Text = "*required"
                TextBox1.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox2.Text = "" Then
                TextBox2.BackColor = Color.LightSkyBlue
                TextBox2.Text = "*required"
                TextBox2.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox3.Text = "" Then
                TextBox3.BackColor = Color.LightSkyBlue
                TextBox3.Text = "*required"
                TextBox3.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox4.Text = "" Then
                TextBox4.BackColor = Color.LightSkyBlue
                TextBox4.Text = "*required"
                TextBox4.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox5.Text = "" Then
                TextBox5.BackColor = Color.LightSkyBlue
                TextBox5.Text = "*required"
                TextBox5.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox6.Text = "" Then
                TextBox6.BackColor = Color.LightSkyBlue

                TextBox6.Text = "*required"
                TextBox6.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox7.Text = "" Then

                TextBox7.BackColor = Color.LightSkyBlue
                TextBox7.Text = "*required"
                TextBox7.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox8.Text = "" Then

                TextBox8.BackColor = Color.LightSkyBlue
                TextBox8.Text = "*required"
                TextBox8.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox9.Text = "" Then

                TextBox9.BackColor = Color.LightSkyBlue
                TextBox9.Text = "*required"
                TextBox9.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox10.Text = "" Then

                TextBox10.BackColor = Color.LightSkyBlue
                TextBox10.Text = "*required"
                TextBox10.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If ComboBox1.Text = "" Then

                ComboBox1.BackColor = Color.LightSkyBlue
                ComboBox1.Text = "*required"
                ComboBox1.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If

            If TextBox12.Text = "" Then

                TextBox12.BackColor = Color.LightSkyBlue
                TextBox12.Text = "*required"
                TextBox12.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If

            If TextBox13.Text = "" Then

                TextBox13.BackColor = Color.LightSkyBlue
                TextBox13.Text = "*required"
                TextBox13.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False
            End If
            If TextBox14.Text = "" Then

                TextBox14.BackColor = Color.LightSkyBlue
                TextBox14.Text = "*required"
                TextBox14.ForeColor = Color.Red
                TextBox14.Text = ""
                TextBox14.Enabled = False

            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("Resident registered successfully!")

                Try
                    If serialport IsNot Nothing AndAlso serialport.IsOpen Then
                        Dim phoneNumber As String = TextBox9.Text.Trim()
                        If Not String.IsNullOrEmpty(phoneNumber) Then
                            ' Set up event handler for SMS completion
                            AddHandler serialport.DataReceived, AddressOf SmsSentHandler

                            ' Send the SMS
                            serialport.WriteLine("AT+CMGF=1")
                            System.Threading.Thread.Sleep(1000)
                            serialport.WriteLine($"AT+CMGS=""{phoneNumber}""")
                            System.Threading.Thread.Sleep(1000)

                            Dim message As String = "Residence registered successfully."
                            serialport.Write(message & Label19.Text & Chr(26))
                            TextBox9.Text = ""
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
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox10.Text = ""
                ComboBox1.Text = ""
                TextBox12.Text = ""
                TextBox13.Text = ""
                TextBox14.Text = ""
                TextBox15.Text = ""
                TextBox1.BackColor = Color.White
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White
                TextBox5.BackColor = Color.White
                TextBox6.BackColor = Color.White
                TextBox7.BackColor = Color.White
                TextBox8.BackColor = Color.White
                TextBox9.BackColor = Color.White
                TextBox10.BackColor = Color.White
                ComboBox1.BackColor = Color.White
                TextBox12.BackColor = Color.White
                TextBox13.BackColor = Color.White
                TextBox14.BackColor = Color.White
                TextBox15.BackColor = Color.White
                sendsms.TextBox9.Text = "0" & Val(TextBox9.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Console.ReadLine()
    End Sub

    Private Function AreRequiredFieldsEmpty() As Boolean
        ' Check if any of the required textboxes is empty
        Return String.IsNullOrWhiteSpace(TextBox15.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox2.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox3.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox4.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox5.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox6.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox7.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox8.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox9.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox10.Text) OrElse
           String.IsNullOrWhiteSpace(ComboBox1.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox12.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox13.Text) OrElse
           String.IsNullOrWhiteSpace(TextBox14.Text)
    End Function

    Private Sub TextBox15_GotFocus(sender As Object, e As EventArgs) Handles TextBox15.GotFocus
        TextBox15.BackColor = Color.White
        TextBox15.Text = ""
        TextBox14.Enabled = True
        TextBox15.ForeColor = Color.Black
    End Sub
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.White

        TextBox14.Enabled = True
        TextBox1.ForeColor = Color.Black

    End Sub
    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.BackColor = Color.White

        TextBox14.Enabled = True
        TextBox2.ForeColor = Color.Black
    End Sub
    Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
        TextBox3.BackColor = Color.White
        TextBox3.Text = ""
        TextBox14.Enabled = True
        TextBox3.ForeColor = Color.Black
    End Sub
    Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs) Handles TextBox4.GotFocus
        TextBox4.BackColor = Color.White
        TextBox4.Text = ""
        TextBox14.Enabled = True
        TextBox4.ForeColor = Color.Black
    End Sub
    Private Sub TextBox5_GotFocus(sender As Object, e As EventArgs) Handles TextBox5.GotFocus
        TextBox5.BackColor = Color.White
        TextBox14.Enabled = True
        TextBox5.Text = ""
        TextBox5.ForeColor = Color.Black
    End Sub

    Private Sub TextBox6_GotFocus(sender As Object, e As EventArgs) Handles TextBox6.GotFocus
        TextBox6.BackColor = Color.White
        TextBox6.Text = ""
        TextBox14.Enabled = True
        TextBox6.ForeColor = Color.Black
    End Sub
    Private Sub TextBox7_GotFocus(sender As Object, e As EventArgs) Handles TextBox7.GotFocus
        TextBox7.BackColor = Color.White
        TextBox7.Text = ""
        TextBox14.Enabled = True
        TextBox7.ForeColor = Color.Black

    End Sub
    Private Sub TextBox8_GotFocus(sender As Object, e As EventArgs) Handles TextBox8.GotFocus
        TextBox8.BackColor = Color.White
        TextBox8.Text = ""
        TextBox14.Enabled = True
        TextBox8.ForeColor = Color.Black
    End Sub
    Private Sub TextBox9_GotFocus(sender As Object, e As EventArgs) Handles TextBox9.GotFocus
        TextBox9.BackColor = Color.White
        TextBox9.Text = ""
        TextBox14.Enabled = True
        TextBox9.ForeColor = Color.Black
    End Sub
    Private Sub TextBox10_GotFocus(sender As Object, e As EventArgs) Handles TextBox10.GotFocus
        TextBox10.BackColor = Color.White
        TextBox10.Text = ""
        TextBox14.Enabled = True
        TextBox10.ForeColor = Color.Black
    End Sub

    Private Sub TextBox12_MouseHover(sender As Object, e As EventArgs) Handles TextBox12.MouseHover
        TextBox12.BackColor = Color.White
        TextBox12.Text = ""
        TextBox14.Enabled = True
        TextBox12.ForeColor = Color.Black
    End Sub
    Private Sub TextBox13_GotFocus(sender As Object, e As EventArgs) Handles TextBox13.GotFocus
        TextBox13.BackColor = Color.White
        TextBox13.Text = ""
        TextBox14.Enabled = True
        TextBox13.ForeColor = Color.Black
    End Sub
    Private Sub TextBox14_GotFocus(sender As Object, e As EventArgs) Handles TextBox14.GotFocus
        TextBox14.BackColor = Color.White
        TextBox14.Text = ""
        TextBox14.Enabled = True
        TextBox14.ForeColor = Color.Black
    End Sub

    Private Sub ComboBox1_GotFocus(sender As Object, e As EventArgs) Handles ComboBox1.GotFocus
        ComboBox1.BackColor = Color.White
        ComboBox1.Text = ""
        TextBox14.Enabled = True
        ComboBox1.ForeColor = Color.Black
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        sendsms.ShowDialog()
        sendsms.TextBox9.Text = "0"
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
    Private Sub Captured(sender As Object, eventargs As NewFrameEventArgs)
        bmp = DirectCast(eventargs.Frame.Clone(), Bitmap)
        PictureBox3.Image = DirectCast(eventargs.Frame.Clone(), Bitmap)
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
            Button10.Enabled = True
            Button8.Enabled = False
        End If
    End Sub


    Private Sub Resident_registration_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        sendsms.TextBox9.Text = "0" & Val(TextBox9.Text)
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk

    End Sub

    Private Sub Resident_registration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConnectToGSMModule()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        camera.Stop()
        PictureBox3.Visible = False
        Button4.Enabled = True
        Button10.Enabled = False
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click

    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub
End Class