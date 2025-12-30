[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.DataAnnotations)](https://www.nuget.org/packages/Elephant.DataAnnotations/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.DataAnnotations.svg)](https://www.nuget.org/packages/Elephant.DataAnnotations/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.DataAnnotations/LICENSE.txt)

# About

Contains validation attributes commonly used in backend endpoint Controllers and view models.

# Validation attribute list

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

# Attribute list

- **[IfEmptyMakeItNull]** (To use it without writing code, use the **DataAnnotationService.ReplaceEmptyStringsWithNulls(..)**)

# Services

- IDataAnnotationService



# Examples

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

# Upgrade instructions

## 1.0.0 &rarr; 2.0.0

- `[FileSignature]` now may throw an `ArgumentException` instead of an `IndexOutOfRangeException `.
