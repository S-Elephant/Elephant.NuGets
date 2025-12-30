[![Nuget downloads](https://img.shields.io/nuget/v/Elephant.Texts)](https://www.nuget.org/packages/Elephant.Texts/) [![NuGet Downloads](https://img.shields.io/nuget/dt/Elephant.Texts.svg)](https://www.nuget.org/packages/Elephant.Texts/) ![Workflow](https://github.com/S-Elephant/Elephant.NuGets/actions/workflows/GitHubActions.yml/badge.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/S-Elephant/Elephant.NuGets/tree/master/Elephant.Texts/LICENSE.txt)

# About

Provides various text utilities:

- Anagrams
- Palindromes
- Pangrams
- Parentheses validator
  - Supports default, custom symmetric and custom asymmetric parentheses.
- Roman numerals
  - Supports 4 different Roman large number formats (for values `> 3999`).
  - Integer to Roman supported range: `0 - int.MaxValue`.
  - Roman to integer supported range: `0 - 3999`.

# Examples

## General
```c#
bool isValidAnagram1 = new Anagram().IsValid("new york times", "monkeys write");
bool isValidAnagram2 = new Anagram().IsValid("résumé", "mésuré");

bool isValidPalindrome1 = new Palindrome().IsValid("Taco cat");
bool isValidPalindrome2 = new Palindrome().IsValid("Was it Eliot's toilet I saw?");

// The PanGram class delivers exceptional performance.
bool isValidPangram1 = new Pangram().IsValid("The quick brown fox jumps over the lazy dog");
bool isValidPangram2 = new Pangram().IsValid(new System.Text.StringBuilder().Append('x', 1_000_000).Append("abcdefghijklmnopqrstuvwxyz").ToString()));
```

## Parentheses validator

```c#
IParenthesesValidator parenthesesValidator;

bool isValid = _parenthesesValidator.IsValid("([{}])"); // Returns true
isValid = _parenthesesValidator.IsValid("Something(yellow[{with a tail}])!"); // Returns true
isValid = _parenthesesValidator.IsValid("("); // Returns false
isValid = _parenthesesValidator.IsValid("[("); // Returns false
isValid = _parenthesesValidator.IsValid("[()]"); // Returns true
isValid = _parenthesesValidator.IsValid("([)]"); // Returns false (because not strictly nested)

IParenthesesValidator customValidator = new ParenthesesValidator(new() { { '«', '»' }, { '{', '}' } });
isValid = customValidator.IsValid("«test»"); // Returns true
isValid = customValidator.IsValid("(test)"); // Returns true (because these parentheses aren't included in the custom validator)
isValid = customValidator.IsValid("(test)(("); // Returns true (because these parentheses aren't included in the custom validator)
isValid = customValidator.IsValid("«t{e}st»"); // Returns true
```



## Roman

```c#
IRomanNumeralConverter romanNumeralConverter = new RomanNumeralConverter();

// Supported range for IntToRoman(): Between 0 and int.MaxValue.
romanNumeralConverter.IntToRoman(1); // Outputs I
romanNumeralConverter.IntToRoman(10); // Outputs X
romanNumeralConverter.IntToRoman(49); // Outputs XLIX

romanNumeralConverter.IntToRoman(10000); // Using the default overline Roman large number format, outputs: X̅
romanNumeralConverter.IntToRoman(15000); // Using the default overline Roman large number format, outputs: X̅V̅
romanNumeralConverter.IntToRoman(10000, RomanLargeNumberFormat.Apostrophus); // Outputs: CC|Ↄ|Ↄ
romanNumeralConverter.IntToRoman(15000, RomanLargeNumberFormat.Parentheses); // Outputs: (XV)
romanNumeralConverter.IntToRoman(5000, RomanLargeNumberFormat.MPrefix); // Outputs: MMMMM
romanNumeralConverter.IntToRoman(int.MaxValue, RomanLargeNumberFormat.Overline); // Outputs: I̅I̅C̅X̅L̅V̅I̅I̅C̅D̅L̅X̅X̅X̅I̅I̅I̅DCXLVII

// Supported range for SmallRomanToInt(): Between 0 and 3999.
romanNumeralConverter.SmallRomanToInt("mcmxcix"); // Outputs: 1999
```

# Upgrade instructions

## 1.0.0 &rarr; 2.0.0

- Renamed the namespace of `ParenthesesValidator` from `Elephant.Texts.Tests` into `Elephant.Texts`.
- Renamed the namespace of `IParenthesesValidator` from `Elephant.Texts.Tests` into `Elephant.Texts.Abstractions`.
