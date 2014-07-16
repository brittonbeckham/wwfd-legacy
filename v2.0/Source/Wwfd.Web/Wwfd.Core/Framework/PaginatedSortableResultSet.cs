namespace Wwfd.Core.Framework
{
	public class PaginatedSortableResultSet : PaginatedResultSet
	{
		public string SortColumn { get; set; }

		public SortDirection SortOrder { get; set; }
	}
}