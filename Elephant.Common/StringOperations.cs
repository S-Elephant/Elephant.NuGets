namespace Elephant.Common
{
    /// <summary>
    /// String operations.
    /// </summary>
    public static class StringOperations
    {
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
            if (stringsToCombine == null)
                return string.Empty;

            List<string> strippedStringsToCombine = new();
            for (int i = 0; i < stringsToCombine.Length; i++)
            {
                if (stringsToCombine[i] != null)
                    strippedStringsToCombine.Add(stringsToCombine[i]!.Trim(separatorChar));
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
    }
}
