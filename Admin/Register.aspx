<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Lottotry.Admin.Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <act:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></act:ToolkitScriptManager>
            <h1>Register</h1>
            <hr />
            <div style="font-size: small;">
                <asp:Label ID="Label1" class="star" runat="server" Text="* Indicates required field" />
            </div>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tables" cellspacing="3" cellpadding="5" width="600">
                        <tr>
                            <td>Username:
                            </td>
                            <td>
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="tbUserid"
                                    WatermarkText="Enter User Name" WatermarkCssClass="watermark">
                                </act:TextBoxWatermarkExtender>
                                <asp:TextBox ID="tbUserid" runat="server" class="textbox" SkinID="TextBoxSkin" TabIndex="1"
                                    Text=""></asp:TextBox>
                                <asp:Label ID="Label2" runat="server" class="star" Text=" * " />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="lbl2" ControlToValidate="tbUserid"
                                    Display="Dynamic" ErrorMessage="Username is Required" SetFocusOnError="True" Width="100%"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Email:
                            </td>
                            <td>
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="tbEmail"
                                    WatermarkText="Enter Your Valid Email" WatermarkCssClass="watermark">
                                </act:TextBoxWatermarkExtender>
                                <asp:TextBox ID="tbEmail" runat="server" class="textbox" SkinID="TextBoxSkin" TabIndex="6"
                                    Text=""></asp:TextBox>
                                <asp:Label ID="Label3" runat="server" class="star" Text="*" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" class="lbl2" ControlToValidate="tbEmail"
                                    ErrorMessage="Email Format is Invalid!" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"
                                    Display="Dynamic" SetFocusOnError="True" Width="100%"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="lbl2" ControlToValidate="tbEmail"
                                    ErrorMessage="Email is Required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>Confirm Email:
                            </td>
                            <td>
                                <act:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="tbEmail2"
                                    WatermarkText="Enter Your Valid Email again" WatermarkCssClass="watermark">
                                </act:TextBoxWatermarkExtender>
                                <asp:TextBox ID="tbEmail2" runat="server" class="textbox" SkinID="TextBoxSkin" TabIndex="7"
                                    Text=""></asp:TextBox>
                                <asp:Label ID="Label4" runat="server" class="star" Text="*" />
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail2"
                                            ErrorMessage="Not a valid email address!" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"></asp:RegularExpressionValidator>--%>
                                <%-- ValidationExpression="[\w-]+([\.]?[\w-])*\@[\w-]+([\.][\w-]+)+"></asp:RegularExpressionValidator>--%>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" class="lbl2" ControlToCompare="tbEmail"
                                    ControlToValidate="tbEmail2" ErrorMessage="Emails do not match" Height="18px"
                                    Display="Dynamic" SetFocusOnError="True" Width="100%"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" class="lbl2" ControlToValidate="tbEmail2"
                                    ErrorMessage="Confirm Email Required" Display="Dynamic" SetFocusOnError="True" Width="100%"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <table class="tables" cellspacing="5" cellpadding="5" style="width: 400px;">
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" class="buttons" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            TabIndex="14" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" class="buttons" runat="server" Text="Cancel"
                            Style="margin-left: 50px" TabIndex="15"
                            OnClick="btnCancel_Click" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" class="buttons" runat="server" Text="Reset" Style="margin-left: 50px"
                            OnClick="btnReset_Click" TabIndex="16" />
                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" />
    </div>
</asp:Content>

