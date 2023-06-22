using System.Linq.Expressions;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database
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
		Task<TEntity?> ById(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

		/// <summary>
		/// Return true if any record with <paramref name="id"/> exists.
		/// </summary>
		Task<bool> HasId(int id, CancellationToken cancellationToken);

		/// <summary>
		/// Return the highest id that exists in the table.
		/// Returns -1 if there are no records.
		/// </summary>
		Task<int> HighestId(CancellationToken cancellationToken);

		/// <summary>
		/// Return the highest id that exists in the table.
		/// Returns -1 if there are no records.
		/// </summary>
		Task<int> LowestId(CancellationToken cancellationToken);

		/// <summary>
		/// Return the next available id from the <paramref name="sourceId"/>.
		/// Returns -1 if there are no records.
		/// Returns -1 if <paramref name="sourceId"/> is the highest id and <paramref name="cycle"/> is false.
		/// Returns the lowest id if <paramref name="sourceId"/> is the highest id and <paramref name="cycle"/> is true and if there is at least 1 record.
		/// </summary>
		/// <param name="sourceId">The id to take the next id from.</param>
		/// <param name="cycle">If true, will return the lowest id if the <paramref name="sourceId"/> is the highest id.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		Task<int> NextId(int sourceId, bool cycle, CancellationToken cancellationToken);

		/// <summary>
		/// Return the previous available id from the <paramref name="sourceId"/>.
		/// Returns -1 if there are no records.
		/// Returns -1 if <paramref name="sourceId"/> is the lowest id and <paramref name="cycle"/> is false.
		/// Returns the highest id if <paramref name="sourceId"/> is the lowest id and <paramref name="cycle"/> is true and if there is at least 1 record.
		/// </summary>
		/// <param name="sourceId">The id to take the next id from.</param>
		/// <param name="cycle">If true, will return the highest id if the <paramref name="sourceId"/> is the lowest id.</param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
		Task<int> PreviousId(int sourceId, bool cycle, CancellationToken cancellationToken);
	}
}
