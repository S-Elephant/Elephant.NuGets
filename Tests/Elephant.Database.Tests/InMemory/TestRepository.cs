using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elephant.Database.Repositories;

namespace Elephant.Database.Tests.InMemory
{
	/// <summary>
	/// Test <see cref="GenericCrudIdRepository{TEntity,TContext}"/>.
	/// </summary>
	internal class TestRepository : GenericCrudIdRepository<TestEntity, TestContext>
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public TestRepository(TestContext testContext)
			: base(testContext)
		{
		}

		/// <summary>
		/// Seed 5 entities with id's from 1-5 (inclusive).
		/// </summary>
		public async Task Seed(CancellationToken cancellationToken)
		{
			await Table.AddRangeAsync(new List<TestEntity>
				{
					new(1, "A"),
					new(2, "B"),
					new(3, "C"),
					new(4, "D"),
					new(5, "E"),
				},
				cancellationToken);

			await SaveAsync(cancellationToken);
		}
	}
}
