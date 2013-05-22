<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedInSettings.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Account.Settings.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appacts | Users</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

<div class="Info">
    <h3>USERS</h3>
    <p>Manage who has access to your app's analytics</p>
</div>

<div class="Section">
    <div class="Inputs">
        <div class="Messages">
            <asp:Label runat="server" ID="lblSuccessAdded" Text="<%$ Resources:Messages, SuccessSettingsUserAdded %>" Visible="false" EnableViewState="false" SkinID="sknMessageSuccess" />
            <asp:Label runat="server" ID="lblError" Text="%$ Resources:Messages, ErrorGeneral %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblErrorEmailTaken" Text="<%$ Resources:Messages, ErrorEmailTaken %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblErrorValidation" Text="<%$ Resources:Messages, ErrorValidation %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
            <asp:Label runat="server" ID="lblSuccessRemoved" Text="<%$ Resources:Messages, SuccessSettingsUserRemoved %>" Visible="false" EnableViewState="false" SkinID="sknMessageSuccess" />
            <asp:Label runat="server" ID="lblErrorEmailCantBeSent" Text="<%$ Resources:Messages, ErrorSettingsEmailCantBeSent %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />

            <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtName"
                ErrorMessage="Please enter your name" Text="" ValidationGroup="vgMemberAdd" CssClass="ValidatorNormal FloatLeft" Display="Dynamic" ForeColor="#353535" />
            <asp:RequiredFieldValidator runat="server" ID="reqEmail" ControlToValidate="txtEmail" Display="Dynamic"
                ErrorMessage="Please enter your email address." Text="" ValidationGroup="vgMemberAdd" ForeColor="#353535" />
            <asp:RegularExpressionValidator runat="server" ID="regForgotPassword" ErrorMessage="Email address must be valid i.e. email@email.com" Text=""
                Display="Dynamic" ValidationGroup="vgMemberAdd" ControlToValidate="txtEmail" ForeColor="#353535"
                ValidationExpression="([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3})|([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+)" />
        </div>

        <div class="Input">
            <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" Text="Name" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusName" ControlToValidate="txtName" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgMemberAdd" />
            <asp:TextBox runat="server" ID="txtName" CssClass="Field" />
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmeName" TargetControlID="txtName"
                    WatermarkText="Their name i.e. James" WatermarkCssClass="AjaxWatermark TextBoxNormal ClearBoth" />        
        </div>
        <div class="Input">
            <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="Email" CssClass="Title" />
            <asp:CustomValidator runat="server" ID="cusEmail" ControlToValidate="txtEmail" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="vgMemberAdd" />
            <asp:TextBox runat="server" ID="txtEmail" CssClass="Field" />
            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmeEmail" TargetControlID="txtEmail"
                    WatermarkText="Their email address i.e. email@email.com" WatermarkCssClass="AjaxWatermark TextBoxNormal ClearBoth" />        
        </div>

        <asp:Button runat="server" ID="btnMemberAdd"  OnClick="btnMemberAdd_OnClick" Text="ADD" CausesValidation="true"
            ValidationGroup="vgMemberAdd" CssClass="callToAction" />
    </div>
</div>

<div class="Section">
    <asp:GridView runat="server" ID="grvMembers" ShowHeader="true" Width="100%" BorderWidth="0" RowStyle-Height="50"
        OnRowDataBound="grvMembers_OnRowDataBound" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnMemberRemove" Text="REMOVE" CssClass="callToActionSmall" OnClick="btnMemberRemove_OnClick" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
</asp:Content>
