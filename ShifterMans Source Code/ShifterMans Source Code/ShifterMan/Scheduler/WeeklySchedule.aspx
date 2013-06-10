<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeeklySchedule.aspx.cs" Inherits="Scheduler_WeeklySchedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }
        .auto-style2 {
            margin-left: 0px;
            width: 329px;
        }
        .auto-style3 {
            width: 247px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    Create Weekly Schedule Page</h1>
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
                        <asp:MenuItem NavigateUrl="~/Workers/ManagerInfo.aspx" Text="Change Information" Value="Change Information"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Workers/WorkersIDs.aspx" Text="Add Workers" Value="Add Workers"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Scheduler/ShiftSpecifications.aspx" Text="Shift Specifications" Value="Shift Specifications"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Scheduler/WeeklySchedule.aspx" Text="Create Weekly Schedule" Value="Create Weekly Schedule"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/Scheduler/ScheduleHistory.aspx" Text="Schedule History" Value="Schedule History"></asp:MenuItem>
                    </Items>
                </asp:Menu>
            </div>
        </div>
        <div class="main">
            <div>
                <h2>
                    <asp:Label ID="weeklyLabel" runat="server" Text="WEEKLY SCHEDULE FOR "></asp:Label>
                    <asp:Label ID="orgNameLabel" runat="server" Text="OrgName"></asp:Label>
                </h2>
            </div>
            <div>
                <asp:GridView ID="WeeklyScheduleGrid" runat="server" AutoGenerateColumns="False" Width="933px" ShowHeaderWhenEmpty="True" CellPadding="10" CssClass="auto-style1" HorizontalAlign="Left">
                    <AlternatingRowStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField DataField="HourDay" HeaderText="Hour/Day">
                        <ControlStyle Height="100%" Width="100%" />
                        <ItemStyle BackColor="#FF66FF" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sunday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Monday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tusday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Wednsday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thursday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Friday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Saturday">
                            <ControlStyle Height="100%" Width="100%" />
                            <HeaderStyle BackColor="#FF66CC" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle HorizontalAlign="Justify" VerticalAlign="Top" />
                </asp:GridView>
            </div>
            <div class="clear">
                <br/>
                <br/>
                <table class="auto-style2" align="center">
                    <tr>
                        <td class="auto-style3">
                            <asp:Button ID="GenerateScheduleButton" runat="server" Text="Generate" CssClass="auto-style1" Font-Bold="True" Font-Italic="True" Font-Size="Large" Height="42px" Width="144px" OnClick="GenerateScheduleButton_Click" />
                        </td>
                        <td class="style1">
                            <asp:Button ID="SubmitScheduleButton" runat="server" Text="Submit" Font-Bold="True" Font-Italic="True" Font-Size="Large" Height="41px" Width="130px" OnClick="SubmitScheduleButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>Workers That Gave Options:</td>
                        <td>
                            <asp:DropDownList ID="goodWorkerDropDown" runat="server">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="GaveOptionsWorkers" runat="server" ConnectionString="<%$ ConnectionStrings:ShifterManDB %>" SelectCommand="SELECT DISTINCT [Worker ID] AS Worker_ID FROM [Shift Options] WHERE ([Organization Name] = @Organization_Name)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="orgNameLabel" Name="Organization_Name" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="footer">
        
    </div>
    </form>
</body>
</html>
