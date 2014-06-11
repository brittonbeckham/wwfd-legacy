<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_AddFounder" Codebehind="add-founder.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Add Founder</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <h1>Add Founder</h1>
    
    <p>
    It is important that the Founder's name is entered in the same way that it is commonly known and
    referred to in history and in texts. To add a new Founder to the list, fill out the small form below. 
    First and last name are required, but please ensure that any middle name(s) or initial(s) are 
    included. Use the suffix field for names that have Jr., Sr., II, etc.
    </p>
    
    <center>
    <table>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="txtFirstName" runat="server" Width="147px" 
                    ValidationGroup="AddFounder"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtFirstName" ErrorMessage="•" CssClass="Validator" 
                    ValidationGroup="AddFounder"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Middle Name:</td>
            <td><asp:TextBox ID="txtMiddleName" runat="server" Width="147px" 
                    ValidationGroup="AddFounder"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Last Name:</td>
            <td><asp:TextBox ID="txtLastName" runat="server" Width="147px" 
                    ValidationGroup="AddFounder"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtLastName" ErrorMessage="•" CssClass="Validator" 
                    ValidationGroup="AddFounder"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Suffix:</td>
            <td style="text-align: left"><asp:TextBox ID="txtSuffix" runat="server" 
                    Width="50px" ValidationGroup="AddFounder"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align: left">&nbsp;</td>
        </tr>
        <tr>
            <td>Born:</td>
            <td style="text-align: left"><asp:TextBox ID="txtBirthDate" runat="server" 
                    ValidationGroup="AddFounder"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtBirthDate" ErrorMessage="•" CssClass="Validator" 
                    ValidationGroup="AddFounder"></asp:RequiredFieldValidator>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthDate" Animated="true" SelectedDate="1/1/1725" DefaultView="Years" />
            </td>
        </tr>
        <tr>
            <td>Died:</td>
            <td style="text-align: left"><asp:TextBox ID="txtDeathDate" runat="server" 
                    ValidationGroup="AddFounder"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtDeathDate" ErrorMessage="•" CssClass="Validator" 
                    ValidationGroup="AddFounder"></asp:RequiredFieldValidator>
            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDeathDate" Animated="true" SelectedDate="1/1/1790" DefaultView="Years" /></td>
        </tr>
    </table>

        
        
    </center>
    <br />
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
        Text="Cancel" Width="110px" CausesValidation="False" 
         UseSubmitBehavior="False" ValidationGroup="AddFounder" />&nbsp;
         <asp:Button ID="btnAddFounder" runat="server" onclick="btnAddFounder_Click" 
        Text="Add Founder" Width="110px" ValidationGroup="AddFounder" CausesValidation="True" />
        
        <script type="text/javascript">
            window.onload = document.getElementById('ctl00_ContentPlaceHolder1_txtFirstName').focus();   
        </script>

</asp:Content>

