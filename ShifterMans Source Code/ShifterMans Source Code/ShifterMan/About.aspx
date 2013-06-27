<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        About
    </h2>
    <p>
        <table>
            <tr>
                <td> <asp:Image ID="Image1" runat="server" ImageUrl="~/Pictures/shifterMan5.jpg" /></td>
                <td><h4>Vladimir Novikov: Scrum master and supervisor of research and development.</h4><br /><br />

                     <h4>Daniel Shwarcman: WEB Master and Design.</h4><br /><br />

                     <h4>Stanislav Kuzmin: Data Base Manager.</h4><br /><br />

                     <h4>Koby Vurgaft: Chief Developer.</h4><br /><br />
                     
                     <h1>Product Support : kakifish@hotmail.com </h1></td>
                     <h1>Wiki:<a href="https://github.com/kakifish/ShifterMan/wiki"></h1></a></td>
            </tr>
        </table>
    </p>
</asp:Content>
