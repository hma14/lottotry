<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="Lottotry.Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <div class="htmlcontent">
            <h1 style="text-align:center;">Important Notice</h1>
            <p>
                This site is not a public site anymore. That means there will be no monthly free membership anymore. 
                Since LottoTry has been moved to and hosted on Azure, the costs to keep her alive have become unaffordable to the owner. 
                Therefore, we decide to close it to the public. <br />
                However, to those dedicated users we will create a one-year membership with cost of <i>US$120</i> as donation to the LottoTry website. 
                During this one-year period, members will be gueranteed to access LottoTry fully.<br /> 
                Once one-year membership expired, they can choose to renew their memberships for another year.<br />
                If you think you're one of the dedicated LottoTry users, please send us email to <a href="mailto:info@lottotry.com" style="color:springgreen;text-decoration:none;font-size:1em;">info@lottotry.com</a>, 
                so that we'll process your membership immidiately.<br />
                Your donation will be critical to the survive of the LottoTry website.<br /><br />
                
            

                Best Regards,<br />
                LottoTry Team 
            </p>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" />
    </div>
</asp:Content>

