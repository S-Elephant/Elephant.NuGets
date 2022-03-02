using Elephant.Testing.Xunit;
using System.Collections.Generic;
using Xunit;

namespace Elephant.Rijksdriehoek.Tests
{
    public class MathRdTests
    {
        [Theory]
        [SpeedVeryFast]
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
            Assert.Equal(expected, MathRd.Distance(x1, y1, x2, y2), 6);
        }

        [Theory]
        [SpeedVeryFast]
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

        [Theory]
        [SpeedVeryFast]
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

        [Theory]
        [SpeedVeryFast]
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

        [Theory]
        [SpeedVeryFast]
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

        [Theory]
        [SpeedFast]
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
            Assert.Equal(expectedX, resultX, 6);
            Assert.Equal(expectedY, resultY, 6);
        }

        [Theory]
        [SpeedVeryFast]
        [InlineData(122202d, 487250d, 52.372143838117d, 4.90559760435224d, 13)]
        public void ConvertToLatitudeLongitude(double rdX, double rdY, double expectedLattitude, double expectedLongitude, int precision)
        {
            (double latitude, double longitude) = MathRd.ConvertToLatitudeLongitude(rdX, rdY);
            Assert.Equal(expectedLattitude, latitude, precision);
            Assert.Equal(expectedLongitude, longitude, precision);
        }

        [Theory]
        [SpeedVeryFast]
        [InlineData(122202f, 487250f, 52.372143838117f, 4.90559760435224f, 6)]
        public void ConvertToLatitudeLongitudeFloat(float rdX, float rdY, float expectedLattitude, float expectedLongitude, int precision)
        {
            (float latitude, float longitude) = MathRd.ConvertToLatitudeLongitude(rdX, rdY);
            Assert.Equal(expectedLattitude, latitude, precision);
            Assert.Equal(expectedLongitude, longitude, precision);
        }

        [Theory]
        [SpeedFast]
        [InlineData(52.372143838117d, 4.90559760435224d, 122202d, 487250d, 0)]
        public void ConvertToRijksdriehoek(double latitude, double longitude, double expectedRdX, double expectedRdY, int precision)
        {
            (double rdX, double rdY) = MathRd.ConvertToRijksdriehoek(latitude, longitude);
            Assert.Equal(expectedRdX, rdX, precision);
            Assert.Equal(expectedRdY, rdY, precision);
        }

        [Theory]
        [SpeedVeryFast]
        [InlineData(52.372143838117f, 4.90559760435224f, 122202f, 487250f, 0)]
        public void ConvertToRijksdriehoekFloat(float latitude, float longitude, float expectedRdX, float expectedRdY, int precision)
        {
            (float rdX, float rdY) = MathRd.ConvertToRijksdriehoek(latitude, longitude);
            Assert.Equal(expectedRdX, rdX, precision);
            Assert.Equal(expectedRdY, rdY, precision);
        }

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

        [Theory]
        [SpeedFast]
        [MemberData(nameof(ConvertStringToPolygonRdData))]
        public void ConvertStringToPolygonRd(string polygonString, List<(float x, float y)> expected)
        {
            List<(float x, float y)> result = MathRd.ConvertStringToPolygonRd(polygonString);
            int expectedCount = expected.Count;

            // Ensure that that the length is correct.
            Assert.Equal(result.Count, expectedCount);

            // Ensure all coordinates are the same (and in the same order).
            for (int i = 0; i < expectedCount; i++)
            {
                Assert.Equal(expected[i].x, result[i].x, 6);
                Assert.Equal(expected[i].y, result[i].y, 6);
            }
        }
    }
}