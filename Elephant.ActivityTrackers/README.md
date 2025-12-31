[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.ActivityTrackers)](https://www.nuget.org/packages/Elephant.ActivityTrackers/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.ActivityTrackers.svg)](https://www.nuget.org/packages/Elephant.ActivityTrackers/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.ActivityTrackers/LICENSE.txt)

# About

Is able to track various activities and also has a generic version with custom keys and custom data.

For example: while downloading and uploading various images you may want to disable other actions, GUI elements or other things based on what activities are currently running. You may also want to know more about those activities for debugging purposes and perhaps you are also saving different versions of the same images simultaneously.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.ActivityTrackers`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.ActivityTrackers
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.ActivityTrackers" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.ActivityTrackers
```

# How to Use

## Example

```c#
using Elephant.ActivityTrackers;

internal class ExampleClass
{
	private readonly ActivityTracker _activityTracker = new();

	internal void Foo()
	{
		_activityTracker.Add("processing-image");
		_activityTracker.Remove("processing-image");
		_activityTracker.Add("saving-image");
		_activityTracker.Add("send-image-to-backend");
	}

	internal void UpdateGui()
	{
		if (_activityTracker.IsBeingProcessed("saving-image"))
		{
			// Enable some UI elements or such.
		}
	}
}
```

## Generic example

```c#
using Elephant.ActivityTrackers;

/// <summary>Your custom class. Can also be a struct.</summary>
internal class ActivityInfo
{
	public string ExampleData { get; set; } = string.Empty;

	public ActivityInfo()
	{
	}

	public ActivityInfo(string exampleData)
	{
		ExampleData = exampleData;
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
			return false;

		ActivityInfo other = (ActivityInfo)obj;
		return ExampleData == other.ExampleData;
	}

	public override int GetHashCode()
	{
		return ExampleData.GetHashCode();
	}
}

internal class ExampleGenericClass
{
    // In this example we use a string key to distinguish between different activities.
	private readonly ActivityTrackerGeneric<string, ActivityInfo> _activityTracker = new();

	internal void Foo()
	{
		ActivityInfo processingImage1 = new("My custom data here.");
		ActivityInfo processingImage2 = new("My custom data here2.");
		ActivityInfo processingImage3 = new("My custom data here3.");

		_activityTracker.Add("processing-image", processingImage1);
		_activityTracker.Add("processing-image", processingImage2);

		// Will remove 1 "processing-image" key with the ActivityInfo that contains string "My custom data here" only.
		_activityTracker.Remove("processing-image", processingImage1);

		// Will remove nothing because the combination of key and value doesn't exist.
		_activityTracker.Remove("processing-image", processingImage3);

		// Will remove one entry from key "processing-image", regardless of it's value.
		_activityTracker.Remove("processing-image");
	}

	internal void UpdateGui()
	{
		if (_activityTracker.IsBeingProcessed("saving-image"))
		{
			// Enable some UI elements or such.
		}
	}
}
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.