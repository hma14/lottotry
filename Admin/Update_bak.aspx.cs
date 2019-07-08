using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessTier;
using DataAccessTier;

namespace Lottery
{
    public partial class Update : System.Web.UI.Page
    {
        private clsLotto lotto;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void submit3_Click(object sender, EventArgs e)
        {
            Database db = (Database)int.Parse(DBDl3.SelectedValue);

            string op = OpDdl2.SelectedItem.Text;
            int drawno = 0;
            if (tbDN3.Text != "")
            {
                drawno = int.Parse(tbDN3.Text);
            }


            this.Visible = false;
            try
            {
                lotto = new BusinessTier.clsLotto(db, fromSite.Value);
                string stmt = lotto.retrieveGroup(drawno, op);


                Response.Write(stmt);
            }
            catch (Exception exp)
            {
                Response.Write(exp.Message);
            }
        }
        protected void uploadBtn_Click(object sender, EventArgs e)
        {
            Session.Add("formname", "Uploadfile");
            Server.Transfer("Login.aspx", true);
        }
    }
}
