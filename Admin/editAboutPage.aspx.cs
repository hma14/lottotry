using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccessTier;

namespace Lottotry.Admin
{
    public partial class editAboutPage : System.Web.UI.Page
    {
        DataAccessLayer dbManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = new DataAccessLayer();
            if (!IsPostBack)
            {
                string txt = dbManager.spGetAboutContent();
                txtTitle.Text = txt.Replace("<br />", "\n");
                lblLiveText.Text = txt;
            }
        }

        protected void lbtnBold_Click(object sender, EventArgs e)
        {
            txtTitle.Text = txtTitle.Text.Replace(hfSelected.Value, "<b>" + hfSelected.Value + "</b>");
            lblLiveText.Text = txtTitle.Text.Replace("\n", "<br />");

        }

        protected void lbtnItalics_Click(object sender, EventArgs e)
        {
            txtTitle.Text = txtTitle.Text.Replace(hfSelected.Value, "<i>" + hfSelected.Value + "</i>");
            lblLiveText.Text = txtTitle.Text.Replace("\n", "<br />");

        }

        protected void lbtnUnderline_Click(object sender, EventArgs e)
        {
            txtTitle.Text = txtTitle.Text.Replace(hfSelected.Value, "<u>" + hfSelected.Value + "</u>");
            lblLiveText.Text = txtTitle.Text.Replace("\n", "<br />");


        }

        protected void lbtnPreview_Click(object sender, EventArgs e)
        {
            lblLiveText.Text = txtTitle.Text.Replace("\n", "<br />");
        }

        protected void txtTitle_TextChanged(object sender, EventArgs e)
        {
            dbManager.OpenConnection();
            dbManager.spPublishAboutContent(txtTitle.Text.Replace("\n", "<br />"));
            dbManager.CloseConnection();
        }

        protected void lblPublish_Click(object sender, EventArgs e)
        {
            
            dbManager.OpenConnection();
            dbManager.spPublishAboutContent(txtTitle.Text.Replace("\n", "<br />"));
            dbManager.CloseConnection();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            txtLinkTargetURL.Visible = true;
            txtLinkText.Visible = true;

            txtLinkText.Text = "";
            txtLinkTargetURL.Text = "";

            lblLinkTargetURL.Visible = true;
            lblLinkText.Visible = true;
            lbtnCreateLink.Visible = true;

        }

        protected void lbtnCreateLink_Click(object sender, EventArgs e)
        {

            string linkTag = "<a href=" + txtLinkTargetURL.Text + ">"
                + txtLinkText.Text + "</a>\n";
            txtLinkTargetURL.Visible = false;
            txtLinkText.Visible = false;
            lblLinkTargetURL.Visible = false;
            lblLinkText.Visible = false;
            lbtnCreateLink.Visible = false;
            txtTitle.Text += linkTag;

            lblLiveText.Text = txtTitle.Text.Replace("\n", "<br />");

        }


    }
}
