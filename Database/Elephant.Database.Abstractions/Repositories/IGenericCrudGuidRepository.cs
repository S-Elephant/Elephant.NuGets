using System.Linq.Expressions;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Abstractions.Repositories
{
	/// <summary>
	/// Generic CRUD repository base class that expects the entity to be of type IId.
	/// </summary>
	public interface IGenericCrudGuidRepository<TEntity> : IGenericCrudRepository<TEntity>
		where TEntity : class, IGuid
	{
		/// <summary>
		/// Retrieves <typeparamref name="TEntity"/> by id.
		/// </summary>
		Task<TEntity?> ByIdAsync(Guid id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

		/// <summary>
		/// Return true if any record with <paramref name="id"/> exists.
		/// </summary>
		Task<bool> HasIdAsync(Guid id, CancellationToken cancellationToken);
	}
}
