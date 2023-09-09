using System.Globalization;
using Elephant.GeoSystems.Validators;

namespace Elephant.GeoSystems.Tests.Validators
{
	/// <summary>
	/// <see cref="GpsValidator.IsValid(float,float)"/> tests.
	/// </summary>
	public class GpsValidatorTests
	{
		#region Float

		/// <summary>
		/// <see cref="GpsValidator.IsValid(float,float)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(50.85204602321809f, 5.693181181444698f)] // Maastricht.
		[InlineData(51.4370644101525f, 5.483039577727214f)] // Eindhoven.
		[InlineData(81.977125f, -41.745396f)] // Greenland.
		[InlineData(-84.008727f, 21.575477f)] // Antarctica.
		[InlineData(44.129232f, -98.163781f)] // South Dakota.
		[InlineData(33.290083f, 105.749336f)] // HanzHong, china.
		[InlineData(-29.358614f, 23.404558f)] // Bo Karoo, South Africa.
		public void IsValidFloat(float latitude, float longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(float,float)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(-91f, 5.693181181444698f)]
		[InlineData(91f, 5.693181181444698f)]
		[InlineData(51.4370644101525f, -181f)]
		[InlineData(51.4370644101525f, 181f)]
		[InlineData(-91f, -181f)]
		[InlineData(91f, 181f)]
		[InlineData(float.MinValue, float.MinValue)]
		[InlineData(float.MaxValue, float.MaxValue)]
		public void IsInvalidFloat(float latitude, float longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(float?,float?)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(50.85204602321809f, 5.693181181444698f)] // Maastricht.
		[InlineData(51.4370644101525f, 5.483039577727214f)] // Eindhoven.
		[InlineData(81.977125f, -41.745396f)] // Greenland.
		[InlineData(-84.008727f, 21.575477f)] // Antarctica.
		[InlineData(44.129232f, -98.163781f)] // South Dakota.
		[InlineData(33.290083f, 105.749336f)] // HanzHong, china.
		[InlineData(-29.358614f, 23.404558f)] // Bo Karoo, South Africa.
		public void IsValidNullableFloat(float? latitude, float? longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(float?,float?)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(-91f, 5.693181181444698f)]
		[InlineData(91f, 5.693181181444698f)]
		[InlineData(51.4370644101525f, -181f)]
		[InlineData(51.4370644101525f, 181f)]
		[InlineData(-91f, -181f)]
		[InlineData(91f, 181f)]
		[InlineData(float.MinValue, float.MinValue)]
		[InlineData(float.MaxValue, float.MaxValue)]
		[InlineData(50.85204602321809f, null)]
		[InlineData(null, 5.693181181444698f)]
		[InlineData(null, null)]

		public void IsInvalidNullableFloat(float? latitude, float? longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		#endregion

		#region Double

		/// <summary>
		/// <see cref="GpsValidator.IsValid(double,double)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(81.977125d, -41.745396d)] // Greenland.
		[InlineData(-84.008727d, 21.575477d)] // Antarctica.
		[InlineData(44.129232d, -98.163781d)] // South Dakota.
		[InlineData(33.290083d, 105.749336d)] // HanzHong, china.
		[InlineData(-29.358614d, 23.404558d)] // Bo Karoo, South Africa.
		public void IsValidDouble(double latitude, double longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(double,double)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(-91d, 5.693181181444698d)]
		[InlineData(91d, 5.693181181444698d)]
		[InlineData(51.4370644101525d, -181d)]
		[InlineData(51.4370644101525d, 181d)]
		[InlineData(-91d, -181d)]
		[InlineData(91d, 181d)]
		[InlineData(double.MinValue, double.MinValue)]
		[InlineData(double.MaxValue, double.MaxValue)]
		public void IsInvalidDouble(double latitude, double longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(double?,double?)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(81.977125d, -41.745396d)] // Greenland.
		[InlineData(-84.008727d, 21.575477d)] // Antarctica.
		[InlineData(44.129232d, -98.163781d)] // South Dakota.
		[InlineData(33.290083d, 105.749336d)] // HanzHong, china.
		[InlineData(-29.358614d, 23.404558d)] // Bo Karoo, South Africa.
		public void IsValidNullableDouble(double? latitude, double? longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(double?,double?)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(-91d, 5.693181181444698d)]
		[InlineData(91d, 5.693181181444698d)]
		[InlineData(51.4370644101525d, -181d)]
		[InlineData(51.4370644101525d, 181d)]
		[InlineData(-91d, -181d)]
		[InlineData(91d, 181d)]
		[InlineData(double.MinValue, double.MinValue)]
		[InlineData(double.MaxValue, double.MaxValue)]
		[InlineData(50.85204602321809d, null)]
		[InlineData(null, 5.693181181444698d)]
		[InlineData(null, null)]

		public void IsInvalidNullableDouble(double? latitude, double? longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		#endregion

		#region Decimal

		/// <summary>
		/// <see cref="GpsValidator.IsValid(decimal,decimal)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(81.977125d, -41.745396d)] // Greenland.
		[InlineData(-84.008727d, 21.575477d)] // Antarctica.
		[InlineData(44.129232d, -98.163781d)] // South Dakota.
		[InlineData(33.290083d, 105.749336d)] // HanzHong, china.
		[InlineData(-29.358614d, 23.404558d)] // Bo Karoo, South Africa.
		public void IsValidDecimal(decimal latitude, decimal longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(decimal,decimal)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(-91d, 5.693181181444698d)]
		[InlineData(91d, 5.693181181444698d)]
		[InlineData(51.4370644101525d, -181d)]
		[InlineData(51.4370644101525d, 181d)]
		[InlineData(-91d, -181d)]
		[InlineData(91d, 181d)]
		[InlineData(int.MinValue, int.MinValue)]
		[InlineData(int.MaxValue, int.MaxValue)]
		public void IsInvalidDecimal(decimal latitude, decimal longitude)
		{
			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(decimal?,decimal?)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("50.85204602321809", "5.693181181444698")] // Maastricht.
		[InlineData("51.4370644101525", "5.483039577727214")] // Eindhoven.
		[InlineData("81.977125", " -41.745396")] // Greenland.
		[InlineData("-84.008727", "21.575477")] // Antarctica.
		[InlineData("44.129232", " -98.163781")] // South Dakota.
		[InlineData("33.290083", "105.749336")] // HanzHong, china.
		[InlineData("-29.358614", "23.404558")] // Bo Karoo, South Africa.
		public void IsValidNullableDecimal(string? latitudeAsString, string? longitudeAsString)
		{
			// Arrange.
			decimal? latitude = Convert.ToDecimal(latitudeAsString, CultureInfo.InvariantCulture);
			decimal? longitude = Convert.ToDecimal(longitudeAsString, CultureInfo.InvariantCulture);

			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="GpsValidator.IsValid(decimal?,decimal?)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("-91", "5.693181181444698")]
		[InlineData("91", "5.693181181444698")]
		[InlineData("51.4370644101525", "-181")]
		[InlineData("51.4370644101525", "181")]
		[InlineData("-91", "-181")]
		[InlineData("91", "181")]
		[InlineData("50.85204602321809", null)]
		[InlineData(null, "5.693181181444698")]
		[InlineData(null, null)]

		public void IsInvalidNullableDecimal(string? latitudeAsString, string? longitudeAsString)
		{
			// Arrange.
			decimal? latitude = latitudeAsString == null ? null : Convert.ToDecimal(latitudeAsString, CultureInfo.InvariantCulture);
			decimal? longitude = longitudeAsString == null ? null : Convert.ToDecimal(longitudeAsString, CultureInfo.InvariantCulture);

			// Act.
			bool isValid = GpsValidator.IsValid(latitude, longitude);

			// Assert.
			Assert.False(isValid);
		}

		#endregion
	}
}