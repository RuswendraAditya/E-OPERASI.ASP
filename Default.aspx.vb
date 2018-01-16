Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration


Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub btnLogIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogIn.Click
        Session.Clear()

        Session("ssNidDokter") = ""
        If MainLibWebERM.MasterLib.CheckUser(Me.txtUserName.Text, Me.txtPwd.Text, clmain.ConString, "") Then
            Session("ssusername") = txtUserName.Text.ToString().Trim()
            Session("sshakaksespilih") = "OK"
            Session("ssNidLogin") = MainLibWebERM.MasterLib.ShowData("vc_username", "vc_nid", "pde_user", Me.txtUserName.Text, "", clmain.ConString)
            If Session("ssNidLogin") = "NULL" Then
                Session("ssNidLogin") = ""
            End If
            Session("cIdUser") = MainLibWebERM.MasterLib.ShowData("vc_username", "vc_id", "pde_user", Me.txtUserName.Text, "", clmain.ConString)

            If MainLibWebERM.MasterLib.LoginModulOperasi(Session("cIdUser"), clmain.ConString) = True Then
                Response.Redirect("~/Datapasien.aspx")
            Else
                PESAN("Anda tidak berhak mengakses modul ini!...")
            End If

        Else
            Me.txtUserName.Focus()
        End If

    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

End Class
