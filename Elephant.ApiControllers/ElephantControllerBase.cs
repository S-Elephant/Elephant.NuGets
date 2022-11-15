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
		protected IActionResult ToApiResult<T>(ResponseWrapper<T> result) where T : new()
		{
			if (result.IsSuccess)
			{
				return result.StatusCode switch
				{
					StatusCodes.Status200OK => Ok(result.Data),
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
		/// Returns an <see cref="IActionResult"/> <see cref="StatusCodes.Status201Created"/>.
		/// </summary>
		protected IActionResult CreatedResult()
		{
			return StatusCode(StatusCodes.Status201Created);
		}
	}
}