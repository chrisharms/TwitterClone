﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TwitterClone.Homepage" %>
<%@ Register src="PostCard.ascx" tagname="PC" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center" style="width: 80%; background-color: white" >
    <h1>Welcome to not twitter.</h1>
    <uc1:PC ID="pcPost" runat="server" />
        </div>
</asp:Content>
