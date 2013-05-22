<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupAppAddView.ascx.cs" Inherits="AppActs.Client.WebSite.App_Views.SetupAppAddView" %>

<h3>ADD A NEW APP</h3>

<asp:Panel runat="server" DefaultButton="btnAdd" CssClass="Container">
    <div class="Inputs">
        <div class="Input">
            <div class="Messages">
                <asp:Label runat="server" ID="lblErrorPlatformNeedsToBeSelected" Text="<%$ Resources:Messages, ErrorAtleastOnePlatformMustBeSelected %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                <asp:Label runat="server" ID="lblErrorNameTaken" Text="<%$ Resources:Messages, ErrorAppNameTaken %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                <asp:Label runat="server" ID="lblErrorValidation" Text="<%$ Resources:Messages, ErrorValidation %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                <asp:Label runat="server" ID="lblError" Text="<%$ Resources:Messages, ErrorGeneral %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ValidationGroup="AppAdd"
                    ErrorMessage="Please enter an app name." ID="reqName" ForeColor="#353535" Display="Dynamic" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPlatform" ValidationGroup="AppAdd"
                    ErrorMessage="Please select at least one platform." ID="RequiredFieldValidator1" ForeColor="#353535" Display="Dynamic" />
            </div>

            <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" CssClass="Title">App Name</asp:Label>
            <asp:CustomValidator runat="server" ID="cusName" ControlToValidate="txtName" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="AppAdd" />
            <asp:TextBox runat="server" ID="txtName" ValidationGroup="AppAdd" CssClass="Field" />
        </div>
        <div class="Input">
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    $('#<%= ddlPlatform.ClientID %>').multiselect({
                        header : false,
                        classes : "Field"
                    });
                });
            </script>

            <asp:Label runat="server" ID="lblPlatform" AssociatedControlID="ddlPlatform" CssClass="Title">Platform</asp:Label>
            <asp:CompareValidator runat="server" ControlToValidate="ddlPlatform" ErrorMessage="" Operator="NotEqual" ValueToCompare="0" ValidationGroup="AppAdd" ID="reqPlatform" />
            <asp:ListBox runat="server" ID="ddlPlatform" DataTextField="Name" 
                DataValueField="Id" SelectionMode="Multiple" CssClass="Field"  />
        </div>
        <asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_OnClick" 
            Text="ADD" CssClass="callToAction" ValidationGroup="AppAdd" />        
    </div>
</asp:Panel>


