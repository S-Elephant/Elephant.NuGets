# About

Contains a MAUI/Blazor ClipBoard service.

# Services

- IClipBoardService & ClipBoardService.

## Example usage

```c#
public partial class MyPage
{
	[Inject] private IClipboardService ClipboardService { get; set; } = null!;
    
    public async Task SetClipboard()
    {
		await ClipboardService.SetTextAsync("Your text here.");
    }
    
	public async Task GetClipboard()
    {
		string clipboardText = await ClipboardService.GetTextAsync();
    }
}
```

