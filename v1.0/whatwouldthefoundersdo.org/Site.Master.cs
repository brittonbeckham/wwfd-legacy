using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Wwfd.Web;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Web;
using BLB.Common.Extensions;

namespace Wwfd.Web
{

	public partial class SiteMaster : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			QuotesViewTableAdapter quotesAdpt = new QuotesViewTableAdapter();
			QuoteCountsTableAdapter quoteCountsAdpt = new QuoteCountsTableAdapter();

			//founders list
			foreach (CoreDataObjects.QuoteCountsRow r in quoteCountsAdpt.GetQuoteCounts().Where(r => r.TotalQuotes > 0))
			{
				HyperLink link = new HyperLink();
				link.NavigateUrl = string.Format("~/search.aspx?searchType=Founders&founderId={0}", r.FounderID);
				link.Text = string.Format("{0} ({1})", r.FullName, r.TotalQuotes);
				link.CssClass = "CategoryLink";
				link.ToolTip = string.Format("All quotes by '{0}'", r.FullName);
				link.AppRelativeTemplateSourceDirectory = "~";
				PanelCategories.Controls.Add(link);
			}

			//for logged in users
			if (Authenticator.IsUserLoggedIn())
			{
				Contributor user = Authenticator.GetUser();

				lblUserEmail.Text = user.EmailAddress + " | ";
				lblUserEmail.ToolTip = "You have contributed: " + quotesAdpt.GetCountByContributor(user.ContributorId).ToString() + " quotes.";

				lnkSignInOut.Text = "Sign out";
				lnkSignInOut.NavigateUrl = "~/logout.aspx";
				lnkSignInOut.ToolTip = "Click here to log out.";

				lnkDashboard.Visible = true;
				lnkContribute.Visible = false;
			}
			else
			{
				lnkSignInOut.Text = "Sign In";
				lnkSignInOut.NavigateUrl = "~/contribute/login.aspx";

				lnkDashboard.Visible = false;
				lnkContribute.Visible = true;
			}

			//set the subscription email validator regular expression
		}

		protected void txtSearchFounders_TextChanged(object sender, EventArgs e)
		{
			PanelCategories.Controls.Clear();

			QuotesViewTableAdapter quotesAdpt = new QuotesViewTableAdapter();
			QuoteCountsTableAdapter quoteCountsAdpt = new QuoteCountsTableAdapter();

			//quotes by founder
			foreach (CoreDataObjects.QuoteCountsRow r in quoteCountsAdpt.SearchFounders(txtSearchFounders.Text).Where(r => r.TotalQuotes > 0))
			{
				HyperLink link = new HyperLink();
				link.NavigateUrl = string.Format("~/search.aspx?searchType=Founders&founderId={0}", r.FounderID);
				link.Text = string.Format("{0} ({1})", r.FullName, r.TotalQuotes);
				link.CssClass = "CategoryLink";
				link.ToolTip = string.Format("All quotes by '{0}'", r.FullName);
				link.AppRelativeTemplateSourceDirectory = "~";
				PanelCategories.Controls.Add(link);
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			if (txtSearchBox.Text != "Search for advice" && txtSearchBox.Text != string.Empty)
				Response.Redirect("~/search.aspx?custom=true&searchType=Quotes&searchText=" + Server.UrlEncode(txtSearchBox.Text));
		}

		protected void btnSubscribe_Click(object sender, EventArgs e)
		{
			//verify that the email is valid
			try
			{
				QOTDSubscribersTableAdapter subscribers = new QOTDSubscribersTableAdapter();
				subscribers.InsertQuick(txtSubscriberEmail.Text);

				PanelQOTDSubscribe.Controls.Clear();

				Label l = new Label();
				l.Text = "Your email address has been added to the subscription successfully!";
				PanelQOTDSubscribe.Controls.Add(l);
			}
			catch (Exception ex)
			{
				PanelQOTDSubscribe.ShowError(ex);
			}
		}
	}
}