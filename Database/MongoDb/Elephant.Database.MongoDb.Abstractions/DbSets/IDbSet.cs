using Elephant.Database.MongoDb.Types.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Elephant.Database.MongoDb.Abstractions.DbSets;

/// <summary>
/// MongoDb database set.
/// </summary>
/// <typeparam name="TEntity">Your database entity.</typeparam>
public interface IDbSet<TEntity> : IMongoCollection<TEntity>, IMongoQueryable<TEntity>
	where TEntity : IId
{
	/// <summary>
	/// Finds an entity by its MongoDb key.
	/// </summary>
	/// <param name="idValue">Mongo identifier (is always a string).</param>
	/// <param name="findOptions"><see cref="FindOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>Entity or null if not found.</returns>
	Task<TEntity?> ById(string idValue, FindOptions<TEntity>? findOptions = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// For more info see: https://www.mongodb.com/docs/drivers/csharp/current/usage-examples/updateOne.
	/// </summary>
	/// <param name="idValue">Unique identifier value of the entity to update.</param>
	/// <param name="updateDefinition"><![CDATA[Example: UpdateDefinition<YourEntity> updateDefinition = Builders<YourEntity>.Update.Set(yourEntity => yourEntity.Price, 100);]]></param>
	/// <param name="updateOptions"><see cref="UpdateOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="UpdateResult"/></returns>
	Task<UpdateResult> UpdateById(string idValue, UpdateDefinition<TEntity> updateDefinition, UpdateOptions? updateOptions = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// For more info see: https://www.mongodb.com/docs/drivers/csharp/current/usage-examples/updateOne.
	/// Has no <see cref="UpdateOptions"/>.
	/// </summary>
	/// <param name="idValue">Unique identifier value of the entity to update.</param>
	/// <param name="updateDefinition"><![CDATA[Example: UpdateDefinition<YourEntity> updateDefinition = Builders<YourEntity>.Update.Set(yourEntity => yourEntity.Price, 100);]]></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="UpdateResult"/></returns>
	Task<UpdateResult> UpdateById(string idValue, UpdateDefinition<TEntity> updateDefinition, CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete entity with id <paramref name="idValue"/>.
	/// For more info see: https://www.mongodb.com/docs/drivers/csharp/current/usage-examples/deleteOne/.
	/// </summary>
	/// <param name="idValue">The entity with this Mongo unique identifier will be deleted.</param>
	/// <param name="deleteOptions"><see cref="DeleteOptions"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="DeleteResult"/></returns>
	Task<DeleteResult> DeleteById(string idValue, DeleteOptions? deleteOptions = null, CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete entity with id <paramref name="idValue"/>.
	/// Has no <see cref="DeleteOptions"/>.
	/// For more info see: https://www.mongodb.com/docs/drivers/csharp/current/usage-examples/deleteOne/.
	/// </summary>
	/// <param name="idValue">The entity with this Mongo unique identifier will be deleted.</param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns><see cref="DeleteResult"/></returns>
	Task<DeleteResult> DeleteById(string idValue, CancellationToken cancellationToken = default);
}