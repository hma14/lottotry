<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="memberLogin.aspx.cs" Inherits="Lottotry.memberLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></act:ToolkitScriptManager>
            <h1>Log In </h1>
            <hr />
            <div style="font-size: small;">
                <asp:Label ID="Label2" class="star" runat="server" Text="* Indicates required field" />
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                    <table class="tables" cellpadding="3" cellspacing="5" width="500">
                        <tr>
                            <td>User ID:
                            </td>
                            <td>
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="tbUserID"
                                    WatermarkText="Enter Your User Name" WatermarkCssClass="watermark">
                                </act:TextBoxWatermarkExtender>
                                <asp:TextBox ID="tbUserID" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="1"
                                    Text="" />
                                <asp:Label ID="Label1" runat="server" class="star" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="lbl2" ErrorMessage="Required Field"
                                    ControlToValidate="tbUserID" SetFocusOnError="True" Display="Dynamic" Width="100%"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">Password:
                            </td>
                            <td>
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="tbPassword"
                                    WatermarkText="Enter Your Password" WatermarkCssClass="watermark">
                                </act:TextBoxWatermarkExtender>
                                <asp:TextBox ID="tbPassword" class="textbox" runat="server" TextMode="Password" SkinID="TextBoxSkin"
                                    TabIndex="2" />
                                <asp:Label ID="Labe2" runat="server" class="star" Text="*"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="lbl2" ControlToValidate="tbPassword"
                                    ErrorMessage="Required Field" Display="Dynamic" SetFocusOnError="True" Width="100%"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="rememberme" class="rememberme_forget_password" runat="server" Text="Remember Me" />
                            </td>
                            <td>
                                <asp:HyperLink ID="hlForget" class="rememberme_forget_password" runat="server" NavigateUrl="~/forget_password.aspx">Forget your password?</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
 
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <table class="tables" cellspacing="3" cellpadding="5" width="400px">
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" class="buttons" runat="server" Text="Submit" OnClick="btnSubmit_Click" TabIndex="3" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" class="buttons" runat="server" Text="Cancel" TabIndex="4"
                            Style="margin-left: 50px" OnClick="btnCancel_Click" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" class="buttons" runat="server" Text="Reset" Style="margin-left: 50px" OnClick="btnReset_Click" TabIndex="5" CausesValidation="False" />
                    </td>
                </tr>

            </table>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" />
    </div>
</asp:Content>




