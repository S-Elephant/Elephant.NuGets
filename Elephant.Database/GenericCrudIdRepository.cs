using System.Linq.Expressions;
using Elephant.Database;
using Elephant.Types.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database
{
    /// <inheritdoc cref="IGenericCrudIdRepository{TEntity}"/>
    public abstract class GenericCrudIdRepository<TEntity, TContext> : GenericCrudRepository<TEntity, TContext>, IGenericCrudIdRepository<TEntity>
        where TEntity : class, IId
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
        public virtual async Task<TEntity?> ById(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
                .Aggregate(Table.AsQueryable(), (current, include) => current.Include(include))
                .AsTracking(queryTrackingBehavior)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> HighestId(CancellationToken cancellationToken)
        {
            int? highestId = await Table
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return highestId ?? -1;
        }

        /// <inheritdoc/>
        public virtual async Task<int> LowestId(CancellationToken cancellationToken)
        {
            int? highestId = await Table
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            return highestId ?? -1;
        }
    }
}
