using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using DataAccessTier;
using BusinessTier;

namespace Lottery
{
    public partial class UploadExcelFiles : System.Web.UI.Page
    {
        private string fileName = " ";
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.FileName == "")
                fileName = "C:\\temp\\649.csv";
            else
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                fileName = filePath + FileUpload1.FileName;
                FileUpload1.SaveAs(fileName);
                //string path = System.Web.HttpContext.Current.Server.MapPath("\\"); 
                
            }

            Lbl1.Text = fileName;

            LoadToDatabase(fileName);
        }

        private void LoadToDatabase(string fileName)
        {
            string delimiter = ",";
            Database db = Database.LottoMax;
            if (fileName.Contains("649"))
            {
                db = Database.Lottery;
            }
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            try
            {
                dataAccessLayer.CleanDb(db);

                StreamReader sr = new StreamReader(fileName);

                sr.ReadLine(); // skip first Row 
                while (!sr.EndOfStream)
                {
                    string r = sr.ReadLine();
                    string[] items = r.Split(delimiter.ToCharArray());
                    dataAccessLayer.ModifyDb(items, DbMode.Add, db);
                }
                Lbl1.Text = string.Format("File: {0} has been successfully loaded to Database!", fileName);
            }
            catch (SqlException ex)
            {
                Lbl1.Text = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                Lbl1.Text = ex.Message;
            }
            catch (Exception ex)
            {
                Lbl1.Text = ex.Message;
            }
            finally
            {
                Lbl2.Text = "Database execution time: " + dataAccessLayer.CloseSqlConnection().ToString() + " ms";
            }
        }
    }
}
