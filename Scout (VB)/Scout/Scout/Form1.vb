Imports System.ComponentModel

Public Class Form1

    Public OldSelectedIndex As Integer = 0


    Public Shared inFullscreen As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "SCOUT (YPWB) | v" + Application.ProductVersion + " ALPHA | DW "

        ' Remove Window borders
        If Not My.Settings.ShowWindowBorder Then
            ControlBox = False
        End If


        My.Settings.LastSessionList.Clear()

        If My.Settings.OpenLatestSession Then

            TabControl1.TabPages.RemoveAt(1)

            For Each s In My.Settings.LastSessionList

                Dim t As New TabPage
                Dim newtab As New tab
                newtab.Show()
                newtab.TopLevel = False
                newtab.Dock = DockStyle.Fill
                newtab.NavigateTab(s)
                newtab.TableLayoutPanel2.Hide()

                t.Controls.Add(newtab)

                TabControl1.TabPages.Insert(TabControl1.TabPages.Count - 1, t)

            Next

        ElseIf My.Settings.OpenHomePage

            TabControl1.TabPages.RemoveAt(1)
            Dim t As New TabPage
            Dim newtab As New tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            newtab.NavigateTab(My.Settings.homepage)
            newtab.TableLayoutPanel2.Hide()

            t.Controls.Add(newtab)

            TabControl1.TabPages.Insert(TabControl1.TabPages.Count - 1, t)

        Else

            Dim newtab As New tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            TabPage2.Controls.Add(newtab)

        End If

        TabControl1.SelectedIndex = 1
        MenuStrip1.Show()
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs)


    End Sub


    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)

    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        Dim t As New TabPage
        Dim newtab As New tab
        newtab.Show()
        newtab.TopLevel = False
        newtab.Dock = DockStyle.Fill
        t.Controls.Add(newtab)
        TabControl1.TabPages.Insert(TabControl1.TabPages.Count - 1, t)
    End Sub

    Private Sub RemoveTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveTabToolStripMenuItem.Click
        If TabControl1.TabCount = 1 Then
            Dim t As New TabPage
            Dim newtab As New tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            t.Controls.Add(newtab)
            TabControl1.TabPages.Add(t)
            TabControl1.SelectedTab.Dispose()
        Else
            TabControl1.SelectedTab.Dispose()
        End If
    End Sub

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click
        Try
            tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        Try
            tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINTPREVIEW, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub FullscreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullscreenToolStripMenuItem.Click
        Try
            If inFullscreen = True Then
                Me.FormBorderStyle = FormBorderStyle.Sizable
                Me.TopMost = False
                Me.WindowState = FormWindowState.Normal
                FullscreenToolStripMenuItem.Visible = True
                inFullscreen = False

            Else
                If inFullscreen = False Then
                    Me.FormBorderStyle = FormBorderStyle.None
                    Me.TopMost = True
                    Me.WindowState = FormWindowState.Maximized
                    inFullscreen = True
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PageSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PageSetupToolStripMenuItem.Click
        Try
            tab.AxWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_PAGESETUP, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER)
        Catch ex As Exception

        End Try


    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        Settings.Show()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            tab.RefreshPage()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ExitToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub TabPage2_TabIndexChanged(sender As Object, e As EventArgs) Handles TabPage2.TabIndexChanged

    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As TabControlEventArgs) Handles TabControl1.Selected

        ' add new field
        If TabControl1.SelectedIndex = TabControl1.TabPages.Count - 1 Then

            Dim t As New TabPage
            Dim newtab As New tab
            newtab.Show()
            newtab.TopLevel = False
            newtab.Dock = DockStyle.Fill
            t.Controls.Add(newtab)
            TabControl1.TabPages.Insert(TabControl1.TabPages.Count - 1, t)
            TabControl1.SelectedIndex = TabControl1.TabPages.Count - 2

            ' remove a field
        ElseIf TabControl1.SelectedIndex = 0 Then
            If TabControl1.TabCount = 3 Then

                Dim newtab As New tab
                newtab.Show()
                newtab.TopLevel = False
                newtab.Dock = DockStyle.Fill
                TabPage2.Controls.Add(newtab)
                TabControl1.SelectedIndex = 1
            Else
                TabControl1.TabPages.RemoveAt(OldSelectedIndex)
                TabControl1.SelectedIndex = 1
            End If
        End If

        OldSelectedIndex = TabControl1.SelectedIndex
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If My.Settings.OpenLatestSession Then
            For Each Tab In TabControl1.TabPages

                'If first / If last
                If TabControl1.TabPages(TabControl1.TabPages.Count - 1).Equals(Tab) Or TabControl1.TabPages(0).Equals(Tab) Then

                Else
                    Dim s As String = Tab.ToString

                    s = s.Replace("TabPage:", "")
                    s = s.Replace("{", "")
                    s = s.Replace("}", "")

                    Console.WriteLine(s)
                    My.Settings.LastSessionList.Add(s)

                End If
            Next

            My.Settings.Save()


            End

        End If
    End Sub
End Class
