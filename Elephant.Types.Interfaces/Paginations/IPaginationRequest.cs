namespace Elephant.Types.Interfaces.Paginations
{
	/// <summary>
	/// Pagination request.
	/// </summary>
	public interface IPaginationRequest
	{
		/// <summary>
		/// The current requested page. Starts at 0.
		/// </summary>
		int Offset { get; set; }

		/// <summary>
		/// The maximum amount of items per page for this request.
		/// </summary>
		int Limit { get; set; }
	}
}
