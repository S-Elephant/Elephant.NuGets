[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Constants.SqlServer)](https://www.nuget.org/packages/Elephant.Constants.SqlServer/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Constants.SqlServer.svg)](https://www.nuget.org/packages/Elephant.Constants.SqlServer/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Constants.SqlServer/LICENSE.txt)

# About

Contains constant Microsoft SQL Server database types plus a few extras.

Has both an identical static version and a non-static with an interface.

## DbTypes

The following tables document the available database type constants provided by this package.

### Numeric

| Constant | Description |
|---|---|
| `BigInt` | 64-bit integer type. |
| `Bool` | Boolean stored as integer. |
| `Decimal` | Fixed-precision numeric. |
| `Double` | Double-precision floating-point (`FLOAT(53)`). |
| `Float` | Floating-point numeric. |
| `Float4` | 4-byte floating-point numeric. |
| `Int` | 32-bit integer type. |
| `Money` | Monetary type. |
| `SmallInt` | 16-bit integer. |
| `SmallMoney` | Small monetary type. |
| `TinyInt` | 8-bit integer. |
| `Real` | Single-precision floating-point. |

### Date and datetime

| Constant | Description |
|---|---|
| `Date` | Date only. |
| `DateTime` | Date and time. |
| `DateTime2` | Higher precision date and time. |
| `SmallDateTime` | Lower precision date and time. |
| `DateTimeOffset` | Date and time with offset. |
| `Time` | Time only. |
| `Timestamp` | Row version or timestamp. |

### Guid

| Constant | Description |
|---|---|
| `Guid` | Guid stored natively or as binary. |
| `GuidString` | Guid stored as string. |
| `GuidHex` | Guid stored as hexadecimal string. |

### Spatial

| Constant | Description |
|---|---|
| `Geography` | Geography spatial type. |
| `Geometry` | Geometry spatial type. |

### String

| Constant | Description |
|---|---|
| `NVarCharMax` | Unicode variable-length string with max size. |
| `Text` | Text or large string. |
| `VarCharMax` | Non-Unicode variable-length string with max size. |

### Language

| Constant | Description |
|---|---|
| `Iso639Dash1` | Two-letter ISO 639 language code. |
| `Iso639Dash2` | Three-letter ISO 639 language code. |

### File and folder

| Constant | Description |
|---|---|
| `Filename` | File name. |
| `FolderPath` | Folder path for Windows. |
| `FolderPathLinux` | Folder path for Linux. |

### Other

| Constant | Description |
|---|---|
| `Email` | Email address. |
| `Enum` | Enumeration stored as an integer or string. |
| `Name` | Name fields. |
| `Password` | Password fields. |
| `Url` | URLs. |

## DbLengths

Documented length constants used for column length definitions or validation rules.

| Constant | Description |
|---|---|
| `DecimalPrecision` | Default decimal precision. |
| `DecimalScale` | Default decimal scale. |
| `Float` | Default float length. |
| `Float4` | Default float4 length. |
| `Iso639Dash1` | Length for ISO 639-1 codes. |
| `Iso639Dash2` | Length for ISO 639-2 codes. |
| `Filename` | Recommended maximum filename length. |
| `FolderPath` | Recommended maximum folder path length. |
| `FolderPathLinux` | Recommended maximum Linux folder path length. |
| `Guid` | Length when stored as string. |
| `GuidHex` | Hexadecimal length for GUID. |
| `Email` | Recommended maximum email length. |
| `Name` | Recommended maximum name length. |
| `Password` | Recommended maximum password length. |
| `PhoneNumberInternational` | International phone number recommended length. |
| `PhoneNumberNetherlands` | Dutch phone number recommended length. |
| `Url` | Recommended maximum URL length. |
| `Xml` | Recommended maximum XML string length. |

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Constants.SqlServer`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Constants.SqlServer
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Constants.SqlServer" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Constants.SqlServer
```

# How to Use

## Basic create table example.

~~~csharp
public void Configure(EntityTypeBuilder<Video> builder)
{
	// <snip>

	builder.Property(p => p.Filename)
		.HasColumnType(DbTypes.Filename)
		.IsRequired();

	builder.Property(p => p.FileSize)
		.HasColumnType(DbTypes.BigInt)
		.IsRequired(false);

	builder.Property(p => p.Rating)
		.HasColumnType(DbTypes.TinyInt)
		.IsRequired(false);
    
	builder.Property(p => p.Url)
		.HasColumnType(DbTypes.Url)
		.IsRequired(false);
    
    // Etc.
}
~~~

# Upgrade instructions

## 2.x.x &rarr; 3.0.0

- Replace your **DbTypes.Guid** with **DbTypes.GuidString**.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../LICENSE.txt) file for details.