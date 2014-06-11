using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Web;

public partial class _Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			QuotesViewTableAdapter quotesAdpt = new QuotesViewTableAdapter();
			CoreDataObjects.QuotesViewRow r = (CoreDataObjects.QuotesViewRow)quotesAdpt.GetQuoteOfTheDay().Rows[0];

			string likeUrl = string.Format("http://www.whatwouldthefoundersdo.org/showQuote.aspx?q={0}", r.QuoteID);

			//setup page meta data title
			Page.Title = string.Format("{0} Quote #{1}", r.FullName, r.QuoteID);
			Page.AddMetaInfo(OpenGraphMetaTags.Title, string.Format("{0} Quote #{1}", r.FullName, r.QuoteID));
			Page.AddMetaInfo(OpenGraphMetaTags.Type, "article");
			Page.AddMetaInfo(OpenGraphMetaTags.Site_Name, "WhatWouldTheFoundersDo.org");
			Page.AddMetaInfo(OpenGraphMetaTags.URL, likeUrl);
			Page.AddMetaInfo(OpenGraphMetaTags.Description, string.Format("{0}", r.QuoteText));

			//load the quote
			Wwfd.Web.Quotes.QuoteHelper.LoadQuote(PanelQOTD, r.FullName, r.QuoteText, r.QuoteID, r.ReferenceInfo, r.Keywords, false);

			//adjust the like page to link to the actual quote of the day url
			FrameFacebookLike.Attributes.Add("src", string.Format(@"http://www.facebook.com/plugins/like.php?href={0}&layout=standard&show_faces=false&width=330&action=like&font=tahoma&colorscheme=light&height=28", likeUrl));
		}
		catch 
		{ }
		
	}
}
