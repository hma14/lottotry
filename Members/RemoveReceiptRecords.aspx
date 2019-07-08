<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RemoveReceiptRecords.aspx.cs" Inherits="Lottotry.Members.RemoveReceiptRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content10" runat="server" contentplaceholderid="cphcontent">
    <div class="contents">
    <div class="htmlcontent">
       
            <h1>Remove Receipt Records</h1>
            <hr />
            <br />
         <div class="htmlsubcontent">
            <div id="lblReceiptRecords">
                <asp:Label ID="Label1" class="basicLabel" runat="server" Text="Select a <em>Transaction ID</em> to remove a receipt record or to remove all:"></asp:Label><br />
                <br />
                <asp:DropDownList ID="ddlReceiptRecords" runat="server" 
                    SkinID="dwopDownListLongSkin"
                    onselectedindexchanged="ddlReceiptRecords_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
        <div class="htmlsubcontent2"> 
            <p>Note: You are solely responsible to remove any receipt records you own. 
            The removed receipt records will be permanently lost in database!</p>
        </div>
    </div>
    </div>
</asp:Content>

<asp:Content ID="Content9" runat="server" contentplaceholderid="ErrorLabel">
    <div id="LblError">
    <asp:Label ID="lblError" runat="server" Text="" style="color:#FAFAFA;font-weight:bold;" />
    </div>
</asp:Content>
