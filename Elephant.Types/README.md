# About

Contains shared/common/generic types but contains no specific types.

# Basic types

## BaseId

Abstract class with an **Id** property and an I**dComparer : IEqualityComparer<BaseId>**.

## BaseIdName

Abstract class that inherits from [BaseId](##BaseId) with a **Name** property.

## BaseIdNameDescription

Abstract class that inherits from [BaseIdName](##BaseIdName) with a **Description** property .

## Trilean

An alternative type to a nullable bool. Its value can be either: **True**, **False** or **Unknown**.

## IIsEnabled

Interface with a boolean **IsEnabled** property.

# Response wrappers

## ResponseWrapper

Wrapper that can hold data, HTTP status code, message and such. Usually used for returning data from service layer &rarr; controller/API layer.

## PagedResponseWrapper

Paginated version of the [ResponseWrapper](##ResponseWrapper)

# Operator enums

Contains the following operator enums:

- Arithmetics
- Assignments
- Bitwises
- Logicals
- Relationals
- Special