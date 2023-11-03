CodeFirst.SqlServer [![Nuget downloads](https://img.shields.io/nuget/v/Elephant.CodeFirst.SqlServer)](https://www.nuget.org/packages/Elephant.CodeFirst.SqlServer/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.CodeFirst.SqlServer.svg)](https://www.nuget.org/packages/Elephant.CodeFirst.SqlServer/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.CodeFirst.SqlServer/LICENSE.txt)

# About

Contains Code First configuration helper methods for MS SQL Server.

# ConfigurationHelper

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



