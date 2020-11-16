using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class User
    {
        private int userid;
        private string firstname;
        private string lastname;
        private string password;
        private string profileimage;
        private string homeaddress;
        private string billingaddress;
        private string emailaddress;
        private int phone;
        private string secretquestion;
        private string secretanswer;

        public User()
        {

        }

        public User(int userid, string firstname, string lastname, string password, string profileimage, string homeaddress, string billingaddress, string emailaddress, int phone, string secretquestion, string secretanswer)
        {
            this.userid = userid;
            this.firstname = firstname;
            this.lastname = lastname;
            this.password = password;
            this.profileimage = profileimage;
            this.homeaddress = homeaddress;
            this.billingaddress = billingaddress;
            this.emailaddress = emailaddress;
            this.phone = phone;
            this.secretanswer = secretanswer;
            this.secretquestion = secretquestion;
        }

        public int UserID
        {
            get { return this.userid; }
            set { this.userid = value; }
        }
        public string FirstName
        {
            get { return this.firstname; }
            set { this.firstname = value; }
        }
        public string LastName
        {
            get { return this.lastname; }
            set { this.lastname = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string ProfileImage
        {
            get { return this.profileimage; }
            set { this.profileimage = value; }
        }
        public string HomeAddress
        {
            get { return this.homeaddress; }
            set { this.homeaddress = value; }
        }
        public string BillingAddress
        {
            get { return this.billingaddress; }
            set { this.billingaddress = value; }
        }
        public string EmailAddress
        {
            get { return this.emailaddress; }
            set { this.emailaddress = value; }
        }
        public int Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public string SecretQuestion
        {
            get { return this.secretquestion; }
            set { this.secretquestion = value; }
        }
        public string SecretAnswer
        {
            get { return this.secretanswer; }
            set { this.secretanswer = value; }
        }
    }
}
