<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrivateMessage.ascx.cs" Inherits="TwitterClone.PrivateMessage" %>
<div class="card text-center pmCard pmHover" id="pmCard" runat="server">
        <div class="card-header">
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
        </div>
        <div id="pmCardBody" class="card-body" runat="server" visible="false">
            <h5 class="card-title" id="pmSubject" runat="server"></h5>
            <p class="card-text" id="pmText" runat="server"></p>
        </div>
        <div class="card-footer">
            <asp:Label ID="lblDate" runat="server"  Text=""></asp:Label>
        </div>
    </div>
