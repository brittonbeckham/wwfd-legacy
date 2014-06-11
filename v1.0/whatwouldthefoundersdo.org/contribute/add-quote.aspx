<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="_AddQuote" Codebehind="add-quote.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Add Quote</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">
        function updateCharCount(objField, maxChars, lblName) {
            if (!objField)
                return;

            objLbl = document.getElementById(lblName);
            if (!objLbl)
                return;

            objLbl.innerHTML = "[" + (maxChars - objField.value.length) + "]";
        }
    </script>

    <h1>Contribute a Founder's Advice</h1>
    
    <asp:Label ID="lblError" runat="server" BackColor="#FFFFCC" CssClass="Error" 
        Font-Bold="False" Font-Overline="False" ForeColor="Red" Text="Label" 
        Visible="False"></asp:Label>
    
    <br />
    <b>Select Founder:</b>
    <asp:DropDownList ID="ddFounders" runat="server" ValidationGroup="AddQuote"></asp:DropDownList>
    <asp:RequiredFieldValidator 
        ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddFounders" 
        ErrorMessage="•" Font-Bold="True" 
        ValidationGroup="AddQuote"></asp:RequiredFieldValidator>
    (<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="add-founder.aspx">add new Founder</asp:HyperLink>)
    <br />
    <br />
    <b>Enter/Paste Quote </b>(don&#39;t use quotation marks at beginning and end): <label id="quoteCharCount" class="charcount"></label>
    <asp:RequiredFieldValidator 
        ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuote" 
        Display="Dynamic" ErrorMessage="<br>This field is required!" 
        Font-Bold="True" ValidationGroup="AddQuote"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtQuote" runat="server" Height="112px" Width="488px" 
        TextMode="MultiLine" MaxLength="1850" 
        onkeydown="updateCharCount(this, 1850, 'quoteCharCount');" 
        ValidationGroup="AddQuote"></asp:TextBox>
    <br />
    <br />
    <b>Reference Information</b> (required): <label id="referenceCharCount" class="charcount"></label> <asp:RequiredFieldValidator 
        ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtReferenceInfo" Display="Dynamic" 
        ErrorMessage="This field is required!" Font-Bold="True" 
        ValidationGroup="AddQuote"></asp:RequiredFieldValidator>
    <br />
    <asp:TextBox ID="txtReferenceInfo" runat="server" Height="43px" 
    Width="488px" MaxLength="255" TextMode="MultiLine" 
        onkeydown="updateCharCount(this, 255, 'referenceCharCount');" 
        ValidationGroup="AddQuote"></asp:TextBox>
    <br />
    <br />
    <b>Key words</b> (comma seperated): <label id="keyWordsCharCount" class="charcount"></label><br />
&nbsp;<asp:TextBox ID="txtKeyWords" runat="server" Width="488px" MaxLength="255"  
        onkeydown="updateCharCount(this, 255, 'keyWordsCharCount');" 
        ValidationGroup="AddQuote"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="txtKeyWords" ErrorMessage="•"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="btnSumit" runat="server" onclick="btnSumit_Click" 
        Text="Add Quote" Width="128px" ValidationGroup="AddQuote" />
    <br />
</asp:Content>

