namespace Wwfd.Core.Framework
{
	public class PaginatedSortableQuery : PaginatedQuery
	{
		public string SortColumn { get; set; }

		public SortDirection SortOrder { get; set; }
	}
}