using Elephant.GeoSystems.Converters;
#if !NETSTANDARD2_0 && !NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace Elephant.GeoSystems.Shared
{
	/// <summary>
	/// A struct for Rijksdriehoekscoördinaten, WGS84 (=GPS), Open Street Map Tile coordinates and custom coordinates using <see cref="decimal"/>.
	/// In this version, the Rd is the leader.
	/// </summary>
	public class GeoMultiCoordRd
	{
		/// <summary>
		/// Open Street Map minimum zoom.
		/// For more info see: https://wiki.openstreetmap.org/wiki/Zoom_levels.
		/// </summary>
		public const int OpenStreetMapZoomMin = 1;

		/// <summary>
		/// Open Street Map maximum zoom.
		/// For more info see: https://wiki.openstreetmap.org/wiki/Zoom_levels.
		/// </summary>
		public const int OpenStreetMapZoomMax = 20;

		/// <summary>
		/// Protected backing field Rijksdriehoek x-coordinate.
		/// </summary>
		// ReSharper disable once InconsistentNaming
#if DEBUG
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Naming conflict.")]
#endif
		protected decimal _rdX { get; set; } = Constants.AmersfoortRdCoordinateX;

		/// <summary>
		/// Protected backing field Rijksdriehoek y-coordinate.
		/// </summary>
		// ReSharper disable once InconsistentNaming
#if DEBUG
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules",
			"SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Naming conflict.")]
#endif
		protected decimal _rdY { get; set; } = Constants.AmersfoortRdCoordinateY;

		private decimal? _gpsLatitude = null;
		private decimal? _gpsLongitude = null;
		private decimal? _osmX = null;
		private decimal? _osmY = null;
		private decimal? _customX = null;
		private decimal? _customY = null;
		private int _openStreetMapZoom = 1;

		/// <summary>
		/// Open Street Map zoom.
		/// For more info see: https://wiki.openstreetmap.org/wiki/Zoom_levels.
		/// Default value is 1, which equals 1:500 million (the whole world).
		/// Value must be between 1-20 (inclusive) and will be clamped if it falls outside of that range.
		/// </summary>
		public int OpenStreetMapZoom
		{
			get => _openStreetMapZoom;
			set => _openStreetMapZoom = Clamp(value, OpenStreetMapZoomMin, OpenStreetMapZoomMax);
		}

		/// <summary>
		/// X Rd coordinate.
		/// </summary>
		public decimal RdX
		{
			get => _rdX;
		}

		/// <summary>
		/// Y Rd coordinate.
		/// </summary>
		public decimal RdY
		{
			get => _rdY;
		}

		/// <summary>
		/// Latitude or x-coordinate WGS84 (=GPS) coordinate.
		/// </summary>
		public decimal GpsLatitude
		{
			get
			{
				if (_gpsLatitude == null)
					CalculateGps();

				return (decimal)_gpsLatitude!;
			}
		}

		/// <summary>
		/// Longitude or y-coordinate WGS84 (=GPS)
		/// </summary>
		public decimal GpsLongitude
		{
			get
			{
				if (_gpsLongitude == null)
					CalculateGps();

				return (decimal)_gpsLongitude!;
			}
		}

		/// <summary>
		/// X Open Street Map coordinate.
		/// </summary>
		public decimal OsmX
		{
			get
			{
				if (_osmX == null)
					CalculateOpenStreetMapTile();

				return (decimal)_osmX!;
			}
		}

		/// <summary>
		/// Y Open Street Map coordinate.
		/// </summary>
		public decimal OsmY
		{
			get
			{
				if (_osmY == null)
					CalculateOpenStreetMapTile();

				return (decimal)_osmY!;
			}
		}

		/// <summary>
		/// X Custom coordinate.
		/// </summary>
		public decimal CustomX
		{
			get
			{
				if (_customX == null)
					CalculateCustom();

				return (decimal)_customX!;
			}
		}

		/// <summary>
		/// Y Custom coordinate.
		/// </summary>
		public decimal CustomY
		{
			get
			{
				if (_customY == null)
					CalculateCustom();

				return (decimal)_customY!;
			}
		}

		/// <summary>
		/// Constructor with <paramref name="rdX"/> and <paramref name="rdX"/> initializers.
		/// </summary>
		/// <param name="rdX">X Rd coordinate.</param>
		/// <param name="rdY">Y Rd coordinate.</param>
		/// <param name="openStreetMapZoom">Open Street Map zoom level (1-20). For more info see: https://wiki.openstreetmap.org/wiki/Zoom_levels.</param>
		public GeoMultiCoordRd(decimal rdX, decimal rdY, int openStreetMapZoom = 1)
		{
			OpenStreetMapZoom = openStreetMapZoom;
			SetRds(rdX, rdY);
		}

		/// <summary>
		/// Set new RD coordinate. If this new value doesn't equal the current value
		/// then all cached values will be cleared.
		/// </summary>
		public void SetRds(decimal rdX, decimal rdY)
		{
			// If the new values equal the old values then do nothing.
			if (RdX == rdX && RdY == rdY)
				return;

			_rdX = rdX;
			_rdY = rdY;
			ResetNonRdCoordinates();
		}

		#region Calculate

		/// <summary>
		/// Calculate GPS (=WGS84) coordinate from RD coordinate.
		/// </summary>
		protected virtual void CalculateGps()
		{
			(decimal Latitude, decimal Longitude) gps = ConverterUtils.RdToGps(RdX, RdY);
			_gpsLatitude = gps.Latitude;
			_gpsLongitude = gps.Longitude;
		}

		/// <summary>
		/// Calculate Open Street Map tile coordinate from RD coordinate.
		/// </summary>
		protected virtual void CalculateOpenStreetMapTile()
		{
			if (_gpsLatitude == null)
				CalculateGps();

			(int TileX, int TileY) osm = ConverterUtils.GpsToOsmTile(OpenStreetMapZoom, (decimal)_gpsLatitude!, (decimal)_gpsLongitude!);
			_osmX = osm.TileX;
			_osmY = osm.TileY;
		}

		/// <summary>
		/// Calculate Custom coordinate from RD coordinate.
		/// </summary>
		protected virtual void CalculateCustom()
		{
			_customX = RdX;
			_customY = RdY;
		}

		#endregion

		private void ResetNonRdCoordinates()
		{
			_gpsLatitude = _gpsLongitude = _osmX = _osmY = _customX = _customY = null;
		}

		private static int Clamp(int value, int min, int max)
		{
			if (value < min) return min;
			if (value > max) return max;
			return value;
		}

		#region From

		/// <summary>
		/// Convert RD coordinate into a custom coordinate.
		/// </summary>
		public virtual void FromCustom(decimal customX, decimal customY)
		{
			ResetNonRdCoordinates();
			_rdX = customX;
			_rdY = customY;
			CalculateCustom();
		}

		/// <summary>
		/// Convert GPS (=WGS84) coordinate into an RD coordinate.
		/// </summary>
		public virtual void FromGps(decimal gpsX, decimal gpsY)
		{
			(decimal rdX, decimal rdY) rdCoordinate = ConverterUtils.GpsToRd(gpsX, gpsY);
			ResetNonRdCoordinates();
			_rdX = rdCoordinate.rdX;
			_rdY = rdCoordinate.rdY;
		}

		/// <summary>
		/// Convert an Open Street Map tile coordinate into a GPS (=WGS84) coordinate.
		/// </summary>
		public virtual void FromOsm(int zoom, int tileX, int tileY)
		{
			(decimal Latitude, decimal Longitude) rdCoordinate = ConverterUtils.OsmTileToGpsAsDecimal(zoom, tileX, tileY);
			ResetNonRdCoordinates();
			_rdX = rdCoordinate.Latitude;
			_rdY = rdCoordinate.Longitude;
		}

		#endregion

		// TODO: Implement distance and such for all positioning systems.
		/////// <summary>
		/////// <inheritdoc />
		/////// </summary>
		////public float Distance(IRdCoordinate<float> otherRdCoordinate)
		////{
		////	return MathRd.Distance(_rdX, _rdY, otherRdCoordinate.X, otherRdCoordinate.Y);
		////}

		/////// <summary>
		/////// <inheritdoc />
		/////// </summary>
		////public float Distance(RdCoordinate? otherRdCoordinate)
		////{
		////	if (otherRdCoordinate == null)
		////		return 0f;

		////	return MathRd.Distance(_rdX, _rdY, otherRdCoordinate.Value.X, otherRdCoordinate.Value.Y);
		////}

		/////// <summary>
		/////// <inheritdoc cref="MathRd.TryParseFromPointString(string, out float, out float)"/>
		/////// </summary>
		////public static RdCoordinate? TryParseFromPointString(string pointString)
		////{
		////	if (MathRd.TryParseFromPointString(pointString, out float x, out float y))
		////		return new RdCoordinate(x, y);

		////	return null;
		////}

		/////// <summary>
		/////// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a + b.
		/////// </summary>
		////public static RdCoordinate operator +(RdCoordinate a, RdCoordinate b) => new(a.X + b.X, a.Y + b.Y);

		/////// <summary>
		/////// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a - b.
		/////// </summary>
		////public static RdCoordinate operator -(RdCoordinate a, RdCoordinate b) => new(a.X - b.X, a.Y - b.Y);

		/////// <summary>
		/////// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a * b.
		/////// </summary>
		////public static RdCoordinate operator *(RdCoordinate a, RdCoordinate b) => new(a.X * b.X, a.Y * b.Y);

		/////// <summary>
		/////// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a / b.
		/////// </summary>
		////public static RdCoordinate operator /(RdCoordinate a, RdCoordinate b)
		////{
		////	if (b.X == 0f || b.Y == 0f)
		////		throw new DivideByZeroException($"Cannot divide {a} by {b}. Both b.X and b.Y cannot be zero.");

		////	return new(a.X / b.X, a.Y / b.Y);
		////}

		/////// <summary>
		/////// Check if the two <see cref="RdCoordinate"/>s are equal.
		/////// </summary>
		////public static bool operator ==(RdCoordinate left, RdCoordinate right) => left.Equals(right);

		/////// <summary>
		/////// Check if the two <see cref="RdCoordinate"/>s are not equal.
		/////// </summary>
		////public static bool operator !=(RdCoordinate left, RdCoordinate right) => !(left == right);

		////		/// <summary>
		////		/// <inheritdoc />
		////		/// </summary>
		////#if NETSTANDARD2_0 || NETSTANDARD2_1
		////		public override bool Equals(object? obj)
		////#else
		////		public override bool Equals([NotNullWhen(true)] object? obj)
		////#endif
		////		{
		////			RdCoordinate? other = obj as RdCoordinate?;
		////			if (other == null)
		////				return false;

		////#if NETSTANDARD2_0 || NETSTANDARD2_1
		////			float tempResult = X - other.Value.X;
		////			return (tempResult >= 0 ? tempResult : -tempResult) <= float.Epsilon;
		////#else
		////			return MathF.Abs(X - other.Value.X) <= float.Epsilon;
		////#endif
		////		}

		////		/// <summary>
		////		/// <inheritdoc />
		////		/// </summary>
		////		public override int GetHashCode()
		////		{
		////			unchecked
		////			{
		////				int hash = 17;
		////				hash *= 23 + X.GetHashCode();
		////				hash *= 23 + Y.GetHashCode();
		////				return hash;
		////			}
		////		}

		/// <summary>
		/// <inheritdoc />
		/// </summary>
		public override string ToString()
		{
			return $"{RdX},{RdY}";
		}
	}
}
