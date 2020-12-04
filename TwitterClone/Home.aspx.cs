using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TwitterClassLibrary;
using TwitterClassLibrary.DBObjCreator;
using TwitterClassLibrary.DBObjWriter;

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

                    btnNewPost.Visible = false;
                }
            }
            SetupPostCardEvents();
            HideComments();

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

        private void HideComments()
        {
            divComments.Visible = false;
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

        protected void SetupPostCardEvents()
        {
            foreach (RepeaterItem i in repeaterAll.Items)
            {
                PostCard pc = i.FindControl("postCard") as PostCard;
                pc.TagSearch += new EventHandler(TagSearchEvent);
                pc.ViewComments += new EventHandler(ViewCommentsEvent);
            }

            foreach (RepeaterItem i in repeaterFollow.Items)
            {
                PostCard pc = i.FindControl("postCard") as PostCard;
                pc.TagSearch += new EventHandler(TagSearchEvent);
                pc.ViewComments += new EventHandler(ViewCommentsEvent);
            }
        }

        protected void TagSearchEvent(object sender, EventArgs e)
        {
            TagControl tag = sender as TagControl;
            TagSearch(tag.Text);
            upAllRepeater.Update();
        }



        protected void ViewCommentsEvent(object sender, EventArgs e)
        {
            PostCard parentPost = sender as PostCard;
            Repeater repeater = parentPost.Parent.Parent as Repeater;
            int postId = parentPost.PostId;
            Session["CurrentParentPost"] = postId;
            Exception ex = null;
            foreach (RepeaterItem i in repeater.Items)
            {
                PostCard pc = i.FindControl("postCard") as PostCard;
                if (pc.PostId != postId)
                {
                    i.Visible = false;
                }
            }
            divComments.Visible = true;
            List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
            filter.Add(DBObjCreator.CreateFilter("CommentPostId", postId, typeof(int)));
            List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_FindCommentsForPost", ref ex, filter);
            List<Comment> comments = new List<Comment>();
            records.ForEach(r => comments.Add(DBObjCreator.CreateObj<Comment>(r, typeof(Comment))));
            repeaterComments.DataSource = comments;
            repeaterComments.DataBind();

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
            pc.PostTagList = new List<string>();
            pc.TagSearch += new EventHandler(TagSearchEvent);
            
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
            pc.PostTagList = new List<string>();
            pc.TagSearch += new EventHandler(TagSearchEvent);
            if (Session["Username"] != null)
            {
                pc.PostImage = post.PostPhoto;
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
                pc.PostImage = post.PostPhoto;
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
            pc.PostTagList = new List<string>();
            pc.TagSearch += new EventHandler(TagSearchEvent);
            if (Session["Username"] != null)
            {
                pc.DisableFollowButton(Session["Username"].ToString());
            }
            if (Session["Guest"] != null)
            {
                pc.EnableGuestRestrictions();
            }

        }

        private void ShowAllPosts()
        {
            Greeting.InnerText = "All Posts";
            Session["CurrentView"] = ALL;
            repeaterAll.Visible = true;
            repeaterAll.Items.OfType<RepeaterItem>().ToList().ForEach(i => i.Visible = true);
            repeaterFollow.Visible = false;
            upAllRepeater.Update();
        }

        private void ShowFollowPosts()
        {
            Greeting.InnerText = "All Posts";
            Session["CurrentView"] = FOLLOW;
            repeaterAll.Visible = false;
            repeaterFollow.Items.OfType<RepeaterItem>().ToList().ForEach(i => i.Visible = true);
            repeaterFollow.Visible = true;
            upAllRepeater.Update();
        }

        protected void btnAllPosts_Click(object sender, EventArgs e)
        {
            ShowAllPosts();
        }


        protected void btnFollowPosts_Click(object sender, EventArgs e)
        {
            ShowFollowPosts();
        }

        protected void lnkAdvancedSearch_Click(object sender, EventArgs e)
        {
            divAdvSearch.Visible = !divAdvSearch.Visible;
            advSearch = !advSearch;
            Session["AdvSearch"] = advSearch;
            upSearch.Update();
        }

        private void TagSearch(string tag)
        {
            foreach (RepeaterItem i in repeaterAll.Items)
            {
                PostCard pc = i.FindControl("postCard") as PostCard;
                int index = pc.PostTagList.IndexOf(tag.Trim());
                if (pc.PostTagList.Count == 0 || index == -1)
                {
                    i.Visible = false;
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            repeaterAll.Items.OfType<RepeaterItem>().ToList().ForEach(i => i.Visible = true);
            string tagValue = txtSearch.Text;
            if (string.IsNullOrEmpty(tagValue) && divAdvSearch.Visible == false)
            {
                lblSearchError.Visible = true;
                lblSearchError.Text = "Must have at least one search critera";
                return;
            }
            else if (!string.IsNullOrEmpty(tagValue))
            {
                if(!(tagValue.ToCharArray()[0] == '#'))
                {
                    tagValue = $"#{tagValue}";
                }

                TagSearch(tagValue);

            }

            if (divAdvSearch.Visible)
            {

                if (!string.IsNullOrEmpty(txtUsername.Text))
                {

                    string usernameFilter = txtUsername.Text;
                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        if (!pc.PostUsername.Equals($"@{usernameFilter}"))
                        {
                            i.Visible = false;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(txtLikes.Text) && int.Parse(txtLikes.Text) > 0)
                {
                    int likes = int.Parse(txtLikes.Text);

                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        int pcLikes = int.Parse(pc.Likes.Split(':')[1].TrimStart());
                        if (string.IsNullOrEmpty(pc.Likes) || pcLikes < likes)
                        {
                            i.Visible = false;
                        }
                    }
                }
                if (ddlImage.SelectedIndex == 1)
                {
                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        if (pc.PostImage.Equals("fake.png"))
                        {
                            i.Visible = false;
                        }
                    }
                }
                if (ddlImage.SelectedIndex == 2)
                {
                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        if (!pc.PostImage.Equals("fake.png"))
                        {
                            i.Visible = false;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(txtFilterStartDate.Text))
                {
                    DateTime startDate = DateTime.Parse(txtFilterStartDate.Text);
                    
                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        DateTime pcDate = DateTime.Parse(pc.PostDate.Split(':')[1].TrimStart());
                        if (pcDate < startDate.Date)
                        {
                            i.Visible = false;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(txtFilterEndDate.Text))
                {
                    DateTime endDate = DateTime.Parse(txtFilterEndDate.Text);
                    foreach (RepeaterItem i in repeaterAll.Items)
                    {
                        PostCard pc = i.FindControl("postCard") as PostCard;
                        DateTime pcDate = DateTime.Parse(pc.PostDate.Split(':')[1].TrimStart());
                        if (pcDate > endDate.Date)
                        {
                            i.Visible = false;
                        }
                    }
                }

            }
            upAllRepeater.Update();
        }

        protected void btnNewPost_Click(object sender, EventArgs e)
        {
            containerNewPost.Visible = true;
            containerPosts.Visible = false;
        }

        protected void btnCreatePost_Click(object sender, EventArgs e)
        {
            string postText = txtPostText.Text;
            string postPhoto = txtPostPhoto.Text;
            string tag1 = txtTag1.Text;
            string tag2 = txtTag2.Text;
            string tag3 = txtTag3.Text;
            string username = Session["Username"].ToString();
            string postDate = DateTime.Today.ToShortDateString();

            if (postText == "")
            {
                smlPostTextHelp.InnerText = "Please add text to the post";
                return;
            }
            if (postPhoto == "")
            {
                postPhoto = "fake.png";
            }

            Post post = new Post(0, postDate, postText, postPhoto, username, 0);
            string url = "https://localhost:44312/api/Post/CreatePost";
            JavaScriptSerializer js = new JavaScriptSerializer();
            string obj = js.Serialize(post);

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = obj.Length;

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(obj);
            writer.Flush();
            writer.Close();

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            int postId = int.Parse(reader.ReadToEnd());
            reader.Close();
            response.Close();

            if (postId == 0)
            {
                smlPostTag3Help.InnerText = "Error creating new post, try again later.";
                return;
            }

            bool good = true;

            if (tag1 != "")
            {
                if (tag1.Substring(0, 1) != "#")
                {
                    tag1 = "#" + tag1;
                }

                Tag tag = new Tag(0, tag1, postId, false);
                string url2 = "https://localhost:44312/api/Tag/CreateTag";
                string obj2 = js.Serialize(tag);

                WebRequest request2 = WebRequest.Create(url2);
                request2.Method = "POST";
                request2.ContentType = "application/json";
                request2.ContentLength = obj2.Length;

                StreamWriter writer2 = new StreamWriter(request2.GetRequestStream());
                writer2.Write(obj2);
                writer2.Flush();
                writer2.Close();

                WebResponse response2 = request2.GetResponse();
                Stream stream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                bool tagCreated = bool.Parse(reader2.ReadToEnd());
                reader2.Close();
                response2.Close();

                if (!tagCreated)
                {
                    smlPostTag1Help.InnerText = "Error creating tag, please try again later";
                    good = false;
                }
            }

            if (tag2 != "")
            {
                if (tag2.Substring(0, 1) != "#")
                {
                    tag2 = "#" + tag2;
                }

                Tag tag = new Tag(0, tag2, postId, false);
                string url3 = "https://localhost:44312/api/Tag/CreateTag";
                string obj3 = js.Serialize(tag);

                WebRequest request3 = WebRequest.Create(url3);
                request3.Method = "POST";
                request3.ContentType = "application/json";
                request3.ContentLength = obj3.Length;

                StreamWriter writer3 = new StreamWriter(request3.GetRequestStream());
                writer3.Write(obj3);
                writer3.Flush();
                writer3.Close();

                WebResponse response3 = request3.GetResponse();
                Stream stream3 = response3.GetResponseStream();
                StreamReader reader3 = new StreamReader(stream3);
                bool tagCreated = bool.Parse(reader3.ReadToEnd());
                reader3.Close();
                response3.Close();

                if (!tagCreated)
                {
                    smlPostTag2Help.InnerText = "Error creating tag, please try again later";
                    good = false;
                }
            }

            if (tag3 != "")
            {
                if (tag3.Substring(0, 1) != "#")
                {
                    tag3 = "#" + tag3;
                }

                Tag tag = new Tag(0, tag3, postId, false);
                string url4 = "https://localhost:44312/api/Tag/CreateTag";
                string obj4 = js.Serialize(tag);

                WebRequest request4 = WebRequest.Create(url4);
                request4.Method = "POST";
                request4.ContentType = "application/json";
                request4.ContentLength = obj4.Length;

                StreamWriter writer4 = new StreamWriter(request4.GetRequestStream());
                writer4.Write(obj4);
                writer4.Flush();
                writer4.Close();

                WebResponse response4 = request4.GetResponse();
                Stream stream4 = response4.GetResponseStream();
                StreamReader reader4 = new StreamReader(stream4);
                bool tagCreated = bool.Parse(reader4.ReadToEnd());
                reader4.Close();
                response4.Close();

                if (!tagCreated)
                {
                    smlPostTag3Help.InnerText = "Error creating tag, please try again later";
                    good = false;
                }


            }
            if (!good)
            {
                return;
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            containerPosts.Visible = true;
            containerNewPost.Visible = false;
        }



        protected void repeaterComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            PostCard pc = e.Item.FindControl("postCard") as PostCard;
            Comment comment = e.Item.DataItem as Comment;

            pc.PostText = comment.CommentText;
            pc.PostUsername = comment.CommentUsername;
            pc.EnableCommentRestrictions();
            pc.PostId = comment.Id;
            if (Session["Username"] != null)
            {
                pc.DisableFollowButton(Session["Username"].ToString());
            }
            if (Session["Guest"] != null)
            {
                pc.EnableGuestRestrictions();
            }
        }

        protected void btnAddNewComment_Click(object sender, EventArgs e)
        {
            List<(bool, int, Exception)> ex = new List<(bool, int, Exception)>();
            int postId = (int)Session["CurrentParentPost"];
            string commentText = taComment.InnerText;

            if (string.IsNullOrEmpty(commentText))
            {
                taComment.InnerText = "Must add text to your comment";
                return;
            }
            else if (commentText.Length > 500)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Comment is too long(Max 500 characters)')", true);
            }

            Comment newComment = new Comment(-1, currentUser.Username, postId, commentText);
            bool result = DBObjWriter.GenericWriteToDB<Comment>(newComment, "TP_CreateComment", ref ex, new List<string> { "Id" });


            if (result)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Comment created succesfully')", true);
                Response.Redirect("Home.aspx");
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('There was an error creating your comment, please try again')", true);
                return;
            }
        }

        protected void btnCreateNewComment_Click(object sender, EventArgs e)
        {
            divCreateComment.Visible = !divCreateComment.Visible;
            upAllRepeater.Update();
        }
    }
}