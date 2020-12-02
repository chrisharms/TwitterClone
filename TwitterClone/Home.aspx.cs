using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
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
                if (Session["Username"] != null)
                {
                    InitializeTrendingList();
                    InitializeFollowList();
                }
                if (Session["Guest"] != null)
                {
                    Greeting.InnerText = "All Posts";
                    InitializeAllPostsList();
                    InitializeTrendingList();
                    btnFollowPosts.Visible = false;
                }
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

        private void InitializeFollowList()
        {
            Exception ex = null;
            List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            filter.Add(DBObjCreator.CreateFilter("Username", currentUsername, typeof(string)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetPostsByFollow", ref ex, filter);
            List<Post> usersPosts = new List<Post>();
            records.ForEach(r => usersPosts.Add(DBObjCreator.CreateObj<Post>(r, typeof(Post))));
            //Needs to be changed to be trending or something
            List<Post> uniquePosts = usersPosts.GroupBy(p => p.Id).Select(id => id.First()).ToList(); //Select only the first occurence of a post ID
            repeaterFollow.DataSource = uniquePosts;
            repeaterFollow.DataBind();
        }

        private void InitializeAllPostsList()
        {
            //string url = "https://localhost:44312/api/Post/GetAllPosts";
            //WebRequest request = WebRequest.Create(url);
            //WebResponse response = request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader reader = new StreamReader(stream);
            //String data = reader.ReadToEnd();
            //JavaScriptSerializer js = new JavaScriptSerializer();

            //List<Post> posts = js.Deserialize<List<Post>>(data);

            //stream.Close();
            //reader.Close();
            //repeaterFollow.DataSource = posts;
            //repeaterFollow.DataBind();

            Exception ex = null;
            // List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            // filter.Add(DBObjCreator.CreateFilter("Trending", TRENDING, typeof(bool)));
            List<object[]> records = DBObjCreator.ReadDBObjs("TP_GetAllPosts", ref ex);
            List<Post> posts = new List<Post>();
            records.ForEach(r => posts.Add(DBObjCreator.CreateObj<Post>(r, typeof(Post))));
            //Needs to be changed to be trending or something
            // List<Post> uniquePosts = usersPosts.GroupBy(p => p.Id).Select(id => id.First()).ToList(); //Select only the first occurence of a post ID
            repeaterFollow.DataSource = posts;
            repeaterFollow.DataBind();
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

            
            pc.HiddenField = post.Id.ToString();
            pc.PostText = post.PostText;
            pc.PostUsername = post.Username;
            pc.Likes = post.Likes.ToString();
            pc.PostId = post.Id;
            if (Session["Username"] != null)
            {
                pc.DisableFollowButton(Session["Username"].ToString());
            }
            if (Session["Guest"] != null)
            {
                pc.EnableGuestRestrictions();
            }
        }

        protected void repeaterFollow_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            pc.HiddenField = post.Id.ToString();
            pc.PostText = post.PostText;
            pc.PostUsername = post.Username;
            pc.Likes = post.Likes.ToString();
            pc.PostId = post.Id;
            if (Session["Username"] != null)
            {
                pc.DisableFollowButton(Session["Username"].ToString());
            }
            if (Session["Guest"] != null)
            {
                pc.EnableGuestRestrictions();
            }

        }

        protected void btnAllPosts_Click(object sender, EventArgs e)
        {
            Greeting.InnerText = "All Posts";
            InitializeAllPostsList();
        }

        protected void btnFollowPosts_Click(object sender, EventArgs e)
        {
            Greeting.InnerText = "Who You're Following";
            InitializeFollowList();
        }
    }
}