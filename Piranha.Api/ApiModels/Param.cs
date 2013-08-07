using System;

namespace Piranha.ApiModels
{
	/// <summary>
	/// Generic class for typed parameters.
	/// </summary>
	/// <typeparam name="T">The value type</typeparam>
	public sealed class Param<T> : ParamBase
	{
		/// <summary>
		/// Checks if the param has a value.
		/// </summary>
		public bool HasValue { get { return Value != null ; } }

		/// <summary>
		/// Gets/sets the param value.
		/// </summary>
		public T Value { get ; set ; }
	}
}
