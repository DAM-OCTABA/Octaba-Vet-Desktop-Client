Public Class frmMenu
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        TextBox1.Text = Str(res(1, 2))
    End Sub
    Public Function res(a As Integer, b As Integer) As Integer
        Return a - b
    End Function
End Class