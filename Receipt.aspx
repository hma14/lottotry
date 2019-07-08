<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="Lottotry.Receipt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">

    <div class="contents">
    <div class="htmlcontent">
    <h1>Receipt</h1>
    <hr />
    <br />
    <div class="ReceiptRecord">
    <table cellpadding="2", cellspacing="3" style="color:Maroon;margin-left:20px" width="500" >
   
        <col width="50%" />
        <col width="50%" />
        <tr><td>User ID:</td><td>
            <asp:Label ID="lblUid"  class="clsReceipt" runat="server" Text=""></asp:Label></td></tr>
<%--        <tr><td>Transaction ID:</td><td>
            <asp:Label ID="lblTransID"  class="clsReceipt" runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Credit Card:</td><td>
            <asp:Label ID="lblCCT"  class="clsReceipt" runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Card Number:</td><td>
            <asp:Label ID="lblCCN"  class="clsReceipt" runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Card Expiry Date:</td><td>
            <asp:Label ID="lblExpiryDate"  class="clsReceipt" runat="server" Text=""></asp:Label></td></tr>--%>
        <tr><td>Full Name:</td><td>
            <asp:Label ID="lblFullName"   class="clsReceipt"  runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Member Plan:</td><td>
            <asp:Label ID="lblPlan"  class="clsReceipt"   runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Start Date:</td><td>
            <asp:Label ID="lblStartDate"  class="clsReceipt"  runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Expired Date:</td><td>
            <asp:Label ID="lblExpiredDate"   class="clsReceipt"  runat="server" Text=""></asp:Label></td></tr>
        <tr><td>Company Name: </td><td>
            <asp:Label ID="Label1"   class="clsReceipt"  runat="server" Text="Soft Solution"></asp:Label></td></tr>
        <tr><td>Company Website: </td><td>
            <asp:Label ID="Label2"   class="clsReceipt"  runat="server" Text="www.lottotry.com"></asp:Label></td></tr>
        
    </table>
    </div>
        <br />
        <br />
        
        <div>
            <asp:Label ID="lblThankyou" class="lbl2" runat="server" Text="" style="margin-left:20px;"></asp:Label>
        </div>
       
    </div>
    </div>

</asp:Content>





