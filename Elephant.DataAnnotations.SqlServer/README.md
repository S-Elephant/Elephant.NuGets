[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.DataAnnotations.SqlServer)](https://www.nuget.org/packages/Elephant.DataAnnotations.SqlServer/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.DataAnnotations.SqlServer.svg)](https://www.nuget.org/packages/Elephant.DataAnnotations.SqlServer/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.DataAnnotations.SqlServer/LICENSE.txt)

# About

Contains validation attributes specific for MS SQL Server.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.DataAnnotations.SqlServer`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.DataAnnotations.SqlServer
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.DataAnnotations.SqlServer" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.DataAnnotations.SqlServer
```

# How to Use

## Example 1

```c#
// Optional filename with default max.
// Validation fails if the filename length exceeds the package's configured max (see Elephant.Constants.SqlServer).
public class UploadRequest
{
	[FilenameMaxLength] // Uses library default max length.
	public string? FileName { get; set; }
}

// Usage:
var req = new UploadRequest { FileName = new string('a', 1000) }; // overly long
// After model validation: ModelState is invalid (FileName too long).
```

## Example 2

```c#
// Required filename (cannot be null/empty and must respect max).
public class UploadRequiredRequest
{
	[FilenameMaxLengthRequired] // Required + max length check.
	public string FileName { get; set; } = string.Empty;
}

// Usage:
var req = new UploadRequiredRequest { FileName = "" };
// After model validation: ModelState is invalid (empty string not allowed).
```

## Example 3

```c#
// Required path with a minimum name length.
// The attribute accepts the optional minLength parameter (minLength = 3 here).
public class SavePathRequiredRequest
{
	[PathFolderMaxLengthRequired(3)] // Required + minLength (folder name segments must be >= 3).
	public string FolderPath { get; set; } = string.Empty;
}

// Usage:
var reqShort = new SavePathRequiredRequest { FolderPath = "C:\\a\\b" };
// After model validation: ModelState is invalid because one or more segments are shorter than minLength.
```


## Attribute list

Some attributes use predefined lengths from the [Elephant.Constants.SqlServer NuGet](https://www.nuget.org/packages/Elephant.Constants.SqlServer/).

- [**FilenameMaxLength**(int minLength = 0)] *(compatible with string)*
- [**FilenameMaxLengthRequired**(int minLength = 0)] *(compatible with string)*
- [**PathFolderMaxLength**(int minLength = 0)] *(compatible with string)*
- [**PathFolderMaxLengthRequired**(int minLength = 0)] *(compatible with string)*

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.