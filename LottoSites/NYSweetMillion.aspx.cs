﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;

namespace Lottotry
{
    public partial class NYSweetMillion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lottolinks.Attributes.Add("src", "http://nylottery.ny.gov/wps/portal/sweetmillion");
            Util.SetLottoPageLayout(lottolinks);
        
        }
    }
}