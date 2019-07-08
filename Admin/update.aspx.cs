using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;

using BusinessTier;
using DataAccessTier;

namespace Lottery.Admin
{
    public partial class update : System.Web.UI.Page
    {
        private clsLotto lotto;
        private string fileName = " ";
        Database db;
        DataAccessLayer dataAccessLayer;

        protected void Page_Load(object sender, EventArgs e)
        {
            dataAccessLayer = new DataAccessLayer();
            if (!IsPostBack)
            {
                
                SqlDataReader reader;
                try
                {
                    dataAccessLayer.OpenConnection();
                    reader = dataAccessLayer.SpGetLottoName();
                    while (reader.Read())
                    {
                        DBDl3.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
                    }
                }
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    dataAccessLayer.CloseConnection();
                }
            }

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

            lblResult.Text = "";

            try
            {
                lotto = new BusinessTier.clsLotto(db, "");
                string stmt = lotto.retrieveGroup(drawno, op);

                lblResult.Text += stmt;
            }
            catch (Exception exp)
            {
                lblError.Text = exp.Message;
            }
        }
        protected void uploadBtn_Click(object sender, EventArgs e)
        {
            FileUpload1.Visible = true;
            UploadButton.Visible = true;
            uploadBtn.Visible = false;

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (FileUpload1.FileName == "")
                fileName = "J:\\temp\\649.csv";
            else
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                fileName = filePath + FileUpload1.FileName;
                FileUpload1.SaveAs(fileName);
                //string path = System.Web.HttpContext.Current.Server.MapPath("\\"); 

            }

            lblError.Text = fileName;

            LoadToDatabase(fileName);
        }

        private void LoadToDatabase(string fileName)
        {
            string delimiter = ",";
            
            db = Database.LottoMax;
            if (fileName.Contains("649"))
            {
                db = Database.Lottery;
            }
            else if (fileName.Contains("BC49"))
            {
                db = Database.BC49;
            }
            else if (fileName.Contains("FloridaLotto"))
            {
                db = Database.FloridaLotto;
                delimiter = " ";
            }
            else if (fileName.Contains("MegaMillions"))
            {
                db = Database.MegaMillions;
                delimiter = " ";
            }
            else if (fileName.Contains("PowerBall"))
            {
                db = Database.PowerBall;
                delimiter = " ";
            }
            else if (fileName.Contains("NYLotto"))
            {
                db = Database.NYLotto;
                delimiter = " ";
            }

            dataAccessLayer.OpenConnection();
            try
            {   
                dataAccessLayer.CleanDb(db);

                StreamReader sr = new StreamReader(fileName);

                if (db == Database.Lottery || db == Database.BC49 || db == Database.LottoMax)
                    sr.ReadLine(); // skip first Row 

                while (!sr.EndOfStream)
                {
                    string r = sr.ReadLine();
                    string[] items = r.Split(delimiter.ToCharArray());

                    switch (db)
                    {
                        case Database.FloridaLotto:
                        case Database.NYLotto:
                            dataAccessLayer.ModifyDb_FloridaLotto(items, DbMode.Add, db);
                            break;
                        case Database.MegaMillions:
                        case Database.PowerBall:
                            dataAccessLayer.ModifyDb_MegaMillions_PowerBall(items, DbMode.Add, db);
                            break;
                        case Database.BC49:
                        case Database.Lottery:
                        case Database.LottoMax:
                            dataAccessLayer.ModifyDb(items, DbMode.Add, db);
                            break;

                        default:
                            lblError.Text = "Undefined database table name";
                            break;                     
                    }
                }
                lblError.Text = string.Format("File: {0} has been successfully loaded to Database!", fileName);
            }
            catch (SqlException ex)
            {
                lblError.Text = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                lblError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                lblError.Text += "<br />Database execution time: " + dataAccessLayer.CloseSqlConnection().ToString() + " ms";
                dataAccessLayer.CloseSqlConnection();
            }
        }

    }
}
