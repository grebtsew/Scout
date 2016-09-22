Public Class Shortcuts
    Private Sub Shortcuts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ListBox1.Items.Add("Function")
        ListBox2.Items.Add("Shortcuts")
        ListBox1.Items.Add("New Tab")
        ListBox2.Items.Add("ctrl + t")
        ListBox1.Items.Add("Remove Tab")
        ListBox2.Items.Add("ctrl + d")
        ListBox1.Items.Add("Print")
        ListBox2.Items.Add("ctrl + p")
        ListBox1.Items.Add("Print Preview")
        ListBox2.Items.Add("ctrl + shift + p")
        ListBox1.Items.Add("Fullscreen")
        ListBox2.Items.Add("ctrl + shift + f")
        ListBox1.Items.Add("PageSetup")
        ListBox2.Items.Add("ctrl + alt + shift + p")
        ListBox1.Items.Add("Settings")
        ListBox2.Items.Add("ctrl + s")
        ListBox1.Items.Add("Refresh")
        ListBox2.Items.Add("F5")

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged

    End Sub
End Class