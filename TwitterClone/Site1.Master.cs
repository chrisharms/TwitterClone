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
            //Check if object exists
            if (Session["LoginStatus"] != null)
            {
                //Set Session to true on login page after succesful login. 
                if ((bool)Session["LoginStatus"])
                {
                    //Enable/Disable navbar links depending on result
                }
            }
            else
            {
                //Send back to homepage because they're not logged in
                Server.Transfer("Login.aspx"); 
            }
        }
    }
}