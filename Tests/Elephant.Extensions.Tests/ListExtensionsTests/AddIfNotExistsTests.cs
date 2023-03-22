namespace Elephant.Extensions.Tests.ListExtensionsTests
{
	/// <summary>
	/// <see cref="ListExtensions.AddIfNotExists{T}"/> tests.
	/// </summary>
	public class AddIfNotExistsTests
	{
		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/> test that should not add anything.
		/// </summary>
		[Theory]
		[InlineData(-10)]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[SpeedFast, UnitTest]
		public void NothingIsAddedIfExists(int valueToAdd)
		{
			List<int> list = new() { -10, 1, 2, 3 };

			list.AddIfNotExists(valueToAdd);

			Assert.Equal(new List<int>() { -10, 1, 2, 3 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/> test that should add the number 5.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsAddedIfNotExists()
		{
			List<int> list = new() { -10, 1, 2, 3, 7 };

			list.AddIfNotExists(5);

			Assert.Equal(new List<int>() { -10, 1, 2, 3, 7, 5 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/> test that should add the numbers 5 and 10 but not 3.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void AddsOnlyNonExistingValues()
		{
			List<int> list = new() { -10, 1, 2, 3, 7 };

			list.AddIfNotExists(5);
			list.AddIfNotExists(3);
			list.AddIfNotExists(10);

			Assert.Equal(new List<int>() { -10, 1, 2, 3, 7, 5, 10 }, list);
		}
	}
}
