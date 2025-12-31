[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Constants.MockData)](https://www.nuget.org/packages/Elephant.Constants.MockData/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Constants.MockData.svg)](https://www.nuget.org/packages/Elephant.Constants.MockData/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Constants.MockData/LICENSE.txt)

# About

Contains static ReadOnlyCollection unique mock data in x10, x100 and x1000 for:

- Cities (x10 and x100).
- Guids (x10 and x100).
- Postal Codes (Dutch 4PP and 6PP formats).
- Streets.
- Zip Codes (United States).

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Constants.MockData`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Constants.MockData
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Constants.MockData" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Constants.MockData
```

# How to Use

XUnit example

~~~csharp
public class CitiesTests
{
    [Fact]
    public void Cities_Contains_NewYork()
    {
        // Arrange.
        ReadOnlyCollection<string> list = Cities.Us10;

        // Act.
        bool contains = list.Contains("New York");

        // Assert.
        Assert.True(contains);
    }
}
~~~

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../LICENSE.txt) file for details.