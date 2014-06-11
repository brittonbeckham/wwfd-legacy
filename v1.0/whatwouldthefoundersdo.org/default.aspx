<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_Default" Codebehind="default.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>What Would The Founders Do? search quotes from the American Founding Fathers, search from the founding documents, U.S. Consitution, Declaration of Independence, Bill of Rights</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h1>Welcome to WhatWouldTheFoundersDo.org</h1>

    Are you a concerned American citizen? Have you ever thought or wondered what the American Founders would think
    of America today? Do you question whether the path that our congresses and Presdients from the last 100 years is
    right for what America stands for? This website is designed to give unbiased, uninterpreted information straight
    from the mouth of the American Founders. Search for any topic that troubles our society today and see what the 
    Founder's thoughts were. You may be surprised at how much foresight these men and women actually had.

    <br /><br />
    <center><h2>Today's Quote of the Day for <%=DateTime.Today.ToString("MMMM dd, yyyy") %>:</h2></center>
    <asp:Panel ID="PanelQOTD" runat="server"></asp:Panel>
    
    <asp:Panel ID="Panel1" runat="server" class="sharing_panel">

        <iframe id="FrameFacebookLike" scrolling="no" frameborder="0" allowTransparency="true" class="facebook_like_plugin" runat="server"></iframe>

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

