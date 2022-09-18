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

void Delete(object id);

Task<IResultStatus<int>> DeleteAndSave(object id, CancellationToken cancellationToken);

Task Insert(TEntity obj, CancellationToken cancellationToken);

Task<IResultStatus<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken);

Task<IResultStatus<int>> Save(CancellationToken cancellationToken);

void Update(TEntity obj);

Task<IResultStatus<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken);

Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo");
```

## QueryableExtensions

```c#
IQueryable<T> AsTracking<T>(this IQueryable<T> source, bool isTracked) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, ListSortDirection sortDirection = ListSortDirection.Ascending) { .. }

IQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnName, bool isAscending = true) { .. }