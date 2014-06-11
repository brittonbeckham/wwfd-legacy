using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace Wwfd.Data
{
	public partial class WwfdEntities
	{
		public static WwfdEntities GetProfiledContext()
		{
			return null;
			/*var context = new WwfdEntities();
			//var conn = new StackExchange.Profiling.Data.ProfiledDbConnection(new SqlConnection(context.Database.Connection.ConnectionString), MiniProfiler.Current);
			var conn = new StackExchange.Profiling.Data.ProfiledDbConnection(new EntityConnection(ConfigurationManager.ConnectionStrings["WwfdEntities"].ConnectionString), MiniProfiler.Current); 
			return new WwfdEntities(conn); // resides in the MiniProfiler.EF nuget pack
*/
			/*var builder = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["WwfdEntities"].ConnectionString);
			var sqlConnection = new SqlConnection(builder.ProviderConnectionString);
			var profiledConnection = new EFProfiledDbConnection(sqlConnection, MiniProfiler.Current);

			return profiledConnection.CreateObjectContext<WwfdEntities>();*/
		}
	}
}
