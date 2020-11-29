using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterClassLibrary.DBObjCreator;
using TwitterClassLibrary.DBObjWriter;
using TwitterCloneAPI.Models;

namespace TwitterCloneAPI.Controllers
{
    [Route("api/[controller]")]

    public class CommentController : Controller
    {

        [HttpPost("CreateComment")] //https://localhost:44312/api/Post/CreateComment
        public string CreateComment(string text, string username, int postId)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", $"{username}", typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUser", ref ex, filter);

            if (records.Count != 1)//Username/password combo not valid
            {
                return "Invalid username.";
            }
            Comment newComment = new Comment(-1, username, postId, text);
            List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            bool result = DBObjWriter.GenericWriteToDB<Comment>(newComment, "TP_CreateComment", ref errors, new List<string>() { "Id" });
            return "Comment succesfully created";
        }

        [HttpPost("DeleteComment")] //https://localhost:44312/api/Post/CreateComment
        public string DeleteComment(int commentId)
        {
            Exception ex = null;
            List<(string field, dynamic value, Type type)> filter = new List<(string field, dynamic value, Type type)>();
            filter.Add(DBObjCreator.CreateFilter("Id", commentId, typeof(int)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_FindCommentById", ref ex, filter);

            if (records.Count != 1)
            {
                return "Invalid comment ID.";
            }

            List<(bool, int, Exception)> errors = new List<(bool, int, Exception)>();
            bool result = DBObjWriter.DeleteWithWhere("TP_DeleteCommentById", ref ex, filter);
            if (result)
            {
                return "Comment succesfully deleted";
            }
            else
            {
                return "Comment could not be deleted, please try again later";
            }
        }



    }
}