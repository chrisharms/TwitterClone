using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Tag
    {
        private int tagid;
        private int postid;
        private string tagtext;

        public Tag()
        {

        }

        public Tag(int tagid, int postid, string tagtext)
        {
            this.tagid = tagid;
            this.postid = postid;
            this.tagtext = tagtext;
        }

        public int TagID
        {
            get { return this.tagid; }
            set { this.tagid = value; }
        }

        public int PostID
        {
            get { return this.postid; }
            set { this.postid = value; }
        }
        public string TagText
        {
            get { return this.tagtext; }
            set { this.tagtext = value; }
        }
    }
}
