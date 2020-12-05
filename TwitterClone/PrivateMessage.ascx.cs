using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class PrivateMessage : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pmCard.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(pmCard, string.Empty));
            if (IsPostBack && Request["__EVENTTARGET"] == pmCard.UniqueID)
            {
                pmCard_Click(pmCard, EventArgs.Empty);
            }
        }

        private void pmCard_Click(HtmlGenericControl pmCard, EventArgs empty)
        {
            string pmCardClass = pmCard.Attributes["class"];
            pmCardBody.Visible = !pmCardBody.Visible;
            if (pmCardBody.Visible)
            {
                pmCard.Attributes.Add("class", pmCardClass.Replace("pmHover", ""));
            }
            else
            {
                pmCard.Attributes.Add("class", pmCardClass + " pmHover");
            }
        }

        public string PMText
        {
            get { return pmText.InnerText; }
            set { pmText.InnerText = value; }
        }

        public string PMDate
        {
            get { return lblDate.Text; }
            set { lblDate.Text = $"Recieved: {value}"; }
        }

        public string PMSubject
        {
            get { return pmSubject.InnerText; }
            set { pmSubject.InnerText = value; }
        }

        public string PMSender
        {
            get { return lblUsername.Text; }
            set { lblUsername.Text = $"From: {value}"; }
        }

        //protected void btnReply_Click(object sender, EventArgs e)
        //{
        //    replyDiv.Visible = !replyDiv.Visible;
        //}
    }
}