## Coding Standards

- Always use explicit types instead of `var` (e.g., `string name = "John";` not `var name = "John";`) unless the explicit type would be very long and dictionary value-key-pairs may also use var.
- Favor implicit object creation `new()` over explicit type repetition.
- All code comments should end with a period/full stop.
- Add XML summaries for all methods and for all properties and fields.
- For unit- and integration tests always use the Triple A pattern. Add the comments: `// Arrange.`, `// Act,`, `// Assert.` or `// Act & Assert.` where applicable.
- Write proper variable names without single letters if possible. I.e. `foreach (Video v in sampleVideos)` should be `foreach (Video video in sampleVideos)`
- Use trailing comma in multi-line initializers where applicable (i.e. don't use this for JSON files).
- Use `GeneratedRegexAttribute` to generate the regular expression implementation at compile-time.
- Don't use the name `Shared` for variables, classes, projects, etc. because `shared` is a reserved word in Visual Basic. Use `Common` instead.
- Use `EF.Functions.Collate` for database LINQ queries instead of `.ToLower`. Example: `.Where(a => a.ActorAliases.Any(alias => EF.Functions.Like(EF.Functions.Collate(alias.Name, Databases.CaseSensitiveCollation), $"{actorNameLower}")))`
- For exceptions use `ThrowIfNull` when applicable. I.e. use `ArgumentNullException.ThrowIfNull(obj);` instead of `if (obj == null) throw new ArgumentNullException(nameof(obj));`
- When suppressing warnings or errors, you must include a justification comment or add a TODO task.
- Single-line TODO comments MUST start with the following prefix: `TODO: `.
- TODO's must be described. This is wrong: `// TODO` while this is correct: `// TODO: rewrite the interface because it's outdated and then remove this suppression`.
- Avoid starting XML documentation comments with articles like "A", "An", "The", etc.
  - Wrong: `<summary>A class that handles user authentication.</summary>`
  - Correct: `<summary>Handles user authentication.</summary>`.



## Testing Guidelines

### Comprehensive Test Coverage

- When writing unit/integration tests, ensure thorough coverage by testing (where applicable) the following inputs:
  - Negatives
  - nulls
  - Min and max
  - Empty string or value or collection
  - White-space-only
  - Different case (i.e. uppercase, lowercase, both upper and lower combined).
  



## Readme guidelines

### Structure in chronological order

- **Badges** (when applicable)
  - NuGet version, downloads, workflow status, and license badges.
- **About** (or project name)
  - Clear description of what the package does and its purpose.
  - Include a brief explanation of key concepts when applicable.
  - **Features** (optional)
- **Installation**
  - **Install via NuGet**
    - Show both dotnet CLI and Package Manager Console commands.
- **How to Use**
  - Provide clear, practical code examples if applicable.
  - Follow all coding standards.

- **Building** (optional, contains instructions for how to build it)
- **Requirements** (optional)
- **Upgrade Instructions** (required when applicable)
- **Contributing** (optional but recommended)
  - Brief note that contributions are welcome (or not) with a link to the main `CONTRIBUTING.md` if applicable.
- **License**
  - Brief license statement with link to the `LICENSE.txt` file.

#### GitHub badges example

  ```markdown
  [![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Constants.Sqlite)](https://www.nuget.org/packages/Elephant.Constants.Sqlite/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Constants.Sqlite.svg)](https://www.nuget.org/packages/Elephant.Constants.Sqlite/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Constants.Sqlite/LICENSE.txt)
  ```

#### Install via NuGet example

````markdown
# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.Common`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.Common
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.Common" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.Common
```
````

#### Upgrade instructions example

```markdown
# Upgrade Instructions

## 1.0.0 &rarr; 2.0.0

- Replace `DbType.Primitives.Blob` with `DbType.Primitives.BlobPrimitive`
- Replace `DbType.Primitives.Real` with `DbType.Primitives.RealPrimitive`

## 2.0.0 &rarr; 3.0.0

- `Foo(string message)` now has 1 required string parameter.
```

### Note example

```markdown
> NOTE: Your note here.
```




## Other

- NEVER remove all unused usings across the entire project and/or solution because some are actually in use (i.e. when they are inside compiler directives) may not be recognized as being in use and would be wrongly removed. Manually check every single one before removing.
