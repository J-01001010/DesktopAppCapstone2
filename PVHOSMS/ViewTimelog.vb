Imports System.IO.File
Imports System.IO.FileStream
Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class ViewTimelog
    Private Sub ViewTimelog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadUsers()
        LoadUsers1()
        LoadListView2()
        LoadListView1()
        CenterToScreen()
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class