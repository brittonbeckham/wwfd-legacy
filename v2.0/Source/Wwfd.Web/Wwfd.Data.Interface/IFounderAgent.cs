using System.Collections.Generic;

namespace Wwfd.Data.Interface
{
    public interface IFounderAgent
    {
	    FounderDto GetById(int id);

		IEnumerable<FounderDto> GetByName(string firstName, string lastName);

		IEnumerable<FounderDto> GetAll();

	    int Save(FounderDto founder, IEnumerable<FounderRoleDto> roles);

		IEnumerable<FounderWithQuoteCountDto> GetWithQuoteCountByName(string firstName, string lastName);
    }
}
