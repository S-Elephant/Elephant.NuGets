using System;
using System.Globalization;

namespace Elephant.Texts
{
	/// <summary>
	/// Text truncation helpers.
	/// </summary>
	public static class Truncator
	{
		/// <summary>
		/// Truncates <paramref name="value"/> to at most <paramref name="maxLength"/> characters and appends <paramref name="ellipsis"/> when truncation occurs.
		/// Returns null if <paramref name="value"/> is null.
		/// If <paramref name="maxLength"/> is less than or equal to zero an empty string is returned.
		/// If <paramref name="maxLength"/> is less than the length of <paramref name="ellipsis"/> (measured in text elements) the ellipsis is truncated to fit.
		/// This implementation avoids splitting Unicode text elements (grapheme clusters) when truncating and measures both <paramref name="value"/> and <paramref name="ellipsis"/> by text elements for consistent Unicode handling.
		/// When <paramref name="allowGraphemeOverflow"/> is true the method may return a string longer than <paramref name="maxLength"/> (measured in UTF-16 code units) to avoid splitting a grapheme cluster; when false the returned string will be less than or equal to <paramref name="maxLength"/> (in UTF-16 code units).
		/// </summary>
		/// <param name="value">Input string to truncate or null.</param>
		/// <param name="maxLength">Maximum allowed length of the returned string including ellipsis when applied (measured in UTF-16 code units).</param>
		/// <param name="ellipsis">String to append when truncation occurs. Defaults to "..".</param>
		/// <param name="allowGraphemeOverflow">If true, preserve grapheme clusters even if the returned string exceeds <paramref name="maxLength"/>; if false (default) ensure returned string length is less than or equal to <paramref name="maxLength"/> (in UTF-16 code units).</param>
		/// <returns>Truncated string, original string when no truncation required, or null if <paramref name="value"/> is null.</returns>
		public static string? TruncateWithEllipsis(string? value, int maxLength, string ellipsis = "..", bool allowGraphemeOverflow = false)
		{
			// Null handling.
			if (value == null)
				return null;

			// Non-positive max length returns empty string.
			if (maxLength <= 0)
				return string.Empty;

			// Default ellipsis to empty string if null.
			ellipsis ??= string.Empty;

			int ellipsisLen = ellipsis.Length;

			// If the ellipsis itself is longer than or equal to maxLength, return a truncated ellipsis.
			if (ellipsisLen >= maxLength)
				return ellipsis.Substring(0, maxLength);

			// Number of UTF-16 code units we can keep from the original string before appending ellipsis.
			int keep = maxLength - ellipsisLen;
			if (keep <= 0)
				return ellipsis.Substring(0, maxLength);

			// If the value fits within keep (no truncation needed), return original.
			if (value.Length <= keep)
				return value;

			// Use TextElementEnumerator to iterate text elements without allocating the full index array.
			TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(value);
			int totalConsumedChars = 0;

			while (enumerator.MoveNext())
			{
				string element = (string)enumerator.Current;
				int elemLen = element.Length;

				// If adding this element does not exceed keep, continue.
				if (totalConsumedChars + elemLen <= keep)
				{
					totalConsumedChars += elemLen;
					continue;
				}

				// This element would cross the keep boundary.
				if (allowGraphemeOverflow)
				{
					// Include the whole element to avoid splitting grapheme.
					return value.Substring(0, totalConsumedChars + elemLen) + ellipsis;
				}
				else
				{
					// Do not include this element; strict mode.
					return value.Substring(0, totalConsumedChars) + ellipsis;
				}
			}

			// Fallback: consumed all text elements without hitting boundary.
			// This means value fits completely within keep (shouldn't happen due to earlier length check).
			return value.Substring(0, Math.Min(totalConsumedChars, keep)) + ellipsis;
		}
	}
}
