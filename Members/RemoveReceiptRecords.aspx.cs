using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataAccessTier;

namespace Lottotry.Members
{
    public partial class RemoveReceiptRecords : System.Web.UI.Page
    {
        DataAccessLayer dataAccess = new DataAccessLayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = HttpContext.Current.User.Identity.Name;
            int i = 1;
            if (!IsPostBack)
            {
                SqlDataReader reader;
                try {
                    dataAccess.OpenConnection();
                    reader = dataAccess.SpGetTransactionID(uid);
                    
                    while (reader.Read())
                    {
                        i++;
                        ddlReceiptRecords.Items.Add(new ListItem(reader["TransactionID"].ToString(), i.ToString()));
                    }
                } 
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                finally {
                    dataAccess.CloseConnection();
                }
                ListItem litem0 = new ListItem("Select ...", "0");
                ddlReceiptRecords.Items.Insert(0, litem0);
                if (i > 1)
                {
                    ListItem litem1 = new ListItem("Remove All", "1");
                    litem1.Attributes.CssStyle.Add("color", "Red");
                    ddlReceiptRecords.Items.Insert(1, litem1);
                }

                ddlReceiptRecords.SelectedIndex = 0;
            }
        }

        protected void ddlReceiptRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            string uid = HttpContext.Current.User.Identity.Name;
            try
            {
                dataAccess.OpenConnection();

                if (ddlReceiptRecords.SelectedIndex == 1)
                {
                    dataAccess.SpRemoveAllReceipts(uid);
                }
                else if (ddlReceiptRecords.SelectedIndex > 1)
                {
                    dataAccess.SpRemoveReceipt(uid, ddlReceiptRecords.SelectedItem.Text);
                }
               
                Response.Redirect(Request.RawUrl);
            }
            catch (SqlException ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                dataAccess.CloseConnection();
            }
                
        }
    }
}