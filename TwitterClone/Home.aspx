<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>

<%@ Register Src="PostCard.ascx" TagName="PC" TagPrefix="uc1" %>
<%@ Register Src="TagControl.ascx" TagName="Tag" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div id="contentRow" class="row">
            <div class="col-3">
            </div>
            <div class="col-5">
                <div id="HomeContainer" class="container text-center justify-content-center">
                    <h1 class="m-5">Welcome to Not Twitter!</h1>
                    <div class="d-flex justify-content-center">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="All Posts" ID="btnAllPosts" OnClick="btnAllPosts_Click" />
                        <asp:Button runat="server" CssClass="btn btn-primary ml-2" Text="My Followers Posts" ID="btnFollowPosts" OnClick="btnFollowPosts_Click" />
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
                                <asp:UpdatePanel ID="upAllRepeater" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                <asp:Repeater ID="repeaterAll" runat="server" OnItemDataBound="repeaterAll_ItemDataBound">
                                    <ItemTemplate>
                                        <uc1:PC runat="server" ID="postCard" />
                                    </ItemTemplate>
                                </asp:Repeater>
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
                            <asp:Label ID="lblSearchError" runat="server" Text="" Visible="false"></asp:Label>
                            <h3 id="searchTitle">Search</h3>
                            <div id="searchInput" class="searchInput">
                                <asp:TextBox ID="txtSearch" Placeholder="Enter a tag to search for..." CssClass="form-control searchTextBox" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="lnkAdvancedSearch" runat="server" OnClick="lnkAdvancedSearch_Click">Advanced Search...</asp:LinkButton>
                                <div id="divAdvSearch" class="form-group advSearch" visible="false" runat="server">
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
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnSearch btn-sm" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
</asp:Content>
