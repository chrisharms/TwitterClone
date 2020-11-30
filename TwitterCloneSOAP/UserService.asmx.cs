using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using TwitterClassLibrary;
using TwitterClassLibrary.Connection;

namespace TwitterCloneSOAP
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        public bool ValidateUser(string username, string password)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_VerifyUser";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }

            string hashPW = ds.Tables[0].Rows[0]["Password"].ToString();

            return PasswordEncryption.DecryptPassword(password, hashPW);
        }

        [WebMethod]
        public bool ValidateUsername(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_LookUpUser";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }
        [WebMethod]
        public bool ValidateEmail(string email)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_ValidateEmail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmailAddress", email);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        [WebMethod]
        public bool AddUser(User user)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CreateUser";
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
            cmd.Parameters.AddWithValue("@HomeAddress", user.HomeAddress);
            cmd.Parameters.AddWithValue("@BillingAddress", user.BillingAddress);
            cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@SecretQuestions", user.SecretQuestions);
            cmd.Parameters.AddWithValue("@SecretAnswers", user.SecretAnswers);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public User GetUser(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            DataRow userRow = ds.Tables[0].Rows[0];
            User user = new User(userRow["Username"].ToString(), userRow["FirstName"].ToString(), userRow["Lastname"].ToString(), 
                userRow["Password"].ToString(), userRow["ProfileImage"].ToString(), userRow["HomeAddress"].ToString(), 
                userRow["BillingAddress"].ToString(), userRow["EmailAddress"].ToString(), userRow["Phone"].ToString(), 
                userRow["SecretQuestions"].ToString(), userRow["SecretAnswers"].ToString(), userRow["Verified"].ToString());

            return user;
        }

        [WebMethod]
        public User GetUserByEmail(string email)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetUserByEmail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmailAddress", email);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            DataRow userRow = ds.Tables[0].Rows[0];
            User user = new User(userRow["Username"].ToString(), userRow["FirstName"].ToString(), userRow["Lastname"].ToString(),
                userRow["Password"].ToString(), userRow["ProfileImage"].ToString(), userRow["HomeAddress"].ToString(),
                userRow["BillingAddress"].ToString(), userRow["EmailAddress"].ToString(), userRow["Phone"].ToString(),
                userRow["SecretQuestions"].ToString(), userRow["SecretAnswers"].ToString(), userRow["Verified"].ToString());

            return user;
        }
        [WebMethod]
        public bool IsUserVerified(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_IsUserVerified";
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public bool UpdateUserVerification(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_UpdateUserVerification";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod]
        public bool UpdatePassword(string username, string password)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_UpdatePassword";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;

        }

        [WebMethod]
        public bool DeleteUser(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_DeleteUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        [WebMethod]
        public bool UpdateUser(string oldUsername, User user)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_UpdateUser";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OldUsername", oldUsername);
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@ProfileImage", user.ProfileImage);
            cmd.Parameters.AddWithValue("@HomeAddress", user.HomeAddress);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@BillingAddress", user.BillingAddress);
            cmd.Parameters.AddWithValue("@SecretQuestions", user.SecretQuestions);
            cmd.Parameters.AddWithValue("@SecretAnswers", user.SecretAnswers);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
