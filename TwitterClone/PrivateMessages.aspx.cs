using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TwitterClone
{
    public partial class PrivateMessages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            privateMessage.PMText = "This is private message text";
            privateMessage.PMSubject = "Subject";
            privateMessage.PMSender = "SenderUsername";
            privateMessage.PMDate = "1/1/2020";
        }
    }
}