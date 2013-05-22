<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/DefaultLoggedOut.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppActs.Client.WebSite.Password_Change.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Appacts | Password Reset</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

<asp:Panel ID="Panel1" runat="server" CssClass="Main">
    <div class="borderRight">
        <div class="borderLeft">
            <div class="Container">
                <div class="Info">
                    <h3>PASSWORD RESET</h3>
                </div>
            
                <div class="Section">
                    <asp:MultiView runat="server" ID="mvChangePassword" ActiveViewIndex="0">
                        <asp:View runat="server" ID="vwChangePassword">
                             <asp:Label runat="server" ID="lblErrorInvalidPassword" Text="<%$ Resources:Messages, ErrorPasswordValidation %>" 
                                EnableViewState="false" Visible="false" SkinID="sknMessageError"/>

                            <div class="Inputs">
                                <div class="Messages">
                                    <asp:RequiredFieldValidator runat="server" ID="reqPassword" ControlToValidate="txtPassword" 
                                        ValidationGroup="vgPasswordChange" ErrorMessage="Please enter your new password." Text="" ForeColor="#353535" Display="Dynamic" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqPasswordConfirm" 
                                        ControlToValidate="txtPasswordConfirm" ValidationGroup="vgPasswordChange" ErrorMessage="Please confirm your password."
                                        Text="" ForeColor="#353535" Display="Dynamic" />
                                </div>
                                <div class="Input">
                                    <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password:" CssClass="Title" />
                                    <asp:CustomValidator runat="server" ID="cusPassword" ControlToValidate="txtPassword" ClientValidationFunction="validateField"
                                        ValidateEmptyText="true" ValidationGroup="vgLogin" />
                                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="Field" />                               
                                </div>
                                <div class="Input">
                                    <asp:Label runat="server" AssociatedControlID="txtPasswordConfirm" Text="Password Confirm:" CssClass="Title" />
                                    <asp:CustomValidator runat="server" ID="cusPasswordConfirm" ControlToValidate="txtPasswordConfirm" ClientValidationFunction="validateField"
                                        ValidateEmptyText="true" ValidationGroup="vgLogin" />
                                    <asp:TextBox runat="server" ID="txtPasswordConfirm" TextMode="Password" CssClass="Field" />                            
                                </div>

                                <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_OnClick" CssClass="callToAction"
                                    Text="Change Password" CausesValidation="true" ValidationGroup="vgPasswordChange" />
                            </div>
                        </asp:View>

                        <asp:View runat="server" ID="vwChangePasswordSuccess">
                            <asp:Label runat="server" Text="<%$ Resources:Messages, SuccessPasswordReset %>" SkinID="sknMessageSuccess" />
                        </asp:View>

                        <asp:View runat="server" ID="vwInvalidGuid">
                            <asp:Label runat="server" Text="<%$ Resources:Messages, ErrorPasswordResetInvalidToken %>" SkinID="sknMessageError" />
                        </asp:View>

                        <asp:View runat="server" ID="vwError">
                            <asp:Label runat="server" Text="<%$ Resources:Messages, ErrorGeneral %>" SkinID="sknMessageError" />
                        </asp:View>
                    </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
</asp:Panel>






</asp:Content>
