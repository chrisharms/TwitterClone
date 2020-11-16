using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Post
    {
        private int id;
        private DateTime postdate;
        private string posttext;
        private string postphoto;

        public Post()
        {

        }

        public Post(int id, DateTime postdate, string posttext, string postphoto)
        {
            this.id = id;
            this.posttext = posttext;
            this.postdate = postdate;
            this.postphoto = postphoto;
        }
       
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
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
        public string PostPhoto
        {
            get { return this.postphoto; }
            set { this.postphoto = value; }
        }
    }
}
