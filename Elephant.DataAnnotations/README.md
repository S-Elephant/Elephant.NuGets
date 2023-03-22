# About

Contains validation attributes commonly used in backend endpoint Controllers and view models.

# Attribute list

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