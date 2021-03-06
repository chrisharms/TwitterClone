﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using TwitterClassLibrary.Encryption;
using TwitterClassLibrary.Connection;
using TwitterClassLibrary.DBObjCreator;
using TwitterClassLibrary.DBObjWriter;
using TwitterCloneAPI.Models;

namespace TwitterCloneAPI.Controllers
{
    

    [Route("api/Post")] //api/Post
    public class PostController : Controller
    {


        [HttpGet("GetUserPostsFiltered/{Username}/{Likes?}/{Time?}/{Image?}")] //https://localhost:44312/api/Post/GetUserPostsFiltered/TestUser/20/11_20_2020-11_21_2020/1
        public List<Post> GetUsersPosts(string Username, int Likes = -1, string Time = "No Time", int Image = -1)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUsersPosts", ref ex, filter);
            List<Post> posts = new List<Post>();
            records.ForEach(o => posts.Add(DBObjCreator.CreateObj<Post>(o, typeof(Post))));

            if (Likes >= 0)
            {
                posts.RemoveAll(p => p.Likes < Likes);
            }

            if (Image > -1)
            {
                if (Image == 1) //True, remove posts without images
                {
                    posts.RemoveAll(p => string.IsNullOrEmpty(p.PostPhoto));
                }
                else // False, remove posts with images
                { 
                    posts.RemoveAll(p => !string.IsNullOrEmpty(p.PostPhoto));
                }
            }

            if(!Time.Equals("No Time"))
            {
                Time = Time.Replace('_', '/'); //C# api's cant handle encoded forward slashes easily so need to use _ and convert them to slashes
                DateTime dateOne;
                DateTime dateTwo;
                string[] times = Time.Split("-");
                if(times.Length < 2)
                {
                    return null;
                }

                if(!DateTime.TryParse(times[0], out dateOne))
                {
                    return null;
                }

                if (!DateTime.TryParse(times[1], out dateTwo))
                {
                    return null;
                }

                posts.RemoveAll(p => DateTime.Parse(p.PostDate) < dateOne || DateTime.Parse(p.PostDate) > dateTwo);
            }

            return posts;
        }

        [HttpPost("CreatePost")] //https://localhost:44312/api/Post/CreatePost
        public int CreatePost([FromBody] Post post)
        {
            //Exception ex = null;
            //List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            //filter.Add(DBObjCreator.CreateFilter("Username", $"{post.Username}", typeof(string)));
            //List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUser", ref ex, filter);

            //if (records.Count != 1)//Username/password combo not valid
            //{
            //    return false;
            //}
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_CreatePost";
            cmd.Parameters.AddWithValue("@PostText", post.PostText);
            cmd.Parameters.AddWithValue("@PostPhoto", post.PostPhoto);
            cmd.Parameters.AddWithValue("@Username", post.Username);
            cmd.Parameters.AddWithValue("@PostDate", post.PostDate);
            cmd.Parameters.AddWithValue("@Likes", post.Likes);
            SqlParameter param = new SqlParameter("@ID", DbType.Int32);
            param.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);

            db.DoUpdateUsingCmdObj(cmd);

            return int.Parse(cmd.Parameters["@ID"].Value.ToString());


            //int likes = 0;
            //string time = DateTime.Today.ToShortDateString();
            //Post newPost = new Post(-1, time, post.PostText, post.PostPhoto, post.Username, likes);
            //List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            //bool result = DBObjWriter.GenericWriteToDB<Post>(newPost, "TP_CreatePost", ref errors, new List<string>() { "Id" });
            //return result;
        }

        [HttpPost("DeletePost")] //https://localhost:44312/api/Post/DeletePost/10
        public string DeletePost([FromBody] int postId)
        {

            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Id", postId, typeof(int)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetPostsById", ref ex, filter);

            if (records.Count != 1)
            {
                return "Post ID does not exist.";
            }

            List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            bool result = DBObjWriter.DeleteWithWhere("TP_DeletePostById", ref ex, filter);
            if (result)
            {
                return "Post succesfully deleted";
            }
            else
            {
                return "Failed to delete post, please try again later";
            }
        }

        [HttpGet("GetAllPosts")]
        public List<Post> GetAllPosts()
        {
            DBConnect db = new DBConnect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TP_GetAllPosts";

            DataSet ds = db.GetDataSetUsingCmdObj(cmd);
            List<Post> posts = new List<Post>();
            DataTable postTable = ds.Tables[0];
            for (int i=0; i < postTable.Rows.Count; i++)
            {
                DataRow row = postTable.Rows[i];
                string id = row["Id"].ToString();
                Post post = new Post(Int32.Parse(row["Id"].ToString()), row["PostDate"].ToString(), row["PostText"].ToString(), row["PostPhoto"].ToString(), row["Username"].ToString(), Int32.Parse(row["Likes"].ToString()));
                posts.Add(post);
            }

            return posts;
        }
    }
}
