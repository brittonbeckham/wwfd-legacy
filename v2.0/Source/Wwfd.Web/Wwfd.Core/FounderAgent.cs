using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using StackExchange.Profiling;
using Wwfd.Core.Aggregates;
using Wwfd.Data;

namespace Wwfd.Core
{
	public class FounderAgent
	{
		public int AddFounder(string firstName, string middleName, string lastName, string suffix, string gender, DateTime? dateBorn, DateTime? dateDied)
		{
			using (WwfdEntities context = new WwfdEntities())
			{
				Founder f = new Founder()
				{
					FirstName = firstName,
					LastName = lastName,
					MiddleName = middleName,
					Suffix = suffix,
					Gender = gender,
					DateBorn = dateBorn,
					DateDied = dateDied
				};

				context.Founders.Add(f);
				context.SaveChanges();
				context.ChangeTracker.DetectChanges();
				return f.FounderID;
			}
		}

		public Founder GetFounder(int founderId)
		{
			using (WwfdEntities context = new WwfdEntities())
			{
				return context.Founders.FirstOrDefault(r => r.FounderID == founderId);
			}
		}

		public List<Founder> GetFounder(string firstName, string lastName)
		{
			using (WwfdEntities context = new WwfdEntities())
			{
				return context.Founders
					.Where(r =>
							(firstName != null && r.FirstName.Contains(firstName))
						|| (lastName != null && r.LastName.Contains(lastName)))
					.ToList();
			}
		}


		public List<FounderWithQuoteCount> GetFoundersWithQuoteCount(string searchString)
		{
			using (WwfdEntities context = new WwfdEntities())
			{
				var x = context.Founders
					.Where(r => searchString == null || r.FullName.Contains(searchString))
					.Join(context.Quotes, f => f.FounderID, q => q.FounderID, (f, q) => new { f, q })
					.GroupBy(@t => new
						{
							@t.q.FounderID,
							@t.f.FullName,
						}, @t => @t.q)
					.OrderBy(r => r.Key.FullName)
					.Select(grp1 => new FounderWithQuoteCount
						{
							FullName = grp1.Key.FullName,
							FounderId = grp1.Key.FounderID,
							QuoteCount = grp1.Count()
						});

				return x.ToList();

			}
		}
	}
}