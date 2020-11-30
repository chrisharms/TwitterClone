﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>

<%@ Register Src="PostCard.ascx" TagName="PC" TagPrefix="uc1" %>
<%@ Register Src="TagControl.ascx" TagName="Tag" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="HomeContainer" class="container text-center justify-content-center">
        <h1 class="m-5">Welcome to Not Twitter!</h1>
        <h2 id="Greeting">What are people talking about right now?</h2>
        <div id="trendingContainer"  class="justify-content-center text-center border border-secondary">
            <div class="row justify-content-center">
                <div class="col-2"></div>
                <div class="col-8 ">
                    <asp:Repeater ID="repeaterTrending" runat="server" OnItemDataBound="repeaterTrending_ItemDataBound">
                        <ItemTemplate>
                            <uc1:PC runat="server" ID="postCard" />
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-2"></div>
            </div>
        </div>
    </div>

</asp:Content>
