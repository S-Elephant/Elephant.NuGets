[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Common.Maths)](https://www.nuget.org/packages/Elephant.Common.Maths/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Common.Maths.svg)](https://www.nuget.org/packages/Elephant.Common.Maths/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Common.Maths/LICENSE.txt)

# About

Common mathematics library. Provides performant helpers for common numeric tasks via the `Arithmetics`, `Combinatorics` and `NumberTheory` APIs. Designed to be framework-agnostic, dependency-free, and easy to unit-test. Use it to avoid reimplementing common algorithms and to keep numeric code concise and correct.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Common.Maths`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Common.Maths
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Common.Maths" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Common.Maths
```

# How to Use

## Arithmetic

Example

```c#
int v1 = 16;
bool p1 = Arithmetics.IsPowerOfTwo(v1); // returns true

int v2 = 20;
int n1 = Arithmetics.ToNearestPowerOfTwo(v2); // returns 32

long v3 = 3L;
bool p2 = Arithmetics.IsPowerOfTwo(v3); // returns false

long v4 = 1_000_000L;
long n2 = Arithmetics.ToNearestPowerOfTwo(v4); // returns 1048576
```

### Available helpers

```c#
Arithmetics.IsPowerOfTwo(int value);
Arithmetics.IsPowerOfTwo(long value);
Arithmetics.ToNearestPowerOfTwo(int value);
Arithmetics.ToNearestPowerOfTwo(long value);
```



## Combinatorics

Example

```c#
// Factorial examples.
int fact5 = Combinatorics.Factorial(5); // returns 120
int fact0 = Combinatorics.Factorial(0); // returns 1

// Fibonacci examples (0-based sequence: 0,1,1,2,3,5,8,13,etc).
int fib7 = Combinatorics.Fibonacci(7); // returns 13
int fib1 = Combinatorics.Fibonacci(1); // returns 1
```



### Available helpers

```c#
Combinatorics.Factorial(int value);
Combinatorics.Fibonacci(int n)
```



## NumberTheory

Example

```c#
// Prime checks.
int n1 = 17;
bool isPrime1 = NumberTheory.IsPrime(n1); // returns true.

int n2 = 18;
bool isPrime2 = NumberTheory.IsPrime(n2); // returns false.

// Greatest common divisor.
int a = 42;
int b = 56;
int gcd = NumberTheory.GreatestCommonDivisor(a, b); // returns 14.

// Least common multiple (pair).
int lcmPair = NumberTheory.LeastCommonMultiple(4, 6); // returns 12.

// Least common multiple (multiple values).
int lcmMany = NumberTheory.LeastCommonMultiple(2, 3, 5); // returns 30.
```

### Available helpers

```c#
NumberTheory.IsPrime(int value);
NumberTheory.GreatestCommonDivisor(int a, int b);
NumberTheory.LeastCommonMultiple(int a, int b);
NumberTheory.LeastCommonMultiple(params int[] values)
```

# Mathematic branches

Some general info about mathematic branches.

## Arithmetic

The most basic branch of mathematics, focusing on the fundamental operations of addition, subtraction, multiplication, and division. It is concerned with the manipulation of numbers to solve basic computational problems. It does not generally involve variables, unlike algebra. In essence, arithmetic provides the building blocks for other areas of mathematics.

## Algebra

Extends arithmetic by introducing variables and constants to represent unknown numbers or quantities. It also deals with equations, functions, and the rules for manipulating these mathematical objects. Algebra allows you to understand relationships between numbers and variables, and it forms the basis for more advanced mathematical studies in fields like calculus, linear algebra, and more.

## Geometry

Focuses on the properties and relationships of shapes and spaces. It uses algebra and arithmetic to solve problems related to lines, angles, surfaces, and solids.

## Calculus

Concerned with the study of change and motion, dealing with derivatives and integrals. It often requires a strong understanding of algebra and trigonometry.

## Statistics

Focuses on collecting, analysing, interpreting, presenting, and organizing data. It often uses concepts from algebra and calculus.

## Number Theory

Investigates the properties and relationships of numbers, especially integers. It's more abstract and often doesn't involve direct computation like arithmetic.

## Combinatorics

Concerned with counting, arrangement, and combination. This area often utilizes factorials, a concept you can calculate using basic arithmetic but which has wider applications in various branches of mathematics.

## Linear Algebra

Focuses on vector spaces and linear mappings between these spaces. It's fundamental in computer graphics, engineering, physics, and more.

## Differential Equations

Focuses on functions and the derivatives thereof and the solutions to those functions. This is essential in physics, engineering, economics, etc.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.