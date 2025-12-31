[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Constants.Sqlite)](https://www.nuget.org/packages/Elephant.Constants.Sqlite/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Constants.Sqlite.svg)](https://www.nuget.org/packages/Elephant.Constants.Sqlite/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Constants.Sqlite/LICENSE.txt)

# About

A .NET library that provides type-safe constants for SQLite database column types. This package helps you define consistent and correct SQLite column types in your code-first database implementations, avoiding typos and ensuring compatibility with SQLite's type system.

SQLite uses a dynamic type system with only four primitive data types: `INTEGER`, `REAL`, `TEXT`, and `BLOB`. This library provides convenient constants that map .NET types to their appropriate SQLite equivalents.

## Why Use This NuGet?

1. **Type Safety**: Avoid typos and inconsistencies when defining SQLite column types.
2. **IntelliSense Support**: Get autocomplete suggestions for available types.
3. **Maintainability**: Centralized type definitions make refactoring easier.
4. **Documentation**: Self-documenting code through semantic constant names.
5. **SQLite Compatibility**: Based on [official SQLite type documentation](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/types).

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Constants.Sqlite`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Constants.Sqlite
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Constants.Sqlite" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Constants.Sqlite
```

# How to Use

Use the `DbType` class to access SQLite type constants when defining your database schemas, migrations, or code-first models.

## Available Constants

### Primitive Types
- `DbType.Blob` - Binary large object data
- `DbType.Int` - Integer values
- `DbType.Real` - Floating-point values
- `DbType.TextMax` - Text data (no size limit)

### .NET Type Mappings

| Constant              | .NET Type  | SQLite Type | Description                     |
| --------------------- | ---------- | ----------- | ------------------------------- |
| `DbType.Bool`         | `bool`     | `INTEGER`   | Boolean values (0 or 1)         |
| `DbType.Byte`         | `byte`     | `INTEGER`   | Byte values                     |
| `DbType.DateTime`     | `DateTime` | `TEXT`      | Date and time values            |
| `DbType.Decimal`      | `decimal`  | `REAL`      | Decimal numbers                 |
| `DbType.Double`       | `double`   | `REAL`      | Double-precision floating-point |
| `DbType.Float`        | `float`    | `REAL`      | Single-precision floating-point |
| `DbType.EnumAsInt`    | `enum`     | `INTEGER`   | Enumerations stored as integers |
| `DbType.EnumAsString` | `enum`     | `TEXT`      | Enumerations stored as strings  |
| `DbType.Guid`         | `Guid`     | `TEXT`      | Globally unique identifiers     |

### Specialized Text Types
- `DbType.Email` - Email addresses (TEXT)
- `DbType.Name` - Names (TEXT)
- `DbType.Password` - Passwords (TEXT)
- `DbType.Url` - URLs (TEXT)

# Upgrade Instructions

## 1.x.x &rarr; 2.0.0

- Replace `DbType.Primitives.Blob` with `DbType.Primitives.BlobPrimitive`
- Replace `DbType.Primitives.Real` with `DbType.Primitives.RealPrimitive`

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../LICENSE.txt) file for details.