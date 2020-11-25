using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary;

namespace TwitterClone
{
    public partial class Login : System.Web.UI.Page
    {
        string[] securityQuestions = { "What is your oldest sibling's name?",
            "What is the name of your elementary school?",
            ""};
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
            divForgotPassword.Visible = false;
            divForgotUsername.Visible = false;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            divRegister.Visible = true;
            divLogin.Visible = false;
            divForgotPassword.Visible = false;
            divForgotUsername.Visible = false;
        }

        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            divLogin.Visible = false;
            divForgotPassword.Visible = true;
        }

        protected void lnkForgotUsername_Click(object sender, EventArgs e)
        {
            divLogin.Visible = false;
            divForgotUsername.Visible = true;
        }

        protected void btnSubmitLogin_Click(object sender, EventArgs e)
        {
            string username = txtLogUsername.Text;
            string password = txtLogPassword.Text;
            bool good = true;
            if (username == "")
            {
                smlLogUsernameHelp.InnerText = "Please enter your username";
                good = false;
            }
            else
            {
                smlLogUsernameHelp.InnerText = "";
            }
            if (password == "")
            {
                smlLogPasswordHelp.InnerText = "Please enter your password";
                good = false;
            }
            else
            {
                smlLogPasswordHelp.InnerText = "";
            }
            if (!good)
            {
                return;
            }

            UserService.UserService proxy = new UserService.UserService();

            bool verify = proxy.ValidateUser(username, password);
            if (!verify)
            {
                smlLogPasswordHelp.InnerText = "Username or password is incorrect, try again";
                return;
            }
            else
            {
                smlLogPasswordHelp.InnerText = "";
            }


            Session["Username"] = txtLogUsername.Text;
            if (chkLoginCookie.Checked)
            {
                Response.Cookies["Username"].Value = txtLogUsername.Text;
            }

            Response.Redirect("Home.aspx");
        }

        protected void btnSubmitRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegUsername.Text;
            string password = txtRegPassword.Text;
            string firstName = txtRegFirstName.Text;
            string lastName = txtRegLastName.Text;
            string emailAddress = txtRegEmail.Text;
            string homeAddress = txtRegHomeAddress.Text;
            string billingAddress = txtRegBillingAddress.Text;
            int phoneNumber;
            string profileImage = txtRegImage.Text;
            string securityQuestion1 = txtRegSecurity1.Text;
            string securityQuestion2 = txtRegSecurity2.Text;
            string securityQuestion3 = txtRegSecurity3.Text;
            bool good = true;
            if (username == "")
            {
                smlRegUsernameHelp.InnerText = "Please enter a username";
                good = false;
            }
            else
            {
                smlRegUsernameHelp.InnerText = "";
            }
            if (password == "")
            {
                smlRegPasswordHelp.InnerText = "Please enter a password";
                good = false;
            }
            else
            {
                smlRegPasswordHelp.InnerText = "";
            }
            if (firstName == "")
            {
                smlRegFirstNameHelp.InnerText = "Please enter a first name";
                good = false;
            }
            else
            {
                smlRegFirstNameHelp.InnerText = "";
            }
            if (lastName == "")
            {
                smlRegLastNameHelp.InnerText = "Please enter a last name";
                good = false;
            }
            else
            {
                smlRegLastNameHelp.InnerText = "";
            }
            if (emailAddress == "")
            {
                smlRegEmailHelp.InnerText = "Please enter an email address";
                good = false;
            }
            else
            {
                smlRegEmailHelp.InnerText = "";
            }
            if (homeAddress == "")
            {
                smlRegHomeAddressHelp.InnerText = "Please enter a home address";
                good = false;
            }
            else
            {
                smlRegHomeAddressHelp.InnerText = "";
            }
            if (billingAddress == "")
            {
                smlRegBillingAddressHelp.InnerText = "Please enter a billing address";
                good = false;
            }
            else
            {
                smlRegBillingAddressHelp.InnerText = "";
            }
            if (!Int32.TryParse(txtRegPhone.Text, out phoneNumber))
            {
                smlRegPhoneHelp.InnerText = "Please enter a valid phone number";
                good = false;
            }
            else
            {
                smlRegPhoneHelp.InnerText = "";
            }
            if (profileImage == "")
            {
                smlRegImageHelp.InnerText = "Please enter a valid image URL";
                good = false;
            }
            else
            {
                smlRegImageHelp.InnerText = "";
            }
            if (securityQuestion1 == "")
            {
                smlRegQuestion1Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlRegQuestion1Help.InnerText = "";
            }
            if (securityQuestion2 == "")
            {
                smlRegQuestion2Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlRegQuestion2Help.InnerText = "";
            }
            if (securityQuestion3 == "")
            {
                smlRegQuestion3Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlRegQuestion3Help.InnerText = "";
            }

            if (!good)
            {
                return;
            }

            UserService.UserService proxy = new UserService.UserService();
            bool validateUsername = proxy.ValidateUsername(username);
            if (!validateUsername)
            {
                smlRegUsernameHelp.InnerText = "Username already taken, pick a new one";
                return;
            }
            else
            {
                smlRegUsernameHelp.InnerText = "";
            }

            // FINISH THIS
            int[] secretquestions = { 1, 2, 3 };
            string[] secretAnswers = { "test", "test", "test" };
            User user = new User(username, firstName, lastName, password, profileImage, homeAddress, billingAddress, emailAddress, phoneNumber.ToString(), 1 , secretAnswers.ToString());


            Session["Username"] = txtRegUsername.Text;
            if (chkRegCookie.Checked)
            {
                Response.Cookies["Username"].Value = txtRegUsername.Text;
            }

        }

        protected void btnFindPassword_Click(object sender, EventArgs e)
        {

        }
    }
}