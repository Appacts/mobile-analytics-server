<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedIn.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Setup.Default" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>Appacts | Setup</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="content">


<div class="DivSetupView CenterDiv">
    <div>
        <h3 class="LabelNormal FloatLeft">App</h3>
        <h4>register new Apps, updating exist App's and view App keys</h4>
    </div>
    <div>
        <View:SetupAppAdd runat="server" />
    </div>
    <div>
        <View:SetupAppUpdate runat="server" /> 
    </div>
    <div>
        <View:SetupAppView runat="server" />
    </div>
</div>


</asp:Content>