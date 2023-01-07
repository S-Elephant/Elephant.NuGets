using Elephant.Types.ResponseWrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Elephant.Database
{
    /// <inheritdoc cref="IGenericCrudRepository{TEntity}"/>
    public abstract class GenericCrudRepository<TEntity, TContext> : IGenericCrudRepository<TEntity>
        where TEntity : class
        where TContext : IContext
    {
        /// <summary>
        /// Main database context.
        /// </summary>
        protected TContext Context { get; private set; }

        /// <summary>
        /// Table/DbSet to use for this repository.
        /// </summary>
        protected DbSet<TEntity> Table { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public GenericCrudRepository(TContext mainContext)
        {
            Context = mainContext;
            Table = mainContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> ById(object id, CancellationToken cancellationToken)
        {
            // Note that the object[] array is required because otherwise it will consider the cancellationToken as the second id field in the params list. See: https://stackoverflow.com/questions/55758059/get-error-entity-type-course-is-defined-with-a-single-key-property-but-2-va
            return await Table.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> Count(CancellationToken cancellationToken)
        {
            return await Table.CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Table.CountAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task Insert(TEntity obj, CancellationToken cancellationToken) => await Table.AddAsync(obj, cancellationToken);

        /// <inheritdoc/>
        public virtual async Task Insert(ICollection<TEntity> objects, CancellationToken cancellationToken)
        {
            foreach (TEntity objectToInsert in objects)
            {
                await Insert(objectToInsert, cancellationToken);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<ResponseWrapper<int>> Save(CancellationToken cancellationToken)
        {
            int recordsAffectedCount = await Context.SaveChangesAsync(cancellationToken);
            return new ResponseWrapper<int>(recordsAffectedCount);
        }

        /// <inheritdoc/>
        public virtual void Update(TEntity obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            Table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public virtual void Update(ICollection<TEntity> objects)
        {
            foreach (TEntity objectToUpdate in objects)
                Update(objectToUpdate);
        }

        /// <inheritdoc/>
        public virtual bool Delete(object id)
        {
            TEntity? existing = Table.Find(id);
            if (existing == null)
                return false; ;

            Table.Remove(existing);
            return true;
        }

        /// <inheritdoc/>
        public virtual async Task<ResponseWrapper<int>> DeleteAndSave(object id, CancellationToken cancellationToken)
        {
            bool isDeleted = Delete(id);

            if (isDeleted)
                return await Save(cancellationToken);

            return ResponseWrapper<int>.NewNotFound();
        }

        /// <inheritdoc/>
        public virtual async Task<bool> HasAny(CancellationToken cancellationToken)
        {
            return await Table.AnyAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> HasAny(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Table.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<ResponseWrapper<int>> InsertAndSave(TEntity obj, CancellationToken cancellationToken)
        {
            await Insert(obj, cancellationToken);
            ResponseWrapper<int> result = await Save(cancellationToken);
            return (result.Data == 0) ? result.InternalServerError("Nothing to insert.") : result;
        }

        /// <inheritdoc/>
        public virtual async Task<ResponseWrapper<int>> UpdateAndSave(TEntity obj, CancellationToken cancellationToken)
        {
            Update(obj);
            return await Save(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> All(CancellationToken cancellationToken) => await Table.ToListAsync(cancellationToken);

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> All(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.TrackAll, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            return await includes
                .Aggregate(Table.AsQueryable(), (current, include) => current.Include(include))
                .AsTracking(queryTrackingBehavior)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAllAndResetAutoIncrement(CancellationToken cancellationToken = default, string schema = "dbo")
        {
            string tableName = typeof(TEntity).Name;
            // Don't use truncate, use delete instead. See: https://stackoverflow.com/questions/253849/cannot-truncate-table-because-it-is-being-referenced-by-a-foreign-key-constraint
            // These are both DDL commands. Saving is not required.
            string sql = $"DELETE FROM [{schema}].[{tableName}];DBCC CHECKIDENT ([{schema}.{tableName}], RESEED, 0)";
            await Context.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        #region Transactions

        // Important note: Be sure to use the transactions with a using(..) or a try-catch-finally because otherwise it won't dispose the IDbContextTransaction if it were to crash.

        /// <inheritdoc/>
        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            return await Context.BeginTransaction(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task CommitTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            await Context.CommitTransactionAndDispose(transaction, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task RollbackTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
        {
            await Context.RollbackTransactionAndDispose(transaction, cancellationToken);
        }

        #endregion
    }
}