[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Models)](https://www.nuget.org/packages/Elephant.Models/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Models.svg)](https://www.nuget.org/packages/Elephant.Models/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Models/LICENSE.txt)

# About

Contains shared/common/generic models.

# Request models

## PaginationRequestModel

Use it for pagination. Can be combined with the Elephant.Types NuGet for returning pagination within a response. Example:

```c#
public class PaginationRequest : IPaginationRequest
{
	[Range(0, int.MaxValue)]
	public int Offset { get; set; } = 0;

	[Range(0, int.MaxValue)]
	public int Limit { get; set; } = int.MaxValue;
	..
}
```



# Response models

## PaginationResponseWrapper and PaginationResponseModel

```c#
PaginationResponseModel : IPaginationResponseModel
PaginationResponseWrapper<TData> : PaginationResponseModel, IPaginationResponseWrapper<TData> where TData : new()
```

### Properties

- TData? Data *(wrapper only)*
- bool IsFirstPage
- bool IsLastPage
- int Offset
- int Limit
- string? PageUriFirst
- string? PageUriLast
- string? PageUriNext
- string? PageUriPrevious
- int TotalItems
- int TotalPageCount

# Upgrade instructions

## 1.0.2 &rarr; 2.0.0

- Add/Replace your **Elephant.Models.RequestModels** with **Elephant.Models.ResponseModels** if you used the PaginationResponseWrapper<TData>.