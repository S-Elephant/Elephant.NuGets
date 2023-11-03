namespace Elephant.Types.Results.Abstractions
{
	/// <summary>
	/// Contains 0 or more messages.
	/// </summary>
	public interface IResultMessage
	{
		/// <summary>
		/// Message.
		/// </summary>
		string? Message { get; }
	}
}
