using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class TexasCashFive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "https://www.txlottery.org/export/sites/lottery/Games/Cash_Five/index.html");
            Util.SetLottoPageLayout(lottolinks);

        }
    }
}