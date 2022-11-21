# About

Contains various EF Core related database helpers.

## DbSetExtensions

**Clear()**: Remove all entities from that DbSet.

## GenericCrud

```c#
Task<List<TEntity>> All(CancellationToken cancellationToken);

// Example usage: await myRepository.All(1, QueryTrackingBehavior.NoTracking, CancellationToken.None, x => x.CustomersCrossOrders, x => x.CustomersCrossAddresses);
Task<List<TEntity>> All(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

Task<TEntity?> ById(object id, CancellationToken cancellationToken);

Task<int> Count(CancellationToken cancellationToken);

Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

void Delete(object id);

Task<ResponseWrapper<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

Task<bool> HasAny(CancellationToken cancellationToken);

Task<bool> HasAny(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

Task Insert(TEntity obj, CancellationToken cancellationToken);

Task Insert(ICollection<TEntity> objects, CancellationToken cancellationToken);

Task<ResponseWrapper<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

Task<ResponseWrapper<int>> Save(CancellationToken cancellationToken);

void Update(TEntity obj);

void Update(ICollection<TEntity> objects);

Task<ResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);

Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo");

// Transactions:

Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

Task CommitTransactionAsyncAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

Task RollbackTransactionAsyncAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);
```

## QueryableExtensions

```c#
IQueryable<T> AsTracking<T>(this IQueryable<T> source, bool isTracked) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, ListSortDirection sortDirection = ListSortDirection.Ascending) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, bool isAscending = true) { .. }