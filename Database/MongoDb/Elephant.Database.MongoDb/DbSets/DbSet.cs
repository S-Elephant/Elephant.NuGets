using System.Collections;
using System.Linq.Expressions;
using Elephant.Database.MongoDb.Abstractions.DbSets;
using Elephant.Database.MongoDb.Types;
using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Elephant.Database.MongoDb.DbSets
{
	/// <summary>
	/// MongoDb database set.
	/// </summary>
	/// <typeparam name="TEntity">Your database entity.</typeparam>
	public class DbSet<TEntity> : IDbSet<TEntity>
		where TEntity : IId
	{
		/// <inheritdoc cref="IMongoCollection{TDocument}"/>
		private readonly IMongoCollection<TEntity> _collection;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="collection"><inheritdoc cref="IMongoCollection{TDocument}"/></param>
		public DbSet(IMongoCollection<TEntity> collection)
		{
			_collection = collection;
		}

		/// <summary>
		/// Create and return a by-id <see cref="FilterDefinition{TDocument}"/>.
		/// </summary>
		/// <param name="idValue">The MongoDb unique identifier used for the <see cref="FilterDefinition{TDocument}"/>.</param>
		/// <returns>The created <see cref="FilterDefinition{TDocument}"/>.</returns>
		private static FilterDefinition<TEntity> CreateByIdFilterDefinition(string idValue)
		{
			return Builders<TEntity>.Filter.Eq(nameof(BaseId.MongoId), idValue);
		}

		/// <inheritdoc/>
		public async Task<TEntity?> ById(string idValue, FindOptions<TEntity>? findOptions = null, CancellationToken cancellationToken = default)
		{
			FilterDefinition<TEntity> filter = CreateByIdFilterDefinition(idValue);
			IAsyncCursor<TEntity> cursor = await _collection.FindAsync(filter, findOptions, cancellationToken: cancellationToken);
			return await cursor.FirstOrDefaultAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<UpdateResult> UpdateById(string idValue, UpdateDefinition<TEntity> updateDefinition, UpdateOptions? updateOptions = null, CancellationToken cancellationToken = default)
		{
			FilterDefinition<TEntity> filter = CreateByIdFilterDefinition(idValue);
			return await _collection.UpdateOneAsync(filter, updateDefinition, updateOptions, cancellationToken);
		}

		/// <summary>
		/// For more info see: https://www.mongodb.com/docs/drivers/csharp/current/usage-examples/updateOne.
		/// Has no <see cref="UpdateOptions"/>.
		/// </summary>
		/// <param name="idValue">Unique identifier value of the entity to update.</param>
		/// <param name="updateDefinition"><![CDATA[Example: UpdateDefinition<YourEntity> updateDefinition = Builders<YourEntity>.Update.Set(yourEntity => yourEntity.Price, 100);]]></param>
		/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
		/// <returns><see cref="UpdateResult"/></returns>
		public async Task<UpdateResult> UpdateById(string idValue, UpdateDefinition<TEntity> updateDefinition, CancellationToken cancellationToken = default)
		{
			FilterDefinition<TEntity> filter = CreateByIdFilterDefinition(idValue);
			return await _collection.UpdateOneAsync(filter, updateDefinition, null, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> DeleteById(string idValue, DeleteOptions? deleteOptions = null, CancellationToken cancellationToken = default)
		{
			FilterDefinition<TEntity> filter = CreateByIdFilterDefinition(idValue);
			return await _collection.DeleteOneAsync(filter, deleteOptions, cancellationToken);
		}

		/// <inheritdoc/>
		public async Task<DeleteResult> DeleteById(string idValue, CancellationToken cancellationToken = default)
		{
			FilterDefinition<TEntity> filter = CreateByIdFilterDefinition(idValue);
			return await _collection.DeleteOneAsync(filter, null, cancellationToken);
		}

		#region IMongoCollection interface implementation

		/// <inheritdoc/>
		public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.Aggregate(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session,
			PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.Aggregate(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.AggregateAsync(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session,
			PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.AggregateAsync(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void AggregateToCollection<TResult>(PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			_collection.AggregateToCollection(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void AggregateToCollection<TResult>(IClientSessionHandle session,
			PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null, CancellationToken cancellationToken = default)
		{
			_collection.AggregateToCollection(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task AggregateToCollectionAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.AggregateToCollectionAsync(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task AggregateToCollectionAsync<TResult>(IClientSessionHandle session,
			PipelineDefinition<TEntity, TResult> pipeline,
			AggregateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.AggregateToCollectionAsync(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public BulkWriteResult<TEntity> BulkWrite(IEnumerable<WriteModel<TEntity>> requests,
			BulkWriteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.BulkWrite(requests, options, cancellationToken);
		}

		/// <inheritdoc/>
		public BulkWriteResult<TEntity> BulkWrite(IClientSessionHandle session,
			IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.BulkWrite(session, requests, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<WriteModel<TEntity>> requests,
			BulkWriteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.BulkWriteAsync(requests, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<BulkWriteResult<TEntity>> BulkWriteAsync(IClientSessionHandle session,
			IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.BulkWriteAsync(session, requests, options, cancellationToken);
		}

		/// <inheritdoc/>
		public long Count(FilterDefinition<TEntity> filter, CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocuments(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public long Count(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocuments(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<long> CountAsync(FilterDefinition<TEntity> filter, CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocumentsAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<long> CountAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocumentsAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public long CountDocuments(FilterDefinition<TEntity> filter, CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocuments(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public long CountDocuments(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocuments(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter, CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocumentsAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<long> CountDocumentsAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			CountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.CountDocumentsAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult
			DeleteMany(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
		{
			return _collection.DeleteMany(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult DeleteMany(FilterDefinition<TEntity> filter, DeleteOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteMany(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult DeleteMany(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			DeleteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteMany(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteManyAsync(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, DeleteOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteManyAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			DeleteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteManyAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult
			DeleteOne(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOne(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult DeleteOne(FilterDefinition<TEntity> filter, DeleteOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOne(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public DeleteResult DeleteOne(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			DeleteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOne(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOneAsync(filter, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, DeleteOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOneAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<DeleteResult> DeleteOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			DeleteOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.DeleteOneAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<TEntity, TField> field,
			FilterDefinition<TEntity> filter, DistinctOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.Distinct(field, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TField> Distinct<TField>(IClientSessionHandle session,
			FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter,
			DistinctOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.Distinct(session, field, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<TEntity, TField> field,
			FilterDefinition<TEntity> filter, DistinctOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.DistinctAsync(field, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TField>> DistinctAsync<TField>(IClientSessionHandle session,
			FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter,
			DistinctOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.DistinctAsync(session, field, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public long EstimatedDocumentCount(EstimatedDocumentCountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.EstimatedDocumentCount(options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<long> EstimatedDocumentCountAsync(EstimatedDocumentCountOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.EstimatedDocumentCountAsync(options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<TEntity> filter,
			FindOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindSync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TProjection> FindSync<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindSync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<TEntity> filter,
			FindOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndDelete<TProjection>(FilterDefinition<TEntity> filter,
			FindOneAndDeleteOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndDelete(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndDelete<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			FindOneAndDeleteOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndDelete(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<TEntity> filter,
			FindOneAndDeleteOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndDeleteAsync(filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndDeleteAsync<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter,
			FindOneAndDeleteOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndDeleteAsync(session, filter, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndReplace<TProjection>(FilterDefinition<TEntity> filter, TEntity replacement,
			FindOneAndReplaceOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndReplace(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndReplace<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter, TEntity replacement,
			FindOneAndReplaceOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndReplace(session, filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<TEntity> filter,
			TEntity replacement,
			FindOneAndReplaceOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndReplaceAsync<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter, TEntity replacement,
			FindOneAndReplaceOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndReplaceAsync(session, filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			FindOneAndUpdateOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndUpdate(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public TProjection FindOneAndUpdate<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndUpdate(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			FindOneAndUpdateOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<TProjection> FindOneAndUpdateAsync<TProjection>(IClientSessionHandle session,
			FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
			FindOneAndUpdateOptions<TEntity, TProjection>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.FindOneAndUpdateAsync(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void InsertOne(TEntity document, InsertOneOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			_collection.InsertOne(document, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void InsertOne(IClientSessionHandle session, TEntity document, InsertOneOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			_collection.InsertOne(session, document, options, cancellationToken);
		}

		/// <inheritdoc/>
#pragma warning disable CA1725 // Parameter names should match base declaration. Reason: original parameter naming is not great.
		public Task InsertOneAsync(TEntity document, CancellationToken cancellationToken)
#pragma warning restore CA1725 // Parameter names should match base declaration.
		{
			return _collection.InsertOneAsync(document, null, cancellationToken);
		}

		/// <inheritdoc/>
		public Task InsertOneAsync(TEntity document, InsertOneOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.InsertOneAsync(document, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task InsertOneAsync(IClientSessionHandle session, TEntity document, InsertOneOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.InsertOneAsync(session, document, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void InsertMany(IEnumerable<TEntity> documents, InsertManyOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			_collection.InsertMany(documents, options, cancellationToken);
		}

		/// <inheritdoc/>
		public void InsertMany(IClientSessionHandle session, IEnumerable<TEntity> documents,
			InsertManyOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			_collection.InsertMany(session, documents, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task InsertManyAsync(IEnumerable<TEntity> documents, InsertManyOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.InsertManyAsync(documents, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task InsertManyAsync(IClientSessionHandle session, IEnumerable<TEntity> documents,
			InsertManyOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.InsertManyAsync(session, documents, options, cancellationToken);
		}

#pragma warning disable CS0618

		/// <inheritdoc/>
		public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce,
			MapReduceOptions<TEntity, TResult>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.MapReduce(map, reduce, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IAsyncCursor<TResult> MapReduce<TResult>(IClientSessionHandle session, BsonJavaScript map,
			BsonJavaScript reduce,
			MapReduceOptions<TEntity, TResult>? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.MapReduce(session, map, reduce, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce,
			MapReduceOptions<TEntity, TResult>? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.MapReduceAsync(map, reduce, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(IClientSessionHandle session, BsonJavaScript map,
			BsonJavaScript reduce,
			MapReduceOptions<TEntity, TResult>? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.MapReduceAsync(session, map, reduce, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>()
			where TDerivedDocument : TEntity
		{
			return _collection.OfType<TDerivedDocument>();
		}

		/// <inheritdoc/>
		public ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement,
			ReplaceOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOne(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOne(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			TEntity replacement,
			ReplaceOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOne(session, filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			TEntity replacement,
			UpdateOptions options, CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOne(session, filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement,
			ReplaceOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement,
			UpdateOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			TEntity replacement,
			ReplaceOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			TEntity replacement, UpdateOptions options,
			CancellationToken cancellationToken = default)
		{
			return _collection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
		}

#pragma warning restore CS0618

		/// <inheritdoc/>
		public UpdateResult UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
			UpdateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.UpdateMany(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public UpdateResult UpdateMany(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			UpdateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.UpdateMany(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
			UpdateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.UpdateManyAsync(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			UpdateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.UpdateManyAsync(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
			UpdateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.UpdateOne(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public UpdateResult UpdateOne(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			UpdateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.UpdateOne(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update,
			UpdateOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.UpdateOneAsync(filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter,
			UpdateDefinition<TEntity> update,
			UpdateOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.UpdateOneAsync(session, filter, update, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IChangeStreamCursor<TResult> Watch<TResult>(
			PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.Watch(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IChangeStreamCursor<TResult> Watch<TResult>(IClientSessionHandle session,
			PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline,
			ChangeStreamOptions? options = null, CancellationToken cancellationToken = default)
		{
			return _collection.Watch(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(
			PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.WatchAsync(pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(IClientSessionHandle session,
			PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions? options = null,
			CancellationToken cancellationToken = default)
		{
			return _collection.WatchAsync(session, pipeline, options, cancellationToken);
		}

		/// <inheritdoc/>
		public IMongoCollection<TEntity> WithReadConcern(ReadConcern readConcern)
		{
			return _collection.WithReadConcern(readConcern);
		}

		/// <inheritdoc/>
		public IMongoCollection<TEntity> WithReadPreference(ReadPreference readPreference)
		{
			return _collection.WithReadPreference(readPreference);
		}

		/// <inheritdoc/>
		public IMongoCollection<TEntity> WithWriteConcern(WriteConcern writeConcern)
		{
			return _collection.WithWriteConcern(writeConcern);
		}

		/// <inheritdoc/>
		public CollectionNamespace CollectionNamespace => _collection.CollectionNamespace;

		/// <inheritdoc cref="IMongoDatabase"	/>
		public IMongoDatabase Database => _collection.Database;

		/// <inheritdoc/>
		public IBsonSerializer<TEntity> DocumentSerializer => _collection.DocumentSerializer;

		/// <inheritdoc/>
		public IMongoIndexManager<TEntity> Indexes => _collection.Indexes;

		/// <inheritdoc/>
		public MongoCollectionSettings Settings => _collection.Settings;

		#endregion

		#region IMongoQueryable Implementation

		/// <inheritdoc/>
		public QueryableExecutionModel GetExecutionModel()
		{
			return _collection.AsQueryable().GetExecutionModel();
		}

		/// <inheritdoc/>
		public IAsyncCursor<TEntity> ToCursor(CancellationToken cancellationToken = default)
		{
			return _collection.AsQueryable().ToCursor(cancellationToken);
		}

		/// <inheritdoc/>
		public Task<IAsyncCursor<TEntity>> ToCursorAsync(CancellationToken cancellationToken = default)
		{
			return _collection.AsQueryable().ToCursorAsync(cancellationToken);
		}

		/// <inheritdoc/>
		public IEnumerator<TEntity> GetEnumerator()
		{
			return _collection.AsQueryable().GetEnumerator();
		}

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return _collection.AsQueryable().GetEnumerator();
		}

		/// <inheritdoc/>
		public Type ElementType => _collection.AsQueryable().ElementType;

		/// <inheritdoc/>
		public Expression Expression => _collection.AsQueryable().Expression;

		/// <inheritdoc/>
		public IQueryProvider Provider => _collection.AsQueryable().Provider;

		#endregion
	}
}