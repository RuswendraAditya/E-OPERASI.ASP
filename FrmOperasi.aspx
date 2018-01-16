<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="FrmOperasi.aspx.vb" Inherits="FrmOperasi" title="Laporan Operasi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    &nbsp;<div class="clear">
    </div>
    <div class="gridContainer">
        <h1>
            Laporan Operasi</h1>
    </div>
    <div class="gridContainer">
        <div class="responsiveForm pd10" style="background-color: #7a7cf0">
            <div>
                <label id="title1" style="color:red" for="Field1">NAMA TEMPLATE:</label>
                <div>
                    <asp:DropDownList ID="ddloperasi" runat="server" AutoPostBack="True" CausesValidation="True"
                        Height="30px" Width="304px">
                    </asp:DropDownList>&nbsp;</div>
            </div>
            <div>
                    <div>&nbsp;</div>
            </div>
            <asp:TextBox ID="TxtDeskripsi" runat="server" Height="318px" TextMode="MultiLine" Font-Size="Large" Width="100%"></asp:TextBox>
        </div>
        <br />
        <asp:Button ID="cmdInput" runat="server" Text="Simpan" Font-Bold="True" ForeColor="Red" />
        <asp:Button ID="cmdhapus" runat="server" Text="Hapus" Font-Bold="True" ForeColor="Red" OnClientClick="return confirm('Data ini mau dihapus?');" />
        <asp:Button ID="CmdKeluar" runat="server" Text="Keluar" Font-Bold="True" ForeColor="Red" />
        <div class="clear">

    </div>
    </div>
</asp:Content>

