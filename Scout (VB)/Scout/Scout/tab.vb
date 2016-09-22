Imports AxSHDocVw
Imports System.Net
Imports mshtml


Public Class tab
    Public web As String
    Public doc As mshtml.IHTMLDocument3


    Public Function GetWebsite() As String
        Return AxWebBrowser1.LocationName
    End Function

    Public Sub NavigateTab(s As String)
        Try
            AxWebBrowser1.Navigate2(s)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub RemoveBookmarkBar()
        Panel4.Hide()
        Panel2.Location = New Point(Panel2.Location.X, Panel2.Location.Y - Panel4.Height)
        Panel2.Height = Panel2.Height + Panel4.Height

    End Sub

    Public Sub AddBookmarkBar()
        Panel4.Show()
        Panel2.Location = New Point(Panel2.Location.X, Panel2.Location.Y + Panel4.Height)
        Panel2.Height = Panel2.Height - Panel4.Height

    End Sub


    Private Sub tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim newtab As New tab
        Panel2.Hide()
        TextBox2.Hide()
        My.Settings.Panel2 = "Hidden"
        newtab.Dock = DockStyle.Fill
        My.Settings.Reload()
        AxWebBrowser1.Silent = True

        'listview
        Dim i As Integer = 1
        For Each p In My.Settings.AttachedBookmarks
            ListView1.Items.Add(p, i)
            i += 1
        Next


        'Show bookmarkfiled
        If Not My.Settings.ShowBookmarkBar Then
            RemoveBookmarkBar()
        End If
        '

        ' fix most visited pages on tablelayout2
        ' fast code
        If My.Settings.history.Count > 7 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
            b4.Text = My.Settings.history.Item(3)
            b5.Text = My.Settings.history.Item(4)
            b6.Text = My.Settings.history.Item(5)
            b7.Text = My.Settings.history.Item(6)
            b8.Text = My.Settings.history.Item(7)
        ElseIf My.Settings.history.Count > 6 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
            b4.Text = My.Settings.history.Item(3)
            b5.Text = My.Settings.history.Item(4)
            b6.Text = My.Settings.history.Item(5)
            b7.Text = My.Settings.history.Item(6)
        ElseIf My.Settings.history.Count > 5 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
            b4.Text = My.Settings.history.Item(3)
            b5.Text = My.Settings.history.Item(4)
            b6.Text = My.Settings.history.Item(5)
        ElseIf My.Settings.history.Count > 4 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
            b4.Text = My.Settings.history.Item(3)
            b5.Text = My.Settings.history.Item(4)
        ElseIf My.Settings.history.Count > 3 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
            b4.Text = My.Settings.history.Item(3)
        ElseIf My.Settings.history.Count > 2 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
            b3.Text = My.Settings.history.Item(2)
        ElseIf My.Settings.history.Count > 1 Then
            b1.Text = My.Settings.history.Item(0)
            b2.Text = My.Settings.history.Item(1)
        ElseIf My.Settings.history.Count > 0 Then
            b1.Text = My.Settings.history.Item(0)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            AxWebBrowser1.GoBack()
        Catch ex As Exception
            statustext("Couldn't Go Back...")
        End Try
    End Sub

    Public Sub RefreshPage()
        AxWebBrowser1.Refresh()

    End Sub



    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            AxWebBrowser1.GoForward()

        Catch ex As Exception
            statustext("Couldn't Go Forward...")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            AxWebBrowser1.Navigate2(My.Settings.homepage)
            TableLayoutPanel2.Hide()

        Catch ex As Exception
            statustext("Couldn't Go To " + My.Settings.homepage)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            AxWebBrowser1.Refresh()
        Catch ex As Exception
            statustext("Couldn't Refresh Page")
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            AxWebBrowser1.Stop()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Function isValidUrl(s As String) As Boolean
        Dim test As String = s

        If String.IsNullOrEmpty(test) Then Return False
        If test.Equals("about:blank") Then Return False
        If Not test.StartsWith("http://") And
        Not test.StartsWith("https://") Then
            If Not test.Contains(".") Then
                Return False
            End If
            test = "http://" & test
        End If

        If test.Contains(" ") Or test.Length = 0 Then
            Return False
        End If

        Return True

    End Function

    Private Sub DisplayOK(address As String)
        Try
            'should create new page?
            If My.Settings.Javascript Or My.Settings.Addblock Or My.Settings.IsSafe Then

                AxWebBrowser1.Navigate2(address)

                doc = AxWebBrowser1.Document

                If My.Settings.IsSafe Then
                    If doc.getElementsByTagName("Script").length > 0 Or doc.getElementsByTagName("script").length > 0 Then
                        'Ask if continue
                        'deny javascript

                        Dim result As Integer = MessageBox.Show("This Page contains scripts that might be harmful, do you want to continue?", "Script", MessageBoxButtons.YesNoCancel)
                        If result = DialogResult.Cancel Then

                        ElseIf result = DialogResult.No Then
                            AxWebBrowser1.Stop()
                            statustext("User Stopped Script...")
                        ElseIf result = DialogResult.Yes Then

                        End If
                    End If
                End If

                For Each e As IHTMLDOMNode In doc.All

                    If My.Settings.Javascript Then
                        If e.ToString.Contains("Script") Then
                            e.parentNode.removeChild(e)
                            Continue For
                        Else

                        End If
                    End If

                    If My.Settings.Addblock Then

                        'deny ads
                        If e.nodeName.Contains("ads") Or e.nodeName.Contains("Ads") Then
                            e.parentNode.removeChild(e)
                            Continue For
                        End If
                    End If

                Next

                web = doc.Body.OuterHtml.ToString()

            Else
                AxWebBrowser1.Navigate2(address)
                web = AxWebBrowser1.Document.Body.OuterHtml.ToString()

            End If



        Catch ex As Exception
            statustext("Could not Precheck Page...")
        End Try


    End Sub

    Private Sub GoUrl()
        Try
            If isValidUrl(TextBox1.Text) Then
                statustext("Loading " + TextBox1.Text + "...")
                DisplayOK(TextBox1.Text)

                TableLayoutPanel2.Hide()

            ElseIf My.Settings.SearchInTextField Then

                Dim searchURL As String = ""
                Select Case My.Settings.SearchEngine
                    Case "google"
                        searchURL = "https://www.google.com/search?q="
                        Exit Select
                    Case "bing"
                        searchURL = "https://www.bing.com/search?q="
                        Exit Select
                End Select

                'Fix searchable
                searchURL += TextBox1.Text.Replace(" ", "+")
                statustext("Loading " + searchURL + "...")
                DisplayOK(searchURL)

                TableLayoutPanel2.Hide()
            End If

        Catch ex As Exception
            statustext("Couldn't Load Page " + TextBox1.Text)
        End Try


    End Sub



    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        GoUrl()

    End Sub

    Public Sub statustext(s As String)
        TextBox2.Show()
        TextBox2.Text = s
        TextBox2.Width = TextBox2.TextLength * 6

        '    TextBox2.Hide()
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Panel2Status()
    End Sub

    Public Sub Panel2Status()
        Try
            If My.Settings.Panel2 = "Hidden" Then
                Panel2.Show()
                My.Settings.Panel2 = "Showing"
            Else
                If My.Settings.Panel2 = "Showing" Then
                    Panel2.Hide()
                    My.Settings.Panel2 = "Hidden"
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub toolstripTextBox1_keydown(ByVal sender As Object, ByVal e As KeyEventArgs) _
        Handles TextBox1.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            GoUrl()
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Settings.Show()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        End
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Downloader.Show()
    End Sub

    Private Sub AxWebBrowser1_Enter(sender As Object, e As EventArgs) Handles AxWebBrowser1.Enter

    End Sub

    Private Sub AxWebBrowser1_NavigateComplete2(sender As Object, e As DWebBrowserEvents2_NavigateComplete2Event) Handles AxWebBrowser1.NavigateComplete2


        Parent.Text = AxWebBrowser1.LocationName
        TextBox1.Text = AxWebBrowser1.LocationURL
        If My.Settings.SaveHistory Then
            If Not My.Settings.history.Contains(AxWebBrowser1.LocationURL.ToString) Then
                My.Settings.history.Add(AxWebBrowser1.LocationURL.ToString)
            Else
                ' put first in list
                My.Settings.history.RemoveAt(My.Settings.history.IndexOf(AxWebBrowser1.LocationURL.ToString))
                My.Settings.history.Insert(0, AxWebBrowser1.LocationURL.ToString())

            End If
        End If



        My.Settings.Save()
        TextBox2.Hide()
    End Sub

    Private Sub AxWebBrowser1_NewWindow2(sender As Object, e As DWebBrowserEvents2_NewWindow2Event) Handles AxWebBrowser1.NewWindow2
        Dim t As New TabPage
        Dim newtab As New tab
        newtab.Show()
        newtab.Dock = DockStyle.Fill
        newtab.AxWebBrowser1.RegisterAsBrowser = True
        e.ppDisp = newtab.AxWebBrowser1.Application
        newtab.Visible = True
        newtab.TopLevel = False
        t.Controls.Add(newtab)

        Form1.TabControl1.TabPages.Insert(Form1.TabControl1.TabPages.Count - 1, t)
        Form1.TabControl1.SelectedTab = t
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        'Bookmarks clicked
        Settings.Show()
        Settings.TabControl1.SelectedIndex = 4

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        'About clicked
        Settings.Show()
        Settings.TabControl1.SelectedIndex = 5
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        'Shortcuts clicked
        Shortcuts.Show()

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        '3,1 clicked


        Try

            Source.changeText(web)

            Source.Show()

        Catch ex As Exception
            MessageBox.Show("No Source Could be found!")
        End Try



    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        'Translate clicked
        Panel2.Hide()
        My.Settings.Panel2 = "Hidden"
        Try
            AxWebBrowser1.Navigate2("https://translate.google.se/")
        Catch ex As Exception

        End Try

        TableLayoutPanel2.Hide()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        '3,3 clicked

    End Sub


    Private Sub ButtonFunction(button As Button)
        If button.Text.Length > 0 Then
            AxWebBrowser1.Navigate2(button.Text)
            TableLayoutPanel2.Hide()
        End If


    End Sub

    Private Sub b1_Click(sender As Object, e As EventArgs) Handles b1.Click
        ButtonFunction(b1)

    End Sub

    Private Sub b2_Click(sender As Object, e As EventArgs) Handles b2.Click
        ButtonFunction(b2)
    End Sub

    Private Sub b3_Click(sender As Object, e As EventArgs) Handles b3.Click
        ButtonFunction(b3)
    End Sub

    Private Sub b4_Click(sender As Object, e As EventArgs) Handles b4.Click
        ButtonFunction(b4)
    End Sub

    Private Sub b5_Click(sender As Object, e As EventArgs) Handles b5.Click
        ButtonFunction(b5)
    End Sub

    Private Sub b6_Click(sender As Object, e As EventArgs) Handles b6.Click
        ButtonFunction(b6)
    End Sub

    Private Sub b7_Click(sender As Object, e As EventArgs) Handles b7.Click
        ButtonFunction(b7)
    End Sub

    Private Sub b8_Click(sender As Object, e As EventArgs) Handles b8.Click
        ButtonFunction(b8)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not My.Settings.Bookmarks.Contains(AxWebBrowser1.LocationURL) Then
            My.Settings.Bookmarks.Add(AxWebBrowser1.LocationURL)
        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

        If ListView1.FocusedItem.Index > 0 Then
            Dim s As String = ListView1.Items(ListView1.FocusedItem.Index).Text
            AxWebBrowser1.Navigate2(s)
            TableLayoutPanel2.Hide()
        End If

    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick

    End Sub

    Private Sub ListView1_MouseUp(sender As Object, e As MouseEventArgs) Handles ListView1.MouseUp
        If Not e.Button <> MouseButtons.Right Then
            If ListView1.Items.Count > 0 Then
                Dim result As Integer = MessageBox.Show("Want to Remove Bookmark? " + ListView1.Items(ListView1.FocusedItem.Index).Text, "Remove Bookmark", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then

                ElseIf result = DialogResult.No Then

                ElseIf result = DialogResult.Yes Then
                    My.Settings.AttachedBookmarks.Remove(ListView1.Items(ListView1.FocusedItem.Index).Text)
                    ListView1.Items.RemoveAt(ListView1.FocusedItem.Index)
                End If
            End If
        End If
        My.Settings.Save()

    End Sub

    Private Sub AxWebBrowser1_DownloadBegin(sender As Object, e As EventArgs) Handles AxWebBrowser1.DownloadBegin

    End Sub

    Private Sub AxWebBrowser1_FileDownload(sender As Object, e As DWebBrowserEvents2_FileDownloadEvent) Handles AxWebBrowser1.FileDownload
        If My.Settings.DontDownload Then
            AxWebBrowser1.Stop()
            statustext("Downloading disabled!")
        End If
    End Sub

    Private Sub AxWebBrowser1_DocumentComplete(sender As Object, e As DWebBrowserEvents2_DocumentCompleteEvent) Handles AxWebBrowser1.DocumentComplete
        web = AxWebBrowser1.Document.Body.OuterHtml.ToString()
    End Sub
End Class