[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Database)](https://www.nuget.org/packages/Elephant.Database/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Database.svg)](https://www.nuget.org/packages/Elephant.Database/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Database/LICENSE.txt)

# About

EF Core code-first helpers that reduce EntityTypeBuilder boilerplate and enforce consistent schema conventions. The package provides `ConfigurationHelper`utilities to:
•	Create tables with int or Guid primary keys (`ToIdTableWithPrimaryKey<T>`, `ToGuidTableWithPrimaryKey<T>`).
•	Add standardized columns such as Name, Description, and IsEnabled (`AddName<T>`, `AddDescription<T>`, `AddIsEnabled<T>`).
•	Combine common patterns (for example `ToIdNameTableWithPrimaryKey<T>`).
•	Keep column types, constraints, and defaults consistent across your model.

Use these helpers from `IEntityTypeConfiguration<T>.Configure` or `OnModelCreating` to simplify model configuration and migrations.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Database`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Database
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Database" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Database
```

# How to Use

## DbSetExtensions

### Example

```c#
// Mark all entities in the DbSet as deleted and persist the change.
DbSet<Todo> todos = context.Set<Todo>();
todos.Clear();
context.SaveChanges();
```





## GenericCrudRepository

### Example

```c#
// Example usage: fetch customers including Orders and Addresses without tracking.
public async Task<List<Customer>> GetActiveCustomersAsync(IGenericCrudRepository<Customer> repository, CancellationToken cancellationToken)
{
	// Retrieve all customers with includes and no tracking.
	List<Customer> customers = await repository.AllAsync(QueryTrackingBehavior.NoTracking, cancellationToken, x => x.Orders, x => x.Addresses);
}
```

### Example

```c#
// Example usage: create an Order and ensure atomic commit using repository transaction helpers.
public async Task<int> CreateOrderTransactionalAsync(IGenericCrudRepository<Order> repository, Order order, CancellationToken cancellationToken)
{
	IDbContextTransaction transaction = await repository.BeginTransactionAsync(cancellationToken);
	try
	{
		// Your code here.
	}
	catch
	{
		// Rollback on error and rethrow.
		await repository.RollbackTransactionAndDisposeAsync(transaction, cancellationToken);
		throw;
	}
}
```

### Available helpers

```c#
virtual Task<List<TEntity>> AllAsync(CancellationToken cancellationToken);

// Example usage: await myRepository.All(1, QueryTrackingBehavior.NoTracking, CancellationToken.None, x => x.CustomersCrossOrders, x => x.CustomersCrossAddresses);
virtual Task<List<TEntity>> AllAsync(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

virtual Task<TEntity?> ByIdAsync(object id, CancellationToken cancellationToken);

virtual Task<int> CountAsync(CancellationToken cancellationToken);

virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

virtual void Delete(object id);

virtual Task<IResult<int>> DeleteAndSaveAsync(object id, CancellationToken cancellationToken);

virtual Task<bool> HasAnyAsync(CancellationToken cancellationToken);

virtual Task<bool> HasAnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

virtual Task InsertAsync(TEntity obj, CancellationToken cancellationToken);

virtual Task InsertAsync(ICollection<TEntity> objects, CancellationToken cancellationToken);

virtual Task<IResult<int>> InsertAndSaveAsync(TEntity obj, CancellationToken cancellationToken);

virtual Task<int> SaveAsync(CancellationToken cancellationToken);

virtual void Update(TEntity obj);

virtual void Update(ICollection<TEntity> objects);

virtual Task<IResult<int>> UpdateAndSaveAsync(TEntity obj, CancellationToken cancellationToken);

virtual Task DeleteAllAndResetAutoIncrementAsync(CancellationToken cancellationToken = default, string schema = "dbo");

// Transactions:

virtual Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

virtual Task CommitTransactionAsyncAndDisposeAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken);

virtual Task RollbackTransactionAsyncAndDisposeAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken);
```

### Example IContext transaction implementation

```c#
// Inside your DbContext or IdentityDbContext class:

/// <inheritdoc/>
public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
{
	return await Database.BeginTransactionAsync(cancellationToken);
}

/// <inheritdoc/>
public async Task CommitTransactionAndDisposeAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken)
{
	if (transaction == null)
		return;

	await Database.CommitTransactionAsync(cancellationToken);
	await transaction.DisposeAsync();
}

/// <inheritdoc/>
public async Task RollbackTransactionAndDisposeAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken)
{
	if (transaction == null)
		return;

	await Database.RollbackTransactionAsync(cancellationToken);
	await transaction.DisposeAsync();
}

```

### Example transaction in your service

```c#
using (IDbContextTransaction transaction = await _customerRepository.BeginTransactionAsync(cancellationToken))
{
    try
    {
        // Your code here.
    }
    catch (Exception exception)
    {
        _logger.LogError($"An error occurred. Rolling back changes. Exception: {exception}".);
        await _customerRepository.RollbackTransactionAndDisposeAsync(transaction, cancellationToken);
        return;
    }

    // Perform CRUD actions or whatever.
	await _customerRepository.SaveAsync(cancellationToken);
	await _customerRepository.CommitTransactionAndDisposeAsync(transaction, cancellationToken);
}
```



## GenericCrudIdRepository

### Example

```c#
// DI registration:
services.AddScoped<IGenericCrudIdRepository<Customer>, CustomerRepository>();
services.AddScoped<IGenericCrudIdRepository<RecentlyUsedTag>, RecentlyUsedTagsRepository>();
services.AddScoped<ICustomerService, CustomerService>();

public class CustomerRepository : GenericCrudIdRepository<Customer, AppDbContext>
{
	public CustomerRepository(AppDbContext databaseService) : base(databaseService)
	{
	}
}

public class RecentlyUsedTagsRepository : GenericCrudIdRepository<RecentlyUsedTag, AppDbContext>
{
	public RecentlyUsedTagsRepository(AppDbContext databaseService) : base(databaseService)
	{
	}
}

public class CustomerService
{
	private readonly IGenericCrudIdRepository<Customer> _customerRepository;
	private readonly IGenericCrudIdRepository<RecentlyUsedTag> _recentlyUsedTagsRepository;

    public CustomerService(IGenericCrudIdRepository<Customer> customerRepository, IGenericCrudIdRepository<RecentlyUsedTag> recentlyUsedTagsRepository)
	{
		_customerRepository = customerRepository;
        _recentlyUsedTagsRepository = recentlyUsedTagsRepository;
	}

	public async Task<List<Customer>> GetCustomersWithRelationsAsync(CancellationToken cancellationToken)
	{
		// Call AllAsync with NoTracking and include Orders and Addresses.
		List<Customer> customers = await _customerRepository.AllAsync(QueryTrackingBehavior.NoTracking, cancellationToken, c => c.Orders, c => c.Addresses);
		return customers;
	}

	public async Task<int> PurgeOldRecentlyUsedTagsAsync(int keepCount, CancellationToken cancellationToken)
	{
		// Delete overflowing recently used tags ordered by LastUsedAt (keeps newest 'keepCount' records).
		int deleted = await _recentlyUsedTagsRepository.DeleteOverflowingEntities(keepCount, source => source.OrderByDescending(x => x.LastUsedAt), cancellationToken);
		return deleted;
	}
}
```

### Available helpers

`GenericCrudIdRepository` contains all methods as listed in the [GenericCrudRepository](##GenericCrudRepository) plus these available helpers:

```c#
virtual Task<T?> ByIdAsync(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
virtual async Task<int> DeleteOverflowingEntities(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = default)
virtual Task<bool> HasIdAsync(int id, CancellationToken cancellationToken);
virtual Task<int> HighestIdAsync(CancellationToken cancellationToken);
virtual Task<int> LowestIdAsync(CancellationToken cancellationToken);
virtual Task<int> NextIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken);
virtual async Task<List<int>> OverflowingIds(int maxEntities, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default)
virtual Task<int> PreviousIdAsync(int sourceId, bool cycle, CancellationToken cancellationToken);
```



## GenericCrudGuidRepository

### Available helpers

`GenericCrudGuidRepository` contains all methods as listed in the [GenericCrudRepository](##GenericCrudRepository) plus these:

```c#
virtual Task<TEntity?> ByIdAsync(Guid id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

virtual Task<bool> HasIdAsync(Guid id, CancellationToken cancellationToken);
```



## QueryableExtensions

### Example

```c#
private async Task<Actor?> ById(int actorId, QueryTrackingBehavior queryTrackingBehavior, CancellationToken cancellationToken)
{
	return await Context.Actors
		.AsTracking(queryTrackingBehavior) // This line.
		.Where(x => x.Id == actorId)
		.OrderBy(x => x.Id)
		.Include(x => x.Actors_Websites)
			.ThenInclude(aw => aw.Website)
		.Include(x => x.Actors_Tags)
			.ThenInclude(at => at.Tag)
		.AsSplitQuery()
		.FirstOrDefaultAsync(cancellationToken);
}
```



### Available helpers

```c#
IQueryable<T> AsTracking<T>(this IQueryable<T> source, bool isTracked) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, ListSortDirection sortDirection = ListSortDirection.Ascending) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, bool isAscending = true) { .. }
```

# Upgrade instructions

## 0.6.3 &rarr; 0.7.0

Implement ExecuteSqlAsync(..) in your DbContext. Example implementation in your DbContext that inherits from IContext:

```c#
/// <inheritdoc/>
public async Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
{
	return await Database.ExecuteSqlAsync(sql, cancellationToken);
}
```

## 0.8.1 &rarr; 0.9.0

Files have been moved into a different folder and namespace. Update your usings.

Some files have been moved into a different project. You may also need the NuGet [Elephant.Database.Abstractions](https://www.nuget.org/packages/Elephant.Database.Abstractions) and also the NuGets [Elephant.Types.Results](https://www.nuget.org/packages/Elephant.Types.Results) and [Elephant.Types.Results.Abstractions](https://www.nuget.org/packages/Elephant.Types.Results.Abstractions).

The ResponseWrappers are no longer used.

## 0.9.0 &rarr; 0.10.0

Add the "Async" (without double quotes) to your calls and overrides.

## 0.10.3 &rarr; 1.0.0

Moving to 1.0.0 to mark the project's stability. The transition from 0.10.3 is fully compatible with no breaking changes.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../LICENSE.txt) file for details.
