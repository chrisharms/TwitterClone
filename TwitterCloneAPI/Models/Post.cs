using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Post
    {
        private int postid;
        private DateTime postdate;
        private string posttext;
        private string postimage;

        public Post()
        {

        }

        public Post(int postid, DateTime postdate, string posttext, string postimage)
        {
            this.postid = postid;
            this.posttext = posttext;
            this.postdate = postdate;
            this.postimage = postimage;
        }
       
        public int PostID
        {
            get { return this.postid; }
            set { this.postid = value; }
        }
        public DateTime PostDate
        {
            get { return this.postdate; }
            set { this.postdate = value; }
        }
        public string PostText
        {
            get { return this.posttext; }
            set { this.posttext = value; }
        }
        public string PostImage
        {
            get { return this.postimage; }
            set { this.postimage = value; }
        }
    }
}
