[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Io)](https://www.nuget.org/packages/Elephant.Io/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Io.svg)](https://www.nuget.org/packages/Elephant.Io/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Io/LICENSE.txt)

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

## DirectoryService

```c#
DirectoryInfo CreateDirectory(string path);
void Delete(string path, bool recursive = false);
IEnumerable<string> EnumerateFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
IEnumerable<string> EnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
bool Exists(string? path);
string[] GetFiles(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
string[] GetDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly);
void Move(string sourceDirName, string destDirName);
string GetCurrentDirectory();
bool NotExists(string? path);
void SetCurrentDirectory(string path);
```

