<%@ Page Language="VB" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="false" CodeFile="3kolom.aspx.vb" Inherits="_3kolom" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TxtOperasi_onclick() {

}

// ]]>
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">
   
    <div class="gridContainer">
        <!-- kolom 1-->       
        <div class="fourGrid" > <!--fourGrid-->
            &nbsp;
            <br />
            selamat datang
        </div> <!--fourGrid-->
        <br />
        
        <!-- kolom 2-->
        
        <div class="threequartersGrid" style="width: 30%">
            <div class="responsiveForm pd10" style="background-color: #7a7cf0">
                <div>
                    <label id="title1" style="color:red" for="Field1">NAMA TEMPLATE:</label>
                    <div>&nbsp;<asp:TextBox ID="TxtOperasi" runat="server" AutoPostBack="True" Font-Size="Large"></asp:TextBox>
                </div>
            </div>
            <div>
                <label id="title4" style="color:red" for="Field4">
                    DESKRIPSI:
                </label>
            </div>
                <asp:TextBox ID="TxtDeskripsi" runat="server" Height="318px" TextMode="MultiLine" Font-Size="Large"></asp:TextBox>
            </div>
            
          
            <asp:Button ID="cmdInput" runat="server" Text="Input" Font-Bold="True" ForeColor="Red" />
            <asp:Button ID="cmdhapus" runat="server" Text="Hapus" Font-Bold="True" ForeColor="Red" OnClientClick="return confirm('Data ini mau dihapus?');" />
            <asp:Button ID="CmdKeluar" runat="server" Text="Keluar" Font-Bold="True" ForeColor="Red" />
            
            <div class="clear"></div> 
            SSAMPAI SINI
         </div> <div class="threequartersGrid" style="width: 100%; left: 10%; position: relative; left: 55px;">
             <div class="responsiveForm pd10" style="background-color: #7a7cf0">
                 <div>
                     <label id="Label1" style="color:red" for="Field1">
                         NAMA TEMPLATE:</label>
                     <div>
                         &nbsp;<asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" Font-Size="Large"></asp:TextBox>
                     </div>
                 </div>
                 <div>
                     <label id="Label2" style="color:red" for="Field4">
                         DESKRIPSI:
                     </label>
                 </div>
                 <asp:TextBox ID="TextBox2" runat="server" Font-Size="Large" Height="318px" TextMode="MultiLine"></asp:TextBox>
             </div>
             <asp:Button ID="Button1" runat="server" Text="Input" Font-Bold="True" ForeColor="Red" />
             <asp:Button ID="Button2" runat="server" Text="Hapus" Font-Bold="True" ForeColor="Red" OnClientClick="return confirm('Data ini mau dihapus?');" />
             <asp:Button ID="Button3" runat="server" Text="Keluar" Font-Bold="True" ForeColor="Red" />
             <div class="clear">
             </div>
             SSAMPAI SINI
         </div>
        <!--threequartersGrid-->            


        <!-- kolom 3-->       

    </div>  <!--gridContainer-->

    <!-- HARUS ADA DIV CELAR UNTUK MENYAMBUNGSPACE DENGAN FOOTER-->
    <div class="clear"></div>
</asp:Content>

