using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Abstractions.Contexts
{
	/// <summary>
	/// Mongo database context.
	/// </summary>
	public interface IMongoContext
	{
		/// <inheritdoc cref="IMongoDatabase"/>
		public IMongoDatabase Database { get; }
	}
}
