using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;

public partial class _Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FoundersTableAdapter founders = new FoundersTableAdapter();
        ContributionsViewTableAdapter contributors = new ContributionsViewTableAdapter();
        QuotesViewTableAdapter quotes = new QuotesViewTableAdapter();
        
        Contributor c = Authenticator.GetUser();

        int tquotes = (int)quotes.GetCount();;
        int cquotes = (int)quotes.GetCountByContributor(c.ContributorId);

        lblTotalFounders.Text = founders.GetCount().ToString();
        lblTotalQuotes.Text = tquotes.ToString();
        lblQuotesContributed.Text = cquotes.ToString();
        lblContributionRatio.Text = Math.Round(((double)cquotes / (double)tquotes)*100,2).ToString();
        
        //popluate grids
        GridView1.DataSource = contributors.GetData();
        GridView1.DataBind();

        GridView2.DataSource = quotes.GetTop15ByContributorId(c.ContributorId);
        GridView2.DataBind();
    }
}