using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace TwitterClone
{
    public partial class PostCard : System.Web.UI.UserControl
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string PostImage
        {
            get { return imgPost.ImageUrl; }
            set { imgPost.ImageUrl = value; }
        }

        public string PostText
        {
            get { return txtPost.InnerText; }
            set { txtPost.InnerText = value; }
        }

        public string PostTitle
        {
            get { return postTitle.InnerText; }
            set { postTitle.InnerText = value; }
        }

        public string TagList
        {
            get { return tagList.InnerHtml; }
            set { tagList.InnerHtml = value; }
        }

    }
}