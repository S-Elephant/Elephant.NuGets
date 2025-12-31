namespace Elephant.GeoSystems.Validators
{
	/// <summary>
	/// Positioning validation.
	/// </summary>
	public static class GpsValidator
	{
		/// <summary>
		/// Minimum valid GPS (WGS84) latitude value (inclusive).
		/// </summary>
		public const int LatitudeMin = -90;

		/// <summary>
		/// Maximum valid GPS (WGS84) latitude value (inclusive).
		/// </summary>
		public const int LatitudeMax = 90;

		/// <summary>
		/// Minimum valid GPS (WGS84) longitude value (inclusive).
		/// </summary>
		public const int LongitudeMin = -180;

		/// <summary>
		/// Maximum valid GPS (WGS84) longitude value (inclusive).
		/// </summary>
		public const int LongitudeMax = 180;

		/// <summary>
		/// Checks if the given latitude and longitude coordinates are valid WGS84 coordinates.
		/// </summary>
		/// <param name="latitude">The latitude to validate, expected to be between -90 and 90.</param>
		/// <param name="longitude">The longitude to validate, expected to be between -180 and 180.</param>
		/// <returns><c>true</c> if the values are valid WGS84.</returns>
		public static bool IsValid(float latitude, float longitude)
		{
			return latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}

		/// <summary>
		/// Checks if the given latitude and longitude coordinates are valid WGS84.
		/// If any value is <c>null</c> then the coordinate is always invalid.
		/// </summary>
		/// <param name="latitude">The latitude to validate, expected to be between -90 and 90.</param>
		/// <param name="longitude">The longitude to validate, expected to be between -180 and 180.</param>
		/// <returns><c>true</c> if the value are valid WGS84 and if both values are not null.</returns>
		public static bool IsValid(float? latitude, float? longitude)
		{
			return latitude != null && longitude != null &&
				   latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}

		/// <inheritdoc cref="IsValid(float,float)"/>
		public static bool IsValid(double latitude, double longitude)
		{
			return latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}

		/// <inheritdoc cref="IsValid(float?,float?)"/>
		public static bool IsValid(double? latitude, double? longitude)
		{
			return latitude != null && longitude != null &&
				   latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}

		/// <inheritdoc cref="IsValid(float,float)"/>
		public static bool IsValid(decimal latitude, decimal longitude)
		{
			return latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}

		/// <inheritdoc cref="IsValid(float?,float?)"/>
		public static bool IsValid(decimal? latitude, decimal? longitude)
		{
			return latitude != null && longitude != null &&
				   latitude >= LatitudeMin && latitude <= LatitudeMax &&
				   longitude >= LongitudeMin && longitude <= LongitudeMax;
		}
	}
}
