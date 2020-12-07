using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class Verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mail"] == "true" && Request.QueryString["uname"] != null)
            {
                
            }
            else if ((Session["Username"] == null && Session["Guest"] == null && Request.QueryString["mail"] == null) || (Session["Username"] == null && Session["Guest"] == null && Request.QueryString["mail"] != null))
            {
                Response.Redirect("Login.aspx");
            }
            else if ((Session["Guest"] != null || Session["Username"] != null) && Request.QueryString["mail"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            else if (Session["Guest"] != null && Request.QueryString["mail"] != null)
            {
                Response.Redirect("Home.aspx");
            }

            string username;
            string mail = Request.QueryString["mail"].ToString();
            if (mail == "true")
            {
                if (Request.QueryString["uname"] == null)
                {
                    Response.Redirect("Verification.aspx?mail=false");
                }
                pAppVerify.Visible = false;
                btnLogout.Visible = false;
                pVerified.Visible = false;
                btnLogin.Visible = false;
                username = Request.QueryString["uname"].ToString();
            }
            else
            {
                pEmailVerify.Visible = false;
                btnVerifyUser.Visible = false;
                pVerified.Visible = false;
                btnLogin.Visible = false;
                username = Session["Username"].ToString();
            }
            h5Username.InnerText = "Username: " + username;

            UserService.UserService proxy = new UserService.UserService();
            bool verified = proxy.IsUserVerified(username);
            if (verified)
            {
                pAppVerify.Visible = false;
                btnLogout.Visible = false;
                pEmailVerify.Visible = false;
                btnVerifyUser.Visible = false;
                btnLogin.Visible = true;
                pVerified.Visible = true;
            }
            if (verified && Session["Username"] != null)
            {
                Response.Redirect("Home.aspx");
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

        protected void btnVerifyUser_Click(object sender, EventArgs e)
        {
            string username = Request.QueryString["uname"].ToString();
            UserService.UserService proxy = new UserService.UserService();
            bool verify = proxy.UpdateUserVerification(username);
            if (verify == true)
            {
                Response.Redirect("Home.aspx");
                Session["Username"] = username;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}