Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration
Partial Class MasterPage2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("ssusername") = "" Then
            Response.Redirect("~/default.aspx")
        End If


        If Not Page.IsPostBack Then
            showdataSosial(Request.QueryString("pNoRM"))
        End If
    End Sub


    Sub mnu0101_click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/DataPasien.aspx?" + "pNoRM=" + Request.QueryString("pNoRM") + "&pNoReg=" + Request.QueryString("pNoReg") + "&pNoTrans=" + Request.QueryString("pNoTrans") + "&pTgl=" + Request.QueryString("pTgl"))
    End Sub

    Sub mnu0102_click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("~/FrmOperasi.aspx?" + "pNoRM=" + Request.QueryString("pNoRM") + "&pNoReg=" + Request.QueryString("pNoReg") + "&pNoTrans=" + Request.QueryString("pNoTrans") + "&pTgl=" + Request.QueryString("pTgl"))
    End Sub

    Sub mnu0103_click(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect(Request.RawUrl)
    End Sub


    Sub mnu0104_click(ByVal sender As Object, ByVal e As EventArgs)
        Session.Clear()
        Response.Redirect("~/default.aspx")
    End Sub


    Sub mnu0109_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim baseUrl As String = Request.Url.GetLeftPart(UriPartial.Authority)
        Response.Redirect(baseUrl + "/erm")
    End Sub


    Private Sub showdataSosial(ByVal cNoRm As String)
        Dim lada As Boolean = False
        Dim nNoUrut As Integer = 0
        Dim strsql As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim cAgama As String = ""
        Dim cKawin As String = ""
        Dim cPendidikan As String = ""
        Dim cNegara As String = ""
        Dim cPropinsi As String = ""
        Dim cKota As String = ""
        Dim cPekerjaan As String = ""
        Dim cKelurahan As String = ""
        Dim cKecamatan As String = ""

        On Error Resume Next
        LBLiNFO.Text = ""

        If cNoRm <> "" Then
            strsql = "select * from rmpasien where VC_no_rm='" & Trim(cNoRm) & "'"
            Dim sqlCommand As New SqlClient.SqlCommand(strsql)
            sqlCommand.Connection = connection
            connection.Open()

            Dim objdatareader As SqlClient.SqlDataReader
            objdatareader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
            While objdatareader.Read

                'Me.TxtNoRM.Text = cNoRm
                'Dim cTglLahir As String = Format(objdatareader("DT_tgl_lhr"), "dd/MM/yyyy")
                Dim cTglLahir As String = Format(objdatareader("DT_tgl_lhr"), "M/dd/yyyy")
                Dim cTglLahir2 As String = Format(objdatareader("DT_tgl_lhr"), "dd/MM/yyyy")

                LBLiNFO.Text = Trim(objdatareader("VC_Nama_P")).ToString
                '    TxtAlamat.Text = Trim(objdatareader("VC_ALAMAT")).ToString
                '    Me.TxtKelamin.Text = Trim(objdatareader("VC_Jenis_k")).ToString
                '    TxtTempatLahir.Text = Trim(objdatareader("VC_tp_lhr")).ToString
                '    'txttgllahir.Text = Trim(objdatareader("DT_tgl_lhr")).ToString
                '    TxtTglLahir.Text = cTglLahir
                '    TxtUmurTH.Text = Trim(objdatareader("IN_umurth")).ToString
                '    TxtUmurBln.Text = Trim(objdatareader("IN_umurbl")).ToString
                '    TxtUmurHr.Text = Trim(objdatareader("IN_umurhr")).ToString
                '    'If txttgllahir.Text <> "__/__/____" And IsDate(xDate(txttgllahir.Text)) Then
                '    If IsDate(CDate(cTglLahir)) Then
                '        'hitUmur(Me.txttgllahir.Text, Me.txtumurTh.Text, Me.TxtUmurBl.Text, Me.TxtUmurHari.Text)
                '        MainLibWebERM.MasterLib.hitUmur(cTglLahir, Me.TxtUmurTH.Text, Me.TxtUmurBln.Text, Me.TxtUmurHr.Text, clmain.ConString)
                '        Me.TxtTglLahir.Text = cTglLahir2
                '    Else
                '        If TxtUmurTH.Text <> "" Or TxtUmurTH.Text < 0 Then
                '            Me.TxtTglLahir.Text = MainLibWebERM.MasterLib.ValidTglLahir(TxtUmurTH.Text, MainLibWebERM.MasterLib.xDate(Me.TxtTglLahir.Text), clmain.ConString)
                '        End If
                '    End If
                '    'TxtAgama.Text = Trim(objdatareader("VC_k_agm")).ToString
                '    'Me.TxtNegara.Text = Trim(objdatareader("VC_k_negara")).ToString
                '    'TxtTuris.Text = Trim(objdatareader("VC_turis")).ToString
                '    'Me.TxtPekerjaan.Text = Trim(objdatareader("VC_k_pek")).ToString
                '    'TxtKawin.Text = Trim(objdatareader("VC_k_status")).ToString
                '    'TxtPendidikan.Text = Trim(objdatareader("VC_k_pend")).ToString
                '    TxtTelepon.Text = Trim(objdatareader("VC_telpon")).ToString
                '    'TxtPropinsi.Text = Trim(objdatareader("VC_k_prop")).ToString
                '    'TxtKota.Text = Trim(objdatareader("VC_k_kota")).ToString

                '    TxtKelurahan.Text = Trim(objdatareader("VC_kelurahan")).ToString
                '    TxtKecamatan.Text = Trim(objdatareader("VC_kecamatan")).ToString

                '    cKelurahan = Trim(objdatareader("VC_kelurahan")).ToString
                '    cKecamatan = Trim(objdatareader("VC_kecamatan")).ToString
                '    Me.TxtPegawaiRS.Text = IIf(objdatareader("BT_kary") = True, "Y", "T")
                '    TxtNik.Text = Trim(objdatareader("VC_nik")).ToString
                '    Me.TxtGolDarah.Text = Trim(objdatareader("VC_gol_d")).ToString
                '    cAgama = Trim(objdatareader("VC_k_agm")).ToString
                '    cKawin = Trim(objdatareader("VC_k_status")).ToString
                '    cPendidikan = Trim(objdatareader("VC_k_pend")).ToString
                '    cNegara = Trim(objdatareader("VC_k_negara")).ToString
                '    cPropinsi = Trim(objdatareader("VC_k_prop")).ToString
                '    cKota = Trim(objdatareader("VC_k_kota")).ToString
                '    cPekerjaan = Trim(objdatareader("VC_k_pek")).ToString

            End While
            'Me.TxtAgama.Text = MainLibWebERM.MasterLib.ShowData("VC_kode", "VC_agama", "pubAgama", cAgama, "", clmain.ConString)
            'Me.TxtKawin.Text = MainLibWebERM.MasterLib.ShowData("VC_kode", "VC_stkawin", "pubStKawin", cKawin, "", clmain.ConString)
            'Me.TxtPendidikan.Text = MainLibWebERM.MasterLib.ShowData("VC_kode", "VC_Pendidikan", "pubPddk", cPendidikan, "", clmain.ConString)
            'Me.TxtNegara.Text = MainLibWebERM.MasterLib.ShowData("VC_k_negara", "VC_n_negara", "pubnegara", cNegara, "", clmain.ConString)
            'Me.TxtPropinsi.Text = MainLibWebERM.MasterLib.ShowData("VC_kode", "VC_propinsi", "pubpropinsi", cPropinsi, "", clmain.ConString)
            'Me.TxtKota.Text = MainLibWebERM.MasterLib.ShowData("VC_kode", "VC_kota", "pubKota", cKota, "", clmain.ConString)
            'Me.TxtPekerjaan.Text = MainLibWebERM.MasterLib.ShowData("VC_Kode", "VC_Pekerjaan", "PubKerja", cPekerjaan, "", clmain.ConString)
            'objdatareader.Close()
        End If
    End Sub
End Class

