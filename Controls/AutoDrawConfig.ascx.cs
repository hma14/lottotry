using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lottotry.Controls
{
    public partial class AutoDrawConfig : System.Web.UI.UserControl
    {
        private static bool autoDrawCacheNeedUpdate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {// Fill ddlSelectMode Drop Down List
                for (int i = 0; i < 4; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            ddlSelectMode.Items.Insert(i, new ListItem("Semi Hot Numbers", i.ToString()));
                            break;
                        case 1:
                            ddlSelectMode.Items.Insert(i, new ListItem("Hot Numbers", i.ToString()));
                            break;
                        case 2:
                            ddlSelectMode.Items.Insert(i, new ListItem("Mix", i.ToString()));
                            break;
                        case 3:
                            ddlSelectMode.Items.Insert(i, new ListItem("Number Range", i.ToString()));
                            break;
                        default:
                            break;
                    }
                }

                // Fill ddlRange Drop Down List
                for (int i = 0; i < 6; ++i)
                {
                    switch (i)
                    {
                        case 0:
                            ddlRange.Items.Insert(i, new ListItem("1 - 9", i.ToString()));
                            break;
                        case 1:
                            ddlRange.Items.Insert(i, new ListItem("10 - 19", i.ToString()));
                            break;
                        case 2:
                            ddlRange.Items.Insert(i, new ListItem("20 - 29", i.ToString()));
                            break;
                        case 3:
                            ddlRange.Items.Insert(i, new ListItem("30 - 39", i.ToString()));
                            break;
                        case 4:
                            ddlRange.Items.Insert(i, new ListItem("40 - 49", i.ToString()));
                            break;
                        case 5:
                            ddlRange.Items.Insert(i, new ListItem("50 - 59", i.ToString()));
                            break;
                        default:
                            break;

                    }
                }
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
            
        }
        
        protected void ddlSelectMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectMode.SelectedIndex == 3)
            {
                ddlRange.Visible = true;
                lblRange.Visible = true;
            }
            else
            {
                ddlRange.Visible = false;
                lblRange.Visible = false;

            }
            AutoDrawCacheNeedUpdate = true;

        }

        protected void ddlRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
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

        public DropDownList DdlSelectMode
        {
            get { return ddlSelectMode; }
            set { ddlSelectMode = value; }
        }

        public DropDownList DdlRange
        {
            get { return ddlRange; }
            set { ddlRange = value; }
        }

        

        protected void tbHot_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbHotMin_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbHotMax_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHot_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHotMin_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiHotMax_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiCold_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiColdMin_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbSemiColdMax_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbCold_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbColdMin_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbColdMax_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbVeryCold_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbVeryColdMin_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        protected void tbVeryHot_TextChanged(object sender, EventArgs e)
        {
            AutoDrawCacheNeedUpdate = true;
        }

        public bool AutoDrawCacheNeedUpdate
        {
            get { return autoDrawCacheNeedUpdate; }
            set { autoDrawCacheNeedUpdate = value; }
        }
    }
}