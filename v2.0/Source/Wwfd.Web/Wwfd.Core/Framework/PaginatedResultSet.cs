using System;

namespace Wwfd.Core.Framework
{
	public class PaginatedResultSet
	{
		public PaginatedResultSet()
		{
			//set defaults
			RowsPerPage = PaginatedQuery.DEFAULT_ROWS_PER_PAGE;
			CurrentPage = PaginatedQuery.DEFAULT_INITIAL_PAGE;
		}

		public int RowsPerPage { get; set; }
		public int CurrentPage { get; set; }

		/// <summary>
		///     Returns the number of pages based off the TotalResults and RowsPerPage property values.
		/// </summary>
		public int TotalPages
		{
			get { return (int) Math.Ceiling((decimal) TotalResults/RowsPerPage); }
		}

		public int TotalResults { get; set; }
	}
}