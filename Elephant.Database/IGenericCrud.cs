using Elephant.Types.ResponseWrappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Elephant.Database
{
    /// <summary>
    /// Generic CRUD repository base class that also includes retrieving an entity by id.
    /// </summary>
    public interface IGenericCrudRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all.
        /// </summary>
        Task<List<TEntity>> All(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieve all records with support for <see cref="QueryTrackingBehavior"/> and includes.
        /// </summary>
        /// <example>await myRepository.All(1, QueryTrackingBehavior.NoTracking, CancellationToken.None, x => x.CustomersCrossOrders, x => x.CustomersCrossAddresses)</example>
        Task<List<TEntity>> All(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get by id.
        /// </summary>
        Task<TEntity?> ById(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Delete by id.
        /// </summary>
        /// <returns>True if found and deleted (note: doesn't save).</returns>
        bool Delete(object id);

        /// <summary>
        /// Delete by id and save.
        /// </summary>
        Task<ResponseWrapper<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

        /// <summary>
        /// Insert.
        /// </summary>
        Task Insert(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Insert one or more.
        /// </summary>
        Task Insert(ICollection<TEntity> objects, CancellationToken cancellationToken);

        /// <summary>
        /// Insert and save.
        /// </summary>
        Task<ResponseWrapper<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
        /// Save.
        /// </summary>
        Task<ResponseWrapper<int>> Save(CancellationToken cancellationToken);

        /// <summary>
        /// Update.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="obj"/> wasn't found.</exception>
        void Update(TEntity obj);

        /// <summary>
        /// Update one or more.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if any <paramref name="objects"/> wasn't found.</exception>
        void Update(ICollection<TEntity> objects);

        /// <summary>
        /// UpdateAndSave
        /// </summary>
        Task<ResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);

        /// <summary>
		/// Delete all rows from the table, resets the auto-increment and saves.
		/// </summary>
		/// <remarks>Works only on relational databases. Does NOT work on an in-memory database.</remarks>
		Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo");
    }
}
