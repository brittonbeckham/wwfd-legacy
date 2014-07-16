using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Text.RegularExpressions;

namespace Wwfd.Data.CodeFirst.Context
{
	//thanks: http://www.entityframework.info/Home/FullTextSearch
	public class FullTextSearchInterceptor : IDbCommandInterceptor
	{
		private const string FULL_TEXT_PREFIX = "-FTSPREFIX-";
		public static string AsFullTextSearch(string search)
		{
			return string.Format("({0}{1})", FULL_TEXT_PREFIX, search);
		}
		public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}
		public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}
		public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
			RewriteFullTextQuery(command);
		}
		public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
		}
		public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
			RewriteFullTextQuery(command);
		}
		public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
		}
		private static void RewriteFullTextQuery(DbCommand cmd)
		{
			string text = cmd.CommandText;
			for (int i = 0; i < cmd.Parameters.Count; i++)
			{
				DbParameter parameter = cmd.Parameters[i];
				if (parameter.DbType.In(DbType.String, DbType.AnsiString, DbType.StringFixedLength, DbType.AnsiStringFixedLength))
				{
					if (parameter.Value == DBNull.Value)
						continue;

					var value = (string)parameter.Value;
					if (value.IndexOf(FULL_TEXT_PREFIX) >= 0)
					{
						parameter.Size = 4096;
						parameter.DbType = DbType.AnsiStringFixedLength;
						value = value.Replace(FULL_TEXT_PREFIX, ""); // remove prefix we added n linq query
						value = value.Substring(1, value.Length - 2); // remove %% escaping by linq translator from string.Contains to sql LIKE
						parameter.Value = value;
						cmd.CommandText = Regex.Replace(text,
							string.Format(@"\[(\w*)\].\[(\w*)\]\s*LIKE\s*@{0}\s?(?:ESCAPE N?'~')", parameter.ParameterName),
							string.Format(@"contains([$1].[$2], @{0})", parameter.ParameterName));

						if (text == cmd.CommandText)
							throw new Exception("FTS was not replaced on: " + text);

						text = cmd.CommandText;
					}
				}
			}
		}
	}
}