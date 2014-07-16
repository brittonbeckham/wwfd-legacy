using System;

namespace Wwfd.Core.Framework
{
	public class PaginatedQuery
	{
		public const int DEFAULT_ROWS_PER_PAGE = 12;
		public const int DEFAULT_INITIAL_PAGE = 1;

		private int _currentPage;

		public PaginatedQuery()
		{
			//set defaults
			RowsPerPage = DEFAULT_ROWS_PER_PAGE;
			CurrentPage = DEFAULT_INITIAL_PAGE;
		}

		public int RowsPerPage { get; set; }

		/// <summary>
		///     The one-based (1) index of the result pages to retrieve. Setting this property to a number less than one will
		///     result in an InvalidOperationException exception.
		/// </summary>
		public int CurrentPage
		{
			get { return _currentPage; }
			set
			{
				if (value <= 0)
					throw new InvalidOperationException("The current page property cannot be less than or equal to zero.");

				_currentPage = value;
			}
		}

		public int CalcNumberOfRecordsToSkip()
		{
			return RowsPerPage*(CurrentPage - 1); //converts to zero-based index
		}
	}
}