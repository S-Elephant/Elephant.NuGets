using System.Linq.Expressions;
using Elephant.Database.Abstractions.DbContexts;
using Elephant.Database.Abstractions.Repositories;
using Elephant.Types.Interfaces;
using Elephant.Types.Results;
using Elephant.Types.Results.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Repositories
{
	/// <inheritdoc cref="IGenericCrudGuidRepository{TEntity}"/>
	public abstract class GenericCrudGuidRepository<TEntity, TContext> : GenericCrudRepository<TEntity, TContext>, IGenericCrudGuidRepository<TEntity>
		where TEntity : class, IGuid
		where TContext : IContext
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		protected GenericCrudGuidRepository(TContext databaseService)
			: base(databaseService)
		{
		}

		/// <inheritdoc/>
		public virtual async Task<TEntity?> ByIdAsync(Guid id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
		{
			return await includes
				.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include))
				.AsTracking(queryTrackingBehavior)
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<bool> HasIdAsync(Guid id, CancellationToken cancellationToken)
		{
			return await Table.AnyAsync(x => x.Id == id, cancellationToken);
		}

		/// <summary>
		/// Update and save.
		/// Checks if the <paramref name="obj"/> already exists and if not, returns a not-found result.
		/// </summary>
		public override async Task<IResult<int>> UpdateAndSaveAsync(TEntity obj, CancellationToken cancellationToken)
		{
			bool hasId = await HasIdAsync(obj.Id, cancellationToken);
			if (hasId)
				return await base.UpdateAndSaveAsync(obj, cancellationToken);
			return Result<int>.NotFound($"Entity with Id {obj.Id} was not found. No insert and no save were performed.");
		}
	}
}
