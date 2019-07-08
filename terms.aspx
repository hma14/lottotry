<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="terms.aspx.cs" Inherits="Lottotry.terms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">

<div class="docs" >

<%--<iframe id="pdf" runat="server" src=""  frameborder="0" marginwidth="0"  > 

</iframe>--%>
<%--<object  id="pdf" data="/Doc/Terms_and_Conditions.pdf" type="application/pdf" width="830" height="500">--%>
<object class="pdf">
  <param name="allowScriptAccess" value="sameDomain"></param>
  <param name="wmode" value="transparent"></param>
 
  <embed wmode="transparent" src="/Doc/Terms_and_Conditions.pdf" type="application/pdf" width="830" height="500"  ></embed>
</object>
</div>
</asp:Content>

