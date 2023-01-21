using Elephant.Types.Interfaces.Paginations;
using System.ComponentModel.DataAnnotations;

namespace Elephant.Models.RequestModels
{
	/// <summary>
	/// Pagination request model.
	/// </summary>
	/// <example><![CDATA[public async Task<IActionResult> All([FromQuery] PaginationRequestModel pagination, CancellationToken cancellationToken)]]></example>
	public class PaginationRequest : IPaginationRequest
	{
		/// <summary>
		/// The offset (=page number). Cannot be smaller than 0. Defaults to 0.
		/// </summary>
		[Range(0, int.MaxValue)]
		public int Offset { get; set; } = 0;

		/// <summary>
		/// Maximum amount of results per page. Cannot be smaller than 1. Defaults to <see cref="int.MaxValue"/>.
		/// </summary>
		[Range(0, int.MaxValue)]
		public int Limit { get; set; } = int.MaxValue;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PaginationRequest()
		{
		}

		/// <summary>
		/// Constructor with initializers.
		/// </summary>
		/// <param name="offset">The offset (=page number). Starts at 0 and defaults to 0. Cannot be smaller than 0.</param>
		/// <param name="limit">
		/// Maximum amount of results per page.
		/// If this value is set to a value smaller than 1 then the value <see cref="int.MaxValue"/> will be assigned instead.
		/// Defaults to <see cref="int.MaxValue"/>.
		/// </param>
		public PaginationRequest(int offset, int limit = int.MaxValue)
		{
			Offset = offset < 0 ? 0 : offset;
			Limit = limit < 1 ? int.MaxValue : limit;
		}
	}
}
