using System.Linq.Expressions;
using Elephant.Database.Abstractions.DbContexts;
using Elephant.Database.Abstractions.Repositories;
using Elephant.Types.Interfaces;
using Elephant.Types.Results;
using Elephant.Types.Results.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Repositories
{
	/// <inheritdoc cref="IGenericCrudIdRepository{TEntity}"/>
	public abstract class GenericCrudIdRepository<TEntity, TContext> : GenericCrudRepository<TEntity, TContext>, IGenericCrudIdRepository<TEntity>
		where TEntity : class, IId
		where TContext : IContext
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		protected GenericCrudIdRepository(TContext databaseService)
			: base(databaseService)
		{
		}

		/// <inheritdoc/>
		public virtual async Task<TEntity?> ByIdAsync(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
		{
			return await includes
				.Aggregate(Table.AsQueryable(), (current, include) => current.Include(include))
				.AsTracking(queryTrackingBehavior)
				.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<bool> HasIdAsync(int id, CancellationToken cancellationToken)
		{
			return await Table.AnyAsync(x => x.Id == id, cancellationToken);
		}

		/// <inheritdoc/>
		public virtual async Task<int> HighestIdAsync(CancellationToken cancellationToken)
		{
			int highestId = await Table
				.Select(x => x.Id)
				.OrderByDescending(id => id)
				.FirstOrDefaultAsync(cancellationToken);

			return highestId == 0 ? -1 : highestId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> LowestIdAsync(CancellationToken cancellationToken)
		{
			int lowestId = await Table
				.Select(x => x.Id)
				.OrderBy(id => id)
				.FirstOrDefaultAsync(cancellationToken);

			return lowestId == 0 ? -1 : lowestId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> NextIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken)
		{
			int nextId = await Table
				.Where(x => x.Id > sourceId)
				.Select(x => x.Id)
				.OrderBy(id => id)
				.FirstOrDefaultAsync(cancellationToken);

			// If none found then return the lowest id instead.
			if (nextId == 0)
			{
				if (cycle)
					return await LowestIdAsync(cancellationToken);

				return -1;
			}

			return nextId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> DeleteOverflowingEntities(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
		{
			// No overflow.
			if (maxEntities <= 0)
				return 0;

			// Fast path for default ordering by Id: avoid materializing the whole table where possible.
			if (orderBy == null)
			{
				// Quick total count check.
				int totalCount = await Table.CountAsync(cancellationToken);
				if (totalCount <= maxEntities)
					return 0;

				// Pull only the ids that must be deleted (server-side Skip/Select) — minimal memory footprint.
				int[] idsToDelete = await Table
					.OrderBy(x => x.Id)
					.Skip(maxEntities)
					.Select(x => x.Id)
					.ToArrayAsync(cancellationToken);

				if (idsToDelete.Length == 0)
					return 0;

				// Delete via EF (loads entities) to remain provider-agnostic.
				List<TEntity> toDelete = await Table.Where(x => idsToDelete.Contains(x.Id)).ToListAsync(cancellationToken);
				if (toDelete.Count == 0)
					return 0;

				Table.RemoveRange(toDelete);
				_ = await SaveAsync(cancellationToken);
				return toDelete.Count;
			}

			// Fallback for custom ordering: must materialize the ordered id sequence to determine positions.
			IOrderedQueryable<TEntity> orderedQuery = orderBy(Table);

			// Only select ids to minimize memory footprint.
			List<int> orderedIds = await orderedQuery.Select(x => x.Id).ToListAsync(cancellationToken);

			if (orderedIds.Count <= maxEntities)
				return 0;

			int[] idsToDeleteCustom = orderedIds.Skip(maxEntities).ToArray();
			if (idsToDeleteCustom.Length == 0)
				return 0;

			List<TEntity> toDeleteCustom = await Table.Where(x => idsToDeleteCustom.Contains(x.Id)).ToListAsync(cancellationToken);
			if (toDeleteCustom.Count == 0)
				return 0;

			Table.RemoveRange(toDeleteCustom);
			_ = await SaveAsync(cancellationToken);
			return toDeleteCustom.Count;
		}

		/// <inheritdoc/>
		public virtual async Task<List<int>> OverflowingIds(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default)
		{
			// No overflow.
			if (maxEntities <= 0)
				return new List<int>();

			// Base query.
			IQueryable<TEntity> baseQuery = Table.AsQueryable().AsTracking(queryTrackingBehavior);

			// Fast path for default ordering by Id, avoid materializing the whole table if possible.
			if (orderBy == null)
			{
				// Quick total count check.
				int totalCount = await baseQuery.CountAsync(cancellationToken);
				if (totalCount <= maxEntities)
					return new List<int>();

				// Server-side Skip/Select to return only the overflowing ids.
				List<int> ids = await baseQuery
					.OrderBy(x => x.Id)
					.Skip(maxEntities)
					.Select(x => x.Id)
					.ToListAsync(cancellationToken);

				return ids;
			}

			// Fallback for custom ordering: we must materialize the ordered id sequence to determine positions.
			IOrderedQueryable<TEntity> orderedQuery = orderBy(baseQuery);

			// Select only ids to minimize memory footprint.
			List<int> orderedIds = await orderedQuery
				.Select(x => x.Id)
				.ToListAsync(cancellationToken);

			if (orderedIds.Count <= maxEntities)
				return new List<int>();

			return orderedIds.Skip(maxEntities).ToList();
		}

		/// <inheritdoc/>
		public virtual async Task<int> PreviousIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken)
		{
			int previousId = await Table
				.Where(x => x.Id < sourceId)
				.Select(x => x.Id)
				.OrderByDescending(id => id)
				.FirstOrDefaultAsync(cancellationToken);

			// If none found then return the highest id instead.
			if (previousId == 0)
			{
				if (cycle)
					return await HighestIdAsync(cancellationToken);

				return -1;
			}

			return previousId;
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
