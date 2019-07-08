<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LottoDbStatus.aspx.cs" Inherits="Lottotry.Admin.LottoDbStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contents">
                <div class="refresh">
                    <asp:Button ID="btnRefresh" SkinID="buttonSkin" runat="server" Text="Refresh" OnClick="btnRefresh_Click" /></div>
                <div class="gridviews">

                    <asp:GridView ID="gvLottoStatus" SkinID="GridViewSkin" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="720px"
                        AutoGenerateColumns="False" AllowSorting="True" HorizontalAlign="Left" Font-Size="Small" DataSourceID="SqlDataSource1" OnRowCommand="gvLottoStatus_RowCommand">                       
                        <Columns>

                            <asp:TemplateField HeaderText="No.">                               
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>.
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LottoName" HeaderText="Lotto Name" SortExpression="LottoName" HeaderStyle-Wrap="false" >
                                <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DrawDate" HeaderText="Draw Date" SortExpression="DrawDate" HeaderStyle-Wrap="false">
                                <HeaderStyle  Wrap="False" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Action" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" SkinID="smallerButtonSkin" runat="server" Text="Update Now" CommandName="UpdateNow" Font-Size="Smaller"
                                        Font-Bold="true"
                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" />
                                </ItemTemplate>
                                <HeaderStyle Wrap="False" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" HorizontalAlign="Right" Font-Size="Smaller" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        
                    </asp:GridView>
                </div>
            </div>

            <div id="LblError">
                <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRefresh" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:lottotryConnectionString2 %>" 
        SelectCommand="spLottoUpdateStatus" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

</asp:Content>





