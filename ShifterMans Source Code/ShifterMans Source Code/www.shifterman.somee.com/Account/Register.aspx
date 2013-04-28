<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">

        .style1
        {
            height: 26px;
        }
        .auto-style1 {
            padding: 0px 12px;
            margin: 12px 8px 8px 8px;
            min-height: 420px;
            width: 890px;
            background-color: #F7F7F7;
        }
    </style>
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Manager Registration
    </h2>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Pictures/shifterMan4.jpg" Height="221px" Width="243px" />
        <div class="auto-style1">
              <div>
                <table class="style1">
                    <tr>
                        <td class="style1">Organization Name:</td>
                        <td class="style1">
                            <asp:TextBox ID="TxtOrganizationName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="OrganizationNameValidator" runat="server" ControlToValidate="TxtOrganizationName" ErrorMessage="Organization Name Required"></asp:RequiredFieldValidator>
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
    <div class="footer">
        
    </div>
</asp:Content>