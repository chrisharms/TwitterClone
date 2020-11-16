using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Follow
    {
        private string username;
        private string followusername;
        private DateTime followdate;

        public Follow()
        {

        }

        public Follow(string username, string followusername, DateTime followdate)
        {
            this.username = username;
            this.followdate = followdate;
            this.followusername = followusername;
        }

        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
        public string FollowUsername
        {
            get { return this.followusername; }
            set { this.followusername = value; }
        }
        public DateTime FollowDate
        {
            get { return this.followdate; }
            set { this.followdate = value; }
        }
    }
}
