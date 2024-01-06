Imports MongoDB.Driver
Imports MongoDB.Bson


Public Class addemployee
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")

        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")



        Dim document As BsonDocument = New BsonDocument()
        document.Add("employee id", TextBox1.Text)

        document.Add("first name", TextBox2.Text)

        document.Add("middle name", TextBox3.Text)
        document.Add("last name", TextBox4.Text)

        document.Add("position)", TextBox5.Text)


        document.Add("username", TextBox6.Text)
        document.Add("password", TextBox7.Text)


        Try

            If TextBox1.Text = "" Then

                TextBox1.BackColor = Color.LightSkyBlue
                TextBox1.Text = "*required"
                TextBox1.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox2.Text = "" Then

                TextBox2.BackColor = Color.LightSkyBlue
                TextBox2.Text = "*required"
                TextBox2.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox3.Text = "" Then

                TextBox3.BackColor = Color.LightSkyBlue
                TextBox3.Text = "*required"
                TextBox3.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox4.Text = "" Then

                TextBox4.BackColor = Color.LightSkyBlue
                TextBox4.Text = "*required"
                TextBox4.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox5.Text = "" Then

                TextBox5.BackColor = Color.LightSkyBlue
                TextBox5.Text = "*required"
                TextBox5.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox6.Text = "" Then
                TextBox6.BackColor = Color.LightSkyBlue

                TextBox6.Text = "*required"
                TextBox6.ForeColor = Color.Red
                TextBox7.Text = ""
                TextBox7.Enabled = False
            End If
            If TextBox7.Text = "" Then

                TextBox7.BackColor = Color.LightSkyBlue
                TextBox7.Text = "*required"
                TextBox7.ForeColor = Color.Red
                TextBox7.Text = ""


            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("save succesfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""

                TextBox1.BackColor = Color.White
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White

                TextBox5.BackColor = Color.White

                TextBox6.BackColor = Color.White
                TextBox7.BackColor = Color.White
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Console.ReadLine()
    End Sub

    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        TextBox1.BackColor = Color.White
        TextBox1.Text = ""
        TextBox7.Enabled = True
        TextBox1.ForeColor = Color.Black

    End Sub
    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.BackColor = Color.White
        TextBox2.Text = ""
        TextBox7.Enabled = True
        TextBox2.ForeColor = Color.Black
    End Sub
    Private Sub TextBox3_GotFocus(sender As Object, e As EventArgs) Handles TextBox3.GotFocus
        TextBox3.BackColor = Color.White
        TextBox3.Text = ""
        TextBox7.Enabled = True
        TextBox3.ForeColor = Color.Black
    End Sub
    Private Sub TextBox4_GotFocus(sender As Object, e As EventArgs) Handles TextBox4.GotFocus
        TextBox4.BackColor = Color.White
        TextBox4.Text = ""
        TextBox7.Enabled = True
        TextBox4.ForeColor = Color.Black
    End Sub
    Private Sub TextBox5_GotFocus(sender As Object, e As EventArgs) Handles TextBox5.GotFocus
        TextBox5.BackColor = Color.White
        TextBox7.Enabled = True
        TextBox5.Text = ""
        TextBox5.ForeColor = Color.Black
    End Sub

    Private Sub TextBox6_GotFocus(sender As Object, e As EventArgs) Handles TextBox6.GotFocus
        TextBox6.BackColor = Color.White
        TextBox6.Text = ""
        TextBox7.Enabled = True
        TextBox6.ForeColor = Color.Black
    End Sub
    Private Sub TextBox7_GotFocus(sender As Object, e As EventArgs) Handles TextBox7.GotFocus
        TextBox7.BackColor = Color.White
        TextBox7.Text = ""
        TextBox7.Enabled = True
        TextBox7.ForeColor = Color.Black

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Public Sub LoadListView1()
        Try
            ' Clear existing items in the ListView
            ListView1.Items.Clear()

            ' Define the MongoDB connection string and database name
            Dim connectionString As String = "mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority"
            Dim dbName As String = "pvhosms_db"
            Dim collectionName As String = "employee" ' Replace with your collection name

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
            Label7.Text = ": " & itemCount.ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Private Sub addemployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadListView1()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadListView1()
    End Sub
End Class