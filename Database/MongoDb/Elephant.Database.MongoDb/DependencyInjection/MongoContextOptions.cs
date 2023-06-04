namespace Elephant.Database.MongoDb.DependencyInjection
{
	/// <summary>
	/// Mongo context options container for the dependency injection
	/// setup (usually in Startup.cs).
	/// </summary>
	public class MongoContextOptions
	{
		/// <summary>
		/// MongoDb connection string (including authentication database if required).
		/// </summary>
		/// <example>mongodb://localhost:27017</example>
		public string ConnectionString { get; set; } = string.Empty;

		/// <summary>
		/// The database to connect with for the context that you are currently configuring.
		/// </summary>
		public string DatabaseName { get; set; } = string.Empty;
	}
}