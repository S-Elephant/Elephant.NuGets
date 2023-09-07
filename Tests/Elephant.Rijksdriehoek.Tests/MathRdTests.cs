using System.Collections.Generic;
using Elephant.Testing.Xunit;
using Xunit;
using Xunit.Categories;

namespace Elephant.Rijksdriehoek.Tests
{
	/// <summary>
	/// <see cref="MathRd"/> tests.
	/// </summary>
	public class MathRdTests
	{
		/// <summary>
		/// <see cref="MathRd.Distance(float, float, float, float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0f, 0f, 0f, 0f, 0f)]
		[InlineData(10f, 0f, 0f, 0f, 10f)]
		[InlineData(0f, 10f, 0f, 0f, 10f)]
		[InlineData(0f, 0f, 10f, 0f, 10f)]
		[InlineData(0f, 0f, 0f, 10f, 10f)]
		[InlineData(10f, 10f, 10f, 10f, 0f)]
		[InlineData(-10f, -10f, -10f, -10f, 0f)]
		[InlineData(0f, 0f, -10f, 0f, 10f)]
		public void Distance(float x1, float y1, float x2, float y2, float expected)
		{
			Assert.Equal(expected, MathRd.Distance(x1, y1, x2, y2), 6f);
		}

		/// <summary>
		/// <see cref="MathRd.IsValidRdCoordinate(float, float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0f, 0f, false)]
		[InlineData(MathRd.RdMaxY, MathRd.RdMaxY, false)]
		[InlineData(-1000f, 0f, false)]
		[InlineData(1000000f, 300000f, false)]
		[InlineData(MathRd.RdMinX, MathRd.RdMinY, true)]
		[InlineData(MathRd.RdMaxX, MathRd.RdMaxY, true)]
		[InlineData(85334f, 270000f, false)]
		[InlineData(140000f, 300000f, true)]
		[InlineData(55000f, 532974f, true)]
		[InlineData(260000f, 615000f, true)]
		public void IsValidRdCoordinate(float x, float y, bool expected)
		{
			Assert.Equal(expected, MathRd.IsValidRdCoordinate(x, y));
		}

		/// <summary>
		/// <see cref="MathRd.IsValidRdCoordinate(decimal, decimal)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0, 0, false)]
		[InlineData(MathRd.RdMaxY, MathRd.RdMaxY, false)]
		[InlineData(-1000, 0, false)]
		[InlineData(1000000, 300000, false)]
		[InlineData(MathRd.RdMinX, MathRd.RdMinY, true)]
		[InlineData(MathRd.RdMaxX, MathRd.RdMaxY, true)]
		[InlineData(85334.43, 270000.83, false)]
		[InlineData(140000, 300000, true)]
		[InlineData(55000.1, 532974, true)]
		[InlineData(260000, 615000.2244, true)]
		public void IsValidRdCoordinateDecimal(decimal x, decimal y, bool expected)
		{
			Assert.Equal(expected, MathRd.IsValidRdCoordinate(x, y));
		}

		/// <summary>
		/// <see cref="MathRd.IsValidRdCoordinate(double, double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0d, 0d, false)]
		[InlineData(MathRd.RdMaxY, MathRd.RdMaxY, false)]
		[InlineData(-1000d, 0d, false)]
		[InlineData(1000000d, 300000d, false)]
		[InlineData(MathRd.RdMinX, MathRd.RdMinY, true)]
		[InlineData(MathRd.RdMaxX, MathRd.RdMaxY, true)]
		[InlineData(85334.43d, 270000.83d, false)]
		[InlineData(140000d, 300000d, true)]
		[InlineData(55000.1d, 532974d, true)]
		[InlineData(260000d, 615000.2244d, true)]
		public void IsValidRdCoordinateDouble(double x, double y, bool expected)
		{
			Assert.Equal(expected, MathRd.IsValidRdCoordinate(x, y));
		}

		/// <summary>
		/// <see cref="MathRd.IsValidRdCoordinate(float, float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(0f, 0f, false)]
		[InlineData(MathRd.RdMaxY, MathRd.RdMaxY, false)]
		[InlineData(-1000f, 0f, false)]
		[InlineData(1000000f, 300000f, false)]
		[InlineData(MathRd.RdMinX, MathRd.RdMinY, true)]
		[InlineData(MathRd.RdMaxX, MathRd.RdMaxY, true)]
		[InlineData(85334.43f, 270000.83f, false)]
		[InlineData(140000f, 300000f, true)]
		[InlineData(55000.1f, 532974f, true)]
		[InlineData(260000f, 615000.2244f, true)]
		public void IsValidRdCoordinateFloat(float x, float y, bool expected)
		{
			Assert.Equal(expected, MathRd.IsValidRdCoordinate(x, y));
		}

		/// <summary>
		/// <see cref="MathRd.TryParseFromPointString(string, out float, out float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[InlineData("10.12", "33.44", 10.12f, 33.44f, true)]
		[InlineData("0", "0", 0f, 0f, true)]
		[InlineData("500000.000008", "-400000.000002", 500000.000008f, 0f, true)]
		[InlineData("123,200", "456,752", 123f, 200f, true)]
		[InlineData("123 124", "456 457", 123, 124, true)]
		[InlineData("100.123", "-1000", 100.123f, 0f, true)]
		public void TryParseFromPointString(string x, string y, float expectedX, float expectedY, bool expectedSuccess)
		{
			bool success = MathRd.TryParseFromPointString($"POINT({x} {y})", out float resultX, out float resultY);

			Assert.Equal(expectedSuccess, success);
			Assert.Equal(expectedX, resultX, 6f);
			Assert.Equal(expectedY, resultY, 6f);
		}

		private const int ConvertToLatitudeLongitudePrecision = 4;

		/// <summary>
		/// <see cref="MathRd.ConvertToLatitudeLongitude(double, double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548d, 318068d, 50.85204602321809d, 5.693181181444698d, ConvertToLatitudeLongitudePrecision)] // Maastricht.
		[InlineData(161664d, 383111d, 51.4370644101525d, 5.483039577727214d, ConvertToLatitudeLongitudePrecision)] // Eindhoven.
		[InlineData(133561d, 397126d, 51.56267096731468d, 5.078016769674488d, ConvertToLatitudeLongitudePrecision)] // Tilburg.
		[InlineData(112691d, 400437d, 51.591244471919005d, 4.776662178460902d, ConvertToLatitudeLongitudePrecision)] // Breda.
		[InlineData(32143d, 391346d, 51.497724935924936d, 3.617838516788694d, ConvertToLatitudeLongitudePrecision)] // Middelburg.
		[InlineData(91873d, 436967d, 51.91760749459275d, 4.46962367068906d, ConvertToLatitudeLongitudePrecision)] // Rotterdam.
		[InlineData(80300d, 453738d, 52.06688833415241d, 4.297768334323142d, ConvertToLatitudeLongitudePrecision)] // The Hague.
		[InlineData(121605d, 487759d, 52.37668890821108d, 4.896782660725934d, ConvertToLatitudeLongitudePrecision)] // Amsterdam.
		[InlineData(182687d, 579310d, 53.19971993186204d, 5.801522396941571d, ConvertToLatitudeLongitudePrecision)] // Leeuwarden.
		[InlineData(233627d, 581737d, 53.21647004196559d, 6.564292486864583d, ConvertToLatitudeLongitudePrecision)] // Groningen.
		[InlineData(256494d, 534047d, 52.78420195511749d, 6.891631130023316d, ConvertToLatitudeLongitudePrecision)] // Emmen.
		[InlineData(257781d, 471341d, 52.22056106147205d, 6.891387579659575d, ConvertToLatitudeLongitudePrecision)] // Enschede.
		[InlineData(185826d, 424987d, 51.81265295534943d, 5.834226504298018d, ConvertToLatitudeLongitudePrecision)] // Nijmegen.
		[InlineData(154675d, 463049d, 52.15561201090535d, 5.382458244825791d, ConvertToLatitudeLongitudePrecision)] // Amersfoort.
		[InlineData(227846d, 618116d, 53.54411077235744d, 6.486070323938958d, ConvertToLatitudeLongitudePrecision)] // Rottumerplaat.
		public void ConvertToLatitudeLongitude(double rdX, double rdY, double expectedLattitude, double expectedLongitude, int precision)
		{
			(double latitude, double longitude) = MathRd.ConvertToLatitudeLongitude(rdX, rdY);
			Assert.Equal(expectedLattitude, latitude, precision);
			Assert.Equal(expectedLongitude, longitude, precision);
		}

		/// <summary>
		/// <see cref="MathRd.ConvertToLatitudeLongitude(float, float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548f, 318068f, 50.85204602321809f, 5.693181181444698f, 13)] // Maastricht.
		[InlineData(161664f, 383111f, 51.4370644101525f, 5.483039577727214f, 13)] // Eindhoven.
		[InlineData(133561f, 397126f, 51.56267096731468f, 5.078016769674488f, 13)] // Tilburg.
		[InlineData(112691f, 400437f, 51.591244471919005f, 4.776662178460902d, 13)] // Breda.
		[InlineData(32143f, 391346f, 51.497724935924936f, 3.617838516788694f, 13)] // Middelburg.
		[InlineData(91873f, 436967f, 51.91760749459275f, 4.46962367068906f, 13)] // Rotterdam.
		[InlineData(80300f, 453738f, 52.06688833415241f, 4.297768334323142f, 13)] // The Hague.
		[InlineData(121605f, 487759f, 52.37668890821108f, 4.896782660725934f, 13)] // Amsterdam.
		[InlineData(182687f, 579310f, 53.19971993186204f, 5.801522396941571f, 13)] // Leeuwarden.
		[InlineData(233627f, 581737f, 53.21647004196559f, 6.564292486864583f, 13)] // Groningen.
		[InlineData(256494f, 534047f, 52.78420195511749f, 6.891631130023316f, 13)] // Emmen.
		[InlineData(257781f, 471341f, 52.22056106147205f, 6.891387579659575f, 13)] // Enschede.
		[InlineData(185826f, 424987f, 51.81265295534943f, 5.834226504298018f, 13)] // Nijmegen.
		[InlineData(154675f, 463049f, 52.15561201090535f, 5.382458244825791f, 13)] // Amersfoort.
		[InlineData(227846f, 618116f, 53.54411077235744f, 6.486070323938958f, 13)] // Rottumerplaat.
		public void ConvertToLatitudeLongitudeFloat(float rdX, float rdY, float expectedLattitude, float expectedLongitude, float precision)
		{
			(float latitude, float longitude) = MathRd.ConvertToLatitudeLongitude(rdX, rdY);
			Assert.Equal(expectedLattitude, latitude, precision);
			Assert.Equal(expectedLongitude, longitude, precision);
		}

		private const float ConvertToRijksdriehoekPrecision = 13;

		/// <summary>
		/// <see cref="MathRd.ConvertToRijksdriehoek(double, double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[InlineData(50.85204602321809d, 5.693181181444698d, 176548d, 318068d, ConvertToRijksdriehoekPrecision)] // Maastricht.
		[InlineData(51.4370644101525d, 5.483039577727214d, 161664d, 383111d, ConvertToRijksdriehoekPrecision)] // Eindhoven.
		[InlineData(51.56267096731468d, 5.078016769674488d, 133561d, 397126d, ConvertToRijksdriehoekPrecision)] // Tilburg.
		[InlineData(51.591244471919005d, 4.776662178460902d, 112691d, 400437d, ConvertToRijksdriehoekPrecision)] // Breda.
		[InlineData(51.497724935924936d, 3.617838516788694d, 32143d, 391346d, ConvertToRijksdriehoekPrecision)] // Middelburg.
		[InlineData(51.91760749459275d, 4.46962367068906d, 91873d, 436967d, ConvertToRijksdriehoekPrecision)] // Rotterdam.
		[InlineData(52.06688833415241d, 4.297768334323142d, 80300d, 453738d, ConvertToRijksdriehoekPrecision)] // The Hague.
		[InlineData(52.37668890821108d, 4.896782660725934d, 121605d, 487759d, ConvertToRijksdriehoekPrecision)] // Amsterdam.
		[InlineData(53.19971993186204d, 5.801522396941571d, 182687d, 579310d, ConvertToRijksdriehoekPrecision)] // Leeuwarden.
		[InlineData(53.21647004196559d, 6.564292486864583d, 233627d, 581737d, ConvertToRijksdriehoekPrecision)] // Groningen.
		[InlineData(52.78420195511749d, 6.891631130023316d, 256494d, 534047d, ConvertToRijksdriehoekPrecision)] // Emmen.
		[InlineData(52.22056106147205d, 6.891387579659575d, 257781d, 471341d, ConvertToRijksdriehoekPrecision)] // Enschede.
		[InlineData(51.81265295534943d, 5.834226504298018d, 185826d, 424987d, ConvertToRijksdriehoekPrecision)] // Nijmegen.
		[InlineData(52.15561201090535d, 5.382458244825791d, 154675d, 463049d, ConvertToRijksdriehoekPrecision)] // Amersfoort.
		[InlineData(53.54411077235744d, 6.486070323938958d, 227846d, 618116d, ConvertToRijksdriehoekPrecision)] // Rottumerplaat.
		public void ConvertToRijksdriehoek(double latitude, double longitude, double expectedRdX, double expectedRdY, float precision)
		{
			(double rdX, double rdY) = MathRd.ConvertToRijksdriehoek(latitude, longitude);
			Assert.Equal(expectedRdX, rdX, precision);
			Assert.Equal(expectedRdY, rdY, precision);
		}

		/// <summary>
		/// <see cref="MathRd.ConvertToRijksdriehoek(float, float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(52.372143838117f, 4.90559760435224f, 122202f, 487250f, 0.9f)]
		public void ConvertToRijksdriehoekFloat(float latitude, float longitude, float expectedRdX, float expectedRdY, float precision)
		{
			(float rdX, float rdY) = MathRd.ConvertToRijksdriehoek(latitude, longitude);
			Assert.Equal(expectedRdX, rdX, precision);
			Assert.Equal(expectedRdY, rdY, precision);
		}

		/// <summary>
		/// <see cref="ConvertStringToPolygonRd"/> test data.
		/// </summary>
		public static IEnumerable<object[]> ConvertStringToPolygonRdData =>
		  new List<object[]>
		  {
		   new object[] { "POLYGON ((0 0))", new List<(float, float)> { (0f, 0f) } },
		   new object[] { "POLYGON ((1 1))", new List<(float, float)> { (1f, 1f) } },
		   new object[] { "POLYGON ((1.2 1))", new List<(float, float)> { (1.2f, 1f) } },
		   new object[] { "POLYGON ((1 1.2))", new List<(float, float)> { (1f, 1.2f) } },
		   new object[] { "POLYGON ((1.0 1.0))", new List<(float, float)> { (1f, 1f) } },
		   new object[] { "POLYGON ((10.100 400000.100))", new List<(float, float)> { (10.100f, 400000.100f) } },
		   new object[] { "POLYGON ((10.100 400000.100, 20.200 500000.500))", new List<(float, float)> { (10.100f, 400000.100f), (20.200f, 500000.500f) } },
		   new object[] { "POLYGON ((10.100 400000.100, 20.200 500000.500, 30.300 600000.600))", new List<(float, float)> { (10.100f, 400000.100f), (20.200f, 500000.500f), (30.300f, 600000.600f) } },
		  };

		/// <summary>
		/// <see cref="MathRd.ConvertStringToPolygonRd(string)"/> tests.
		/// </summary>
		[Theory]
		[SpeedFast, UnitTest]
		[MemberData(nameof(ConvertStringToPolygonRdData))]
		public void ConvertStringToPolygonRd(string polygonString, List<(float x, float y)> expected)
		{
			List<(float x, float y)> result = MathRd.ConvertStringToPolygonRd(polygonString);
			int expectedCount = expected.Count;

			// Ensure that the length is correct.
			Assert.Equal(result.Count, expectedCount);

			// Ensure all coordinates are the same (and in the same order).
			for (int i = 0; i < expectedCount; i++)
			{
				Assert.Equal(expected[i].x, result[i].x, 6f);
				Assert.Equal(expected[i].y, result[i].y, 6f);
			}
		}
	}
}