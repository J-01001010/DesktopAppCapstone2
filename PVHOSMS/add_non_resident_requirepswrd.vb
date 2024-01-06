Imports MongoDB.Driver
Imports MongoDB.Bson
Public Class add_non_resident_requirepswrd
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim password As String = TextBox1.Text



        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("password", password)



        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("authenticated")
            add_non_resident.ShowDialog()

            TextBox1.Text = ""
            Me.Hide()

        Else

            MessageBox.Show("cannot authenticate")
        End If
    End Sub
End Class