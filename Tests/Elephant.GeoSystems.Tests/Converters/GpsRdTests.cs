using Elephant.GeoSystems.Converters;

namespace Elephant.GeoSystems.Tests.Converters
{
	/// <summary>
	/// Conversion tests between GPS and Rijksdriehoek.
	/// </summary>
	public class GpsRdTests
	{
		private const double Tolerance = 1e-4;

		/// <summary>
		/// <see cref="ConverterUtils.RdToGps(float,float)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548f, 318068f, 50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(161664f, 383111f, 51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(133561f, 397126f, 51.56267096731468d, 5.078016769674488d)] // Tilburg.
		[InlineData(112691f, 400437f, 51.591244471919005d, 4.776662178460902d)] // Breda.
		[InlineData(32143f, 391346f, 51.497724935924936d, 3.617838516788694d)] // Middelburg.
		[InlineData(91873f, 436967f, 51.91760749459275d, 4.46962367068906d)] // Rotterdam.
		[InlineData(80300f, 453738f, 52.06688833415241d, 4.297768334323142d)] // The Hague.
		[InlineData(121605f, 487759f, 52.37668890821108d, 4.896782660725934d)] // Amsterdam.
		[InlineData(182687f, 579310f, 53.19971993186204d, 5.801522396941571d)] // Leeuwarden.
		[InlineData(233627f, 581737f, 53.21647004196559d, 6.564292486864583d)] // Groningen.
		[InlineData(256494f, 534047f, 52.78420195511749d, 6.891631130023316d)] // Emmen.
		[InlineData(257781f, 471341f, 52.22056106147205d, 6.891387579659575d)] // Enschede.
		[InlineData(185826f, 424987f, 51.81265295534943d, 5.834226504298018d)] // Nijmegen.
		[InlineData(154675f, 463049f, 52.15561201090535d, 5.382458244825791d)] // Amersfoort.
		[InlineData(227846f, 618116f, 53.54411077235744d, 6.486070323938958d)] // Rottumerplaat.
		public void RdsToGpsFloat(float rdX, float rdY, double expectedLatitude, double expectedLongitude)
		{
			// Act.
			(double X, double Y) = ConverterUtils.RdToGps(rdX, rdY);

			// Assert.
			Assert.Equal(expectedLatitude, X, Tolerance);
			Assert.Equal(expectedLongitude, Y, Tolerance);
		}

		/// <summary>
		/// <see cref="ConverterUtils.RdToGps(double,double)"/> tests.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548d, 318068d, 50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(161664d, 383111d, 51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(133561d, 397126d, 51.56267096731468d, 5.078016769674488d)] // Tilburg.
		[InlineData(112691d, 400437d, 51.591244471919005d, 4.776662178460902d)] // Breda.
		[InlineData(32143d, 391346d, 51.497724935924936d, 3.617838516788694d)] // Middelburg.
		[InlineData(91873d, 436967d, 51.91760749459275d, 4.46962367068906d)] // Rotterdam.
		[InlineData(80300d, 453738d, 52.06688833415241d, 4.297768334323142d)] // The Hague.
		[InlineData(121605d, 487759d, 52.37668890821108d, 4.896782660725934d)] // Amsterdam.
		[InlineData(182687d, 579310d, 53.19971993186204d, 5.801522396941571d)] // Leeuwarden.
		[InlineData(233627d, 581737d, 53.21647004196559d, 6.564292486864583d)] // Groningen.
		[InlineData(256494d, 534047d, 52.78420195511749d, 6.891631130023316d)] // Emmen.
		[InlineData(257781d, 471341d, 52.22056106147205d, 6.891387579659575d)] // Enschede.
		[InlineData(185826d, 424987d, 51.81265295534943d, 5.834226504298018d)] // Nijmegen.
		[InlineData(154675d, 463049d, 52.15561201090535d, 5.382458244825791d)] // Amersfoort.
		[InlineData(227846d, 618116d, 53.54411077235744d, 6.486070323938958d)] // Rottumerplaat.
		public void RdsToGpsDouble(double rdX, double rdY, double expectedLatitude, double expectedLongitude)
		{
			// Act.
			(double X, double Y) = ConverterUtils.RdToGps(rdX, rdY);

			// Assert.
			Assert.Equal(expectedLatitude, X, Tolerance);
			Assert.Equal(expectedLongitude, Y, Tolerance);
		}

		/// <summary>
		/// <see cref="ConverterUtils.RdToGps(decimal,decimal)"/> tests.
		/// Note that the unrounded expected values will be rounded to 4 decimals prior to asserting.
		/// </summary>
		[Theory]
		[SpeedVeryFast, UnitTest]
		[InlineData(176548d, 318068d, 50.85204602321809d, 5.693181181444698d)] // Maastricht.
		[InlineData(161664d, 383111d, 51.4370644101525d, 5.483039577727214d)] // Eindhoven.
		[InlineData(133561d, 397126d, 51.56267096731468d, 5.078016769674488d)] // Tilburg.
		[InlineData(112691d, 400437d, 51.591244471919005d, 4.776662178460902d)] // Breda.
		[InlineData(32143d, 391346d, 51.497724935924936d, 3.617838516788694d)] // Middelburg.
		[InlineData(91873d, 436967d, 51.91760749459275d, 4.46962367068906d)] // Rotterdam.
		[InlineData(80300d, 453738d, 52.06688833415241d, 4.297768334323142d)] // The Hague.
		[InlineData(121605d, 487759d, 52.37668890821108d, 4.896782660725934d)] // Amsterdam.
		[InlineData(182687d, 579310d, 53.19971993186204d, 5.801522396941571d)] // Leeuwarden.
		[InlineData(233627d, 581737d, 53.21647004196559d, 6.564292486864583d)] // Groningen.
		[InlineData(256494d, 534047d, 52.78420195511749d, 6.891631130023316d)] // Emmen.
		[InlineData(257781d, 471341d, 52.22056106147205d, 6.891387579659575d)] // Enschede.
		[InlineData(185826d, 424987d, 51.81265295534943d, 5.834226504298018d)] // Nijmegen.
		[InlineData(154675d, 463049d, 52.15561201090535d, 5.382458244825791d)] // Amersfoort.
		[InlineData(227846d, 618116d, 53.54411077235744d, 6.486070323938958d)] // Rottumerplaat.
		public void RdsToGpsDecimal(decimal rdX, decimal rdY, decimal unroundedExpectedLatitude, decimal unroundedExpectedLongitude)
		{
			// Act.
			(decimal X, decimal Y) = ConverterUtils.RdToGps(rdX, rdY);

			// Assert.
			Assert.Equal(Math.Round(unroundedExpectedLatitude, 4), X);
			Assert.Equal(Math.Round(unroundedExpectedLongitude, 4), Y);
		}
	}
}