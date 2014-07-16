using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class Quote
	{
		public int QuoteId { get; set; }
		
		public int FounderId { get; set; }

		public int QuoteStatusTypeId { get; set; }

		[Required]
		public string QuoteText { get; set; }

		[Required]
		[StringLength(255)]
		public string Keywords { get; set; }

		[Required]
		public DateTime DateAdded { get; set; }
		
		public DateTime? DateModified { get; set; }
				
		public virtual Founder Founder { get; set; }

		public virtual QuoteStatusType QuoteStatusType { get; set; }

		public virtual ICollection<QuoteHistory> QuoteHistories { get; set; } 

		public virtual ICollection<QuoteReference> QuoteReferences { get; set; } 
	}
}