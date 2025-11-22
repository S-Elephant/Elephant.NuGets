using System.Collections.ObjectModel;

namespace Elephant.Extensions.Tests.ListExtensionTests
{
	/// <summary>
	/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/> tests.
	/// </summary>
	public class HasAnyTests
	{
		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// returns true if it has 1 item.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfHasOne()
		{
			// Arrange.
			List<int> list = new() { int.MinValue };

			// Act.
			bool result = list.HasAny();

			// Assert.
			Assert.True(result);
		}

		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// returns true if it has multiple items.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfHasMultiple()
		{
			// Arrange.
			List<int> list = new() { 1000, -30, 30, int.MaxValue };

			// Act.
			bool result = list.HasAny();

			// Assert.
			Assert.True(result);
		}

		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// returns true if it has any items.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsFalseIfEmpty()
		{
			// Arrange.
			List<int> list = new();

			// Act.
			bool result = list.HasAny();

			// Assert.
			Assert.False(result);
		}

		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// returns true if it has any items.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsFalseIfNull()
		{
			// Arrange.
			List<int>? list = null;

			// Act.
			bool result = list.HasAny();

			// Assert.
			Assert.False(result);
		}

		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// using a list of reference types that contain a null element returns true.
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void ReturnsTrueIfContainsNullReference()
		{
			// Arrange.
			List<string?> list = new() { null };

			// Act.
			bool result = list.HasAny();

			// Assert.
			Assert.True(result);
		}

		/// <summary>
		/// <see cref="ListExtensions.HasAny{TSource}(IList{TSource}?)"/>
		/// works for other IList implementations (i.e. ReadOnlyCollection from AsReadOnly()).
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void WorksForReadOnlyCollectionWrapper()
		{
			// Arrange.
			ReadOnlyCollection<int> wrapper = new List<int> { 42 }.AsReadOnly();

			// Act.
			bool result = wrapper.HasAny();

			// Assert.
			Assert.True(result);
		}
	}
}
