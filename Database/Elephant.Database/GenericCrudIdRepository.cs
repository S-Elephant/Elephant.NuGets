﻿using System.Linq.Expressions;
using Elephant.Types.Interfaces;
using Elephant.Types.Interfaces.ResponseWrappers;
using Elephant.Types.ResponseWrappers;
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
		protected GenericCrudIdRepository(TContext databaseService)
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
		public async Task<bool> HasId(int id, CancellationToken cancellationToken)
		{
			return await Table.AnyAsync(x => x.Id == id, cancellationToken);
		}

		/// <inheritdoc/>
		public virtual async Task<int> HighestId(CancellationToken cancellationToken)
		{
			int highestId = await Table
				.AsNoTracking()
				.OrderByDescending(x => x.Id)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

			return highestId == 0 ? -1 : highestId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> LowestId(CancellationToken cancellationToken)
		{
			int lowestId = await Table
				.AsNoTracking()
				.OrderBy(x => x.Id)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

			return lowestId == 0 ? -1 : lowestId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> NextId(int sourceId, bool cycle, CancellationToken cancellationToken)
		{
			int nextId = await Table
				.OrderBy(x => x.Id)
				.Where(x => x.Id > sourceId)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

			// If none found then return the lowest id instead.
			if (nextId == 0)
			{
				if (cycle)
					return await LowestId(cancellationToken);

				return -1;
			}

			return nextId;
		}

		/// <inheritdoc/>
		public virtual async Task<int> PreviousId(int sourceId, bool cycle, CancellationToken cancellationToken)
		{
			int previousId = await Table
				.OrderByDescending(x => x.Id)
				.Where(x => x.Id < sourceId)
				.Select(x => x.Id)
				.FirstOrDefaultAsync(cancellationToken);

			// If none found then return the highest id instead.
			if (previousId == 0)
			{
				if (cycle)
					return await HighestId(cancellationToken);

				return -1;
			}

			return previousId;
		}

		/// <summary>
		/// Update and save.
		/// Checks if the <paramref name="obj"/> already exists and if not, returns a <see cref="ResponseWrapperNotFound{TData}"/>.
		/// </summary>
		public override async Task<IResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken)
		{
			bool hasId = await HasId(obj.Id, cancellationToken);
			if (hasId)
				return await base.UpdateAndSave(obj, cancellationToken);
			else
				return new ResponseWrapperNotFound<int>($"Entity with Id {obj.Id} was not found. No insert and no save were performed.");
		}
	}
}
