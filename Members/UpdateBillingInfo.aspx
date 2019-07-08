<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UpdateBillingInfo.aspx.cs" Inherits="Lottotry.UpdateBillingInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server" >
    
    <div class="contents">
      <div class="htmlcontent">
      <h1>Update Billing Info</h1>
      <hr />

      <div style="font-size:small;"><asp:Label class="star" runat="server" Text="*" />Indicates required field</div>
      <br />
      <table cellpadding="0" cellspacing="0" width="500" >
        

            <col width="50%" />
            <col width="50%" />


            <tr>
            <td>User ID:</td>
            <td>
                <asp:Label ID="lblUsername" runat="server" Text=""  ForeColor="Red"></asp:Label>
            </td>
            </tr>
                        
            <tr><td colspan="2" style="width:500px;">
                &nbsp;
            </td>
            </tr>
            <tr>
            <td>Email:</td>
            <td>
                <asp:TextBox ID="tbEmail" class="textbox" runat="server" Text="" 
                    SkinID="TextBoxSkin" TabIndex="4"></asp:TextBox><asp:Label class="star" runat="server" Text="*" /></td>
            </tr>

            <tr><td colspan="2" style="width:500px;">
                &nbsp;
            </td>
            </tr>
            <tr>
            <td class="buttons" colspan="2">
                <asp:Button ID="btnSubmit" class="buttons" runat="server" Text="Update" 
                    onclick="btnSubmit_Click" SkinID="buttonSkin" TabIndex="12" /></td>
            </tr>        
        
            <tr><td colspan="2" style="width:500px;">&nbsp;</td>
            </tr>
            <tr><td colspan="2">
                <div class="indicator"><asp:Label id="lblIndicator"   runat="server" Text="" ></asp:Label></div></td></tr>
        </table>

        </div>
    </div>

</asp:Content>










