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
```
