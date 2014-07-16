using System;
using System.Collections.Generic;

namespace Wwfd.Core.Dto
{
	public class FounderDto
	{
		public int FounderId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public DateTime DateBorn { get; set; }
		public DateTime DateDied { get; set; }
		public string FullName { get; set; }

		public List<FounderRoleDto> Roles { get; set; }

	}
}