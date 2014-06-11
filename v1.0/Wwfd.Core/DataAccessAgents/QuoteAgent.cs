using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Wwfd.Core.Data;

namespace Wwfd.Core.Agents
{
	public static class QuoteAgent
	{
		public static List<DataObjects.Quote> Search(string searchText)
		{
			using (WwfdEntities dbContext = new WwfdEntities())
			{
				return dbContext.SearchQuotes(searchText, true)
				                .Select(r =>
				                        new DataObjects.Quote
					                        {
						                        FounderName = r.FullName,
						                        QuoteText = r.QuoteText,
												QuoteId = r.QuoteID,
												FounderId = r.FounderID,
												ReferenceInfo = r.ReferenceInfo,
												FounderImageThumbnail = new System.Uri(
													string.Format("{0}/images/founders/thumbs/{1}_thumb.jpg",
													ConfigurationManager.AppSettings["Wwfd:FounderImagesBaseUrl"],
													r.FullName.Replace(' ', '-')))
					                        }).ToList();
			}
		}
	}
}
