using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary.DBObjCreator;

namespace TwitterClone
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        const string LOGO = "Logo";
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
            InitializeWebAssets();
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

        private void InitializeWebAssets()
        {
            Exception ex = null;
            List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            filter.Add(DBObjCreator.CreateFilter("Key", LOGO, typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetAsset", ref ex, filter);
            string serializedData = (string)records[0][2];
            WebAssetSerializer.DeserializeImage(serializedData, imgLogo);
            
        }
    }
}