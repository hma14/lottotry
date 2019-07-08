<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="uploadDataFile.aspx.cs" Inherits="Lottery.Admin.uploadDataFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="contents">
        <h3>Upload Data File and Update Database</h3>
        
        <hr />
        <div> 
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
    </div>
</asp:Content>

