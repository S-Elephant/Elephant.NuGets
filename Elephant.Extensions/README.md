[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Extensions)](https://www.nuget.org/packages/Elephant.Extensions/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Extensions.svg)](https://www.nuget.org/packages/Elephant.Extensions/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Extensions/LICENSE.txt)

# About

Generic and shared extensions library.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Extensions`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Extensions
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Extensions" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Extensions
```

# How to Use

## Between extensions

### Example

```c#
// Example: check if 5 is between 1 and 10 (interior value — all variants return true).
int value = 5;
int min = 1;
int max = 10;

bool between = value.IsBetween(min, max);    // true.
bool betweenII = value.IsBetweenII(min, max); // true  (inclusive both).
bool betweenEI = value.IsBetweenEI(min, max); // true  (exclusive lower, inclusive upper).
bool betweenIE = value.IsBetweenIE(min, max); // true  (inclusive lower, exclusive upper).
bool betweenEE = value.IsBetweenEE(min, max); // true  (exclusive both).
```

### Extensions

```c#
IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenII<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenEI<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenIE<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenEE<T>(this T value, T min, T max) where T : IComparable<T>
```



## Enumerable extensions

### Example

```c#
// AreAllItemsUnique: returns false when duplicates exist, true otherwise.
List<int> numbersWithDup = new List<int> { 1, 2, 2, 3 };
bool unique1 = numbersWithDup.AreAllItemsUnique(); // returns false.

List<int> uniqueNumbers = new List<int> { 1, 2, 3, 4 };
bool unique2 = uniqueNumbers.AreAllItemsUnique(); // returns true.

List<int> empty = [];
bool isEmpty = empty.IsEmpty(); // returns true.

// IsFirst / IsLast: check whether an item is the first or last in the sequence.
List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
bool isFirst = names.IsFirst("Alice"); // returns true.
bool isLast = names.IsLast("Charlie"); // returns true.
bool isFirstFalse = names.IsFirst("Bob"); // returns false.
```

### Extensions

```c#
AreAllItemsUnique<TSource>(this IEnumerable<TSource>? source)
ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
ContainsNone<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values)
IsEmpty<TSource>(this IEnumerable<TSource> source)
IsFirst<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
IsLast<TSource>(this IEnumerable<TSource> source, TSource itemToCompare)
None<TSource>(this IEnumerable<TSource> source)
None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
IEnumerable<List<T>> SplitIntoChunks<T>(this IEnumerable<T> source, int maxChunkSize)
```



## List extensions

### Example

```c#
// AddIfNotExists<T>(this List<T> list, T itemToAdd).
List<string> tags = new List<string> { "red", "blue" };
tags.AddIfNotExists("green"); // tags now contains: ["red", "blue", "green"].
tags.AddIfNotExists("red");   // tags unchanged: ["red", "blue", "green"].

// AddOrRemoveIfExists<TSource>(this IList<TSource> list, TSource item).
IList<int> favorites = new List<int> { 1, 2, 3 };
favorites.AddOrRemoveIfExists(2); // 2 existed, so it is removed. favorites: [1, 3].
favorites.AddOrRemoveIfExists(4); // 4 did not exist, so it is added. favorites: [1, 3, 4].
```

### Extensions

```c#
AddIfNotExists<T>(this List<T> list, T itemToAdd)
AddOrRemoveIfExists<TSource>(this IList<TSource> list, TSource item)
AddOrRemoveIfExistsNullable<TSource>(this IList<TSource>? list, TSource item)
HasAny<TSource>(this IList<TSource>? list)
```



## Recycle extensions

### Example

```c#
// Recycle(int value, int max, int min = 0)
// Example: value overflows above max and wraps into range [min..max].
int value1 = 26;
int recycled1 = value1.Recycle(10, 5); // recycled1: 6

// RecycleOne(int value, int max)
// Example: keeps value in range [1..max], wrapping overflow.
int value2 = 12;
int recycledOne = value2.RecycleOne(5); // recycledOne: 4

// Recycle(int? value, int max, int min = 0)
// Example: nullable value preserved or recycled; null returns null.
int? value3 = 26;
int? recycledNullable = value3.Recycle(10, 5); // recycledNullable: 6

// RecycleOne(int? value, int max)
// Example: nullable null is preserved.
int? value4 = null;
int? recycledOneNullable = value4.RecycleOne(5); // recycledOneNullable: null
```

### Extensions

```c#
Recycle(this int value, int max, int min = 0)
RecycleOne(this int value, int max)
Recycle(this int? value, int max, int min = 0)
RecycleOne(this int? value, int max)
```



## Stream extensions

### Example

```c#
// Read a file into a byte[].
System.IO.FileStream fileStream = System.IO.File.OpenRead(@"C:\temp\example.pdf");
int bufferSize = 81920; // 80 KB recommended default for stream copy.
byte[] fileBytes = fileStream.ToByteArray(bufferSize); // fileBytes contains the raw bytes of example.pdf.
fileStream.Close();

// Convert an in-memory stream to a byte[].
System.Text.Encoding encoding = System.Text.Encoding.UTF8;
System.IO.MemoryStream mem = new System.IO.MemoryStream(encoding.GetBytes("hello world"));
byte[] memBytes = mem.ToByteArray(4096); // memBytes contains UTF8 bytes for "hello world".
mem.Dispose();

// Handle a null stream safely.
System.IO.Stream? maybeNull = null;
byte[] empty = maybeNull.ToByteArray(4096); // Empty is an empty byte[] (no exception).
```

### Extensions

```c#
byte[] ToByteArray(this Stream? stream)
```

# Upgrade instructions

## 1.x.x &rarr; 2.0.0

- Renamed `Enumerable` static class (that contains the Enumerable extensions) into `EnumerableExtensions` to prevent Linq conflicts.
- `ContainsAll()` now returns **false** if the source is null instead of throwing..

## 2.x.x &rarr; 3.0.0

- `ToIFormFile()` got a new out parameter: `out MemoryStream stream`.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.