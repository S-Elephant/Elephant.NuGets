[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.UndoRedo)](https://www.nuget.org/packages/Elephant.UndoRedo/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.UndoRedo.svg)](https://www.nuget.org/packages/Elephant.UndoRedo/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.UndoRedo/LICENSE.txt)

# About

Provides undo-redo logic.

# Installation

Choose one:

## **Package Manager** (Visual Studio GUI)
1. Right-click your project → "Manage NuGet Packages".
2. Search for `Elephant.UndoRedo`.
3. Click "Install".

## **.NET CLI** (Command Line)
```bash
dotnet add package Elephant.UndoRedo
```

## **PackageReference** (Project File)
```xml
<PackageReference Include="Elephant.UndoRedo" Version="x.x.x" />
```

## **Package Manager (CLI)**
```bash
nuget install Elephant.UndoRedo
```

# How to Use

## Example

```c#
using Elephant.UndoRedo;

internal class ExampleClass
{
    private UndoRedo<int> _undoRedoManager = new();
    
    private void Example()
    {
       _undoRedoManager.SaveState(1, 2, 3);
       _undoRedoManager.Undo();
       _undoRedoManager.Redo();
       _undoRedoManager.Undo();
       int value = _undoRedoManager.CurrentState; // Contains value: 2
    }
}
```

## Other features

```c#
private UndoRedo<int> _undoRedoManager = new();

private void Example()
{
  int undoCount = _undoRedoManager.UndoCount;
  int redoCount = _undoRedoManager.RedoCount;
  bool canUndo = _undoRedoManager.CanUndo;
  bool canRedo = _undoRedoManager.CanRedo;
  _undoRedoManager.SaveStates(new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 }); // Save multiple states.
  _undoRedoManager.UndoXTimes(2); // Undo two times.
  _undoRedoManager.RedoXTimes(10, true); // Redo ten times, ignoring errors which in this case could mean that it will redo until the end.
  _undoRedoManager.UndoAll();
  _undoRedoManager.RedoAll();
}
```

# Contributing

Contributions are welcome. Please read our [CONTRIBUTING.md](../CONTRIBUTING.md) file for guidelines on how to proceed.

# License

This project is licensed under the MIT License. See the [LICENSE.txt](../LICENSE.txt) file for details.

