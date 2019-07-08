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
    public partial class BC49 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "https://lotto.bclc.com/winning-numbers/bc49-and-extra.html");
            Util.SetLottoPageLayout(lottolinks);

        }
        
    }
}