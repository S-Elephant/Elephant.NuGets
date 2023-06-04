using Elephant.Database.MongoDb.Abstractions.Contexts;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Contexts
{
	/// <summary>
	/// Created collections, configures mappers and configures indices.
	/// </summary>
	/// <typeparam name="TEntity">Your database entity type.</typeparam>
	public class EntityTypeBuilder<TEntity> : IEntityTypeBuilder<TEntity>
		where TEntity : class
	{
		/// <inheritdoc cref="IMongoDatabase"/>
		private readonly IMongoDatabase _database;

		/// <summary>
		/// Don't remove this property, it is used by reflection.
		/// More info: <see cref="IMongoCollection{TDocument}"/>.
		/// </summary>
		public IMongoCollection<TEntity>? Collection { get; private set; } = null;

		/// <summary>
		/// Constructor.
		/// </summary>
		public EntityTypeBuilder(IMongoDatabase database)
		{
			_database = database;
		}

		/// <summary>
		/// Creates a <see cref="IMongoCollection{TDocument}"/> and returns it.
		/// </summary>
		public IMongoCollection<TEntity> ToCollection(string collectionName, MongoCollectionSettings? settings = null)
		{
			Collection = _database.GetCollection<TEntity>(collectionName, settings);
			return Collection;
		}
	}
}