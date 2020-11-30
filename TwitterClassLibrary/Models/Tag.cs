using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string TagText { get; set; }
        public bool Trending { get; set; }

        public Tag()
        {

        }

        public Tag(int id, string tagtext, int postid, bool trending)
        {
            Id = id;
            PostId = postid;
            TagText = tagtext;
            Trending = trending;

        }

    }
}
