<%@ Page Language="VB" MasterPageFile="~/MasterPageHome.master" ValidateRequest="false" AutoEventWireup="false" EnableEventValidation = "false" CodeFile="Template.aspx.vb" Inherits="Template" title="Template Operasi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TxtOperasi_onclick() {

}

// ]]>
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="clear">
    </div>
    <div class="gridContainer" >
        <h1>
            Template Operasi</h1>
    </div>
    <div class="gridContainer">
        <!--fourGrid-->
        <div class="fourGrid" >
            <asp:GridView ID="GridView1" runat="server"
                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CellPadding="4"
                CssClass="mGrid"  
                PagerStyle-CssClass="pgr" Width="100%" Height="50px">
                <RowStyle BackColor="#F1F2F6" Font-Size="20px" Font-Bold="False" Font-Names="serif" BorderColor="#000040" BorderStyle="Solid" Height="35px"   />
                    <Columns>
                        <asp:TemplateField HeaderText="OPERASI">
                            <ItemTemplate>
                                <asp:Label ID="lblOperasi" runat="server" Text='<%# Bind("VC_NAMA_OPERASI") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="70px" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
                    <PagerStyle BackColor="#666666" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#B23516" Font-Bold="False" ForeColor="#99F65E" />         
                    <HeaderStyle BackColor="#3C5779" Font-Bold="True" ForeColor="Aquamarine" Font-Names="Times New Roman" Font-Size="X-Large" Height="50px" />         
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
            <asp:SqlDataSource ID="SdsData" runat="server"></asp:SqlDataSource>
        </div> <!--fourGrid-->
        <br />
    <div class="threequartersGrid" style="width: 70%">
        <div class="responsiveForm pd10" style="background-color: #7a7cf0">
            <div>
                <label id="title1" style="color:red" for="Field1">NAMA TEMPLATE:</label>
                <div>&nbsp;<asp:TextBox ID="TxtOperasi" runat="server" AutoPostBack="True" Font-Size="Large"></asp:TextBox></div>
            </div>
            <div>
                    <label id="title4" style="color:red" for="Field4">
                        DESKRIPSI:
                    </label>
                    <div>&nbsp;</div>
            </div>
            <asp:TextBox ID="TxtDeskripsi" runat="server" Height="318px" TextMode="MultiLine" Font-Size="Large"></asp:TextBox>
        </div>
        <br />
        <asp:Button ID="cmdInput" runat="server" Text="Input" Font-Bold="True" ForeColor="Red" />
        <asp:Button ID="cmdhapus" runat="server" Text="Hapus" Font-Bold="True" ForeColor="Red" OnClientClick="return confirm('Data ini mau dihapus?');" />
        <asp:Button ID="CmdKeluar" runat="server" Text="Keluar" Font-Bold="True" ForeColor="Red" />
        <div class="clear">
    </div>
    </DIV><!--gridContainer--><div class="clear">
    </div>
</asp:Content>

