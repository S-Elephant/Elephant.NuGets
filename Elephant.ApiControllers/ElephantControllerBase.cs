using Elephant.Types.ResponseWrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Elephant.ApiControllers
{
	/// <summary>
	/// Base controller class.
	/// </summary>
	public class ElephantControllerBase : ControllerBase
	{
		/// <summary>
		/// Converts to <see cref="IActionResult"/>.
		/// </summary>
		/// <param name="result"><see cref="ResponseWrapper{TData}"/></param>
		/// <param name="useData">If true, <see cref="ResponseWrapper{TData}.Data"/> will be returned if applicable and possible; Otherwise, <see cref="ResponseWrapper{TData}.Data"/> will never be returned.</param>
		protected IActionResult ToApiResult<TData>(ResponseWrapper<TData> result, bool useData = true)
			where TData : new()
		{
			if (result.IsSuccess)
			{
				return result.StatusCode switch
				{
					StatusCodes.Status200OK => result.UsesData && useData ? Ok(result.Data) : Ok(),
					StatusCodes.Status201Created => CreatedResult(),
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
                StatusCodes.Status400BadRequest => BadRequest(result.Message),
				StatusCodes.Status401Unauthorized => Unauthorized(result.Message),
				StatusCodes.Status404NotFound => NotFound(result.Message),
                StatusCodes.Status422UnprocessableEntity => UnprocessableEntity(result.Message),
                StatusCodes.Status500InternalServerError => StatusCode(result.StatusCode, result.Message),
				_ => throw new InvalidEnumArgumentException(nameof(StatusCodes), result.StatusCode, result.StatusCode.GetType()),
			};
		}

		/// <summary>
		/// Converts to <see cref="IActionResult"/>.
		/// </summary>
		protected IActionResult ToApiResult(ResponseWrapper result)
		{
			return ToApiResult(result, false);
		}

		/// <summary>
		/// Returns an <see cref="IActionResult"/> <see cref="StatusCodes.Status201Created"/>.
		/// </summary>
		protected IActionResult CreatedResult()
		{
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}