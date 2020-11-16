using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Tag
    {
        private int id;
        private int postid;
        private string tagtext;

        public Tag()
        {

        }

        public Tag(int id, int postid, string tagtext)
        {
            this.id = id;
            this.postid = postid;
            this.tagtext = tagtext;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public int PostId
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
