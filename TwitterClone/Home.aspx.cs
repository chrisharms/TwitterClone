using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary;
using TwitterClassLibrary.DBObjCreator;

namespace TwitterClone
{
    public partial class Homepage : System.Web.UI.Page
    {
        string currentUsername;
        User currentUser;
        private const bool TRENDING = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            Exception ex = null;
            if (Session["Username"] != null)
            {
                currentUsername = (string)Session["Username"];
                List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
                filter.Add(DBObjCreator.CreateFilter("Username", currentUsername, typeof(string)));
                List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetUser", ref ex, filter);
                List<User> newUser = new List<User>();
                records.ForEach(r => newUser.Add(DBObjCreator.CreateObj<User>(r, typeof(User))));
                currentUser = newUser[0];
                Session["CurrentUserObj"] = currentUser;
            }
            if (!IsPostBack)
            {
                InitializeTrendingList();
            }

        }

        private void InitializeTrendingList()
        {
            Exception ex = null;
            List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            filter.Add(DBObjCreator.CreateFilter("Trending", TRENDING, typeof(bool)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetPostsByTrending", ref ex, filter);
            List<Post> usersPosts = new List<Post>();
            records.ForEach(r => usersPosts.Add(DBObjCreator.CreateObj<Post>(r, typeof(Post))));
            //Needs to be changed to be trending or something
            List<Post> uniquePosts = usersPosts.GroupBy(p => p.Id).Select(id => id.First()).ToList(); //Select only the first occurence of a post ID
            repeaterTrending.DataSource = uniquePosts;
            repeaterTrending.DataBind();
        }



        //Bind post objects to Custom User Controls
        protected void repeaterTrending_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PostCard pc = e.Item.FindControl("postCard") as PostCard;
            Post post = e.Item.DataItem as Post;

            if (post.PostPhoto.Equals("fake.png"))
            {
                pc.ChangeImageVisibility(); //True by default, change to false
            }
            else
            {
                pc.PostImage = post.PostPhoto;
            }


            pc.PostText = post.PostText;
            pc.PostUsername = post.Username;
            pc.Likes = post.Likes.ToString();
            Exception ex = null;
            List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            filter.Add(DBObjCreator.CreateFilter("PostId", post.Id, typeof(int)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetTagsByPost", ref ex, filter);
            List<Tag> tags = new List<Tag>();
            records.ForEach(r => tags.Add(DBObjCreator.CreateObj<Tag>(r, typeof(Tag))));
            
            foreach(Tag t in tags)
            {
                var tc = (TagControl)Page.LoadControl("TagControl.ascx");
                tc.Text = t.TagText;
                tc.ButtonClick += new EventHandler(Tag_ButtonClick);
                pc.ph.Controls.Add(tc);
            }
        }

        protected void Tag_ButtonClick(object sender, EventArgs e)
        {
            
        }
    }
}