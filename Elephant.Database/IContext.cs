using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elephant.Database
{
	/// <summary>
	/// Custom <see cref="DbContext"/> with transactions and interfaceable.
	/// </summary>
	public interface IContext
	{
		/// <summary>
		/// Creates a <see cref="DbSet{TEntity}" /> that can be used to query and save instances of <typeparamref name="TEntity" />.
		/// </summary>
		/// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
		DbSet<TEntity> Set<TEntity>()
			where TEntity : class;

		/// <summary>
		///     <para>
		///         Saves all changes made in this context to the database.
		///     </para>
		///     <para>
		///         This method will automatically call <see cref="ChangeTracker.DetectChanges" /> to discover any
		///         changes to entity instances before saving to the underlying database. This can be disabled via
		///         <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
		///     </para>
		///     <para>
		///         Entity Framework Core does not support multiple parallel operations being run on the same DbContext instance. This
		///         includes both parallel execution of async queries and any explicit concurrent use from multiple threads.
		///         Therefore, always await async calls immediately, or use separate DbContext instances for operations that execute
		///         in parallel. See <see href="https://aka.ms/efcore-docs-threading">Avoiding DbContext threading issues</see> for more information.
		///     </para>
		/// </summary>
		/// <remarks>
		///     See <see href="https://aka.ms/efcore-docs-saving-data">Saving data in EF Core</see> for more information.
		/// </remarks>
		/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <returns>
		///     A task that represents the asynchronous save operation. The task result contains the
		///     number of state entries written to the database.
		/// </returns>
		/// <exception cref="DbUpdateException">
		///     An error is encountered while saving to the database.
		/// </exception>
		/// <exception cref="DbUpdateConcurrencyException">
		///     A concurrency violation is encountered while saving to the database.
		///     A concurrency violation occurs when an unexpected number of rows are affected during save.
		///     This is usually because the data in the database has been modified since it was loaded into memory.
		/// </exception>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		/// <summary>
		///     <para>
		///         Gets an <see cref="EntityEntry" /> for the given entity. The entry provides
		///         access to change tracking information and operations for the entity.
		///     </para>
		///     <para>
		///         This method may be called on an entity that is not tracked. You can then
		///         set the <see cref="EntityEntry.State" /> property on the returned entry
		///         to have the context begin tracking the entity in the specified state.
		///     </para>
		/// </summary>
		/// <remarks>
		///     See <see href="https://aka.ms/efcore-docs-entity-entries">Accessing tracked entities in EF Core</see> for more information.
		/// </remarks>
		/// <param name="entity">The entity to get the entry for.</param>
		/// <returns>The entry for the given entity.</returns>
		EntityEntry Entry(object entity);

		/// <summary>
		/// Executes the given SQL against the database and returns the number of rows affected.
		/// Note that this method does not start a transaction. To use this method with a
		/// transaction, first call Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.BeginTransaction(Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade,System.Data.IsolationLevel)
		/// or UseTransaction.
		/// Note that the current Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy
		/// is not used by this method since the SQL may not be idempotent and does not run
		/// in a transaction. An Microsoft.EntityFrameworkCore.Storage.ExecutionStrategy
		/// can be used explicitly, making sure to also use a transaction if the SQL is not
		/// idempotent.
		///      var userSuppliedSearchTerm = ".NET";
		///      context.Database.ExecuteSqlRawAsync("UPDATE Blogs SET Rank = 50 WHERE Name
		/// = {0}", userSuppliedSearchTerm);
		/// Never pass a concatenated or interpolated string ($"") with non-validated user-provided
		/// values into this method. Doing so may expose your application to SQL injection
		/// attacks.
		/// </summary>
		Task ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);

		/// <summary>
		///     Executes the given SQL against the database and returns the number of rows affected.
		/// </summary>
		/// <remarks>
		///     <para>
		///         Note that this method does not start a transaction. To use this method with
		///         a transaction, first call <see cref="BeginTransaction" /> or <see cref="O:UseTransaction" />.
		///     </para>
		///     <para>
		///         Note that the current <see cref="ExecutionStrategy" /> is not used by this method
		///         since the SQL may not be idempotent and does not run in a transaction. An <see cref="ExecutionStrategy" />
		///         can be used explicitly, making sure to also use a transaction if the SQL is not
		///         idempotent.
		///     </para>
		///     <para>
		///         As with any API that accepts SQL it is important to parameterize any user input to protect against a SQL injection
		///         attack. You can include parameter place holders in the SQL query string and then supply parameter values as additional
		///         arguments. Any parameter values you supply will automatically be converted to a DbParameter.
		///     </para>
		///     <para>
		///         See <see href="https://aka.ms/efcore-docs-raw-sql">Executing raw SQL commands with EF Core</see>
		///         for more information and examples.
		///     </para>
		/// </remarks>
		/// <param name="sql">The interpolated string representing a SQL query with parameters.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
		/// <returns>
		///     A task that represents the asynchronous operation. The task result is the number of rows affected.
		/// </returns>
		/// <exception cref="OperationCanceledException">If the <see cref="CancellationToken" /> is canceled.</exception>
		Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default);

		#region Transactions

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.BeginTransactionAsync(CancellationToken)"/>
		Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken);

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.CommitTransactionAsync(CancellationToken)"/>
		Task CommitTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

		/// <inheritdoc cref="Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.RollbackTransactionAsync(CancellationToken)"/>
		Task RollbackTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken);

		#endregion
	}
}
