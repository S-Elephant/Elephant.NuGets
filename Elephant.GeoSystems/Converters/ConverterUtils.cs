using System;
using Elephant.GeoSystems.Validators;
// ReSharper disable RedundantCast

namespace Elephant.GeoSystems.Converters
{
	/// <summary>
	/// Various positioning conversion utilities.
	/// </summary>
	public static class ConverterUtils
	{
		#region GPS <==> Open Street Map

		/// <summary>
		/// Convert a GPS (WGS84) coordinate into an Open Street Maps tile coordinate.
		/// </summary>
		/// <param name="zoom">Open Street Map zoom level (between 1-20 and will be clamped). For more info see: https://wiki.openstreetmap.org/wiki/Zoom_levels.</param>
		/// <param name="latitude">GPS (WGS84) latitude.</param>
		/// <param name="longitude">GPS (WGS84) longitude.</param>
		/// <returns>Open Street Map tile coordinate.</returns>
		/// <exception cref="ArgumentException">Thrown if the GPS coordinate is invalid.</exception>
		/// <remarks>More info here: https://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#C#.</remarks>
		public static (int TileX, int TileY) GpsToOsmTile(int zoom, float latitude, float longitude)
		{
			return GpsToOsmTile(zoom, (double)latitude, (double)longitude);
		}

		/// <inheritdoc cref="GpsToOsmTile(int,float,float)"/>
		public static (int TileX, int TileY) GpsToOsmTile(int zoom, double latitude, double longitude)
		{
			if (!GpsValidator.IsValid(latitude, longitude))
				throw new ArgumentException($"Invalid GPS coordinate. Got ({latitude}, {longitude}).");

			// Clamp zoom.
			if (zoom < 1)
				zoom = 1;
			if (zoom > 20)
				zoom = 20;

			// Code below replaces Math.Pow(2, zoom).
			int zoomScale = 1;
			for (int i = 0; i < zoom; i++)
				zoomScale *= 2;

			// Convert the longitude to a tile X coordinate.
			// The formula takes the longitude, adjusts it into a 0-360 degree range (by adding 180),
			// then scales it by the number of tiles at the given zoom level (which is 2^zoom).
			int tileX = (int)Math.Floor((longitude + 180) / 360 * zoomScale);

			// Convert the latitude to a tile Y coordinate.
			// This formula involves some trigonometry to transform from a latitude in degrees to a
			// Mercator-projected Y coordinate, then scales it by the number of tiles at the given zoom level.
			// Note that latitude * Math.PI / 180 converts the latitude to radians.
			// See also: https://en.wikipedia.org/wiki/Mercator_projection.
			int tileY = (int)Math.Floor((1 - Math.Log(Math.Tan(latitude * Math.PI / 180) + 1 / Math.Cos(latitude * Math.PI / 180)) / Math.PI) / 2 * zoomScale);

			return (tileX, tileY);
		}

		/// <inheritdoc cref="GpsToOsmTile(int,float,float)"/>
		public static (int TileX, int TileY) GpsToOsmTile(int zoom, decimal latitude, decimal longitude)
		{
			if (!GpsValidator.IsValid(latitude, longitude))
				throw new ArgumentException($"Invalid GPS coordinate. Got ({latitude}, {longitude}).");

			// Clamp zoom.
			if (zoom < 1)
				zoom = 1;
			if (zoom > 20)
				zoom = 20;

			// Code below replaces Math.Pow(2, zoom).
			int zoomScale = 1;
			for (int i = 0; i < zoom; i++)
				zoomScale *= 2;

			// Convert the longitude to a tile X coordinate.
			// The formula takes the longitude, adjusts it into a 0-360 degree range (by adding 180),
			// then scales it by the number of tiles at the given zoom level (which is 2^zoom).
			int tileX = (int)Math.Floor((longitude + 180) / 360 * zoomScale);

			// Convert the latitude to a tile Y coordinate.
			// This formula involves some trigonometry to transform from a latitude in degrees to a
			// Mercator-projected Y coordinate, then scales it by the number of tiles at the given zoom level.
			// See also: https://en.wikipedia.org/wiki/Mercator_projection.
			int tileY = (int)Math.Floor((1 - Math.Log(Math.Tan((double)latitude * Math.PI / 180) + 1 / Math.Cos((double)latitude * Math.PI / 180)) / Math.PI) / 2 * zoomScale);

			return (tileX, tileY);
		}

		/// <summary>
		/// Convert Open Street Map tile coordinate ==> GPS coordinate.
		/// Latitude is the first parameter.
		/// </summary>
		public static (float Latitude, float Longitude) OsmTileToGpsAsFloat(int zoom, int tileX, int tileY)
		{
			// Calculate the width of a single tile at the given zoom level in meters.
			// The total width (2*HalfEarthCircumference) is divided by the number of tiles, which is 2^zoom.
			double tileWidth = 2d * Constants.HalfEarthCircumference / (1 << zoom);
			// The height of a tile is the same as the width because the tiles in Open Street Map are square.
			double tileHeight = 2d * Constants.HalfEarthCircumference / (1 << zoom);

			// Calculate the horizontal distance in meters from the left-most edge of the left-most tile to the center of the given tileX.
			double centerX = (tileX + 0.5d) * tileWidth - Constants.HalfEarthCircumference;
			// Calculate the vertical distance in meters from the top-most edge of the top-most tile to the center of the given tileY.
			double centerY = Constants.HalfEarthCircumference - (tileY + 0.5d) * tileHeight;

			// Convert the centerX value to degrees to get the longitude.
			// The formula derives from the relationship between arc length and angle in a circle (arc length = angle * radius).
			double longitude = centerX / Constants.EarthRadius * 180d / Math.PI;
			// Convert the centerY value to degrees to get the latitude.
			// The formula involves some trigonometry and is used to transform Mercator-projected Y coordinate back to latitude.
			double latitude = (2d * Math.Atan(Math.Exp(centerY / Constants.EarthRadius)) - Math.PI / 2d) * 180d / Math.PI;

			return ((float)latitude, (float)longitude);
		}

		/// <summary>
		/// Convert Open Street Map tile coordinate ==> GPS coordinate.
		/// Latitude is the first parameter.
		/// </summary>
		public static (double Latitude, double Longitude) OsmTileToGps(int zoom, int tileX, int tileY)
		{
			// Calculate the width of a single tile at the given zoom level in meters.
			// The total width (2*HalfEarthCircumference) is divided by the number of tiles, which is 2^zoom.
			double tileWidth = 2d * Constants.HalfEarthCircumference / (1 << zoom);
			// The height of a tile is the same as the width because the tiles in Open Street Map are square.
			double tileHeight = 2d * Constants.HalfEarthCircumference / (1 << zoom);

			// Calculate the horizontal distance in meters from the left-most edge of the left-most tile to the center of the given tileX.
			double centerX = (tileX + 0.5d) * tileWidth - Constants.HalfEarthCircumference;
			// Calculate the vertical distance in meters from the top-most edge of the top-most tile to the center of the given tileY.
			double centerY = Constants.HalfEarthCircumference - (tileY + 0.5d) * tileHeight;

			// Convert the centerX value to degrees to get the longitude.
			// The formula derives from the relationship between arc length and angle in a circle (arc length = angle * radius).
			double longitude = centerX / Constants.EarthRadius * 180d / Math.PI;
			// Convert the centerY value to degrees to get the latitude.
			// The formula involves some trigonometry and is used to transform Mercator-projected Y coordinate back to latitude.
			double latitude = (2d * Math.Atan(Math.Exp(centerY / Constants.EarthRadius)) - Math.PI / 2d) * 180d / Math.PI;

			return (latitude, longitude);
		}

		/// <summary>
		/// Convert Open Street Map tile coordinate ==> GPS coordinate.
		/// Latitude is the first parameter.
		/// </summary>
		public static (decimal Latitude, decimal Longitude) OsmTileToGpsAsDecimal(int zoom, int tileX, int tileY)
		{
			// Calculate the width of a single tile at the given zoom level in meters.
			// The total width (2*HalfEarthCircumference) is divided by the number of tiles, which is 2^zoom.
			decimal tileWidth = 2m * Constants.HalfEarthCircumferenceAsDecimal / (1 << zoom);
			// The height of a tile is the same as the width because the tiles in Open Street Map are square.
			decimal tileHeight = 2m * Constants.HalfEarthCircumferenceAsDecimal / (1 << zoom);

			// Calculate the horizontal distance in meters from the left-most edge of the left-most tile to the center of the given tileX.
			decimal centerX = (tileX + 0.5m) * tileWidth - Constants.HalfEarthCircumferenceAsDecimal;
			// Calculate the vertical distance in meters from the top-most edge of the top-most tile to the center of the given tileY.
			decimal centerY = Constants.HalfEarthCircumferenceAsDecimal - (tileY + 0.5m) * tileHeight;

			// Convert the centerX value to degrees to get the longitude.
			// The formula derives from the relationship between arc length and angle in a circle (arc length = angle * radius).
			decimal longitude = centerX / Constants.EarthRadiusAsDecimal * 180m / Constants.PiAsDecimal;

			// Convert the centerY value to degrees to get the latitude.
			// The formula involves some trigonometry and is used to transform Mercator-projected Y coordinate back to latitude.
			decimal latitude = (2m * Atan(Exp(centerY / Constants.EarthRadiusAsDecimal)) - Constants.PiAsDecimal / 2m) * 180m / Constants.PiAsDecimal;

			return (latitude, longitude);
		}

		/// <summary>
		/// <c>decimal</c> version of <see cref="Math.Atan"/>.
		/// For more info see: https://en.wikipedia.org/wiki/Taylor_series.
		/// </summary>
		/// <param name="y">Y-coordinate of the point.</param>
		/// <param name="x">X-coordinate of the point.</param>
		/// <returns>Arctangent of y/x, in radians.</returns>
		/// <remarks>
		/// This function uses the CORDIC (COordinate Rotation DIgital Computer) algorithm to approximate
		/// the value of arctan(y/x). It utilizes a series of trigonometric identities to perform rotations
		/// that gradually approximate the angle whose tangent is y/x.
		///
		/// The CORDIC algorithm is computationally efficient, making it suitable for applications requiring
		/// high performance.
		/// </remarks>
		private static decimal Atan(decimal y, decimal x = 1m)
		{
			decimal[] cordicAngles =
			{
				0.7853981633974483096157M,
				0.4636476090008061162143M,
				0.2449786631268641541720M,
				0.1243549945467614350310M,
				0.0624188099959573489986M,
				0.0312398334302682762533M,
				0.0156237286204768308028M,
				0.0078123410601011112965M,
				0.0039062301319669718276M,
				0.0019531225164788186851M,
				0.0009765621895593194305M,
				0.0004882812111948983010M,
				0.0002441406201493617644M,
				0.0001220703118936702102M,
				0.0000610351561742087726M,
				0.0000305175781155260969M,
				0.0000152587890613157625M,
				0.0000076293945311019702M,
				0.0000038146972656064962M,
				0.0000019073486328101870M,
				0.0000009536743164059601M,
				0.0000004768371582030886M,
				0.0000002384185791015441M,
				0.0000001192092895508223M,
				0.0000000596046447753906M,
			};

			decimal angle = 0.0M;
			decimal currentX = 1.0M;
			decimal currentY = 0.0M;

			for (int j = 0; j < cordicAngles.Length; j++)
			{
				decimal c = (decimal)Math.Cos((double)cordicAngles[j]);
				decimal s = (decimal)Math.Sin((double)cordicAngles[j]);

				if (y > 0)
				{
					decimal tmpX = currentX - currentY * s;
					currentY = currentX * s + currentY * c;
					currentX = tmpX;
					angle += cordicAngles[j];
				}
				else
				{
					decimal tmpX = currentX + currentY * s;
					currentY = -currentX * s + currentY * c;
					currentX = tmpX;
					angle -= cordicAngles[j];
				}
				y -= currentY;
			}

			return angle * x / currentX;
		}

		/// <summary>
		/// <c>decimal</c> version of <see cref="Math.Exp"/>.
		/// For more info see: https://en.wikipedia.org/wiki/Taylor_series.
		/// </summary>
		/// <param name="value">Decimal value to compute the exponential for.</param>
		/// <param name="iterations">Increase for more precision.</param>
		/// <returns>Exponential of the given <paramref name="value"/>.</returns>
		/// <remarks>
		/// This function uses the Taylor series expansion to approximate the value of e^x.
		/// It sums up the first 50 terms of the series; you can adjust maxIterations for more precision.
		/// </remarks>
		private static decimal Exp(decimal value, int iterations = 50)
		{
			decimal result = 1;  // 0! = 1
			decimal term = 1;    // Initial term (x^0 / 0!)

			for (int n = 1; n <= iterations; n++)
			{
				term *= value / n;
				result += term;
			}

			return result;
		}


		#endregion

		#region GPS <==> Rijksdriehoek

		/// <summary>
		/// Create and return rpq coefficients for the transformation from ellipsoidal WGS84 coordinates to RD coordinates.
		/// These values come from the TU Delft university in The Netherlands.
		/// </summary>
		private static double[,] CreateRpq()
		{
			double[,] rpq = new double[4, 5];
			rpq[0, 1] = 190094.945d;
			rpq[0, 2] = -0.008d;
			rpq[0, 3] = -32.391d;
			rpq[1, 0] = -0.705d;
			rpq[1, 1] = -11832.228d;
			rpq[1, 3] = -0.608d;
			rpq[2, 1] = -114.221d;
			rpq[2, 3] = 0.148d;
			rpq[3, 1] = -2.340d;

			return rpq;
		}

		/// <summary>
		/// Create and return spq coefficients for the transformation from ellipsoidal WGS84 coordinates to RD coordinates.
		/// These values come from the TU Delft university in The Netherlands.
		/// </summary>
		private static double[,] CreateSpq()
		{
			double[,] spq = new double[4, 5];
			spq[0, 1] = 0.433d;
			spq[0, 2] = 3638.893d;
			spq[0, 4] = 0.092d;
			spq[1, 0] = 309056.544d;
			spq[1, 1] = -0.032d;
			spq[1, 2] = -157.984d;
			spq[1, 4] = -0.054d;
			spq[2, 0] = 73.077d;
			spq[2, 2] = -6.439d;
			spq[3, 0] = 59.788d;

			return spq;
		}

		/// <summary>
		/// Convert a GPS (WGS84) coordinate into an RD coordinate.
		/// </summary>
		/// <param name="latitudeWgs84">A.k.a. x-axis. Latitude is the measurement of the angle between the equatorial plane and a line that
		/// passes through a point on the Earth's surface. The latitude ranges from 0° at the Equator to 90° (North or South) at the poles.
		/// Latitude lines are parallel circles around the Earth, and they are perpendicular to the Earth's axis.</param>
		/// <param name="longitudeWgs84">A.k.a. y-axis. Longitude is the measurement of the angle along the equatorial plane between the Prime
		/// Meridian (which passes through Greenwich, London, UK) and the point in question on the Earth's surface. Longitude lines run from
		/// the North Pole to the South Pole.</param>
		/// <returns>RD x and y coordinates.</returns>
		/// <exception cref="ArgumentException">Thrown if the GPS coordinate is invalid.</exception>
		public static (double X, double Y) GpsToRd(float latitudeWgs84, float longitudeWgs84)
		{
			return GpsToRd((double)latitudeWgs84, (double)longitudeWgs84);
		}

		/// <inheritdoc cref="GpsToRd(float,float)"/>
		public static (double X, double Y) GpsToRd(double latitudeWgs84, double longitudeWgs84)
		{
			if (!GpsValidator.IsValid(latitudeWgs84, longitudeWgs84))
				throw new ArgumentException($"Invalid GPS coordinate. Got ({latitudeWgs84}, {longitudeWgs84}).");

			/*
			 * For the inverse transformation of ellipsoidal WGS84 coordinates WGS84 coordinates (Φ, λ) to RD-coordinates(X, Y) applies:
			 * dΦ = 0,36(Φ - Φ0)
			 * dλ = 0,36(λ - λ0)
			 * X = X0 + Σp Σq Rpq dΦ^p dλ dλq
			 * Y = Y0 + Σp Σq Spq dΦ^p dλ dλq
			 * where X0, Y0, Φ0, λ0 are also the coordinates of the base point Amersfoort. The coefficients R and S
			 * from the above formula are equal to the rpq arrays below:
			 */

			// Coefficients for the transformation from ellipsoidal WGS84 coordinates to RD coordinates.
			double[,] rpq = CreateRpq();

			// Coefficients for the transformation from ellipsoidal WGS84 coordinates to RD coordinates
			double[,] spq = CreateSpq();

			// Normalize the WGS84 latitude and longitude with respect to Amersfoort coordinates.
			double latitude = 0.36d * (latitudeWgs84 - Constants.AmersfoortWgs84CoordinateX);
			double longitude = 0.36d * (longitudeWgs84 - Constants.AmersfoortWgs84CoordinateY);

			// Initialize variables to hold the calculated RD latitude and longitude.
			double latitudeCalculation = 0;
			double longitudeCalculation = 0;

			for (int p = 0; p < 4; p++)
			{
				for (int q = 0; q < 5; q++)
				{
					// Incrementally add to the RD latitude based on the rpq coefficients.
					latitudeCalculation += rpq[p, q] * Math.Pow(latitude, p) * Math.Pow(longitude, q);

					// Incrementally add to the RD longitude based on the spq coefficients.
					longitudeCalculation += spq[p, q] * Math.Pow(latitude, p) * Math.Pow(longitude, q);
				}
			}

			// Add the calculated RD latitude and longitude to the base Amersfoort RD coordinates.
			double rdX = Constants.AmersfoortRdCoordinateX + latitudeCalculation;
			double rdY = Constants.AmersfoortRdCoordinateY + longitudeCalculation;

			return (rdX, rdY);
		}

		/// <inheritdoc cref="GpsToRd(float,float)"/>
		public static (decimal X, decimal Y) GpsToRd(decimal latitudeWgs84, decimal longitudeWgs84)
		{
			(double X, double Y) rdCoordinate = GpsToRd((double)latitudeWgs84, (double)longitudeWgs84);
			return new((decimal)rdCoordinate.X, (decimal)rdCoordinate.Y);
		}

		/// <summary>
		/// All lines of latitude above the Equator are indicated with the letter 'N'.
		/// The RD and ellipsoidal WGS84 coordinates of the base point Amersfoort.
		/// Compute the sum of terms for latitude adjustment based on a set of coefficients and powers.
		/// These values come from the TU Delft university in The Netherlands.
		/// </summary>
		private static double SumN(double deltaX, double deltaY)
		{
			// Precalculate powers to avoid redundant calculations.
			double deltaX2 = deltaX * deltaX;
			double deltaX4 = deltaX2 * deltaX2;
			double deltaY2 = deltaY * deltaY;
			double deltaY3 = deltaY2 * deltaY;

			return 3235.65389d * deltaY +
				   -32.58297d * deltaX2 +
				   -0.2475d * deltaY2 +
				   -0.84978d * deltaX2 * deltaY +
				   -0.0655d * deltaY3 +
				   -0.01709d * deltaX2 * deltaY2 +
				   -0.00738d * deltaX +
				   0.0053d * deltaX4 +
				   -0.00039d * deltaX2 * deltaY3 +
				   0.00033d * deltaX4 * deltaY +
				   -0.00012d * deltaX * deltaY;
		}

		/// <summary>
		/// All lines of longitude east of the Prime Meridian are indicated with the letter 'E' to denote east of the Prime Meridian.
		/// The RD and ellipsoidal WGS84 coordinates of the base point Amersfoort.
		/// Compute the sum of terms for longitude adjustment based on a set of coefficients and powers.
		/// These values come from the TU Delft university in The Netherlands.
		/// </summary>
		private static double SumE(double deltaX, double deltaY)
		{
			// Calculate powers of deltaX and deltaY that are used more than once.
			double deltaX2 = deltaX * deltaX;  // deltaX^2
			double deltaX3 = deltaX2 * deltaX; // deltaX^3
			double deltaY2 = deltaY * deltaY;  // deltaY^2
			double deltaY3 = deltaY2 * deltaY; // deltaY^3
			double deltaY4 = deltaY3 * deltaY; // deltaY^4

			return 5260.52916d * deltaX +
				   105.94684d * deltaX * deltaY +
				   2.45656d * deltaX * deltaY2 +
				   -0.81885d * deltaX3 +
				   0.05594d * deltaX * deltaY3 +
				   -0.05607d * deltaX3 * deltaY +
				   0.01199d * deltaY +
				   -0.00256d * deltaX3 * deltaY2 +
				   0.00128d * deltaX * deltaY4 +
				   0.00022d * deltaY2 +
				   -0.00022d * deltaX2 +
				   0.00026d * deltaX3 * deltaX2; // deltaX^5, re-using deltaX3 (deltaX^3) and deltaX2 (deltaX^2).
		}

		/// <summary>
		/// Convert an RD coordinate into a GPS (WGS84) coordinate.
		/// </summary>
		/// <param name="rdX">RD x-coordinate.</param>
		/// <param name="rdY">RD y-coordinate.</param>
		/// <returns>GPS latitude (x) and longitude (y).</returns>
		/// <exception cref="ArgumentException">Thrown if the RD-coordinate is invalid.</exception>
		public static (double Latitude, double Longitude) RdToGps(float rdX, float rdY)
		{
			return RdToGps((double)rdX, (double)rdY);
		}

		/// <inheritdoc cref="RdToGps(float,float)"/>
		public static (double Latitude, double Longitude) RdToGps(double rdX, double rdY)
		{
			if (!RdValidator.IsValid(rdX, rdY))
				throw new ArgumentException($"Invalid RD-coordinate. Got ({rdX}, {rdY}).");

			// Convert RD coordinates to a delta relative to Amersfoort, then scale down by 10^5.
			double deltaX = (rdX - Constants.AmersfoortRdCoordinateX) * 0.00001d; //// 0.00001 = Math.Pow(10, -5);
			double deltaY = (rdY - Constants.AmersfoortRdCoordinateY) * 0.00001d; //// 0.00001 = Math.Pow(10, -5);

			double sumN = SumN(deltaX, deltaY);
			double sumE = SumE(deltaX, deltaY);

			// Convert the calculated sums to WGS84 coordinates by adding them to the Amersfoort base coordinates
			// Dividing by 3600 to converts the sums from arcseconds into degrees. Arcseconds are a unit of angular measurement
			// that are used in geography, astronomy, and other fields where angular distances need to be specified.
			// One degree is divided into 60 arcminutes, and each arcminute is further divided into 60 arcseconds.
			// Therefore, one degree contains 3600 arcseconds (60 arcminutes x 60 arcseconds).
			double latitude = Constants.AmersfoortWgs84CoordinateX + (sumN / 3600d);
			double longitude = Constants.AmersfoortWgs84CoordinateY + (sumE / 3600d);

			// Round to 4 decimals because that is the maximum possible accuracy.
			return (Math.Round(latitude, 4), Math.Round(longitude, 4));
		}

		/// <inheritdoc cref="RdToGps(float,float)"/>
		public static (decimal Latitude, decimal Longitude) RdToGps(decimal rdX, decimal rdY)
		{
			// Convert RD coordinates to a delta relative to Amersfoort, then scale down by 10^5.
			double deltaX = ((double)rdX - Constants.AmersfoortRdCoordinateX) * 0.00001d; //// 0.00001 = Math.Pow(10, -5);
			double deltaY = ((double)rdY - Constants.AmersfoortRdCoordinateY) * 0.00001d; //// 0.00001 = Math.Pow(10, -5);

			double sumN = SumN(deltaX, deltaY);
			double sumE = SumE(deltaX, deltaY);

			// Convert the calculated sums to WGS84 coordinates by adding them to the Amersfoort base coordinates
			// Dividing by 3600 to converts the sums from arcseconds into degrees. Arcseconds are a unit of angular measurement
			// that are used in geography, astronomy, and other fields where angular distances need to be specified.
			// One degree is divided into 60 arcminutes, and each arcminute is further divided into 60 arcseconds.
			// Therefore, one degree contains 3600 arcseconds (60 arcminutes x 60 arcseconds).
			double latitude = Constants.AmersfoortWgs84CoordinateX + (sumN / 3600d);
			double longitude = Constants.AmersfoortWgs84CoordinateY + (sumE / 3600d);

			// Round to 4 decimals because that is the maximum possible accuracy.
			return ((decimal)Math.Round(latitude, 4), (decimal)Math.Round(longitude, 4));
		}

		#endregion
	}
}
