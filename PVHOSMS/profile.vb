Imports MongoDB.Driver
Imports MongoDB.Bson
Imports System.IO.Ports

Public Class profile
    Private r As BsonDocument

    Public Sub New(profileData As BsonDocument)
        InitializeComponent()
        r = profileData
    End Sub

    Private Sub profile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If r IsNot Nothing Then
            lebel01.Text = r("Resident id(RFID no)").ToString()
            label02.Text = r("first name").ToString()
            label03.Text = r("middle name").ToString()
            lebel04.Text = r("last name").ToString()
            label05.Text = r("email").ToString()
            lebel06.Text = r("contact no").ToString()
            lebel07.Text = r("no of each residence").ToString()
            lebel08.Text = r("residence category").ToString()
            lebel09.Text = r("block no)").ToString()
            lebel10.Text = r("lot no").ToString()
            lebel11.Text = r("street").ToString()
            lebel12.Text = r("phase no").ToString()
        End If
        CenterToScreen()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Me.Close()
    End Sub


End Class
