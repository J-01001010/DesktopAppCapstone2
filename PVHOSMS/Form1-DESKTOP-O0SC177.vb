Imports MongoDB.Driver
Imports MongoDB.Bson



Public Class Form1

    Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                                         Builders(Of BsonDocument).Filter.AnyEq("password", password)


        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("Login successful!")
            dashboard.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""

        Else
            MessageBox.Show("Invalid username or password.")
            TextBox1.Text = ""
            TextBox2.Text = ""
        End If



    End Sub


End Class
