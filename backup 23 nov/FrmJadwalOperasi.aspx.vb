Imports System.Web
Imports System.Configuration
Imports System.Data
Imports System.Data.Sql
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Partial Class FrmJadwalOperasi
    Inherits System.Web.UI.Page
    Private status As String = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            LoadDataPetugas()
            GetNamaRuangOp()
            GetTindakanOp()
            GetAlasanBatal()
            loadDataOperasiPasien()
            Dim check_dokter As Boolean = False
            check_dokter = checkDokterExists()
            If (check_dokter) Then
                FirstInitDataDokterOps()
            Else
                FirstInitDataRow()
            End If
        Else
            Dim parameter As String = Request("__EVENTARGUMENT")
            If (String.Equals("confirmed", Parameter, StringComparison.InvariantCultureIgnoreCase)) Then
                SimpanTransaksi()
            End If

        End If
    End Sub

    Private Function checkDokterExists() As Boolean
        Dim check As Boolean = False
        Dim strsql As String = ""

        strsql = "SELECT * FROM IBSDokterOps where vc_no_trans ='" & Request.QueryString("pNoTrans") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If (dr.HasRows) Then
            check = True
        Else
            check = False
        End If
        Return check

    End Function

    Private Sub LoadDataPetugas()
        With Me.RadComboNamaPetugas
            Dim strSQL As String = ""
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim con As SqlConnection = New SqlConnection(connectionString)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter("SELECT VC_NIK,VC_NAMA_KRY FROM SDMKARYAWAN where bt_aktif=1", con)
            Dim links As DataTable = New DataTable()
            adapter.Fill(links)
            .DataTextField = "VC_NAMA_KRY"
            .DataValueField = "VC_NIK"
            .DataSource = links
            .DataBind()
        End With


    End Sub

    Private Sub loadDataOperasiPasien()
        Dim strsql As String = ""

        'strsql = " SELECT	RMP_inap.vc_no_reg vc_no_reg, RMP_inap.vc_no_rm VC_No_RM,RMPasien.vc_nama_p vc_nama_p,  " & _
        '              " RMPasien.vc_alamat, RMPasien.vc_kelurahan, RMPasien.vc_jenis_k, " & _
        '              " RMPasien.vc_BB, RMPasien.in_umurth, RMPasien.in_umurbl, RMPasien.in_umurhr,  " & _
        '              " RMRuang.VC_n_ruang vc_n_ruang, RMKelas.vc_n_kelas AS vc_kelas,   " & _
        '              " RMKelas.vc_k_kelas,	RMKamar.vc_nama,IBSJdwlOps.*,IBSJdwlOps.vc_kd_batal kode_batal   " & _
        '              " FROM RMP_inap " & _
        '              " INNER JOIN	  IBSJdwlOps On RMP_Inap.vc_no_reg = IBSJdwlOps.vc_no_reg  " & _
        '              " INNER JOIN    RMKamar On Case When (RMP_inap.vc_KLAS_MUT Is NULL Or    RMP_inap.vc_KLAS_MUT = '') THEN RMP_inap.vc_Kd_kamar_Masuk ELSE RMP_inap.vc_kd_kamar_MUTasi END = RMKamar.vc_no_bed   " & _
        '              " INNER JOIN    RMPasien On RMP_inap.vc_no_rm = RMPasien.vc_no_rm   " & _
        '              " INNER JOIN    RMRuang On RMKamar.vc_k_gugus = RMRuang.VC_k_ruang  " & _
        '              " INNER JOIN    RMKelas On Case When (RMP_inap.vc_KLAS_MUT Is NULL Or RMP_inap.vc_KLAS_MUT = '') THEN RMP_inap.vc_Kd_klas_masuk ELSE RMP_inap.vc_Kd_klas_mutasi END = RMKelas.vc_k_kelas  " & _
        '              " WHERE  RMP_inap.vc_no_rm = '" & Request.QueryString("pNoRM") & "'and RMP_inap.vc_no_reg ='" & Request.QueryString("pNoReg") & "'   " & _
        '              " And vc_no_trans ='" & Request.QueryString("pNoTrans") & "'   " & _
        '              " And RMP_inap.dt_tgl_pul Is NULL " & _
        '              " And IBSJdwlOps.vc_status In ('1','2','4') "
        strsql = " select	vc_no_trans, dt_tgl_trans,dt_tgl_operasi ,vc_jam_trans,in_umurth,vc_jenis_k,vc_bb, " _
                             & "        dt_tgl_daftar, aa.vc_no_rm,aa.vc_no_reg, vc_nama_p,vc_lokasi,vc_kd_jenisop, vc_kd_batal,bt_tindakan_medik,  " _
                             & "       bt_setuju_anesthesi, vc_sifat, vc_derajat,vc_no_ok,vc_diagnosa_penyerta,vc_diagnosa_utama,vc_ket_lain,   " _
                             & " nu_jam_perkiraan,nu_menit_perkiraan,vc_jam_daftar,vc_nid_operator,vc_nik_jadwal,vc_asal_daftar, " _
                             & " vc_tindakan, vc_n_ruang, rmkelas.vc_n_kelas As vc_kelas, Case When isnull(aa.bt_jkn,0)=0 Then 'NON JKN' else 'JKN' end as Tanggungan, aa.vc_status, bt_operasi " _
                             & " from	IBSJdwlOps aa " _
                             & "        inner join RMP_Inap bb on aa.vc_no_reg = bb.vc_no_reg " _
                             & "        inner join RMPasien cc on bb.vc_no_rm = cc.vc_no_rm "
        strsql &= "       inner join rmkamar ff ON CASE WHEN (bb.vc_KLAS_MUT IS NULL OR " _
                     & "        bb.vc_KLAS_MUT = '') THEN bb.vc_Kd_kamar_Masuk ELSE bb.vc_kd_kamar_MUTasi END = ff.vc_no_bed " _
                     & "        inner join RMRuang gg ON ff.vc_k_gugus = gg.VC_k_ruang " _
                     & "        INNER JOIN dbo.RMKelas ON dbo.RMKelas.vc_k_kelas = CASE WHEN (bb.vc_KLAS_MUT IS NULL OR " _
                     & "        bb.vc_KLAS_MUT = '') THEN bb.vc_Kd_KLAS_Masuk ELSE bb.vc_kd_KLAS_MUTasi END " _
                     & " WHERE	 aa.vc_status in  ('1','2','4') and  vc_no_trans ='" & Request.QueryString("pNoTrans") & "'" _
                     & " UNION " _
                     & " SELECT	vc_no_trans, dt_tgl_trans, dt_tgl_operasi , vc_jam_trans, in_umurth,vc_jenis_k,vc_bb, " _
                     & "        dt_tgl_daftar, aa.vc_no_rm,aa.vc_no_reg, vc_nama_p,vc_lokasi, vc_kd_jenisop,vc_kd_batal,bt_tindakan_medik, " _
                     & "        bt_setuju_anesthesi,vc_sifat,vc_derajat,vc_no_ok, vc_diagnosa_penyerta,vc_diagnosa_utama,vc_ket_lain, " _
                     & " nu_jam_perkiraan,nu_menit_perkiraan,vc_jam_daftar,vc_nid_operator,vc_nik_jadwal,vc_asal_daftar, " _
                     & " vc_tindakan, '' as vc_n_ruang, '' as vc_kelas, 'NON JKN' as tanggungan, aa.vc_status, bt_operasi " _
                     & " FROM	IBSJdwlOps aa " _
                     & "        inner join RMKunjung bb on aa.vc_no_reg = bb.vc_no_regj " _
                     & "        inner join RMPasien cc on bb.vc_no_rm = cc.vc_no_rm " _
                     & " WHERE	aa.vc_status in ('1','2','4') and  vc_no_trans ='" & Request.QueryString("pNoTrans") & "'" _
                     & " ORDER BY aa.dt_tgl_trans,aa.vc_jam_trans"
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()

        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()

        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Me.lblNoRM.Text = ""
        Me.LblNamaPasien.Text = ""
        Me.lblUsia.Text = ""
        Me.lblBB.Text = ""
        Me.LblJnsKel.Text = ""
        Me.txtTglDaftar.Text = ""
        Me.LblRuang.Text = ""
        Me.LblKelas.Text = ""
        Me.txtTglOperasi.Text = ""
        Me.txtJamOperasi.Text = ""
        Me.txtPlanDay.Text = ""
        Me.txtPlanPukul.Text = ""
        Me.txtKetLain.Text = ""
        Me.txtDiagnosaUtama.Text = ""
        Me.txtDiagnosaPenyerta.Text = ""
        Me.TxtNikPetugas.Text = ""
        Me.txtKdTindakan.Text = ""

        While dr.Read
            chkOperated.Checked = dr("bt_operasi")
            Me.lblNoRM.Text = dr("vc_no_rm")
            Me.LblNamaPasien.Text = dr("vc_nama_p")
            Me.lblUsia.Text = dr("in_umurth")
            Me.LblJnsKel.Text = dr("vc_jenis_k")
            Me.lblBB.Text = dr("vc_BB")
            Me.txtTglDaftar.Text = Format(dr("dt_tgl_daftar"), "dd/MM/yyyy")
            Me.LblRuang.Text = dr("vc_n_ruang")
            Me.LblKelas.Text = dr("vc_kelas")
            ' Me.txtTglOperasi.Text = Format(dr("dt_tgl_operasi"), "dd/MM/yyyy")
            ' Me.txtJamOperasi.Text = dr("vc_jam_operasi")

            If IsDBNull(dr("dt_tgl_operasi")) Then
                Me.txtTglOperasi.Text = ""
            Else
                Me.txtTglOperasi.Text = Format(dr("dt_tgl_operasi"), "dd/MM/yyyy")
                Me.txtJamOperasi.Text = Format(dr("dt_tgl_operasi"), "hh:mm")
            End If
            Select Case dr("vc_lokasi")
                Case "1"
                    Me.chkKiri.Checked = True
                    Me.chkKanan.Checked = False
                Case "2"
                    Me.chkKiri.Checked = False
                    Me.chkKanan.Checked = True
                Case "3"
                    Me.chkKiri.Checked = True
                    Me.chkKanan.Checked = True
                Case Else
            End Select
            If IsDBNull(dr("vc_kd_jenisop")) Then
                Me.txtKdTindakan.Text = ""
            Else
                Me.txtKdTindakan.Text = dr("vc_kd_jenisop")
                Try
                    Me.RadComboTindakan.Text = RadComboTindakan.FindItemByValue(dr("vc_kd_jenisop")).Text
                Catch ex As Exception
                    Me.txtKdTindakan.Text = ""
                End Try

            End If


            If (dr("vc_kd_batal").ToString.Length > 0) Then
                Me.txtKdBatal.Text = dr("vc_kd_batal")
                Try
                    Me.RadComboBatal.Text = RadComboBatal.FindItemByValue(dr("vc_kd_batal")).Text
                Catch ex As Exception
                    Me.txtKdBatal.Text = ""
                End Try

            End If
            Me.chkSetujuMedik.Checked = dr("bt_tindakan_medik")
            Me.chkSetujuAnesthesi.Checked = dr("bt_setuju_anesthesi")
            Me.dropDownSifatOperasi.SelectedIndex = dr("vc_sifat")
            Me.DropDownDerajat.SelectedValue = dr("vc_derajat")
            If (IsDBNull(dr("vc_no_ok")) = False) Then
                Me.DdRuangOp.SelectedValue = dr("vc_no_ok")
            End If
            Me.txtDiagnosaPenyerta.Text = dr("vc_diagnosa_penyerta")
            Me.txtDiagnosaUtama.Text = dr("vc_diagnosa_utama")
            Me.txtKetLain.Text = dr("vc_ket_lain")
            If (dr("nu_jam_perkiraan").ToString < 10) Then
                Me.txtJam.Text = "0" & dr("nu_jam_perkiraan").ToString
            Else
                Me.txtJam.Text = dr("nu_jam_perkiraan").ToString
            End If
            If (dr("nu_menit_perkiraan").ToString < 10) Then
                Me.txtMenit.Text = "0" & dr("nu_menit_perkiraan").ToString
            Else
                Me.txtMenit.Text = dr("nu_menit_perkiraan").ToString
            End If
            '  Me.txtJam.Text = String.Format("tt", "99")
            Me.txtPlanDay.Text = Format(dr("dt_tgl_daftar"), "dd/MM/yyyy")
            Me.txtPlanPukul.Text = dr("vc_jam_daftar")
            Me.TxtNikPetugas.Text = dr("vc_nid_operator")
            Me.txtKdTindakan.Text = dr("vc_kd_jenisop")

            If (dr("vc_nik_jadwal").ToString.Length > 0) Then
                Me.TxtNikPetugas.Text = dr("vc_nik_jadwal")
                Try
                    Me.RadComboNamaPetugas.Text = Me.RadComboNamaPetugas.FindItemByValue(dr("vc_nik_jadwal")).Text
                Catch ex As Exception
                    Me.TxtNikPetugas.Text = ""
                End Try
            End If

            Select Case True
                Case dr("vc_asal_daftar") = "IGD"
                    rdoIGD.Checked = True
                    rdoRajal.Checked = False
                Case dr("vc_asal_daftar") = "RJ"
                    rdoRajal.Checked = True
                    rdoIGD.Checked = False
                Case Else
                    rdoIGD.Checked = False
                    rdoRajal.Checked = False
            End Select


        End While
        dr.Close()
    End Sub

    Private Sub GetTindakanOp()
        With Me.RadComboTindakan
            Dim strSQL As String = ""
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim con As SqlConnection = New SqlConnection(connectionString)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter("Select vc_kd_jenisOp, vc_jenisOp FROM IBS_JENIS_OP", con)
            Dim links As DataTable = New DataTable()
            adapter.Fill(links)
            .DataTextField = "vc_jenisOp"
            .DataValueField = "vc_kd_jenisOp"
            .DataSource = links
            .DataBind()
        End With

    End Sub

    Private Sub GetAlasanBatal()
        With Me.RadComboBatal
            Dim strSQL As String = ""
            Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
            Dim connection As SqlConnection = New SqlConnection(connectionString)
            Dim con As SqlConnection = New SqlConnection(connectionString)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter("Select vc_kd_batal, vc_nm_batal FROM IBSAlasanBatal", con)
            Dim links As DataTable = New DataTable()
            adapter.Fill(links)
            .DataTextField = "vc_nm_batal"
            .DataValueField = "vc_kd_batal"
            .DataSource = links
            .DataBind()
        End With

    End Sub
    Private Sub GetNamaRuangOp()
        Dim strSQL As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)


        strSQL = "Select vc_kd_ruang,vc_nm_ruang FROM IBSRuangOK Order By vc_kd_ruang"

        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Dim conn As New SqlConnection(connectionString)
        conn.Open()
        Dim comm As SqlCommand = New SqlCommand(strSQL, conn)
        da.SelectCommand = comm
        da.Fill(ds)
        conn.Close()

        With Me.DdRuangOp
            .DataSource = ds
            .DataTextField = "vc_nm_ruang"
            .DataValueField = "vc_kd_ruang"
            .DataBind()
        End With
        DdRuangOp.Items.Insert(0, New ListItem("---", "---"))
    End Sub


    Private Sub FirstInitDataRow()

        Dim Table1 As DataTable
        Table1 = New DataTable("TableName")
        '   Dim rowNumber As DataColumn = New DataColumn("RowNumber")
        ' rowNumber.DataType = System.Type.GetType("System.String")
        Dim column1 As DataColumn = New DataColumn("Nid")
        column1.DataType = System.Type.GetType("System.String")

        Dim column2 As DataColumn = New DataColumn("Dokter")
        column2.DataType = System.Type.GetType("System.String")

        '  Table1.Columns.Add(rowNumber)
        Table1.Columns.Add(column1)
        Table1.Columns.Add(column2)

        Dim Row1 As DataRow
        Row1 = Table1.NewRow()
        '  Row1.Item("RowNumber") = 1
        Row1.Item("Nid") = String.Empty
        Row1.Item("Dokter") = String.Empty



        Table1.Rows.Add(Row1)
        ViewState("CurrentTable") = Table1


        gridviewDokter.DataSource = Table1
        gridviewDokter.DataBind()

    End Sub


    Private Sub FirstInitDataDokterOps()
        Dim Table1 As DataTable
        Table1 = New DataTable("TableName")
        Dim column1 As DataColumn = New DataColumn("Nid")
        column1.DataType = System.Type.GetType("System.String")

        Dim column2 As DataColumn = New DataColumn("Dokter")
        column2.DataType = System.Type.GetType("System.String")
        Table1.Columns.Add(column1)
        Table1.Columns.Add(column2)
        Dim strsql As String = ""
        strsql = "SELECT * FROM IBSDokterOps where vc_no_trans ='" & Request.QueryString("pNoTrans") & "' "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Dim listNid As New ArrayList
        While dr.Read
            Dim Row1 As DataRow
            Row1 = Table1.NewRow()
            Row1.Item("Nid") = String.Empty
            Row1.Item("Dokter") = String.Empty
            Table1.Rows.Add(Row1)
            listNid.Add(dr("vc_nid"))
        End While
        dr.Close()

        ViewState("CurrentTable") = Table1
        gridviewDokter.DataSource = Table1
        gridviewDokter.DataBind()
        For i As Integer = 0 To gridviewDokter.Rows.Count - 1
            Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(i).Cells(0).FindControl("txtNid"), TextBox)
            Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(i).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)
            txtNid.Text = listNid(i)
            radDokter.Text = radDokter.FindItemByValue(listNid(i)).Text
        Next
        Table1 = gridviewDokter.DataSource
        ViewState("CurrentTable") = Table1
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            'Find the DropDownList in the Row
            Dim radDokter As Telerik.Web.UI.RadComboBox = CType(e.Row.FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)
            With radDokter
                Dim strSQL As String = ""
                Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
                Dim connection As SqlConnection = New SqlConnection(connectionString)
                Dim con As SqlConnection = New SqlConnection(connectionString)
                Dim adapter As SqlDataAdapter = New SqlDataAdapter("Select [vc_nid], [vc_nama_kry] FROM [SDMDokter] where dt_tgl_klr Is null ORDER BY [vc_nama_kry]", con)
                Dim links As DataTable = New DataTable()
                adapter.Fill(links)
                .DataTextField = "vc_nama_kry"
                .DataValueField = "vc_nid"
                .DataSource = links
                .DataBind()
                ' .Attributes.Add("onChange", "setNid('" + radDokter.SelectedValue + "')")

            End With
        End If
    End Sub

    Private Function cekJadwalOperasi(ByVal dtTglOps As DateTime, ByVal RuangOK As String) As String

        Dim strSql As String = ""
        Dim vJamAwal As String
        Dim vJamAkhir As String
        Dim vNoTrans As String = ""
        Dim interval As Integer = 0

        If txtJam.Text = "" Then txtJam.Text = "0"
        If txtMenit.Text = "" Then txtJam.Text = "0"
        interval = (Integer.Parse(Me.txtJam.Text) * 60) + (Integer.Parse(Me.txtMenit.Text))

        vJamAwal = txtJamOperasi.Text
        If vJamAwal.Trim = ":" Then
            vJamAwal = "00:00"
        End If
        vJamAkhir = Format(DateAdd(DateInterval.Minute, interval, CDate(vJamAwal)), "HH:mm")
        cekJadwalOperasi = ""

        strSql = "SELECT vc_no_trans,dt_tgl_operasi,vc_nm_ruang " _
             & " FROM   IBSJdwlOps aa left join IBSRuangOK bb ON aa.vc_no_OK = bb.vc_kd_ruang " _
             & " WHERE  vc_kd_ruang ='" & RuangOK & "'" _
             & "    And convert(varchar(10),dt_tgl_operasi,120) = '" & Format(dtTglOps.Date, "yyyy-MM-dd") & "'" _
             & "    And convert(varchar(5),dt_tgl_operasi,114) between '" & vJamAwal & "' And '" & vJamAkhir & "'" _
             & "    And vc_no_trans <> '" & Request.QueryString("pNoTrans") & "'"

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            While dr.Read
                cekJadwalOperasi = cekJadwalOperasi & dr("vc_no_trans")
            End While
        Else
            cekJadwalOperasi = ""
        End If
        dr.Close()

    End Function


    Private Sub addRow()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim cJumRec As Integer = dtCurrentTable.Rows.Count
                    Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(0).FindControl("txtNid"), TextBox)

                    Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)

                    drCurrentRow = dtCurrentTable.NewRow()
                    ' dtCurrentTable.Rows(cJumRec - 1)("RowNumber") = dtCurrentTable.Rows.Count + 1
                    dtCurrentTable.Rows(i - 1)("Nid") = txtNid.Text
                    dtCurrentTable.Rows(i - 1)("Dokter") = radDokter.Text
                    drCurrentRow = dtCurrentTable.NewRow()
                    rowIndex += 1
                Next
                dtCurrentTable.Rows.Add(drCurrentRow)
                ViewState("CurrentTable") = dtCurrentTable


                gridviewDokter.DataSource = dtCurrentTable
                gridviewDokter.DataBind()

            End If
        Else
            Response.Write("ViewState is null")
        End If
        SetPreviousData()
    End Sub
    Protected Sub BtnAddDokter_Click(ByVal sender As Object, ByVal e As EventArgs)
        addRow()
    End Sub

    Private Sub SetPreviousData()
        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(0).FindControl("txtNid"), TextBox)
                    Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)
                    txtNid.Text = dt.Rows(i)("Nid").ToString()
                    radDokter.Text = dt.Rows(i)("Dokter").ToString()
                    rowIndex += 1
                Next
            End If
        End If
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (txtTglOperasi.Text = "") Then
            PESAN("Tanggal Operasi Harus Diisi")
        ElseIf (txtJamOperasi.Text = "") Then
            PESAN("Jam Operasi Harus Diisi")
        ElseIf (DdRuangOp.text = "---") Then
            PESAN("Ruang Operasi Belum Dipilih")
        ElseIf (RadComboNamaPetugas.Text = "") Then
            PESAN("Petugas Harus Diisi")
        Else

            If (InStr(Me.txtJam.Text, "_") > 0) Then
                Me.txtJam.Text = "0" & txtJam.Text
            End If
            If (InStr(Me.txtMenit.Text, "_") > 0) Then
                Me.txtMenit.Text = "0" & txtMenit.Text
            End If
            Dim tglOperasi As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(Me.txtTglOperasi.Text))
            Dim cek_jadwal As String = ""
            cek_jadwal = Me.cekJadwalOperasi(tglOperasi, Me.DdRuangOp.SelectedValue)
            Dim Message = "Jadwal Bertabrakan dengan no Trans : " + cek_jadwal + "..Apakah anda akan tetap menyimpan data??"
            If (cek_jadwal = "") Then
                Dim confirm As String = "if(confirm('Apakah Anda Yakin Menyimpan Data??')) __doPostBack('', 'confirmed');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "temp", confirm, True)
                '  SimpanTransaksi()
            Else

                Dim confirm As String = "if(confirm('" & Message & "')) __doPostBack('', 'confirmed');"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "temp", confirm, True)

                ' Page.ClientScript.RegisterStartupScript(Me.GetType, "confirm_tabrak", "confirm_tabrak('" & Message & "');", True)
            End If

        End If



    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub
    Protected Sub RadComboBatal_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBatal.SelectedIndexChanged
        Me.txtKdBatal.Text = RadComboBatal.SelectedValue.ToString
    End Sub
    Protected Sub RadComboTindakan_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboTindakan.SelectedIndexChanged
        Me.txtKdTindakan.Text = RadComboTindakan.SelectedValue.ToString
    End Sub
    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        If (RadComboBatal.Text = "") Then
            PESAN("Alasan Batal Harus Di Isi")
        Else
            Dim confirmValue As String = Request.Form("confirm_batal")
            If confirmValue = "Yes" Then
                If (getStatusOperasi() = 4) Then
                    PESAN("Transaksi Sudah Pernah Dibatalkan Sebelumnya")
                Else
                    BatalTransaksi()
                End If
            End If
        End If
    End Sub
    Protected Sub RadComboBoxDokter_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs)

        Dim rowIndex As Integer = 0
        Dim dokter As String = ""
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    rowIndex += 1
                Next
            End If
            For i As Integer = 0 To rowIndex - 1
                Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(i).Cells(0).FindControl("txtNid"), TextBox)
                Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(i).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)
                Try
                    txtNid.Text = radDokter.FindItemByText(radDokter.Text).Value
                Catch ex As Exception
                    txtNid.Text = ""
                    radDokter.Text = ""
                End Try



            Next

        End If
    End Sub

    Private Sub SimpanTransaksi()
        Dim tglOperasi As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(Me.txtTglOperasi.Text))
        Dim tglDaftar As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(Me.txtTglDaftar.Text))
        Dim tglPlan As Date = MainLibWebERM.MasterLib.SqlDate(MainLibWebERM.MasterLib.xDate(txtPlanDay.Text))
        Dim strsql As String = ""
        Dim delete_sql As String = ""
        Dim strsql_2 As String = ""
        Dim lokasi As String = ""
        Dim asal As String = ""
        Dim tgl_operasi As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        If chkKiri.Checked = True Then
            lokasi = "1"
        Else
            lokasi = "2"
            If chkKanan.Checked = True Then
                lokasi = "3"
            Else
                lokasi = ""
            End If
        End If

        tgl_operasi = tglOperasi & " " & IIf(txtJamOperasi.Text.Trim = ":", "00:00:01 AM", txtJamOperasi.Text)
        Select Case True
            Case (rdoIGD.Checked = True And rdoRajal.Checked = False)
                asal = "IGD"
            Case (rdoIGD.Checked = False And rdoRajal.Checked = True)
                asal = "RJ"
            Case (rdoIGD.Checked = False And rdoRajal.Checked = False)
                asal = ""
        End Select
        Try
            'update data 
            strsql = " update IBSJdwlOps set " & _
                       "vc_no_reg = '" & Request.QueryString("pNoReg") & "'  " & _
                       " , vc_no_rm = '" & Request.QueryString("pNoRM") & "'  " & _
                       ",  dt_tgl_trans = '" & tglDaftar & " " & Format(Now, "hh:mm:ss tt") & "'  " & _
                       ", dt_tgl_daftar = '" & tglPlan & " " & IIf(txtPlanPukul.Text.Trim = ":", "00:00:01 AM", txtPlanPukul.Text) & "'  " & _
                       ", vc_jam_daftar = '" & txtPlanPukul.Text & "'  " & _
                       ",nu_jam_perkiraan = '" & Val(Me.txtJam.Text) & "'  " & _
                       ",nu_menit_perkiraan = '" & Val(Me.txtMenit.Text) & "'  " & _
                       ", vc_status = '2' " & _
                       ", vc_kd_batal = '" & Me.txtKdBatal.Text & "' " & _
                       ", vc_sifat = '" & Me.dropDownSifatOperasi.SelectedIndex & "' " & _
                       ", vc_derajat = '" & Me.DropDownDerajat.SelectedValue.ToString & "' " & _
                       ", bt_tindakan_medik = '" & (Me.chkSetujuMedik.Checked) & "' " & _
                       ", bt_setuju_anesthesi = '" & (Me.chkSetujuAnesthesi.Checked) & "' " & _
                       ", vc_lokasi = '" & (lokasi) & "' " & _
                       ", vc_diagnosa_utama = '" & (Me.txtDiagnosaUtama.Text) & "' " & _
                       ", vc_diagnosa_penyerta = '" & (txtDiagnosaPenyerta.Text) & "' " & _
                       ", vc_tindakan = '" & (Me.RadComboTindakan.Text) & "' " & _
                       ", dt_create_date = '" & (GetCurrentDate()) & "' " & _
                       ", vc_create_by = '" & Session("ssusername") & "' " & _
                       ", vc_jam_trans = '" & Format(Now, "HH:mm") & "' " & _
                       ", dt_tgl_operasi = '" & tgl_operasi & "'  " & _
                       ",vc_no_ok = '" & DdRuangOp.SelectedItem.Value.ToString & "' " & _
                       ",vc_asal= '2' " & _
                       ",vc_asal_daftar= '" & asal & "' " & _
                       ",vc_nik_jadwal= '" & Me.TxtNikPetugas.Text & "' " & _
                       ",bt_operasi= '" & IIf(Me.chkOperated.Checked, 1, 0) & "' " & _
                       ",vc_kd_jenisop = '" & txtKdTindakan.Text & "' " & _
                       " where vc_no_trans =  '" & Request.QueryString("pNoTrans") & "' "


            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()

            'delete data dokter lama
            delete_sql = "Delete From IBSDokterOps Where vc_no_trans = '" & Request.QueryString("pNoTrans") & "'"

            command.CommandText = delete_sql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            'insert data dokter
            Dim rowIndex As Integer = 0

            If ViewState("CurrentTable") IsNot Nothing Then
                Dim dt As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        rowIndex += 1
                    Next
                End If
                For i As Integer = 0 To rowIndex - 1
                    Dim nid = ""
                    Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(i).Cells(0).FindControl("txtNid"), TextBox)
                    Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(i).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)
                    Try
                        nid = radDokter.FindItemByText(radDokter.Text).Value
                        If (nid <> "|") Then
                            strsql_2 = "Insert Into IBSDokterOps(vc_no_trans,vc_nid) VALUES ('" & Request.QueryString("pNoTrans") & "','" & nid & "') "
                            command.CommandText = strsql_2
                            command.CommandType = CommandType.Text
                            command.ExecuteNonQuery()
                        End If
                    Catch ex As Exception
                        nid = ""
                    End Try



                Next

            End If

            MyTrans.Commit()
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        Finally
            command.Dispose()
            MyTrans.Dispose()
            'PESAN(getStatusOperasi())
            PESAN("Data berhasil disimpan!")
        End Try
    End Sub

    Private Sub BatalTransaksi()
        Dim strsql As String = ""
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim MyTrans As SqlTransaction
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans
        Try
            strsql = "update IBSJdwlOps set " & _
                                  "  vc_status = '4' " & _
                                  ", vc_kd_batal = '" & Me.txtKdBatal.Text & "' " & _
                                  "  where vc_no_trans =  '" & Request.QueryString("pNoTrans") & "' "


            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()
        Catch ex As Exception
            MyTrans.Rollback()
            Throw New Exception("Error: ", ex)
        Finally
            command.Dispose()
            MyTrans.Dispose()
            'PESAN(getStatusOperasi())
            PESAN("Data berhasil dibatalkan!")
        End Try

    End Sub

    Private Function getStatusOperasi() As String
        Dim status As String = 0
        Dim strsql As String = ""

        strsql = " SELECT vc_status FROM  IBSJdwlOps where vc_status In ('1','2','4') and vc_no_trans = '" & Request.QueryString("pNoTrans") & "'  "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            status = dr("vc_status")
        End While
        dr.Close()

        Return status
    End Function

    Private Function GetCurrentDate() As DateTime
        Dim strsql As String = ""
        Dim tgl As DateTime = Nothing
        strsql = " select getdate() as tgl  "

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim sqlCommand As New SqlClient.SqlCommand(strsql)
        sqlCommand.Connection = connection
        connection.Open()
        Dim dr As SqlClient.SqlDataReader
        dr = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            tgl = dr("tgl")
        End While
        dr.Close()

        Return tgl
    End Function

    Protected Sub GridviewDokter_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridviewDokter.RowDeleting
        setRow()
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dt As DataTable = ViewState("CurrentTable")
            Dim drCurrentRow As DataRow = Nothing
            Dim rowIndex As Integer = Convert.ToInt32(e.RowIndex)
            If (dt.Rows.Count > 1) Then

                dt.Rows.Remove(dt.Rows(rowIndex))
                drCurrentRow = dt.NewRow()
                ViewState("CurrentTable") = dt
                gridviewDokter.DataSource = dt
                gridviewDokter.DataBind()

                SetPreviousData()
            End If
        End If

    End Sub

    Private Sub setRow()

        Dim rowIndex As Integer = 0
        If ViewState("CurrentTable") IsNot Nothing Then
            Dim dtCurrentTable As DataTable = DirectCast(ViewState("CurrentTable"), DataTable)
            Dim drCurrentRow As DataRow = Nothing
            If dtCurrentTable.Rows.Count > 0 Then
                For i As Integer = 1 To dtCurrentTable.Rows.Count
                    Dim cJumRec As Integer = dtCurrentTable.Rows.Count
                    Dim txtNid As TextBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(0).FindControl("txtNid"), TextBox)

                    Dim radDokter As Telerik.Web.UI.RadComboBox = DirectCast(gridviewDokter.Rows(rowIndex).Cells(1).FindControl("RadComboBoxDokter"), Telerik.Web.UI.RadComboBox)

                    drCurrentRow = dtCurrentTable.NewRow()
                    ' dtCurrentTable.Rows(cJumRec - 1)("RowNumber") = dtCurrentTable.Rows.Count + 1
                    dtCurrentTable.Rows(i - 1)("Nid") = txtNid.Text
                    dtCurrentTable.Rows(i - 1)("Dokter") = radDokter.Text
                    drCurrentRow = dtCurrentTable.NewRow()
                    rowIndex += 1
                Next
                ViewState("CurrentTable") = dtCurrentTable
            End If
        Else
            Response.Write("ViewState is null")
        End If

    End Sub
    Protected Sub RadComboNamaPetugas_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboNamaPetugas.SelectedIndexChanged
        Me.TxtNikPetugas.Text = RadComboNamaPetugas.FindItemByText(RadComboNamaPetugas.Text).Value
    End Sub

    Protected Sub txtJam_TextChanged(sender As Object, e As EventArgs) Handles txtJam.TextChanged
        txtJam.Text = txtJam.Text.Replace("_", "0")
    End Sub
    Protected Sub txtMenit_TextChanged(sender As Object, e As EventArgs) Handles txtMenit.TextChanged
        txtMenit.Text = txtMenit.Text.Replace("_", "0")
    End Sub
End Class
