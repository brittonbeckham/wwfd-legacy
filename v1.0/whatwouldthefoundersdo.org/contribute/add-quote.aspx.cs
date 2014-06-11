using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Extensions;

public partial class _AddQuote : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FoundersTableAdapter foundersAdpt = new FoundersTableAdapter();
            this.ddFounders.AppendDataBoundItems = true;
            this.ddFounders.Items.Add("");
            this.ddFounders.DataSource = foundersAdpt.GetData().OrderBy(n => n.FirstName);
            this.ddFounders.DataTextField = "FullName";
            this.ddFounders.DataValueField = "FounderID";
            this.ddFounders.DataBind();

            try
            {
                if (Request["Founder"] != null)
                    this.ddFounders.Items.FindByValue(Request["Founder"]).Selected = true;
            }
            catch
            {
                //do nothing
            }
        }
    }

    protected void btnSumit_Click(object sender, EventArgs e)
    {
        try
        {
            QuotesTableAdapter quotesAdpt = new QuotesTableAdapter();
            
            string keywords = string.Empty;
            if (txtKeyWords.Text != string.Empty)
                keywords = txtKeyWords.Text.Trim();

            string quote = txtQuote.Text;

            //check for first quotation mark
            if (quote.Substring(0, 1) == "\"") 
                quote = quote.Substring(1);
            
            //check for ending quotation mark
            if (quote.Substring(quote.Length - 1, 1) == "\"")
                quote = quote.Substring(0, quote.Length - 1);

            //confirm the quote doesn't already exist
            if ((int)quotesAdpt.DoesQuoteExist(quote) > 0)
                throw new ApplicationException("The quote already exists in the archive.");                
                
            //check for super admin
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
            Contributor c = new Contributor(ticket.Name);

            //8ebd9a3c-3993-4f92-993b-261e61e040cf == britton's id
            quotesAdpt.Insert(c.ContributorId, int.Parse(this.ddFounders.SelectedItem.Value), quote, this.txtReferenceInfo.Text, keywords, DateTime.Now, true);

            //this creates a unique url for the page (to ensure that it refreshes properly in the browser)
            Guid g = Guid.NewGuid();
            Response.Redirect("add-quote.aspx?Founder=" + this.ddFounders.SelectedValue + "&g=" + g.ToString().Substring(4,9));
        }
        catch(Exception ex)
        {
            this.ShowError(ex);
        }  
    }
}
