﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class Lotto649 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://lotto.bclc.com/winning-numbers/lotto-649-and-extra.html");
            Util.SetLottoPageLayout(lottolinks);

        }
    }
}