using System;
using Elephant.Testing.Xunit;
using Xunit;
using Xunit.Categories;

namespace Elephant.Rijksdriehoek.Tests
{
	/// <summary>
	/// <see cref="RdCoordinate"/> tests.
	/// </summary>
	public class RdCoordinateTests
	{
		/// <summary>
		/// Equal tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0f, 0f, 0f, 0f, true)]
		[InlineData(-0.00001f, 0f, 0f, 0f, false)]
		[InlineData(0.00001f, 0f, 0f, 0f, false)]
		[InlineData(0f, 0f, 2f, 0f, false)]
		[InlineData(0f, -10f, 3f, 0f, false)]
		[InlineData(-83324f, 32f, 50f, 10f, false)]
		[InlineData(-83324f, 500000.5001, -83324f, 500000.5001f, true)]
		[InlineData(-83324f, 500000.5001, 83324f, -500000.5001f, false)]
		public void Equal(float x1, float y1, float x2, float y2, bool expected)
		{
			Assert.Equal(expected, new RdCoordinate(x1, y1) == new RdCoordinate(x2, y2));
		}

		/// <summary>
		/// <see cref="Add(float, float, float, float, float, float)"/> test data.
		/// </summary>
		public static TheoryData<float, float, float, float, float, float> DataAdd =>
			new()
			{
				{ 0f, 0f, 0f, 0f, 0f, 0f },
				{ -5f, -7f, 5f, 7f, 0f, 0f },
				{ 55000f, 350000f, 25000f, 10000f, 80000f, 360000f },
			};

		/// <summary>
		/// Add tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(DataAdd))]
		public void Add(float aX, float aY, float bX, float bY, float expectedX, float expectedY)
		{
			RdCoordinate a = new(aX, aY);
			RdCoordinate b = new(bX, bY);
			RdCoordinate expected = new(expectedX, expectedY);
			Assert.Equal(expected, a + b);
		}

		/// <summary>
		/// <see cref="Subtract(float, float, float, float, float, float)"/> test data.
		/// </summary>
		public static TheoryData<float, float, float, float, float, float> DataSubtract =>
			new()
			{
				{ 0f, 0f, 0f, 0f, 0f, 0f },
				{ -5f, -7f, 5f, 7f, -10f, -14f },
				{ 55000f, 350000f, 25000f, 10000f, 30000f, 340000f },
			};

		/// <summary>
		/// Subtract tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(DataSubtract))]
		public void Subtract(float aX, float aY, float bX, float bY, float expectedX, float expectedY)
		{
			RdCoordinate a = new(aX, aY);
			RdCoordinate b = new(bX, bY);
			RdCoordinate expected = new(expectedX, expectedY);
			Assert.Equal(expected, a - b);
		}

		/// <summary>
		/// <see cref="Multiply(float, float, float, float, float, float)"/> test data.
		/// </summary>
		public static TheoryData<float, float, float, float, float, float> DataMultiply =>
			new()
			{
				{ 0f, 0f, 0f, 0f, 0f, 0f },
				{ -5f, -7f, 5f, 7f, -25f, -49f },
				{ 55000f, 350000f, 25000f, 10000f, 1375000000f, 3500000000f },
			};

		/// <summary>
		/// Multiply tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[MemberData(nameof(DataMultiply))]
		public void Multiply(float aX, float aY, float bX, float bY, float expectedX, float expectedY)
		{
			RdCoordinate a = new(aX, aY);
			RdCoordinate b = new(bX, bY);
			RdCoordinate expected = new(expectedX, expectedY);
			Assert.Equal(expected, a * b);
		}

		/// <summary>
		/// <see cref="Divide(float, float, float, float, float, float)"/> test data.
		/// </summary>
		public static TheoryData<float, float, float, float, float, float> DataDivide =>
		new()
		{
			{ 1f, 1f, 1f, 1f, 1f, 1f },
			{ -5f, -7f, 5f, 7f, -1f, -1f },
			{ 55000f, 350000f, 25000f, 10000f, 2.2f, 35f },
		};

		/// <summary>
		/// Divide tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[MemberData(nameof(DataDivide))]
		public void Divide(float aX, float aY, float bX, float bY, float expectedX, float expectedY)
		{
			RdCoordinate a = new(aX, aY);
			RdCoordinate b = new(bX, bY);
			RdCoordinate expected = new(expectedX, expectedY);
			Assert.Equal(expected, a / b);
		}

		/// <summary>
		/// Divide by zero test.
		/// </summary>
		[Fact]
		[SpeedFast, UnitTest]
		public void DivideByZero()
		{
			// Arrange.
			RdCoordinate a = new(1f, 1f);
			RdCoordinate b = new(0f, 0f);

			// Act & Assert.
			_ = Assert.Throws<DivideByZeroException>(() => a / b);
		}

		/// <summary>
		/// <see cref="RdCoordinate.TryParseFromPointString"/> tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[InlineData("10.12", "33.44", 10.12f, 33.44f, true)]
		[InlineData("0", "0", 0f, 0f, true)]
		[InlineData("500000.000008", "-400000.000002", 500000.000008f, 0f, true)]
		[InlineData("123,200", "456,752", 123f, 200f, true)]
		[InlineData("123 124", "456 457", 123, 124, true)]
		[InlineData("100.123", "-1000", 100.123f, 0f, true)]
		[Obsolete("Use https://github.com/NetTopologySuite/NetTopologySuite instead.")]
		public void TryParseFromPointString(string x, string y, float expectedX, float expectedY, bool expectedSuccess)
		{
			// Act.
			RdCoordinate? rdCoordinate = RdCoordinate.TryParseFromPointString($"POINT({x} {y})");

			// Assert.
			if (expectedSuccess)
			{
				_ = Assert.NotNull(rdCoordinate);
				Assert.Equal(expectedX, rdCoordinate.Value.X, 6f);
				Assert.Equal(expectedY, rdCoordinate.Value.Y, 6f);
			}
			else
			{
				Assert.Null(rdCoordinate);
			}
		}
	}
}