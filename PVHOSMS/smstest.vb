Imports System.IO.Ports

Public Class smstest
    Dim WithEvents serialPort As SerialPort

    Private Sub btn_Click(sender As Object, e As EventArgs) Handles btn.Click
        Try
            If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
                Dim phoneNumber As String = TextBox1.Text.Trim()
                If Not String.IsNullOrEmpty(phoneNumber) Then
                    ' Set up event handler for SMS completion
                    AddHandler serialPort.DataReceived, AddressOf SmsSentHandler

                    ' Send the SMS
                    serialPort.WriteLine("AT+CMGF=1")
                    System.Threading.Thread.Sleep(1000)
                    serialPort.WriteLine($"AT+CMGS=""{phoneNumber}""")
                    System.Threading.Thread.Sleep(1000)
                    Dim message As String = "Private Village: Thank you for your patience. Your delivery and vehicle appear to be in order. Please drive safely and have a great day!"
                    serialPort.WriteLine($"{message}{Chr(26)}")

                Else
                    MessageBox.Show("Error: Please enter a valid phone number.")
                End If
            Else
                MessageBox.Show("Error: GSM module not connected.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub smstest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Automatically connect to the COM port with the GSM module
        If ConnectToGSMModule() Then
            MessageBox.Show("Connected to GSM module successfully.")
        Else
            MessageBox.Show("Error: GSM module not found on any available COM port.")
        End If
    End Sub

    ' Search for available COM ports
    Private Function ConnectToGSMModule() As Boolean
        ' Search for available COM ports
        Dim availablePorts As String() = SerialPort.GetPortNames()

        ' Try connecting to each port to find the one with the GSM module
        For Each portName As String In availablePorts
            Try
                ' Attempt to open the port
                serialPort = New SerialPort(portName)
                serialPort.Open()

                ' Send a command to check if the GSM module is responding
                serialPort.WriteLine("AT")
                System.Threading.Thread.Sleep(1000)

                ' Check the response
                Dim response As String = serialPort.ReadExisting()
                If response.Contains("OK") Then
                    ' GSM module found on this port
                    Return True
                End If

                ' Close the port if the module is not found
                serialPort.Close()
            Catch ex As Exception
                ' Ignore exceptions and try the next port
            End Try
        Next

        ' No GSM module found on any port
        Return False
    End Function

    Private Sub SmsSentHandler(sender As Object, e As SerialDataReceivedEventArgs)
        Dim response As String = serialPort.ReadExisting()

        If response.Contains("OK") Then
            MessageBox.Show("SMS sent successfully!")
        End If
        RemoveHandler serialPort.DataReceived, AddressOf SmsSentHandler
    End Sub


End Class


