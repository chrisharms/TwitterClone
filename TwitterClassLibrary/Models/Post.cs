using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    [Serializable]
    public class Post
    {
        public int Id { get; set; }
        public string PostDate { get; set; }
        public string PostText { get; set; }
        public string PostPhoto { get; set; }
        public string Username { get; set; }
        public int Likes { get; set; }

        public Post()
        {

        }

        public Post(int id, string postdate, string posttext, string postphoto, string username, int likes)
        {
            Id = id;
            PostText = posttext;
            PostDate = postdate;
            PostPhoto = postphoto;
            Username = username;
            Likes = likes;
        }
       
    }
}
