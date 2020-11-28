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
                string username = Session["Username"].ToString();
                UserService.UserService proxy = new UserService.UserService();
                bool verified = proxy.IsUserVerified(username);
                if (!verified)
                {
                    Response.Redirect("Verification.aspx?mail=false");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }
    }
}