namespace Elephant.Extensions.Tests.ListExtensionTests
{
	/// <summary>
	/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> tests.
	/// </summary>
	public class AddOrRemoveIfExistsTests
	{
		/// <summary>
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should remove a number.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should remove a number.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/> test that should add a number.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/>
		/// with a null variable will throw <see cref="NullReferenceException"/>.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/>
		/// with duplicates onlyremoves the first occurrence.
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
		/// <see cref="ListExtensions.AddOrRemoveIfExists{TSource}(IList{TSource}, TSource)"/>
		/// returns the same instance.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void AddOrRemoveIfExists_ReturnsSameInstance()
		{
			// Arrange.
			List<int> list = new() { 1 };

			// Act.
			IList<int> result = list.AddOrRemoveIfExists(2);

			// Assert.
			Assert.Same(list, result);
		}
	}
}
