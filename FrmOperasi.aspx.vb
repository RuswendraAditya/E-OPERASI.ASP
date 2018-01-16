Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration

Partial Class FrmOperasi
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            With Me.ddloperasi
                .DataSource = MainLibWebERM.MasterLib.SetDataSourceTemplate(clmain.ConString)
                .DataTextField = "vc_nama_operasi"
                .DataValueField = "vc_deskripsi"
                .DataBind()
            End With


            'cek apakah No.trans ada di jawwal operasi
            'Jika tidak ada maka tidak bisa dilakukan pengisian laporan operasi

            If MainLibWebERM.MasterLib.ShowData("vc_no_trans", "vc_no_trans", "IBSJdwlOps", Request.QueryString("pNoTrans"), "", clmain.ConString) = "" Then
                PESAN("Pasien ini belum terjadwal di daftar pasien operasi...")

                Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
                Response.Redirect(baseUrl + "/operasi/datapasien.aspx")

            End If

            'Tampilkan datanya jika sudah pernah diisi oleh dokter laporan operasinya
            Dim cKodeOperasi As String = ""
            Dim nIndexOperasi As Integer = 0

            MainLibWebERM.MasterLib.GetLapOperasi(Request.QueryString("pNoTrans"), cKodeOperasi, Me.TxtDeskripsi.Text, nIndexOperasi, clmain.ConString)

            Me.ddloperasi.SelectedIndex = nIndexOperasi
        End If


    End Sub

    Protected Sub ddloperasi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddloperasi.SelectedIndexChanged
        Me.TxtDeskripsi.Text = Me.ddloperasi.SelectedValue
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub


    Protected Sub cmdInput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInput.Click
        Dim lLengkap As Boolean = True

        If Me.TxtDeskripsi.Text = "" Then
            lLengkap = False
            PESAN("Deskripsi tidak boleh kosong...")
            Me.TxtDeskripsi.Focus()
        End If


        If lLengkap Then
            If simpanData() Then
                PESAN("Data Berhasil disimpan")
            Else
                PESAN("Data Gagal disimpan")
            End If
        Else
            PESAN("Data harus dilengkapi semua...")
            Me.TxtDeskripsi.Focus()
        End If

    End Sub

    Function simpanData() As Boolean
        Dim strsql As String = ""

        simpanData = False
        Dim lFound As Boolean = False
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim lAda As Boolean = False
        Dim dTglServer As Date = MainLibWebERM.MasterLib.GetCurrentDate(clmain.ConString)


        If MainLibWebERM.MasterLib.ShowData("vc_no_trans", "vc_no_trans", "IBSLapOperasi", Request.QueryString("pNoTrans"), "", clmain.ConString) <> "" Then
            lAda = True
        End If

        Dim MyTrans As SqlTransaction
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans

        Try

            If lAda Then
                strsql = "update IBSLapOperasi set vc_deskripsi = '" & Me.TxtDeskripsi.Text & "' WHERE VC_no_trans = '" & Request.QueryString("pNoTrans") & "' "
            Else

                strsql = "insert into IBSLapOperasi(vc_no_trans, vc_no_rm, vc_no_reg, vc_kode_operasi, vc_deskripsi, in_index_operasi, vc_nid, vc_operator, dt_TGL_create) " & _
                                                 "values " & _
                                                 "('" & Request.QueryString("pNoTrans") & "', '" & Request.QueryString("pNoRM") & "', '" & Request.QueryString("pNoReg") & "', '" & Me.ddloperasi.SelectedItem.Text & "', '" & Me.TxtDeskripsi.Text & "', " & Me.ddloperasi.SelectedIndex & ", '" & Session("ssNidLogin") & "', '" & Session("ssusername") & "', '" & dTglServer & "')"
            End If

            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()

            simpanData = True
        Catch ex As Exception
            simpanData = False
            MyTrans.Rollback()
        End Try
    End Function

    Protected Sub CmdKeluar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdKeluar.Click
        Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
        Response.Redirect(baseUrl + "/operasi/datapasien.aspx?&pTgl=" + Request.QueryString("pTgl"))
    End Sub

    Protected Sub cmdhapus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdhapus.Click
        Dim lLengkap As Boolean = True
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()


        Dim strsql As String = ""


        Dim MyTrans As SqlTransaction
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans

        Try
            'hapus Laporan Operasi
            strsql = "delete from IBSLapOperasi where vc_no_trans = '" & Request.QueryString("pNoTrans") & "'"

            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()

            Me.TxtDeskripsi.Text = ""
            Me.ddloperasi.SelectedIndex = 0

            PESAN("Data Berhasil dihapus!...")

            'Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            MyTrans.Rollback()
            PESAN("Data Gagal dihapus!...")
        End Try

    End Sub
End Class
