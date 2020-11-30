﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TwitterClone.UserProfile" %>
<%@ Register src="PostCard.ascx" tagname="PC" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" runat="server" id="divMyProfile">
        <h3 class="mt-3 mb-3 mr-3">My Profile</h3>
        <asp:ScriptManager runat="server" ID="ScriptManager">
             
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                        <div class="d-flex justify-content-center">
                    <div class="card mt-4" style="width: 90%;">
            
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <asp:Image runat="server" ID="imgProfileImage" Width="250px" CssClass="card-img-top"/>
                                    <h4 class="card-title mt-3 mb-3" runat="server" id="lblUsername">Card title</h4>
                        
                                </div>
                                <div class="col">
                        
                                    <h6>First Name</h6>
                                    <p runat="server" id="lblFirstName"></p>
                                    <h6>Last Name</h6>
                                    <p runat="server" id="lblLastName"></p>
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
                            <div class="row">
                                <div class="col">
                                    <asp:Button runat="server" CssClass="btn-sm btn-primary mb-3" Text="Edit Profile" ID="btnEditProfile" OnClick="btnEditProfile_Click"></asp:Button>
                                    <asp:LinkButton runat="server" CssClass="text-danger small ml-2 mb-3" Text="Delete Profile" ID="lnkDeleteProfile" OnClick="lnkDeleteProfile_Click"></asp:LinkButton>
                                </div>
                            </div>
                
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnEditProfile"/> 
                <asp:AsyncPostBackTrigger ControlID="btnUpdateProfile" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

        

    </div>
    <div runat="server" class="container" id="divUpdateProfile"  visible="false">
        <h3 class="mt-3 mb-5 mr-3">Update Profile</h3>
        <div class="form-group">
            <label>Username</label>
            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
            <small id="smlUsernameHelp" runat="server" class="form-text text-danger"></small>
         </div>
          <div class="form-group">
                    <label>First Name</label>
                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                    <small id="smlFirstNameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Last Name</label>
                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"></asp:TextBox>
                    <small id="smlLastNameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Home Address</label>
                    <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control"></asp:TextBox>
                    <small id="smlHomeAddressHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Billing Address</label>
                    <asp:TextBox runat="server" ID="txtBillingAddress" CssClass="form-control"></asp:TextBox>
                    <small id="smlBillingAddressHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Phone Number</label>
                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                    <small id="smlPhoneHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Profile Picture</label>
                    <asp:TextBox runat="server" ID="txtImage" CssClass="form-control"></asp:TextBox>
                    <small id="smlImageHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 1</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity1">
                        <asp:ListItem Value="0">What is your oldest sibling's name?</asp:ListItem>
                        <asp:ListItem Value="1">What is the name of the elementary school you want to?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtSecurity1"></asp:TextBox>
                    <small id="smlQuestion1Help" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 2</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity2">
                        <asp:ListItem Value="2">What was the name of your favorite pet?</asp:ListItem>
                        <asp:ListItem Value="3">What is your favorite color?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtSecurity2"></asp:TextBox>
                    <small id="smlQuestion2Help" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 3</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity3">
                        <asp:ListItem Value="4">What is the name of your first employer?</asp:ListItem>
                        <asp:ListItem Value="5">What is the name of your favorite movie?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtSecurity3"></asp:TextBox>
                    <small id="smlQuestion3Help" runat="server" class="form-text text-danger"></small>
                </div>
        <div class="form-group mt-5">
            <asp:Button runat="server" ID="btnUpdateProfile" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdateProfile_Click"/>
            <asp:Button runat="server" ID="btnCancelUpdate" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancelUpdate_Click"/>
        </div>
    </div>
    <div runat="server" class="container" id="divPostContainer" >
        <h3 class="mt-5 mb-3 mr-3">My Posts</h3>
    </div>
</asp:Content>