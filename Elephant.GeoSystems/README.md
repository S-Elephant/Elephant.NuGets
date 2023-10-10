# About

Provides various conversions, validations and data-containers for using and converting between Rd, Open Street Map, GPS and custom positioning systems.

## WARNING: This is [WIP] Work In Progress. Please be careful when using this NuGet in production environments. While in WIP, this NuGet will not use Semantic Versioning.

# Abreviations


| Abreviation | Meaning |
| -------- | ------- |
| RD | [Rijksdriehoek](https://nl.wikipedia.org/wiki/Rijksdriehoeksco%C3%B6rdinaten) |
| OSM | [Open Street Maps](https://wiki.openstreetmap.org/wiki/Main_Page) |
| GPS | https://en.wikipedia.org/wiki/Global_Positioning_System |
|Utils|Utilities|



# Supported positioning systems

- Custom.
- GPS (WGS84).
- Open Street Map tile API coordinates.
- RD (=Rijksdriehoek).

# Constants

```c#
// Earth.
double EarthRadius = 6378137d;
decimal EarthRadiusAsDecimal = 6378137m;
HalfEarthCircumference = Math.PI * EarthRadius;
PiAsDecimal = 3.14159265358979323846264338327950288419716939937510m;
HalfEarthCircumferenceAsDecimal = PiAsDecimal * EarthRadiusAsDecimal;

// RD.
double AmersfoortWgs84CoordinateX = 52.15517440d;
double AmersfoortWgs84CoordinateY = 5.38720621d;
decimal AmersfoortWgs84CoordinateXAsDecimal = 52.15517440m;
decimal AmersfoortWgs84CoordinateYAsDecimal = 5.38720621m;
int AmersfoortRdCoordinateX = 155000;
int AmersfoortRdCoordinateY = 463000;
```

# # Conversions

```c#
ConverterUtils.GpsToOsmTile(int zoom, float latitude, float longitude);
ConverterUtils.GpsToOsmTile(int zoom, double latitude, double longitude);
ConverterUtils.GpsToOsmTile(int zoom, decimal latitude, decimal longitude);
ConverterUtils.GpsToRd(float latitudeWgs84, float longitudeWgs84);
ConverterUtils.GpsToRd(decimal latitudeWgs84, decimal longitudeWgs84);
ConverterUtils.OsmTileToGpsAsFloat(int zoom, int tileX, int tileY);
ConverterUtils.OsmTileToGps(int zoom, int tileX, int tileY);
ConverterUtils.OsmTileToGpsAsDecimal(int zoom, int tileX, int tileY);
ConverterUtils.RdToGps(float rdX, float rdY);
ConverterUtils.RdToGps(double rdX, double rdY);
ConverterUtils.RdToGps(decimal rdX, decimal rdY);
```

# Validators

```c#
GpsValidator.IsValid(float latitude, float longitude) // Plus another 5 overloads.
RdValidator.IsValid(float rdX, float rdY) // Plus another 5 overloads.
```

# Convert Polygon into/from a WKT (=Well Known Text) string snippet

I removed my code and I suggest that you use the NetTopologySuite NuGet instead found [here](https://github.com/NetTopologySuite/NetTopologySuite). Conversion examples:

```c#
/// <summary>
/// Convert a Well-Known Text (WKT) representation of a polygon to a NetTopologySuite Polygon.
/// </summary>
/// <param name="wkt">The Well-Known Text (WKT) representation of the polygon.</param>
/// <returns>The NetTopologySuite Polygon.</returns>
/// <exception cref="ArgumentException">Thrown if the provided WKT does not represent a polygon.</exception>
public static ToPolygon ConvertWktToPolygon(string wkt)
{
	if (string.IsNullOrEmpty(wkt))
		return Polygon.Empty;

	WKTReader reader = new();
	Geometry geometry = reader.Read(wkt);
	if (geometry is Polygon polygon)
		return polygon;
	else
		throw new ArgumentException($"WKT does not represent a polygon. Got: {wkt}");
}
```

Convert a Polygon to WKT text:

```c#
string wkt = myPolgyon.ToText();
```
