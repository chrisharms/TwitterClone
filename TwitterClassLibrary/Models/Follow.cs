using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    public class Follow
    {
        public string Username { get; set; }
        public string FollowUsername { get; set; }
        public string FollowDate { get; set; }

        public Follow()
        {

        }

        public Follow(string username, string followusername, string followdate)
        {
            Username = username;
            FollowDate = followdate;
            FollowUsername = followusername;
        }

    }
}
