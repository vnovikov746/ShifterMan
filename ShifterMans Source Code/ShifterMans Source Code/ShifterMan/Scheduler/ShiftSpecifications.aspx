<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShiftSpecifications.aspx.cs" Inherits="Scheduler_ShiftSpecifications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 2px;
        }
        .auto-style2 {
            width: 116px;
        }
        .auto-style3 {
            width: 135px;
        }
        .auto-style4 {
            width: 151px;
        }
        .auto-style5 {
            width: 286px;
        }
        .auto-style6 {
            width: 922px;
        }
        .auto-style7 {
            width: 123px;
        }
        .auto-style8 {
            direction: ltr;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Shift Specifications
    </h2>
          <div>
              <h3>Edit Your Own Shift Table
                  </h3>
              <div>
                  Your Company:
                  <asp:Label ID="orgNameLabel" runat="server" Text="Label"></asp:Label>
              </div>
              <br/>
              <br/>
              <br/>
              <div>
                  <asp:GridView ID="ShiftSpcificationsTable" runat="server" AutoGenerateColumns="False" DataSourceID="ShiftSpecificationTable" Width="923px" AllowSorting="True" ShowHeaderWhenEmpty="True">
                      <Columns>
                          <asp:BoundField DataField="Day" HeaderText="Day" SortExpression="Day" />
                          <asp:BoundField DataField="Begin_Time" HeaderText="Begin Time" SortExpression="Begin_Time" />
                          <asp:BoundField DataField="End_Time" HeaderText="End Time" SortExpression="End_Time" />
                          <asp:BoundField DataField="Shift_Info" HeaderText="Number Of Workers" SortExpression="Shift_Info" />
                      </Columns>
                  </asp:GridView>
                  <asp:SqlDataSource ID="ShiftSpecificationTable" runat="server" ConnectionString="<%$ ConnectionStrings:ShifterManDB %>" SelectCommand="SELECT [Day], [Begin Time] AS Begin_Time, [End Time] AS End_Time, [Shift Info] AS Shift_Info FROM [Shift Schedule] WHERE ([Organization Name] = @Organization_Name)">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="orgNameLabel" Name="Organization_Name" PropertyName="Text" Type="String" />
                      </SelectParameters>
                  </asp:SqlDataSource>
              </div>

              <br/>
              <br/>
              <br/>
              <div>
                  <table class="auto-style6">
                      <tr>
                          <td class="auto-style2">

                              Day

                          </td>
                          <td class="auto-style3">

                              Begining Hour

                          </td>
                          <td class="auto-style4">

                              End Hour

                          </td>
                          <td class="auto-style7">

                              Number Of Workers</td>
                      </tr>

                      <tr>
                          <td class="auto-style2">
                              <asp:Button ID="SelectAllButton" runat="server" OnClick="SelectAllButton_Click" Text="Select All" />
                              <asp:CheckBoxList ID="DayList" runat="server" BorderColor="Black" BorderStyle="None" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="True" Width="140px" CellPadding="3" ToolTip="Select Days">
                                  <asp:ListItem>Sunday</asp:ListItem>
                                  <asp:ListItem>Monday</asp:ListItem>
                                  <asp:ListItem>Tusday</asp:ListItem>
                                  <asp:ListItem>Wednsday</asp:ListItem>
                                  <asp:ListItem>Thursday</asp:ListItem>
                                  <asp:ListItem>Friday</asp:ListItem>
                                  <asp:ListItem>Saturday</asp:ListItem>
                              </asp:CheckBoxList>
                          </td>
                          <td class="auto-style3">
                                  <table class="style1">
                                      <tr>
                                          <td>
                                                <asp:DropDownList ID="BeginHourDropDown" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem Selected="True">07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                          </td>
                                          <td>
                                                :
                                          </td>
                                          <td>
                                                <asp:DropDownList ID="BeginMinDropDown" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>
                                          </td>
                                      </tr>
                                      </table>

                          </td>

                          <td class="auto-style4">
                                  <table class="style1">
                                      <tr>
                                          <td>
                                                <asp:DropDownList ID="EndHourDropDown" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>01</asp:ListItem>
                                                <asp:ListItem>02</asp:ListItem>
                                                <asp:ListItem>03</asp:ListItem>
                                                <asp:ListItem>04</asp:ListItem>
                                                <asp:ListItem>05</asp:ListItem>
                                                <asp:ListItem>06</asp:ListItem>
                                                <asp:ListItem>07</asp:ListItem>
                                                <asp:ListItem>08</asp:ListItem>
                                                <asp:ListItem>09</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem Selected="True">15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                </asp:DropDownList>
                                          </td>
                                          <td>
                                                :
                                          </td>
                                          <td>
                                                <asp:DropDownList ID="EndMinDropDown" runat="server">
                                                <asp:ListItem>00</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                </asp:DropDownList>
                                          </td>
                                      </tr>
                                      </table>
                          </td>
                          
                          <td class="auto-style7">

                              <asp:DropDownList ID="NumOfWorList" runat="server" Width="73px">
                                  <asp:ListItem>1</asp:ListItem>
                                  <asp:ListItem>2</asp:ListItem>
                                  <asp:ListItem>3</asp:ListItem>
                                  <asp:ListItem>4</asp:ListItem>
                                  <asp:ListItem>5</asp:ListItem>
                                  <asp:ListItem>6</asp:ListItem>
                                  <asp:ListItem>7</asp:ListItem>
                                  <asp:ListItem>8</asp:ListItem>
                                  <asp:ListItem>9</asp:ListItem>
                                  <asp:ListItem>10</asp:ListItem>
                              </asp:DropDownList>

                          </td>
                      </tr>
                  </table>
              <br/>
              <br/>
              <br/>
                  <table class="auto-style6">
                      <tr>
                          <td class="auto-style5">                              
                              <asp:Button ID="AddShiftButton" runat="server" Text="Add Shift" Width="442px" OnClick="AddShiftButton_Click" />

                          </td>
                          <td class="auto-style8">

                              <asp:Button ID="RemoveShiftButton" runat="server" Text="Remove Shift" CssClass="auto-style1" Width="471px" OnClick="RemoveShiftButton_Click" />

                          </td>
                      </tr>
                </table>
              </div>
          </div>
</asp:Content>

