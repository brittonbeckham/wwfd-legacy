using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class QuoteReference
	{
		public int QuoteReferenceId { get; set; }

		public int QuoteId { get; set; }

		public int QuoteReferenceStatusTypeId { get; set; }

		[ForeignKey("Contributor")]
		public Guid ContributorId { get; set; }

		[ForeignKey("Verifier")]
		public Guid? VerifierId { get; set; }

		[Required]
		public string ReferenceInformation { get; set; }
		
		public string Notes { get; set; }

		[Required]
		public DateTime DateAdded { get; set; }

		public DateTime? DateModified { get; set; }

		public DateTime? DateVerified { get; set; }

		//public virtual Quote Quote { get; set; }

		public virtual QuoteReferenceStatusType QuoteReferenceStatusType { get; set; }

		public virtual Contributor Contributor { get; set; }
		
		public virtual Contributor Verifier { get; set; }
	}
}