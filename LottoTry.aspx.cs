using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DataAccessTier;

namespace Lottotry
{
    public partial class LottoTry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataAccessLayer dataAccess = new DataAccessLayer();
            SqlDataReader reader;

            ContentPlaceHolder placeHolder = (ContentPlaceHolder)Page.Master.FindControl("cphcontent");
            Label lbl = (Label) placeHolder.FindControl("lblLottoList");
            try
            {
                
                lbl.Text = "<ol><br>";
                reader = dataAccess.SpGetLottoName();
                while (reader.Read())
                {
                    lbl.Text += string.Format("<li><a href='{0}'>{1}</a></li>", reader["links"].ToString(), reader["name"].ToString());
                }
                lbl.Text += "</ol><br>";
            }
            catch (Exception ex)
            {
                lbl.Text = ex.Message;
            }
            finally
            {
                dataAccess.CloseConnection();
            }
        }
    }
}