[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Database)](https://www.nuget.org/packages/Elephant.Database/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Database.svg)](https://www.nuget.org/packages/Elephant.Database/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Database/LICENSE.txt)

# About

Contains various EF Core related database helpers.



## DbSetExtensions

**Clear()**: Remove all entities from that DbSet.



## GenericCrudRepository

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

All methods as listed in [GenericCrudRepository](##GenericCrudRepository) plus:

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

All methods as listed in [GenericCrudRepository](##GenericCrudRepository) plus:

```c#
virtual Task<TEntity?> ByIdAsync(Guid id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

virtual Task<bool> HasIdAsync(Guid id, CancellationToken cancellationToken);
```



## QueryableExtensions

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
