<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SiteMap.aspx.cs" Inherits="Lottotry.SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <h1>LottoTry.com Site Map</h1>
            <hr />

            <%--        <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1" SkinID="MenuSkin">
        </asp:Menu>
            --%>
            <asp:SiteMapPath ID="SiteMapPath1" runat="server" OnItemCreated="SiteMapPath1_ItemCreated">
            </asp:SiteMapPath>

            <asp:TreeView ID="TreeView1" runat="server" SkinID="TreeViewSkin"
                DataSourceID="SiteMapDataSource1">
            </asp:TreeView>

            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server"
                ViewStateMode="Enabled" />
        </div>
    </div>
</asp:Content>




