using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Elephant.Database.MongoDb.Abstractions.Contexts;
using Elephant.Database.MongoDb.DbSets;
using MongoDB.Driver;

namespace Elephant.Database.MongoDb.Contexts
{
	/// <inheritdoc cref="IMongoContextOptionsBuilder"/>
	public class MongoContextOptionsBuilder : IMongoContextOptionsBuilder
	{
		/// <summary>
		/// Only allows certain configurations in <see cref="Configure"/> if this is false.
		/// </summary>
		private bool IsConfigured { get; set; }

		private readonly ConcurrentDictionary<Type, object> _entityToBuilderMap = new();

		/// <inheritdoc/>
		public IMongoDatabase Database { get; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="database"><see cref="IMongoDatabase"/></param>
		public MongoContextOptionsBuilder(IMongoDatabase database)
		{
			Database = database;
		}

		/// <inheritdoc/>
		[SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "I don't like a space between ) and ! .")]
		public void Configure(IMongoContext context, Action configAction)
		{
			if (!IsConfigured)
				configAction.Invoke();

			// Get collection properties in context.
			(string Name, Type Type)[] contextProperties = context.GetType().GetRuntimeProperties()
				.Where(
					p => !(p.GetMethod ?? p.SetMethod)!.IsStatic
						 && !p.GetIndexParameters().Any()
						 && p.DeclaringType != typeof(MongoContext)
						 && p.PropertyType.GetTypeInfo().IsGenericType
						 && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
				.OrderBy(p => p.Name)
				.Select(
					p => (
						p.Name,
						Type: p.PropertyType.GenericTypeArguments.Single()))
				.ToArray();

			// Set value of each collection property.
			foreach ((string Name, Type Type) propertyInfo in contextProperties)
			{
				Type dbSetType = typeof(DbSet<>).MakeGenericType(propertyInfo.Type);
				object? dbSet = Activator.CreateInstance(dbSetType, this, context, GetCollectionInstance(propertyInfo));

				PropertyInfo? propertyInfo2 = context.GetType().GetProperty(propertyInfo.Name);
				if (propertyInfo2 == null)
				{
					throw new NullReferenceException($"{nameof(PropertyInfo)} may not be null when attempting to retrieve it from context with name: {propertyInfo.Name}.");
				}
				propertyInfo2.SetValue(context, dbSet);
			}

			// Note: if required, indices may be configured here.

			IsConfigured = true;
		}

		private object GetCollectionInstance((string Name, Type Type) propertyInfo)
		{
			Type builderType = typeof(EntityTypeBuilder<>).MakeGenericType(propertyInfo.Type);

			object? builder = _entityToBuilderMap.ContainsKey(propertyInfo.Type)
				? _entityToBuilderMap[propertyInfo.Type]
				: Activator.CreateInstance(builderType, Database);
			PropertyInfo? collectionProperty = builderType.GetProperty("Collection");

			if (collectionProperty == null)
			{
				throw new NullReferenceException("Missing Collection property.");
			}

			object? collection = collectionProperty.GetValue(_entityToBuilderMap[propertyInfo.Type]);

			if (collection == null)
			{
				MethodInfo getCollectionMethod = Database.GetType().GetMethod("GetCollection") ??
												 throw new InvalidOperationException(
													 "IMongoDatabase don't expose GetCollection method which is expected.");
				MethodInfo generic = getCollectionMethod.MakeGenericMethod(propertyInfo.Type);

				collection =
					generic.Invoke(Database, new object?[] { propertyInfo.Name, null }) ??
					throw new InvalidOperationException(
						$"GetCollection<{propertyInfo.Type}>(\"{propertyInfo.Name}\") doesn't return a value.");

				collectionProperty.SetValue(builder, collection);
				_entityToBuilderMap[propertyInfo.Type] = builder!;
			}

			return collection;
		}

		/// <inheritdoc/>
		public IEntityTypeBuilder<TEntity> Entity<TEntity>(Action<IEntityTypeBuilder<TEntity>> buildAction)
			where TEntity : class
		{
			EntityTypeBuilder<TEntity>? builder = _entityToBuilderMap.ContainsKey(typeof(TEntity))
				? _entityToBuilderMap[typeof(TEntity)] as EntityTypeBuilder<TEntity>
				: new EntityTypeBuilder<TEntity>(Database);

			if (builder == null)
			{
				throw new NullReferenceException($"{nameof(EntityTypeBuilder<TEntity>)} cannot be null. Failed to retrieve it.");
			}

			buildAction.Invoke(builder);
			_entityToBuilderMap[typeof(TEntity)] = builder;
			return builder;
		}
	}
}