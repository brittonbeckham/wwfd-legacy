using System.Collections.Generic;
using System.Web.Http;
using Wwfd.Core.Agents;
using Wwfd.Core.Dto;

namespace Wwfd.Web.Api.Controllers
{
	public class QuotesController : ApiController
	{
		/// <summary>
		/// Retrieves a quote by the given Id.
		/// </summary>
		/// <param name="id">The quoteId.</param>
		/// <returns></returns>
		[Route("Quotes/GetById/{id}")]
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
		[Route("Quotes/GetByFounderId/{id}")]
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
		[Route("Quotes/SearchText/{searchString}")]
		[AcceptVerbs("GET")]

		public IEnumerable<QuoteDto> SearchByText(string searchString)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(searchString, null);
		}

		/// <summary>
		/// Searches all quotes by their keywords using the given search string.
		/// </summary>
		/// <param name="searchString">Any word or words seperated by spaces.</param>
		/// <returns></returns>
		[Route("Quotes/SearchKeyword/{searchString}")]
		[AcceptVerbs("GET")]
		public IEnumerable<QuoteDto> SearchByKeyword(string searchString)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(null, searchString);
		}

		/// <summary>
		/// Searches all quotes by their text and keywords using the given search string.
		/// </summary>
		/// <param name="searchString">Any word or words seperated by spaces.</param>
		/// <returns></returns>
		[Route("Quotes/SearchAll/{searchString}")]
		[AcceptVerbs("GET")]
		public IEnumerable<QuoteDto> SearchAll(string searchString)
		{
			using (var agent = new QuoteAgent())
				return agent.Search(searchString, searchString);
		}
	}
}
