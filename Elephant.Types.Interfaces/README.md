# About

Contains shared/common/generic type interfaces.

# Basic type interfaces

- IId
- IIdName
- IIdNameDescription
- IIsEnabled
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