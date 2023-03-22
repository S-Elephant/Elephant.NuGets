using Elephant.Blazor.Clipboard.Interfaces;
using Microsoft.JSInterop;

namespace Elephant.Blazor.Clipboard
{
	/// <inheritdoc cref="IClipboardService"/>
	public sealed class ClipboardService : IClipboardService
	{
		private readonly IJSRuntime _jsRuntime;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ClipboardService(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		/// <inheritdoc/>
		public ValueTask<string> GetTextAsync()
		{
			return _jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
		}

		/// <inheritdoc/>
		public ValueTask SetTextAsync(string text)
		{
			return _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
		}
	}
}
