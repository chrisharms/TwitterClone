using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwitterClassLibrary;
using TwitterCloneAPI.Models;

namespace TwitterCloneAPI.Controllers
{
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        [HttpGet("GetUserPostCount/{Username}")] //https://localhost:44312/api/User/GetUserPostCount/TestUser
        public int GetUsersPostCount(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUsersPosts", ref ex, filter);
            return records.Count;
        }

        [HttpGet("GetUserPosts/{Username}")] //https://localhost:44312/api/User/GetUserPosts/TestUser
        public List<Post> GetUsersPosts(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUsersPosts", ref ex, filter);
            List<Post> posts = new List<Post>();
            records.ForEach(o => posts.Add(DBObjCreator.CreateObj<Post>(o, typeof(Post))));
            return posts;
        }

        //[HttpPost("CreateFollow")]
        //public int CreateFollow([FromBody] Follow follow)
        //{
        //    return 0;
        //}

        //[HttpDelete("DeleteFollow/{Username}/{FollowUsername}")]
        //public int DeleteFollow(string Username, string FollowUsername)
        //{
            
        //}
    }

}
