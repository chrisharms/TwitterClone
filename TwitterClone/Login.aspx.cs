using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary;

namespace TwitterClone
{
    public partial class Login : System.Web.UI.Page
    {
        string[] securityQuestions = { "What is your oldest sibling's name?",
            "What is the name of the elementary school you want to?",
            "What was the name of your favorite pet?",
            "What is your favorite color?",
            "What is the name of your first employer?",
            "What is the name of your favorite movie?"};
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
            string username = txtLogUsername.Text;
            if (username == "")
            {
                smlLogUsernameHelp.InnerText = "Enter your username to retrieve password";
                return;
            }
            else
            {
                smlLogUsernameHelp.InnerText = "";
            }

            UserService.UserService proxy = new UserService.UserService();
            bool verify = proxy.ValidateUsername(username);
            if (verify)
            {
                smlLogUsernameHelp.InnerText = "Username is not registered";
                return;
            }

            UserService.User serviceUser = proxy.GetUser(username);
            User recoverUser = new User(serviceUser.Username, serviceUser.FirstName, serviceUser.LastName,
                serviceUser.Password, serviceUser.ProfileImage, serviceUser.HomeAddress, serviceUser.BillingAddress,
                serviceUser.EmailAddress, serviceUser.Phone, serviceUser.SecretQuestions, serviceUser.SecretAnswers, serviceUser.Verified);

            int arrayIndex;
            int secretQuestion = recoverUser.GetRandomQuestion(out arrayIndex);
            lblPasswordSecretQuestion.InnerText = securityQuestions[secretQuestion];

            Session["UsernameRetrieve"] = username;
            Session["RetrievedPassword"] = recoverUser.Password;
            Session["SecretAnswer"] = recoverUser.GetSecretAnswer(arrayIndex);
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
            long phoneNumber;
            string profileImage = txtRegImage.Text;
            string securityQuestion1 = txtRegSecurity1.Text;
            string securityQuestion2 = txtRegSecurity2.Text;
            string securityQuestion3 = txtRegSecurity3.Text;
            string secretAnswers = securityQuestion1 + "," + securityQuestion2 + "," + securityQuestion3;
            string secretQuestions = ddlSecurity1.SelectedValue + "," + ddlSecurity2.SelectedValue + "," + ddlSecurity3.SelectedValue;
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
            if (!Int64.TryParse(txtRegPhone.Text, out phoneNumber))
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

            bool validateEmail = proxy.ValidateEmail(emailAddress);
            if (!validateEmail)
            {
                smlRegEmailHelp.InnerText = "Email is taken, please try again with a new one";
                return;
            }
            else
            {
                smlRegEmailHelp.InnerText = "";
            }

            
            UserService.User user1 = new UserService.User();
            user1.Username = username;
            user1.FirstName = firstName;
            user1.LastName = lastName;
            user1.Password = password;
            user1.EmailAddress = emailAddress;
            user1.HomeAddress = homeAddress;
            user1.BillingAddress = billingAddress;
            user1.Phone = phoneNumber.ToString();
            user1.ProfileImage = profileImage;
            user1.SecretQuestions = secretQuestions;
            user1.SecretAnswers = secretAnswers;
            user1.Verified = "false";


            bool addUser = proxy.AddUser(user1);
            if (!addUser)
            {
                smlRegUsernameHelp.InnerText = "User registration failed, try again later";
                return;
            }
            else
            {
                smlRegUsernameHelp.InnerText = "";
            }

            Session["Username"] = txtRegUsername.Text;
            if (chkRegCookie.Checked)
            {
                Response.Cookies["Username"].Value = txtRegUsername.Text;
            }

            MailMessage verificationMail = new MailMessage(new MailAddress("tug92197@temple.edu"), new MailAddress(emailAddress));
            verificationMail.Subject = "Not Twitter: New Account Verification";
            verificationMail.Body = "http://localhost:62631/Verification.aspx?username=" + username;
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
        }

        protected void btnFindPassword_Click(object sender, EventArgs e)
        {
            string userAnswer = txtSecretAnswer.Text;
            if (userAnswer == "")
            {
                smlForgotPasswordHelp.InnerText = "Please answer the security question";
                return;
            }
            else
            {
                smlForgotPasswordHelp.InnerText = "";
            }
            
            if (userAnswer == Session["SecretAnswer"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Recovery Successful! Your Password = " + Session["RetrievedPassword"] + "')", true);
            }
            else
            {
                smlForgotPasswordHelp.InnerText = "Answer incorrect, try again";
                return;
            }
        }

        protected void btnVerifyEmail_Click(object sender, EventArgs e)
        {
            string email = txtVerifyEmail.Text;
            if (email == "")
            {
                smlVerifyEmailHelp.InnerText = "Please enter your email address";
                return;
            }
            else
            {
                smlVerifyEmailHelp.InnerText = "";
            }

            UserService.UserService proxy = new UserService.UserService();
            bool verifyEmail = proxy.ValidateEmail(email);
            if (verifyEmail)
            {
                smlVerifyEmailHelp.InnerText = "Email does not belong to a profile, try again";
                return;
            }
            else
            {
                smlVerifyEmailHelp.InnerText = "";
            }

            UserService.User serviceUser = proxy.GetUserByEmail(email);
            User recoverUser = new User(serviceUser.Username, serviceUser.FirstName, serviceUser.LastName,
                serviceUser.Password, serviceUser.ProfileImage, serviceUser.HomeAddress, serviceUser.BillingAddress,
                serviceUser.EmailAddress, serviceUser.Phone, serviceUser.SecretQuestions, serviceUser.SecretAnswers, serviceUser.Verified);

            int arrayIndex;
            int secretQuestion = recoverUser.GetRandomQuestion(out arrayIndex);
            lblUsernameSecretQuestion.InnerText = securityQuestions[secretQuestion];

            Session["UsernameRetrieve"] = recoverUser.Username;
            Session["SecretAnswer"] = recoverUser.GetSecretAnswer(arrayIndex);

            divUsernameSecretQuestion.Visible = true;
        }

        protected void btnRetrieveUsername_Click(object sender, EventArgs e)
        {
            string userAnswer = txtUsernameSecretAnswer.Text;
            if (userAnswer == "")
            {
                smlForgotUsernameHelp.InnerText = "Please answer the security question";
                return;
            }
            else
            {
                smlForgotUsernameHelp.InnerText = "";
            }

            if (userAnswer == Session["SecretAnswer"].ToString())
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Recovery Successful! Your Username = " + Session["UsernameRetrieve"] + "')", true);
            }
            else
            {
                smlForgotUsernameHelp.InnerText = "Answer incorrect, please try again";
                return;
            }
        }

        protected void lnkGuest_Click(object sender, EventArgs e)
        {
            Session["Guest"] = "true";
            Response.Redirect("Home.aspx");
        }
    }
}