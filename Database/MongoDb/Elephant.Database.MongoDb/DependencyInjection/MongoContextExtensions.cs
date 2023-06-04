using System.Diagnostics.CodeAnalysis;
using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Elephant.Database.MongoDb.DependencyInjection
{
	/// <summary>
	/// Dependency injection extensions for the <see cref="MongoContext"/>.
	/// </summary>
	/// <example><![CDATA[
	/// services.AddMongoContext<IShopContext, ShopContext>(options =>
	/// {
	///   options.ConnectionString = "mongodb://localhost:27017";
	///   options.DatabaseName = "MyDatabaseName";
	///	});]]>
	/// </example>
	[SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1004:DocumentationLinesMustBeginWithSingleSpace", Justification = "Readability.")]
	public static class MongoContextExtensions
	{
		/// <summary>
		/// Add your MongoContext to the dependency injection.
		/// </summary>
		/// <example><![CDATA[
		/// services.AddMongoContext<IShopContext, ShopContext>(options =>
		///	{
		///	  options.ConnectionString = "mongodb://localhost:27017";
		///	  options.DatabaseName = "MyDatabaseName";
		///	});]]>
		/// </example>
		public static void AddMongoContext<TContextService, TContextImplementation>(
			this IServiceCollection services, Action<MongoContextOptions> mongoContextOptionsAction, IEventSubscriber? mongoEventSubscriber = null)
			where TContextImplementation : MongoContext, TContextService
			where TContextService : class
		{
			AddRequiredServices(services, mongoContextOptionsAction, mongoEventSubscriber);

			services.AddScoped<TContextService, TContextImplementation>();
		}

		/// <summary>
		/// Add your MongoContext to the dependency injection.
		/// </summary>
		public static void AddMongoContext<TContextImplementation>(
			this IServiceCollection services, Action<MongoContextOptions> mongoContextOptionsAction, IEventSubscriber? mongoEventSubscriber = null)
			where TContextImplementation : MongoContext
		{
			AddRequiredServices(services, mongoContextOptionsAction, mongoEventSubscriber);

			services.AddScoped<TContextImplementation>();
		}

		/// <summary>
		/// Add required services.
		/// </summary>
		private static void AddRequiredServices(IServiceCollection services, Action<MongoContextOptions> mongoContextOptionsAction, IEventSubscriber? mongoEventSubscriber)
		{
			services.Configure(mongoContextOptionsAction);

			services.AddSingleton(provider =>
			{
				IOptions<MongoContextOptions> options = provider.GetRequiredService<IOptions<MongoContextOptions>>();
				MongoClientSettings settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
				if (mongoEventSubscriber != null)
					settings.ClusterConfigurator = builder => builder.Subscribe(mongoEventSubscriber);
				MongoClient client = new(settings);

				return client.GetDatabase(options.Value.DatabaseName);
			});
			services.AddSingleton<IMongoContextOptionsBuilder>(provider =>
			{
				IMongoDatabase? database = provider.GetRequiredService<IMongoDatabase>();
				return new MongoContextOptionsBuilder(database);
			});
		}
	}
}