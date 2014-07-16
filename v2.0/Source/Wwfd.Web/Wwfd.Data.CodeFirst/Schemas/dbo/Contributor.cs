using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class Contributor
	{
		public Guid ContributorId { get; set; }

		[Required]
		[StringLength(20)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(20)]
		public string LastName { get; set; }

		[Required]
		[StringLength(75)]
		public string Email { get; set; }

		[Required]
		[StringLength(25)]
		public string PasswordHash { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }
		
		public bool IsActive { get; set; }
		
		public virtual ICollection<ContributorRoleType> ContributorRoles { get; set; }
	}
}