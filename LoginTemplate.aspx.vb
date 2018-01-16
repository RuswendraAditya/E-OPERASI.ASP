Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration


Partial Class LoginTemplate
    Inherits System.Web.UI.Page

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        Session.Clear()

        If Me.txtUserName.Text = "IBS" And Me.txtPwd.Text = "IBs15" Then
            Session("ssusernameTemp") = txtUserName.Text.ToString().Trim()

            Response.Redirect("~/Template.aspx")
        Else
            PESAN("Anda tidak berhak mengakses modul ini!...")
        End If


    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

End Class
