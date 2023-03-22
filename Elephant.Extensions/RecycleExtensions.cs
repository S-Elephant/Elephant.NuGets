namespace Elephant.Extensions
{
	/// <summary>
	/// Extensions that 'recycle' (as in, after they overflow below or above their threshold, they will start over again on the other side).
	/// </summary>
	public static class RecycleExtensions
	{
		/// <summary>
		/// Keeps the value between <paramref name="min"/> and <paramref name="max"/>. If it exceeds max then it'll start over at min again (keeping the overflow value).
		/// </summary>
		/// <example>
		/// A value: 26, min: 5, max: 10, results in a value of 6 (5 because of the min-value plus 1 from the overflow).
		/// </example>
		/// <param name="value">The value to recycle.</param>
		/// <param name="max">The maximum value (inclusive).</param>
		/// <param name="min">The minimum value (inclusive).</param>
		/// <returns>
		/// The recycled value.
		/// If <paramref name="min"/> is >= than <paramref name="max"/> then it will always return <paramref name="min"/>.
		/// </returns>
		public static int Recycle(this int value, int max, int min = 0)
		{
			if (min >= max || value < min)
				return min;

			int delta = max - min;
			int result = value;

			while (result > max)
				result -= delta;

			return result;
		}

		/// <summary>
		/// Keeps the value between 1 and <paramref name="max"/>. If it exceeds max then it'll start over at min again (keeping the overflow value).
		/// </summary>
		/// <param name="value">The value to recycle.</param>
		/// <param name="max">The maximum value (inclusive).</param>
		/// <returns>
		/// The recycled value.
		/// If <paramref name="max"/> is <![CDATA[<]]>= than 1 then it will always return 1.
		/// </returns>
		public static int RecycleOne(this int value, int max)
		{
			if (max <= 1 || value < 1)
				return 1;

			int delta = max - 1;
			int result = value;

			while (result > max)
				result -= delta;

			return result;
		}

		/// <summary>
		/// Keeps the value between <paramref name="min"/> and <paramref name="max"/>. If it exceeds max then it'll start over at min again (keeping the overflow value).
		/// </summary>
		/// <example>
		/// A value: 26, min: 5, max: 10, results in a value of 6 (5 because of the min-value plus 1 from the overflow).
		/// </example>
		/// <param name="value">The value to recycle.</param>
		/// <param name="max">The maximum value (inclusive).</param>
		/// <param name="min">The minimum value (inclusive).</param>
		/// <returns>
		/// The recycled value.
		/// If <paramref name="min"/> is >= than <paramref name="max"/> then <paramref name="min"/> will be returned.
		/// If <paramref name="value"/> is null then null will be returned.
		/// </returns>
		public static int? Recycle(this int? value, int max, int min = 0)
		{
			if (value == null)
				return null;

			if (min >= max || value < min)
				return min;

			int delta = max - min;
			int? result = value;

			while (result > max)
				result -= delta;

			return result;
		}

		/// <summary>
		/// Keeps the value between 1 and <paramref name="max"/>. If it exceeds max then it'll start over at min again (keeping the overflow value).
		/// </summary>
		/// <param name="value">The value to recycle.</param>
		/// <param name="max">The maximum value (inclusive).</param>
		/// <returns>
		/// The recycled value.
		/// If <paramref name="value"/> is null then null will be returned.
		/// If <paramref name="max"/> is <![CDATA[<]]>= 1 then 1 will be returned.
		/// </returns>
		public static int? RecycleOne(this int? value, int max)
		{
			if (value == null)
				return null;

			if (max <= 1 || value < 1)
				return 1;

			int delta = max - 1;
			int? result = value;

			while (result > max)
				result -= delta;

			return result;
		}
	}
}
