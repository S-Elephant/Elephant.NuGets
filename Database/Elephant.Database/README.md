# About

Contains various EF Core related database helpers.



## DbSetExtensions

**Clear()**: Remove all entities from that DbSet.



## GenericCrudRepository

```c#
virtual Task<List<TEntity>> All(CancellationToken cancellationToken);

// Example usage: await myRepository.All(1, QueryTrackingBehavior.NoTracking, CancellationToken.None, x => x.CustomersCrossOrders, x => x.CustomersCrossAddresses);
virtual Task<List<TEntity>> All(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

virtual Task<TEntity?> ById(object id, CancellationToken cancellationToken);

virtual Task<int> Count(CancellationToken cancellationToken);

virtual Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

virtual void Delete(object id);

virtual Task<ResponseWrapper<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

virtual Task<bool> HasAny(CancellationToken cancellationToken);

virtual Task<bool> HasAny(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

virtual Task Insert(TEntity obj, CancellationToken cancellationToken);

virtual Task Insert(ICollection<TEntity> objects, CancellationToken cancellationToken);

virtual Task<ResponseWrapper<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

virtual Task<ResponseWrapper<int>> Save(CancellationToken cancellationToken);

virtual void Update(TEntity obj);

virtual void Update(ICollection<TEntity> objects);

virtual Task<ResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);

virtual Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo");

// Transactions:

virtual Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

virtual Task CommitTransactionAsyncAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

virtual Task RollbackTransactionAsyncAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);
```

### Example IContext transaction implementation

```c#
// Usings
using Elephant.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

// Inside your DbContext or IdentityDbContext class:

/// <inheritdoc/>
public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
{
	return await Database.BeginTransactionAsync(cancellationToken);
}

/// <inheritdoc/>
public async Task CommitTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
{
	if (transaction == null)
		return;

	await Database.CommitTransactionAsync(cancellationToken);
	await transaction.DisposeAsync();
}

/// <inheritdoc/>
public async Task RollbackTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
{
	if (transaction == null)
		return;

	await Database.RollbackTransactionAsync(cancellationToken);
	await transaction.DisposeAsync();
}

```



### Example transaction in your service

```c#
using (IDbContextTransaction transaction = await _customerRepository.BeginTransaction(cancellationToken))
{
    try
    {
        // Your code here.
    }
    catch (Exception exception)
    {
        _logger.LogError($"An error occurred. Rolling back changes. Exception: {exception}".);
        await _customerRepository.RollbackTransactionAndDispose(transaction, cancellationToken);
        return;
    }

    // Perform CRUD actions or whatever.
	await _customerRepository.Save(cancellationToken);
	await _customerRepository.CommitTransactionAndDispose(transaction, cancellationToken);
}
```



## GenericCrudIdRepository

All methods as listed in [GenericCrudRepository](##GenericCrudRepository) plus:

```c#
virtual Task<T?> ById(int id, QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);

virtual Task<bool> HasId(int id, CancellationToken cancellationToken);

virtual Task<int> HighestId(CancellationToken cancellationToken);

virtual Task<int> LowestId(CancellationToken cancellationToken);
virtual Task<int> NextId(int sourceId, bool cycle, CancellationToken cancellationToken);
virtual Task<int> PreviousId(int sourceId, bool cycle, CancellationToken cancellationToken);
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

