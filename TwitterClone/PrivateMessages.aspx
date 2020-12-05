<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PrivateMessages.aspx.cs" Inherits="TwitterClone.PrivateMessages" %>

<%@ Register Src="PrivateMessage.ascx" TagName="PM" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <uc3:PM runat="server" ID="privateMessage" />
    </div>

</asp:Content>
