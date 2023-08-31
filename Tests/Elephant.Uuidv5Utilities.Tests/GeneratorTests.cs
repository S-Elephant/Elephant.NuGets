namespace Elephant.Uuidv5Utilities.Tests
{
	/// <summary>
	/// UUID v5 generation tests.
	/// </summary>
	public class GeneratorTests
	{
		private const string NameSpaceId1AsString = "7f858204-6efb-4c4d-9a1e-1cd6f6d11452";
		private const string NameSpaceId2AsString = "2a33ca8b-136d-417f-9268-e7667d5567aa";

		/// <summary>
		/// Does the generation only result in unique <see cref="Guid"/>s?
		/// </summary>
		[Fact]
		[SpeedVeryFast, UnitTest]
		public void IsGenerationUnique()
		{
			// Arrange.
			List<Guid> guids = new();
			Guid namespaceId = new(NameSpaceId1AsString);

			// Act.
			for (int i = 0; i < 50; i++)
				guids.Add(Uuidv5Utils.GenerateGuid(namespaceId, $"customer{i}"));

			guids.Add(Uuidv5Utils.GenerateGuid(namespaceId, "."));
			guids.Add(Uuidv5Utils.GenerateGuid(new Guid(NameSpaceId2AsString), "."));
			guids.Add(Uuidv5Utils.GenerateGuid(namespaceId, "--"));
			guids.Add(Uuidv5Utils.GenerateGuid(namespaceId, string.Empty));

			// Assert.
			Assert.True(guids.Distinct().Count() == guids.Count);
		}

		/// <summary>
		/// Does the generation only result in the same <see cref="Guid"/>s if the namespace and name are equal?
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(NameSpaceId1AsString, "John", NameSpaceId1AsString, "John")]
		[InlineData(NameSpaceId1AsString, "john", NameSpaceId1AsString, "john")]
		[InlineData(NameSpaceId2AsString, "", NameSpaceId2AsString, "")]
		[InlineData(NameSpaceId2AsString, " ", NameSpaceId2AsString, " ")]
		[InlineData(NameSpaceId2AsString, ".", NameSpaceId2AsString, ".")]
		[InlineData(NameSpaceId2AsString, "..", NameSpaceId2AsString, "..")]
		public void GeneratesEqualGuids(string namespaceId1AsString, string name1, string namespaceId2AsString, string name2)
		{
			// Arrange.
			Guid namespaceId1 = new(namespaceId1AsString);
			Guid namespaceId2 = new(namespaceId2AsString);

			// Act.
			Guid a = Uuidv5Utils.GenerateGuid(namespaceId1, name1);
			Guid b = Uuidv5Utils.GenerateGuid(namespaceId2, name2);

			// Assert.
			Assert.Equal(a, b);
		}

		/// <summary>
		/// Does the generation only result in different <see cref="Guid"/>s if the namespace and/or name are different?
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(NameSpaceId1AsString, "John", NameSpaceId1AsString, "john")]
		[InlineData(NameSpaceId1AsString, "john", NameSpaceId1AsString, "john ")]
		[InlineData(NameSpaceId2AsString, " ", NameSpaceId2AsString, "")]
		[InlineData(NameSpaceId2AsString, ".", NameSpaceId2AsString, "..")]
		[InlineData(NameSpaceId2AsString, "^", NameSpaceId2AsString, "&")]
		[InlineData(NameSpaceId1AsString, "John", NameSpaceId2AsString, "john")]
		[InlineData(NameSpaceId1AsString, "john", NameSpaceId2AsString, "john ")]
		[InlineData(NameSpaceId1AsString, " ", NameSpaceId2AsString, "")]
		[InlineData(NameSpaceId1AsString, ".", NameSpaceId2AsString, "..")]
		[InlineData(NameSpaceId1AsString, "^", NameSpaceId2AsString, "&")]
		public void GeneratesDifferentGuids(string namespaceId1AsString, string name1, string namespaceId2AsString, string name2)
		{
			// Arrange.
			Guid namespaceId1 = new(namespaceId1AsString);
			Guid namespaceId2 = new(namespaceId2AsString);

			// Act.
			Guid a = Uuidv5Utils.GenerateGuid(namespaceId1, name1);
			Guid b = Uuidv5Utils.GenerateGuid(namespaceId2, name2);

			// Assert.
			Assert.NotEqual(a, b);
		}
	}
}