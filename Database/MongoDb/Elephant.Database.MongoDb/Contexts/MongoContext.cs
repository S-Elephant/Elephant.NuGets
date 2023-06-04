using System.Reflection;
using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.Configurations;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Contexts
{
	/// <inheritdoc cref="IMongoContext"/>
	public class MongoContext : IMongoContext
	{
		/// <inheritdoc cref="IMongoContextOptionsBuilder"/>
		protected IMongoContextOptionsBuilder OptionsBuilder { get; }

		/// <inheritdoc cref="IMongoDatabase"/>
		public IMongoDatabase Database => OptionsBuilder.Database;

		/// <summary>
		/// Constructor.
		/// </summary>
		protected MongoContext(IMongoContextOptionsBuilder optionsBuilder)
		{
			OptionsBuilder = optionsBuilder;
			optionsBuilder.Configure(this, OnConfiguring);
		}

		/// <summary>
		/// Is called when this context wants run its configurations.
		/// Note that a <see cref="TimeoutException"/> may indicate that
		/// the database is either offline or unreachable.
		/// </summary>
		protected virtual void OnConfiguring()
		{
		}

		/// <summary>
		/// Automatically call all <see cref="BaseConfiguration.Configure"/> on all
		/// classes that derive from <see cref="BaseConfiguration"/> in the specified assemblies with names: <paramref name="assemblyNames"/>.
		/// </summary>
		/// <param name="throwIfNotFound">If true, will throw an <see cref="ArgumentNullException"/> if any assembly is not found.</param>
		/// <param name="assemblyNames">The assembly names to scan (case sensitive).</param>
		/// <returns>Amount of times <see cref="BaseConfiguration.Configure"/> was called by this in this one call.</returns>
		/// <exception cref="NullReferenceException">Thrown if <paramref name="throwIfNotFound"/> and any <paramref name="assemblyNames"/> wasn't not found.</exception>
		protected int AutoLoadConfigurationsByAssemblyNames(bool throwIfNotFound, params string[] assemblyNames)
		{
			if (!assemblyNames.Any())
				return 0; // There are no assemblies to scan.

			List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().
				Where(assembly => assemblyNames.Contains(assembly.GetName().Name ?? string.Empty))
				.ToList();

			if (throwIfNotFound && assemblies.Count != assemblyNames.Length)
			{
				throw new NullReferenceException($"One or more assemblies not found. Assemblies expected: {string.Join(", ", assemblyNames)}. Assemblies found: {string.Join(", ", assemblies.Select(x => x.FullName ?? "<Unknown>"))}");
			}

			return AutoLoadConfigurations(assemblies, OptionsBuilder);
		}

		/// <summary>
		/// Automatically call all <see cref="BaseConfiguration.Configure"/> on all
		/// classes that derive from <see cref="BaseConfiguration"/> in the specified <paramref name="assembliesToScan"/>.
		/// </summary>
		/// <returns>Amount of times <see cref="BaseConfiguration.Configure"/> was called by this in this one call.</returns>
		protected int AutoLoadConfigurations(List<Assembly> assembliesToScan, IMongoContextOptionsBuilder mongoContextOptionsBuilder)
		{
			int callCount = 0;

			foreach (Assembly assembly in assembliesToScan)
			{
				foreach (Type type in assembly.GetTypes()
							 .Where(myType =>
								 myType.IsClass && !myType.IsAbstract &&
								 myType.IsSubclassOf(typeof(BaseConfiguration))))
				{
					BaseConfiguration? newInstance = (BaseConfiguration?)Activator.CreateInstance(type);
					if (newInstance != null)
					{
						newInstance.Configure(mongoContextOptionsBuilder);
						callCount++;
					}
				}
			}

			return callCount;
		}
	}
}