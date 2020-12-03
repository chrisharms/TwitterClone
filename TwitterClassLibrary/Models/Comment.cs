using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    [Serializable]
    public class Comment
    {
        public int Id { get; set; }
        public string CommentUsername { get; set; }
        public int CommentPostId { get; set; }
        public string CommentText { get; set; }

        public Comment()
        {

        }

        public Comment(int id, string commentUsername, int commentPostId, string commentText)
        {
            Id = id;
            CommentUsername = commentUsername;
            CommentPostId = commentPostId;
            CommentText = commentText;
        }

    }
}
