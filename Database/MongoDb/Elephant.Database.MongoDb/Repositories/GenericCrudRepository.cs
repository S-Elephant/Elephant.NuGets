using System.Linq.Expressions;
using Elephant.Database.MongoDb.Abstractions.Repositories;
using Elephant.Database.MongoDb.DbSets;
using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Repositories
{
	/// <inheritdoc cref="IGenericCrudRepository{TEntity}"/>
	public class GenericCrudRepository<TEntity> : IGenericCrudRepository<TEntity>
		where TEntity : IId
	{
		private readonly DbSet<TEntity> _dbSet;

		/// <summary>
		/// Constructor.
		/// </summary>
		public GenericCrudRepository(DbSet<TEntity> dbSet)
		{
			_dbSet = dbSet;
		}

		/// <inheritdoc/>
		public async Task<TEntity?> ById(string id, CancellationToken cancellationToken)
		{
			return await _dbSet.ById(id, cancellationToken: cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<List<TEntity>> List(CancellationToken cancellationToken)
		{
			return await _dbSet.Find(_ => true).ToListAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<List<TEntity>> List(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
		{
			return await _dbSet.Find(filter).ToListAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<long> Count(CancellationToken cancellationToken)
		{
			return await _dbSet.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<long> Count(Expression<Func<TEntity, bool>> filter, CountOptions? countOptions = null, CancellationToken cancellationToken = default)
		{
			return await _dbSet.CountDocumentsAsync(filter, countOptions, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<string> Insert(TEntity entityToInsert, CancellationToken cancellationToken)
		{
			await _dbSet.InsertOneAsync(entityToInsert, cancellationToken);
			return entityToInsert.MongoId;
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<string>> Insert(List<TEntity> entitiesToInsert, InsertManyOptions? insertManyOptions, CancellationToken cancellationToken)
		{
			if (!entitiesToInsert.Any())
				return new List<string>();

			await _dbSet.InsertManyAsync(entitiesToInsert, insertManyOptions, cancellationToken);
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
			return await _dbSet.ReplaceOneAsync(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<ReplaceOneResult> ReplaceOne(string id, TEntity replacement, ReplaceOptions? options = null, CancellationToken cancellationToken = default)
		{
			return await _dbSet.ReplaceOneAsync(doc => doc.MongoId == id, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<UpdateResult> UpdateById(string id, UpdateDefinition<TEntity> updateDefinition, UpdateOptions? updateOptions = null, CancellationToken cancellationToken = default)
		{
			return await _dbSet.UpdateById(id, updateDefinition, updateOptions, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> Delete(string id, CancellationToken cancellationToken)
		{
			return await _dbSet.DeleteById(id, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> Delete(FilterDefinition<TEntity> filter, CancellationToken cancellationToken)
		{
			return await _dbSet.DeleteManyAsync(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> DeleteAll(CancellationToken cancellationToken)
		{
			return await _dbSet.DeleteManyAsync(FilterDefinition<TEntity>.Empty, cancellationToken);
		}
	}
}
