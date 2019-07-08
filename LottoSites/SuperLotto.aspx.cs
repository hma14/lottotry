using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class SuperLotto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://www.lottery.gov.cn/lottery/dlt/Default.aspx");
            Util.SetLottoPageLayout(lottolinks);
        }
    }
}