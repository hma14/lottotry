using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessTier;
using BusinessTier;

namespace Lottotry.Controls
{
    public partial class ConfigForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                addAttributes();
            }
        }

        protected void addAttributes()
        {
            tbHot.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiHot.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiCold.Attributes.Add("onclick", "highLightBackColor();");
            tbCold.Attributes.Add("onclick", "highLightBackColor();");
            tbVeryCold.Attributes.Add("onclick", "highLightBackColor();");
            tbHotMin.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiHotMin.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiColdMin.Attributes.Add("onclick", "highLightBackColor();");
            tbColdMin.Attributes.Add("onclick", "highLightBackColor();");
            tbVeryColdMin.Attributes.Add("onclick", "highLightBackColor();");
            tbHotMax.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiHotMax.Attributes.Add("onclick", "highLightBackColor();");
            tbSemiColdMax.Attributes.Add("onclick", "highLightBackColor();");
            tbColdMax.Attributes.Add("onclick", "highLightBackColor();");
            tbVeryHot.Attributes.Add("onclick", "highLightBackColor();");
            tbSumMin.Attributes.Add("onclick", "highLightBackColor();");
            tbSumMax.Attributes.Add("onclick", "highLightBackColor();");
            tbOdds.Attributes.Add("onclick", "highLightBackColor();");
        }


        public string TbHot
        {
            get { return tbHot.Text; }
            set { tbHot.Text = value; }
        }

        public string TbHotMin
        {
            get { return tbHotMin.Text; }
            set { tbHotMin.Text = value; }
        }

        public string TbHotMax
        {
            get { return tbHotMax.Text; }
            set { tbHotMax.Text = value; }
        }

        public string TbSemiHot
        {
            get { return tbSemiHot.Text; }
            set { tbSemiHot.Text = value; }
        }

        public string TbSemiHotMin
        {
            get { return tbSemiHotMin.Text; }
            set { tbSemiHotMin.Text = value; }
        }
        public string TbSemiHotMax
        {
            get { return tbSemiHotMax.Text; }
            set { tbSemiHotMax.Text = value; }
        }


        public string TbCold
        {
            get { return tbCold.Text; }
            set { tbCold.Text = value; }
        }


        public string TbColdMin
        {
            get { return tbColdMin.Text; }
            set { tbColdMin.Text = value; }
        }


        public string TbColdMax
        {
            get { return tbColdMax.Text; }
            set { tbColdMax.Text = value; }
        }


        public string TbSemiCold
        {
            get { return tbSemiCold.Text; }
            set { tbSemiCold.Text = value; }
        }


        public string TbSemiColdMin
        {
            get { return tbSemiColdMin.Text; }
            set { tbSemiColdMin.Text = value; }
        }


        public string TbSemiColdMax
        {
            get { return tbSemiColdMax.Text; }
            set { tbSemiColdMax.Text = value; }
        }

        public string TbVeryCold
        {
            get { return tbVeryCold.Text; }
            set { tbVeryCold.Text = value; }
        }

        public string TbVeryColdMin
        {
            get { return tbVeryColdMin.Text; }
            set { tbVeryColdMin.Text = value; }
        }

        public string TbVeryHot
        {
            get { return tbVeryHot.Text; }
            set { tbVeryHot.Text = value; }
        }

        public string TbSumMin
        {
            get { return tbSumMin.Text; }
            set { tbSumMin.Text = value; }
        }

        public string TbSumMax
        {
            get { return tbSumMax.Text; }
            set { tbSumMax.Text = value; }
        }

        public string TbOdds
        {
            get { return tbOdds.Text; }
            set { tbOdds.Text = value; }
        }


        public void setSumMinMax(Database db)
        {
            int nummbers = Util.getTotalLottoNumbers(db);
            int columns = Util.getColumnnsOfLotto(db);
            if (columns == 1)
            {
                tbSumMin.Text = "5";
                tbSumMax.Text = nummbers.ToString();
                tbOdds.Text = "1";
            }
            else if (nummbers < 40 && columns < 3)
            {
                tbSumMin.Text = "5";
                tbSumMax.Text = "50";
                tbOdds.Text = "1";
            }
            else if (nummbers < 50 && columns < 7)
            {
                tbSumMin.Text = "125";
                tbSumMax.Text = "175";
                tbOdds.Text = "3";
            }
            else
            {
                tbSumMin.Text = "145";
                tbSumMax.Text = "205";
                tbOdds.Text = "3";
            }
        }
    }
}