using System;
using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.dbo
{
	public class PerformedSearch
	{
		public int PerformedSearchId { get; set; }

		[StringLength(1024)]
		public string TextSearchString { get; set; }

		[StringLength(1024)]
		public string KeywordSearchString { get; set; }

		public DateTime DateSearched { get; set; }
	}
}