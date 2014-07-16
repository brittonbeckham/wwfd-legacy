using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wwfd.Core.Agents;
using Wwfd.Core.Dto.Aggregates;

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
			List<FounderWithQuoteCountDto> model = new FounderAgent().GetWithQuoteCountByName(id, null).ToList();

			return PartialView(model);
		}
	}
}