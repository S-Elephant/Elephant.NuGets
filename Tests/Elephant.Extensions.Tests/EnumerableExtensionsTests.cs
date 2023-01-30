namespace Elephant.Extensions.Tests
{
	/// <summary>
	/// <see cref="Enumerable"/> tests.
	/// </summary>
	public class EnumerableExtensionsTests
	{
		/// <summary>
		/// <see cref="Enumerable.None{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void NoneReturnsTrueIfListIsEmpty()
		{
			List<int> list = new ();

			Assert.True(list.None());
		}

		/// <summary>
		/// <see cref="Enumerable.IsEmpty{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsEmptyReturnsTrueIfListIsEmpty()
		{
			List<int> list = new ();

			Assert.True(list.IsEmpty());
		}

		/// <summary>
		/// <see cref="Enumerable.None{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void NoneReturnsFalseIfListIsNotEmpty()
		{
			List<int> list = new () { -10, 1, 2, 3 };

			Assert.False(list.None());
		}

		/// <summary>
		/// <see cref="Enumerable.IsEmpty{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsEmptyReturnsFalseIfListIsNotEmpty()
		{
			List<int> list = new () { -10, 1, 2, 3 };

			Assert.False(list.IsEmpty());
		}

		/// <summary>
		/// <see cref="Enumerable.None{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/> tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[InlineData(true, 0)]
		[InlineData(true, -1)]
		[InlineData(true, 10)]
		[InlineData(true, int.MinValue)]
		[InlineData(true, int.MaxValue)]
		[InlineData(false, 1)]
		[InlineData(false, 2)]
		[InlineData(false, 3)]
		[InlineData(false, -10)]
		public void NoneWithPredicateTest(bool expected, int value)
		{
			List<int> x = new () { -10, 1, 2, 3 };

			Assert.Equal(expected, x.None(x => x == value));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsTrueIfAllContained()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new () { 1, 5, 10, };

			Assert.True(source.ContainsAll(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsFalseIfNotAllContained()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new () { 1, 5, 10, 0, };

			Assert.False(source.ContainsAll(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsFalseIfSourceIsEmpty()
		{
			List<int> source = new ();
			List<int> values = new () { 0, };

			Assert.False(source.ContainsAll(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsTrueIfValuesIsEmpty()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new ();

			Assert.True(source.ContainsAll(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsFalseIfSourceContainsAll()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new () { 1, 5, 10, };

			Assert.False(source.ContainsNone(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsFalseIfSourceContainsAny()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new () { -1, 5, 100, };

			Assert.False(source.ContainsNone(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfSourceIsEmpty()
		{
			List<int> source = new ();
			List<int> values = new () { 0, };

			Assert.True(source.ContainsNone(values));
		}

		/// <summary>
		/// <see cref="Enumerable.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfValuesIsEmpty()
		{
			List<int> source = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new ();

			Assert.True(source.ContainsNone(values));
		}
	}
}
