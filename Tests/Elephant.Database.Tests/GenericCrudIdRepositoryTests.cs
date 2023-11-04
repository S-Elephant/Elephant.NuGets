using System.Threading;
using System.Threading.Tasks;
using Elephant.Database.Abstractions.Repositories;
using Elephant.Database.Repositories;
using Elephant.Database.Tests.InMemory;

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
		/// <see cref="IGenericCrudIdRepository{TEntity}.HighestId"/> test with records.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task HighestIdReturns5()
		{
			// Arrange.
			await _systemUnderTest.Seed(CancellationToken.None);

			// Act.
			int highestId = await _systemUnderTest.HighestId(CancellationToken.None);

			// Assert.
			Assert.Equal(5, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.HighestId"/> test without records.
		/// </summary>
		[Fact]
		[SpeedVeryFast, IntegrationTest]
		public async Task HighestIdReturnsMinus1()
		{
			// Act.
			int highestId = await _systemUnderTest.HighestId(CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.NextId"/> test with records.
		/// </summary>
		[Theory]
		[SpeedVeryFast, IntegrationTest]
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
			int highestId = await _systemUnderTest.NextId(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(expectedId, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.NextId"/> test without records.
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
			int highestId = await _systemUnderTest.NextId(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.PreviousId"/> test with records.
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
			int highestId = await _systemUnderTest.PreviousId(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(expectedId, highestId);
		}

		/// <summary>
		/// <see cref="IGenericCrudIdRepository{TEntity}.PreviousId"/> test without records.
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
			int highestId = await _systemUnderTest.PreviousId(sourceId, cycle, CancellationToken.None);

			// Assert.
			Assert.Equal(-1, highestId);
		}
	}
}