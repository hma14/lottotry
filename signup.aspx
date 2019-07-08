<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="signup.aspx.cs" Inherits="Lottotry.signup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <h1>Sign Up</h1>
            <hr />
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <%--AJAX--%>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="font-size: small;">
                        <asp:Label class="star" runat="server" Text="* Indicates required field" />
                    </div>
                    <br />
                    <%--<table id="tblSignup" cellpadding="0" cellspacing="0" width="600" style="margin-left: 20px">--%>
                    <table class="tables" cellpadding="5" cellspacing="3" width="600">
                        <colgroup>
                            <col width="30%" />
                            <col width="70%" />
                            <tr>
                                <td>User ID:
                                </td>
                                <td>
                                    <asp:Label ID="lblUserID" class="userID" runat="server" SkinID="TextBoxSkin" TabIndex="2" Text="" />
                                </td>
                            </tr>
                            <tr>
                                <td>Password:
                                </td>
                                <td>
                                    <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="tbPassword"  TextCssClass="passwordStrength"
                                        RequiresUpperAndLowerCaseCharacters="true">
                                    </asp:PasswordStrength>
                                    <asp:TextBox ID="tbPassword" class="textbox" runat="server" SkinID="TextBoxSkin"
                                        TextMode="Password" TabIndex="2" Text=""></asp:TextBox>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" class="lbl2" ControlToValidate="tbPassword" Width="100%"
                                        ErrorMessage="Password is Required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Confirm Password:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbPassword2" class="textbox" runat="server" SkinID="TextBoxSkin"
                                        TabIndex="3" TextMode="Password"></asp:TextBox>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" class="lbl2" ErrorMessage="RequiredFieldValidator"
                                        ControlToValidate="tbPassword2" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" class="lbl2" ControlToCompare="tbPassword"
                                        ControlToValidate="tbPassword2" Display="Dynamic" ErrorMessage="Passwords do not match."
                                        Operator="Equal" SetFocusOnError="True"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>First Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbFName" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="4"></asp:TextBox>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" class="lbl2" ControlToValidate="tbFName" Width="100%"
                                        ErrorMessage="First Name is Required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Last Name:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbLName" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="5"></asp:TextBox>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" class="lbl2" ControlToValidate="tbLName" Width="100%"
                                        ErrorMessage="Last Name is Required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>City:
                                </td>
                                <td>
                                    <asp:TextBox ID="tbCity" class="textbox" runat="server" SkinID="TextBoxSkin" TabIndex="6"></asp:TextBox>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" class="lbl2" ControlToValidate="tbCity" Width="100%"
                                        ErrorMessage="City is Required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>Country:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCountry" runat="server" SkinID="dwopDownListLongSkin" Width="210px" TabIndex="7"></asp:DropDownList>
                                    <asp:Label runat="server" class="star" Text=" * " />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" class="lbl2" ControlToValidate="ddlCountry" Width="100%"
                                        ErrorMessage="Country is Required" Display="Dynamic" SetFocusOnError="True" InitialValue="none"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </colgroup>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <%--<table width="600" style="margin-left: 20px" cellpadding="5" cellspacing="5">--%>
            <table class="tables" cellspacing="5" cellpadding="5" style="width: 500px;">
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" class="buttons" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            Width="100" TabIndex="14" CausesValidation="True" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" class="buttons" runat="server" Text="Cancel"
                            Width="100" Style="margin-left: 50px" OnClientClick="disableReqiredValidation('<%= RequiredFieldValidator1%>');"
                            OnClick="btnCancel_Click" TabIndex="15" CausesValidation="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" class="buttons" runat="server" Text="Reset"
                            Width="100" Style="margin-left: 50px" OnClientClick="disableReqiredValidation(RequiredFieldValidator1);"
                            OnClick="btnReset_Click" TabIndex="16" CausesValidation="False" />
                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" />
    </div>
</asp:Content>

