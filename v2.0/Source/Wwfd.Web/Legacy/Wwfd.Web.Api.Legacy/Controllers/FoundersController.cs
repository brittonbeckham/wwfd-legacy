using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Wwfd.Core.Agents;
using Wwfd.Core.Dto;

namespace Wwfd.Web.Api.Controllers
{
	[RoutePrefix("founder")]
	public class FoundersController : ApiController
	{
		#region Open Api

		/// <summary>
		/// Retrieves a founder by the given id.
		/// </summary>
		/// <param name="id">The founderId.</param>
		/// <returns></returns>
		[Route("{id:int}")]
		public FounderDto GetById(int id)
		{
			using (var agent = new FounderAgent())
				return agent.GetById(id);
		}

		/// <summary>
		/// Retrieves all Founders with matching first name or last name. This call is a search of sorts so it will return
		/// any matching records with only part of the name matching.
		/// </summary>
		/// <param name="firstName">Any part of the Founder's first name. Use null to ignore searching this field.</param>
		/// <param name="lastName">Any part of the Founder's last name. Use null to ignore searching this field.</param>
		/// <returns></returns>
		[Route("name/{firstname}/last/{lastname}")]
		public IEnumerable<FounderDto> GetByName(string firstName, string lastName)
		{
			using (var agent = new FounderAgent())
				return agent.GetByName(firstName, lastName);
		}

		/// <summary>
		/// Returns all the Founders.
		/// </summary>
		/// <returns></returns>
		[Route("")]
		public IEnumerable<FounderDto> GetAll()
		{
			using (var agent = new FounderAgent())
				return agent.GetAll();
		}

		/// <summary>
		/// Returns a list of summaries of a Founder's full name, their related id, and the count of assocaited quotes. 
		/// This call uses the same search technique as GetByName.
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <returns></returns>
		[Route("quotecount/{firstname}/last/{lastname}")]
		public IEnumerable<FounderWithQuoteCountDto> GetWithQuoteCountByName(string firstName, string lastName)
		{
			using (var agent = new FounderAgent())
				return agent.GetWithQuoteCountByName(firstName, lastName);
		}

		/// <summary>
		/// Returns a summary of a Founder's full name, their related id, and the count of assocaited quotes, for the given founderId.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[Route("quotecount/{id:int}")]
		public IEnumerable<FounderWithQuoteCountDto> GetWithQuoteCountById(int id)
		{
			using (var agent = new FounderAgent())
				return agent.GetWithQuoteCountById(id);
		}

		#endregion

		#region Authorized

		[ApiExplorerSettings(IgnoreApi=true)]
		[Authorize]
		[Route("")]
		[HttpPost]
		public int Save(FounderDto founder, IEnumerable<FounderRoleDto> roles)
		{
			using (var agent = new FounderAgent())
				return agent.Save(founder, roles);
		}

		#endregion
	}
}
