using System;
using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.DailyQuote
{
	public class DailyQuoteProcess
	{
		public int DailyQuoteProcessId { get; set; }

		public int DailyQuoteId { get; set; }

		public int DailyQuoteProcessStatusId { get; set; }
		
		[Required]
		public DateTime StartTime { get; set; }

		[Required]
		public DateTime EndTime { get; set; }

		public virtual DailyQuote DailyQuote { get; set; }

		public virtual DailyQuoteProcessStatusType Status { get; set; }
	}
}