<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">

        .style1
        {
            height: 26px;
        }
    </style>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="False" 
            NavigateUrl="~/Account/Register.aspx">Register</asp:HyperLink> if you don't have an account.
    </p>
            <table class="style1">
                <tr>
                    <td>
                <table class="style1">
                    <tr>
                        <td class="style1">Organization Name:<br />
                            <br />
                        </td>
                        <td class="style1">
                            <asp:DropDownList ID="OrgNameList" runat="server" DataSourceID="OrganizationName" DataTextField="Organization_Name" DataValueField="Organization_Name">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="OrganizationName" runat="server" ConnectionString="<%$ ConnectionStrings:ShifterManDB %>" SelectCommand="SELECT DISTINCT [Organization Name] AS Organization_Name FROM [Worker]"></asp:SqlDataSource>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>ID:<br />
                            <br />
                            <br />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtID1" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                ControlToValidate="TxtID1" Display="Dynamic" ErrorMessage="ID must be 9 numbers" 
                                ValidationExpression="([0-9]{9})"></asp:RegularExpressionValidator>
                            <br />
                            <asp:Literal ID="NoUserLiteral" runat="server" Text="No Such User In This Organization" Visible="False"></asp:Literal>
                            <br />
                            <br />
                        </td>
                    </tr>
        </table>
                        </td>
                    <td>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Pictures/shifterMan3.jpg" />
                        </td>
                    </tr>
        </table>
            <asp:Button ID="LoginButton" runat="server" Text="Log In" 
                             onclick="LoginButton_Click" />

</asp:Content>