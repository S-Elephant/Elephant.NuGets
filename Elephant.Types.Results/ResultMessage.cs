using Elephant.Types.Results.Abstractions;

namespace Elephant.Types.Results
{
	/// <inheritdoc/>
	public class ResultMessage : IResultMessage
	{
		/// <inheritdoc/>
		public string? Message { get; set; } = null;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ResultMessage()
		{
		}

		/// <summary>
		/// Constructor with initializer(s).
		/// </summary>
		public ResultMessage(string? message)
		{
			Message = message;
		}
	}
}
