namespace Elephant.Common
{
    /// <summary>
    /// Base class with an <see cref="Id"/> property.
    /// </summary>
    public abstract class BaseId : IId
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public int Id { get; set; }
	}

	/// <summary>
	/// Comparer for <see cref="BaseId"/>.
	/// </summary>
	/// <example>Distinct example on a table called "BaseIds".
	/// var result = await Context.BaseIds.Distinct(new IdComparer()).ToListAsync(cancellationToken);</example>
	public class IdComparer : IEqualityComparer<BaseId>
	{
		/// <inheritdoc cref="IEqualityComparer{T}.Equals(T, T)"/>
		public bool Equals(BaseId? x, BaseId? y)
		{
			if (x == null || y == null)
				return x == null && y == null;

			return x.Id.Equals(y.Id);
		}

		/// <inheritdoc cref="IEqualityComparer{T}.GetHashCode(T)"/>
		public int GetHashCode(BaseId obj)
		{
			return obj.Id.GetHashCode();
		}
	}
}
