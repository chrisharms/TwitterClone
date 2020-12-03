using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using TwitterClassLibrary.DBObjCreator;
using TwitterClassLibrary;

namespace TwitterClone
{
    public partial class PostCard : System.Web.UI.UserControl
    {
        private List<string> tags = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            
            if (pageName == "UserProfile")
            {
                btnDeletePost.Visible = true;
                btnFollowUser.Visible = false;
            }

            populateTags();

        }

        public int PostId {
            get { return int.Parse(fldPostId.Value); }
            set { fldPostId.Value = value.ToString(); }
        }


        public string PostImage
        {
            get { return imgPost.ImageUrl; }
            set { imgPost.ImageUrl = value; }
        }

        public List<string> PostTagList
        {
            get { return tags; }
            set { tags = value; }
        }

        public void AddTag(string tag)
        {
            PostTagList.Add(tag);
        }

        public string PostText
        {
            get { return txtPost.InnerText; }
            set { txtPost.InnerText = value; }
        }

        public string PostUsername
        {
            get { return postUsername.InnerText; }
            set { postUsername.InnerText = "@" + value; }
        }

        public string PostDate
        {
            get { return postDate.InnerText; }
            set { postDate.InnerText = $"Posted: {value}"; }
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
        public void DisableFollowButton(string username)
        {
            if (username == postUsername.InnerText.Substring(1))
            {
                btnFollowUser.Visible = false;
            }
            
        }
        public void EnableGuestRestrictions()
        {
            btnFollowUser.Visible = false;
            lnkLike.Enabled = false;
        }

        protected void btnDeletePost_Click(object sender, EventArgs e)
        {
            string postId = fldPostId.Value;
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + postId + "')", true);
            // Deleting Post
            string url = "https://localhost:44312/api/Post/DeletePost";
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postId.Length;

            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(postId);
            writer.Flush();
            writer.Close();


            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            String data = reader.ReadToEnd();
            reader.Close();
            response.Close();

            if (data != "Post succesfully deleted")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + data + "')", true);
                return;
            }
            
            Response.Redirect("UserProfile.aspx");
        }

        protected void populateTags()
        {
            List<Tag> tags = new List<Tag>();

            if (ViewState["TagList"] == null)
            {
                Exception ex = null;
                List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
                filter.Add(DBObjCreator.CreateFilter("PostId", PostId, typeof(int)));
                List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetTagsByPost", ref ex, filter);
                records.ForEach(r => tags.Add(DBObjCreator.CreateObj<Tag>(r, typeof(Tag))));
                ViewState["TagList"] = tags;
            }



            tags = (List<Tag>)ViewState["TagList"];

            foreach (Tag t in tags)
            {
                var tc = (TagControl)Page.LoadControl("TagControl.ascx");
                tc.Text = t.TagText;
                AddTag(t.TagText);
                tc.ButtonClick += new EventHandler(Tag_ButtonClick);
                ph.Controls.Add(tc);
            }
        }

        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks tag button")]
        public event EventHandler TagSearch;

        protected void Tag_ButtonClick(object sender, EventArgs e)
        {
            TagSearch?.Invoke(sender, e);
        }

        protected void lnkLike_Click(object sender, EventArgs e)
        {
            int postId = PostId;
            string text = PostText;
            string username = Session["Username"].ToString();
            string[] likeStuff = Likes.Split(':');
            int likes = int.Parse(likeStuff[1]);

            List<PostCard> allControls = new List<PostCard>();
            GetControlList<PostCard>(Page.Controls, allControls);


            string url = "https://localhost:44312/api/Like/CheckLike/" + username + "/" + postId;
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            bool data = bool.Parse(reader.ReadToEnd());
            reader.Close();
            response.Close();

            if (data)
            {
                // User has liked
                string url2 = "https://localhost:44312/api/Like/DeleteLike/" + username + "/" + postId;
                WebRequest request2 = WebRequest.Create(url2);
                request2.Method = "DELETE";
                request2.ContentType = "application/json";

                WebResponse response2 = request2.GetResponse();
                Stream stream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                bool data2 = bool.Parse(reader2.ReadToEnd());
                reader2.Close();
                response2.Close();

                if (data2)
                {
                    foreach (PostCard childControl in allControls)
                    {
                        if (childControl.PostId == postId)
                        {
                            string[] likeStuff2 = childControl.Likes.Split(':');
                            int likes2 = int.Parse(likeStuff[1]);
                            childControl.Likes = (likes2 - 1).ToString();
                        }
                        
                    }
                    // Likes = (likes - 1).ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Post has been unliked')", true);
                }
            }
            else
            {
                // User hasnt liked
                Like like = new Like(0, postId, username);
                JavaScriptSerializer js = new JavaScriptSerializer();

                string url2 = "https://localhost:44312/api/Like/AddLike";
                WebRequest request2 = WebRequest.Create(url2);
                request2.Method = "POST";
                request2.ContentType = "application/json";
                request2.ContentLength = js.Serialize(like).Length;

                StreamWriter writer2 = new StreamWriter(request2.GetRequestStream());
                writer2.Write(js.Serialize(like));
                writer2.Flush();
                writer2.Close();

                WebResponse response2 = request2.GetResponse();
                Stream stream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                bool data2 = bool.Parse(reader2.ReadToEnd());
                reader2.Close();
                response2.Close();
                if (data2)
                {
                    foreach (PostCard childControl in allControls)
                    {
                        if (childControl.PostId == postId)
                        {
                            string[] likeStuff3 = childControl.Likes.Split(':');
                            int likes3 = int.Parse(likeStuff[1]);
                            childControl.Likes = (likes3 + 1).ToString();
                        }
                        
                    }
                    // Likes = (likes + 1).ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Post has been liked')", true);
                }

            }
        }

        protected void btnComments_Click(object sender, EventArgs e)
        {

        }

        protected void btnFollowUser_Click(object sender, EventArgs e)
        {
            string username = Session["Username"].ToString();
            string FollowUsername = postUsername.InnerText.Substring(1);
            bool same = FollowUsername == username;
            if (same)
            {
                return;
            }

            string url = "https://localhost:44312/api/Follow/VerifyFollow/" + username + "/" + FollowUsername;
            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            bool data = bool.Parse(reader.ReadToEnd());
            reader.Close();
            response.Close();

            if (data)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User is already being followed.')", true);
            }
            else
            {
                DateTime today = DateTime.Today;
                Follow follow = new Follow(username, FollowUsername, today.ToShortDateString());
                JavaScriptSerializer js = new JavaScriptSerializer();
                string obj = js.Serialize(follow);

                string url2 = "https://localhost:44312/api/Follow/CreateFollow";
                WebRequest request2 = WebRequest.Create(url2);
                request2.Method = "POST";
                request2.ContentType = "application/json";
                request2.ContentLength = obj.Length;

                StreamWriter writer2 = new StreamWriter(request2.GetRequestStream());
                writer2.Write(obj);
                writer2.Flush();
                writer2.Close();


                WebResponse response2 = request2.GetResponse();
                Stream stream2 = response2.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                bool data2 = bool.Parse(reader2.ReadToEnd());
                reader2.Close();
                response2.Close();
                if (data2)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are now following " + FollowUsername + "')", true);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Error following user, try again later')", true);
                }

            }

        }

        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
        where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) // This is cleaner
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
            }
        }
    }
}