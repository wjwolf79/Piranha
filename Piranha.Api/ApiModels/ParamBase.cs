using System;

namespace Piranha.ApiModels
{
	/// <summary>
	/// Base class for the parameter model.
	/// </summary>
	public abstract class ParamBase {
		/// <summary>
		/// Gets the param id.
		/// </summary>
		public Guid? Id { get ; internal set ; }

		/// <summary>
		/// Gets if the param is locked from deletion.
		/// </summary>
		public bool IsLocked { get ; internal set ; }

		/// <summary>
		/// Gets/sets the unique param name.
		/// </summary>
		public string Name { get ; set ; }

		/// <summary>
		/// Gets/sets the optional param description.
		/// </summary>
		public string Description { get ; set ; }
	}
}