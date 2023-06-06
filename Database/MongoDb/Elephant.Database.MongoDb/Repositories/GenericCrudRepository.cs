using System.Linq.Expressions;
using Elephant.Database.MongoDb.Abstractions.DbSets;
using Elephant.Database.MongoDb.Abstractions.Repositories;
using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Repositories
{
	/// <inheritdoc cref="IGenericCrudRepository{TEntity}"/>
	public class GenericCrudRepository<TEntity> : IGenericCrudRepository<TEntity>
		where TEntity : IId
	{
		/// <summary>
		/// Collection/DbSet of your entity.
		/// </summary>
		public IDbSet<TEntity> DbSet { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public GenericCrudRepository(IDbSet<TEntity> dbSet)
		{
			DbSet = dbSet;
		}

		/// <inheritdoc/>
		public async Task<TEntity?> ById(string id, CancellationToken cancellationToken)
		{
			return await DbSet.ById(id, cancellationToken: cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<List<TEntity>> List(CancellationToken cancellationToken)
		{
			return await DbSet.Find(_ => true).ToListAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<List<TEntity>> List(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
		{
			return await DbSet.Find(filter).ToListAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<long> Count(CancellationToken cancellationToken)
		{
			return await DbSet.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<long> Count(Expression<Func<TEntity, bool>> filter, CountOptions? countOptions = null, CancellationToken cancellationToken = default)
		{
			return await DbSet.CountDocumentsAsync(filter, countOptions, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<string> Insert(TEntity entityToInsert, CancellationToken cancellationToken)
		{
			await DbSet.InsertOneAsync(entityToInsert, cancellationToken: cancellationToken);
			return entityToInsert.MongoId;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<string>> Insert(List<TEntity> entitiesToInsert, InsertManyOptions? insertManyOptions, CancellationToken cancellationToken)
		{
			if (!entitiesToInsert.Any())
				return new List<string>();

			await DbSet.InsertManyAsync(entitiesToInsert, insertManyOptions, cancellationToken);
			return entitiesToInsert.Select(x => x.MongoId);
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<string>> Insert(List<TEntity> entitiesToInsert, CancellationToken cancellationToken)
		{
			return await Insert(entitiesToInsert, null, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<ReplaceOneResult> ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions? options = null, CancellationToken cancellationToken = default)
		{
			return await DbSet.ReplaceOneAsync(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<ReplaceOneResult> ReplaceOne(string id, TEntity replacement, ReplaceOptions? options = null, CancellationToken cancellationToken = default)
		{
			return await DbSet.ReplaceOneAsync(doc => doc.MongoId == id, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<UpdateResult> UpdateById(string id, UpdateDefinition<TEntity> updateDefinition, UpdateOptions? updateOptions = null, CancellationToken cancellationToken = default)
		{
			return await DbSet.UpdateById(id, updateDefinition, updateOptions, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> Delete(string id, CancellationToken cancellationToken)
		{
			return await DbSet.DeleteById(id, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> Delete(FilterDefinition<TEntity> filter, CancellationToken cancellationToken)
		{
			return await DbSet.DeleteManyAsync(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> DeleteAll(CancellationToken cancellationToken)
		{
			return await DbSet.DeleteManyAsync(FilterDefinition<TEntity>.Empty, cancellationToken);
		}
	}
}
