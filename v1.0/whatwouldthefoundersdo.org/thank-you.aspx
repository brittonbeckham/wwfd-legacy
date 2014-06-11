<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ThankYou" Codebehind="thank-you.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Panel runat="server" ID="MessageRegComplete" Visible="false">
    <h1>Registration Complete</h1>
    <h2>You have successfully confirmed your registration to become a contributor 
    for whatwouldthefoundersdo.org. You may now contribute. To start, visit the 
    login page <a href="~/contribute/login.aspx" runat="server">here</a>.</h2>
    </asp:Panel>

    <asp:Panel runat="server" ID="MessageRegSubmitted" Visible="false">
    <h1>Registration Submitted</h1>
    <h2>You have successfully submitted your registration request to become a contributor for 
    whatwouldthefoundersdo.org. Before you can begin contributing to the cause, you will need to confirm your registration. 
    You should receive an email shortly with instructions on how to finalize your registration.</h2>
    </asp:Panel>

    <asp:Panel runat="server" ID="Panel2" Visible="false">
    <h1></h1>
    <h2></h2>
    </asp:Panel>

    
</asp:Content>

