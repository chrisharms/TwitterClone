<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostCard.ascx.cs" Inherits="TwitterClone.PostCard" %>

<div class="card border-primary">
    <asp:Image ID="imgPost" CssClass="card-img-top" style="width: 100%; height: 300px" runat="server" />
    <div class="card-body">
        <h5 id="postTitle" class="card-title" runat="server">Card title</h5>
        <p class="card-text" id="txtPost" runat="server"></p>
        <div class="row">
            <div id="tagList" runat="server">
                <asp:PlaceHolder ID="phTagList" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="row">
            <asp:Image ID="heartIcon" runat="server" ImageUrl="~/Images/HeartIcon-removebg-preview.png" />
            <asp:Label ID="lblLikes" runat="server" Text="Likes: 0" CssClass="my-5"></asp:Label>
        </div>
        <div class="row justify-content-center">
            <asp:Button ID="btnViewComments" runat="server" Text="View Comments" CssClass="btn btn-sm btn-primary " />
        </div>
    </div>
</div>
