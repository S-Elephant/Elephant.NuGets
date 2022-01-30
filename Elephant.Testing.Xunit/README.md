# About
Contains 5 speed attributes for categorizing your tests by performance.

# Example usage
```c#
[Theory]
[SpeedVeryFast]
[InlineData(1, -1, -1)]
public void FooTest(int expected, int customParameter1, int customParameter2) { .. }
```

