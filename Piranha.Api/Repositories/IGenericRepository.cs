using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Piranha.Repositories
{
	/// <summary>
	/// Generic interface defining the basic repository methods available.
	/// </summary>
	/// <typeparam name="TModel">The main repository model</typeparam>
	/// <typeparam name="TEntity">The underlying domain entity</typeparam>
	public interface IGenericRepository<TModel, TEntity>
	{
		/// <summary>
		/// Gets the models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The optional predicate</param>
		/// <returns>The matching models</returns>
		IList<TModel> Get(Expression<Func<TEntity, bool>> predicate = null) ;

		/// <summary>
		/// Gets the model identified by the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model, null if it wasn't found</returns>
		TModel GetById(Guid id) ;

		/// <summary>
		/// Adds a new model to the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		void Add(TModel model) ;

		/// <summary>
		/// Updates the given model in the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		void Update(TModel model) ;

		/// <summary>
		/// Removes the given model from the current unit of work.
		/// </summary>
		/// <param name="model"></param>
		void Remove(TModel model) ;
	}
}
