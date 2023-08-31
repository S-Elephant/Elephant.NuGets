# About

Contains **UUID v5** (a.k.a. **UUID type 5**) helper functions according to https://datatracker.ietf.org/doc/html/rfc4122.

## Why use it?

Usually you have a centralized point that generates and/or stores your GUID's but if for any reason you cannot, and you have to create them in several different unconnected places but still require unique GUID's that are the same for a given namespace and value, then this is an option.

# Usage example

```c#
using System;
using Elephant.Uuidv5Utilities;

/// <summary>
/// Example UUID v5 usage.
/// </summary>
class Program
{
    /// <summary>
	/// Application entry point.
	/// </summary>
	private static void Main()
    {
        Guid namespaceId = new Guid("0a9c8784-f000-42bd-8ac2-8cc3c51dc0ec");
        string customerName = "John Pikachu";
        
        // Note that the UUID v5 is returned as a GUID.
        Guid newGuid = Uuidv5Utils.GenerateGuid(namespaceId, uniqueName);
        Console.WriteLine($"UUIDv5: {newGuid}");
    }
}
```

*Note that there's also an identical non-static class **Uuidv5 : IUuidv5** included in this NuGet.*

