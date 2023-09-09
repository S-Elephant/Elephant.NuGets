using System.Globalization;
using Elephant.GeoSystems.Converters;
using Elephant.GeoSystems.Shared;

namespace Elephant.GeoSystems.Tests.Shared
{
	/// <summary>
	/// <see cref="GeoMultiCoordRd"/> tests.
	/// </summary>
	public class GeoMultiCoordRdTests
	{
		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,float,float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 176548d, 318068d, 270435, 175861)] // Maastricht.
		[InlineData(19, 161664d, 383111d, 270129, 174503)] // Eindhoven.
		public void GpsToTileFloat(int zoom, decimal rdX, decimal rdY, int expectedOsmTileX, int expectedOsmTileY)
		{
			// Arrange (Act happens internally in that class).
			GeoMultiCoordRd coordinate = new(rdX, rdY, zoom);

			// Assert.
			Assert.Equal(expectedOsmTileX, coordinate.OsmX);
			Assert.Equal(expectedOsmTileY, coordinate.OsmY);
		}

		/// <summary>
		/// <see cref="ConverterUtils.GpsToOsmTile(int,float,float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(19, 176548d, 318068d, "50.8520", "5.6932")] // Maastricht.
		[InlineData(19, 161664d, 383111d, "51.4371", "5.483")] // Eindhoven.
		public void RdToGpsFloat(int zoom, decimal rdX, decimal rdY, string expectedGpsLatitudeAsString, string expectedGpsLongitudeAsString)
		{
			// Arrange (Act happens internally in that class).
			GeoMultiCoordRd coordinate = new(rdX, rdY, zoom);

			decimal expectedGpsLatitude = Convert.ToDecimal(expectedGpsLatitudeAsString, CultureInfo.InvariantCulture);
			decimal expectedGpsLongitude = Convert.ToDecimal(expectedGpsLongitudeAsString, CultureInfo.InvariantCulture);

			// Assert.
			Assert.Equal(expectedGpsLatitude, coordinate.GpsLatitude);
			Assert.Equal(expectedGpsLongitude, coordinate.GpsLongitude);
		}
	}
}