#if !NETSTANDARD2_0 && !NETSTANDARD2_1
using System.Diagnostics.CodeAnalysis;
#endif

namespace Elephant.Common
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
        /// The currently assigned value.
        /// </summary>
        private Value _value;

        /// <summary>
        /// Get the currently assigned value.
        /// </summary>
        public Value GetValue => _value;

        /// <summary>
        /// Returns true if the value equals <see cref="Value.Unknown"/>.
        /// </summary>
        public bool IsUnknown => _value == Value.Unknown;

        /// <summary>
        /// Returns true if the value doesn't equals <see cref="Value.Unknown"/>.
        /// </summary>
        public bool IsNotUnknown => _value != Value.Unknown;

        /// <summary>
        /// Returns true if the value equals <see cref="Value.False"/>.
        /// </summary>
        public bool IsFalse => _value == Value.False;

        /// <summary>
        /// Returns true if the value doesn't equals <see cref="Value.False"/>.
        /// </summary>
        public bool IsNotFalse => _value != Value.False;

        /// <summary>
        /// Returns true if the value equals <see cref="Value.True"/>.
        /// </summary>
        public bool IsTrue => _value == Value.True;

        /// <summary>
        /// Returns true if the value doesn't equals <see cref="Value.True"/>.
        /// </summary>
        public bool IsNotTrue => _value != Value.True;

        /// <summary>
        /// Returns true if the value is <see cref="Value.Unknown"/> or <see cref="Value.False"/>.
        /// </summary>
        public bool IsFalseOrUnknown => _value == Value.Unknown || _value == Value.False;

        /// <summary>
        /// Returns true if the value is <see cref="Value.Unknown"/> or <see cref="Value.True"/>.
        /// </summary>
        public bool IsTrueOrUnknown => _value == Value.Unknown || _value == Value.True;

        /// <summary>
        /// Returns true if the value is <see cref="Value.False"/> or <see cref="Value.True"/>.
        /// </summary>
        public bool IsTrueOrFalse => _value == Value.False || _value == Value.True;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">The <see cref="Value"/> to initialze this <see cref="Trilean"/> with.</param>
        public Trilean(Value value)
        {
            _value = value;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">The value to initialze this <see cref="Trilean"/> with. Use a null-value to initialize with <see cref="Value.Unknown"/>.</param>
        public Trilean(bool? value)
        {
            switch (value)
            {
                case null:
                    _value = Value.Unknown;
                    break;
                case false:
                    _value = Value.False;
                    break;
                case true:
                    _value = Value.True;
                    break;
            }
        }

        /// <summary>
        /// Asigns the specified <paramref name="newValue"/> to this <see cref="Trilean"/>.
        /// </summary>
        /// <param name="newValue">The <see cref="Value"/> to assign to this <see cref="Trilean"/> with.</param>
        public void SetValue(Value newValue) => _value = newValue;

        /// <summary>
        /// Asigns the specified <paramref name="newValue"/> to this <see cref="Trilean"/> Use a null-value to assign it to <see cref="Value.Unknown"/>.
        /// </summary>
        /// <param name="newValue">The <see cref="Value"/> to assign to this <see cref="Trilean"/> with.</param>
        public void SetValue(bool? newValue)
        {
            switch (newValue)
            {
                case null:
                    _value = Value.Unknown;
                    break;
                case false:
                    _value = Value.False;
                    break;
                case true:
                    _value = Value.True;
                    break;
            }
        }

        /// <summary>
        /// Asigns the <see cref="Value.Unknown"/> value to this <see cref="Trilean"/>.
        /// </summary>
        public void AssignUnknown() => _value = Value.Unknown;

        /// <summary>
        /// Asigns the <see cref="Value.False"/> value to this <see cref="Trilean"/>.
        /// </summary>
        public void AssignFalse() => _value = Value.False;

        /// <summary>
        /// Asigns the <see cref="Value.True"/> value to this <see cref="Trilean"/>.
        /// </summary>
        public void AssignTrue() => _value = Value.True;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
#if NETSTANDARD2_0 || NETSTANDARD2_1
        public override bool Equals(object? obj)
#else
        public override bool Equals([NotNullWhen(true)] object? obj)
#endif
        {
            if (obj is bool)
            {
                bool objAsBool = (bool)obj;
                return (_value == Value.True && objAsBool) || (_value == Value.False && !objAsBool);
            }

            Trilean? other = obj as Trilean?;
            if (other == null)
            {
                return false;
            }

            return _value == other.Value.GetValue;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash *= 23 + (int)_value;
                return hash;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string ToString()
        {
            return _value.ToString();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public static bool operator ==(Trilean left, Trilean right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// <inheritdoc/>
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
