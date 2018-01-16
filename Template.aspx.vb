Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Configuration

Partial Class Template
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("ssusernameTemp") = "" Then
            Response.Redirect("~/LoginTemplate.aspx")
        End If
        If Not Page.IsPostBack Then
            SetDataSourceTemplate()
            GridView1.DataSourceID = SdsData.ID
        End If
    End Sub

    Private Sub PESAN(ByVal cpesan As String)
        ClientScript.RegisterStartupScript(Me.GetType, "ClientSideScript", "<script type='text/javascript'>window.alert('" & cpesan & "')</script>")
    End Sub

    Protected Sub TxtOperasi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtOperasi.TextChanged
        Me.TxtDeskripsi.Text = ""
        Me.TxtDeskripsi.Text = MainLibWebERM.MasterLib.ShowData("vc_nama_operasi", "vc_nama_operasi", "IbsTemplateOperasi", Me.TxtOperasi.Text, "", clmain.ConString)
    End Sub


    Function simpanData() As Boolean
        'Dim MyTrans As SqlTransaction
        Dim strsql As String = ""

        simpanData = False
        Dim lFound As Boolean = False
        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim command As SqlCommand = New SqlCommand()
        Dim lAda As Boolean = False

        If MainLibWebERM.MasterLib.ShowData("vc_nama_operasi", "vc_nama_operasi", "IbsTemplateOperasi", Me.TxtOperasi.Text, "", clmain.ConString) <> "" Then
            lAda = True
        End If

        Dim MyTrans As SqlTransaction
        connection.Open()
        command.Connection = connection
        MyTrans = connection.BeginTransaction()
        command.Transaction = MyTrans

        Try

            If lAda Then
                strsql = "update IBSTemplateOperasi set vc_deskripsi = '" & Me.TxtDeskripsi.Text & "' WHERE VC_NAMA_OPERASI = '" & Me.TxtOperasi.Text & "' "
            Else

                strsql = "insert into IBSTemplateOperasi(vc_nama_operasi, vc_deskripsi) " & _
                                                 "values " & _
                                                 "('" & UCase(Me.TxtOperasi.Text) & "', '" & Me.TxtDeskripsi.Text & "')"
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


    Protected Sub cmdInput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInput.Click
        Select Case cmdInput.Text
            Case "Input"
                Me.TxtOperasi.Text = ""
                Me.TxtDeskripsi.Text = ""
                Me.TxtOperasi.Focus()
                Me.cmdInput.Text = "Simpan"
                Me.CmdKeluar.Text = "Batal"
            Case "Simpan"
                Dim lLengkap As Boolean = True

                If Me.TxtOperasi.Text = "" Then
                    lLengkap = False
                    Me.TxtOperasi.Focus()
                End If

                If Me.TxtDeskripsi.Text = "" Then
                    lLengkap = False
                    Me.TxtDeskripsi.Focus()
                End If


                If lLengkap Then
                    If simpanData() Then
                        PESAN("Data Berhasil disimpan")
                        Me.cmdInput.Text = "Input"
                        Me.CmdKeluar.Text = "Keluar"
                    Else
                        PESAN("Data Gagal disimpan")
                    End If
                Else
                    PESAN("Data harus dilengkapi semua...")
                    Me.TxtDeskripsi.Focus()
                End If
        End Select

    End Sub

    Protected Sub CmdKeluar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdKeluar.Click
        Session.Clear()
        Response.Redirect("~/LoginTemplate.aspx")
    End Sub

    Private Sub SetDataSourceTemplate()

        Dim connectionString As String = WebConfigurationManager.ConnectionStrings("koneksi").ConnectionString
        Dim connection As SqlConnection = New SqlConnection(connectionString)
        Dim strsql As String = ""

        strsql = "SELECT vc_nama_operasi from IbsTemplateOperasi order by vc_nama_operasi"

        SdsData.ConnectionString = connectionString

        SdsData.DataSourceMode = SqlDataSourceMode.DataSet
        SdsData.ProviderName = "System.Data.SqlClient"

        SdsData.SelectCommand = strsql
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
            'Dim cStatus As String = DirectCast(DataBinder.Eval(e.Row.DataItem, "vc_no_trans"), String)
            'Select Case cStatus
            '    Case Request.QueryString("pNoTrans")
            '        e.Row.BackColor = Drawing.Color.Blue
            '        e.Row.ForeColor = Drawing.Color.Yellow
            '        onmouseoutStyle = "this.style.backgroundColor='blue'"
            '        e.Row.Attributes.Add("onmouseout", onmouseoutStyle.Replace("@BackColor", rowBackColor))
            'End Select

        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

        On Error Resume Next

        Dim cNamaOperasi As String = CType(GridView1.SelectedRow.FindControl("lblOperasi"), Label).Text

        Me.TxtOperasi.Text = cNamaOperasi
        Me.TxtDeskripsi.Text = MainLibWebERM.MasterLib.ShowData("vc_nama_operasi", "vc_deskripsi", "IbsTemplateOperasi", cNamaOperasi, "", clmain.ConString)

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
            strsql = "delete from IBSTemplateOperasi where vc_nama_operasi= '" & Me.TxtOperasi.Text & "'"

            command.CommandText = strsql
            command.CommandType = CommandType.Text
            command.ExecuteNonQuery()
            MyTrans.Commit()

            Me.TxtDeskripsi.Text = ""
            Me.TxtOperasi.Text = ""

            PESAN("Data Berhasil dihapus!...")

            Response.Redirect(Request.RawUrl)

        Catch ex As Exception
            MyTrans.Rollback()
            PESAN("Data Gagal dihapus!...")
        End Try

    End Sub
End Class
