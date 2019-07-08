<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoDrawConfig.ascx.cs"
	Inherits="Lottotry.Controls.AutoDrawConfig" %>
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
<table id="tblAutoDraw" runat="server" title="Configuration" cellpadding="5" cellspacing="3"
	width="100%" alignment="center" >
	<tr>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbHot">
				Select Numbers for HOT:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbHot" class="textbox" runat="server" Text="20" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbHot_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbHotMin">
				HOT Min:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbHotMin" class="textbox" runat="server" Text="1" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbHotMin_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbHotMax">
				HOT Max:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbHotMax" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbHotMax_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="ddlSelectMode">
				Select Algorithm to Use:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:DropDownList ID="ddlSelectMode" runat="server" SkinID="dwopDownListSkin" Width="160"
				AutoPostBack="True" OnSelectedIndexChanged="ddlSelectMode_SelectedIndexChanged">
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiHot">
				Select Numbers for Semi HOT:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiHot" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiHot_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiHotMin">
				Semi HOT Min:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiHotMin" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiHotMin_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiHotMax">
				Semi HOT Max:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiHotMax" class="textbox" runat="server" Text="9" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiHotMax_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<asp:Label runat="server" ID="lblRange" class="lbl" for="ddlRange" Visible="false">Select a range in which 3 numbers fall in: </asp:Label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:DropDownList ID="ddlRange" runat="server" SkinID="dwopDownListSkin" Width="90"
				Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlRange_SelectedIndexChanged">
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiCold">
				Select Numbers for Semi COLD:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiCold" class="textbox" runat="server" Text="10" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiCold_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiColdMin">
				Semi COLD Min:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiColdMin" class="textbox" runat="server" Text="9" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiColdMin_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbSemiColdMax">
				Semi COLD Max:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbSemiColdMax" class="textbox" runat="server" Text="15" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbSemiColdMax_TextChanged"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbCold">
				Select Numbers for COLD:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbCold" class="textbox" runat="server" Text="5" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbCold_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbColdMin">
				COLD Min:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbColdMin" class="textbox" runat="server" Text="15" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbColdMin_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbColdMax">
				COLD Max:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbColdMax" class="textbox" runat="server" Text="20" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbColdMax_TextChanged"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbVeryCold">
				Select Numbers for VERY COLD:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbVeryCold" class="textbox" runat="server" Text="3" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbVeryCold_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbVeryColdMin">
				VERY COLD Min:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbVeryColdMin" class="textbox" runat="server" Text="20" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbVeryColdMin_TextChanged"></asp:TextBox>
		</td>
		<td style="width: 230px; text-align: right;">
			<label class="lbl" for="tbVeryHot">
				Select Numbers for VERY HOT:
			</label>
		</td>
		<td style="width: 200px; text-align: left;">
			<asp:TextBox ID="tbVeryHot" class="textbox" runat="server" Text="3" SkinID="ShorterTextBoxSkin"
				OnTextChanged="tbVeryHot_TextChanged"></asp:TextBox>
		</td>
	</tr>
</table>
