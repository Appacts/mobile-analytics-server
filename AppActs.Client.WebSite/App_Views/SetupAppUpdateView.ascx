<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupAppUpdateView.ascx.cs" Inherits="AppActs.Client.WebSite.App_Views.SetupAppUpdateView" %>


<h3>UPDATE AN EXISTING APP</h3>

<asp:Panel runat="server" DefaultButton="btnUpdate"  CssClass="Container">
<div class="Inputs">
    <div class="Messages">
        <asp:Label runat="server" ID="lblErrorPlatformNeedsToBeSelected" Text="<%$ Resources:Messages, ErrorAtleastOnePlatformMustBeSelected %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
        <asp:Label runat="server" ID="lblErrorNameTaken" Text="<%$ Resources:Messages, ErrorAppNameTaken %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
        <asp:Label runat="server" ID="lblErrorValidation" Text="<%$ Resources:Messages, ErrorValidation %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
        <asp:Label runat="server" ID="lblError" Text="<%$ Resources:Messages, ErrorGeneral %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
        <asp:Label runat="server" ID="lblSuccessRemoved"  Visible="false" EnableViewState="false" SkinID="sknMessageSuccess" />

        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlApps" ValidationGroup="AppUpdate" ErrorMessage="Please select an app."
            ID="RequiredFieldValidator1" Display="Dynamic" ForeColor="#353535" InitialValue="0" />

        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlApps" ValidationGroup="AppRemove" ErrorMessage="Please select an app."
            ID="RequiredFieldValidator3" Display="Dynamic" ForeColor="#353535" InitialValue="0" />

        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ValidationGroup="AppUpdate" ErrorMessage="Please enter the new app name."
            ID="reqName" Display="Dynamic" ForeColor="#353535" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPlatform" ValidationGroup="AppUpdate"
            ErrorMessage="Please select at least one platform." ID="RequiredFieldValidator2" ForeColor="#353535" Display="Dynamic" />
    </div>
    <div class="Input">
        <asp:Label runat="server" AssociatedControlID="ddlApps" CssClass="Title">Select App</asp:Label>
        <asp:CompareValidator runat="server" ControlToValidate="ddlApps" ErrorMessage="" Operator="NotEqual" ValueToCompare="0" ValidationGroup="AppUpdateSelect" ID="reqApps" />
        <script type="text/javascript" language="javascript">
            $(document).ready(function () {
                $('#<%= ddlApps.ClientID %>').msDropDown({ showIcon: false });
            });
        </script>
        <asp:DropDownList runat="server" ID="ddlApps" DataTextField="Name" DataValueField="Guid" 
            OnSelectedIndexChanged="ddlApps_OnSelectedIndexChanged" CausesValidation="true" 
            ValidationGroup="AppUpdateSelect" AutoPostBack="true" CssClass="Field"  />

        <script type"text/javascript" language="javascript">
            var ddlAppsUpdate = '#<%= ddlApps.ClientID %>';
        </script>

    </div>
    <div class="Input">
        <asp:Label runat="server" ID="lblPlatform" AssociatedControlID="ddlPlatform" CssClass="Title">Platform</asp:Label>
        <asp:CompareValidator runat="server" ControlToValidate="ddlPlatform" ErrorMessage="" Operator="NotEqual" ValueToCompare="0" ValidationGroup="AppUpdate" ID="reqPlatform" />
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    $('#<%= ddlPlatform.ClientID %>').multiselect({
                        header: false,
                        classes: "Field"
                    });
                });
            </script>
        <asp:ListBox runat="server" ID="ddlPlatform" DataTextField="Name" ValidationGroup="AppUpdate"
            DataValueField="Id" SelectionMode="Multiple" CssClass="Field"  />
    </div>
    <div class="Input">
        <asp:Label runat="server" ID="lblName" AssociatedControlID="txtName" CssClass="Title">Rename the App</asp:Label>
        <asp:CustomValidator runat="server" ID="cusName" ControlToValidate="txtName" ClientValidationFunction="validateField"
                ValidateEmptyText="true" ValidationGroup="AppUpdate" />
        <asp:TextBox runat="server" ID="txtName" CssClass="Field" ValidationGroup="AppUpdate" />
    </div>
    <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_OnClick" 
        Text="SAVE" ValidationGroup="AppUpdate" CssClass="callToAction" CausesValidation="true" />

    <asp:Button runat="server" ID="btnRemove" OnClick="btnRemove_OnClick" 
        Text="REMOVE APP"  CssClass="additionalOption" CausesValidation="true" ValidationGroup="AppRemove" 
        OnClientClick="return Page_ClientValidate('AppRemove') && confirm('Are you sure you want to remove this app?');" />
</div>
</asp:Panel>

<asp:HiddenField runat="server" ID="hdnApplicationId" />