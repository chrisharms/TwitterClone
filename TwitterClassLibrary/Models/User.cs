using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string HomeAddress { get; set; }
        public string BillingAddress { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string SecretQuestions { get; set; }
        public string SecretAnswers { get; set; }

        public User()
        {

        }

        public User(string username, string firstname, string lastname, string password, string profileimage, string homeaddress, string billingaddress, string emailaddress, string phone, string secretquestions, string secretanswers)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Password = password;
            ProfileImage = profileimage;
            HomeAddress = homeaddress;
            BillingAddress = billingaddress;
            EmailAddress = emailaddress;
            Phone = phone;
            SecretAnswers = secretanswers;
            SecretQuestions = secretquestions;
        }

        public int GetRandomQuestion(out int index)
        {
            Random rand = new Random();
            int randomIndex = rand.Next(0, 3);
            string[] indexes = this.SecretQuestions.Split(',');
            index = randomIndex;
            return Int32.Parse(indexes[randomIndex]);
        }

        public string GetSecretAnswer(int index)
        {
            string[] secretAnswers = this.SecretAnswers.Split(',');
            return secretAnswers[index];
        }

    }
}
