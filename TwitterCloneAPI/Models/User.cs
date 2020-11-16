using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class User
    {
        private string username;
        private string firstname;
        private string lastname;
        private string password;
        private string profileimage;
        private string homeaddress;
        private string billingaddress;
        private string emailaddress;
        private string phone;
        private int secretquestions;
        private string secretanswers;

        public User()
        {

        }

        public User(string username, string firstname, string lastname, string password, string profileimage, string homeaddress, string billingaddress, string emailaddress, string phone, int secretquestions, string secretanswers)
        {
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.password = password;
            this.profileimage = profileimage;
            this.homeaddress = homeaddress;
            this.billingaddress = billingaddress;
            this.emailaddress = emailaddress;
            this.phone = phone;
            this.secretanswers = secretanswers;
            this.secretquestions = secretquestions;
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
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
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public int SecretQuestions
        {
            get { return this.secretquestions; }
            set { this.secretquestions = value; }
        }
        public string SecretAnswers
        {
            get { return this.secretanswers; }
            set { this.secretanswers = value; }
        }
    }
}
