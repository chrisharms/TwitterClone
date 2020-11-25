using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterCloneAPI.Models
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string TagText { get; set; }

        public Tag()
        {

        }

        public Tag(int id, int postid, string tagtext)
        {
            Id = id;
            PostId = postid;
            TagText = tagtext;
        }

    }
}
