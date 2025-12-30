using Elephant.Types;

namespace Elephant.Common.Entities
{
	/// <summary>
	/// Common entity properties for record synching.
	/// </summary>
	public abstract class BaseEntitySync : BaseId, IBaseEntitySync
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public DateTime LastModifiedOn { get; set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public int? LastModifiedBy { get; set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public int? CreatedBy { get; set; }
	}
}
