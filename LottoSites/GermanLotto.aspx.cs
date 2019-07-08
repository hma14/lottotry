using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class GermanLotto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://www.europeanlotteryguild.com/lottery_results/german_lotto_results");
            Util.SetLottoPageLayout(lottolinks);

        }
    }
}