using System;
using System.ComponentModel.DataAnnotations;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Data.CodeFirst.Schemas.DailyQuote
{
	public class DailyQuote
	{
		public int DailyQuoteId { get; set; }

		public int QuoteId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		//public virtual Quote Quote { get; set; }
	}
}