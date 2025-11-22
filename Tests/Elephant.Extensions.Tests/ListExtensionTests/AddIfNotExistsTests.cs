namespace Elephant.Extensions.Tests.ListExtensionTests
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
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3 };

			// Act.
			list.AddIfNotExists(valueToAdd);

			// Assert.
			Assert.Equal(new List<int>() { -10, 1, 2, 3 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/> test that should add the number 5.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void IsAddedIfNotExists()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3, 7 };

			// Act.
			list.AddIfNotExists(5);

			// Assert.
			Assert.Equal(new List<int>() { -10, 1, 2, 3, 7, 5 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/> test
		/// that should add the numbers 5 and 10 but not 3, using
		/// fluent chaining.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void AddsOnlyNonExistingValues()
		{
			// Arrange.
			List<int> list = new() { -10, 1, 2, 3, 7 };

			// Act.
			list.AddIfNotExists(5).AddIfNotExists(3).AddIfNotExists(10);

			// Assert.
			Assert.Equal(new List<int>() { -10, 1, 2, 3, 7, 5, 10 }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/>
		/// throws <see cref="ArgumentNullException"/> if the list is null.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ThrowsIfListIsNull()
		{
			// Arrange.
			List<int> list = null!;

			// Act & Assert.
			Assert.Throws<ArgumentNullException>(() => list.AddIfNotExists(5));
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/>
		/// for objects uses overridden Equals and GetHashCode methods.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void UsesObjectEqualityOverrides()
		{
			// Arrange.
			Person existing = new Person("John");
			List<Person> list = new() { existing };

			// Act: different instance but equal by overridden Equals.
			list.AddIfNotExists(new Person("John"));

			// Assert: not added.
			Assert.Single(list);
			Assert.Same(existing, list[0]);
		}

		/// <summary>Test class.</summary>
		private sealed class Person
		{
			/// <summary>Test property.</summary>
			public string Name { get; }

			/// <summary>Constructor.</summary>
			public Person(string name)
			{
				Name = name;
			}

			/// <summary>Test method.</summary>
			public override bool Equals(object? obj) => obj is Person p && string.Equals(Name, p.Name, StringComparison.Ordinal);

			/// <summary>Test method.</summary>
			public override int GetHashCode() => Name?.GetHashCode(StringComparison.Ordinal) ?? 0;
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/>
		/// handles reference types and null-adding correctly.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void HandlesReferenceTypesAndNulls()
		{
			// Arrange.
			List<string?> list = new() { "a" };

			// Act.
			list.AddIfNotExists(null);

			// Assert.
			Assert.Equal(new List<string?>() { "a", null }, list);
		}

		/// <summary>
		/// <see cref="ListExtensions.AddIfNotExists{T}"/>
		/// returns the same instance when fluent chaining.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void ReturnsSameInstanceForFluentChaining()
		{
			// Arrange.
			List<int> originalList = new() { 1, 2 };

			// Act.
			List<int> result = originalList.AddIfNotExists(3);

			// Assert.
			Assert.Same(originalList, result);
		}
	}
}
