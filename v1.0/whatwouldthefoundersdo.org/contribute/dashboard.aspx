<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" Inherits="_Dashboard" Codebehind="dashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Contributor Dashboard</h1>
    
    <p>Database size: <asp:Label ID="lblTotalQuotes" runat="server" Text="" CssClass="bold"></asp:Label> quotes, <asp:Label ID="lblTotalFounders" runat="server" Text="" CssClass="bold"></asp:Label> Founders.</p>
    <p>You have contributed <asp:Label ID="lblQuotesContributed" runat="server" Text="" CssClass="bold"></asp:Label> quotes.</p>
    <p>Your contribution ratio is <asp:Label ID="lblContributionRatio" runat="server" Text="" CssClass="bold"></asp:Label>%.</p>
    
    <hr />
    <h3>Leading Contributors:</h3>
    <dd>
    <asp:GridView ID="GridView1" runat="server"
        AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" 
        BorderColor="Tan" BorderWidth="1px" CellPadding="2" 
        EnableModelValidation="True" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>         
            <asp:BoundField DataField="Contributor" HeaderText="Name" 
                HeaderStyle-Width="250" >
<HeaderStyle Width="250px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="QuotesEntered" HeaderText="Quotes" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    </asp:GridView></dd>
    

    <h3>Tasks:</h3>
    <ul>
        <li><a href="add-quote.aspx">Add new quote.</a></li>
    </ul>

    <h3>My Recently Added Quotes:</h3>
    <dd><asp:GridView ID="GridView2" runat="server"
        AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" 
        BorderColor="Tan" BorderWidth="1px" CellPadding="2" 
        EnableModelValidation="True" ForeColor="Black" GridLines="None" >
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>         
            <asp:BoundField DataField="FullName" HeaderText="Founder" />
            <asp:BoundField DataField="QuoteText" HeaderText="Quote Text" DataFormatString="{0}" />
            <asp:BoundField DataField="DateAdded" DataFormatString="{0:d}" 
                HeaderText="Date " />
            <asp:HyperLinkField DataNavigateUrlFields="QuoteID" 
                DataNavigateUrlFormatString="~/browse.aspx?q={0}&edit=true&RedirectUrl=/contribute/dashboard.aspx" HeaderText="Edit" Text="Edit" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
            HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
    </asp:GridView></dd>



</asp:Content>


