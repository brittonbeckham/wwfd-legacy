using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Wwfd.Core.Agents;
using Wwfd.Core.DataObjects;

namespace Wwfd.Api
{
	public class WwfdSearchService : IWwfdSearchService
	{
		public List<Quote> SearchQuotes(string searchText)
		{
			return QuoteAgent.Search(searchText);
		}

		public List<Founder> SearchFounders(string founderName)
		{
			throw new NotImplementedException();
		}

		public List<Founder> GetFounders(string founderName)
		{
			throw new NotImplementedException();
		}
	}
}
