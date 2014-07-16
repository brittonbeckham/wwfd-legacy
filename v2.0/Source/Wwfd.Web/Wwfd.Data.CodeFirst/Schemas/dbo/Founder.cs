using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class Founder
	{
		public int FounderId { get; set; }

		[StringLength(10)]
		public string Prefix { get; set; }

		[Required]
		[StringLength(20)]
		public string FirstName { get; set; }

		[StringLength(20)]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(20)]
		public string LastName { get; set; }

		[StringLength(10)]
		public string Suffix { get; set; }

		[Required]
		[Column(TypeName = "Char")]
		[StringLength(1)]
		public string Gender { get; set; }
		
		[Column(TypeName = "Date")]
		public DateTime DateBorn { get; set; }
		
		[Column(TypeName = "Date")]
		public DateTime DateDied { get; set; }

		public string FullName { get; set; }

		public virtual ICollection<Quote> Quotes { get; set; }

		public virtual ICollection<FounderRoleType> FounderRoles { get; set; }
	}
}