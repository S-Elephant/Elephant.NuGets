using Elephant.GeoSystems.Converters;

namespace Elephant.GeoSystems.Tests.Converters
{
	/// <summary>
	/// Conversion tests between GPS and Open Street Maps.
	/// </summary>
	public class GpsOsmTests
	{
		private const double Tolerance = 1e-3;

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,float,float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.2487615386578f, 5.702945063231038f, 270449, 174942)] // Station Weert https://a.tile.openstreetmap.org/19/270449/174942.png
		[InlineData(19, 52.37872150516985f, 4.901261307346452f, 269281, 172280)] // Station Amsterdam central https://a.tile.openstreetmap.org/19/269281/172280.png
		[InlineData(19, 51.91044428462077f, 4.48340352957081f, 268673, 173391)] // // Erasmus bridge Rotterdam https://a.tile.openstreetmap.org/19/268673/173391.png
		[InlineData(19, 52.155181880258425f, 5.3872152008141185f, 269989, 172812)] // Onze Lieve Vrouwetoren Amersfoort https://a.tile.openstreetmap.org/19/269989/172812.png
		public void GpsToTileFloat(int zoom, float latitude, float longitude, int expectedTileX, int expectedTileY)
		{
			// Arrange.
			(int expectedTileX, int expectedTileY) expected = (expectedTileX, expectedTileY);

			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expected, tile);
		}

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,double,double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.2487615386578d, 5.702945063231038d, 270449, 174942)] // Station Weert https://a.tile.openstreetmap.org/19/270449/174942.png
		[InlineData(19, 52.37872150516985d, 4.901261307346452d, 269281, 172280)] // Station Amsterdam central https://a.tile.openstreetmap.org/19/269281/172280.png
		[InlineData(19, 51.91044428462077d, 4.48340352957081d, 268673, 173391)] // // Erasmus bridge Rotterdam https://a.tile.openstreetmap.org/19/268673/173391.png
		[InlineData(19, 52.155181880258425d, 5.3872152008141185d, 269989, 172812)] // Onze Lieve Vrouwetoren Amersfoort https://a.tile.openstreetmap.org/19/269989/172812.png
		public void GpsToTileDouble(int zoom, double latitude, double longitude, int expectedTileX, int expectedTileY)
		{
			// Arrange.
			(int expectedTileX, int expectedTileY) expected = (expectedTileX, expectedTileY);

			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expected, tile);
		}

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,decimal,decimal)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 51.2487615386578d, 5.702945063231038d, 270449, 174942)] // Station Weert https://a.tile.openstreetmap.org/19/270449/174942.png
		[InlineData(19, 52.37872150516985d, 4.901261307346452d, 269281, 172280)] // Station Amsterdam central https://a.tile.openstreetmap.org/19/269281/172280.png
		[InlineData(19, 51.91044428462077d, 4.48340352957081d, 268673, 173391)] // // Erasmus bridge Rotterdam https://a.tile.openstreetmap.org/19/268673/173391.png
		[InlineData(19, 52.155181880258425d, 5.3872152008141185d, 269989, 172812)] // Onze Lieve Vrouwetoren Amersfoort https://a.tile.openstreetmap.org/19/269989/172812.png
		public void GpsToTileDecimal(int zoom, decimal latitude, decimal longitude, int expectedTileX, int expectedTileY)
		{
			// Arrange.
			(int expectedTileX, int expectedTileY) expected = (expectedTileX, expectedTileY);

			// Act.
			(int TileX, int TileY) tile = ConverterUtils.GpsToOsmTile(zoom, latitude, longitude);

			// Assert.
			Assert.Equal(expected, tile);
		}

		/// <summary>
		/// <see cref="ConverterUtils.OsmTileToGps"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 270449, 174942, 51.2487615386578d, 5.702945063231038d)] // Station Weert https://a.tile.openstreetmap.org/19/270449/174942.png
		[InlineData(19, 269281, 172280, 52.37872150516985d, 4.901261307346452d)] // Station Amsterdam central https://a.tile.openstreetmap.org/19/269281/172280.png
		[InlineData(19, 268673, 173391, 51.91044428462077d, 4.48340352957081d)] // // Erasmus bridge Rotterdam https://a.tile.openstreetmap.org/19/268673/173391.png
		[InlineData(19, 269989, 172812, 52.155181880258425d, 5.3872152008141185d)] // Onze Lieve Vrouwetoren Amersfoort https://a.tile.openstreetmap.org/19/269989/172812.png
		public void OsmTileToGps(int zoom, int tileX, int tileY, double expectedLatitude, double expectedLongitude)
		{
			// Act.
			(double X, double Y) = ConverterUtils.OsmTileToGps(zoom, tileX, tileY);

			// Assert.
			Assert.Equal(expectedLatitude, X, Tolerance);
			Assert.Equal(expectedLongitude, Y, Tolerance);
		}
	}
}