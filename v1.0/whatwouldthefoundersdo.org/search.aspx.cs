using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Wwfd.Web;
using Wwfd.Web.Quotes;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Extensions;
using BLB.Common.Web;

public partial class Search : System.Web.UI.Page
{
	QuotesViewTableAdapter quotesAdpt = new QuotesViewTableAdapter();
	TextBox searchBox;
	SearchTypes _searchType;
	int _founderId;
	int _page = 1;
	bool _sort = false;
	const int RECORDS_PER_PAGE = 10;

	protected void Page_Load(object sender, EventArgs e)
	{
		bool saveSearch = false;
		string searchText = string.Empty;
		
		//get the text in the search box
		searchBox = (TextBox)FindControl("ctl00$txtSearchBox");

		//validate page vars
		PageVariablesCollection vars = new PageVariablesCollection();
		vars.Add(new RequestVariable("p", typeof(int), false));
		vars.Add(new RequestVariable("founderId", typeof(int), false));
		vars.Add(new RequestVariable("searchType", typeof(SearchTypes), true));
		vars.Add(new RequestVariable("sort", typeof(bool), false));

		Page.ValidateVariables(vars);

		//the page variable (browsing pages with a result set)
		//_searchType = (SearchTypes)Enum.Parse(typeof(SearchTypes), vars["type"].Value.ToString());
		_searchType = (SearchTypes)vars["searchType"].Value;
		_sort = vars["sort"].HasValue ? (bool)vars["sort"].Value : false; 
		
		//check for page variable
		if (vars["p"].HasValue)
			_page = (int)vars["p"].Value;
		
		
		switch(_searchType)
		{
			case SearchTypes.Founders:

				//check to see if a founderId is supplied (i.e. the search is for all things related to a founder)
				if (vars["founderId"].HasValue)
					_founderId = (int)vars["founderId"].Value;

				//load founder quotes 
				LoadQuotesByFounder(_founderId);

				PanelFounder.Visible = true;
				PanelFounderQuotesSummary.Visible = true;
				PanelSearchSummary.Visible = false;

				break;

			case SearchTypes.Quotes:
				//upon search postback (the user searched in the search box from this page)
				if (IsPostBack)
					this.SearchQuotes(searchBox.Text, true);
				//the request is an initial one, or from another page
				else
				{
					//a supplied search string was given
					if (Request.QueryString["SearchText"] != null)
					{
						//find out if this was a custom search
						if(Request.QueryString["Custom"] != null)
							saveSearch = (Request.QueryString["Custom"] == "true") ? true : false;

						//search
						this.SearchQuotes(Request.QueryString["SearchText"], saveSearch);
					}
					//no search string -- redirect back to home
					else
						Response.Redirect("Default.aspx");
				}               
				break;

			case SearchTypes.Documents:
				//do nothing
				break;            
		}       
	}

	private void SearchQuotes(string searchText, bool saveSearch)
	{
		searchBox.Text = searchText;
		string[] words = searchText.Split(' ');

		var quotesAdpt = new QuotesViewTableAdapter();
		var results = quotesAdpt.SearchQuotes(searchText, saveSearch);

		//if (_sort)
		//    quotesAdpt.SearchQuotes(searchText, saveSearch).OrderBy<Data.QuotesViewRow, System.String>(r => r.FullName).CopyToDataTable<Data.QuotesViewRow>(results, LoadOption.OverwriteChanges);
		

		lblSummaryText.Text = string.Format(@"Your search for ""<b>{0}</b>"" returned {1} results from {2} Founders. ", searchText, results.Rows.Count, results.SelectDistinct("FounderID").Rows.Count);
		
		if (results.Rows.Count > 0)
			ShowResults(results, words);
	}
	
	private void ShowResults(DataTable results, string[] searchWords)
	{
		lnkSort.NavigateUrl = string.Format("~/Search.aspx?searchType=Quotes&searchText={0}&sort={1}", Server.UrlEncode(searchBox.Text), "true");

		bool addedMeta = false;
		foreach (CoreDataObjects.QuotesViewRow r in results.Rows)
		{
			QuoteHelper.LoadQuote(PanelQuotes, r.FullName, r.QuoteText, r.QuoteID, r.ReferenceInfo, r.Keywords, searchWords, true);

			if (!addedMeta)
			{
				int founderCount = results.SelectDistinct("FounderID").Rows.Count;

				Page.Title = string.Format("{0} - WWFD Search", searchBox.Text);
				Page.AddMetaInfo(MetaTags.Title, string.Format("WWFD Search for '{0}' Returned {1} Quote(s) From {2} Founder(s).", searchBox.Text, results.Rows.Count, founderCount));
				Page.AddMetaInfo(MetaTags.Description, string.Format("{0}", r.QuoteText, r.FullName));
				addedMeta = true;
			}

		}
	}

	private void LoadQuotesByFounder(int founderId)
	{
		bool addedMeta = false;
		CoreDataObjects.QuotesViewDataTable quotesDt = quotesAdpt.GetByFounderID(founderId);

		int numOfRecords = quotesDt.Rows.Count;
		int numOfPages = (int)Math.Ceiling((decimal)numOfRecords / (decimal)RECORDS_PER_PAGE);

		//loop for generating the page links for these results
		for (int p = 1; p <= numOfPages; p++)
		{
			string linkUrl = string.Format("~/search.aspx?searchType=Founders&founderId={0}&p={1}", _founderId, p);
			HyperLink pageLink = new HyperLink();
			pageLink.NavigateUrl = linkUrl;
			pageLink.Text = p.ToString();

			//set visual difference for active page
			if (p == _page)
				pageLink.CssClass = "ActivePage";

			PanelPagingBottom.Controls.Add(pageLink);
		}

		//loop that displays the quotes
		int startRecord = ((_page * RECORDS_PER_PAGE) - RECORDS_PER_PAGE) + 1;
		int lastRecOnPage = _page * RECORDS_PER_PAGE;
		int recRatio = lastRecOnPage / numOfRecords;
		int totalRecsLeft = numOfRecords - ((_page - 1) * RECORDS_PER_PAGE);
		int endRecord = lastRecOnPage + (recRatio * totalRecsLeft) - (recRatio * RECORDS_PER_PAGE);

		if (numOfRecords < RECORDS_PER_PAGE)
		{
			startRecord = 1;
			endRecord = numOfRecords;
			PanelPagingBottom.Visible = false;
		}
		
		for (int i = startRecord; i <= endRecord; i++)
		{
			//add the quote to the page
			CoreDataObjects.QuotesViewRow r = (CoreDataObjects.QuotesViewRow)quotesDt.Rows[i - 1];
			QuoteHelper.LoadQuote(PanelQuotes, "", r.QuoteText, r.QuoteID, r.ReferenceInfo, r.Keywords, false);

			//add the page meta data only once
			if (!addedMeta)
			{
				SummaryText.Text = string.Format("Showing {3} Quotes <b>{0}</b> - <b>{1}</b> of <b>{2}</b>", startRecord, endRecord, quotesDt.Rows.Count, r.FullName);
				LoadFounderData(_founderId);
				Page.Title = string.Format("Quotes By {0}", r.FullName);
				Page.AddMetaInfo(MetaTags.Title, string.Format("What Would The Founders Do : {0}", r.FullName));
				Page.AddMetaInfo(MetaTags.Description, string.Format("WWFD found {0} quotes by {1}: \"{2}\" More quotes available at http://whatwouldthefoundersdo.org.", quotesDt.Rows.Count, r.FullName, r.QuoteText));
				addedMeta = true;
			}
		}
	}

	private void LoadFounderData(int founderId)
	{
		FoundersTableAdapter founders = new FoundersTableAdapter();
		CoreDataObjects.FoundersRow r = (CoreDataObjects.FoundersRow)founders.GetFounderById(founderId).Rows[0];

		//display image if available
		string imageUrl = string.Format("~/images/founders/thumbs/{0}_thumb.jpg", r.FullName.Replace(' ', '-'));
		if (!File.Exists(Server.MapPath(imageUrl)))
			imgFounder.Visible = false;

		//add the image and meta tag data for social networks
		imgFounder.ImageUrl = imageUrl;
		imgFounder.AlternateText = "Image of Founder " + r.FullName;
		imgFounder.ToolTip = r.FullName;
		imgFounder.Width = 83;
		imgFounder.Height = 110;
		Page.AddMetaInfo(MetaTags.ImageThumbnail, imageUrl);

		//populate the founder information fields
		lblFounderName.InnerText = r.FullName;
		lblFounderLifeSpan.InnerText = string.Format("{0} - {1}", r.DateBorn.ToString("MMMM dd, yyyy"), r.DateDied.ToString("MMMM dd, yyyy"));
	}

}
