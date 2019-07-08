<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeBehind="Lotto.aspx.cs" Inherits="Lottotry.Members.Lotto" enableViewState="true" %>

<%@ Register TagPrefix="uc" TagName="ConfigForm" Src="~/Controls/ConfigForm.ascx" %>
<%@ Register TagPrefix="uc" TagName="AutoDrawConfig" Src="~/Controls/AutoDrawConfig.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Extender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: 10px;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 15px;
            padding: 1px;
            background-color: White;
        }

        .ExtenderList
        {
            border-bottom: 1px #006699;
            cursor: pointer;
            color: black;
        }

        .Highlight
        {
            color: White;
            background-color: #229603;
            cursor: pointer;
        }

        #width
        {
            width: 100px;
        }
    </style>
    <script type="text/javascript" language="javascript">



        function SetContextKey(nodeId) {
            if (nodeId == "DBDdl12") {
                $find('<%=AutoCompleteExtender1.ClientID %>').set_contextKey($get("<%=DBDdl12.ClientID %>").value);
            }
            else if (nodeId == "DBDdl8") {
                $find('<%=AutoCompleteExtender2.ClientID %>').set_contextKey($get("<%=DBDdl8.ClientID %>").value);
            }
            else if (nodeId == "DBDdl10") {
                $find('<%=AutoCompleteExtender3.ClientID %>').set_contextKey($get("<%=DBDdl10.ClientID %>").value);
            }
            else if (nodeId == "DBDdl1") {
                $find('<%=AutoCompleteExtender4.ClientID %>').set_contextKey($get("<%=DBDdl1.ClientID %>").value);
                $find('<%=AutoCompleteExtender5.ClientID %>').set_contextKey($get("<%=DBDdl1.ClientID %>").value);
            }
            else if (nodeId == "DBDdl4") {
                $find('<%=AutoCompleteExtender6.ClientID %>').set_contextKey($get("<%=DBDdl4.ClientID %>").value);
                $find('<%=AutoCompleteExtender7.ClientID %>').set_contextKey($get("<%=DBDdl4.ClientID %>").value);
            }
            else if (nodeId == "DBDdl5") {
                $find('<%=AutoCompleteExtender8.ClientID %>').set_contextKey($get("<%=DBDdl5.ClientID %>").value);
                $find('<%=AutoCompleteExtender9.ClientID %>').set_contextKey($get("<%=DBDdl5.ClientID %>").value);
            }
            else if (nodeId == "DBDdl6") {
                $find('<%=AutoCompleteExtender10.ClientID %>').set_contextKey($get("<%=DBDdl6.ClientID %>").value);
                $find('<%=AutoCompleteExtender11.ClientID %>').set_contextKey($get("<%=DBDdl6.ClientID %>").value);
            }
            else if (nodeId == "DBDdl2") {
                $find('<%=AutoCompleteExtender12.ClientID %>').set_contextKey($get("<%=DBDdl2.ClientID %>").value);
            }
            else if (nodeId == "DBDdl3") {
                $find('<%=AutoCompleteExtender13.ClientID %>').set_contextKey($get("<%=DBDdl3.ClientID %>").value);
			    $find('<%=AutoCompleteExtender14.ClientID %>').set_contextKey($get("<%=DBDdl3.ClientID %>").value);
			}
			else if (nodeId == "DBDdl7") {
			    $find('<%=AutoCompleteExtender15.ClientID %>').set_contextKey($get("<%=DBDdl7.ClientID %>").value);
			    $find('<%=AutoCompleteExtender16.ClientID %>').set_contextKey($get("<%=DBDdl7.ClientID %>").value);
			}
			else if (nodeId == "DBDdl13") {
			    $find('<%=AutoCompleteExtender17.ClientID %>').set_contextKey($get("<%=DBDdl13.ClientID %>").value);
			    $find('<%=AutoCompleteExtender18.ClientID %>').set_contextKey($get("<%=DBDdl13.ClientID %>").value);
			}


}

//  Non-AJAX spinner
$(function () {
    $('.buttons').click(function () {
        $('#spinner').show();
    });
});

function resetDDL(nodeId) {
    if (nodeId == "DBDdl12") {

        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
        //$get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl12.ClientID %>").value;
    }
    else if (nodeId == "DBDdl10") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
        // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl10.ClientID %>").value;
    }
    else if (nodeId == "DBDdl1") {
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
        $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl1.ClientID %>").value;
    }
    else if (nodeId == "DBDdl2") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
        // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl2.ClientID %>").value;
    }
    else if (nodeId == "DBDdl3") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
        //  $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl3.ClientID %>").value;
    }
    else if (nodeId == "DBDdl4") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
        // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl4.ClientID %>").value;
    }
    else if (nodeId == "DBDdl5") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
        // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl5.ClientID %>").value;
    }
    else if (nodeId == "DBDdl6") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
        $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl6.ClientID %>").value;
    }

    else if (nodeId == "DBDdl7") {
        $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			    // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl7.ClientID %>").value;
			}

			else if (nodeId == "DBDdl8") {
			    $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			    // $get("<%=DBDdl13.ClientID %>").value = $get("<%=DBDdl8.ClientID %>").value;
			}
			else if (nodeId == "DBDdl13") {
			    $get("<%=DBDdl1.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl2.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl3.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl4.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl5.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl6.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl7.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl8.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    $get("<%=DBDdl10.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			    // $get("<%=DBDdl12.ClientID %>").value = $get("<%=DBDdl13.ClientID %>").value;
			}

}



    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphcontent" runat="server">
    <%--<div>
		<script language="javascript" type="text/javascript">
			var prm = Sys.WebForms.PageRequestManager.getInstance();

			prm.add_initializeRequest(InitializeRequest);
			prm.add_endRequest(EndRequest);
			var postBackElement;
			function InitializeRequest(sender, args) {

				if (prm.get_isInAsyncPostBack())
					args.set_cancel(true);
				postBackElement = args.get_postBackElement();
				if ((postBackElement.id == 'submit8') ||
					(postBackElement.id == 'submit10') ||
					(postBackElement.id == 'submit12'))
					$get('UpdateProgress1').style.display = 'block';
			}

			function EndRequest(sender, args) {
				if ((postBackElement.id == 'submit8') ||
					(postBackElement.id == 'submit10') ||
					(postBackElement.id == 'submit12'))
					$get('UpdateProgress1').style.display = 'none';
			} 
		</script>
	</div>--%>
    <div id="lotto_content">
        <%--AJAX spinner--%>
        <div id="spinner" class="spinner" style="display: none;">
            <img src="/images/ajax-loader-large.gif" alt="Loading" class="spinner" />
        </div>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="WSGetDrawNumbers.asmx" />
            </Services>
        </asp:ToolkitScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="10">
            <ProgressTemplate>
                <img id="img-spinner" src="/images/ajax-loader-large.gif" alt="Loading" class="spinner" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div id="TabbedPanels1" class="TabbedPanels">
            <ul class="TabbedPanelsTabGroup">
                <li class="TabbedPanelsTab" tabindex="0">Predict Draws</li>
                <li class="TabbedPanelsTab" tabindex="1">Auto Draws</li>
                <%--<li class="TabbedPanelsTab" >Compare</li>--%>
                <li class="TabbedPanelsTab" tabindex="2">Potential</li>
                <%--<li class="TabbedPanelsTab" >Scope</li>--%>
                <li class="TabbedPanelsTab" tabindex="3">Stat 1</li>
                <li class="TabbedPanelsTab" tabindex="4">Stat 2</li>
                <li class="TabbedPanelsTab" tabindex="5">Stat 3</li>
                <li class="TabbedPanelsTab" tabindex="6">Stat 4</li>
                <li class="TabbedPanelsTab" tabindex="7">Stat 5</li>
                <li class="TabbedPanelsTab" tabindex="8">Stat 6</li>
                <li class="TabbedPanelsTab" tabindex="9">Stat 7</li>
                <li class="TabbedPanelsTab" tabindex="10">Chart</li>
            </ul>
            <div class="TabbedPanelsContentGroup">
                <%--Predict Draws--%>
                <div class="TabbedPanelsContent">
                    <p style="display: block">
                        This tool provides following functionalities: <a class="more" href="#">details</a>
                    </p>
                    <p style="display: block">
                        <span class="numItems">1.</span>You can tune the variables to whatever you need
						to. This tool provides default values for each variable. You can tune them freely
						to meet the needs of your prediction. Once you done the variable configuration,
						click submit button and a new group of numbers will be generated.<br />
                        <br />
                        <span class="numItems">2.</span>To start a draw number generating, enter an integer
						value in target draw text box. This value indicates which target draw you want to
						predict, if the <strong>text box is left empty</strong> while clicking submit button, the
						target draw would be the next draw. You also can enter a past draw number, which
						you can find out in Statistics 7, to run and test how much you can match the past
						target draw. This is one of important functionalities for this tool. By doing this
						practice, you can accumulate your experience on how to select right numbers in future
						draws.<br />
                        <br />
                        <span class="numItems">3.</span>Next, select a lottery that may be available to
						you. The lotteries this tool covers are displayed in the drop down list.
						<br />
                        <br />
                        <span class="numItems">4.</span>Actual generating draw numbers for target draw (could
						be next draw, or any past draws as long as it is correct one). The generated numbers
						(in RED) are based on user input as shown in text boxes of Sum Min, Sum Max and
						Number of Odds displayed in the screen of Variable Configuration page. These three
						values determine the generated draw numbers, i.e. the sum of six draw numbers must
						be greater than Sum Min and less than Sum Max. And they must contain same amount
						of odd numbers as the values entered in Number of Odds text box. <strong>Statistics 7</strong>
                        provides history of draws and statistics related to <strong>sum</strong> and <strong>odd/even</strong>.
						You may refer stat tool first to research the trend then come back to make decision
						what values you may enter in these text boxes. No matter how randomness of each
						draw, some trends still can be traced, caught and followed by. LottoTry™ provides
						sufficient indications and statistics and rest of things are up to your wisdom to
						conquer the lottery.
						<br />
                        <br />
                        <span class="numItems">5.</span>You can get new generated set of draw numbers (in
						RED color) by clicking submit button each time. If you do not satisfy the generated
						set of draw numbers (ex. For Lotto 649, the set draw number is 6, for Mega Millions,
						is 5, etc.), just keep clicking submit button (Warning: don’t click too fast – every
						1-2 seconds interval at least for the sake of server response time) and different
						set of draw numbers (as well as different set of alternative numbers in WHITE color,
						called alternative numbers in the rest of documents) would be generated. You should
						consider numbers in both colors. Most of time the final draw numbers may come from
						both groups. Don’t just simply pick the generated set of draw numbers (in RED).
						To choose which numbers, usually you have to base on analysis from other tools and
						statistics on other tabs.
						<br />
                        <br />
                        <span class="numItems">6.</span>Value for <strong>R</strong> indicates <strong>Relative Distance</strong>,
						which means this number got last hit is number value draws ago. For instance,<strong>19
							(R=4)</strong> refers that number 19 was hit 4 draws ago. Value for <strong>S</strong> indicates
						<strong>Saved Distance</strong>, which means how many draws ago this number got hit prior
						to the last hit. For instance <strong>19 (R=4) (S=16)</strong> refers that number 19 got
						hit prior to last hit was 16 draws ago and the last hit to now is 4 draws ago. Therefore,
						in this case <strong>19 (R=4) (S=16)</strong> indicates that 19 was once cold number and
						became hot after last hit. This indication may be very useful in choosing which
						number should be your last choice. The 4 and 16 here are values of <strong>distance</strong>.
						<strong>Statistics 3/4</strong> provides longer history for all numbers and better go through
						those statistics tools before making any final decision.<br />
                        <br />
                        <span class="numItems">7.</span>All numbers generated here, either in RED or in
						WHITE, are based on preprietary algorithms which are supposed to have been filtering
						out those DEAD numbers in order to narrow down the range of number for selection.
						This will increase the hit possibility and save money from spending on those virtually
						no-hope, "DEAD" numbers. This is also one of reasons why you must use LottoTry™
						analysis tools for your adventure on making unthinkable to be thinkable.<br />
                        <br />
                        For the details on explanation of how to use this tool, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
						<br />
                    </p>
                    <%--AJAX--%>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbTargetDraw4">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tbTargetDraw4"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            UseContextKey="true" EnableCaching="false" CompletionListCssClass="Extender"
                                            FirstRowSelected="false" CompletionListItemCssClass="ExtenderList" CompletionListHighlightedItemCssClass="Highlight"
                                            CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetDraw4" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl12')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl12">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl12" class="ddlFadeIn" runat="server" SkinID="dwopDownListLongSkin"
                                            AutoPostBack="True" OnSelectedIndexChanged="DBDdl12_SelectedIndexChanged" onchange="resetDDL('DBDdl12')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit12" class="buttonGen" runat="server" OnClick="submit12_Click" Text="Submit" />
                                    </td>
                                    <td>
                                        <asp:Button ID="CalibrateGen" class="buttonGen" runat="server" Text="Tune Variables" OnClick="CalibrateGen_Click" />
                                    </td>
                                    <td>
                                        <asp:Image ID="ImagePredict" runat="server" Height="30" ImageAlign="Middle" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl12" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <uc:ConfigForm ID="ConfigForm1" runat="server" Visible="false"></uc:ConfigForm>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="submit10" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit12" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit8" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="CalibrateGen" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Auto Draws--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This tool provides following functionalities: <a class="more2" href="#">details</a>
                    </p>
                    <p style="display: block">
                        <span class="numItems">1.</span>Variable configuration as shown herein, you can
						tune the variables to whatever you need to. This tool provides default values for
						each variable. You can tune them freely to meet the needs of your prediction. Once
						you done the variable configuration, click submit button and the new group of numbers
						will be generated. The only difference between this tool and <strong>Predict Draws</strong>
                        in variable configuration panel is that the generated draw numbers are not depending
						on sum min/max and number of odd as shown in <strong>Predict Draws</strong>. Instead, they
						depend on selection of algorithm. The details of each algorithm are shown in the
						right column of the panel. The explanation of each algorithm selected shown as below:
						<br />
                        <span class="numItems">•</span>TSemi Hot Numbers – tool will select 3 numbers in
						random from Semi Hot Number group and rest of numbers of the target draw will be
						chosen from other groups in random.
						<br />
                        <span class="numItems">•</span>Hot Numbers - tool will select 3 numbers from Hot
						Number group in random and the rest of numbers of the target draw will be chosen
						in random from other groups.
						<br />
                        <span class="numItems">•</span>Mix – tool will select all draw numbers in random
						from whole groups.
						<br />
                        <span class="numItems">•</span>Number Range – a new selection list will be visible
						under the algorithm selection list, in which number range shown up.
						<br />
                        For each number range, the tool will select 3 numbers from that range and the rest
						of the numbers for the target draw will be chosen from other groups. This algorithm,
						therefore, is number range oriented. According to analysis and research on the other
						statistics on other tabs, the trend that draw numbers fallen in certain number ranges
						can be caught, <strong>Statistics 1</strong> typically for this purpose and from which you
						can analyze what would be the next number ranges in which the next draw may fall.
						Let’s say you know at least two to three numbers might be possible fall in to the
						number ranges 1 – 9 for instance, after analyzing on <strong>Statistics 1</strong>, you
						can choose number ranges 1 – 9 in this selection list and let nature to select the
						remaining numbers for the next draw. This way, you bucks might be invested more
						profitably and you also can choose different number range for each ticket you going
						to buy to cover all possible missing guess. Well this would depend on how many tickets
						you want to buy, the more you buy, the more numbers may get hit for the next draw.
						This tool particularly works for those group-ticket-buyers, which will increase
						chances enormously in hitting range of breakdown prices, even a Jackpot.<br />
                        <br />
                        <span class="numItems">2.</span>The other difference from the <strong>Predict Draws</strong>
                        is that since the generated draw numbers are not directly from so called alternative
						numbers (which generated based on variable configuration panel) but from selected
						algorithm, some of the generated draw numbers (in RED color) may not appear in the
						alternative number group. As you may noticed when you click submit button and run,
						only part of numbers out of six in RED color, but you can find the all generated
						draw numbers on the bottom of tablet. The point is that alternative numbers some
						time may miss some number that tool suppose to be DEAD number, but actually those
						numbers may hit the next draw because of the nature of randomness. Therefore this
						tool can be considered as supplement to the <strong>Predict Draws</strong>. Bottomline is
						that you might need to run both tools as well as others to compare the results before
						you make final decision.
						<br />
                        <br />
                        <span class="numItems">3.</span>To start a draw number generating, enter an integer
						value in target draw text box. This value indicates that which target draw you want
						to generate for, if the text box is left empty while clicking submit button, the
						target draw would be the next draw. You also can enter a past draw number, which
						you can find in <strong>Statistics 7</strong>, to run and test how much you can match the
						past target draw pretending not knowing the result to the selected past draw. This
						is one of important functionalities for this tool. By doing so, you can accumulate
						your skills and experience on how to select right numbers in future draws.<br />
                        <br />
                        <span class="numItems">4.</span>Next, select a lottery that may be available to
						you. The lotteries this tool covers are displayed in the drop down list.
						<br />
                        <br />
                        <span class="numItems">5.</span>You can get new generated draw numbers (in RED color)
						by clicking submit button each time. If you do not satisfy the generated set of
						draw numbers, just keep clicking submit button (Warning: don’t click too fast –
						every 1-2 seconds interval at least, for the sake of server response time) and different
						set of draw numbers, as well as new group of alternative number would be generated
						– this is another difference from <strong>Predict Draws</strong> where alternative numbers
						remain unchanged until you tune a new value in configuration panel.<br />
                        <br />
                        <span class="numItems">6.</span>Value for <strong>R</strong> indicates <strong>Relative Distance</strong>,
						which means this number got last hit is number value draws ago. For instance,<strong>19
							(R=4)</strong> refers that number 19 was hit 4 draws ago. Value for <strong>S</strong> indicates
						<strong>Saved Distance</strong>, which means how many draws ago this number got hit prior
						to the last hit. For instance <strong>19 (R=4) (S=16)</strong> refers that number 19 got
						hit prior to last hit was 16 draws ago and the last hit to now is 4 draws ago. Therefore,
						in this case <strong>19 (R=4) (S=16)</strong> indicates that 19 was once cold number and
						became hot after last hit. This indication may be very useful in choosing which
						number should be your last choice. The 4 and 16 here are values of <strong>distance</strong>.
						<strong>Statistics 3/4</strong> provides longer history for all numbers and better go through
						those statistics tools before making any final decision.<br />
                        <br />
                        <span class="numItems">7.</span>All numbers generated, either in RED or in WHITE,
						are based on proprietary algorithms which are supposed to have been filtering out
						those DEAD numbers in order to narrow down the range of number for selection. This
						will increase the hit possibility and save money from spending on those virtually
						no-hope, DEAD numbers. This is also one of reasons why you must use LottoTry™ analysis
						tools for your adventure on making unthinkable to be thinkable.<br />
                        <br />
                        For the details on explanation of how to use this tool, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.<br />
                    </p>
                    <%--AJAX--%>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbTargetDraw8">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tbTargetDraw8"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetDraw8" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl8')" OnTextChanged="tbTargetDraw8_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl8">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl8" runat="server" SkinID="dwopDownListLongSkin" AutoPostBack="True"
                                            OnSelectedIndexChanged="DBDdl8_SelectedIndexChanged" onchange="resetDDL('DBDdl8')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit8" class="buttonGen" runat="server" OnClick="submit8_Click" Text="Submit" />
                                    </td>
                                    <td>
                                        <asp:Button ID="CalibrateAutoDraw" class="buttonGen" runat="server" Text="Tune Variables" OnClick="CalibrateAutoDraw_Click" />
                                    </td>
                                    <td>
                                        <asp:Image ID="ImageAuto" runat="server" Height="30" ImageAlign="Middle" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl8" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <uc:AutoDrawConfig ID="AutoDrawConfig1" runat="server" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="submit10" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit12" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit8" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="CalibrateAutoDraw" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Compare--%>
                <%--        
		<div class="TabbedPanelsContent">
			<p style="display:block">Select a target draw from database and presume that 6 numbers be next
			<a class="more3" href="#">details</a></p>
			<p>oncommuing draw,
			then run the prepriatory algorithm to see how many times, you can hit selected draw, then results would
			be displayed. This is very important referential statistics for your choosing certain numbers for the
			real draws.
			 <br /> 
			After generated group of possible hit numbers for the next draw, 
			randomly select 6 numbers to compare if they match. This is only for practice presuming 
			the next draw already known. We do this to verify how many time we can hit the next draw 
			and make a statistics output. Below are the procedures to accomplish this<br />
				
				1. Generate potential hit number group refer to last section<br />
				2. Make sure that the next draw be 100% among this group<br />
				3. Randomly select 6 numbers from generated group<br />
				4. Match the target draw numbers with these selected numbers<br />
					a. If they match, output counter (loop numbers so far have been through) <br />
					b. If they do not match, go step 3 to start a new loop <br />
			 </p>
			 <br />
			
		 
		 <asp:UpdatePanel ID="UpdatePanel6" runat="server">
		 <ContentTemplate>

			<label class="lbl" for="tbTargetRow8">Target Draw: </label>
			<asp:TextBox ID="tbTargetDraw" runat="server" Width="75px" SkinID="ShorterTextBoxSkin"></asp:TextBox>
			<label class="lbl" for="DBDdl9">Select Lotteries: </label>
			<asp:DropDownList ID="DBDdl9" runat="server" SkinID="dwopDownListSkin">
				<asp:ListItem Value="0" Selected="True">649</asp:ListItem>
				<asp:ListItem Value="1">LottoMax</asp:ListItem>
			</asp:DropDownList>
			<asp:Button ID="submit9" runat="server" OnClick="submit9_Click" Text="Submit" 
				SkinID="buttonSkin" />
		</ContentTemplate>
		</asp:UpdatePanel>
			
			<hr />
	  </div>
                --%>
                <%--Potential--%>
                <div class="TabbedPanelsContent">
                    <p style="display: block">
                        This tool provides the following functionalities: <a class="more3" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span>Variable configuration as shown in this screen.
						This tool provides default values for each variables. You can tune them freely to
						meet the needs of your prediction. Once you done the variable configuration, click
						<strong>submit</strong> button and the new group of numbers will be generated.<br />
                        <br />
                        <span class="numItems">2.</span>This tool is different from other two random generating
						tools (<strong>Predict/Auto Draws</strong>) on that it works not for future draws, but provides
						indications for a given past draw. It works like this: once <strong>submit</strong> button
						clicked, the tool will run into a loop and each of the loops generates a group of
						numbers, which does the same random generating as other two tools do, until a group
						contains the whole given target numbers (given past draw), it then stops and outputs
						that group of numbers. The number of loops it’s been running through also displayed
						on top of the table.<br />
                        <br />
                        <span class="numItems">3.</span>The objective of creating this tool is to let users
						realize that not every random generating can cover a given draw, you might need
						to run multiple rounds in order to generate an ideal group of numbers which would
						be as short range as possible, meanwhile covers all numbers provided by a past given
						draw.<br />
                        <br />
                        <span class="numItems">4.</span>Therefore given draw numbers (in RED color, must
						be past known draw) are always remain unchanged upon each new running only alternative
						numbers and number of loops are changing. You can re-configure those variables and
						re-run it to optimize and improve the random generating by watching the number of
						loops each run results. Then you may apply the best configuration you think to other
						two tools to do the real challenge. Because they all utilize the same random generating
						algorithm. This is another important motivation why we need this tool.<br />
                        <br />
                        For the details on explanation of how to use this tool, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.<br />
                    </p>
                    <%--AJAX--%>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbTargetDraw10">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tbTargetDraw10"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="true" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetDraw10" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl10')" OnTextChanged="tbTargetDraw10_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl10">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl10" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl10_SelectedIndexChanged"
                                            AutoPostBack="True" onchange="resetDDL('DBDdl10')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit10" class="buttonGen" runat="server" OnClick="submit10_Click" Text="Submit" />
                                    </td>
                                    <td>
                                        <asp:Button ID="CalibratePotential" class="buttonGen" runat="server" Text="Tune Variables" OnClick="CalibratePotential_Click" />
                                    </td>
                                    <td>
                                        <asp:Image ID="ImagePotential" runat="server" Height="30" ImageAlign="Middle" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl10" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <hr />
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <uc:ConfigForm ID="ConfigForm2" runat="server" Visible="false" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="submit10" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit12" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="submit8" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="CalibratePotential" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Scope--%>
                <%--      <div class="TabbedPanelsContent">
		<p style="display:block">Generate potential numbers group for next real draw
		<a class="more5" href="#">details</a></p>
		<p style="display:block" >These numbers are chosen, out of 49 total numbers, based on proprietary algorithm and would be definitely  
		narrowing down the scope of potential numbers which may hit for next draw.<br /> 
		This is not practise. This is for real and good luck!<br /></p>
			<br />
				
		
		<asp:UpdatePanel ID="UpdatePanel3" runat="server">
		<ContentTemplate>
		<asp:Button ID="CalibrateScope" runat="server" CssClass="calibration"
				Text="Calibrate and Tune Configuration Variables" 
				onclick="CalibrateScope_Click"  SkinID="buttonSkin" Width="400"/>

		<label class="lbl" for="tbTargetDraw3">Target Draw: </label>
		<asp:TextBox ID="tbTargetDraw3" runat="server" Width="75px" SkinID="ShorterTextBoxSkin"></asp:TextBox>
		<label class="lbl" for="DBDdl11">Select Lotteries: </label>
		<asp:DropDownList ID="DBDdl11" runat="server" SkinID="dwopDownListSkin">
			<asp:ListItem Value="0" Selected="True">649</asp:ListItem>
			<asp:ListItem Value="1">LottoMax</asp:ListItem>
		</asp:DropDownList>
		<asp:Button ID="submit11" runat="server" OnClick="submit11_Click" Text="Submit" 
			SkinID="buttonSkin" />
		
		<hr />
		<table id="tblScope" runat="server" title="Configuration" cellpadding="5" cellspacing="3" 
			width="850">
 
			<tr>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbHot">Select Numbers for HOT: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbHot" runat="server" Text="15" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbHotMin">HOT Min: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbHotMin" runat="server" Text="1" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbHotMax">HOT Max: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbHotMax" runat="server" Text="5" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			</tr>

			<tr>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbSemiHot">Select Numbers for Semi HOT: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiHot" runat="server" Text="5" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbSemiHotMin">Semi HOT Min: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiHotMin" runat="server" Text="5" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbSemiHotMax">Semi HOT Max: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiHotMax" runat="server" Text="9" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			</tr>

			<tr>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbCold">Select Numbers for COLD: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbCold" runat="server" Text="5" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbColdMin">COLD Min: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbColdMin" runat="server" Text="15" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbColdMax">COLD Max: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbColdMax" runat="server" Text="20" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			</tr>
 
			<tr>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbSemiCold">Select Numbers for Semi COLD: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiCold" runat="server" Text="10" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbSemiColdMin">Semi COLD Min: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiColdMin" runat="server" Text="9" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbSemiColdMax">Semi COLD Max: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbSemiColdMax" runat="server" Text="15" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			</tr>

			<tr>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbVeryCold">Select Numbers for VERY COLD: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbVeryCold" runat="server" Text="3" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;"><label class="lbl" for="tbVeryColdMin">VERY COLD Min: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbVeryColdMin" runat="server" Text="20" 
					SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			<td style="width: 230px; text-align:right;" ><label class="lbl" for="tbVeryHot">Select Numbers for VERY HOT: </label></td>
			<td style="width: 200px; text-align:left;">
				<asp:TextBox ID="tbVeryHot" runat="server" Text="3" SkinID="ShorterTextBoxSkin"></asp:TextBox></td>
			
			</tr>           
			</table>
			</ContentTemplate>
			</asp:UpdatePanel>
		</div>
                --%>
                <%--Statistics 1--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This tool provides valuable statistics to facilitate users ... <a class="more4" href="#">details</a>
                    </p>
                    <p style="display: block">
                        on predicting which ranges the next hit numbers might be falling in after researching the previous draws of
						this statistics.<br />
                        <br />
                        <span class="numItems">1.</span>It provides distributions of hit numbers for each
						past draws in perspective of <strong>number ranges</strong>. The <strong>number ranges</strong>
                        are classified as <strong>1 – 9, 10 – 19, 20 – 29, 30 – 39, 40 – 49</strong>, in case of
						<strong>Lotto 649</strong>. Blank means no numbers got hit among that range of that draw.
						Numbers present in the columns of the row indicate how many numbers got hit on that
						range. If a range shows blanks for couple of past consecutive draws, you probably
						need to keep your eyes round on that range for the next draw. In constrast, a range
						displays numbers for number of past consecutive draws, the chances that any numbers
						get hit on that range may be slim. This is the basic idea this statistics tries
						to provide to users in predicting potential hit numbers for the next comming draw
						in perspective of <strong>number range</strong>.
						<br />
                        <br />
                        <span class="numItems">2.</span>Once you got idea of which ranges may get some numbers
						hit in next comming draw, you can concentrate on those ranges and find out candidate
						nubmers among those ranges with the help of cross-referencing other tools and statistics.<br />
                        <br />
                        <span class="numItems">3.</span>Though, you may always keep in mind with the nature
						of randomness and unpredictability of lottery, next draw probably still not following
						this common sense (for instance, the range of your interesting still got blank),
						one thing is pretty sure that one of next few draws might prove that you common
						sense will get paid-off. And what you can do is just watch and catch.<br />
                        <br />
                        <span class="numItems">4.</span>You can select range of draws by filling start draw
						with a past draw number. If leave it empty, it will start from 25 draws ago as default.
						Select a target draw, if leave it empty, next draw number will be selected.<br />
                        <br />
                        For the details on explanation of how to use this statistics, refer to the <strong>Guide
							Links</strong> and <strong>Video Clips</strong> on the left side bar.<br />
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="tbStartRow"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl1')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="tbTargetRow"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl1')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl1">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl1" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl1_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl1')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit1" class="buttons" runat="server" OnClick="submit1_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl1" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 2--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides <strong>distance</strong> oriented hit number distribution information
						<a class="more5" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span>This statistcs classifies <strong>distance</strong> into
						10 ranges. Column 1 means <strong>distance</strong> equals to 1, ... and column 10 means
						<strong>distance</strong> equals to 10. In the generated table, the 1st column is draw number
						(bottom row of the table is the most recent draw). 2nd is draw date. The next 10
						columns are distance ranges classified from 1 to 10 and the last column indicates
						the total numbers which got hit on current draw and their distances are less than
						or equal to 10.
						<br />
                        <br />
                        <span class="numItems">2.</span>When a distance range displays blank, it means there
						are <strong>no</strong> numbers which's <strong>distance</strong> equals to that distance range
						are got hit in that particular draw. When a distance range displays an integer value,
						it means there are that value of numbers which's <strong>distance</strong> equals to this
						distance range got hit in that particular draw. A number's <strong>distance</strong> equals
						to 4 means that number got hit 4 draws ago.<br />
                        <br />
                        <span class="numItems">3.</span>This statistics shows that most numbers got hit
						are those which's <strong>distance</strong> less than 10. Further, the generated table also
						visually displays that along with the <strong>distance</strong> getting shorter, more numbers
						of those distances getting hit (therefore blank getting fewer). This indicates that
						hot numbers (which's <strong>distance</strong> less than 5) may have more chances to get
						hit again.<br />
                        <br />
                        <span class="numItems">4.</span>Another hint this statistics provides is that when
						consecutive blanks presented on a particular distance range all the way to the current
						draw, this <strong>distance</strong> range should be watched for the next draw or next couple
						of draws, because it may be this distance range's turn to get hit soon.
						<br />
                        <br />
                        <span class="numItems">5.</span>While you concentrate on hot numbers, you may also
						notice that at least one or two numbers, which's <strong>distance</strong> greater than
						10 (cold number), got hit averagely after carefully looking at the past draws. Every
						once a while, only 2 or 3 numbers got hit which's <strong>distance</strong> less than 10,
						and the rest of hit numbrers for a particular draw, come from cold numbers.<br />
                        <br />
                        <span class="numItems">6.</span>Based on the facts above mentioned, you may discover
						some valuable trends from analyzing the past draws from this statistics. You may
						ask yourself a simple question: if these trends happened repeatedly before, why
						not it would be happened again in future? With this question in mind, when you make
						final decision, number's <strong>distance</strong> should be put into consideration and
						may resolve your dilemma when you facing multiple choices.<br />
                        <br />
                        <span class="numItems">7.</span>You can select range of draws by filling start draw
						with a past draw number. If leave it empty, it will start from 25 draws ago as default.
						Select a target draw, if leave it empty, next draw number will be selected as default.
						You can select a <strong>distance</strong> value, by default the <strong>distance</strong> is 10,
						it only cause different number of columns presented in the generated table.
						<br />
                        <br />
                        For the details on explanation of how to use this statistics, refer to the <strong>Guide
							Links</strong> and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow4">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="tbStartRow4"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow4" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl4')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow4">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="tbTargetRow4"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow4" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl4')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DistDdl4">
                                            Distance:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DistDdl4" runat="server" SkinID="dwopDownListSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl4">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl4" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl4_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl4')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit4" class="buttons" runat="server" OnClick="submit4_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl4" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit4" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 3--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides following functionalities and is the most import statistics
						in LottoTry&trade; <a class="more6" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span> All numbers (49 in case of Lotto 649) have been
						divided into number of Scales from left to right in ascending order based on frequency
						(see the top and bottom of rows of the generated table). The default scales are
						7 and you can choose other value you like. Each scale contains 7 columns in this
						case.
						<br />
                        <br />
                        <span class="numItems">2.</span> <strong>Statistics 3</strong> provides a panorama of the
						whole numbers with their distance info attached to each number. One more important
						thing is that each number maintains different colors to distinguish between hit
						and not hit in a particular draw. Further, the location of each number (in columns)
						is indicating the frequency of hit rate. For instance, from the generated table
						you can tell which numbers are hot, semi-hot, semi-cold, cold, very cold and very
						hot, etc., through the distance value(inside parentheses) attached to each number.
						You also can tell which numbers are inactive and which are most active by colored
						scale bands (both on top and bottom of generated table). With these statistics in
						mind, you can easily determine which numbers are most possibly the best candidates
						for the next coming draw. In fact, you may feel too many numbers which you think
						so close to be the candidate numbers for the next draw and would like to choose,
						you will be facing dilemma. Therefore, you need to combine your selection here with
						the conclusions of other statistics and tools in other tabs to come up with a final
						decision.
						<br />
                        <br />
                        <span class="numItems">3.</span> <strong>Statistics 3</strong> also provides you with a
						good visual facility on analyzing of past draws and chances to research on past
						hit numbers and in what conditions they got hit. You may find some trends being
						followed and happened again and again in the past. You may raise a question – It
						happened again and again in the past why not will be happened again in future? Good
						question, this is the core of objectives and motivation the <strong>Statistics 3</strong>
                        designated to facilitate users in discovering these kind of trends from virtually
						unthinkable random world.
						<br />
                        <br />
                        <span class="numItems">4.</span> Hit numbers fall into which colored scale bands
						in current draw also provides hints to the user that the attention might be paid
						on those scale bands, which got no hit numbers on current draw. There may be some
						numbers in these bands get hit for the next coming draw, because God is fare to
						everyone. This also applies to the cases the other way around.
						<br />
                        <br />
                        <span class="numItems">5.</span> You also can practice your number-picking skill
						by guessing/predicting past draws and then compare the result retrieved from database.
						<br />
                        <br />
                        <span class="numItems">6.</span>As you make your selections it's a good idea to
						include number selections that run consecutively. For instance let's say you have
						chosen the numbers 15 25 and 37. You should also consider selecting a consecutive
						number either higher or lower as well. A very high percentage of winning selections
						will have (2) numbers drawn this way.
						<br />
                        <br />
                        <span class="numItems">7.</span>There are a few sets of numbers that you should
						not play as they a very slim chance of ever being drawn. Consecutive sets of numbers
						at the beginning and ending of your lotto game should not be played. Example: Your
						lotto game has (49) total numbers. Don't play the numbers 1 2 3 4 5 6 or 44 45 46
						47 48 49. Each week people spend thousands of dollars on sets of numbers just like
						these.
						<br />
                        <br />
                        <span class="numItems">8.</span>Another point to remember is that most lotto games
						have a number range that extends beyond that of the calendar. If your lotto game
						has (49) total numbers and you'r not playing any numbers higher than (31) you may
						have to wait a very long time before your numbers come in. One way to overcome this
						problem is to start your number selection at the high end and slowly work down from
						there.
						<br />
                        <br />
                        <span class="numItems">9.</span>As amazing as it may seem you Do Not Need hundreds
						of past drawing in order to pinpoint the winning lottery numbers. At the very most
						you could use (50) drawings but you can do very well by using (25) drawings. Most
						of the time the numbers to be drawn next will have already appeared in the last
						ten drawings. If you track the numbers you will probably find that a few of them
						have been drawn quite often. If you find a few numbers that have been Hot but have
						Not been drawn for a couple of drawings you should consider them.
						<br />
                        <br />
                        <span class="numItems">10.</span> You can select range of draws by filling start
						draw with a past draw number. If leave it empty, it will start from 25 draws ago
						as default. Select a target draw, if leave it empty, next draw number will be selected
						as default. You can select a <strong>scales</strong> value, by default the <strong>scales</strong>
                        is 7, it only cause different number of scale bands presented on both top and bottom
						of the generated table. The bottom row is the most recent draw.
						<br />
                        <br />
                        For the details on explanation of this statistics, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow5">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="tbStartRow5"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow5" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbStartRow5_TextChanged" onkeyup="SetContextKey('DBDdl5')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow5">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" TargetControlID="tbTargetRow5"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow5" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbTargetRow5_TextChanged" onkeyup="SetContextKey('DBDdl5')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="scalesDdl5">
                                            Scales:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="scalesDdl5" runat="server" SkinID="dwopDownListSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl5">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl5" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl5_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl5')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit5" class="buttons" runat="server" OnClick="submit5_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl5" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit5" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 4--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides the following funtionalities: <a class="more7" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span><strong>Statistics 4</strong> provides a panorama of the
						whole numbers grouped by their distances. The grouped numbers will facilitate users
						in finding which numbers belong to which distance group distinctly and visually.
						It also facilitates users to find out how many numbers and which numbers belong
						to a distance group quickly. Left side of table, in the generated table, shows all
						hit numbers from past draws all the way to the current draw and you can easily infer
						that these numbers’ distance be zero as well as on the left side of the table have
						shorter distance than those on the right side of table (see the value inside parentheses
						attached to each number).
						<br />
                        <br />
                        <span class="numItems">2.</span>Since number’s temperature (hot number, … cold number,
						etc) based on their distance, <strong>Statistics 4</strong> enables users to find out hot,
						cold, very cold, etc. quickly and easily.
						<br />
                        <br />
                        <span class="numItems">3.</span>Usually, analysis results of <strong>Statistics 4</strong>
                        are used to combine with other statistics and analysis tools in selecting number
						candidates for the next coming draw.
						<br />
                        <br />
                        <span class="numItems">4.</span>Whole numbers appeared in each rows (each draws)
						of table enable users to have panorama view and better idea on whole numbers which
						made the comparison among numbers, as well as among different draws, in one screen
						possible.
						<br />
                        <br />
                        <span class="numItems">5.</span>The history of the draws visually exposes some trends
						which may not be easily or possibly detected for the most of similar analysis tools
						venders out there to provide. Dedicated users may even dig deeper to find out more
						helpful and valuable info from this statistics.
						<br />
                        <br />
                        <span class="numItems">6.</span> You can select range of draws by filling start
						draw with a past draw number. If leave it empty, it will start from 25 draws ago
						as default. Select a target draw, if leave it empty, next draw number will be selected
						as default. The bottom row is the most recent draw (see the draw nubmer in the first
						column of the generated table).
						<br />
                        <br />
                        For the details on explanation of this statistics, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow6">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender10" runat="server" TargetControlID="tbStartRow6"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow6" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbStartRow6_TextChanged" onkeyup="SetContextKey('DBDdl6')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow6">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender11" runat="server" TargetControlID="tbTargetRow6"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow6" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbTargetRow6_TextChanged" onkeyup="SetContextKey('DBDdl6')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl6">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl6" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl6_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl6')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit6" class="buttons" runat="server" OnClick="submit6_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl6" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit6" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 5--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides the following functionalities: <a class="more8" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span>There are four tables displayed in <strong>Statistics 5</strong>
                        and each provides certain statistics from different perspectives. In the top two
						tables, one for showing the coldest numbers of all and one for hottest numbers of
						all up to current selected draw. With a quick glance of these two tables, you would
						have whole idea of which numbers are hot and which are cold – you don’t have to
						go through all 49 (even 59 in case of Power Ball) numbers as well as a range of
						past draws to look up, one by one, in order to find these info – very time-consuming,
						tedious job. For instance, you want to pick 2 numbers which's distance equals to
						1 then find them in the second table, if you want to find a coldest number for the
						next draw (usually in each past draws there would be at least one to two nubmers
						are the cold/very cold numbers) pick them from the first table.
						<br />
                        <br />
                        <span class="numItems">2.</span>The third table listed all numbers with <strong>distance</strong>
                        and <strong>frequency</strong> info mapping to each numbers. It also high-lighted which
						numbers got hit in current draw. All numbers are ordered by their frequencies and
						classified into fragments. You can easily find active/inactive numbers from this
						table. The third and fourth tables are related in <strong>scales</strong>, the third table
						flatened the fourth table's columns, other than that the both tables can be treated
						identical. By combining this table with the fourth table, users may have idea which
						groups got hit numbers (how many) and which are not (in blank). This is one of factors
						that you may keep in mind when make a final decision. Considering a blank column
						(scale fragment) presenting in the current draw might indicate you that this column
						must be watched for the next (or next couple of) coming draw(s) and you may go ahead
						to pick some numbers from that scale fragment.
						<br />
                        <br />
                        <span class="numItems">3.</span><strong>Statistics 5</strong> can also be cross-referenced
						with <strong>Statistics 3</strong>, because they all share the same <strong>scale fragment</strong>
                        consecpt. Only difference on that the <strong>Statistics 3</strong> displays all past draws
						selected all the way to the current draw.
						<br />
                        <br />
                        <span class="numItems">4.</span>You can select range of draws by filling start draw
						with a past draw number. If leave it empty, it will start from 25 draws ago as default.
						Select a target draw, if leave it empty, next draw number will be selected as default.
						You can select a <strong>scales</strong> value, by default the <strong>scales</strong> is 7, it
						only cause different number of scale bands presented on both top and bottom of the
						generated table.
						<br />
                        <br />
                        For the details on explanation of this statistics, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbTarget">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender12" runat="server" TargetControlID="tbTarget"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTarget" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            onkeyup="SetContextKey('DBDdl2')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DistDdl">
                                            Distance:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DistDdl" runat="server" SkinID="dwopDownListSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label class="lbl" for="scalesDdl">
                                            Scales:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="scalesDdl" runat="server" SkinID="dwopDownListSkin">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl2">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl2" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl2_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl2')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit2" class="buttons" runat="server" OnClick="submit2_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl2" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 6--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides the following functionalities: <a class="more9" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span>Statistics 6 provides, one of two generated tables
						and also the major one, a panorama view of whole numbers with the nature number
						order so that you can easily go to the number you are interested in and find its
						history. You also can easily compare one number to others in terms of their history.
						The range of past draws are default as 25, usually that is far past enough, if you
						want to trace back the history further past, you can set start draw value whatever
						you want, but one thing might be kept in mind, the bigger range, the time you get
						your statistics displayed would be longer.
						<br />
                        <br />
                        <span class="numItems">2.</span>You can also select <strong>Past Draws</strong> from Ranges
						drop down list, which will only display hit numbers in each past draws all the way
						to the current draw.
						<br />
                        <br />
                        <span class="numItems">3.</span>You can select range of draws by filling start draw
						with a past draw number. If leave it empty, it will start from 25 draws ago as default.
						Select a target draw, if leave it empty, next draw number will be selected as default.
						Ranges has two selections one is the <strong>Past Draws</strong> as mentioned in <strong>2</strong>.
						The other one is <strong>All Numbers</strong> as mention in <strong>1</strong> above. This is the
						default selection. The top row is the most recent draw (see the draw nubmer in the
						first column of the generated table).
						<br />
                        <br />
                        For the details on explanation of this statistics, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow3">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender13" runat="server" TargetControlID="tbStartRow3"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow3" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbStartRow3_TextChanged" onkeyup="SetContextKey('DBDdl3')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow3">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender14" runat="server" TargetControlID="tbTargetRow3"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow3" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbTargetRow3_TextChanged" onkeyup="SetContextKey('DBDdl3')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="rangeDdl">
                                            Ranges:</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="rangeDdl" runat="server" Width="150px" SkinID="dwopDownListSkin"
                                            OnSelectedIndexChanged="rangeDdl_SelectedIndexChanged" OnTextChanged="rangeDdl_TextChanged">
                                            <asp:ListItem Value="0">Past Draws</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="1">All Numbers</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl3">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl3" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl3_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl3')">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit3" class="buttons" runat="server" OnClick="submit3_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl3" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit3" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Statistics 7--%>
                <div class="TabbedPanelsContent">
                    <p>
                        This statistics provides the following functionalities: <a class="more10" href="#">details</a>
                    </p>
                    <p>
                        <span class="numItems">1.</span>Statistics 7 introduce the new key words, such as,
						<strong>Sum</strong>, <strong>frequency of Sum</strong>, <strong>ratio of Odd/Even</strong>, <strong>rate of Odd/Even</strong>,
						<strong>ratio of total Odds/Evens</strong>. The definition of and usage of these key words
						are as followings:<br />
                        <span class="numItems">•</span> <strong>Sum</strong> is sum of all hit numbers to a particular
						draw. It is hard to have exact same sum value between two draws in its whole history
						of any lotteries. But they could be very close or far away in terms of sum in a
						neighboring draws (which user may particularly pay attention to). For instance last
						couple of draws achieved consecutively large sums, statistics tells us we might
						expect small sum for the next coming draw. To determine how small it is, you may
						need to go through other statistics then may come back with some idea about the
						exact value the sum. As mentioned above, no two draws have had exact same sum (it
						may happened before, but extremely rare), the Statistics 7 scales sum value from
						90 to 210 into 12 divisions – 10 as interval. For instance, if a draw’s sum is 124,
						then we treat the sum as 120, and so on.
						<br />
                        <span class="numItems">•</span> <strong>Frequency of Sum</strong> is about counting how
						many sums of draws fall into a division. Continue to the above example since this
						draw’s sum is 124, frequency of 120 will be incremented by 1. There are Sum scales
						of 90, 100 … and all the way to 210 and the last is all above 220. Frequency of
						Sum indicates which scale of sum value is the most popular and which is the most
						unpopular and which are average one, etc. Based on this info you can pick one sum
						value from that scale. Once you have a sum value in mind you can determine the <strong>Sum
							Min</strong> and <strong>Sum Max</strong> in <strong>Predict Draws</strong> and <strong>Potential Draws</strong>
                        number generating tools where you can tune to generate more accurate numbers for
						the next coming draw.
						<br />
                        <span class="numItems">•</span> <strong>Ratio of Odds/Evens</strong> provides another hint
						in determining what kind of number you may choose for the next draw. It is based
						on a very basic rule of probability that odd numbers and even numbers each should
						have 50% chance to show up in any draws, but in reality and short period of time,
						each draws are not following the rule exactly – this statistics can tell that. But
						one thing you may need to follow up is that after couple of draws in which consecutively
						4 out of 6 Odd numbers in each draw, next draw probably would be the Even’s turn
						to overwhelming the Odd numbers. This is the core of motivation that ratios of Odds/Evens
						are provided by this statistics. This information may impact your final decision
						on certain situation.
						<br />
                        <span class="numItems">•</span> <strong>Total Odds/Evens</strong> shows ratio of total Odds
						to total Evens allows in long term.
						<br />
                        <br />
                        <span class="numItems">2.</span>The SUM of each of your sets of combinations to
						play can mean the difference between Winning and Losing. For example: Let's say
						one of your sets of numbers to play was (3 7 12 23 31 37) which has a sum of 113.
						While the combination itself doesn't look that bad, the sum is still just below
						the average sum of winning numbers that have been drawn. The average (SUM) for a
						Pick 6 Lotto Game is between <strong>120-190</strong>.
						<br />
                        <br />
                        <span class="numItems">3.</span>Another thing to keep in mind is not to play sets
						of numbers that are all Odd or all Even. Example: You should not play combinations
						like 3 17 21 37 41 53 - all Odd or 2 8 18 28 34 42 - all Even. While this may happen
						some time in the future the odds of it happening are quite slim. You want to get
						all the odds you can in your favor. So try to split up your sets of numbers with
						say (3) odd and (3) even or (2) even and (4) odd etc.
						<br />
                        <br />
                        <span class="numItems">4.</span>As you make your selections it's a good idea to
						include number selections that run consecutively. For instance let's say you have
						chosen the numbers 15 25 and 37. You should also consider selecting a consecutive
						number either higher or lower as well. A very high percentage of winning selections
						will have (2) numbers drawn this way.
						<br />
                        <br />
                        <span class="numItems">5.</span>You can select range of draws by filling start draw
						with a past draw number. If leave it empty, it will start from 25 draws ago as default.
						Select a target draw, if leave it empty, next draw number will be selected as default.
						You can select a <strong>scales</strong> value, by default the <strong>scales</strong> is 7, it
						only cause different number of scale bands presented on both top and bottom of the
						generated table.
						<br />
                        <br />
                        For the details on explanation of this statistics, refer to the <strong>Guide Links</strong>
                        and <strong>Video Clips</strong> on the left side bar.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow7">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender15" runat="server" TargetControlID="tbStartRow7"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow7" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbStartRow7_TextChanged" onkeyup="SetContextKey('DBDdl7')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow7">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender16" runat="server" TargetControlID="tbTargetRow7"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow7" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbTargetRow7_TextChanged" onkeyup="SetContextKey('DBDdl7')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl7">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl7" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl7_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl7')" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit7" class="buttons" runat="server" OnClick="submit7_Click" Text="Submit" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl7" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="submit7" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--Chart--%>
                <div class="TabbedPanelsContent">
                    <p>
                        To understand what chart indicats, refer to: <a class="more11" href="#">details</a>
                    </p>
                    <p>
                        This chart is reflecting the Stat-7 on column "Freq. of Sum" and presenting a visually
						understanding of that column. Sum of a lotto numbers means that for a particular
						draw of a Lottery, 6/49 for instance: 4 12 23 27 35 44. The sum is 4 + 12 + 23 +
						27 + 35 + 44 = 145. The 145 is falling into range of 140s (140 - 149), therefore,
						140s frequncy increment by 1. According to the selecting of past lotto draws (default
						is past 50 draws if you don't put any value in Start Draw or Target Draw text boxes),
						you'll see that all the number ranges' frequecy displayed on Y-axis as shown in
						this chart. Obviously, X-axis displays each number range in which sum of a draw
						numbers may falls.
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <table class="tblUserInput" runat="server" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <label class="lbl" for="tbStartRow13">
                                            Start Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender17" runat="server" TargetControlID="tbStartRow13"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbStartRow13" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbStartRow13_TextChanged" onkeyup="SetContextKey('DBDdl13')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="tbTargetRow13">
                                            Target Draw:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender18" runat="server" TargetControlID="tbTargetRow13"
                                            ServicePath="WSGetDrawNumbers.asmx" ServiceMethod="GetDrawNumbers" MinimumPrefixLength="1"
                                            EnableCaching="false" CompletionListCssClass="Extender" CompletionListItemCssClass="ExtenderList"
                                            CompletionListHighlightedItemCssClass="Highlight" CompletionSetCount="10" ShowOnlyCurrentWordInCompletionListItem="true"
                                            CompletionListElementID="width">
                                        </asp:AutoCompleteExtender>
                                        <asp:TextBox ID="tbTargetRow13" class="textbox" runat="server" SkinID="ShorterTextBoxSkin"
                                            OnTextChanged="tbTargetRow13_TextChanged" onkeyup="SetContextKey('DBDdl13')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label class="lbl" for="DBDdl13">
                                            Select Lotteries:
                                        </label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DBDdl13" runat="server" SkinID="dwopDownListLongSkin" OnSelectedIndexChanged="DBDdl13_SelectedIndexChanged"
                                            onchange="resetDDL('DBDdl13')" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="submit13" runat="server" OnClick="submit13_Click" Text="Submit" SkinID="buttonSkin" />
                                    </td>
                                    <td>
                                        <asp:Image ID="ImageChart" runat="server" Height="30" ImageAlign="Middle" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DBDdl13" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%--TabbedPanelsContentGroup--%>
        </div>
        <%--TabbedPanels1--%>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="lblOutput">
                        <asp:Label ID="output" runat="server" Text=""></asp:Label>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="submit12" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="submit10" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="submit8" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="CalibrateAutoDraw" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="CalibrateGen" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="CalibratePotential" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <ContentTemplate>
                    <asp:Chart ID="Chart1" runat="server" Width="1040PX" Style="font-size: x-small">
                        <Series>
                            <asp:Series Name="Y-axis indicates acumulated number of times the sum of Lotto numbers has been falling into a particular number range shown on X-axis. i.g. sum of one Lotto numbers = 167 will fall in 160s(160 - 169), etc. The chart based on Stat-7 'Freq. of Sum' column."
                                XValueMember="NumberRange" YValueMembers="Freq" IsVisibleInLegend="true" MarkerStep="1"
                                ChartType="Column" YValueType="Int32" Legend="Legend1" LabelBorderDashStyle="NotSet">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="false" Area3DStyle-WallWidth="1"
                                IsSameFontSizeForAllAxes="true" BackColor="White" BackSecondaryColor="White">
                                <AxisX LineColor="DarkGray" Interval="1" IsLabelAutoFit="false">
                                    <MajorGrid LineColor="LightGray" />
                                </AxisX>
                                <AxisY LineColor="DarkGray">
                                    <MajorGrid LineColor="LightGray" />
                                </AxisY>
                                <Area3DStyle Enable3D="false"></Area3DStyle>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Alignment="Center" Docking="Bottom" Name="Legend1" IsEquallySpacedItems="True"
                                AutoFitMinFontSize="8" ForeColor="DarkOliveGreen" LegendItemOrder="SameAsSeriesOrder"
                                TextWrapThreshold="150">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Name="Title1" Text="Display Frequent Number Ranges in which the Sum of Lotto Numbers falls"
                                BackGradientStyle="LeftRight" BackImageWrapMode="TileFlipX" BorderDashStyle="NotSet"
                                ForeColor="DarkViolet">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="submit13" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <script type="text/javascript">
	<!--
    //function getURLParam(strParamName) {
    //    var strReturn = "";
    //    var strHref = window.location.href;
    //    if (strHref.indexOf("?") > -1) {
    //        var strQueryString = strHref.substr(strHref.indexOf("?"));
    //        var aQueryString = strQueryString.split("&");
    //        for (var iParam = 0; iParam < aQueryString.length; iParam++) {
    //            if (aQueryString[iParam].indexOf(strParamName + "=") > -1) {
    //                var aParam = aQueryString[iParam].split("=");
    //                strReturn = aParam[1];
    //                break;
    //            }
    //        }
    //    }

    //    return strReturn;
    //}
            //var tab = parseInt(getURLParam('tab'));

    var tab = "<%= Session["DefaultTabbedPanel"].ToString()%>";
    tab = parseInt(tab);
    var TabbedPanels1 = new Spry.Widget.TabbedPanels("TabbedPanels1", { defaultTab: tab });
    //-->
        </script>
    </div>
    <%--content--%>
</asp:Content>
<asp:Content ID="Content6" runat="server" ContentPlaceHolderID="cphrightsidebar">
</asp:Content>
<asp:Content ID="Content7" runat="server" ContentPlaceHolderID="ErrorLabel">
    <div id="LblError">
        <asp:Label ID="lblError" runat="server" Text="" Style="color: Red; font-weight: bold;" />
    </div>
</asp:Content>
<asp:Content ID="Content8" runat="server" ContentPlaceHolderID="cphleftsidebar">
    <div id="sidebar">
    </div>
</asp:Content>

