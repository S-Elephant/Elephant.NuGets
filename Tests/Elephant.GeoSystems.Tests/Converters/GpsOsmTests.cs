using Elephant.GeoSystems.Converters;

namespace Elephant.GeoSystems.Tests.Converters
{
	/// <summary>
	/// Conversion tests between GPS and Open Street Maps.
	/// </summary>
	public class GpsOsmTests
	{
		private const double Tolerance = 1e-4;

		// TODO: Tests below may be bad. Double-check the GPS and RD coordinates and ensure that they really match. I believe that there may be rounding issues.

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,float,float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.516220730357787d, 7.4834060668945348d, 273042, 174318)] // Maastricht.
		[InlineData(19, 51.829200540310971d, 7.2623062133789169d, 272720, 173583)] // Eindhoven.
		public void GpsToTileFloat(int zoom, float latitude, float longitude, int expectedTileX, int expectedTileY)
		{
			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expectedTileX, tile.TileX);
			Assert.Equal(expectedTileY, tile.TileY);
		}

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,double,double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.516220730357787d, 7.4834060668945348d, 273042, 174318)] // Maastricht.
		[InlineData(19, 51.829200540310971d, 7.2623062133789169d, 272720, 173583)] // Eindhoven.
		public void GpsToTileDouble(int zoom, double latitude, double longitude, int expectedTileX, int expectedTileY)
		{
			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expectedTileX, tile.TileX);
			Assert.Equal(expectedTileY, tile.TileY);
		}

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,decimal,decimal)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.516220730357787d, 7.4834060668945348d, 273042, 174318)] // Maastricht.
		[InlineData(19, 51.829200540310971d, 7.2623062133789169d, 272720, 173583)] // Eindhoven.
		public void GpsToTileDecimal(int zoom, decimal latitude, decimal longitude, int expectedTileX, int expectedTileY)
		{
			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expectedTileX, tile.TileX);
			Assert.Equal(expectedTileY, tile.TileY);
		}

		/// <summary>
		/// <see cref="ConverterUtils.OsmTileToGps"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 273042, 174318, 51.516220730357787d, 7.4834060668945348d)] // Maastricht.
		[InlineData(19, 272720, 173583, 51.829200540310971d, 7.2623062133789169d)] // Eindhoven.
		public void OsmTileToGps(int zoom, int tileX, int tileY, double expectedLatitude, double expectedLongitude)
		{
			// Act.
			(double X, double Y) gps = ConverterUtils.OsmTileToGps(zoom, tileX, tileY);

			// Assert.
			Assert.Equal(expectedLatitude, gps.X, Tolerance);
			Assert.Equal(expectedLongitude, gps.Y, Tolerance);
		}
	}
}