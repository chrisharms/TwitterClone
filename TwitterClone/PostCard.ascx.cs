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

        public string PostUsername
        {
            get { return postUsername.InnerText; }
            set { postUsername.InnerText = value; }
        }

        public string TagList
        {
            get { return tagList.InnerHtml; }
            set { tagList.InnerHtml = value; }
        }

        public string Likes
        {
            get { return lblLikes.Text; }
            set { lblLikes.Text = $"Likes: {value}";}
        }
        
        public PlaceHolder ph
        {
            get { return phTagList; }
        }

        public void ChangeImageVisibility()
        {
            imgPost.Visible = !imgPost.Visible;
        }
    }
}