<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="about.aspx.cs" Inherits="Lottotry.about" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
<%--        <div id="profilePicture">
            <asp:Image ID="logo" runat="server" ImageUrl="~/images/LottoTryLogo2.png" />
        </div>
--%>    
        <%--<asp:Panel id="aboutContent"  runat="server" CssClass="basicLabel">--%>
        <div class="htmlcontent">
            <h1>About LottoTry&trade;</h1>
            <hr />
            <br />
            LottoTry&trade; is a unique web based Lotto statistics, analysis tools.
            With LottoTry&trade;, you can play multiple lotteries as following:
            <br />
            <asp:Label ID="lblLottoList" runat="server" CssClass="LottoList" Text=""></asp:Label>
            <br />
            LottoTry&trade; provides statistics, analysis tools for those dedicated, serious Lotto players to analyze, predict, guess next comming draw 
            and narrow down the scope of selectable numbers and filter out those 'dead' numbers. 
            <br />
            LottoTry&trade; also provides tools to auto-generate numbers for the users as a valuable reference which much better than 
            quick-pick by lotto retailers. It enables players to re-generate numbers freely and repeatedly until they are satisfied.
            <br />
            Though, LottoTry&trade; never promise or guarantee its members to win any prices, however, LottoTry&trade; does 
            improve its members' chances in winning any breakdown prices even jackpot.<br />
            Once you master the tools and thoroughly analysis those statistics, you will find out that to win the 
            jackpot of a lottery is not a dream any more.
            <br />
        </div>
        <%--</asp:Panel>--%>
    

    </div>
</asp:Content>

<asp:Content ID="Content5" runat="server" contentplaceholderid="ErrorLabel">
            <div id="LblError">
            <asp:Label ID="lblError" runat="server" Text="" ></asp:Label>
            </div>
        </asp:Content>


