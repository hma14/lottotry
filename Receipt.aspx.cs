using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using DataAccessTier;
using BusinessTier;


namespace Lottotry
{
    public partial class Receipt : System.Web.UI.Page
    {
        DataAccessLayer dbManager = new DataAccessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader reader;
           
            string uid = Server.UrlDecode(Request.QueryString["userName"]);
            string transid = Server.HtmlDecode(Request.QueryString["transactionID"]);
            try
            {
                dbManager.OpenConnection();
                
                reader = dbManager.SpGetReceipt(uid, transid);    
                if (reader.Read())
                {
                    lblUid.Text = reader["userName"].ToString();
                    //lblTransID.Text = reader["TransactionID"].ToString();
                    //lblCCT.Text = reader["CCType"].ToString();
                    //lblCCN.Text = reader["CCNumber"].ToString();
                    //lblExpiryDate.Text = reader["CCExpiryDate"].ToString();
                    lblFullName.Text = reader["FullName"].ToString();
                    lblPlan.Text = reader["MemberPlan"].ToString();
                    lblStartDate.Text = reader["StartDate"].ToString();
                    lblExpiredDate.Text = reader["ExpiredDate"].ToString();
                    
                }
                //lblThankyou.Text = "Thank you, <em>" + lblFullName.Text + "</em> for the payment!";
                lblThankyou.Text = "Thank you, <em style='color: #C24641;font-size: 100%;font-style:Italic;'>" + 
                    lblFullName.Text +
                    "</em> for joining the LottoTry Membership! You can fully use LottoTry application for <em style='color: #C24641;font-size: 100%;font-style:Italic;'>one year.</em> Enjoy and Good Luck!";
            }
            catch (SqlException ex)
            {
                //lblError.Text = ex.Message;
                throw ex;
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }
    }
}