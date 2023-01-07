using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Elephant.Database
{
    /// <summary>
    /// Generic CRUD repository base class that expects the entity to be of type IId.
    /// </summary>
    public interface IGenericCrudIdRepository<T, TContext> : IGenericCrudRepository<T>
        where T : class, IId
        where TContext : IContext
    {
        /// <summary>
        /// Retrieves <typeparamref name="T"/> by id.
        /// </summary>
        Task<T?> ById(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
    }
}
