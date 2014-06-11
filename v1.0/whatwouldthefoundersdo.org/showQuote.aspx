<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ShowQuote" Codebehind="showQuote.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <!-- Founder information -->
    <asp:Panel ID="PanelFounder" runat="server" CssClass="FounderSummary">
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
        
    <!-- holds all the quotes for the page -->
    <asp:Panel ID="PanelQuote" runat="server"></asp:Panel>
    <asp:Panel ID="PanelAdmin" runat="server" Visible="false">
        <asp:HiddenField ID="QuoteId" Value="" runat="server" />
        <asp:DropDownList ID="ddFounders" runat="server"></asp:DropDownList>
        <asp:TextBox TextMode="MultiLine" ID="QuoteText" runat="server"></asp:TextBox>
        <asp:Button Text="Save Changes" runat="server" ID="btnSaveChanges" onclick="btnSaveChanges_Click" Width="105px"/>
    </asp:Panel>
        
    <asp:Panel ID="PanelQuoteActions" runat="server" class="ActionPanel" Visible="false">
        <img src="/images/flag.png" style="width: 18px;" align="absmiddle" /><a href="javascript:setFlag();">Flag as 'Duplicate'</a>
        <img src="/images/flag.png" style="width: 18px;" align="absmiddle" /><a href="">Flag as 'Source Invalid'</a>
        <img src="/images/flag.png" style="width: 18px;" align="absmiddle" /><a href="">Flag as 'Typo Found'</a>
        <input type="hidden" name="flag" value="" />
        
        <div class="ActionSubPanel">
            Notes: <asp:TextBox ID="txtFlagNotex" runat="server" TextMode="MultiLine" Width="350" Rows="1"></asp:TextBox><br />
            <asp:Button ID="btnSetFlag" runat="server" Text="Set Flag" />
        </div>
    </asp:Panel>


    <asp:Panel ID="GlobalShareToolbar" runat="server" class="sharing_panel">

        <iframe src="http://www.facebook.com/plugins/like.php?href=http://www.whatwouldthefoundersdo.org/showQuote.aspx?q=<%=Request.QueryString["q"]%>&amp;layout=standard&amp;show_faces=false&amp;width=330&amp;action=like&amp;font=tahoma&amp;colorscheme=light&amp;height=35" scrolling="no" frameborder="0" allowTransparency="true" class="facebook_like_plugin"></iframe>

        <!-- sharing toolbar -->
        <div class="addthis_toolbox addthis_default_style" style="display: inline-block; margin-top: 4px; vertical-align: top; text-align: left;">
            <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=wwfd" class="addthis_button_compact">Share</a>
            <span class="addthis_separator">|</span>
            <a class="addthis_button_facebook"></a>
            <a class="addthis_button_email"></a>
            <a class="addthis_button_favorites"></a>
            <a class="addthis_button_print"></a>
        </div>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=wwfd"></script>
             
    </asp:Panel>
    
</asp:Content>

