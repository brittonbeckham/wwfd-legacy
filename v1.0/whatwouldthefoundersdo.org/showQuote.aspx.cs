using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wwfd;
using Wwfd.Web.Authentication;
using Wwfd.Web.Quotes;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Web;
using BLB.Common.Extensions;
using Wwfd.Data;

public partial class ShowQuote : System.Web.UI.Page
{
	QuotesViewTableAdapter quotesAdpt = new QuotesViewTableAdapter();
	int _quoteId;
	bool _qotd;
	 
	protected void Page_Load(object sender, EventArgs e)
	{
		//do not execute on postback
		if (IsPostBack)
			return;
		
		//verify page variables
		PageVariablesCollection vars = new PageVariablesCollection();
		vars.Add(new RequestVariable("q", typeof(int), true));

		Page.ValidateVariables(vars);
		
		//check for request of a single quote
		try
		{
			_quoteId = (int)vars["q"].Value;
			if (_quoteId == 0)
				_qotd = true;

			LoadSingleQuote(_quoteId);
			
			//show the action bar if the user viewing is a logged in admin
			if (Authenticator.IsUserLoggedIn())
				if (Authenticator.GetUser().Role == ContributorRoles.Admin)
					PanelQuoteActions.Visible = true;
		}
		catch
		{
			GetRandomQuote();
		}
	}


	private void GetRandomQuote()
	{
		//load a random quote
		CoreDataObjects.QuotesViewRow r = (CoreDataObjects.QuotesViewRow)quotesAdpt.GetRandomQuote().Rows[0];
		Response.Redirect("showQuote.aspx?q=" + r.QuoteID.ToString());
	}

	private void LoadSingleQuote(int quoteId)
	{
		CoreDataObjects.QuotesViewRow r;

		if (quoteId == 0)
			//load the quote of the day
			r = (CoreDataObjects.QuotesViewRow)quotesAdpt.GetQuoteOfTheDay().Rows[0];
		else
			//get quote that is requested
			r = (CoreDataObjects.QuotesViewRow)quotesAdpt.GetByQuoteID(quoteId).Rows[0];

		//setup page meta data title
		Page.Title = string.Format("{0} Quote #{1}", r.FullName, r.QuoteID);

		//change meta title on the quote of the day request
		if (_qotd)
			Page.AddMetaInfo(OpenGraphMetaTags.Title, string.Format("Quote of the Day: {0} Quote #{1}", r.FullName, r.QuoteID));
		else
			Page.AddMetaInfo(OpenGraphMetaTags.Title, string.Format("{0} Quote #{1}", r.FullName, r.QuoteID));

		Page.AddMetaInfo(OpenGraphMetaTags.Type, "article");
		Page.AddMetaInfo(OpenGraphMetaTags.Site_Name, "WhatWouldTheFoundersDo.org");
		Page.AddMetaInfo(OpenGraphMetaTags.URL, string.Format("http://www.whatwouldthefoundersdo.org/showquote.aspx?q={0}", r.QuoteID));
		Page.AddMetaInfo(OpenGraphMetaTags.Description, string.Format("{0}", r.QuoteText));

		LoadFounderData(r.FounderID);

		try
		{

			if (Request.QueryString["Edit"] != null)
			{
				if (Authenticator.GetUser().Role == ContributorRoles.Admin)
				{
					if (!IsPostBack)
					{
						FoundersTableAdapter foundersAdpt = new FoundersTableAdapter();
						this.ddFounders.DataSource = foundersAdpt.GetData().OrderBy(n => n.FirstName);
						this.ddFounders.DataTextField = "FullName";
						this.ddFounders.DataValueField = "FounderID";
						this.ddFounders.DataBind();
					}


					//show the quote in a text box
					QuoteText.Text = r.QuoteText;
					QuoteText.Width = 580;
					QuoteText.Rows = (r.QuoteText.Length / 118) + 1;
					QuoteText.CssClass = "transparent";
					QuoteText.ToolTip = "Click to modify.";
					QuoteText.Attributes.Add("onclick", "this.className = 'selected';");
					QuoteText.Attributes.Add("onblur", "this.className = 'transparent';");

					ddFounders.ClearSelection();
					//ddFounders.SelectedValue = r.FounderID.ToString();
					ddFounders.Items.FindByValue(r.FounderID.ToString()).Attributes.Add("selected", "true");

					PanelAdmin.Visible = true;
					QuoteId.Value = r.QuoteID.ToString();
				}
			}
			else
				QuoteHelper.LoadQuote(PanelQuote, "", r.QuoteText, 0, r.ReferenceInfo, r.Keywords, false);
		}
		catch
		{
			QuoteHelper.LoadQuote(PanelQuote, "", r.QuoteText, 0, r.ReferenceInfo, r.Keywords, false);
		}
	}

	private void LoadFounderData(int founderId)
	{
		FoundersTableAdapter founders = new FoundersTableAdapter();
		CoreDataObjects.FoundersRow r = (CoreDataObjects.FoundersRow)founders.GetFounderById(founderId).Rows[0];

		//display image if available
		string imageUrl = string.Format("/images/founders/thumbs/{0}_thumb.jpg", r.FullName.Replace(' ', '-'));
		if (!File.Exists(Server.MapPath(@"~" + imageUrl)))
			imgFounder.Visible = false;

		//add the image and meta tag data for social networks
		imgFounder.ImageUrl = imageUrl;
		imgFounder.AlternateText = "Image of Founder " + r.FullName;
		imgFounder.ToolTip = r.FullName;
		imgFounder.Width = 83;
		imgFounder.Height = 110;
		Page.AddMetaInfo(OpenGraphMetaTags.Image, imageUrl);
		Page.AddMetaInfo(MetaTags.ImageThumbnail, imageUrl);
	   
		//populate the founder information fields
		lblFounderName.InnerText = r.FullName;
		lblFounderLifeSpan.InnerText = string.Format("{0} - {1}", r.DateBorn.ToString("MMMM dd, yyyy"), r.DateDied.ToString("MMMM dd, yyyy"));
	}

	//for editing
	protected void btnSaveChanges_Click(object sender, EventArgs e)
	{
		QuotesTableAdapter quotes = new QuotesTableAdapter();
		CoreDataObjects.QuotesRow r = quotes.GetByQuoteId(int.Parse(QuoteId.Value))[0];

		r.FounderID = ddFounders.SelectedValue.ToInt();
		r.QuoteText = QuoteText.Text;
		quotes.Update(r);

		if (Request.QueryString["RedirectUrl"] != null)
			Response.Redirect(Request.QueryString["RedirectUrl"]);
		else
			Response.Redirect("browse.aspx?q=" + Request.QueryString["q"]);
	}
}
