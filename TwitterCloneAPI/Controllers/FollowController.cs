using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneAPI.Models;
using TwitterClassLibrary.Connection;
using System.Data;
using System.Data.SqlClient;

namespace TwitterCloneAPI.Controllers
{
    [Route("api/Follow")]

    public class FollowController : Controller
    {

        [HttpGet("GetFollowsByUser/{Username}")]
        public List<Follow> GetFollows(string Username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_GetFollowsByUser";
            cmd.Parameters.AddWithValue("@Username", Username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            DataTable follows = ds.Tables[0];
            List<Follow> followList = new List<Follow>();
            for (int i = 0; i < follows.Rows.Count; i++)
            {
                Follow follow = new Follow();
                follow.Username = follows.Rows[i]["Username"].ToString();
                follow.FollowUsername = follows.Rows[i]["FollowUsername"].ToString();
                follow.FollowDate = follows.Rows[i]["FollowDate"].ToString();
                followList.Add(follow);
            }

            return followList;
        }

        [HttpGet("GetUserFollowers/{Username}")]
        public List<Follow> GetFollowers(string Username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetUserFollowers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", Username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            DataTable followers = ds.Tables[0];
            List<Follow> followerList = new List<Follow>();
            for (int i = 0; i < followers.Rows.Count; i++)
            {
                Follow follower = new Follow();
                follower.Username = followers.Rows[i]["Username"].ToString();
                follower.FollowUsername = followers.Rows[i]["FollowUsername"].ToString();
                follower.FollowDate = followers.Rows[i]["FollowDate"].ToString();
                followerList.Add(follower);
            }

            return followerList;
        }

        [HttpGet("GetFollowerCount/{username}")]
        public int GetFollowerCount(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetUserFollowers";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            return ds.Tables[0].Rows.Count;
        }

        [HttpGet("GetFollowCount/{username}")]
        public int GetFollowCount(string username)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_GetFollowsByUser";
            cmd.Parameters.AddWithValue("@Username", username);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            return ds.Tables[0].Rows.Count;
        }

        [HttpPost("CreateFollow")]
        public bool CreateFollow([FromBody] Follow follow)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CreateFollow";
            cmd.Parameters.AddWithValue("@Username", follow.Username);
            cmd.Parameters.AddWithValue("@FollowUsername", follow.FollowUsername);
            cmd.Parameters.AddWithValue("@FollowDate", follow.FollowDate);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("DeleteFollow/{Username}/{FollowUsername}")]
        public bool DeleteFollow(string Username, string FollowUsername)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_DeleteFollow";
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@FollowUsername", FollowUsername);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        [HttpGet("VerifyFollow/{Username}/{FollowUsername}")]
        public bool VerifyFollow(string Username, string FollowUsername)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_VerifyFollow";
            cmd.Parameters.AddWithValue("@Username", Username);
            cmd.Parameters.AddWithValue("@FollowUsername", FollowUsername);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}