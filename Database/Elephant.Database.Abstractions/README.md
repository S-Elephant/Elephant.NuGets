[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Database.Abstractions)](https://www.nuget.org/packages/Elephant.Database.Abstractions/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Database.Abstractions.svg)](https://www.nuget.org/packages/Elephant.Database.Abstractions/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Database.Abstractions/LICENSE.txt)

# About

Contains abstractions for the [Elephant.Database](https://www.nuget.org/packages/Elephant.Database) NuGet.

# Contained abstractions

- IContext
- IGenericCrudGuidRepository
- IGenericCrudIdRepository
- IGenericCrudRepository

# Upgrade instructions

## 0.9.0 &rarr; 0.10.0

Add the "Async" (without double quotes) to your calls and overrides.

## 0.10.3 &rarr; 1.0.0

Moving to 1.0.0 to mark the project's stability. The transition from 0.10.3 is fully compatible with no breaking changes.
