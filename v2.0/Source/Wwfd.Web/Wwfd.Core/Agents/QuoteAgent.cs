using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using AutoMapper;
using Wwfd.Core.Dto;
using Wwfd.Data.CodeFirst.Context;
using Wwfd.Data.CodeFirst.Schemas.dbo;

namespace Wwfd.Core.Agents
{
	public class QuoteAgent : AgentBase
	{
		protected override void CreateDtoMaps()
		{
			Mapper.CreateMap<Founder, FounderDto>();
			Mapper.CreateMap<QuoteReference, QuoteReferenceDto>();

			Mapper.CreateMap<Quote, QuoteDto>()
				.ForMember(dest => dest.Founder, opt => opt.MapFrom(src => src.Founder))
				.ForMember(dest => dest.QuoteStatus, opt => opt.MapFrom(src => src.QuoteStatusType.Name))
				.ForMember(dest => dest.References, opt => opt.MapFrom(src => src.QuoteReferences));

		}

		/// <summary>
		/// Searches the quote text and keywords for the given string using SQL full-text searching.
		/// </summary>
		/// <param name="searchText"></param>
		/// <param name="searchKeyword"></param>
		/// <returns></returns>
		public IEnumerable<QuoteDto> Search(string searchText, string searchKeyword, bool recordSearch = true)
		{
			bool error = false;
			try
			{
				BeginContextTrans();

				if(recordSearch)
				{
					//always save a record of each search performed for analytics purposes
					CurrentContext.PerformedSerarches.Add(new PerformedSearch()
					{
						DateSearched = DateTime.Now,
						TextSearchString = searchText,
						KeywordSearchString = searchKeyword,
					});

					CurrentContext.SaveChanges();
				}
				
				if (searchText != null)
					searchText = FullTextSearchInterceptor.AsFullTextSearch(searchText);

				if (searchKeyword != null)
					searchKeyword = FullTextSearchInterceptor.AsFullTextSearch(searchKeyword);

				return CurrentContext.Quotes
					.Include(r => r.QuoteReferences)
					.Include(r => r.Founder)
					.Where(r =>
						r.QuoteText.Contains(searchText)
						|| r.Keywords.Contains(searchKeyword))
					.ToArray()
					.Select(r => MapToDto<Quote, QuoteDto>(r))
					.ToArray();
			}
			catch
			{
				error = true;
				RollBackContextTrans();
				throw;
			}
			finally
			{
				if(!error)
					CommitContextTrans();
			}
		}

		public QuoteDto GetById(int quoteId)
		{
			var entity = CurrentContext.Quotes
							.Include(r => r.QuoteReferences)
							.Include(r => r.Founder)
							.First(r => r.QuoteId == quoteId);

			ErrorIfEntityIsNull(entity);

			return MapToDto<Quote, QuoteDto>(entity, false);
		}


		public IEnumerable<QuoteDto> GetByFounderId(int founderId)
		{
			return CurrentContext.Quotes
					.Include(r => r.QuoteReferences)
					.Include(r => r.Founder)
					.Where(r => r.FounderId == founderId)
					.ToArray()
					.Select(r => MapToDto<Quote, QuoteDto>(r))
					.ToArray();

		}
	}
}