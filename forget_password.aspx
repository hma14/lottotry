<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="forget_password.aspx.cs" Inherits="Lottotry.forget_password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">    
    <div class="htmlcontent">
        <h3>Forget Password</h3>
        <hr />
        <br />
        <p>Please provide your email address and we will send you a new password.</p>
        
       
            <br />
            <table cellpadding="5" cellspacing="5" width="500" style="margin-left:5px;">
            <col width="35%"/>
            <col width="35%"/>
            <col width="30%"/>
            <tr><td>Enter Email Address: </td>
            <td>
                <asp:TextBox ID="tbEmail" class="textbox" runat="server" SkinID="TextBoxSkin"></asp:TextBox></td>
            <td>
                <asp:Button ID="tbSubmit" class="buttons" runat="server" Text="Submit" 
                    SkinID="buttonSkin" onclick="tbSubmit_Click" /></td></tr>

            
            <tr><td colspan="3" >
                <div class="indicator" ><asp:Label id="lblIndicator"  runat="server" Text="" ></asp:Label></div></td></tr>

            </table>

        <br />
        <div style="font-size:small;margin-left:5px; margin-top:280px;">
            Not a Member? Click <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Register.aspx">join</asp:HyperLink>
        </div>
        </div>
        
        
     </div> 
    
 
</asp:Content>


<asp:Content ID="Content5" runat="server" contentplaceholderid="ErrorLabel">
    <div id="LblError">
    <asp:Label ID="lblError" runat="server" Text="" style="color:Red;font-weight:bold;" />
    </div>
</asp:Content>





