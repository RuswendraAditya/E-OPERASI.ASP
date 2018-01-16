<%@ Page Language="VB" MasterPageFile="~/MasterPageHome.master" AutoEventWireup="false" CodeFile="LoginTemplate.aspx.vb" Inherits="LoginTemplate" title="Login Template" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <!--meta name="description" content="Barebone is a ASP.NET template developed by www.templatesaspnet.com site." />
    <meta name="keywords" content="Barebone, template, asp.net template, template aspx" /-->

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContent" Runat="Server">
    <div class="gridContainer">
    <div class="gridContainer" style="width: 200px">
        <h1>Template Operasi</h1>
        <div class="titleGrid">
            <h3>
                Login</h3>
        </div>
        <div class="LoginForm f12">
            <div class="responsiveForm">
                <div>
                    <label id="Name" class="" for="Field1">
                        User:</label>
                    <div>
                        &nbsp;<asp:TextBox ID="txtUserName" runat="server" autocomplete="off" Height="22px"
                            MaxLength="50" Width="148px"></asp:TextBox><asp:RequiredFieldValidator ID="RV1" runat="server"
                                ControlToValidate="txtUserName" ErrorMessage="Masukkan UserName" InitialValue='""'
                                SetFocusOnError="True">*
</asp:RequiredFieldValidator></div>
                </div>
                <div>
                    <label id="Password" class="" for="Field1">
                        Password:</label>
                    <div>
                        <asp:TextBox ID="txtPwd" runat="server" autocomplete="off" CssClass="pwd" Height="22px"
                            MaxLength="15" TextMode="Password" Width="148px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="txtPwd" ErrorMessage="Masukkan Password"
                            InitialValue='""' SetFocusOnError="True">*
</asp:RequiredFieldValidator></div>
                </div>
                <div>
                    <div>
                        &nbsp;<asp:Button ID="btnLogIn" runat="server" BackColor="Aquamarine" CssClass="button"
                            Height="27px" Text="OK" Width="103px" /></div>
                </div>
            </div>
        </div>
        </div> <!--fourGrid-->

    </div> <!--gridContainer-->
<div class="clear"></div>
</asp:Content>
