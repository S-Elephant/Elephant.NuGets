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