using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Comment
    {
        private int commentid;
        private int postid;
        private int userid;
        private string commenttext;

        public Comment()
        {

        }

        public Comment(int commentid, int postid, int userid, string commenttext)
        {
            this.commentid = commentid;
            this.postid = postid;
            this.userid = userid;
            this.commenttext = commenttext;
        }

        public int CommentID
        {
            get { return this.commentid; }
            set { this.commentid = value; }
        }
        public int PostID
        {
            get { return this.postid; }
            set { this.postid = value; }
        }
        public int UserID
        {
            get { return this.userid; }
            set { this.userid = value; }
        }
        public string CommentText
        {
            get { return this.commenttext; }
            set { this.commenttext = value; }
        }
    }
}
