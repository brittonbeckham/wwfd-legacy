using System.Collections.Generic;
using System.Web.Http;
using Wwfd.Core.Agents;
using Wwfd.Core.Dto;

namespace Wwfd.Web.Api.Controllers
{
	[RoutePrefix("quote")]
	public class QuotesController : ApiController
	{
		/// <summary>
		/// Retrieves a quote by the given Id.
		/// </summary>
		/// <param name="id">The quoteId.</param>
		/// <returns></returns>
		[Route("{id:int}")]
		public QuoteDto GetById(int id)
		{
			using (var agent = new QuoteAgent())
				return agent.GetById(id);
		}

		/// <summary>
		/// Retrieves all quotes for a given Founder.
		/// </summary>
		/// <param name="id">The founderId.</param>
		/// <returns></returns>
		[Route("founder/{id:int}")]
		public IEnumerable<QuoteDto> GetByFounderId(int id)
		{
			using (var agent = new QuoteAgent())
				return agent.GetByFounderId(id);
		}

		/// <summary>
		/// Searches all quotes by their text using the given search string.
		/// </summary>
		/// <param name="searchString">Any word or words seperated by spaces.</param>
		/// <returns></returns>
		[Route("search/text/{searchString}")]
		[AcceptVerbs("GET")]

		public IEnumerable<QuoteDto> SearchByText(string searchString)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(searchString, null);
		}

		/// <summary>
		/// Searches all quotes by their keywords using the given search string.
		/// </summary>
		/// <param name="keywords">Any word or words seperated by spaces.</param>
		/// <returns></returns>
		[Route("search/keyword/{keywords}")]
		[AcceptVerbs("GET")]
		public IEnumerable<QuoteDto> SearchByKeyword(string keywords)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(null, keywords);
		}

		/// <summary>
		/// Searches all quotes by their text and keywords using the given search string.
		/// </summary>
		/// <param name="searchString">Any word or words seperated by spaces.</param>
		/// <returns></returns>
		[Route("search/all/{searchString}")]
		[AcceptVerbs("GET")]
		public IEnumerable<QuoteDto> SearchAll(string searchString)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(searchString, searchString);
		}
	}
}
