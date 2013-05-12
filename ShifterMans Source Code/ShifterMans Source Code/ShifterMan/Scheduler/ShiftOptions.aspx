<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShiftOptions.aspx.cs" Inherits="Scheduler_ShiftOptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 360px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <div>
       Your Company:
       <asp:DropDownList ID="OrgNameList3" runat="server">
       </asp:DropDownList>
   </div>
                <div>
                <h2>
                    Your Shift Options</h2>
            </div>
            <div>
                <asp:GridView ID="ShiftOptionsGrid" runat="server" AutoGenerateColumns="False" Width="929px" OnRowCommand="ShiftOptionsGrid_RowCommand" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:BoundField DataField="HourDay" HeaderText="Hour/Day">
                        <ItemStyle BackColor="#FF66FF"/>
                        </asp:BoundField>
                        <asp:ButtonField HeaderText="Sunday" ButtonType="Button" CommandName="Sunday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Monday" ButtonType="Button" CommandName="Monday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Tusday" ButtonType="Button" CommandName="Tusday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Wednsday" ButtonType="Button" CommandName="Wednsday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Thursday" ButtonType="Button" CommandName="Thursday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Friday" ButtonType="Button" CommandName="Friday">
                        <ControlStyle Height="100px" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Saturday" ButtonType="Button" CommandName="Saturday">
                        <ControlStyle Height="100" Width="120px"/>
                        <HeaderStyle BackColor="#FF66CC" />
                        <ItemStyle BackColor="White" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
    <div class="auto-style1">

        <br />
        <br />
        <br />
        <asp:Button ID="SubmitShiftsButton" runat="server" OnClick="SubmitShiftsButton_Click" Text="Submit Shifts" />

        </div>
</asp:Content>

