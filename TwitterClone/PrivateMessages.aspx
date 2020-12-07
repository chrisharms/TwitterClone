<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="PrivateMessages.aspx.cs" Inherits="TwitterClone.PrivateMessages" %>

<%@ Register Src="PrivateMessage.ascx" TagName="PM" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container" id="pmContainer">
        
            <h2 class="mt-3 mb-3">My Messages</h2>
            <div class="composeNewPM">
                <div class="form-group">
                    <asp:Button ID="btnComposeNew" runat="server" Text="Compose New" CssClass="btn-sm btn-danger mb-4" OnClick="btnComposeNew_Click" />
                </div>
                
                <div id="divComposeNewPM" class="mx-auto mb-5" runat="server" visible="false" style="width: 75%">
                    <asp:Label ID="lblNewPmError" runat="server" Text="Label"></asp:Label>
                    <div class="form-group pmFormGroup" id="divNewPM" runat="server">
                        <div class="form-row mb-1">
                            <asp:TextBox ID="txtRecipient" runat="server" placeholder="Who is the message for?" CssClass="form-control pmControl"></asp:TextBox>
                            <small id="smlRecipientHelp" runat="server" class="form-text text-danger ml-3"></small>
                        </div>
                        <div class="form-row mb-1">
                            <asp:TextBox ID="txtSubject" runat="server" placeholder="Subject" CssClass="form-control pmControl"></asp:TextBox>
                            <small id="smlSubjectHelp" runat="server" class="form-text text-danger ml-3"></small>
                        </div>
                        <div class="form-row mb-1">
                            <textarea id="taPMText" cols="115" rows="5" class="form-control pmControl" placeholder="Your Message..." runat="server"></textarea>
                            <small id="smlTextHelp" runat="server" class="form-text text-danger ml-3"></small>
                        </div>
                        <div class="form-group ml-3">
                            <asp:Button ID="btnSendNewPm" runat="server" Text="Send" CssClass="btn-sm btn-primary" OnClick="btnSendNewPm_Click" />
                        </div>
                        
                    </div>
                </div>
            </div>
            <div class="">
                <asp:Label ID="lblRepeaterMessage" runat="server" Text=""></asp:Label>
                <asp:UpdatePanel ID="upPMs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Repeater ID="repeaterPMs" runat="server" OnItemDataBound="repeaterPMs_ItemDataBound">
                            <ItemTemplate>
                                    
                                        <div class=" d-flex justify-content-center mb-4">
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
                                    
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
        
    </div>

</asp:Content>
