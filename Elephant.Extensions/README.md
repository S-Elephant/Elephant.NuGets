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
IsEmpty<TSource>(this IEnumerable<TSource> source)
None<TSource>(this IEnumerable<TSource> source)
None<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
```

## Recycle extensions
```c#
Recycle(this int value, int max, int min = 0)
RecycleOne(this int value, int max)
Recycle(this int? value, int max, int min = 0)
RecycleOne(this int? value, int max)
```