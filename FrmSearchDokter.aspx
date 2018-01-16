<%@ Page Language="VB" EnableEventValidation = "false"  MasterPageFile="~/MasterPageBlank.master" AutoEventWireup="false" CodeFile="FrmSearchDokter.aspx.vb" Inherits="FrmSearchDokter" Title="Cari Dokter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="Server" >
    <div class="gridContainer">
        <h1>Pilih Dokter</h1>
    </div>
    <div class="gridContainer" >

        <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Cari Nama Dokter" BorderStyle="Dashed"></asp:Label>
        &nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Font-Size="X-Small"></asp:TextBox>
        <br />
        <asp:GridView ID="GridView1" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CellPadding="4" 
            CssClass="responsiveTable1" Width="100%" Font-Size="Small" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Nama Dokter" SortExpression="vc_nama_kry">
                    <ItemTemplate>
                        <asp:Label ID="lblNamaDokter" runat="server" Text='<%# Bind("vc_nama_kry") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="170px" />

                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="Aqua" Font-Bold="False" ForeColor="Black" />
            <PagerStyle BackColor="#666666" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BorderStyle="Solid" />
            <SelectedRowStyle BackColor="#CCFFCC" Font-Bold="False" ForeColor="DarkOrange" />
            <HeaderStyle BackColor="#3C5779" Font-Bold="True" ForeColor="White" Height="30px" />
            <EditRowStyle BackColor="#7C6F57" ForeColor="Red" />
            <AlternatingRowStyle BackColor="White" CssClass="alt" />
        </asp:GridView>
        <asp:SqlDataSource ID="SdsData" runat="server"></asp:SqlDataSource>
        <br />

    </div>
    <script type="text/javascript">
        function closePage() {
         // parent.removeMyIFrame()
            parent.document.getElementById("irm1").style.display = "none"
          
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>
</asp:Content>
