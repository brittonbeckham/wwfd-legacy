using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wwfd.Data;
using Wwfd.Data;

namespace Wwfd.Core
{
    public class QuotesAgent
    {
	    public List<Quote> Search(string searchText)
	    {
		    using (WwfdEntities context = new Data.WwfdEntities())
		    {
				return context.SearchQuotes(searchText).Cast<Quote>().ToList();				
		    }
	    }

	    public Quote GetQuote(int quoteId)
	    {
			using (WwfdEntities context = new Data.WwfdEntities())
			{
				return context.Quotes.FirstOrDefault(r => r.QuoteID == quoteId);
			}
		}
    }
}
