using System;
using System.Collections.Generic;
using System.Globalization;

namespace Elephant.Rijksdriehoek
{
    public static class MathRd
    {
        #region Min and max valid coordinate values

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

        #endregion

        /// <summary>
        /// Calculates the distance between 2 points.
        /// </summary>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        #region IsValidRdCoordinate helpers

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten.
        /// </summary>
        public static bool IsValidRdCoordinate(decimal x, decimal y)
        {
            return x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten  and if both aren't null.
        /// </summary>
        public static bool IsValidRdCoordinate(decimal? x, decimal? y)
        {
            return x != null && y != null &&
                x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten.
        /// </summary>
        public static bool IsValidRdCoordinate(double x, double y)
        {
            return x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten  and if both aren't null.
        /// </summary>
        public static bool IsValidRdCoordinate(double? x, double? y)
        {
            return x != null && y != null &&
                x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten.
        /// </summary>
        public static bool IsValidRdCoordinate(float x, float y)
        {
            return x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        /// <summary>
        /// Returns true if the supplied coordinates are considered valid Rijksdriehoekscoördinaten and if both aren't null.
        /// </summary>
        public static bool IsValidRdCoordinate(float? x, float? y)
        {
            return x != null && y != null &&
                x >= RdMinX && x <= RdMaxX &&
                y >= RdMinY && y <= RdMaxY;
        }

        #endregion

        /// <summary>
        /// Parses a string like "POINT(10500.123 350000.456)" into 10500.123f and 350000.456f.
        /// </summary>
        /// <param name="pointString">The string. Example (w/o quotes): "POINT(1001.100 550000.789)"</param>
        /// <param name="x">The parsed x value or <see cref="float.MinValue"/> if something went wrong.</param>
        /// <param name="y">The parsed y value or <see cref="float.MinValue"/> if something went wrong.</param>
        /// <returns>true if success.</returns>
        /// <remarks><para>The optional decimal separator must be a dot.</para>
        /// <para>The x and y values must be separated by either a single blank space or a comma.</para>
        /// <para>Negative values will result in a 0f value. Rd coordinates can never be smaller than 0.</para></remarks>
        public static bool TryParseFromPointString(string pointString, out float x, out float y)
        {
            try
            {
                // Remove the prefix and suffix.
                string contents = pointString.Split(new string[] { "POINT(", ")" }, StringSplitOptions.None)[1];

                // Split the coordinates.
                string[] split = contents.Split(' ', ',');

                // TryParse() the string values (that are dot separated) into floats.
                float.TryParse(split[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out x);
                float.TryParse(split[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out y);

                // Report success.
                return true;
            }
            catch
            {
                x = y = float.MinValue;
                return false;
            }
        }

        /// <summary>
        /// Used by <see cref="ConvertStringToPolygonRd(string)"/> for splitting coordinates.
        /// </summary>
        private static readonly string[] CoordinateSeparators = new string[] { ", " };

        /// <summary>
        /// Converts a RD polygon string into a list of floats (rd-x and rd-y).
        /// </summary>
        /// <param name="polygonString">Example value: POLYGON ((12.194 500500.123, 121.888 488444.423, 2000.101 450400.400))"</param>
        public static List<(float x, float y)> ConvertStringToPolygonRd(string polygonString)
        {
            if (string.IsNullOrWhiteSpace(polygonString))
                throw new ArgumentNullException(nameof(polygonString));

            int polygonStringLength = polygonString.Length;
            if (polygonStringLength < 15)
                throw new ArgumentException($"{nameof(polygonString)} is too short.");

            // Remove suffix and prefix.
            string data = polygonString.Remove(polygonStringLength - 2).Remove(0, 10);

            // Split into coordinate pairs.
            string[] coordinatePairs = data.Split(CoordinateSeparators, StringSplitOptions.None);

            var result = new List<(float, float)>();
            foreach (string coordinatePair in coordinatePairs)
            {
                string[] splitCoordinatePair = coordinatePair.Split(' ');
                float.TryParse(splitCoordinatePair[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float rdX);
                float.TryParse(splitCoordinatePair[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out float rdY);
                result.Add((rdX, rdY));
            }

            return result;
        }

        /// <summary>
        /// RD (=Rijksdriehoek) x coordinate of the City Amersfoort (in The Netherlands).
        /// </summary>
        private const int AmersfoortRdCoordinateX = 155000;

        /// <summary>
        /// RD (=Rijksdriehoek) y coordinate of the City Amersfoort (in The Netherlands).
        /// </summary>
        private const int AmersfoortRdCoordinateY = 463000;

        /// <summary>
        /// WSG84 GPS x coordinate of the City Amersfoort (in The Netherlands).
        /// </summary>
        private const double AmersfoortWgs84CoordinateX = 52.15517d;

        /// <summary>
        /// WSG84 GPS y coordinate of the City Amersfoort (in The Netherlands).
        /// </summary>
        private const double AmersfoortWgs84CoordinateY = 5.387206d;

        /// <summary>
        /// Convert a WGS84 GPS coordinate into RD (=Rijksdriehoek) coordinates.
        /// </summary>
        /// <param name="latitudeWgs84">The WGS84 latitude coordinate.</param>
        /// <param name="longitudeWgs84">The WGS84 longitude coordinate.</param>
        public static (double rdX, double rdY) ConvertToRijksdriehoek(double latitudeWgs84, double longitudeWgs84)
        {
            double[,] rpq = new double[4, 5];
            rpq[0, 1] = 190094.945;
            rpq[1, 1] = -11832.228;
            rpq[2, 1] = -114.221;
            rpq[0, 3] = -32.391;
            rpq[1, 0] = -0.705;
            rpq[3, 1] = -2.340;
            rpq[0, 2] = -0.008;
            rpq[1, 3] = -0.608;
            rpq[2, 3] = 0.148;

            double[,] spq = new double[4, 5];
            spq[0, 1] = 0.433;
            spq[0, 2] = 3638.893;
            spq[0, 4] = 0.092;
            spq[1, 0] = 309056.544;
            spq[2, 0] = 73.077;
            spq[1, 2] = -157.984;
            spq[3, 0] = 59.788;
            spq[2, 2] = -6.439;
            spq[1, 1] = -0.032;
            spq[1, 4] = -0.054;

            double dLatitude = (0.36 * (latitudeWgs84 - AmersfoortWgs84CoordinateX));
            double dLongitude = (0.36 * (longitudeWgs84 - AmersfoortWgs84CoordinateY));
            double latitudeCalculation = 0;
            double longitudeCalculation = 0;

            for (int p = 0; p < 4; p++)
            {
                for (int q = 0; q < 5; q++)
                {
                    latitudeCalculation += rpq[p, q] * Math.Pow(dLatitude, p) * Math.Pow(dLongitude, q);
                    longitudeCalculation += spq[p, q] * Math.Pow(dLatitude, p) * Math.Pow(dLongitude, q);
                }
            }

            double rdX = AmersfoortRdCoordinateX + latitudeCalculation;
            double rdY = AmersfoortRdCoordinateY + longitudeCalculation;

            return (rdX, rdY);
        }

        /// <summary>
        /// Convert a WGS84 GPS coordinate into RD (=Rijksdriehoek) coordinates.
        /// </summary>
        /// <param name="latitudeWgs84">The WGS84 latitude coordinate.</param>
        /// <param name="longitudeWgs84">The WGS84 longitude coordinate.</param>
        public static (float rdX, float rdY) ConvertToRijksdriehoek(float latitudeWgs84, float longitudeWgs84)
        {
            double[,] rpq = new double[4, 5];
            rpq[0, 1] = 190094.945d;
            rpq[1, 1] = -11832.228d;
            rpq[2, 1] = -114.221d;
            rpq[0, 3] = -32.391d;
            rpq[1, 0] = -0.705d;
            rpq[3, 1] = -2.340d;
            rpq[0, 2] = -0.008d;
            rpq[1, 3] = -0.608d;
            rpq[2, 3] = 0.148d;

            double[,] spq = new double[4, 5];
            spq[0, 1] = 0.433d;
            spq[0, 2] = 3638.893d;
            spq[0, 4] = 0.092d;
            spq[1, 0] = 309056.544d;
            spq[2, 0] = 73.077d;
            spq[1, 2] = -157.984d;
            spq[3, 0] = 59.788d;
            spq[2, 2] = -6.439d;
            spq[1, 1] = -0.032d;
            spq[1, 4] = -0.054d;

            double dLatitude = (0.36d * (latitudeWgs84 - AmersfoortWgs84CoordinateX));
            double dLongitude = (0.36d * (longitudeWgs84 - AmersfoortWgs84CoordinateY));
            double latitudeCalculation = 0d;
            double longitudeCalculation = 0d;

            for (int p = 0; p < 4; p++)
            {
                for (int q = 0; q < 5; q++)
                {
                    latitudeCalculation += rpq[p, q] * Math.Pow(dLatitude, p) * Math.Pow(dLongitude, q);
                    longitudeCalculation += spq[p, q] * Math.Pow(dLatitude, p) * Math.Pow(dLongitude, q);
                }
            }

            double rdX = AmersfoortRdCoordinateX + latitudeCalculation;
            double rdY = AmersfoortRdCoordinateY + longitudeCalculation;

            return ((float)rdX, (float)rdY);
        }

        /// <summary>
        /// Convert a RD (=Rijksdriehoek) coordinates into a WGS84 GPS coordinate.
        /// </summary>
        /// <param name="x">RD x coordinate.</param>
        /// <param name="y">RD y coordinate.</param>
        public static (double latitude, double longitude) ConvertToLatitudeLongitude(double x, double y)
        {
            double deltaX = (double)(x - AmersfoortRdCoordinateX) * (double)Math.Pow(10, -5);
            double deltaY = (double)(y - AmersfoortRdCoordinateY) * (double)Math.Pow(10, -5);

            // All lines of latitude above the Equator are indicated with the letter 'N'.
            double sumN =
                3235.65389d * deltaY +
                -32.58297d * Math.Pow(deltaX, 2) +
                -0.2475d * Math.Pow(deltaY, 2) +
                -0.84978d * Math.Pow(deltaX, 2) * deltaY +
                -0.0655d * Math.Pow(deltaY, 3) +
                -0.01709d * Math.Pow(deltaX, 2) * Math.Pow(deltaY, 2) +
                -0.00738d * deltaX +
                0.0053d * Math.Pow(deltaX, 4) +
                -0.00039d * Math.Pow(deltaX, 2) * Math.Pow(deltaY, 3) +
                0.00033d * Math.Pow(deltaX, 4) * deltaY +
                -0.00012d * deltaX * deltaY;

            // All lines of longitude east of the Prime Meridian are indicated with the letter 'E' to denote east of the Prime Meridian.
            double sumE =
                5260.52916d * deltaX +
                105.94684d * deltaX * deltaY +
                2.45656d * deltaX * Math.Pow(deltaY, 2) +
                -0.81885d * Math.Pow(deltaX, 3) +
                0.05594d * deltaX * Math.Pow(deltaY, 3) +
                -0.05607d * Math.Pow(deltaX, 3) * deltaY +
                0.01199d * deltaY +
                -0.00256d * Math.Pow(deltaX, 3) * Math.Pow(deltaY, 2) +
                0.00128d * deltaX * Math.Pow(deltaY, 4) +
                0.00022d * Math.Pow(deltaY, 2) +
                -0.00022d * Math.Pow(deltaX, 2) +
                0.00026d * Math.Pow(deltaX, 5);

            double latitude = AmersfoortWgs84CoordinateX + (sumN / 3600d);
            double longitude = AmersfoortWgs84CoordinateY + (sumE / 3600d);

            return (latitude, longitude);
        }

        /// <summary>
        /// Convert a RD (=Rijksdriehoek) coordinates into a WGS84 GPS coordinate.
        /// </summary>
        /// <param name="x">RD x coordinate.</param>
        /// <param name="y">RD y coordinate.</param>
        public static (float latitude, float longitude) ConvertToLatitudeLongitude(float x, float y)
        {
            double deltaX = (x - AmersfoortRdCoordinateX) * Math.Pow(10, -5);
            double deltaY = (y - AmersfoortRdCoordinateY) * Math.Pow(10, -5);

            // All lines of latitude above the Equator are indicated with the letter 'N'.
            double sumN =
                3235.65389d * deltaY +
                -32.58297d * Math.Pow(deltaX, 2) +
                -0.2475d * Math.Pow(deltaY, 2) +
                -0.84978d * Math.Pow(deltaX, 2) * deltaY +
                -0.0655d * Math.Pow(deltaY, 3) +
                -0.01709d * Math.Pow(deltaX, 2) * Math.Pow(deltaY, 2) +
                -0.00738d * deltaX +
                0.0053d * Math.Pow(deltaX, 4) +
                -0.00039d * Math.Pow(deltaX, 2) * Math.Pow(deltaY, 3) +
                0.00033d * Math.Pow(deltaX, 4) * deltaY +
                -0.00012d * deltaX * deltaY;

            // All lines of longitude east of the Prime Meridian are indicated with the letter 'E' to denote east of the Prime Meridian.
            double sumE =
                5260.52916d * deltaX +
                105.94684d * deltaX * deltaY +
                2.45656d * deltaX * Math.Pow(deltaY, 2) +
                -0.81885d * Math.Pow(deltaX, 3) +
                0.05594d * deltaX * Math.Pow(deltaY, 3) +
                -0.05607d * Math.Pow(deltaX, 3) * deltaY +
                0.01199d * deltaY +
                -0.00256d * Math.Pow(deltaX, 3) * Math.Pow(deltaY, 2) +
                0.00128d * deltaX * Math.Pow(deltaY, 4) +
                0.00022d * Math.Pow(deltaY, 2) +
                -0.00022d * Math.Pow(deltaX, 2) +
                0.00026d * Math.Pow(deltaX, 5);

            double latitude = AmersfoortWgs84CoordinateX + (sumN / 3600d);
            double longitude = AmersfoortWgs84CoordinateY + (sumE / 3600d);

            return ((float)latitude, (float)longitude);
        }
    }
}
