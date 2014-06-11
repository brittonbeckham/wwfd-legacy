using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Wwfd.Web;
using Wwfd.Web.Authentication;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;

namespace Wwfd.Web
{
	public partial class Login : System.Web.UI.Page
	{
		

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			try
			{
				Authenticator.AuthenticateUser(txtEmail.Text, txtPassword.Text);

				//this gets the page they were trying to access
				string url = FormsAuthentication.GetRedirectUrl(txtEmail.Text, false);

				//sets ASP.net Forms auth cookie
				FormsAuthentication.SetAuthCookie(txtEmail.Text, true);

				//set the expiration to 14 days from today
				HttpCookie cookie = Response.Cookies[FormsAuthentication.FormsCookieName];
				cookie.Expires = DateTime.Now + new TimeSpan(14, 0, 0, 0);

				//redirect them to the request page
				Response.Redirect("dashboard.aspx", false);
			}
			catch (Exception ex)
			{
				Output.Text = ex.Message;
				Output.Visible = true;
			}
		}

		protected void btnLogin_Load(object sender, EventArgs e)
		{
			this.Page.Form.DefaultButton = btnLogin.UniqueID;
		}
	}
}