using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessTier;



namespace Lottotry.Admin
{
    public partial class LottoDbStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


#if false
        protected void gvLottoStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "UpdateNow")
            {
                int rowIndex = Convert.ToInt16(e.CommandArgument);
                string script = gvLottoStatus.Rows[rowIndex].Cells[1].Text.Trim() + ".py";


                UpdateLottoDbClient client = new UpdateLottoDbClient();               
                try
                {
                    client.RunUpdateDb(script);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message + "<br>" + ex.InnerException.Message ;
                }
                finally
                {
                    client.Close();
                }

            }
        }
#endif

        protected void gvLottoStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "UpdateNow")
            {
                ProcessStartInfo startInfo;
                Process process;
                try
                {
                    int rowIndex = Convert.ToInt16(e.CommandArgument);
                    string arguments = gvLottoStatus.Rows[rowIndex].Cells[1].Text.Trim() + ".py";
                    startInfo = new ProcessStartInfo(@"C:\Python27\python.exe");
                    string directory = Server.MapPath("~/Python");
                    startInfo.WorkingDirectory = directory;
                    startInfo.Arguments = arguments;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = false;
                    startInfo.RedirectStandardError = false;
                    

                    startInfo.UserName = "henryma14@gmail.com";
                    string chars = "m1985425";

                    // Instantiate the secure string.
                    SecureString testString = new SecureString();

                    // Assign the character array to the secure string.
                    foreach (char ch in chars)
                        testString.AppendChar(ch);

                    startInfo.Password = testString;
                    process = new Process();
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
        }
    }
}