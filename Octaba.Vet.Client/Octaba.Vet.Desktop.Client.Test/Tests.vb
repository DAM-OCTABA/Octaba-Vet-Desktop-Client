Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Octaba.Vet.Desktop.Client

<TestClass()> Public Class Tests

    <TestMethod()> Public Sub TestMethod1()

        Dim frm As New frmLogin
        Dim sum = frm.Sum(1, 2)

        Assert.AreEqual(3, sum)

        Dim frmM As New frmMenu
        Dim rest = frmM.res(2, 2)

        Assert.AreEqual(0, rest)
    End Sub

End Class