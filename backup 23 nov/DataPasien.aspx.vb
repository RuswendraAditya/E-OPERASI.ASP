Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration

Partial Class DataPasien
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (String.IsNullOrEmpty(Request.QueryString("pTglfrom")) = False) Then
                Me.TxtTanggalStart.Text = Request.QueryString("pTglfrom")
            Else
                Me.TxtTanggalStart.Text = MainLibWebERM.MasterLib.GetCurrentDate(clmain.ConString).ToString("dd/MM/yyyy")
            End If
            If (String.IsNullOrEmpty(Request.QueryString("pTglto")) = False) Then
                Me.TxtTanggalEnd.Text = Request.QueryString("pTglto")
            Else
                Me.TxtTanggalEnd.Text = MainLibWebERM.MasterLib.GetCurrentDate(clmain.ConString).ToString("dd/MM/yyyy")
            End If
            IsiGrid()

        End If


    End Sub



    Private Sub IsiGrid()
        Dim strsql As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)

        SdsData.ConnectionString = connectionString

        SdsData.DataSourceMode = SqlDataSourceMode.DataSet
        SdsData.ProviderName = "System.Data.SqlClient"
        Dim tgl_start As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(Me.TxtTanggalStart.Text))

        Dim tgl_end As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(Me.TxtTanggalEnd.Text))
        Dim type_tgl As String = "dt_tgl_trans"

        Select Case Me.DdlFilterData.SelectedIndex
            Case 0
                type_tgl = "dt_tgl_trans"
            Case 1
                type_tgl = "dt_tgl_daftar"
        End Select

        'strsql = " SELECT	RMP_inap.vc_no_reg vc_no_reg, RMP_inap.vc_no_rm VC_No_RM,RMPasien.vc_nama_p vc_nama_p,  " & _
        '              " RMPasien.vc_alamat, RMPasien.vc_kelurahan, RMPasien.vc_jenis_k, " & _
        '              " RMPasien.vc_BB, RMPasien.in_umurth, RMPasien.in_umurbl, RMPasien.in_umurhr,  " & _
        '              " RMRuang.VC_n_ruang vc_n_ruang, RMKelas.vc_n_kelas AS vc_kelas,   " & _
        '              " RMKelas.vc_k_kelas,	RMKamar.vc_nama,IBSJdwlOps.*   " & _
        '              " FROM RMP_inap " & _
        '              " INNER JOIN	  IBSJdwlOps On RMP_Inap.vc_no_reg = IBSJdwlOps.vc_no_reg  " & _
        '              " INNER JOIN    RMKamar ON CASE WHEN (RMP_inap.vc_KLAS_MUT IS NULL OR    RMP_inap.vc_KLAS_MUT = '') THEN RMP_inap.vc_Kd_kamar_Masuk ELSE RMP_inap.vc_kd_kamar_MUTasi END = RMKamar.vc_no_bed   " & _
        '              " INNER JOIN    RMPasien ON RMP_inap.vc_no_rm = RMPasien.vc_no_rm   " & _
        '              " INNER JOIN    RMRuang ON RMKamar.vc_k_gugus = RMRuang.VC_k_ruang  " & _
        '              " INNER JOIN    RMKelas ON CASE WHEN (RMP_inap.vc_KLAS_MUT IS NULL OR    RMP_inap.vc_KLAS_MUT = '') THEN RMP_inap.vc_Kd_klas_masuk ELSE RMP_inap.vc_Kd_klas_mutasi END = RMKelas.vc_k_kelas  " & _
        '              " WHERE	RMP_inap.dt_tgl_pul IS NULL  " & _
        '              " AND   IBSJdwlOps.vc_status in ('1','2','4') "
        strsql = " select	vc_no_trans, dt_tgl_trans,dt_tgl_operasi ,vc_jam_trans,  " _
                             & "        dt_tgl_daftar, aa.vc_no_rm,aa.vc_no_reg, vc_nama_p, " _
                             & "        vc_tindakan, vc_n_ruang, rmkelas.vc_n_kelas as kelas, case when isnull(aa.bt_jkn,0)=0 then 'NON JKN' else 'JKN' end as Tanggungan, aa.vc_status, bt_operasi " _
                             & " from	IBSJdwlOps aa " _
                             & "        inner join RMP_Inap bb on aa.vc_no_reg = bb.vc_no_reg " _
                             & "        inner join RMPasien cc on bb.vc_no_rm = cc.vc_no_rm "
        strsql &= "       inner join rmkamar ff ON CASE WHEN (bb.vc_KLAS_MUT IS NULL OR " _
                     & "        bb.vc_KLAS_MUT = '') THEN bb.vc_Kd_kamar_Masuk ELSE bb.vc_kd_kamar_MUTasi END = ff.vc_no_bed " _
                     & "        inner join RMRuang gg ON ff.vc_k_gugus = gg.VC_k_ruang " _
                     & "        INNER JOIN dbo.RMKelas ON dbo.RMKelas.vc_k_kelas = CASE WHEN (bb.vc_KLAS_MUT IS NULL OR " _
                     & "        bb.vc_KLAS_MUT = '') THEN bb.vc_Kd_KLAS_Masuk ELSE bb.vc_kd_KLAS_MUTasi END " _
                     & " WHERE	convert(varchar(10)," & type_tgl & ",120) Between '" & Format(tgl_start, "yyyy-MM-dd") & "' and '" & Format(tgl_end, "yyyy-MM-dd") & "' " _
                     & "   AND  aa.vc_status in  ('1','2','4')" _
                     & " UNION " _
                     & " SELECT	vc_no_trans, dt_tgl_trans, dt_tgl_operasi , vc_jam_trans, " _
                     & "        dt_tgl_daftar, aa.vc_no_rm,aa.vc_no_reg, vc_nama_p," _
                     & "        vc_tindakan, '' as vc_n_ruang, '' as kelas, 'NON JKN' as tanggungan, aa.vc_status, bt_operasi " _
                     & " FROM	IBSJdwlOps aa " _
                     & "        inner join RMKunjung bb on aa.vc_no_reg = bb.vc_no_regj " _
                     & "        inner join RMPasien cc on bb.vc_no_rm = cc.vc_no_rm " _
                     & " WHERE	convert(varchar(10)," & type_tgl & ",120) Between '" & Format(tgl_start, "yyyy-MM-dd") & "' and '" & Format(tgl_end, "yyyy-MM-dd") & "' " _
                     & "   AND  aa.vc_status in ('1','2','4')" _
                     & " ORDER BY aa.dt_tgl_trans,aa.vc_jam_trans"
        SdsData.SelectCommand = strsql

        GridView1.DataSourceID = SdsData.ID
    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'warna cursor
        Dim onmouseoverStyle As String = "this.style.backgroundColor='cyan' "

        Dim onmouseoutStyle As String = "this.style.backgroundColor='#@BackColor'"
        'Dim onmouseoutStyle As String = ""

        Dim rowBackColor As String = String.Empty

        If e.Row.RowType = DataControlRowType.DataRow Then
            'tambahan jika row diklik maka data sudah terpilih (tanpa harus mengklik tombol select
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Silahkan Klik baris data untuk dipilih..."
            e.Row.Attributes("style") = "cursor:pointer"
            '-------------------------------------------------------

            'warna saat cursor memilih
            Dim cStatus As String = DirectCast(DataBinder.Eval(e.Row.DataItem, "vc_no_trans"), String)
            If (IsDBNull(DataBinder.Eval(e.Row.DataItem, "dt_tgl_operasi")) = False) Then

                e.Row.BackColor = Drawing.Color.Pink
                e.Row.ForeColor = Drawing.Color.Blue
                e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            End If

            'set warna
            'If (IsDBNull(DataBinder.Eval(e.Row.DataItem, "dt_tgl_operasi")) = False) Then
            '    Dim tanggal_operasi As String = DataBinder.Eval(e.Row.DataItem, "dt_tgl_operasi")
            '    If (tanggal_operasi.Length = 0) Then

            '    Else
            '        e.Row.BackColor = Drawing.Color.Pink
            '        e.Row.ForeColor = Drawing.Color.Blue
            '        e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            '    End If

            'End If


            Dim operasi As Boolean = DirectCast(DataBinder.Eval(e.Row.DataItem, "bt_operasi"), Boolean)
            If (operasi = True) Then
                e.Row.BackColor = Drawing.Color.LightGreen
                e.Row.ForeColor = Drawing.Color.Blue
                e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            End If
            'If cLapOperasi <> "" Then
            '    e.Row.BackColor = Drawing.Color.LightGreen
            '    e.Row.ForeColor = Drawing.Color.DeepPink
            '    'onmouseoutStyle = "this.style.backgroundColor='LightGreen'"
            '    e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            'End If

            Select Case cStatus
                Case Request.QueryString("pNoTrans")
                    e.Row.BackColor = Drawing.Color.Blue
                    e.Row.ForeColor = Drawing.Color.Cyan
                    onmouseoutStyle = "this.style.backgroundColor='blue'"
                    e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            End Select

        End If



    End Sub

    Protected Sub CmdVIEW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdVIEW.Click
        IsiGrid()
    End Sub


    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        On Error Resume Next

        Dim cNoRM As String = CType(GridView1.SelectedRow.FindControl("lblNoRM"), Label).Text
        Dim cNoRegJ As String = CType(GridView1.SelectedRow.FindControl("lblNoRegJ"), Label).Text
        Dim cNoTrans As String = CType(GridView1.SelectedRow.FindControl("lblNoTrans"), Label).Text
        Dim tgl_from As String = Me.TxtTanggalStart.Text
        Dim tgl_to As String = Me.TxtTanggalEnd.Text
        Response.Redirect("~/DataPasien.aspx?" + "pNoRM=" + cNoRM + "&pNoReg=" + cNoRegJ + "&pNoTrans=" + cNoTrans + "&pTglfrom=" + tgl_from + "&pTglto=" + tgl_to)
        IsiGrid()
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

End Class
