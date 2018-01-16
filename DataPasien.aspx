<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="false" EnableEventValidation = "false" CodeFile="DataPasien.aspx.vb" Inherits="DataPasien" title="Data Pasien" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" Runat="Server">



    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="Server" CombineScripts="false"
        EnableScriptGlobalization="true" EnableScriptLocalization="true" ScriptMode="Debug">     
    </ajaxToolkit:ToolkitScriptManager>
 <div>
            <div class="column tRight">
                  <br />
                  <asp:DropDownList ID="DdlFilterData" runat="server" Font-Size="Small">
                      <asp:ListItem>Form Masuk</asp:ListItem>
                      <asp:ListItem>Rencana Operasi</asp:ListItem>
                  </asp:DropDownList>
                  Tgl&nbsp;
                <asp:TextBox ID="TxtTanggalStart" runat="server" Width="80px" Font-Size="Small" ></asp:TextBox>
                    <ajaxToolkit:MaskedEditExtender ID="TxtTanggalStart_MaskedEditExtender" runat="server"
                        TargetControlID="TxtTanggalStart"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled="True" />
                    <ajaxToolkit:CalendarExtender id="TxtTanggalStart_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TargetControlID="TxtTanggalStart">
                    </ajaxToolkit:CalendarExtender>                
                  <label id="lblDari" for="Field1" >
                    &nbsp;S.d: 
                </label>
                <asp:TextBox ID="TxtTanggalEnd" runat="server" Width="80px" Font-Size="Small" ></asp:TextBox>&nbsp;  
                <asp:Button ID="CmdVIEW" runat="server" Text="View" Width="53px" />
            </div>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                        TargetControlID="TxtTanggalEnd"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left"
                        ErrorTooltipEnabled="True" />
                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server"
                        ControlExtender="MaskedEditExtender5"
                        ControlToValidate="TxtTanggalEnd"
                        EmptyValueMessage="Date is required"
                        InvalidValueMessage=""
                        Display="Dynamic"
                        IsValidEmpty="false"
                        TooltipMessage="Input a date"
                        EmptyValueBlurredText="*"
                        InvalidValueBlurredMessage=""
                        ValidationGroup="MKE" />                    
                    <ajaxToolkit:CalendarExtender id="TxtTanggalEnd_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TargetControlID="TxtTanggalEnd">
                    </ajaxToolkit:CalendarExtender>                
     <br />
                 <asp:GridView ID="GridView1" runat="server"
                AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="False" CellPadding="4"
                CssClass="responsiveTable1" DataKeyNames="vc_no_rm" Width="100%" >
                <RowStyle BackColor="#F1F2F6" Font-Size="15px" Font-Bold="False" Font-Names="serif" Height="28px" />
         <Columns>
             <asp:TemplateField HeaderText="NAMA PASIEN" SortExpression="vc_nama_p">
                 <ItemTemplate>
                     <asp:Label ID="lblNamaPasien" runat="server" Text='<%# Bind("vc_nama_p") %>'></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="170px" />
             </asp:TemplateField>

             <asp:TemplateField HeaderText="No.RM">
                 <ItemTemplate>
                     <asp:Label ID="lblNoRM" runat="server" Text='<%# Bind("VC_No_RM") %>'></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="10px" />
             </asp:TemplateField>

             <asp:TemplateField HeaderText="KAMAR">
                 <ItemTemplate>
                     <asp:Label ID="lblKamar" runat="server" Text='<%# Bind("vc_n_ruang") %>'></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="200px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="NO.REG">
                 <ItemTemplate>
                     <asp:Label ID="lblNoRegj" runat="server" Text='<%# Bind("vc_no_reg") %>'></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="5px" />
             </asp:TemplateField>
             <asp:TemplateField HeaderText="NO.TRANSAKSI">
                 <ItemTemplate>
                     <asp:Label ID="lblNoTrans" runat="server" Text='<%# Bind("vc_no_trans") %>'></asp:Label>
                 </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="5px" />
             </asp:TemplateField>

     

         </Columns>
         <FooterStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" />
         <PagerStyle BackColor="#666666" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="#B23516" Font-Bold="False" ForeColor="DarkOrange" />         
         <HeaderStyle BackColor="#3C5779" Font-Bold="True" ForeColor="White" Height="30px" />         
         <EditRowStyle BackColor="#7C6F57" ForeColor="Red" />
         <AlternatingRowStyle BackColor="White" CssClass="alt" />
     </asp:GridView>
     <asp:SqlDataSource ID="SdsData" runat="server"></asp:SqlDataSource>
     <br />
     <asp:TextBox ID="TextBox1" runat="server" BackColor="LightGreen" Width="36px"></asp:TextBox>
     &nbsp;Selesai Operasi
     <asp:TextBox ID="TextBox2" runat="server" BackColor="Pink" Width="36px"></asp:TextBox>
     &nbsp;Masuk Jadwal Operasi</div>
</asp:Content>

