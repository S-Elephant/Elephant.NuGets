[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Testing.Xunit)](https://www.nuget.org/packages/Elephant.Testing.Xunit/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Testing.Xunit.svg)](https://www.nuget.org/packages/Elephant.Testing.Xunit/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Testing.Xunit/LICENSE.txt)

# About

Contains 5 speed attributes for categorizing your tests by expected execution time: SpeedVerySlow, SpeedSlow, SpeedNormal, SpeedFast, SpeedVeryFast.

These attributes enable teams to annotate and filter tests for development and CI workflows, allowing fast checks to run frequently, separating long-running integration/performance tests from quick unit tests, and prioritizing tests for reporting and orchestration. Use them to simplify test selection, improve pipeline performance, and enforce time-based testing policies.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Testing.Xunit`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Testing.Xunit
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Testing.Xunit" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Testing.Xunit
```

# How to Use

## Example
```c#
[Theory]
[SpeedVeryFast]
[InlineData(1, -1, -1)]
public void FooTest(int expected, int customParameter1, int customParameter2) { .. }
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../../LICENSE.txt) file for details.