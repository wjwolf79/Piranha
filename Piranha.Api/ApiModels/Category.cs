using System;
using System.Collections.Generic;

namespace Piranha.ApiModels
{
	public sealed class Category
	{
		public Guid? Id { get ; internal set ; }
		public string Name { get ; internal set ; }
		public string Permalink { get ; set ; }
		public string Description { get ; set ; }
		public DateTime? Created { get ; internal set ; }
		public DateTime? Updated { get ; internal set ; }
	}
}