Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Imports System.IO.Ports


Public Class dashboard

    Dim WithEvents serialport As SerialPort
    Dim client As MongoClient
    Dim database As IMongoDatabase
    Dim collection As IMongoCollection(Of BsonDocument)



    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.BackColor = Color.FromArgb(67, 83, 98)
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackColor = Color.Transparent
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then

            Me.Hide()
            Form1.Show()
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DisconnectFromGSMModule()
        add_non_resident.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        set_report.ShowDialog()

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        employee_requiredpwsrd.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DisconnectFromGSMModule()
        Resident_registration.Show()
        Me.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        aboutus.ShowDialog()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ViewResident.ShowDialog()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ViewNonresident.ShowDialog()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        concern.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Confirmation.Show()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Public Sub LoadListView1()
        Try
            ' Clear existing items in the ListView
            ListView1.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "time_login" ' Replace with your collection name

            ' Create a MongoDB client
            Dim client As New MongoClient(connectionString)

            ' Access the database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            ' Define a filter to retrieve documents (you can customize this filter)
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty

            ' Execute the query
            Dim documents As List(Of BsonDocument) = collection.Find(filter).ToList()

            Dim itemCount As Integer = 0 ' Initialize item count

            For Each doc As BsonDocument In documents
                ' Create an array to hold the values from the document
                Dim itemcoll(doc.ElementCount - 1) As String
                Dim i As Integer = 0

                ' Iterate through the document's elements
                For Each element As BsonElement In doc
                    itemcoll(i) = element.Value.ToString()
                    i += 1
                Next

                ' Create a ListViewItem and add it to the ListView
                Dim lvItem As New ListViewItem(itemcoll)
                ListView1.Items.Add(lvItem)

                itemCount += 1 ' Increment item count
            Next

            ' Display the item count in Label1
            Label10.Text = ": " & itemCount.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub LoadListView2()
        Try
            ' Clear existing items in the ListView
            ListView2.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "time_logout" ' Replace with your collection name

            ' Create a MongoDB client
            Dim client As New MongoClient(connectionString)

            ' Access the database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            ' Define a filter to retrieve documents (you can customize this filter)
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty

            ' Execute the query
            Dim documents As List(Of BsonDocument) = collection.Find(filter).ToList()

            Dim itemCount As Integer = 0 ' Initialize item count

            For Each doc As BsonDocument In documents
                ' Create an array to hold the values from the document
                Dim itemcoll(doc.ElementCount - 1) As String
                Dim i As Integer = 0

                ' Iterate through the document's elements
                For Each element As BsonElement In doc
                    itemcoll(i) = element.Value.ToString()
                    i += 1
                Next

                ' Create a ListViewItem and add it to the ListView
                Dim lvItem As New ListViewItem(itemcoll)
                ListView2.Items.Add(lvItem)

                itemCount += 1 ' Increment item count
            Next

            ' Display the item count in Label1
            Label11.Text = ": " & itemCount.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub LoadUsers()
        Try
            ' Clear existing items in the ListView
            ListView3.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "non_resident_time_login" ' Replace with your collection name

            ' Create a MongoDB client
            Dim client As New MongoClient(connectionString)

            ' Access the database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            ' Define a filter to retrieve documents (you can customize this filter)
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty

            ' Execute the query
            Dim documents As List(Of BsonDocument) = collection.Find(filter).ToList()

            Dim itemCount As Integer = 0 ' Initialize item count

            For Each doc As BsonDocument In documents
                ' Create an array to hold the values from the document
                Dim itemcoll(doc.ElementCount - 1) As String
                Dim i As Integer = 0

                ' Iterate through the document's elements
                For Each element As BsonElement In doc
                    itemcoll(i) = element.Value.ToString()
                    i += 1
                Next

                ' Create a ListViewItem and add it to the ListView
                Dim lvItem As New ListViewItem(itemcoll)
                ListView3.Items.Add(lvItem)

                itemCount += 1 ' Increment item count
            Next

            ' Display the item count in Label1
            Label12.Text = ": " & itemCount.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub LoadUsers1()
        Try
            ' Clear existing items in the ListView
            ListView4.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "non_resident_time_logout" ' Replace with your collection name

            ' Create a MongoDB client
            Dim client As New MongoClient(connectionString)

            ' Access the database
            Dim database As IMongoDatabase = client.GetDatabase(dbName)

            ' Access the collection
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            ' Define a filter to retrieve documents (you can customize this filter)
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.Empty

            ' Execute the query
            Dim documents As List(Of BsonDocument) = collection.Find(filter).ToList()

            Dim itemCount As Integer = 0 ' Initialize item count

            For Each doc As BsonDocument In documents
                ' Create an array to hold the values from the document
                Dim itemcoll(doc.ElementCount - 1) As String
                Dim i As Integer = 0

                ' Iterate through the document's elements
                For Each element As BsonElement In doc
                    itemcoll(i) = element.Value.ToString()
                    i += 1
                Next

                ' Create a ListViewItem and add it to the ListView
                Dim lvItem As New ListViewItem(itemcoll)
                ListView4.Items.Add(lvItem)

                itemCount += 1 ' Increment item count
            Next

            ' Display the item count in Label1
            Label13.Text = ": " & itemCount.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    ' Search for available COM ports
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
    Public Sub ReconnectToGSMModule()
        ConnectToGSMModule()
    End Sub
    Private Sub dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ConnectToGSMModule()
        Timer1.Enabled = True
        Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
        client = New MongoClient(connectionString)
        database = client.GetDatabase("pvhosms_db")
        collection = database.GetCollection(Of BsonDocument)("residence")
        LoadUsers()
        LoadUsers1()
        LoadListView2()
        LoadListView1()
        CenterToScreen()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LoadListView1()
        LoadListView2()
        LoadUsers()
        LoadUsers1()
        TextBox9.Text = ""
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        If TextBox1.Text.Length = 10 Then
            Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("Resident id(RFID no)", TextBox1.Text)
            Dim r As BsonDocument = collection.Find(filter).FirstOrDefault()

            If r IsNot Nothing Then
                Dim profileForm As New profile(r)
                profileForm.Show()
                If r IsNot Nothing Then
                    TextBox2.Text = r("contact no").ToString()
                End If
            Else
                MessageBox.Show("No data found for the given RFID.")
                TextBox1.Text = ""
            End If
        Else
            MessageBox.Show("Please tap your RFID.")
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        qrscan.Show()
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label16.Text = Date.Now.ToString("dd-MM-yyy hh:mm:ss ")
    End Sub


    Private Sub SmsSentHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Dim response As String = serialport.ReadExisting()
        RemoveHandler serialport.DataReceived, AddressOf SmsSentHandler
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                TextBox1.BackColor = Color.LightSkyBlue
                MsgBox("*Please Tap RFID Card")
                TextBox1.ForeColor = Color.Red
            ElseIf String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(ComboBox1.Text) Then
                MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Else
                ' Save data to MongoDB
                Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
                Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("time_login")
                Dim document As BsonDocument = New BsonDocument()

                document.Add("_rno", TextBox1.Text)
                document.Add("_pnov", ComboBox1.Text)
                document.Add("_ti", Label16.Text)

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("Resident vehicle passing out the village successfully.")
                TextBox1.Text = ""
                ComboBox1.Text = ""

                Try
                    If serialport IsNot Nothing AndAlso serialport.IsOpen Then
                        Dim phoneNumber As String = TextBox2.Text.Trim()
                        If Not String.IsNullOrEmpty(phoneNumber) Then
                            ' Set up event handler for SMS completion
                            AddHandler serialport.DataReceived, AddressOf SmsSentHandler

                            ' Send the SMS
                            serialport.WriteLine("AT+CMGF=1")
                            System.Threading.Thread.Sleep(1000)
                            serialport.WriteLine($"AT+CMGS=""{phoneNumber}""")
                            System.Threading.Thread.Sleep(1000)

                            Dim message As String = "You are entered in the Private Village of Monte brisa at  "
                            serialport.Write(message & Label16.Text & Chr(26))
                            TextBox2.Text = ""
                        Else
                            MessageBox.Show("Error: Please enter a valid phone number.")
                        End If
                    Else
                        MessageBox.Show("Error: GSM module not connected.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Console.ReadLine()

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Try
            If String.IsNullOrWhiteSpace(TextBox1.Text) Then
                TextBox1.BackColor = Color.LightSkyBlue
                MsgBox("*Please Tap RFID Card")
                TextBox1.ForeColor = Color.Red
            ElseIf String.IsNullOrWhiteSpace(TextBox1.Text) OrElse String.IsNullOrWhiteSpace(ComboBox2.Text) Then
                MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Else
                ' Save data to MongoDB
                Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
                Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("time_logout")
                Dim document As BsonDocument = New BsonDocument()


                document.Add("_rno", TextBox1.Text)
                document.Add("_pnov", ComboBox2.Text)
                document.Add("_ti", Label16.Text)

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("Resident vehicle passing out the village successfully.")
                TextBox1.Text = ""
                ComboBox2.Text = ""

                Try
                    If serialport IsNot Nothing AndAlso serialport.IsOpen Then
                        Dim phoneNumber As String = TextBox2.Text.Trim()
                        If Not String.IsNullOrEmpty(phoneNumber) Then
                            ' Set up event handler for SMS completion
                            AddHandler serialport.DataReceived, AddressOf SmsSentHandler

                            ' Send the SMS
                            serialport.WriteLine("AT+CMGF=1")
                            System.Threading.Thread.Sleep(1000)
                            serialport.WriteLine($"AT+CMGS=""{phoneNumber}""")
                            System.Threading.Thread.Sleep(1000)

                            Dim message As String = "You are exited in the Private Village of Monte brisa at  "
                            serialport.Write(message & Label16.Text & Chr(26))
                            TextBox2.Text = ""

                        Else
                            MessageBox.Show("Error: Please enter a valid phone number.")
                        End If
                    Else
                        MessageBox.Show("Error: GSM module not connected.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Console.ReadLine()
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If String.IsNullOrWhiteSpace(TextBox9.Text) OrElse String.IsNullOrWhiteSpace(ComboBox3.Text) Then
            MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
            Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("non_resident_time_login")
            Dim document As BsonDocument = New BsonDocument()

            ' Check if a document with the specified "_rno" value already exists
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("_rno", TextBox9.Text)
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                MsgBox("This QR Code has been already used.", MsgBoxStyle.Exclamation)
                Return
            End If

            document.Add("_rno", TextBox9.Text)
            document.Add("_pnov", ComboBox3.Text)
            document.Add("_ti", Label16.Text)
            document.Add("_purpose", ComboBox5.Text)
            document.Add("_photo", TextBox7.Text)

            collection.InsertOne(document)
            Console.WriteLine("Document saved successfully.")
            MsgBox("Non-Resident vehicle passing in successfully.")
            TextBox9.Text = ""
            ComboBox3.Text = ""
            ComboBox5.Text = ""
            TextBox7.Text = ""
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If String.IsNullOrWhiteSpace(TextBox9.Text) OrElse String.IsNullOrWhiteSpace(ComboBox4.Text) Then
            MsgBox("Please fill in all the required fields.", MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
            Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("non_resident_time_logout")
            Dim document As BsonDocument = New BsonDocument()
            document.Add("_rno", TextBox9.Text)


            ' Check if a document with the specified "_rno" value already exists
            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of String)("_rno", TextBox9.Text)
            Dim existingDocument = collection.Find(filter).FirstOrDefault()

            If existingDocument IsNot Nothing Then
                MsgBox("This QR Code has been already used.", MsgBoxStyle.Exclamation)
                Return
            End If

            document.Add("_pnov", ComboBox4.Text)
            document.Add("_ti", Label16.Text)
            document.Add("_photo", TextBox7.Text)

            collection.InsertOne(document)
            Console.WriteLine("Document saved successfully.")
            MsgBox("Non-Resident vehicle passing out successfully.")
            TextBox7.Text = ""
            TextBox9.Text = ""
        Catch ex As Exception
            MsgBox($"Error: {ex.Message}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
    End Sub
End Class