using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    public class Follow
    {
        private int userid;
        private int followuserid;
        private DateTime followdate;

        public Follow()
        {

        }

        public Follow(int userid, int followuserid, DateTime followdate)
        {
            this.userid = userid;
            this.followdate = followdate;
            this.followuserid = followuserid;
        }

        public int UserID
        {
            get { return this.userid; }
            set { this.userid = value; }
        }
        public int FollowUserID
        {
            get { return this.followuserid; }
            set { this.followuserid = value; }
        }
        public DateTime FollowDate
        {
            get { return this.followdate; }
            set { this.followdate = value; }
        }
    }
}
