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
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            UserService.UserService proxy = new UserService.UserService();
            UserService.User proxyUser = proxy.GetUser(username);
            User user = new User(proxyUser.Username, proxyUser.FirstName, proxyUser.LastName, proxyUser.Password,
                proxyUser.ProfileImage, proxyUser.HomeAddress, proxyUser.BillingAddress, proxyUser.EmailAddress,
                proxyUser.Phone, proxyUser.SecretQuestions, proxyUser.SecretAnswers, proxyUser.Verified);

        }
    }
}