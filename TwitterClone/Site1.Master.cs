using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null && Session["Guest"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["Username"] != null)
            {
                lblUserInfo.Text = "@" + Session["Username"].ToString();
                lnkBtnLogin.Visible = false;
                string username = Session["Username"].ToString();
                UserService.UserService proxy = new UserService.UserService();
                bool verified = proxy.IsUserVerified(username);
                if (!verified)
                {
                    Response.Redirect("Verification.aspx?mail=false");
                }
            }
            if (Session["Guest"] != null)
            {
                lnkBtnLogout.Visible = false;
                lnkMyProfile.Visible = false;
                lblUserInfo.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            if (Request.Cookies["Username"] != null)
            {
                Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Redirect("Login.aspx");
        }

        protected void lnkBtnLogin_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}