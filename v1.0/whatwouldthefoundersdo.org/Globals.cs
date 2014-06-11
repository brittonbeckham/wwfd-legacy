using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Wwfd.Data.CoreDataObjectsTableAdapters;
using BLB.Common;
using BLB.Common.Web;

namespace Wwfd.Web
{
	public enum SearchTypes
	{
		Quotes,
		Founders,
		Documents
	}

	public static class ExtensionMethods_DataTable
	{
		#region Select Distinct
		/// <summary>
		/// "SELECT DISTINCT" over a DataTable
		/// </summary>
		/// <param name="SourceTable">Input DataTable</param>
		/// <param name="FieldNames">Fields to select (distinct)</param>
		/// <returns></returns>
		public static DataTable SelectDistinct(this DataTable SourceTable, String FieldName)
		{
			return SelectDistinct(SourceTable, FieldName, String.Empty);
		}

		/// <summary>
		///"SELECT DISTINCT" over a DataTable
		/// </summary>
		/// <param name="SourceTable">Input DataTable</param>
		/// <param name="FieldNames">Fields to select (distinct)</param>
		/// <param name="Filter">Optional filter to be applied to the selection</param>
		/// <returns></returns>
		public static DataTable SelectDistinct(this DataTable SourceTable, String FieldNames, String Filter)
		{
			DataTable dt = new DataTable();
			String[] arrFieldNames = FieldNames.Replace(" ", "").Split(',');
			foreach (String s in arrFieldNames)
			{
				if (SourceTable.Columns.Contains(s))
					dt.Columns.Add(s, SourceTable.Columns[s].DataType);
				else
					throw new Exception(String.Format("The column {0} does not exist.", s));
			}

			Object[] LastValues = null;
			foreach (DataRow dr in SourceTable.Select(Filter, FieldNames))
			{
				Object[] NewValues = GetRowFields(dr, arrFieldNames);
				if (LastValues == null || !(ObjectComparison(LastValues, NewValues)))
				{
					LastValues = NewValues;
					dt.Rows.Add(LastValues);
				}
			}

			return dt;
		}
		#endregion

		#region Private Methods
		private static Object[] GetRowFields(DataRow dr, String[] arrFieldNames)
		{
			if (arrFieldNames.Length == 1)
				return new Object[] { dr[arrFieldNames[0]] };
			else
			{
				ArrayList itemArray = new ArrayList();
				foreach (String field in arrFieldNames)
					itemArray.Add(dr[field]);

				return itemArray.ToArray();
			}
		}

		/// <summary>
		/// Compares two values to see if they are equal. Also compares DBNULL.Value.
		/// </summary>
		/// <param name="A">Object A</param>
		/// <param name="B">Object B</param>
		/// <returns></returns>
		private static Boolean ObjectComparison(Object a, Object b)
		{
			if (a == DBNull.Value && b == DBNull.Value) //  both are DBNull.Value
				return true;
			if (a == DBNull.Value || b == DBNull.Value) //  only one is DBNull.Value
				return false;
			return (a.Equals(b));  // value type standard comparison
		}

		/// <summary>
		/// Compares two value arrays to see if they are equal. Also compares DBNULL.Value.
		/// </summary>
		/// <param name="A">Object Array A</param>
		/// <param name="B">Object Array B</param>
		/// <returns></returns>
		private static Boolean ObjectComparison(Object[] a, Object[] b)
		{
			Boolean retValue = true;
			Boolean singleCheck = false;

			if (a.Length == b.Length)
				for (Int32 i = 0; i < a.Length; i++)
				{
					if (!(singleCheck = ObjectComparison(a[i], b[i])))
					{
						retValue = false;
						break;
					}
					retValue = retValue && singleCheck;
				}

			return retValue;
		}
		#endregion
	}

	public static class Extensions
	{
		public static void ShowError(Exception ex, Label lblError)
		{
			lblError.Text = ex.Message;
			lblError.Visible = true;
		}

		public static string AddInlineSpan(this string text, string[] words)
		{
			foreach (string word in words)
			{
				//this regex fails for some reason
				//Regex reg = new Regex(string.Format("\b{0}\b", word));
				//foreach (Match match in reg.Matches(text))
				//{
				//    text = text.Insert(match.Index, @"<span class='SearchWord'>");
				//    //text = text.Insert(match.Index + match.Length, @"</span>");
				//}

				//check first word of text
				if (text.Contains(' '))
				{
					if (text.Substring(0, text.IndexOf(' ')) == word.ToLower())
					{
						text.Insert(text.IndexOf(' '), "</span>");
						text = "<span class='SearchWord'>" + text;
					}
				}
				else if (text.ToLower() == word.ToLower())
				{
					text.Replace(word, string.Format("<span class='SearchWord'>{0}</span>", word));
				}

				////check words with puncuation at end (.,;"?! etc) and a space prior
				foreach (string punct in Arrays.Punctuations)
					text = text.Replace(string.Format(" {0}{1}", word, punct), string.Format(" <span class='SearchWord'>{0}</span>{1}", word, punct));

				//all other words (leading and trailing spaces)
				text = text.Replace(string.Format(" {0} ", word), string.Format(" <span class='SearchWord'>{0}</span> ", word));
		   }

			return text;
		}
	}

}