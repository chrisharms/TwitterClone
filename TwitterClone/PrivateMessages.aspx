<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PrivateMessages.aspx.cs" Inherits="TwitterClone.PrivateMessages" %>

<%@ Register Src="PrivateMessage.ascx" TagName="PM" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-flex" id="pmContainer">
        <div class="row">
            <div class="col-4 composeNewPM">
                <asp:Button ID="btnComposeNew" runat="server" Text="Compose New" CssClass="btn-sm btn-danger" OnClick="btnComposeNew_Click" />
                <div id="divComposeNewPM" runat="server" visible="false">
                    <asp:Label ID="lblNewPmError" runat="server" Text="Label"></asp:Label>
                    <div class="form-group pmFormGroup" id="divNewPM" runat="server">
                        <div class="form-row mt-1">
                            <asp:TextBox ID="txtRecipient" runat="server" placeholder="Who is the message for?" CssClass="form-control pmControl"></asp:TextBox>
                        </div>
                        <div class="form-row mt-1">
                            <asp:TextBox ID="txtSubject" runat="server" placeholder="Subject" CssClass="form-control pmControl"></asp:TextBox>
                        </div>
                        <div class="form-row mt-1">
                            <textarea id="taPMText" cols="115" rows="5" class="pmControl" runat="server">Your Message...</textarea>
                        </div>
                        <asp:Button ID="btnSendNewPm" runat="server" Text="Send" CssClass="btn-primary" OnClick="btnSendNewPm_Click" />
                    </div>
                </div>
            </div>
            <div class="col-6">
                <asp:Label ID="lblRepeaterMessage" runat="server" Text=""></asp:Label>
                <asp:UpdatePanel ID="upPMs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="repeaterPMs" runat="server" OnItemDataBound="repeaterPMs_ItemDataBound">
                            <ItemTemplate>
                                    <div class="row">
                                        <div class="col-6">
                                            <%-- Private Message Form Start --%>
                                            <div class="card text-center pmCard pmHover" id="pmCard" runat="server">
                                                <div class="card-header">
                                                    <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div id="pmCardBody" class="card-body" runat="server">
                                                    <h5 class="card-title" id="pmSubject" runat="server"></h5>
                                                    <p class="card-text" id="pmText" runat="server"></p>
                                                </div>
                                                <div class="card-footer">
                                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                            <%-- Private Message Form End--%>
                                        </div>
                                    </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-2"></div>
        </div>
    </div>

</asp:Content>
