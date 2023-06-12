# About

Contains constant Microsoft SQL Server database types plus a few extras.

Has both an identical static version and a non-static with an interface.

## DbTypes

### Numeric

- BigInt
- Bool
- Decimal
- Double (=Float(53))
- Float
- Float4
- Int
- Money
- SmallInt
- SmallMoney
- TinyInt
- Real

### Date and datetime

- Date
- DateTime
- DateTime2
- SmallDateTime
- DateTimeOffset
- Time
- Timestamp

### Guid

- Guid
- GuidString
- GuidHex

### Spatial

- Geography
- Geometry

### String

- NVarCharMax
- Text
- VarCharMax

### Language

- Iso639Dash1
- Iso639Dash2

### File and folder

- Filename
- FolderPath
- FolderPathLinux

### Other

- Email
- Enum
- Name
- Password
- Url



## DbLengths

### Numeric

- DecimalPrecision
- DecimalScale
- Float
- Float4

### Language

- Iso639Dash1
- Iso639Dash2

### File and folder

- Filename
- FolderPath
- FolderPathLinux

### Other

- Guid
- GuidHex
- Email
- Name
- Password
- PhoneNumberInternational
- PhoneNumberNetherlands
- Url
- Xml

# Upgrade instructions

## 2.0.4 &rarr; 3.0.0

- Replace your **DbTypes.Guid** with **DbTypes.GuidString**.
