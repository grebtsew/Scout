Imports System.ComponentModel

Public Class Settings


    Private Sub Settings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Form1.Enabled = True

    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Form1.Enabled = False

        Try

            Label13.Text = My.Settings.history.Count

            If Not My.Settings.history.Count = 0 Then
                Dim ihist As Integer
                Do Until ListBox1.Items.Count = My.Settings.history.Count
                    ListBox1.Items.Add(My.Settings.history(ihist))
                    ihist += 1
                Loop
            End If

            If Not My.Settings.Bookmarks.Count = 0 Then
                Dim ihist As Integer
                Do Until ListBox2.Items.Count = My.Settings.Bookmarks.Count
                    ListBox2.Items.Add(My.Settings.Bookmarks(ihist))
                    ihist += 1
                Loop
            End If

        Catch ex As Exception

        End Try


        Try

            ' Set data as it should
            TextBox1.Text = My.Settings.homepage
            TextBox2.Text = My.Settings.SaveFolder
            CheckBox1.Checked = My.Settings.OpenHomePage
            CheckBox2.Checked = My.Settings.SearchInTextField
            CheckBox3.Checked = My.Settings.OpenLatestSession
            CheckBox4.Checked = My.Settings.SaveHistory
            CheckBox5.Checked = My.Settings.Javascript
            CheckBox6.Checked = My.Settings.IsSafe
            CheckBox7.Checked = My.Settings.Addblock
            CheckBox8.Checked = My.Settings.DontDownload
            CheckBox10.Checked = My.Settings.ShowWindowBorder
            CheckBox11.Checked = My.Settings.ShowBookmarkBar

        Catch ex As Exception

        End Try

        Try

            Dim array1() As String = {"google", "bing"}
            ComboBox1.Items.AddRange(array1)


            Select Case My.Settings.SearchEngine
                Case "google"
                    ComboBox1.SelectedIndex = 0
                    Exit Select
                Case "bing"
                    ComboBox1.SelectedIndex = 1
                    Exit Select
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.homepage = TextBox1.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Settings.history.Clear()
        ListBox1.Items.Clear()
        MsgBox("History Cleared!")
        Label13.Text = My.Settings.history.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        My.Settings.history.Remove(ListBox1.SelectedItem)
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        Label13.Text = My.Settings.history.Count

    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub GroupBox6_Enter(sender As Object, e As EventArgs) Handles GroupBox6.Enter

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        My.Settings.Save()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        My.Settings.Save()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        My.Settings.Save()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        My.Settings.Save()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        My.Settings.Save()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            My.Settings.OpenHomePage = True
        Else
            My.Settings.OpenHomePage = False
        End If
    End Sub



    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.SearchEngine = ComboBox1.SelectedItem


    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            My.Settings.SearchInTextField = True
        Else
            My.Settings.SearchInTextField = False
        End If

    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked Then
            My.Settings.SaveHistory = True
        Else
            My.Settings.SaveHistory = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            My.Settings.OpenLatestSession = True
        Else
            My.Settings.OpenLatestSession = False
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        My.Settings.SaveFolder = TextBox2.Text
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked Then
            My.Settings.DontDownload = True
        Else
            My.Settings.DontDownload = False
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked Then
            My.Settings.Javascript = True
        Else
            My.Settings.Javascript = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            My.Settings.IsSafe = True
        Else
            My.Settings.IsSafe = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked Then
            My.Settings.Addblock = True
        Else
            My.Settings.Addblock = False
        End If
    End Sub


    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10.CheckedChanged
        'show window border
        If CheckBox10.Checked Then
            My.Settings.ShowWindowBorder = True
        Else
            My.Settings.ShowWindowBorder = False
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Settings.Bookmarks.Remove(ListBox1.SelectedItem)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        My.Settings.Bookmarks.Clear()
        ListBox2.Items.Clear()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ' Attach to bookmarkfield
        My.Settings.AttachedBookmarks.Add(ListBox2.SelectedItem)

    End Sub

    Private Sub Settings_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing



        If Not My.Settings.ShowWindowBorder Then
            Form1.ControlBox = False
        Else
            Form1.ControlBox = True
        End If

        If Not My.Settings.ShowBookmarkBar Then
            tab.RemoveBookmarkBar()
        Else
            tab.AddBookmarkBar()
        End If


        My.Settings.Save()


    End Sub

    Private Sub CheckBox11_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox10.Checked Then
            My.Settings.ShowBookmarkBar = True
        Else
            My.Settings.ShowBookmarkBar = False
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        SaveFileDialog1.ShowDialog()
        TextBox2.Text = SaveFileDialog1.FileName
        My.Settings.SaveFolder = TextBox2.Text
    End Sub
End Class