using System;
using System.Collections.Generic;

namespace Wwfd.Core.Dto
{
	public class QuoteDto
	{
		public int QuoteId { get; set; }

		public string QuoteStatus { get; set; }

		public int QuoteStatusTypeId { get; set; }

		public string QuoteText { get; set; }

		public string Keywords { get; set; }

		public DateTime DateAdded { get; set; }

		public DateTime? DateModified { get; set; }

		public FounderDto Founder { get; set; }
		public List<QuoteReferenceDto> References { get; set; }

	}
}