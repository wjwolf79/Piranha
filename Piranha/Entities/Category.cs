using System;
using System.Collections.Generic;

namespace Piranha.Entities
{
	/// <summary>
	/// The category entity. Categories can be attached to any entity.
	/// </summary>
	[Serializable]
	public class Category : StandardEntity<Category>
	{
		#region Properties
		/// <summary>
		/// Gets/sets the optional id of the parent category.
		/// </summary>
		public Guid? ParentId { get ; set ; }

		/// <summary>
		/// Gets/sets the id of the permalink.
		/// </summary>
		public Guid PermalinkId { get ; set ; }

		/// <summary>
		/// Gets/sets the name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the description.
		/// </summary>
		public string Description { get ; set ; }
		#endregion

		#region Navigation properties
		/// <summary>
		/// Gets/sets the optional parent category.
		/// </summary>
		public Category Parent { get ; set ; }

		/// <summary>
		/// Gets/sets the permalink used to access the category.
		/// </summary>
		public Permalink Permalink { get ; set ; }

		/// <summary>
		/// Gets/sets the currently available extensions.
		/// </summary>
		public IList<Extension> Extensions { get ; set ; }
		#endregion

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Category() {
			Extensions = new List<Extension>() ;
		}

		/// <summary>
		/// Creates a new linked category.
		/// </summary>
		/// <returns>The category</returns>
		public static Category Create() {
			var c = new Category() { 
				Id = Guid.NewGuid() 
			} ;
			c.Permalink = new Permalink() { 
				Id = Guid.NewGuid(),
				Type = "CATEGORY"
			} ;
			c.PermalinkId = c.Permalink.Id ;

			return c ;
		}
	}
}
