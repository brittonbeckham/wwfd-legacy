<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Register" Codebehind="register.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>WWFD Contributor Registration</title>
    <style type="text/css">
        .style1
        {
            width: 79px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1 id="title" runat="server">Contributor Registration</h1>
    <h2 id="subtitle" runat="server">Please fill out the form below to register. <br />A confirmation email will be sent to you after registering successfully.</h2>
    <h3><asp:Label ID="lblMessage" RunAt="server" ForeColor="Red" /></h3>

    <center>
    <asp:Panel ID="RegistrationForm" runat="server">
        
    <table cellpadding="5">
        <tr align=left>
          <td class="style1" nowrap>First Name:</td>
          <td style="margin-left: 80px"><asp:TextBox ID="txtFirstName" RunAt="server" 
                  Width=175 />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align=left>
          <td class="style1" nowrap>Last Name:</td>
          <td style="margin-left: 80px"><asp:TextBox ID="txtLastName" RunAt="server" 
                  Width=175 />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align=left>
          <td class="style1">Email:</td>
          <td style="margin-left: 80px"><asp:TextBox ID="txtEmail" RunAt="server" Width=175 />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align=left>	
          <td class="style1">Password:</td>
          <td><asp:TextBox ID="txtPassword1" TextMode="password" RunAt="server"  Width=148px 
                  MaxLength="14" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtPassword1"></asp:RequiredFieldValidator>
            </td>
        </tr>    
        <tr align=left>	
          <td class="style1">Confirm:</td>
          <td><asp:TextBox ID="txtPassword2" TextMode="password" RunAt="server"  Width=148px 
                  MaxLength="14" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtPassword2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align=left>	
          <td class="style1">Message:<br />
              <span class="small">(tell us why you are interested in being a&nbsp; 
              contributor.)</span></td>
          <td><asp:TextBox ID="txtMessage" TextMode="MultiLine" RunAt="server"  Width=282px 
                  MaxLength="14" Height="72px" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                  CssClass="Validator" ErrorMessage="•" ControlToValidate="txtMessage"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align=leftz>
          <td colspan=2 align=center>
            <asp:Button ID="btnSubmit" Text="Submit" RunAt="server" Width=95 
                  onclick="btnSubmit_Click" />  
          </td>     
        </tr>
    </table>
    </asp:Panel>
    </center>

</asp:Content>

