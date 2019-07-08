<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReceiptRecord.aspx.cs" Inherits="Lottotry.Members.ReceiptRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <h1>Receipt Record for 
        <asp:Label ID="lblFName" runat="server" Text="" Style="color: #FCDFFF; font-style: italic; font-size: 100%;"></asp:Label></h1>
            <hr />
            <asp:Repeater ID="rptReceiptRecord" runat="server">
                <ItemTemplate>
                    <div class="ReceiptRecord">

                        <table style="color: Maroon; margin-left: 20px" width="500">
                            <tr>
                                <td>Full Name:</td>
                                <td><%# Eval("FullName") %></td>
                            </tr>
                            <tr>
                                <td>Member Plan:</td>
                                <td><%# Eval("MemberPlan") %></td>
                            </tr>
                            <tr>
                                <td>Start Date:</td>
                                <td><%# Eval("StartDate") %></td>
                            </tr>
                            <tr>
                                <td>Expired Date:</td>
                                <td><%# Eval("ExpiredDate") %></td>
                            </tr>
                            <tr>
                                <td>Company Name:</td>
                                <td>Soft Solution</td>
                            </tr>
                            <tr>
                                <td>Company Website:</td>
                                <td>www.lottotry.com</td>
                            </tr>

                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>


