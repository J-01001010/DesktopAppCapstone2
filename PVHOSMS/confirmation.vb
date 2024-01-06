Imports System.Net.Http
Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson

Public Class Confirmation
    Private selectedObjectId As ObjectId
    Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
    Dim dbName As String = "pvhosms_db"
    Dim collectionName As String = "reserved"

    Private Sub confirmation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadListView1()
        CenterToScreen()
        ListView1.FullRowSelect = True
    End Sub
    Public Sub LoadListView1()
        Try
            ' Clear existing items in the ListView
            ListView1.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "reservations"

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
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = ListView1.SelectedItems(0)
            Dim objectIdString As String = selectedItem.SubItems(0).Text

            If ObjectId.TryParse(objectIdString, selectedObjectId) Then
                TextBox1.Text = If(selectedItem.SubItems(1).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(1).Text, "")
                txtRFID.Text = If(selectedItem.SubItems(2).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(2).Text, "")
                txtFirstName.Text = If(selectedItem.SubItems(3).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(3).Text, "")
                txtMiddleName.Text = If(selectedItem.SubItems(4).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(4).Text, "")
                txtLastName.Text = If(selectedItem.SubItems(5).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(5).Text, "")
                blk.Text = If(selectedItem.SubItems(6).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(6).Text, "")
                lt.Text = If(selectedItem.SubItems(7).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(7).Text, "")
                str.Text = If(selectedItem.SubItems(8).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(8).Text, "")
                ph.Text = If(selectedItem.SubItems(9).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(9).Text, "")
                email.Text = If(selectedItem.SubItems(10).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(10).Text, "")
                moN.Text = If(selectedItem.SubItems(11).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(11).Text, "")
                NoR.Text = If(selectedItem.SubItems(12).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(12).Text, "")
                ReC.Text = If(selectedItem.SubItems(13).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(13).Text, "")
                photo.Text = If(selectedItem.SubItems(14).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(14).Text, "")
                status1.Text = If(selectedItem.SubItems(15).Text <> BsonNull.Value.ToString(), selectedItem.SubItems(15).Text, "")
            Else
                MessageBox.Show("Error parsing ObjectId.")
            End If
        End If
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If selectedObjectId <> ObjectId.Empty Then
            If status1.Text = "Reserved" Then
                Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
                Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
                Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("reserved")

                Dim document As BsonDocument = New BsonDocument()
                document.Add("rfid", TextBox1.Text)
                document.Add("name)", txtRFID.Text)
                document.Add("residency", txtFirstName.Text)
                document.Add("email", txtMiddleName.Text)
                document.Add("event", txtLastName.Text)
                document.Add("DaysOfEvent", blk.Text)
                document.Add("OneDayEvent", lt.Text)
                document.Add("TimeIn", str.Text)
                document.Add("TimeOut", ph.Text)
                document.Add("FirstDay", email.Text)
                document.Add("Ftimein", moN.Text)
                document.Add("LastDay", NoR.Text)
                document.Add("Ltimeout", ReC.Text)
                document.Add("NumberOfGuest", photo.Text)
                document.Add("status", status1.Text)

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("Residence reserved successfully.")

                LoadListView1()
                TextBox1.Text = ""
                txtRFID.Text = ""
                txtFirstName.Text = ""
                txtMiddleName.Text = ""
                txtLastName.Text = ""
                blk.Text = ""
                lt.Text = ""
                str.Text = ""
                ph.Text = ""
                email.Text = ""
                moN.Text = ""
                NoR.Text = ""
                ReC.Text = ""
                photo.Text = ""
                status1.Text = ""
            Else
                MessageBox.Show("Status must be 'Reserved' to save the data.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Else
            MessageBox.Show("Please fill up all fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        reserved.Show()
    End Sub


End Class
