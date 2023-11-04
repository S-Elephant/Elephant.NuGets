using System.ComponentModel;
using Elephant.Types.Interfaces.ResponseWrappers;
using Elephant.Types.ResponseWrappers;
using Elephant.Types.Results.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elephant.ApiControllers
{
	/// <summary>
	/// Base controller class.
	/// </summary>
	public class ElephantControllerBase : ControllerBase
	{
		/// <summary>
		/// Unwrap the <paramref name="result"/> to an <see cref="IActionResult"/>.
		/// </summary>
		/// <typeparam name="TData"><paramref name="result"/> data type.</typeparam>
		/// <param name="result">Result.</param>
		/// <returns><see cref="IActionResult"/></returns>
		protected virtual IActionResult Unwrap<TData>(IResult<TData> result)
		{
			if (result.UsesData)
			{
				return result.StatusCode switch
				{
					StatusCodes.Status204NoContent => NoContent(),
					StatusCodes.Status201Created => CreatedResult(result.Data),
					_ => StatusCode(result.StatusCode, result.Data)
				};
			}

			return StatusCode(result.StatusCode, result.Message);
		}

		/// <summary>
		/// Unwrap the <paramref name="result"/> that uses no data to an <see cref="IActionResult"/>.
		/// </summary>
		/// <param name="result">Result.</param>
		/// <returns><see cref="IActionResult"/></returns>
		protected virtual IActionResult Unwrap(IResult result)
		{
			if (result.UsesData)
				throw new InvalidOperationException($"{nameof(result)} shouldn't be using any data. If you want to use data then use IResult<TData> instead.");

			return Unwrap<bool>(result);
		}

		/// <summary>
		/// Convert to <see cref="IActionResult"/>.
		/// </summary>
		/// <param name="result"><see cref="ResponseWrapper{TData}"/></param>
		/// <param name="useData">If true, <see cref="ResponseWrapper{TData}.Data"/> will be returned if applicable and possible; Otherwise, <see cref="ResponseWrapper{TData}.Data"/> will never be returned.</param>
		[Obsolete("Use Unwrap() and the new NuGet Elephant.Types.Results(.Abstractions) instead.")]
		protected IActionResult ToApiResult<TData>(IResponseWrapper<TData> result, bool useData = true)
			where TData : new()
		{
			if (result.IsSuccess)
			{
				return result.StatusCode switch
				{
					StatusCodes.Status200OK => result.UsesData && useData ? Ok(result.Data) : Ok(),
					StatusCodes.Status201Created => result.UsesData && useData ? CreatedResult(result.Data) : CreatedResult(),
					StatusCodes.Status204NoContent => NoContent(),
					StatusCodes.Status401Unauthorized => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
                    StatusCodes.Status404NotFound => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
                    StatusCodes.Status422UnprocessableEntity => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
                    StatusCodes.Status500InternalServerError => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
                    _ => throw new InvalidEnumArgumentException(nameof(StatusCodes), result.StatusCode, result.StatusCode.GetType()),
				};
			}

			return result.StatusCode switch
			{
				StatusCodes.Status200OK => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
                StatusCodes.Status201Created => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
				StatusCodes.Status204NoContent => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {result.StatusCode}?"),
				StatusCodes.Status400BadRequest => BadRequest(result.Message),
				StatusCodes.Status401Unauthorized => Unauthorized(result.Message),
				StatusCodes.Status404NotFound => NotFound(result.Message),
                StatusCodes.Status422UnprocessableEntity => UnprocessableEntity(result.Message),
                StatusCodes.Status500InternalServerError => StatusCode(result.StatusCode, result.Message),
				_ => throw new InvalidEnumArgumentException(nameof(StatusCodes), result.StatusCode, result.StatusCode.GetType()),
			};
		}

		/// <summary>
		/// Convert to <see cref="IActionResult"/>.
		/// </summary>
		[Obsolete("Use Unwrap() and the new NuGet Elephant.Types.Results(.Abstractions) instead.")]
		protected IActionResult ToApiResult(IResponseWrapper result)
		{
			return ToApiResult(result, false);
		}

		/// <summary>
		/// Return an <see cref="IActionResult"/> <see cref="StatusCodes.Status201Created"/>.
		/// </summary>
		protected IActionResult CreatedResult(object? value = null)
		{
			if (value == null)
				return StatusCode(StatusCodes.Status201Created);

			return StatusCode(StatusCodes.Status201Created, value);
		}
	}
}