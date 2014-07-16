using System;
using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class QuoteHistory
	{
		public int QuoteHistoryId { get; set; }

		[Required]
		public int QuoteId { get; set; }

		[Required]
		public int QuoteHistoryTypeId { get; set; }

		[Required]
		public Guid ContributorId { get; set; }

		[Required]
		public DateTime DateOccured { get; set; }

		public virtual Quote Quote { get; set; }

		public virtual QuoteHistoryType HistoryType { get; set; }

		public virtual Contributor Contributor { get; set; }
	}
}