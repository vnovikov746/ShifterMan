<%@ Page Title="Manager Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Manager.aspx.cs" Inherits="Workers_Manager" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Manager Page
    </h2>
    <br /> 
    
    <div style="margin-left: 20px;">
        <asp:Label ID="LabelRows" Text="Rows (Shifts):" style="text-align: right;" Width="80px" runat="server"></asp:Label> <asp:TextBox style="margin-left: 5px" ID="txtRows" runat="server" Width="40px"> </asp:TextBox> <br />
        <asp:Label ID="LableCols" Text="Cols (Days):" style="text-align: right;" Width="80px" runat="server"></asp:Label> <asp:TextBox style="margin-left: 5px" ID="txtCols" runat="server" Width="40px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnGenerate" OnClick="btnGenerate_Click" runat="server" Text="Generate" />&nbsp;<br /> <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <br />
        <br />
    </div>
    <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" Text="Cause Postback" />
 
</asp:Content>
