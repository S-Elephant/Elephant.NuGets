using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.Attributes;

namespace Elephant.Database.MongoDb.Configurations
{
	/// <summary>
	/// Base configuration class.
	/// You may optionally inherit from this class in your configurations classes.
	/// </summary>
	[AutoLoadConfiguration]
	public abstract class BaseConfiguration
	{
		/// <summary>
		/// Configure.
		/// </summary>
		public abstract void Configure(IMongoContextOptionsBuilder optionsBuilder);
	}
}
