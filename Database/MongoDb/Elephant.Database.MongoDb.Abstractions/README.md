[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Database.MongoDb.Abstractions)](https://www.nuget.org/packages/Elephant.Database.MongoDb.Abstractions/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Database.MongoDb.Abstractions.svg)](https://www.nuget.org/packages/Elephant.Database.MongoDb.Abstractions/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Database.MongoDb.Abstractions/LICENSE.txt)

# About

Contains abstractions for the **Elephant.Database.MongoDb.Abstractions** NuGet.

## Abstractions

- Context
  - IEntityTypeBuilder
  - IMongoContext
  - IMongoContextOptionsBuilder
- Repositories
  - IDatabaseRepository
  - IGenericCrudRepository

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Database.MongoDb.Abstractions`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Database.MongoDb.Abstractions
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Database.MongoDb.Abstractions" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Database.MongoDb.Abstractions
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../../LICENSE.txt) file for details.