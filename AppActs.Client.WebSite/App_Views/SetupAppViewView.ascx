<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupAppViewView.ascx.cs" Inherits="AppActs.Client.WebSite.App_Views.SetupAppViewView" %>

<h3>VIEW APP DETAILS</h3>

<div class="Container">
    <div class="Inputs" style="margin-bottom:0px;">
        <div class="Input">
            <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlApps" CssClass="Title">Select App</asp:Label>
            <asp:CompareValidator runat="server" ControlToValidate="ddlApps" ErrorMessage="*" Operator="NotEqual" ValueToCompare="0" ValidationGroup="AppViewSelect" ID="reqApps" />
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    $('#<%= ddlApps.ClientID %>').msDropDown({ showIcon: false });
                });
            </script>
            <asp:DropDownList runat="server" ID="ddlApps" DataTextField="Name" DataValueField="Id" 
                OnSelectedIndexChanged="ddlApps_OnSelectedIndexChanged" CausesValidation="true" 
                ValidationGroup="AppViewSelect" AutoPostBack="true"
                CssClass="Field"  />        
        </div>
        <div id="divApplication" class="Input" runat="server" visible="false">
            <span class="Title">Application ID</span>
            <asp:Label runat="server" CssClass="FieldSmall NoWrap" ID="lblApplicationId" />
        </div>
    </div>
</div>

<asp:Label runat="server" ID="lblError" Text="<%$ Resources:Messages, ErrorGeneral %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />