<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostCard.ascx.cs" Inherits="TwitterClone.PostCard" EnableViewState="True" %>

<div class="card border-secondary mx-auto" style="margin-top: 3em">
    <asp:Image ID="imgPost" CssClass="card-img-top mx-auto rounded mt-2" Width="70%" Height="200px" runat="server" />
    <div class="card-body">
        <asp:HiddenField runat="server" ID="fldPostId"/>
        <h4 id="postUsername" class="card-header" runat="server">Card title</h4>
        <h5 id="postDate" runat="server"></h5>
        <div id="cardText" class="row mt-4 mb-4" style=" width: 100%">
            <div class="col-2"></div>
            <div class="col-8">
                <p class="card-text" id="txtPost" runat="server"></p>
            </div>
            <div class="col-2"></div>
            </div>
        <div id="tagRow" class="row ml-1">
            <div id="tagList" runat="server">
                <asp:PlaceHolder ID="phTagList" runat="server"></asp:PlaceHolder>
            </div>
        </div>
        <div class="row ml-1">
            <asp:LinkButton runat="server" ID="lnkLike" OnClick="lnkLike_Click">
                <asp:Image ID="heartIcon" CssClass="my-4 mr-2" runat="server" ImageUrl="~/Images/1024px-Heart_corazón.svg.png" Width="30px" Height="30px"/>
            </asp:LinkButton>
            
            <asp:Label ID="lblLikes" runat="server" Text="Likes: 0" CssClass="my-4"></asp:Label>
        </div>
        <div class="row justify-content-center">
            <asp:Button ID="btnComments" runat="server" Text="Comments" CssClass="btn btn-sm btnViewComments" OnClick="btnComments_Click" />
            <asp:Button ID="btnFollowUser" runat="server" Text="Follow User" CssClass="btn btn-sm btn-primary ml-2" OnClick="btnFollowUser_Click" />
            <asp:Button ID="btnDeletePost" runat="server" Text="Delete Post" CssClass="btn btn-sm btn-danger ml-2" visible="false" OnClick="btnDeletePost_Click"/>
        </div>
    </div>
</div>
