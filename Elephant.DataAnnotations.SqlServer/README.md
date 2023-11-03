[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.DataAnnotations.SqlServer)](https://www.nuget.org/packages/Elephant.DataAnnotations.SqlServer/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.DataAnnotations.SqlServer.svg)](https://www.nuget.org/packages/Elephant.DataAnnotations.SqlServer/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.DataAnnotations.SqlServer/LICENSE.txt)

# About

Contains validation attributes specific for Sql Server.

# Attribute list

Some attributes use predefined lengths from the [Elephant.Constants.SqlServer NuGet](https://www.nuget.org/packages/Elephant.Constants.SqlServer/).

- [**FilenameMaxLength**(int minLength = 0)] *(compatible with string)*
- [**FilenameMaxLengthRequired**(int minLength = 0)] *(compatible with string)*
- [**PathFolderMaxLength**(int minLength = 0)] *(compatible with string)*
- [**PathFolderMaxLengthRequired**(int minLength = 0)] *(compatible with string)*