<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Search" Codebehind="search.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
        <!-- Founder information -->
        <asp:Panel ID="PanelFounder" runat="server" CssClass="FounderSummary" Visible="false">
            <center>
            <table>
                <tr>
                    <td><asp:Image ID="imgFounder" runat="server" CssClass="FounderThumbnail" ImageAlign="AbsMiddle" BorderWidth=1/></td>
                    <td valign="top">
                        <h1 id="lblFounderName" class="FounderName" runat="server" ></h1>
                        <h5 id="lblFounderLifeSpan" class="FounderLifeSpan" runat="server"></h5>
                        <!--<h2 class="FounderTitle">American Founder</h2>-->
                    </td>
                </tr>
            </table>
            </center>           
        </asp:Panel>

        <!-- summary of the results for this founder -->
        <asp:Panel ID="PanelFounderQuotesSummary" runat="server" CssClass="FounderQuotesSummary" Visible="false">
            <asp:Label ID="SummaryText" runat="server" Text=""></asp:Label>
        </asp:Panel>

        <!-- summary panel for custom searches -->        
        <asp:Panel ID="PanelSearchSummary" runat="server" CssClass="SearchResultsSummary">
                <asp:Label ID="lblSummaryText" runat="server">Search result text.</asp:Label>
                <asp:HyperLink ID="lnkSort" runat="server" CssClass="SearchSortLink" Visible="false">(Sort by Founder)</asp:HyperLink>
        </asp:Panel>

        <asp:Panel ID="PanelQuotes" runat="server"></asp:Panel>

        <!-- paging links -->
        <asp:Panel ID="PanelPagingBottom" runat="server" CssClass="PagingPanel"></asp:Panel>

    
</asp:Content>

