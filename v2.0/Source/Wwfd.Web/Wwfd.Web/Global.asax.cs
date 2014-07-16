using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SSW.Data.EF.Enums.Core;
using Wwfd.Data.CodeFirst;
using Wwfd.Data.CodeFirst.Context;
using Wwfd.Data.CodeFirst.Enums;

namespace Wwfd.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

		protected void Application_BeginRequest()
		{
			WwfdContext.Initialize();
		}

		protected void Application_EndRequest()
		{
		}
    }
}
