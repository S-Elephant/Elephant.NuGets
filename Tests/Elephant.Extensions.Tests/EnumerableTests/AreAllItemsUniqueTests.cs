namespace Elephant.Extensions.Tests.EnumerableTests
{
	/// <summary>
	/// <see cref="Enumerable.AreAllItemsUnique{TSource}(IEnumerable{TSource}?)"/> tests.
	/// </summary>
	public class AreAllItemsUniqueTests
	{
		/// <summary>
		/// <see cref="Enumerable.AreAllItemsUnique{TSource}"/>
		/// returns true if all items are unique.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfAllUnique()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5 };

			// Act.
			bool areAllItemsUnique = source.AreAllItemsUnique();

			// Assert.
			Assert.True(areAllItemsUnique);
		}

		/// <summary>
		/// <see cref="Enumerable.AreAllItemsUnique{TSource}"/>
		/// returns false if not all items are unique.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsFalseIfNotAllUnique()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 4, 3, 4, 5 };

			// Act.
			bool areAllItemsUnique = source.AreAllItemsUnique();

			// Assert.
			Assert.False(areAllItemsUnique);
		}

		/// <summary>
		/// <see cref="Enumerable.AreAllItemsUnique{TSource}"/>
		/// returns true if source is empty.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfEmpty()
		{
			// Arrange.
			List<int> source = new();

			// Act.
			bool areAllItemsUnique = source.AreAllItemsUnique();

			// Assert.
			Assert.True(areAllItemsUnique);
		}

		/// <summary>
		/// <see cref="Enumerable.AreAllItemsUnique{TSource}"/>
		/// returns true if source is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfNull()
		{
			// Arrange.
			List<int>? source = null;

			// Act.
			bool areAllItemsUnique = source.AreAllItemsUnique();

			// Assert.
			Assert.True(areAllItemsUnique);
		}
	}
}
