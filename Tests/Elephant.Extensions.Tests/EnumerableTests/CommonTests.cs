// ReSharper disable CollectionNeverUpdated.Local

namespace Elephant.Extensions.Tests.EnumerableTests
{
	/// <summary>
	/// <see cref="EnumerableExtensions"/> tests.
	/// </summary>
	public class CommonTests
	{
		/// <summary>
		/// <see cref="EnumerableExtensions.None{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void NoneReturnsTrueIfListIsEmpty()
		{
			List<int> list = new();

			Assert.True(list.None());
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsEmpty{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsEmptyReturnsTrueIfListIsEmpty()
		{
			List<int> list = new();

			Assert.True(list.IsEmpty());
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.None{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void NoneReturnsFalseIfListIsNotEmpty()
		{
			List<int> list = new() { -10, 1, 2, 3 };

			Assert.False(list.None());
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsEmpty{TSource}(IEnumerable{TSource})"/> tests.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsEmptyReturnsFalseIfListIsNotEmpty()
		{
			List<int> list = new() { -10, 1, 2, 3 };

			Assert.False(list.IsEmpty());
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.None{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/> tests.
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
			// Arrange.
			List<int> x = new() { -10, 1, 2, 3 };

			// Act.
			bool containsNone = x.None(y => y == value);

			// Assert.
			Assert.Equal(expected, containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsTrueIfAllContained()
		{
			// Arange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new() { 1, 5, 10, };

			// Act.
			bool containsAll = source.ContainsAll(values);

			// Assert.
			Assert.True(containsAll);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsFalseIfNotAllContained()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new() { 1, 5, 10, 0, };

			// Act.
			bool containsAll = source.ContainsAll(values);

			// Assert.
			Assert.False(containsAll);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsFalseIfSourceIsEmpty()
		{
			// Arrange.
			List<int> source = new();
			List<int> values = new() { 0, };

			// Act.
			bool containsAll = source.ContainsAll(values);

			// Assert.
			Assert.False(containsAll);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsTrueIfValuesIsEmpty()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new();

			// Act.
			bool containsall = source.ContainsAll(values);

			// Assert.
			Assert.True(containsall);
		}


		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsAll{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
		/// returns false if source is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsAllReturnsFalseIfSourceNull()
		{
			// Arrange.
			List<int> source = null!;
			List<int> values = new();

			// Act.
			bool containsall = source.ContainsAll(values);

			// Assert.
			Assert.False(containsall);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsFalseIfSourceContainsAll()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new() { 1, 5, 10, };

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.False(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsFalseIfSourceContainsAny()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new() { -1, 5, 100, };

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.False(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfSourceIsEmpty()
		{
			// Arrange.
			List<int> source = new();
			List<int> values = new() { 0, };

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.True(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/> test.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfValuesIsEmpty()
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };
			List<int> values = new();

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.True(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
		/// returns true if source is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfSourceIsNull()
		{
			// Arrange.
			List<int> source = null!;
			List<int> values = new(1);

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.True(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.ContainsNone{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>
		/// returns true if source is null and if values is empty.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ContainsNoneReturnsTrueIfSourceIsNullAndValuesIsEmpty()
		{
			// Arrange.
			List<int> source = null!;
			List<int> values = new();

			// Act.
			bool containsNone = source.ContainsNone(values);

			// Assert.
			Assert.True(containsNone);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsFirst{TSource}"/> tests.
		/// </summary>
		[Theory]
		[InlineData(1, true)]
		[InlineData(2, false)]
		[InlineData(9, false)]
		[InlineData(10, false)]
		[InlineData(11, false)]
		[InlineData(int.MinValue, false)]
		[InlineData(int.MaxValue, false)]
		[SpeedVeryFast, UnitTest]
		public void IsFirstTests(int itemToCompare, bool expectedValue)
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

			// Act.
			bool isFirst = source.IsFirst(itemToCompare);

			// Assert.
			Assert.Equal(expectedValue, isFirst);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsFirst{TSource}"/>
		/// returns false if empty.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsFirstReturnsFalseIfEmpty()
		{
			// Arrange.
			List<int> source = new();

			// Act.
			bool isFirst = source.IsFirst(10);

			// Assert.
			Assert.False(isFirst);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsFirst{TSource}"/>
		/// returns false if source is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsFirstReturnsFalseIfSourceNull()
		{
			// Arrange.
			List<int> source = null!;

			// Act.
			bool isFirst = source.IsFirst(10);

			// Assert.
			Assert.False(isFirst);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}"/> tests.
		/// </summary>
		[Theory]
		[InlineData(1, false)]
		[InlineData(2, false)]
		[InlineData(9, false)]
		[InlineData(10, true)]
		[InlineData(11, false)]
		[InlineData(int.MinValue, false)]
		[InlineData(int.MaxValue, false)]
		[SpeedVeryFast, UnitTest]
		public void IsLastTests(int itemToCompare, bool expectedValue)
		{
			// Arrange.
			List<int> source = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, };

			// Act.
			bool isLast = source.IsLast(itemToCompare);

			// Assert.
			Assert.Equal(expectedValue, isLast);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsFirst{TSource}"/> test with a simple class.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsFirstSimpleClassReturnsTrueIfFirst()
		{
			// Arrange.
			List<SimpleClass> source = new()
			{
				new SimpleClass(),
				new SimpleClass(),
				new SimpleClass(),
			};

			SimpleClass first = source[0];

			// Act.
			bool isFirst = source.IsFirst(first);

			// Assert.
			Assert.True(isFirst);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsFirst{TSource}"/> test with a simple class.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsFirstSimpleClassReturnsFalseIfNotFirst()
		{
			// Arrange.
			List<SimpleClass> source = new()
			{
				new SimpleClass(),
				new SimpleClass(),
				new SimpleClass(),
			};

			SimpleClass last = source[^1];

			// Act.
			bool isFirst = source.IsFirst(last);

			// Assert.
			Assert.False(isFirst);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}"/> test with a simple class.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsLastSimpleClassReturnsTrueIfFirst()
		{
			// Arrange.
			List<SimpleClass> source = new()
			{
				new SimpleClass(),
				new SimpleClass(),
				new SimpleClass(),
			};

			SimpleClass last = source[^1];

			// Act.
			bool isLast = source.IsLast(last);

			// Assert.
			Assert.True(isLast);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}"/> test with a simple class.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsLastSimpleClassReturnsFalseIfNotFirst()
		{
			// Arrange.
			List<SimpleClass> source = new()
			{
				new SimpleClass(),
				new SimpleClass(),
				new SimpleClass(),
			};

			SimpleClass first = source[0];

			// Act.
			bool isLast = source.IsLast(first);

			// Assert.
			Assert.False(isLast);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}"/> test with a simple class and a null value.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsLastSimpleClassReturnsFalseIfNull()
		{
			// Arrange.
			List<SimpleClass> source = new()
			{
				new SimpleClass(),
				new SimpleClass(),
				new SimpleClass(),
			};

			SimpleClass? itemToCompare = null;

			// Act.
			bool isLast = source.IsLast(itemToCompare);

			// Assert.
			Assert.False(isLast);
		}

		/// <summary>
		/// Simple class used for testing.
		/// </summary>
		private sealed class SimpleClass
		{
			/// <summary>
			/// Test value.
			/// </summary>
			// ReSharper disable once UnusedMember.Local
			public string A { get; set; } = "a";
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}(IEnumerable{TSource}, TSource)"/>
		/// returns false if empty.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsLastReturnsFalseIfEmpty()
		{
			// Arrange.
			List<int> source = new();

			// Act.
			bool isLast = source.IsFirst(10);

			// Assert.
			Assert.False(isLast);
		}

		/// <summary>
		/// <see cref="EnumerableExtensions.IsLast{TSource}(IEnumerable{TSource}, TSource)"/>
		/// returns false if source is null.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsLastReturnsFalseIfSourceNull()
		{
			// Arrange.
			List<int> source = null!;

			// Act.
			bool isLast = source.IsFirst(10);

			// Assert.
			Assert.False(isLast);
		}
	}
}
