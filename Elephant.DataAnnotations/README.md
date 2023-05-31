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

