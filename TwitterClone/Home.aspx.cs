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
        bool advSearch;
        string currentUsername;
        User currentUser;
        private const bool TRENDING = true;
        private const int ALL = 0;
        private const int FOLLOW = 1;

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
                Session["AdvSearch"] = false;
                if (Session["Username"] != null)
                {
                    InitializeTrendingList();
                    InitializeFollowList();
                    InitializeAllPostsList();
                    repeaterAll.Visible = false;
                    Session["CurrentView"] = FOLLOW;
                }
                if (Session["Guest"] != null)
                {
                    Greeting.InnerText = "All Posts";
                    InitializeAllPostsList();
                    InitializeTrendingList();
                    btnFollowPosts.Visible = false;
                    Session["CurrentView"] = ALL;

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
            List<Post> uniquePosts = usersPosts.GroupBy(p => p.Id).Select(id => id.First()).ToList(); //Select only the first occurence of a post ID
            repeaterFollow.DataSource = uniquePosts;
            repeaterFollow.DataBind();
        }

        private void InitializeAllPostsList()
        {

            Exception ex = null;
            List<object[]> records = DBObjCreator.ReadDBObjs("TP_GetAllPosts", ref ex);
            List<Post> posts = new List<Post>();
            records.ForEach(r => posts.Add(DBObjCreator.CreateObj<Post>(r, typeof(Post))));
            Session["AllPosts"] = posts;
            repeaterAll.DataSource = posts;
            repeaterAll.DataBind();
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
            pc.PostId = post.Id;
            pc.PostDate = post.PostDate;
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

            pc.PostText = post.PostText;
            pc.PostUsername = post.Username;
            pc.Likes = post.Likes.ToString();
            pc.PostId = post.Id;
            pc.PostDate = post.PostDate;
            if (Session["Username"] != null)
            {
                pc.DisableFollowButton(Session["Username"].ToString());
            }
            if (Session["Guest"] != null)
            {
                pc.EnableGuestRestrictions();
            }

        }

        protected void repeaterAll_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            Session["CurrentView"] = ALL; 
            repeaterAll.Visible = true;
            repeaterAll.Items.OfType<RepeaterItem>().ToList().ForEach(i => i.Visible = true);
            repeaterFollow.Visible = false;
            upAllRepeater.Update();
        }

        protected void btnFollowPosts_Click(object sender, EventArgs e)
        {
            Greeting.InnerText = "Who You're Following";
            Session["CurrentView"] = FOLLOW;
            repeaterAll.Visible = false;
            repeaterFollow.Visible = true;
        }

        protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
        {
            divAdvSearch.Visible = !divAdvSearch.Visible;
            advSearch = !advSearch;
            Session["AdvSearch"] = advSearch;
            upSearch.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
 
            string tagValue = txtSearch.Text;
            if(string.IsNullOrEmpty(tagValue) && (bool)Session["AdvSearch"] == false)
            {
                lblSearchError.Visible = true;
                lblSearchError.Text = "Must have at least one search critera";
                return;
            }
            else if(!string.IsNullOrEmpty(tagValue))
            {
                foreach (RepeaterItem i in repeaterAll.Items)
                {
                    PostCard pc = i.FindControl("postCard") as PostCard;
                    if (string.IsNullOrEmpty(pc.TagList) || !pc.TagList.Contains($"{tagValue}"))
                    {
                        i.Visible = false;
                    }
                }
            }

            if ((bool)Session["AdvSearch"])
            {

                if (!string.IsNullOrEmpty(txtUsername.Text))
                {

                    string usernameFilter = txtUsername.Text;
                    foreach(RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        if (!pc.PostUsername.Equals($"@{usernameFilter}"))
                        {
                            i.Visible = false;
                        }
                    }
                    //(repeaterAll.Items.OfType<RepeaterItem>().ToList()
                    //    .Where(i => !(i.DataItem as Post).Username.Equals(usernameFilter)) as List<RepeaterItem>)
                    //    .ForEach(i => i.Visible = false);
                }
                if(!string.IsNullOrEmpty(txtLikes.Text) && int.Parse(txtLikes.Text) > 0)
                {
                    int likes = int.Parse(txtLikes.Text);

                    (repeaterAll.Items.OfType<RepeaterItem>().ToList()
                        .Where(i => (i.DataItem as Post).Likes < likes) as List<RepeaterItem>)
                        .ForEach(i => i.Visible = false);
                }
                if(ddlImage.SelectedIndex == 1)
                {
                    (repeaterAll.Items.OfType<RepeaterItem>().ToList()
                        .Where(i => (i.DataItem as Post).PostPhoto.Equals("fake.png")) as List<RepeaterItem>)
                        .ForEach(i => i.Visible = false);
                }
                if (ddlImage.SelectedIndex == 2)
                {
                    (repeaterAll.Items.OfType<RepeaterItem>().ToList()
                        .Where(i => !(i.DataItem as Post).PostPhoto.Equals("fake.png")) as List<RepeaterItem>)
                        .ForEach(i => i.Visible = false);
                }
                if (!string.IsNullOrEmpty(txtFilterStartDate.Text))
                {
                    DateTime startDate = DateTime.Parse(txtFilterStartDate.Text);
                    (repeaterAll.Items.OfType<RepeaterItem>().ToList()
                        .Where(i => DateTime.Parse((i.DataItem as Post).PostDate) > startDate) as List<RepeaterItem>)
                        .ForEach(i => i.Visible = false);
                }
                if (!string.IsNullOrEmpty(txtFilterEndDate.Text))
                {
                    DateTime endDate = DateTime.Parse(txtFilterEndDate.Text);
                    (repeaterAll.Items.OfType<RepeaterItem>().ToList()
                        .Where(i => DateTime.Parse((i.DataItem as Post).PostDate) < endDate) as List<RepeaterItem>)
                        .ForEach(i => i.Visible = false);
                }

            }
            upAllRepeater.Update();

        }
    }
}