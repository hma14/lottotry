<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="editAboutPage.aspx.cs" Inherits="Lottotry.Admin.editAboutPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content9" runat="server" contentplaceholderid="cphcontent">
    <div class="contents">
    <div class="htmlcontent">
    <h1>Edit LottoTry&trade; About Page</h1>
    <hr />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="upEditor" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="lbtnBold" runat="server" CssClass="linkButton" 
                onclick="lbtnBold_Click" OnClientClick="GetSelection();">Bold</asp:LinkButton>
            <asp:LinkButton ID="lbtnUnderline" runat="server" CssClass="linkButton"
             onclick="lbtnUnderline_Click" OnClientClick="GetSelection();">
            Underline</asp:LinkButton>    
            <asp:LinkButton ID="lbtnItalics" runat="server" CssClass="linkButton"
             onclick="lbtnItalics_Click" OnClientClick="GetSelection();">
            Italics</asp:LinkButton>    
            <asp:LinkButton ID="lbtnPreview" runat="server" CssClass="linkButton"
             onclick="lbtnPreview_Click">
            Preview</asp:LinkButton>    
            <asp:LinkButton ID="lbtnHyperLink" runat="server" onclick="LinkButton1_Click">Hyperlink</asp:LinkButton>
            <asp:LinkButton ID="lblPublish" runat="server" onclick="lblPublish_Click">Publish</asp:LinkButton>
            
            <asp:Label CssClass="basicLabel" ID="lblLinkTargetURL" runat="server" Text="Link Target URL" Visible="false"></asp:Label>
            
            <asp:TextBox ID="txtLinkTargetURL" class="textbox" runat="server" Visible="false"></asp:TextBox>
            
            <asp:Label CssClass="basicLabel" ID="lblLinkText" runat="server" Text="Link Text" Visible="false"></asp:Label>
           
            <asp:TextBox ID="txtLinkText" class="textbox" runat="server" Visible="false"></asp:TextBox>
            
            <asp:LinkButton ID="lbtnCreateLink" runat="server" CssClass="basicLabel"
                onclick="lbtnCreateLink_Click" Visible="false">Create Link</asp:LinkButton>
           <br />
            Page Content<br />
            <asp:TextBox ID="txtTitle" class="textbox"  TextMode="MultiLine" runat="server"  SkinID="TextBoxSkin"
                ontextchanged="txtTitle_TextChanged" Width="650" Height="400">
<%--                LottoTry&trade; is a unique web based Lottery software tool,which means you don't have to 
                download and then install it in your computer, as most of Lotto software providers do, 
                and run it only on that computer. LottoTry&trade;.com can be accessed from any computers as long
                as it is connected with Internet. Therefore, it also can be accessed via iPhone, BlackBerry, etc.
                 
                LottoTry&trade; provides statistics for those dedicated Lottery players to predict next draw and help them narrow down the scope of 
                selectable numbers which may hit next draw and filter out those 'dead' numbers.

                LottoTry&trade; also provides tools to auto generating numbers for the users as a valuable reference which much better than 
                quick-pick by Lotto peddler in a corner of a Mall or a 7-Eleven.

                Once you master the tool, you will find that to win a grand price of a lotto is not a dream any more.
--%>
                </asp:TextBox>
            
                <br />
                <asp:Label ID="lblLiveText" runat="server" CssClass="basicLabel">
                
                </asp:Label>
            <asp:HiddenField runat="server" ID="hfSelected" />
            <script type="text/javascript">
                function GetSelection() {
                    var txt = '';
                    // IE
                    if (window.getSelection) {
                        txt = window.getSelection();
                    }
                    else if (document.getSelection) {
                        txt = document.getSelection();
                    }
                    else if (document.selection) {
                        txt = document.selection.createRange().text;
                    }
                    else return;
                    //FF
                    var $obj = document.getElementById("ctl00_Content_txtTitle")
                    document.getElementById("ctl00_Content_hfSelected").value
                    = $obj.value.substring($obj.selectionStart, $obj.selectionEnd);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

    </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content10" runat="server" contentplaceholderid="ErrorLabel">
    <div id="LblError">
    <asp:Label ID="lblError" runat="server" Text="" ></asp:Label>
    </div>
</asp:Content>

