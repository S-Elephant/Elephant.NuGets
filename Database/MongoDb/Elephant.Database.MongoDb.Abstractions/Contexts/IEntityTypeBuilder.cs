using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Abstractions.Contexts;

/// <summary>
/// Mongo context type builder.
/// </summary>
/// <typeparam name="TEntity">Entity (=document type) used in collection.</typeparam>
public interface IEntityTypeBuilder<TEntity>
	where TEntity : class
{
	/// <summary>
	/// Creates a <see cref="IMongoCollection{TDocument}"/> and returns it.
	/// </summary>
	IMongoCollection<TEntity> ToCollection(string collectionName, MongoCollectionSettings? settings = null);
}