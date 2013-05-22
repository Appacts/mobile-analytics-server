<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedInSettings.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Account.Profile.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appacts | Profile</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

<div class="Info">
    <h3>ACCOUNT</h3>
    <p>Change your basic account settings.</p>
</div>

<div class="Section">
    <div class="Inputs">
        <div class="Messages">
            <asp:Label runat="server" ID="lblSuccess" Text="<%$ Resources:Messages, SuccessProfileUpdated %>" Visible="false" EnableViewState="false" SkinID="sknMessageSuccess" />
            <asp:Label runat="server" ID="lblErrorEmailTaken" Text="<%$ Resources:Messages, ErrorEmailTaken %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblErrorValidation" Text="<%$ Resources:Messages, ErrorValidation %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />

            <asp:RequiredFieldValidator runat="server" ID="reqName" ValidationGroup="vgProfile" ErrorMessage="Please enter your name." Text=""
                ControlToValidate="txtName" ForeColor="#353535" Display="Dynamic" />
            <asp:RequiredFieldValidator runat="server" ID="reqEmail" ValidationGroup="vgProfile" ErrorMessage="Please enter your email address." Text=""
             ControlToValidate="txtEmail" ForeColor="#353535" Display="Dynamic" />
            <asp:RegularExpressionValidator runat="server" ID="regForgotPassword" ErrorMessage="Email address must be valid i.e. email@email.com" Text=""
                Display="Dynamic" ValidationGroup="vgProfile" ControlToValidate="txtEmail" ForeColor="#353535"
                ValidationExpression="([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3})|([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+)" />
        </div>

        <div class="Input">
            <asp:Label runat="server" AssociatedControlID="txtName" Text="Name:" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusName" ControlToValidate="txtName" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgProfile" />
            <asp:TextBox runat="server" ID="txtName" CssClass="Field" />
        </div>
        <div class="Input">
            <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email:" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusEmail" ControlToValidate="txtEmail" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgProfile" />
            <asp:TextBox runat="server" ID="txtEmail" CssClass="Field" />
        </div>

        <asp:Button runat="server" ID="btnSubmitDetails" Text="UPDATE"
                CausesValidation="true" ValidationGroup="vgProfile" OnClick="btnSubmitDetails_OnClick" CssClass="callToAction" />
    </div>
</div>

<div class="Section">
    <div class="Inputs">
        <div class="Messages">
            <asp:Label runat="server" ID="lblError" Visible="false" Text="<%$ Resources:Messages, ErrorGeneral %>" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblSuccessPasswordChanged" Text="<%$ Resources:Messages, SuccessProfilePasswordChanged %>" Visible="false" EnableViewState="false" SkinID="sknMessageSuccess" />
            <asp:Label runat="server" ID="lblErrorPasswordValidation" Text="<%$ Resources:Messages, ErrorPasswordValidation %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblErrorPasswordOldDontMatch" Text="<%$ Resources:Messages, ErrorPasswordOldDontMatch %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPasswordOld" ValidationGroup="vgPasswordChange"
                ErrorMessage="Please enter your current password." Text="" ForeColor="#353535" Display="Dynamic" />
            <asp:RequiredFieldValidator runat="server" ID="reqPassword" ControlToValidate="txtPassword" 
                ValidationGroup="vgPasswordChange" ErrorMessage="Please enter your new password" Text="" ForeColor="#353535" Display="Dynamic" />
            <asp:RequiredFieldValidator runat="server" ID="reqPasswordConfirm" 
                ControlToValidate="txtPasswordConfirm" ValidationGroup="vgPasswordChange" ErrorMessage="Please confirm your new password" Text="" ForeColor="#353535" Display="Dynamic" />
        </div>

        <div class="Input">
            <asp:Label runat="server" AssociatedControlID="txtPasswordOld" Text="Current Password:" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusPasswordOld" ControlToValidate="txtPasswordOld" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgPasswordChange" />
            <asp:TextBox runat="server" ID="txtPasswordOld" CssClass="Field" TextMode="Password" />
        </div>
        <div class="Input">
            <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password:" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusPassword" ControlToValidate="txtPassword" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgPasswordChange" />
            <asp:TextBox runat="server" ID="txtPassword" CssClass="Field" TextMode="Password" />
        </div>
        <div class="Input">
            <asp:Label runat="server" AssociatedControlID="txtPasswordConfirm" Text="Password Confirm:" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusPasswordConfirm" ControlToValidate="txtPasswordConfirm" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgPasswordChange" />
            <asp:TextBox runat="server" ID="txtPasswordConfirm" CssClass="Field" TextMode="Password" />        
        </div>

        <asp:Button runat="server" ID="btnSubmitPassword" Text="CHANGE PASSWORD" CssClass="callToAction"
            OnClick="btnSubmitPassword_OnClick" ValidationGroup="vgPasswordChange" CausesValidation="true" />
    </div>
</div>

</asp:Content>
