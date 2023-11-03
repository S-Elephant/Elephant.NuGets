[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.ApiControllers)](https://www.nuget.org/packages/Elephant.ApiControllers/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.ApiControllers.svg)](https://www.nuget.org/packages/Elephant.ApiControllers/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.ApiControllers/LICENSE.txt)

# About

Contains Controller helpers/extensions.

## ElephantControllerBase

```c#
protected IActionResult ToApiResult<T>(ResponseWrapper<T> result) where T : new();
protected IActionResult CreatedResult();
```

## ControllerExtensions

```c#
public static byte[] ToByteArray(this IFormFile formFile);
public static IFormFile ToIFormFile(this byte[] byteArray, string name = "", string filename = "", string contentType = "");
```

## Unwrap example

```c#
// Your attributes here.
MyController : ElephantControllerBase
{
	[HttpPost]
	[Route("customer-create-test")]
	[ProducesResponseType(typeof(List<Customer>), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateCustomers(List<CustomerCreateRequestModel> requestModel, CancellationToken cancellationToken)
	{
        IResult<Customer> result = await _customerService.Create(requestModel, cancellationToken);

        // Will return a 201 created response along with the created customers (assuming that your service returns them).
        return Unwrap(result);
	}
    
	[HttpPost]
	[Route("order-create-test")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateOrders(List<OrderCreateRequestModel> requestModel, CancellationToken cancellationToken)
	{
        IResult result = await _orderService.Create(requestModel, cancellationToken);

        // Will return a 201 created response without any order data.
        return Unwrap(result);
	}
}
```

