<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="FrmJadwalOperasi.aspx.vb" Inherits="FrmJadwalOperasi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="Server">

    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" CombineScripts="false"
        EnableScriptGlobalization="true" EnableScriptLocalization="true" ScriptMode="Debug">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        &nbsp; &nbsp; &nbsp;&nbsp;

        <br />
        <div>&nbsp;</div>
        <div class="gridContainer">
            <h1>
                <asp:Label ID="Label15" runat="server" Text="Data Pasien" ForeColor="Blue" Font-Overline="False"></asp:Label>&nbsp;</h1>
            <asp:Label ID="Label22" runat="server" Text="Tgl Daftar" Font-Bold="True" Font-Size="Small"></asp:Label>
            &nbsp;<asp:TextBox ID="txtTglDaftar" runat="server" Font-Size="Small" Width="79px" ReadOnly="True"></asp:TextBox><br />
            <asp:Label ID="Label23" runat="server" Text="NO RM" Font-Bold="True" Font-Size="Small"></asp:Label>
            &nbsp;<asp:Label ID="lblNoRM" runat="server" Height="24px" Font-Size="Small" Text="No RM" Width="160px"></asp:Label><br />
            <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="Small" Text="Nama"></asp:Label>
            &nbsp;<asp:Label ID="LblNamaPasien" runat="server" Height="24px" Font-Size="Small" Text="Nama Pasien" Width="160px"></asp:Label><br />
            <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Size="Small" Text="Usia"></asp:Label>
            &nbsp;<asp:Label ID="lblUsia" runat="server" Font-Size="Small" Text="Label Usia"></asp:Label><br />
            <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Small" Text="J.Kel"></asp:Label>
            &nbsp;<asp:Label ID="LblJnsKel" runat="server" Font-Size="Small" Text="Laki/Perempuan"></asp:Label><br />
            <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Size="Small" Text="Ruang"></asp:Label>
            &nbsp;<asp:Label ID="LblRuang" runat="server" Font-Size="Small" Text="Nama Ruang"></asp:Label><br />
            <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="Small" Text="Kamar"></asp:Label>
            &nbsp;<asp:Label ID="LblKelas" runat="server" Font-Size="Small" Text="Nama Kelas"></asp:Label><br />
            <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="Small" Text="Berat Bdn"></asp:Label>
            &nbsp;<asp:Label ID="lblBB" runat="server" Font-Size="Small" Text="Berat Bdn"></asp:Label><br />
            <asp:Label ID="Label31" runat="server" Text="Asal Pasien" Font-Bold="True" Font-Size="Small"></asp:Label>
            <asp:RadioButton ID="rdoIGD" runat="server" Text="IGD" Font-Size="Small" GroupName="rdoAsal" />
            <asp:RadioButton ID="rdoRajal" runat="server" Text="Rawat Jalan" Font-Size="Small" GroupName="rdoAsal" />
            <br />
            <asp:Label ID="Label35" runat="server" Text="Pasien Sudah Dioperasi" Font-Bold="True" Font-Size="Small"></asp:Label>
            <asp:CheckBox ID="chkOperated" runat="server" />
        </div>
        <div class="gridContainer">
            <h1>
                <asp:Label ID="Label14" runat="server" Text="Penjadwalan Operasi" ForeColor="Blue" Font-Overline="False"></asp:Label></h1>

            <br />
            <asp:Label ID="Label19" runat="server" Font-Overline="False" ForeColor="Blue" Text="Jadwal Operasi"></asp:Label><br />
            <br />
            <asp:Label ID="Label16" runat="server" Text="Tgl Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;<asp:TextBox ID="txtTglOperasi" runat="server" Width="70px" Font-Size="Small"></asp:TextBox><br />
            <asp:Label ID="Label18" runat="server" Text="Jam Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>&nbsp;
    <asp:TextBox ID="txtJamOperasi" runat="server" Width="40px" Font-Size="Small"></asp:TextBox>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                TargetControlID="txtJamOperasi" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" />

            <asp:Label ID="Label21" runat="server" Text="(hh:mm)" Font-Size="Small"></asp:Label><br />
            <asp:Label ID="Label20" runat="server" Text="Ruang Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;<asp:DropDownList ID="DdRuangOp" runat="server" Width="96px" Font-Size="Small">
            </asp:DropDownList>&nbsp;
      <br />
            <br />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy"
                TargetControlID="txtTglOperasi">
            </ajaxToolkit:CalendarExtender>

            <asp:Label ID="Label1" runat="server" Font-Overline="False" ForeColor="Blue" Text="Dokter Operator"></asp:Label><br />
            <asp:GridView ID="gridviewDokter" runat="server" CssClass="responsiveTable1"
                ShowFooter="True" AutoGenerateColumns="False" Style="text-align: left"
                CellPadding="4" ForeColor="#333333" Width="30%" OnRowDataBound="OnRowDataBound"
                GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Nid">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNid" runat="server" Width="40px" Font-Size="Small"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Dokter">
                        <ItemTemplate>
                            <%-- <asp:TextBox ID="txtDokter" runat="server" Width="170px"></asp:TextBox>--%>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxDokter" runat="server" Height="200px" Width="192px"
                                AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxDokter_SelectedIndexChanged"
                                Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false" EmptyMessage="Pilih Dokter" Font-Size="Small"
                                DataTextField="vc_nama_kry" DataValueField="vc_nid" Skin="Office2010Silver">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="RadComboBoxDokter"
                                Display="Dynamic" ErrorMessage="!" CssClass="validator">
                            </asp:RequiredFieldValidator>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="70px" />
                        <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="BtnAddDokter" runat="server" Text="Tambah Dokter" OnClick="BtnAddDokter_Click" Font-Size="Small" />
                            <%-- <ajaxToolkit:ModalPopupExtender ID="gridviewDokter_ModalPopupExtender" PopupControlID="panelDokter" runat="server" TargetControlID="BtnAddDokter">
            </ajaxToolkit:ModalPopupExtender>--%>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>

        </div>



        <br />
        <div class="gridContainer">
            <h1>
                <asp:Label ID="Label13" runat="server" Text="Informasi Operasi" ForeColor="Blue" Font-Overline="False"></asp:Label></h1>


            <asp:Label ID="Label2" runat="server" Text="Rencana Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtPlanDay" runat="server" Width="90px" Font-Size="Small"></asp:TextBox>&nbsp;
            <br />


            <asp:Label ID="Label32" runat="server" Text="Perkiraan Lama Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;<asp:TextBox ID="txtJam" runat="server" Width="23px" Font-Size="Small" Height="16px" OnTextChanged="txtJam_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"  ClearMaskOnLostFocus="False" MaskType="None"
                TargetControlID="txtJam" Mask="99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />


            <asp:Label ID="Label33" runat="server" Text="Jam" Font-Size="Small"></asp:Label>
            &nbsp;<asp:TextBox ID="txtMenit" runat="server" Width="23px" Font-Size="Small" Height="16px" OnTextChanged="txtMenit_TextChanged" AutoPostBack="true"></asp:TextBox>&nbsp;
                 <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"  ClearMaskOnLostFocus="False" MaskType="None"
                     TargetControlID="txtMenit" Mask="99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" />


            <asp:Label ID="Label34" runat="server" Text="Menit" Font-Size="Small"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Pukul" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;<asp:TextBox ID="txtPlanPukul" runat="server" Width="40px" Font-Size="Small"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label17" runat="server" Text="(hh:mm)" Font-Size="Small"></asp:Label>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender0" runat="server" Enabled="True"
                Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy"
                TargetControlID="txtPlanDay">
            </ajaxToolkit:CalendarExtender>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Sifat Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;
    <asp:DropDownList ID="dropDownSifatOperasi" runat="server" Font-Size="Small">
        <asp:ListItem>Elektif</asp:ListItem>
        <asp:ListItem>Cito</asp:ListItem>
        <asp:ListItem>Daftar Tunggu</asp:ListItem>
    </asp:DropDownList>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Derajat Kontaminasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            &nbsp;
    <asp:DropDownList ID="DropDownDerajat" runat="server">
        <asp:ListItem>B</asp:ListItem>
        <asp:ListItem>BT</asp:ListItem>
        <asp:ListItem>T</asp:ListItem>
        <asp:ListItem>K</asp:ListItem>
    </asp:DropDownList>
            <br />
            <asp:Label ID="Label6" runat="server" Text="Lokasi Operasi" Font-Size="Small" Font-Bold="True"></asp:Label>
            <asp:CheckBox ID="chkKiri" runat="server" Text="Kiri" Font-Size="Small" />
            <asp:CheckBox ID="chkKanan" runat="server" Text="Kanan" Font-Size="Small" />
            <br />
            <asp:CheckBox ID="chkSetujuMedik" runat="server" Text="Persetujuan Tindakan Medik" Font-Size="Small" />
            <br />
            <asp:CheckBox ID="chkSetujuAnesthesi" runat="server" Text="Persetujuan Dokter Anesthesi" Font-Size="Small" />
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Diagnosa Utama" Font-Size="Small" Font-Bold="True"></asp:Label>
            <br />
            <asp:TextBox ID="txtDiagnosaUtama" runat="server" TextMode="MultiLine" MaxLength="32767" Font-Size="Small" Width="271px"></asp:TextBox>
            <br />
            <asp:Label ID="Label8" runat="server" Text="Diagnosa Penyerta" Font-Size="Small" Font-Bold="True"></asp:Label>
            <br />
            <asp:TextBox ID="txtDiagnosaPenyerta" runat="server" TextMode="MultiLine" MaxLength="32767" Font-Size="Small" Width="271px"></asp:TextBox>
            <br />
            <div>
                <asp:Label ID="Label9" runat="server" Text="Operasi / Tindakan" Font-Size="Small" Font-Bold="True"></asp:Label><br />
                <asp:TextBox ID="txtKdTindakan" runat="server" TextMode="MultiLine" Width="59px" MaxLength="8" Height="24px" Font-Size="Small"></asp:TextBox>
                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboTindakan" runat="server" Height="200px" Width="229px"
                    Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false" EmptyMessage="Pilih Tindakan Operasi"
                     Skin="Office2010Silver" Font-Size="Small" AutoPostBack="True">
                </telerik:RadComboBox>
                <br />
            </div>
            <asp:Label ID="Label10" runat="server" Text="Keterangan Lain" Font-Size="Small" Font-Bold="True"></asp:Label>
            <br />
            <asp:TextBox ID="txtKetLain" runat="server" TextMode="MultiLine" MaxLength="32767" Font-Size="Small" Width="271px"></asp:TextBox>
            <br />
            <asp:Label ID="Label11" runat="server" Text="Alasan Pembatalan" Font-Size="Small" Font-Bold="True"></asp:Label>
            <br />
            <asp:TextBox ID="txtKdBatal" runat="server" TextMode="MultiLine" Width="99px" MaxLength="8" Height="24px"></asp:TextBox>
            &nbsp;<telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBatal" runat="server" Height="200px" Width="224px"
                Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false" EmptyMessage="Alasan Pembatalan"
                DataTextField="vc_jenisOp" DataValueField="vc_kd_jenisOp" Skin="Office2010Silver" Font-Size="Small" AutoPostBack="True">
            </telerik:RadComboBox>
            <br />
            <br />
            <div>
                <asp:Label ID="Label12" runat="server" Text="Petugas yang daftar" Font-Size="Small" Font-Bold="True"></asp:Label><br />
                <asp:TextBox ID="TxtNikPetugas" runat="server" TextMode="MultiLine" Width="99px" MaxLength="8" Height="24px" Font-Size="Small"></asp:TextBox>
                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboNamaPetugas" runat="server" Height="200px" Width="192px"
                    AutoPostBack="true"
                    Filter="Contains" MarkFirstMatch="true" ChangeTextOnKeyBoardNavigation="false" EmptyMessage="Pilih Petugas" Font-Size="Small"
                    DataTextField="vc_nama_kry" DataValueField="vc_nid" Skin="Office2010Silver" />

                <br />
            </div>
          
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender0" runat="server"
                TargetControlID="txtPlanPukul" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" />
            &nbsp;
                  <center>
                <asp:Button ID="btnSave" runat="server" Text="Save" Font-Size="Small"/>  &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnBatal" runat="server" Text="Batal" Font-Size="Small" OnClientClick="Confirm_Batal()"/>
            </center>
        </div>


    </div>
    <script type="text/javascript">
        function Confirm_Batal() {
            var confirm_batal = document.createElement("INPUT");
            confirm_batal.type = "hidden";
            confirm_batal.name = "confirm_batal";
            if (confirm("Apakah Anda Yakin Membatalkan Transaksi?")) {
                confirm_batal.value = "Yes";
            } else {
                confirm_batal.value = "No";
            }
            document.forms[0].appendChild(confirm_batal);
        }
        function confirm_tabrak(message) {

            var confirm_tabrak = document.createElement("INPUT");
            confirm_tabrak.type = "hidden";
            confirm_tabrak.name = "confirm_tabrak";
            if (confirm(message)) {
                confirm_tabrak.value = "Yes";
            } else {
                confirm_tabrak.value = "No";
            }
            document.forms[0].appendChild(confirm_tabrak);
        }

    </script>
    <%-- <ajaxToolkit:ModalPopupExtender ID="gridviewDokter_ModalPopupExtender" PopupControlID="panelDokter" runat="server" TargetControlID="BtnAddDokter">
            </ajaxToolkit:ModalPopupExtender>--%>
</asp:Content>
