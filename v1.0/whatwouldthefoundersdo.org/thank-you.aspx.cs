using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLB.Common.Extensions;
using BLB.Common.Web;

public partial class ThankYou : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageVariablesCollection vars = new PageVariablesCollection();
        vars.Add(new RequestVariable("message", typeof(Guid), true, PostMethods.Get));

        try
        {
            this.ValidateVariables(vars);
        }
        catch
        {
            Response.Redirect("~/home.aspx");
        }

        Guid messageId = (Guid)vars["message"].Value;

        switch(messageId.ToInt())
        {
            case 1:
                Page.Title = "Registration Submitted Successfully";
                this.MessageRegSubmitted.Visible = true;
                break;

            case 2:
                Page.Title = "Registration Confirmed";
                this.MessageRegComplete.Visible = true;
                break;
        }        
    }
}
