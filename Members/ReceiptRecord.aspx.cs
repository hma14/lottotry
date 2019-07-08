using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DataAccessTier;

namespace Lottotry.Members
{
    public partial class ReceiptRecord : System.Web.UI.Page
    {
        DataAccessLayer dbManager = new DataAccessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {

            SqlDataReader reader;
            if (!IsPostBack)
            {
                
                try
                {
                    dbManager.OpenConnection();
                    
                    string uid = HttpContext.Current.User.Identity.Name;
                    lblFName.Text = uid;
                    
                    reader = dbManager.SpGetAllReceipt(uid);                   
                    rptReceiptRecord.DataSource = reader;
                    rptReceiptRecord.DataBind();


                }
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    dbManager.CloseConnection();
                }
            }
        }
    }
}