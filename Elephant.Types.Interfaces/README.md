[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Types.Interfaces)](https://www.nuget.org/packages/Elephant.Types.Interfaces/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Types.Interfaces.svg)](https://www.nuget.org/packages/Elephant.Types.Interfaces/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Types.Interfaces/LICENSE.txt)

# About

Contains shared/common/generic type interfaces.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Types.Interfaces`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Types.Interfaces
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Types.Interfaces" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Types.Interfaces
```

# How to Use

## Basic type interfaces

- IId
- IIdName
- IIdNameDescription
- IIsEnabled
- IGuid
- IName

## Response wrapper interfaces

- IPagedResponseWrapper<TData>
- IResponseWrapper
- IResponseWrapper<TData>


## Pagination interfaces

- IPaginationRequest
- IPaginationResponseModel
- IPaginationResponseWrapper

# Upgrade instructions

## 2.x.x &rarr; 3.0.0

- Update your pagination URL properties into URI properties (only its name changed).

## 3.x.x &rarr; 4.0.0

- All your implementations of **IGuid** should be renamed from **Guid** &rarr; **Id**.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.