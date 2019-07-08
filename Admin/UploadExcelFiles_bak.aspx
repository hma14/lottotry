<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UploadExcelFiles_bak.aspx.cs" Inherits="Lottery.UploadExcelFiles" %>
<asp:Content ID="Content4" ContentPlaceHolderID="cphcontent" runat="server">
    <h1>Upload cvs file and update database page</h1>
    <br />
    <hr />
    <div id="content"> 
        <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" 
            style="margin-top: 24px" />
            
        <asp:Button ID="UploadButton" runat="server" OnClick="UploadButton_Click" 
            Text="Update Database"  SkinID="buttonSkin" 
            Width="150px" /><br />

        <br />
        <div id="lbl">
            <asp:Label ID="Lbl1" runat="server" AssociatedControlID="FileUpload1"></asp:Label>&nbsp;<br />
        </div>

    </div>
    
    <br />
    <div id="dbexetime">
        <asp:Label ID="Lbl2" runat="server"></asp:Label>&nbsp;<br />
    </div>

</asp:Content>

