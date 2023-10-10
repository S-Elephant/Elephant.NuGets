# Deprecated

This NuGet will soon be deprecated and be replaced by Elephant.GeoSystems when its done.

# About

Provides various [Rijksdriehoek coordinate](https://nl.wikipedia.org/wiki/Rijksdriehoeksco%C3%B6rdinaten) calculations and helpers.

# Example usage

```c#
using Elephant.Rijksdriehoek;

internal class ExampleClass
{
    private static void Example()
    {
        bool isValid = MathRd.IsValidRdCoordinate(10.434f, 350000.123f);
        var gpsCoordinates = MathRd.ConvertToLatitudeLongitude(10.434f, 350000.123f);
        var rdCoordinates = MathRd.ConvertToRijksdriehoek(52.372143838117f, 4.90559760435224f);
        // And more.
    }
}
```

Note that many methods have double overloads and some methods also have decimal overloads.