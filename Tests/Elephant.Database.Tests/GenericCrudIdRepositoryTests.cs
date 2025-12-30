using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elephant.Database.Abstractions.Repositories;
using Elephant.Database.Repositories;
using Elephant.Database.Tests.InMemory;
using Microsoft.EntityFrameworkCore;

namespace Elephant.Database.Tests
{
	/// <summary>
	/// <see cref="GenericCrudIdRepository{TEntity,TContext}"/> tests.
	/// </summary>
	public class GenericCrudIdRepositoryTests
	{
		private readonly TestRepository _systemUnderTest;

		/// <summary>
		/// Setup.
		/// </summary>
		public GenericCrudIdRepositoryTests()
		{
			_systemUnderTest = new TestRepository(new DatabaseHelper().TestContext);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.OverflowingIds"/>
		/// deletes and returns count with custom ordering.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task DeleteOverflowingEntities_CustomOrdering_DeletesAndReturnsCount()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Custom ordering: descending by Id.
			static IOrderedQueryable<TestEntity> TestOrderBy(IQueryable<TestEntity> q)
			{
				return q.OrderByDescending(x => x.Id);
			}

			// Act.
			int recordsDeletedCount = await _systemUnderTest.DeleteOverflowingEntities(3, TestOrderBy, CancellationToken.None);

			// Assert.
			Assert.Equal(2, recordsDeletedCount); // With descending order overflow should be ids 2 and 1.

			// Assert: remaining count should be 3 and highest id remains 5 (we deleted lower ids).
			int remainingCount = await _systemUnderTest.CountAsync(CancellationToken.None);
			Assert.Equal(3, remainingCount);
			int highest = await _systemUnderTest.HighestIdAsync(CancellationToken.None);
			Assert.Equal(5, highest);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.OverflowingIds"/>
		/// delete and return count with default ordering.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task DeleteOverflowingEntities_DefaultOrdering_DeletesAndReturnsCount()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			int recordsDeletedCount = await _systemUnderTest.DeleteOverflowingEntities(3, null, CancellationToken.None);

			// Assert.
			Assert.Equal(2, recordsDeletedCount); // Two entities (ids 4 and 5) should have been removed.

			// Assert: verify table now contains only first 3 items.
			int remainingCount = await _systemUnderTest.CountAsync(CancellationToken.None);
			Assert.Equal(3, remainingCount);

			// Assert: highestId should now be 3.
			int highest = await _systemUnderTest.HighestIdAsync(CancellationToken.None);
			Assert.Equal(3, highest);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.HighestIdAsync"/> test with records.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task HighestIdReturns5()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			int highestId = await _systemUnderTest.HighestIdAsync(CancellationToken.None);

			// Assert.
			Assert.Equal(5, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.HighestIdAsync"/> test without records.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task HighestIdReturnsMinus1()
		{
			// Act.
			int highestId = await _systemUnderTest.HighestIdAsync(CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.NextIdAsync"/> test with records.
		/// </summary>
		[Theory]
		[SpeedNormal, IntegrationTest]
		[InlineData(1, true, 2)]
		[InlineData(2, true, 3)]
		[InlineData(3, true, 4)]
		[InlineData(4, true, 5)]
		[InlineData(5, true, 1)]
		[InlineData(1, false, 2)]
		[InlineData(2, false, 3)]
		[InlineData(3, false, 4)]
		[InlineData(4, false, 5)]
		[InlineData(5, false, -1)]
		public async Task NextIdIdTestsWithRecords(int sourceId, bool cycle, int expectedId)
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			int highestId = await _systemUnderTest.NextIdAsync(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(expectedId, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.NextIdAsync"/> test without records.
		/// </summary>
		[Theory]
		[SpeedVerySlow, IntegrationTest]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, true)]
		[InlineData(4, true)]
		[InlineData(5, true)]
		[InlineData(1, false)]
		[InlineData(2, false)]
		[InlineData(3, false)]
		[InlineData(4, false)]
		[InlineData(5, false)]
		public async Task NextIdIdTestsWithoutRecords(int sourceId, bool cycle)
		{
			// Act.
			int highestId = await _systemUnderTest.NextIdAsync(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.OverflowingIds"/>
		/// returns ids beyond max with custom ordering.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task OverflowingIds_CustomOrdering_ReturnsIdsBeyondMax()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Custom ordering: descending by Id (ties broken by Id implicitly).
			static IOrderedQueryable<TestEntity> TestOrderBy(IQueryable<TestEntity> q)
			{
				return q.OrderByDescending(x => x.Id);
			}

			// Act.
			List<int> overflowing = await _systemUnderTest.OverflowingIds(3, TestOrderBy, QueryTrackingBehavior.NoTracking, CancellationToken.None);

			// Assert.
			Assert.Equal(new List<int> { 2, 1 }, overflowing); // Ordering desc -> sequence [5,4,3,2,1], keep first 3 => overflow [2,1].
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.OverflowingIds"/> test
		/// with default ordering.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task OverflowingIds_DefaultOrdering_ReturnsIdsBeyondMax()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			List<int> overflowing = await _systemUnderTest.OverflowingIds(3, null, QueryTrackingBehavior.NoTracking, CancellationToken.None);

			// Assert.
			Assert.Equal(new List<int> { 4, 5 }, overflowing); // Default ordering is by Id ascending, so with 5 seeded items and maxItems=3 the overflow is [4,5].
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.PreviousIdAsync"/> test with records.
		/// </summary>
		[Theory]
		[SpeedVeryFast, IntegrationTest]
		[InlineData(1, true, 5)]
		[InlineData(2, true, 1)]
		[InlineData(3, true, 2)]
		[InlineData(4, true, 3)]
		[InlineData(5, true, 4)]
		[InlineData(1, false, -1)]
		[InlineData(2, false, 1)]
		[InlineData(3, false, 2)]
		[InlineData(4, false, 3)]
		[InlineData(5, false, 4)]
		public async Task PreviousIdIdTestsWithRecords(int sourceId, bool cycle, int expectedId)
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			int highestId = await _systemUnderTest.PreviousIdAsync(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(expectedId, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.PreviousIdAsync"/> test without records.
		/// </summary>
		[Theory]
		[SpeedVeryFast, IntegrationTest]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, true)]
		[InlineData(4, true)]
		[InlineData(5, true)]
		[InlineData(1, false)]
		[InlineData(2, false)]
		[InlineData(3, false)]
		[InlineData(4, false)]
		[InlineData(5, false)]
		public async Task PreviousIdIdTestsWithoutRecords(int sourceId, bool cycle)
		{
			// Act.
			int highestId = await _systemUnderTest.PreviousIdAsync(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}
	}
}