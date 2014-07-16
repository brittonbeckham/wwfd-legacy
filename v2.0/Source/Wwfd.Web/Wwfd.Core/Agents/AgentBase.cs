using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using AutoMapper;
using Wwfd.Core.Exceptions;
using Wwfd.Core.Framework;
using Wwfd.Data.CodeFirst.Context;
using System.Linq.Dynamic;

namespace Wwfd.Core.Agents
{
	public abstract class AgentBase : IDisposable
	{
		private WwfdContext _context;

		protected AgentBase()
		{
			CreateDtoMaps();
		}

		/// <summary>
		///     True if the the underlying context is currently within a transaction. Note that this is not related to the given
		///     Agent class instance.
		/// </summary>
		public bool IsContextInTransaction
		{
			get { return HttpContext.Current.Items["_dbContextTransaction"] != null; }
		}

		/// <summary>
		/// Retrieves the current data context. This object is stored in the HttpContext for hosted environments
		/// item collection during calls to minimize memory usage and the number of connections to the data store.
		/// </summary>
		protected WwfdContext CurrentContext
		{
			get
			{
				//are we running this code from IIS?
				if (HostingEnvironment.IsHosted)
				{
					//look for a context item already generated for this request; we store it in the 
					//request so as to reduce the potential costs of it being re-instaniated multiple 
					//times within the same requuest
					if (HttpContext.Current.Items["_dbContext"] == null)
						HttpContext.Current.Items.Add("_dbContext", new WwfdContext());

					return (WwfdContext) HttpContext.Current.Items["_dbContext"];
				}

				return _context ?? (_context = new WwfdContext());
			}
		}

		#region IDisposable Members

		public virtual void Dispose()
		{
			//this function currently does nothing but is left here incase code is added that needs disposing		
		}

		#endregion

		/// <summary>
		/// When overridden in a derived class, contains the AutoMapper code that maps the entity framework class to the Dto
		/// class.
		/// </summary>
		protected abstract void CreateDtoMaps();

		/// <summary>
		/// Creates a new context transaction. This transaction spans all agents, whether instantiated previous to or after
		/// this call.
		/// </summary>
		public void BeginContextTrans()
		{
			//add transaction to the request items
			if (HttpContext.Current.Items["_dbContextTransaction"] != null)
				throw new TransactionAlreadyInProcessException();

			HttpContext.Current.Items.Add("_dbContextTransaction", CurrentContext.Database.BeginTransaction(IsolationLevel.Serializable));
		}

		/// <summary>
		///     Commits the context transaction for all actions performed from any agents since BeginContextTrans() was called.
		/// </summary>
		public void CommitContextTrans()
		{
			if (HttpContext.Current.Items["_dbContextTransaction"] == null)
				throw new TransactionNotStartedException();

			var trans = (DbContextTransaction)HttpContext.Current.Items["_dbContextTransaction"];
			trans.Commit();
			trans.Dispose();
			HttpContext.Current.Items.Remove("_dbContextTransaction");
		}

		/// <summary>
		///     Rolls back the context transaction for all actions performed from any agents since BeginContextTrans() was called.
		/// </summary>
		public void RollBackContextTrans()
		{
			if (HttpContext.Current.Items["_dbContextTransaction"] == null)
				throw new TransactionNotStartedException();

			var trans = (DbContextTransaction)HttpContext.Current.Items["_dbContextTransaction"];
			trans.Rollback();
			trans.Dispose();
			HttpContext.Current.Items.Remove("_dbContextTransaction");
		}

		/// <summary>
		///     Generically sorts a query based on the supplied parameters.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="queryParams"></param>
		/// <returns></returns>
		protected IQueryable<T> SortQuery<T>(IQueryable<T> query, PaginatedSortableQuery queryParams)
		{
			//if no sorting colum is provided, disregard
			if (string.IsNullOrEmpty(queryParams.SortColumn))
				return query;

			return query.OrderBy(string.Format("{0} {1}", queryParams.SortColumn, queryParams.SortOrder.ToString().ToLower()));
		}

		/// <summary>
		///     Creates a map and populates the a new EF object from the given Dto object.
		/// </summary>
		/// <typeparam name="TDtoObject"></typeparam>
		/// <typeparam name="TEfObject"></typeparam>
		/// <param name="dto"></param>
		/// <returns></returns>
		protected TEfObject MapToEntity<TDtoObject, TEfObject>(TDtoObject dto)
		{
			Mapper.CreateMap<TDtoObject, TEfObject>();
			return Mapper.Map<TEfObject>(dto);
		}

		/// <summary>
		///     Creates a map and populates the given EntityFramework object from the given Dto object.
		/// </summary>
		/// <typeparam name="TDtoObject"></typeparam>
		/// <typeparam name="TEfObject"></typeparam>
		/// <param name="dto"></param>
		/// <param name="existing"></param>
		/// <returns></returns>
		protected TEfObject MapToExistingEntity<TDtoObject, TEfObject>(TDtoObject dto, TEfObject existing)
		{
			Mapper.CreateMap<TDtoObject, TEfObject>();
			return Mapper.Map(dto, existing);
		}

		/// <summary>
		///     Creates a map and populates the given Dto object from the given EntityFramework object.
		/// </summary>
		/// <typeparam name="TEfObject"></typeparam>
		/// <typeparam name="TDtoObject"></typeparam>
		/// <param name="efObject"></param>
		/// <param name="autoCreateMap"></param>
		/// <returns></returns>
		protected TDtoObject MapToDto<TEfObject, TDtoObject>(TEfObject efObject, bool autoCreateMap = true)
		{
			if (autoCreateMap)
				Mapper.CreateMap<TEfObject, TDtoObject>();

			return Mapper.Map<TDtoObject>(efObject);
		}

		/// <summary>
		///     Checks if the given object is null, if it is, throws a new EntityNotFoundException with a respective message for
		///     the given type; otherwise, does nothing.
		/// </summary>
		/// <param name="entity"></param>
		protected void ErrorIfEntityIsNull(object entity)
		{
			if (entity == null)
				throw new EntityNotFoundException(entity.GetType().ToString());
		}
	}
}