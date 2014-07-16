using System;
using System.Collections.Generic;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Core.Dto
{
	public class ContributorDto
	{
		public Guid ContributorId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string PasswordHash { get; set; }

		public DateTime DateCreated { get; set; }
		
		public bool IsActive { get; set; }
		
		public virtual ICollection<ContributorRoleType> ContributorRoles { get; set; }
	}
}