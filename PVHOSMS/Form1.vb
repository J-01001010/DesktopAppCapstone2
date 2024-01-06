Imports MongoDB.Driver
Imports MongoDB.Bson



Public Class Form1

    Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
    Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")


    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                                 Builders(Of BsonDocument).Filter.AnyEq("password", password)


        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dashboard.Show()

            TextBox1.Text = ""
            TextBox2.Text = ""

        Else
            MessageBox.Show("Login Failed. Please check your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub





    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                                 Builders(Of BsonDocument).Filter.AnyEq("password", password)


        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dashboard.Show()

            TextBox1.Text = ""
            TextBox2.Text = ""

        Else
            MessageBox.Show("Login Failed. Please check your credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        confirmation.Show()
        Me.Close()

    End Sub

    Private Sub Button1_Click_2(sender As Object, e As EventArgs) Handles Button1.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                                 Builders(Of BsonDocument).Filter.AnyEq("password", password)


        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()


        If user IsNot Nothing Then

            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim dashboardForm As New dashboard()
            dashboardForm.Show()
            Me.Hide()

            TextBox1.Text = ""
            TextBox2.Text = ""

        Else

            MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub Button2_Click_2(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs)
        smstest.Show()
    End Sub

    Private Sub Button3_Click_2(sender As Object, e As EventArgs)
        smstest.ShowDialog()
    End Sub

    Private Sub PerformLogin()
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                             Builders(Of BsonDocument).Filter.AnyEq("password", password)

        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()

        If user IsNot Nothing Then
            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            dashboard.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
        Else
            MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            PerformLogin()
        End If
    End Sub

    Private Sub PerformLogin1()
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")
        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("employee")

        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text

        Dim filter As FilterDefinition(Of BsonDocument) = Builders(Of BsonDocument).Filter.AnyEq("username", username) And
                                             Builders(Of BsonDocument).Filter.AnyEq("password", password)

        Dim user As BsonDocument = collection.Find(filter).FirstOrDefault()

        If user IsNot Nothing Then
            MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            dashboard.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
        Else
            MessageBox.Show("Invalid username or password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            PerformLogin1()
        End If
    End Sub
End Class
