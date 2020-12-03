using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    [Serializable]
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Username { get; set; }

        public Like()
        {

        }
        public Like(int Id, int PostId, string Username)
        {
            this.Id = Id;
            this.PostId = PostId;
            this.Username = Username;
        }
    }
}
