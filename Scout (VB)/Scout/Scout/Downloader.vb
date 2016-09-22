Imports System.Net

Public Class Downloader

    Private WithEvents httpclient As WebClient

    Private Sub Downloader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Downloader for " + Application.ProductName
        Label3.Text = "Current Status: Idle..."
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SaveFileDialog1.ShowDialog()
        SaveFileDialog1.Title = "Save File To...."
        TextBox2.Text = SaveFileDialog1.FileName
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ProgressBar1.Style = ProgressBarStyle.Continuous
        MsgBox("Starting Download...")
        ProgressBar1.Style = ProgressBarStyle.Blocks
        httpclient = New WebClient
        Dim download As String = TextBox1.Text
        Dim save As String = TextBox2.Text
        httpclient.DownloadFileAsync(New Uri(download), save)
        Label3.Text = "Current Status: Downloading..."
    End Sub

    Private Sub httpclient_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs) Handles httpclient.DownloadProgressChanged

        ProgressBar1.Maximum = e.TotalBytesToReceive
        ProgressBar1.Value = e.BytesReceived
        Label4.Text = "Download Status: " & e.ProgressPercentage & "%"
        If e.ProgressPercentage = 100 Then
            Label3.Text = "Current Status: COMPLETE!"
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        httpclient.CancelAsync()
    End Sub


End Class