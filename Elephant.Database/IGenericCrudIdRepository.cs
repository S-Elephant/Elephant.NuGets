using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    }
}
