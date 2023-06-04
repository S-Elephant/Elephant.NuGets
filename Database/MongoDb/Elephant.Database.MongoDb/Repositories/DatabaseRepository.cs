using Elephant.Database.MongoDb.Abstractions.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Repositories
{
	/// <inheritdoc cref="IDatabaseRepository"/>
	public class DatabaseRepository : IDatabaseRepository
	{
		/// <inheritdoc cref="IMongoClient"/>
		private readonly IMongoClient _mongoClient;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="mongoClient"><see cref="IMongoClient"/></param>
		public DatabaseRepository(IMongoClient mongoClient)
		{
			_mongoClient = mongoClient;
		}

		/// <inheritdoc/>
		public async Task<string> ListAllDatabasesAsJson(JsonWriterSettings? jsonWriterSettings = null, CancellationToken cancellationToken = default)
		{
			JsonWriterSettings finalJsonWriterSettings = jsonWriterSettings ?? new JsonWriterSettings
			{
				OutputMode = JsonOutputMode.CanonicalExtendedJson,
				Indent = true,
			};

			List<BsonDocument> databases = (await _mongoClient.ListDatabasesAsync(cancellationToken)).ToList();
			return databases.ToJson(finalJsonWriterSettings);
		}
	}
}
