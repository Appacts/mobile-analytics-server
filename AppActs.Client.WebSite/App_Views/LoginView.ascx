<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginView.ascx.cs" Inherits="AppActs.Client.WebSite.App_Views.LoginView" %>

<asp:Panel runat="server" CssClass="Main">
<div class="borderRight">
    <div class="borderLeft">
        <div class="Container">
            <div class="Info">
                <h3>LOG IN</h3>
                <asp:HyperLink runat="server" NavigateUrl="~/Login.aspx?forgot-password=true">
                Forgotten log in details?
                </asp:HyperLink>
            </div>
            <div class="Section">
                    <div class="Inputs">
                        <div class="Messages">
                            <asp:Label runat="server" ID="lblErrorInvalidLogin" Text="<%$ Resources:Messages, ErrorLoginInvalid %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                            <asp:Label runat="server" ID="lblErrorInvalidCredentials" Text="<%$ Resources:Messages, ErrorLoginInvalidCredentials %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                            <asp:Label runat="server" ID="lblError" Text="<%$ Resources:Messages, ErrorGeneral %>" Visible="false" Enabled="false" SkinID="sknMessageError" />

                            <asp:RegularExpressionValidator runat="server" ID="regEmail" ErrorMessage="Email address must be valid i.e. email@email.com." Text=""
                                Display="Dynamic" ValidationGroup="vgLogin" ControlToValidate="txtEmail"
                                ValidationExpression="([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3})|([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+)"
                                ForeColor="#353535" />
                            <asp:RequiredFieldValidator runat="server" ID="reqEmail" ControlToValidate="txtEmail" ErrorMessage="Please enter your email address." Text="" ValidationGroup="vgLogin" 
                                   ForeColor="#353535" Display="Dynamic" />
                            <asp:RequiredFieldValidator runat="server" ID="reqPassword" ControlToValidate="txtPassword" ErrorMessage="Please enter your password." Text=""
                                    ValidationGroup="vgLogin" ForeColor="#353535" Display="Dynamic" />
                        </div>

                        <div class="Input">
                            <asp:Label runat="server" ID="lblEmail" AssociatedControlID="txtEmail" Text="Email" CssClass="Title" />
                            <asp:CustomValidator runat="server" ID="cusEmail" ControlToValidate="txtEmail" ClientValidationFunction="validateField"
                                ValidateEmptyText="true" ValidationGroup="vgLogin" />
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="Field" />  
                            <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmeEmail" TargetControlID="txtEmail"
                                WatermarkText="Your email address i.e. email@email.com" WatermarkCssClass="AjaxWatermark TextBoxNormal FloatLeft" />                                  
                        </div>
                        <div class="Input">
                            <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password" CssClass="Title" />
                            <asp:CustomValidator runat="server" ID="cusPassword" ControlToValidate="txtPassword" ClientValidationFunction="validateField"
                                ValidateEmptyText="true" ValidationGroup="vgLogin" />
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="Field" TextMode="Password" />            
                        </div>
                        <asp:Button runat="server" ID="btnSubmit" Text="LOG IN" CssClass="callToAction"
                            OnClick="btnSubmit_OnClick" CausesValidation="true" ValidationGroup="vgLogin" />    
                    </div>  
                </div>            
            </div>
        </div>
    </div>
</asp:Panel>
