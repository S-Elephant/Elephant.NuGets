namespace Elephant.Blazor.Clipboard.Interfaces
{
	/// <summary>
	/// Clipboard service (frontend).
	/// </summary>
	public interface IClipboardService
	{
		/// <summary>
		/// Get text.
		/// </summary>
		ValueTask<string> GetTextAsync();

		/// <summary>
		/// Set text.
		/// </summary>
		ValueTask SetTextAsync(string text);
	}
}
