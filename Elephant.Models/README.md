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

## PaginationResponseWrapper

```c#
PaginationResponseWrapper<TData> : IPaginationResponseWrapper<TData> where TData : new()
```

### Properties

- TData? Data
- bool IsFirstPage
- bool IsLastPage
- int Offset
- int Limit
- string? PageUrlFirst
- string? PageUrlLast
- string? PageUrlNext
- string? PageUrlPrevious
- int TotalItems
- int TotalPageCount
