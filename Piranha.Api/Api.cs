using System;

using AutoMapper;

namespace Piranha
{
	public sealed class Api : IDisposable
	{
		private static bool Initialized = false ;
		private static object mutex = new object() ;
		internal readonly DataContext Db ;
		public readonly Repositories.ICategoryRepository Categories ;
		public readonly Repositories.IParamRepository Params ;

		public Api() {
			if (!Initialized) {
				lock (mutex) {
					if (!Initialized)
						Init() ;
				}
			}

			Db = new DataContext() ;

			Categories = new Repositories.CategoryRepository(this) ;
			Params = new Repositories.ParamRepository(this) ;
		}

		public int SaveChanges() {
			return Db.SaveChanges() ;
		}

		public void Dispose() {
			Db.Dispose() ;
			GC.SuppressFinalize(this) ;
		}

		private static void Init() {
			Mapper.CreateMap<Entities.Category, ApiModels.Category>()
				.ForMember(mc => mc.Permalink, o => o.MapFrom(ec => ec.Permalink.Name)) ;
			Mapper.CreateMap<Entities.Param, ApiModels.ParamBase>() ;

			Mapper.CreateMap<ApiModels.Category, Entities.Category>()
				.ForMember(ec => ec.Created, o => o.Ignore())
				.ForMember(ec => ec.CreatedBy, o => o.Ignore())
				.ForMember(ec => ec.CreatedById, o => o.Ignore())
				.ForMember(ec => ec.Extensions, o => o.Ignore())
				.ForMember(ec => ec.Id, o => o.Ignore())
				.ForMember(ec => ec.Parent, o => o.Ignore())
				.ForMember(ec => ec.ParentId, o => o.Ignore())
				.ForMember(ec => ec.Permalink, o => o.Ignore())
				.ForMember(ec => ec.PermalinkId, o => o.Ignore())
				.ForMember(ec => ec.Updated, o => o.Ignore())
				.ForMember(ec => ec.UpdatedBy, o => o.Ignore())
				.ForMember(ec => ec.UpdatedById, o => o.Ignore()) ;
			Mapper.CreateMap<ApiModels.ParamBase, Entities.Param>()
				.ForMember(ep => ep.Created, o => o.Ignore())
				.ForMember(ep => ep.CreatedBy, o => o.Ignore())
				.ForMember(ep => ep.CreatedById, o => o.Ignore())
				.ForMember(ep => ep.Id, o => o.Ignore())
				.ForMember(ep => ep.Updated, o => o.Ignore())
				.ForMember(ep => ep.UpdatedBy, o => o.Ignore())
				.ForMember(ep => ep.UpdatedById, o => o.Ignore())
				.ForMember(ep => ep.Value, o => o.Ignore()) ;

			Mapper.AssertConfigurationIsValid() ;

			Initialized = true ;
		}
	}
}