using System;
using System.ComponentModel.DataAnnotations;

namespace Elephant.Validation.Attributes
{
    /// <summary>
    /// Specifies that a data field value is required and
    /// that its value is at least a minimum value and
    /// that it has not an overflow value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredEqualOrGreaterThan : RequiredAttribute
    {
        private enum ValueType
        {
            Int,
            Double,
            Float,
        }

        /// <summary>
        /// The minimum allowed value (inclusive).
        /// </summary>
        private object _minValue;

        /// <summary>
        /// The maximum allowed value (inclusive).
        /// </summary>
        private object _maxValue;

        /// <summary>
        /// The type of numeric variable. This should be stored because it's problematic
        /// and sometimes even impossible to differentiate between string doubles and floats.
        /// </summary>
        private ValueType _valueType;

        /// <summary>
        /// Constructor for <see cref="int"/> value.
        /// </summary>
        public RequiredEqualOrGreaterThan(int minValue)
        {
            _minValue = minValue;
            _maxValue = int.MaxValue;
            SetDefaultErrorMessage();
            _valueType = ValueType.Int;
        }

        /// <summary>
        /// Constructor for <see cref="double"/> value.
        /// </summary>
        public RequiredEqualOrGreaterThan(double minValue)
        {
            _minValue = minValue;
            _maxValue = double.MaxValue;
            SetDefaultErrorMessage();
            _valueType = ValueType.Double;
        }

        /// <summary>
        /// Constructor for <see cref="float"/> value.
        /// </summary>
        public RequiredEqualOrGreaterThan(float minValue)
        {
            _minValue = minValue;
            _maxValue = float.MaxValue;
            SetDefaultErrorMessage();
            _valueType = ValueType.Float;
        }

        /// <summary>
        /// Assigns the default <see cref="RequiredEqualOrGreaterThan.ErrorMessage"/>.
        /// </summary>
        private void SetDefaultErrorMessage()
        {
            ErrorMessage = "Value {0} is not between {1} and {2}.";
        }

        /// <summary>
        /// Between check <![CDATA[min <= value <= max]]> (inclusive).
        /// </summary>
        private static bool IsBetweenII<T>(T value, T min, T max) where T : IComparable<T>
        {
            return (min.CompareTo(value) <= float.Epsilon) && (value.CompareTo(max) <= float.Epsilon);
        }

        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!base.IsValid(value))
            {
                return base.IsValid(value, validationContext); // TODO: This double call is not great for performance.
            }

            switch (_valueType)
            {
                case ValueType.Int:
                    return IsValidRangeInt(value) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                case ValueType.Double:
                    return IsValidRangeDouble(value) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                case ValueType.Float:
                    return IsValidRangeFloat(value) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                default:
                    return new ValidationResult($"Value {value} is of type {value.GetType().FullName} but only double, float and int are supported.");
            }
        }

        private bool IsValidRangeInt(object intAsObject)
        {
            if (int.TryParse(intAsObject.ToString(), out int i) &&
                int.TryParse(_minValue.ToString(), out int minValueInt) &&
                int.TryParse(_maxValue.ToString(), out int maxValueInt))
            {
                return i >= minValueInt && i <= maxValueInt;
            }

            return false;
        }

        private bool IsValidRangeDouble(object doubleAsObject)
        {
            if (double.TryParse(doubleAsObject.ToString(), out double d) &&
                double.TryParse(_minValue.ToString(), out double minValueDouble) &&
                double.TryParse(_maxValue.ToString(), out double maxValueDouble))
            {
                return (double.MaxValue - d <= double.Epsilon) || d >= double.PositiveInfinity || IsBetweenII(d, minValueDouble, maxValueDouble);
            }

            return false;
        }

        private bool IsValidRangeFloat(object floatAsObject)
        {
            if (float.TryParse(floatAsObject.ToString(), out float f) &&
                float.TryParse(_minValue.ToString(), out float minValueFloat) &&
                float.TryParse(_maxValue.ToString(), out float maxValueFloat))
            {
                return (float.MaxValue - f <= float.Epsilon) || f >= float.PositiveInfinity || IsBetweenII(f, minValueFloat, maxValueFloat);
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool IsValid(object value)
        {
            if (!base.IsValid(value))
            {
                return false;
            }

            switch (_valueType)
            {
                case ValueType.Int:
                    return IsValidRangeInt(value);
                case ValueType.Double:
                    return IsValidRangeDouble(value);
                case ValueType.Float:
                    return IsValidRangeFloat(value);
                default:
                    return false;
            }
        }

        // Just copy the entire range into here: https://github.com/microsoft/referencesource/blob/master/System.ComponentModel.DataAnnotations/DataAnnotations/RangeAttribute.cs
        // and then change the max value and the required. Then it should work.
        private void SetupConversion()
        {
            if (this.Conversion == null)
            {
                object minimum = this.Minimum;
                object maximum = this.Maximum;

                if (minimum == null || maximum == null)
                {
                    throw new InvalidOperationException(DataAnnotationsResources.RangeAttribute_Must_Set_Min_And_Max);
                }

                // Careful here -- OperandType could be int or double if they used the long form of the ctor.
                // But the min and max would still be strings.  Do use the type of the min/max operands to condition
                // the following code.
                Type operandType = minimum.GetType();

                if (operandType == typeof(int))
                {
                    this.Initialize((int)minimum, (int)maximum, v => Convert.ToInt32(v, CultureInfo.InvariantCulture));
                }
                else if (operandType == typeof(double))
                {
                    this.Initialize((double)minimum, (double)maximum, v => Convert.ToDouble(v, CultureInfo.InvariantCulture));
                }
                else
                {
                    Type type = this.OperandType;
                    if (type == null)
                    {
                        throw new InvalidOperationException(DataAnnotationsResources.RangeAttribute_Must_Set_Operand_Type);
                    }
                    Type comparableType = typeof(IComparable);
                    if (!comparableType.IsAssignableFrom(type))
                    {
                        throw new InvalidOperationException(
                            String.Format(
                                CultureInfo.CurrentCulture,
                                DataAnnotationsResources.RangeAttribute_ArbitraryTypeNotIComparable,
                                type.FullName,
                                comparableType.FullName));
                    }

                    TypeConverter converter = TypeDescriptor.GetConverter(type);
                    IComparable min = (IComparable)converter.ConvertFromString((string)minimum);
                    IComparable max = (IComparable)converter.ConvertFromString((string)maximum);

                    Func<object, object> conversion = value => (value != null && value.GetType() == type) ? value : converter.ConvertFrom(value);

                    this.Initialize(min, max, conversion);
                }
            }
        }

        private void Initialize(IComparable minimum, IComparable maximum, Func<object, object> conversion)
        {
            if (minimum.CompareTo(maximum) > 0)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, DataAnnotationsResources.RangeAttribute_MinGreaterThanMax, maximum, minimum));
            }

            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Conversion = conversion;
        }

        /// <inheritdoc/>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessage, name, _minValue, _maxValue);
        }
    }
}
