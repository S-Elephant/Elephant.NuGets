[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Blazor.Clipboard)](https://www.nuget.org/packages/Elephant.Blazor.Clipboard/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Blazor.Clipboard.svg)](https://www.nuget.org/packages/Elephant.Blazor.Clipboard/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Blazor.Clipboard/LICENSE.txt)

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

