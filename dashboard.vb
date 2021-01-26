Public Class dashboard

    Private Property btnOrder As Object

    Private Sub btnDM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDM.Click
        If (DAFTARMENU_baru = False) Then
            BikinMenu(cldDM, TabControl1, "DAFTAR MENU")
            DAFTARMENU_baru = True
        Else
            x = getTabIndex(TabControl1, "DAFTAR MENU")
            TabControl1.SelectedTabIndex = x
        End If
    End Sub
    Private Sub btnmakanan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMknan.Click
        If (MAKANAN_baru = False) Then
            BikinMenu(cldMK, TabControl1, " Makanan ")
            MAKANAN_baru = True
        End If
    End Sub


    Private Sub TabControl1_ControlAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles TabControl1.ControlAdded
        TabControl1.SelectedTabIndex = TotalTab - 1
    End Sub

    Private Sub TabControl1_TabItemClose(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.TabStripActionEventArgs) Handles TabControl1.TabItemClose
        Dim itemToRemove As DevComponents.DotNetBar.TabItem = TabControl1.SelectedTab
        If (TotalTab > 2) Then
            TotalTab = TotalTab - 1
        Else
            TotalTab = 0
        End If


        TabControl1.Tabs.Remove(itemToRemove)
        TabControl1.Controls.Remove(itemToRemove.AttachedControl)
        TabControl1.RecalcLayout()

        If (itemToRemove.ToString = "User") Then
            menu_baru = False
        ElseIf (itemToRemove.ToString = "Search") Then
            MINUMAN_baru = False
        ElseIf (itemToRemove.ToString = "Report") Then
            MAKANAN_baru = False
        Else
        End If
    End Sub

    Private Sub TabControl1_TabItemOpen(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.TabItemOpen
        If (TotalTab = 0) Then
            TotalTab = TotalTab + 2
        Else
            TotalTab = TotalTab + 1
        End If
    End Sub

    Private Sub RibbonPanel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RibbonPanel1.Click

    End Sub

End Class  
