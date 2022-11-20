# About

File IO library.

## FileService

```c#
void Copy(string sourceFileName, string destFileName, bool overwrite = false);

bool Exists(string? path);

bool NotExists(string? path);

void Delete(string? path);

IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true);

IAsyncEnumerable<IEnumerable<FileInfo>> GetFilesAsync(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true);

Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, bool ignoreInaccessible = true);

Task<IEnumerable<FileInfo>> GetFilesAsyncAsIEnumerable(IEnumerable<DirectoryInfo> sourceDirectories, string searchPattern, SearchOption searchOption, IEnumerable<string> extensions, bool ignoreInaccessible = true);
```

## Example usage

```c#
private async Task Foo(List<string> directoryPaths)
{
    IFileService fileService = new FileService();
    await foreach (IEnumerable<FileInfo> files in fileService.GetFilesAsync(directoryPaths.Select(d => new DirectoryInfo(d)), "*.*", SearchOption.AllDirectories, ".mp4"))
    {
        foreach (FileInfo file in files)
            Console.WriteLine(file.Name);
    }
}
```