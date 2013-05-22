<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedInSettings.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Apps.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Appacts | Apps</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content">

<div class="Info">
    <h3>APP</h3>
    <p>Register new Apps, updating exist App's and view App keys</p>
</div>
<div class="Section">
    <View:SetupAppAdd runat="server" />
</div>
<div class="Section">
    <View:SetupAppUpdate runat="server" /> 
</div>
<div class="Section">
    <View:SetupAppView runat="server" />
</div>


</asp:Content>