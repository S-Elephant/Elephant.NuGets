[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.CodeFirst.SqlServer)](https://www.nuget.org/packages/Elephant.CodeFirst.SqlServer/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.CodeFirst.SqlServer.svg)](https://www.nuget.org/packages/Elephant.CodeFirst.SqlServer/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.CodeFirst.SqlServer/LICENSE.txt)

# About

Provides EF Core Code First helpers for Microsoft SQL Server to simplify `EntityTypeBuilder` configuration and enforce consistent schema conventions.
The `ConfigurationHelper` exposes methods to create tables with integer or GUID primary keys, add standardized Name, Description, and IsEnabled columns, and combine common patterns (for example, Id + Name). Use these helpers in `IEntityTypeConfiguration<T>.Configure` or `OnModelCreating` to reduce boilerplate and ensure consistent column types, constraints, and defaults across your model.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.CodeFirst.SqlServer`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.CodeFirst.SqlServer
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.CodeFirst.SqlServer" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.CodeFirst.SqlServer
```

# How to Use

Use ConfigurationHelper from your entity configuration (for example in `IEntityTypeConfiguration<T>.Configure or OnModelCreating`) to apply common table/column conventions with a single call.

Quick example: int Id primary key, name, description and is-enabled columns:

```c#
public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        // Create table and int auto-incrementing Id primary key.
        ConfigurationHelper.ToIdTableWithPrimaryKey<Todo>(ref builder, schema: "dbo", tableName: "Todos");

        // Add required Name and Description columns.
        ConfigurationHelper.AddName(ref builder);
        ConfigurationHelper.AddDescription(ref builder);

        // Add IsEnabled column with default true.
        ConfigurationHelper.AddIsEnabled(ref builder, defaultValue: true);
    }
}
```

Quick example: GUID primary key variant:
```c#
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Create table with GUID primary key.
        ConfigurationHelper.ToGuidTableWithPrimaryKey<Order>(ref builder, schema: "sales", tableName: "Orders");

        // Add name if applicable.
        ConfigurationHelper.AddName(ref builder);
    }
}
```

## ConfigurationHelper

```c#
string ToIdTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IId

void ToIdNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IId, IName

void ToGuidTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IGuid

void ToGuidNameTableWithPrimaryKey<TEntity>(ref EntityTypeBuilder<TEntity> builder, string? schema = null, string? tableName = null) where TEntity : class, IGuid, IName

void AddName<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdName

void AddDescription<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdNameDescription

void AddNameAndDescription<T>(ref EntityTypeBuilder<T> builder) where T : class, IIdNameDescription

void AddIsEnabled<T>(ref EntityTypeBuilder<T> builder, bool defaultValue = true) where T : class, IIsEnabled
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.