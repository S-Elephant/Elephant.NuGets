[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Texts)](https://www.nuget.org/packages/Elephant.Texts/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Texts.svg)](https://www.nuget.org/packages/Elephant.Texts/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Texts/LICENSE.txt)

# About

Provides text utilities.

# Examples

```c#
bool isValidAnagram1 = new Anagram().IsValid("new york times", "monkeys write");
bool isValidAnagram2 = new Anagram().IsValid("résumé", "mésuré");

bool isValidPalindrome1 = new Palindrome().IsValid("Taco cat");
bool isValidPalindrome2 = new Palindrome().IsValid("Was it Eliot's toilet I saw?");

// The PanGram class delivers exceptional performance.
bool isValidPangram1 = new Pangram().IsValid("The quick brown fox jumps over the lazy dog");
bool isValidPangram2 = new Pangram().IsValid(new System.Text.StringBuilder().Append('x', 1_000_000).Append("abcdefghijklmnopqrstuvwxyz").ToString()));
```

