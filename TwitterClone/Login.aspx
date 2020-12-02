<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TwitterClone.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Twitter - Login</title>
    <link rel="stylesheet" href="CSS/Styles.css"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="jumbotron m-4">
                    <h1>Not Twitter</h1>
                    <p>Join us in using the next best thing to Twitter. But seriously it's not a copy, ours is much better.</p>
                </div>
                <div class="form-group text-center">
                    <asp:Button runat="server" CssClass="btn btn-success btn-lg m-2" Text="Login" ID="btnLogin" OnClick="btnLogin_Click"/>
                    <asp:Button runat="server" CssClass="btn btn-light btn-lg m-2" Text="Register" ID="btnRegister" OnClick="btnRegister_Click"/>

                </div>
                <div class="form-group mt-3 text-center">
                    
                    <asp:LinkButton runat="server" CssClass="form-text text-info" ID="lnkGuest" OnClick="lnkGuest_Click">Continue as Guest</asp:LinkButton>
                </div>
            </div>
            <div class="container mt-5" id="divLogin" runat="server" visible="false">
                <div class="form-group">
                    <label>Username</label>
                    <asp:TextBox runat="server" ID="txtLogUsername" CssClass="form-control"></asp:TextBox>
                    <small id="smlLogUsernameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Password</label>
                    <asp:TextBox runat="server" ID="txtLogPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <small id="smlLogPasswordHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-check">
                    <input runat="server" type="checkbox" class="form-check-input" id="chkLoginCookie"/>
                    <label class="form-check-label" for="chkCookie">Store cookie for auto login</label>
                </div>
                <div class="form-group mt-3">
                    <asp:LinkButton runat="server" CssClass="form-text text-info" ID="lnkForgotUsername" OnClick="lnkForgotUsername_Click">Forgot Username</asp:LinkButton>
                    <asp:LinkButton runat="server" CssClass="form-text text-info" ID="lnkForgotPassword" OnClick="lnkForgotPassword_Click">Forgot Password</asp:LinkButton>
                </div>
                <div class="form-group text-center">
                    <asp:Button runat="server" CssClass="btn btn-danger m-3" Text="Submit Login" ID="btnSubmitLogin" OnClick="btnSubmitLogin_Click"/>
                </div>
            </div>
            <div class="container mt-5" id="divForgotUsername" runat="server" visible="false">
                <div class="form-group" runat="server">
                    <label>Verify Email Address</label>
                    <asp:TextBox runat="server" ID="txtVerifyEmail" CssClass="form-control"></asp:TextBox>
                    <small id="smlVerifyEmailHelp" runat="server" class="form-text text-danger"></small>
                    
                </div>
                <div class="form-group text-center" runat="server">
                    <asp:Button runat="server" CssClass="btn btn-danger mt-3" Text="Verify" ID="btnVerifyEmail" OnClick="btnVerifyEmail_Click"/>
                </div>
                <div class="form-group mt-5" id="divUsernameSecretQuestion" runat="server" visible="false">
                    <label>Please answer the secret question to retrieve your username.</label><br />
                    <label runat="server" id="lblUsernameSecretQuestion"></label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtUsernameSecretAnswer"></asp:TextBox>
                    <small id="smlForgotUsernameHelp" runat="server" class="form-text text-danger"></small>
                    <div class="form-group text-center">
                        <asp:Button runat="server" CssClass="btn btn-danger m-3" Text="Get My Username" ID="btnRetrieveUsername" OnClick="btnRetrieveUsername_Click"/>
                    </div>
                </div>
                
            </div>
            <div class="container mt-5" id="divForgotPassword" runat="server" visible="false">
                <div class="form-group">
                    <label>Please answer the secret question to update your password.</label><br />
                    <label runat="server" id="lblPasswordSecretQuestion">What is your name?</label>
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtSecretAnswer"></asp:TextBox>
                    <small id="smlForgotPasswordHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group text-center">
                    <asp:Button runat="server" CssClass="btn btn-danger m-3" Text="Verify" ID="btnFindPassword" OnClick="btnFindPassword_Click"/>
                </div>
                <div class="form-group" runat="server" id="divUpdatePassword" visible="false">
                    <label>Enter your new password</label><br />
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtNewPassword" TextMode="Password"></asp:TextBox>
                    <small id="smlNewPasswordHelp" runat="server" class="form-text text-danger"></small>
                    <div class="form-group text-center">
                        <asp:Button runat="server" CssClass="btn btn-danger m-3" Text="Update Password" ID="btnUpdatePassword" OnClick="btnUpdatePassword_Click" />
                    </div>
                    
                </div>
            </div>
            <div class="container mt-5" id="divRegister" runat="server" visible="false">
                <div class="form-group">
                    <label>Username</label>
                    <asp:TextBox runat="server" ID="txtRegUsername" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegUsernameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Password</label>
                    <asp:TextBox runat="server" ID="txtRegPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    <small id="smlRegPasswordHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>First Name</label>
                    <asp:TextBox runat="server" ID="txtRegFirstName" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegFirstNameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Last Name</label>
                    <asp:TextBox runat="server" ID="txtRegLastName" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegLastNameHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Email Address</label>
                    <asp:TextBox runat="server" ID="txtRegEmail" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegEmailHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Home Address</label>
                    <asp:TextBox runat="server" ID="txtRegHomeAddress" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegHomeAddressHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Billing Address</label>
                    <asp:TextBox runat="server" ID="txtRegBillingAddress" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegBillingAddressHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Phone Number</label>
                    <asp:TextBox runat="server" ID="txtRegPhone" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegPhoneHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Profile Picture</label>
                    <asp:TextBox runat="server" ID="txtRegImage" CssClass="form-control"></asp:TextBox>
                    <small id="smlRegImageHelp" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 1</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity1">
                        <asp:ListItem Value="0">What is your oldest sibling's name?</asp:ListItem>
                        <asp:ListItem Value="1">What is the name of the elementary school you want to?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtRegSecurity1"></asp:TextBox>
                    <small id="smlRegQuestion1Help" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 2</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity2">
                        <asp:ListItem Value="2">What was the name of your favorite pet?</asp:ListItem>
                        <asp:ListItem Value="3">What is your favorite color?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtRegSecurity2"></asp:TextBox>
                    <small id="smlRegQuestion2Help" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-group">
                    <label>Security Question 3</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSecurity3">
                        <asp:ListItem Value="4">What is the name of your first employer?</asp:ListItem>
                        <asp:ListItem Value="5">What is the name of your favorite movie?</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox runat="server" CssClass="form-control mt-3" ID="txtRegSecurity3"></asp:TextBox>
                    <small id="smlRegQuestion3Help" runat="server" class="form-text text-danger"></small>
                </div>
                <div class="form-check">
                    <input runat="server" type="checkbox" class="form-check-input" id="chkRegCookie"/>
                    <label class="form-check-label" for="chkCookie">Store cookie for auto login</label>
                </div>
                <div class="form-group text-center">
                    <asp:Button runat="server" CssClass="btn btn-danger m-3" Text="Submit Registration" ID="btnSubmitRegister" OnClick="btnSubmitRegister_Click"/>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
