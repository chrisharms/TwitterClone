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
    [Route("api/Like")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        [HttpGet("CheckLike/{username}/{postId}")]
        public bool CheckLike(string username, int postId)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CheckLike";
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PostId", postId);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        [HttpPost("AddLike/{username}/{postId}")]
        public bool AddLike(string username, int postId)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CreateLike";
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PostId", postId);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TP_IncrementLikeCount";
                cmd.Parameters.AddWithValue("@PostId", postId);

                int count2 = db.DoUpdateUsingCmdObj(cmd2);
                if (count > 0) {
                    return true;
                }

                return false;
            }

            return false;
            
        }

        [HttpDelete("DeleteLike/{username}/{postId}")]
        public bool DeleteLike(string username, int postId)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_DeleteLike";
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@PostId", postId);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count > 0)
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TP_DecrementLikeCount";
                cmd.Parameters.AddWithValue("@PostId", postId);

                int count2 = db.DoUpdateUsingCmdObj(cmd2);
                if (count > 0)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}