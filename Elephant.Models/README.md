[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Models)](https://www.nuget.org/packages/Elephant.Models/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Models.svg)](https://www.nuget.org/packages/Elephant.Models/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Models/LICENSE.txt)

# About

Contains common/generic models.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Models`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Models
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Models" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Models
```

# How to Use

## Request models

### PaginationRequestModel

Use it for pagination. Can be combined with the Elephant.Types NuGet for returning pagination within a response. Example:

```c#
public class PaginationRequest : IPaginationRequest
{
	[Range(0, int.MaxValue)]
	public int Offset { get; set; } = 0;

	[Range(0, int.MaxValue)]
	public int Limit { get; set; } = int.MaxValue;
	..
}
```



## Response models

### PaginationResponseWrapper and PaginationResponseModel

```c#
PaginationResponseModel : IPaginationResponseModel
PaginationResponseWrapper<TData> : PaginationResponseModel, IPaginationResponseWrapper<TData> where TData : new()
```

#### Properties

- TData? Data *(wrapper only)*
- bool IsFirstPage
- bool IsLastPage
- int Offset
- int Limit
- string? PageUriFirst
- string? PageUriLast
- string? PageUriNext
- string? PageUriPrevious
- int TotalItems
- int TotalPageCount

# Upgrade instructions

## 1.x.x &rarr; 2.0.0

- Add/Replace your **Elephant.Models.RequestModels** with **Elephant.Models.ResponseModels** if you used the PaginationResponseWrapper<TData>.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.