using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry.LottoSites
{
    public partial class NewJerseyPick6Lotto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://www.state.nj.us/lottery/games/1-3_pick6.shtml");
            Util.SetLottoPageLayout(lottolinks);
        }
    }
}