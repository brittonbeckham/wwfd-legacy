using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Wwfd.Core.Dto;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Core.Agents
{
	public class FounderAgent : AgentBase
	{
		protected override void CreateDtoMaps()
		{
			Mapper.CreateMap<Founder, FounderDto>()
				.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.FounderRoles));

			Mapper.CreateMap<FounderRoleType, FounderRoleDto>();
		}

		public int Save(FounderDto founder, IEnumerable<FounderRoleDto> roles)
		{
			var entity = MapToEntity<FounderDto, Founder>(founder);

			//attach the roles
			foreach (var role in roles)
				entity.FounderRoles.Add(MapToEntity<FounderRoleDto, FounderRoleType>(role));

			CurrentContext.Founders.Add(entity);
			CurrentContext.SaveChanges();

			return entity.FounderId;
		}

		public FounderDto GetById(int founderId)
		{
			var entity = CurrentContext.Founders.FirstOrDefault(r => r.FounderId == founderId);
			ErrorIfEntityIsNull(entity);

			return MapToDto<Founder, FounderDto>(entity);
		}

		/// <summary>
		/// Returns all Founders.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<FounderDto> GetAll()
		{
			return CurrentContext.Founders
					.Include(r=>r.FounderRoles)
					.ToArray()
					.Select(r => MapToDto<Founder, FounderDto>(r))
					.ToArray();

		}

		/// <summary>
		/// Returns Founders with names containing the given text.
		/// </summary>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <returns></returns>
		public IEnumerable<FounderDto> GetByName(string firstName, string lastName)
		{
			return CurrentContext.Founders
					.Include(r => r.FounderRoles)
					.Where(r =>
						(firstName != null && r.FirstName.Contains(firstName))
						|| (lastName != null && r.LastName.Contains(lastName)))
					.ToArray()
					.Select(r => MapToDto<Founder, FounderDto>(r))
					.ToArray();
		}

		public IEnumerable<FounderWithQuoteCountDto> GetWithQuoteCountByName(string firstName, string lastName)
		{
			IQueryable<FounderWithQuoteCountDto> x = CurrentContext.Founders
					.Where(r =>
						(firstName != null && r.FirstName.Contains(firstName))
						|| (lastName != null && r.LastName.Contains(lastName)))
					.Join(CurrentContext.Quotes, f => f.FounderId, q => q.FounderId, (f, q) => new
					{
						f,
						q
					})
					.GroupBy(@t => new
					{
						@t.q.FounderId,
						@t.f.FullName,
					}, @t => @t.q)
					.OrderBy(r => r.Key.FullName)
					.Select(grp1 => new FounderWithQuoteCountDto
					{
						FullName = grp1.Key.FullName,
						FounderId = grp1.Key.FounderId,
						QuoteCount = grp1.Count()
					});

			return x.ToArray();
		}

		public IEnumerable<FounderWithQuoteCountDto> GetWithQuoteCountById(int id)
		{
			IQueryable<FounderWithQuoteCountDto> x = CurrentContext.Founders
					.Where(r => r.FounderId == id)
					.Join(CurrentContext.Quotes, f => f.FounderId, q => q.FounderId, (f, q) => new
					{
						f,
						q
					})
					.GroupBy(@t => new
					{
						@t.q.FounderId,
						@t.f.FullName,
					}, @t => @t.q)
					.OrderBy(r => r.Key.FullName)
					.Select(grp1 => new FounderWithQuoteCountDto
					{
						FullName = grp1.Key.FullName,
						FounderId = grp1.Key.FounderId,
						QuoteCount = grp1.Count()
					});

			return x.ToArray();
		}

		public void Update(FounderDto founder, IEnumerable<FounderRoleDto> roles)
		{
			
		}
	}
}