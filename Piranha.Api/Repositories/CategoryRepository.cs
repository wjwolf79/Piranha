using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

namespace Piranha.Repositories
{
	/// <summary>
	/// Default implementation of the category repository.
	/// </summary>
	internal sealed class CategoryRepository : ICategoryRepository
	{
		#region Members
		private readonly Api uow ;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="api">The current unit of work</param>
		public CategoryRepository(Api api) {
			uow = api ;
		}

		/// <summary>
		/// Gets the models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The optional predicate</param>
		/// <returns>The matching models</returns>
		public IList<ApiModels.Category> Get(Expression<Func<Entities.Category, bool>> predicate = null) {
			var models = new List<ApiModels.Category>() ;

			// Create query
			var query = uow.Db.Categories.Include(c => c.Permalink) ;

			// Add predicate
			if (predicate != null)
				query = query.Where(predicate) ;

			// Get categories
			var categories = query.OrderBy(c => c.Name).ToList() ;

			// Get models
			foreach (var cat in categories) {
				models.Add(Mapper.Map<Entities.Category, ApiModels.Category>(cat)) ;
			}
			return models ;
		}

		/// <summary>
		/// Gets the model identified by the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model, null if it wasn't found</returns>
		public ApiModels.Category GetById(Guid id) {
			return Get(c => c.Id == id).SingleOrDefault() ;
		}

		/// <summary>
		/// Gets the model identified by the given unique permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model, null if it wasn't found</returns>
		public ApiModels.Category GetByPermalink(string permalink) {
			return Get(c => c.Permalink.Name == permalink).SingleOrDefault() ;
		}

		/// <summary>
		/// Adds a new model to the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		public void Add(ApiModels.Category model) {
			var category = Entities.Category.Create() ;
			model.Id = category.Id ;
			uow.Db.Categories.Add(category) ;

			Mapper.Map<ApiModels.Category, Entities.Category>(model, category) ;
			category.Permalink.Name = model.Permalink ;
		}

		/// <summary>
		/// Updates the given model in the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		public void Update(ApiModels.Category model) {
			if (model.Id.HasValue) {
				var category = uow.Db.Categories
					.Include(c => c.Permalink)
					.Where(c => c.Id == model.Id.Value).Single() ;

				Mapper.Map<ApiModels.Category, Entities.Category>(model, category) ;
				category.Permalink.Name = model.Permalink ;
			} else throw new ArgumentNullException("Model id not set to an instance of an object") ;
		}

		/// <summary>
		/// Removes the given model from the current unit of work.
		/// </summary>
		/// <param name="model"></param>
		public void Remove(ApiModels.Category model) {
			if (model.Id.HasValue) {
				var category = uow.Db.Categories
					.Include(c => c.Permalink)
					.Where(c => c.Id == model.Id.Value).Single() ;

				uow.Db.Permalinks.Remove(category.Permalink) ;
				uow.Db.Categories.Remove(category) ;
			} else throw new ArgumentNullException("Model id not set to an instance of an object") ;
		}
	}
}