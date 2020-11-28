<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TwitterClone.UserProfile" %>
<%@ Register src="PostCard.ascx" tagname="PC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h3 class="mt-3 mb-3 mr-3">My Profile</h3>
        <asp:ScriptManager runat="server" ID="ScriptManager">

        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        </asp:UpdatePanel>

    </div>
</asp:Content>
