#if !NETSTANDARD2_0 && !NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace Elephant.Types
{
	/// <summary>
	/// Using nullable bools are bad practice. This struct fills the gap. Possible value: <see cref="Value.Unknown"/>, <see cref="Value.False"/>, <see cref="Value.True"/>.
	/// </summary>
	/// <remarks>Defaults to <see cref="Value.Unknown"/>.</remarks>
	public struct Trilean
	{
		/// <summary>
		/// Trilean value.
		/// </summary>
		public enum Value
		{
			/// <summary>
			/// Unknown (default value).
			/// </summary>
			Unknown = 0,

			/// <summary>
			/// False.
			/// </summary>
			False = 1,

			/// <summary>
			/// True.
			/// </summary>
			True = 2,
		}

		/// <summary>
		/// Get the currently assigned value.
		/// </summary>
		public Value GetValue { get; private set; }

		/// <summary>
		/// Returns true if the value equals <see cref="Value.Unknown"/>.
		/// </summary>
		public readonly bool IsUnknown => GetValue == Value.Unknown;

		/// <summary>
		/// Returns true if the value doesn't equals <see cref="Value.Unknown"/>.
		/// </summary>
		public readonly bool IsNotUnknown => GetValue != Value.Unknown;

		/// <summary>
		/// Returns true if the value equals <see cref="Value.False"/>.
		/// </summary>
		public readonly bool IsFalse => GetValue == Value.False;

		/// <summary>
		/// Returns true if the value doesn't equals <see cref="Value.False"/>.
		/// </summary>
		public readonly bool IsNotFalse => GetValue != Value.False;

		/// <summary>
		/// Returns true if the value equals <see cref="Value.True"/>.
		/// </summary>
		public readonly bool IsTrue => GetValue == Value.True;

		/// <summary>
		/// Returns true if the value doesn't equals <see cref="Value.True"/>.
		/// </summary>
		public readonly bool IsNotTrue => GetValue != Value.True;

		/// <summary>
		/// Returns true if the value is <see cref="Value.Unknown"/> or <see cref="Value.False"/>.
		/// </summary>
		public readonly bool IsFalseOrUnknown => GetValue == Value.Unknown || GetValue == Value.False;

		/// <summary>
		/// Returns true if the value is <see cref="Value.Unknown"/> or <see cref="Value.True"/>.
		/// </summary>
		public readonly bool IsTrueOrUnknown => GetValue == Value.Unknown || GetValue == Value.True;

		/// <summary>
		/// Returns true if the value is <see cref="Value.False"/> or <see cref="Value.True"/>.
		/// </summary>
		public readonly bool IsTrueOrFalse => GetValue == Value.False || GetValue == Value.True;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="value">The <see cref="Value"/> to initialize this <see cref="Trilean"/> with.</param>
		public Trilean(Value value)
		{
			GetValue = value;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="value">The value to initialize this <see cref="Trilean"/> with. Use a null-value to initialize with <see cref="Value.Unknown"/>.</param>
		public Trilean(bool? value)
		{
			switch (value)
			{
				case null:
					GetValue = Value.Unknown;
					break;
				case false:
					GetValue = Value.False;
					break;
				case true:
					GetValue = Value.True;
					break;
			}
		}

		/// <summary>
		/// Assign the specified <paramref name="newValue"/> to this <see cref="Trilean"/>.
		/// </summary>
		/// <param name="newValue">The <see cref="Value"/> to assign to this <see cref="Trilean"/> with.</param>
		public void SetValue(Value newValue)
		{
			GetValue = newValue;
		}

		/// <summary>
		/// Assign the specified <paramref name="newValue"/> to this <see cref="Trilean"/> Use a null-value to assign it to <see cref="Value.Unknown"/>.
		/// </summary>
		/// <param name="newValue">The <see cref="Value"/> to assign to this <see cref="Trilean"/> with.</param>
		public void SetValue(bool? newValue)
		{
			switch (newValue)
			{
				case null:
					GetValue = Value.Unknown;
					break;
				case false:
					GetValue = Value.False;
					break;
				case true:
					GetValue = Value.True;
					break;
			}
		}

		/// <summary>
		/// Assign the <see cref="Value.Unknown"/> value to this <see cref="Trilean"/>.
		/// </summary>
		public void AssignUnknown()
		{
			GetValue = Value.Unknown;
		}

		/// <summary>
		/// Assign the <see cref="Value.False"/> value to this <see cref="Trilean"/>.
		/// </summary>
		public void AssignFalse()
		{
			GetValue = Value.False;
		}

		/// <summary>
		/// Assign the <see cref="Value.True"/> value to this <see cref="Trilean"/>.
		/// </summary>
		public void AssignTrue()
		{
			GetValue = Value.True;
		}

		/// <inheritdoc/>
#if NETSTANDARD2_0 || NETSTANDARD2_1
		public override readonly bool Equals(object? obj)
#else
        public override bool Equals([NotNullWhen(true)] object? obj)
#endif
		{
			if (obj is bool)
			{
#pragma warning disable IDE0020 // Use pattern matching. Reason: clarity.
				bool objAsBool = (bool)obj;
#pragma warning restore IDE0020 // Use pattern matching.
				return (GetValue == Value.True && objAsBool) || (GetValue == Value.False && !objAsBool);
			}

			Trilean? other = obj as Trilean?;
			if (other == null)
			{
				return false;
			}

			return GetValue == other.Value.GetValue;
		}

		/// <inheritdoc/>
		public override readonly int GetHashCode()
		{
			unchecked
			{
				int hash = 17;
				hash *= 23 + (int)GetValue;
				return hash;
			}
		}

		/// <inheritdoc/>
		public override readonly string ToString()
		{
			return GetValue.ToString();
		}

		/// <summary>
		/// Equals.
		/// </summary>
		public static bool operator ==(Trilean left, Trilean right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Not equal.
		/// </summary>
		public static bool operator !=(Trilean left, Trilean right)
		{
			return !(left == right);
		}

		/// <summary>
		/// Returns a new <see cref="Trilean"/> with a <see cref="Value.Unknown"/> value.
		/// </summary>
		public static Trilean Unknown => new(Value.Unknown);

		/// <summary>
		/// Returns a new <see cref="Trilean"/> with a <see cref="Value.False"/> value.
		/// </summary>
		public static Trilean False => new(Value.False);

		/// <summary>
		/// Returns a new <see cref="Trilean"/> with a <see cref="Value.True"/> value.
		/// </summary>
		public static Trilean True => new(Value.True);
	}
}
