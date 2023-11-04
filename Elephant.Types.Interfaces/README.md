[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Types.Interfaces)](https://www.nuget.org/packages/Elephant.Types.Interfaces/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Types.Interfaces.svg)](https://www.nuget.org/packages/Elephant.Types.Interfaces/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Types.Interfaces/LICENSE.txt)

# About

Contains shared/common/generic type interfaces.

# Basic type interfaces

- IId
- IIdName
- IIdNameDescription
- IIsEnabled
- IGuid
- IName

# Response wrapper interfaces

- IPagedResponseWrapper<TData>
- IResponseWrapper
- IResponseWrapper<TData>


# Pagination interfaces

- IPaginationRequest
- IPaginationResponseModel
- IPaginationResponseWrapper

# Upgrade instructions

## 2.0.0 &rarr; 3.0.0

- Update your pagination URL properties into URI properties (only its name changed).

## 3.0.2 &rarr; 4.0.0

- All your implementations of **IGuid** should be renamed from **Guid** &rarr; **Id**.