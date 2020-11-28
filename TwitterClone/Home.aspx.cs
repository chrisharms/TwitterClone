using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class Homepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 

            pcPost.PostText = "This is some text";
            pcPost.PostImage = "https://help.twitter.com/content/dam/help-twitter/brand/logo.png";
            pcPost.PostTitle = "This is a customer user control";
        }
    }
}