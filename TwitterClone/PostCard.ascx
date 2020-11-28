<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostCard.ascx.cs" Inherits="TwitterClone.PostCard" %>

<div class="card" style="width: 18rem;">
  <asp:Image ID="imgPost" CssClass="card-img-top" runat="server" />
  <div class="card-body">
    <h5 id="postTitle" class="card-title" runat="server">Card title</h5>
    <p class="card-text" id="txtPost" runat="server"></p>
    <asp:Button ID="btnViewComments" runat="server" Text="View Comments"  CssClass="btn btn-primary"/>
    <div id="tagList" runat="server"></div>
  </div>
</div>