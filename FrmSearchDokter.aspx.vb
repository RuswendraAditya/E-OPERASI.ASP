Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Partial Class FrmSearchDokter
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        IsiGridDokter()

    End Sub

    Private Sub IsiGridDokter()
        Dim strsql As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        SdsData.ConnectionString = connectionString
        SdsData.DataSourceMode = SqlDataSourceMode.DataSet
        SdsData.ProviderName = "System.Data.SqlClient"
        strsql = "SELECT  vc_nid,vc_nama_kry FROM    SDMDokter"
        SdsData.SelectCommand = strsql
        GridView1.DataSourceID = SdsData.ID
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes.Add("onmouseover", "ToolTip('Display some message');")
            e.Row.Attributes.Add("onMouseOver", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='cyan';this.style.cursor='pointer';")
            e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=this.originalstyle;")
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Klik untuk Memilih Dokter"
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim index As Integer = GridView1.SelectedIndex
        Session("qty_" + "1") = "OKE"
        Session("qty_" + "2") = "OKE_2"
        Session("test") = "OKE1111"
        Response.Redirect("default.aspx")
        'ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "closePage();", True)

        '        PESAN(Session("test"))

    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
End Class
