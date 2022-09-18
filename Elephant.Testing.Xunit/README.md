# About
Contains 5 speed attributes for categorizing your tests by performance: SpeedVerySlow, SpeedSlow, SpeedNormal, SpeedFast, SpeedVeryFast.

# Example usage
```c#
[Theory]
[SpeedVeryFast]
[InlineData(1, -1, -1)]
public void FooTest(int expected, int customParameter1, int customParameter2) { .. }
```

