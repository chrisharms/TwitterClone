using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["Username"] != null)
            {
                // Username cookie exists, user doesn't need to log in again
                Session["Username"] = Request.Cookies["Username"].Value;
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            divLogin.Visible = true;
            divRegister.Visible = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            divRegister.Visible = true;
            divLogin.Visible = false;
        }
    }
}