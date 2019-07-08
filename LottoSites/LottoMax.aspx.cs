using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class LottoMax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "https://lotto.bclc.com/winning-numbers/lotto-max-and-extra.html");
            Util.SetLottoPageLayout(lottolinks);

        }
    }
}