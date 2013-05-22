<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgotPasswordView.ascx.cs" Inherits="AppActs.Client.WebSite.App_Views.ForgotPasswordView" %>

<div class="Main">
    <div class="borderRight">
        <div class="borderLeft">
            <div class="Container">
                <div class="Info"">
                    <h3>FORGOT YOUR PASSWORD</h3>
                </div>
                     <div class="Section">
                        <asp:MultiView runat="server" ID="mvForgotPassword" ActiveViewIndex="0">
                            <asp:View runat="server" ID="vwMain">

                                <asp:Label runat="server" ID="lblError" Text="<%$ Resources:Messages, ErrorGeneral %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                                <asp:Label runat="server" ID="lblErrorNoEmailAddress" Text="<%$ Resources:Messages, ErrorNoSuchEmailAddress %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />
                                <asp:Label runat="server" ID="lblErrorInvalidEmailAddress" Text="<%$ Resources:Messages, ErrorInvalidEmailAddress %>" Visible="false" EnableViewState="false" SkinID="sknMessageError" />

                                <div class="Inputs">
                                    <div class="Messages">
                                        <asp:RequiredFieldValidator runat="server" ID="reqEmail" ErrorMessage="Email" Text="Please enter your email address." Display="Dynamic"
                                            ValidationGroup="vgForgotPassword" ControlToValidate="txtEmail" ForeColor="#353535" />
                                        <asp:RegularExpressionValidator runat="server" ID="regForgotPassword" ErrorMessage="Email address must be valid i.e. email@email.com" Text=""
                                            Display="Dynamic" ValidationGroup="vgForgotPassword" ControlToValidate="txtEmail" ForeColor="#353535"
                                            ValidationExpression="([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+\.[a-zA-Z0-9]{1,3})|([a-zA-Z0-9._%-]+@[-a-zA-Z0-9]+\.[a-zA-Z0-9]+)" />
                                    </div>
                                    <div class="Input">
                                        <asp:Label runat="server" Text="Email" AssociatedControlID="txtEmail" CssClass="Title" />
                                        <asp:CustomValidator runat="server" ID="cusEmail" ControlToValidate="txtEmail" ClientValidationFunction="validateField"
                                            ValidateEmptyText="true" ValidationGroup="vgForgotPassword" />
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="Field" />
                                        <ajaxToolkit:TextBoxWatermarkExtender runat="server" ID="wmeEmail" TargetControlID="txtEmail"
                                            WatermarkText="Your email address i.e. email@email.com" WatermarkCssClass="AjaxWatermark TextBoxNormal FloatLeft" />                                
                                    </div>

                                    <asp:Button runat="server" ID="btnSubmit" Text="RESET" CssClass="callToAction"
                                        OnClick="btnSubmit_OnClick" CausesValidation="true" ValidationGroup="vgForgotPassword" /> 
                                </div>

                            </asp:View>
                            <asp:View runat="server" ID="vwSuccesfull">
                                <asp:Label runat="server" SkinID="sknMessageSuccess" ID="lblSuccessForgotPasswordRequest" />
                            </asp:View>
                        </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>

