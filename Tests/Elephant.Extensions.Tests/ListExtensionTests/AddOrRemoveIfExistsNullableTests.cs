namespace Elephant.Extensions.Tests.ListExtensionTests
{
	/// <summary>
	/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> tests.
	/// </summary>
	public class AddOrRemoveIfExistsNullableTests
	{
		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should remove a number.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ValueIsRemoved1()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3 };

			// Act.
			_ = list.AddOrRemoveIfExists(1);

			// Assert.
			Assert.Equal(new List<int>() { -10, 2, 3 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should remove a number.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ValueIsRemovedminus10()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3 };

			// Act.
			_ = list.AddOrRemoveIfExists(-10);

			// Assert.
			Assert.Equal(new List<int>() { 1, 2, 3 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should add a number.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ValueIsAdded4()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3 };

			// Act.
			_ = list.AddOrRemoveIfExists(4);

			// Assert.
			Assert.Equal(new List<int>() { -10, 1, 2, 3, 4 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should add a number.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ValueIsAddedMinus2()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3 };

			// Act.
			_ = list.AddOrRemoveIfExists(-2);

			// Assert.
			Assert.Equal(new List<int>() { -10, 1, 2, 3, -2 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should add a number.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ValueIsAddedToEmptyList()
		{
			// Arrange.
			List<int> list = new();

			// Act.
			_ = list.AddOrRemoveIfExists(0);

			// Assert.
			Assert.Equal(new List<int>() { 0 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test that should do nothing.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void DoesNothingIfListIsNull()
		{
			// Arrange.
			List<int>? list = null;

			// Act.
			_ = list.AddOrRemoveIfExistsNullable(0);

			// Assert.
			Assert.Null(list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/> test.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void AddOrRemoveIfExists_ThrowsWhenListIsNull()
		{
			// Arrange.
			List<int>? list = null;

			// Act & Assert.
			_ = Assert.Throws<NullReferenceException>(() => list!.AddOrRemoveIfExists(1));
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/>
		/// with duplicates then only the first occurrence is removed by the current implementation.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void RemovesOnlyFirstOccurrenceWhenDuplicatesExist()
		{
			// Arrange.
			List<int> list = new() { 1, 2, 1, 3 };

			// Act.
			_ = list.AddOrRemoveIfExists(1);

			// Assert.
			Assert.Equal(new List<int>() { 2, 1, 3 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExistsNullable{TSource}(IList{TSource}?, TSource)"/>
		/// returns the same instance when list is non-null and returns null when the input is null.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void AddOrRemoveIfExistsNullable_ReturnsSameInstanceOrNull()
		{
			// Arrange.
			List<int> list = new() { 1 };

			// Act.
			IList<int>? result = list.AddOrRemoveIfExistsNullable(2);

			// Assert: same.
			Assert.Same(list, result);
			List<int>? nullList = null;

			// Assert: null.
			IList<int>? result2 = nullList.AddOrRemoveIfExistsNullable(0);
			Assert.Null(result2);
		}
	}
}
