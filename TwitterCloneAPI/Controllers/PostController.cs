using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterClassLibrary.DBObjCreator;
using TwitterClassLibrary.DBObjWriter;
using TwitterCloneAPI.Models;

namespace TwitterCloneAPI.Controllers
{
    

    [Route("api/[controller]")] //api/Post
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


        //[HttpPost("CreatePost")] //https://localhost:44312/api/Post/CreatePost
        //public string CreatePost(string text, string image, string username, string password)
        //{
        //    Exception ex = null;
        //    List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
        //    filter.Add(DBObjCreator.CreateFilter("Username", $"{username}", typeof(string)));
        //    filter.Add(DBObjCreator.CreateFilter("Password", $"{password}", typeof(string)));
        //    List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_VerifyUser", ref ex, filter);

        //    if (records.Count != 1)//Username/password combo not valid
        //    {
        //        return "Invalid credentials.";
        //    }
        //    int likes = 0;
        //    string time = DateTime.Today.ToString("d");
        //    Post newPost = new Post(-1, time, text, image, username, likes);
        //    List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
        //    bool result = DBObjWriter.GenericWriteToDB<Post>(newPost, "TP_CreatePost", ref errors, new List<string>() { "Id" });
        //    return "Post succesfully created";
        //}


        [HttpPost("CreatePost")] //https://localhost:44312/api/Post/CreatePost
        public string CreatePost(string text, string image, string username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUser", ref ex, filter);

            if (records.Count != 1)//Username/password combo not valid
            {
                return "Invalid username.";
            }
            int likes = 0;
            string time = DateTime.Today.ToString("d");
            Post newPost = new Post(-1, time, text, image, username, likes);
            List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            bool result = DBObjWriter.GenericWriteToDB<Post>(newPost, "TP_CreatePost", ref errors, new List<string>() { "Id" });
            return "Post succesfully created";
        }

        [HttpPost("DeletePost")] //https://localhost:44312/api/Post/CreateComment
        public string DeletePost(int postId)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Id", postId, typeof(int)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_FindCommentById", ref ex, filter);

            if (records.Count != 1)
            {
                return "Invalid comment ID.";
            }

            List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            bool result = DBObjWriter.DeleteWithWhere("TP_DeleteCommentById", ref ex, filter);
            if (result)
            {
                return "Post succesfully deleted";
            }
            else
            {
                return "Failed to delete post, please try again later";
            }
        }
    }
}
