<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    Manager Registration Page</h1>
            </div>
            <div class="loginDisplay">
                <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                    <AnonymousTemplate>
                        [ <a href="~/Account/Login.aspx" ID="HeadLoginStatus" runat="server">Log In</a> ]
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        Welcome <span class="bold"><asp:LoginName ID="HeadLoginName" runat="server" /></span>!
                        [ <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out" LogoutPageUrl="~/"/> ]
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
            <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="About"/>
                    </Items>
                </asp:Menu>
            </div>
        </div>
        <div class="main">
              <div>
                <table class="style1">
                    <tr>
                        <td class="style1">Organization Name:</td>
                        <td class="style1">
                            <asp:TextBox ID="TxtOrganizationName" runat="server"></asp:TextBox>
                            <asp:CustomValidator ID="OrganizationValidator" runat="server" 
                                ControlToValidate="TxtOrganizationName"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>ID:</td>
                        <td>
                            <asp:TextBox ID="TxtID" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="TxtID" Display="Dynamic" ErrorMessage="ID must be 9 numbers" 
                                ValidationExpression="([0-9]{9})"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Verify ID:</td>
                        <td>
                            <asp:TextBox ID="TxtVerifyID" runat="server"></asp:TextBox>
                            
                            <asp:CompareValidator ID="IDCompareValidator" runat="server" 
                                ControlToCompare="TxtID" ControlToValidate="TxtVerifyID" Display="Dynamic" 
                                ErrorMessage="Verify ID must be the same as ID field"></asp:CompareValidator>
                            
                        </td>
                    </tr>
        </table>
    </div>
    <asp:Button ID="SignUpButton" runat="server" Text="Sign Up" 
                             onclick="SignUp_Click" />
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
        
    </div>
    </form>
</body>
</html>
