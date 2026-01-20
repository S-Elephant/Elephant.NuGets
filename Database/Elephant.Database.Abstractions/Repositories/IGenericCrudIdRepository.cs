using System.Linq.Expressions;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Abstractions.Repositories
{
	/// <summary>
	/// Generic CRUD repository base class that expects the entity to be of type IId.
	/// </summary>
	public interface IGenericCrudIdRepository<TEntity> : IGenericCrudRepository<TEntity>
		where TEntity : class, IId
	{
		/// <summary>
		/// Retrieves <typeparamref name="TEntity"/> by id.
		/// </summary>
		Task<TEntity?> ByIdAsync(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

		/// <summary>
		/// Orders by <paramref name="orderBy"/> and then deletes all overflowing entities above <paramref name="maxEntities"/>.
		/// </summary>
		/// <param name="maxEntities">Maximum number of entities to keep. Entities beyond this count (after ordering) will be deleted.</param>
		/// <param name="orderBy">Optional: ordering to apply before selecting overflowing entities. If null a default ordering (by Id ascending) is used.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>Entities deleted count.</returns>
		Task<int> DeleteOverflowingEntities(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Return true if any record with <paramref name="id"/> exists.
		/// </summary>
		Task<bool> HasIdAsync(int id, CancellationToken cancellationToken);

		/// <summary>
		/// Return the highest id that exists in the table.
		/// Returns -1 if there are no records.
		/// </summary>
		Task<int> HighestIdAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Return the lowest id that exists in the table.
		/// Returns -1 if there are no records.
		/// </summary>
		Task<int> LowestIdAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Return the next available id from the <paramref name="sourceId"/>.
		/// Returns -1 if there are no records.
		/// Returns -1 if <paramref name="sourceId"/> is the highest id and <paramref name="cycle"/> is false.
		/// Returns the lowest id if <paramref name="sourceId"/> is the highest id and <paramref name="cycle"/> is true and if there is at least 1 record.
		/// </summary>
		/// <param name="sourceId">The id to take the next id from.</param>
		/// <param name="cycle">If true, will return the lowest id if the <paramref name="sourceId"/> is the highest id.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		Task<int> NextIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken);

		/// <summary>
		/// Orders by <paramref name="orderBy"/> and then returns all overflowing entity id's above <paramref name="maxEntities"/>.
		/// </summary>
		/// <param name="maxEntities">Maximum number of entities to keep. Entities beyond this count (after ordering) will be deleted.</param>
		/// <param name="orderBy">Optional: ordering to apply before selecting overflowing entities. If null a default ordering (by Id ascending) is used.</param>
		/// <param name="queryTrackingBehavior"><see cref="QueryTrackingBehavior"/>.</param>
		/// <param name="cancellationToken">Cancellation token.</param>
		/// <returns>All overflowing id's or an empty list if none are overflowing.</returns>
		Task<List<int>> OverflowingIds(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default);

		/// <summary>
		/// Return the previous available id from the <paramref name="sourceId"/>.
		/// Returns -1 if there are no records.
		/// Returns -1 if <paramref name="sourceId"/> is the lowest id and <paramref name="cycle"/> is false.
		/// Returns the highest id if <paramref name="sourceId"/> is the lowest id and <paramref name="cycle"/> is true and if there is at least 1 record.
		/// </summary>
		/// <param name="sourceId">The id to take the next id from.</param>
		/// <param name="cycle">If true, will return the highest id if the <paramref name="sourceId"/> is the lowest id.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		Task<int> PreviousIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken);
	}
}
