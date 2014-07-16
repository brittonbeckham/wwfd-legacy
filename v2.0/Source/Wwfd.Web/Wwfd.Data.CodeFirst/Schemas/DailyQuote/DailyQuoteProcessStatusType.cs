using System.ComponentModel.DataAnnotations;

namespace Wwfd.Data.CodeFirst.Schemas.DailyQuote
{
	public class DailyQuoteProcessStatusType
	{
		public int Id { get; set; }

		[Required]
		[StringLength(25)]
		public string Name { get; set; }
	}
}