<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="clientStatus.aspx.cs" Inherits="Lottotry.Admin.clientStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="docs">

        <h1>LottoTry&trade; Membership Status</h1>

        <br />
        <div id="userInfo1">
            <h2>Clients Less One Month to Expire</h2>
            <asp:GridView ID="clientCloseExpire" runat="server" SkinID="GridViewSkin" AutoGenerateColumns="False"
                OnRowCommand="clientCloseExpire_RowCommand" CellPadding="4" Width="100%"
                ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="UserName" />
                    <asp:BoundField DataField="Client Name" HeaderText="Client Name"
                        ItemStyle-Width="150">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Expiry Date" HeaderText="Expiry Date" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LBSelect" class="gridViewButtons" runat="server" CommandArgument='<%# Eval("UserName")+"," + Eval("Client Name")+"," + Eval("Email") +"," +Eval("Expiry Date")%>' CommandName="SELECT">Send Email</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <br />
        <br />




        <div id="userInfo2">
            <h2>Expired Clients</h2>
            <asp:GridView ID="expiredClients" runat="server" SkinID="GridViewSkin" AutoGenerateColumns="False"
                OnRowCommand="expiredClients_RowCommand" OnRowDeleting="expiredClients_RowDeleting"
                PagerSettings-Mode="NextPreviousFirstLast" CellPadding="4" ForeColor="#333333"
                GridLines="None" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="UserName" HeaderText="UserName" />
                    <asp:BoundField DataField="Client Name" HeaderText="Client Name"
                        ItemStyle-Width="150">
                        <ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Membership" HeaderText="Membership" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LBSelect" class="gridViewButtons" runat="server" CommandArgument='<%# Eval("UserName")+"," + Eval("Client Name")+"," + Eval("Email") +"," +Eval("Membership")%>' CommandName="SELECT">Send Email</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LBDelete" class="gridViewButtons" runat="server"
                                CommandName="DELETE" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this client from database?');"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            </div>
        <%--<div id="DeleteButton">
            <br />
            <asp:Button ID="btnDeleteAll" class="buttons" runat="server" Text="Delete All Expired?"
                OnClientClick="return confirm('Are you sure you want to delete all expired clients?')"
                OnClick="btnDeleteAll_Click" SkinID="buttonSkin" Width="180" />
        </div>--%>
    </div>

</asp:Content>

<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;" />
    </div>
</asp:Content>



<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="cphleftsidebar">
</asp:Content>
<asp:Content ID="Content9" runat="server" ContentPlaceHolderID="cphrightsidebar">
</asp:Content>




