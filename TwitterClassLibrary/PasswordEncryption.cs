using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClassLibrary.BCrypt;

namespace TwitterClassLibrary
{
    public class PasswordEncryption
    {
        public static string EncryptPassword(string password)
        {
            string mySalt = BCrypt.BCrypt.GenerateSalt();
            string myHash = BCrypt.BCrypt.HashPassword(password, mySalt);
            if (BCrypt.BCrypt.CheckPassword(password, myHash))
                return myHash;
            else
                return "";
        }
        //Pass this method the users entered plain text and the password stored in the DB
        public static bool DecryptPassword(string password, string hashedPw)
        {
            return BCrypt.BCrypt.CheckPassword(password, hashedPw);
        }
    }
}
