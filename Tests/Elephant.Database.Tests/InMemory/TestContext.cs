using System;
using System.Threading;
using System.Threading.Tasks;
using Elephant.Database.Abstractions.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Elephant.Database.Tests.InMemory
{
	/// <summary>
	/// Test context.
	/// </summary>
	internal class TestContext : DbContext, IContext
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public TestContext(DbContextOptions<TestContext> dbContextOptions)
		: base(dbContextOptions)
		{
		}

		/// <inheritdoc/>
		public Task ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<int> ExecuteSqlAsync(FormattableString sql, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task CommitTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public Task RollbackTransactionAndDispose(IDbContextTransaction? transaction, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TestEntity>()
				.HasKey(p => p.Id);
		}
	}
}
