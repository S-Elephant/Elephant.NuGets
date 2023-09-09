namespace Elephant.GeoSystems.Validators
{
	/// <summary>
	/// Positioning validation.
	/// </summary>
	public static class RdValidator
	{
		/// <summary>
		/// Minimum allowed valid Rd X coordinate (in meter, inclusive).
		/// </summary>
		public const int RdMinX = 0;

		/// <summary>
		/// Maximum allowed valid Rd X coordinate (in meter, inclusive).
		/// </summary>
		public const int RdMaxX = 280000;

		/// <summary>
		/// Minimum allowed valid Rd Y coordinate (in meter, inclusive).
		/// </summary>
		public const int RdMinY = 300000;

		/// <summary>
		/// Maximum allowed valid Rd Y coordinate (in meter, inclusive).
		/// </summary>
		public const int RdMaxY = 625000;

		/// <summary>
		/// <para>Returns true if the supplied values are considered to be a valid Rijksdriehoekscoördinaat.</para>
		/// <para>x-coordinate must be between 0 and 280000 and the Y-coordinate must be between 300000 and
		/// 625000.</para>
		/// </summary>
		/// <param name="rdX">RD x-coordinate in meter.</param>
		/// <param name="rdY">RD y-coordinate in meter.</param>
		/// <returns><c>true</c> if both RD coordinates are within the RD limits.</returns>
		public static bool IsValid(float rdX, float rdY)
		{
			return rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}

		/// <summary>
		/// <para>Returns true if the supplied values are considered to be a valid Rijksdriehoekscoördinaat.</para>
		/// <para>x-coordinate must be between 0 and 280000 and the Y-coordinate must be between 300000 and
		/// 625000.</para>
		/// <para>Any <c>null</c> value counts as invalid.</para>
		/// </summary>
		/// <param name="rdX">RD x-coordinate in meter.</param>
		/// <param name="rdY">RD y-coordinate in meter.</param>
		/// <returns><c>true</c> if both RD coordinates are within the RD limits and when both values are not <c>null</c>.</returns>
		public static bool IsValid(float? rdX, float? rdY)
		{
			return rdX != null && rdY != null &&
			       rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}

		/// <inheritdoc cref="IsValid(float,float)"/>
		public static bool IsValid(double rdX, double rdY)
		{
			return rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}

		/// <inheritdoc cref="IsValid(float?,float?)"/>
		public static bool IsValid(double? rdX, double? rdY)
		{
			return rdX != null && rdY != null &&
			       rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}

		/// <inheritdoc cref="IsValid(float,float)"/>
		public static bool IsValid(decimal rdX, decimal rdY)
		{
			return rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}

		/// <inheritdoc cref="IsValid(float?,float?)"/>
		public static bool IsValid(decimal? rdX, decimal? rdY)
		{
			return rdX != null && rdY != null &&
			       rdX >= RdMinX && rdX <= RdMaxX &&
			       rdY >= RdMinY && rdY <= RdMaxY;
		}
	}
}
