Imports MongoDB.Driver

Imports MongoDB.Bson
Public Class employee_requiredpwsrd
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("super_system_operator")

        Dim password As String = TextBox1.Text
        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("password", password)
        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("Admin authenticated successfully. Welcome Back!")
            addemployee.ShowDialog()

            TextBox1.Text = ""
            Me.Hide()

        Else
            MessageBox.Show("Error: Authentication failed. Please check your credentials and try again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        dashboard.Show()
    End Sub
End Class