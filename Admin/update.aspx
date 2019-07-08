<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="update.aspx.cs" Inherits="Lottery.Admin.update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content8" runat="server" contentplaceholderid="cphcontent">
    <div class="contents">
    <div class="htmlcontent" style="width:700px;margin-left:12px;">
        <h1>Update LottoTry&trade; Database</h1>
        <hr />

        <br />
        
        <div>
            <p>Convert a zip file to an Excel csv file <a class="more" href="#">details</a></p>
            <p>The converted Excel CSV file will be used to parse the columns of each record and then be stored into 
            database either for Lotto649 or Lotto Max. The zip files can be downloaded from BCLC Lottery Website.</p>
            <br />
            <asp:LinkButton ID="lotto649" runat="server" PostBackUrl="https://www.bclc.com/documents/DownloadableNumbers/CSV/649.zip">
            Unzip Lotto 649</asp:LinkButton>
            &nbsp &nbsp
            <asp:LinkButton ID="lottomax" runat="server" PostBackUrl="https://www.bclc.com/documents/DownloadableNumbers/CSV/LottoMAX.zip">
            Unzip LottoMax</asp:LinkButton>
            &nbsp &nbsp
            <asp:LinkButton ID="bc49" runat="server" PostBackUrl="https://www.bclc.com/documents/DownloadableNumbers/CSV/BC49.zip">
            Unzip BC49</asp:LinkButton>

          
            <br />
            <br />
            <div>
                <p>Upload Excel file and update Database <a class="more2" href="#">details</a></p>
                <p>Click the button below will lead to the page where the converted CSV file will be browsered and parsed then 
                stored into corresponding database.</p>
                <br />
            </div>


        
            <div>
            <asp:Button ID="uploadBtn" class="buttonHover" runat="server" onclick="uploadBtn_Click" 
                    Text="Upload" SkinID="buttonSkin"/>

                
                <asp:FileUpload ID="FileUpload1" runat="server" Width="250px"  style="margin-top: 24px" Visible="false" />
                &nbsp
                <asp:Button ID="UploadButton" class="buttonHover" runat="server" OnClick="UploadButton_Click" 
                    Text="Update Database"  SkinID="buttonSkin"  Width="150px"  Visible="false" /><br />
            </div>
            
            <br />
            <p>Retrieve past draws from Database <a class="more3" href="#">details</a></p>
            <p>Retrieve a range of draws start from a specified <em>draw number</em>.
            If no <em>draw number</em> provided, server will return a current draw.</p>

            <br />

            <%--AJAX--%>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        
            <table id="tblRetrieve" class="tblUserInput" runat="server" cellpadding="4" cellspacing="4">
            <tr>
                <td><label class="lbl" for="DBDl3">Select Lotteries: </label></td>
                <td>
                    <asp:DropDownList ID="DBDl3"  runat="server" SkinID="dwopDownListLongSkin">
                    </asp:DropDownList>
                 </td>
                <td><label class="lbl" for="OpDdl2">Range Selection: </label></td>
                <td>
                    <asp:DropDownList ID="OpDdl2" runat="server" SkinID="dwopDownListSkin">
                        <asp:ListItem Selected="True" Value="0">=</asp:ListItem>
                        <asp:ListItem Value="1">&gt;=</asp:ListItem>
                        <asp:ListItem Value="2">&lt;=</asp:ListItem>
                        <asp:ListItem Value="3">&gt;</asp:ListItem>
                        <asp:ListItem Value="4">&lt;</asp:ListItem>
                        </asp:DropDownList>

                </td>
                <td><label class="lbl" for="tbDN3">Draw Number: </label></td>
                <td><asp:TextBox ID="tbDN3" class="textbox" 
                    runat="server" SkinID="ShorterTextBoxSkin"></asp:TextBox> 
                </td>
                <td>
                    <asp:Button ID="submit3" class="buttonHover" runat="server" Text="Submit"  
                    OnClick="submit3_Click" SkinID="buttonSkin" />
                </td>
            </tr>
            </table>
            
            
            <hr />

            <div id="result_table">
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            </div>

            </ContentTemplate>
            </asp:UpdatePanel>


        </div>
        </div>
            
                        

    </div>

</asp:Content>

<asp:Content ID="Content9" runat="server" contentplaceholderid="ErrorLabel">
    <div id="LblError">
    <asp:Label ID="lblError" runat="server" Text="" style="color:Red;font-weight:bold;" />
    </div>
</asp:Content>



