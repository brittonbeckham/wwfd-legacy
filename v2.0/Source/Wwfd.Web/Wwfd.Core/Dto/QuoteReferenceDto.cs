using System;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Core.Dto
{
	public class QuoteReferenceDto
	{
		public int QuoteReferenceId { get; set; }

		public int QuoteId { get; set; }

		public int QuoteReferenceStatusTypeId { get; set; }

		public string ReferenceInformation { get; set; }
		
		public string Notes { get; set; }

		public DateTime DateAdded { get; set; }

		public DateTime? DateModified { get; set; }

		public DateTime? DateVerified { get; set; }

		public QuoteReferenceStatusType QuoteReferenceStatusType { get; set; }

		public Guid ContributorId { get; set; }

		public Guid VerifierId { get; set; }

	}
}