<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="emailToAll.aspx.cs" Inherits="Lottotry.Admin.emailToAll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            jqueryIndicator();
        });

        function jqueryIndicator() {

            $('.default-value').each(function () {
                var default_value = this.value;
                $(this).css('color', '#666'); // this could be in the style sheet instead
                $(this).focus(function () {
                    if (this.value == default_value) {
                        this.value = '';
                        $(this).css('color', '#333');
                    }
                });
                $(this).blur(function () {
                    if (this.value == '') {
                        $(this).css('color', '#666');
                        this.value = default_value;
                    }
                });
            });
        }

    </script>



</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cphcontent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <%--<Scripts>
            <asp:ScriptReference Path="/jquery.js" />
        </Scripts>--%>
    </asp:ScriptManager>

    <div class="docs">

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>


                <h1>Email to All LottoTry&trade; Members</h1>
                <hr />
                <br />
                <asp:Button ID="btShowMember" class="buttons" runat="server" Text="Show Members"
                    OnClick="btShowMember_Click" SkinID="buttonSkin" Width="150" />
                <asp:Button ID="btEmail" class="buttons" runat="server" Text="Open Editor" Width="150"
                    OnClick="btEmail_Click" SkinID="buttonSkin" />
                <br />
                <asp:TextBox ID="tbSubject" class="default-value" runat="server" Text="Type your email subject here"
                    Visible="false" Width="650px" Height="30px" SkinID="TextBoxSkin"></asp:TextBox>



                <br />
                <asp:TextBox ID="tbEmailContent" class="default-value" runat="server" Width="100%" Height="300px"
                    TextMode="MultiLine" Text="Input email content here"
                    Visible="false" SkinID="TextBoxSkin"></asp:TextBox>
                <br />

                
                <asp:Button ID="btSendEmail" class="buttons" runat="server" Text="Send" Visible="false"
                    OnClick="btSendEmail_Click" SkinID="buttonSkin" />
                <asp:Button ID="btCancel" class="buttons" runat="server" Text="Cancel" Visible="false"
                    OnClick="btCancel_Click" SkinID="buttonSkin" />
                <br />
                <div class="indicator">
                    <asp:Label ID="lblIndicator" runat="server" Text=""></asp:Label>
                </div>
                <br />
                <asp:Label ID="lblOrderBy" runat="server" Visible="false" Text="Order By:" Style="color: Maroon"></asp:Label>
                <asp:DropDownList ID="ddlOrderBy" runat="server" SkinID="dwopDownListSkin"
                    Width="140" AutoPostBack="True" Visible="false"
                    OnSelectedIndexChanged="ddlOrderBy_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <div class="userInfo">
                    <asp:GridView ID="GridView1" runat="server" SkinID="GridViewSkin" Width="100%"
                        GridLines="None" AllowPaging="True" OnRowCommand="GridView1_RowCommand"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                        PageSize="10"
                        PagerSettings-Mode="NumericFirstLast"
                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last">

                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" class="gridViewButtons" runat="server"
                                        CommandArgument='<%# Eval("firstName")+"," + Eval("lastname") +"," +Eval("email")%>'
                                        CommandName="SELECT">SELECT</asp:LinkButton>
                                </ItemTemplate>
                                <%--<ItemStyle Height="50px" HorizontalAlign="Center" Width="150px" />--%>
                            </asp:TemplateField>
                            <asp:BoundField DataField="firstName" HeaderText="First Name">
                                <HeaderStyle Width="120px" />
                                <%--<ItemStyle Width="120px" Height="10px" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="lastName" HeaderText="Last Name">
                                <HeaderStyle Width="120px" />
                                <%--<ItemStyle Width="120px" Height="20px" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="email" HeaderText="Email">
                                <HeaderStyle Width="120px" />
                                <%--<ItemStyle Width="120px" Height="20px" />--%>
                            </asp:BoundField>
                            <asp:BoundField DataField="isLoggedIn" HeaderText="Is Logged In">
                                <HeaderStyle Width="120px" />
                                <%--<ItemStyle Width="120px" Height="20px" />--%>
                            </asp:BoundField>

                        </Columns>



                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#77BFC7" Font-Bold="True" ForeColor="White" />
                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next"
                            PreviousPageText="Prev" />
                        <PagerStyle BackColor="#77BFC7" ForeColor="White" HorizontalAlign="Left" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />



                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>
</asp:Content>


<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="cphleftsidebar">
</asp:Content>
<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="cphrightsidebar">
</asp:Content>








