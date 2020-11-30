<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostCard.ascx.cs" Inherits="TwitterClone.PostCard" %>

<div class="card border-secondary" style="margin-top: 1em">
    <asp:Image ID="imgPost" CssClass="card-img-top" Style="width: 100%; height: 300px" runat="server" />
    <div class="card-body">
        <h5 id="postUsername" class="card-header border-secondary" style="background-color:" runat="server">Card title</h5>
        <div id="cardText" class="row" style="height: 128px; width: 100%">
            <div class="col-2"></div>
            <div class="col-8 border-primary">
                <p class="card-text" id="txtPost" runat="server"></p>
            </div>
            <div class="col-2"></div>
            </div>
        <div id="tagRow" class="row">
            <div id="tagList" runat="server">
                <asp:PlaceHolder ID="phTagList" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="row">
            <asp:Image ID="heartIcon" runat="server" ImageUrl="~/Images/HeartIcon-removebg-preview.png" />
            <asp:Label ID="lblLikes" runat="server" Text="Likes: 0" CssClass="my-5"></asp:Label>
        </div>
        <div class="row justify-content-center">
            <asp:Button ID="btnViewComments" runat="server" Text="View Comments" CssClass="btn btn-sm btnViewComments" />
        </div>
    </div>
</div>
