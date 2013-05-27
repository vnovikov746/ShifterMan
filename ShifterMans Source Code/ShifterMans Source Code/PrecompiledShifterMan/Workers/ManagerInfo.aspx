<%@ page title="Manager Information Page" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Workers_ManagerInfo, ShifterMan" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Manager Info
    </h2>
          <div>
                <table class="style1">
                    <tr>
                        <td class="style1">First Name:</td>
                        <td class="style1">
                            <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="FirstNameValidator" runat="server" ControlToValidate="FirstName" ErrorMessage="First Name Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Last name:</td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="LastNameValidator" runat="server" ControlToValidate="LastName" ErrorMessage="Last Name Required"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Email:</td>
                        <td>
                            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                ControlToValidate="Email" Display="Dynamic" ErrorMessage="Invalid Email" 
                                ValidationExpression="[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Verify Email:</td>
                        <td>
                            <asp:TextBox ID="VerifyEmail" runat="server"></asp:TextBox>
                            
                            <asp:CompareValidator ID="EmailCompareValidator" runat="server" 
                                ControlToCompare="Email" ControlToValidate="VerifyEmail" Display="Dynamic" 
                                ErrorMessage="Verify Email must be the same as Email field"></asp:CompareValidator>
                        </td>
                    </tr>
                     <tr>
                        <td>Password:</td>
                        <td>
                            <asp:TextBox ID="Password" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                ControlToValidate="Password" Display="Dynamic" ErrorMessage="Password must contain from 4 to 8 letters and at least one number" 
                                ValidationExpression="^(?=.*\d).{4,8}$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Password check:</td>
                        <td>
                            <asp:TextBox ID="PasswordVerify" runat="server"></asp:TextBox>
                            <asp:CompareValidator ID="PasswordCompareValidator" runat="server" 
                                ControlToCompare="Password" ControlToValidate="PasswordVerify" Display="Dynamic" 
                                ErrorMessage="Password check must be the same as Password"></asp:CompareValidator>
                        </td>
                    </tr>
        </table>
              <div>
                <asp:Button ID="ManagerInfoFinishButton" runat="server" Text="Continue" onclick="infoFinish_Click" />
                  <asp:Button ID="CancelEditingButton" runat="server" OnClick="CancelEditingButton_Click" Text="Cancel Editing" />
        </div>
    </div>
</asp:Content>
