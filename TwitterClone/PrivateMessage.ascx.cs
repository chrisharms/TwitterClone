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

        public bool PMVisibility
        {
            get { return pmCardBody.Visible; }
            set { pmCardBody.Visible = value; }
        }

        public void AlterCssClass()
        {
            string pmCardClass = pmCard.Attributes["class"];
            if (pmCardBody.Visible)
            {
                pmCard.Attributes.Add("class", pmCardClass.Replace("pmHover", ""));
            }
            else
            {
                pmCard.Attributes.Add("class", pmCardClass + " pmHover");
            }
        }

    }
}