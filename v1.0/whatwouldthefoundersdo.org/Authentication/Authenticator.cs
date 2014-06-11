using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Wwfd.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;

namespace Wwfd.Web.Authentication
{
	public static class Authenticator
	{
		public static Contributor AuthenticateUser(string username, string password)
		{
			ContributorsTableAdapter contributorsAdpt = new ContributorsTableAdapter();

			try
			{
				if (!(bool)contributorsAdpt.AuthenticateUser(username, password))
					throw new Exception("The supplied credentials are invalid.");

				return new Contributor(username);
			}
			catch
			{
				throw;
			}
		}

		public static FormsAuthenticationTicket GetTicket()
		{
			//check for super admin
			if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				return FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
			else
				return null;
		}

		public static Contributor GetUser()
		{
			//check for super admin
			if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				return new Contributor(FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
			else
				return null;
		}

		public static bool IsUserLoggedIn()
		{
			//check for super admin
			if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				return true;
			else
				return false;
		}
	}
}