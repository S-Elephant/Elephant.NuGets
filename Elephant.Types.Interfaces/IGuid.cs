using System;

namespace Elephant.Types.Interfaces
{
	/// <summary>
	/// Interface with a <see cref="Guid"/> property.
	/// </summary>
	public interface IGuid
	{
		/// <summary>
		/// Unique identifier as a <see cref="Guid"/>.
		/// </summary>
		Guid Id { get; set; }
	}
}
