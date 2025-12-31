[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.DataAnnotations)](https://www.nuget.org/packages/Elephant.DataAnnotations/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.DataAnnotations.svg)](https://www.nuget.org/packages/Elephant.DataAnnotations/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.DataAnnotations/LICENSE.txt)

# About

Contains validation attributes commonly used in backend endpoint Controllers and view models.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.DataAnnotations`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.DataAnnotations
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.DataAnnotations" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.DataAnnotations
```

# How to Use

## Examples

```c#
[IfEmptyMakeItNull] // Note that this attribute may also be placed on the property or properties themselves.
private class Customer
{
	public string? Notes { get; set; } = string.Empty; // Note that this MUST be a nullable type.
	..
}

..

private void Foo()
{
	DataAnnotationService dataAnnotationService = new(); // Use DI if possible.
	Customer customer = new();
	dataAnnotationService.ReplaceEmptyStringsWithNulls(ref customer);
	// customer.Notes value is now null.
}
```

```c#
// Example: list constraints.
public class TagRequest
{
	[ListNotEmptyRequired] // list must be non-null and contain at least one element.
	public List<string>? Tags { get; set; }

	[ListMinRequired(2)] // Must contain at least 2 items.
	[ListMax(10)] // must contain at most 10 items.
	public List<int>? Ids { get; set; }

	[ListOneItemRequired] // Exactly one item required (useful for single-selection fields).
	public List<string>? SingleSelection { get; set; }
}
```

```c#
// Example: using in a controller action (MVC / minimal).
[HttpPost]
public IActionResult Create([FromBody] TagRequest request)
{
	if (!ModelState.IsValid)
		return BadRequest(ModelState); // Validation attributes above populate ModelState errors.

	return Ok();
}
```



## Validation attribute list

- [**EqualsAnotherPropertyStringAttribute**(string otherPropertyName)]
- [**EqualsAnotherPropertyStringRequiredAttribute**(string otherPropertyName)]
- [**FilenameAllowAlphaNumericOnly**(bool allowDot, bool allowUnderscore, string extraAllowedCharacters)] *(compatible with string, byte, sbyte, int, long, short)*
- [**FilenameAllowAlphaNumericOnlyRequired**(bool allowDot, bool allowUnderscore, string extraAllowedCharacters)] *(compatible with string, byte, sbyte, int, long, short)*
- [**FileSignature**(params AllowedFileExtensionType[] allowedFileExtensions)]
- [**FileSignatureRequired**(params AllowedFileExtensionType[] allowedFileExtensions)]
- [**FileSizeMax**(int fileSizeMax)]
- [**FileSizeMin**(int fileSizeMin)]
- [**GreaterThanZero**] *(compatible with all standard numeric types)*
- [**GreaterThanZeroRequired**] *(compatible with all standard numeric types)*
- [**GuidNotEmpty**(allowNull)]
- [**ListMax**(int maxValue)]
- [**ListMin**(int minValue)]
- [**ListMinRequired**(int minValue)]
- [**ListNotEmptyRequired**]
- [**ListOneItemRequired**]

## Attribute list

- **[IfEmptyMakeItNull]** (To use it without writing code, use the **DataAnnotationService.ReplaceEmptyStringsWithNulls(..)**)

## Services

- IDataAnnotationService


# Upgrade instructions

## 1.0.0 &rarr; 2.0.0

- `[FileSignature]` now may throw an `ArgumentException` instead of an `IndexOutOfRangeException `.

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.