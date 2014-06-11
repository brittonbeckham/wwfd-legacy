using System.Collections.Generic;
using System.ServiceModel;
using Wwfd.Core.DataObjects;

namespace Wwfd.Api
{
	[ServiceContract]
	public interface IWwfdSearchService
	{
		[OperationContract]
		List<Quote> SearchQuotes(string searchText);

		[OperationContract]
		List<Founder> SearchFounders(string founderName);

		[OperationContract]
		List<Founder> GetFounders(string founderName);
	}
}