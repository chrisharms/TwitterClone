<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TwitterClone.UserProfile" %>
<%@ Register src="PostCard.ascx" tagname="PC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h3 class="mt-3 mb-3 mr-3">My Profile</h3>
        <asp:ScriptManager runat="server" ID="ScriptManager">
             
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        </asp:UpdatePanel>
        <div class="card mt-4" style="width: 90%;">
            
            <div class="card-body">
                <div class="row">
                    <div class="col">
                        <asp:Image runat="server" ID="imgProfileImage" Width="250px" CssClass="card-img-top"/>
                        <h4 class="card-title mt-3 mb-3" runat="server" id="lblUsername">Card title</h4>
                        <asp:Button runat="server" CssClass="btn-sm btn-primary mb-3" Text="Edit Profile" ID="btnEditProfile" OnClick="btnEditProfile_Click"></asp:Button>
                        <asp:LinkButton runat="server" CssClass="text-danger small ml-2 mb-3" Text="Delete Profile" ID="lnkDeleteProfile" OnClick="lnkDeleteProfile_Click"></asp:LinkButton>
                    </div>
                    <div class="col">
                        
                        <h6>Name</h6>
                        <p runat="server" id="lblName"></p>
                        <h6>Email Address</h6>
                        <p runat="server" id="lblEmail"></p>
                        <h6>Phone Number</h6>
                        <p runat="server" id="lblPhone"></p>
                        
                    </div>
                    <div class="col">
                        <h6>Home Address</h6>
                        <p runat="server" id="lblHomeAddress"></p>
                        <h6>Billing Address</h6>
                        <p runat="server" id="lblBillingAddress"></p>
                        
                    </div>
                    <div class="col">
                        <h6>Security Question 1</h6>
                        <p runat="server" id="lblSecurityQuestion1"></p>
                        <h6>Security Question 2</h6>
                        <p runat="server" id="lblSecurityQuestion2"></p>
                        <h6>Security Question 3</h6>
                        <p runat="server" id="lblSecurityQuestion3"></p>
                        
                    </div>
                    
                </div>
                
            </div>
        </div>

    </div>
    <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
    <div class="container" id="divPostContainer">
        <h3 class="mt-3 mb-3 mr-3">My Posts</h3>
    </div>
</asp:Content>
