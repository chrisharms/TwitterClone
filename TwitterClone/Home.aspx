<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>

<%@ Register Src="PostCard.ascx" TagName="PC" TagPrefix="uc1" %>
<%@ Register Src="TagControl.ascx" TagName="Tag" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    
                    <div id="followContainer" class="justify-content-center text-center">
                        <div class="row justify-content-center">
                            <div class="col-10 ">
                                <asp:UpdatePanel ID="upAllRepeater" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <h2 runat="server" id="Greeting">Who You're Following</h2>
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
                                        <!-- Comments Start -->
                                        <div id="divComments" runat="server" visible="false">
                                            <div id="divCreateComment" class="mt-5" runat="server">
                                                <label class="mt-4 mb-3">Add a New Comment!</label>
                                                <textarea id="taComment" cols="50" rows="3" class="form-control" runat="server"></textarea>
                                                <small id="smlCommentHelp" runat="server" class="form-text text-danger"></small>
                                                <asp:Button ID="btnAddNewComment" CssClass="btn btn-success mt-3 mb-2" runat="server" Text="Create Comment" OnClick="btnAddNewComment_Click" />
                                            </div>
                                            <h3 runat="server" class="mt-5" id="h5NoComments" visible="false">This post has no comments :/</h3>
                                            <asp:Repeater ID="repeaterComments" runat="server"  OnItemDataBound="repeaterComments_ItemDataBound">
                                                <ItemTemplate>
                                                    <uc1:PC runat="server" ID="postCard" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <!-- Comments End -->
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-4">
                <asp:UpdatePanel ID="upSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="searchPanel" class="searchPanel" runat="server">
                            <div class="text-center">
                                <asp:Label ID="lblSearchError" CssClass="text-danger" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                            
                            <h2 id="searchTitle" class="text-center">Search</h2>
                            <div id="searchInput" class="searchInput mx-auto">
                                <asp:TextBox ID="txtSearch" Placeholder="Enter a tag to search for..." CssClass="form-control searchTextBox" runat="server"></asp:TextBox>
                                <div class="containerAdvancedSearch text-center">
                                    <asp:LinkButton ID="lnkAdvancedSearch" CssClass="mt-5" runat="server" OnClick="lnkAdvancedSearch_Click">Advanced Search</asp:LinkButton>
                                </div>
                                
                                <div id="divAdvSearch" class="form-group advSearch mb-0" visible="false" runat="server">
                                    <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" CssClass="form-control searchTextBox"></asp:TextBox>
                                    <asp:TextBox ID="txtLikes" runat="server" placeholder="Number of likes" TextMode="Number" CssClass="form-control searchTextBox"></asp:TextBox>
                                    <asp:DropDownList ID="ddlImage" runat="server" CssClass="form-control searchTextBox">
                                        <asp:ListItem Value="NoImageChoice">Don&#39;t Filter By Image</asp:ListItem>
                                        <asp:ListItem>Has Image</asp:ListItem>
                                        <asp:ListItem>No Image</asp:ListItem>
                                    </asp:DropDownList>
                                    <div class="row no-gutters">
                                        <div class="col-2 my-4">
                                            <asp:Label ID="lblStartDate" runat="server" AssociatedControlID="txtFilterStartDate" Text="Start Date: "></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtFilterStartDate" runat="server" TextMode="Date" placeholder="Start Date" CssClass="form-control searchTextBox"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row no-gutters">
                                        <div class="col-2 my-4">
                                            <asp:Label ID="lblEndDate" runat="server" AssociatedControlID="txtFilterEndDate" Text="End Date: "></asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtFilterEndDate" runat="server" TextMode="Date" placeholder="End Date" CssClass="form-control searchTextBox"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class=" d-flex form-group justify-content-center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnSearch btn-sm" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="FollowsPanel" class="followsPanel">
                    <h2 id="followsTitle" class="text-center mt-4">What's Trending?</h2>
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
    <div class="container" id="containerNewPost" runat="server" visible="false">
        <h2 class="mb-5">Create a New Post!</h2>
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
            <asp:Button runat="server" Text="Create Post" ID="btnCreatePost" CssClass="btn btn-primary" OnClick="btnCreatePost_Click" />
            <asp:Button runat="server" Text="Cancel" ID="btnCancel" CssClass="btn btn-danger ml-2" OnClick="btnCancel_Click" />
        </div>
    </div>


</asp:Content>
