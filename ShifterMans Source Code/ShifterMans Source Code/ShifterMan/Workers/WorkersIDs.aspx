<%@ Page Title="Workers IDs Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkersIDs.aspx.cs" Inherits="Workers_WorkersIDs" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            width: 168px;
        }
        .auto-style2 {
            width: 126px;
        }
        .auto-style4 {
            margin-left: 1px;
        }
        .auto-style6 {
            margin-left: 0px;
        }
        .auto-style8 {
            width: 246px;
        }
        .auto-style9 {
            width: 283px;
        }
        .auto-style10 {
            width: 245px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Workers
    </h2>
          <div>
              <div>
                  Your Company:
                  <asp:Label ID="orgNameLable" runat="server" Text="Label"></asp:Label>
                  <br />
                  <br />
                  Your Workers:<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="WorInfo" AllowSorting="True" Width="923px">
                      <Columns>
                          <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                          <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                          <asp:BoundField DataField="First_Name" HeaderText="First_Name" SortExpression="First_Name" />
                          <asp:BoundField DataField="Last_Name" HeaderText="Last_Name" SortExpression="Last_Name" />
                          <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                      </Columns>
                      <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                      <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                      <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                      <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                      <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                      <SortedAscendingCellStyle BackColor="#FFF1D4" />
                      <SortedAscendingHeaderStyle BackColor="#B95C30" />
                      <SortedDescendingCellStyle BackColor="#F1E5CE" />
                      <SortedDescendingHeaderStyle BackColor="#93451F" />
                  </asp:GridView>
                  <asp:SqlDataSource ID="WorInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ShifterManDB %>" SelectCommand="SELECT [Type], [ID], [First Name] AS First_Name, [Last Name] AS Last_Name, [Email] FROM [Worker] WHERE (([Organization Name] = @Organization_Name) AND ([Organization Name] = @Organization_Name2))">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="orgNameLable" Name="Organization_Name" PropertyName="Text" Type="String" />
                          <asp:ControlParameter ControlID="orgNameLable" Name="Organization_Name2" PropertyName="Text" Type="String" />
                      </SelectParameters>
                  </asp:SqlDataSource>
                  <br />
                  <br />
                  <table>
                      <tr>
                          <td class="auto-style10"><asp:Label ID="WorkerIDLabel" runat="server" Text="Worker ID"></asp:Label></td>
                          <td><asp:TextBox ID="WorkerIDTxt" runat="server" Width="275px" CssClass="auto-style4"></asp:TextBox></td>
                      </tr>
                  </table>
                   <table>
                      <tr>
                           <td class="auto-style8"><asp:Label ID="Label1" runat="server" Text="Worker Type"></asp:Label></td>
                           <td class="auto-style9"><asp:DropDownList ID="WorTypeList" runat="server" Width="283px" CssClass="auto-style6">
                                <asp:ListItem Selected="True">Employee</asp:ListItem>
                                <asp:ListItem>Manager</asp:ListItem>
                                </asp:DropDownList></td>
                          </tr>
                       </table>
                           <br/>
                           <br/>
                           <br/>
                       <table>
                           <tr>&nbsp;&nbsp;</tr>
                           <tr>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</tr>
                           <tr>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</tr>
                          <tr>
                            <td class="auto-style1"><asp:Button ID="AddWorkerButton" runat="server" OnClick="AddWorkerButton_Click" Text="Add Worker" Width="172px" /></td>
                              <td><asp:Button ID="RemoveWorButton" runat="server" OnClick="RemoveWorButton_Click" Text="Remove Worker" Width="197px" /></td>                             
                               <td class="auto-style2"><asp:Button ID="DoneButton" runat="server" OnClick="DoneButton_Click" Text="DONE" Width="157px" /></td>
                            </tr>
                       </table>
                  
        </div>
    </div>
</asp:Content>
