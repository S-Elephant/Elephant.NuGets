# About

Generic and shared common code library.

```c#
StartOnOsStart.IsRegisteredInStartup(string applicationName);

StartOnOsStart.RegisterInStartup(string applicationName, string fullPathToExecutable);

StartOnOsStart.DeregisterFromStartup(string applicationName);
```

```c#
StringOperations.CapitalizeFirstChar(string stringToCapitalize);

StringOperations.CapitalizeFirstCharNullable(string? stringToCapitalize);
    
StringOperations.Join(char separatorChar, params string?[] stringsToCombine);

StringOperations.JoinWithLeading(char separatorChar, params string?[] stringsToCombine);

StringOperations.JoinWithTrailing(char separatorChar, params string?[] stringsToCombine);

StringOperations.JoinWithLeadingAndTrailing(char separatorChar, params string?[] stringsToCombine);

StringOperations.RemoveSubstringFromString(string source, string substringToRemove)

StringOperations.RemoveSubstringsFromString(string source, IEnumerable<string> substringsToRemove)

StringOperations.ToTitleCase(string stringToTitleCase)

StringOperations.ToTitleCaseNullable(string? stringToTitleCase)
```

```c#
ProcessStarter.StartProcess(string? fullPath, string? arguments = null, string? workingDirectory = null, bool useShellExecute = false);
```

## Various

Base entities.
