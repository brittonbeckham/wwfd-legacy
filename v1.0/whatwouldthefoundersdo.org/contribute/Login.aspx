<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Wwfd.Web.Login" Codebehind="Login.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<title>WWFD Contributor Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h1>WWFD Contributor Login</h1>

<h3><asp:Label ID="Output" RunAt="server" ForeColor="Red" /></h3>

<center>
<table cellpadding="5">
    <tr>
      <td>Email:</td>
      <td>
        <asp:TextBox ID="txtEmail" RunAt="server" Width=175 ValidationGroup="Login" />
      </td>
    </tr>
    <tr>	
      <td>Password:</td>
      <td>
        <asp:TextBox ID="txtPassword" TextMode="password" RunAt="server"  Width=175 
              ValidationGroup="Login" />          
      </td>
    </tr>
    <tr>
      <td colspan=2 align=center>
        <asp:Button ID="btnLogin" Text="Log In" OnClick="btnLogin_Click" RunAt="server" 
              Width=95 ValidationGroup="Login" onload="btnLogin_Load" />  
        <asp:CheckBox Text="Keep me signed in" ID="Persistent" RunAt="server" Visible =false />        
      </td>     
    </tr>
</table>
</center>

</asp:Content>

