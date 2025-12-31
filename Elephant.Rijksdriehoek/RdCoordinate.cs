using System;
#if !NETSTANDARD2_0 && !NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace Elephant.Rijksdriehoek
{
	/// <summary>
	/// A struct for Rijksdriehoekscoördinaten using <see cref="float"/>.
	/// </summary>
	[Serializable]
	public struct RdCoordinate : IRdCoordinateFloat
	{
		/// <inheritdoc />
		public readonly bool IsValid => MathRd.IsValidRdCoordinate(X, Y);

		/// <inheritdoc />
		public float X { get; set; }

		/// <inheritdoc />
		public float Y { get; set; }

		/// <summary>
		/// Constructor.
		/// </summary>
		public RdCoordinate()
		{
			X = Y = 0;
		}

		/// <summary>
		/// Constructor with <paramref name="x"/> and <paramref name="y"/> initializers.
		/// </summary>
		/// <param name="x">X Rd coordinate.</param>
		/// <param name="y">Y Rd coordinate.</param>
		public RdCoordinate(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <inheritdoc />
		public readonly float Distance(IRdCoordinate<float> otherRdCoordinate)
		{
			return MathRd.Distance(X, Y, otherRdCoordinate.X, otherRdCoordinate.Y);
		}

		/// <inheritdoc />
		public readonly float Distance(RdCoordinate? otherRdCoordinate)
		{
			if (otherRdCoordinate == null)
				return 0f;

			return MathRd.Distance(X, Y, otherRdCoordinate.Value.X, otherRdCoordinate.Value.Y);
		}

		/// <inheritdoc cref="MathRd.TryParseFromPointString(string, out float, out float)"/>
		[Obsolete("Use https://github.com/NetTopologySuite/NetTopologySuite instead.")]
		public static RdCoordinate? TryParseFromPointString(string pointString)
		{
			if (MathRd.TryParseFromPointString(pointString, out float x, out float y))
				return new RdCoordinate(x, y);

			return null;
		}

		/// <summary>
		/// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a + b.
		/// </summary>
		public static RdCoordinate operator +(RdCoordinate a, RdCoordinate b)
		{
			return new(a.X + b.X, a.Y + b.Y);
		}

		/// <summary>
		/// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a - b.
		/// </summary>
		public static RdCoordinate operator -(RdCoordinate a, RdCoordinate b)
		{
			return new(a.X - b.X, a.Y - b.Y);
		}

		/// <summary>
		/// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a * b.
		/// </summary>
		public static RdCoordinate operator *(RdCoordinate a, RdCoordinate b)
		{
			return new(a.X * b.X, a.Y * b.Y);
		}

		/// <summary>
		/// Creates and returns a new <see cref="RdCoordinate"/> that is the result of a / b.
		/// </summary>
		public static RdCoordinate operator /(RdCoordinate a, RdCoordinate b)
		{
			if (b.X == 0f || b.Y == 0f)
				throw new DivideByZeroException($"Cannot divide {a} by {b}. Both b.X and b.Y cannot be zero.");

			return new(a.X / b.X, a.Y / b.Y);
		}

		/// <summary>
		/// Check if the two <see cref="RdCoordinate"/>s are equal.
		/// </summary>
		public static bool operator ==(RdCoordinate left, RdCoordinate right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Check if the two <see cref="RdCoordinate"/>s are not equal.
		/// </summary>
		public static bool operator !=(RdCoordinate left, RdCoordinate right)
		{
			return !(left == right);
		}

#if NETSTANDARD2_0 || NETSTANDARD2_1
		/// <inheritdoc cref="object.Equals(object)"/>
		public override readonly bool Equals(object? obj)
#else
		/// <inheritdoc/>
		public override bool Equals([NotNullWhen(true)] object? obj)
#endif
		{
			RdCoordinate? other = obj as RdCoordinate?;
			if (other == null)
				return false;

#if NETSTANDARD2_0 || NETSTANDARD2_1
			float tempResult = X - other.Value.X;
			return (tempResult >= 0 ? tempResult : -tempResult) <= float.Epsilon;
#else
			return MathF.Abs(X - other.Value.X) <= float.Epsilon;
#endif
		}

		/// <inheritdoc cref="object.GetHashCode"/>
		public override readonly int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + X.GetHashCode();
				hash *= 23 + Y.GetHashCode();
				return hash;
			}
		}

		/// <inheritdoc />
		public override readonly string ToString()
		{
			return $"{X},{Y}";
		}
	}
}
