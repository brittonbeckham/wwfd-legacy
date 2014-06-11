using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Extensions;

public partial class _AddFounder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAddFounder_Click(object sender, EventArgs e)
    {
        int id = 0;

        try
        {
            FoundersTableAdapter foundersAdpt = new FoundersTableAdapter();

            string firstName, lastName, middleName, suffix;
            firstName = txtFirstName.Text.Trim();
            middleName = txtMiddleName.Text.Trim() == "" ? null : txtMiddleName.Text.Trim();
            lastName = txtLastName.Text.Trim();
            suffix = txtSuffix.Text.Trim() == "" ? null : txtSuffix.Text.Trim();
            
            //query the addition against the database and confirm if the 
            if (foundersAdpt.GetData().Count(
                row => 
                    row.FirstName == firstName && 
                    row.LastName == lastName &&
                    middleName == (row.IsMiddleNameNull() ? null : row.MiddleName) &&
                    suffix == (row.IsSuffixNull() ? null : row.Suffix)) != 0)
                throw new ApplicationException("The Founder that you attempted to enter already exists.");

            //insert the new record
            foundersAdpt.InsertQuick(firstName, middleName, lastName, suffix);

            //get the new identity
            id = (int)foundersAdpt.GetIdentity();

            //kick the user to the add quote page
            Response.Redirect("add-quote.aspx?Founder=" + id.ToString());
        }
        catch (Exception ex)
        {
            this.ShowError(ex);
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMiddleName.Text = "";
            txtSuffix.Text = "";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("add-quote.aspx");
    }
}

