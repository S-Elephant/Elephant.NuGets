using System.Globalization;
using Elephant.GeoSystems.Validators;

namespace Elephant.GeoSystems.Tests.Validators
{
	/// <summary>
	/// <see cref="RdValidator.IsValid(float,float)"/> tests.
	/// </summary>
	public class RdValidatorTests
	{
		#region Float

		/// <summary>
		/// <see cref="RdValidator.IsValid(float,float)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548f, 318068f)] // Maastricht.
		[InlineData(161664f, 383111f)] // Eindhoven.
		[InlineData(133561f, 397126f)] // Tilburg.
		[InlineData(112691f, 400437f)] // Breda.
		[InlineData(32143f, 391346f)] // Middelburg.
		[InlineData(91873f, 436967f)] // Rotterdam.
		[InlineData(80300f, 453738f)] // The Hague.
		[InlineData(121605f, 487759f)] // Amsterdam.
		[InlineData(182687f, 579310f)] // Leeuwarden.
		[InlineData(233627f, 581737f)] // Groningen.
		[InlineData(256494f, 534047f)] // Emmen.
		[InlineData(257781f, 471341f)] // Enschede.
		[InlineData(185826f, 424987f)] // Nijmegen.
		[InlineData(154675f, 463049f)] // Amersfoort.
		[InlineData(227846f, 618116f)] // Rottumerplaat.
		public void IsValidFloat(float rdX, float rdY)
		{
			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="RdValidator.IsValid(float,float)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(RdValidator.RdMinX - 1f, 318068f)]
		[InlineData(RdValidator.RdMaxX + 1f, 318068f)]
		[InlineData(176548f, RdValidator.RdMinY - 1f)]
		[InlineData(176548f, RdValidator.RdMaxY + 1f)]
		[InlineData(RdValidator.RdMinX - 1f, RdValidator.RdMaxY + 1f)]
		[InlineData(float.MinValue, float.MaxValue)]
		[InlineData(float.MaxValue, float.MinValue)]
		[InlineData(0f, 0f)]
		[InlineData(176548f, null)]
		[InlineData(null, 318068f)]
		[InlineData(null, null)]
		public void IsInvalidFloat(float? rdX, float? rdY)
		{
			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.False(isValid);
		}

		#endregion

		#region Double

		/// <summary>
		/// <see cref="RdValidator.IsValid(double,double)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548d, 318068d)] // Maastricht.
		[InlineData(161664d, 383111d)] // Eindhoven.
		[InlineData(133561d, 397126d)] // Tilburg.
		[InlineData(112691d, 400437d)] // Breda.
		[InlineData(32143d, 391346d)] // Middelburg.
		[InlineData(91873d, 436967d)] // Rotterdam.
		[InlineData(80300d, 453738d)] // The Hague.
		[InlineData(121605d, 487759d)] // Amsterdam.
		[InlineData(182687d, 579310d)] // Leeuwarden.
		[InlineData(233627d, 581737d)] // Groningen.
		[InlineData(256494d, 534047d)] // Emmen.
		[InlineData(257781d, 471341d)] // Enschede.
		[InlineData(185826d, 424987d)] // Nijmegen.
		[InlineData(154675d, 463049d)] // Amersfoort.
		[InlineData(227846d, 618116d)] // Rottumerplaat.
		public void IsValidDouble(double rdX, double rdY)
		{
			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="RdValidator.IsValid(double,double)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(RdValidator.RdMinX - 1d, 318068d)]
		[InlineData(RdValidator.RdMaxX + 1d, 318068d)]
		[InlineData(176548d, RdValidator.RdMinY - 1d)]
		[InlineData(176548d, RdValidator.RdMaxY + 1d)]
		[InlineData(RdValidator.RdMinX - 1d, RdValidator.RdMaxY + 1d)]
		[InlineData(double.MinValue, double.MaxValue)]
		[InlineData(double.MaxValue, double.MinValue)]
		[InlineData(0d, 0d)]
		[InlineData(176548d, null)]
		[InlineData(null, 318068d)]
		[InlineData(null, null)]
		public void IsInvalidDouble(double? rdX, double? rdY)
		{
			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.False(isValid);
		}

		#endregion

		#region Decimals

		/// <summary>
		/// <see cref="RdValidator.IsValid(decimal,decimal)"/> tests should all be valid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("176548", "318068")] // Maastricht.
		[InlineData("161664", "383111")] // Eindhoven.
		[InlineData("133561", "397126")] // Tilburg.
		[InlineData("112691", "400437")] // Breda.
		[InlineData("32143", "391346")] // Middelburg.
		[InlineData("91873", "436967")] // Rotterdam.
		[InlineData("80300", "453738")] // The Hague.
		[InlineData("121605", "487759")] // Amsterdam.
		[InlineData("182687", "579310")] // Leeuwarden.
		[InlineData("233627", "581737")] // Groningen.
		[InlineData("256494", "534047")] // Emmen.
		[InlineData("257781", "471341")] // Enschede.
		[InlineData("185826", "424987")] // Nijmegen.
		[InlineData("154675", "463049")] // Amersfoort.
		[InlineData("227846", "618116")] // Rottumerplaat.
		public void IsValidDecimal(string rdXAsString, string rdYAsString)
		{
			// Arrange.
			decimal? rdX = Convert.ToDecimal(rdXAsString, CultureInfo.InvariantCulture);
			decimal? rdY = Convert.ToDecimal(rdYAsString, CultureInfo.InvariantCulture);

			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.True(isValid);
		}

		/// <summary>
		/// <see cref="RdValidator.IsValid(decimal,decimal)"/> tests should all be invalid.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData("-1", "318068")]
		[InlineData("280001", "318068")]
		[InlineData("176548", "299999")]
		[InlineData("176548", "625001")]
		[InlineData("299999", "625001")]
		[InlineData("-53453423", "3974534")]
		[InlineData("3974534", "-53453423")]
		[InlineData("0", "0")]
		[InlineData("176548", null)]
		[InlineData(null, "318068")]
		[InlineData(null, null)]
		public void IsInvalidDecimal(string? rdXAsString, string? rdYAsString)
		{
			// Arrange.
			decimal? rdX = rdXAsString == null ? null : Convert.ToDecimal(rdXAsString, CultureInfo.InvariantCulture);
			decimal? rdY = rdYAsString == null ? null : Convert.ToDecimal(rdYAsString, CultureInfo.InvariantCulture);

			// Act.
			bool isValid = RdValidator.IsValid(rdX, rdY);

			// Assert.
			Assert.False(isValid);
		}

		#endregion
	}
}