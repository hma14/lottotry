<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Update_bak.aspx.cs" Inherits="Lottery.Update" %>
<asp:Content ID="Content4" ContentPlaceHolderID="cphcontent" runat="server">

    <h1>Update Database</h1>
    <hr />
    <br />

    <div id="content">
    <p>Convert a zip file to an Excel csv file <a class="more" href="#">details</a></p>
    <p>The converted Excel CSV file will be used to parse the columns of each record and then be stored into 
    database either for Lotto649 or Lotto Max. The zip files can be downloaded from BCLC Lottery Website.</p>
    <br />
    <div id="unzipfile">
        <div id="lotto649">
           <a href="https://www.bclc.com/documents/DownloadableNumbers/CSV/649.zip" >Unzip Lotto 649</a>
        </div>
        <div id="lottomax">
           <a href="https://www.bclc.com/documents/DownloadableNumbers/CSV/LottoMAX.zip" >Unzip Lotto Max</a> 
        </div>
    </div>
   <br />
   <br />
   <div style="clear:both">
       <p>Upload Excel file and update Database <a class="more2" href="#">details</a></p>
       <p>Click the button below will lead to the page where the converted CSV file will be browsered and parsed then 
       stored into corresponding database.</p>
       <br />
            <div id="uploadbutton">
               <asp:Button ID="uploadBtn" runat="server" onclick="uploadBtn_Click" 
                        Text="Upload Excel to Database" SkinID="buttonSkin" Width="200px" />
            </div>
   </div>
   <br />
   <p>Retrieve past draws from Database <a class="more3" href="#">details</a></p>
        <div id="retrive">
            <p>Retrieve a range of draws start from a specified <em>draw number</em>.
            If no <em>draw number</em> provided, server will return a current draw.</p>
            <br />
            <label class="lbl" for="DBDl3">DB Tables: </label><asp:DropDownList ID="DBDl3" 
                runat="server" 
                Style="border-top-style: ridge; border-right-style: ridge;
            border-left-style: ridge; border-collapse: separate; border-bottom-style: ridge" 
                SkinID="dwopDownListSkin">
            <asp:ListItem Selected="True" Value="0">649</asp:ListItem>
            <asp:ListItem Value="1">LottoMax</asp:ListItem></asp:DropDownList>
            <label class="lbl" for="OpDdl2">Range Selection: </label>
            <asp:DropDownList ID="OpDdl2" runat="server" SkinID="dwopDownListSkin">
            <asp:ListItem Selected="True" Value="0">=</asp:ListItem>
            <asp:ListItem Value="1">&gt;=</asp:ListItem>
            <asp:ListItem Value="2">&lt;=</asp:ListItem>
            <asp:ListItem Value="3">&gt;</asp:ListItem>
            <asp:ListItem Value="4">&lt;</asp:ListItem>
            </asp:DropDownList>
            <label class="lbl" for="tbDN3">Draw Number: </label>
            <asp:TextBox ID="tbDN3" 
                runat="server" SkinID="TextBoxSkin" ></asp:TextBox>
            <asp:Button ID="submit3" runat="server" Text="Submit" Width="90px" 
                OnClick="submit3_Click" SkinID="buttonSkin" />
    
            <input id="fromSite" runat="server" name="fromSite" style="width: 111px" type="hidden" value="Update.aspx" />
        </div>
</asp:Content>

