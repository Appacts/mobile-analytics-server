<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedOut.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppActs.Client.WebSite.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appacts | Analytics</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

<asp:MultiView runat="server" ID="mvMain" ActiveViewIndex="1">
    <asp:View runat="server" ID="vwLogin">
        <View:Login ID="Login1" runat="server" />
    </asp:View>
    <asp:View runat="server" ID="vwForgotPassword">
        <View:ForgotPassword ID="ForgotPassword1" runat="server" />
    </asp:View>
</asp:MultiView>   

</asp:Content>
