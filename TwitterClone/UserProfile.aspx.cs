using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            lblName.InnerText = user.FirstName + " " + user.LastName;
            lblEmail.InnerText = user.EmailAddress;
            lblPhone.InnerText = user.Phone;
            lblHomeAddress.InnerText = user.HomeAddress;
            lblBillingAddress.InnerText = user.BillingAddress;
            string[] questionindexes = user.SecretQuestions.Split(',');
            lblSecurityQuestion1.InnerText = securityQuestions[Int32.Parse(questionindexes[0])];
            lblSecurityQuestion2.InnerText = securityQuestions[Int32.Parse(questionindexes[1])];
            lblSecurityQuestion3.InnerText = securityQuestions[Int32.Parse(questionindexes[2])];
        }
    }
}