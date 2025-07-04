using System;
using System.Collections.Generic;
using System.Linq;

namespace Elephant.Texts.Tests
{
	/// <inheritdoc/>
	public class ParenthesesValidator : IParenthesesValidator
	{
		// Default parentheses pairs.
		private readonly Dictionary<char, char> _defaultPairs = new()
		{
			{ '(', ')' },
			{ '[', ']' },
			{ '{', '}' },
			{ '<', '>' },
		};

		/// <summary>
		/// Active parentheses pairs.
		/// </summary>
		private readonly Dictionary<char, char> _pairs;

		/// <summary>
		/// True if the <see cref="_defaultPairs"/> are being used for the <see cref="_pairs"/>.
		/// </summary>
		private bool _isUsingDefaultPairs;

		/// <summary>
		/// Constructor.
		/// </summary>
		public ParenthesesValidator(Dictionary<char, char>? customPairs = null)
		{
			if (customPairs == null)
			{
				_pairs = _defaultPairs;
				_isUsingDefaultPairs = true;
			}
			else
			{
				_pairs = customPairs;
				_isUsingDefaultPairs = false;
			}
		}

		/// <inheritdoc/>
		public bool IsValid(string? text)
		{
			// Early exit for null, empty and white-space-only.
			if (string.IsNullOrWhiteSpace(text))
				return true;

			// Use the other method if custom pairs are being used.
			if (!_isUsingDefaultPairs)
				return IsValidUsingCustomPairs(text);

			Stack<char> stack = new(text!.Length / 2); // Pre-size stack for better performance.
			foreach (char c in text)
			{
				// Handle opening chars (this performs better than a dictionary lookup).
				if (c == '(')
				{
					stack.Push(')');
					continue;
				}
				if (c == '[')
				{
					stack.Push(']');
					continue;
				}
				if (c == '{')
				{
					stack.Push('}');
					continue;
				}
				if (c == '<')
				{
					stack.Push('>');
					continue;
				}

				// Handle closing chars.
				if (c == ')' || c == ']' || c == '}' || c == '>')
				{
					if (stack.Count == 0 || stack.Pop() != c)
						return false; // Early exit on mismatch.
				}

				// All other characters are ignored here.
			}

			// If all brackets are properly nested and closed, the stack should be empty at the end.
			// I.e., if there are any unclosed parentheses then stack.Count will be > 0.
			return stack.Count == 0;
		}

		/// <summary>
		/// Validate whether all parentheses in the <paramref name="text"/> string are properly balanced and closed
		/// using strict nesting and custom pairs. Doesn't perform as wel as <see cref="IsValid"/>.
		/// </summary>
		/// <param name="text">String to validate for balanced parentheses.</param>
		/// <returns>
		/// True if all parentheses are properly balanced and closed, false otherwise.
		/// Non-parentheses characters are ignored during validation.
		/// </returns>
		private bool IsValidUsingCustomPairs(string? text)
		{
			// Early exit for null, empty and white-space-only.
			if (string.IsNullOrWhiteSpace(text))
				return true;

			// Use the other method if default pairs are being used.
			if (_isUsingDefaultPairs)
				return IsValid(text);

			char[] openers = _pairs.Keys.ToArray();
			char[] closers = _pairs.Values.ToArray();
			Stack<char> stack = new(text!.Length / 2);

			foreach (char c in text)
			{
				bool isOpener = false;
				bool isSymmetric = false;
				char expectedCloser = '\0';

				// Check if character is a defined opener.
				foreach (char opener in openers)
				{
					if (c == opener)
					{
						expectedCloser = _pairs[opener];
						isSymmetric = (opener == expectedCloser);
						isOpener = true;
						break;
					}
				}

				if (isOpener)
				{
					// For symmetric pairs.
					if (isSymmetric)
					{
						if (stack.Count > 0 && stack.Peek() == c)
						{
							stack.Pop(); // Treat as closer.
						}
						else
						{
							stack.Push(c); // Treat as opener.
						}
					}
					// For asymmetric pairs.
					else
					{
						stack.Push(expectedCloser);
					}
				}
				else
				{
					// Check if character is a defined closer (and not also an opener).
					bool isCloser = false;
					foreach (char closer in closers)
					{
						if (c == closer && Array.IndexOf(openers, c) < 0)
						{
							isCloser = true;
							break;
						}
					}

					// If a valid closing character was found.
					if (isCloser)
					{
						// If stack is empty (no matching opener) OR the popped opener's
						// expected closer doesn't match this closer.
						if (stack.Count == 0 || stack.Pop() != c)
							return false; // Invalid, mismatched or unmatched closer.
					}
				}
			}

			// If all brackets are properly nested and closed, the stack should be empty at the end.
			// I.e., if there are any unclosed parentheses then stack.Count will be > 0.
			return stack.Count == 0;
		}
	}
}
