using Elephant.Common;
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
		/// Converts to <see cref="IActionResult"/>.
		/// </summary>
		protected IActionResult ToApiResult<T>(IResultStatus<T> result)
		{
			if (result.IsSuccess)
			{
				return result.StatusCode switch
				{
					StatusCodes.Status200OK => Ok(result.Value),
					StatusCodes.Status201Created => CreatedResult(),
					_ => throw new CaseStatementMissingException(result.StatusCode),
				};
			}

			return result.StatusCode switch
			{
				StatusCodes.Status200OK => throw new Exception($"IsSuccess is {result.IsSuccess} and {nameof(result.StatusCode)} is {StatusCodes.Status200OK}?"),
				StatusCodes.Status400BadRequest => BadRequest(result.Message),
				StatusCodes.Status401Unauthorized => Unauthorized(result.Message),
				StatusCodes.Status404NotFound => NotFound(result.Message),
				StatusCodes.Status500InternalServerError => StatusCode(500, result.Message),
				_ => throw new CaseStatementMissingException(result.StatusCode),
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