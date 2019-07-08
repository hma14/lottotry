using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccessTier;

namespace Lottotry
{
    public partial class terms : System.Web.UI.Page
    {
        DataAccessLayer dbManager = new DataAccessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            //pdf.Attributes.Add("src", "/Doc/Terms_and_Conditions.pdf");
            //pdf.Attributes.Add("width", "830");
            //pdf.Attributes.Add("height", "500");
        }
    }
}
