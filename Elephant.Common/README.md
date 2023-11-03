[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Common)](https://www.nuget.org/packages/Elephant.Common/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Common.svg)](https://www.nuget.org/packages/Elephant.Common/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Common/LICENSE.txt)

# About

Generic and shared common code library.

## ConvertFrom

```c#
ConvertFrom.NewLineStringToStringList(string? newLineValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
ConvertFrom.SemiColonStringToStringList(string? semiColonValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
ConvertFrom.StringListToNewLineString(IList<string>? list)
ConvertFrom.StringListToSemiColonString(IList<string>? list)
```

## String Operations

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
```c#
ProcessStarter.StartProcess(string? fullPath, string? arguments = null, string? workingDirectory = null, bool useShellExecute = false);
```

## Pagination
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
```c#
StartOnOsStart.IsRegisteredInStartup(string applicationName)
StartOnOsStart.RegisterInStartup(string applicationName, string fullPathToExecutable)
StartOnOsStart.DeregisterFromStartup(string applicationName)
```


## Various

Base entities.
