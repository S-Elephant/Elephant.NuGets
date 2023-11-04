[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Database.Utilities)](https://www.nuget.org/packages/Elephant.Database.Utilities/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Database.Utilities.svg)](https://www.nuget.org/packages/Elephant.Database.Utilities/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Database.Utilities/LICENSE.txt)

# About

Contains database utility methods without having database dependencies.

# IdService

May be used to check if a model is an insert or update model.

```c#
bool IsIdInsert(int id);
bool IsIdUpdate(int id);
bool IsIdNotInsert(int id);
bool IsIdNotUpdate(int id);
bool IsInvalid(int id);
bool IsValid(int id);
```