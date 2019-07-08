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
    public partial class FloridaFantasy5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://flalottery.com/fantasy5.do");
            Util.SetLottoPageLayout(lottolinks);

        }
    }
}