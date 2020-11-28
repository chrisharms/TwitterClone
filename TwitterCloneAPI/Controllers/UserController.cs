using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TwitterCloneAPI.Models;
using TwitterClassLibrary.DBObjCreator;


namespace TwitterCloneAPI.Controllers
{
    [Route("api/[controller]")] //api/User
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

        
        //Get followers


        [HttpGet("GetUserFollowerCount/{Username}")] //https://localhost:44312/api/User/GetUserFollowCount/TestUser
        public int GetUsersFollowerCount(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUserFollowers", ref ex, filter);
            return records.Count;   
        }


        [HttpGet("GetUserFollowers/{Username}")] //https://localhost:44312/api/User/GetUserFollowers/TestUser
        public List<Follow> GetUsersFollowers(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUserFollowers", ref ex, filter);
            List<Follow> follows = new List<Follow>();
            records.ForEach(o => follows.Add(DBObjCreator.CreateObj<Follow>(o, typeof(Follow))));
            return follows;
        }

        //Get Follows

        [HttpGet("GetUserFollowCount/{Username}")] //https://localhost:44312/api/User/GetUserFollowCount/TestUser
        public int GetUsersFollowCount(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUserFollows", ref ex, filter);
            return records.Count;
        }


        [HttpGet("GetUserFollows/{Username}")] //https://localhost:44312/api/User/GetUserFollows/TestUser
        public List<Follow> GetUsersFollows(string Username)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{Username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUserFollows", ref ex, filter);
            List<Follow> follows = new List<Follow>();
            records.ForEach(o => follows.Add(DBObjCreator.CreateObj<Follow>(o, typeof(Follow))));
            return follows;
        }

    }

}
