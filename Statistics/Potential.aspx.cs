using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottotry
{
    public partial class Potential : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pdf.Attributes.Add("src", "/Doc/Potential_Draws.pdf");
            string width = ConfigurationManager.AppSettings["DocWidth"];
            string height = ConfigurationManager.AppSettings["DocHeight"];
            pdf.Attributes.Add("width", width);
            pdf.Attributes.Add("height", height);
        }
    }
}