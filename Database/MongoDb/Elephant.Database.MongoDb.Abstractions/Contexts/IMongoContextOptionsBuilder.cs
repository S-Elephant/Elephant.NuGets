using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Abstractions.Contexts
{
	/// <summary>
	/// Mongo context options builder.
	/// </summary>
	public interface IMongoContextOptionsBuilder
	{
		/// <inheritdoc cref="IMongoDatabase"/>
		IMongoDatabase Database { get; }

		/// <summary>
		/// Configure this Mongo context.
		/// </summary>
		/// <exception cref="NullReferenceException">Thrown if unable to retrieve a required PropertyInfo
		/// from <paramref name="context"/> or if a Collection property was not found when it was required.</exception>
		void Configure(IMongoContext context, Action configAction);

		/// <summary>
		/// Used in your entity configuration classes.
		/// </summary>
		/// <example><![CDATA[optionsBuilder.Entity<Customer>(entity =>...]]></example>
		/// <exception cref="NullReferenceException">Thrown if unable to cast or if private _entityToBuilderMap
		/// doesn't contain the builder we need.</exception>
		IEntityTypeBuilder<TEntity> Entity<TEntity>(Action<IEntityTypeBuilder<TEntity>> buildAction)
			where TEntity : class;
	}
}