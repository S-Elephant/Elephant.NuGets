# About

Contains lnk (= Windows shortcut) helper methods.

## LnkHelper

Public methods:

```c#
LnkInfo? GetAllInfo(string? fullLnkPath);

string? GetArgumentsAsString(string? fullLnkPath);

string? GetTarget(string? fullLnkPath);

string? GetTargetWithArguments(string? fullLnkPath);

IWshShortcut? TryToReadInfo(string? fullLnkPath);
```

## LnkInfo

Custom Lnk data container.