[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Rijksdriehoek)](https://www.nuget.org/packages/Elephant.Rijksdriehoek/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Rijksdriehoek.svg)](https://www.nuget.org/packages/Elephant.Rijksdriehoek/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Rijksdriehoek/LICENSE.txt)

# Deprecated

This NuGet will soon be deprecated and be replaced by Elephant.GeoSystems when its done.

# About

Provides various [Rijksdriehoek coordinate](https://nl.wikipedia.org/wiki/Rijksdriehoeksco%C3%B6rdinaten) calculations and helpers.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Rijksdriehoek`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Rijksdriehoek
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Rijksdriehoek" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Rijksdriehoek
```

# How to Use

## Example

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

> NOTE: Many methods have double overloads and some methods also have decimal overloads.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.