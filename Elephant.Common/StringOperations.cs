using System.Globalization;

namespace Elephant.Common
{
	/// <summary>
	/// String operations.
	/// </summary>
	public static class StringOperations
	{
		/// <summary>
		/// Returns a new string with the first character being capitalized.
		/// This will not lower everything else and it will not capitalize the first character of every string.
		/// If <paramref name="stringToCapitalize"/> is an empty string then it will return just that.
		/// </summary>
		public static string CapitalizeFirstChar(string stringToCapitalize)
		{
			if (stringToCapitalize.Length > 1)
				return char.ToUpper(stringToCapitalize[0]) + stringToCapitalize.Substring(1);

			return stringToCapitalize.ToUpper();
		}

		/// <summary>
		/// Returns a new string with the first character being capitalized.
		/// This will not lower everything else and it will not capitalize the first character of every string.
		/// If <paramref name="stringToCapitalize"/> is null or an empty string then it will return just that.
		/// </summary>
		public static string? CapitalizeFirstCharNullable(string? stringToCapitalize)
		{
			if (stringToCapitalize == null)
				return null;

			return CapitalizeFirstChar(stringToCapitalize);
		}

		/// <summary>
		/// Encloses <paramref name="value"/> with <paramref name="encloser"/>. Will do nothing if its already encased in <paramref name="encloser"/>.
		/// </summary>
		public static string EncloseByIfNotAlready(string value, char encloser)
		{
			if (value[0] != encloser)
			{
				if (value.Last() != encloser)
					return $"{encloser}{value}{encloser}";
				return encloser + value;
			}

			if (value.Last() != encloser)
				return value + encloser;

			return value;
		}

		/// <summary>
		/// Join strings together using a separator.
		/// There's no leading and no trailing separator.
		/// When joining, it will prevent double adjoining separators at the join-spots.
		/// </summary>
		/// <param name="separatorChar">The separator character.</param>
		/// <param name="stringsToCombine">The strings to combine.</param>
		/// <returns>The joined string or an empty string if there was nothing to join.</returns>
		public static string Join(char separatorChar, params string?[] stringsToCombine)
		{
			if (!stringsToCombine.Any())
				return string.Empty;

			List<string> strippedStringsToCombine = new();
			for (int i = 0; i < stringsToCombine.Length; i++)
			{
				if (stringsToCombine[i] != null)
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
					strippedStringsToCombine.Add(stringsToCombine[i]!.Trim(separatorChar));
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
			}

			return string.Join(separatorChar, strippedStringsToCombine);
		}

		/// <summary>
		/// Join strings together using a separator.
		/// There's a leading separator but no trailing separator.
		/// When joining, it will prevent double adjoining separators at the join-spots.
		/// </summary>
		/// <param name="separatorChar">The separator character.</param>
		/// <param name="stringsToCombine">The strings to combine.</param>
		/// <returns>The joined string or just a separator if there was nothing to join.</returns>
		public static string JoinWithLeading(char separatorChar, params string?[] stringsToCombine)
		{
			return $"{separatorChar}{Join(separatorChar, stringsToCombine)}";
		}

		/// <summary>
		/// Join strings together using a separator.
		/// There's no leading separator but there's a trailing separator.
		/// When joining, it will prevent double adjoining separators at the join-spots.
		/// </summary>
		/// <param name="separatorChar">The separator character.</param>
		/// <param name="stringsToCombine">The strings to combine.</param>
		/// <returns>The joined string or just a separator if there was nothing to join.</returns>
		public static string JoinWithTrailing(char separatorChar, params string?[] stringsToCombine)
		{
			return $"{Join(separatorChar, stringsToCombine)}{separatorChar}";
		}

		/// <summary>
		/// Join strings together using a separator.
		/// There's both a leading and a trailing separator.
		/// When joining, it will prevent double adjoining separators at the join-spots.
		/// </summary>
		/// <param name="separatorChar">The separator character.</param>
		/// <param name="stringsToCombine">The strings to combine.</param>
		/// <returns>The joined string or a double separator if there was nothing to join.</returns>
		public static string JoinWithLeadingAndTrailing(char separatorChar, params string?[] stringsToCombine)
		{
			return $"{separatorChar}{Join(separatorChar, stringsToCombine)}{separatorChar}";
		}

		/// <summary>
		/// Remove <paramref name="substringToRemove"/> from <paramref name="source"/>.
		/// Does nothing if <paramref name="source"/> does not contain the <paramref name="substringToRemove"/>.
		/// Is case-sensitive and removes only the first occurance.
		/// </summary>
		public static string RemoveSubstringFromString(string source, string substringToRemove)
		{
			int index = source.IndexOf(substringToRemove, StringComparison.Ordinal);

			return index < 0 ? source : source.Remove(index, substringToRemove.Length);
		}

		/// <summary>
		/// Removes all <paramref name="substringsToRemove"/> from <paramref name="source"/>.
		/// Does nothing if <paramref name="source"/> does not contain the <paramref name="substringsToRemove"/>.
		/// Is case-sensitive and removes only the first occurance for each <paramref name="substringsToRemove"/> item.
		/// </summary>
		public static string RemoveSubstringsFromString(string source, IEnumerable<string> substringsToRemove)
		{
			foreach (string substringToRemove in substringsToRemove)
				source = RemoveSubstringFromString(source, substringToRemove);

			return source;
		}

		/// <summary>
		/// All new line delimiters.
		/// </summary>
		private static readonly string[] NewLineDelimiters = { "\r\n", "\n", "\r" };

		/// <summary>
		/// Split by new line.
		/// </summary>
		/// <param name="value">The value to split.</param>
		/// <param name="stringSplitOptions"><see cref="StringSplitOptions"/></param>
		/// <returns><paramref name="value"/> split by new lines using <paramref name="stringSplitOptions"/>.</returns>
		public static IEnumerable<string> SplitByNewLine(string value, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
		{
			return value.Split(NewLineDelimiters, stringSplitOptions);
		}

		/// <summary>
		/// Title-cases <paramref name="stringToTitleCase"/>.
		/// If <paramref name="stringToTitleCase"/> is an empty string then it will return just that.
		/// </summary>
		/// <param name="stringToTitleCase">String to title-case.</param>
		/// <returns>Title-cased string. For example: Turns "the LONG white dog." into "The Long White Dog.".</returns>
		/// <example>Turns "the long white dog." into "The LONG White Dog.".</example>
		public static string ToTitleCase(string stringToTitleCase)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringToTitleCase.ToLower());
		}

		/// <summary>
		/// Title-cases <paramref name="stringToTitleCase"/>.
		/// If <paramref name="stringToTitleCase"/> is null or an empty string then it will return just that.
		/// </summary>
		/// <param name="stringToTitleCase">String to title-case.</param>
		/// <returns>Title-cased string. For example: Turns "the LONG white dog." into "The Long White Dog.". Returns null if <paramref name="stringToTitleCase"/> is null.</returns>
		/// <example>Turns "the long white dog." into "The LONG White Dog.".</example>
		public static string? ToTitleCaseNullable(string? stringToTitleCase)
		{
			if (stringToTitleCase == null)
				return null;

			return ToTitleCase(stringToTitleCase);
		}
	}
}
