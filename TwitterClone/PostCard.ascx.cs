﻿using System;
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
        

        protected void Page_Load(object sender, EventArgs e)
        {
            string pageName = Path.GetFileNameWithoutExtension(Page.AppRelativeVirtualPath);
            
            if (pageName == "UserProfile")
            {
                btnDeletePost.Visible = true;
            }

            populateTags();
        }

        public int PostId { get; set; }

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
            set { postUsername.InnerText = "@" + value; }
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
        public string HiddenField
        {
            get { return fldPostId.Value; }
            set { fldPostId.Value = value; }
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
            if (ViewState["TagList"] == null)
            {
                Exception ex = null;
                List<(string, dynamic, Type)> filter = new List<(string, dynamic, Type)>();
                filter.Add(DBObjCreator.CreateFilter("PostId", PostId, typeof(int)));
                List<object[]> records = DBObjCreator.ReadDBObjsWithWhere("TP_GetTagsByPost", ref ex, filter);
                List<Tag> tags = new List<Tag>();
                records.ForEach(r => tags.Add(DBObjCreator.CreateObj<Tag>(r, typeof(Tag))));
                ViewState["TagList"] = tags;
            }


            foreach (Tag t in (List<Tag>)ViewState["TagList"])
            {
                var tc = (TagControl)Page.LoadControl("TagControl.ascx");
                tc.Text = t.TagText;
                tc.ButtonClick += new EventHandler(Tag_ButtonClick);
                ph.Controls.Add(tc);
            }
        }

        protected void Tag_ButtonClick(object sender, EventArgs e)
        {

        }

        protected void lnkLike_Click(object sender, EventArgs e)
        {

        }
    }
}