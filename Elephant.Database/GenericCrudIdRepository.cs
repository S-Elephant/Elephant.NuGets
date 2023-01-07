using System.Linq.Expressions;
using Elephant.Database;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database
{
    /// <inheritdoc cref="IGenericCrudRepository{T}"/>
    public abstract class GenericCrudIdRepository<T, TContext> : GenericCrudRepository<T, TContext>, IGenericCrudIdRepository<T, TContext>
        where T : class, IId
        where TContext : IContext
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public GenericCrudIdRepository(TContext databaseService)
            : base(databaseService)
        {
        }

        /// <inheritdoc/>
        public async Task<T?> ById(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            return await includes
                .Aggregate(Table.AsQueryable(), (current, include) => current.Include(include))
                .AsTracking(queryTrackingBehavior)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
