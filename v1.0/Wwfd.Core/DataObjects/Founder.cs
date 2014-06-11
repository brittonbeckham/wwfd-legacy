using System;

namespace Wwfd.Core.DataObjects
{
	public class Founder
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public DateTime DateBorn { get; set; }
		public DateTime DateDied { get; set; }
	}
}