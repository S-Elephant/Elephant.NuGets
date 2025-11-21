[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Extensions)](https://www.nuget.org/packages/Elephant.Extensions/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Extensions.svg)](https://www.nuget.org/packages/Elephant.Extensions/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Extensions/LICENSE.txt)

# About

Generic and shared extensions library.

## Between extensions

```c#
IsBetween<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenII<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenEI<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenIE<T>(this T value, T min, T max) where T : IComparable<T>
IsBetweenEE<T>(this T value, T min, T max) where T : IComparable<T>
```

## Enumerable extensions
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

```c#
AddIfNotExists<T>(this List<T> list, T itemToAdd)
AddOrRemoveIfExists<TSource>(this IList<TSource> list, TSource item)
AddOrRemoveIfExistsNullable<TSource>(this IList<TSource>? list, TSource item)
```

## Recycle extensions

```c#
Recycle(this int value, int max, int min = 0)
RecycleOne(this int value, int max)
Recycle(this int? value, int max, int min = 0)
RecycleOne(this int? value, int max)
```

## Stream extensions
```c#
byte[] ToByteArray(this Stream? stream)
```