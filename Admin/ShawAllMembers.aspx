<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShawAllMembers.aspx.cs" Inherits="Lottotry.Admin.ShawAllMembers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">

    <asp:ScriptManager ID="scriptManager" runat="server" />

    <div class="docs">

        <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:GridView ID="GVShowMembers" SkinID="GridViewSkin" runat="server" Width="100%"
                    Font-Size="Small"
                    GridLines="None"
                    AllowPaging="True"
                    PageSize="15"
                    PagerSettings-Mode="NumericFirstLast"
                    CellPadding="4" ForeColor="#333333" AllowSorting="True"
                    DataSourceID="SqlDataSource1" AutoGenerateColumns="False">

                   

                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>.
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Full Name" HeaderText="Full Name" ReadOnly="true"
                            SortExpression="Full Name" />
                        <asp:BoundField DataField="City" HeaderText="City" ReadOnly="true"
                            SortExpression="Citye" />
                        <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="true"
                            SortExpression="Country" />


                        <asp:TemplateField HeaderText="Email" SortExpression="Email" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <a href="mailto:<%# Eval("Email") %>"><%# Eval("Email") %></a>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Role" HeaderText="Role"
                            SortExpression="Role" />
                        <asp:BoundField DataFormatString="{0:MM-dd-yyyy}" DataField="Signup Date" HeaderText="Signup Date"
                            SortExpression="SignupDate" />
                        <asp:BoundField DataFormatString="{0:MM-dd-yyyy}" DataField="Expiry Date" HeaderText="Expiry Date"
                            SortExpression="ExpiryDate">

                            <%--<ItemStyle Width="120px"  />--%>
                        </asp:BoundField>
                        <asp:BoundField DataField="IsLoggedIn" HeaderText="IsLoggedIn"
                            SortExpression="IsLoggedIn">
                            <%--<ItemStyle Width="20px" />--%>
                        </asp:BoundField>

                    </Columns>

                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next"
                        PreviousPageText="Prev" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                </asp:GridView>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                       
                    ConnectionString="<%$ ConnectionStrings:lottotryConnectionString2 %>"
                    SelectCommand="spViewAlltblUsers"
                    SelectCommandType="StoredProcedure"></asp:SqlDataSource>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


</asp:Content>

<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="cphleftsidebar">
</asp:Content>
<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="cphrightsidebar">
</asp:Content>


