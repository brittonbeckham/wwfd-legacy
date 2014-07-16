using System.ComponentModel;

namespace Wwfd.Data.CodeFirst.Enums
{
	public enum DailyQuoteProcessStatusType
	{
		Processed = 1,

		[Description("Processed with Errors")]
		ProcessedWithErrors = 2,
		Failure = 3,
	}
}