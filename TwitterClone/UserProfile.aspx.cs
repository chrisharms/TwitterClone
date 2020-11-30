﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary;

namespace TwitterClone
{
    public partial class UserProfile : System.Web.UI.Page
    {
        string[] securityQuestions = { "What is your oldest sibling's name?",
            "What is the name of the elementary school you want to?",
            "What was the name of your favorite pet?",
            "What is your favorite color?",
            "What is the name of your first employer?",
            "What is the name of your favorite movie?"};

        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            UserService.UserService proxy = new UserService.UserService();
            UserService.User proxyUser = proxy.GetUser(username);
            User user = new User(proxyUser.Username, proxyUser.FirstName, proxyUser.LastName, proxyUser.Password,
                proxyUser.ProfileImage, proxyUser.HomeAddress, proxyUser.BillingAddress, proxyUser.EmailAddress,
                proxyUser.Phone, proxyUser.SecretQuestions, proxyUser.SecretAnswers, proxyUser.Verified);

            imgProfileImage.ImageUrl = user.ProfileImage;
            lblUsername.InnerText = user.Username;
            lblFirstName.InnerText = user.FirstName;
            lblLastName.InnerText = user.LastName;
            lblEmail.InnerText = user.EmailAddress;
            lblPhone.InnerText = user.Phone;
            lblHomeAddress.InnerText = user.HomeAddress;
            lblBillingAddress.InnerText = user.BillingAddress;
            string[] questions = user.SecretQuestions.Split(',');
            lblSecurityQuestion1.InnerText = securityQuestions[Int32.Parse(questions[0])];
            lblSecurityQuestion2.InnerText = securityQuestions[Int32.Parse(questions[1])];
            lblSecurityQuestion3.InnerText = securityQuestions[Int32.Parse(questions[2])];

            // Getting Posts
            string url = "https://localhost:44312/api/User/GetUserPosts/" + username;
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String data = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();

            List<Post> posts = js.Deserialize<List<Post>>(data);
        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            UserService.UserService proxy = new UserService.UserService();
            UserService.User user = proxy.GetUser(username);
            string[] questions = user.SecretQuestions.Split(',');
            string[] answers = user.SecretAnswers.Split(',');
            txtUsername.Text = user.Username;
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtHomeAddress.Text = user.HomeAddress;
            txtBillingAddress.Text = user.BillingAddress;
            txtImage.Text = user.ProfileImage;
            txtPhone.Text = user.Phone;
            txtSecurity1.Text = answers[0];
            txtSecurity2.Text = answers[1];
            txtSecurity3.Text = answers[2];
            ddlSecurity1.SelectedValue = questions[0];
            ddlSecurity2.SelectedValue = questions[1];
            ddlSecurity3.SelectedValue = questions[2];

            divMyProfile.Visible = false;
            divUpdateProfile.Visible = true;
            divPostContainer.Visible = false;
        }

        protected void lnkDeleteProfile_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            UserService.UserService proxy = new UserService.UserService();
            bool delete = proxy.DeleteUser(username);
            if (!delete)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Profile delete failed, please contact a developer.')", true);
            }
            else
            {
                if (Request.Cookies["Username"] != null)
                {
                    Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
                }
                Session.Abandon();
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            string oldUsername = Session["Username"].ToString();
            string username = txtUsername.Text;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string homeAddress = txtHomeAddress.Text;
            string billingAddress = txtBillingAddress.Text;
            long phoneNumber;
            string profileImage = txtImage.Text;
            string securityQuestion1 = txtSecurity1.Text;
            string securityQuestion2 = txtSecurity2.Text;
            string securityQuestion3 = txtSecurity3.Text;
            string secretAnswers = securityQuestion1 + "," + securityQuestion2 + "," + securityQuestion3;
            string secretQuestions = ddlSecurity1.SelectedValue + "," + ddlSecurity2.SelectedValue + "," + ddlSecurity3.SelectedValue;
            bool good = true;

            if (username == "")
            {
                smlUsernameHelp.InnerText = "Please enter a username";
                good = false;
            }
            else
            {
                smlUsernameHelp.InnerText = "";
            }
            if (firstName == "")
            {
                smlFirstNameHelp.InnerText = "Please enter a first name";
                good = false;
            }
            else
            {
                smlFirstNameHelp.InnerText = "";
            }
            if (lastName == "")
            {
                smlLastNameHelp.InnerText = "Please enter a last name";
                good = false;
            }
            else
            {
                smlLastNameHelp.InnerText = "";
            }
            if (homeAddress == "")
            {
                smlHomeAddressHelp.InnerText = "Please enter a home address";
                good = false;
            }
            else
            {
                smlHomeAddressHelp.InnerText = "";
            }
            if (billingAddress == "")
            {
                smlBillingAddressHelp.InnerText = "Please enter a billing address";
                good = false;
            }
            else
            {
                smlBillingAddressHelp.InnerText = "";
            }
            if (!Int64.TryParse(txtPhone.Text, out phoneNumber))
            {
                smlPhoneHelp.InnerText = "Please enter a valid phone number";
                good = false;
            }
            else
            {
                smlPhoneHelp.InnerText = "";
            }
            if (profileImage == "")
            {
                smlImageHelp.InnerText = "Please enter a valid image URL";
                good = false;
            }
            else
            {
                smlImageHelp.InnerText = "";
            }
            if (securityQuestion1 == "")
            {
                smlQuestion1Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlQuestion1Help.InnerText = "";
            }
            if (securityQuestion2 == "")
            {
                smlQuestion2Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlQuestion2Help.InnerText = "";
            }
            if (securityQuestion3 == "")
            {
                smlQuestion3Help.InnerText = "Please enter a question response";
                good = false;
            }
            else
            {
                smlQuestion3Help.InnerText = "";
            }

            if (!good)
            {
                return;
            }

            UserService.UserService proxy = new UserService.UserService();
            bool validateUsername = proxy.ValidateUsername(username);
            if (!validateUsername && username != Session["Username"].ToString())
            {
                smlUsernameHelp.InnerText = "New username already taken, pick a new one";
                return;
            }
            else
            {
                smlUsernameHelp.InnerText = "";
            }

            UserService.User user1 = new UserService.User();
            user1.Username = username;
            user1.FirstName = firstName;
            user1.LastName = lastName;
            user1.HomeAddress = homeAddress;
            user1.BillingAddress = billingAddress;
            user1.Phone = phoneNumber.ToString();
            user1.ProfileImage = profileImage;
            user1.SecretQuestions = secretQuestions;
            user1.SecretAnswers = secretAnswers;


            bool updateUser = proxy.UpdateUser(oldUsername, user1);
            if (!updateUser)
            {
                smlUsernameHelp.InnerText = "User profile update failed, try again later";
                return;
            }
            else
            {
                smlUsernameHelp.InnerText = "";
                Session["Username"] = username;
                if (Request.Cookies["Username"] != null)
                {
                    Response.Cookies["Username"].Value = username;
                }
            }

            divMyProfile.Visible = true;
            divUpdateProfile.Visible = false;
            divPostContainer.Visible = true;
        }

        protected void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            divMyProfile.Visible = true;
            divUpdateProfile.Visible = false;
            divPostContainer.Visible = true;
        }
    }
}