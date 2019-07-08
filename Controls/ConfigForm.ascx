<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ConfigForm.ascx.cs" Inherits="Lottotry.Controls.ConfigForm" %>
<script type="text/javascript">

    function highLightBackColor() {
        $('.textbox').mouseover(function () {
            $(this).css('background-color', 'yellow');
        })
		    .mouseout(function () {
		        $(this).css('background-color', '#fff');
		    })
    };
</script>
<table id="tblGen" runat="server" title="Configuration" cellpadding="5" cellspacing="3"
                                width="90%" alignment="center" >
                                <tr>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbHot">
                                            Select Numbers for HOT:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbHot" class="textbox" runat="server" onclick="highLightBackColor();"
                                            Text="20" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbHotMin">
                                            HOT Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbHotMin" class="textbox" runat="server" Text="1" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbHotMax">
                                            HOT Max:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbHotMax" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSumMin">
                                            Sum Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSumMin" class="textbox" runat="server" Text="125" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiHot">
                                            Select Numbers for Semi HOT:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiHot" class="textbox" runat="server" Text="15" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiHotMin">
                                            Semi HOT Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiHotMin" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiHotMax">
                                            Semi HOT Max:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiHotMax" class="textbox" runat="server" Text="9" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSumMax">
                                            Sum Max:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSumMax" class="textbox" runat="server" Text="175" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiCold">
                                            Select Numbers for Semi COLD:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiCold" class="textbox" runat="server" Text="10" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiColdMin">
                                            Semi COLD Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiColdMin" class="textbox" runat="server" Text="9" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbSemiColdMax">
                                            Semi COLD Max:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbSemiColdMax" class="textbox" runat="server" Text="15" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbOdds">
                                            Number of Odds:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbOdds" class="textbox" runat="server" Text="3" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbCold">
                                            Select Numbers for COLD:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbCold" class="textbox" runat="server" Text="10" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbColdMin">
                                            COLD Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbColdMin" class="textbox" runat="server" Text="15" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbColdMax">
                                            COLD Max:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbColdMax" class="textbox" runat="server" Text="20" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbVeryCold">
                                            Select Numbers for VERY COLD:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbVeryCold" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbVeryColdMin">
                                            VERY COLD Min:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbVeryColdMin" class="textbox" runat="server" Text="20" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                    <td style="width: 230px; text-align: right;">
                                        <label class="lbl" for="tbVeryHot">
                                            Select Numbers for VERY HOT:
                                        </label>
                                    </td>
                                    <td style="width: 200px; text-align: left;">
                                        <asp:TextBox ID="tbVeryHot" class="textbox" runat="server" Text="6" SkinID="ShorterTextBoxSkin"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>