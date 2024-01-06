Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Imports System.IO

Public Class ViewResident
    ' Define your MongoDB client and database
    Private client As MongoClient
    Private database As IMongoDatabase
    Private collection As IMongoCollection(Of BsonDocument)

    Private selectedObjectId As ObjectId
    Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
    Dim dbName As String = "pvhosms_db"
    Dim collectionName As String = "residence"
    Private Sub ViewResident_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadListView()
        CenterToScreen()
        ListView1.FullRowSelect = True

        ' Initialize your MongoDB client and database
        client = New MongoClient(connectionString)
        database = client.GetDatabase(dbName)
        Collection = database.GetCollection(Of BsonDocument)(collectionName)
    End Sub
    Public Sub LoadListView()
        Try
            ' Clear existing items in the ListView
            ListView1.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "residence" ' Replace with your collection name

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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        updateinfo.Enabled = True
        If ListView1.SelectedItems.Count > 0 Then
            Dim selectedItem As ListViewItem = ListView1.SelectedItems(0)
            selectedObjectId = ObjectId.Parse(selectedItem.SubItems(0).Text)

            txtRFID.Text = selectedItem.SubItems(1).Text
            txtFirstName.Text = selectedItem.SubItems(2).Text
            txtMiddleName.Text = selectedItem.SubItems(3).Text
            txtLastName.Text = selectedItem.SubItems(4).Text
            blk.Text = selectedItem.SubItems(5).Text
            lt.Text = selectedItem.SubItems(6).Text
            str.Text = selectedItem.SubItems(7).Text
            ph.Text = selectedItem.SubItems(8).Text
            email.Text = selectedItem.SubItems(9).Text
            moN.Text = selectedItem.SubItems(10).Text
            NoR.Text = selectedItem.SubItems(11).Text
            ReC.Text = selectedItem.SubItems(12).Text
            photo.Text = selectedItem.SubItems(13).Text
        End If
    End Sub



    Private Sub updateinfo_Click_1(sender As Object, e As EventArgs) Handles updateinfo.Click
        updateinfo.Enabled = False
        If selectedObjectId <> ObjectId.Empty Then

            Dim client As New MongoClient(connectionString)
            Dim database As IMongoDatabase = client.GetDatabase(dbName)
            Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)(collectionName)

            Dim filter = Builders(Of BsonDocument).Filter.Eq(Of ObjectId)("_id", selectedObjectId)
            Dim update = Builders(Of BsonDocument).Update.
            Set(Of String)("Resident id(RFID no)", BsonValue.Create(txtRFID.Text)).
            Set(Of String)("first name", BsonValue.Create(txtFirstName.Text)).
            Set(Of String)("middle name", BsonValue.Create(txtMiddleName.Text)).
            Set(Of String)("last name", BsonValue.Create(txtLastName.Text)).
            Set(Of String)("block no)", BsonValue.Create(blk.Text)).
            Set(Of String)("lot no", BsonValue.Create(lt.Text)).
            Set(Of String)("street", BsonValue.Create(str.Text)).
            Set(Of String)("phase no", BsonValue.Create(ph.Text)).
            Set(Of String)("email", BsonValue.Create(email.Text)).
            Set(Of String)("contact no", BsonValue.Create(moN.Text)).
            Set(Of String)("no of each residence", BsonValue.Create(NoR.Text)).
            Set(Of String)("residence category", BsonValue.Create(ReC.Text)).
            Set(Of String)("photo", BsonValue.Create(photo.Text))
            collection.UpdateOne(filter, update)

            LoadListView()
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

        Else
            MessageBox.Show("Please select a row to update.")
        End If
    End Sub

    Private Sub seachbtn_Click(sender As Object, e As EventArgs) Handles seachbtn.Click
        Dim searchValue As String = TextBox1.Text.Trim()
        Dim filter = Builders(Of BsonDocument).Filter.Regex("Resident id(RFID no)", New BsonRegularExpression(searchValue, "i"))
        Dim searchResults = collection.Find(filter).ToList()

        If searchResults.Count > 0 Then
            ListView1.Items.Clear()
            For Each result In searchResults
                Dim item As New ListViewItem(result("Resident id(RFID no)").ToString())

                item.SubItems.Add(result("Resident id(RFID no)").ToString())
                item.SubItems.Add(result("first name").ToString())
                item.SubItems.Add(result("middle name").ToString())
                item.SubItems.Add(result("last name").ToString())
                item.SubItems.Add(result("block no)").ToString())
                item.SubItems.Add(result("lot no").ToString())
                item.SubItems.Add(result("street").ToString())
                item.SubItems.Add(result("phase no").ToString())
                item.SubItems.Add(result("email").ToString())
                item.SubItems.Add(result("contact no").ToString())
                item.SubItems.Add(result("no of each residence").ToString())
                item.SubItems.Add(result("residence category").ToString())
                item.SubItems.Add(result("photo").ToString())
                ListView1.Items.Add(item)
            Next
        Else
            MessageBox.Show("RFID number not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class