using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wwfd.Core;
using Wwfd.Core.Agents;
using Wwfd.Data.CodeFirst.Context;

namespace CodeFirstTest
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			WwfdContext.Initialize();

			return;


			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ProgramTester.SeedQuotes.sql"))
			using (StreamReader reader = new StreamReader(stream))
			{

				StringBuilder quotes = new StringBuilder();
				StringBuilder references = new StringBuilder();
				do
				{
					var line = reader.ReadLine();
					var strings = line.Split(new[] {", N'"}, StringSplitOptions.RemoveEmptyEntries);

					Console.WriteLine(strings[2]);
					Console.WriteLine();

					//first string is quote
					quotes.AppendLine(line.Replace(", N'" + strings[2], ""));

					//second string is ref
					references.AppendLine(line.Replace(", N'" + strings[1], "").Replace(", N'" + strings[3], "") + "),");
					
				} while (!reader.EndOfStream);

				Console.WriteLine(quotes.ToString());
				Console.WriteLine(references.ToString());

				/*var writer = File.CreateText("D:\\Desktop\\quotes.sql");
				writer.Write(quotes.ToString());
				writer.Close();*/

				var writer = File.CreateText("D:\\Desktop\\references.sql");
				writer.Write(references.ToString());
				writer.Close();


				Console.ReadKey();
			}
		}
	}
}
