using MongoDB.Bson.IO;

namespace Elephant.Database.MongoDb.Abstractions.Repositories;

/// <summary>
/// Generic CRUD base repository. Example setup:
/// <![CDATA[services.AddScoped<IDatabaseRepository, DatabaseRepository>(x => new DatabaseRepository(new MongoClient("YourMongoDbConnectionStringHere")));]]>
/// </summary>
public interface IDatabaseRepository
{
	/// <summary>
	/// Returns the list of all databases in json format.
	/// </summary>
	/// <param name="jsonWriterSettings"><see cref="JsonWriterSettings"/></param>
	/// <param name="cancellationToken"><see cref="CancellationToken"/></param>
	/// <returns>All databases in json format</returns>
	Task<string> ListAllDatabasesAsJson(JsonWriterSettings? jsonWriterSettings = null, CancellationToken cancellationToken = default);
}