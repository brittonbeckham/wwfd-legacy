using System.Collections.Generic;
using System.Web.Mvc;
using Wwfd.Core;
using Wwfd.Core.Aggregates;

namespace Wwfd.Web.Controllers
{
    public class FounderController : Controller
    {
        //
        // GET: /Founder/
        public ActionResult Quotes(int id)
        {
            return View();
        }
		
		public PartialViewResult Listing(string id)
		{
			List<FounderWithQuoteCount> model = new FounderAgent().GetFoundersWithQuoteCount(id);

			return PartialView(model);
		}
	}
}