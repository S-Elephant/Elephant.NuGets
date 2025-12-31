namespace Elephant.Extensions.Tests.EnumerableTests
{
	/// <summary>
	/// <see cref="EnumerableExtensions.SplitIntoChunks{T}(IEnumerable{T}, int)"/> tests.
	/// </summary>
	/// <remarks>
	/// To avoid duplicating each test for <![CDATA[List<T>, IEnumerable<T> and IList<T>]]>
	/// (or creating many MemberData providers), some tests exercise the method with multiple
	/// collection types and therefore contain multiple Act/Assert sections.
	/// </remarks>
	public class SplitIntoChunksTests
	{
		/// <summary>
		/// <see cref="EnumerableExtensions.SplitIntoChunks{T}(IEnumerable{T}, int)"/> normal behaviour.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsExpectedChunks()
		{
			// Arrange.
			List<int> sourceList = new() { 1, 2, 3, 4, 5, 6, 7 };
			IEnumerable<int> sourceEnumerable = [.. sourceList];
			IList<int> sourceIList = [.. sourceList];

			// Act: for all supported types.
			List<List<int>> chunkedList = sourceList.SplitIntoChunks(3).ToList();
			List<List<int>> chunkedEnumerable = sourceEnumerable.SplitIntoChunks(3).ToList();
			List<List<int>> chunkedIList = sourceIList.SplitIntoChunks(3).ToList();

			// Assert: for all supported types.
			Assert.Equal(3, chunkedList.Count);
			Assert.Equal(new List<int> { 1, 2, 3 }, chunkedList[0]);
			Assert.Equal(new List<int> { 4, 5, 6 }, chunkedList[1]);
			Assert.Equal(new List<int> { 7 }, chunkedList[2]);
			Assert.Equal(chunkedList, chunkedEnumerable);
			Assert.Equal(chunkedList, chunkedIList);
		}

		/// <summary>
		/// Null source should throw <see cref="ArgumentNullException"/>.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void NullSource_Throws()
		{
			// Arrange.
			List<int>? sourceList = null;
			IEnumerable<int>? sourceEnumerable = null;
			IList<int>? sourceIList = null;

			// Act & Assert: for all supported types.
			_ = Assert.Throws<ArgumentNullException>(() => sourceList!.SplitIntoChunks(3).ToList());
			_ = Assert.Throws<ArgumentNullException>(() => sourceEnumerable!.SplitIntoChunks(3).ToList());
			_ = Assert.Throws<ArgumentNullException>(() => sourceIList!.SplitIntoChunks(3).ToList());
		}

		/// <summary>
		/// Size 1 returns each item in its own chunk
		/// for all supported <see cref="IEnumerable{T}"/> types.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SizeOne_EachItemOwnChunk()
		{
			// Arrange.
			List<int> sourceList = new() { int.MinValue, 1, 2, 3 };
			IEnumerable<int> sourceEnumerable = [.. sourceList];
			IList<int> sourceIList = [.. sourceList];

			// Act: for all supported types.
			List<List<int>> chunkedList = sourceList.SplitIntoChunks(1).ToList();
			List<List<int>> chunkedEnumerable = sourceEnumerable.SplitIntoChunks(1).ToList();
			List<List<int>> chunkedIList = sourceIList.SplitIntoChunks(1).ToList();

			// Assert: for all supported types.
			Assert.Equal(4, chunkedList.Count);
			Assert.All(chunkedList, c => Assert.Single(c));
			Assert.Equal(new List<int> { int.MinValue, 1, 2, 3 }, chunkedList.SelectMany(c => c).ToList());
			Assert.Equal(chunkedList, chunkedEnumerable);
			Assert.Equal(chunkedList, chunkedIList);
		}

		/// <summary>
		/// Size greater than source length returns a single chunk with all items
		/// for all supported <see cref="IEnumerable{T}"/> types.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void SizeGreaterThanCount_ReturnsSingleChunk()
		{
			// Arrange.
			List<int> sourceList = new() { int.MinValue, 1, 2, 3 };
			IEnumerable<int> sourceEnumerable = [.. sourceList];
			IList<int> sourceIList = [.. sourceList];

			// Act: for all supported types.
			List<List<int>> chunkedList = sourceList.SplitIntoChunks(10).ToList();
			List<List<int>> chunkedEnumerable = sourceEnumerable.SplitIntoChunks(10).ToList();
			List<List<int>> chunkedIList = sourceIList.SplitIntoChunks(10).ToList();

			// Assert: for all supported types.
			_ = Assert.Single(chunkedList);
			Assert.Equal(new List<int> { int.MinValue, 1, 2, 3 }, chunkedList[0]);
			Assert.Equal(chunkedList, chunkedEnumerable);
			Assert.Equal(chunkedList, chunkedIList);
		}

		/// <summary>
		/// Zero and negative chunk sizes throw <see cref="ArgumentOutOfRangeException"/>
		/// for all supported <see cref="IEnumerable{T}"/> types.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0)]
		[InlineData(-1)]
		[InlineData(int.MinValue)]
		public void ZeroAndNegativeSize_Throws(int size)
		{
			// Arrange.
			List<int> sourceList = new() { 1, 2, 3 };
			IEnumerable<int> sourceEnumerable = [.. sourceList];
			IList<int> sourceIList = [.. sourceList];

			// Act & Assert: for all supported types.
			_ = Assert.Throws<ArgumentOutOfRangeException>(() => sourceList.SplitIntoChunks(size).ToList());
			_ = Assert.Throws<ArgumentOutOfRangeException>(() => sourceEnumerable.SplitIntoChunks(size).ToList());
			_ = Assert.Throws<ArgumentOutOfRangeException>(() => sourceIList.SplitIntoChunks(size).ToList());
		}
	}
}
