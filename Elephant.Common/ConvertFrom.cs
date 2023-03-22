namespace Elephant.Common
{
	/// <summary>
	/// Converter helper.
	/// </summary>
	public static class ConvertFrom
	{
		/// <summary>
		/// New line delimiters.
		/// </summary>
		private static readonly string[] NewLineDelimiters = { "\r\n", "\n", "\r" };

		/// <summary>
		/// Converts the <see cref="Environment.NewLine"/>, rn and/or n separated string <paramref name="newLineValues"/> into a <see cref="List{T}"/>.
		/// </summary>
		/// <param name="newLineValues"><see cref="string"/> with values separated by newlines.</param>
		/// <param name="stringSplitOptions"><see cref="StringSplitOptions"/>.</param>
		/// <returns><see cref="List{T}"/> that was separated using new lines.</returns>
		public static List<string> NewLineStringToStringList(string? newLineValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
		{
			if (newLineValues == null)
				return new List<string>();

			return newLineValues.Split(NewLineDelimiters, stringSplitOptions).ToList();
		}

		/// <summary>
		/// Converts the semi-colon separated string <paramref name="semiColonValues"/> into a <see cref="List{T}"/>.
		/// </summary>
		/// <param name="semiColonValues">Semi-colon separated string.</param>
		/// <param name="stringSplitOptions"><see cref="StringSplitOptions"/>.</param>
		/// <returns><see cref="List{T}"/> that was separated using semi-colons.</returns>
		public static List<string> SemiColonStringToStringList(string? semiColonValues, StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries)
		{
			if (semiColonValues == null)
				return new List<string>();

			return semiColonValues.Split(';', stringSplitOptions).ToList();
		}

		/// <summary>
		/// Convert using <see cref="Environment.NewLine"/>.
		/// </summary>
		/// <param name="list">The list to convert from.</param>
		/// <returns>Single string, separated by <see cref="Environment.NewLine"/>s.</returns>
		public static string StringListToNewLineString(IList<string>? list)
		{
			if (list == null)
				return string.Empty;

			return string.Join(Environment.NewLine, list);
		}

		/// <summary>
		/// Convert <paramref name="list"/> into a semi-colon separated <see cref="string"/>.
		/// </summary>
		/// <param name="list">The list to convert from.</param>
		/// <returns>Semi-colon separated <see cref="string"/>. For example: "a;b;c;d".</returns>
		public static string StringListToSemiColonString(IList<string>? list)
		{
			if (list == null)
				return string.Empty;

			return string.Join(';', list);
		}
	}
}
