<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="signupContinue.aspx.cs" Inherits="Lottery.signupContinue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">

    <div class="contents">
    <div class="htmlcontent">
    <h1>Payment Information</h1>
    <hr />
   
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <p style="font-size:small;"><asp:Label ID="Label1" class="star" runat="server" Text="*"></asp:Label>Indicates required field</p>
     <br />
    <table cellpadding="0" cellspacing="0" width="650" style="margin-left:10px" >
        
            <col width="35%" />
            <col width="65%" />

            <tr>
                <td>
                    Credit Card:</td>
                <td>
                    <asp:DropDownList ID="ddlCreditCardType" runat="server" 
                        SkinID="dwopDownListSkin" TabIndex="1" Width="150">
                        <asp:ListItem Selected="True">VISA</asp:ListItem>
                        <asp:ListItem>MasterCard</asp:ListItem>
                        <asp:ListItem>AMEX</asp:ListItem>
                    </asp:DropDownList>
                    <img class="credit_cards_image" src="/images/credit_cards.gif" alt="Credit Cards" height="30" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="width:500px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                            ControlToValidate="tbCreditCardNumber" 
                            ErrorMessage="Credit Card Number must be 16-digit" 
                            ValidationExpression="\d{16}" Width="250px"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="tbCreditCardNumber" ErrorMessage="Required Field" 
                            Width="114px"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Card Number:</td>
                <td>
                    <asp:TextBox ID="tbCreditCardNumber" runat="server" SkinID="TextBoxSkin" 
                        TabIndex="2" Text="4322084004629157" />
                    <asp:Label ID="Label2" class="star" runat="server" Text="*" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="width:600px">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="tbMM" ErrorMessage="Invalid format!" 
                            ValidationExpression="\d{1,2}" Width="86px"></asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="tbYY" ErrorMessage="4-digit" ValidationExpression="\d{4}" 
                            Width="37px"></asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                            ControlToValidate="tbYY" ErrorMessage="Expired year must be in future." 
                            Operator="GreaterThan" SetFocusOnError="True" ValueToCompare="2010" 
                            Width="186px"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="tbMM" ErrorMessage="Required Field" Width="86px"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="tbYY" ErrorMessage="Required Field" Width="87px"></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Expiration Date (MM/YYYY):</td>
                <td>
                    <div>
                        <asp:TextBox ID="tbMM" runat="server" SkinID="ShorterTextBoxSkin" TabIndex="3" 
                            Text="03"></asp:TextBox>
                        /<asp:TextBox ID="tbYY" runat="server" SkinID="ShorterTextBoxSkin" 
                            TabIndex="4" Text="15"></asp:TextBox>
                        <asp:Label class="star" runat="server" Text=" * " />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="width:500px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="tbCVD" ErrorMessage="Required Field" Height="16px" 
                            Width="100px"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="tbCVD" EnableClientScript="False" 
                            ErrorMessage="Security code must be 3-digit" ValidationExpression="^(\d{3})" 
                            Width="177px"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Security Code:</td>
                <td>
                    <div>
                        <asp:TextBox ID="tbCVD" runat="server" SkinID="ShorterTextBoxSkin" 
                            TabIndex="5" Text="123"></asp:TextBox>
                        <asp:Label ID="Label4" class="star" runat="server" Text=" * " />
                        <img id="Img1" class="credit_cards_image"  runat="server" src="/images/credit-security-code.gif" alt="Credit Card Security Code" />
                        <input type="button"  value="?" onclick="jAlert('3-digit security code on the back of the card', 'Credit Card Security Code');" 
                        style="background-color:transparent;color:Red;font-weight:bolder;"/> 
                        
                    </div>
             
                </td>
            </tr>
            <tr>
                <td colspan="2"> &nbsp;</td>
            </tr>

           

            <tr>
                <td>
                    Choose a Plan <input type="button" value="?" 
                    onclick="jAlert('1 month for <em>$4.99</em>\n\n6-month for <em>$24.99</em> (regular price: <strike>$29.99</strike>), saving 1 month\n\n12-month for <em>$44.99</em> (regular price: <strike>$59.99</strike>), saving 3 months', 'Membership Plans:');" 
                    style="background-color:transparent;color:Red;font-weight:bolder;"/></td>
                <td>
                    <%--AJAX--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                    <asp:DropDownList ID="ddlPlans" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlPlans_SelectedIndexChanged" Width="150px" SkinID="dwopDownListSkin" >
                    </asp:DropDownList>
                    <asp:Label class="star" runat="server" Text="*" />
                    <asp:CheckBox ID="chkAutoBill" runat="server" checked="false" /><label class="lbl2" for="chkAutoBill">&nbsp;Auto Bill</label>

                    </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Payment Amount (C$):</td>
                <td width="500px">
                    <%--AJAX--%>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                   
                    <asp:TextBox ID="tbPayment" runat="server"  ReadOnly="true" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                    <asp:Label ID="lblSavePlan" class="lbl2" runat="server" Text="" />

                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                        SkinID="buttonSkin" TabIndex="9" Text="Reset" />
                </td>
                <td>
                    <div style="float:left">
                        <asp:Button ID="tbnBack" runat="server" onclick="btnBack_Click" 
                            SkinID="buttonSkin" TabIndex="8" Text="Back" />
                    </div>
                    <div style="float:right;margin-right:70px;">
                        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                            SkinID="buttonSkin" TabIndex="7" Text="Submit" />
                    </div>
                </td>
            </tr>
        
    </table>

    
 
    <br />
   
    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblResult0" runat="server" Text=""></asp:Label>

    </div>
    </div>
</asp:Content>

