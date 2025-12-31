[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.CodeFirst.Sqlite)](https://www.nuget.org/packages/Elephant.CodeFirst.Sqlite/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.CodeFirst.Sqlite.svg)](https://www.nuget.org/packages/Elephant.CodeFirst.Sqlite/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.CodeFirst.Sqlite/LICENSE.txt)

# About

Provides `ConfigurationHelper` methods to simplify common column and table setup:

-	`ToIdTableWithPrimaryKey<T>(ref EntityTypeBuilder<T>, string? tableName = null)` creates a table and an auto-incrementing int Id primary key.
-	`AddName<T>` adds a required Name column using `DbType.Name`.
-	`AddDescription<T>` adds a required Description column using `DbType.TextMax`.
-	`AddNameAndDescription<T>` adds both Name and Description.
-	`AddIsEnabled<T>(ref EntityTypeBuilder<T>, bool defaultValue = true)` adds an `IsEnabled` column using `DbType.Bool` with a default value.

> NOTE: SQLite doesn't support schemas.


# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.CodeFirst.Sqlite`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.CodeFirst.Sqlite
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.CodeFirst.Sqlite" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.CodeFirst.Sqlite
```

# How to Use

## Example usage

```c#
builder.Property(p => p.TodoStatus)
	.HasColumnType(DbType.Enum)
	.HasConversion<int>()
	.IsRequired();
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.