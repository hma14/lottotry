<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="userAdmin.aspx.cs" Inherits="Lottotry.Members.userAdmin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="contents">
        <div class="htmlcontent">
            <h1>
                Edit Profile</h1>
            <hr />
            <div style="font-size: small;">
                <asp:Label ID="star" class="star" runat="server" Text="* Indicates required field" /></div>
            <br />
            <table class="tables" cellpadding="2" cellspacing="1" width="500">
                <tr>
                    <td>
                        User ID:
                    </td>
                    <td>
                        <asp:Label ID="lblUserID" runat="server" class="userID"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPassword"
                            ErrorMessage="Password is required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Password:
                    </td>
                    <td>
                        <asp:PasswordStrength ID="PasswordStrength1" runat="server" 
                            RequiresUpperAndLowerCaseCharacters="True" Enabled="True" 
                            TargetControlID="tbPassword">
                        </asp:PasswordStrength>
                        <asp:TextBox ID="tbPassword" class="textbox" runat="server" SkinID="TextBoxSkin"
                            TextMode="Password" TabIndex="1"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbPassword2"
                            ControlToCompare="tbPassword" Operator="Equal" ErrorMessage="Passwords do not match."
                            SetFocusOnError="True" Display="Dynamic"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Password:
                    </td>
                    <td>
                        <asp:TextBox ID="tbPassword2" class="textbox" runat="server" SkinID="TextBoxSkin"
                            TextMode="Password" TabIndex="2"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbFName"
                            ErrorMessage="First Name is required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbFName" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="3"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbLName"
                            ErrorMessage="Last Name is required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID="tbLName" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="4"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbEmail"
                            ErrorMessage="Email is required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Email Format is Invalid"
                            SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"
                            ControlToValidate="tbEmail" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="tbEmail" class="textbox" runat="server" Text="" SkinID="TextBoxSkin"
                            TabIndex="5"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text="*" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:Button ID="btnSubmit" class="buttons" runat="server" Text="Save" OnClick="btnSubmit_Click"
                            SkinID="buttonSkin" TabIndex="6" Width="100" Style="margin-left: 50px" />
                    </td>
                    <td >
                        <asp:Button ID="btnCancel" class="buttons"  runat="server" Text="Cancel"  TabIndex="7"
                            OnClientClick="disableReqiredValidation();" OnClick="btnCancel_Click" Width="100" Style="margin-left: 50px"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 500px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="indicator">
                            <asp:Label ID="lblIndicator" runat="server" Text=""></asp:Label></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
