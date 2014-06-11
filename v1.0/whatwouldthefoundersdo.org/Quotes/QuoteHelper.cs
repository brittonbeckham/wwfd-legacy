using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wwfd.Data;
using Wwfd.Web.Authentication;
using Wwfd.Data.CoreDataObjectsTableAdapters;

namespace Wwfd.Web.Quotes
{
	public class QuoteHelper
	{
		public static void LoadQuote(WebControl control, string founder, string quoteText, int quoteId, string referenceText, string keywordsText, bool addLine)
		{
			LoadQuote(control, founder, quoteText, quoteId, referenceText, keywordsText, new string[0], addLine);
		}

		public static void LoadQuote(WebControl control, string founder, string quoteText, int quoteId, string referenceText, string keywordsText, string[] highlightWords, bool addLine)
		{
			Panel p = new Panel();
			p.CssClass = "QuoteBox";

			if (founder != string.Empty)
			{
				Label author = new Label();
				author.CssClass = "Author";
				author.Text = founder.AddInlineSpan(highlightWords) + ":";
				p.Controls.Add(author);
			}

			if (quoteId > 0)
			{
				HyperLink quote = new HyperLink();
				quote.CssClass = "Quote";
				quote.ToolTip = "View this quote alone (optimized for sharing on social networks).";
				quote.Text = "\"" + quoteText.AddInlineSpan(highlightWords) + "\"";
				quote.NavigateUrl = "~/showQuote.aspx?q=" + quoteId;
				p.Controls.Add(quote);

			}
			else
			{
				ContributorRoles role;

				//check for super admin
				if (Authenticator.IsUserLoggedIn())
					role = Authenticator.GetUser().Role;
				else
					role = ContributorRoles.None;

				//choose the role
				switch (role)
				{
					//case ContributorRoles.Admin:
					//    //show the quote in a text box
					//    TextBox t = new TextBox();                        
					//    t.ID = "txtQuoteText";                        
					//    t.Text = quoteText;
					//    t.TextMode = TextBoxMode.MultiLine;
					//    t.Width = 580;
					//    t.Rows = (quoteText.Length / 118) + 1;
					//    t.CssClass = "transparent";
					//    t.ToolTip = "Click to modify.";
					//    t.Attributes.Add("onclick", "this.className = 'selected';");
					//    t.Attributes.Add("onblur", "this.className = 'transparent';");
					//    p.Controls.Add(t);
					//    break;

					default:
						Label quote = new Label();
						quote.CssClass = "QuoteNoLink";
						quote.Text = "\"" + quoteText.AddInlineSpan(highlightWords) + "\"";
						p.Controls.Add(quote);
						break;
				}
			}

			Label lblReference = new Label();
			lblReference.CssClass = "Reference";
			lblReference.Text = "source: " + referenceText;
			p.Controls.Add(lblReference);

			string[] keywords = keywordsText.Split(',');
			foreach (string phrase in keywords)
			{
				HyperLink lblKeyword = new HyperLink();
				lblKeyword.CssClass = "Keywords";
				lblKeyword.ToolTip = string.Format("Search quotes related to '{0}'", phrase.Trim());
				lblKeyword.Text = phrase.Trim().AddInlineSpan(highlightWords);
				lblKeyword.NavigateUrl = "~/search.aspx?searchType=Quotes&searchText=" + phrase.Trim().Replace(' ', '+');
				p.Controls.Add(lblKeyword);

				if (phrase != keywords[keywords.Length - 1])
					p.Controls.Add(new LiteralControl(", "));
			}

			//if(addLine)
			//    p.Controls.Add(new LiteralControl("<hr/>"));

			control.Controls.Add(p);
		}

	}
}