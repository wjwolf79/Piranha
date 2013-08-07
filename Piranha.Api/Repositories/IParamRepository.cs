using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Piranha.Repositories
{
	public interface IParamRepository
	{
		/// <summary>
		/// Gets the models matching the given predicate.
		/// </summary>
		/// <param name="predicate">The optional predicate</param>
		/// <returns>The matching models</returns>
		IList<ApiModels.Param<string>> Get(Expression<Func<Entities.Param, bool>> predicate = null) ;

		/// <summary>
		/// Gets the model identified by the given unique id.
		/// </summary>
		/// <param name="id">The unique id</param>
		/// <returns>The model, null if it wasn't found</returns>
		ApiModels.Param<T> GetById<T>(Guid id) ;

		/// <summary>
		/// Gets the model identified by the given name.
		/// </summary>
		/// <param name="name">The name</param>
		/// <returns>The model, null if it wasn't found</returns>
		ApiModels.Param<T> GetByName<T>(string name) ;

		/// <summary>
		/// Adds a new model to the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		void Add<T>(ApiModels.Param<T> model) ;

		/// <summary>
		/// Updates the given model in the current unit of work.
		/// </summary>
		/// <param name="model">The model</param>
		void Update<T>(ApiModels.Param<T> model) ;

		/// <summary>
		/// Inserts the model with the given value if it doesn't exist.
		/// </summary>
		/// <param name="name">The name</param>
		/// <param name="value">The value</param>
		void Ensure(string name, object value) ;

		/// <summary>
		/// Removes the given model from the current unit of work.
		/// </summary>
		/// <param name="model"></param>
		void Remove<T>(ApiModels.Param<T> model) ;
	}
}
