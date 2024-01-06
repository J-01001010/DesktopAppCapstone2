Imports MongoDB.Driver

Imports MongoDB.Bson


Public Class super_operator
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim client As MongoClient = New MongoClient("mongodb+srv://albertzkie:Ewankonga123@pvhosms.jghekic.mongodb.net/?retryWrites=true&w=majority")
        Dim database As IMongoDatabase = client.GetDatabase("pvhosms_db")

        Dim collection As IMongoCollection(Of BsonDocument) = database.GetCollection(Of BsonDocument)("super_system_operator")



        Dim document As BsonDocument = New BsonDocument()
        document.Add("super_admin_id", TextBox1.Text)

        document.Add("name", TextBox2.Text)

        document.Add("username", TextBox3.Text)
        document.Add("password", TextBox4.Text)




        Try

            If TextBox1.Text = "" Then

                TextBox1.BackColor = Color.LightSkyBlue
                TextBox1.Text = "*required"
                TextBox1.ForeColor = Color.Red

            End If
            If TextBox2.Text = "" Then

                TextBox2.BackColor = Color.LightSkyBlue
                TextBox2.Text = "*required"
                TextBox2.ForeColor = Color.Red

            End If
            If TextBox3.Text = "" Then

                TextBox3.BackColor = Color.LightSkyBlue
                TextBox3.Text = "*required"
                TextBox3.ForeColor = Color.Red

            End If
            If TextBox4.Text = "" Then

                TextBox4.BackColor = Color.LightSkyBlue
                TextBox4.Text = "*required"
                TextBox4.ForeColor = Color.Red



            Else

                collection.InsertOne(document)
                Console.WriteLine("Document saved successfully.")
                MsgBox("save succesfully")
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""


                TextBox1.BackColor = Color.White
                TextBox2.BackColor = Color.White
                TextBox3.BackColor = Color.White
                TextBox4.BackColor = Color.White

            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Console.ReadLine()
    End Sub
End Class