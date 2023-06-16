# About

Features:

- MongoDb Configurations (that can optionally be auto-loaded).
  - Configurations may optionally be put in a different assembly or even across multiple assemblies.
- Generic CRUD repository.
- Generic MongoContext.
- List all MongoDb databases.

# MongoDb connection strings explained in short

```json
Explanation:
"mongodb://myUser:myPassword@myServerAddress:myMongoDbPortOnServer/myAuthenticationDatabase"

Example with authenication database:
"mongodb://admin:password123@server-001:27017/authDatabase"

Example without authenication database:
"mongodb://admin:password123@server-001:27017"
```

Note that the default MongoDb port is **27017**.

You can easily test connection strings to a working MongoDb using [Compass](https://www.mongodb.com/try/download/compass) (its official and free).

# Getting started

## Create a database POCO/Entity

Example:

```c#
using Elephant.Database.MongoDb.Types;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbShop.Entities.Shop
{
	/// <summary>
	/// Product.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class Product : BaseId // BaseId requires the "Elephant.Database.MongoDb.Types" NuGet.
	{
		/// <summary>
		/// Name.
		/// </summary>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Quantity in store.
		/// </summary>
		[BsonElement("quantity")]
		public int Quantity { get; set; } = 0;

		/// <summary>
		/// Price per entity.
		/// </summary>
		[BsonElement("price")]
		public decimal Price { get; set; } = 1.25M;

		/// <summary>
		/// Description.
		/// </summary>
		[BsonElement("description")]
		public string Description { get; set; } = string.Empty;
	}
}

```



## Create a custom repository and interface that inherits from the generic one

Example:

```c#
// Requires NuGets "Elephant.Database.MongoDb" and "Elephant.Database.MongoDb.Abstractions".

public interface IProductRepository : IGenericCrudRepository<Product>
{
	// Your custom methods here.
}

public class ProductRepository : GenericCrudRepository<Product>, IProductRepository
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductRepository(IShopContext shopContext) : base(shopContext.Products)
	{
        // Your custom methods here. You may use the inherited variable DbSet.
	}
}
```




## (Optional) Add them to the dependency injection

```c#
public void ConfigureServices(..)
{
    ..
        
	services.AddMongoContext<IShopContext, ShopContext>(options =>
	{
		options.ConnectionString = "mongodb://myUser:myPassword@myServerAddress:myServerPort/myAuthenticationDatabase" "Your MongoDb connection string here";
		options.DatabaseName = "Your mongoDb Database name here (case sensitive!)";
	});

	services.AddScoped<IProductRepository, ProductRepository>();
	services.AddScoped<IDatabaseRepository, DatabaseRepository>(x => new DatabaseRepository(new MongoClient(Constants.ConnectionString))); // This one is also optional.
}
```

# Conventions

## camelCase example

```c#
using Elephant.Database.MongoDb;

public void ConfigureServices(IServiceCollection services, ..)
{
    ConfigureDatabases(..)       
    ..
}

public void ConfigureDatabases(IServiceCollection services, ..)
{
   // Execute this BEFORE creating your MongoDb context. So usually before services.AddMongoContext<..>(options => ..);
   ConventionPacks.EnforceGlobalCamelCase(); // Add optional namespaces as needed in the parameter.
   
   // Configure your MongoDB context(s) here somewhere.
}
```

