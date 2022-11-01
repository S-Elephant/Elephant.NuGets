# About

Contains validation attributes commonly used in backend endpoint Controllers and view models.

# Attribute list

- [**GreaterThanZero**] *(compatible with all standard numeric types)*
- [**GreaterThanZeroRequired**] *(compatible with all standard numeric types)*
- [**FileSignature**(params AllowedFileExtensionType[] allowedFileExtensions)]
- [**FileSignatureRequired**(params AllowedFileExtensionType[] allowedFileExtensions)]
- [**FileSizeMax**(int fileSizeMax)]
- [**FileSizeMin**(int fileSizeMin)]
- [**ListMax**(int maxValue)]
- [**ListMin**(int minValue)]
- [**ListMinRequired**(int minValue)]
- [**ListNotEmptyRequired**]
- [**ListOneItemRequired**]