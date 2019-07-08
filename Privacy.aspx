<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Privacy.aspx.cs" Inherits="Lottotry.Privacy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
<div class="docs" >


<object class="pdf">
  <param name="allowScriptAccess" value="sameDomain"></param>
  <param name="wmode" value="transparent"></param>
 
  <embed wmode="transparent" src="/Doc/Privacy_Policy.pdf" type="application/pdf" width="830" height="500"  ></embed>
</object>
</div>
</asp:Content>

