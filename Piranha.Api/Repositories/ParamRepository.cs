using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using AutoMapper;

namespace Piranha.Repositories
{
	public class ParamRepository : IParamRepository
	{
		#region Members
		private readonly Api uow ;
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="api">The current unit of work</param>
		public ParamRepository(Api api) {
			uow = api ;
		}

		/// <summary>
		/// Gets the models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The optional predicate</param>
		/// <returns>The matching models</returns>
		public IList<ApiModels.Param<string>> Get(Expression<Func<Entities.Param, bool>> predicate = null) {
			return Get<string>(predicate) ;
		}

	
		/// <summary>
		/// Gets the models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The optional predicate</param>
		/// <returns>The matching models</returns>
		private IList<ApiModels.Param<T>> Get<T>(Expression<Func<Entities.Param, bool>> predicate = null) {
			var models = new List<ApiModels.Param<T>>() ;

			// Create query
			var query = uow.Db.Params.AsQueryable() ;

			// Add predicate
			if (predicate != null)
				query = query.Where(predicate) ;

			// Get categories
			var parameters = query.OrderBy(c => c.Name).ToList() ;

			// Get models
			foreach (var p in parameters) {
				var param = Activator.CreateInstance<ApiModels.Param<T>>() ;
				Mapper.Map<Entities.Param, ApiModels.ParamBase>(p, param) ;
				param.Value = (T)Convert.ChangeType(p.Value, typeof(T)) ;
				models.Add(param) ;
			}
			return models ;
		}

		/// <summary>
		/// Gets the model identified by the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model, null if it wasn't found</returns>
		public ApiModels.Param<T> GetById<T>(Guid id) {
			return Get<T>(p => p.Id == id).SingleOrDefault() ;
		}

		/// <summary>
		/// Gets the model identified by the given name.
		/// </summary>
		/// <param name="name">The name</param>
		/// <returns>The model, null if it wasn't found</returns>
		public ApiModels.Param<T> GetByName<T>(string name) {
			return Get<T>(p => p.Name == name.ToUpper()).SingleOrDefault() ;
		}

		/// <summary>
		/// Adds a new model to the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		public void Add<T>(ApiModels.Param<T> model) {
			var param = Entities.Param.Create() ;
			model.Id = param.Id ;
			uow.Db.Params.Add(param) ;

			Mapper.Map<ApiModels.ParamBase, Entities.Param>(model, param) ;
			param.Value = model.Value.ToString() ;
		}

		/// <summary>
		/// Updates the given model in the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		public void Update<T>(ApiModels.Param<T> model) {
			if (model.Id.HasValue) {
				var param = uow.Db.Params.Where(p => p.Id == model.Id.Value).Single() ;

				Mapper.Map<ApiModels.ParamBase, Entities.Param>(model, param) ;
				param.Value = model.Value.ToString() ;
			} else throw new ArgumentNullException("Model id not set to an instance of an object") ;
		}

		/// <summary>
		/// Inserts the model with the given value if it doesn't exist.
		/// </summary>
		/// <param name="name">The name</param>
		/// <param name="value">The value</param>
		public void Ensure(string name, object value) {
			var param = GetByName<string>(name) ;
			if (param == null) {
				param = new ApiModels.Param<string>() {
					Name = name,
					Value = value.ToString()
				} ;
				Add(param) ;
			}
		}

		/// <summary>
		/// Removes the given model from the current unit of work.
		/// </summary>
		/// <param name="model"></param>
		public void Remove<T>(ApiModels.Param<T> model) {
			if (model.Id.HasValue) {
				var param = uow.Db.Params.Where(p => p.Id == model.Id.Value).Single() ;

				if (!param.IsLocked)
					uow.Db.Params.Remove(param) ;
			} else throw new ArgumentNullException("Model id not set to an instance of an object") ;
		}
	}
}