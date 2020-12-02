using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterClassLibrary.Connection;
using System.Data;
using TwitterCloneAPI.Models;
using System.Data.SqlClient;

namespace TwitterCloneAPI.Controllers
{
    [Route("api/Tag")]

    public class TagController : Controller
    {
        [HttpGet("GetTagsByPost/{PostId}")]
        public List<Tag> GetTags(int PostId)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_GetTagsByPost";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", PostId);

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            DataTable tagsTable = ds.Tables[0];
            List<Tag> tagsList = new List<Tag>();
            for (int i = 0; i < tagsTable.Rows.Count; i++)
            {
                Tag tag = new Tag();
                tag.Id = Int32.Parse(tagsTable.Rows[i]["Id"].ToString());
                tag.PostId = Int32.Parse(tagsTable.Rows[i]["PostId"].ToString());
                tag.TagText = tagsTable.Rows[i]["TagText"].ToString();
                tagsList.Add(tag);
            }

            return tagsList;
        }

        [HttpPost("CreateTag")]
        public bool CreateTag([FromBody] Tag tag)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_CreateTag";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", tag.PostId);
            cmd.Parameters.AddWithValue("@TagText", tag.TagText);
            cmd.Parameters.AddWithValue("@Trending", tag.Trending);

            int count = db.DoUpdateUsingCmdObj(cmd);
            if (count < 1)
            {
                return false;
            }
            return true;
        }

        [HttpDelete("DeleteTagsByPost/{PostId}")]
        public int DeleteTags(int PostId)
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "TP_DeleteTagsByPost";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PostId", PostId);

            int count = db.DoUpdateUsingCmdObj(cmd);
            return count;
        }
    }
}