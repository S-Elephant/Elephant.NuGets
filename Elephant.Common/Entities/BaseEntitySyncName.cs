using Elephant.Types.Interfaces;

namespace Elephant.Common.Entities
{
	/// <summary>
	/// Common entity properties for record synching, with a name.
	/// </summary>
	public abstract class BaseEntitySyncName : BaseEntitySync, IBaseEntitySyncName, IIdName
	{
		/// <summary>
		/// <inheritdoc cref="IName.Name" />
		/// </summary>
		public string Name { get; set; } = string.Empty;
	}
}
