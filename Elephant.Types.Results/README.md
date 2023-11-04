[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Types.Results)](https://www.nuget.org/packages/Elephant.Types.Results/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Types.Results.svg)](https://www.nuget.org/packages/Elephant.Types.Results/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Types.Results/LICENSE.txt)

# About

Continuation of my older ResponseWrappers from the [Elephant.Types](https://www.nuget.org/packages/Elephant.Types) [![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Types)](https://www.nuget.org/packages/Elephant.Types/). I renamed it to Result because its much shorter and made a variety of improvements.

#  Why use the result pattern

There are really many reasons that can be found online. I'll highlight the 3 that I personally find the most important:

1. Better performance. than exceptions.
2. I don't like throwing exceptions for something that is expected.
3. In some situations (e.x. n-tier) you don't want to be dealing with HTTP status codes in your data access- and business logic layers.

# Usage examples


## With data

```c#
public async Task<IResult<Customer>> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken)
{
	// Update customer logic here.

    return Result<Customer>.Ok(updatedCustomer, "Optional message here.");
}
```

## Without data

```c#
public async Task<IResult> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken)
{
    Customer? customerInDb = _customerRepository.ById(customer.Id);
    if (customerInDb == null)
		return Result.NotFoundNoData();
    
    // Update customer logic here.
}

public async Task<IResult> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken)
{
    Customer? customerInDb = await _customerRepository.ByIdAsync(customer.Id);
    if (customerInDb == null)
		Result.NotFoundNoData($"{nameof(Customer)} with {nameof(Customer.Id)} {Customer.Id} not found.");
    
	// Update customer logic here.

	return Result.OkNoData();
}
```

## Multi status support

```c#
private IResult<string> ErrorMethod(bool errorOccurred)
{
	IResult<string> result = Result<string>.Ok("Success.");

	if (errorOccurred)
    {
        // You can chain different statuses as long as the generic is of the same type.
        // That means you can't combine the data and the no-data versions in the same chain because
        // they have a different return type.
		result.AddError("Error 1")
			.AddInternalServerError("Error 2")
			.AddContinue("Continue")
			.AddOk("Ok data")
			.AddConflict("Concurrency conflict on ..");
    }

	return result;
}

private IResult ChainNoDataMethod()
{
	IResult result = Result.OkNoData();
	result.AddErrorNoData("Unable to fetch cat food.")
		.AddInternalServerErrorNoData("Unable to fetch cat food.")
		.AddInternalServerErrorNoData()
		.AddInternalServerErrorNoData("Unable to fetch cat food.")
		.AddContinueNoData("Continue")
		.AddConflictNoData("Concurrency conflict on ..");

	return result;
}
```

## Custom errors

```c#
return Result.ErrorNoData("Something went wrong.");
return Result<Customer>.Error(new YourCustomException(), 23122, customer); // With 23122 being your optional custom code.
```

## Unwrap in controller

If you easily want to unwrap it in your controller, you may use the NuGet [Elephant.ApiController](https://www.nuget.org/packages/Elephant.ApiControllers) version 3.0.0 or greater and then:

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



# Other

You can override **Result.GetResultStatus** if for example you want to prioritize different statuses using custom logic.

You can create custom statuses as long as you derive from **IResultStatus**.

You can also override the success, error, informative and custom status checks in the **ResultStatus** class. This way you can create custom errors and successes and etc.



