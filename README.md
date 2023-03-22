# Elephant NuGet project

This project contains all (public) NuGet packages. Each project contains its own README.md

These NuGets can be downloaded from [NuGet.org](https://www.nuget.org/profiles/SquirtingElephant) or from the "Manage NuGet Packages" in your IDE.

# Troubleshooting

## Reset your local NuGet service package(s)

On Windows browse to:

```bash
%userprofile%\.nuget\packages
```

On Mac and Linux browse to:

```bash
~/.nuget/packages
```

Delete all packages that you'd like to reset and if Visual Studio was open during the deletion process then restart Visual Studio.

See: https://docs.microsoft.com/en-us/nuget/consume-packages/managing-the-global-packages-and-cache-folders