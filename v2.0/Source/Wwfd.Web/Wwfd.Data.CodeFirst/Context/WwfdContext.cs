using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Reflection;
using Wwfd.Data.CodeFirst.Schemas.DailyQuote;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Data.CodeFirst.Context
{
	public class WwfdContext : DbContext
	{
		private static Assembly _assembly;

		private static Assembly Assembly
		{
			get { return _assembly ?? (_assembly = Assembly.GetExecutingAssembly()); }
		}

		//dbo
		public DbSet<Founder> Founders { get; set; }
		public DbSet<Quote> Quotes { get; set; }
		public DbSet<ContributorRoleType> ContributorRoleTypes { get; set; }
		public DbSet<FounderRoleType> FounderRoleTypes { get; set; }
		public DbSet<Contributor> Contributors { get; set; }
		public DbSet<QuoteHistory> QuoteHistories { get; set; }
		public DbSet<QuoteHistoryType> QuoteHistoryTypes { get; set; }
		public DbSet<QuoteReference> QuoteReferences { get; set; }
		public DbSet<QuoteReferenceStatusType> QuoteReferenceStatusTypes { get; set; }
		public DbSet<QuoteStatusType> QuoteStatus { get; set; }
		public DbSet<PerformedSearch> PerformedSerarches { get; set; }

		//DailyQuote
		public DbSet<DailyQuoteSubscriber> DailuQuoteSubscribers { get; set; }
		public DbSet<DailyQuote> DailyQuotes { get; set; }
		public DbSet<DailyQuoteProcess> DailyQuoteProcesses { get; set; }
		public DbSet<DailyQuoteProcessStatusType> DailyQuoteProcessesStatusTypes { get; set; }

		public static void Initialize(bool populateSeedData = true)
		{
			//delete existing db
			var context = new WwfdContext();
			context.Database.Delete();

			//create new db from scratch
			context.Database.Initialize(true);

			//seed the lookup tables
			context.CreateLookupForEnum<Enums.FounderRoleType>();
			context.CreateLookupForEnum<Enums.ContributorRoleType>();
			context.CreateLookupForEnum<Enums.QuoteHistoryType>();
			context.CreateLookupForEnum<Enums.QuoteStatusType>();
			context.CreateLookupForEnum<Enums.QuoteReferenceStatusType>();
			context.CreateLookupForEnum<Enums.DailyQuoteProcessStatusType>();

			//setup advanced stuff
			SetupExtendedOptions(context);

			//populate db with data if requested
			if (populateSeedData)
				PopulateSeedData(context);
		}

		private static void SetupExtendedOptions(WwfdContext context)
		{
			//full text searching
			context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SetupFullText.sql"));

			//triggers
			context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SetupTriggers.sql"));
		}

		private static void PopulateSeedData(WwfdContext context)
		{
			context.Database.ExecuteSqlCommand(GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SeedContributors.sql"));
			context.Database.ExecuteSqlCommand(GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SeedFounders.sql"));
			context.Database.ExecuteSqlCommand(GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SeedQuotes.sql"));
			context.Database.ExecuteSqlCommand(GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SeedQuoteHistories.sql"));
			context.Database.ExecuteSqlCommand(GetResourceSqlScript("Wwfd.Data.CodeFirst.Scripts.SeedQuoteReferences.sql"));
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//schemas
			/*
						modelBuilder.Entity<DailyQuoteSubscriber>().ToTable("DailyQuoteSubscriber", WwfdSchemas.DAILY_QUOTE);
						modelBuilder.Entity<DailyQuoteSubscriber>().ToTable("DailyQuote", WwfdSchemas.DAILY_QUOTE);
						modelBuilder.Entity<DailyQuoteSubscriber>().ToTable("DailyQuoteProcess", WwfdSchemas.DAILY_QUOTE);
						modelBuilder.Entity<DailyQuoteSubscriber>().ToTable("DailyQuoteProcessStatus", WwfdSchemas.DAILY_QUOTE);
			*/

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

/*
			modelBuilder.Entity<QuoteReference>()
				.HasRequired(m => m.Contributor)
				.WithRequiredDependent()
				.Map(x => x.MapKey("ContributorId"))
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<QuoteReference>()
				.HasOptional(e => e.Verifier)
				.WithOptionalDependent()
				.Map(x => x.MapKey("VerifierId"))
				.WillCascadeOnDelete(false);
*/

			//create many-to-many relationship with contributor roles
			modelBuilder.Entity<Contributor>()
				.HasMany(x => x.ContributorRoles)
				.WithMany(x => x.Contributors)
				.Map(x =>
				{
					x.ToTable("ContributorRole");
					x.MapLeftKey("ContributorId");
					x.MapRightKey("ContributorRoleTypeId");
				});

			//create many-to-many relationship with founder roles
			modelBuilder.Entity<Founder>()
				.HasMany(x => x.FounderRoles)
				.WithMany(x => x.Founders)
				.Map(x =>
				{
					x.ToTable("FounderRole");
					x.MapLeftKey("FounderId");
					x.MapRightKey("FounderRoleTypeId");
				});

			//this ensures that full text searching will be enabled for certain fields
			DbInterception.Add(new FullTextSearchInterceptor());
		}

		private static string GetResourceSqlScript(string resourcePath)
		{
			using (Stream stream = Assembly.GetManifestResourceStream(resourcePath))
			using (StreamReader reader = new StreamReader(stream))
				return reader.ReadToEnd();
		}

	}
}
