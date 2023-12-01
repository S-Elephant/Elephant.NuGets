using Elephant.Extensions;

namespace Elephant.Constants.MockData.Tests
{
	/// <summary>
	/// Test if all mock data is unique.
	/// </summary>
	public class UniquenessTests
	{
		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void Cities10()
		{
			// Act.
			bool allAreUnique = Cities.Us10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void Cities100()
		{
			// Act.
			bool allAreUnique = Cities.Us100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void Guids10()
		{
			// Act.
			bool allAreUnique = Guids.Guids10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void Guids100()
		{
			// Act.
			bool allAreUnique = Guids.Guids100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl4pp10()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl4pp10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl4pp100()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl4pp100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl4pp1000()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl4pp1000.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl6pp10()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl6pp10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl6pp100()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl6pp100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		// ReSharper disable once InconsistentNaming
		public void PostalCodesNl6pp1000()
		{
			// Act.
			bool allAreUnique = PostalCodes.Nl6pp1000.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void StreetsUs10()
		{
			// Act.
			bool allAreUnique = Streets.Us10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void StreetsUs100()
		{
			// Act.
			bool allAreUnique = Streets.Us100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void StreetsUs1000()
		{
			// Act.
			bool allAreUnique = Streets.Us1000.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void ZipCodesUs10()
		{
			// Act.
			bool allAreUnique = ZipCodes.Us10.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void ZipCodesUs100()
		{
			// Act.
			bool allAreUnique = ZipCodes.Us100.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}

		/// <summary>
		/// UniquenessTest.
		/// </summary>
		[Fact]
		[SpeedVeryFast]
		public void ZipCodesUs1000()
		{
			// Act.
			bool allAreUnique = ZipCodes.Us1000.AreAllItemsUnique();

			// Assert.
			Assert.True(allAreUnique);
		}
	}
}
