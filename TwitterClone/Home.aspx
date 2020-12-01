<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>

<%@ Register Src="PostCard.ascx" TagName="PC" TagPrefix="uc1" %>
<%@ Register Src="TagControl.ascx" TagName="Tag" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div id="contentRow" class="row">
            <div class="col-3">
            </div>
            <div class="col-6">
                <div id="HomeContainer" class="container text-center justify-content-center">
                    <h1 class="m-5">Welcome to Not Twitter!</h1>
                    <h2 id="Greeting">Who You're Paying Attention To</h2>
                    <div id="followContainer" class="justify-content-center text-center">
                        <div class="row justify-content-center">
                            <div class="col-10 ">
                                <asp:Repeater ID="repeaterFollow" runat="server" OnItemDataBound="repeaterFollow_ItemDataBound">
                                    <ItemTemplate>
                                        <uc1:PC runat="server" ID="postCard" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-3">
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

</asp:Content>
