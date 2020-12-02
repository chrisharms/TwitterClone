<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>

<%@ Register Src="PostCard.ascx" TagName="PC" TagPrefix="uc1" %>
<%@ Register Src="TagControl.ascx" TagName="Tag" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" id="containerPosts" runat="server">
        <div id="contentRow" class="row">
            <div class="col-3">
            </div>
            <div class="col-5">
                <div id="HomeContainer" class="container text-center justify-content-center">
                    <div class="d-flex justify-content-center m-5">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="All Posts" ID="btnAllPosts" OnClick="btnAllPosts_Click" />
                        <asp:Button runat="server" CssClass="btn btn-primary ml-2" Text="My Follower's Posts" ID="btnFollowPosts" OnClick="btnFollowPosts_Click" />
                        <asp:Button runat="server" CssClass="btn btn-danger ml-2" Text="New Post" ID="btnNewPost" OnClick="btnNewPost_Click" />
                    </div>
                    <h2 runat="server" id="Greeting">Who You're Following</h2>
                    <div id="followContainer" class="justify-content-center text-center">
                        <div class="row justify-content-center">
                            <div class="col-10 ">
                                <asp:Repeater ID="repeaterFollow" runat="server" OnItemDataBound="repeaterFollow_ItemDataBound">
                                    <ItemTemplate>
                                        <uc1:PC runat="server" ID="postCard" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="repeaterAll" runat="server" OnItemDataBound="repeaterAll_ItemDataBound">
                                    <ItemTemplate>
                                        <uc1:PC runat="server" ID="postCard" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-4">
                <div id="searchPanel" class="searchPanel" runat="server">
                    <h3 id="searchTitle">Search</h3>
                    <div id="searchInput" class="searchInput">
                        <asp:TextBox ID="txtSearch" Placeholder="Enter a tag to search for..." CssClass="form-control searchTextBox" runat="server"></asp:TextBox>
                        <asp:LinkButton ID="lnkAdvancedSearch" runat="server">Advanced Search...</asp:LinkButton>
                    </div>
                    <div id="FollowsPanel" class="followsPanel">
                        <h3 id="followsTitle">What are people talking about right now?</h3>
                        <div id="trendingContainer" class="justify-content-center text-center">
                            <div class="row justify-content-center">
                                <div class="col-10">
                                    <asp:Repeater ID="repeaterTrending" runat="server" OnItemDataBound="repeaterTrending_ItemDataBound">
                                        <ItemTemplate>
                                            <uc1:PC runat="server" ID="postCard" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="container" id="containerNewPost" runat="server" visible="false">
        <h3 class="mb-5">Create a New Post!</h3>
        <div class="form-group">
            <label>Post Text</label>
            <small id="Small1" runat="server" class="form-text text-danger mb-2">Required</small>
            <asp:TextBox runat="server" ID="txtPostText" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
            <small id="smlPostTextHelp" runat="server" class="form-text text-danger"></small>
        </div>
        <div class="form-group">
            <label>Image URL</label>
            <asp:TextBox runat="server" ID="txtPostPhoto" CssClass="form-control"></asp:TextBox>
            <small id="smlPostPhotoHelp" runat="server" class="form-text text-danger"></small>
        </div>
        <div class="form-group">
            <label>Post Tag 1</label>
            <asp:TextBox runat="server" ID="txtTag1" Placeholder="#Dope" CssClass="form-control"></asp:TextBox>
            <small id="smlPostTag1Help" runat="server" class="form-text text-danger"></small>
        </div>
        <div class="form-group">
            <label>Post Tag 2</label>
            <asp:TextBox runat="server" ID="txtTag2" Placeholder="#Corona" CssClass="form-control"></asp:TextBox>
            <small id="smlPostTag2Help" runat="server" class="form-text text-danger"></small>
        </div>
        <div class="form-group">
            <label>Post Tag 3</label>
            <asp:TextBox runat="server" ID="txtTag3" Placeholder="#Zoom" CssClass="form-control"></asp:TextBox>
            <small id="smlPostTag3Help" runat="server" class="form-text text-danger"></small>
        </div>
        <div class="form-group mt-5">
            <asp:Button runat="server" Text="Create Post" ID="btnCreatePost" CssClass="btn btn-primary" OnClick="btnCreatePost_Click"/>
            <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn btn-danger ml-2" OnClick="btnCancel_Click"/>
        </div>
    </div>

</asp:Content>
