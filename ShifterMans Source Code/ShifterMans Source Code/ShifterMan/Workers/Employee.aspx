<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="Workers_Employee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    Employee Page</h1>
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
                        <asp:MenuItem NavigateUrl="~/Workers/EmployeeInfo.aspx" Text="Change Info" Value="Change Info"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Scheduler/ShiftOptions.aspx" Text="Give Shift Options" Value="Give Shift Options"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Scheduler/ScheduleHistory.aspx" Text="Schedule History" Value="Schedule History"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </div>
        </div>
        <div class="main">
                        <div>
                  Your Company:
                  <asp:DropDownList ID="OrgNameList3" runat="server">
                  </asp:DropDownList>
            </div>
            <div>
                <h2>
                    Weekly Schedule</h2>
            </div>
            <div>
                <asp:GridView ID="WeeklyScheduleGrid" runat="server" AutoGenerateColumns="False" Width="929px">
                    <Columns>
                        <asp:BoundField DataField="HourDay" HeaderText="Hour/Day">
                        <ItemStyle BackColor="#FF66FF" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Sunday" HeaderText="Sunday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Monday" HeaderText="Monday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tusday" HeaderText="Tusday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Wednsday" HeaderText="Wednsday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Thursday" HeaderText="Thursday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Friday" HeaderText="Friday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Saturday" HeaderText="Saturday">
                        <HeaderStyle BackColor="#FF66CC" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
        
    </div>
    </form>
</body>
</html>
