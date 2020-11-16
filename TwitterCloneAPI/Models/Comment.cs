using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Comment
    {
        private int id;
        private int commentpostid;
        private int commentparent;
        private string commenttext;

        public Comment()
        {

        }

        public Comment(int id, int commentpostid, int commentparent, string commenttext)
        {
            this.id = id;
            this.commentpostid = commentpostid;
            this.commentparent = commentparent;
            this.commenttext = commenttext;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int CommentPostId
        {
            get { return this.commentpostid; }
            set { this.commentpostid = value; }
        }
        public int CommentParent
        {
            get { return this.commentparent; }
            set { this.commentparent = value; }
        }
        public string CommentText
        {
            get { return this.commenttext; }
            set { this.commenttext = value; }
        }
    }
}
