using System;

namespace Elephant.GeoSystems.Converters
{
	/// <summary>
	/// Positioning systems converter constants.
	/// </summary>
	public static class Constants
	{
		#region Earth

		/// <summary>
		/// The Earth's radius in meters (mean value, as the Earth is an oblate spheroid).
		/// The value 6,371,000 meters is often used as a rounded, average value for the Earth's radius.
		/// The value 6,378,137 meters is the semi-major axis of the Earth according to the
		/// WGS 84 standard, which is widely used in cartography and navigation.
		/// </summary>
		public const double EarthRadius = 6378137d;

		/// <inheritdoc cref="EarthRadius"/>
		public const decimal EarthRadiusAsDecimal = 6378137m;

		/// <summary>
		/// Half the circumference (=omtrek in Dutch) of the Earth.
		/// </summary>
		public const double HalfEarthCircumference = Math.PI * EarthRadius;

		/// <summary>
		/// PI as a decimal.
		/// </summary>
		public const decimal PiAsDecimal = 3.14159265358979323846264338327950288419716939937510m;

		/// <inheritdoc cref="HalfEarthCircumference"/>
		public const decimal HalfEarthCircumferenceAsDecimal = PiAsDecimal * EarthRadiusAsDecimal;

		#endregion

		#region RD

		/// <summary>
		/// WSG84 GPS x coordinate of the City Amersfoort (in The Netherlands).
		/// </summary>
		public const double AmersfoortWgs84CoordinateX = 52.15517440d;

		/// <summary>
		/// WSG84 GPS y coordinate of the City Amersfoort (in The Netherlands).
		/// </summary>
		public const double AmersfoortWgs84CoordinateY = 5.38720621d;

		/// <summary>
		/// WSG84 GPS x coordinate of the City Amersfoort (in The Netherlands) as a <see cref="decimal"/>.
		/// </summary>
		public const decimal AmersfoortWgs84CoordinateXAsDecimal = 52.15517440m;

		/// <summary>
		/// WSG84 GPS y coordinate of the City Amersfoort (in The Netherlands) as a <see cref="decimal"/>.
		/// </summary>
		public const decimal AmersfoortWgs84CoordinateYAsDecimal = 5.38720621m;

		/// <summary>
		/// <inheritdoc cref="AmersfoortWgs84CoordinateX"/>
		/// </summary>
		public const int AmersfoortRdCoordinateX = 155000;

		/// <summary>
		/// RD (=Rijksdriehoek) y coordinate of the City Amersfoort (in The Netherlands).
		/// </summary>
		public const int AmersfoortRdCoordinateY = 463000;

		#endregion
	}
}
