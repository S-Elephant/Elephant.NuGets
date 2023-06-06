using System.Linq.Expressions;
using Elephant.Database.MongoDb.Abstractions.DbSets;
using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Abstractions.Repositories;

/// <summary>
/// Generic CRUD base repository.
/// </summary>
public interface IGenericCrudRepository<TEntity>
	where TEntity : IId
{
	/// <summary>
	/// Collection/DbSet of your entity.
	/// </summary>
	protected IDbSet<TEntity> DbSet { get; }

	/// <summary>
	/// Get entity by <paramref name="id"/>.
	/// </summary>
	/// <param name="id">Unique identifier of the entity to retrieve.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>The entity found or null if not found.</returns>
	Task<TEntity?> ById(string id, CancellationToken cancellationToken);

	/// <summary>
	/// Return all documents in the collection.
	/// </summary>
	Task<List<TEntity>> List(CancellationToken cancellationToken);

	/// <summary>
	/// Return all filtered documents in the collection.
	/// </summary>
	Task<List<TEntity>> List(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

	/// <summary>
	/// Return the amount of documents in the collection.
	/// </summary>
	Task<long> Count(CancellationToken cancellationToken);

	/// <summary>
	/// Return the amount of filtered documents in the collection.
	/// </summary>
	Task<long> Count(Expression<Func<TEntity, bool>> filter, CountOptions? countOptions = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Insert on entity.
	/// </summary>
	/// <param name="entityToInsert">The entity to insert. Note that its mongo id will be filled.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>The mongo id of the newly inserted entity.</returns>
	Task<string> Insert(TEntity entityToInsert, CancellationToken cancellationToken);

	/// <summary>
	/// Insert one or more entities.
	/// </summary>
	/// <param name="entitiesToInsert">The entities to insert. Note that their mongo ids will be filled.</param>
	/// <param name="insertManyOptions"><see cref="InsertManyOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>The mongo ids of the newly inserted entities.</returns>
	Task<IEnumerable<string>> Insert(List<TEntity> entitiesToInsert, InsertManyOptions? insertManyOptions, CancellationToken cancellationToken);

	/// <summary>
	/// Insert one or more entities.
	/// </summary>
	/// <param name="entitiesToInsert">The entities to insert. Note that their mongo ids will be filled.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>The mongo ids of the newly inserted entities.</returns>
	Task<IEnumerable<string>> Insert(List<TEntity> entitiesToInsert, CancellationToken cancellationToken);

	/// <summary>
	/// Update a document by <paramref name="filter"/> by fully replacing it.
	/// </summary>
	/// <param name="filter">The filter used to find the document to be replaced. Example:
	/// <![CDATA[doc => doc.MongoId == myCustomerId]]></param>
	/// <param name="replacement">The new document.</param>
	/// <param name="options"><see cref="ReplaceOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="ReplaceOneResult"/></returns>
	Task<ReplaceOneResult> ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions? options = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Update a document by <paramref name="id"/> by fully replacing it.
	/// </summary>
	/// <param name="id">Id of the document to be replaced.</param>
	/// <param name="replacement">The new document.</param>
	/// <param name="options"><see cref="ReplaceOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="ReplaceOneResult"/></returns>
	Task<ReplaceOneResult> ReplaceOne(string id, TEntity replacement, ReplaceOptions? options = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Update some (or all) of a document its properties by <paramref name="updateDefinition"/>.
	/// </summary>
	/// <param name="id">The id of the entity in the database that requires updating.</param>
	/// <param name="updateDefinition">The new definition used to determine which document properties to update. Example:
	/// <![CDATA[UpdateDefinition<Customer> updateDefinition = Builders<Customer>.Update.Set(customer => customer.Name, "John Lee");]]></param>
	/// <param name="updateOptions"><see cref="UpdateOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="UpdateResult"/></returns>
	Task<UpdateResult> UpdateById(string id, UpdateDefinition<TEntity> updateDefinition, UpdateOptions? updateOptions = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete zero or one by <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The entity with this mongo id will be deleted.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="DeleteResult"/></returns>
	Task<DeleteResult> Delete(string id, CancellationToken cancellationToken);

	/// <summary>
	/// Delete zero or more by <paramref name="filter"/>.
	/// </summary>
	/// <param name="filter">Documents that meet this criteria will be deleted.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="DeleteResult"/></returns>
	Task<DeleteResult> Delete(FilterDefinition<TEntity> filter, CancellationToken cancellationToken);

	/// <summary>
	/// Delete all documents in the collection.
	/// </summary>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="DeleteResult"/></returns>
	Task<DeleteResult> DeleteAll(CancellationToken cancellationToken);
}