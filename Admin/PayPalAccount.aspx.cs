using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottotry.Admin
{
    public partial class PayPalAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "https://www.sandbox.paypal.com/");
            lottolinks.Attributes.Add("width", "830");
            lottolinks.Attributes.Add("height", "650");
        }
    }
}