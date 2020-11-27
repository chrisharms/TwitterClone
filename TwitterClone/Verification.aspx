<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Verification.aspx.cs" Inherits="TwitterClone.Verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Twitter - Verification</title>
    <link rel="stylesheet" href="CSS/Styles.css"/>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ho+j7jyWK8fNQe+A12Hb8AhRq26LrZ/JpcUGGOn+Y7RsweNrtN/tE3MoK7ZeZDyx" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
                <div class="jumbotron m-4">
                    <h1>Not Twitter</h1>
                    <p runat="server" id="pEmailVerify">You must verify your new account before gaining access to it. Please click the button below to verify that you are creating an account for the specified user.</p>
                    <p runat="server" id="pAppVerify">An email was sent to the email address provided for a verification step, please click the verification link in that email to start using your account.</p>
                    <p runat="server" id="pVerified">You have already verified your account, you can now login to access the application.</p>
                    <h5 runat="server" id="h5Username">Username: </h5>
                    <asp:Button runat="server" CssClass="btn btn-success mt-4" ID="btnVerifyUser" Text="Verify User" OnClick="btnVerifyUser_Click"/>
                    <asp:Button runat="server" CssClass="btn btn-info mt-4" ID="btnLogout" Text="Logout" OnClick="btnLogout_Click"/>
                    <asp:Button runat="server" CssClass="btn btn-info mt-4" ID="btnLogin" Text="Login" OnClick="btnLogin_Click"/>
                </div>
                
            </div>
    </form>
</body>
</html>
