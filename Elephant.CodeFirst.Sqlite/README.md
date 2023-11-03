[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.CodeFirst.Sqlite)](https://www.nuget.org/packages/Elephant.CodeFirst.Sqlite/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.CodeFirst.Sqlite.svg)](https://www.nuget.org/packages/Elephant.CodeFirst.Sqlite/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.CodeFirst.Sqlite/LICENSE.txt)

# Example usage

```c#
builder.Property(p => p.TodoStatus)
	.HasColumnType(DbType.Enum)
	.HasConversion<int>()
	.IsRequired();
```

