<%@ Page Title="Workers IDs Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="WorkersIDs.aspx.cs" Inherits="Workers_WorkersIDs" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Workers
    </h2>
          <div>
              <div>
                  Your Company:&nbsp;
                  <asp:DropDownList ID="OrgNameList" runat="server">
                  </asp:DropDownList>
                  <br />
                  <br />
                  Your Workers:<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="WorInfo" AllowSorting="True">
                      <Columns>
                          <asp:BoundField DataField="Wor_ID" HeaderText="Wor_ID" SortExpression="Wor_ID" />
                          <asp:BoundField DataField="Wor_FirstName" HeaderText="Wor_FirstName" SortExpression="Wor_FirstName" />
                          <asp:BoundField DataField="Wor_LastName" HeaderText="Wor_LastName" SortExpression="Wor_LastName" />
                          <asp:BoundField DataField="Wor_Email" HeaderText="Wor_Email" SortExpression="Wor_Email" />
                          <asp:BoundField DataField="Wor_Type" HeaderText="Wor_Type" SortExpression="Wor_Type" />
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
                  <asp:SqlDataSource ID="WorInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ShifterManDB %>" SelectCommand="SELECT [Wor_ID], [Wor_FirstName], [Wor_LastName], [Wor_Email], [Wor_Type] FROM [Worker] WHERE ([Org_Name] = @Org_Name)">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="OrgNameList" Name="Org_Name" PropertyName="SelectedValue" Type="String" />
                      </SelectParameters>
                  </asp:SqlDataSource>
                  <br />
                  <br />
                  <br />
                  <asp:Label ID="WorkerIDLabel" runat="server" Text="Worker ID"></asp:Label>
                  <asp:TextBox ID="WorkerIDTxt" runat="server"></asp:TextBox>
                  <br />
                  Worker Type <asp:DropDownList ID="WorTypeList" runat="server">
                      <asp:ListItem Selected="True">Employee</asp:ListItem>
                      <asp:ListItem>Manager</asp:ListItem>
                  </asp:DropDownList>
                  <br />
                  <asp:Button ID="AddWorkerButton" runat="server" OnClick="AddWorkerButton_Click" Text="Add Worker" />
                  <asp:Button ID="DoneButton" runat="server" OnClick="DoneButton_Click" Text="DONE" />
        </div>
    </div>
</asp:Content>
