[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Common)](https://www.nuget.org/packages/Elephant.Common/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Common.svg)](https://www.nuget.org/packages/Elephant.Common/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Common/LICENSE.txt)

# About

Generic and common C# code library with various useful methods. Includes string manipulation helpers, process-start utilities, pagination helpers, and startup registration helpers, among others. Designed to be framework-agnostic and easy to integrate into .NET projects to reduce boilerplate, improve code clarity, and speed development.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Common`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Common
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Common" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Common
```

# How to Use

## ConvertFrom

### Example

```c#
string? input = "one\r\ntwo\r\nthree";
IList<string> list = ConvertFrom.NewLineStringToStringList(input, StringSplitOptions.RemoveEmptyEntries); // Now contains ["one", "two", "three"]
string output = ConvertFrom.StringListToNewLineString(list); // Now contains "one\r\ntwo\r\nthree".
```

### Available helpers

```c#
ConvertFrom.NewLineStringToStringList(string? newLineValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
ConvertFrom.SemiColonStringToStringList(string? semiColonValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
ConvertFrom.StringListToNewLineString(IList<string>? list)
ConvertFrom.StringListToSemiColonString(IList<string>? list)
```

## String Operations

Example

```c#
string name = "elephant";
string capitalized = StringOperations.CapitalizeFirstChar(name); // Now contains "Elephant".

string? nullableTitle = "The LONG dog.";
string? safeTitle = StringOperations.ToTitleCaseNullable(nullableTitle); // Now contains "The Long Dog.".

string enclosed = StringOperations.EncloseByIfNotAlready("value", '"'); // Now contains "'value'".
string joined = StringOperations.Join(',', "a", null, "b"); // Now contains: "a,b"
string joinedWithBoth = StringOperations.JoinWithLeadingAndTrailing('-', "x", "y");
```

Available helpers

```c#
StringOperations.CapitalizeFirstChar(string stringToCapitalize)
StringOperations.CapitalizeFirstCharNullable(string? stringToCapitalize)
StringOperations.EncloseByIfNotAlready(string value, char encloser)
StringOperations.Join(char separatorChar, params string?[] stringsToCombine)
StringOperations.JoinWithLeading(char separatorChar, params string?[] stringsToCombine)
StringOperations.JoinWithTrailing(char separatorChar, params string?[] stringsToCombine)
StringOperations.JoinWithLeadingAndTrailing(char separatorChar, params string?[] stringsToCombine)
StringOperations.RemoveSubstringFromString(string source, string substringToRemove)
StringOperations.RemoveSubstringsFromString(string source, IEnumerable<string> substringsToRemove)
StringOperations.SplitByNewLine(string value, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
StringOperations.ToTitleCase(string stringToTitleCase)
StringOperations.ToTitleCaseNullable(string? stringToTitleCase)
```

## Process Starter

Example

```c#
string? fullPath = @"C:\Program Files\Example\example.exe";
string? arguments = "--help";
string? workingDirectory = @"C:\Temp";
bool useShell = false;

ProcessStarter.StartProcess(fullPath, arguments, workingDirectory, useShell);
```

Available helper

```c#
ProcessStarter.StartProcess(string? fullPath, string? arguments = null, string? workingDirectory = null, bool useShellExecute = false);
```

## Pagination

Example

```c#
IQueryable<int> source = Enumerable.Range(1, 100).AsQueryable();
int offset = 10;
int limit = 20;

IQueryable<int> page = PaginationHelper.Paginate(source, offset, limit);
int totalPages = PaginationHelper.TotalPageCount(source.Count(), limit);
bool isLast = PaginationHelper.IsLastPage(source.Count(), offset, limit);
```

Available helpers

```c#
IQueryable<TSource> PaginationHelper.Paginate<TSource>(IList<TSource> source, int offset, int limit)
IQueryable<TSource> PaginationHelper.Paginate<TSource>(this IQueryable<TSource> source, int offset, int limit)
IQueryable<TSource> PaginationHelper.Paginate<TSource>(this IQueryable<TSource> source, IPaginationRequest paginationRequest)
int PaginationHelper.CurrentOffset(int sourceCount, int offset, int limit)
int PaginationHelper.LastOffset(int sourceCount, int limit)
int PaginationHelper.TotalPageCount(int sourceCount, int limit)
bool PaginationHelper.IsLastPage(int sourceCount, int offset, int limit)
```

## Various

Example

```c#
string applicationName = "MyApp";
string fullPathToExecutable = @"C:\Apps\MyApp\MyApp.exe";

// Register on startup.
StartOnOsStart.RegisterInStartup(applicationName, fullPathToExecutable);

// Check registration.
bool registered = StartOnOsStart.IsRegisteredInStartup(applicationName);

// Remove registration.
StartOnOsStart.DeregisterFromStartup(applicationName);
```

Available helpers

```c#
StartOnOsStart.IsRegisteredInStartup(string applicationName)
StartOnOsStart.RegisterInStartup(string applicationName, string fullPathToExecutable)
StartOnOsStart.DeregisterFromStartup(string applicationName)
```

## Various

Base entities.

# Upgrade instructions

## 4.x.x &rarr; 5.0.0

- `PaginationHelper.Paginate(this IQueryable<TSource> source, IPaginationRequest paginationRequest)` now returns all records instead of none if the limit property of IPaginationRequest is less or equal than zero (this is in line with the other overloads and pagination performance is increased for these cases).

## 5.x.x &rarr; 6.0.0

- Fixed edge case handling in `PaginationHelper` for integer boundary values (`int.MaxValue` and `int.MinValue`). If you implemented custom workarounds for these cases, you can now remove them. For most users, no action is required.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.