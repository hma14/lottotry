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
    public partial class ConnecticutLotto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://www.ctlottery.org/Modules/Games/default.aspx?id=6");
            Util.SetLottoPageLayout(lottolinks);
        }
    }
}