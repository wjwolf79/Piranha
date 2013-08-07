using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Piranha.Repositories
{
	/// <summary>
	/// Interface defining the different methods available for the category repository.
	/// </summary>
	public interface ICategoryRepository : IGenericRepository<ApiModels.Category, Entities.Category>
	{
		/// <summary>
		/// Gets the model identified by the given unique permalink.
		/// </summary>
		/// <param name="permalink">The permalink</param>
		/// <returns>The model, null if it wasn't found</returns>
		ApiModels.Category GetByPermalink(string permalink) ;
	}
}
