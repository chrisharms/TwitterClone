using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TwitterClassLibrary;
namespace TwitterClone
{
    public partial class PrivateMessages : System.Web.UI.Page
    {
        string currentUsername;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null && Session["Guest"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (Session["Username"] != null)
            {
                currentUsername = Session["Username"].ToString();
                UserService.UserService proxy = new UserService.UserService();
                bool verified = proxy.IsUserVerified(currentUsername);
                if (!verified)
                {
                    Response.Redirect("Verification.aspx?mail=false");
                }
            }

            List<TwitterClassLibrary.Models.PrivateMessage> usersPms = new List<TwitterClassLibrary.Models.PrivateMessage>();
            usersPms = DBSerialize.ReadSerializedPM(currentUsername);
            if(usersPms.Count == 0)
            {
                lblRepeaterMessage.Text = "You don't have any private messages!";
            }
            else
            {
                repeaterPMs.DataSource = usersPms;
                repeaterPMs.DataBind();
            }


        }

        protected void repeaterPMs_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            TwitterClassLibrary.Models.PrivateMessage pmData = e.Item.DataItem as TwitterClassLibrary.Models.PrivateMessage;
            RepeaterItem ri = e.Item;
            Label lblUsername = ri.FindControl("lblUsername") as Label;
            Label lblDate = ri.FindControl("lblDate") as Label;
            HtmlGenericControl subject = ri.FindControl("pmSubject") as HtmlGenericControl;
            HtmlGenericControl text = ri.FindControl("pmText") as HtmlGenericControl;
            lblDate.Text = pmData.DateSent;
            lblUsername.Text = pmData.SenderUsername;
            text.InnerText = pmData.Text;
            subject.InnerText = pmData.Subject;
        }

        protected void btnComposeNew_Click(object sender, EventArgs e)
        {
            divComposeNewPM.Visible = !divComposeNewPM.Visible;
            lblNewPmError.Text = "";
        }

        protected void btnSendNewPm_Click(object sender, EventArgs e)
        {
            string reciever = txtRecipient.Text;
            if(reciever.Equals(currentUsername) || string.IsNullOrEmpty(reciever))
            {
                lblNewPmError.Text = "Invalid Recipient";
                return;
            }

            UserService.UserService proxy = new UserService.UserService();
            bool validateUsername = proxy.ValidateUsername(reciever);
            if (validateUsername)
            {
                lblNewPmError.Text = "Recipient does not exist, check spelling or try a different user";
                return;
            }

            string subject = txtSubject.Text;
            if (string.IsNullOrEmpty(subject))
            {
                lblNewPmError.Text = "Invalid Subject";
                return;
            }

            string message = taPMText.InnerText;
            if (string.IsNullOrEmpty(message))
            {
                lblNewPmError.Text = "Must have a message";
                return;
            }

            TwitterClassLibrary.Models.PrivateMessage pm = new TwitterClassLibrary.Models.PrivateMessage(0, currentUsername, reciever, DateTime.Now.ToString(), subject, message);
            DBSerialize.WriteSerializedPM(pm);
            divComposeNewPM.Visible = false;
            lblNewPmError.Text = "Message sent.";

        }
    }
}