[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.ApiControllers)](https://www.nuget.org/packages/Elephant.ApiControllers/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.ApiControllers.svg)](https://www.nuget.org/packages/Elephant.ApiControllers/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.ApiControllers/LICENSE.txt)

# About

Provides helpers and extensions for ASP.NET Core controllers to simplify common API patterns. Includes `ElephantControllerBase` for consistent API result handling, ControllerExtensions for file conversion helpers, and Unwrap helpers that translate service `IResult` objects into appropriate `IActionResult` responses. Use these helpers to reduce boilerplate, enforce consistent response mapping, and make testable controllers easier to maintain.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.ApiControllers`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.ApiControllers
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.ApiControllers" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.ApiControllers
```

# How to Use

## ElephantControllerBase

```c#
ToApiResult(): Converts a service response wrapper into an IActionResult using standard HTTP status mapping.
CreatedResult(): Returns a 201 Created response with optional payload.
```

## ControllerExtensions

```c#
ToByteArray(): Convert an uploaded file to a byte array.
ToIFormFile(): Recreate an IFormFile from a byte array.
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

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.