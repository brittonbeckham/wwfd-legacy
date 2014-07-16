using System.Collections.Generic;

namespace Wwfd.Data.CodeFirst.Context
{
	public static class LanguageExtensions
	{
		public static bool In<T>(this T source, params T[] list)
		{
			return (list as IList<T>).Contains(source);
		}
	}
}